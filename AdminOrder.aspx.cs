using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Affinity
{
    public partial class AdminOrder : PageBase
    {
        private Affinity.Order order;

        override protected void PageBase_Init(object sender, System.EventArgs e)
        {
            // we have to call the base first so phreezer is instantiated
            base.PageBase_Init(sender, e);

            int id = NoNull.GetInt(Request["id"], 0);
            this.order = new Affinity.Order(this.phreezer);
            this.order.Load(id);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageBase pb = (PageBase)this.Page;
            Affinity.Account acc = pb.GetAccount();
            ((Affinity.MasterPage)this.Master).SetLayout("Process Order", MasterPage.LayoutStyle.ContentOnly);
            this.RequirePermission(Affinity.RolePermission.AdminSystem);

            this.btnClose.Attributes.Add("onclick", "return confirm('Mark this order as closed?');");

            string state = "";
            Affinity.Account origacc = this.order.Account;

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
            if (state.ToUpper().Equals("IN"))
            {
                pnlContentScript.Controls.Add(new LiteralControl("<script>isIndiana = true;</script>"));
            }

            if (!Page.IsPostBack)
            {
                // populate current requests grid
                Affinity.RequestCriteria rc = new Affinity.RequestCriteria();
                rc.AppendToOrderBy("Created", true);
                rGrid.DataSource = order.GetOrderRequests(rc);
                rGrid.DataBind();

                // show any attachments that go with this order
                Affinity.Attachments atts = order.GetAttachments();

                foreach (Affinity.Attachment att in atts)
                {
                    pnlAttachments.Controls.Add(new LiteralControl("<div><a class=\"attachment\" href=\"MyAttachment.aspx?id=" + att.Id + "\">" + att.Name + "</a> (" + att.Created.ToString("M/dd/yyyy hh:mm tt") + ") <a class=\"delete\" onclick=\"return confirm('Delete this attachment?');\" href=\"AdminAttachment.aspx?a=delete&id=" + att.Id + "\">Delete</a></div>"));
                }

                // populate the form

                btnReAssign.Visible = acc.Role.HasPermission(Affinity.RolePermission.AffinityManager);

                Affinity.OrderStatuss codes = new Affinity.OrderStatuss(this.phreezer);
                Affinity.OrderStatusCriteria sc = new Affinity.OrderStatusCriteria();
                sc.InternalExternal = 1;
                codes.Query(sc);

                /*
                ddStatus.DataSource = codes;
                ddStatus.DataTextField = "Description";
                ddStatus.DataValueField = "Code";
                ddStatus.SelectedValue = this.order.CustomerStatusCode;
                ddStatus.DataBind(); 
                */
                lblStatus.Text = this.order.CustomerStatus.Description;

                txtId.Text = "WEB-" + this.order.Id.ToString();
                txtInternalId.Text = this.order.InternalId.ToString();
                txtInternalATSId.Text = this.order.InternalATSId.ToString();
                txtCustomerId.Text = this.order.CustomerId.ToString();
                txtPin.Text = this.order.Pin.ToString();
                txtAdditionalPins.Text = this.order.AdditionalPins.ToString();
                txtPropertyAddress.Text = this.order.PropertyAddress.ToString();
                txtPropertyAddress2.Text = this.order.PropertyAddress2.ToString();
                txtPropertyCity.Text = this.order.PropertyCity.ToString();
                txtPropertyState.Text = this.order.PropertyState.ToString();
                txtPropertyZip.Text = this.order.PropertyZip.ToString();
                txtPropertyCounty.Text = this.order.PropertyCounty.ToString();
                //txtInternalStatusCode.Text = this.order.InternalStatusCode.ToString();
                //txtCustomerStatusCode.Text = this.order.CustomerStatusCode.ToString();
                txtOriginator.Text = this.order.Account.FullName;
                txtCreated.Text = this.order.Created.ToString("MM/dd/yyyy hh:mm tt");
                txtModified.Text = this.order.Modified.ToString("MM/dd/yyyy hh:mm tt");
                txtClosingDate.Text = this.order.ClosingDate.ToShortDateString();
                txtClientName.Text = this.order.ClientName.ToString();
                radioResidential.Checked = (this.order.PropertyUse.Equals("Residential"));
                radioNonResidential.Checked = (this.order.PropertyUse.Equals("Nonresidential"));

                if (txtInternalId.Text.Trim().Equals(""))
                {
                    txtInternalId.Focus();
                    txtInternalId.Text = "AFF-";
                }
                else if (txtInternalATSId.Text.Trim().Equals(""))
                {
                    txtInternalATSId.Focus();
                }

                // the admin wants to see if there is a previous order from ANY user w/ the same PIN because
                // if the PIN was recently submitted, they may be able to use internal records and not have
                // to incur the expense of a full title search.
                Affinity.Order previousOrder = order.GetPrevious(false);

                // If there are any previous orders display a message to the admin
                if (previousOrder != null)
                {
                    PreviousOrderNotice.InnerHtml = "This order has a potential duplicate submitted on " + previousOrder.Created.ToShortDateString() + " - order number <a href=\"AdminOrder.aspx?id=" + previousOrder.Id.ToString() + "\">" + previousOrder.Id.ToString() + "</a> " + previousOrder.InternalId.ToString();
                    PreviousOrderNotice.Visible = true;
                }

            }
        }

        protected void gvrGrid_rowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gev = (GridView)e.Row.FindControl("elGrid");

                Affinity.ExportLogs el = new Affinity.ExportLogs(this.phreezer);
                // populate current exports grid
                Affinity.ExportLogCriteria elc = new Affinity.ExportLogCriteria();
                Affinity.Request r = (Affinity.Request)e.Row.DataItem;
                elc.RequestID = r.Id;
                el.Query(elc);

                gev.DataSource = el;
                gev.DataBind();


                GridView guv = (GridView)e.Row.FindControl("ulGrid");

                Affinity.UploadLogs ul = new Affinity.UploadLogs(this.phreezer);
                // populate current uploads grid
                Affinity.UploadLogCriteria ulc = new Affinity.UploadLogCriteria();
                ulc.RequestID = r.Id;
                ul.Query(ulc);

                guv.DataSource = ul;
                guv.DataBind();
            }
        }

        protected void gvelGrid_rowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Parent.Parent.Parent.Parent.FindControl("lblExportHistory").Visible = true;
                e.Row.Parent.Parent.Parent.Parent.FindControl("pnlExportHistory").Visible = true;
            }
        }

        protected void gvulGrid_rowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Parent.Parent.Parent.Parent.FindControl("lblUploadHistory").Visible = true;
                e.Row.Parent.Parent.Parent.Parent.FindControl("pnlUploadHistory").Visible = true;
            }
        }

        /// <summary>
        /// Persist to DB
        /// </summary>
        protected void UpdateOrder()
        {
            //this.order.Id = int.Parse(txtId.Text);
            this.order.InternalId = txtInternalId.Text;
            this.order.InternalATSId = txtInternalATSId.Text;
            this.order.CustomerId = txtCustomerId.Text;
            this.order.Pin = txtPin.Text;
            this.order.AdditionalPins = txtAdditionalPins.Text;
            this.order.PropertyAddress = txtPropertyAddress.Text;
            this.order.PropertyAddress2 = txtPropertyAddress2.Text;
            this.order.PropertyCity = txtPropertyCity.Text;
            this.order.PropertyState = txtPropertyState.Text;
            this.order.PropertyZip = txtPropertyZip.Text;
            this.order.PropertyCounty = txtPropertyCounty.Text;
            //this.order.InternalStatusCode = txtInternalStatusCode.Text;
            //this.order.CustomerStatusCode = ddStatus.SelectedValue;
            //this.order.OriginatorId = int.Parse(txtOriginatorId.Text);
            //this.order.Created = DateTime.Parse(txtCreated.Text);
            this.order.Modified = DateTime.Now;
            this.order.ClientName = txtClientName.Text;
            this.order.PropertyUse = (radioResidential.Checked) ? "Residential" : "Nonresidential";

            try
            {
                this.order.ClosingDate = DateTime.Parse(txtClosingDate.Text);
                this.order.Update();
                this.Redirect("AdminOrders.aspx?feedback=Order+Updated");
            }
            catch (FormatException ex)
            {
                ((Affinity.MasterPage)this.Master).ShowFeedback("Please check that the estimated closing date is valid and in the format 'mm/dd/yyyy'", MasterPage.FeedbackType.Error);
            }
            catch (Exception ex)
            {
                ((Affinity.MasterPage) this.Master).ShowFeedback(ex.Message, MasterPage.FeedbackType.Error);
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            UpdateOrder();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.Redirect("AdminOrders.aspx");
        }

        /// <summary>
        /// Assigns the AFF Id and notifies the customer, if their preferences are set
        /// to notify on file confirmations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            Affinity.WsResponse wsr = this.order.Confirm(txtInternalId.Text, this.GetSystemSettings());

            // we have to re-direct so the requests will be updated
            this.Redirect("AdminOrder.aspx?id=" + this.order.Id + "&feedback=" + Server.UrlEncode("The order was confirmed." + wsr.NotificationMessage));

        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            this.order.InternalStatusCode = Affinity.OrderStatus.ClosedCode;
            this.order.CustomerStatusCode = Affinity.OrderStatus.ClosedCode;
            this.order.Update();

            this.Redirect("AdminOrders.aspx?feedback=" + Server.UrlEncode("Order " + this.order.WorkingId + " has been closed"));
        }

        /// <summary>
        // This is called when the Re-Assign button is clicked and just shows the dropdown of possible user choices
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReAssign_Click(object sender, EventArgs e)
        {
            this.RequirePermission(Affinity.RolePermission.AffinityManager);
            Affinity.Accounts accts = new Affinity.Accounts(this.phreezer);
            Affinity.AccountCriteria ac = new Affinity.AccountCriteria();
            ac.AppendToOrderBy("LastName");
            ac.AppendToOrderBy("FirstName");
            accts.Query(ac);

            ddNewOriginator.DataSource = accts;
            ddNewOriginator.DataTextField = "FullName";
            ddNewOriginator.DataValueField = "Id";
            try
            {
                ddNewOriginator.SelectedValue = this.order.OriginatorId.ToString();
            }
            catch (Exception)
            {

            }
            ddNewOriginator.DataBind();

            txtOriginator.Visible = false;
            btnReAssign.Visible = false;
            ddNewOriginator.Visible = true;
            btnDoReAssign.Visible = true;
        }

        /// <summary>
        /// This processed the order re-assign
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDoReAssign_Click(object sender, EventArgs e)
        {
            this.RequirePermission(Affinity.RolePermission.AffinityManager);
            Affinity.Account acct = new Affinity.Account(this.phreezer);
            acct.Load(ddNewOriginator.SelectedValue);
            txtOriginator.Text = acct.FullName;

            Affinity.Request request;

            int id = NoNull.GetInt(Request["id"], 0);
            Affinity.Requests requests = new Affinity.Requests(this.phreezer);

            Affinity.RequestCriteria rc = new Affinity.RequestCriteria();
            rc.OrderId = id;
            requests.Query(rc);

            XmlDocument prefDoc = new XmlDocument();
            prefDoc.LoadXml(acct.PreferencesXml);

            foreach (Affinity.Request req in requests)
            {
                req.OriginatorId = acct.Id;

                if (req.RequestTypeCode.Equals("Order"))
                {
                    XmlDocument reqDoc = new XmlDocument();
                    reqDoc.LoadXml(req.Xml);

                    // ApplicantName
                    XmlNode reqNode = reqDoc.SelectSingleNode("//field[@name='ApplicantName']");
                    XmlNode prefNode = prefDoc.SelectSingleNode("//field[@name='ApplicantName']");

                    if (prefNode != null && reqNode != null)
                    {
                        reqNode.InnerText = prefNode.InnerText;
                    }

                    // ApplicantAttorneyName
                    reqNode = reqDoc.SelectSingleNode("//field[@name='ApplicantAttorneyName']");
                    prefNode = prefDoc.SelectSingleNode("//field[@name='ApplicantAttorneyName']");

                    if (prefNode != null && reqNode != null)
                    {
                        reqNode.InnerText = prefNode.InnerText;
                    }

                    // ApplicantAddress
                    reqNode = reqDoc.SelectSingleNode("//field[@name='ApplicantAddress']");
                    prefNode = prefDoc.SelectSingleNode("//field[@name='ApplicantAddress']");

                    if (prefNode != null && reqNode != null)
                    {
                        reqNode.InnerText = prefNode.InnerText;
                    }

                    // ApplicantAddress2
                    reqNode = reqDoc.SelectSingleNode("//field[@name='ApplicantAddress2']");
                    prefNode = prefDoc.SelectSingleNode("//field[@name='ApplicantAddress2']");

                    if (prefNode != null && reqNode != null)
                    {
                        reqNode.InnerText = prefNode.InnerText;
                    }

                    // ApplicantCity
                    reqNode = reqDoc.SelectSingleNode("//field[@name='ApplicantCity']");
                    prefNode = prefDoc.SelectSingleNode("//field[@name='ApplicantCity']");

                    if (prefNode != null && reqNode != null)
                    {
                        reqNode.InnerText = prefNode.InnerText;
                    }

                    // ApplicantState
                    reqNode = reqDoc.SelectSingleNode("//field[@name='ApplicantState']");
                    prefNode = prefDoc.SelectSingleNode("//field[@name='ApplicantState']");

                    if (prefNode != null && reqNode != null)
                    {
                        reqNode.InnerText = prefNode.InnerText;
                    }

                    // ApplicantZip
                    reqNode = reqDoc.SelectSingleNode("//field[@name='ApplicantZip']");
                    prefNode = prefDoc.SelectSingleNode("//field[@name='ApplicantZip']");

                    if (prefNode != null && reqNode != null)
                    {
                        reqNode.InnerText = prefNode.InnerText;
                    }

                    // ApplicantPhone
                    reqNode = reqDoc.SelectSingleNode("//field[@name='ApplicantPhone']");
                    prefNode = prefDoc.SelectSingleNode("//field[@name='ApplicantPhone']");

                    if (prefNode != null && reqNode != null)
                    {
                        reqNode.InnerText = prefNode.InnerText;
                    }

                    // ApplicantFax
                    reqNode = reqDoc.SelectSingleNode("//field[@name='ApplicantFax']");
                    prefNode = prefDoc.SelectSingleNode("//field[@name='ApplicantFax']");

                    if (prefNode != null && reqNode != null)
                    {
                        reqNode.InnerText = prefNode.InnerText;
                    }

                    // ApplicantEmail
                    reqNode = reqDoc.SelectSingleNode("//field[@name='ApplicantEmail']");
                    prefNode = prefDoc.SelectSingleNode("//field[@name='ApplicantEmail']");

                    if (prefNode != null && reqNode != null)
                    {
                        reqNode.InnerText = prefNode.InnerText;
                    }

                    // ApplicantAttentionTo
                    reqNode = reqDoc.SelectSingleNode("//field[@name='ApplicantAttentionTo']");
                    prefNode = prefDoc.SelectSingleNode("//field[@name='ApplicantAttentionTo']");

                    if (prefNode != null && reqNode != null)
                    {
                        reqNode.InnerText = prefNode.InnerText;
                    }
                    req.Xml = reqDoc.OuterXml;
                    req.Update();
                }
            }

            this.order.OriginatorId = acct.Id;
            this.order.Update();

            txtOriginator.Visible = true;
            //txtOriginator.Text = request.Id.ToString() + " - " + request.OrderId.ToString();
            btnReAssign.Visible = true;
            ddNewOriginator.Visible = false;
            btnDoReAssign.Visible = false;
        }
    }
}