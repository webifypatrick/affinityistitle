using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Affinity
{
    public partial class AdminNotification : PageBase
    {
        private Affinity.Notification notification;
        private bool isUpdate;

        override protected void PageBase_Init(object sender, System.EventArgs e)
        {
            // we have to call the base first so phreezer is instantiated
            base.PageBase_Init(sender, e);

            int id = NoNull.GetInt(Request["id"], 0);
            this.notification = new Affinity.Notification(this.phreezer);

            this.isUpdate = (!id.Equals(0));

            if (this.isUpdate)
            {
                this.notification.Load(id);
            }
        }


        /// <summary>
        /// Persist to DB
        /// </summary>
        protected void UpdateNotification()
        {
            DateTime nulldate1 = new DateTime(1, 1, 1);
            DateTime nulldate2 = new DateTime(1, 1, 1);
            DateTime validfrom = nulldate1;

            if (!txtValidFrom.Text.Trim().Equals(""))
            {
                DateTime.TryParse(txtValidFrom.Text, out validfrom);
            }

            DateTime validto = nulldate2;

            if (!txtValidTo.Text.Trim().Equals(""))
            {
                DateTime.TryParse(txtValidTo.Text, out validto);
            }

            //this.notification.Id = int.Parse(txtId.Text);
            this.notification.Subject = txtSubject.Text;
            this.notification.Message = txtMessage.Text;
            int accountid = GetAccount().Id;

            this.notification.ValidFrom = validfrom;
            this.notification.ValidTo = validto;
            this.notification.OriginatorId = accountid;
            DateTime now = DateTime.Now;
            int notificationid = 0;

            if (this.notification.Id == 0)
            {
                this.notification.Created = now;
                this.notification.Modified = now;
                this.notification.Insert();

                Notifications notes = new Notifications(this.phreezer);
                NotificationCriteria notecrit = new NotificationCriteria();
                notecrit.OriginatorID = accountid;
                notecrit.Subject = txtSubject.Text;
                notecrit.Created = now;
                notecrit.AppendToOrderBy("Created", true);
                notes.Query(notecrit);

                foreach(Notification note in notes)
                {
                    notificationid = note.Id;
                    break;
                }
            }
            else
            {
                notificationid = this.notification.Id;
                this.notification.Modified = DateTime.Now;
                this.notification.Update();
            }

            if (notificationid > 0)
            {
                StringBuilder sb = new StringBuilder();
                NotificationAccount na = new NotificationAccount(this.phreezer);
                na.AccountId = 0;
                na.NotificationId = notificationid;
                na.IsRead = 0;

                if (selAccounts.SelectedValue.Equals("0"))
                {
                    Accounts accts = new Accounts(this.phreezer);
                    AccountCriteria acrit = new AccountCriteria();
                    acrit.StatusCode = "Active";
                    accts.Query(acrit);

                    foreach (Account acct in accts)
                    {
                        if (sb.Length > 0)
                        {
                            sb.Append(",");
                        }
                        sb.Append(acct.Id.ToString());
                    }
                }
                else
                {
                    foreach (ListItem item in selAccounts.Items)
                    {
                        if(item.Selected)
                        {
                            if (sb.Length > 0)
                            {
                                sb.Append(",");
                            }
                            sb.Append(item.Value);
                        }
                    }
                }

                na.AccountIds = sb.ToString();
                na.Insert();
            }

            this.Redirect("AdminNotifications.aspx?feedback=Notification+Updated");
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            this.RequirePermission(Affinity.RolePermission.AdminSystem);
            this.RequirePermission(Affinity.RolePermission.AffinityManager);
            ((Affinity.MasterPage)this.Master).SetLayout("Notification", MasterPage.LayoutStyle.ContentOnly);

            this.btnDelete.Attributes.Add("onclick", "return confirm('Delete this record?')");

            if (!Page.IsPostBack)
            {
                // populate the form
                txtId.Text = this.notification.Id.ToString();
                txtSubject.Text = this.notification.Subject.ToString();
                txtMessage.Text = this.notification.Message.ToString();
                DateTime nulldate1 = new DateTime(1, 1, 1);
                DateTime nulldate2 = new DateTime(1999, 9, 9);

                if (this.notification.ValidFrom != null && !this.notification.ValidFrom.Equals(nulldate1) && !this.notification.ValidFrom.Equals(nulldate2))
                {
                    txtValidFrom.Text = this.notification.ValidFrom.ToShortDateString();
                }

                if (this.notification.ValidTo != null && !this.notification.ValidTo.Equals(nulldate1) && !this.notification.ValidTo.Equals(nulldate2))
                {
                    txtValidTo.Text = this.notification.ValidTo.ToShortDateString();
                }
                txtCreated.Text = this.notification.Created.ToShortDateString() + " " + this.notification.Created.ToShortTimeString();
                txtModified.Text = this.notification.Modified.ToShortDateString() + " " + this.notification.Modified.ToShortTimeString();

                selAccounts.Items.Clear();

                Accounts accts = new Accounts(this.phreezer);
                AccountCriteria acctcrit = new AccountCriteria();
                acctcrit.StatusCode = "Active";
                acctcrit.AppendToOrderBy("LastName");
                acctcrit.AppendToOrderBy("FirstName");
                accts.Query(acctcrit);
                selAccounts.Items.Clear();
                selAccounts.Items.Add(new ListItem("All Accounts", "0"));

                foreach (Account acct in accts)
                {
                    if (!acct.LastName.Trim().Equals("") && !acct.FirstName.Trim().Equals("") && acct.Id > 0)
                    {
                        ListItem item = new ListItem(acct.LastName + ", " + acct.FirstName, acct.Id.ToString());
                        foreach (Account nacct in this.notification.Accounts)
                        {
                            if (nacct.Id == acct.Id)
                            {
                                item.Selected = true;
                                break;
                            }
                        }

                        selAccounts.Items.Add(item);
                    }
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            UpdateNotification();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.Redirect("AdminNotifications.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.notification.Delete();
                this.Redirect("AdminNotifications.aspx?feedback=Notification+Deleted");
            }
            catch (Exception ex)
            {
                ((Affinity.MasterPage)this.Master).ShowFeedback("The notification cannot be deleted.  Most likely because there are still accounts assigned to it.", MasterPage.FeedbackType.Error);
            }
        }
    }
}