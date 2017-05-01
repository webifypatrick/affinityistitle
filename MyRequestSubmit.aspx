<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeFile="MyRequestSubmit.aspx.cs" Inherits="MyRequestSubmit" Title="Untitled Page" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pain_one_cph" runat="server">
        <style>
				#outer_CopyApplicationTo td
				{
					white-space: nowrap;
					font-size: 14px;
					padding: 0;
				}
      	</style>
    <h2 id="header" runat="server">Submit Request for Order</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:Panel ID="pnlForm" runat="server" EnableViewState="false">
    </asp:Panel>
    
    <asp:Panel ID="pnlResults" runat="server" EnableViewState="false" Visible="false">
        <p class="information">Your request has been submitted.  We attempt to process all requests
        within three (3) business days.  You will receive an automatic notification
        when this request has been processed.</p>
        
        <h3>Where would you like to go next?</h3>
    </asp:Panel>

    <p id="action_buttons">
        <asp:Button ID="btnSubmit" runat="server" Text="Submit Order" OnClick="btnSubmit_Click" />
        <asp:Button ID="btnCancelSubmit" runat="server" Text="Cancel" OnClick="btnCancelSubmit_Click" />
        <asp:Button ID="btnChange" runat="server" Text="Submit Changes" Visible="false" OnClick="btnChange_Click" />
        <asp:Button ID="btnCancelChange" runat="server" Text="Cancel" Visible="false" OnClick="btnCancelChange_Click" />
    </p>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Please correct the following errors:" />
</asp:Content>

<asp:Content ID="ContentFooter" ContentPlaceHolderID="lblcontent_footer" runat="server">
	<span id="ContentFooterSpan" runat="server">&copy; Copyright 2009, Affinity Title Services, LLC</span>
</asp:Content>