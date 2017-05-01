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

using System.Xml;

public partial class AdminRequest : PageBase
{
	protected Affinity.Request request;

	override protected void PageBase_Init(object sender, System.EventArgs e)
	{
		// we have to call the base first so phreezer is instantiated
		base.PageBase_Init(sender, e);

		int id = NoNull.GetInt(Request["id"], 0);
		request = new Affinity.Request(this.phreezer);
		this.request.Load(id);

		// add the form for the request details
		XmlForm xf = new XmlForm(this.request.Account);
		pnlDetails.Controls.Add(xf.GetFormFieldControl(request.RequestType.Definition, request.Xml));
	}
	
	protected void Page_Load(object sender, EventArgs e)
	{
		this.RequirePermission(Affinity.RolePermission.AdminSystem);
		this.Master.SetLayout("Administration", MasterPage.LayoutStyle.ContentOnly);

		if (!Page.IsPostBack)
		{
			// populate the form
			Affinity.RequestStatuss codes = new Affinity.RequestStatuss(this.phreezer);
			Affinity.RequestStatusCriteria sc = new Affinity.RequestStatusCriteria();
			codes.Query(sc);

			ddStatus.DataSource = codes;
			ddStatus.DataTextField = "Description";
			ddStatus.DataValueField = "Code";
			ddStatus.SelectedValue = this.request.StatusCode;
			ddStatus.DataBind();

			Affinity.AttachmentPurposes aps = new Affinity.AttachmentPurposes(this.phreezer);
			Affinity.AttachmentPurposeCriteria apc = new Affinity.AttachmentPurposeCriteria();
			aps.Query(apc);

			ddFilePurpose.DataSource = aps;
			ddFilePurpose.DataTextField = "Description";
			ddFilePurpose.DataValueField = "Code";
			//ddStatus.SelectedValue = null; // 
			ddFilePurpose.SelectedValue = "Committment";
			ddFilePurpose.DataBind();

			txtId.Text = request.Order.WorkingId;
			txtRequestId.Text = this.request.Id.ToString();
			txtRequestTypeCode.Text = this.request.RequestTypeCode.ToString();
			txtOriginatorId.Text = this.request.Account.FullName;
			txtCreated.Text = this.request.Created.ToShortDateString();

			pnlIsCurrent.Visible = !this.request.IsCurrent;

			txtNote.Text = this.request.Note.ToString();


			lblWorkingId.Text = request.Order.WorkingId;
			txtCustomerId.Text = request.Order.CustomerId;
			txtClientName.Text = request.Order.ClientName;
			txtPIN.Text = request.Order.Pin;
			txtAdditionalPins.Text = request.Order.AdditionalPins;
			txtPropertyAddress.Text = request.Order.PropertyAddress;
			txtPropertyAddress2.Text = request.Order.PropertyAddress2;
			txtPropertyCity.Text = request.Order.PropertyCity;
			txtPropertyState.Text = request.Order.PropertyState;
			txtPropertyZip.Text = request.Order.PropertyZip;
			txtCustomerId.Text = request.Order.CustomerId;
			txtPropertyCounty.Text = request.Order.PropertyCounty;
			txtClosingDate.Text = request.Order.ClosingDate.ToShortDateString();
			txtPropertyUse.Text = request.Order.PropertyUse;

			// changes
			Hashtable dif = request.GetDifference();
			if (dif.Count > 0)
			{
				foreach (string key in dif.Keys)
				{
					pnlChanges.Controls.Add(new LiteralControl("<div class=\"line\"><div class=\"field horizontal\"><div class=\"label horizontal\">" + key + "</div><div class=\"input horizontal readonly\">" + dif[key].ToString() + "</div></div></div>"));
				}
			}
			else
			{
				pnlChanges.Controls.Add(new LiteralControl("<div class=\"line\"><em>No Changes</em></div>"));
			}
		}
	
	}
	
