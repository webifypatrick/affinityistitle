using System;

namespace Affinity
{
    public partial class AdminSettings : PageBase
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

            LoadForm();
            //Affinity.SystemSetting ss = new Affinity.SystemSetting(this.phreezer);
            //ss.Load("SYSTEM");

            //pnlForm.Controls.Add(xmlForm.GetFormFieldControl(rtype.Definition, ss.Data));
        }

        private void LoadForm()
        {
            Affinity.SystemSetting ss = new Affinity.SystemSetting(this.phreezer);
            ss.Load(Affinity.SystemSetting.DefaultCode);
            pnlForm.Controls.Clear();
            pnlForm.Controls.Add(xmlForm.GetFormFieldControl(rtype.Definition, ss.Data));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.RequirePermission(Affinity.RolePermission.AffinityManager);
            this.RequirePermission(Affinity.RolePermission.AffinityStaff);
            this.RequirePermission(Affinity.RolePermission.SubmitOrders);
            ((Affinity.MasterPage)this.Master).SetLayout("System Settings", MasterPage.LayoutStyle.ContentOnly);
        }

        protected void UpdateSettings()
        {
            Affinity.SystemSetting ss = new Affinity.SystemSetting(this.phreezer);
            ss.Load(Affinity.SystemSetting.DefaultCode);

            ss.Data = XmlForm.XmlToString(xmlForm.GetResponse(pnlForm));
            ss.Update();

            // we have to update the application global as well because otherwise it won't 
            // reflect the changes until the web application is restarted
            Application[Affinity.SystemSetting.DefaultCode] = ss.Settings;

            ((Affinity.MasterPage)this.Master).ShowFeedback("System Settings have been updated", MasterPage.FeedbackType.Information);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateSettings();

            // we have to re-load the form because the settings have changed and 
            // we need to show the new ones
            LoadForm();
        }
        protected void btnSendTestEmail_Click(object sender, EventArgs e)
        {

            // send the test email
            string from = this.GetSystemSetting("SendFromEmail");
            string to = txtTestEmailAddress.Text.Replace("\r", "").Replace("\n", "");
            string smtp = this.GetSystemSetting("SmtpHost");
            string subject = "Affinity Email Test Sent " + DateTime.Now.ToLongTimeString();

            Com.VerySimple.Email.Mailer mailer = new Com.VerySimple.Email.Mailer(smtp);


            string msg = "SmtpHost = '" + smtp + "'\r\n"
                + "SendFromEmail = '" + from + "'\r\n"
                + "TestEmailAddress = '" + to + "'\r\n\r\n"
                + this.GetSystemSetting("EmailFooter");

            mailer.Send(from, to, subject, msg);

            ((Affinity.MasterPage)this.Master).ShowFeedback(subject, MasterPage.FeedbackType.Information);
        }
    }
}