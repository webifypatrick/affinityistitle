using System;
using System.Web.Security;

namespace Affinity
{
    public partial class Logout : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.DeleteActiveUser(this.GetAccount().Username);
            FormsAuthentication.SignOut();
            Session.Abandon();
            this.SetAccount(null);
            ((Affinity.MasterPage)this.Master).SetLayout("Thank You!", MasterPage.LayoutStyle.Photo_01);
            ((Affinity.MasterPage)this.Master).SetNavigation("");  // now that we're no longer authenticated, we need to reset the navigation
        }
    }
}
