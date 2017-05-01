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

public partial class AdminAttachmentPurposes : PageBase
{
	protected void Page_Load(object sender, EventArgs e)
	{
		this.RequirePermission(Affinity.RolePermission.AdminSystem);
		this.RequirePermission(Affinity.RolePermission.AffinityManager);
		this.RequirePermission(Affinity.RolePermission.AffinityStaff);
		this.Master.SetLayout("Administration Dashboard", MasterPage.LayoutStyle.ContentOnly);


		Affinity.AttachmentPurposes aps = new Affinity.AttachmentPurposes(this.phreezer);
		Affinity.AttachmentPurposeCriteria crit = new Affinity.AttachmentPurposeCriteria();
		crit.AppendToOrderBy("Description");
		aps.Query(crit);
		
		aGrid.DataSource = aps;
		aGrid.DataBind();
	
	}
}
