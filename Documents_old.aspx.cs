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
using Spire.Doc;

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

		int filecount = 0;
		this.Master.SetLayout("Documents", MasterPage.LayoutStyle.ContentOnly);
		if(Request["file"] != null)
		{
			string [] file = Request["file"].Split(',');
			string [] MailAddress = "".Split(',');
			
			if(Request["MailAddress"] != null)
			{
				MailAddress = Request["MailAddress"].Split(',');
			}
			
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
						
			for(int i = 0; i < file.Length; i++)
			{
				if((file[i].Equals("WD") || file[i].Equals("WD_S") || file[i].Equals("WD_JT") || file[i].Equals("WD_TE")) && Request["warrantyfile"] == null) continue;
				string path = HttpContext.Current.Server.MapPath(".") + "\\downloads\\" + file[i] + ".xml";
				if(File.Exists(path))
				{
					using(FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
					{
						using(StreamReader sr = new StreamReader(fs))
						{
							contents += sr.ReadToEnd();
							if((i+1) < filecount)
							{
								contents += "\n\n\n<w:p w:rsidR=\"00F4055C\" w:rsidRDefault=\"00F4055C\"/><w:p w:rsidR=\"005D7E36\" w:rsidRDefault=\"005D7E36\"><w:r><w:br w:type=\"page\"/></w:r></w:p><w:p w:rsidR=\"005D7E36\" w:rsidRDefault=\"005D7E36\"><w:bookmarkStart w:id=\"0\" w:name=\"_GoBack\"/><w:bookmarkEnd w:id=\"0\"/></w:p>";
							}
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
	    string ContractDate = "";
	    string BuyersName = "";
	    string SellersName = "";
	    string SellersAddress = "";
	    string BuyersAddress = "";
	    string GrantorAddressLine1 = "";
	    string GrantorAddressLine2 = "";
	    string GrantorCity = "";
	    string GrantorState = "";
	    string GrantorZip = "";
	    string GrantorCounty = "";
	    DateTime now = DateTime.Now;
	    string DateDay = Ordinal(now.Day).ToString();
	    string DateMonth = now.ToString("MMMM");
	    string DateYear = now.ToString("yyyy");
	    string PIN = "";
	    string SellersAttorney = "";
	    string SellersAttorneyAddress = "";
	    
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
	    
      //Response.Write(request.Xml);
      //Response.End();
			
			try {
	    XmlDocument doc = new XmlDocument();
	    // show the details for the active requests
			Affinity.Requests rs = order.GetCurrentRequests();

			foreach (Affinity.Request r in rs)
			{
			
			if (r.IsCurrent)
			{

		    doc.LoadXml(r.Xml);
		    //Response.Write(r.Xml);
		    //Response.End();
		
		    XmlNode node = doc.SelectSingleNode("//response/field[@name = 'CommittmentDeadline']");
		    if (node != null) CommittmentDate = XMLEscape(node.InnerText.Replace("T:00:00:00", ""));
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Seller']");
		    if (node != null) SellersName = XMLEscape(node.InnerText);
		    Grantor = SellersName;
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Seller1Name2']");
		    if (node != null) Grantor2 = " and " + XMLEscape(node.InnerText);
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Buyer']");
		    if (node != null) BuyersName = XMLEscape(node.InnerText);
		    Grantee = BuyersName;
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Buyer1Name2']");
		    if (node != null)
		    {
		    	Grantee2 = " and " + XMLEscape(node.InnerText);
		    	BuyersName += ", " + XMLEscape(node.InnerText);
		    }
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Buyer2Name1']");
		    if (node != null) BuyersName += ", " + XMLEscape(node.InnerText);
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Buyer2Name2']");
		    if (node != null) BuyersName += ", " + XMLEscape(node.InnerText);
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Buyer3Name1']");
		    if (node != null) BuyersName += ", " + XMLEscape(node.InnerText);
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Buyer3Name2']");
		    if (node != null) BuyersName += ", " + XMLEscape(node.InnerText);
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Buyer4Name1']");
		    if (node != null) BuyersName += ", " + XMLEscape(node.InnerText);
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Buyer4Name2']");
		    if (node != null) BuyersName += ", " + XMLEscape(node.InnerText);
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Buyer5Name1']");
		    if (node != null) BuyersName += ", " + XMLEscape(node.InnerText);
		
		    node = doc.SelectSingleNode("//response/field[@name = 'Buyer5Name2']");
		    if (node != null) BuyersName += ", " + XMLEscape(node.InnerText);
		
		    node = doc.SelectSingleNode("//response/field[@name = 'ApplicantAddress']");
		    if (node != null) BuyersAddress = XMLEscape(node.InnerText);
		
		    node = doc.SelectSingleNode("//response/field[@name = 'ApplicantAddress2']");
		    if (node != null) BuyersAddress += " " + XMLEscape(node.InnerText);
		
		    node = doc.SelectSingleNode("//response/field[@name = 'ApplicantCity']");
		    if (node != null) BuyersAddress += " " + XMLEscape(node.InnerText);
		
		    node = doc.SelectSingleNode("//response/field[@name = 'ApplicantState']");
		    if (node != null) BuyersAddress += ", " + XMLEscape(node.InnerText);
		
		    node = doc.SelectSingleNode("//response/field[@name = 'ApplicantZip']");
		    if (node != null) BuyersAddress += " " + XMLEscape(node.InnerText);
		    BuyersAddress = BuyersAddress.Trim();
		
		    node = doc.SelectSingleNode("//response/field[@name = 'SellersAttorneyName']");
		    if (node != null) SellersAttorney = XMLEscape(node.InnerText);
		
		    node = doc.SelectSingleNode("//response/field[@name = 'SellersAttorneyAddress']");
		    if (node != null) SellersAttorneyAddress = XMLEscape(node.InnerText);
		
		    node = doc.SelectSingleNode("//response/field[@name = 'SellersAttorneyAddress2']");
		    if (node != null) SellersAttorneyAddress += " " + XMLEscape(node.InnerText);
		
		    node = doc.SelectSingleNode("//response/field[@name = 'SellersAttorneyCity']");
		    if (node != null) SellersAttorneyAddress += " " + XMLEscape(node.InnerText);
		
		    node = doc.SelectSingleNode("//response/field[@name = 'SellersAttorneyState']");
		    if (node != null) SellersAttorneyAddress += ", " + XMLEscape(node.InnerText);
		
		    node = doc.SelectSingleNode("//response/field[@name = 'SellersAttorneyZip']");
		    if (node != null) SellersAttorneyAddress += " " + XMLEscape(node.InnerText);
		    
		    GrantorAddressLine1 = XMLEscape(order.PropertyAddress);
		    GrantorAddressLine2 = XMLEscape(order.PropertyAddress2);
		    GrantorCity = XMLEscape(order.PropertyCity);
		    GrantorState = XMLEscape(order.PropertyState);
		    GrantorZip = XMLEscape(order.PropertyZip);
		    GrantorCounty = XMLEscape(order.PropertyCounty);
		    
		    SellersAddress = GrantorAddressLine1 + " " + GrantorAddressLine2 + " " + GrantorCity + ", " + GrantorState + " " + GrantorZip;
		    
		    ContractDate = XMLEscape(order.ClosingDate.ToString()).Replace(" 12:00:00 AM", "");
		
		    County = order.PropertyCounty;
		    PIN = order.Pin;
		  }
			}
	
			} catch (Exception err){}
				
			contents = contents.Replace("[COUNTY]", County).Replace("[GRANTOR]", Grantor).Replace("[GRANTOR2]", Grantor2).Replace("[GRANTORCITY]", GrantorCity).Replace("[GRANTORCOUNTY]", GrantorCounty).Replace("[GRANTORADDRESSLINE1]", GrantorAddressLine1).Replace("[GRANTORADDRESSLINE2]", GrantorAddressLine2).Replace("[GRANTEE]", Grantee).Replace("[GRANTEE2]", Grantee2).Replace("[DEEDDATE]", DeedDate.ToString("MM/dd/yyyy")).Replace("[DEEDMONTH]", DeedDate.ToString("MMMM")).Replace("[DEEDYEAR]", DeedDate.ToString("yyyy")).Replace("[DEEDDAY]", DeedDateOrdinal).Replace("[CONTRACTDATE]", ContractDate).Replace("[BUYERSNAME]", BuyersName).Replace("[BUYERSADDRESS]", BuyersAddress).Replace("[SELLERSNAME]", SellersName).Replace("[SELLERSADDRESS]", SellersAddress).Replace("[COMMITMENTDATE]", CommittmentDate).Replace("[DATEDAY]", DateDay).Replace("[DATEMONTH]", DateMonth).Replace("[DATEYEAR]", DateYear).Replace("[PIN]", PIN).Replace("[SELLERSATTORNEY]", SellersAttorney).Replace("[SELLERSATTORNEYADDRESS]", SellersAttorneyAddress);
			
			contents = contents.Replace("[MAILTONAME]", "");
			contents = contents.Replace("[MAILTOADDRESS1]", "");
			contents = contents.Replace("[MAILTOADDRESS2]", "");
			
			contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type month]\" </w:instrText>", "").Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type year]\" </w:instrText>", "");
			
			if(!County.Equals(""))
			{
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesInDoc \"[Click and type in county]\" </w:instrText>", "");
			}
			
			if(!CommittmentDate.Equals(""))
			{
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesInDoc \"[Click and type in commitment date]\" </w:instrText>", "");
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
			}
			
			if(!GrantorAddressLine1.Equals("") || !GrantorAddressLine2.Equals(""))
			{
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type grantor's address line 2]\" </w:instrText>", "");
			}
			
			if(!GrantorCity.Equals(""))
			{
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type grantors' town]\" </w:instrText>", "");
			}
			
			if(!GrantorCounty.Equals(""))
			{
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type grantors’s County]\" </w:instrText>", "").Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type county]\" </w:instrText>", "").Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesInDoc \"[Click and type in County}\" </w:instrText>", "");
			}
			
			if(!DeedDate.Equals(""))
			{
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesInDoc \"[Click and type in date of deed]\" </w:instrText>", "");
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type month of deed]\" </w:instrText>", "");
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type date of deed]\" </w:instrText>", "");
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type month of deed]\" </w:instrText>", "");
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type year of deed]\" </w:instrText>", "");
			}
			
			if(!ContractDate.Equals(""))
			{
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesInDoc \"[Click and type contract date]\" </w:instrText>", "");
			}
			
			if(!SellersName.Equals(""))
			{
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesInDoc \"[Click and type sellers' names]\" </w:instrText>", "");
			}
			
			if(!BuyersName.Equals(""))
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
			
			if(!SellersAttorney.Equals(""))
			{
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type Attorney's Name]\" </w:instrText>", "");
			}
			
			if(!SellersAttorneyAddress.Equals(""))
			{
				contents = contents.Replace("<w:instrText xml:space=\"preserve\"> MACROBUTTON  AcceptAllChangesShown \"[Click here and type Attorney's Address]\" </w:instrText>", "");
			}

      /*
      string fileNameXML = HttpContext.Current.Server.MapPath(".") + "\\downloads\\Disclosure_Agent.doc";
	    string FileName = HttpContext.Current.Server.MapPath(".") + "\\downloads\\tested.xml";

			 Document dc = new Document();
	            dc.LoadFromFile(fileNameXML, FileFormat.Doc);
	            dc.SaveToFile(FileName, FileFormat.Xml);
	            //dc.SaveToStream(HttpContext.Current.Response.OutputStream, FileFormat.Xml);
	     */  
			
			int len = contents.Length+50;
			HttpContext.Current.Response.ContentType = "application/ms-word";
			HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=ATS.doc");
			HttpContext.Current.Response.AppendHeader("Content-Length", len.ToString());
			
	    HttpContext.Current.Response.Write(contents + "                                                  ");
			HttpContext.Current.Response.Flush();
			
		}

   }
}

}
