<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AffinityTemplate.master" CodeBehind="AdminRequestTypes.aspx.cs" Inherits="Affinity.AdminRequestTypes" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="reload_content_title" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h3>Reload Request Types</h3>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">

    <p class="warning">Warning: Do not use this feature unless you understand what you are doing!</p>
    
    <p>This will reload the request type definitions from the XML files in the RequestTypes directory.
    This should be done after the files have been updatd and tested.</p>
    
    <p><asp:Button ID="btnReload" runat="server" Text="Reload" OnClick="btnReload_Click" /></p>
<br />
		<h3>Edit the following Request Types:</h3>
    <ContentTemplate>
    <asp:GridView 
        ID="oGrid" 
        AllowPaging="false" 
        PageSize="20"
        AllowSorting="false" 
        GridLines="None" 
        runat="server" 
        CssClass="subtle" 
        AutoGenerateColumns="false" 
        EnableSortingAndPagingCallbacks="false" 
        >
        <EmptyDataTemplate>
            <p><em>No Current Request Types</em></p>
        </EmptyDataTemplate>
        <AlternatingRowStyle CssClass="odd" />
            <Columns>
                <asp:HyperLinkField
                    Text="Edit"
                    DataNavigateUrlFields="Code"
                    DataNavigateUrlFormatString="AdminRequestType.aspx?code={0}"
                    ControlStyle-CssClass="process_order" />
                <asp:BoundField DataField="code" HeaderText="Code" SortExpression="code" />
                <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                <asp:TemplateField HeaderText="Export Formats" SortExpression="exportformats">
                   <ItemTemplate>
                        <asp:Label ID="lbTest" runat="server"><%# System.Text.RegularExpressions.Regex.Replace(System.Text.RegularExpressions.Regex.Replace(System.Text.RegularExpressions.Regex.Replace("," + DataBinder.Eval(Container.DataItem, "exportformats").ToString(), ",([^=]*)=[^,]*", "$1, ", System.Text.RegularExpressions.RegexOptions.ECMAScript), "([^.]*), $", "$1", System.Text.RegularExpressions.RegexOptions.ECMAScript), "^,$", "", System.Text.RegularExpressions.RegexOptions.ECMAScript) %></asp:Label>
                   </ItemTemplate>
                </asp:TemplateField>
           </Columns>
    </asp:GridView>   		        
    </ContentTemplate>
</asp:Content>

