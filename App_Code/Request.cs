using System.Collections;
using System.Xml;
using Com.VerySimple.Util;

namespace Affinity
{
	/// <summary>
	/// Business Logic For Request Class
	/// </summary>
	public partial class Request
	{

		/// <summary>
		/// Parameterless constructor is here only so the object can be
		/// serialized for web services.  do not use this constructor!
		/// </summary>
		public Request() : base() { }

		private Hashtable xmlHashCache;

		/// <summary>
		/// Returns a value for a specific field in the Data XML.  If the field isn't defined, then
		/// empty string is returned
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string GetDataValue(string key)
		{
			if (this.xmlHashCache == null)
			{
				this.xmlHashCache = this.GetTranslatedHashTable("name", false, false);
			}

			return this.xmlHashCache.ContainsKey(key) ? this.xmlHashCache[key].ToString() : "";
		}

		/// <summary>
		/// If this is a change request, returns the previous request of the same type.  This
		/// is useful for comparing the difference when a change is submitted.  If no previous
		/// request exists, then null is returned
		/// </summary>
		/// <returns>Affinity.Request || null</returns>
		public Affinity.Request GetPreviousRequest()
		{
			RequestCriteria rc = new RequestCriteria();
			rc.RequestTypeCode = this.RequestTypeCode;
			rc.IdLessThan = this.Id;

			rc.AppendToOrderBy("Id", true);

			Affinity.Requests rs = this.Order.GetOrderRequests(rc);
			Affinity.Request prev = null;

			if (rs.Count > 0)
			{
				prev = (Affinity.Request)rs[0];
			}

			return prev;

		}

		public Hashtable GetDifference()
		{
			return this.GetDifference("name");
		}

		/// <summary>
		/// Returns a Hashtable with the fields that have been updated since the previous request.
		/// The Hashtable key is the fieldname and the value is a string description of the change
		/// for this order
		/// </summary>
		/// <param name="keyAttribute"></param>
		/// <returns>Hashtable</returns>
		/// <returns></returns>
		public Hashtable GetDifference(string keyAttribute)
		{
			Affinity.Request prev = this.GetPreviousRequest();
			Hashtable ht = new Hashtable();

			if (prev != null)
			{
				Hashtable cHt = this.GetTranslatedHashTable(keyAttribute, true, true);
				Hashtable pHt = prev.GetTranslatedHashTable(keyAttribute, true, true);

				foreach (string key in cHt.Keys)
				{
					if (pHt.ContainsKey(key))
					{
						if (!pHt[key].Equals(cHt[key]))
						{
							ht.Add(key, "CHANGED FROM: '" + pHt[key].ToString() + "' TO: '" + cHt[key].ToString() + "'" );
						}
					}
					else
					{
						ht.Add(key, "NEW: '" + cHt[key].ToString() + "'" );
					}
				}
			}

			return ht;
		}

		/// <summary>
		/// Returns a Hashtable with the fields that have been updated since the previous request.
		/// The Hashtable key is the fieldname and the value is the new value 
		/// </summary>
		/// <param name="keyAttribute"></param>
		/// <returns></returns>
		public Hashtable GetNewValues(string keyAttribute)
		{
			Hashtable returnHt = new Hashtable();
			Hashtable ht = this.GetChangePairs(keyAttribute);
			foreach (string key in ht.Keys)
			{
				string[] pair = (string[]) ht[key];
				returnHt.Add(key,pair[1]);
			}

			return returnHt;
		}


		/// <summary>
		/// Returns a Hashtable with the fields that have been updated since the previous request.
		/// The Hashtable key is the fieldname and the value is an array where item[0] is the 
		/// original value and item[1] is the new value
		/// </summary>
		/// <param name="keyAttribute"></param>
		/// <returns>Hashtable</returns>
		public Hashtable GetChangePairs(string keyAttribute)
		{
			Affinity.Request prev = this.GetPreviousRequest();
			Hashtable ht = new Hashtable();

			if (prev != null)
			{
				Hashtable cHt = this.GetTranslatedHashTable(keyAttribute, true, true);
				Hashtable pHt = prev.GetTranslatedHashTable(keyAttribute, true, true);

				foreach (string key in cHt.Keys)
				{
					if (pHt.ContainsKey(key))
					{
						if (!pHt[key].Equals(cHt[key]))
						{
							// changed value
							string[] pair = new string[2] { pHt[key].ToString(), cHt[key].ToString() };
							ht.Add(key, pair);
						}
					}
					else
					{
						// new value that didn't exist before
						string[] pair = new string[2] { "", cHt[key].ToString() };
						ht.Add(key, pair);
					}
				}
			}

			return ht;
		}

