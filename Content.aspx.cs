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

public partial class Content : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
    Affinity.Account account = this.GetAccount();
		string code = Request["page"] != null ? Request["page"] : "";

		Affinity.Content c = new Affinity.Content(this.phreezer);

		try
		{
			c.Load(code);

			this.Master.SetLayout(c.MetaTitle, MasterPage.LayoutStyle.Photo_01);
			
			header.InnerHtml = c.Header;
			pnlBody.Controls.Clear();
			pnlBody.Controls.Add(new LiteralControl(c.Body));
		}
		catch (Exception ex)
		{
			// we don't really care - just show the default not-found message
		}

    }
}
