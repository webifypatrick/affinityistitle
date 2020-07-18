using System;
using System.Web.UI;

namespace Affinity
{
    public partial class _Content : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Affinity.Account account = this.GetAccount();
            string code = Request["page"] != null ? Request["page"] : "";

            Affinity.Content c = new Affinity.Content(this.phreezer);

            try
            {
                c.Load(code);

                ((Affinity.MasterPage) this.Master).SetLayout(c.MetaTitle, MasterPage.LayoutStyle.Photo_01);

                header.InnerHtml = c.Header;
                pnlBody.Controls.Clear();
                pnlBody.Controls.Add(new LiteralControl(c.Body));
            }
            catch (Exception)
            {
                // we don't really care - just show the default not-found message
            }

        }
    }
}