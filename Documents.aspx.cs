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
using System.Xml;
using MySql.Data.MySqlClient;

using Com.VerySimple.Util;

public partial class Documents : PageBase
{
    public static string Ordinal(int number)
    {
        int ones = number % 10;
        double num = number / 10;
        double tens = Math.Floor (num) % 10;
        if (tens == 1)
        {
            return number.ToString() + "th";
        }

        switch (ones)
        {
            case 1: return number.ToString() + "st";
            case 2: return number.ToString() + "nd";
            case 3: return number.ToString() + "rd";
            default: return number.ToString() + "th";
        }
    }
    
    protected string XMLEscape(string s)
    {
    	return s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;");
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
		/*
		// get stack trace
		System.Diagnostics.StackFrame fr = new System.Diagnostics.StackFrame(1, true);
		System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(fr);
		Response.Write( "method = " + fr.GetMethod().Name + " trace = " + st.ToString() );
		*/
		// we have to call the base first so phreezer is instantiated
		base.PageBase_Init(sender, e);
    PageBase pb = (PageBase)this.Page;
    Affinity.Account acc = pb.GetAccount();
    string contents = "";

		int id = NoNull.GetInt(Request["id"], 0);
		
		//Response.Write(Request["file"]);
		//Response.End();
		
		if(id > 0)
		{
		
		Affinity.Order order = new Affinity.Order(this.phreezer);
		order.Load(id);
		
		if(order.OriginatorId != 1 && order.OriginatorId != 2 && order.OriginatorId != 4 && order.OriginatorId != 5 && order.OriginatorId != 7 && order.OriginatorId != 8 && order.OriginatorId != 9 && order.OriginatorId != 11 && order.OriginatorId != 12  && order.OriginatorId != 13 && order.OriginatorId != 16 && order.OriginatorId != 117 && order.OriginatorId != 199 && order.OriginatorId != 333)
		{
			Disclosure_OwnerDIV.Visible = false;
		}
		else
		{
			Disclosure_OwnerDIV.Visible = true;
		}

		int filecount = 0;
		this.Master.SetLayout("Documents", MasterPage.LayoutStyle.ContentOnly);
		if(Request["file"] != null || Request["warrantyfile"] != null)
		{
			string files = "";
			if(Request["file"] != null)
			{
				files = Request["file"];
				if(Request["warrantyfile"] != null)
				{
					files += "," + Request["wfile"];
				}
			}
			else
			{
				files = Request["wfile"];
			}
			string [] file = files.Split(',');
						
			for(int i = 0; i < file.Length; i++)
			{
				if((file[i].Equals("WD") || file[i].Equals("WD_S") || file[i].Equals("WD_JT") || file[i].Equals("WD_TE")) && Request["warrantyfile"] == null) continue;
				string path = HttpContext.Current.Server.MapPath(".") + "\\downloads\\" + file[i] + ".xml";
				if(File.Exists(path))
				{
					filecount++;
				}
			}
			
			//Response.Write(path);
			
			string header = "";
			string footer = "";
			
			using(FileStream fs = new FileStream(HttpContext.Current.Server.MapPath(".") + "\\downloads\\header.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite))
			{
				using(StreamReader sr = new StreamReader(fs))
				{
					header = sr.ReadToEnd() + "\n";
				}
			}

			using(FileStream fs = new FileStream(HttpContext.Current.Server.MapPath(".") + "\\downloads\\footer.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite))
			{
				using(StreamReader sr = new StreamReader(fs))
				{
					footer = sr.ReadToEnd() + "\n";
				}
			}
			
			contents = header;
			int ii = 0;
			string poacontents = "";
			string executorcontents = "";
			bool isPOA = false;
			bool isExecutor = false;
			string previousFile = "";
						
			for(int i = 0; i < file.Length; i++)
			{
				bool isWD = (file[i].Equals("WD") || file[i].Equals("WD_S") || file[i].Equals("WD_JT") || file[i].Equals("WD_TE"));
				if(isWD && Request["warrantyfile"] == null) continue;
				string path = HttpContext.Current.Server.MapPath(".") + "\\downloads\\" + file[i] + ".xml";
				if(File.Exists(path))
				{
					using(FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
					{
						using(StreamReader sr = new StreamReader(fs))
						{
							//if(ii > 0 && (!isPOA || ii > 1) && !file[i].Equals("ExecutorDeed") && !file[i].Equals("DeedinTrust") && !file[i].Equals("TrusteesDeed") && !file[i].Equals("1099_HUD_Page_4") && !file[i].Equals("Disclosure_Owner"))
							if(ii > 0 && (!isPOA || !file[i].Equals("POA") || ii > 1) && !file[i].Equals("1099_HUD_Page_4") && !file[i].Equals("ExecutorDeed") && !file[i].Equals("DeedinTrust") && !file[i].Equals("TrusteesDeed") && !file[i].Equals("Disclosure_Owner") && !isWD)
							{
								contents += "\n<w:p w:rsidR=\"00996AC2\" w:rsidRDefault=\"00996AC2\"><w:r><w:br w:type=\"page\"/></w:r></w:p>";
							}

							string filecontents = sr.ReadToEnd();
							
							if(file[i].Equals("1099_HUD_Page_4") && (previousFile.Equals("POA") || previousFile.Equals("Affidavit_of_Title") || previousFile.Equals("Bill_of_Sale") || previousFile.Equals("DeedinTrust") || previousFile.Equals("TrusteesDeed")))
							{
								filecontents = filecontents.Replace("<w:p w:rsidR=\"006F279C\" w:rsidRPr=\"000200E0\" w:rsidRDefault=\"007B104C\"><w:pPr><w:jc w:val=\"center\"/><w:outlineLvl w:val=\"0\"/><w:rPr><w:sz w:val=\"18\"/><w:szCs w:val=\"18\"/></w:rPr></w:pPr><w:r w:rsidRPr=\"000200E0\"><w:rPr><w:sz w:val=\"18\"/><w:szCs w:val=\"18\"/></w:rPr>", "<w:p w:rsidR=\"00CA06F6\" w:rsidRDefault=\"00A77337\"/><w:p w:rsidR=\"006F279C\" w:rsidRPr=\"000200E0\" w:rsidRDefault=\"00A77337\" w:rsidP=\"00AF44B5\"><w:pPr><w:jc w:val=\"center\"/><w:rPr><w:sz w:val=\"18\"/><w:szCs w:val=\"18\"/></w:rPr></w:pPr><w:r><w:br w:type=\"page\"/></w:r><w:r w:rsidRPr=\"000200E0\"><w:rPr><w:sz w:val=\"18\"/><w:szCs w:val=\"18\"/></w:rPr><w:lastRenderedPageBreak/>");
							}
							contents += filecontents;
							previousFile = file[i];
							
							if(!file[i].Equals("WD_JT") && !file[i].Equals("ExecutorDeed") && !file[i].Equals("DeedinTrust") && !file[i].Equals("TrusteesDeed") && !file[i].Equals("1099_HUD_Page_4") && !file[i].Equals("POA"))
							{
								contents += "<w:p w:rsidR=\"00CA06F6\" w:rsidRDefault=\"00CA06F6\"/><w:sectPr w:rsidR=\"00CA06F6\"><w:endnotePr><w:numFmt w:val=\"decimal\"/></w:endnotePr><w:pgSz w:w=\"12240\" w:h=\"15840\" w:code=\"1\"/><w:pgMar w:top=\"720\" w:right=\"720\" w:bottom=\"720\" w:left=\"720\" w:header=\"720\" w:footer=\"331\" w:gutter=\"0\"/><w:cols w:space=\"720\"/><w:noEndnote/></w:sectPr>";
							}
							
							if(file[i].Equals("POA"))
							{
								contents += "[POAFILE]";
								poacontents = filecontents;
								isPOA = true;
							}
							
							if(file[i].Equals("ExecutorDeed"))
							{
								contents += "[EXECUTORFILE]";
								executorcontents = filecontents;
								isExecutor = true;
							}
							ii++;
						}
					}
				}
			}
			
		
			contents += footer;

			//Response.Write(contents);
		
			//Response.End();
	    string County = "";
	    string CommittmentDate = "";
	    string Grantee = "";
	    string Grantor = "";
	    string Grantee2 = "";
	    string Grantor2 = "";
	    DateTime DeedDate = order.ClosingDate;
	    string[] BuyersNameArray = new string[10];
	    string[] SellersNameArray = new string[10];
	    
			for(int sellerIdx = 0; sellerIdx < SellersNameArray.Length; sellerIdx++)
			{
				SellersNameArray[sellerIdx] = "";
			}
			
			for(int buyerIdx = 0; buyerIdx < BuyersNameArray.Length; buyerIdx++)
			{
				BuyersNameArray[buyerIdx] = "";
			}
			
	    string SellersNames = "";
	    string BuyersNames = "";
	    string SellersAddress = "";
	    string BuyersAddress = "";
	    string GrantorAddressLine1 = "";
	    string GrantorAddressLine2 = "";
	    string GrantorCity = "";
	    string GrantorState = "";
	    string GrantorZip = "";
	    string BuyerAddressLine1 = "";
	    string BuyerAddressLine2 = "";
	    string BuyerCity = "";
	    string BuyerState = "";
	    string BuyerZip = "";
	    string GrantorCounty = "";
	    DateTime now = DateTime.Now;
	    string DateDay = Ordinal(now.Day).ToString();
	    string DateMonth = now.ToString("MMMM");
	    string DateYear = now.ToString("yyyy");
	    string PIN = "";
	    string AFFID = "";
	    string SellersAttorneyFirm = "";
	    string SellersAttorney = "";
	    string ApplicantAttorneyName = "";
	    string SellersAttorneyAddress = "";
			bool morethantwoSellers = false;
	    
	    string BuyerAttorneyNameFirm = "";
	    string BuyerAttorneyName = "";
	    string BuyerAttorneyAddressLine1 = "";
	    string BuyerAttorneyAddressLine2 = "";
	    string BuyerAttorneyCity = "";
	    string BuyerAttorneyState = "";
	    string BuyerAttorneyZip = "";

	    string BuyerAttorneyMailToAddress1 = "";
	    string BuyerAttorneyMailToAddress2 = "";
	    
	    // FEES
	    string WireTransferFee = "";
	    string PolicyUpdateFee = "";
	    string CommitmentLaterDate = "";
	    string MortgagePolicy = "";
	    string AbstractFee = "";
	    string RecordingFee = "";
	    string EmailFee = "";
	    string EndorsementFee = "";
	    string PurchasePrice = "";
	    string InsuranceRate = "";
	    string ClosingRate = "";
	    string BuyerTransferStamps = "________________";
	    string SellerTransferStamps = "________________";
	    
	    // Get Fees
	    Affinity.TitleFee tfitem = new Affinity.TitleFee(this.phreezer);
	    tfitem.Load("147");
	    if(tfitem.Fee.ToString().Length < 6) {
	    	WireTransferFee = tfitem.Fee.ToString().PadLeft(7);
	    }
	    else {
	    	WireTransferFee = tfitem.Fee.ToString();
	    }
	    
	    tfitem.Load("145");
	    if(tfitem.Fee.ToString().Length < 6) {
	    	PolicyUpdateFee = tfitem.Fee.ToString().PadLeft(7);
	    }
	    else {
	    	PolicyUpdateFee = tfitem.Fee.ToString();
	    }
	    
	    tfitem.Load("109");
	    if(tfitem.Fee.ToString().Length < 6) {
	    	CommitmentLaterDate = tfitem.Fee.ToString().PadLeft(7);
	    }
	    else {
	    	CommitmentLaterDate = tfitem.Fee.ToString();
	    }
	    	    
	    tfitem.Load("84");
	    if(tfitem.Fee.ToString().Length < 6) {
	    	MortgagePolicy = tfitem.Fee.ToString().PadLeft(7);
	    }
	    else {
	    	MortgagePolicy = tfitem.Fee.ToString();
	    }
	    	    
	    tfitem.Load("83");
	    if(tfitem.Fee.ToString().Length < 6) {
	    	AbstractFee = tfitem.Fee.ToString().PadLeft(7);
	    }
	    else {
	    	AbstractFee = tfitem.Fee.ToString();
	    }
	     
	    tfitem.Load("150");
	    if(tfitem.Fee.ToString().Length < 6) {
	    	RecordingFee = tfitem.Fee.ToString().PadLeft(7);
	    }
	    else {
	    	RecordingFee = tfitem.Fee.ToString();
	    }
	     
	    tfitem.Load("139");
	    if(tfitem.Fee.ToString().Length < 6) {
	    	EmailFee = tfitem.Fee.ToString().PadLeft(7);
	    }
	    else {
	    	EmailFee = tfitem.Fee.ToString();
	    }
	    
	    tfitem.Load("142");
	    if(tfitem.Fee.ToString().Length < 6) {
	    	EndorsementFee = tfitem.Fee.ToString().PadLeft(7);
	    }
	    else {
	    	EndorsementFee = tfitem.Fee.ToString();
	    }
	    
	    int day = DeedDate.Day;
	    string DeedDateOrdinal = day.ToString();
	    
		  if (day == 11 || day == 12 || day == 13)
		  {
		    DeedDateOrdinal += "th";
		  }
		  else
		  {
			  switch (day % 10)
			  {
			    case 1: DeedDateOrdinal += "st"; break;
			    case 2: DeedDateOrdinal += "nd"; break;
			    case 3: DeedDateOrdinal += "rd"; break;
			    default: DeedDateOrdinal += "th"; break;
			  }
			}
			
			using(MySqlConnection mysqlCon = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString))
			{
				mysqlCon.Open();
				using (MySqlCommand cmd = new MySqlCommand
        ("SELECT * FROM agent_examination WHERE aef_order_id = " + id.ToString() + " ORDER BY aef_id DESC LIMIT 1",  mysqlCon))
        {
            try
            {
                MySqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read()) // this part is wrong somehow
                {
                    //Response.Write("Name: " + Reader["aef_agents_name"].ToString());
                    SellersNameArray[0] = Reader["aef_agents_name"].ToString();
                    Grantor = SellersNameArray[0];
                    GrantorAddressLine1 = Reader["aef_property_address"].ToString().Trim();
								    GrantorCity = Reader["aef_property_city_state_zip"].ToString().Trim();
								    GrantorCounty = Reader["aef_property_county"].ToString().Trim();

                    SellersAddress = Reader["aef_property_address"].ToString().Trim() + " " + Reader["aef_property_city_state_zip"].ToString().Trim();
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
			
      //Response.Write(request.Xml);
      //Response.End();
			
			try {
	    XmlDocument doc = new XmlDocument();
	    // show the details for the active requests
			Affinity.Requests rs = order.GetCurrentRequests();

			foreach (Affinity.Request r in rs)
			{
			
			if (r.RequestTypeCode.Equals("Order"))
			{

		    doc.LoadXml(r.Xml);
		    //Response.Write(r.Xml);
		    //Response.End();
		
		    XmlNode node = doc.SelectSingleNode("//response/field[@name = 'CommittmentDeadline']");
		    if (node != null && !node.InnerText.Equals("")) CommittmentDate = XMLEscape(node.InnerText.Replace("T:00:00:00", ""));
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Seller']");
		    if (node != null && !node.InnerText.Equals("")) SellersNameArray[0] = XMLEscape(node.InnerText);
		    Grantor = SellersNameArray[0];
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Seller1Name2']");
		    if (node != null && !node.InnerText.Equals("")) 
		    {
		    	SellersNameArray[1] = XMLEscape(node.InnerText);
		    	Grantor2 = SellersNameArray[1];
		    }
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Seller2Name1']");
		    if (node != null && !node.InnerText.Equals("")) 
		    {
		    	SellersNameArray[2] = XMLEscape(node.InnerText);
		    }
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Seller2Name2']");
		    if (node != null && !node.InnerText.Equals("")) 
		    {
		    	SellersNameArray[3] = XMLEscape(node.InnerText);
		    }
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Seller3Name1']");
		    if (node != null && !node.InnerText.Equals("")) 
		    {
		    	SellersNameArray[4] = XMLEscape(node.InnerText);
		    }
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Seller3Name2']");
		    if (node != null && !node.InnerText.Equals("")) 
		    {
		    	SellersNameArray[5] = XMLEscape(node.InnerText);
		    }
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Seller4Name1']");
		    if (node != null && !node.InnerText.Equals("")) 
		    {
		    	SellersNameArray[6] = XMLEscape(node.InnerText);
		    }
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Seller4Name2']");
		    if (node != null && !node.InnerText.Equals("")) 
		    {
		    	SellersNameArray[7] = XMLEscape(node.InnerText);
		    }
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Seller5Name1']");
		    if (node != null && !node.InnerText.Equals("")) 
		    {
		    	SellersNameArray[8] = XMLEscape(node.InnerText);
		    }
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Seller5Name2']");
		    if (node != null && !node.InnerText.Equals("")) 
		    {
		    	SellersNameArray[9] = XMLEscape(node.InnerText);
		    }
		    //SellersNameArray[1] = "Wilma";
		    //Grantor2 = SellersNameArray[1];

		    if(isPOA)
		    {
		    	bool isSet = false;
		    	string poaTotalContents = "";
					for(int sellerIdx = 1; sellerIdx < SellersNameArray.Length; sellerIdx++)
					{
			    	if(!SellersNameArray[sellerIdx].Equals(""))
			    	{
			    		if(!isSet) {
			    			//poacontents = "\n<w:p w:rsidR=\"00996AC2\" w:rsidRDefault=\"00996AC2\"><w:r><w:br w:type=\"page\"/></w:r></w:p>" + poacontents;
			    			poaTotalContents = "\n<w:p w:rsidR=\"00996AC2\" w:rsidRDefault=\"00996AC2\"><w:r><w:br w:type=\"page\"/></w:r></w:p>" + poaTotalContents;
			    			isSet = true;
			    		}
							poaTotalContents += poacontents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type name of trustee]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t>" + SellersNameArray[sellerIdx] + "</w:t>").Replace("s1026", "s102899").Replace("s1027", "s102999");
			    	}
			    }
			    if(poaTotalContents.Equals(""))
			    {
			    	contents = contents.Replace("[POAFILE]", "<w:p w:rsidR=\"00CA06F6\" w:rsidRDefault=\"00CA06F6\"/><w:sectPr w:rsidR=\"00CA06F6\"><w:endnotePr><w:numFmt w:val=\"decimal\"/></w:endnotePr><w:pgSz w:w=\"12240\" w:h=\"15840\" w:code=\"1\"/><w:pgMar w:top=\"720\" w:right=\"720\" w:bottom=\"720\" w:left=\"720\" w:header=\"720\" w:footer=\"331\" w:gutter=\"0\"/><w:cols w:space=\"720\"/><w:noEndnote/></w:sectPr>");
			    }
			    else
			    {
				    contents = contents.Replace("[POAFILE]", poaTotalContents);
				  }
		    }

		    if(isExecutor)
		    {
		    	string executorTotalContents = "";
					for(int sellerIdx = 1; sellerIdx < SellersNameArray.Length; sellerIdx++)
					{
			    	if(!SellersNameArray[sellerIdx].Equals(""))
			    	{
							executorTotalContents += executorcontents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type name of deceased]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t>" + SellersNameArray[sellerIdx] + "</w:t>");
			    	}
			    }
			    if(executorTotalContents.Equals(""))
			    {
			    	contents = contents.Replace("[EXECUTORFILE]", "");
			    }
			    else
			    {
				    contents = contents.Replace("[EXECUTORFILE]", executorTotalContents);
				  }
		    }		
		    node = doc.SelectSingleNode("//response/field[@name = 'Buyer']");
		    if (node != null && !node.InnerText.Equals("")) BuyersNameArray[0] = XMLEscape(node.InnerText);
		    Grantee = BuyersNameArray[0];
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Buyer1Name2']");
		    if (node != null && !node.InnerText.Equals("")) 
		    {
		    	BuyersNameArray[1] = XMLEscape(node.InnerText);
		    	Grantee2 = BuyersNameArray[1];
		    }
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Buyer2Name1']");
		    if (node != null && !node.InnerText.Equals("")) 
		    {
		    	BuyersNameArray[2] = XMLEscape(node.InnerText);
		    }
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Buyer2Name2']");
		    if (node != null && !node.InnerText.Equals("")) 
		    {
		    	BuyersNameArray[3] = XMLEscape(node.InnerText);
		    }
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Buyer3Name1']");
		    if (node != null && !node.InnerText.Equals("")) 
		    {
		    	BuyersNameArray[4] = XMLEscape(node.InnerText);
		    }
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Buyer3Name2']");
		    if (node != null && !node.InnerText.Equals("")) 
		    {
		    	BuyersNameArray[5] = XMLEscape(node.InnerText);
		    }
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Buyer4Name1']");
		    if (node != null && !node.InnerText.Equals("")) 
		    {
		    	BuyersNameArray[6] = XMLEscape(node.InnerText);
		    }
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Buyer4Name2']");
		    if (node != null && !node.InnerText.Equals("")) 
		    {
		    	BuyersNameArray[7] = XMLEscape(node.InnerText);
		    }
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Buyer5Name1']");
		    if (node != null && !node.InnerText.Equals("")) 
		    {
		    	BuyersNameArray[8] = XMLEscape(node.InnerText);
		    }
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Buyer5Name2']");
		    if (node != null && !node.InnerText.Equals("")) 
		    {
		    	BuyersNameArray[9] = XMLEscape(node.InnerText);
		    }
		
		    node = doc.SelectSingleNode("//response/field[@name = 'ApplicantAddress']");
		    if (node != null && !node.InnerText.Trim().Equals("")) 
		    {
		    	BuyerAddressLine1 = XMLEscape(node.InnerText.Trim());
		    	BuyersAddress = BuyerAddressLine1;
		    }
		
		    node = doc.SelectSingleNode("//response/field[@name = 'ApplicantAddress2']");
		    if (node != null && !node.InnerText.Trim().Equals("")) 
		    {
		    	BuyerAddressLine2 = XMLEscape(node.InnerText.Trim());
		    	BuyersAddress += " " + BuyerAddressLine2;
		    }
		
		    node = doc.SelectSingleNode("//response/field[@name = 'ApplicantCity']");
		    if (node != null && !node.InnerText.Trim().Equals("")) 
		    {
		    	BuyerCity = XMLEscape(node.InnerText.Trim());
		    	BuyersAddress += " " + BuyerCity;
		    }
		
		    node = doc.SelectSingleNode("//response/field[@name = 'ApplicantState']");
		    if (node != null && !node.InnerText.Trim().Equals(""))
		    {
		    	BuyerState = XMLEscape(node.InnerText.Trim());
		    	BuyersAddress += ", " + BuyerState;
		    }
		
		    node = doc.SelectSingleNode("//response/field[@name = 'ApplicantZip']");
		    if (node != null && !node.InnerText.Trim().Equals("")) 
		    {
		    	BuyerZip = XMLEscape(node.InnerText.Trim());
		    	BuyersAddress += " " + BuyerZip;
		    }
		    BuyersAddress = BuyersAddress.Trim();
		
		    node = doc.SelectSingleNode("//response/field[@name = 'ApplicantAttorneyName']");
		    if (node != null && !node.InnerText.Trim().Equals("")) ApplicantAttorneyName = XMLEscape(node.InnerText.Trim());
		
		    node = doc.SelectSingleNode("//response/field[@name = 'SellersAttorneyName']");
		    if (node != null && !node.InnerText.Trim().Equals("")) SellersAttorneyFirm = XMLEscape(node.InnerText.Trim());
		
		    node = doc.SelectSingleNode("//response/field[@name = 'SellersAttorneyAttentionTo']");
		    if (node != null && !node.InnerText.Trim().Equals("")) SellersAttorney = XMLEscape(node.InnerText.Trim());
		
		    node = doc.SelectSingleNode("//response/field[@name = 'SellersAttorneyAddress']");
		    if (node != null && !node.InnerText.Trim().Equals("")) SellersAttorneyAddress = XMLEscape(node.InnerText.Trim());
		
		    node = doc.SelectSingleNode("//response/field[@name = 'SellersAttorneyAddress2']");
		    if (node != null && !node.InnerText.Trim().Equals("")) SellersAttorneyAddress += " " + XMLEscape(node.InnerText.Trim());
		
		    node = doc.SelectSingleNode("//response/field[@name = 'SellersAttorneyCity']");
		    if (node != null && !node.InnerText.Trim().Equals("")) SellersAttorneyAddress += " " + XMLEscape(node.InnerText.Trim());
		
		    node = doc.SelectSingleNode("//response/field[@name = 'SellersAttorneyState']");
		    if (node != null && !node.InnerText.Trim().Equals("")) SellersAttorneyAddress += ", " + XMLEscape(node.InnerText.Trim());
		
		    node = doc.SelectSingleNode("//response/field[@name = 'SellersAttorneyZip']");
		    if (node != null && !node.InnerText.Trim().Equals("")) SellersAttorneyAddress += " " + XMLEscape(node.InnerText.Trim());
		    
		    if(Request["MailAddress"] == "MailAddress_BuyersAttorney")
		    {
			    node = doc.SelectSingleNode("//response/field[@name = 'BuyersAttorneyName']");
			    if (node != null && !node.InnerText.Equals("")) BuyerAttorneyNameFirm = XMLEscape(node.InnerText.Trim());
			
			    node = doc.SelectSingleNode("//response/field[@name = 'BuyersAttorneyAttentionTo']");
			    if (node != null && !node.InnerText.Equals("")) BuyerAttorneyName = XMLEscape(node.InnerText.Trim());
			
			    node = doc.SelectSingleNode("//response/field[@name = 'BuyersAttorneyAddress']");
			    if (node != null && !node.InnerText.Equals("")) BuyerAttorneyAddressLine1 = XMLEscape(node.InnerText.Trim());
			
			    node = doc.SelectSingleNode("//response/field[@name = 'BuyersAttorneyAddress2']");
			    if (node != null && !node.InnerText.Equals("")) BuyerAttorneyAddressLine2 = XMLEscape(node.InnerText.Trim());
			
			    node = doc.SelectSingleNode("//response/field[@name = 'BuyersAttorneyCity']");
			    if (node != null && !node.InnerText.Equals("")) BuyerAttorneyCity = XMLEscape(node.InnerText.Trim());
			
			    node = doc.SelectSingleNode("//response/field[@name = 'BuyersAttorneyState']");
			    if (node != null && !node.InnerText.Equals("")) BuyerAttorneyState = XMLEscape(node.InnerText.Trim());
			
			    node = doc.SelectSingleNode("//response/field[@name = 'BuyersAttorneyZip']");
			    if (node != null && !node.InnerText.Equals("")) BuyerAttorneyZip = XMLEscape(node.InnerText.Trim());
				}
				
		    node = doc.SelectSingleNode("//response/field[@name = 'PurchasePrice']");
		    if (node != null && !node.InnerText.Equals("")) PurchasePrice = XMLEscape(node.InnerText.Trim());
		    
		    if(SellersAddress.Equals(""))
		    {
			    GrantorAddressLine1 = XMLEscape(order.PropertyAddress);
			    GrantorAddressLine2 = XMLEscape(order.PropertyAddress2);
		    
			    GrantorCity = XMLEscape(order.PropertyCity.Trim());
			    GrantorState = XMLEscape(order.PropertyState.Trim());
			    GrantorZip = XMLEscape(order.PropertyZip.Trim());
			    GrantorCounty = XMLEscape(order.PropertyCounty.Trim());
			    
			    SellersAddress = GrantorAddressLine1 + ((!GrantorAddressLine2.Equals(""))? " " + GrantorAddressLine2 : "") + " " + GrantorCity + ", " + GrantorState + " " + GrantorZip;
			  }
		
		    County = order.PropertyCounty;
		    PIN = order.Pin;
		    AFFID = order.WorkingId;
		  }
			}
			
    	decimal stamptaxrate = 0;
    	decimal stamptaxper = 0;
    	decimal sales = 0;
	    
      decimal.TryParse(PurchasePrice.Trim().Replace(",", "").Replace("$", ""), out sales);
			if(GrantorCity.Trim().ToLower().Equals("chicago"))
			{
				stamptaxper = 1000;
				
				SellerTransferStamps = (((sales / stamptaxper) * ((decimal) 3)) * 100).ToString();
				
				int numint = (int) Math.Floor(((sales / stamptaxper) * ((decimal) 7.5)) * 100);
				BuyerTransferStamps = ((decimal) numint / (decimal) 100).ToString("C");
				
				numint = (int) Math.Floor((((sales / stamptaxper) * ((decimal) 3)) * 100));
				SellerTransferStamps = ((decimal) numint / (decimal) 100).ToString("C");
			}
			else
			{
				MySqlDataReader reader = this.phreezer.ExecuteReader("select * from `taxing_district` t where t.taxing_district = '" + GrantorCity + "'");
        if (reader.Read())
        {
       		string liability_party = reader["Liable_party"].ToString();
        	bool isSeller = (liability_party.ToLower().IndexOf("sell") > -1);
        	bool isBuyer = (liability_party.ToLower().IndexOf("buy") > -1);
        	string stamp_exempt = reader["Stamp_exempt"].ToString();
        	string amount = reader["Amount"].ToString().Replace(",", "").Replace("$", "");
        	string[] amountArray = amount.Split('/');
        	
        	decimal.TryParse(amountArray[0], out stamptaxrate);
        	if(amountArray.Length > 1) decimal.TryParse(amountArray[1], out stamptaxper);
        	decimal stamptax = 0;
        	
        	if(stamptaxper > 0)
        	{
	        	stamptax = (sales / stamptaxper) * stamptaxrate;
	        }
	        else if(amount.ToLower().IndexOf("trans") > -1)
	        {
	        	stamptax = stamptaxrate;
	        }
        	int numint = ((int) Math.Floor(stamptax * 100));
        	string stamptaxStr = ((decimal) numint / (decimal) 100).ToString("C");
	        	
        	if(isSeller && isBuyer)
        	{
        		BuyerTransferStamps = stamptaxStr;
        		SellerTransferStamps = stamptaxStr;
        	}
        	else if(isBuyer)
        	{
						BuyerTransferStamps = stamptaxStr;
        	}
        	else if(isSeller)
        	{
						SellerTransferStamps = stamptaxStr;
        	}
        }
				reader.Close();
			}	    	
			} catch (Exception err)
			{
				Response.Write(err.Message);
				Response.End();
			}
				
			if(!PurchasePrice.Equals(""))
			{
				decimal purchprice = 0;
				decimal.TryParse(PurchasePrice, out purchprice);
				
				if(purchprice <= 100000)
				{
			    tfitem.Load("1");
			    InsuranceRate = tfitem.Fee.ToString();
			    
			    tfitem.Load("95");
			    ClosingRate = tfitem.Fee.ToString();
				}
				else
				{
					for(int purchidx = 10; purchidx < 91; purchidx++)
					{
						if(purchprice > (purchidx * 10000) && purchprice <= ((purchidx + 1) * 10000))
						{
			        Affinity.TitleFees tf1 = new Affinity.TitleFees(this.phreezer);
			
			        Affinity.TitleFeesCriteria tfc = new Affinity.TitleFeesCriteria();
			      	tfc.FeeType = "Insurance Rate";
			      	tfc.Name = "$" + purchidx.ToString() + "0,001 to $" + (purchidx + 1).ToString() + "0,000";
			        tfc.AppendToOrderBy("Id");
			        tf1.Query(tfc);
			
			        IEnumerator tfi = tf1.GetEnumerator();
			
			        // loop through the checkboxes and insert or delete as needed
			        while (tfi.MoveNext())
			        {
			            Affinity.TitleFee tfitem1 = (Affinity.TitleFee) tfi.Current;
									InsuranceRate = tfitem1.Fee.ToString();
			        }
			      }
					}
					
					if(purchprice <= 150000)
					{
						tfitem.Load("96");
			    	ClosingRate = tfitem.Fee.ToString();
					}
					else if(purchprice <= 200000)
					{
						tfitem.Load("97");
			    	ClosingRate = tfitem.Fee.ToString();
					}
					else if(purchprice <= 250000)
					{
						tfitem.Load("98");
			    	ClosingRate = tfitem.Fee.ToString();
					}
					else if(purchprice <= 300000)
					{
						tfitem.Load("99");
			    	ClosingRate = tfitem.Fee.ToString();
					}
					else if(purchprice <= 400000)
					{
						tfitem.Load("100");
			    	ClosingRate = tfitem.Fee.ToString();
					}
					else if(purchprice <= 500000)
					{
						tfitem.Load("101");
			    	ClosingRate = tfitem.Fee.ToString();
					}
					else
					{
						tfitem.Load("101");
						decimal purchleft = purchprice - 500000;
						decimal div = purchleft / 50000;
						int mult = (int) Math.Round (div);
						ClosingRate = (tfitem.Fee + ((mult + 1) * 50)).ToString();
					}
				}
			}
			
			SellersNames = SellersNameArray[0];
			for(int sellerIdx = 1; sellerIdx < SellersNameArray.Length; sellerIdx++)
			{
				if(!SellersNameArray[sellerIdx].Equals(""))
				{
					SellersNames += " and " + SellersNameArray[sellerIdx];
					
					if(sellerIdx > 1) morethantwoSellers = true;
				}
			}
			
			BuyersNames = BuyersNameArray[0];
			for(int buyerIdx = 1; buyerIdx < BuyersNameArray.Length; buyerIdx++)
			{
				if(!BuyersNameArray[buyerIdx].Equals(""))
				{
					if(!BuyersNames.Trim().Equals(""))
					{
						BuyersNames += " and ";
					}
					BuyersNames += BuyersNameArray[buyerIdx];
				}
			}

			if(Grantor2.Equals(""))
			{
				contents = contents.Replace("<w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:b/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:b/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type grantor's name]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:t>(SEAL)</w:t></w:r>", "").Replace("<w:tcBorders><w:bottom w:val=\"single\" w:sz=\"4\" w:space=\"0\" w:xxxx=\"xx\" w:color=\"auto\"/></w:tcBorders>", "").Replace("<w:p w:rsidR=\"00CC6072\" w:rsidRDefault=\"00CC6072\"><w:pPr><w:spacing w:line=\"240\" w:lineRule=\"atLeast\"/><w:ind w:left=\"5040\"/><w:jc w:val=\"both\"/><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr></w:pPr></w:p><w:p w:rsidR=\"00CC6072\" w:rsidRDefault=\"00CC6072\"><w:pPr><w:spacing w:line=\"240\" w:lineRule=\"atLeast\"/><w:ind w:left=\"5040\"/><w:jc w:val=\"both\"/><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr></w:pPr><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:t>_________________________________(SEAL)</w:t></w:r></w:p><w:p w:rsidR=\"00CC6072\" w:rsidRDefault=\"00CC6072\"><w:pPr><w:spacing w:line=\"240\" w:lineRule=\"atLeast\"/><w:ind w:left=\"5040\"/><w:jc w:val=\"both\"/><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr></w:pPr><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:t>As Trustee as aforesaid</w:t></w:r></w:p><w:p w:rsidR=\"00CC6072\" w:rsidRDefault=\"00CC6072\"><w:pPr><w:spacing w:line=\"240\" w:lineRule=\"atLeast\"/><w:ind w:left=\"5040\"/><w:jc w:val=\"both\"/><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr></w:pPr></w:p><w:p w:rsidR=\"00CC6072\" w:rsidRDefault=\"00CC6072\"><w:pPr><w:spacing w:line=\"240\" w:lineRule=\"atLeast\"/><w:ind w:left=\"5040\"/><w:jc w:val=\"both\"/><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr></w:pPr><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"begin\"/> </w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type name of trustee ]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/></w:r></w:p>", "").Replace("<w:tc><w:tcPr><w:tcW w:w=\"4105\" w:type=\"dxa\"/><w:tcBorders><w:bottom w:val=\"single\" w:sz=\"4\" w:space=\"0\" w:color=\"auto\"/></w:tcBorders></w:tcPr><w:p w:rsidR=\"00CA06F6\" w:rsidRDefault=\"00925D7E\"><w:pPr><w:spacing w:line=\"240\" w:lineRule=\"atLeast\"/><w:jc w:val=\"both\"/><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:szCs w:val=\"20\"/></w:rPr></w:pPr></w:p></w:tc><w:tc><w:tcPr><w:tcW w:w=\"900\" w:type=\"dxa\"/><w:vAlign w:val=\"bottom\"/></w:tcPr><w:p w:rsidR=\"00CA06F6\" w:rsidRDefault=\"00925D7E\"><w:pPr><w:spacing w:line=\"240\" w:lineRule=\"atLeast\"/><w:jc w:val=\"both\"/><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:szCs w:val=\"20\"/></w:rPr></w:pPr><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:szCs w:val=\"20\"/></w:rPr><w:t>(SEAL)</w:t></w:r></w:p></w:tc>", "").Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type name of trustee]  \" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "");
			}
			if(!morethantwoSellers)
			{
				contents = contents.Replace("<w:tr w:rsidR=\"00CA06F6\"><w:trPr><w:trHeight w:val=\"402\"/></w:trPr><w:tc><w:tcPr><w:tcW w:w=\"4105\" w:type=\"dxa\"/><w:tcBorders><w:bottom w:val=\"single\" w:sz=\"4\" w:space=\"0\" w:color=\"auto\"/></w:tcBorders></w:tcPr><w:p w:rsidR=\"00CA06F6\" w:rsidRDefault=\"00CA06F6\"><w:pPr><w:spacing w:line=\"240\" w:lineRule=\"atLeast\"/><w:jc w:val=\"both\"/><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr></w:pPr></w:p></w:tc><w:tc><w:tcPr><w:tcW w:w=\"900\" w:type=\"dxa\"/><w:vAlign w:val=\"bottom\"/></w:tcPr><w:p w:rsidR=\"00CA06F6\" w:rsidRDefault=\"00CA06F6\"><w:pPr><w:spacing w:line=\"240\" w:lineRule=\"atLeast\"/><w:jc w:val=\"both\"/><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr></w:pPr><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:t>(SEAL)</w:t></w:r></w:p></w:tc><w:tc><w:tcPr><w:tcW w:w=\"630\" w:type=\"dxa\"/></w:tcPr><w:p w:rsidR=\"00CA06F6\" w:rsidRDefault=\"00CA06F6\"><w:pPr><w:spacing w:line=\"240\" w:lineRule=\"atLeast\"/><w:jc w:val=\"both\"/><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr></w:pPr></w:p></w:tc><w:tc><w:tcPr><w:tcW w:w=\"3690\" w:type=\"dxa\"/><w:tcBorders><w:bottom w:val=\"single\" w:sz=\"4\" w:space=\"0\" w:color=\"auto\"/></w:tcBorders></w:tcPr><w:p w:rsidR=\"00CA06F6\" w:rsidRDefault=\"00CA06F6\"><w:pPr><w:spacing w:line=\"240\" w:lineRule=\"atLeast\"/><w:jc w:val=\"both\"/><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr></w:pPr></w:p></w:tc><w:tc><w:tcPr><w:tcW w:w=\"990\" w:type=\"dxa\"/><w:vAlign w:val=\"bottom\"/></w:tcPr><w:p w:rsidR=\"00CA06F6\" w:rsidRDefault=\"00CA06F6\"><w:pPr><w:spacing w:line=\"240\" w:lineRule=\"atLeast\"/><w:jc w:val=\"both\"/><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr></w:pPr><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:t>(SEAL)</w:t></w:r></w:p></w:tc></w:tr>", "");
			}
				
			contents = contents.Replace("[COUNTY]", County).Replace("[GRANTOR]", Grantor).Replace("[GRANTOR2]", Grantor2).Replace("[GRANTOR2a]", Grantor2.Replace(" and ", "")).Replace("[GRANTORCITY]", GrantorCity).Replace("[GRANTORSTATE]", GrantorState).Replace("[GRANTORZIP]", GrantorZip).Replace("[GRANTORCOUNTY]", GrantorCounty).Replace("[GRANTORADDRESSLINE1]", GrantorAddressLine1).Replace("[GRANTORADDRESSLINE2]", GrantorAddressLine2).Replace("[GRANTEE]", Grantee).Replace("[GRANTEE2]", Grantee2).Replace("[DEEDDATE]", DeedDate.ToString("MM/dd/yyyy")).Replace("[DEEDMONTH]", DeedDate.ToString("MMMM")).Replace("[DEEDYEAR]", DeedDate.ToString("yyyy")).Replace("[DEEDDAY]", DeedDateOrdinal).Replace("[CONTRACTDATE]", "").Replace("[BUYERSNAME]", BuyersNameArray[0]).Replace("[BUYERSADDRESS]", BuyersAddress).Replace("[SELLERSNAME]", SellersNameArray[0]).Replace("[SELLERSNAME2]", SellersNameArray[1]).Replace("[SELLERSADDRESS]", SellersAddress).Replace("[COMMITMENTDATE]", CommittmentDate).Replace("[DATEDAY]", DateDay).Replace("[DATEMONTH]", DateMonth).Replace("[DATEYEAR]", DateYear).Replace("[PIN]", PIN).Replace("[APPLICANTATTORNEYNAME]", ApplicantAttorneyName).Replace("[SELLERSATTORNEY]", SellersAttorney).Replace("[SELLERSATTORNEYFIRM]", SellersAttorneyFirm).Replace("[SELLERSATTORNEYADDRESS]", SellersAttorneyAddress).Replace("[TITLEAGENT]", order.Account.FullName);
			
			if(!BuyersNames.Equals(""))
			{
		    	contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:i/><w:iCs/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type tax bill name]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:i/><w:iCs/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + BuyersNames.Trim() + "</w:t>");
		  }
		  
			if(!GrantorAddressLine1.Equals(""))
			{
				contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type tax bill mail address line 1]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + GrantorAddressLine1.Trim() + "</w:t>");
				
				if(!GrantorAddressLine2.Equals(""))
				{
					contents = contents.Replace("<w:p w:rsidR=\"006E0DD7\" w:rsidRDefault=\"006E0DD7\"><w:pPr><w:spacing w:line=\"240\" w:lineRule=\"atLeast\"/><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:color w:val=\"000000\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr></w:pPr><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"016\"/></w:rPr><w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type tax bill mail address line 2]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/></w:r></w:p>", "<w:p w:rsidR=\"006E0DD7\" w:rsidRDefault=\"006E0DD7\"><w:pPr><w:spacing w:line=\"240\" w:lineRule=\"atLeast\"/><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:color w:val=\"000000\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr></w:pPr><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"016\"/></w:rPr><w:t xml:space=\"preserve\">" + GrantorAddressLine2.Trim() + "</w:t></w:r></w:p>");
				}
				else
				{
					contents = contents.Replace("<w:p w:rsidR=\"006E0DD7\" w:rsidRDefault=\"006E0DD7\"><w:pPr><w:spacing w:line=\"240\" w:lineRule=\"atLeast\"/><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:color w:val=\"000000\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr></w:pPr><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type tax bill mail address line 2]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/></w:r></w:p>", "");
				}
			}
			
			if(!GrantorCity.Equals("") || !GrantorState.Equals("") || !GrantorZip.Equals(""))
			{
				contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type tax bill mail city, state zip]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + (GrantorCity + ", " + GrantorState + " " + GrantorZip).Trim() + "</w:t>");
			}
			
			if(Request["MailAddress"] == "MailAddress_BuyersPropertyAddress")
			{
		    if(!BuyersNames.Trim().Equals(""))
		    {
		    	contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:i/><w:iCs/><w:spacing w:val=\"-2\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type mail to name]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:i/><w:iCs/><w:spacing w:val=\"-2\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + BuyersNames.Trim() + "</w:t>");
		    }
				if(!GrantorAddressLine1.Trim().Equals(""))
				{
		    	contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type mail to address line 1]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + GrantorAddressLine1.Trim() + "</w:t>");
		    	
		    	if(!GrantorAddressLine2.Equals("")) 
					{
				    	contents = contents.Replace("<w:p w:rsidR=\"006E0DD7\" w:rsidRDefault=\"006E0DD7\"><w:pPr><w:tabs><w:tab w:val=\"left\" w:pos=\"-1230\"/><w:tab w:val=\"left\" w:pos=\"-720\"/><w:tab w:val=\"left\" w:pos=\"0\"/><w:tab w:val=\"left\" w:pos=\"336\"/><w:tab w:val=\"left\" w:pos=\"643\"/><w:tab w:val=\"left\" w:pos=\"1367\"/><w:tab w:val=\"left\" w:pos=\"1689\"/><w:tab w:val=\"left\" w:pos=\"3779\"/><w:tab w:val=\"left\" w:pos=\"3859\"/><w:tab w:val=\"left\" w:pos=\"4744\"/><w:tab w:val=\"left\" w:pos=\"6030\"/><w:tab w:val=\"left\" w:pos=\"7200\"/><w:tab w:val=\"left\" w:pos=\"7920\"/><w:tab w:val=\"left\" w:pos=\"8640\"/><w:tab w:val=\"left\" w:pos=\"9682\"/><w:tab w:val=\"left\" w:pos=\"10248\"/><w:tab w:val=\"left\" w:pos=\"10800\"/><w:tab w:val=\"left\" w:pos=\"11520\"/><w:tab w:val=\"left\" w:pos=\"12240\"/><w:tab w:val=\"left\" w:pos=\"12960\"/><w:tab w:val=\"left\" w:pos=\"13680\"/><w:tab w:val=\"left\" w:pos=\"14400\"/><w:tab w:val=\"left\" w:pos=\"15120\"/><w:tab w:val=\"left\" w:pos=\"15840\"/><w:tab w:val=\"left\" w:pos=\"16560\"/><w:tab w:val=\"left\" w:pos=\"17280\"/><w:tab w:val=\"left\" w:pos=\"18000\"/><w:tab w:val=\"left\" w:pos=\"18720\"/></w:tabs><w:suppressAutoHyphens/><w:spacing w:line=\"240\" w:lineRule=\"atLeast\"/><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:szCs w:val=\"20\"/></w:rPr></w:pPr><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:szCs w:val=\"20\"/></w:rPr><w:t xml:space=\"preserve\"> </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type mail to address line 2]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/></w:r></w:p>", "<w:p w:rsidR=\"006E0DD7\" w:rsidRDefault=\"006E0DD7\"><w:pPr><w:tabs><w:tab w:val=\"left\" w:pos=\"-1230\"/><w:tab w:val=\"left\" w:pos=\"-720\"/><w:tab w:val=\"left\" w:pos=\"0\"/><w:tab w:val=\"left\" w:pos=\"336\"/><w:tab w:val=\"left\" w:pos=\"643\"/><w:tab w:val=\"left\" w:pos=\"1367\"/><w:tab w:val=\"left\" w:pos=\"1689\"/><w:tab w:val=\"left\" w:pos=\"3779\"/><w:tab w:val=\"left\" w:pos=\"3859\"/><w:tab w:val=\"left\" w:pos=\"4744\"/><w:tab w:val=\"left\" w:pos=\"6030\"/><w:tab w:val=\"left\" w:pos=\"7200\"/><w:tab w:val=\"left\" w:pos=\"7920\"/><w:tab w:val=\"left\" w:pos=\"8640\"/><w:tab w:val=\"left\" w:pos=\"9682\"/><w:tab w:val=\"left\" w:pos=\"10248\"/><w:tab w:val=\"left\" w:pos=\"10800\"/><w:tab w:val=\"left\" w:pos=\"11520\"/><w:tab w:val=\"left\" w:pos=\"12240\"/><w:tab w:val=\"left\" w:pos=\"12960\"/><w:tab w:val=\"left\" w:pos=\"13680\"/><w:tab w:val=\"left\" w:pos=\"14400\"/><w:tab w:val=\"left\" w:pos=\"15120\"/><w:tab w:val=\"left\" w:pos=\"15840\"/><w:tab w:val=\"left\" w:pos=\"16560\"/><w:tab w:val=\"left\" w:pos=\"17280\"/><w:tab w:val=\"left\" w:pos=\"18000\"/><w:tab w:val=\"left\" w:pos=\"18720\"/></w:tabs><w:suppressAutoHyphens/><w:spacing w:line=\"240\" w:lineRule=\"atLeast\"/><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:szCs w:val=\"20\"/></w:rPr></w:pPr><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:szCs w:val=\"20\"/></w:rPr><w:t xml:space=\"preserve\"> </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:t xml:space=\"preserve\">" + GrantorAddressLine2.Trim() + "</w:t></w:r></w:p>");
					}
					else
					{
				    	contents = contents.Replace("<w:tr w:rsidR=\"006E0DD7\"><w:trPr><w:trHeight w:val=\"260\"/></w:trPr><w:tc><w:tcPr><w:tcW w:w=\"3646\" w:type=\"dxa\"/><w:vAlign w:val=\"center\"/></w:tcPr><w:p w:rsidR=\"006E0DD7\" w:rsidRDefault=\"006E0DD7\"><w:pPr><w:tabs><w:tab w:val=\"left\" w:pos=\"-1230\"/><w:tab w:val=\"left\" w:pos=\"-720\"/><w:tab w:val=\"left\" w:pos=\"0\"/><w:tab w:val=\"left\" w:pos=\"336\"/><w:tab w:val=\"left\" w:pos=\"643\"/><w:tab w:val=\"left\" w:pos=\"1367\"/><w:tab w:val=\"left\" w:pos=\"1689\"/><w:tab w:val=\"left\" w:pos=\"3779\"/><w:tab w:val=\"left\" w:pos=\"3859\"/><w:tab w:val=\"left\" w:pos=\"4744\"/><w:tab w:val=\"left\" w:pos=\"6030\"/><w:tab w:val=\"left\" w:pos=\"7200\"/><w:tab w:val=\"left\" w:pos=\"7920\"/><w:tab w:val=\"left\" w:pos=\"8640\"/><w:tab w:val=\"left\" w:pos=\"9682\"/><w:tab w:val=\"left\" w:pos=\"10248\"/><w:tab w:val=\"left\" w:pos=\"10800\"/><w:tab w:val=\"left\" w:pos=\"11520\"/><w:tab w:val=\"left\" w:pos=\"12240\"/><w:tab w:val=\"left\" w:pos=\"12960\"/><w:tab w:val=\"left\" w:pos=\"13680\"/><w:tab w:val=\"left\" w:pos=\"14400\"/><w:tab w:val=\"left\" w:pos=\"15120\"/><w:tab w:val=\"left\" w:pos=\"15840\"/><w:tab w:val=\"left\" w:pos=\"16560\"/><w:tab w:val=\"left\" w:pos=\"17280\"/><w:tab w:val=\"left\" w:pos=\"18000\"/><w:tab w:val=\"left\" w:pos=\"18720\"/></w:tabs><w:suppressAutoHyphens/><w:spacing w:line=\"240\" w:lineRule=\"atLeast\"/><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:szCs w:val=\"20\"/></w:rPr></w:pPr><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:szCs w:val=\"20\"/></w:rPr><w:t xml:space=\"preserve\"> </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type mail to address line 2]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/></w:r></w:p></w:tc></w:tr>", "");
					}
				}
				if(!GrantorCity.Trim().Equals("") || !GrantorState.Trim().Equals("") || !GrantorZip.Trim().Equals("")) 
				{
		    	contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type mail to city, state zip]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + (GrantorCity + ", " + GrantorState + " " + GrantorZip).Trim() + "</w:t>");
				}
			}
			else if(Request["MailAddress"] == "MailAddress_BuyersAttorney")
			{
		    if(!BuyerAttorneyNameFirm.Trim().Equals(""))
		    {
		    	contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:i/><w:iCs/><w:spacing w:val=\"-2\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type mail to name]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:i/><w:iCs/><w:spacing w:val=\"-2\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + BuyerAttorneyNameFirm.Trim() + "</w:t>");
		    }
				if(!BuyerAttorneyAddressLine1.Trim().Equals(""))
				{
		    	contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type mail to address line 1]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + BuyerAttorneyAddressLine1.Trim() + "</w:t>");
		    	
		    	if(!BuyerAttorneyAddressLine2.Equals("")) 
					{
				    	contents = contents.Replace("<w:p w:rsidR=\"006E0DD7\" w:rsidRDefault=\"006E0DD7\"><w:pPr><w:tabs><w:tab w:val=\"left\" w:pos=\"-1230\"/><w:tab w:val=\"left\" w:pos=\"-720\"/><w:tab w:val=\"left\" w:pos=\"0\"/><w:tab w:val=\"left\" w:pos=\"336\"/><w:tab w:val=\"left\" w:pos=\"643\"/><w:tab w:val=\"left\" w:pos=\"1367\"/><w:tab w:val=\"left\" w:pos=\"1689\"/><w:tab w:val=\"left\" w:pos=\"3779\"/><w:tab w:val=\"left\" w:pos=\"3859\"/><w:tab w:val=\"left\" w:pos=\"4744\"/><w:tab w:val=\"left\" w:pos=\"6030\"/><w:tab w:val=\"left\" w:pos=\"7200\"/><w:tab w:val=\"left\" w:pos=\"7920\"/><w:tab w:val=\"left\" w:pos=\"8640\"/><w:tab w:val=\"left\" w:pos=\"9682\"/><w:tab w:val=\"left\" w:pos=\"10248\"/><w:tab w:val=\"left\" w:pos=\"10800\"/><w:tab w:val=\"left\" w:pos=\"11520\"/><w:tab w:val=\"left\" w:pos=\"12240\"/><w:tab w:val=\"left\" w:pos=\"12960\"/><w:tab w:val=\"left\" w:pos=\"13680\"/><w:tab w:val=\"left\" w:pos=\"14400\"/><w:tab w:val=\"left\" w:pos=\"15120\"/><w:tab w:val=\"left\" w:pos=\"15840\"/><w:tab w:val=\"left\" w:pos=\"16560\"/><w:tab w:val=\"left\" w:pos=\"17280\"/><w:tab w:val=\"left\" w:pos=\"18000\"/><w:tab w:val=\"left\" w:pos=\"18720\"/></w:tabs><w:suppressAutoHyphens/><w:spacing w:line=\"240\" w:lineRule=\"atLeast\"/><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:szCs w:val=\"20\"/></w:rPr></w:pPr><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:szCs w:val=\"20\"/></w:rPr><w:t xml:space=\"preserve\"> </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type mail to address line 2]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/></w:r></w:p>", "<w:p w:rsidR=\"006E0DD7\" w:rsidRDefault=\"006E0DD7\"><w:pPr><w:tabs><w:tab w:val=\"left\" w:pos=\"-1230\"/><w:tab w:val=\"left\" w:pos=\"-720\"/><w:tab w:val=\"left\" w:pos=\"0\"/><w:tab w:val=\"left\" w:pos=\"336\"/><w:tab w:val=\"left\" w:pos=\"643\"/><w:tab w:val=\"left\" w:pos=\"1367\"/><w:tab w:val=\"left\" w:pos=\"1689\"/><w:tab w:val=\"left\" w:pos=\"3779\"/><w:tab w:val=\"left\" w:pos=\"3859\"/><w:tab w:val=\"left\" w:pos=\"4744\"/><w:tab w:val=\"left\" w:pos=\"6030\"/><w:tab w:val=\"left\" w:pos=\"7200\"/><w:tab w:val=\"left\" w:pos=\"7920\"/><w:tab w:val=\"left\" w:pos=\"8640\"/><w:tab w:val=\"left\" w:pos=\"9682\"/><w:tab w:val=\"left\" w:pos=\"10248\"/><w:tab w:val=\"left\" w:pos=\"10800\"/><w:tab w:val=\"left\" w:pos=\"11520\"/><w:tab w:val=\"left\" w:pos=\"12240\"/><w:tab w:val=\"left\" w:pos=\"12960\"/><w:tab w:val=\"left\" w:pos=\"13680\"/><w:tab w:val=\"left\" w:pos=\"14400\"/><w:tab w:val=\"left\" w:pos=\"15120\"/><w:tab w:val=\"left\" w:pos=\"15840\"/><w:tab w:val=\"left\" w:pos=\"16560\"/><w:tab w:val=\"left\" w:pos=\"17280\"/><w:tab w:val=\"left\" w:pos=\"18000\"/><w:tab w:val=\"left\" w:pos=\"18720\"/></w:tabs><w:suppressAutoHyphens/><w:spacing w:line=\"240\" w:lineRule=\"atLeast\"/><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:szCs w:val=\"20\"/></w:rPr></w:pPr><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:szCs w:val=\"20\"/></w:rPr><w:t xml:space=\"preserve\"> </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:t xml:space=\"preserve\">" + BuyerAttorneyAddressLine2.Trim() + "</w:t></w:r></w:p>");
					}
					else
					{
				    	contents = contents.Replace("<w:tr w:rsidR=\"006E0DD7\"><w:trPr><w:trHeight w:val=\"260\"/></w:trPr><w:tc><w:tcPr><w:tcW w:w=\"3646\" w:type=\"dxa\"/><w:vAlign w:val=\"center\"/></w:tcPr><w:p w:rsidR=\"006E0DD7\" w:rsidRDefault=\"006E0DD7\"><w:pPr><w:tabs><w:tab w:val=\"left\" w:pos=\"-1230\"/><w:tab w:val=\"left\" w:pos=\"-720\"/><w:tab w:val=\"left\" w:pos=\"0\"/><w:tab w:val=\"left\" w:pos=\"336\"/><w:tab w:val=\"left\" w:pos=\"643\"/><w:tab w:val=\"left\" w:pos=\"1367\"/><w:tab w:val=\"left\" w:pos=\"1689\"/><w:tab w:val=\"left\" w:pos=\"3779\"/><w:tab w:val=\"left\" w:pos=\"3859\"/><w:tab w:val=\"left\" w:pos=\"4744\"/><w:tab w:val=\"left\" w:pos=\"6030\"/><w:tab w:val=\"left\" w:pos=\"7200\"/><w:tab w:val=\"left\" w:pos=\"7920\"/><w:tab w:val=\"left\" w:pos=\"8640\"/><w:tab w:val=\"left\" w:pos=\"9682\"/><w:tab w:val=\"left\" w:pos=\"10248\"/><w:tab w:val=\"left\" w:pos=\"10800\"/><w:tab w:val=\"left\" w:pos=\"11520\"/><w:tab w:val=\"left\" w:pos=\"12240\"/><w:tab w:val=\"left\" w:pos=\"12960\"/><w:tab w:val=\"left\" w:pos=\"13680\"/><w:tab w:val=\"left\" w:pos=\"14400\"/><w:tab w:val=\"left\" w:pos=\"15120\"/><w:tab w:val=\"left\" w:pos=\"15840\"/><w:tab w:val=\"left\" w:pos=\"16560\"/><w:tab w:val=\"left\" w:pos=\"17280\"/><w:tab w:val=\"left\" w:pos=\"18000\"/><w:tab w:val=\"left\" w:pos=\"18720\"/></w:tabs><w:suppressAutoHyphens/><w:spacing w:line=\"240\" w:lineRule=\"atLeast\"/><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:szCs w:val=\"20\"/></w:rPr></w:pPr><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:szCs w:val=\"20\"/></w:rPr><w:t xml:space=\"preserve\"> </w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type mail to address line 2]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/></w:r></w:p></w:tc></w:tr>", "");
					}
				}
				if(!BuyerAttorneyCity.Trim().Equals("") || !BuyerAttorneyState.Trim().Equals("") || !BuyerAttorneyZip.Trim().Equals("")) 
				{
		    	contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type mail to city, state zip]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"16\"/><w:szCs w:val=\"16\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + (BuyerAttorneyCity + ", " + BuyerAttorneyState + " " + BuyerAttorneyZip).Trim() + "</w:t>");
				}
			}
			
			// FULL REPLACEMENT SECTION
			
			if(!BuyersNames.Equals(""))
			{
				contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type name(s) of grantee(s)]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + BuyersNames.Trim() + "</w:t>").Replace("[BUYERSNAMES]", BuyersNames.Trim());
			}
			
			if(!Grantor.Equals(""))
			{
				contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type name of deceased]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + Grantor + "</w:t>");
			}
			
			if(!County.Equals(""))
			{
				contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type property's county]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + County + "</w:t>");
				contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type county]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + County + "</w:t>");
				contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type grantor’s County]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + County + "</w:t>");
				//contents = contents.Replace("w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type notary County]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + County + "</w:t>");
			}
			
			if(!PIN.Equals(""))
			{
				contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type PIN]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\"> " + PIN + " </w:t>");
			}
			
			if(!SellersAddress.Equals(""))
			{
				contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Times New Roman\" w:hAnsi=\"Times New Roman\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type property address]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Times New Roman\" w:hAnsi=\"Times New Roman\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + SellersAddress + "</w:t>");
			}
			
			if(!Grantor.Equals(""))
			{
				contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type grantors' names]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + SellersNames + "</w:t>");
				contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type name of trustee(s)]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + SellersNames + "</w:t>");
				contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type name of trustee]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + Grantor + "</w:t>");
				contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type grantor's name]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + SellersNames + "</w:t>");
				contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/> </w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type name of trustee ]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + Grantor2.Replace(" and ", "") + "</w:t>").Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type name of trustee]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + Grantor + "</w:t>").Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type name of trustee] \" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + Grantor2 + "</w:t>");
			}
			
			if(!Grantor2.Equals(""))
			{
				contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type name of trustee]  \" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + Grantor2 + "</w:t>");
			}
			
			if(!GrantorCity.Equals(""))
			{
				contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type name of town]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + GrantorCity + "</w:t>");
				contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type grantors' town]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:spacing w:val=\"-2\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + GrantorCity + "</w:t>");
				contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type grantor's address line 2]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + GrantorCity + ", " + GrantorState + " " + GrantorZip + "</w:t>");
			}
			
			if(!GrantorAddressLine1.Equals(""))
			{
				contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type grantor's address line 1]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + GrantorAddressLine1 + "</w:t>");
			}
			
			// PARTIAL REPLACEMENT SECTION
			contents = contents.Replace("[AFFID]", AFFID);
			
			// FEE REPLACEMENTS
			contents = contents.Replace("[WIRETRANSFERFEE]", WireTransferFee);
			contents = contents.Replace("[POLICYUPDATEFEE]", PolicyUpdateFee);
			contents = contents.Replace("[COMMITMENTLATERDDATE]", CommitmentLaterDate);
			contents = contents.Replace("[MORTGAGEPOLICY]", MortgagePolicy);
			contents = contents.Replace("[ABSTRACTFEE]", AbstractFee);
			contents = contents.Replace("[RECORDINGFEE]", RecordingFee);
			contents = contents.Replace("[EMAILFEE]", EmailFee);
			contents = contents.Replace("[ENDORSEMENTFEE]", EndorsementFee);
			contents = contents.Replace("[INSURANCERATE]", InsuranceRate);
			contents = contents.Replace("[CLOSINGRATE]", ClosingRate);
			contents = contents.Replace("[BUYERTRANSFERSTAMPS]", BuyerTransferStamps);
			contents = contents.Replace("[SELLERTRANSFERSTAMPS]", SellerTransferStamps);
			
			if(!County.Equals(""))
			{
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesInDoc \"[Click and type in county]\" </w:instrText>", "");
			}
			
			if(!Grantee.Equals(""))
			{
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type grantee's name]\" </w:instrText>", "").Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type grantees' names]\" </w:instrText>", "").Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type grantee's name]\" </w:instrText>", "");
			}
			
			if(!Grantor.Equals(""))
			{
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type grantor's name]\" </w:instrText>", "").Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type grantors' names]\" </w:instrText>", "");
			}
			
			if(!GrantorAddressLine1.Equals(""))
			{
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type grantor's address line 1]\" </w:instrText>", "");
				
				// GrantorAddressLine1 + " " + GrantorAddressLine2 + " " + GrantorCity + ", " + GrantorState + " " + GrantorZip;
				if(false)
				{
					
				}
			}
			
			if(!GrantorAddressLine1.Equals("") || !GrantorAddressLine2.Equals(""))
			{
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type grantor's address line 2]\" </w:instrText>", "");
			}
			
			if(!GrantorCounty.Equals(""))
			{
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type grantors’s County]\" </w:instrText>", "").Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type county]\" </w:instrText>", "").Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesInDoc \"[Click and type in County}\" </w:instrText>", "");
			}
			
			contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type date of deed]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + DeedDateOrdinal + "</w:t>");
			contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type month of deed]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + DeedDate.ToString("MMMM") + " </w:t>");
			contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type year of deed]\" </w:instrText></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Baskerville Old Face\" w:hAnsi=\"Baskerville Old Face\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t xml:space=\"preserve\">" + DeedDate.ToString("yyyy") + "</w:t>");
			
			if(!SellersNames.Equals(""))
			{
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesInDoc \"[Click and type sellers' names]\" </w:instrText>", "");
			}
			
			if(!BuyersNames.Equals(""))
			{
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesInDoc \"[Click and type buyers' name]\" </w:instrText>", "");
			}
			
			if(!BuyersAddress.Equals(""))
			{
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesInDoc \"[Click and type buyers' address]\" </w:instrText>", "");
			}
			
			if(!SellersAddress.Equals(""))
			{
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesInDoc \"[Click and type sellers' address]\" </w:instrText>", "").Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesInDoc \"[Click and type property address]\" </w:instrText>", "").Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type grantee's address]\" </w:instrText>", "").Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type property address]\" </w:instrText>", "").Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type grantees' address]\" </w:instrText>", "");
			}
			
			if(!PIN.Equals(""))
			{
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type PIN]\" </w:instrText>", "");
			}
			
			if(!SellersAttorney.Trim().Equals(""))
			{
				contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type Attorney's Name]\" </w:instrText></w:r><w:r><w:rPr><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t>" + SellersAttorney + "</w:t>");
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type Attorney's Name]\" </w:instrText>", "");
			}
			
			if(!SellersAttorneyAddress.Trim().Equals(""))
			{
				contents = contents.Replace("<w:fldChar w:fldCharType=\"begin\"/></w:r><w:r><w:rPr><w:szCs w:val=\"20\"/></w:rPr><w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type Attorney's Address]\" </w:instrText></w:r><w:r><w:rPr><w:szCs w:val=\"20\"/></w:rPr><w:fldChar w:fldCharType=\"end\"/>", "<w:t>" + SellersAttorneyAddress + "</w:t>");
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type Attorney's Address]\" </w:instrText>", "");
			}
			
			contents = contents.Replace("[SELLERSNAMES]", SellersNames);
			
			contents = contents.Replace(", ,", "");
			contents = contents.Replace("    ,", ",");
			contents = contents.Replace(", ,", "");
			contents = contents.Replace(">, <", "><");
			contents = contents.Replace("[BUYERSNAMES]", "");
			

      /*
      string fileNameXML = HttpContext.Current.Server.MapPath(".") + "\\downloads\\Disclosure_Agent.doc";
	    string FileName = HttpContext.Current.Server.MapPath(".") + "\\downloads\\tested.xml";

			 Document dc = new Document();
	            dc.LoadFromFile(fileNameXML, FileFormat.Doc);
	            dc.SaveToFile(FileName, FileFormat.Xml);
	            //dc.SaveToStream(HttpContext.Current.Response.OutputStream, FileFormat.Xml);
	     */  
			
			int len = contents.Length+50;
			//HttpContext.Current.Response.ContentType = "application/ms-word";
			HttpContext.Current.Response.ContentType = "text/xml";
			HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=ATS.doc");
			HttpContext.Current.Response.AppendHeader("Content-Length", len.ToString());
			
	    HttpContext.Current.Response.Write(contents + "                                                  ");
			HttpContext.Current.Response.Flush();
			HttpContext.Current.Response.End();
		}

   }
}

}
