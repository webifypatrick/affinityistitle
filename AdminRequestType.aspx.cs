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

public partial class AdminRequestType : PageBase
{
	Affinity.RequestType rt = null;
	protected void Page_Load(object sender, EventArgs e)
	{
		this.RequirePermission(Affinity.RolePermission.AdminSystem);
		this.RequirePermission(Affinity.RolePermission.AffinityManager);
		this.RequirePermission(Affinity.RolePermission.AffinityStaff);
		this.Master.SetLayout("Administration", MasterPage.LayoutStyle.ContentOnly);

		string code = NoNull.GetString(Request.QueryString["code"],"");
		
		// load the specified request type
		rt = new Affinity.RequestType(this.phreezer);
		rt.Load(code);
		codeHdn.Value = code;
		
		RequestTypepnl.InnerHtml = rt.Description;
		
		// load the checkboxes if this is not postback
		if (!Page.IsPostBack)
		{
			IEnumerator i = ExportFormatboxes.Items.GetEnumerator();
			string exportformats = "," + rt.ExportFormats + ",";
			
			// loop through the checkboxes and check the ones that are in the database
			while(i.MoveNext())
			{
				ListItem li = (ListItem) i.Current;
				li.Selected = (exportformats.IndexOf("," + li.Value + ",") > -1);
			}
		}
	}

	/// <summary>
	/// When the Set Export Formats button is clicked, the export formats checked will be saved for this request type
	/// </summary>
	protected void btnSetExportFormats_Click(object sender, EventArgs e)
	{
		IEnumerator i = ExportFormatboxes.Items.GetEnumerator();
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		
		// loop through the checkboxes and if checked, append them to a comma-delimited string to be saved to the database
		while(i.MoveNext())
		{
			ListItem li = (ListItem) i.Current;
			if(li.Selected) sb.Append(li.Value + ",");
		}
		if(sb.Length > 0) sb.Length--;
		
		// set the export formats to the object property
		rt.ExportFormats = sb.ToString();
		
		// save the export formats to the database
		rt.Update();
		this.Master.ShowFeedback("Export Formats have been saved for " + rt.Description + ".", MasterPage.FeedbackType.Information);
	}
}
