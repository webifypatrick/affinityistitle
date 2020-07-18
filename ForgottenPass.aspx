<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="ForgottenPass.aspx.cs" Inherits="Affinity.ForgottenPass" MasterPageFile="~/AffinityTemplate.master"%>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>Forgotten Password</h2>
    
    <p>Because Affinity implements the highest security standards possible, your password 
    is always stored using one-way encryption.  For that reason, your original password cannot be 
    recovered even by the Affinity technology team.</p>
    
    <p>What we can do is generate a new, random password and send it to you via email.  Once you
    receive this password, you can use it to login.</p>
    
    <p>It is recommended that you either make a note of your new password and store it in a secure
    place.  If you'd like, once you login, you can change your password to something that 
    you can remember more easily.</p>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">
     <fieldset>
        <legend>Reset Your Password:</legend>
        
    <asp:Label ID="lblError" runat="server" CssClass="error" Visible="false" EnableViewState="false">
    </asp:Label>
    
    <asp:Panel ID="pnlResults" runat="server" Visible="false" EnableViewState="false">
        <p>Your password has been reset to a new, random password and sent to you via email.  If
        you do not recieve an email within the next few minutes, please check that your email
        filters are not blocking the automated email from 
            <asp:Label ID="lblFromDomain" runat="server" Text="">Us</asp:Label>.</p>
        
        <p>Once you receive your new password, return to the <a href="Default.aspx">Home Page</a> and login
        using this NEW password.  If you like, you can continue to use this new password
        without changing it.  However, you also may change the password to something
        that is easier to remember.  To do so, simply click on "My Preferences" and 
        enter a new password.</p>
        
        <p>Be sure to commit your password to memory or store it in a safe place.</p>
    </asp:Panel>
    
    <asp:Panel ID="pnlReset" runat="server">
        <div class="label">
            Your Email:</div>
        <div class="input"><asp:TextBox ID="txtEmail" runat="server" Width="250px"></asp:TextBox></div>
        <div class="actions"><asp:Button ID="btnReset" runat="server" Text="Reset My Password" OnClick="btnReset_Click" /></div>
   </asp:Panel>

    </fieldset>    

    <p><a href="Default.aspx">Return to the Home Page...</a></p>

</asp:Content>