using System;

using Com.VerySimple.Util;

namespace Affinity
{
    public partial class Forms : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            // get stack trace
            System.Diagnostics.StackFrame fr = new System.Diagnostics.StackFrame(1, true);
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(fr);
            Response.Write( "method = " + fr.GetMethod().Name + " trace = " + st.ToString() );
            */

            ((Affinity.MasterPage)this.Master).SetLayout("Forms", MasterPage.LayoutStyle.ContentOnly);


        }


    }
}