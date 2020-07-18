using System;

using System.Xml;
using System.Collections;
using MySql.Data.MySqlClient;

namespace Affinity
{
	/// <summary>
	/// Business Logic For Order Class
	/// </summary>
	public partial class Order
	{

		/// <summary>
		/// Parameterless constructor is here only so the object can be
		/// serialized for web services.  do not use this constructor!
		/// </summary>
		public Order() : base() { }
		
		public static string DefaultInternalId = "";

		protected override void OnPreSave(bool isInsert)
		{
			this.Modified = DateTime.Now;
		}

		/// <summary>
		/// returns true if the given account is allowed to read this record
		/// </summary>
		/// <param name="acc"></param>
		/// <returns></returns>
		public bool CanRead(Account acc)
		{
			bool isOk = false;

			if (this.OriginatorId == acc.Id)
			{
				isOk = true;
			}
			else if (acc.Role.HasPermission(RolePermission.AdminSystem))
			{
				isOk = true;
			}
			else if (acc.Role.HasPermission(RolePermission.ReadOtherEmployeeOrders) && this.Account.CompanyId == acc.CompanyId)
			{
				isOk = true;
			}

			return isOk;
		}

		/// <summary>
		/// Returns true if the given account is allowed to update this record
		/// </summary>
		/// <param name="acc"></param>
		/// <returns></returns>
		public bool CanUpdate(Account acc)
		{
			bool isOk = false;

			if (this.OriginatorId == acc.Id)
			{
				isOk = true;
			}
			else if (acc.Role.HasPermission(RolePermission.AdminSystem))
			{
				isOk = true;
			}
			else if (acc.Role.HasPermission(RolePermission.ModifyOtherEmployeeOrders) && this.Account.CompanyId == acc.CompanyId)
			{
				isOk = true;
			}

			return isOk;
		}

		/// <summary>
		/// returns either the web id or the AFT id for this order
		/// </summary>
		public string WorkingId
		{
			get
			{
				if (this.InternalId == null || this.InternalId.Equals(""))
				{
					return "WEB-" + this.Id;
				}
				else
				{
					return this.InternalId;
				}
			}
		}

		/// <summary>
		/// syncs the status code of the order to match the outstanding requests
		/// </summary>
		public void SyncStatus()
		{
			RequestCriteria rc = new RequestCriteria();
			rc.OrderId = this.Id;
			rc.IsCurrent = 1;
			rc.AppendToOrderBy("Created",false);

			Requests rs = GetOrderRequests(rc);

			bool hasChanged = false;
			bool hasNew = false;
			bool hasOpen = false;

			foreach (Request r in rs)
			{
				hasNew = (r.StatusCode == RequestStatus.DefaultCode) ? true : hasNew;
				hasChanged = (r.StatusCode == RequestStatus.ChangedCode) ? true : hasChanged;
				hasOpen = (r.RequestStatus.PermissionBit > 1) ? true : hasOpen;
			}

			string newCode = this.InternalStatusCode;

			// new is 1st priority
			// changed is 2nd priority
			// open is 3rd priority
			// otherwise everything is good
			if (hasNew)
			{
				newCode = OrderStatus.DefaultCode;
			}
			else if (hasChanged)
			{
				newCode = OrderStatus.ChangedCode;
			}
			else if (hasOpen)
			{
				newCode = OrderStatus.InProgressCode;
			}
			else
			{
				newCode = OrderStatus.ReadyCode;
			}

			if (newCode != this.InternalStatusCode)
			{
				this.InternalStatusCode = newCode;
				this.CustomerStatusCode = newCode;
			}

			// update every time
			this.Update();
		}

		/// <summary>
		/// Returns all attachments for this order
		/// </summary>
		/// <returns></returns>
		public Attachments GetAttachments()
		{
			Attachments atts = new Attachments(this.phreezer);
			AttachmentCriteria attc = new AttachmentCriteria();
			attc.OrderId = this.Id;
			attc.AppendToOrderBy("Created", true);
			atts.Query(attc);
			return atts;
		}

