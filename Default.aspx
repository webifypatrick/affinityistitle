<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="Default.aspx.cs" Inherits="Affinity._Default" MasterPageFile="~/AffinityTemplate.master"%>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pain_one_cph" runat="server">

    <h2 id="header" runat="server">Welcome to Affinity Title Services</h2>
    
    <p id="welcome" runat="server">Affinity Title provides services for mortgage companies and real estate agents from 
    its central processing center located in the Chicago suburb of Des Plains, IL. Affinity Title's 
    closing specialists handle every aspect of the title process including title abstracts, 
    issuing title commitments, preparing HUD/Settlement statements, scheduling real estate 
    signings/closings, disbursement of funds and issuing title insurance policies.</p>
    
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">

    <fieldset>
        <legend>Access Your Personal Account:</legend>
        <div class="label">Username:</div>
        <div class="input"><asp:TextBox ID="txtUsername" runat="server"></asp:TextBox></div>
        <div class="label">Password:</div>
        <div class="input"><asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox></div>
        <div class="actions"><asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" /></div>
    </fieldset>    

    <p><em>Forgotten your login? <a href="ForgottenPass.aspx">Click here to reset your password</a>.</em></p>

</asp:Content>