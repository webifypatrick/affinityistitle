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
		
		this.Master.SetLayout("Forms", MasterPage.LayoutStyle.ContentOnly);


    }


}
