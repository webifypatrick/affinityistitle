<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeFile="AdminDebug.aspx.cs" Inherits="AdminDebug" Title="Untitled Page" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>Debug</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">

    <div class="search_filter">Show: 
        <asp:CheckBox ID="cbError" runat="server" Text="Error" Checked="true" />
        <asp:CheckBox ID="cbWarning" runat="server" Text="Warning" Checked="true" />
        <asp:CheckBox ID="cbInfo" runat="server" Text="Info" Checked="true" />
        <asp:CheckBox ID="cbDebug" runat="server" Text="Debug" />
    <asp:Button ID="btnSearch" runat="server" Text="Go" OnClick="btnSearch_Click" /></div>
        
    <asp:Panel ID="pnlLog" runat="server">
    </asp:Panel>
    
</asp:Content>

