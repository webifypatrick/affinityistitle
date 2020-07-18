<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeBehind="AdminContent.aspx.cs" Inherits="Affinity.AdminContent" Title="Untitled Page" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="pain_one" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>Edit Site Content Page</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">

    <asp:Panel ID="pnlForm" runat="server">
        <div class="fields">

		    <div class="groupheader">Content Details</div>
		    <fieldset id="Content_fields">
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Code</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtCode" runat="server"></asp:TextBox></div>
                        &nbsp;This code controls the URL of the page (Content.aspx?page=CODE)</div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">MetaTitle</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtMetaTitle" runat="server" Width="450px"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">MetaKeywords</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtMetaKeywords" runat="server" Width="450px"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">MetaDescription</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtMetaDescription" runat="server" Width="450px"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Header</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtHeader" runat="server" Width="450px"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Body</div>
				        <div class="input horizontal">
				            <FTB:FreeTextBox id="txtBody" runat="Server" Height="200px" Width="650px" />
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
            <asp:Button ID="btnSave" runat="server" Text="Save Content" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" /></p>
    </asp:Panel>

</asp:Content>
