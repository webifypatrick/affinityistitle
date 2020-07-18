<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="Logout.aspx.cs" Inherits="Affinity.Logout" MasterPageFile="~/AffinityTemplate.master"%>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>Thank You!</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">

    <div>You are now logged out of the system.  You may close your 
    browser window or <a href="MyAccount.aspx">login again</a>.</div>
    
    <div style="height: 250px;"></div>

</asp:Content>