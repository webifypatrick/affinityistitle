<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeFile="Example.aspx.cs" Inherits="Example" Title="Untitled Page" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="main_content" ContentPlaceHolderID="pain_one_cph" Runat="Server">
<h1>Page Header</h1>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content_cph" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:Panel ID="pnlForm" runat="server" EnableViewState="false">
    </asp:Panel>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pane_two_cph" Runat="Server">
</asp:Content>

