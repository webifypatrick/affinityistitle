using System;
using System.Collections;
using System.Web.UI;
using System.IO;
using GemBox.Spreadsheet;

namespace Affinity
{
	public partial class QuotingSheet : PageBase
	{  
	    protected void Page_Load(object sender, EventArgs e)
	    {
				if(Request["PurchasePrice"] != null || Request["LoanAmount"] != null)
				{
		      decimal purchprice = 0;
		      if(Request["PurchasePrice"] != null)
		      {
						decimal.TryParse(Request["PurchasePrice"].Replace(",", "").Replace("$", ""), out purchprice);
					}
					else if(Request["LoanAmount"] != null)
					{
						decimal.TryParse(Request["LoanAmount"].Replace(",", "").Replace("$", ""), out purchprice);
					}
					string InsuranceRate = "";
		      
		      for(int purchidx = 10; purchidx < 91; purchidx++)
					{
						if(purchprice > (purchidx * 10000) && purchprice <= ((purchidx + 1) * 10000))
						{
			        Affinity.TitleFees tf1 = new Affinity.TitleFees(this.phreezer);

			        Affinity.TitleFeesCriteria tfc = new Affinity.TitleFeesCriteria();
			      	tfc.FeeType = "Insurance Rate";
                    tfc.State = "IL";
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

            /*
            if(Request["LoanAmount"] != null)
            {
          decimal loanamt = 0;
                decimal.TryParse(Request["LoanAmount"].Replace(",", "").Replace("$", ""), out loanamt);
                decimal InsuranceRate = 0;

          if(loanamt <= 100000)
          {
            Affinity.TitleFees tf1 = new Affinity.TitleFees(this.phreezer);

            Affinity.TitleFeesCriteria tfc = new Affinity.TitleFeesCriteria();
                tfc.FeeType = "Second/Equity Mortgage Rates";
            tfc.Name = "Up to $100,000.00";
            tfc.State = "IL";
            tfc.AppendToOrderBy("Id");
            tf1.Query(tfc);

            IEnumerator tfi = tf1.GetEnumerator();

            // loop through the checkboxes and insert or delete as needed
            while (tfi.MoveNext())
            {
                Affinity.TitleFee tfitem1 = (Affinity.TitleFee) tfi.Current;
                            InsuranceRate = decimal.Parse(tfitem1.Fee.ToString());
            }
          }
          else if(loanamt > 200000)
          {
            Affinity.TitleFees tf1 = new Affinity.TitleFees(this.phreezer);

            Affinity.TitleFeesCriteria tfc = new Affinity.TitleFeesCriteria();
                tfc.FeeType = "Second/Equity Mortgage Rates";
            tfc.Name = "$190,001 to $200,000";
            tfc.State = "IL";
            tfc.AppendToOrderBy("Id");
            tf1.Query(tfc);

            IEnumerator tfi = tf1.GetEnumerator();

            // loop through the checkboxes and insert or delete as needed
            while (tfi.MoveNext())
            {
                Affinity.TitleFee tfitem1 = (Affinity.TitleFee) tfi.Current;
                            InsuranceRate = decimal.Parse(tfitem1.Fee.ToString());
            }

            tf1 = new Affinity.TitleFees(this.phreezer);

                    decimal InsuranceRateForEvery1000 = 0;
            tfc = new Affinity.TitleFeesCriteria();
                tfc.FeeType = "Second/Equity Mortgage Rates";
            tfc.Name = "for every $1,000 over $200,000 up to $500,000";
            tfc.AppendToOrderBy("Id");
            tfc.State = "IL";
           tf1.Query(tfc);

            tfi = tf1.GetEnumerator();

            // loop through the checkboxes and insert or delete as needed
            while (tfi.MoveNext())
            {
                Affinity.TitleFee tfitem1 = (Affinity.TitleFee) tfi.Current;
                            InsuranceRateForEvery1000 = decimal.Parse(tfitem1.Fee.ToString());
            }

            if(loanamt < 500000)
            {
                InsuranceRate = InsuranceRate + (Math.Floor((loanamt - 200000) / 1000) * InsuranceRateForEvery1000);
            }
            else
            {
                InsuranceRate = InsuranceRate + (300 * InsuranceRateForEvery1000);
            }
          }
          else
          {
              for(int loanamtidx = 10; loanamtidx < 19; loanamtidx++)
                    {
                        if(loanamt > (loanamtidx * 10000) && loanamt <= ((loanamtidx + 1) * 10000))
                        {
                    Affinity.TitleFees tf1 = new Affinity.TitleFees(this.phreezer);

                    Affinity.TitleFeesCriteria tfc = new Affinity.TitleFeesCriteria();
                        tfc.FeeType = "Second/Equity Mortgage Rates";
                    tfc.Name = "$" + loanamtidx.ToString() + "0,001 to $" + (loanamtidx + 1).ToString() + "0,000";
                    tfc.State = "IL";
                    tfc.AppendToOrderBy("Id");
                    tf1.Query(tfc);

                    IEnumerator tfi = tf1.GetEnumerator();

                    // loop through the checkboxes and insert or delete as needed
                    while (tfi.MoveNext())
                    {
                        Affinity.TitleFee tfitem1 = (Affinity.TitleFee) tfi.Current;
                                    InsuranceRate = decimal.Parse(tfitem1.Fee.ToString());
                    }
                  }
                    }
                }

                Response.Write(InsuranceRate);
            Response.End();
        }
        */

            if (Page.IsPostBack)
	    	{
	    		generateSpreadsheet();
	    	}
	    	else
	    	{
	        Affinity.TitleFees tf2 = new Affinity.TitleFees(this.phreezer);

	        Affinity.TitleFeesCriteria tfc2 = new Affinity.TitleFeesCriteria();
	    		tfc2.FeeType = "Mortgage Policy/Endorsement Fees";
	      	tfc2.Name = "Simultaneously Issued Mortgage Policy";
            tfc2.State = "IL";
            tfc2.AppendToOrderBy("Id");
	        tf2.Query(tfc2);
	
	        IEnumerator tfi2 = tf2.GetEnumerator();
	
	        // loop through the checkboxes and insert or delete as needed
	        while (tfi2.MoveNext())
	        {
	            Affinity.TitleFee tfitem2 = (Affinity.TitleFee) tfi2.Current;
							SimultaneousCharge.Value = tfitem2.Fee.ToString();
	        }
	      }
			}
			
		protected void generateSpreadsheet() {
			SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
			ExcelFile file = new ExcelFile();
			ExcelWorksheet ws = file.Worksheets.Add("HUD");
			ws.PrintOptions.FitWorksheetWidthToPages = 1;
			
      ws.Columns[1].Style.NumberFormat = "_($* #,##0.00_)";
      ws.Columns[1].Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;
			
			ws.Rows[0].Cells[0].Value = "Quoting Sheet";
    	ws.Rows[0].Cells[0].Style.Font.Weight = ExcelFont.BoldWeight;
    	ws.Rows[0].Cells[0].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;

    	decimal amount = 0;
    	
    	ws.Rows[1].Cells[0].Value = "Purchase Price";
	    decimal.TryParse(PurchasePrice.Value, out amount);
    	ws.Rows[1].Cells[1].Value = amount;
    	
    	ws.Rows[2].Cells[0].Value = "Loan Amount";
    	amount = 0;
	    decimal.TryParse(LoanAmount.Value, out amount);
    	ws.Rows[2].Cells[1].Value = amount;
    	
    	ws.Rows[4].Cells[0].Value = "Full Owners Policy Premium";
    	amount = 0;
	    decimal.TryParse(FullOwnersPolicyPremium.Value, out amount);
    	ws.Rows[4].Cells[1].Value = amount;
    	
    	ws.Rows[5].Cells[0].Value = "+Simultaneous Charge";
    	amount = 0;
	    decimal.TryParse(SimultaneousCharge.Value, out amount);
    	ws.Rows[5].Cells[1].Value = amount;
    	
    	ws.Rows[6].Cells[0].Value = "-Full Loan Policy";
    	amount = 0;
	    decimal.TryParse(FullLoanPolicy.Value, out amount);
    	ws.Rows[6].Cells[1].Value = amount;
    	ws.Rows[6].Cells[1].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Black, LineStyle.Thick);
    	
    	ws.Rows[7].Cells[0].Value = "Disclosed Owner's Policy Premium";
    	//amount = 0;
	    //decimal.TryParse(DisclosedOwnersPolicyPremium.Value, out amount);
    	ws.Rows[7].Cells[1].Formula = "=((B5 + B6) - B7)";
    	
    	ws.Rows[9].Cells[0].Value = "LE/CD Title-Loan Policy";
    	ws.Rows[9].Cells[1].Formula = "=B7";
    	
    	ws.Rows[10].Cells[0].Value = "LE/CD Title-Owner's Policy";
    	ws.Rows[10].Cells[1].Formula = "=B8";
    	
    	ws.Rows[12].Cells[0].Value = "LE/CD page 3 Section L & N Premium Credit";
    	ws.Rows[12].Cells[1].Formula = "=(B5 - B11)";
    	
    	ws.Rows[14].Cells[0].Value = "Settlement Statement Loan Policy";
    	ws.Rows[14].Cells[1].Formula = "=B6";
    	
    	ws.Rows[15].Cells[0].Value = "Settlement Statement Owner's Policy";
    	ws.Rows[15].Cells[1].Formula = "=B5";
    	
	    ws.Columns[0].AutoFit();
	    ws.Columns[1].Width = (15 * 250);
    	
    	if(File.Exists("c:\\Web\\Temp\\QuotingSheet.xls"))
    	{
      	File.Delete("c:\\Web\\Temp\\QuotingSheet.xls");
			}
      file.SaveXls("c:\\Web\\Temp\\QuotingSheet.xls");
	
			Response.ContentType = "application/vnd.ms-excel"; 
	
	    // add a header to response to force download (specifying filename) 
	    Response.AddHeader("Content-Disposition", "attachment; filename=\"QuotingSheet.xls\"");
	    Response.WriteFile("c:\\Web\\Temp\\QuotingSheet.xls");		}
	}
}