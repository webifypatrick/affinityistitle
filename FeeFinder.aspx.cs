using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class FeeFinder : PageBase
{
	protected void Page_Load(object sender, EventArgs e)
	{
		// Affinity.Account account = this.GetAccount();
		this.Master.SetLayout("Fee Finder", MasterPage.LayoutStyle.ContentOnly);
		// this.RequirePermission(Affinity.RolePermission.SubmitOrders);

	}
}