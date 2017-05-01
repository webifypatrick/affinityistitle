<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeFile="AdminContents.aspx.cs" Inherits="AdminContents" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="pain_one" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>Manage Site Content Pages</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">
    <asp:GridView 
        ID="cGrid" 
        GridLines="None" 
        runat="server" 
        CssClass="subtle" 
        AutoGenerateColumns="false">
        <EmptyDataTemplate>
            <p><em>There are no content pages</em></p>
        </EmptyDataTemplate>
        <AlternatingRowStyle CssClass="odd" />
            <Columns>
                <asp:HyperLinkField
                    Text="Edit Page"
                    DataNavigateUrlFields="Code"
                    DataNavigateUrlFormatString=
                    "AdminContent.aspx?code={0}" />
                <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
                <asp:BoundField DataField="MetaTitle" HeaderText="MetaTitle" SortExpression="MetaTitle" />
                <asp:BoundField DataField="Modified" HeaderText="Modified" DataFormatString="{0:M/dd/yyyy}" HtmlEncode="false" />
            </Columns>
    </asp:GridView>
    
    <p>
        <asp:Button ID="btnNew" runat="server" Text="Add New Page" OnClick="btnNew_Click" /></p>
</asp:Content>