		/// <summary>
		/// Returns true if there is a previous order from the same originator with the same PIN
		/// </summary>
		/// <returns>true if previous orders exist</returns>
		public bool PreviousOrderExists()
		{
			return this.PreviousOrderExists(true);
		}

		/// <summary>
		/// Returns true if there is a previous order with the same PIN
		/// </summary>
		/// <param name="sameOriginatorOnly">true will only include orders from the same originator as this one</param>
		/// <returns>true if previous orders exist</returns>
		public bool PreviousOrderExists(bool sameOriginatorOnly)
		{
			return (this.GetPrevious(sameOriginatorOnly) != null);
		}

		/// <summary>
		/// Returns the most recent previous order from the same originator with the same PIN (or null if none exist)
		/// </summary>
		/// <returns>Order || null</returns>
		public Order GetPrevious()
		{
			return this.GetPrevious(true);
		}
		
		/// <summary>
		/// Returns the most recent previous order with the same PIN or the same address and city (or null if none exist)
		/// </summary>
		/// <param name="anyOrisameOriginatorOnlyginator">true will only include orders from the same originator as this one</param>
		/// <returns>Order || null</returns>
		public Order GetPrevious(bool sameOriginatorOnly)
		{
			Order returnOrder = null;

			OrderCriteria chkorderCriteria = new OrderCriteria();
			chkorderCriteria.IsDemo = false;
			chkorderCriteria.AppendToOrderBy("Created", true);
			chkorderCriteria.FindDuplicateOf = this;
			if (sameOriginatorOnly) chkorderCriteria.OriginatorId = this.OriginatorId;

			Orders chkorders = new Orders(this.phreezer);
			chkorders.Query(chkorderCriteria); 
			
			if (chkorders.Count > 0)
			{
				returnOrder = (Order) chkorders[0];
			}
			
			return returnOrder;
		}

