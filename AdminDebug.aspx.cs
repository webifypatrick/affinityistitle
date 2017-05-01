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
using System.Collections;

public partial class AdminDebug : PageBase
{
	protected void Page_Load(object sender, EventArgs e)
	{
		this.RequirePermission(Affinity.RolePermission.AdminSystem);
		this.RequirePermission(Affinity.RolePermission.AffinityManager);
		this.RequirePermission(Affinity.RolePermission.AffinityStaff);
		this.Master.SetLayout("Administration", MasterPage.LayoutStyle.ContentOnly);

		FileStream fs = File.Open(
			Server.MapPath("logs/debug.log"),
			FileMode.Open,
			FileAccess.Read,
			FileShare.ReadWrite);

		//Stream = File.Open(FileName, FileMode.Open,
		//FileAccess.Read, FileShare.ReadWrite)
		//Dim sr As StreamReader
		//sr = New StreamReader(str)

		
		if (fs.CanRead)
		{
			StreamReader sr = new StreamReader(fs);

			pnlLog.Controls.Add(new LiteralControl("<table class='oldschool'>\r\n"));
			pnlLog.Controls.Add(new LiteralControl("<tr><th>Date</th><th>Type</th><th>Class</th><th>Message</th></tr>\r\n"));
			string logtype = "";
			while (sr.Peek() >= 0)
			{
				string[] line = sr.ReadLine().Split("^".ToCharArray());
				logtype =(line.Length > 1) ? line[1].ToLower() : "error";

				if (
					(logtype == "error" && cbError.Checked)
					|| (logtype == "warn" && cbWarning.Checked)
					|| (logtype == "info" && cbInfo.Checked)
					|| (logtype == "debug" && cbDebug.Checked)
					)
				{
					pnlLog.Controls.Add(new LiteralControl("<tr>"));

					foreach (string col in line)
					{
						try
						{
							pnlLog.Controls.Add(new LiteralControl("<td class='debug_"
								+ logtype + "'>"
								+ col + "</td>"));
						}
						catch (Exception ex)
						{
							pnlLog.Controls.Add(new LiteralControl("<td class='debug_error'>" + col + "</td>"));
						}
					}
					pnlLog.Controls.Add(new LiteralControl("</tr>\r\n"));
				}
			}

			sr.Close();

			pnlLog.Controls.Add(new LiteralControl("</table>\r\n"));

		}
		else
		{
			pnlLog.Controls.Add(new LiteralControl("<div class='error'>Cannot read the logifle!</div>"));
		}

		fs.Close();
    }

	protected void btnSearch_Click(object sender, EventArgs e)
	{
		// we really don't have to do anything here
	}
}
