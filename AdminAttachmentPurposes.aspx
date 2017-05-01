<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminAttachmentPurposes.aspx.cs" Inherits="AdminAttachmentPurposes" MasterPageFile="~/AffinityTemplate.master"%>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="pane_one" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>Manage Attachment Types</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">

<asp:GridView 
        ID="aGrid" 
        EnableSortingAndPagingCallbacks="false" 
        AllowPaging="false" AllowSorting="false" 
        GridLines="None" 
        runat="server" 
        CssClass="subtle ManageAttachmentTypes" 
        AutoGenerateColumns="false">
        <EmptyDataTemplate>
            <p><em>There are no records</em></p>
        </EmptyDataTemplate>
        <AlternatingRowStyle CssClass="odd" />
            <Columns>
                <asp:HyperLinkField
                    Text="Edit"
                    DataNavigateUrlFields="Code"
                    DataNavigateUrlFormatString="AdminAttachmentPurpose.aspx?code={0}" />
                <asp:BoundField DataField="Code" HeaderText="Code" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:BoundField DataField="Roles" HeaderText="Permissions" />
             </Columns>
    </asp:GridView>
    
    <h3>Add New Records:</h3>
    
        <asp:Panel ID="pnlActions" CssClass="actions" runat="server">
            <div><a class="account" href="AdminAttachmentPurpose.aspx">Add New Attachment Type</a></div>
        </asp:Panel>

</asp:Content>