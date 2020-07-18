using System;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Affinity
{
    public partial class AdminReport : PageBase
    {

        private Affinity.RequestType rtype;
        private XmlForm xmlForm;

        /// <summary>
        /// The form controls are created at this point.  if we create them at page load
        /// then their viewstate will not be persisted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        override protected void PageBase_Init(object sender, System.EventArgs e)
        {
            // we have to call the base first so phreezer is instantiated
            base.PageBase_Init(sender, e);

            rtype = new Affinity.RequestType(this.phreezer);
            rtype.Load(Affinity.RequestType.SystemSettings);

            xmlForm = new XmlForm(this.GetAccount());

            //Affinity.SystemSetting ss = new Affinity.SystemSetting(this.phreezer);
            //ss.Load("SYSTEM");

            //pnlForm.Controls.Add(xmlForm.GetFormFieldControl(rtype.Definition, ss.Data));
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
            oc.ReportType = "Search Package / Commitment Posted";
            if (Session["IsDemo"] != null)
            {
                oc.IsDemo = true;
            }
            oc.AppendToOrderBy(se, sd == SortDirection.Descending);

            // set the page index after binding
            oGrid.PageIndex = pi;

            // TODO: implment custom paging for performance
            oc.PagingEnabled = true;
            oc.PageSize = oGrid.PageSize;
            oc.Page = oGrid.PageIndex + 1;

            Affinity.Orders orders = new Affinity.Orders(this.phreezer);
            orders.Query(oc);

            oGrid.DataSource = orders;
            oGrid.DataBind();


            ViewState["lastSE"] = se;
            ViewState["lastSD"] = sd;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.RequirePermission(Affinity.RolePermission.AffinityManager);
            //this.RequirePermission(Affinity.RolePermission.AffinityStaff);
            //this.RequirePermission(Affinity.RolePermission.SubmitOrders);
            ((Affinity.MasterPage)this.Master).SetLayout("Report", MasterPage.LayoutStyle.ContentOnly);

            if (!Page.IsPostBack)
            {
                LoadGrid("Modified", SortDirection.Descending, 0);
            }
        }
    }
}