using System;

namespace Affinity
{
    public partial class AdminNotifications : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.RequirePermission(Affinity.RolePermission.AdminSystem);
            this.RequirePermission(Affinity.RolePermission.AffinityManager);
            ((Affinity.MasterPage)this.Master).SetLayout("Manage Notifications", MasterPage.LayoutStyle.ContentOnly);

            Affinity.Notifications cps = new Affinity.Notifications(this.phreezer);
            Affinity.NotificationCriteria crit = new Affinity.NotificationCriteria();
            crit.AppendToOrderBy("Created");

            cps.Query(crit);

            cGrid.DataSource = cps;
            cGrid.DataBind();
        }
    }
}