using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Affinity
{
    public partial class MyRequestSubmit : PageBase
    {

        private XmlForm xmlForm;
        private Affinity.RequestType rtype;
        private Affinity.Order order;
        private int changeId = 0;
        private bool isChange = false;
        public string errors = "";
        public string propertyState = "";

        /// <summary>
        /// The form controls are created at this point.  if we create them at page load
        /// then their viewstate will not be persisted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        override protected void PageBase_Init(object sender, System.EventArgs e)
        {
            bool isRefinance = (Request["Refinance"] != null && Request["Refinance"].Equals("True"));

            // we have to call the base first so phreezer is instantiated
            base.PageBase_Init(sender, e);

            int orderId = NoNull.GetInt(Request["id"], 0);

            string requestCode = NoNull.GetString(Request["code"], Affinity.RequestType.DefaultCode);

            this.order = new Affinity.Order(this.phreezer);
            order.Load(orderId);
            Affinity.Account acc = this.GetAccount();
            Affinity.Account origacc = this.order.Account;
            propertyState = order.PropertyState.ToUpper().Trim().Substring(0, 2);

            // make sure this user has permission to make updates to this order
            if (!order.CanUpdate(this.GetAccount()))
            {
                this.Crash(300, "Permission denied.");
            }

            this.rtype = new Affinity.RequestType(this.phreezer);
            rtype.Load(requestCode);

            string state = "";

            if (!origacc.PreferencesXml.Equals(""))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(origacc.PreferencesXml);
                XmlNode statenode = doc.SelectSingleNode("//field[@name=\"ApplicantState\"]");
                if (statenode != null)
                {
                    state = statenode.InnerText;
                }
            }
            //Response.Write(acc.State);
            string RoleCode = (Session["RoleCode"] != null) ? Session["RoleCode"].ToString() : acc.RoleCode;

            if (RoleCode.Equals("Realtor"))
            {
                string script = "";
                if (state.ToUpper().Equals("IN"))
                {
                    script = "isIndiana = true; var $tds = $('#ctl00_content_cph_field_CopyApplicationTo td'); $($tds[0]).hide(); $($tds[1]).hide();";
                }
                pnlContentScript.Controls.Add(new LiteralControl("<script>$(function(){$('#outer_ApplicantName .label, #outer_ListingRealtorName .label, #outer_SellersRealtorName .label').html('Brokerage');}); $('#outer_ApplicantAttorneyName label').html('Realtor'); $('#outer_ApplicantAttorneyName .label').html('Realtor'); " + script + "</script>"));
                //ContentScript.InnerHtml = "";
            }
            else if (state.ToUpper().Equals("IN"))
            {
                pnlContentScript.Controls.Add(new LiteralControl("<script>isIndiana = true;</script>"));
            }

            this.xmlForm = new XmlForm(this.order.Account);

            this.changeId = NoNull.GetInt(Request["change"], 0);
            this.isChange = (!changeId.Equals(0));

            if (this.rtype.Code.Equals("ClerkingRequest"))
            {
                ContentFooterSpan.InnerHtml = "&copy; Copyright <%=DateTime.Now.Year.ToString() %>, Advocate Title Services, LLC";
            }

            string busindxml = "<field name=\"BusinessLicenseID\">" + this.GetAccount().BusinessLicenseID + "</field>" + "<field name=\"IndividualLicenseID\">" + this.GetAccount().IndividualLicenseID + "</field>";
            if (!IsPostBack && this.isChange)
            {
                // create a form for a change request
                Affinity.Request req = new Affinity.Request(this.phreezer);
                req.Load(changeId);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(req.Xml);
                //Response.Write(req.Xml);
                var node = doc.SelectSingleNode("//field[@name=\"TransactionType\"]");
                //var node = doc.SelectSingleNode("//field");
                //Response.Write("xml: " + node.OuterXml);

                // Check Indiana Purchases for contact information
                if (propertyState.Equals("IN") && node != null && node.InnerText.Equals("Purchase"))
                {
                    //Response.Write(node.InnerText);
                    node = doc.SelectSingleNode("//field[@name=\"Seller\"]");
                    if (node != null && node.InnerText.Trim().Equals(""))
                    {
                        // no seller
                        errors += "<li>No Seller Name was entered.</li>";
                    }
                    node = doc.SelectSingleNode("//field[@name=\"Buyer\"]");
                    if (node != null && node.InnerText.Trim().Equals(""))
                    {
                        // no buyer
                        errors += "<li>No Buyer Name was entered.</li>";
                    }

                    // Originator Contact Information check
                    node = doc.SelectSingleNode("//field[@name=\"ApplicantName\"]");
                    if (node != null && node.InnerText.Trim().Equals(""))
                    {
                        // no buyer
                        errors += "<li>No Firm Name was entered under Originator.</li>";
                    }
                    node = doc.SelectSingleNode("//field[@name=\"ApplicantAddress\"]");
                    if (node != null && node.InnerText.Trim().Equals(""))
                    {
                        // no buyer
                        errors += "<li>No Address Line 1 was entered under Originator.</li>";
                    }
                    node = doc.SelectSingleNode("//field[@name=\"ApplicantCity\"]");
                    if (node != null && node.InnerText.Trim().Equals(""))
                    {
                        // no buyer
                        errors += "<li>No City was entered under Originator.</li>";
                    }
                    node = doc.SelectSingleNode("//field[@name=\"ApplicantState\"]");
                    if (node != null && node.InnerText.Trim().Equals(""))
                    {
                        // no buyer
                        errors += "<li>No State was entered under Originator.</li>";
                    }
                    node = doc.SelectSingleNode("//field[@name=\"ApplicantZip\"]");
                    if (node != null && node.InnerText.Trim().Equals(""))
                    {
                        // no buyer
                        errors += "<li>No Zip was entered under Originator.</li>";
                    }

                    // Listing Agent Contact Information check
                    node = doc.SelectSingleNode("//field[@name=\"ListingRealtorName\"]");
                    if (node != null && node.InnerText.Trim().Equals(""))
                    {
                        // no buyer
                        errors += "<li>No Firm Name was entered under Listing Realtor.</li>";
                    }
                    node = doc.SelectSingleNode("//field[@name=\"ListingRealtorAddress\"]");
                    if (node != null && node.InnerText.Trim().Equals(""))
                    {
                        // no buyer
                        errors += "<li>No Address Line 1 was entered under Listing Realtor.</li>";
                    }
                    node = doc.SelectSingleNode("//field[@name=\"ListingRealtorCity\"]");
                    if (node != null && node.InnerText.Trim().Equals(""))
                    {
                        // no buyer
                        errors += "<li>No City was entered under Listing Realtor.</li>";
                    }
                    node = doc.SelectSingleNode("//field[@name=\"ListingRealtorZip\"]");
                    if (node != null && node.InnerText.Trim().Equals(""))
                    {
                        // no buyer
                        errors += "<li>No Zip was entered under Listing Realtor.</li>";
                    }
                    node = doc.SelectSingleNode("//field[@name=\"ListingRealtorPhone\"]");
                    if (node != null && node.InnerText.Trim().Equals(""))
                    {
                        // no buyer
                        errors += "<li>No Phone was entered under Listing Realtor.</li>";
                    }
                    node = doc.SelectSingleNode("//field[@name=\"ListingRealtorEmail\"]");
                    if (node != null && node.InnerText.Trim().Equals(""))
                    {
                        // no buyer
                        errors += "<li>No Email was entered under Listing Realtor.</li>";
                    }

                    // Selling Agent Contact Information check
                    node = doc.SelectSingleNode("//field[@name=\"SellersRealtorName\"]");
                    if (node != null && node.InnerText.Trim().Equals(""))
                    {
                        // no buyer
                        errors += "<li>No Firm Name was entered under Selling Realtor.</li>";
                    }
                    node = doc.SelectSingleNode("//field[@name=\"SellersRealtorAddress\"]");
                    if (node != null && node.InnerText.Trim().Equals(""))
                    {
                        // no buyer
                        errors += "<li>No Address Line 1 was entered under Selling Realtor.</li>";
                    }
                    node = doc.SelectSingleNode("//field[@name=\"SellersRealtorCity\"]");
                    if (node != null && node.InnerText.Trim().Equals(""))
                    {
                        // no buyer
                        errors += "<li>No City was entered under Selling Realtor.</li>";
                    }
                    node = doc.SelectSingleNode("//field[@name=\"SellersRealtorZip\"]");
                    if (node != null && node.InnerText.Trim().Equals(""))
                    {
                        // no buyer
                        errors += "<li>No Zip was entered under Selling Realtor.</li>";
                    }
                    node = doc.SelectSingleNode("//field[@name=\"SellersRealtorPhone\"]");
                    if (node != null && node.InnerText.Trim().Equals(""))
                    {
                        // no buyer
                        errors += "<li>No Phone was entered under Selling Realtor.</li>";
                    }
                    node = doc.SelectSingleNode("//field[@name=\"SellersRealtorEmail\"]");
                    if (node != null && node.InnerText.Trim().Equals(""))
                    {
                        // no buyer
                        errors += "<li>No Email was entered under Selling Realtor.</li>";
                    }
                    if(!errors.Equals(""))
                    {
                        ErrorH3.InnerHtml = "<script defer='defer'>$(function() {if($.trim($('#_ctl0_pain_one_cph_header').text()) !== 'Your order has been submitted') showPopup('Please address the following errors: <ul>" + errors + "</ul><br /><br /><br /><center><button onclick=\"hidePopup(); return false; \">Close</button>');});</script>";
                        ErrorH3.Visible = true;
                    }
                }


                pnlForm.Controls.Add(this.xmlForm.GetFormFieldControl(rtype.Definition, req.Xml.Replace("</response>", "") + busindxml + "</response>"));

                this.btnCancelChange.Visible = true;
                this.btnChange.Visible = true;
                this.btnCancelSubmit.Visible = false;
                this.btnSubmit.Visible = false;
            }
            else if (rtype.Code == Affinity.RequestType.DefaultChangeCode)
            {
                // this is a change to the main order, we store this as a request as well
                // but we treat it a little bit differently
                string resXml = XmlForm.XmlToString(order.GetResponse());
                pnlForm.Controls.Add(this.xmlForm.GetFormFieldControl(rtype.Definition, resXml.Replace("</response>", "") + busindxml + "</response>"));

                this.btnCancelChange.Visible = true;
                this.btnChange.Visible = true;
                this.btnCancelSubmit.Visible = false;
                this.btnSubmit.Visible = false;
            }
            else
            {
                // create a form for a new request
                //string reqXml = XmlForm.XmlToString(order.GetResponse());
                string reqXml = this.GetAccount().PreferencesXml;

                if (this.rtype.Code.Equals("ClerkingRequest"))
                {
                    Affinity.RequestCriteria rc = new Affinity.RequestCriteria();
                    rc.RequestTypeCode = "Order";
                    Affinity.Requests reqs = order.GetOrderRequests(rc);

                    if (reqs.Count > 0)
                    {
                        Affinity.Request r = (Affinity.Request)reqs[reqs.Count - 1];


                        //log.Debug(r.Xml);
                        reqXml = reqXml.Replace("</response>", "") + busindxml +
                        XmlForm.XmlToString(order.GetResponse()).Replace("<response>", "").Replace("</response>", "") +
                        r.Xml.Replace("<response>", "");
                        pnlForm.Controls.Add(this.xmlForm.GetFormFieldControl(rtype.Definition, reqXml));
                    }
                    else
                    {
                        reqXml = reqXml.Replace("</response>", "") + busindxml +
                        XmlForm.XmlToString(order.GetResponse()).Replace("<response>", "").Replace("</response>", "");
                        pnlForm.Controls.Add(this.xmlForm.GetFormFieldControl(rtype.Definition, XmlForm.XmlToString(order.GetResponse())));
                    }
                }
                else
                {
                    pnlForm.Controls.Add(this.xmlForm.GetFormFieldControl(rtype.Definition, reqXml));
                }
            }

            if (!RoleCode.Equals("Realtor"))
            {
                Response.Write("<style>#outer_IdentifierNumber{display:none;}</style>");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            this.btnCancelSubmit.Attributes.Add("onclick", "return confirm('If you cancel your changes will not be saved.  Are you sure?');");
            this.btnCancelChange.Attributes.Add("onclick", "return confirm('If you cancel your changes will not be saved.  Are you sure?');");

            this.RequirePermission(Affinity.RolePermission.SubmitOrders);

            this.header.InnerText = NoNull.GetString(Request["new"]) == "" ? (this.isChange || this.rtype.Code == Affinity.RequestType.DefaultChangeCode ? "Edit:" : "Add")
                + " " + this.rtype.Description + "" : "Step 2 of 2: New " + this.rtype.Description;

            ((Affinity.MasterPage) this.Master).SetLayout(this.header.InnerText, MasterPage.LayoutStyle.ContentOnly);

            ContentFooterSpan.InnerHtml = "&copy; Copyright " + DateTime.Now.Year.ToString() + ", Affinity Title Services, LLC";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pnl"></param>
        /// <param name="fieldname"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        private string FindOrderField(Panel pnl, string fieldname, string defaultVal)
        {
            TextBox tb = (TextBox)pnl.FindControl("field_" + fieldname);
            return (tb != null) ? tb.Text : defaultVal;
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SubmitOrder();
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            SubmitOrder();
        }

        protected void btnCancelSubmit_Click(object sender, EventArgs e)
        {
            this.Redirect("MyAccount.aspx");
        }

        protected void btnCancelChange_Click(object sender, EventArgs e)
        {
            this.Redirect("MyOrder.aspx?id=" + this.order.Id);
        }

        /// <summary>
        /// submits the order
        /// </summary>
        protected void SubmitOrder()
        {
            this.header.InnerText = "Your order has been submitted";
            ((Affinity.MasterPage)this.Master).SetLayout(this.header.InnerText, MasterPage.LayoutStyle.ContentOnly);

            // parse the controls on the page and generate an xml doc
            XmlDocument doc = this.xmlForm.GetResponse(pnlForm);

            XmlNode TractSearchNode = doc.SelectSingleNode("//field[@name = 'TractSearch']");
            XmlNode BuyerNameNode = doc.SelectSingleNode("//field[@name = 'Buyer']");
            if (TractSearchNode != null && TractSearchNode.InnerText.Equals("Yes") && BuyerNameNode != null && BuyerNameNode.InnerText.Equals(""))
            {
                BuyerNameNode.InnerText = ".";
            }

            Affinity.Request req = new Affinity.Request(this.phreezer);
            req.OrderId = order.Id;
            req.OriginatorId = this.GetAccount().Id;
            req.RequestTypeCode = this.rtype.Code;
            req.StatusCode = (this.isChange || this.rtype.Code == Affinity.RequestType.DefaultChangeCode) ? Affinity.RequestStatus.ChangedCode : Affinity.RequestStatus.DefaultCode;

            req.Xml = XmlForm.XmlToString(doc).Replace("’", "'");

            req.Insert();

            string surveyServices = req.GetDataValue("SurveyServices");

            // if this is a change, we have to update any previous requests of the 
            // same type so they are no longer recognized as the most current and we
            // know that they have been replaced by a newer request
            if (this.isChange)
            {
                Affinity.Request cr = new Affinity.Request(this.phreezer);
                cr.Load(this.changeId);
                // just a safety check to block any querystring monkeybusiness
                if (cr.OrderId == order.Id)
                {
                    cr.IsCurrent = false;
                    cr.Update();
                }

                if (surveyServices != cr.GetDataValue("SurveyServices"))
                {
                    // the survey services has been changed.  it's either new or cancelled
                    SendSurveyNotification(req, (surveyServices != "REQUIRED"), (surveyServices == "REQUIRED"));
                }
            }
            else if (surveyServices == "REQUIRED")
            {
                // send a notification that a new survey service has been requested
                SendSurveyNotification(req, false, true);
            }

            // if this was a property change, we need to update the master order record
            // as well so the data on the datagrids shows correctly
            if (this.rtype.Code == Affinity.RequestType.DefaultChangeCode)
            {
                order.ClientName = FindOrderField(pnlForm, "ClientName", order.ClientName);
                order.Pin = FindOrderField(pnlForm, "PIN", order.Pin);  // case is different
                order.AdditionalPins = FindOrderField(pnlForm, "AdditionalPins", order.AdditionalPins);
                order.PropertyAddress = FindOrderField(pnlForm, "PropertyAddress", order.PropertyAddress2);
                order.PropertyAddress2 = FindOrderField(pnlForm, "PropertyAddress2", order.PropertyAddress);
                order.PropertyCity = FindOrderField(pnlForm, "PropertyCity", order.PropertyCity);
                order.PropertyState = FindOrderField(pnlForm, "PropertyState", order.PropertyState);
                order.PropertyZip = FindOrderField(pnlForm, "PropertyZip", order.PropertyZip);
                order.CustomerId = FindOrderField(pnlForm, "CustomerId", order.CustomerId);
                order.PropertyCounty = FindOrderField(pnlForm, "PropertyCounty", order.PropertyCounty);

                string closingDate = FindOrderField(pnlForm, "ClosingDate", order.ClosingDate.ToShortDateString());

                try
                {
                    order.ClosingDate = DateTime.Parse(closingDate);
                }
                catch (FormatException ex)
                {
                    // TODO: check this at an earlier stage so we can roll back the transaction
                    ((Affinity.MasterPage)this.Master).ShowFeedback("Your order was updated however the new closing date was ignored because it was not a valid date in the format 'mm/dd/yyyy'", MasterPage.FeedbackType.Error);
                }
            }

            // we have to sync the status of the order because any new requests may change it
            // this will also call Update on the order if we've made any changes
            order.SyncStatus();

            // notify affinity if specified in system settings
            SendNotification(req);

            //TODO: redirect to a thank-you page instead of just showing the message
            ShowConfirmation();
        }

        /// <summary>
        /// Hides the form and shows the results of the request submission
        /// </summary>
        protected void ShowConfirmation()
        {
            pnlResults.Visible = true;

            // hide all the form controls
            pnlForm.Visible = false;
            btnSubmit.Visible = false;
            btnChange.Visible = false;
            btnCancelChange.Visible = false;
            btnCancelSubmit.Visible = false;

            pnlResults.Controls.Add(new LiteralControl("<div class=\"actions\">"));

            bool isTaxStampsSet = false;

            Affinity.RequestCriteria rc = new Affinity.RequestCriteria();
            rc.RequestTypeCode = "ClerkingRequest";

            Affinity.Requests reqs = order.GetOrderRequests(rc);

            int requestCount = reqs.Count;
            string changeParameter = "";

            // check to see if any Clerking Requests exist
            if (requestCount > 0)
            {
                Affinity.Request req = (Affinity.Request)reqs[requestCount - 1];

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(req.Xml);

                XmlNode clerkingservicenode = doc.SelectSingleNode("//field[@name='ClerkingServicesSuburbanTransferStamps']");

                if (clerkingservicenode != null)
                {
                    string[] csnArry = clerkingservicenode.InnerText.ToLower().Split(',');
                    int csnLen = csnArry.GetLength(0);

                    for (int i = 0; i < csnLen; i++)
                    {
                        // verify if city is selected
                        if (csnArry[i].Equals(order.PropertyCity.ToLower()))
                        {
                            isTaxStampsSet = true;
                            break;
                        }
                    }
                }

                changeParameter = "change=" + req.Id.ToString() + "&";
            }

            if (!isTaxStampsSet && order.IsTaxStampsRequired())
            {
                if (changeParameter.Equals(""))
                {
                    pnlResults.Controls.Add(new LiteralControl("<div class=\"notice\">The Tax District, " + order.PropertyCity + ", requires the purchase of tax stamps.<br>You can add tax stamps to this order by clicking on \"Add a Clerking Services Request to this Order\" below: </div>"));
                }
                else
                {
                    pnlResults.Controls.Add(new LiteralControl("<div class=\"notice\">The Tax District, " + order.PropertyCity + ", requires the purchase of tax stamps.<br>You have an existing Clerking Request, but you didn't pick the stamp for your city.<br>You can add the tax stamp for your city to this order by clicking on \"Add a Clerking Services Request to this Order\" below and under the section heading \"SUBURBAN TRANSFER STAMPS\", check the checkbox next to " + order.PropertyCity + ": </div>"));
                }
            }
            else
            {
                isTaxStampsSet = true;
            }

            // show the available actions that can be done with this order
            Affinity.RequestTypes rts = order.GetAvailableRequestTypes();
            pnlResults.Controls.Add(new LiteralControl("<div class=\"actions\">"));
            foreach (Affinity.RequestType rt in rts)
            {
                if (rt.Code.Equals("ClerkingRequest")) isTaxStampsSet = true;
                pnlResults.Controls.Add(new LiteralControl("<div><a class=\"add\" href=\"MyRequestSubmit.aspx?id=" + order.Id + "&code=" + rt.Code + "\">Add a " + rt.Description + " to this Order</a></div>"));
            }
            if (!isTaxStampsSet) pnlResults.Controls.Add(new LiteralControl("<div><a class=\"add\" href=\"MyRequestSubmit.aspx?" + changeParameter + "id=" + order.Id + "&code=ClerkingRequest\">Add a Clerking Services Request to this Order</a></div></div>"));
            pnlResults.Controls.Add(new LiteralControl("</div>"));

            pnlResults.Controls.Add(new LiteralControl("<div><a class=\"order\" href=\"MyOrder.aspx?id=" + order.Id + "\">View My Order</a></div>"));
            pnlResults.Controls.Add(new LiteralControl("<div><a class=\"home\" href=\"MyAccount.aspx\">Return To My Account</a></div>"));
            pnlResults.Controls.Add(new LiteralControl("</div>"));
        }

        /// <summary>
        /// Send a survey services notification
        /// </summary>
        /// <param name="r"></param>
        /// <param name="isCancel"></param>
        /// <param name="isNew"></param>
        protected void SendSurveyNotification(Affinity.Request r, bool isCancel, bool isNew)
        {
            string to = this.GetSystemSetting("SurveyServicesEmail");

            // send the notification email if the originator wants it
            if (!to.Equals(""))
            {
                string url = this.GetSystemSetting("RootUrl") + "AdminOrder.aspx?id=" + r.Order.Id.ToString();
                string header = isNew ? "Survey Request" : "Survey Cancellation";
                string subject = "Affinity " + header + " For " + r.Order.WorkingId;

                // send the email
                Com.VerySimple.Email.Mailer mailer = new Com.VerySimple.Email.Mailer(this.GetSystemSetting("SmtpHost"));

                string msg = "* This is an automated notification from the Affinity Web System *\r\n\r\n"
                    + "Type: " + header + "\r\n"
                    + "Submitted By: " + r.Order.Account.FullName + "\r\n"
                    + "Working ID: " + r.Order.WorkingId + "\r\n"
                    + "PIN: " + r.Order.Pin + "\r\n"
            + "Friendly: " + r.Order.ClientName + "\r\n"
            + "Tracking Code: " + r.Order.CustomerId + "\r\n"
            + "Identifier Number: " + r.Order.IdentifierNumber + "\r\n"
                    + "\r\n"
                    + "URL: " + url + "\r\n"
                    + "\r\n"
                    + "If you no longer wish to receive these notifications, please contact the Affinity Administrator.\r\n"
                    + this.GetSystemSetting("EmailFooter");

                mailer.Send(
                    this.GetSystemSetting("SendFromEmail")
                    , to.Replace(";", ",")
                    , subject
                    , msg);
            }
        }

        /// <summary>
        /// Sends a notification to the affinity administrator if set in the system settings
        /// </summary>
        /// <param name="r"></param>
        protected void SendNotification(Affinity.Request r)
        {

            bool isNewRequest = this.isChange == false && r.RequestTypeCode != Affinity.RequestType.DefaultChangeCode;
            bool isClosing = (r.RequestTypeCode == Affinity.RequestType.ClosingRequestCode);
            bool isClerking = (r.RequestTypeCode == Affinity.RequestType.ClerkingRequestCode);
            bool isNotSurveyServices = r.GetDataValue("SurveyServices").Equals("");

            string to = isClosing ? this.GetSystemSetting("ClosingRequestEmail") : this.GetSystemSetting("NewOrderEmail");

            if (isClerking)
            {
                if (to.Equals(""))
                {
                    to = this.GetSystemSetting("ClerkingRequestEmail");
                }
                else
                {
                    to += ", " + this.GetSystemSetting("ClerkingRequestEmail");
                }
            }

            string state = order.PropertyState.ToUpper();
            if (isNotSurveyServices && (state.Equals("IN") || state.Equals("MI") || state.Equals("FL")))
            {
                string addEmailTo = (state.Equals("IN")) ? "processing@affinityistitle.com" : (state.Equals("MI")) ? "michigangroup@affinityistitle.com" : "floridagroup@affinityistitle.com";
                if (to.Equals(""))
                {
                    to = addEmailTo;
                }
                else
                {
                    to += ", " + addEmailTo;
                }
            }

            // send the notification email if the originator wants it
            if (!to.Equals(""))
            {
                string url = this.GetSystemSetting("RootUrl") + "AdminOrder.aspx?id=" + r.Order.Id.ToString();
                string subject = "Affinity " + r.RequestTypeCode + " Notification For " + r.Order.WorkingId;
                string header = isNewRequest ? "New Request" : "Change Request";


                RequestNotificationAccounts reqaccts = new RequestNotificationAccounts(this.phreezer);
                RequestNotificationAccountCriteria reqacctcrit = new RequestNotificationAccountCriteria();
                reqacctcrit.RequestId = r.Id;
                reqaccts.Query(reqacctcrit);

                foreach (RequestNotificationAccount reqacct in reqaccts)
                {
                    to += ", " + reqacct.AssignedAccount.Email;
                }

                // send the email
                Com.VerySimple.Email.Mailer mailer = new Com.VerySimple.Email.Mailer(this.GetSystemSetting("SmtpHost"));

                string msg = "* This is an automated notification from the Affinity Web System *\r\n\r\n"
                    + "Type: " + r.RequestTypeCode + " - " + header + "\r\n"
                    + "Submitted By: " + r.Order.Account.FullName + "\r\n"
                    + "Working ID: " + r.Order.WorkingId + "\r\n"
                    + "PIN: " + r.Order.Pin + "\r\n"
            + "Friendly: " + r.Order.ClientName + "\r\n"
            + "Tracking Code: " + r.Order.CustomerId + "\r\n"
            + "Identifier Number: " + r.Order.IdentifierNumber + "\r\n"
                    + "\r\n"
                    + "URL: " + url + "\r\n"
                    + "\r\n"
                    + "If you no longer wish to receive these notifications, please contact the Affinity Administrator.\r\n"
                    + this.GetSystemSetting("EmailFooter");

                mailer.Send(
                    this.GetSystemSetting("SendFromEmail")
                    , to.Replace(";", ",")
                    , subject
                    , msg);
            }
        }

    }
}