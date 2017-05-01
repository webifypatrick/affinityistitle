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

public partial class ForgottenPass : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.SetLayout("Forgotten Password", MasterPage.LayoutStyle.Photo_01);
    }
	protected void btnReset_Click(object sender, EventArgs e)
	{
		if (txtEmail.Text != "")
		{
			Affinity.Accounts accs = new Affinity.Accounts(this.phreezer);
			Affinity.AccountCriteria c = new Affinity.AccountCriteria();
			c.Email = txtEmail.Text;

			accs.Query(c);

			if (accs.Count > 0)
			{
				foreach (Affinity.Account ac in accs)
				{
					string newPass = Com.Obviex.RandomPassword.Generate(6, 8);
					ac.Password = newPass;
					ac.SetPassword();
					SendEmail(ac, newPass);
				}

				pnlReset.Visible = false;
				pnlResults.Visible = true;
			}
			else
			{
				lblError.Visible = true;
				lblError.Text = "The email specified was not found";
			}
		}

	}

	protected void SendEmail(Affinity.Account ac, string newPass)
	{
		string to = ac.Email;
		string url = this.GetSystemSetting("RootUrl") + "";
		string subject = "Affinity Password Reset";
		string from = this.GetSystemSetting("SendFromEmail");

		lblFromDomain.Text = from.Substring(from.LastIndexOf("@") + 1);

		// send the email
		Com.VerySimple.Email.Mailer mailer = new Com.VerySimple.Email.Mailer(this.GetSystemSetting("SmtpHost"));

		string msg = "Dear " + ac.FirstName + ",\r\n\r\n"
			+ "Your Affinity password has been reset.  Your login information is:\r\n\r\n"
			+ "Username: " + ac.Username + "\r\n"
			+ "Password: " + newPass + "\r\n\r\n"
			+ "Please use this new password to login at " + url + ".  "
			+ "Once you have logged in, you may click on \"My Preferences\" and change your "
			+ "password.\r\n\r\n"
			+ this.GetSystemSetting("EmailFooter");

		mailer.Send(
			this.GetSystemSetting("SendFromEmail")
			, to
			, subject
			, msg);
	}
}
