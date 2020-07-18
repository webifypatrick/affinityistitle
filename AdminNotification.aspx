<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminNotification.aspx.cs" Inherits="Affinity.AdminNotification" MasterPageFile="~/AffinityTemplate.master"%>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="pane_one" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>Notification</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">

        <div class="fields">

		    <div class="groupheader">Notification</div>
		    <fieldset id="Notification_fields">
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Id</div>
				        <div class="input horizontal">
                            <asp:Label ID="txtId" runat="server"></asp:Label></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Subject</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtSubject" runat="server" Width="250px"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Message</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtMessage" TextMode="MultiLine" runat="server" Width="250px" Height="400px"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Valid From</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtValidFrom" runat="server" Width="250px"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Valid To</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtValidTo" runat="server" Width="250px"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Visible To</div>
				        <div class="input horizontal">
                            <asp:ListBox Height="200" ID="selAccounts" SelectionMode="Multiple" runat="server" Width="250px"></asp:ListBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Created</div>
				        <div class="input horizontal">
                            <asp:Label ID="txtCreated" runat="server"></asp:Label></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Modified</div>
				        <div class="input horizontal">
                            <asp:Label ID="txtModified" runat="server"></asp:Label></div>
			        </div>
		        </div>
		    </fieldset>
	    </div>
	    
	    <p>
            <asp:Button ID="btnSave" runat="server" Text="Save Notification" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" /></p>
         <p>
             <asp:Button ID="btnDelete" runat="server" Text="Delete Notification" OnClick="btnDelete_Click" /></p>
             
    
    </asp:Content>
