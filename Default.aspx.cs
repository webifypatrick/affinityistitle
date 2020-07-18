using System;
using System.Web.Security;

namespace Affinity
{
    public partial class _Default : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((Affinity.MasterPage) this.Master).SetLayout("Please Login", MasterPage.LayoutStyle.Photo_01);

            // get the main content.  this is cached for performance
            Affinity.Content c;
            if (Application[Affinity.Content.HomePageCode] == null)
            {
                c = new Affinity.Content(this.phreezer);
                c.Load(Affinity.Content.HomePageCode);
                Application[Affinity.Content.HomePageCode] = c;

            }
            else
            {
                c = (Affinity.Content)Application[Affinity.Content.HomePageCode];
            }

            header.InnerHtml = c.Header;
            welcome.InnerHtml = c.Body;

        }

        /// <summary>
        /// processes the authentaction provided and redirects to the approp page
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        private void ProcessLogin(string username, string password)
        {
            Affinity.Account account = new Affinity.Account(this.phreezer);

            if (account.Login(username, password))
            {
                if (DateTime.Compare(DateTime.Now, new DateTime(2018, 5, 10)) == -1)
                {
                    Session["showPopup"] = "1";
                }
                // save the account in the session
                this.SetAccount(account);

                // also add to the active users list
                this.AddActiveUser(account.Username);

                if (FormsAuthentication.GetRedirectUrl(txtUsername.Text, false).EndsWith("default.aspx"))
                {
                    // the user is on the home page so they have not tried to access another page
                    FormsAuthentication.SetAuthCookie(txtUsername.Text, false);

                    if (account.Role.HasPermission(Affinity.RolePermission.AffinityManager) || account.Role.HasPermission(Affinity.RolePermission.AffinityStaff))
                    {
                        this.Redirect("Admin.aspx");
                    }
                    else
                    {
                        this.Redirect("MyAccount.aspx");
                    }
                }
                else
                {
                    // the user was trying to access another page, so redirect them where they originally \
                    // wanted to go
                    //Response.Write("'" + FormsAuthentication.GetRedirectUrl(txtUsername.Text, false)+"'");
                    //Response.End();
                    FormsAuthentication.RedirectFromLoginPage(txtUsername.Text, false);
                }
            }
            else
            {
                log.Warn("Invalid password for username " + username);
                ((Affinity.MasterPage)this.Master).ShowFeedback("Unknown Username/Password", MasterPage.FeedbackType.Warning);
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            ProcessLogin(txtUsername.Text, txtPassword.Text);
        }
    }
}