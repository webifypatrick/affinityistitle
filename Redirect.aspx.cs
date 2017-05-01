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

public partial class Redirect : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
		this.body.Attributes.Add("onload","setTimeout(\"redirect('"+Request["url"].Replace("'","") +"')\", 100);");
    }
}
