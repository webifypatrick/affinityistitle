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

using Com.VerySimple.Util;

public partial class AdminAccount : PageBase
{
	private Affinity.Account account;
	private Affinity.RequestType rtype;
	private XmlForm xmlForm;

	override protected void PageBase_Init(object sender, System.EventArgs e)
	{
		// we have to call the base first so phreezer is instantiated
		base.PageBase_Init(sender, e);

		int id = NoNull.GetInt(Request["id"], 0);
		this.account = new Affinity.Account(this.phreezer);
		this.RequirePermission(Affinity.RolePermission.AffinityManager);

		if (!id.Equals(0))
		{
			this.account.Load(id);
		}
		else
		{
			this.account.RoleCode = Affinity.Role.DefaultCode;
			this.account.CompanyId = Affinity.Company.DefaultId;
		}

		rtype = new Affinity.RequestType(this.phreezer);
		rtype.Load(Affinity.RequestType.UserPreferences);

		xmlForm = new XmlForm(this.account);

		LoadForm();
	}

	private void LoadForm()
	{
		pnlPreferences.Controls.Clear();
		pnlPreferences.Controls.Add(xmlForm.GetFormFieldControl(rtype.Definition, this.account.PreferencesXml));
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		this.Master.SetLayout("Edit Account", MasterPage.LayoutStyle.ContentOnly);
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
			ddRole.SelectedValue = this.account.RoleCode;
			ddRole.DataBind();

			Affinity.Underwriters uws = new Affinity.Underwriters(this.phreezer);
			Affinity.UnderwriterCriteria uwc = new Affinity.UnderwriterCriteria();
			uwc.AppendToOrderBy("Description");
			uws.Query(uwc);

			cblUnderwriterCodes.DataSource = uws;
			cblUnderwriterCodes.DataTextField = "Description";
			cblUnderwriterCodes.DataValueField = "Code";
			cblUnderwriterCodes.DataBind();

			// pre-select the checkboxes from the comma-separated user setting
			string[] codes = this.account.UnderwriterCodes.Split(",".ToCharArray());
			foreach (string c in codes)
			{
				ListItem cb = cblUnderwriterCodes.Items.FindByValue(c.Trim());
				if (cb != null)
				{
					cb.Selected = true;
				}
			}
			
			foreach (ListItem cb in cblUnderwriterCodes.Items)
			{
				ListItem i = new ListItem("100%", cb.Value + "100%");
				Endorse100.Items.Add(i);
				if(this.account.UnderwriterEndorsements.IndexOf(cb.Value + "100%") > -1) i.Selected = true;

				i = new ListItem("70%", cb.Value + "70%");
				Endorse70.Items.Add(i);
				i.Attributes.Add("onclick", "verifyCheckboxes(this);");
				if(this.account.UnderwriterEndorsements.IndexOf(cb.Value + "70%") > -1) i.Selected = true;

				i = new ListItem("75%", cb.Value + "75%");
				Endorse75.Items.Add(i);
				i.Attributes.Add("onclick", "verifyCheckboxes(this);");
				if(this.account.UnderwriterEndorsements.IndexOf(cb.Value + "75%") > -1) i.Selected = true;

				i = new ListItem("80%", cb.Value + "80%");
				Endorse80.Items.Add(i);
				i.Attributes.Add("onclick", "verifyCheckboxes(this);");
				if(this.account.UnderwriterEndorsements.IndexOf(cb.Value + "80%") > -1) i.Selected = true;

				i = new ListItem("85%", cb.Value + "85%");
				Endorse85.Items.Add(i);
				i.Attributes.Add("onclick", "verifyCheckboxes(this);");
				if(this.account.UnderwriterEndorsements.IndexOf(cb.Value + "85%") > -1) i.Selected = true;

				i = new ListItem("88%", cb.Value + "88%");
				Endorse88.Items.Add(i);
				i.Attributes.Add("onclick", "verifyCheckboxes(this);");
				if(this.account.UnderwriterEndorsements.IndexOf(cb.Value + "88%") > -1) i.Selected = true;

				i = new ListItem("90%", cb.Value + "90%");
				Endorse90.Items.Add(i);
				i.Attributes.Add("onclick", "verifyCheckboxes(this);");
				if(this.account.UnderwriterEndorsements.IndexOf(cb.Value + "90%") > -1) i.Selected = true;

				if (cb.Text.IndexOf("Direct") > -1)
				{
					Endorse100.Items[(Endorse100.Items.Count - 1)].Attributes.Add("style", "visibility:hidden");
					Endorse70.Items[(Endorse70.Items.Count - 1)].Attributes.Add("style", "visibility:hidden");
					Endorse75.Items[(Endorse75.Items.Count - 1)].Attributes.Add("style", "visibility:hidden");
					Endorse80.Items[(Endorse80.Items.Count - 1)].Attributes.Add("style", "visibility:hidden");
					Endorse85.Items[(Endorse85.Items.Count - 1)].Attributes.Add("style", "visibility:hidden");
					Endorse88.Items[(Endorse88.Items.Count - 1)].Attributes.Add("style", "visibility:hidden");
					Endorse90.Items[(Endorse90.Items.Count - 1)].Attributes.Add("style", "visibility:hidden");
				}
				else if(!cb.Selected)
				{
					cb.Attributes.Add("onclick", "enableDisableCheckboxes(this);");
					Endorse100.Items[(Endorse100.Items.Count - 1)].Enabled = false;
					Endorse70.Items[(Endorse70.Items.Count - 1)].Enabled = false;
					Endorse75.Items[(Endorse75.Items.Count - 1)].Enabled = false;
					Endorse80.Items[(Endorse80.Items.Count - 1)].Enabled = false;
					Endorse85.Items[(Endorse85.Items.Count - 1)].Enabled = false;
					Endorse88.Items[(Endorse88.Items.Count - 1)].Enabled = false;
					Endorse90.Items[(Endorse90.Items.Count - 1)].Enabled = false;
				}
				else
				{
					cb.Attributes.Add("onclick", "enableDisableCheckboxes(this);");
				}
			}


			Affinity.Companys companies = new Affinity.Companys(this.phreezer);
			Affinity.CompanyCriteria cc = new Affinity.CompanyCriteria();
			companies.Query(cc);

			ddCompany.DataSource = companies;
			ddCompany.DataTextField = "Name";
			ddCompany.DataValueField = "Id";
			ddCompany.SelectedValue = this.account.CompanyId.ToString();
			ddCompany.DataBind();

			txtId.Text = this.account.Id.ToString();
			txtUsername.Text = this.account.Username.ToString();
			txtFirstName.Text = this.account.FirstName.ToString();
			txtLastName.Text = this.account.LastName.ToString();
			txtCreated.Text = this.account.Created.ToShortDateString();
			txtModified.Text = this.account.Modified.ToShortDateString();
			txtPasswordHint.Text = this.account.PasswordHint.ToString();
			txtInternalId.Text = this.account.InternalId.ToString();
			txtEmail.Text = this.account.Email.ToString();
			txtBusinessLicenseId.Text = this.account.BusinessLicenseID.ToString();
			txtIndividualLicenseId.Text = this.account.IndividualLicenseID.ToString();

		}
    }

	protected void ShowAccount()
	{
	}

	/// <summary>
	/// Persist to DB
	/// </summary>
	protected void UpdateAccount(bool createNew)
	{
		this.account.Username = txtUsername.Text;
		this.account.FirstName = txtFirstName.Text;
		this.account.LastName = txtLastName.Text;
		this.account.StatusCode = "Active";
		this.account.Modified = DateTime.Now;
		this.account.PasswordHint = txtPasswordHint.Text;
		// this.account.PreferencesXml = txtPreferencesXml.Text;
		this.account.RoleCode = ddRole.SelectedValue;
		this.account.CompanyId = int.Parse(ddCompany.SelectedValue);
		this.account.InternalId = txtInternalId.Text;
		this.account.Email = txtEmail.Text;
		this.account.BusinessLicenseID = txtBusinessLicenseId.Text;
		this.account.IndividualLicenseID = txtIndividualLicenseId.Text;

		// underwriter codes - convert the checkbox list into a comma-separated value
		this.account.UnderwriterCodes = "";
		string delim = "";
		foreach (ListItem cb in cblUnderwriterCodes.Items)
		{
			if (cb.Selected)
			{
				this.account.UnderwriterCodes += delim + cb.Value;
				delim = ",";
			}
		}
		this.account.UnderwriterEndorsements = "";
		delim = "";
		foreach (ListItem cb in Endorse100.Items)
		{
			if (cb.Selected)
			{
				this.account.UnderwriterEndorsements += delim + cb.Value;
				delim = ",";
			}
		}
		foreach (ListItem cb in Endorse90.Items)
		{
			if (cb.Selected)
			{
				this.account.UnderwriterEndorsements += delim + cb.Value;
				delim = ",";
			}
		}
		foreach (ListItem cb in Endorse88.Items)
		{
			if (cb.Selected)
			{
				this.account.UnderwriterEndorsements += delim + cb.Value;
				delim = ",";
			}
		}
		foreach (ListItem cb in Endorse85.Items)
		{
			if (cb.Selected)
			{
				this.account.UnderwriterEndorsements += delim + cb.Value;
				delim = ",";
			}
		}
		foreach (ListItem cb in Endorse80.Items)
		{
			if (cb.Selected)
			{
				this.account.UnderwriterEndorsements += delim + cb.Value;
				delim = ",";
			}
		}
		foreach (ListItem cb in Endorse75.Items)
		{
			if (cb.Selected)
			{
				this.account.UnderwriterEndorsements += delim + cb.Value;
				delim = ",";
			}
		}
		foreach (ListItem cb in Endorse70.Items)
		{
			if (cb.Selected)
			{
				this.account.UnderwriterEndorsements += delim + cb.Value;
				delim = ",";
			}
		}
		//Response.Write(this.account.UnderwriterCodes);
		//Response.End();

		this.account.PreferencesXml = XmlForm.XmlToString(xmlForm.GetResponse(pnlPreferences));


		if (this.account.Id > 0 && !createNew)
		{
			this.account.Update();
		}
		else
		{
			Response.Write(this.account.Username);
			this.account.Insert();
			this.account.Password = "password";
			this.account.SetPassword();
			this.Redirect("AdminAccount.aspx?id=" + this.account.Id.ToString());
		}

		//update the password only if it was supplied
		if (!txtPassword.Text.Equals(""))
		{
			this.account.Password = txtPassword.Text;
			this.account.SetPassword();
		}

	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		UpdateAccount(false);
		this.Redirect("AdminAccounts.aspx?feedback=Account+Saved");
	}

	protected void btnCopyAccount_Click(object sender, EventArgs e)
	{
		UpdateAccount(true);
		//this.Redirect("AdminAccounts.aspx?feedback=Account+Saved");
	}

	protected void btnCancel_Click(object sender, EventArgs e)
	{
		this.Redirect("AdminAccounts.aspx");
	}
}