        /// <summary>
        /// Returns a Zipcode object based on the property zip code for the order
        /// </summary>
        /// <returns></returns>
        public Affinity.Zipcode GetZipCode()
        {
            Zipcode zc = new Affinity.Zipcode(this.phreezer);

            try
            {
                zc.Load(this.PropertyZip);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

            return zc;

        }

        /// <summary>
        /// Verifies whether this requires tax stamps
        /// </summary>
        /// <returns></returns>
        public bool IsTaxStampsRequired()
        {
			bool ret = false;
			
			try
			{
				Affinity.TaxingDistrict td = new TaxingDistrict(this.phreezer);
				td.Load(this.PropertyCity);
				ret = (td.Stamp_required == 1);
			}
			catch(Exception ex)
			{
                log.Error(ex.Message);
			}

            return ret;

        }

		/// <summary>
		/// Returns a collection of the current requests, hiding any that
		/// have been replaced with a change
		/// </summary>
		/// <returns></returns>
		public Requests GetCurrentRequests()
		{
			RequestCriteria rc = new RequestCriteria();
			rc.OrderId = this.Id;
			rc.IsCurrent = 1;
			rc.AppendToOrderBy("Created", true);

			return GetOrderRequests(rc);

			/*
			ArrayList buff = new ArrayList();
			Requests rsd = new Requests(this.phreezer);
			foreach (Request r in rs)
			{
				if (!buff.Contains(r.RequestTypeCode))
				{
					buff.Add(r.RequestTypeCode);
					rsd.Add(r);
				}
			}

			return rsd;
			*/
		}

		/// <summary>
		/// Returns a collection of request types that are available for
		/// this order.  Will exclude any types for which an order has already been
		/// submitted
		/// </summary>
		/// <returns></returns>
		public RequestTypes GetAvailableRequestTypes()
		{
			// TODO create criteria parameters to run this as a distinct query

			// first get all the requests for this order and add them to a buffer array
			Requests rs = this.GetOrderRequests(new RequestCriteria());

			ArrayList buff = new ArrayList();
			foreach (Request r in rs)
			{
				if (!buff.Contains(r.RequestTypeCode))
				{
					buff.Add(r.RequestTypeCode);
				}
			}

			// now get all the request types and add them to a collection if they
			// do not exist in our buffer array
			RequestTypes rts = new RequestTypes(this.phreezer);
			RequestTypeCriteria rtc = new RequestTypeCriteria();
			rtc.IsActive = 1;
			rts.Query(rtc);

			RequestTypes return_rts = new RequestTypes(this.phreezer);

			foreach (RequestType rt in rts)
			{
				if (!buff.Contains(rt.Code))
				{
					return_rts.Add(rt);
				}
			}

			return return_rts;

		}

		/// <summary>
		/// returns a hashtable with all the keys equal to the order fields
		/// </summary>
		/// <param name="keyAttribute"></param>
		/// <returns></returns>
		public Hashtable GetTranslatedHashTable(string keyAttribute, bool includeBlankValues)
		{
			Hashtable ht = new Hashtable();
			if (keyAttribute.Equals("name"))
			{
				// this is faster because the reponse has the name attribute included
				ht = XmlForm.GetResponseHashtable(this.GetResponse());
			}
			else
			{
				// this is slower because we need the definition to translate
				Affinity.RequestType rt = new Affinity.RequestType(this.phreezer);
				rt.Load(Affinity.RequestType.DefaultChangeCode);

				ht = XmlForm.GetTranslatedHashtable(
					rt.GetDefDocument(),
					this.GetResponse(),
					keyAttribute,
					includeBlankValues);
			}

			return ht;

		}

		/// <summary>
		/// Returns the order as a response xml doc
		/// </summary>
		/// <param name="parent"></param>
		/// <returns></returns>
		public XmlDocument GetResponse()
		{
			XmlDocument rXml = new XmlDocument();
			rXml.CreateXmlDeclaration("1.0", "utf-8", null);
			XmlElement rXmlRoot = rXml.CreateElement("response");
			rXml.AppendChild(rXmlRoot);

			//rXmlRoot.AppendChild(XmlForm.GetXmlField(rXml, "AccountInternalId", this.Account.InternalId));
			rXmlRoot.AppendChild(XmlForm.GetXmlField(rXml, "ClientName", this.ClientName));
			rXmlRoot.AppendChild(XmlForm.GetXmlField(rXml, "ClosingDate", this.ClosingDate.ToShortDateString()));
			rXmlRoot.AppendChild(XmlForm.GetXmlField(rXml, "PropertyUse", this.PropertyUse));
			//rXmlRoot.AppendChild(XmlForm.GetXmlField(rXml, "Created", this.Created.ToShortDateString()));
			rXmlRoot.AppendChild(XmlForm.GetXmlField(rXml, "CustomerId", this.CustomerId));
			rXmlRoot.AppendChild(XmlForm.GetXmlField(rXml, "IdentifierNumber", this.IdentifierNumber));
			//rXmlRoot.AppendChild(XmlForm.GetXmlField(rXml, "CustomerStatusCode", this.CustomerStatusCode));
			//rXmlRoot.AppendChild(XmlForm.GetXmlField(rXml, "Id", this.Id.ToString()));
			rXmlRoot.AppendChild(XmlForm.GetXmlField(rXml, "InternalId", this.InternalId));
			rXmlRoot.AppendChild(XmlForm.GetXmlField(rXml, "InternalATSId", this.InternalATSId));
			//rXmlRoot.AppendChild(XmlForm.GetXmlField(rXml, "InternalStatusCode", this.InternalStatusCode));
			//rXmlRoot.AppendChild(XmlForm.GetXmlField(rXml, "Modified", this.Modified.ToShortDateString()));
			rXmlRoot.AppendChild(XmlForm.GetXmlField(rXml, "PIN", this.Pin));
			rXmlRoot.AppendChild(XmlForm.GetXmlField(rXml, "AdditionalPins", this.AdditionalPins));
			rXmlRoot.AppendChild(XmlForm.GetXmlField(rXml, "PropertyAddress", this.PropertyAddress));
			rXmlRoot.AppendChild(XmlForm.GetXmlField(rXml, "PropertyAddress2", this.PropertyAddress2));
			rXmlRoot.AppendChild(XmlForm.GetXmlField(rXml, "PropertyCity", this.PropertyCity));
			rXmlRoot.AppendChild(XmlForm.GetXmlField(rXml, "PropertyCounty", this.PropertyCounty));
			rXmlRoot.AppendChild(XmlForm.GetXmlField(rXml, "PropertyState", this.PropertyState));
			rXmlRoot.AppendChild(XmlForm.GetXmlField(rXml, "PropertyZip", this.PropertyZip));

			return rXml;
		}

		/// <summary>
		/// Confirms the order, which means that it is assigned an Affinity Id and all requests are 
		/// marked as "In Progress".  If the customer preference is set, a notification email will
		/// be sent out as well
		/// </summary>
		/// <param name="internalId">Affinity ID to assign to this order</param>
		/// <returns>Affinity.WsResponse</returns>
		public Affinity.WsResponse Confirm(string internalId, Hashtable systemSettings)
		{
			Affinity.WsResponse wsr = new WsResponse();

			this.InternalId = internalId;

			Affinity.Requests rs = this.GetCurrentRequests();

			foreach (Affinity.Request r in rs)
			{
				r.StatusCode = Affinity.RequestStatus.InProgressCode;
				r.Update();
			}

			this.SyncStatus(); // this will also update the order

			wsr.IsSuccess = true;
			wsr.ActionWasTaken = true;
			wsr.Message = "Order was confirmed";
			
			wsr.NotificationMessage = " The customer was *NOT* notified based on their user preference.";

			// send the notification email if the originator wants it
			if (this.Account.GetPreference("EmailOnConfirmation").Equals("Yes"))
			{
				string to = this.Account.GetPreference("EmailOnConfirmationAddress", this.Account.Email);
				
				if(!to.Equals(""))
				{
					string url = systemSettings["RootUrl"].ToString() + "MyOrder.aspx?id=" + this.Id.ToString();
					string subject = "Affinity Order '" + this.ClientName.Replace("\r", "").Replace("\n", "") + "' #" + this.WorkingId + " Confirmed: " + this.PropertyAddress + " " + (!this.PropertyAddress2.Equals("") ? this.PropertyAddress2 + " " : "") + this.PropertyCity + ", " + this.PropertyState;
                    
	
					// send the email
					Com.VerySimple.Email.Mailer mailer = new Com.VerySimple.Email.Mailer( systemSettings["SmtpHost"].ToString() );
	
					string msg = "Dear " + this.Account.FirstName + ",\r\n\r\n"
						+ "Your Affinity order for '" + this.ClientName + "' has been confirmed and assigned the AFF ID "
						+ this.InternalId + ".  You may use this ID when corresponding with us regarding this order.\r\n\r\n"
	          + "Friendly: " + this.ClientName + "\r\n"
	          + "Tracking Code: " + this.CustomerId + "\r\n\r\n"
						+ "You may view the full details of your order anytime online at " + url + ".  "
						+ "If you would prefer to not receive this notification, you may also login "
						+ "to your Affinity account and customize your email notification preferences.\r\n\r\n"
						+ systemSettings["EmailFooter"].ToString();
	
					wsr.NotificationSent =  mailer.Send(
						systemSettings["SendFromEmail"].ToString()
						, to.Replace(";", ",")
						, subject
						, msg);
	
					if (wsr.NotificationSent)
					{
						wsr.NotificationMessage = " A notification email was sent to " + to + ".";
					}
					else
					{
						wsr.NotificationMessage = " Unable to deliver notification email to " + to + ".  Please check the server debug logs for more information.";
					}
				}
			}

			return wsr;
		}

	}
}