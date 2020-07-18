<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeBehind="AdminOrders.aspx.cs" Inherits="Affinity.AdminOrders" Title="Untitled Page" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="pain_one" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>Manage Orders</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <div class="search_filter">Find Orders: 
        <asp:TextBox ID="txtQuery" runat="server"></asp:TextBox>
        <asp:CheckBox ID="cbShowClosed" Text="Include Closed" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RadioButton ID="cbShowStandard" GroupName="StandardImported" Value="Standard" OnClick="$('#ctl00_content_cph_btnSearch')[0].click();" Checked="true" Text="Standard" runat="server" />
        <asp:RadioButton ID="cbShowImported" GroupName="StandardImported" Value="Imported" OnClick="$('#ctl00_content_cph_btnSearch')[0].click();" Text="Imported" runat="server" />
        <asp:Button ID="btnSearch" runat="server" Text="Go" OnClick="btnSearch_Click" /></div>

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
    
    </ContentTemplate>
    </asp:UpdatePanel>
    
    <!-- refresh the page automatically after 5 minutes -->
    <script type="text/javascript">
        setTimeout("window.location.reload()",(5 * 60000))
    </script>

</asp:Content>


