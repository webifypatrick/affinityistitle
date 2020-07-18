using System;

namespace Affinity
{
    public partial class Redirect : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.body.Attributes.Add("onload", "setTimeout(function() {redirect('" + Request["url"].Replace("'", "") + "');}, 100);");
        }
    }
}
