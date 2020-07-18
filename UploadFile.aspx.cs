using System;
using System.Web.UI;
using System.IO;
using System.Net.Mail;
using System.Web;

namespace Affinity
{
    public partial class UploadFile : PageBase
    {
        protected Affinity.Request request;

        /// <summary>
        /// The form controls are created at this point.  if we create them at page load
        /// then their viewstate will not be persisted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        override protected void PageBase_Init(object sender, System.EventArgs e)
        {
            // we have to call the base first so phreezer is instantiated
            base.PageBase_Init(sender, e);

            int id = NoNull.GetInt(Request["id"], 0);
            request = new Affinity.Request(this.phreezer);
            Affinity.Order order = new Affinity.Order(this.phreezer);
            order.Load(id);
            Affinity.RequestCriteria criteria = new Affinity.RequestCriteria();
            Affinity.Requests reqs = order.GetOrderRequests(criteria);
            Affinity.Request req = (Affinity.Request)reqs[0];
            this.request.Load(req.Id);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.RequirePermission(Affinity.RolePermission.SubmitOrders);
            ((Affinity.MasterPage) this.Master).SetLayout("Upload File", MasterPage.LayoutStyle.ContentOnly);

            if (!Page.IsPostBack)
            {
                // populate the form
                Affinity.AttachmentPurposes aps = new Affinity.AttachmentPurposes(this.phreezer);
                Affinity.AttachmentPurposeCriteria apc = new Affinity.AttachmentPurposeCriteria();
                aps.Query(apc);

                ddFilePurpose.DataSource = aps;
                ddFilePurpose.DataTextField = "Description";
                ddFilePurpose.DataValueField = "Code";
                //ddStatus.SelectedValue = null; // 
                ddFilePurpose.SelectedValue = "Committment";
                ddFilePurpose.DataBind();
            }
        }
        /// <summary>
        /// Persist to DB and send email notification
        /// </summary>
        /// <returns>result of email notification</returns>
        protected string UpdateRequest()
        {
            if (!fuAttachment.HasFile)
            {
                return "No File Uploaded.";
            }

            this.request.StatusCode = "New";  // here we need the code, tho
            this.request.Note = txtNote.Text;

            // Get the HttpFileCollection
            HttpFileCollection hfc = Request.Files;
            for (int i = 0; i < hfc.Count; i++)
            {
                HttpPostedFile hpf = hfc[i];
                if (hpf.ContentLength > 0)
                {
                    string attachname = (i == 0) ? txtAttachmentName.Text : Request["_ctl0:content_cph:txtAttachmentName" + i] ?? "";
                    if (attachname.Trim().Equals(""))
                    {
                        attachname = ddFilePurpose.SelectedItem.Text + i;
                    }

                    // if a file was provided, then upload it
                    string ext = Path.GetExtension(hpf.FileName);
                    string fileName = "req_att_" + request.Id + "_" + DateTime.Now.ToString("yyyyMMddhhss") + "_" + i + "." + ext.Replace(".", "");

                    log.Debug("UploadFile: Attachment: fileName: " + fileName + " attachname: " + attachname);
                    Attachment att = new Attachment(this.phreezer);
                    att.RequestId = this.request.Id;
                    att.Name = attachname;
                    att.PurposeCode = ddFilePurpose.SelectedValue;
                    att.Filepath = fileName;
                    att.MimeType = ext;
                    att.SizeKb = 0; // fuAttachment.FileBytes.GetUpperBound() * 1024;

                    att.Insert();
                    //TODO: block any harmful file types

                    Affinity.UploadLog ul = new Affinity.UploadLog(this.phreezer);

                    ul.AttachmentID = att.Id;
                    ul.AccountID = this.request.Account.Id;
                    ul.UploadAccountID = this.GetAccount().Id;
                    ul.OrderID = this.request.OrderId;
                    ul.RequestID = this.request.Id;

                    ul.Insert();

                    hpf.SaveAs(Server.MapPath("./") + "attachments/" + fileName);

                    Affinity.Account me = this.GetAccount();
                    string surveyServicesStr = this.request.GetDataValue("SurveyServices").ToLower();
                    bool isNotSurveyServices = (surveyServicesStr.Equals("") || surveyServicesStr.Equals("no"));

                    //string to = "title@affinityistitle.com, guy@affinityistitle.com";
                    string to = "title@affinityistitle.com";
                    string state = this.request.Order.PropertyState.ToUpper();
                    log.Debug("UploadFile: Attachment: isNotSurveyServices: " + isNotSurveyServices.ToString() + " state: " + state);
                    if (isNotSurveyServices && (state.Equals("IN") || state.Equals("MI") || state.Equals("FL")))
                    {
                        to += ", " + ((state.Equals("IN")) ? "processing@affinityistitle.com" : (state.Equals("MI")) ? "michigangroup@affinityistitle.com" : "floridagroup@affinityistitle.com");
                    }

                    string identifiernumber = this.request.Order.IdentifierNumber;
                    if (!identifiernumber.Equals(""))
                    {
                        if (identifiernumber.IndexOf("@") == -1)
                        {
                            to += ", " + this.request.Order.IdentifierNumber + "@skyslope.com";
                        }
                        else
                        {
                            to += ", " + this.request.Order.IdentifierNumber;
                        }
                    }
                    log.Debug("UploadFile: Attachment: to: " + to);

                    MailMessage mm = new MailMessage(this.GetSystemSetting("SendFromEmail"), to, "File Uploaded for Affinity Order '" + this.request.Order.ClientName.Replace("\r", "").Replace("\n", "") + "' #" + this.request.Order.WorkingId, "File: " + att.Name + " (" + fileName + ") was uploaded to: #" + this.request.Order.WorkingId + " and was uploaded by: " + me.FirstName + " " + me.LastName + "<br /><br /><br />\r\n\r\n" + this.GetSystemSetting("EmailFooter"));
                    mm.IsBodyHtml = true;
                    mm.Priority = MailPriority.Normal;

                    if (File.Exists(Server.MapPath("./") + "attachments/" + fileName))
                    {
                        System.Net.Mail.Attachment attch = new System.Net.Mail.Attachment(Server.MapPath("./") + "attachments/" + fileName);
                        attch.Name = fileName;

                        mm.Attachments.Add(attch);
                    }
                    //SmtpClient sc = new SmtpClient(this.GetSystemSetting("SmtpHost"));
                    //sc.Send(mm);

                    Com.VerySimple.Email.Mailer mailer = new Com.VerySimple.Email.Mailer(this.GetSystemSetting("SmtpHost"));
                    if(mailer.Send(mm))
                    {
                        log.Debug("UploadFile: Attachment: Mail Sent: successfully");
                    }
                    else
                    {
                        log.Debug("UploadFile: Attachment: Mail Sent: failed");
                    }
                }
            }

            return "File was uploaded successfully.";

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string result = UpdateRequest();
            this.Redirect("MyOrder.aspx?id=" + this.request.OrderId + "&feedback=" + Server.UrlEncode("Request Updated.  " + result));
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.Redirect("MyOrder.aspx?id=" + this.request.OrderId + "");
        }

