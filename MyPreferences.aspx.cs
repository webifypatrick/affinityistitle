using System;
using System.Web.UI;

namespace Affinity
{
    public partial class MyPreferences : PageBase
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
            rtype.Load(Affinity.RequestType.UserPreferences);

            xmlForm = new XmlForm(this.GetAccount());

            LoadForm();
        }

        private void LoadForm()
        {
            pnlPreferences.Controls.Clear();
            pnlPreferences.Controls.Add(xmlForm.GetFormFieldControl(rtype.Definition, this.GetAccount().PreferencesXml));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.RequirePermission(Affinity.RolePermission.SubmitOrders);
            ((Affinity.MasterPage)this.Master).SetLayout("My Preferences", MasterPage.LayoutStyle.ContentOnly);
            System.Web.UI.HtmlControls.HtmlForm frm = (System.Web.UI.HtmlControls.HtmlForm)((Affinity.MasterPage)this.Master).FindControl("form1");
            frm.Enctype = "multipart/form-data";

            if (!Page.IsPostBack)
            {
                // populate the form
                Affinity.Account me = this.GetAccount();
                txtUsername.Text = me.Username.ToString();
                txtFirstName.Text = me.FirstName.ToString();
                txtLastName.Text = me.LastName.ToString();
                txtBusinessID.Text = me.BusinessLicenseID.ToString();
                txtIndividualID.Text = me.IndividualLicenseID.ToString();
                //txtPasswordHint.Text = me.PasswordHint.ToString();
                txtEmail.Text = me.Email.ToString();

                if (me.Signature.Equals(""))
                {
                    signature.Visible = false;
                }
                else
                {
                    signature.Src = "signatures/" + me.Signature;
                }

                if (!me.Role.Code.Equals("Bank") && !me.Role.Code.Equals("Lender"))
                {
                    Response.Write("<script>onload = function(){document.getElementById('group_lenders').style.display = 'none';}</script>");
                }
            }
            else
            {
                if (oFile.HasFile)
                {
                    string strFileName = "";
                    string strFilePath = "";
                    string strFolder;
                    strFolder = Server.MapPath(".") + "\\signatures\\";

                    // Get the name of the file that is posted.

                    strFileName = System.IO.Path.GetFileName(oFile.PostedFile.FileName);
                    string[] s = strFileName.Split('.');
                    string ext = "";

                    if (s.Length > 1)
                    {
                        ext = "." + s[1];
                    }

                    string filename = DateTime.Now.Ticks.ToString() + ext;

                    strFilePath = strFolder + filename;


                    Affinity.Account acc = this.GetAccount();
                    acc.Signature = filename;
                    acc.Update();


                    oFile.PostedFile.SaveAs(strFilePath);
                }
            }
        }

        protected void UpdateSettings()
        {
            Affinity.Account me = this.GetAccount();

            me.FirstName = txtFirstName.Text;
            me.LastName = txtLastName.Text;
            me.Email = txtEmail.Text;
            me.BusinessLicenseID = txtBusinessID.Text;
            me.IndividualLicenseID = txtIndividualID.Text;

            me.PreferencesXml = XmlForm.XmlToString(xmlForm.GetResponse(pnlPreferences));
            me.Update();

            if (txtPassword.Text != "")
            {
                // password change is being attempted
                if (txtPassword.Text == txtPasswordConfirm.Text)
                {
                    me.Password = txtPassword.Text;
                    me.SetPassword();
                    ((Affinity.MasterPage)this.Master).ShowFeedback("Your preferences have been updated and your password has been changed", MasterPage.FeedbackType.Information);
                }
                else
                {
                    // password fields don't match
                    ((Affinity.MasterPage)this.Master).ShowFeedback("The password confirmation did not match.  Your password was not updated", MasterPage.FeedbackType.Error);
                }
            }
            else
            {
                // password was not changed
                ((Affinity.MasterPage)this.Master).ShowFeedback("Your preferences have been updated", MasterPage.FeedbackType.Information);
            }

            me.ClearPreferenceCache(); // force preferences to be reloaded
            this.SetAccount(me);  // refresh the session

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateSettings();

            // we have to re-load the form because the settings have changed and 
            // we need to show the new ones
            LoadForm();
        }
    }
}