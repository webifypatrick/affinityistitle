<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="MyAccount.aspx.cs" Inherits="Affinity.MyAccount" MasterPageFile="~/AffinityTemplate.master"%>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>My Account</h2>
    <p>Welcome to your Affinity Title Services Personal Home Page.
    You may return to this page at any time by clicking "My Account" above.</p>

</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
    <div><a class="add" href="MyOrderSubmit.aspx">Submit a New Order</a></div>

    <h3>My Orders:</h3>

        <div class="search_filter">
        	<div class="label horizontal">Find Orders: </div>
	        <asp:TextBox ID="txtQuery" runat="server"></asp:TextBox>
	        <span>
	        	<asp:CheckBox ID="cbShowInactive" Text="Include Inactive Orders" runat="server" />
	        	<asp:Button ID="btnSearch" runat="server" Text="Go" OnClick="btnSearch_Click" />
	        </span>
        </div>

    <asp:GridView 
        ID="oGrid" 
        AllowPaging="true" 
        PageSize="10"
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
                    Text="Order Details"
                    DataNavigateUrlFields="Id"
                    DataNavigateUrlFormatString=
                    "MyOrder.aspx?id={0}" />
                <asp:BoundField DataField="WorkingId" HeaderText="Working ID" SortExpression="Id" />
                <asp:BoundField DataField="CustomerId" HeaderText="Tracking Code" SortExpression="CustomerId"/>
                <asp:BoundField DataField="ClientName" HeaderText="Name" SortExpression="ClientName" />
                <asp:BoundField DataField="Pin" HeaderText="PIN" SortExpression="Pin" />
                <asp:BoundField DataField="PropertyAddress" HeaderText="Address" SortExpression="PropertyAddress" />
                <asp:BoundField DataField="PropertyCity" HeaderText="City" SortExpression="PropertyCity" />

                <asp:BoundField DataField="CustomerStatusCode" HeaderText="Status" SortExpression="CustomerStatusCode" />
                <asp:BoundField DataField="ClosingDate" HeaderText="Closing Date" DataFormatString="{0:M/dd/yyyy}" HtmlEncode="False" SortExpression="ClosingDate" />
                <asp:BoundField DataField="Created" HeaderText="Submitted" DataFormatString="{0:M/dd/yyyy h:m tt}" HtmlEncode="false" SortExpression="Created" />
                <asp:BoundField DataField="Modified" HeaderText="Modified" DataFormatString="{0:M/dd/yyyy h:m tt}" HtmlEncode="false" SortExpression="Modified" />
            </Columns>
    </asp:GridView>    
    
    </ContentTemplate>
    </asp:UpdatePanel>
    
    <h3>Schedule a Closing Request:</h3>
    
    <p>
    To reserve a closing room for your Affinity order, simply locate your order in the 
    table above and click "<b>Order Details</b>."  At the top of the Order Details page 
    is a link labeled "<em>Add a Closing Schedule Request to this Order.</em>"  Use this
    link to schedule a closing request.  This will ensure that your closing
    request is always associated with the correct order information.
    </p>
    
    <asp:Panel ID="pnlActions" CssClass="actions" runat="server">
    </asp:Panel>
    
    <h3>Downloadable Forms and Information:</h3>
    
    <div></div>
    <div><a href="Content.aspx?page=clerkingprices" class="page">Advocate Title Clerking Services Price Sheet</a></div>
    <div id="DisclosureOwnerDocumentsDIV" runat="server" visible="false">
		<div><a href="downloads/ATS_Rates_for_Title_Insurance_and_Related_Services.doc" class="download">ATS Rates for Insurance and Related Services</a></div>
		<div><a id="BlankControlledOwners" runat="server" href="downloads/Disclosure_Owner.doc" class="download">Blank Controlled Business Disclosure Statement - For Owners</a></div>

	</div>	
	<div id="DisclosureAgentDocumentsDIV" runat="server" visible="false">
		<div><a href="downloads/Disclosure_Agent.doc" class="download">Blank Controlled Business Disclosure Statement -  For Agents</a></div>
		<div><a href="downloads/Examination_Agent.doc" class="download">Blank Examination Form - For Agents</a></div>
    </div>
</asp:Content>