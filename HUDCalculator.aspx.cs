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
using System.Net;
using MySql.Data.MySqlClient;
using GemBox.Spreadsheet;

using Com.VerySimple.Util;

namespace Affinity
{
	public partial class HUDCalculator : PageBase
	{  
	    protected void Page_Load(object sender, EventArgs e)
	    {
	    	if(Request["PIN"] != null && !Request["PIN"].Equals(""))
	    	{
					Affinity.Orders orders = new Affinity.Orders(this.phreezer);
					Affinity.OrderCriteria oc = new Affinity.OrderCriteria();
					oc.AppendToOrderBy("Created",true);
					
					string pin = Request["PIN"].Replace("WEB-", "");
					
					oc.Pin = pin;
					oc.OriginatorId = this.GetAccount().Id;
					orders.Query(oc);
					
					if(orders.Count == 0)
					{
						oc = new Affinity.OrderCriteria();
						oc.OriginatorId = this.GetAccount().Id;
						
						int pinid = 0;
						
						int.TryParse(pin, out pinid);
					
						oc.Id = pinid;
						orders.Query(oc);
						
						if(orders.Count == 0)
						{
							oc = new Affinity.OrderCriteria();
							oc.OriginatorId = this.GetAccount().Id;
						
							oc.InternalId = pin;
							orders.Query(oc);
						}
					}
					
					if(orders.Count > 0)
					{
						foreach (Affinity.Order order in orders)
						{
							Response.Write("<root><address>" + order.PropertyAddress + " " + order.PropertyAddress2 + "</address><city>" + order.PropertyCity + "</city><state>" + order.PropertyState + "</state><zip>" + order.PropertyZip + "</zip><county>" + order.PropertyCounty + "</county><closingdate>" + order.ClosingDate + "</closingdate><clientname>" + order.ClientName + "</clientname><requests>");
							
							Affinity.RequestCriteria reqcrit = new Affinity.RequestCriteria();
							//reqcrit.IsCurrent = 1;
							reqcrit.AppendToOrderBy("Created",true);
							Affinity.Requests reqs = order.GetOrderRequests(reqcrit);
							foreach (Affinity.Request req in reqs)
							{
								Response.Write("<request><type>" + req.RequestTypeCode + "</type>" + req.Xml + "</request>");
							}
							
							Response.Write("</requests></root>");
							break;
						}
					}
					else
					{
						Response.Write("<root></root>");
					}
	    		Response.End();
	    	}
	    	
      	if(Request["TaxingCity"] != null && Request["SalesPriceAmount"] != null)
      	{
  				decimal buystamptax = 0;
  				decimal sellstamptax = 0;
        	decimal stamptaxrate = 0;
        	decimal stamptaxper = 0;
        	decimal sales = 0;
	      	string stamptaxStr = "";
      		if(!Request["TaxingCity"].Trim().Equals("") && !Request["SalesPriceAmount"].Trim().Equals(""))
      		{
        		decimal.TryParse(Request["SalesPriceAmount"].Trim().Replace(",", "").Replace("$", ""), out sales);
      			if(Request["TaxingCity"].Trim().ToLower().Equals("chicago"))
      			{
      				stamptaxper = 1000;
      				buystamptax = (sales / stamptaxper) * ((decimal) 7.5);
      				sellstamptax = (sales / stamptaxper) * ((decimal) 3);
      				stamptaxStr = buystamptax.ToString() + "|" + sellstamptax.ToString();
      			}
      			else
      			{
							MySqlDataReader reader = this.phreezer.ExecuteReader("select * from `taxing_district` t where t.taxing_district = '" + Request["TaxingCity"] + "'");
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
				        	
			        	if(isSeller && isBuyer)
			        	{
			        		stamptaxStr = stamptax.ToString();
			        	}
			        	else if(isBuyer)
			        	{
      						stamptaxStr = stamptax.ToString() + "|0";
			        	}
			        	else if(isSeller)
			        	{
      						stamptaxStr = "0|" + stamptax.ToString();
			        	}
		          }
							reader.Close();
						}
					}
        	
					Response.Write(stamptaxStr);
	    		Response.End();
        }
        
        if(Request["SalesPriceAmount"] != null)
        {
		      decimal purchprice = 0;
					decimal.TryParse(Request["SalesPriceAmount"].Replace(",", "").Replace("$", ""), out purchprice);
					string InsuranceRate = "";
		      
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
         	
					Response.Write(InsuranceRate);
	    		Response.End();
       }
        
        if(Request["EscrowAmount"] != null)
        {
		      decimal purchprice = 0;
					decimal.TryParse(Request["EscrowAmount"].Replace(",", "").Replace("$", ""), out purchprice);
					string EscrowAmt = "";
					bool here = false;
		      
		      for(int purchidx = 0; purchidx < 11; purchidx++)
					{
						if(((purchprice > (purchidx * 50000) && purchprice <= ((purchidx + 1) * 50000)) || purchidx == 10) && !here)
						{
			        Affinity.TitleFees tf1 = new Affinity.TitleFees(this.phreezer);

			        Affinity.TitleFeesCriteria tfc = new Affinity.TitleFeesCriteria();
			      	tfc.FeeType = "Escrow Services: Residential Closing Fees";
			      	
			      	if(purchidx < 2)
			      	{
			      		tfc.Name = "$100,000 or less";
			      	}
			      	else if(purchidx == 6 || purchidx == 7)
			      	{
			      		tfc.Name = "$300,001 to $400,000";
			      	}
			      	else if(purchidx > 7)
			      	{
			      		tfc.Name = "$400,001 to $500,000";
			      	}
			      	else
			      	{
			      		tfc.Name = "$" + (purchidx * 50).ToString() + ",001 to $" + ((purchidx + 1) * 50).ToString() + ",000";
			      	}
			        tfc.AppendToOrderBy("Id");
			        tf1.Query(tfc);
			
			        IEnumerator tfi = tf1.GetEnumerator();
			
			        // loop through the checkboxes and insert or delete as needed
			        while (tfi.MoveNext())
			        {
			            Affinity.TitleFee tfitem1 = (Affinity.TitleFee) tfi.Current;
									EscrowAmt = tfitem1.Fee.ToString();
									
									if(purchidx == 10)
									{
										decimal escrwamt = 0;
										decimal.TryParse(EscrowAmt, out escrwamt);
										decimal pprice = purchprice - 500000;
										int pricmult = (int) Math.Floor(pprice / 50000);
										escrwamt = escrwamt + (pricmult * 50);
										EscrowAmt = escrwamt.ToString();
									}
			        }
			        here = true;
			      }
					}
         	
					Response.Write(EscrowAmt);
	    		Response.End();
       }

	    	
				this.Master.SetLayout("HUD Calculator", MasterPage.LayoutStyle.ContentOnly);
				
				
	    	if (Page.IsPostBack)
	    	{
	    		generateSpreadsheet();
	    	}
	    	else
	    	{
	    		DateTime nw = DateTime.Now;
	    		PrintDate.Value = nw.ToShortDateString();
	    		CountyTaxesDate.Value = "1/1/" + nw.Year.ToString() + " - " + nw.ToShortDateString();
	    		
			    Affinity.TitleFees tf = new Affinity.TitleFees(this.phreezer);
		   		Affinity.TitleFeesCriteria tfc = new Affinity.TitleFeesCriteria();
		      tfc.Name = "Simultaneously Issued Mortgage Policy";
		      tf.Query(tfc);
		      
	        IEnumerator tfi = tf.GetEnumerator();
	
	        // loop through the checkboxes and insert or delete as needed
	        if (tfi.MoveNext())
	        {
	            Affinity.TitleFee tfitem1 = (Affinity.TitleFee) tfi.Current;
							LoanPolicyTitleInsurance.Value = tfitem1.Fee.ToString();
	        }
	    		
	    		tf = new Affinity.TitleFees(this.phreezer);
		   		tfc = new Affinity.TitleFeesCriteria();
		      tfc.Name = "Abstract Fee/Search Fee";
		      tf.Query(tfc);
		      
	        tfi = tf.GetEnumerator();
	
	        // loop through the checkboxes and insert or delete as needed
	        if (tfi.MoveNext())
	        {
	            Affinity.TitleFee tfitem2 = (Affinity.TitleFee) tfi.Current;
							TitleAbstractFee.Value = tfitem2.Fee.ToString();
	        }

	    	}
			}
			
