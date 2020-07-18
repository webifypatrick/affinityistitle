    <%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeBehind="AdminSiteContentByRole.aspx.cs" Inherits="Affinity.AdminSiteContentByRole" Title="Untitled Page" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="pain_one" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>Edit Site Content By Role</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" Runat="Server">
    
        <asp:Panel ID="pnlForm" runat="server">
        	<script type="text/javascript" id="ATG_JQUERY" src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
        <div class="fields">
            <asp:HiddenField ID="hdnID" Value="" runat="server" />
		    <div class="groupheader">Site Content By Role Details</div>
		    <fieldset id="Account_fields">
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal">For menus, any roles not defined will automatically have access to My Account, My Preferences, Forms, GFE Calculator, HUD Calculator, Contact and Logout.</div>
					</div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Content Type</div>
				        <div class="input horizontal">
                            <asp:DropDownList ID="ddContentType" runat="server">
                                <asp:ListItem>Menu</asp:ListItem>
                            </asp:DropDownList></div>
			        </div>
		        </div>
		        <div id="MenuDIV" class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Menu Items</div>
				        <div class="input horizontal">
                            <asp:ListBox ID="ddMenuItems" runat="server" SelectionMode="Multiple" style="height:100px;">
                                <asp:ListItem>My Account</asp:ListItem>
                                <asp:ListItem>Attorney Services</asp:ListItem>
                                <asp:ListItem style="display:none;">GFE Calculator</asp:ListItem>
                                <asp:ListItem>HUD Calculator</asp:ListItem>
                                <asp:ListItem>Forms</asp:ListItem>
                                <asp:ListItem>My Preferences</asp:ListItem>
                                <asp:ListItem>Administration</asp:ListItem>
                                <asp:ListItem style="display:none;">Contact</asp:ListItem>
                                <asp:ListItem>Demo</asp:ListItem>
                                <asp:ListItem style="display:none;">Logout</asp:ListItem>
                            </asp:ListBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">By Role</div>
				        <div class="input horizontal">
                            <asp:DropDownList ID="ddRole" runat="server">
                            </asp:DropDownList></div>
			        </div>
		        </div>

		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">By State</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtState" runat="server" Width="110px"></asp:TextBox>
                            </div>
			        </div>
		        </div>

		        <div class="line">
			        <div class="field vertical">
				        <div class="label vertical"></div>
				        <div class="input vertical">
                            Record was created <asp:Label ID="txtCreated" runat="server"></asp:Label>
                            and last modified <asp:Label ID="txtModified" runat="server"></asp:Label></div>
			        </div>
		        </div>

		    </fieldset>
	    </div>
	    
	    <div style="clear:left">
	    <p>
            <asp:Button ID="btnSave" runat="server" Text="Save Site Content By Role" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />

        </p>
        </div>
    </asp:Panel>


</asp:Content>