		protected override void OnInit()
		{
			this.IsCurrent = true;
		}

		/// <summary>
		/// returns a hashtable with all key/value pairs for this request
		/// </summary>
		/// <param name="keyAttribute">the key attribute used for the hash key (name, sp_id, rei_id, etc)</param>
		/// <param name="includeOrder">true if the order fields should be included</param>
		/// <param name="includeBlankValues">true if blank values should be included</param>
		/// <returns>Hashtable</returns>
		public Hashtable GetTranslatedHashTable(string keyAttribute, bool includeOrder, bool includeBlankValues)
		{
			Hashtable ht = new Hashtable();
			if (keyAttribute.Equals("name"))
			{
				// this is faster because the reponse has the name attribute included
				ht = XmlForm.GetResponseHashtable(this.Xml);
			}
			else
			{
				// this is slower because we need the definition to translate
				ht = XmlForm.GetTranslatedHashtable(this.RequestType.GetDefDocument(), this.GetResponse(), keyAttribute, includeBlankValues);
			}

			// if the order should be included, include it - unless this is
			// the default change code, which contains all of the details of the order
			if (includeOrder && this.RequestTypeCode != Affinity.RequestType.DefaultChangeCode)
			{
				Hashtable oht = this.Order.GetTranslatedHashTable(keyAttribute, includeBlankValues);
				foreach (string key in oht.Keys)
				{
					if (ht.ContainsKey(key))
					{
						if (ht[key].ToString() == oht[key].ToString() || oht[key].ToString() == "")
						{
							// we are inserting the exact same value twice, or else a previous value
							// was entered but the new value is blank.  either way, this will just
							// clutter up the export, so we will ignore the new value
						}
						else if (ht[key].ToString() == "")
						{
							// the key exists, but is empty so don't prepend the comma
							ht[key] = oht[key];
						}
						else
						{
							ht[key] = ht[key].ToString() + ", " + oht[key];
						}
					}
					else
					{
						ht.Add(key, oht[key]);
					}
				}
			}
			return ht;
		}

		/// <summary>
		/// returns an XML Document with all key/value pairs for this request
		/// </summary>
		/// <param name="keyAttribute">the key attribute used for the hash key (name, sp_id, rei_id, etc)</param>
		/// <param name="includeOrder">true if the order fields should be included</param>
		/// <param name="includeBlankValues">true if blank values should be included</param>
		/// <returns>XmlDocument</returns>
		public XmlDocument GetTranslatedResponse(string keyAttribute, bool includeOrder, bool includeBlankValues)
		{
			Hashtable ht = this.GetTranslatedHashTable(keyAttribute, includeOrder, includeBlankValues);

			XmlDocument rXml = new XmlDocument();
			rXml.CreateXmlDeclaration("1.0", "utf-8", null);
			XmlElement rXmlRoot = rXml.CreateElement("response");
			rXml.AppendChild(rXmlRoot);

			ArrayList keys = new ArrayList(ht.Keys);
			keys.Sort();

			foreach (string key in keys)
			{
				if (!key.EndsWith("_validator"))
				{
					rXmlRoot.AppendChild(XmlForm.GetXmlField(rXml, key.ToString(), ht[key].ToString()));
				}
			}

			return rXml;
		}

		/// <summary>
		/// Returns an XML document version of this response exactly as it is saved in the database
		/// </summary>
		/// <returns>XmlDocument</returns>
		public XmlDocument GetResponse()
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(this.Xml);
			return doc;
		}

	}
}