			protected void generateSpreadsheet() {
					SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
					bool isRefi = false;
					bool isShort = false;
					
					if(IsPurchase.Value.Equals("refinance"))
					{
						isRefi = true;
					}
					
					if(IsShortSale.Value.Equals("yes"))
					{
						isShort = true;
					}
					
					XmlDocument submissionDoc = new XmlDocument();
					submissionDoc.LoadXml("<form></form>");
					XmlNode formNode = submissionDoc.SelectSingleNode("form");
					
					foreach(string key in Request.Form) {
						if(key.IndexOf("__") > -1 || key.Equals("s")) continue;
						XmlElement newnode = submissionDoc.CreateElement(key.Replace("$", "").Replace("ctl00content_cph", ""));
						newnode.InnerText = Request.Form[key];
						formNode.AppendChild(newnode);
     			}
					
					// Log the form submission
					Affinity.HUDLog h = new Affinity.HUDLog(this.phreezer);
					h.AccountID = this.GetAccount().Id;
					h.SubmissionXML = submissionDoc.OuterXml;
					h.Insert();
					
					
					ExcelFile file = new ExcelFile();
					ExcelWorksheet ws = file.Worksheets.Add("HUD");
					
					// Setup Worksheet margins
					/*
					ws.PrintOptions.TopMargin = .3;
					ws.PrintOptions.BottomMargin = .3;
					ws.PrintOptions.LeftMargin = .1;
					ws.PrintOptions.RightMargin = .1;
					*/
					ws.PrintOptions.FitWorksheetWidthToPages = 1;
		
					// setup column widths
	      	ws.Columns[0].Width = (10 * 250);
	      	ws.Columns[1].Width = (14 * 250);
	      	ws.Columns[2].Width = (14 * 245);
	      	ws.Columns[3].Width = (50 * 257);
	      	ws.Columns[4].Width = (14 * 250);
	      	ws.Columns[5].Width = (14 * 250);
	
					// setup row height
          ws.Rows[0].Height = 290;
          ws.Rows[1].Height = 290;
          ws.Rows[2].Height = 290;
          ws.Rows[3].Height = 300;
          ws.Rows[4].Height = 300;
          ws.Rows[5].Height = 290;
          ws.Rows[6].Height = 290;
          ws.Rows[7].Height = 290;
          ws.Rows[8].Height = 290;
          ws.Rows[9].Height = 290;
          ws.Rows[10].Height = 300;
          ws.Rows[11].Height = 300;
          ws.Rows[12].Height = 290;
          ws.Rows[13].Height = 290;
          ws.Rows[14].Height = 290;
          ws.Rows[15].Height = 290;
          ws.Rows[16].Height = 290;
          ws.Rows[17].Height = 290;
          ws.Rows[18].Height = 290;
          ws.Rows[19].Height = 290;
          ws.Rows[20].Height = 290;
          ws.Rows[21].Height = 290;
          ws.Rows[22].Height = 290;
          ws.Rows[23].Height = 290;
          ws.Rows[24].Height = 290;
          ws.Rows[25].Height = 290;
          ws.Rows[26].Height = 290;
          ws.Rows[27].Height = 290;
          ws.Rows[28].Height = 290;
          ws.Rows[29].Height = 290;
          ws.Rows[30].Height = 290;
          ws.Rows[31].Height = 290;
          ws.Rows[32].Height = 290;
          ws.Rows[33].Height = 290;
          ws.Rows[34].Height = 290;
          ws.Rows[35].Height = 290;
          ws.Rows[36].Height = 290;
          ws.Rows[37].Height = 290;
          ws.Rows[38].Height = 290;
          ws.Rows[39].Height = 290;
          ws.Rows[40].Height = 290;
          ws.Rows[41].Height = 290;
          ws.Rows[42].Height = 290;
          ws.Rows[43].Height = 290;
          ws.Rows[44].Height = 290;
          ws.Rows[45].Height = 290;
          ws.Rows[46].Height = 290;
          ws.Rows[47].Height = 290;
          ws.Rows[48].Height = 290;
          ws.Rows[49].Height = 290;
          ws.Rows[50].Height = 290;
          ws.Rows[51].Height = 290;
          ws.Rows[52].Height = 290;
          ws.Rows[53].Height = 290;
          ws.Rows[54].Height = 290;
          ws.Rows[55].Height = 290;
          ws.Rows[56].Height = 290;
          ws.Rows[57].Height = 290;
          ws.Rows[58].Height = 290;
          ws.Rows[59].Height = 290;
          ws.Rows[60].Height = 290;
          ws.Rows[61].Height = 290;
          ws.Rows[62].Height = 290;
          ws.Rows[63].Height = 290;
          ws.Rows[64].Height = 290;
          ws.Rows[65].Height = 290;
          ws.Rows[66].Height = 290;
          ws.Rows[67].Height = 290;
          ws.Rows[68].Height = 290;
          ws.Rows[69].Height = 290;
          ws.Rows[70].Height = 290;
          ws.Rows[71].Height = 290;
          ws.Rows[72].Height = 290;
          ws.Rows[73].Height = 290;
          ws.Rows[74].Height = 290;
          ws.Rows[75].Height = 290;
          ws.Rows[76].Height = 290;
          ws.Rows[77].Height = 290;
          ws.Rows[78].Height = 290;
          ws.Rows[79].Height = 290;
          ws.Rows[80].Height = 290;
          ws.Rows[81].Height = 290;
          ws.Rows[82].Height = 290;
          ws.Rows[83].Height = 290;
          ws.Rows[84].Height = 290;
          ws.Rows[85].Height = 290;
          ws.Rows[86].Height = 290;
          ws.Rows[87].Height = 290;
          ws.Rows[88].Height = 290;
          ws.Rows[89].Height = 290;
          ws.Rows[90].Height = 290;
          ws.Rows[91].Height = 290;
          ws.Rows[92].Height = 290;
          ws.Rows[93].Height = 290;
          ws.Rows[94].Height = 290;
          ws.Rows[95].Height = 290;
          ws.Rows[96].Height = 290;
          ws.Rows[97].Height = 290;
          ws.Rows[98].Height = 290;
          ws.Rows[99].Height = 290;
          ws.Rows[100].Height = 290;
          ws.Rows[101].Height = 290;
          ws.Rows[102].Height = 290;
          ws.Rows[103].Height = 290;
          ws.Rows[104].Height = 290;
          ws.Rows[105].Height = 290;
          ws.Rows[106].Height = 290;
          ws.Rows[107].Height = 290;
          ws.Rows[108].Height = 290;
          ws.Rows[109].Height = 290;
          ws.Rows[110].Height = 290;
          ws.Rows[111].Height = 290;
          ws.Rows[112].Height = 290;
          ws.Rows[113].Height = 290;
          ws.Rows[114].Height = 290;
          ws.Rows[115].Height = 290;
          ws.Rows[116].Height = 290;
          ws.Rows[117].Height = 290;
          ws.Rows[118].Height = 290;
          ws.Rows[119].Height = 290;
          ws.Rows[120].Height = 290;
          ws.Rows[121].Height = 290;
          ws.Rows[122].Height = 290;
          ws.Rows[123].Height = 290;
          ws.Rows[124].Height = 290;
          ws.Rows[125].Height = 290;
          ws.Rows[126].Height = 290;
          ws.Rows[127].Height = 290;
          ws.Rows[128].Height = 290;
          ws.Rows[129].Height = 290;
          ws.Rows[130].Height = 290;
          ws.Rows[131].Height = 290;
          ws.Rows[132].Height = 290;
          ws.Rows[133].Height = 290;
          ws.Rows[134].Height = 290;
          ws.Rows[135].Height = 290;
          ws.Rows[136].Height = 290;
          ws.Rows[137].Height = 290;
          ws.Rows[138].Height = 290;
          ws.Rows[139].Height = 290;
          ws.Rows[140].Height = 290;
          ws.Rows[141].Height = 290;
          ws.Rows[142].Height = 290;
          ws.Rows[143].Height = 290;
          ws.Rows[144].Height = 290;
          ws.Rows[145].Height = 290;
          ws.Rows[146].Height = 290;
          ws.Rows[147].Height = 290;
          ws.Rows[148].Height = 290;
          ws.Rows[149].Height = 290;	
	      	
	      	// Borders
					CellRange cr = ws.Cells.GetSubrange("F25", "F149");
					cr.Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Black, LineStyle.Medium);
					
