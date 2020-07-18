<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeBehind="AdminAccounts.aspx.cs" Inherits="Affinity.AdminAccounts" Title="Untitled Page" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="pain_one" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>Manage Accounts</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" Runat="Server">
<table>
	<tr>
		<td>Search Users</td>
		<td>
			<form action="AdminAccounts.aspx" method="post">
				<input type="text" name="srch" />
				<button name="srchbuttn" onclick="this.form.submit();">Submit</button>
			</form>
		</td>
	</tr>
</table>
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
                    DataNavigateUrlFields="Id"
                    DataNavigateUrlFormatString="AdminAccount.aspx?id={0}" />
                <asp:BoundField DataField="Username" HeaderText="Username" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                
                <asp:TemplateField HeaderText="Company" SortExpression="State">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Company.Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:BoundField DataField="RoleCode" HeaderText="Role" />
                <asp:BoundField DataField="Modified" HeaderText="Modified" DataFormatString="{0:M/dd/yyyy}" HtmlEncode="false" />
                <asp:BoundField DataField="BusinessLicenseID" HeaderText="Business License ID" />
                <asp:BoundField DataField="IndividualLicenseID" HeaderText="Individual License ID" />
            </Columns>
    </asp:GridView>
    
    <h3>Add New Records:</h3>
    
        <asp:Panel ID="pnlActions" CssClass="actions" runat="server">
            <div><a class="account" href="AdminAccount.aspx">Add New Account</a></div>
            </asp:Panel>

</asp:Content>
