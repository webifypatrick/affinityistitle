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
    	<div><input type="checkbox" name="file" id="Affidavit_of_Title" value="Affidavit_of_Title" /> Affidavit of Title</div>
    	<div><input type="checkbox" name="file" id="Bill_of_Sale" value="Bill_of_Sale" /> Bill of Sale</div>
    	<div><input type="checkbox" name="warrantyfile" id="warrantyfile" onclick="document.getElementById('Warranty_DeedDIV').disabled = !this.checked;" /> Warranty Deeds</div>
    	<div id="Warranty_DeedDIV" style="padding-left:50px;">
    		<div><input type="radio" name="file" id="Warranty_Deed" value="WD" onclick="document.getElementById('warrantyfile').checked = true;" /> Warranty Deed</div>
    		<div><input type="radio" name="file" id="Warranty_Deed_JT" value="WD_JT" onclick="document.getElementById('warrantyfile').checked = true;" /> Warranty Deed - Joint Tenancy</div>
    		<div><input type="radio" name="file" id="Warranty_Deed_S" value="WD_S" onclick="document.getElementById('warrantyfile').checked = true;" /> Warranty Deed - Special</div>
    		<div><input type="radio" name="file" id="Warranty_Deed_TE" value="WD_TE" onclick="document.getElementById('warrantyfile').checked = true;" /> Warranty Deed - Tenancy By the Entirety</div>
    	</div>
  		<input type="submit" name="s" value="Submit" /> 

	</div> <!-- /float -->
	<div style="float: left; width: 350px;">
    
  </div>
	

</div> <!-- /clearfix -->

</asp:Content>