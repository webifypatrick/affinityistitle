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

public partial class AdminContents : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
		this.RequirePermission(Affinity.RolePermission.AdminSystem);
		this.RequirePermission(Affinity.RolePermission.AffinityManager);
		this.Master.SetLayout("Manage Content Pages", MasterPage.LayoutStyle.ContentOnly);

		Affinity.Contents cs = new Affinity.Contents(this.phreezer);
		cs.Query(new Affinity.ContentCriteria());

		cGrid.DataSource = cs;
		cGrid.DataBind();

    }
	protected void btnNew_Click(object sender, EventArgs e)
	{
		Response.Redirect("AdminContent.aspx");
	}
}
