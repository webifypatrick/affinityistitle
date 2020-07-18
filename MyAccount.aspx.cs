using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Affinity
{
    public partial class MyAccount : PageBase
    {
        //private bool searchFilter;

        protected void Page_Load(object sender, EventArgs e)
        {
            Affinity.Account account = this.GetAccount();
            ((Affinity.MasterPage) this.Master).SetLayout("My Account", MasterPage.LayoutStyle.ContentOnly);
            this.RequirePermission(Affinity.RolePermission.SubmitOrders);

            if (account.Role.HasPermission(Affinity.RolePermission.AttorneyServices))
            {
                DisclosureOwnerDocumentsDIV.Visible = true;
            }

            if (account.Role.HasPermission(Affinity.RolePermission.BrokerAgent))
            {
                DisclosureAgentDocumentsDIV.Visible = true;
            }

            if (!Page.IsPostBack)
            {

                if (Request["Demo"] != null)
                {
                    if (!Request["Demo"].Equals("Off"))
                    {
                        Session["RoleCode"] = Request["Demo"];
                        Session["IsDemo"] = "1";
                    }
                    else
                    {
                        Session["RoleCode"] = null;
                        Session["IsDemo"] = null;
                    }
                }

                string RoleCode = (Session["RoleCode"] == null) ? account.RoleCode : Session["RoleCode"].ToString();

                if (RoleCode.Equals("Realtor"))
                {
                    DisclosureAgentDocumentsDIV.Visible = false;
                }

                if (RoleCode.Equals("Attorney"))
                {
                    BlankControlledOwners.Visible = false;
                }


                // this gets rid of any half-submitted orders in case the customer
                // has been using the back button in mid-order process
                account.CleanUpOrphanOrders();

                LoadGrid("Modified", SortDirection.Descending, 0);

                LoadActions();
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
            if (Session["IsDemo"] != null)
            {
                oc.IsDemo = true;
            }
            oc.AppendToOrderBy(se, sd == SortDirection.Descending);
            oc.HideInactive = (!cbShowInactive.Checked);

            // set the page index after binding
            oGrid.PageIndex = pi;

            // TODO: implment custom paging for performance
            oc.PagingEnabled = true;
            oc.PageSize = oGrid.PageSize;
            oc.Page = oGrid.PageIndex + 1;

            if (txtQuery.Text != "")
            {
                oc.SearchQuery = txtQuery.Text;
                // TODO: implement closed
                //oc.CustomerStatusCode = cbShowClosed.Checked ? "" : "";
                //searchFilter = false;
            }

            // we can either show the accounts orders, or if they are a manager, we show
            // all from their company
            if (this.GetAccount().Role.HasPermission(Affinity.RolePermission.ModifyOtherEmployeeOrders))
            {
                oGrid.DataSource = this.GetAccount().GetCompanyOrders(oc);
            }
            else
            {
                oGrid.DataSource = this.GetAccount().GetOrders(oc);
            }
            oGrid.DataBind();


            ViewState["lastSE"] = se;
            ViewState["lastSD"] = sd;
        }

        /// <summary>
        /// show the actions available to this user for submitting new orders
        /// </summary>
        protected void LoadActions()
        {
            /*
            // show the available actions that can be done from the database
            Affinity.RequestTypes rts = new Affinity.RequestTypes(this.phreezer);
            Affinity.RequestTypeCriteria rtc = new Affinity.RequestTypeCriteria();
            rtc.IsActive = 1;
            rts.Query(rtc);

            pnlActions.Controls.Add(new LiteralControl("<div><a class=\"add\" href=\"MyOrderSubmit.aspx?code=" + Affinity.RequestType.DefaultCode + "\">Submit a New Order</a></div>"));
            foreach (Affinity.RequestType rt in rts)
            {
                if (rt.Code != Affinity.RequestType.DefaultCode)
                {
                    pnlActions.Controls.Add(new LiteralControl("<div><a class=\"add\" href=\"MyOrderSubmit.aspx?code=" + rt.Code + "\">Submit a New " + rt.Description + "</a></div>"));
                }
            }
            */

            // show hard-coded actions
            //this.pnlActions.Controls.Add(new LiteralControl("<div><a class=\"add\" href=\"MyOrderSubmit.aspx\">Submit a New Order with Survey Services</a></div>"));
            //this.pnlActions.Controls.Add(new LiteralControl("<div><a onclick=\"return confirm('This will allow you to submit a request for a closing room if you have NOT submitted an order with Affinity.');\" class=\"add\" href=\"MyOrderSubmit.aspx?code=ClosingRequest\">Submit a 'Closing Room Only' Request</a></div>"));
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
}