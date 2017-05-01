using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MySql.Data.MySqlClient;
using GemBox.Spreadsheet;
using System.Xml;
using System.IO;

public partial class UploadOrderSubmit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
			this.RequirePermission(Affinity.RolePermission.SubmitOrders);
			
			if (!Page.IsPostBack)
			{
	        Affinity.Accounts accts = new Affinity.Accounts(this.phreezer);
	        Affinity.AccountCriteria ac = new Affinity.AccountCriteria();
	        ac.AppendToOrderBy("LastName");
	        ac.AppendToOrderBy("FirstName");
	        accts.Query(ac);
	
	        ddNewOriginator.DataSource = accts;
	        ddNewOriginator.DataTextField = "FullName";
	        ddNewOriginator.DataValueField = "Id";
	
	        ddNewOriginator.DataBind();
	 		}
	
			this.Master.SetLayout(this.header.InnerText, MasterPage.LayoutStyle.ContentOnly);
    }

	/// <summary>
	/// cancel and return to the home page
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void btnCancel_Click(object sender, EventArgs e)
	{
		this.Redirect("Admin.aspx");
	}

	/// <summary>
	/// submit the order
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void btnSubmit_Click(object sender, EventArgs e)
	{
		this.RequirePermission(Affinity.RolePermission.AffinityManager);
		
		if(!fuAttachment.HasFile)
		{
			pnlResults.Visible = true;
			pResults.InnerHtml = "<h3 style=\"color:red;\">No File Uploaded.  Please choose an Excel file to upload.<h3>";
		}
		else
		{
			int accountidInt = 0;
			string originalfilename = fuAttachment.FileName;
			string ext = System.IO.Path.GetExtension(fuAttachment.FileName);
			string accountid = ddNewOriginator.SelectedValue;
			string county = txtPropertyCounty.Text;
			string transactiontype = ddTransactionType.SelectedValue;
			string tractsearch = (TractSearch.Checked.Equals("True")? "Yes" : "No");
			
			int.TryParse(accountid, out accountidInt);
			
			// Get the next available Internal ID and then increment for each order
			int internalId = 0;
			
			using(MySqlDataReader reader = this.phreezer.ExecuteReader("select Max(REPLACE(REPLACE(o_internal_id, 'AFF_', ''), 'AFF', '')) as maxId from `order` where o_internal_id like 'AFF%'"))
			{
				if(reader.Read())
				{
					string numStr = reader["maxId"].ToString();
					int.TryParse(numStr, out internalId);
					internalId++;
				}
			}
			
			string internalIdStr = internalId.ToString();
			string filename = internalIdStr + ext;
			int uploadidInt = 0;
			
			using(MySqlDataReader reader = this.phreezer.ExecuteReader("insert into order_upload_log (oul_a_id, oul_original_filename, oul_filename, oul_starting_internal_id, oul_created, oul_modified) VALUES (" + this.GetAccount().Id.ToString() + ", '" + originalfilename + "', '" + filename + "', " + internalIdStr + ", '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'); SELECT @@IDENTITY as id;"))
			{
				if(reader.Read())
				{
					int.TryParse(reader["id"].ToString(), out uploadidInt);
				}
			}
					
			SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
			string path = Server.MapPath(".\\") + "XlsUploads\\";
			string filepath = path + filename;
			
			if(File.Exists(filepath))
			{
				File.Delete(filepath);
			}
			fuAttachment.SaveAs(filepath);
			
			ExcelFile xlsFile = new ExcelFile();
			
			if(ext.Equals(".xlsx"))
			{
				string zippath = path + internalIdStr + "\\";
				
				if(!Directory.Exists(zippath))
				{
					Directory.CreateDirectory(zippath);
				}
				
				string zipfilepath = path + internalIdStr + ".zip";
				if(File.Exists(zipfilepath)) {
					File.Delete(zipfilepath);
				}
				File.Move(filepath, zipfilepath);
				Xceed.Zip.Licenser.LicenseKey = "ZIN20N4AFUNK71J44NA";
				string[] sarr = { "*" };
				

        Xceed.Zip.QuickZip.Unzip(zipfilepath, zippath, sarr);			
				xlsFile.LoadXlsxFromDirectory(zippath, XlsxOptions.None);
			}
			else
			{
				xlsFile.LoadXls(path);
			}
			ExcelWorksheet ws = xlsFile.Worksheets[0];			
	
			pnlResults.Visible = true;
			pnlForm.Visible = false;
			btnSubmit.Visible = false;
			btnCancel.Text = "Back to Admin";
			ListDictionary duplicatePINs = new ListDictionary();

			
			string pin = " ";
			string address = " ";
			string address1 = "";
			string address2 = "";
			string city = "";
			string state = "";
			string zip = "";
			string firmname = "";
			string attorney = "";
			string attorneyaddress1 = "";
			string attorneyaddress2 = "";
			string attorneycity = "";
			string attorneystate = "";
			string attorneyzip = "";
			string attorneyphone = "";
			string attorneyemail = "";
			string attorneyattentionto = "";
			int idx = 1;
			
			/****************************************************************************************
			GET ACCOUNT INFORMATION
			****************************************************************************************/
			Affinity.Account account = new Affinity.Account(this.phreezer);
	
			account.Load(accountid);

			XmlDocument preferencesXML = new XmlDocument();
			preferencesXML.LoadXml(account.PreferencesXml);
			
			XmlNode node = preferencesXML.SelectSingleNode("//Field[@name='ApplicantName']");
			if(node != null)
			{
				firmname = node.InnerText;
			}
			
			node = preferencesXML.SelectSingleNode("//Field[@name='ApplicantAttorneyName']");
			if(node != null)
			{
				attorney = node.InnerText;
			}					
			
			node = preferencesXML.SelectSingleNode("//Field[@name='ApplicantAddress']");
			if(node != null)
			{
				attorneyaddress1 = node.InnerText;
			}
			
			node = preferencesXML.SelectSingleNode("//Field[@name='ApplicantAddress2']");
			if(node != null)
			{
				attorneyaddress2 = node.InnerText;
			}
			
			node = preferencesXML.SelectSingleNode("//Field[@name='ApplicantCity']");
			if(node != null)
			{
				attorneycity = node.InnerText;
			}
			
			node = preferencesXML.SelectSingleNode("//Field[@name='ApplicantState']");
			if(node != null)
			{
				attorneystate = node.InnerText;
			}
			
			node = preferencesXML.SelectSingleNode("//Field[@name='ApplicantZip']");
			if(node != null)
			{
				attorneyzip = node.InnerText;
			}
			
			node = preferencesXML.SelectSingleNode("//Field[@name='ApplicantPhone']");
			if(node != null)
			{
				attorneyphone = node.InnerText;
			}
			
			node = preferencesXML.SelectSingleNode("//Field[@name='ApplicantEmail']");
			if(node != null)
			{
				attorneyemail = node.InnerText;
			}
			
			node = preferencesXML.SelectSingleNode("//Field[@name='ApplicantAttentionTo']");
			if(node != null)
			{
				attorneyattentionto = node.InnerText;
			}
			
			string xml = "<response><field name=\"CommittmentDeadline\"></field><field name=\"PreviousTitleEvidence\"></field><field name=\"Prior\"></field><field name=\"TypeOfProperty\">Single Family</field><field name=\"PropertyUse\"></field><field name=\"TransactionType\">[TRANSACTIONTYPE]</field><field name=\"TractSearch\">[TRACTSEARCH]</field><field name=\"ShortSale\"></field><field name=\"Foreclosure\"></field><field name=\"CashSale\"></field><field name=\"ConstructionEscrow\"></field><field name=\"ReverseMortgage\"></field><field name=\"EndorsementEPA\"></field><field name=\"EndorsementLocation\"></field><field name=\"EndorsementCondo\"></field><field name=\"EndorsementComp\"></field><field name=\"EndorsementARM\"></field><field name=\"EndorsementPUD\"></field><field name=\"EndorsementBalloon\"></field><field name=\"EndorsementOther\"></field><field name=\"MortgageAmount\">1.00</field><field name=\"PurchasePrice\"></field><field name=\"TRID\">No</field><field name=\"LoanNumber\"></field><field name=\"SecondMortgage\">No</field><field name=\"SecondMortgageAmount\"></field><field name=\"LoanNumber2nd\"></field><field name=\"ChainOfTitle\">No</field><field name=\"Buyer\">" + (TractSearch.Checked.Equals("True")? "." : "") + "</field><field name=\"Buyer1Name2\"></field><field name=\"AddBuyer2\"></field><field name=\"Buyer2Name1\"></field><field name=\"Buyer2Name2\"></field><field name=\"AddBuyer3\"></field><field name=\"Buyer3Name1\"></field><field name=\"Buyer3Name2\"></field><field name=\"AddBuyer4\"></field><field name=\"Buyer4Name1\"></field><field name=\"Buyer4Name2\"></field><field name=\"AddBuyer5\"></field><field name=\"Buyer5Name1\"></field><field name=\"Buyer5Name2\"></field><field name=\"Seller\"></field><field name=\"Seller1Name2\"></field><field name=\"AddSeller2\"></field><field name=\"Seller2Name1\"></field><field name=\"Seller2Name2\"></field><field name=\"AddSeller3\"></field><field name=\"Seller3Name1\"></field><field name=\"Seller3Name2\"></field><field name=\"AddSeller4\"></field><field name=\"Seller4Name1\"></field><field name=\"Seller4Name2\"></field><field name=\"AddSeller5\"></field><field name=\"Seller5Name1\"></field><field name=\"Seller5Name2\"></field><field name=\"Underwriter\"></field><field name=\"ApplicantName\">[FIRMNAME]</field><field name=\"ApplicantAttorneyName\">[ATTORNEY]</field><field name=\"ApplicantAddress\">[ADDRESS1]</field><field name=\"ApplicantAddress2\">[ADDRESS1]</field><field name=\"ApplicantCity\">[CITY]</field><field name=\"ApplicantState\">[STATE]</field><field name=\"ApplicantZip\">[ZIP]</field><field name=\"ApplicantPhone\">[PHONE]</field><field name=\"ApplicantFax\"></field><field name=\"ApplicantEmail\">[EMAIL]</field><field name=\"ApplicantAttentionTo\">[ATTENTIONTO]</field><field name=\"CopyApplicationTo\"></field><field name=\"LenderName\"></field><field name=\"LenderContact\"></field><field name=\"LenderAddress\"></field><field name=\"LenderAddress2\"></field><field name=\"LenderCity\"></field><field name=\"LenderState\"></field><field name=\"LenderZip\"></field><field name=\"LenderPhone\"></field><field name=\"LenderFax\"></field><field name=\"LenderEmail\"></field><field name=\"BrokerName\"></field><field name=\"BrokerLoanOfficer\"></field><field name=\"BrokerAddress\"></field><field name=\"BrokerAddress2\"></field><field name=\"BrokerCity\"></field><field name=\"BrokerState\"></field><field name=\"BrokerZip\"></field><field name=\"BrokerPhone\"></field><field name=\"BrokerFax\"></field><field name=\"BrokerEmail\"></field><field name=\"Notes\"></field><field name=\"Source\">Web Order ID</field><field name=\"SubmittedDate\">[DATE]</field><field name=\"OrderRequestStatus\">Requested</field></response>".Replace("[TRANSACTIONTYPE]", transactiontype).Replace("[TRACTSEARCH]", tractsearch).Replace("[FIRMNAME]", firmname).Replace("[ATTORNEY]", attorney).Replace("[ADDRESS1]", attorneyaddress1).Replace("[ADDRESS2]", attorneyaddress2).Replace("[CITY]", attorneycity).Replace("[STATE]", attorneystate).Replace("[ZIP]", attorneyzip).Replace("[PHONE]", attorneyphone).Replace("[EMAIL]", attorneyemail).Replace("[ATTENTIONTO]", attorneyattentionto).Replace("[DATE]", DateTime.Now.ToString());
			/****************************************************************************************
			END GETTING ACCOUNT INFORMATION
			****************************************************************************************/
					
			while(!pin.Equals("") && ws.Rows[idx] != null && ws.Rows[idx].Cells[1] != null && ws.Rows[idx].Cells[1].Value != null)
			{
				pin = ws.Rows[idx].Cells[1].Value.ToString();
				address = ws.Rows[idx].Cells[3].Value.ToString();
				address1 = "";
				address2 = "";
				city = "";
				state = "";
				zip = "";
				firmname = "";
				attorney = "";
				attorneyaddress1 = "";
				attorneyaddress2 = "";
				attorneycity = "";
				attorneystate = "";
				attorneyzip = "";
				attorneyphone = "";
				attorneyemail = "";
				attorneyattentionto = "";
				
				if(!address.Equals(""))
				{
	      	XmlDocument doc = new XmlDocument();
	      	string url = "http://maps.googleapis.com/maps/api/geocode/xml?address=" + address + "&key=";
	
	        System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
	        req.Method = "GET";
	        req.Accept = "text/xml";
	        req.KeepAlive = false;
	
	        System.Net.HttpWebResponse response = null;
	
	        using (response = (System.Net.HttpWebResponse) req.GetResponse()) //attempt to get the response.
	        {
	          using (Stream RespStrm = response.GetResponseStream())
	          {
	            doc.Load(RespStrm);
	          }
	        }
	        
	        XmlNode streetnumbernode = doc.SelectSingleNode("/GeocodeResponse/result/address_component[type='street_number']");
	        if(streetnumbernode != null)
	        {
	        	address1 += streetnumbernode.SelectSingleNode("long_name").InnerText;
	        }
	        
	        XmlNode routenode = doc.SelectSingleNode("/GeocodeResponse/result/address_component[type='route']");
	        if(routenode != null)
	        {
	        	address1 += " " + routenode.SelectSingleNode("long_name").InnerText;
	        }
	        
	        XmlNode citynode = doc.SelectSingleNode("/GeocodeResponse/result/address_component[type='locality']");
	        if(citynode != null)
	        {
	        	city = citynode.SelectSingleNode("long_name").InnerText;
	        }
	        
	        if(county.Equals(""))
	        {
		        XmlNode countynode = doc.SelectSingleNode("/GeocodeResponse/result/address_component[type='administrative_area_level_2']");
		        if(countynode != null)
		        {
		        	county = countynode.SelectSingleNode("long_name").InnerText;
		        }
		      }
	        
	        XmlNode statenode = doc.SelectSingleNode("/GeocodeResponse/result/address_component[type='administrative_area_level_1']");
	        if(statenode != null)
	        {
	        	state = statenode.SelectSingleNode("short_name").InnerText;
	        }
	        	
	        XmlNode zipnode = doc.SelectSingleNode("/GeocodeResponse/result/address_component[type='postal_code']");
	        if(zipnode != null)
	        {
	        	zip = zipnode.SelectSingleNode("long_name").InnerText;
	        }
	        
	        //Response.Write(idx.ToString() + " - " + pin + " - " + address1 + " - " + city + " - " + state + " - " + zip + " - " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>");
	        
					// create a new order for this request
					Affinity.Order order = new Affinity.Order(this.phreezer);
			
					order.OriginatorId = accountidInt;
					order.CustomerStatusCode = Affinity.OrderStatus.ReadyCode;
					order.InternalStatusCode = Affinity.OrderStatus.ReadyCode;
					order.InternalId = "";
			
					order.ClientName = "";
					order.Pin = pin;
					order.AdditionalPins = "";
					order.PropertyAddress = address1;
					order.PropertyAddress2 = address2;
					order.PropertyCity = city;
					order.PropertyState = state;
					order.PropertyZip = zip;
					order.CustomerId = pin;
					order.PropertyCounty = county;
					order.PropertyUse = "Residential";
					order.OrderUploadLogId = uploadidInt;
	      
					try
					{
						Affinity.Order PreviousOrder = order.GetPrevious();
						// verify the user has not submitted this PIN in the past		
						if(PreviousOrder == null)
						{
							order.Insert();
							internalId++;
							
							Affinity.Request rq = new Affinity.Request(this.phreezer);
							rq.OrderId = order.Id;
							rq.OriginatorId = accountidInt;
							rq.RequestTypeCode = "Refinance";
							rq.StatusCode = Affinity.RequestStatus.DefaultCode;
					
							rq.Xml = xml;
							
							rq.Insert();
						}
						else
						{
							duplicatePINs.Add(pin, "");
						}
					}
					catch (FormatException ex)
					{
						this.Master.ShowFeedback("Please check that the estimated closing date is valid and in the format 'mm/dd/yyyy'",MasterPage.FeedbackType.Error);
					}
				}

				idx++;
			}
			pnlResults.Visible = true;
			pResults.InnerHtml = "<h1 style=\"color:black;\">" + (idx - duplicatePINs.Count).ToString() + " orders have been imported. " + duplicatePINs.Count.ToString() + " records failed.</h1>";
		}
	}
}
