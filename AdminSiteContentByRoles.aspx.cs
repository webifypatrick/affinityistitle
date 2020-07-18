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
using System.Collections.Generic;

namespace Affinity
{
    public partial class AdminSiteContentByRoles : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.RequirePermission(Affinity.RolePermission.AdminSystem);
            this.RequirePermission(Affinity.RolePermission.AffinityManager);
            ((Affinity.MasterPage) this.Master).SetLayout("Manage Site Content By Roles", MasterPage.LayoutStyle.ContentOnly);
            var sdr = this.phreezer.ExecuteReader("SELECT scr_role_code, scr_content_section FROM site_content_roles group by scr_role_code, scr_content_section");

            aGrid.DataSource = sdr;
            aGrid.DataBind();
        }
    }

    public class RoleCode
    {
        public string ID;
        public string Code;
    }
}