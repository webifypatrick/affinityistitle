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

public partial class MyOrderSubmit : PageBase
{
	private Affinity.RequestType rtype;

    protected void Page_Load(object sender, EventArgs e)
    {
		this.RequirePermission(Affinity.RolePermission.SubmitOrders);

		this.btnCancel.Attributes.Add("onclick", "return confirm('If you cancel your changes will not be saved.  Are you sure?');");

		// custom validations
		this.txtPIN.Attributes.Add("onchange","verifyPIN(this);");
		this.txtAdditionalPins.Attributes.Add("onchange", "verifyAdditionalPINs(this);");

		// disable browser based autocomplete
		this.txtPropertyCounty.Attributes.Add("autocomplete", "off");
		this.txtPropertyCity.Attributes.Add("autocomplete", "off");
		this.txtPropertyZip.Attributes.Add("autocomplete", "off");

		// create the request type
		this.rtype = new Affinity.RequestType(this.phreezer);
		string requestCode = (!radioRefinance.Checked)? NoNull.GetString(Request["code"],Affinity.RequestType.DefaultCode) : "Refinance";
		this.rtype = new Affinity.RequestType(this.phreezer);
		this.rtype.Load(requestCode);

		this.header.InnerText = "Step 1 of 2: New " + this.rtype.Description;
		this.Master.SetLayout(this.header.InnerText, MasterPage.LayoutStyle.ContentOnly);
    }

	/// <summary>
	/// cancel and return to the home page
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void btnCancel_Click(object sender, EventArgs e)
	{
		this.Redirect("MyAccount.aspx");
	}

	/// <summary>
	/// submit the order
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void btnSubmit_Click(object sender, EventArgs e)
	{
		Affinity.Account account = this.GetAccount();
		// create a new order for this request
		Affinity.Order order = new Affinity.Order(this.phreezer);

		order.OriginatorId = account.Id;
		order.CustomerStatusCode = Affinity.OrderStatus.PendingCode;
		order.InternalStatusCode = Affinity.OrderStatus.PendingCode;
		order.InternalId = Affinity.Order.DefaultInternalId;

		order.ClientName = txtClientName.Text;
		order.Pin = txtPIN.Text;
		order.AdditionalPins = txtAdditionalPins.Text;
		order.PropertyAddress = txtPropertyAddress.Text;
		order.PropertyAddress2 = txtPropertyAddress2.Text;
		order.PropertyCity = txtPropertyCity.Text;
		order.PropertyState = txtPropertyState.Text;
		order.PropertyZip = txtPropertyZip.Text;
		order.CustomerId = txtCustomerId.Text;
		order.PropertyCounty = txtPropertyCounty.Text;
		order.PropertyUse = (radioResidential.Checked)? "Residential" : "Nonresidential";
		

		try
		{
			order.ClosingDate = DateTime.Parse(txtClosingDate.Text);
			bool isRefinance = radioRefinance.Checked;
			
			Affinity.Order PreviousOrder = order.GetPrevious();
			// verify the user has not submitted this PIN in the past		
			if(PreviousOrder == null || isDupOrderWarnedHdn.Value.Equals("true"))
			{
				order.Insert();

				this.Redirect("MyRequestSubmit.aspx?new=true&id=" + order.Id + "&code=" + this.rtype.Code);
			}
			else
			{
				isDupOrderWarnedHdn.Value = "true";
				
				string AffinityIdMessage = "";
				
				if(!PreviousOrder.InternalId.Equals(""))
				{
					AffinityIdMessage = " and Affinity ID: " + PreviousOrder.InternalId;
				}
				
				this.Master.ShowFeedback("You previously submitted order " + PreviousOrder.Id + " for a similar property (PIN " + order.Pin + AffinityIdMessage + ").  Please verify that this is not a duplicate order and click Continue... Otherwise click Cancel.", MasterPage.FeedbackType.Warning);
			}
		}
		catch (FormatException ex)
		{
			this.Master.ShowFeedback("Please check that the estimated closing date is valid and in the format 'mm/dd/yyyy'",MasterPage.FeedbackType.Error);
		}
		catch (Exception ex)
		{
			this.Master.ShowFeedback(ex.Message, MasterPage.FeedbackType.Error);
		}

	}
}