	/// <summary>
	/// Persist to DB and send email notification
	/// </summary>
	/// <returns>result of email notification</returns>
	protected string UpdateRequest()
	{
		// see if the status was changed from it's previous value
		// note we are getting the full description here vs the code
		string oldStatus = this.request.RequestStatus.Description;
		string newStatus = ddStatus.SelectedItem.Text;
		bool filePosted = fuAttachment.HasFile;

		this.request.StatusCode = ddStatus.SelectedValue;  // here we need the code, tho
		this.request.Note = txtNote.Text;

		// update the details
		XmlForm xf = new XmlForm(this.request.Account);
		this.request.Xml = XmlForm.XmlToString(xf.GetResponse(pnlDetails));

		this.request.Update();
		this.request.Order.SyncStatus();

		// if a file was provided, then upload it
		if (filePosted)
		{
			string ext = System.IO.Path.GetExtension(fuAttachment.FileName);
			string fileName = "req_att_" + request.Id + "_" + DateTime.Now.ToString("yyyyMMddhhss") + "." + ext.Replace(".","");
			Affinity.Attachment att = new Affinity.Attachment(this.phreezer);
			att.RequestId = this.request.Id;
			att.Name = txtAttachmentName.Text != "" ? txtAttachmentName.Text : ddFilePurpose.SelectedItem.Text;
			att.PurposeCode = ddFilePurpose.SelectedValue;
			att.Filepath = fileName;
			att.MimeType = ext;
			att.SizeKb = 0; // fuAttachment.FileBytes.GetUpperBound() * 1024;

			att.Insert();
			if(ddFilePurpose.SelectedValue.Equals("SearchPkg"))
			{
				Affinity.Schedule sched = new Affinity.Schedule(this.phreezer);
				sched.AttachmentID = att.Id;
				sched.AccountID = this.request.Account.Id;
				sched.UploadAccountID = this.GetAccount().Id;
				sched.OrderID = this.request.OrderId;
				sched.RequestID = this.request.Id;
				sched.Search_package_date = DateTime.Now;
				
				sched.Insert();
				Affinity.Global.SetSchedule();
			}
			//TODO: block any harmful file types
			
			Affinity.UploadLog ul = new Affinity.UploadLog(this.phreezer);
			
			ul.AttachmentID = att.Id;
			ul.AccountID = this.request.Account.Id;
			ul.UploadAccountID = this.GetAccount().Id;
			ul.OrderID = this.request.OrderId;
			ul.RequestID = this.request.Id;
			
			ul.Insert();

			fuAttachment.SaveAs(Server.MapPath("./") + "attachments/" + fileName);

			txtEmailNote.Text = "A new file '" + att.Name + "' has been posted and is ready for your review.  "
				+ txtEmailNote.Text;
		}

		return SendNotification(oldStatus, newStatus, filePosted, txtEmailNote.Text);

	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		string result = UpdateRequest();
		this.Redirect("AdminOrder.aspx?id=" + this.request.OrderId + "&feedback=" + Server.UrlEncode("Request Updated.  " + result));
	}

	protected void btnCancel_Click(object sender, EventArgs e)
	{
		this.Redirect("AdminOrder.aspx?id=" + this.request.OrderId + "");
	}


	/// <summary>
	/// If specified on the form and the user prefererence sends customer notification
	/// </summary>
	/// <param name="msg"></param>
	/// <returns>message indicating the notification that was sent</returns>
	protected string SendNotification(string oldStatus, string newStatus, bool filePosted, string message)
	{
		bool sendEmail = false;
		bool userEmailPreference = false;
		string statusChange = "";
		string actionTaken = "";
		string emailPreferenceCode = ""; // this code will return empty string

		// if a file was posted and the status was changed, the file post takes priority
		if (filePosted == true)
		{
			// file posted
			sendEmail = true;
			userEmailPreference = this.request.Account.GetPreference("EmailOnFilePost").Equals("Yes");
			emailPreferenceCode = "EmailOnFilePostAddress";
		}
		else if (oldStatus != newStatus)
		{
			// status update
			statusChange = "The new order status is '" + newStatus + ".'";
			sendEmail = true;

			if (this.request.RequestTypeCode == Affinity.RequestType.ClosingRequestCode)
			{
				// this is a status change for a schedule/closing request
				userEmailPreference = this.request.Account.GetPreference("EmailOnScheduleRequest").Equals("Yes");
				emailPreferenceCode = "EmailOnScheduleRequestAddress";
			}
			else
			{
				// this is a status change for a regular order
				userEmailPreference = this.request.Account.GetPreference("EmailOnStatusChange").Equals("Yes");
				emailPreferenceCode = "EmailOnStatusChangeAddress";
			}
		
		}

		if (sendEmail == false)
		{
			actionTaken = "Email was not sent because no status change or file post occured";
		}
		else if (cbEnableEmail.Checked == false)
		{
			actionTaken = "Email notification was disabled for this update";
		}
		else if (userEmailPreference == false)
		{
			actionTaken = "No email was sent based on the customer's " + emailPreferenceCode + " preference setting";
		}
		else
		{
			// the customer does want to receive this email

			string to = this.request.Account.GetPreference(emailPreferenceCode, this.request.Order.Account.Email);
			string url = this.GetSystemSetting("RootUrl") + "MyOrder.aspx?id=" + this.request.Order.Id.ToString();
			string subject = "Affinity Order '" + this.request.Order.ClientName.Replace("\r", "").Replace("\n", "") + "' #" + this.request.Order.WorkingId + " Updated";

			// send the email
			Com.VerySimple.Email.Mailer mailer = new Com.VerySimple.Email.Mailer(this.GetSystemSetting("SmtpHost"));

			string msg = "Dear " + this.request.Order.Account.FirstName + ",\r\n\r\n"
				+ "Your Affinity " + this.request.RequestTypeCode + " for '" + this.request.Order.ClientName + "', ID #"
				+ this.request.Order.WorkingId + " has been processed.  " + statusChange + "\r\n\r\n"
				+ (message != "" ? message + "\r\n\r\n" : "")
        + "Friendly: " + this.request.Order.ClientName + "\r\n"
        + "Tracking Code: " + this.request.Order.CustomerId + "\r\n\r\n"
				+ "You may view the full details of your order anytime online at " + url + ".  " 
				+ "If you would prefer to not receive this notification, you may also login "
				+ "to your Affinity account and customize your email notification preferences.\r\n\r\n"
				+ this.GetSystemSetting("EmailFooter");

			mailer.Send(
				this.GetSystemSetting("SendFromEmail")
				, to.Replace(";", ",")
				, subject
				, msg);

			actionTaken = "Email notification was sent to " + to;
		}

		return actionTaken;
	}


}
