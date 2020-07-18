using System;
using System.Web.UI;

namespace Affinity
{
    public partial class AdminSiteContentByRole : PageBase
    {
        private Affinity.Account account;
        //private Affinity.RequestType rtype;
        //private XmlForm xmlForm;

        override protected void PageBase_Init(object sender, System.EventArgs e)
        {
            // we have to call the base first so phreezer is instantiated
            base.PageBase_Init(sender, e);

            this.account = new Affinity.Account(this.phreezer);
            this.RequirePermission(Affinity.RolePermission.AffinityManager);

            Affinity.Accounts accts = new Affinity.Accounts(this.phreezer);
            Affinity.AccountCriteria ac = new Affinity.AccountCriteria();
            ac.AppendToOrderBy("LastName");
            ac.AppendToOrderBy("FirstName");
            accts.Query(ac);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Affinity.MasterPage) this.Master).SetLayout("Edit Account", MasterPage.LayoutStyle.ContentOnly);
            this.RequirePermission(Affinity.RolePermission.AdminSystem);

            if (!Page.IsPostBack)
            {
                // populate the form

                Affinity.Roles roles = new Affinity.Roles(this.phreezer);
                Affinity.RoleCriteria rc = new Affinity.RoleCriteria();
                rc.AppendToOrderBy("Description");
                roles.Query(rc);

                ddRole.DataSource = roles;
                ddRole.DataTextField = "Description";
                ddRole.DataValueField = "Code";
                ddRole.DataBind();

                if (Request["code"] != null && !Request["code"].Equals("") && Request["section"] != null && !Request["section"].Equals(""))
                {
                    using (var sdr = this.phreezer.ExecuteReader("SELECT * FROM site_content_roles WHERE scr_role_code = '" + Request["code"] + "' AND scr_content_section = '" + Request["section"] + "'"))
                    {
                        while (sdr.Read())
                        {
                            ddMenuItems.Items.FindByText(sdr["scr_menu_item"].ToString()).Selected = true;
                        }
                    }
                    ddRole.Items.FindByValue(Request["code"]).Selected = true;
                    ddContentType.SelectedValue = Request["section"];
                }
                else
                {
                    ddRole.SelectedValue = this.account.RoleCode;
                }

                txtCreated.Text = this.account.Created.ToShortDateString();
                txtModified.Text = this.account.Modified.ToShortDateString();
            }
        }

        protected void ShowAccount()
        {
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string[] menuitems = Request["_ctl0:content_cph:ddMenuItems"].Split(',');

            this.phreezer.ExecuteNonQuery("DELETE FROM site_content_roles WHERE scr_role_code = '" + Request["_ctl0:content_cph:ddRole"] + "' AND scr_content_section = 'Menu'");

            for (int i = 0; i < menuitems.Length; i++)
            {
                this.phreezer.ExecuteNonQuery("INSERT INTO site_content_roles (scr_role_code, scr_state_code, scr_menu_item, scr_menu_override, scr_content_section, scr_content_label_override, scr_created, scr_modified) VALUES ('" + Request["_ctl0:content_cph:ddRole"] + "', '" + Request["_ctl0:content_cph:txtState"] + "', '" + menuitems[i] + "', '', 'Menu', '', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
            }
            
            this.Redirect("AdminSiteContentByRoles.aspx?feedback=Site+Content+By+Role+Saved");
        }

        protected void btnCopyAccount_Click(object sender, EventArgs e)
        {
            //UpdateAccount(true);
            //this.Redirect("AdminAccounts.aspx?feedback=Account+Saved");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.Redirect("AdminSiteContentByRoles.aspx");
        }
    }
}