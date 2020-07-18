using System;
using System.Configuration;

namespace Affinity
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        private FeedbackType fbtype = FeedbackType.Information;
        public bool showPopup = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            string active = "";
            if (Request["iframe"] != null) active = "iframe";
            this.SetNavigation(active);

            string feedback = Request["feedback"];
            if (feedback != null)
            {
                this.ShowFeedback(feedback, fbtype);
            }

            string isNotifications = Request["showNotifications"];
            if (isNotifications != null)
            {
                showNotifications();
            }

            string notificationRead = Request["notificationRead"];
            if (notificationRead != null)
            {
                int notificationReadInt = 0;
                int.TryParse(notificationRead, out notificationReadInt);
                readNotifications(notificationReadInt);
            }

            checkNotifications();

            /*
            if(Session["showPopup"] != null && Session["showPopup"].Equals("1"))
            {
                Popup.Visible = true;
                Session["showPopup"] = "0";
            }
            */

            //PageBase pb = (PageBase)this.Page;
            // Affinity.Account acc = pb.GetAccount();
            if (Session["IsDemo"] != null && Session["RoleCode"] != null)
            {
                lblDemo.Text = "DEMO MODE: " + Session["RoleCode"];
                pnlDemo.Visible = true;
            }
        }

        public void SetNavigation(string active)
        {
            // set the navigation based on this users permissions
            PageBase pb = (PageBase)this.Page;
            Affinity.Account acc = pb.GetAccount();

            if (acc.Role.IsAuthenticated())
            {
                this.lnkHome.Visible = false;
                this.lnkAbout.Visible = false;
                lnkNews.Visible = false;
                this.checkFrame.Visible = false;
                this.lnkLogout.Visible = true;

                Com.VerySimple.Phreeze.Phreezer phreeze = new Com.VerySimple.Phreeze.Phreezer(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString);

                using (var sdr = phreeze.ExecuteReader("SELECT * FROM site_content_roles WHERE scr_role_code = '" + acc.RoleCode + "' AND scr_content_section = 'menu'"))
                {
                    if (sdr.HasRows)
                    {
                            while (sdr.Read())
                            {
                                  switch(sdr["scr_menu_item"].ToString())
                                   {
                                       case "My Account":
                                           this.lnkMyAccount.Visible = true;
                                           break;
                                       case "Attorney Services":
                                           this.lnkAttorneyServices.Visible = true;
                                           break;
                                       case "GFE Calculator":
                                           //this.lnkFeeFinder.Visible = true;
                                           break;
                                       case "HUD Calculator":
                                           this.lnkHUDCalculator.Visible = true;
                                           break;
                                       case "Forms":
                                           this.lnkForms.Visible = true;
                                           break;
                                       case "My Preferences":
                                           lnkPreferences.Visible = true;
                                           break;
                                       case "Administration":
                                           this.lnkAdmin.Visible = true;
                                           break;
                                       case "Contact":
                                           this.lnkContact.Visible = true;
                                           break;
                                       case "Demo":
                                           this.lnkDemo.Visible = true;
                                           break;
                                       case "Logout":
                                           this.lnkLogout.Visible = true;
                                           break;
                                       default:
                                           break;
                                   }
                               }
                        }
                        else
                        {
                            this.lnkMyAccount.Visible = true;
                            lnkPreferences.Visible = true;
                            this.lnkForms.Visible = true;
                            this.lnkHUDCalculator.Visible = true;

                            //this.lnkAdmin.Visible = acc.Role.HasPermission(Affinity.RolePermission.AdminSystem);
                            //this.lnkAttorneyServices.Visible = acc.Role.HasPermission(Affinity.RolePermission.AttorneyServices);

                            //if (acc.RoleCode.Equals("Sales") || acc.RoleCode.Equals("Admin"))
                            //{
                           //     this.lnkDemo.Visible = true;
                           // }
                        }
                   }
                   phreeze.Close();
            }
            else
            {
                this.timeoutdiv.Visible = false;
            }

            if (active.Equals("iframe"))
            {
                this.timeoutdiv.Visible = false;
                this.headerDIV.Visible = false;
                this.pnlNav.Visible = false;
                this.lblcontent_footer.Visible = false;
                this.checkFrame.Visible = false;
            }

        }

        public void SetLayout(string title, LayoutStyle style)
        {
            // this.Page.Header.Title += ": " + title;
            this.Page.Header.Title = "Affinity Title Services: " + title;

            switch (style)
            {
                case LayoutStyle.Photo_01:
                    this.stage.Attributes.Add("class", "stage clearfix table home");
                    this.leftside.Attributes.Add("class", "td logincontent");
                    this.rightside.Attributes.Add("class", "td photo_01");
                    this.content.Attributes.Add("class", "login");
                    break;
                default:
                    // we don't need to do this because this is the default setting already, but if we did:
                    // this.stage.Attributes.Add("class", "stage clearfix");
                    break;
            }
        }

        public void ShowFeedback(string message, FeedbackType feedbacktype)
        {
            fbtype = feedbacktype;

            switch (fbtype)
            {
                case FeedbackType.Error:
                    pnlFeedback.CssClass = "error";
                    break;
                case FeedbackType.Warning:
                    pnlFeedback.CssClass = "warning";
                    break;
                default:
                    pnlFeedback.CssClass = "information";
                    break;
            }

            lblFeedback.Text = message;
            pnlFeedback.Visible = true;
        }

        public void checkNotifications()
        {
            int notificationsCountInt = 0;
            if (Session[PageBase.accountSessionVar] != null)
            {
                if (Session["checkedNotifications"] == null)
                {
                    Com.VerySimple.Phreeze.Phreezer phreeze = new Com.VerySimple.Phreeze.Phreezer(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString);

                    NotificationAccounts notes = new NotificationAccounts(phreeze);
                    NotificationAccountCriteria nc = new NotificationAccountCriteria();
                    nc.IsRead = 0;
                    Account account = (Affinity.Account)Session[PageBase.accountSessionVar];
                    nc.AccountId = account.Id;

                    notes.Query(nc);
                    notificationsCountInt = notes.Count;
                    Session["checkedNotifications"] = notificationsCountInt.ToString();
                    phreeze.Close();
                }
                else
                {
                    int.TryParse(Session["checkedNotifications"].ToString(), out notificationsCountInt);
                }
                if (notificationsCountInt > 0)
                {
                    NotificationsDIV.Visible = true;
                    unreadRecordCount.InnerHtml = notificationsCountInt.ToString();
                }
            }
        }

        public void showNotifications()
        {
            Com.VerySimple.Phreeze.Phreezer phreeze = new Com.VerySimple.Phreeze.Phreezer(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString);

            PageBase pb = (PageBase)this.Page;
            Affinity.Account acc = pb.GetAccount();

            NotificationAccounts noteaccts = new NotificationAccounts(phreeze);
            NotificationAccountCriteria noteacctcrit = new NotificationAccountCriteria();
            noteacctcrit.AccountId = acc.Id;
            noteacctcrit.IsRead = 0;
            noteaccts.Query(noteacctcrit);

            foreach (NotificationAccount noteacct in noteaccts)
            {
                //Response.Write("Note ID:" + noteacct.NotificationId.ToString() + "\n");
                Notification note = new Notification(phreeze);
                note.Load(noteacct.NotificationId);

                Response.Write("<tr notificationId=\"" + note.Id.ToString() + "\"><td>" + note.Subject + "</td><td>" + note.Created.ToShortDateString() + "</td><td style=\"display: none;\" id=\"Message" + note.Id.ToString() + "\">" + note.Message.Replace("\n", "<br />").Replace("'", "\\'") + "</td></tr>");
            }
            phreeze.Close();
            Response.End();
        }

        public void readNotifications(int notificationId)
        {
            Com.VerySimple.Phreeze.Phreezer phreeze = new Com.VerySimple.Phreeze.Phreezer(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString);

            PageBase pb = (PageBase)this.Page;
            Affinity.Account acc = pb.GetAccount();

            NotificationAccounts noteaccts = new NotificationAccounts(phreeze);
            NotificationAccountCriteria noteacctcrit = new NotificationAccountCriteria();
            noteacctcrit.AccountId = acc.Id;
            noteacctcrit.NotificationId = notificationId;
            noteaccts.Query(noteacctcrit);

            foreach (NotificationAccount noteacct in noteaccts)
            {
                noteacct.IsRead = 1;
                noteacct.Update();
            }

            NotificationAccounts notes = new NotificationAccounts(phreeze);
            NotificationAccountCriteria nc = new NotificationAccountCriteria();
            nc.IsRead = 0;
            Account account = (Affinity.Account)Session[PageBase.accountSessionVar];
            nc.AccountId = account.Id;

            notes.Query(nc);
            Session["checkedNotifications"] = notes.Count.ToString();
            phreeze.Close();
            Response.End();
        }

        public enum LayoutStyle
        {
            ContentOnly = 1,
            Photo_01 = 2
        }

        public enum FeedbackType
        {
            Information = 1,
            Warning = 2,
            Error = 3
        }

    }
}