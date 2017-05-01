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


public partial class AdminAccounts : PageBase
{
	protected void Page_Load(object sender, EventArgs e)
	{
		this.RequirePermission(Affinity.RolePermission.AdminSystem);
		this.RequirePermission(Affinity.RolePermission.AffinityManager);
		this.Master.SetLayout("Manage Accounts", MasterPage.LayoutStyle.ContentOnly);

		Affinity.Accounts accs = new Affinity.Accounts(this.phreezer);
		Affinity.SearchAccountCriteria crit = new Affinity.SearchAccountCriteria();
		crit.AppendToOrderBy("Username");

		if (Request["srch"] != null && !Request["srch"].Equals(""))
		{
			string searchcriteria = Request["srch"];
			crit.FirstName = searchcriteria;
			crit.LastName = searchcriteria;
			//crit. = searchcriteria;
			crit.Username = searchcriteria;
			crit.Email = searchcriteria;
			crit.Company = searchcriteria;
			crit.RoleCode = searchcriteria;
		}

		accs.Query(crit);

		aGrid.DataSource = accs;
		aGrid.DataBind();
	}
}
