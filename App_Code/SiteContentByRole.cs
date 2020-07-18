using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using MySql.Data.MySqlClient;
using Com.VerySimple.Phreeze;
using Com.VerySimple.Util;
using System.Text;

namespace Affinity
{

	/// <summary>
	/// Business Logic For SiteContentByRole Class
	/// </summary>
	public partial class SiteContentByRole : IXmlFormCallback
	{
		private Hashtable prefCache;

		/// <summary>
		/// Parameterless constructor is here only so the object can be
		/// serialized for web services.  do not use this constructor!
		/// </summary>
		public SiteContentByRole() : base() { }

		protected override void OnInit()
		{
			this.RoleCode = Role.AnonymousCode;
		}

		/// <summary>
		/// 
		/// </summary>
		[XmlIgnore]
		public Hashtable Preferences
		{
			get 
			{
				if (this.prefCache == null)
				{
					//this.prefCache = XmlForm.GetResponseHashtable(this.PreferencesXml);
				}
				return this.prefCache;
			}
		}

		/// <summary>
		/// After updating preferences, clears the cache
		/// </summary>
		public void ClearPreferenceCache()
		{
			this.prefCache = null;
		}

		/// <summary>
		/// Returns the Preference with the key name, if not defined or blank, returns empty string
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string GetPreference(string key)
		{
			return this.GetPreference(key, "");
		}

		/// <summary>
		/// Returns the preference with the key name.  if not defined or blank, returns defaultVal
		/// </summary>
		/// <param name="key"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public string GetPreference(string key, string defaultVal)
		{
			return (this.Preferences[key] == null || this.Preferences[key].Equals("")) ? defaultVal : this.Preferences[key].ToString();
		}


		/// <summary>
		/// Onload is used to hide the password and instantiate the Role
		/// </summary>
		/// <param name="reader"></param>
		protected override void OnLoad(MySqlDataReader reader)
		{
			// instantiate the role
			this.Role = new Role(this.phreezer, reader);
		}

		/// <summary>
		/// This deletes any orders that do not have a request attached to them
		/// for this account.  This is done in the event that the current user
		/// has been using the back button and not completing orders
		/// </summary>
		public void CleanUpOrphanOrders()
		{
			string sql = "select o.* from `order` o"
			+ " left join request r on r.r_order_id = o.o_id"
			+ " where r.r_order_id is null"
			+ " and o.o_originator_id = '" + this.Id + "'";

			using(MySqlDataReader r = this.phreezer.ExecuteReader(sql))
			{
				Orders orders = new Orders(this.phreezer);
				orders.Consume(r);
				r.Close();
	
				foreach (Order o in orders)
				{
					o.Delete();
				}
			}

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="primaryKey"></param>
		/// <returns></returns>
		protected override string GetSelectSql(object pk)
		{
			// load the account and a type at the same time because we'll
			// almost always need it
			return "select * from account a inner join role r on a.a_role_code = r.r_code where a_id = '" + pk.ToString() + "'";
		}

		/// <summary>
		/// Persists updates to the DB.  Note that the password field is not updated.
		/// SetPassword() must be called to set the password
		/// </summary>
		/// <returns></returns>
		protected override string GetUpdateSql()
		{
			StringBuilder sb = new StringBuilder();

			// note password and created are not updated here
			sb.Append("update `account` set");

			sb.Append(" where a_id = '" + Preparer.Escape(this.Id) + "'");

			return sb.ToString();
		}

		/// <summary>
		/// Inserts into the DB. Note that password field is not inserted
		/// SetPassword() must be called to set the password
		/// </summary>
		/// <returns></returns>
		protected override string GetInsertSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("insert into `account` (");

			sb.Append(" ) values (");

			sb.Append(" )");

			return sb.ToString();
		}

		/// <summary>
		/// used for IXmlFormCallback
		/// </summary>
		/// <param name="field"></param>
		/// <param name="param"></param>
		/// <returns></returns>
		public Hashtable GetFormOptions(XmlNode field, string param)
		{
			Hashtable ht = new Hashtable();
/*
			switch (param)
			{
				case "UNDERWRITER":
					Underwriters uw;

					// if ID = 0 this is a new account, so show all underwriters
					if (this.Id == 0)
					{
						uw = new Underwriters(this.phreezer);
						uw.Query(new UnderwriterCriteria());
					}
					else
					{
						uw = this.GetUnderwriters();
					}

					foreach (Underwriter u in uw)
					{
						ht.Add(u.Code, u.Description);
					}
					break;
				default:
					ht.Add("UNKNOWN", "UNKNOWN PARAM: " + param);
					break;
			}
*/
			return ht;
		}

		/// <summary>
		/// used for IXmlFormCallback
		/// </summary>
		/// <param name="field"></param>
		/// <param name="param"></param>
		/// <returns></returns>
		public string GetFormDefault(XmlNode field, string param)
		{
			switch (param)
			{
				case "NOW":
					return DateTime.Now.ToShortDateString();
				default:
					return "UNKNOWN PARAM: " + param;
			}
		}
	}
}
