<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AffinityTemplate.master" CodeBehind="AdminRequestType.aspx.cs" Inherits="Affinity.AdminRequestType" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="reload_content_title" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>Manage Request Type</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">

    <p><a href="AdminRequestTypes.aspx" class="back">Return to All Request Types...</a></p>

	<asp:HiddenField ID="codeHdn" runat="server" />
		<p>Please assign the export formats for request type <span ID="RequestTypepnl" runat="server"></span>.</p>
        <div>
	        <div>
		        <div>Export Formats</div>
		        <div>
					<asp:checkboxlist Width="700" id="ExportFormatboxes" runat="server" >
						<asp:ListItem Text="PFT" Value="PFT=Export.aspx?id={ID}"></asp:ListItem>
						<asp:ListItem Text="PFT (Changes Only)" Value="PFT (Changes Only)=Export.aspx?id={ID}&format=change"></asp:ListItem>
						<asp:ListItem Text="Generic XML" Value="Generic XML=Export.aspx?id={ID}&format=xml&key=ts_id"></asp:ListItem>
						<asp:ListItem Text="REI XML" Value="REI XML=Export.aspx?id={ID}&format=rei&key=ts_id"></asp:ListItem>
					</asp:checkboxlist>
                </div>
	        </div>
        </div>
		   <br />     
		<p><asp:Button ID="btnSetExportFormats" runat="server" Text="Set Export Formats" OnClick="btnSetExportFormats_Click" /></p>

</asp:Content>

