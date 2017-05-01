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

public partial class AdminAttachmentPurpose : PageBase
{
	private Affinity.AttachmentPurpose attachmentPurpose;
	private bool isUpdate;

	override protected void PageBase_Init(object sender, System.EventArgs e)
	{
		// we have to call the base first so phreezer is instantiated
		base.PageBase_Init(sender, e);

		string code = NoNull.GetString(Request["code"], "");
		this.attachmentPurpose = new Affinity.AttachmentPurpose(this.phreezer);

		this.isUpdate = (!code.Equals(""));

		if (this.isUpdate)
		{
			this.attachmentPurpose.Load(code);
		}
	}


	/// <summary>
	/// Persist to DB
	/// </summary>
	protected void UpdateAttachmentPurpose()
	{
		string code = txtCode.Text;
		this.attachmentPurpose.Code = code;
		this.attachmentPurpose.Description = txtDescription.Text;
		this.attachmentPurpose.ChangeStatusTo = txtChangeStatusTo.Text;
		this.attachmentPurpose.PermissionRequired = int.Parse(txtPermissionRequired.Text);
		this.attachmentPurpose.SendNotification = cbSendNotification.Checked;
		
		// Insert/Delete Attachment/Roles
		IEnumerator i = RolesCheckboxes.Items.GetEnumerator();
		Affinity.AttachmentRole ardao = new Affinity.AttachmentRole(this.phreezer);
		ardao.AttachmentPurposeCode = code;
		Affinity.AttachmentRolesCriteria arcrit = new Affinity.AttachmentRolesCriteria();
		arcrit.AttachmentPurposeCode = code;
		
		// loop through the checkboxes and insert or delete as needed
		while(i.MoveNext())
		{
			ListItem li = (ListItem) i.Current;
			arcrit.RoleCode = li.Value;
			Affinity.AttachmentRoles aroles = ardao.GetAttachmentRoles(arcrit);
			ardao.RoleCode = li.Value;
			
			//Delete item if there was one there before but is no longer selected
			if(aroles.Count > 0 && !li.Selected)
			{
				ardao.Delete();
			}
			// insert item if there wasn't there before but is now selected
			else if(li.Selected && aroles.Count == 0)
			{
				ardao.Insert(false);
			}
		}

		if (this.isUpdate)
		{

			this.attachmentPurpose.Update();
		}
		else
		{
			this.attachmentPurpose.Insert(false);
		}

		this.Redirect("AdminAttachmentPurposes.aspx?feedback=Attachment+Type+Updated");
	}


	protected void Page_Load(object sender, EventArgs e)
	{
		this.RequirePermission(Affinity.RolePermission.AdminSystem);
		this.RequirePermission(Affinity.RolePermission.AffinityManager);
		this.RequirePermission(Affinity.RolePermission.AffinityStaff);
		this.Master.SetLayout("AttachmentPurpose Details", MasterPage.LayoutStyle.ContentOnly);

		this.btnDelete.Attributes.Add("onclick", "return confirm('Delete this record?')");

		if (!Page.IsPostBack)
		{
			// populate the form
			string code = this.attachmentPurpose.Code.ToString();
			txtCode.Text = code;
			lblCode.Text = code;
			txtDescription.Text = this.attachmentPurpose.Description.ToString();
			txtChangeStatusTo.Text = this.attachmentPurpose.ChangeStatusTo.ToString();
			txtPermissionRequired.Text = this.attachmentPurpose.PermissionRequired.ToString();
			cbSendNotification.Checked = this.attachmentPurpose.SendNotification;

			if (this.isUpdate)
			{
				lblCode.Visible = true;
			}
			else
			{
				txtCode.Visible = true;
			}
	
			// Display checkbox roles
			Affinity.Roles roles = new Affinity.Roles(this.phreezer);
			Affinity.RoleCriteria rc = new Affinity.RoleCriteria();
			
			// order by description
			rc.AppendToOrderBy("Description");
			roles.Query(rc);
			
			// display checkboxes in columns of 3
			RolesCheckboxes.RepeatColumns = 3;
			RolesCheckboxes.Width = 700;
			
			Affinity.AttachmentRole ardao = new Affinity.AttachmentRole(this.phreezer);
			Affinity.AttachmentRolesCriteria arcrit = new Affinity.AttachmentRolesCriteria();
			IEnumerator i = roles.GetEnumerator();
			
			// loop through all the roles and add checkboxes for each one
			while(i.MoveNext())
			{
				ListItem li = new ListItem();
				Affinity.Role r = (Affinity.Role) i.Current;
				li.Text = r.Description;
				li.Value = r.Code;

				// verify if this checkbox should be checked
				arcrit.AttachmentPurposeCode = code;
				arcrit.RoleCode = r.Code;
				Affinity.AttachmentRoles aroles = ardao.GetAttachmentRoles(arcrit);
				li.Selected = (aroles.Count > 0);
				
				// Add checkbox to RolesCheckboxes div
				RolesCheckboxes.Items.Add(li);
			}
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		UpdateAttachmentPurpose();
	}

	protected void btnCancel_Click(object sender, EventArgs e)
	{
		this.Redirect("AdminAttachmentPurposes.aspx");
	}

	protected void btnDelete_Click(object sender, EventArgs e)
	{
		try
		{
			this.attachmentPurpose.Delete();
			this.Redirect("AdminAttachmentPurposes.aspx?feedback=Attachment+Type+Deleted");
		}
		catch (Exception ex)
		{
			this.Master.ShowFeedback("The Attachment Type cannot be deleted.  Most likely because there are still attachments assigned to it.", MasterPage.FeedbackType.Error);
		}
	}
}