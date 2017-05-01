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

public partial class Error : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
		this.Master.SetLayout("An Error Has Occured", MasterPage.LayoutStyle.ContentOnly);
        this.Master.ShowFeedback(Request["feedback"], MasterPage.FeedbackType.Error);
    }
}
