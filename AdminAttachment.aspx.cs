using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.IO;

namespace Affinity
{
    public partial class AdminAttachment : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.RequirePermission(Affinity.RolePermission.AdminSystem);
            //this.Master.SetLayout("Administration Dashboard", MasterPage.LayoutStyle.ContentOnly);

            if (Request["a"].Equals("delete"))
            {
                Affinity.Attachment att = new Affinity.Attachment(this.phreezer);
                att.Load(int.Parse(Request["id"]));
                int orderId = att.Request.Order.Id;

                string fileName = att.Filepath;
                string filePath = Server.MapPath("./") + "attachments/" + fileName;

                try
                {
                    File.Delete(filePath);
                    log.Debug("Deleted attachment #" + att.Id + ": " + filePath);
                }
                catch (Exception)
                {
                    log.Error("Unable attachment #" + att.Id + ": " + filePath);
                }

                att.Delete();
                Affinity.UploadLogCriteria ulc = new Affinity.UploadLogCriteria();
                ulc.AttachmentID = att.Id;
                ulc.OrderID = orderId;

                Affinity.UploadLogs uplogs = new Affinity.UploadLogs(this.phreezer);
                uplogs.Query(ulc);
                int cnt = uplogs.Count;

                for (int i = 0; i < cnt; i++)
                {
                    Affinity.UploadLog ul = (Affinity.UploadLog)uplogs[i];
                    ul.Delete();
                }

                //Affinity.UploadLogs ulc = new Affinity.UploadLogs(this.phreezer);
                this.Redirect("AdminOrder.aspx?id=" + orderId + "&feedback=Attachment+Deleted");
            }
            else
            {
                ((Affinity.MasterPage) this.Master).ShowFeedback("Unknown Action", MasterPage.FeedbackType.Error);
            }

        }
    }
}