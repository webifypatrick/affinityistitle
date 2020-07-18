using System;

namespace Affinity
{
    public partial class FeeFinder : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Affinity.Account account = this.GetAccount();
            ((Affinity.MasterPage)this.Master).SetLayout("Fee Finder", MasterPage.LayoutStyle.ContentOnly);
            // this.RequirePermission(Affinity.RolePermission.SubmitOrders);

        }
    }
}