<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeBehind="AccountingServices.aspx.cs" Inherits="Affinity.AccountingServices" Title="Accounting Services" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="pain_one" ContentPlaceHolderID="pain_one_cph" Runat="Server">
<h2>Accounting Services</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>   
    
     <!--h3>Closing Room Only:</h3>

    <p>If you have not and/or do not plan to submit your order through Affinity, but you would like
    to reserve a closing room only, you may use the link below to submit a "Closing Room Only" request:</p-->


   <asp:Panel ID="pnlForm" runat="server">

    </asp:Panel>
</asp:Content>
