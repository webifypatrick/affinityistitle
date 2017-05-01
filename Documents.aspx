<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeFile="Documents.aspx.cs" Inherits="Documents" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="pane_one" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>Forms</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>   

<div class="clearfix">
	
	<div style="float: left; width: 350px;">

   <div>
    <h3>Attorney Closing Documents</h3>
	
    </div>
    
	
	</div> <!-- /float -->
	<div style="float:left;">
    <p>
    	<!--div><input type="checkbox" name="file" id="Warranty_Deeds" value="Warranty_Deeds" /> Warranty Deeds</div-->
    	<div><input type="checkbox" name="file" id="POA" value="POA" /> Power of Attorney</div>
    	<div><input type="checkbox" name="file" id="Affidavit_of_Title" value="Affidavit_of_Title" /> Affidavit of Title</div>
    	<div><input type="checkbox" name="file" id="Bill_of_Sale" value="Bill_of_Sale" /> Bill of Sale</div>
    	<div><input type="checkbox" name="file" id="ExecutorDeed" value="ExecutorDeed" /> Executor Deed</div>
    	<div><input type="checkbox" name="file" id="DeedinTrust" value="DeedinTrust" /> Deed in Trust</div>
    	<div><input type="checkbox" name="file" id="TrusteesDeed" value="TrusteesDeed" /> Trustees Deed</div>
    	<div><input type="checkbox" name="warrantyfile" id="warrantyfile" onclick="document.getElementById('Warranty_DeedDIV').disabled = !this.checked; document.getElementById('Warranty_Deed').checked = this.checked;" /> Warranty Deeds</div>
    	<div id="Warranty_DeedDIV" style="padding-left:50px;">
    		<div><input type="radio" name="wfile" id="Warranty_Deed" value="WD" onclick="document.getElementById('warrantyfile').checked = true;" /> Warranty Deed</div>
    		<div><input type="radio" name="wfile" id="Warranty_Deed_JT" value="WD_JT" onclick="document.getElementById('warrantyfile').checked = true;" /> Warranty Deed - Joint Tenancy</div>
    		<div><input type="radio" name="wfile" id="Warranty_Deed_S" value="WD_S" onclick="document.getElementById('warrantyfile').checked = true;" /> Warranty Deed - Special</div>
    		<div><input type="radio" name="wfile" id="Warranty_Deed_TE" value="WD_TE" onclick="document.getElementById('warrantyfile').checked = true;" /> Warranty Deed - Tenancy By the Entirety</div>
    	</div>
    	<div><input type="checkbox" name="atsdocumentsfile" id="atsdocumentsfile" onclick="document.getElementById('ATS_DocumentsDIV1').disabled = !this.checked; document.getElementById('1099_HUD_Page_4').checked = this.checked;" /> ATS Documents</div>
    	<div id="ATS_DocumentsDIV1" style="padding-left:50px;">
    		<div><input type="checkbox" name="file" id="1099_HUD_Page_4" value="1099_HUD_Page_4" onclick="document.getElementById('atsdocumentsfile').checked = true;" /> 1099 HUD Page 4</div>
    	</div>
    	<div><input type="checkbox" name="agencydocumentsfile" id="agencydocumentsfile" onclick="document.getElementById('ATS_DocumentsDIV2').disabled = !this.checked; document.getElementById('Disclosure_Agent').checked = this.checked; document.getElementById('Disclosure_Owner').checked = this.checked; document.getElementById('Attorney_Title_Agent_Disclosure').checked = this.checked;" /> Agency Forms</div>
    	<div id="ATS_DocumentsDIV2" style="padding-left:50px;">
    		<div><input type="checkbox" name="file" id="Disclosure_Agent" value="Disclosure_Agent" onclick="document.getElementById('agencydocumentsfile').checked = true;" /> Disclosure Agent</div>
    		<div id="Disclosure_OwnerDIV" runat="server"><input type="checkbox" name="file" id="Disclosure_Owner" value="Disclosure_Owner" onclick="document.getElementById('agencydocumentsfile').checked = true;" /> Disclosure Owner</div>
    		<div id="Attorney_Title_Agent_DisclosureDIV" runat="server"><input type="checkbox" name="file" id="Attorney_Title_Agent_Disclosure" value="Attorney_Title_Agent_Disclosure" onclick="document.getElementById('agencydocumentsfile').checked = true;" /> Attorney Title Agent Disclosure</div>
    	</div>
    	
    	<div style="margin-bottom:20px;">
    		<div>Mail to: </div>
    		<div>
    			<input type="radio" name="MailAddress" id="MailAddress_None" value="MailAddress_None" checked="checked" /> None
    		</div>
    		<div>
    			<input type="radio" name="MailAddress" id="MailAddress_BuyersPropertyAddress" value="MailAddress_BuyersPropertyAddress" /> Buyer's Property Address
    		</div>
    		<div>
    			<input type="radio" name="MailAddress" id="MailAddress_BuyersAttorney" value="MailAddress_BuyersAttorney" /> Buyer's Attorney
    		</div>
    	</div>
  		<input type="submit" name="s" value="Submit" /> 

	</div> <!-- /float -->
	<div style="float: left; width: 350px;">
    
  </div>
	

</div> <!-- /clearfix -->

</asp:Content>