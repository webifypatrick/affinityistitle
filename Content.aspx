<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Content.aspx.cs" Inherits="Content" MasterPageFile="~/AffinityTemplate.master"%>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2 id="header" runat="server">Affinity Title Services</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">

    <asp:Panel ID="pnlBody" runat="server">
        <!-- default "not found" content -->
        <p>The page you have specified was not found.  Please check that you 
        have typed the URL correctly.  Othewise it is possible that we have 
        re-organized our content.  You may want to start over from our 
        <a href="Default.aspx">home page</a>.
        </p>
        
        <p>If you continue to experience problems locating content, please
        feel free to contact us to let us know.</p>
    </asp:Panel>   
    
    <div class="vspacer"></div>

</asp:Content>