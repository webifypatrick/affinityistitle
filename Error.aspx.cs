using System;

namespace Affinity
{
    public partial class Error : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((Affinity.MasterPage) this.Master).SetLayout("An Error Has Occured", MasterPage.LayoutStyle.ContentOnly);
            ((Affinity.MasterPage)this.Master).ShowFeedback(Request["feedback"], MasterPage.FeedbackType.Error);
        }
    }
}