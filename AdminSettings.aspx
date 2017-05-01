<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeFile="AdminSettings.aspx.cs" Inherits="AdminSettings" Title="Untitled Page" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>System Settings</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:Panel ID="pnlForm" runat="server">
    </asp:Panel>
    
    <p>
        <asp:Button ID="btnUpdate" runat="server" Text="Update Settings" OnClick="btnUpdate_Click" /></p>


	<div class="fields">
		<div id="group_path" class="groupshell" >
		    <div id="group_path_header" class="groupheader">Email Settings Test</div>
		    <fieldset id="group_path_fieldset">

                <p><em>Please save settings prior to submitting test</em></p>
                
                <p>Send a test message to: 
                    <asp:TextBox ID="txtTestEmailAddress" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox></p>
                <p>
                    <asp:Button ID="btnSendTestEmail" runat="server" Text="Send" OnClick="btnSendTestEmail_Click" /></p>
            </fieldset>
        </div>
    </div>
    
</asp:Content>

