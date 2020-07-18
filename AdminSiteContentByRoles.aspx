<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeBehind="AdminSiteContentByRoles.aspx.cs" Inherits="Affinity.AdminSiteContentByRoles" Title="Admin Site Content By Roles" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="pain_one" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>Manage Accounts</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" Runat="Server">
   <asp:GridView 
        ID="aGrid" 
        EnableSortingAndPagingCallbacks="false" 
        AllowPaging="false" AllowSorting="false" 
        GridLines="None" 
        runat="server" 
        CssClass="subtle ManageAccounts" 
        AutoGenerateColumns="false">
        <EmptyDataTemplate>
            <p><em>There are no records</em></p>
        </EmptyDataTemplate>
        <AlternatingRowStyle CssClass="odd" />
            <Columns>
                <asp:HyperLinkField
                    Text="Edit"
                    DataNavigateUrlFields="scr_role_code,scr_content_section"
                    DataNavigateUrlFormatString="AdminSiteContentByRole.aspx?code={0}&section={1}" />
                <asp:BoundField DataField="scr_role_code" HeaderText="Code" />
                <asp:BoundField DataField="scr_content_section" HeaderText="Section" />
            </Columns>
    </asp:GridView>
    
    <h3>Add New Records:</h3>
    
        <asp:Panel ID="pnlActions" CssClass="actions" runat="server">
            <div><a class="account" href="AdminSiteContentByRole.aspx">Add New Site Content By Role Change</a></div>
            </asp:Panel>

</asp:Content>
