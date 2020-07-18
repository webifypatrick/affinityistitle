using System;
using System.Collections;
using System.Web.UI;

namespace Affinity
{
    public partial class MyOrder : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.RequirePermission(Affinity.RolePermission.SubmitOrders);
            ((Affinity.MasterPage) this.Master).SetLayout("Order Details", MasterPage.LayoutStyle.ContentOnly);

            string id = NoNull.GetString(Request["id"]);

            if (Request["Attachments"] != null)
            {
                Redirect("MyOrder.aspx?id=" + id + "#Attachments");
            }

            // this is used to track if a property changes was submitted
            int changeId = 0;

            Affinity.Order order = new Affinity.Order(this.phreezer);
            order.Load(id);
            Affinity.Account acc = this.GetAccount();
            string RoleCode = (Session["RoleCode"] == null) ? acc.RoleCode : Session["RoleCode"].ToString();

            pnlStandardAttachments.Visible = !RoleCode.Equals("Realtor");
            BlankControlledOwners.Visible = false;
            BlankControlledAgents.Visible = !RoleCode.Equals("Realtor");
            IdentifierNumberDIV.Visible = RoleCode.Equals("Realtor");

            // make sure this user has permission to make updates to this order
            if (!order.CanRead(acc))
            {
                this.Crash(300, "Permission denied.");
            }

            //order.CustomerStatusCode
            //order.InternalStatusCode

            lblWorkingId.Text = order.WorkingId;

            txtCustomerId.Text = order.CustomerId;
            txtIdentifierNumber.Text = order.IdentifierNumber;

            txtClientName.Text = order.ClientName;
            txtPIN.Text = order.Pin;
            txtAdditionalPins.Text = order.AdditionalPins;
            txtPropertyAddress.Text = order.PropertyAddress;
            txtPropertyAddress2.Text = order.PropertyAddress2;
            txtPropertyCity.Text = order.PropertyCity;
            txtPropertyState.Text = order.PropertyState;
            txtPropertyZip.Text = order.PropertyZip;
            txtCustomerId.Text = order.CustomerId;
            txtPropertyCounty.Text = order.PropertyCounty;
            txtClosingDate.Text = order.ClosingDate.ToShortDateString();

            // show any attachments that go with this order
            Affinity.Attachments atts = new Affinity.Attachments(this.phreezer);
            Affinity.AttachmentCriteria attc = new Affinity.AttachmentCriteria();
            attc.OrderId = order.Id;
            atts.Query(attc);

            // see if the user has access to the attachment
            Affinity.AttachmentRole ardao = new Affinity.AttachmentRole(this.phreezer);
            Affinity.AttachmentRolesCriteria arcrit = new Affinity.AttachmentRolesCriteria();
            arcrit.RoleCode = RoleCode;

            foreach (Affinity.Attachment att in atts)
            {
                arcrit.AttachmentPurposeCode = att.AttachmentPurpose.Code;
                Affinity.AttachmentRoles aroles = ardao.GetAttachmentRoles(arcrit);

                // if the user has permission to view this attachment
                if (aroles.Count > 0 || acc.Id == order.OriginatorId)
                {
                    pnlAttachments.Controls.Add(new LiteralControl("<div><a class=\"attachment\" href=\"MyAttachment.aspx?id=" + att.Id + "\">" + att.Name + "</a> (" + att.Created.ToString("M/dd/yyyy hh:mm tt") + ")</div>"));
                }
            }

            // show the entire order history
            Affinity.RequestCriteria rc = new Affinity.RequestCriteria();
            rc.AppendToOrderBy("Created", true);
            rGrid.DataSource = order.GetOrderRequests(rc);
            rGrid.DataBind();

            // show the available actions that can be done with this order
            Affinity.RequestTypes rts = order.GetAvailableRequestTypes();
            pnlActions.Controls.Add(new LiteralControl("<div class=\"actions\">"));
            foreach (Affinity.RequestType rt in rts)
            {
                pnlActions.Controls.Add(new LiteralControl("<div><a class=\"add\" href=\"MyRequestSubmit.aspx?id=" + order.Id + "&code=" + rt.Code + "\">Add a " + rt.Description + " to this Order</a></div>"));
            }
            pnlActions.Controls.Add(new LiteralControl("<div><a class=\"add\" href=\"documents.aspx?id=" + order.Id + "\">Closing Document Manager – Forms</a></div>"));
            pnlActions.Controls.Add(new LiteralControl("</div>"));

            // show the details for the active requests
            Affinity.Requests rs = order.GetCurrentRequests();

            foreach (Affinity.Request r in rs)
            {
                // we don't want to show changes to the property information
                if (r.RequestType.Code != Affinity.RequestType.DefaultChangeCode)
                {
                    XmlForm xf = new XmlForm(acc);

                    //Hashtable labels = xf.GetLabelHashtable(r.RequestType.Definition);
                    Hashtable responses = XmlForm.GetResponseHashtable(r.Xml);

                    pnlRequests.Controls.Add(new LiteralControl("<div class=\"groupheader\">" + r.RequestType.Description
                        + " [<a href=\"MyRequestSubmit.aspx?change=" + r.Id + "&id=" + order.Id + "&code=" + r.RequestType.Code + "\">Edit</a>]"
                        + "</div>"));
                    pnlRequests.Controls.Add(new LiteralControl("<fieldset class=\"history\">"));

                    // add the basic info
                    pnlRequests.Controls.Add(NewLine("Request Status", r.RequestStatus.Description));
                    pnlRequests.Controls.Add(NewLine("Notes", r.Note));
                    pnlRequests.Controls.Add(NewLine("Submitted", r.Created.ToString("MM/dd/yyyy hh:mm tt")));

                    ArrayList keys = new ArrayList(responses.Keys);
                    keys.Sort();

                    foreach (string key in keys)
                    {
                        // we check for fields ending with "_validator" due to a bug with order prior to 03/13/07
                        // if (responses[key].ToString().Equals("") == false)
                        if (responses[key].ToString().Equals("") == false && key.EndsWith("_validator") == false)
                        {
                            //pnlRequests.Controls.Add(new LiteralControl("<div>" + labels[key].ToString() + ": " + responses[key].ToString() + "</div>"));
                            pnlRequests.Controls.Add(NewLine(key, responses[key]));
                        }
                    }

                    pnlRequests.Controls.Add(new LiteralControl("</fieldset>"));
                }
                else
                {
                    changeId = r.Id;
                }
            }

            lnkChange.NavigateUrl = "MyRequestSubmit.aspx?id=" + order.Id + "&change=" + changeId + "&code=" + Affinity.RequestType.DefaultChangeCode;

        }

        private LiteralControl NewLine(string label, object val)
        {
            return new LiteralControl("<div class=\"line underline\"><div class=\"field vertical\"><div class=\"label horizontal width_250\">"
                + label +
                "</div><div class=\"input horizontal readonly\">"
                + val.ToString() + "</div></div></div>");
        }
    }
}