					cr = ws.Cells.GetSubrange("A25", "A149");
					cr.Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Black, LineStyle.Medium);
					
					ExcelRow row = ws.Rows[0];
					row.Cells[0].Style.Borders.SetBorders(MultipleBorders.Top, System.Drawing.Color.Black, LineStyle.Medium);
					row.Cells[1].Style.Borders.SetBorders(MultipleBorders.Top, System.Drawing.Color.Black, LineStyle.Medium);
					row.Cells[2].Style.Borders.SetBorders(MultipleBorders.Top, System.Drawing.Color.Black, LineStyle.Medium);
					row.Cells[3].Style.Borders.SetBorders(MultipleBorders.Top, System.Drawing.Color.Black, LineStyle.Medium);
					row.Cells[4].Style.Borders.SetBorders(MultipleBorders.Top, System.Drawing.Color.Black, LineStyle.Medium);
					row.Cells[5].Style.Borders.SetBorders(MultipleBorders.Top, System.Drawing.Color.Black, LineStyle.Medium);
					
					row = ws.Rows[3];
					row.Cells[0].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
					row.Cells[1].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
					row.Cells[2].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
					row.Cells[3].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
					row.Cells[4].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
					row.Cells[5].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);

					ws.Rows[0].Cells[0].Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Black, LineStyle.Medium);
					ws.Rows[1].Cells[0].Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Black, LineStyle.Medium);
					ws.Rows[2].Cells[0].Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Black, LineStyle.Medium);
					ws.Rows[3].Cells[0].Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Black, LineStyle.Medium);
					ws.Rows[0].Cells[5].Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Black, LineStyle.Medium);
					ws.Rows[1].Cells[5].Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Black, LineStyle.Medium);
					ws.Rows[2].Cells[5].Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Black, LineStyle.Medium);
					ws.Rows[3].Cells[5].Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Black, LineStyle.Medium);
					
					row = ws.Rows[4];
					row.Cells[0].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
					row.Cells[1].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
					row.Cells[2].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
					row.Cells[3].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
					row.Cells[4].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
					row.Cells[5].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
					
          ws.Rows[5].Cells[0].Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[6].Cells[0].Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[7].Cells[0].Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[8].Cells[0].Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[9].Cells[0].Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[10].Cells[0].Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[5].Cells[5].Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[6].Cells[5].Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[7].Cells[5].Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[8].Cells[5].Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[9].Cells[5].Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[10].Cells[5].Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Black, LineStyle.Medium);

          row = ws.Rows[10];
          row.Cells[0].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[1].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[2].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[3].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[4].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[5].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);

          ws.Rows[12].Cells[0].Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[13].Cells[0].Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[14].Cells[0].Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[15].Cells[0].Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[16].Cells[0].Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[17].Cells[0].Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[18].Cells[0].Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[19].Cells[0].Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[20].Cells[0].Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[21].Cells[0].Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[22].Cells[0].Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[12].Cells[5].Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[13].Cells[5].Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[14].Cells[5].Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[15].Cells[5].Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[16].Cells[5].Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[17].Cells[5].Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[18].Cells[5].Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[19].Cells[5].Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[20].Cells[5].Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[21].Cells[5].Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Black, LineStyle.Medium);
          ws.Rows[22].Cells[5].Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Black, LineStyle.Medium);

          row = ws.Rows[11];
          row.Cells[0].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[1].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[2].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[3].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[4].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[5].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);

          row = ws.Rows[22];
          row.Cells[0].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[1].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[2].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[3].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[4].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[5].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);

          row = ws.Rows[23];
          row.Cells[0].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[1].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[2].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[3].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[4].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[5].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);

          row = ws.Rows[134];
          row.Cells[0].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[1].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[2].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[3].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[4].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[5].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);

          row = ws.Rows[135];
          row.Cells[0].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[1].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[2].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[3].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[4].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[5].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);

          row = ws.Rows[148];
          row.Cells[0].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[1].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[2].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[3].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[4].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[5].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          
          // Start labeling and setting values
	      	ws.Rows[0].Cells[3].Value = "Affinity Title Services";
	      	ws.Rows[0].Cells[3].Style.Font.Weight = ExcelFont.BoldWeight;
	      	ws.Rows[0].Cells[3].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
	      	//ws.Rows[6].Cells[5].Value = "Title Company";
	      	ws.Rows[1].Cells[3].Value = "ALTA Universal ID";
	      	ws.Rows[1].Cells[3].Style.Font.Weight = ExcelFont.BoldWeight;
	      	ws.Rows[1].Cells[3].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
	      	//ws.Rows[7].Cells[5].Value = "Logo";
	      	ws.Rows[2].Cells[3].Value = "2454 E. Dempster Street, Suite 401 Des Plaines, IL  60016";
	      	ws.Rows[2].Cells[3].Style.Font.Weight = ExcelFont.BoldWeight;
	      	ws.Rows[2].Cells[3].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;

					ws.Rows[6].Cells[0].Value = "File No./Escrow No.:";
	      	ws.Rows[6].Cells[2].Value = PIN.Value;
	      	ws.Rows[7].Cells[0].Value = "Print Date & Time:";
	      	ws.Rows[7].Cells[2].Value = PrintDate.Value;
	      	ws.Rows[8].Cells[0].Value = "Contact:";
	      	ws.Rows[8].Cells[2].Value = Contact.Value;
	      	ws.Rows[9].Cells[0].Value = "Settlement Location:";
	      	ws.Rows[9].Cells[2].Value = SettlementLocationAddress.Value;
	      	
	      	ws.Rows[13].Cells[0].Value = "Property Address:";
	      	ws.Rows[13].Cells[2].Value = PropertyAddress.Value;
	      	ws.Rows[14].Cells[0].Value = "Property City:";
	      	ws.Rows[14].Cells[2].Value = PropertyCity.Value;
	      	ws.Rows[15].Cells[0].Value = "Buyer:";
	      	ws.Rows[15].Cells[2].Value = Buyer.Value;
	      	
	      	if(!isRefi)
	      	{
	      		ws.Rows[16].Cells[0].Value = "Seller:";
	      		ws.Rows[16].Cells[2].Value = Seller.Value;
	      	}
	      	ws.Rows[17].Cells[0].Value = "Lender:";
	      	ws.Rows[17].Cells[2].Value = Lender.Value;
	      	ws.Rows[18].Cells[0].Value = "Settlement Date:";
	      	ws.Rows[18].Cells[2].Value = SettlementDate.Value;
	      	ws.Rows[19].Cells[0].Value = "Disbursement Date:";
	      	ws.Rows[19].Cells[2].Value = DisbursementDate.Value;
	      	ws.Rows[20].Cells[0].Value = "Additional dates per state requirements:";
      	
	      	// Setup main cells that will have calculations performed from
	      	ws.Rows[21].Cells[2].Value = AdditionalDates0.Value;
	      	int addRow = 0;
	      	
	      	if(!AdditionalDates1.Value.Equals(""))
	      	{
	      		ws.Rows[22].Cells[2].Value = AdditionalDates1.Value;
	      		addRow++;
	      	}
	      	
	      	if(!AdditionalDates2.Value.Equals(""))
	      	{
	      		ws.Rows[23].Cells[2].Value = AdditionalDates2.Value;
	      		addRow++;
	      	}
	      	
	      	if(!AdditionalDates3.Value.Equals(""))
	      	{
	      		ws.Rows[24].Cells[2].Value = AdditionalDates3.Value;
	      		addRow++;
	      	}
	      	
	      	if(!AdditionalDates4.Value.Equals(""))
	      	{
	      		ws.Rows[25].Cells[2].Value = AdditionalDates4.Value;
	      		addRow++;
	      	}
	      	
	      	if(!AdditionalDates5.Value.Equals(""))
	      	{
	      		ws.Rows[26].Cells[2].Value = AdditionalDates5.Value;
	      		addRow++;
	      	}
	      	
	      	
	        ws.Rows[24 + addRow].Cells[0].Style.FillPattern.SetSolid(System.Drawing.Color.DarkGray);
	        ws.Rows[25 + addRow].Cells[0].Style.FillPattern.SetSolid(System.Drawing.Color.LightGray);
	        ws.Rows[26 + addRow].Cells[0].Style.FillPattern.SetSolid(System.Drawing.Color.LightGray);
	        ws.Rows[24 + addRow].Cells[1].Style.FillPattern.SetSolid(System.Drawing.Color.DarkGray);
	        ws.Rows[25 + addRow].Cells[1].Style.FillPattern.SetSolid(System.Drawing.Color.LightGray);
	        ws.Rows[26 + addRow].Cells[1].Style.FillPattern.SetSolid(System.Drawing.Color.LightGray);
	        ws.Rows[24 + addRow].Cells[2].Style.FillPattern.SetSolid(System.Drawing.Color.DarkGray);
	        ws.Rows[25 + addRow].Cells[2].Style.FillPattern.SetSolid(System.Drawing.Color.LightGray);
	        ws.Rows[26 + addRow].Cells[2].Style.FillPattern.SetSolid(System.Drawing.Color.LightGray);
	        ws.Rows[24 + addRow].Cells[3].Style.FillPattern.SetSolid(System.Drawing.Color.DarkGray);
	        ws.Rows[25 + addRow].Cells[3].Style.FillPattern.SetSolid(System.Drawing.Color.LightGray);
	        ws.Rows[26 + addRow].Cells[3].Style.FillPattern.SetSolid(System.Drawing.Color.LightGray);
	        ws.Rows[24 + addRow].Cells[4].Style.FillPattern.SetSolid(System.Drawing.Color.DarkGray);
	        ws.Rows[25 + addRow].Cells[4].Style.FillPattern.SetSolid(System.Drawing.Color.LightGray);
	        ws.Rows[26 + addRow].Cells[4].Style.FillPattern.SetSolid(System.Drawing.Color.LightGray);
	        ws.Rows[24 + addRow].Cells[5].Style.FillPattern.SetSolid(System.Drawing.Color.DarkGray);
	        ws.Rows[25 + addRow].Cells[5].Style.FillPattern.SetSolid(System.Drawing.Color.LightGray);
	        ws.Rows[26 + addRow].Cells[5].Style.FillPattern.SetSolid(System.Drawing.Color.LightGray);
	      	ws.Rows[25 + addRow].Cells[1].Value = "Borrower/Buyer";
	      	ws.Cells.GetSubrangeAbsolute((25 + addRow), 1, (25 + addRow), 2).Merged = true;
	      	ws.Rows[25 + addRow].Cells[1].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
	      	ws.Rows[25 + addRow].Cells[0].Style.Font.Weight = ExcelFont.BoldWeight;
	      	
	      	ws.Rows[25 + addRow].Cells[3].Value = "Description";
	      	ws.Rows[25 + addRow].Cells[3].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
	      	ws.Rows[25 + addRow].Cells[3].Style.Font.Weight = ExcelFont.BoldWeight;
	      	
	      	ws.Rows[26 + addRow].Cells[1].Value = "Debit";
	      	ws.Rows[26 + addRow].Cells[1].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
	      	ws.Rows[26 + addRow].Cells[1].Style.Font.Weight = ExcelFont.BoldWeight;
	      	
	      	ws.Rows[26 + addRow].Cells[2].Value = "Credit";
	      	ws.Rows[26 + addRow].Cells[2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
	      	ws.Rows[26 + addRow].Cells[2].Style.Font.Weight = ExcelFont.BoldWeight;
	      	
	      	ws.Rows[26 + addRow].Cells[4].Value = "Debit";
	      	ws.Rows[26 + addRow].Cells[4].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
	      	ws.Rows[26 + addRow].Cells[4].Style.Font.Weight = ExcelFont.BoldWeight;
	      	
	      	ws.Rows[26 + addRow].Cells[5].Value = "Credit";
	      	ws.Rows[26 + addRow].Cells[5].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
	      	ws.Rows[26 + addRow].Cells[5].Style.Font.Weight = ExcelFont.BoldWeight;
	
	      	if(!isRefi)
	      	{
		      	ws.Rows[25 + addRow].Cells[4].Value = "Seller";
		      	ws.Cells.GetSubrangeAbsolute((25 + addRow), 4, (25 + addRow), 5).Merged = true;
		      	ws.Rows[25 + addRow].Cells[4].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
		      	ws.Rows[25 + addRow].Cells[4].Style.Font.Weight = ExcelFont.BoldWeight;
		      }
	
		      	decimal val = 0;
	      	
	      	ws.Rows[27 + addRow].Cells[3].Value = "Financial";
	      	ws.Rows[27 + addRow].Cells[3].Style.Font.Weight = ExcelFont.BoldWeight;
	      	
	      	ws.Rows[28 + addRow].Cells[3].Value = "Sales Price of Property";
	      	decimal SalesPriceAmount = 0;
	      	decimal.TryParse(SalesPrice.Value, out SalesPriceAmount);
	      	if(SalesPriceAmount > 0) ws.Rows[28 + addRow].Cells[1].Value = SalesPriceAmount;
	      	
	      	if(!isRefi)
	      	{
	      		if(SalesPriceAmount > 0) ws.Rows[28 + addRow].Cells[5].Value = SalesPriceAmount;
	      	}
	      	
	      	ws.Rows[29 + addRow].Cells[3].Value = "Personal Property";
	      	val = 0;
					decimal.TryParse(PersonalProperty.Value, out val);
	      	if(val > 0) ws.Rows[29 + addRow].Cells[1].Value = val;
	      	if(!isRefi)
	      	{
	      		if(val > 0) ws.Rows[29 + addRow].Cells[4].Value = val;
	      	}
	      	
	      	ws.Rows[30 + addRow].Cells[3].Value = "Deposit including earnest money";
	      	val = 0;
					decimal.TryParse(Deposit.Value, out val);
	      	if(val > 0) ws.Rows[30 + addRow].Cells[2].Value = val;
	      	
	      	if(!isRefi)
	      	{
	      		if(val > 0) ws.Rows[30 + addRow].Cells[4].Value = val;
	      	}
	      	
	      	ws.Rows[31 + addRow].Cells[3].Value = "Loan Amount";
	      	val = 0;
					decimal.TryParse(LoanAmount.Value, out val);
	      	//ws.Rows[31 + addRow].Cells[1].Value = val;
	      	if(val > 0) ws.Rows[31 + addRow].Cells[2].Value = val;
	      	
	      	ws.Rows[32 + addRow].Cells[3].Value = "Existing Loan(s) Assumed or Taken Subject to ________";
	      	val = 0;
					decimal.TryParse(ExistingLoanTakenSubject.Value, out val);
	      	if(val > 0) ws.Rows[32 + addRow].Cells[1].Value = val;
	      	
	      	ws.Rows[33 + addRow].Cells[3].Value = "Excess Deposit";
	      	val = 0;
					decimal.TryParse(ExcessDeposit.Value, out val);
	      	if(val > 0) ws.Rows[33 + addRow].Cells[1].Value = val;
	      	if(!isRefi)
	      	{
	      		if(val > 0) ws.Rows[33 + addRow].Cells[4].Value = val;
	      	}
	      	
	      	ws.Rows[34 + addRow].Cells[3].Value = "Seller Closing Cost Credit";
	      	val = 0;
					decimal.TryParse(SellerClosingCostCredit.Value, out val);
	      	if(val > 0) ws.Rows[34 + addRow].Cells[2].Value = val;
	      	if(!isRefi)
	      	{
	      		if(val > 0) ws.Rows[34 + addRow].Cells[4].Value = val;
	      	}
	      	//ws.Rows[33 + addRow].Cells[4].Style.NumberFormat = "0.00";
	      	
	      	ws.Rows[36 + addRow].Cells[3].Value = "Prorations/Adjustments";
	      	ws.Rows[36 + addRow].Cells[3].Style.Font.Weight = ExcelFont.BoldWeight;
	      	
	      	//ws.Rows[37 + addRow].Cells[3].Value = "County Tax Last Year Billed Amount";
	      	
	      	val = 0;
					//decimal.TryParse(CountyTaxLastYearBilledAmount.Value, out val);
					//ws.Rows[37 + addRow].Cells[1].Value = val;
	      	//if(!isRefi)
	      	//{
	      	//	ws.Rows[37 + addRow].Cells[4].Value = val;
	      	//}
					addRow--;
	      	
	      	//Tax Proration
	      	ws.Rows[38 + addRow].Cells[3].Value = "Prorate County Taxes";

	      	decimal taxprorationvalue = 0;
					decimal.TryParse(ProrateCountyTaxes.Value, out taxprorationvalue);
					if(taxprorationvalue > 0) ws.Rows[38 + addRow].Cells[1].Value = taxprorationvalue;
	      	if(!isRefi)
	      	{
	      		if(taxprorationvalue > 0) ws.Rows[38 + addRow].Cells[4].Value = taxprorationvalue;
	      	}
	      	
	      	/*
	      	ws.Rows[37 + addRow].Cells[3].Value = "County Taxes from (date) to (date)";
	      	val = 0;
	      	ws.Rows[37 + addRow].Cells[1].Value = CountyTaxesFrom.Value + " - " + CountyTaxesTo.Value;
	      	if(!isRefi)
	      	{
	      		ws.Rows[37 + addRow].Cells[4].Value = CountyTaxesFrom.Value + " - " + CountyTaxesTo.Value;
	      	}
	      	*/

					if(!isRefi)
					{
		      	ws.Rows[39 + addRow].Cells[3].Value = "HOA dues from (date) to (date)";
		      	val = 0;
						decimal.TryParse(ProrateHOADues.Value, out val);
						//ws.Rows[40 + addRow].Cells[1].Value = val;
		      	if(val > 0) ws.Rows[39 + addRow].Cells[5].Value = val;
		      }
		      	
	      	ws.Rows[40 + addRow].Cells[3].Value = "Seller Credit";
	      	val = 0;
					decimal.TryParse(ProrateSellerCredit.Value, out val);
	      	if(val > 0) ws.Rows[40 + addRow].Cells[2].Value = val;
	      	
					if(!isRefi)
					{
	      		if(val > 0) ws.Rows[40 + addRow].Cells[4].Value = val;
	      	}
		      
	      	ws.Rows[41 + addRow].Cells[3].Value = "HOA Credit Fixed Amount";
	      	val = 0;
					decimal.TryParse(HOACreditFixedAmount.Value, out val);
	      	if(val > 0) ws.Rows[41 + addRow].Cells[1].Value = val;
	      	if(val > 0) ws.Rows[41 + addRow].Cells[5].Value = val;
		      
	      	ws.Rows[42 + addRow].Cells[3].Value = "Tax Proration Credit";
	      	val = 0;
					decimal.TryParse(TaxProrationCredit.Value, out val);
	      	if(val > 0) ws.Rows[42 + addRow].Cells[2].Value = val;
	      	if(val > 0) ws.Rows[42 + addRow].Cells[4].Value = val;
	      	
	      	addRow++;
	      	
	      	ws.Rows[43 + addRow].Cells[3].Value = "Loan Charges to (lender co.)";
	      	ws.Rows[43 + addRow].Cells[3].Style.Font.Weight = ExcelFont.BoldWeight;
	      	
	      	ws.Rows[44 + addRow].Cells[3].Value = "Points";
	      	val = 0;
					decimal.TryParse(Points.Value, out val);
	      	if(val > 0) ws.Rows[44 + addRow].Cells[1].Value = val;
	      	
	      	ws.Rows[45 + addRow].Cells[3].Value = "Application Fee";
	      	val = 0;
					decimal.TryParse(ApplicationFee.Value, out val);
	      	if(val > 0) ws.Rows[45 + addRow].Cells[1].Value = val;
	      	
	      	ws.Rows[46 + addRow].Cells[3].Value = "Origination Fee";
	      	val = 0;
					decimal.TryParse(OriginationFee.Value, out val);
	      	if(val > 0) ws.Rows[46 + addRow].Cells[1].Value = val;
	      	
	      	ws.Rows[47 + addRow].Cells[3].Value = "Underwriting Fee";
	      	val = 0;
					decimal.TryParse(UnderwritingFee.Value, out val);
	      	if(val > 0) ws.Rows[47 + addRow].Cells[1].Value = val;
	      	
	      	ws.Rows[48 + addRow].Cells[3].Value = "Mortgage Insurance Premium";
	      	val = 0;
					decimal.TryParse(MortgageInsurancePremium.Value, out val);
	      	if(val > 0) ws.Rows[48 + addRow].Cells[1].Value = val;
	      	
	      	ws.Rows[49 + addRow].Cells[3].Value = "Prepaid Interest";
	      	val = 0;
					decimal.TryParse(PrepaidInterest.Value, out val);
	      	if(val > 0) ws.Rows[49 + addRow].Cells[1].Value = val;
	      	
	      	addRow++;
	      	
	      	ws.Rows[50 + addRow].Cells[3].Value = "Other Loan Charges";
	      	ws.Rows[50 + addRow].Cells[3].Style.Font.Weight = ExcelFont.BoldWeight;
	      	
	      	ws.Rows[51 + addRow].Cells[3].Value = "Charges";
	      	val = 0;
					decimal.TryParse(OtherLoanCharges.Value, out val);
	      	if(val > 0) ws.Rows[51 + addRow].Cells[1].Value = val;
	      	
	      	ws.Rows[53 + addRow].Cells[3].Value = "Impounds";
	      	ws.Rows[53 + addRow].Cells[3].Style.Font.Weight = ExcelFont.BoldWeight;
	      	
	      	ws.Rows[54 + addRow].Cells[3].Value = "Homeowner's Insurance  _______ mo @ $ _______/mo";
	      	val = 0;
					decimal.TryParse(HomeownersInsurance.Value, out val);
	      	if(val > 0) ws.Rows[54 + addRow].Cells[1].Value = val;
	      	
	      	ws.Rows[55 + addRow].Cells[3].Value = "Mortgage Insurance  _______ mo @ $ _______/mo";
	      	val = 0;
					decimal.TryParse(MortgageInsurance.Value, out val);
	      	if(val > 0) ws.Rows[55 + addRow].Cells[1].Value = val;
	      	
	      	ws.Rows[56 + addRow].Cells[3].Value = "City/town taxes  _______ mo @ $ _______/mo";
	      	val = 0;
					decimal.TryParse(ImpoundsCityTownTaxes.Value, out val);
	      	if(val > 0) ws.Rows[56 + addRow].Cells[1].Value = val;
	      	/*
	      	ws.Rows[57 + addRow].Cells[3].Value = "County Taxes  _______ mo @ $ _______/mo";
	      	val = 0;
					//decimal.TryParse(CountyTaxes.Value, out val);
	      	if(val > 0) ws.Rows[57 + addRow].Cells[1].Value = val;
	      	*/
	      	addRow--;
	      	ws.Rows[58 + addRow].Cells[3].Value = "Aggregate Adjustment";

	      	val = 0;
					decimal.TryParse(AggregateAdjustment.Value, out val);
	      	if(val > 0) ws.Rows[58 + addRow].Cells[2].Value = val;
	      	//ws.Rows[39+ addRow].Cells[4].Value = val;

					addRow++;
	      	
	      	ws.Rows[59 + addRow].Cells[3].Value = "Title Charges & Escrow / Settlement Charges";
	      	ws.Rows[59 + addRow].Cells[3].Style.Font.Weight = ExcelFont.BoldWeight;
	      	
					if(!isRefi)
					{
		      	ws.Rows[60 + addRow].Cells[3].Value = "Owner's Title Insurance ($ amount) to _______";
		      	val = 0;
						decimal.TryParse(OwnersTitleInsurance.Value, out val);
		      	//ws.Rows[58 + addRow].Cells[1].Value = val;
	      		if(val > 0) ws.Rows[60 + addRow].Cells[4].Value = val;
	      	
		      	ws.Rows[61 + addRow].Cells[3].Value = "Owner's Policy Endorsement(s) ___________";
		      	val = 0;
						decimal.TryParse(OwnersPolicyEndorsement.Value, out val);
		      	//ws.Rows[61 + addRow].Cells[1].Value = val;
		      	if(val > 0) ws.Rows[61 + addRow].Cells[4].Value = val;
	      	}
	      	
	      	ws.Rows[62 + addRow].Cells[3].Value = "Loan Policy of Title Insurance ($ amount) to _______";
	      	val = 0;
					decimal.TryParse(LoanPolicyTitleInsurance.Value, out val);
	      	if(val > 0) ws.Rows[62 + addRow].Cells[1].Value = val;
	      	//ws.Rows[60 + addRow].Cells[4].Value = val;
	      	
	      	ws.Rows[63 + addRow].Cells[3].Value = "Loan Policy Endorsement(s) ____________";
	      	val = 0;
					decimal.TryParse(LoanPolicyEndorsement.Value, out val);
	      	if(val > 0) ws.Rows[63 + addRow].Cells[1].Value = val;
	      	//ws.Rows[61 + addRow].Cells[4].Value = val;
		      	
					if(!isRefi)
					{
		      	ws.Rows[64 + addRow].Cells[3].Value = "Title Abstract Fee ______________";
		      	val = 0;
						decimal.TryParse(TitleAbstractFee.Value, out val);
		      	//ws.Rows[62 + addRow].Cells[1].Value = val;
		      	if(val > 0) ws.Rows[64 + addRow].Cells[4].Value = val;
		      }
	      	
	      	ws.Rows[65 + addRow].Cells[3].Value = "Policy Update Fee ___________";
	      	val = 0;
					decimal.TryParse(PolicyUpdateFee.Value, out val);
	      	if(val > 0) ws.Rows[65 + addRow].Cells[1].Value = val;
	      	
	      	addRow++;
	      	
	      	ws.Rows[65 + addRow].Cells[3].Value = "Escrow / Settlement Closing Fee to ___________";
	      	val = 0;
					decimal.TryParse(EscrowSettlementFee.Value, out val);
	      	if(val > 0) ws.Rows[65 + addRow].Cells[1].Value = val;
	      	//ws.Rows[65 + addRow].Cells[4].Value = val;
	      	
	      	addRow++;
	      	
	      	ws.Rows[65 + addRow].Cells[3].Value = "IL CPL/State Policy Fee to ___________";
	      	val = 0;
					decimal.TryParse(CPLPolicyBuyerFee.Value, out val);
	      	if(val > 0) ws.Rows[65 + addRow].Cells[1].Value = val;

	      	val = 0;
					decimal.TryParse(CPLPolicySellerFee.Value, out val);
	      	if(val > 0) ws.Rows[65 + addRow].Cells[4].Value = val;
	      	
	      	addRow++;
	      	
	      	ws.Rows[65 + addRow].Cells[3].Value = "Settlement Closing Fee to ___________";
	      	val = 0;
					decimal.TryParse(SettlementClosingFee.Value, out val);
					
					if(SettlementClosingFeeSplit.Checked)
					{
						val = val / 2;
					}
	      	if(val > 0) ws.Rows[65 + addRow].Cells[1].Value = val;
	      	
					if(SettlementClosingFeeSplit.Checked)
					{
	      		if(val > 0) ws.Rows[65 + addRow].Cells[4].Value = val;
	      	}

	      	
	      	ws.Rows[66 + addRow].Cells[3].Value = "Notary Fee to _________";
	      	val = 0;
					decimal.TryParse(NotaryFee.Value, out val);
	      	if(val > 0) ws.Rows[66 + addRow].Cells[1].Value = val;
	      	//ws.Rows[64 + addRow].Cells[4].Value = val;
	      	
	      	ws.Rows[67 + addRow].Cells[3].Value = "Signing Fee to __________";
	      	val = 0;
					decimal.TryParse(SigningFee.Value, out val);
	      	if(val > 0) ws.Rows[67 + addRow].Cells[1].Value = val;
	      	//ws.Rows[65 + addRow].Cells[4].Value = val;
	      	
	      	addRow++;

					if(!isRefi)
					{
		      	ws.Rows[68 + addRow].Cells[3].Value = "Commission";
		      	ws.Rows[68 + addRow].Cells[3].Style.Font.Weight = ExcelFont.BoldWeight;
		      	
		      	ws.Rows[69 + addRow].Cells[3].Value = "Real Estate Commission to ______________";
		      	val = 0;
						decimal.TryParse(RealEstateCommission1.Value, out val);
		      	if(val > 0) ws.Rows[69 + addRow].Cells[4].Value = val;
		      	//ws.Rows[69 + addRow].Cells[4].Value = val;
		      	
		      	ws.Rows[70 + addRow].Cells[3].Value = "Real Estate Commission to ______________";
		      	val = 0;
						decimal.TryParse(RealEstateCommission2.Value, out val);
		      	if(val > 0) ws.Rows[70 + addRow].Cells[4].Value = val;
	      	
		      	ws.Rows[71 + addRow].Cells[3].Value = "Other";
		      	val = 0;
						decimal.TryParse(CommissionOther.Value, out val);
		      	if(val > 0) ws.Rows[71 + addRow].Cells[4].Value = val;
		      }
	      	addRow++;
	      	ws.Rows[72 + addRow].Cells[3].Value = "Government Recording and Transfer Charges";
	      	ws.Rows[72 + addRow].Cells[3].Style.Font.Weight = ExcelFont.BoldWeight;
	      	
	      	ws.Rows[73 + addRow].Cells[3].Value = "Recording Fees (Deed) to _______";
	      	val = 0;
					decimal.TryParse(RecordingFeesDeedBuyer.Value, out val);
	      	if(val > 0) ws.Rows[73 + addRow].Cells[1].Value = val;
	      	
					if(!isRefi)
					{
		      	val = 0;
						decimal.TryParse(RecordingFeesDeedSeller.Value, out val);
		      	if(val > 0) ws.Rows[73 + addRow].Cells[4].Value = val;
		      }
	      	
	      	ws.Rows[74 + addRow].Cells[3].Value = "Recording Fees (Mortgage/Deed of Trust) to _____";
	      	val = 0;
					decimal.TryParse(RecordingFeesMortgageDeedTrustBuyer.Value, out val);
	      	if(val > 0) ws.Rows[74 + addRow].Cells[1].Value = val;
	      	
					if(!isRefi)
					{
		      	val = 0;
						decimal.TryParse(RecordingFeesMortgageDeedTrustSeller.Value, out val);
		      	if(val > 0) ws.Rows[74 + addRow].Cells[4].Value = val;
		      }
	      		      	
					if(!isRefi)
					{
		      	ws.Rows[75 + addRow].Cells[3].Value = "County Stamps __________";
		      	val = 0;
						decimal.TryParse(StampsCounty.Value, out val);
		      	if(val > 0) ws.Rows[75 + addRow].Cells[4].Value = val;
		      	
		      	addRow++;
		      	
		      	ws.Rows[75 + addRow].Cells[3].Value = "State Stamps __________";
		      	val = 0;
						decimal.TryParse(StampsState.Value, out val);
		      	if(val > 0) ws.Rows[75 + addRow].Cells[4].Value = val;
		      }
	      	
	      	ws.Rows[76 + addRow].Cells[3].Value = "Transfer Tax to ______________";
	      	val = 0;
					decimal.TryParse(TransferTaxBuyer.Value, out val);
	      	if(val > 0) ws.Rows[76 + addRow].Cells[1].Value = val;
	      	
					if(!isRefi)
					{
		      	val = 0;
						decimal.TryParse(TransferTaxSeller.Value, out val);
		      	if(val > 0) ws.Rows[76 + addRow].Cells[4].Value = val;
		      }
	      	
	      	ws.Rows[77 + addRow].Cells[3].Value = "Village Stamps ______________";
	      	val = 0;
					decimal.TryParse(VillageStampsBuyer.Value, out val);
	      	if(val > 0) ws.Rows[77 + addRow].Cells[1].Value = val;
	      	
	      	if(!isRefi)
					{
		      	val = 0;
						decimal.TryParse(VillageStampsSeller.Value, out val);
		      	if(val > 0) ws.Rows[77 + addRow].Cells[4].Value = val;
					}
	      	
	      	ws.Rows[79 + addRow].Cells[3].Value = "Payoff(s)";
	      	ws.Rows[79 + addRow].Cells[3].Style.Font.Weight = ExcelFont.BoldWeight;
	      	
					if(!isRefi)
					{
		      	ws.Rows[80 + addRow].Cells[3].Value = "Lender: Payoff Lender Co.";
		      	val = 0;
						decimal.TryParse(PayoffLenderCompany1.Value, out val);
		      	//ws.Rows[80 + addRow].Cells[1].Value = val;
		      	if(val > 0) ws.Rows[80 + addRow].Cells[4].Value = val;
		      	
		      	ws.Rows[81 + addRow].Cells[3].Value = "                Principal Balance ($ amount)";
		      	val = 0;
						decimal.TryParse(PrincipalBalance1.Value, out val);
		      	//ws.Rows[81 + addRow].Cells[1].Value = val;
		      	if(val > 0) ws.Rows[81 + addRow].Cells[4].Value = val;
		      	
		      	ws.Rows[82 + addRow].Cells[3].Value = "                Interest on Payoff Loan ($ amount/day)";
		      	val = 0;
						decimal.TryParse(InterestonPayoffLoan1.Value, out val);
		      	//ws.Rows[82 + addRow].Cells[1].Value = val;
		      	if(val > 0) ws.Rows[82 + addRow].Cells[4].Value = val;
		      	
		      	ws.Rows[83 + addRow].Cells[3].Value = "                Additional Payoff fees/Recording Fee/Wire Fee";
		      	val = 0;
						decimal.TryParse(AdditionalPayoffFees1.Value, out val);
		      	if(val > 0) ws.Rows[83 + addRow].Cells[4].Value = val;
		      	
		      	ws.Rows[84 + addRow].Cells[3].Value = "Lender: Payoff Lender Co.";
		      	val = 0;
						decimal.TryParse(PayoffLenderCompany2.Value, out val);
		      	//ws.Rows[84 + addRow].Cells[1].Value = val;
		      	if(val > 0) ws.Rows[84 + addRow].Cells[4].Value = val;
		      	
		      	ws.Rows[85 + addRow].Cells[3].Value = "                Principal Balance ($ amount)";
		      	val = 0;
						decimal.TryParse(PrincipalBalance2.Value, out val);
		      	//ws.Rows[85 + addRow].Cells[1].Value = val;
		      	if(val > 0) ws.Rows[85 + addRow].Cells[4].Value = val;
		      	
		      	ws.Rows[86 + addRow].Cells[3].Value = "                Interest on Payoff Loan ($ amount/day)";
		      	val = 0;
						decimal.TryParse(InterestonPayoffLoan2.Value, out val);
		      	//ws.Rows[86 + addRow].Cells[1].Value = val;
		      	if(val > 0) ws.Rows[86 + addRow].Cells[4].Value = val;
		      	
		      	ws.Rows[87 + addRow].Cells[3].Value = "                Additional Payoff fees/Recording Fee/Wire Fee";
		      	val = 0;
						decimal.TryParse(AdditionalPayoffFees.Value, out val);
		      	if(val > 0) ws.Rows[87 + addRow].Cells[4].Value = val;
	      	}
	      	
	      	ws.Rows[89 + addRow].Cells[3].Value = "Miscellaneous ";
	      	ws.Rows[89 + addRow].Cells[3].Style.Font.Weight = ExcelFont.BoldWeight;
	      	
	      	ws.Rows[90 + addRow].Cells[3].Value = "Pest Inspection Fee to ___________";
	      	val = 0;
					decimal.TryParse(PestInspectionFee.Value, out val);
	      	if(val > 0) ws.Rows[90 + addRow].Cells[1].Value = val;
	      	//ws.Rows[90 + addRow].Cells[4].Value = val;
	      	
	      	if(!isRefi)
	      	{
		      	ws.Rows[91 + addRow].Cells[3].Value = "Survey Fee to ______________";
		      	val = 0;
						decimal.TryParse(SurveyFee.Value, out val);
		      	//ws.Rows[91 + addRow].Cells[1].Value = val;
		      	if(val > 0) ws.Rows[91 + addRow].Cells[4].Value = val;
	      	}
	      	
	      	ws.Rows[92 + addRow].Cells[3].Value = "Homeowner's insurance premium to ___________";
	      	val = 0;
					decimal.TryParse(HomeownersInsurancePremium.Value, out val);
	      	if(val > 0) ws.Rows[92 + addRow].Cells[1].Value = val;
	      	//ws.Rows[92 + addRow].Cells[4].Value = val;
	      	
	      	ws.Rows[93 + addRow].Cells[3].Value = "Home Inspection Fee to ___________";
	      	val = 0;
					decimal.TryParse(HomeInspectionFee.Value, out val);
	      	if(val > 0) ws.Rows[93 + addRow].Cells[1].Value = val;
	      	//ws.Rows[93 + addRow].Cells[4].Value = val;
	      	
	      	if(!isRefi)
	      	{
		      	ws.Rows[94 + addRow].Cells[3].Value = "Home Warranty Fee to ____________";
		      	val = 0;
						decimal.TryParse(HomeWarrantyFee.Value, out val);
		      	//ws.Rows[94 + addRow].Cells[1].Value = val;
		      	if(val > 0) ws.Rows[94 + addRow].Cells[4].Value = val;
	      	
		      	ws.Rows[95 + addRow].Cells[3].Value = "HOA dues to ___________";
		      	val = 0;
						decimal.TryParse(HOADues.Value, out val);
		      	//ws.Rows[95 + addRow].Cells[1].Value = val;
		      	if(val > 0) ws.Rows[95 + addRow].Cells[4].Value = val;
	      	}
	      	
	      	ws.Rows[96 + addRow].Cells[3].Value = "Transfer Fee to Management Co.";
	      	val = 0;
					decimal.TryParse(TransferFeeToManagementCompany.Value, out val);
	      	if(val > 0) ws.Rows[96 + addRow].Cells[1].Value = val;
	      	//ws.Rows[96 + addRow].Cells[4].Value = val;
	      	
	      	ws.Rows[97 + addRow].Cells[3].Value = "Other";
	      	val = 0;
					decimal.TryParse(SpecialHazardDisclosure.Value, out val);
	      	if(val > 0) ws.Rows[97 + addRow].Cells[1].Value = val;
	      	if(!isRefi)
	      	{
	      		if(val > 0) ws.Rows[97 + addRow].Cells[4].Value = val;
	      	
		      	ws.Rows[98 + addRow].Cells[3].Value = "Utility Payment to ___________";
		      	val = 0;
						decimal.TryParse(UtilityPayment.Value, out val);
		      	//ws.Rows[98 + addRow].Cells[1].Value = val;
		      	if(val > 0) ws.Rows[98 + addRow].Cells[4].Value = val;
		      }
	      	
	      	//ws.Rows[111 + addRow].Cells[3].Value = "Assessments";
	      	ws.Rows[99 + addRow].Cells[3].Value = "City/town taxes";
	      	val = 0;
					decimal.TryParse(CityTownTaxes.Value, out val);
	      	if(val > 0) ws.Rows[99 + addRow].Cells[1].Value = val;
	      	if(val > 0) ws.Rows[99 + addRow].Cells[4].Value = val;
	      	
	      	//ws.Rows[114 + addRow].Cells[3].Value = "County Taxes/County Property taxes";
	      	ws.Rows[100 + addRow].Cells[3].Value = "Buyer Attorney fees to ___________";
	      	val = 0;
					decimal.TryParse(BuyerAttorneyFees.Value, out val);
	      	//ws.Rows[100 + addRow].Cells[1].Value = val;
	      	if(val > 0) ws.Rows[100 + addRow].Cells[1].Value = val;
	      	
	      	ws.Rows[101 + addRow].Cells[3].Value = "Seller Attorney fees to __________";
	      	val = 0;
					decimal.TryParse(SellerAttorneyFees.Value, out val);
	      	//ws.Rows[101 + addRow].Cells[1].Value = val;
	      	if(val > 0) ws.Rows[101 + addRow].Cells[4].Value = val;
	      	
	      	ws.Rows[103 + addRow].Cells[1].Value = "Borrower/Buyer";
	      	ws.Rows[103 + addRow].Cells[4].Value = "Seller";
	      	
	        ws.Rows[103 + addRow].Cells[0].Style.FillPattern.SetSolid(System.Drawing.Color.LightGray);
	        ws.Rows[104 + addRow].Cells[0].Style.FillPattern.SetSolid(System.Drawing.Color.LightGray);
	        ws.Rows[103 + addRow].Cells[1].Style.FillPattern.SetSolid(System.Drawing.Color.LightGray);
	        ws.Rows[104 + addRow].Cells[1].Style.FillPattern.SetSolid(System.Drawing.Color.LightGray);
	        ws.Rows[103 + addRow].Cells[2].Style.FillPattern.SetSolid(System.Drawing.Color.LightGray);
	        ws.Rows[104 + addRow].Cells[2].Style.FillPattern.SetSolid(System.Drawing.Color.LightGray);
	        ws.Rows[103 + addRow].Cells[3].Style.FillPattern.SetSolid(System.Drawing.Color.LightGray);
	        ws.Rows[104 + addRow].Cells[3].Style.FillPattern.SetSolid(System.Drawing.Color.LightGray);
	        ws.Rows[103 + addRow].Cells[4].Style.FillPattern.SetSolid(System.Drawing.Color.LightGray);
	        ws.Rows[104 + addRow].Cells[4].Style.FillPattern.SetSolid(System.Drawing.Color.LightGray);
	        ws.Rows[103 + addRow].Cells[5].Style.FillPattern.SetSolid(System.Drawing.Color.LightGray);
	        ws.Rows[104 + addRow].Cells[5].Style.FillPattern.SetSolid(System.Drawing.Color.LightGray);
	      	ws.Rows[103 + addRow].Cells[1].Value = "Borrower/Buyer";
	      	ws.Cells.GetSubrangeAbsolute((103 + addRow), 1, (103 + addRow), 2).Merged = true;
	      	ws.Rows[103 + addRow].Cells[1].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
	      	ws.Rows[103 + addRow].Cells[0].Style.Font.Weight = ExcelFont.BoldWeight;
	      	
	      	ws.Rows[103 + addRow].Cells[3].Value = "Description";
	      	ws.Rows[103 + addRow].Cells[3].Style.Font.Weight = ExcelFont.BoldWeight;
	
	      	ws.Rows[103 + addRow].Cells[4].Value = "Seller";
	      	ws.Cells.GetSubrangeAbsolute((103 + addRow), 4, (103 + addRow), 5).Merged = true;
	      	ws.Rows[103 + addRow].Cells[4].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
	      	ws.Rows[103 + addRow].Cells[4].Style.Font.Weight = ExcelFont.BoldWeight;
	      	
	      	ws.Rows[104 + addRow].Cells[1].Value = "Debit";
	      	ws.Rows[104 + addRow].Cells[1].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
	      	ws.Rows[104 + addRow].Cells[1].Style.Font.Weight = ExcelFont.BoldWeight;
	      	
	      	ws.Rows[104 + addRow].Cells[2].Value = "Credit";
	      	ws.Rows[104 + addRow].Cells[2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
	      	ws.Rows[104 + addRow].Cells[2].Style.Font.Weight = ExcelFont.BoldWeight;
	      	
	      	ws.Rows[104 + addRow].Cells[4].Value = "Debit";
	      	ws.Rows[104 + addRow].Cells[4].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
	      	ws.Rows[104 + addRow].Cells[4].Style.Font.Weight = ExcelFont.BoldWeight;
	      	
	      	ws.Rows[104 + addRow].Cells[5].Value = "Credit";
	      	ws.Rows[104 + addRow].Cells[5].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
	      	ws.Rows[104 + addRow].Cells[5].Style.Font.Weight = ExcelFont.BoldWeight;	
   	
      	/*
	      	XmlDocument doc = new XmlDocument();
	      	string url = "http://maps.googleapis.com/maps/api/geocode/xml?address=" + PropertyAddress.Value + "&key=";
	
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
	        
	        //ws.Rows[104 + addRow].Cells[3].Value = "fred";
	        string city = "";
	        XmlNode citynode = doc.SelectSingleNode("/GeocodeResponse/result/address_component[type='locality']");
	        
	        if(citynode != null)
	        {
	        	city = citynode.SelectSingleNode("long_name").InnerText;
	        }
	        	
	        string county = "";
	        XmlNode countynode = doc.SelectSingleNode("/GeocodeResponse/result/address_component[type='administrative_area_level_2']");
	        
	        if(countynode != null)
	        {
	        	county = countynode.SelectSingleNode("long_name").InnerText;
	        }
	        	
        	if(!city.Equals(""))
        	{
						//Affinity.TaxingDistrict taxingdistrict = new TaxingDistrict(this.phreezer);
						//taxingdistrict.Load(city);
						
						MySqlDataReader reader = this.phreezer.ExecuteReader("select * from `taxing_district` t where t.taxing_district = '" + city + "'");
            if (reader.Read())
            {
		        	string liability_party = reader["Liable_party"].ToString();
		        	string stamp_exempt = reader["Stamp_exempt"].ToString();
		        	string amount = reader["Amount"].ToString().Replace("$", "");
		        	string[] amountArray = amount.Split('/');
		        	decimal stamptaxrate = 0;
		        	decimal stamptaxper = 0;
		        	
		        	decimal.TryParse(amountArray[0], out stamptaxrate);
		        	decimal.TryParse(amountArray[1], out stamptaxper);
		        	decimal stamptax = (SalesPriceAmount / stamptaxper) * stamptaxrate;
		        	ws.Rows[104 + addRow].Cells[3].Value = stamptax;
            }
						reader.Close();
	        	//ws.Rows[104 + addRow].Cells[3].Value = reader["Liable_party"].ToString();
	        }
					*/
	      	/*
	      	      Affinity.TitleFees tf = new Affinity.TitleFees(this.phreezer);
					   		Affinity.TitleFeesCriteria tfc = new Affinity.TitleFeesCriteria();
					      tfc.FeeType = "Insurance Rate";
					      tf.Query(tfc);
					*/
	
	
	      	/*   	
	      	ws.Rows[105 + addRow].Cells[3].Value = "Sales Price";
	      	ws.Rows[105 + addRow].Cells[4].Formula = "=E" + (29 + addRow).ToString();
	      	ws.Rows[106 + addRow].Cells[3].Value = "Seller Credit";
	      	ws.Rows[106 + addRow].Cells[1].Formula = "=((E" + (29 + addRow).ToString() + " * .1) + E" + (40 + addRow).ToString() + ")";
	      	ws.Rows[106 + addRow].Cells[4].Formula = "=((E" + (29 + addRow).ToString() + " * .1) + E" + (40 + addRow).ToString() + ")";
	      	ws.Rows[107 + addRow].Cells[3].Value = "1st Mortgage Payoff";
	      	
	      	if(IsShortSale.Value.Equals("yes"))
	      	{
	      		ws.Rows[107 + addRow].Cells[1].Formula = "=((Gross Amount due to seller) - (E" + (35 + addRow).ToString() + " + E" + (35 + addRow).ToString() + " + E" + (35 + addRow).ToString() + ") - (E" + (35 + addRow).ToString() + " + E" + (60 + addRow).ToString() + " + B" + (107 + addRow).ToString() + " + E" + (60 + addRow).ToString() + " + E" + (38 + addRow).ToString() + "))";
	      		//ws.Rows[107 + addRow].Cells[4].Formula = "=((Gross Amount due to seller) - (E" + (35 + addRow).ToString() + " + E" + (35 + addRow).ToString() + " + E" + (35 + addRow).ToString() + ") - (E" + (35 + addRow).ToString() + " + E" + (60 + addRow).ToString() + " + B" + (107 + addRow).ToString() + " + E" + (60 + addRow).ToString() + " + E" + (38 + addRow).ToString() + "))";
	      	}
	      	else
	      	{
	      		ws.Rows[107 + addRow].Cells[1].Formula = "=E81";
	      		ws.Rows[107 + addRow].Cells[4].Formula = "=E81";
	      	}
	      	
	      	ws.Rows[108 + addRow].Cells[3].Value = "2nd Mortgage Payoff";
	      	ws.Rows[108 + addRow].Cells[1].Formula = "=E" + (85 + addRow).ToString();
	      	//ws.Rows[108 + addRow].Cells[4].Formula = "=E" + (85 + addRow).ToString();
	      	ws.Rows[109 + addRow].Cells[3].Value = "Other Liens";
	      	ws.Rows[109 + addRow].Cells[1].Formula = "=(0+0)";
	      	ws.Rows[109 + addRow].Cells[4].Formula = "=(0+0)";
	      	ws.Rows[110 + addRow].Cells[3].Value = "Real Estate Commission";
	      	ws.Rows[110 + addRow].Cells[4].Formula = "=E" + (69 + addRow).ToString();
	      	ws.Rows[111 + addRow].Cells[3].Value = "Title Charges";
	      	ws.Rows[111 + addRow].Cells[1].Formula = "=SUM(E" + (59 + addRow).ToString() + ":E" + (66 + addRow).ToString() + ")";
	      	ws.Rows[111 + addRow].Cells[4].Formula = "=SUM(E" + (59 + addRow).ToString() + ":E" + (66 + addRow).ToString() + ")";
	      	ws.Rows[112 + addRow].Cells[3].Value = "Attorney Fees";
	      	ws.Rows[112 + addRow].Cells[1].Formula = "=E" + (102 + addRow).ToString();
	      	ws.Rows[112 + addRow].Cells[4].Formula = "=E" + (102 + addRow).ToString();
	      	ws.Rows[113 + addRow].Cells[3].Value = "Transfer Taxes";
	      	ws.Rows[113 + addRow].Cells[1].Formula = "=SUM(E" + (77 + addRow).ToString() + ":E" + (78 + addRow).ToString() + ")";
	      	ws.Rows[113 + addRow].Cells[4].Formula = "=SUM(E" + (77 + addRow).ToString() + ":E" + (78 + addRow).ToString() + ")";
	      	ws.Rows[114 + addRow].Cells[3].Value = "Tax Proration";
	      		      	
	      	DateTime StartDate = DateTime.Parse("1/1/" + DateTime.Now.Year.ToString());
					DateTime EndDate = DateTime.Now;
					double days = (EndDate - StartDate).TotalDays;
					/*
					decimal countytaxes = 0;
					if(county.Equals("cook")
					{ 
						taxprorationvalue = (((countytaxes * .45) + (countytaxes * 1)) /365) * (closing date - 1/1/2015 + 1day) + (Fixed Proration to add)
					
					else 
					
					(Property Tax * Tax Proration (100%)) / 365 * (closing date - 1/1/2014 + 1day) + (Fixed Proration to add)
	      	
	      	ws.Rows[114 + addRow].Cells[1].Formula = "=(0+0)";
	      	ws.Rows[114 + addRow].Cells[4].Formula = "=(0+0)";
	      	ws.Rows[115 + addRow].Cells[3].Value = "Other Fees";
	      	ws.Rows[115 + addRow].Cells[1].Formula = "=(0+0)";
	      	ws.Rows[115 + addRow].Cells[4].Formula = "=(0+0)";
	      	
	      	
	      	ws.Rows[117 + addRow].Cells[3].Value = "Subtotals";
	      	ws.Rows[117 + addRow].Cells[1].Formula = "=SUM(B107:B116)";
	      	ws.Rows[117 + addRow].Cells[4].Formula = "=SUM(E107:E116)";
	      	
	      	ws.Rows[107 + addRow].Cells[3].Value = "Due From/To Borrower";
	      	ws.Rows[107 + addRow].Cells[1].Formula = "=(0+0)";
	      	ws.Rows[107 + addRow].Cells[4].Formula = "=(0+0)";
	      	ws.Rows[108 + addRow].Cells[3].Value = "Due From/To Seller";
	      	ws.Rows[108 + addRow].Cells[1].Formula = "=(0+0)";
	      	ws.Rows[108 + addRow].Cells[4].Formula = "=(0+0)";
	      	*/
					//cr = ws.Cells.GetSubrange("B" + (21 + addRow).ToString(), "B" + (103 + addRow).ToString());
	      	//cr.Style.NumberFormat = "_($* #,##0.00_)";
      		//cr.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;

					cr = ws.Cells.GetSubrange("B" + (106 + addRow).ToString(), "B" + (110 + addRow).ToString());
	      	cr.Style.NumberFormat = "_($* #,##0.00_)";
      		cr.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;

					//cr = ws.Cells.GetSubrange("C" + (21 + addRow).ToString(), "C" + (103 + addRow).ToString());
	      	//cr.Style.NumberFormat = "_($* #,##0.00_)";
      		//cr.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;

					cr = ws.Cells.GetSubrange("C" + (106 + addRow).ToString(), "C" + (110 + addRow).ToString());
	      	cr.Style.NumberFormat = "_($* #,##0.00_)";
      		cr.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;

					//cr = ws.Cells.GetSubrange("E" + (21 + addRow).ToString(), "E" + (103 + addRow).ToString());
	      	//cr.Style.NumberFormat = "_($* #,##0.00_)";
      		//cr.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;

					cr = ws.Cells.GetSubrange("E" + (106 + addRow).ToString(), "E" + (110 + addRow).ToString());
	      	cr.Style.NumberFormat = "_($* #,##0.00_)";
      		cr.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;

					//cr = ws.Cells.GetSubrange("F" + (21 + addRow).ToString(), "F" + (103 + addRow).ToString());
	      	//cr.Style.NumberFormat = "_($* #,##0.00_)";
      		//cr.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;

					cr = ws.Cells.GetSubrange("F" + (106 + addRow).ToString(), "F" + (110 + addRow).ToString());
	      	cr.Style.NumberFormat = "_($* #,##0.00_)";
      		cr.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;

	      	ws.Rows[105 + addRow].Cells[3].Value = "SubTotals";
	      	ws.Rows[105 + addRow].Cells[1].Formula = "=SUM(B28:B" + (103 + addRow).ToString() + ")";
	      	ws.Rows[105 + addRow].Cells[2].Formula = "=SUM(C28:C" + (103 + addRow).ToString() + ")";
	      	ws.Rows[105 + addRow].Cells[4].Formula = "=SUM(E28:E" + (103 + addRow).ToString() + ")";
	      	ws.Rows[105 + addRow].Cells[5].Formula = "=SUM(F28:F" + (103 + addRow).ToString() + ")";
	      	ws.Rows[106 + addRow].Cells[3].Value = "Due From/To Borrower";
	      	ws.Rows[106 + addRow].Cells[2].Formula = "=(B" + (106 + addRow).ToString() + " - C" + (106 + addRow).ToString() + ")";
	      	ws.Rows[107 + addRow].Cells[3].Value = "Due From/To Seller";
	      	ws.Rows[107 + addRow].Cells[4].Formula = "=(F" + (106 + addRow).ToString() + " - E" + (106 + addRow).ToString() + ")";
	      	ws.Rows[108 + addRow].Cells[3].Value = "Totals";
	      	ws.Rows[108 + addRow].Cells[1].Formula = "=SUM(B" + (106 + addRow).ToString() + ":B" + (108 + addRow).ToString() + ")";
	      	ws.Rows[108 + addRow].Cells[2].Formula = "=SUM(C" + (106 + addRow).ToString() + ":C" + (108 + addRow).ToString() + ")";
	      	ws.Rows[108 + addRow].Cells[4].Formula = "=SUM(E" + (106 + addRow).ToString() + ":E" + (108 + addRow).ToString() + ")";
	      	ws.Rows[108 + addRow].Cells[5].Formula = "=SUM(F" + (106 + addRow).ToString() + ":F" + (108 + addRow).ToString() + ")";
	      	
          row = ws.Rows[111 + addRow];
	      	row.Cells[0].Value = "Buyer Signature: ";
          row.Cells[4].Value = "Date: ";
          row.Cells[0].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[1].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[2].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[3].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[4].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[5].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
	      	
          row = ws.Rows[114 + addRow];
	      	row.Cells[0].Value = "Seller Signature: ";
          row.Cells[4].Value = "Date: ";
          row.Cells[0].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[1].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[2].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[3].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[4].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
          row.Cells[5].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Medium);
	      	
	      	
	      	ws.Rows[116 + addRow].Cells[3].Value = "Acknowledgement";
	      	ws.Rows[116 + addRow].Cells[3].Style.Font.Weight = ExcelFont.BoldWeight;      	
	      	ws.Rows[117 + addRow].Cells[0].Value = "We/I have carefully reviewed the ALTA Settlement Statement and find it to be a true and accurate statement";
	      	ws.Rows[118 + addRow].Cells[0].Value = "of all receipts and disbursements made on my account or by me in this transaction and further certify";
	      	ws.Rows[119 + addRow].Cells[0].Value = "that I have received a copy of the ALTA Settlement Statement.  We/I authorize Affinity Title Services, LLC.";
	      	ws.Rows[120 + addRow].Cells[0].Value = "to cause the funds to be disbursed in accordance with this statement.";
	

	
	
	                                                //cell = row.Cells[1];
	                                                //cell.Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Thick);
	                                                //cell.Style.WrapText = false;
	
	                                            
	
	                                                        //Int64 n = 0;
	                                                        //Int64.TryParse("1", out n);
	                                                        //cell.Value = n;
	
	                                                        //Decimal d = 0;
	                                                        //Decimal.TryParse("2", out d);
	                                                        //cell.Value = d;
	                                                        //cell.Style.NumberFormat = "#,##0.00";
	
	                                                    //cell.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;
	
	                                                    //DateTime dt = DateTime.Now;
	                                                    //DateTime.TryParse("1/1/2014", out dt);
	                                                    //cell.Value = dt;
	
	                                                    //cell.Style.NumberFormat = "m/d/yyyy";
	                                                    //cell.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;
	
	
	
	                                    //ws.Rows[2].Style.Font.Color = System.Drawing.Color.White;
	                                    //ws.Rows[2].Style.Font.Weight = ExcelFont.BoldWeight;
	
	                                    // setup field types
	
	
	                                        //ws.Columns[1].AutoFit();
	
	                        //s.Append("<Row>");
	                        /*
	                        ExcelRow row1 = ws.Rows[1];
	                        row1.Style.WrapText = false;
	                        row1.Height = 250;
	                        int j = 0;
	
	                        ExcelCell cell2 = row1.Cells[1];
	                        ws.Columns[1].AutoFit();
	                        cell2.Style.Borders.SetBorders(MultipleBorders.Outside, System.Drawing.Color.Black, LineStyle.Thin);
	                        cell2.Style.WrapText = false;
	
	                        Int64 n1 = 0;
	                        Int64.TryParse("1", out n1);
	                        cell2.Value = n1;
	
	                        Decimal dc = 0;
	                        Decimal.TryParse("1", out dc);
	                        cell2.Value = dc;
	                        cell2.Style.NumberFormat = "#,##0.00";
	
	                        cell2.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;
	
	                        DateTime dt1 = DateTime.Now;
	                        DateTime.TryParse("1/1/2015", out dt1);
	                        cell2.Value = dt1;
	
	                        cell2.Style.NumberFormat = "m/d/yyyy";
	                        cell2.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;
	                        */
	                        
	                        File.Delete("c:\\Web\\Temp\\HUDCalculator.xls");
	
	                        file.SaveXls("c:\\Web\\Temp\\HUDCalculator.xls");
	
	
					
					Response.ContentType = "application/vnd.ms-excel"; 
	
	    // add a header to response to force download (specifying filename) 
	    Response.AddHeader("Content-Disposition", "attachment; filename=\"HUDCalculator.xls\"");
	    Response.WriteFile("c:\\Web\\Temp\\HUDCalculator.xls");
	    
					
					
			}
	}
}