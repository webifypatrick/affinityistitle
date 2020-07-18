using System;

namespace Affinity
{
    public partial class AdminCompanies : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.RequirePermission(Affinity.RolePermission.AdminSystem);
            this.RequirePermission(Affinity.RolePermission.AffinityManager);
            ((Affinity.MasterPage)this.Master).SetLayout("Manage Companies", MasterPage.LayoutStyle.ContentOnly);

            Affinity.Companys cps = new Affinity.Companys(this.phreezer);
            Affinity.CompanyCriteria crit = new Affinity.CompanyCriteria();
            crit.AppendToOrderBy("Name");

            cps.Query(crit);

            cGrid.DataSource = cps;
            cGrid.DataBind();
        }
    }
}