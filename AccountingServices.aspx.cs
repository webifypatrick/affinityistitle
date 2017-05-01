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

public partial class AccountingServices : PageBase
{

	private Affinity.RequestType rtype;
	private XmlForm xmlForm;

	/// <summary>
	/// The form controls are created at this point.  if we create them at page load
	/// then their viewstate will not be persisted.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	override protected void PageBase_Init(object sender, System.EventArgs e)
	{
		// we have to call the base first so phreezer is instantiated
		base.PageBase_Init(sender, e);
	}

	private void LoadForm()
	{

	}

    protected void Page_Load(object sender, EventArgs e)
    {
		this.RequirePermission(Affinity.RolePermission.AttorneyServices);
		this.Master.SetLayout("Accounting Services", MasterPage.LayoutStyle.ContentOnly);
		pnlForm.Controls.Add(new LiteralControl("<div><a onclick=\"return confirm('This will reserve a closing room WITHOUT placing an Affinity Title Service order.  Are you sure?');\" class=\"add\" href=\"MyOrderSubmit.aspx?code=ClosingRequest\">Submit a 'Closing Room Only' Request</a></div><br>"));

		pnlForm.Controls.Add(new LiteralControl("<div><a onclick=\"return confirm('This will order a Clerking Service WITHOUT placing an Affinity Title Service order.  Are you sure?');\" class=\"add\" href=\"MyOrderSubmit.aspx?code=ClerkingRequest\">Submit a 'Clerking Services Only' Request</a></div><br>"));

		pnlForm.Controls.Add(new LiteralControl("<div><a onclick=\"return confirm('This will order a Survey Services WITHOUT placing an Affinity Title Service order.  Are you sure?');\" class=\"add\" href=\"http://orders.mysurveyservices.com/\">Submit a 'Survey Services Only' Request</a></div><br>"));
	}

}
