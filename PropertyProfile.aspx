<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeBehind="PropertyProfile.aspx.cs" Inherits="Affinity.PropertyProfile" %>
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
    <h3>Property Profile</h3>
	
    </div>
    
	
	</div> <!-- /float -->
	<div style="float:left;">
    <p>

  		<input type="submit" name="s" value="Submit" /> 

	</div> <!-- /float -->
	<div style="float: left; width: 350px;">
    
  </div>
	

</div> <!-- /clearfix -->

</asp:Content>