        protected string SendNotification(string message)
        {
            //bool sendEmail = false;
            bool userEmailPreference = false;
            //string statusChange = "";
            string actionTaken = "";
            string emailPreferenceCode = ""; // this code will return empty string

            // file posted
            //sendEmail = true;
            userEmailPreference = this.request.Account.GetPreference("EmailOnFilePost").Equals("Yes");
            emailPreferenceCode = "EmailOnFilePostAddress";

            if (userEmailPreference == false)
            {
                actionTaken = "No email was sent based on the customer's " + emailPreferenceCode + " preference setting";
            }
            else
            {
                // the customer does want to receive this email

                string to = this.request.Account.GetPreference(emailPreferenceCode, this.request.Order.Account.Email);
                string url = this.GetSystemSetting("RootUrl") + "MyOrder.aspx?id=" + this.request.Order.Id.ToString();
                string subject = "Affinity Order '" + this.request.Order.ClientName.Replace("\r", "").Replace("\n", "") + "' #" + this.request.Order.WorkingId + " Updated";

                // send the email
                Com.VerySimple.Email.Mailer mailer = new Com.VerySimple.Email.Mailer(this.GetSystemSetting("SmtpHost"));

                string msg = "Dear " + this.request.Order.Account.FirstName + ",\r\n\r\n"
                    + "Your Affinity " + this.request.RequestTypeCode + " for '" + this.request.Order.ClientName + "', ID #"
                    + this.request.Order.WorkingId + " has a new Attachment\r\n\r\n"
                    + (message != "" ? message + "\r\n\r\n" : "")
                    + "You may view the full details of your order anytime online at " + url + ".  "
                    + "If you would prefer to not receive this notification, you may also login "
                    + "to your Affinity account and customize your email notification preferences.\r\n\r\n"
                    + this.GetSystemSetting("EmailFooter");

                //Response.Write(msg);
                //Response.Write(this.GetSystemSetting("SendFromEmail"));
                //Response.Write(to);
                //Response.Write(subject);

                mailer.Send(
                    this.GetSystemSetting("SendFromEmail")
                    , to.Replace(";", ",")
                    , subject
                    , msg);

                actionTaken = "Email notification was sent to " + to;
            }

            return actionTaken;
        }
    }
}