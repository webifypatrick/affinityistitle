using System.Net.Mail;

namespace Affinity
{
    public partial class notifyUserFax : PageBase
    {
        override protected void PageBase_Init(object sender, System.EventArgs e)
        {
            // we have to call the base first so phreezer is instantiated
            base.PageBase_Init(sender, e);

            string filename = NoNull.GetString(Request["filename"], "");
            string message = NoNull.GetString(Request["message"], "");
            if (!filename.Equals(""))
            {
                bool success = !message.Equals("fail");

                Affinity.Attachments atts = new Affinity.Attachments(this.phreezer);
                Affinity.AttachmentCriteria attc = new Affinity.AttachmentCriteria();
                attc.Filepath = filename;
                atts.Query(attc);

                bool emailsent = false;

                foreach (Affinity.Attachment att in atts)
                {

                    if (emailsent) continue;
                    MailMessage mm = new MailMessage("admin@affinityistitle.com", att.Request.Account.Email, "Agent Examination Form Fax", ((success) ? "Your recent Agent Examination Form has been faxed successfully." : "Your recent Agent Examination Form fax transmission failed."));
                    mm.IsBodyHtml = true;
                    mm.Priority = MailPriority.Normal;

                    SmtpClient sc = new SmtpClient("10.9.8.250");
                    sc.Send(mm);
                    emailsent = true;
                }
            }
        }
    }
}