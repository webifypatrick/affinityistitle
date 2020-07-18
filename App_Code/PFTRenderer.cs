using System;
using System.Collections;

namespace Affinity.ExportRenderer
{

	/// <summary>
	/// Renders Export Output for SoftPro PFT
	/// </summary>
	public class PFTRenderer : BaseRenderer
	{
		// use the base constructor
		public PFTRenderer(Affinity.Request req, Hashtable sys) : base(req,sys) { }

		protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(PFTRenderer));

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

            if (ht.ContainsKey("TITLLUC") == false)
            {
                if (this.request.Order.PropertyState.ToUpper().Equals("IN"))
                {
                    ht.Add("TITLLUC", "ATSVAL");
                }
                else
                {
                    ht.Add("TITLLUC", "AFFINITY");
                }
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
				//ht.Add("TWNCTYOF", "Chicago");
				//ht.Add("F1205", "City of Chicago Stamps");
				//ht.Add("X05PER", this.GetSystemSetting("CHICAGO-X05PER"));
				//ht.Add("X05ATX", this.GetSystemSetting("CHICAGO-X05ATX"));
				//ht.Add("X05BASIS", "S");
				//ht.Add("DZ1205", "G");
				//ht.Add("INCITY", "X");
			}

			// the pin number gets duplicated
			if (ht.ContainsKey("TAXMAPID"))
			{
				ht.Add("BLEGAL1", ht["TAXMAPID"].ToString());
			}
			
			// if this a Tract Search and the Loan Amount is 1 or less than do not display it
			if (ht.ContainsKey("LOANAMT") && ht.ContainsKey("NOTES") && ht["NOTES"].ToString().IndexOf("TractSearch=Yes") > -1)
			{
				decimal loanamt = 0;
				decimal.TryParse(ht["LOANAMT"].ToString(), out loanamt);
				if(loanamt <= 1)
				{
					ht.Remove("LOANAMT");
				}
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
			
			// if PROPUSE is set check for commercial and put in a note and add the UPROPOTH attribute
			if(ht.ContainsKey("PROPUSE"))
			{
				if(ht["PROPUSE"].Equals("Commercial"))
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
				if (!ht.ContainsKey("UPROPOTH"))
				{
					ht.Add("UPROPOTH", "Nonresidential");
				}
			}
			
			// set the LPRMRKS attribute based the value of PropertyUse set on the first page of creating an order
			//if (!ht.ContainsKey("LPRMRKS"))
			//{
			//	ht.Add("LPRMRKS", this.request.Order.PropertyUse);
			//}
			
			//if (!ht.ContainsKey("OPRMRKS"))
			//{
			//	ht.Add("OPRMRKS", this.request.Order.PropertyUse);
			//}
			
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
					
					string undcode = ht["UNDCODE"].ToString().ToUpper();
					if(!this.request.Order.Account.UnderwriterEndorsements.Equals("")) {
						string[] endorsements = this.request.Order.Account.UnderwriterEndorsements.Split(",".ToCharArray());
						
						string endorsement = "";
						bool is100 = false;
						string percnt = "";
						foreach (string ends in endorsements)
						{
							if(ends.IndexOf(undcode) > -1)
							{
								string prefix = " ";
								string val = ends.Replace(undcode, "");
								if(val.Equals("100%"))
								{
									prefix = " endorse ";
									is100 = true;
								}
								else
								{
									percnt = val;
								}
								endorsement += prefix + val;
							}
						}
						
						// added to remove endorsements
						endorsement = "";
						
						if(!ht.ContainsKey("UNDENDPCT")) ht.Add("UNDENDPCT", ((is100)? "100%" : "" ));
						if(!ht.ContainsKey("UNDPOLPCT")) ht.Add("UNDPOLPCT", (percnt ));
						ht["UNDCODE"] = undcode + endorsement;
					}
					else {
						ht["UNDCODE"] = undcode;
					}
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
			
			if (!ht.ContainsKey("SETREFER")) ht.Add("SETREFER", this.request.Order.ClientName);
			//if (!ht.ContainsKey("SETREFTC")) ht.Add("SETREFTC", this.request.Order.CustomerId);
			//if (!ht.ContainsKey("BUSINESSLICENSEID")) ht.Add("BUSINESSLICENSEID", this.request.Order.Account.BusinessLicenseID);
			//if (!ht.ContainsKey("INDIVIDUALLICENSEID")) ht.Add("INDIVIDUALLICENSEID", this.request.Order.Account.IndividualLicenseID);
			
			if(ht.ContainsKey("AG701ID") && ht["AG701ID"].ToString().Equals(""))
			{
				ht.Remove( "AG701ID" );
			}
			
			if(ht.ContainsKey("AG701CONTID") && ht["AG701CONTID"].ToString().Equals(""))
			{
				ht.Remove( "AG701CONTID" );
			}

			
			if(ht.ContainsKey("AG702ID") && ht["AG702ID"].ToString().Equals(""))
			{
				ht.Remove( "AG702ID" );
			}
			
			if(ht.ContainsKey("AG702CONTID") && ht["AG702CONTID"].ToString().Equals(""))
			{
				ht.Remove( "AG702CONTID" );
			}

			
			if(ht.ContainsKey("SLRATID") && ht["SLRATID"].ToString().Equals(""))
			{
				ht.Remove( "SLRATID" );
			}
			
			if(ht.ContainsKey("SLRATCONTID") && ht["SLRATCONTID"].ToString().Equals(""))
			{
				ht.Remove( "SLRATCONTID" );
			}

			
			if(ht.ContainsKey("LENID") && ht["LENID"].ToString().Equals(""))
			{
				ht.Remove( "LENID" );
			}
			
			if(ht.ContainsKey("LENCONTID") && ht["LENCONTID"].ToString().Equals(""))
			{
				ht.Remove( "LENCONTID" );
			}

			
			if(ht.ContainsKey("MTBID") && ht["MTBID"].ToString().Equals(""))
			{
				ht.Remove( "MTBID" );
			}
			
			if(ht.ContainsKey("MTBCONTID") && ht["MTBCONTID"].ToString().Equals(""))
			{
				ht.Remove( "MTBCONTID" );
			}

			
			if(ht.ContainsKey("BYRATID") && ht["BYRATID"].ToString().Equals(""))
			{
				ht.Remove( "BYRATID" );
			}
			
			if(ht.ContainsKey("BYRATCONTID") && ht["BYRATCONTID"].ToString().Equals(""))
			{
				ht.Remove( "BYRATCONTID" );
			}

			return this.HashTableToString(ht);
		}

	}
}