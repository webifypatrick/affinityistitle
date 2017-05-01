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

public partial class AdminContent : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
		this.RequirePermission(Affinity.RolePermission.AdminSystem);
		this.RequirePermission(Affinity.RolePermission.AffinityManager);
		this.Master.SetLayout("Edit Content Page", MasterPage.LayoutStyle.ContentOnly);

		if (!Page.IsPostBack)
		{
			string code = Request["code"];
			Affinity.Content c = new Affinity.Content(this.phreezer);

			if (code == null)
			{
				// this is an insert
			}
			else
			{
				//this is an update
				c.Load(code);
				txtCode.ReadOnly = true;
			}

			txtCode.Text = c.Code.ToString();
			txtMetaTitle.Text = c.MetaTitle.ToString();
			txtMetaKeywords.Text = c.MetaKeywords.ToString();
			txtMetaDescription.Text = c.MetaDescription.ToString();
			txtHeader.Text = c.Header.ToString();
			txtBody.Text = c.Body.ToString();
			txtModified.Text = c.Modified.ToShortDateString();
		}
	
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		Affinity.Content c = new Affinity.Content(this.phreezer);

		c.MetaTitle = txtMetaTitle.Text;
		c.MetaKeywords = txtMetaKeywords.Text;
		c.MetaDescription = txtMetaDescription.Text;
		c.Header = txtHeader.Text;
		c.Body = txtBody.Text;
		c.Modified = DateTime.Now;
		c.Code = txtCode.Text;

		if (txtCode.ReadOnly)
		{
			c.Update();
		}
		else
		{
			c.Insert(false);
		}

		// the default page is cached so we need to treat it specially:
		if (c.Code == Affinity.Content.HomePageCode)
		{
			Application[Affinity.Content.HomePageCode] = c;
		}

		this.Redirect("AdminContents.aspx?feedback=Content+Updated");
	}

	protected void btnCancel_Click(object sender, EventArgs e)
	{
		this.Redirect("AdminContents.aspx");
	}
}
