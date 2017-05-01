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

public partial class AdminOrders : PageBase
{
	private bool searchFilter;

	protected void Page_Load(object sender, EventArgs e)
	{
		this.Master.SetLayout("Manage Orders", MasterPage.LayoutStyle.ContentOnly);
		this.RequirePermission(Affinity.RolePermission.AdminSystem);

		if (!Page.IsPostBack)
		{
			LoadGrid("Modified", SortDirection.Descending, 0);
			LoadActions();
		}
		else
		{
			/*
			cbShowStandard.Checked = Request["ctl000_content_cph_cbShowStandard"] != null; 
			LoadGrid(
				(string)ViewState["lastSE"],
				(SortDirection)ViewState["lastSD"],
				0);
				*/
			}

		if(Request["ctl00$content_cph$StandardImported"] != null) {
			//Response.Write(Request["ctl00$content_cph$StandardImported"]);
		}
	}

	/// <summary>
	/// handle click when col is sorted
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void oGrid_Sorting(object sender, GridViewSortEventArgs e)
	{
		SortDirection sd = e.SortDirection;
		string se = e.SortExpression;

		// if the same sort is clicked twice, we need to reverse the sort direction
		if (se.Equals(ViewState["lastSE"]))
		{
			sd = ViewState["lastSD"].Equals(SortDirection.Descending) ? SortDirection.Ascending : SortDirection.Descending;
		}

		LoadGrid(se, sd, 0);
	}

	/// <summary>
	/// handle when page is changed
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void oGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		LoadGrid(
			(string)ViewState["lastSE"],
			(SortDirection)ViewState["lastSD"],
			e.NewPageIndex);
	}

	/// <summary>
	/// populate the grid from this accounts data
	/// </summary>
	/// <param name="se">sort expression</param>
	/// <param name="sd">sort direction</param>
	/// <param name="pi">page index</param>
	protected void LoadGrid(string se, SortDirection sd, int pi)
	{

		Affinity.OrderCriteria oc = new Affinity.OrderCriteria();
		oc.AppendToOrderBy(se, sd == SortDirection.Descending);

		// don't show closed orders unless the box is checked
		oc.HideInternalClosed = (!cbShowClosed.Checked);

		// set the page index after binding
		oGrid.PageIndex = pi;

		// TODO: implment custom paging for performance
		oc.PagingEnabled = true;
		oc.PageSize = oGrid.PageSize;
		oc.Page = oGrid.PageIndex + 1;
		oc.OrderUploadLogId = (cbShowStandard.Checked)? 0 : 1;

		if (txtQuery.Text != "")
		{
			oc.SearchQuery = txtQuery.Text;
			// TODO: implement closed
			//oc.CustomerStatusCode = cbShowClosed.Checked ? "" : "";
			searchFilter = false;
		}

		Affinity.Orders orders = new Affinity.Orders(this.phreezer);
		orders.Query(oc);

		oGrid.DataSource = orders;
		oGrid.DataBind();


		ViewState["lastSE"] = se;
		ViewState["lastSD"] = sd;
	}

	/// <summary>
	/// show the actions available to this user for submitting new orders
	/// </summary>
	protected void LoadActions()
	{
	}


	/// <summary>
	/// 
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		LoadGrid(
			(string)ViewState["lastSE"],
			(SortDirection)ViewState["lastSD"],
			0);
	}
}
