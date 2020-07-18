<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="Admin.aspx.cs" Inherits="Affinity.Admin" MasterPageFile="~/AffinityTemplate.master"%>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="pane_one" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>Administration Dashboard</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">

<div id="active_users_container">
    <h3>Currently Active Users</h3>

    <asp:Panel ID="pnlActive" runat="server">
    
    <div class='active clearfix'>
	    <div class='active_user'><b>User</b></div>
	    <div class='active_date'><b>Date</b></div>
	    <div class='active_time'><b>Last Click</b></div>
	</div>
	
    </asp:Panel>
    
    <p><em>Sessions expire after 
        <asp:Label ID="lblTimeout" runat="server" Text=""></asp:Label> minutes of inactivity</em></p>
</div>

<div id="management_container">
    <h3>Management</h3>

    <p><a class="order" href="AdminOrders.aspx" id="lnkManageOrders" runat="server" visible="true">Manage Orders</a></p>
    <p><a class="order" href="UploadOrderSubmit.aspx" id="lnkImportOrders" runat="server" visible="true">Import Orders</a></p>
    <p><a class="report" href="AdminReports.aspx" id="lnkReports" runat="server" visible="false">Reports</a></p>
    <p><a class="company" href="AdminCompanies.aspx" id="lnkManageCompanies" runat="server" visible="false">Manage Companies</a></p>
    <p><a class="company" href="AdminNotifications.aspx" id="lnkManageNotifications" runat="server" visible="false">Manage Notifications</a></p>
    <p><a class="account" href="AdminAccounts.aspx" id="lnkManageAccounts" runat="server" visible="false">Manage Accounts</a></p>
    <p><a class="account" href="AdminSiteContentByRoles.aspx" id="lnkSiteContentByRole" runat="server" visible="false">Manage Site Content By Role</a></p>
    <p><a class="content" href="AdminContents.aspx" id="lnkManageWebContent" runat="server" visible="false">Manage Web Content</a></p>
</div>

<div id="system_container" runat="server" visible="false">
    <h3>System Administration</h3>

    <p><a class="config" href="AdminFees.aspx">Update Fees</a></p>
    <p><a class="config" href="AdminAttachmentPurposes.aspx">Attachment Types</a></p>
    <p><a class="config" href="AdminSettings.aspx">System Settings</a></p>
    <p><a class="config" href="AdminRequestTypes.aspx">Request Types</a></p>
    <p><a class="server" href="AdminDebug.aspx">Debuging Information</a></p>
</div>

    <!-- refresh the page automatically after 2 minutes -->
    <script type="text/javascript">
        setTimeout("window.location.reload()", (2 * 60000))
    </script>


</asp:Content>