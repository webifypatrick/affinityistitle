using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections;
using System.IO;

public partial class FeeFinderJS : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		Response.ContentType = "text/plain";
		Response.Write("/**\r\n");
		Response.Write(" * All counties with formulas\r\n");
		Response.Write(" */\r\n");
		Response.Write("var COUNTIES = new Array();\r\n");

		FileStream fs = File.Open(Server.MapPath("./FeeFinder.csv"),FileMode.Open,FileAccess.Read);
		StreamReader sr = new StreamReader(fs);

		ArrayList counties = new ArrayList();

		/*
		Column b 1 = County
		Column e 4 = page count included in standard fee
		Column f 5 = standard fee
		Column k 10 = dollar per page fee OVER page count included in standard fee
		 */

		String line = "";

		while (sr.Peek() >= 0) 
        {
			line = sr.ReadLine();
			string[] columns = line.Split(",".ToCharArray());

			String county = (String)columns[1];

			// the CSV has dups but we only need on per county
			if (!counties.Contains(county))
			{
				counties.Add(county);

				Response.Write("COUNTIES['" + columns[1]
					+ "'] = {basePages: " + columns[4]
					+ ", baseFee: " + columns[5]
					+ ", additionalPageFee: " + columns[10]
					+ "};\r\n");
			}
		}

		sr.Close();
		fs.Close();

		Response.Write("// EOF");
		Response.End();

	}
}
