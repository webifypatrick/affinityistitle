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
using System.IO;
using System.Net.Mail;
using HiQPdf;
using MySql.Data.MySqlClient;

using Com.VerySimple.Util;

public partial class AgentExaminationForm : PageBase
{
	protected Affinity.Request request;

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

		int id = NoNull.GetInt(Request["id"], 0);
		request = new Affinity.Request(this.phreezer);
		Affinity.Order order = new Affinity.Order(this.phreezer);
		order.Load(id);
		Affinity.RequestCriteria criteria = new Affinity.RequestCriteria();
		Affinity.Requests reqs = order.GetOrderRequests(criteria);
		Affinity.Request req = (Affinity.Request)reqs[0];
		this.request.Load(req.Id);
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		this.RequirePermission(Affinity.RolePermission.SubmitOrders);
		this.Master.SetLayout("Agent Examination Form", MasterPage.LayoutStyle.ContentOnly);
		
		if(Request["ajax"] != null)
		{
			Affinity.Account me = this.GetAccount();
			saveAgentExaminationForm(false, me);
			Response.End();
		}
		else if (!Page.IsPostBack)
		{
      txtCommitmentNumber.Text = request.Order.WorkingId.ToString();
			txtAgentsName.Text = request.Account.FullName;
			
			using(MySqlConnection mysqlCon = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString))
			{
				mysqlCon.Open();
				
				bool doInsert = true;
				string aefid = "0";
				using (MySqlCommand cmd = new MySqlCommand ("SELECT * FROM agent_examination WHERE aef_status = 0 AND aef_account_id = " + this.GetAccount().Id.ToString() + " and aef_request_id = " + this.request.Id.ToString() + " ORDER BY aef_id desc",  mysqlCon))
				{
          try
          {
              MySqlDataReader Reader = cmd.ExecuteReader();
              while (Reader.Read()) // this part is wrong somehow
              {
                  txtAgentsName.Text = Reader["aef_agents_name"].ToString();
                  txtCommitmentNumber.Text = Reader["aef_commitment_number"].ToString();
                  txtPropertyVesting.Text = Reader["aef_property_vesting"].ToString();
                  txtPropertyAddress.Text = Reader["aef_property_address"].ToString();
                  txtPropertyCityStateZip.Text = Reader["aef_property_city_state_zip"].ToString();
                  txtPropertyCounty.Text = Reader["aef_property_county"].ToString();
                  txtPIN.Text = Reader["aef_pin"].ToString();
                  txtPriorYear.Text = Reader["aef_prior_year"].ToString();
                  txt1stInstallment.Text = Reader["aef_1st_installment"].ToString();
                  txt2ndInstallment.Text = Reader["aef_2nd_installment"].ToString();
                  txtCurrentYear.Text = Reader["aef_current_year"].ToString();
                  txtTaxAmount1st.Text = Reader["aef_tax_amount_1st"].ToString();
                  txtTaxAmount2nd.Text = Reader["aef_tax_amount_2nd"].ToString();
                  txtDueDate1st.Text = Reader["aef_due_date_1st"].ToString();
                  txtDueDate2nd.Text = Reader["aef_due_date_2nd"].ToString();
                  txtTaxYearofSoldtaxes.Text = Reader["aef_tax_year_of_sold_taxes"].ToString();
                  txtMortgagesofRecord.Text = Reader["aef_mortgages_of_record"].ToString();
                  txtOtherLiensofRecord.Text = Reader["aef_other_liens_of_record"].ToString();
                  txtBuildingLines.Text = Reader["aef_building_lines"].ToString();
                  txtEasements.Text = Reader["aef_easements"].ToString();
                  txtDocumentNumbers.Text = Reader["aef_document_numbers"].ToString();
                  radioCondoAssociationYes.Checked = Reader["aef_condo_association"].ToString().Equals("Yes");
                  chkSearchPackageReviewedAmendments.Checked = Reader["aef_search_package_reviewed_amendments"].ToString().Equals("Yes");
                  chkSearchPackageReviewed.Checked = Reader["aef_search_package_reviewed"].ToString().Equals("Yes");
                  txtAmendments.Text = Reader["aef_amendments"].ToString();
                  
              }
              Reader.Close();
          }

          catch (Exception ex)
          {
              //MessageBox.Show(ex.Message);
              Response.Write(ex.Message);
              
          }
        }
			}
		}

	}
	protected void btnSubmit_Click(object sender, EventArgs e)
	{
		Affinity.Account me = this.GetAccount();
		saveAgentExaminationForm(true, me);
	}
	
	protected void saveAgentExaminationForm(bool submitted, Affinity.Account me)
	{
		//submitted = false;
		// Declare objects:
		//ZfAPIClass oZfAPI = new ZfAPIClass();
		//ZfLib.UserSession oUserSession;
		//ZfLib.Messages oMessages;
		//ZfLib.Message oMessage;

		try
		{
			// make this an attachment to the request

			string ext = ".pdf";
			//string fileName = "req_att_" + request.Id + "_" + DateTime.Now.ToString("yyyyMMddhhss") + "." + ext.Replace(".","");
			string commitment = txtCommitmentNumber.Text;
			string fileName = "Agent_Exam_Sheet_for_AFF-" + ((commitment.Equals(""))? request.OrderId.ToString() : commitment.Replace("AFF", ""));
			string suffix = "_001";
			
			int idx = 1;
			string attID = "0";
			string contentsHTML = "";
			
			if(submitted)
			{
				while(File.Exists(Server.MapPath("./") + "attachments/" + fileName + suffix + ext))
				{
					idx++;
					string idxstr = idx.ToString();
					if(idxstr.Length == 1)
					{
						suffix = "_00" + idxstr;
					}
					else if(idxstr.Length == 2)
					{
						suffix = "_0" + idxstr;
					}
					else
					{
						suffix = "_" + idxstr;
					}
				}
				fileName = fileName + suffix + ext;
				
				//string contentsTXT = "";
	
				using (FileStream fs = new FileStream(Server.MapPath("./") + "AgentExaminationForm.html", FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 8, true))
				{
					using (StreamReader sr = new StreamReader(fs))
					{
	
						// Read the file using StreamReader class
						contentsHTML = sr.ReadToEnd();
					}
				}
	
				contentsHTML = contentsHTML.Replace("margin-left:46px", "").Replace("txtAgentsName", txtAgentsName.Text).Replace("txtCommitmentNumber", commitment).Replace("txtPropertyVesting", txtPropertyVesting.Text).Replace("txtPropertyAddress", txtPropertyAddress.Text).Replace("txtPropertyCityStateZip", txtPropertyCityStateZip.Text).Replace("txtPropertyCounty", txtPropertyCounty.Text).Replace("txtPIN", txtPIN.Text).Replace("txtPriorYear", txtPriorYear.Text).Replace("txt1stInstallment", txt1stInstallment.Text).Replace("txt2ndInstallment", txt2ndInstallment.Text).Replace("txtCurrentYear", txtCurrentYear.Text).Replace("txtTaxAmount1st", txtTaxAmount1st.Text).Replace("txtTaxAmount2nd", txtTaxAmount2nd.Text).Replace("txtDueDate1st", txtDueDate1st.Text).Replace("txtDueDate2nd", txtDueDate2nd.Text).Replace("txtTaxYearofSoldtaxes", txtTaxYearofSoldtaxes.Text).Replace("txtMortgagesofRecord", txtMortgagesofRecord.Text.Replace("\n", "<br />")).Replace("txtOtherLiensofRecord", txtOtherLiensofRecord.Text.Replace("\n", "<br />")).Replace("txtBuildingLines", txtBuildingLines.Text.Replace("\n", "<br />")).Replace("txtEasements", txtEasements.Text.Replace("\n", "<br />")).Replace("txtDocumentNumbers", txtDocumentNumbers.Text.Replace("\n", "<br />")).Replace("radioCondoAssociationYes", ((radioCondoAssociationYes.Checked) ? " checked='checked'" : "")).Replace("radioCondoAssociationNo", ((radioCondoAssociationNo.Checked) ? " checked='checked'" : "")).Replace("chkSearchPackageReviewedAmendments", ((chkSearchPackageReviewedAmendments.Checked) ? " checked='checked'" : "")).Replace("chkSearchPackageReviewed", ((chkSearchPackageReviewed.Checked) ? " checked='checked'" : "")).Replace("txtAmendments", txtAmendments.Text.Replace("\n", "<br />")).Replace("SIGNATURE", (me.Signature.Equals("")) ? "" : "<img src=\"" + ((Request.Url.Port == 443) ? "https://" : "http://") + Request.Url.Host + "/signatures/" + me.Signature + "\" height=\"50\" border=\"0\">").Replace("DATESTAMP", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
	
				/*
				using (FileStream fs = new FileStream(Server.MapPath("./") + "AgentExaminationForm.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 8, true))
				{
					using (StreamReader sr = new StreamReader(fs))
					{
	
						// Read the file using StreamReader class
						contentsTXT = sr.ReadToEnd();
					}
				}
	
				contentsTXT = contentsTXT.Replace("txtAgentsName", txtAgentsName.Text).Replace("txtCommitmentNumber", commitment).Replace("txtPropertyVesting", txtPropertyVesting.Text).Replace("txtPropertyAddress", txtPropertyAddress.Text).Replace("txtPropertyCityStateZip", txtPropertyCityStateZip.Text).Replace("txtPropertyCounty", txtPropertyCounty.Text).Replace("txtPIN", txtPIN.Text).Replace("txtPriorYear", txtPriorYear.Text).Replace("txt1stInstallment", txt1stInstallment.Text).Replace("txt2ndInstallment", txt2ndInstallment.Text).Replace("txtCurrentYear", txtCurrentYear.Text).Replace("txtTaxAmount1st", txtTaxAmount1st.Text).Replace("txtTaxAmount2nd", txtTaxAmount2nd.Text).Replace("txtDueDate1st", txtDueDate1st.Text).Replace("txtDueDate2nd", txtDueDate2nd.Text).Replace("txtTaxYearofSoldtaxes", txtTaxYearofSoldtaxes.Text).Replace("txtMortgagesofRecord", txtMortgagesofRecord.Text).Replace("txtOtherLiensofRecord", txtOtherLiensofRecord.Text).Replace("txtBuildingLines", txtBuildingLines.Text).Replace("txtEasements", txtEasements.Text).Replace("txtDocumentNumbers", txtDocumentNumbers.Text).Replace("radioCondoAssociation", ((radioCondoAssociationYes.Checked) ? "Yes" : "No")).Replace("chkSearchPackageReviewedAmendments", ((chkSearchPackageReviewedAmendments.Checked) ? "The search and the search package has been\nreviewed and examined and we have made our determination of insurability and we direct ATS to\nissue the title commitment in accordance with the search package." : "")).Replace("chkSearchPackageReviewed", ((chkSearchPackageReviewed.Checked) ? "The search and the search package\nhas been reviewed and examined and we have made our determination of insurability and we\ndirect ATS to issue the title commitment after making the following amendments." : "")).Replace("txtAmendments", txtAmendments.Text);
				*/
				Affinity.Attachment att = new Affinity.Attachment(this.phreezer);
				att.RequestId = this.request.Id;
				att.Name = "AgentExaminationForm.pdf";
				att.PurposeCode = "ExamSheet";
				att.Filepath = fileName;
				att.MimeType = ext;
				att.SizeKb = 0; // fuAttachment.FileBytes.GetUpperBound() * 1024;
	
				att.Insert();
				attID = att.Id.ToString();
				//TODO: block any harmful file types
				
				Affinity.UploadLog ul = new Affinity.UploadLog(this.phreezer);
				
				ul.AttachmentID = att.Id;
				ul.AccountID = this.request.Account.Id;
				ul.UploadAccountID = this.GetAccount().Id;
				ul.OrderID = this.request.OrderId;
				ul.RequestID = this.request.Id;
				
				ul.Insert();
			}

			using(MySqlConnection mysqlCon = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString))
			{
				mysqlCon.Open();
				
				bool doInsert = true;
				string aefid = "0";
				
				using (MySqlCommand cmd1 = new MySqlCommand ("SELECT aef_id FROM agent_examination WHERE aef_status = 0 AND aef_account_id = " + this.GetAccount().Id.ToString() + " and aef_request_id = " + this.request.Id.ToString(),  mysqlCon))
				{
          try
          {
              MySqlDataReader Reader = cmd1.ExecuteReader();
              while (Reader.Read()) // this part is wrong somehow
              {
                  aefid = Reader["aef_id"].ToString();
              }
              Reader.Close();
          }

          catch (Exception ex)
          {
              //MessageBox.Show(ex.Message);
              Response.Write(ex.Message);
              
          }
        }
				
				DateTime t = DateTime.Now;
				if(aefid.Equals("0"))
				{
					using (MySqlCommand cmd = new MySqlCommand ("INSERT INTO agent_examination (`aef_agents_name`, `aef_commitment_number`, `aef_property_vesting`, `aef_property_address`, `aef_property_city_state_zip`, `aef_property_county`, `aef_pin`, `aef_prior_year`, `aef_1st_installment`, `aef_2nd_installment`, `aef_current_year`, `aef_tax_amount_1st`, `aef_tax_amount_2nd`, `aef_due_date_1st`, `aef_due_date_2nd`, `aef_tax_year_of_sold_taxes`, `aef_mortgages_of_record`,  `aef_other_liens_of_record`, `aef_building_lines`, `aef_easements`, `aef_document_numbers`, `aef_condo_association`, `aef_search_package_reviewed_amendments`, `aef_search_package_reviewed`, `aef_amendments`, `aef_signature`, `aef_date_stamp`, `aef_attachment_id`, `aef_upload_account_id`, `aef_account_id`, `aef_order_id`, `aef_request_id`, `aef_created`, `aef_status`) VALUES ('" + txtAgentsName.Text.Replace("'", "''") + "', '" + commitment.Replace("'", "''") + "', '" + txtPropertyVesting.Text.Replace("'", "''") + "', '" + txtPropertyAddress.Text.Replace("'", "''") + "', '" + txtPropertyCityStateZip.Text.Replace("'", "''") + "', '" + txtPropertyCounty.Text.Replace("'", "''") + "', '" + txtPIN.Text.Replace("'", "''") + "', '" + txtPriorYear.Text.Replace("'", "''") + "', '" + txt1stInstallment.Text.Replace("'", "''") + "', '" + txt2ndInstallment.Text.Replace("'", "''") + "', '" + txtCurrentYear.Text.Replace("'", "''") + "', '" + txtTaxAmount1st.Text.Replace("'", "''") + "', '" + txtTaxAmount2nd.Text.Replace("'", "''") + "', " + ((txtDueDate1st.Text.Trim().Equals("") || !DateTime.TryParse(txtDueDate1st.Text, out t))? "null" : "'" + String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(txtDueDate1st.Text.Replace("'", "''"))) + "'") + ", " + ((txtDueDate2nd.Text.Trim().Equals("") || !DateTime.TryParse(txtDueDate2nd.Text, out t))? "null" : "'" + String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(txtDueDate2nd.Text.Replace("'", "''"))) + "'") + ", '" + txtTaxYearofSoldtaxes.Text.Replace("'", "''") + "', '" + txtMortgagesofRecord.Text.Replace("'", "''") + "', '" + txtOtherLiensofRecord.Text.Replace("'", "''") + "', '" + txtBuildingLines.Text.Replace("'", "''") + "', '" + txtEasements.Text.Replace("'", "''") + "', '" + txtDocumentNumbers.Text.Replace("'", "''") + "', '" + ((radioCondoAssociationYes.Checked) ? "Yes" : "No") + "', '" + ((chkSearchPackageReviewedAmendments.Checked) ? "Yes" : "No") + "', '" + ((chkSearchPackageReviewed.Checked) ? "Yes" : "No") + "', '" + txtAmendments.Text.Replace("'", "''") + "', '" + me.Signature.Replace("'", "''") + "', '" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + "', " + attID + ", " + this.request.Account.Id.ToString() + ", " + this.GetAccount().Id.ToString() + ", " + this.request.OrderId.ToString() + ", " + this.request.Id.ToString() + ", '" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + "', " + ((submitted)? "1" : "0" ) + " )",  mysqlCon))
	        {
	            try
	            {
	                MySqlDataReader Reader = cmd.ExecuteReader();
	                while (Reader.Read()) // this part is wrong somehow
	                {
	                    //citationstexter.Add(Reader.GetString(loopReading)); // this works
	                    //loopReading++; // this works
	                }
	                Reader.Close();
	            }
	
	            catch (Exception ex)
	            {
	                //MessageBox.Show(ex.Message);
	                Response.Write(ex.Message);
	                
	            }
	        }
	      }
	      else
	      {
					using (MySqlCommand cmd = new MySqlCommand ("UPDATE agent_examination SET `aef_agents_name` = '" + txtAgentsName.Text.Replace("'", "''") + "', `aef_commitment_number` = '" + commitment.Replace("'", "''") + "', `aef_property_vesting` = '" + txtPropertyVesting.Text.Replace("'", "''") + "', `aef_property_address` = '" + txtPropertyAddress.Text.Replace("'", "''") + "', `aef_property_city_state_zip` = '" + txtPropertyCityStateZip.Text.Replace("'", "''") + "', `aef_property_county` = '" + txtPropertyCounty.Text.Replace("'", "''") + "', `aef_pin` = '" + txtPIN.Text.Replace("'", "''") + "', `aef_prior_year` = '" + txtPriorYear.Text.Replace("'", "''") + "', `aef_1st_installment` = '" + txt1stInstallment.Text.Replace("'", "''") + "', `aef_2nd_installment` = '" + txt2ndInstallment.Text.Replace("'", "''") + "', `aef_current_year` = '" + txtCurrentYear.Text.Replace("'", "''") + "', `aef_tax_amount_1st` = '" + txtTaxAmount1st.Text.Replace("'", "''") + "', `aef_tax_amount_2nd` = '" + txtTaxAmount2nd.Text.Replace("'", "''") + "', `aef_due_date_1st` = " + ((txtDueDate1st.Text.Trim().Equals("") || !DateTime.TryParse(txtDueDate1st.Text, out t))? "null" : "'" + String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(txtDueDate1st.Text.Replace("'", "''"))) + "'") + ", `aef_due_date_2nd` = " + ((txtDueDate2nd.Text.Trim().Equals("") || !DateTime.TryParse(txtDueDate2nd.Text, out t))? "null" : "'" + String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(txtDueDate2nd.Text.Replace("'", "''"))) + "'") + ", `aef_tax_year_of_sold_taxes` = '" + txtTaxYearofSoldtaxes.Text.Replace("'", "''") + "', `aef_mortgages_of_record` = '" + txtMortgagesofRecord.Text.Replace("'", "''") + "',  `aef_other_liens_of_record` = '" + txtOtherLiensofRecord.Text.Replace("'", "''") + "', `aef_building_lines` = '" + txtBuildingLines.Text.Replace("'", "''") + "', `aef_easements` = '" + txtEasements.Text.Replace("'", "''") + "', `aef_document_numbers` = '" + txtDocumentNumbers.Text.Replace("'", "''") + "', `aef_condo_association` = '" + ((radioCondoAssociationYes.Checked) ? "Yes" : "No") + "', `aef_search_package_reviewed_amendments` = '" + ((chkSearchPackageReviewedAmendments.Checked) ? "Yes" : "No") + "', `aef_search_package_reviewed` = '" + ((chkSearchPackageReviewed.Checked) ? "Yes" : "No") + "', `aef_amendments` = '" + txtAmendments.Text.Replace("'", "''") + "', `aef_signature` = '" + me.Signature.Replace("'", "''") + "', `aef_date_stamp` = '" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + "', `aef_attachment_id` = " + attID + ", `aef_upload_account_id` = " + this.request.Account.Id.ToString() + ", `aef_account_id` = " + this.GetAccount().Id.ToString() + ", `aef_order_id` = " + this.request.OrderId.ToString() + ", `aef_request_id` = " + this.request.Id.ToString() + ", `aef_created` = '" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + "', `aef_status` = " + ((submitted)? "1" : "0" ) + " WHERE aef_id = " + aefid,  mysqlCon))
	        {
	            try
	            {
	                MySqlDataReader Reader = cmd.ExecuteReader();
	                while (Reader.Read()) // this part is wrong somehow
	                {
	                    //citationstexter.Add(Reader.GetString(loopReading)); // this works
	                    //loopReading++; // this works
	                }
	                Reader.Close();
	            }
	
	            catch (Exception ex)
	            {
	                //MessageBox.Show(ex.Message);
	                Response.Write(ex.Message);
	                
	            }
	        }
	      }
			}

			if(submitted)
			{
				using (FileStream fs = new FileStream(Server.MapPath("./") + "attachments/" + fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite, 8, true))
				{
					/*
					using (StreamWriter sw = new StreamWriter(fs))
					{
						// Write to the file using StreamWriter class 
						sw.BaseStream.Seek(0, SeekOrigin.End);
						sw.Write(contentsHTML);
						sw.Flush();
					}
					*/
					HiQPdf.HtmlToPdf converter = new HtmlToPdf();
		      converter.SerialNumber = "TwcmHh8rKQMmLT0uPTZ6YX9vfm9+b354eW98fmF+fWF2dnZ2";
		      converter.ConvertHtmlToStream(contentsHTML, "http://" + Request.Url.Host, fs);
				}
	
				/*
				using (FileStream fs = new FileStream(Server.MapPath("./") + "App_Data/" + fileName.Replace(".html", ".txt"), FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite, 8, true))
				{
					using (StreamWriter sw = new StreamWriter(fs))
					{
						// Write to the file using StreamWriter class 
						sw.BaseStream.Seek(0, SeekOrigin.End);
						sw.Write(contentsTXT);
						sw.Flush();
					}
				}
				*/
	
				//fuAttachment.SaveAs(Server.MapPath("./") + "attachments/" + fileName);
	
				/*
				txtEmailNote.Text = "A new file '" + att.Name + "' has been posted and is ready for your review.  "
					+ txtEmailNote.Text;
	
			SendNotification(oldStatus, newStatus, filePosted, txtEmailNote.Text);
				*/
	
	
	
	
	
	
	
				/*
				// Load the available messages to the combo box so we can pick one and view the details of this message
				// Logon and get message:
				oUserSession = oZfAPI.Logon("ADMINIST", false);
				oMessages = oUserSession.Outbox.GetMsgList();
	
				// Insert the message body into the combobox so the message would be selected according to this value
				IEnumerator MessaegesEnum = oMessages.GetEnumerator();
	
				while (MessaegesEnum.MoveNext())
				{
					// Iterate through the messages
					oMessage = (ZfLib.Message)MessaegesEnum.Current;
					//cmbMessages.Items.Add(oMessage.GetMsgInfo().Body); //Insert the message into the combobox
				}
	
				// Enable the controls according to the number of messages:
				if (cmbMessages.Items.Count == 0) //If there aren't any messages - disable both the combobox and the button
				{
					cmbMessages.Enabled = false;
					btnGetMessageInfo.Enabled = false;
				}
				else // In case there is at least 1 message in the outbox - put it as a default selection in the combobox
					cmbMessages.SelectedIndex = 0;
				 */
	
				if (FaxRadio.Checked)
				{
					AffinityFaxServer afs = new AffinityFaxServer();
					afs.Form_Load(contentsHTML, fileName);
				}
				else
				{
					//Response.Write(this.GetSystemSetting("SendFromEmail") + "<br />");
	
					// send to: title@affinityistitle.com
					MailMessage mm = new MailMessage(this.GetSystemSetting("SendFromEmail"), "title@affinityistitle.com, guy@affinityistitle.com", "Agent Examination Form " + commitment, "Agent Examination Form has been submitted by: " + me.FirstName + " " + me.LastName + "<br /><br /><br />\r\n\r\n" + this.GetSystemSetting("EmailFooter"));
					mm.IsBodyHtml = true;
					mm.Priority = MailPriority.Normal;
					
					if (File.Exists(Server.MapPath("./") + "attachments/" + fileName))
					{
						Attachment attch = new Attachment(Server.MapPath("./") + "attachments/" + fileName);
						attch.Name = fileName;
	
						mm.Attachments.Add(attch);
					}
					//SmtpClient sc = new SmtpClient(this.GetSystemSetting("SmtpHost"));
					//sc.Send(mm);
	
					Com.VerySimple.Email.Mailer mailer = new Com.VerySimple.Email.Mailer(this.GetSystemSetting("SmtpHost"));
					mailer.Send(mm);
				}
				
				/*
	                HttpContext.Current.Response.BufferOutput = true;
	                HttpContext.Current.Response.ContentType = "application/pdf";
	                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=AgentExaminationForm.pdf");
	      */
	
				Response.Write(contentsHTML.Replace("<h2>Affinity Title Search</h2>", "<h2>Affinity Title Search</h2><div style=\"font-size:24px;color:blue;\">The Agent Examination Form has been submitted successfully.</div>"));
				//Response.End();
			}
		}
		catch (System.Exception ex)
		{
			//this.Master.ShowFeedback(ex.Message, MasterPage.FeedbackType.Information);
			//Response.Write(ex.Message);
			Response.End();
		}
	}

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
		//else if (cbEnableEmail.Checked == false)
		//{
		//	actionTaken = "Email notification was disabled for this update";
		//}
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
				+ "You may view the full details of your order anytime online at " + url + ".  " 
				+ "If you would prefer to not receive this notification, you may also login "
				+ "to your Affinity account and customize your email notification preferences.\r\n\r\n"
				+ this.GetSystemSetting("EmailFooter");
				
				//Response.Write(msg);
				//Response.Write(this.GetSystemSetting("SendFromEmail"));
				//Response.Write(to);
				//Response.Write(subject);

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
