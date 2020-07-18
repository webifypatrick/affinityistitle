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

namespace Affinity
{
    public partial class Admin : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Affinity.Account account = this.GetAccount();
            this.RequirePermission(Affinity.RolePermission.AdminSystem);
            ((MasterPage) this.Master).SetLayout("Administration Dashboard", MasterPage.LayoutStyle.ContentOnly);

            Hashtable active = this.GetActiveUsers();
            foreach (string key in active.Keys)
            {
                DateTime dt = (DateTime)active[key];
                TimeSpan diff = DateTime.Now.Subtract(dt);

                string tag = "<div class='active clearfix'>"
                    + "<div class='active_user account'>" + key + "</div>"
                    + "<div class='active_date'>" + dt.ToShortDateString() + "</div>"
                    + "<div class='active_time'>" + diff.Minutes + " minutes ago</div>"
                    + "</div>\r\n";
                pnlActive.Controls.Add(new LiteralControl(tag));
            }

            if (account.Role.HasPermission(Affinity.RolePermission.AffinityManager))
            {
                lnkManageAccounts.Visible = true;
                lnkSiteContentByRole.Visible = true;
                lnkManageCompanies.Visible = true;
                lnkManageNotifications.Visible = true;
                lnkManageWebContent.Visible = true;
                lnkReports.Visible = true;
            }

            if (account.Role.HasPermission(Affinity.RolePermission.AffinityStaff) && account.Role.HasPermission(Affinity.RolePermission.AffinityManager))
            {
                system_container.Visible = true;
            }

            lblTimeout.Text = Session.Timeout.ToString();

        }
    }
}