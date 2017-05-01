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

public partial class AdminRequestTypes : PageBase
{
	protected void Page_Load(object sender, EventArgs e)
	{
		this.RequirePermission(Affinity.RolePermission.AdminSystem);
		this.RequirePermission(Affinity.RolePermission.AffinityManager);
		this.RequirePermission(Affinity.RolePermission.AffinityStaff);
		this.Master.SetLayout("Administration", MasterPage.LayoutStyle.ContentOnly);

		// load the grid if this is not a postback
		if (!Page.IsPostBack)
		{
			LoadGrid();
		}
	}
	
	/// <summary>
	/// When the Reload button is clicked, the request types are reloaded from xml
	/// </summary>
	protected void btnReload_Click(object sender, EventArgs e)
	{
		string path = Server.MapPath("./RequestTypes/");

		Affinity.RequestType rt = new Affinity.RequestType(this.phreezer);
		rt.ReloadAllDefinitions(path);
		this.Master.ShowFeedback("Request Types have been reloaded", MasterPage.FeedbackType.Information);
	}


	/// <summary>
	/// populate the grid with request types
	/// </summary>
	protected void LoadGrid()
	{

		Affinity.RequestTypeCriteria rtc = new Affinity.RequestTypeCriteria();
		//rtc.IsActive = 1;

		Affinity.RequestTypes requesttypes = new Affinity.RequestTypes(this.phreezer);
		requesttypes.Query(rtc);
		oGrid.DataSource = requesttypes;
		oGrid.DataBind();
	}

}
