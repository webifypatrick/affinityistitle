<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeBehind="AdminReport.aspx.cs" Inherits="Affinity.AdminReport" Title="Untitled Page" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>Search Package / Commitment Posted Report</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">
    <asp:GridView 
        ID="oGrid" 
        AllowPaging="true" 
        PageSize="20"
        AllowSorting="true" 
        GridLines="None" 
        runat="server" 
        CssClass="subtle" 
        AutoGenerateColumns="false" 
        EnableSortingAndPagingCallbacks="false" 
        OnSorting="oGrid_Sorting" OnPageIndexChanging="oGrid_PageIndexChanging"
        >
        <EmptyDataTemplate>
            <p><em>No Current Orders</em></p>
        </EmptyDataTemplate>
        <AlternatingRowStyle CssClass="odd" />
            <Columns>
                <asp:HyperLinkField
                    Text="Process"
                    DataNavigateUrlFields="Id"
                    DataNavigateUrlFormatString="AdminOrder.aspx?id={0}"
                    ControlStyle-CssClass="process_order" />
                <asp:BoundField DataField="Id" HeaderText="Web&nbsp;Id" SortExpression="Id" />
                <asp:BoundField DataField="InternalId" HeaderText="AFF&nbsp;Id" SortExpression="InternalId" />
                <asp:BoundField DataField="ClientName" HeaderText="Client" SortExpression="ClientName" />
                <asp:BoundField DataField="Pin" HeaderText="PIN" SortExpression="Pin" />
                <asp:BoundField DataField="PropertyAddress" HeaderText="Address" SortExpression="PropertyAddress" />
                <asp:BoundField DataField="PropertyCity" HeaderText="City" SortExpression="PropertyCity" />

                <asp:BoundField DataField="InternalStatusCode" HeaderText="Status" SortExpression="InternalStatusCode" />
                <asp:BoundField DataField="ClosingDate" HeaderText="Closing" DataFormatString="{0:M/dd/yyyy}" HtmlEncode="false" SortExpression="ClosingDate" />
                <asp:BoundField DataField="Created" HeaderText="Submitted" DataFormatString="{0:M/dd/yyyy h:m tt}" HtmlEncode="false" SortExpression="Created" />
                <asp:BoundField DataField="Modified" HeaderText="Modified" DataFormatString="{0:M/dd/yyyy h:m tt}" HtmlEncode="false" SortExpression="Modified" />
            </Columns>
    </asp:GridView>  
</asp:Content>
