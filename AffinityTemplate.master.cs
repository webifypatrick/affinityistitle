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

public partial class MasterPage : System.Web.UI.MasterPage
{
	private FeedbackType fbtype = FeedbackType.Information;

    protected void Page_Load(object sender, EventArgs e)
    {
    		string active = "";
    		if(Request["iframe"] != null) active = "iframe";
        this.SetNavigation(active);

		string feedback = Request["feedback"];
		if (feedback != null)
		{
			this.ShowFeedback(feedback, fbtype);
		}
    }

    public void SetNavigation(string active)
    {
        // set the navigation based on this users permissions
        PageBase pb = (PageBase)this.Page;
        Affinity.Account acc = pb.GetAccount();

			// TODO: everyone gets to see the fee finder link for the time being
			this.lnkFeeFinder.Visible = true;
	
			if (acc.Role.IsAuthenticated())
			{
				this.lnkMyAccount.Visible = true;
				lnkPreferences.Visible = true;
				this.lnkLogout.Visible = true;
				this.lnkForms.Visible = true;
				this.lnkHUDCalculator.Visible = true;
	
				this.lnkHome.Visible = false;
				this.lnkAbout.Visible = false;
				lnkNews.Visible = false;
	
				this.lnkAdmin.Visible = acc.Role.HasPermission(Affinity.RolePermission.AdminSystem);
				this.lnkAttorneyServices.Visible = acc.Role.HasPermission(Affinity.RolePermission.AttorneyServices);
				this.checkFrame.Visible = false;
			}
			else
			{
				this.timeoutdiv.Visible = false;
			}
			
			if(active.Equals("iframe"))
			{
				this.timeoutdiv.Visible = false;
				this.headerDIV.Visible = false;
				this.pnlNav.Visible = false;
				this.lblcontent_footer.Visible = false;
				this.checkFrame.Visible = false;
			}

    }

    public void SetLayout(string title, LayoutStyle style)
    {
        // this.Page.Header.Title += ": " + title;
        this.Page.Header.Title = "Affinity Title Services: " + title;

        switch (style)
        {
            case LayoutStyle.Photo_01:
                this.stage.Attributes.Add("class", "stage clearfix table home");
                this.leftside.Attributes.Add("class", "td logincontent");
                this.rightside.Attributes.Add("class", "td photo_01");
                this.content.Attributes.Add("class", "login");
                break;
            default:
                // we don't need to do this because this is the default setting already, but if we did:
                // this.stage.Attributes.Add("class", "stage clearfix");
                break;
        }
    }

    public void ShowFeedback(string message, FeedbackType feedbacktype)
    {
		fbtype = feedbacktype;

        switch (fbtype)
        {
            case FeedbackType.Error:
                pnlFeedback.CssClass = "error";
                break;
            case FeedbackType.Warning:
                pnlFeedback.CssClass = "warning";
                break;
            default:
                pnlFeedback.CssClass = "information";
                break;
        }

        lblFeedback.Text = message;
        pnlFeedback.Visible = true;
    }

    public enum LayoutStyle
    {
        ContentOnly = 1,
        Photo_01 = 2
    }

    public enum FeedbackType
    {
        Information = 1,
        Warning = 2,
        Error = 3
    }

}
