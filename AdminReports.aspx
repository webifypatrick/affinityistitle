<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeBehind="AdminReports.aspx.cs" Inherits="Affinity.AdminReports" Title="Untitled Page" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>Reports</h2>
    <p>
        <a href="AdminReport.aspx"> Search Package / Commitment Posted </a>
    </p>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
      
    
</asp:Content>

