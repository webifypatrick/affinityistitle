using System;

namespace Affinity
{
    public partial class AdminAttachmentPurposes : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.RequirePermission(Affinity.RolePermission.AdminSystem);
            this.RequirePermission(Affinity.RolePermission.AffinityManager);
            this.RequirePermission(Affinity.RolePermission.AffinityStaff);
            ((Affinity.MasterPage) this.Master).SetLayout("Administration Dashboard", MasterPage.LayoutStyle.ContentOnly);


            Affinity.AttachmentPurposes aps = new Affinity.AttachmentPurposes(this.phreezer);
            Affinity.AttachmentPurposeCriteria crit = new Affinity.AttachmentPurposeCriteria();
            crit.AppendToOrderBy("Description");
            aps.Query(crit);

            aGrid.DataSource = aps;
            aGrid.DataBind();

        }
    }
}