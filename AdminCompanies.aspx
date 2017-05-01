<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminCompanies.aspx.cs" Inherits="AdminCompanies" MasterPageFile="~/AffinityTemplate.master"%>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="pane_one" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>Manage Companies</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">

    
    
   <asp:GridView 
        ID="cGrid" 
        EnableSortingAndPagingCallbacks="true" 
        AllowPaging="false" AllowSorting="false" 
        GridLines="None" 
        runat="server" 
        CssClass="subtle" 
        AutoGenerateColumns="false">
        <EmptyDataTemplate>
            <p><em>There are no records</em></p>
        </EmptyDataTemplate>
        <AlternatingRowStyle CssClass="odd" />
            <Columns>
                <asp:HyperLinkField
                    Text="Edit"
                    DataNavigateUrlFields="Id"
                    DataNavigateUrlFormatString="AdminCompany.aspx?id={0}" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
            </Columns>
    </asp:GridView>
    
    <h3>Add New Records:</h3>
    
        <asp:Panel ID="pnlActions" CssClass="actions" runat="server">
            <div><a class="company" href="AdminCompany.aspx">Add New Company</a></div>
            </asp:Panel>
            
</asp:Content>