<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminCompany.aspx.cs" Inherits="Affinity.AdminCompany" MasterPageFile="~/AffinityTemplate.master"%>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="pane_one" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>Company Details</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">

        <div class="fields">

		    <div class="groupheader">Company Details</div>
		    <fieldset id="Company_fields">
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Id</div>
				        <div class="input horizontal">
                            <asp:Label ID="txtId" runat="server"></asp:Label></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Name</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtName" runat="server" Width="250px"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Created</div>
				        <div class="input horizontal">
                            <asp:Label ID="txtCreated" runat="server"></asp:Label></div>
			        </div>
		        </div>
		    </fieldset>
	    </div>
	    
	    <p>
            <asp:Button ID="btnSave" runat="server" Text="Save Company" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" /></p>
         <p>
             <asp:Button ID="btnDelete" runat="server" Text="Delete Company" OnClick="btnDelete_Click" /></p>
             
    
    </asp:Content>
