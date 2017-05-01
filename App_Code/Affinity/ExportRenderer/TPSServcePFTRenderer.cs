using System;
using System.Data;
using System.Collections;
using System.Text;

namespace Affinity.ExportRenderer
{

	/// <summary>
	/// Renders Export Output for SoftPro PFT
	/// </summary>
	public class TPSServicePFTRenderer : BaseRenderer
	{
		// use the base constructor
        public TPSServicePFTRenderer(Affinity.Request req, Hashtable sys) : base(req, sys) { }

        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(TPSServicePFTRenderer));

		public override string GetFileName()
		{
			return this.request.Order.WorkingId + ".pft";
		}

		public override string GetMimeType()
		{
			return "text/plain";
		}

		public override string Render(string keyAttribute)
		{
            /*
			// only include the main order in the export if this is a DefaultCode request type
			bool includeOrder = (this.request.RequestType.Code == RequestType.DefaultCode || this.request.RequestType.Code.Equals("Refinance"));

			Hashtable ht = this.request.GetTranslatedHashTable(keyAttribute, includeOrder, false);
			
			// these have been moved to hidden fields within the order request as of 8/16/07
			// we don't want them for closing and clerking request exports, but for
			// backward compatibility we must verify their existance in order requests
			if (this.request.RequestType.Code == RequestType.DefaultCode || this.request.RequestType.Code.Equals("Refinance"))
			{
				if (ht.ContainsKey("USR40VAL") == false) ht.Add("USR40VAL", this.request.Order.Id.ToString());
				if (ht.ContainsKey("TX01RQDT") == false) ht.Add("TX01RQDT", this.request.Order.Created.ToShortDateString());
				if (ht.ContainsKey("USR40DES") == false) ht.Add("USR40DES", "Web Order ID");
				if (ht.ContainsKey("TX01STAT") == false) ht.Add("TX01STAT", "Requested");

				if (ht.ContainsKey("FIRMCODE") == false) ht.Add("FIRMCODE", this.request.Order.Account.InternalId);
				if (ht.ContainsKey("FIRMNAME") == false) ht.Add("FIRMNAME", this.request.Order.Account.FullName);
			}
			
			// format all the date fields
			FormatDateField(ht, "TX03DUDT");
			FormatDateField(ht, "TX06DUDT");
			FormatDateField(ht, "TX01DUDT");
			FormatDateField(ht, "TX01RQDT");
			FormatDateField(ht, "TX09RQDT");
			FormatDateField(ht, "TX09CRDT");

			// this is a customization for SoftPro in the case of a Chicago order
			string city = ht.ContainsKey("PROPCITY") ? ht["PROPCITY"].ToString().ToLower().Trim() : "";
			if (city.Equals("chicago") || city.Equals("chcago") || city.Equals("chicgo") || city.Equals("city of chicago"))
			{
				ht.Add("TWNCTYOF", "Chicago");
				ht.Add("F1205", "City of Chicago Stamps");
				ht.Add("X05PER", this.GetSystemSetting("CHICAGO-X05PER"));
				ht.Add("X05ATX", this.GetSystemSetting("CHICAGO-X05ATX"));
				ht.Add("X05BASIS", "S");
				ht.Add("DZ1205", "G");
				ht.Add("INCITY", "X");
			}

			// the pin number gets duplicated
			if (ht.ContainsKey("TAXMAPID"))
			{
				ht.Add("BLEGAL1", ht["TAXMAPID"].ToString());
			}

			// if additional pins exist, those need to be split up into two fields
			if (ht.ContainsKey("HOUSHOLD"))
			{
				string[] pins = ht["HOUSHOLD"].ToString().Split(",".ToCharArray());
				ht.Add("BLEGAL2", pins[0]);
				if (pins.Length > 1)
				{
					ht.Add("BLEGAL3", pins[1]);
				}
			}
			
			if(ht.ContainsKey("PROPUSE") && ht["PROPUSE"].Equals("Commercial"))
			{
				if (!ht.ContainsKey("NOTES"))
				{
					ht.Add("NOTES", "Commercial=Yes");
				}
				else
				{
					ht["NOTES"] += ", Commercial=Yes";
				}
			}
			
			if(ht.ContainsKey("CD36DUDT") && !ht["CD36DUDT"].ToString().Equals(""))
			{
				if (!ht.ContainsKey("NOTES"))
				{
					ht.Add("NOTES", "Foreclosure " + ht["CD36DUDT"].ToString());
				}
				else
				{
					ht["NOTES"] += ", Foreclosure " + ht["CD36DUDT"].ToString();
				}
			}
			
			
			if(ht.ContainsKey("CD37DUDT") && !ht["CD37DUDT"].ToString().Equals(""))
			{
				if (!ht.ContainsKey("NOTES"))
				{
					ht.Add("NOTES", "Judicial " + ht["CD37DUDT"].ToString());
				}
				else
				{
					ht["NOTES"] += ", Judicial " + ht["CD37DUDT"].ToString();
				}
			}
			
			if(ht.ContainsKey("NOTES") && ht["NOTES"].ToString().IndexOf("Short") > -1)
			{
				if (!ht.ContainsKey("USR41DES"))
				{
					ht.Add("USR41DES", "Short Sales");
				}
				if (!ht.ContainsKey("USR41VAL"))
				{
					ht.Add("USR41VAL", "Short Sales");
				}
			}
			
			// if PROPUSE is set check for commercial and put in a note and add the UPROPOTH attribute
			if(ht.ContainsKey("CASHSALE"))
			{
				if(ht["CASHSALE"].Equals("X"))
				{
					if (!ht.ContainsKey("NOTES"))
					{
						ht.Add("NOTES", "Cash Sale=Yes");
					}
					else
					{
						ht["NOTES"] += ", Cash Sale=Yes";
					}
				}
			}


			// if underwriter code is included, also add the name
			if (ht.ContainsKey("UNDCODE"))
			{
				try
				{
					Affinity.Underwriter uw = new Affinity.Underwriter( this.request.GetPhreezer() );
					uw.Load(ht["UNDCODE"]);
					ht.Add("UNDNAME", uw.Description);
					ht["UNDCODE"] = ht["UNDCODE"].ToString().ToUpper();
				}
				catch (Exception ex)
				{
					log.Error(ex.Message);
				}
			}

			// if survey was requested, add it to the notes
			if (ht.ContainsKey("TX04STAT") && ht["TX04STAT"].Equals("REQUIRED"))
			{
				string stype = ht.ContainsKey("TX04CMT") ? ht["TX04CMT"].ToString() : "Yes";
				if (!ht.ContainsKey("NOTES"))
				{
					ht.Add("NOTES", "Survey=" + stype);
				}
				else
				{
					ht["NOTES"] += ", Survey=" + stype;
				}
			}

			return this.HashTableToString(ht);
             * */
            return "";
		}

	}
}