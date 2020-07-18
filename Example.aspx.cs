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

using Com.VerySimple.Util;

namespace Affinity
{
    public partial class Example : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            // get stack trace
            System.Diagnostics.StackFrame fr = new System.Diagnostics.StackFrame(1, true);
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(fr);
            Response.Write( "method = " + fr.GetMethod().Name + " trace = " + st.ToString() );
            */

            ((Affinity.MasterPage) this.Master).SetLayout("Example", MasterPage.LayoutStyle.ContentOnly);


            /*
             * Affinity.Request r = new Affinity.Request(this.phreezer);
            r.Load(1);

            Hashtable ht = r.GetHashTable("sp_id", true);
            foreach (string key in ht.Keys)
            {
                pnlForm.Controls.Add(new LiteralControl("<div>"+key+" = "+ (string)ht[key]  +"</div>"));
            }
             * */

        }


    }
}