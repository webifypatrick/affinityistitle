<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeBehind="UploadFile.aspx.cs" Inherits="Affinity.UploadFile" Title="Upload File" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="pain_one" ContentPlaceHolderID="pain_one_cph" Runat="Server">
<h2>Affinity Title Search</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>   
    
    <asp:Panel ID="pnlForm" runat="server">
        <div class="fields">
		    <div class="groupheader">Upload File</div>
		    <fieldset id="Account_fields">
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Notes</div>
				        <div class="input horizontal">
                  <asp:TextBox ID="txtNote" runat="server" Width="400px" CssClass="textbox"></asp:TextBox>
                </div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Attach File</div>
				        <div class="input horizontal">
                  <asp:DropDownList ID="ddFilePurpose" runat="server">
                  </asp:DropDownList>
                  <asp:FileUpload ID="fuAttachment" runat="server" CssClass="upload" multiple="multiple" />
                </div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Attachment Name</div>
				        <div class="input horizontal">
                  <asp:TextBox ID="txtAttachmentName" runat="server" Width="400px" CssClass="textbox"></asp:TextBox>
                  (<em>This name will identify the attachment</em>)
                </div>
			        </div>
		        </div>
		    </fieldset>
	    </div>
	    <p>
        <asp:Button ID="btnSubmit" runat="server" Text="Upload File" OnClick="btnSave_Click" /></p>
 	   <asp:Panel ID="pnlDetails" runat="server"></asp:Panel>
    </asp:Panel>
    
</asp:Content>
