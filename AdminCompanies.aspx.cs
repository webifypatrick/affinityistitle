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

public partial class AdminCompanies : PageBase
{
	protected void Page_Load(object sender, EventArgs e)
	{
		this.RequirePermission(Affinity.RolePermission.AdminSystem);
		this.RequirePermission(Affinity.RolePermission.AffinityManager);
		this.Master.SetLayout("Manage Companies", MasterPage.LayoutStyle.ContentOnly);

		Affinity.Companys cps = new Affinity.Companys(this.phreezer);
		Affinity.CompanyCriteria crit = new Affinity.CompanyCriteria();
		crit.AppendToOrderBy("Name");

		cps.Query(crit);

		cGrid.DataSource = cps;
		cGrid.DataBind();
	}
}
