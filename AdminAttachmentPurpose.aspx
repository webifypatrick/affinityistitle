<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminAttachmentPurpose.aspx.cs" Inherits="AdminAttachmentPurpose"  MasterPageFile="~/AffinityTemplate.master"%>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="pane_one" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>Attachment Type Details</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">

        <div class="fields">

		    <div class="groupheader">Attachment Type Details</div>
		    <fieldset id="AttachmentPurpose_fields">
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Code</div>
				        <div class="input horizontal">
				            <asp:Label ID="lblCode" runat="server" Visible="false"></asp:Label>
                            <asp:TextBox ID="txtCode" MaxLength="50" runat="server" Width="161px" Visible="false"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Description</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtDescription" MaxLength="100" runat="server" Width="371px"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125"> Notification</div>
				        <div class="input horizontal">
                            <asp:CheckBox ID="cbSendNotification" runat="server" /></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">
                            New Status Code</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtChangeStatusTo" MaxLength="15" runat="server" Width="56px"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Permission</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtPermissionRequired" MaxLength="3" runat="server" Width="55px"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">User Types</div>
				        <div class="label horizontal width_125">
							<asp:checkboxlist id="RolesCheckboxes" runat="server" />
                        </div>
			        </div>
		        </div>
		    </fieldset>
	    </div>
	    
	    <p>
            <asp:Button ID="btnSave" runat="server" Text="Save Attachment Type" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" /></p>
         <p>
             <asp:Button ID="btnDelete" runat="server" Text="Delete Attachment Type" OnClick="btnDelete_Click" /></p>
             
    
    </asp:Content>
