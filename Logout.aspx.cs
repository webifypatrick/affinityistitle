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

public partial class Logout : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
		this.DeleteActiveUser(this.GetAccount().Username);
        FormsAuthentication.SignOut();
        Session.Abandon();
        this.SetAccount(null);
        this.Master.SetLayout("Thank You!",MasterPage.LayoutStyle.Photo_01);
        this.Master.SetNavigation("");  // now that we're no longer authenticated, we need to reset the navigation
    }
}
