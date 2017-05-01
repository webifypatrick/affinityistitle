<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeFile="AdminRequest.aspx.cs" Inherits="AdminRequest" Title="Untitled Page" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>Process Request</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <p>
    <a href="AdminOrder.aspx?id=<% Response.Write(this.request.OrderId); %>" class="back">Return Order Details...</a>
    <a href="AdminOrders.aspx" class="back">Return All Orders...</a>
    </p>

    <asp:Panel ID="pnlForm" runat="server">
        <div class="fields">

		    <div class="groupheader">Request Summary</div>
		    <fieldset id="Request_fields">
            <div class="line highlight">
                Type: <asp:Label ID="txtRequestTypeCode" runat="server"></asp:Label>
                <span class="shim">&nbsp;</span><span class="shim">&nbsp;</span>
                Order Id: <asp:Label ID="txtId" runat="server"></asp:Label>
                <span class="shim">&nbsp;</span><span class="shim">&nbsp;</span>
                Request Id: <asp:Label ID="txtRequestId" runat="server"></asp:Label>
                <span class="shim">&nbsp;</span><span class="shim">&nbsp;</span>
                Submitted: <asp:Label ID="txtCreated" runat="server"></asp:Label>
                <span class="shim">&nbsp;</span><span class="shim">&nbsp;</span>
                By <asp:Label ID="txtOriginatorId" runat="server"></asp:Label>
            </div>
            
            <asp:Panel ID="pnlIsCurrent" runat="server" CssClass="warning" Visible="false">
            This is not the most current request for this order.  Please check the order history for changes.
            </asp:Panel>
            
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Status Code</div>
				        <div class="input horizontal">
                            <asp:DropDownList ID="ddStatus" runat="server" CssClass="select">
                            </asp:DropDownList></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Request Note</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtNote" runat="server" Width="400px" CssClass="textbox"></asp:TextBox>
                            (<em>General notes regarding this request</em>)
                            </div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Attach File</div>
				        <div class="input horizontal">
                            <asp:DropDownList ID="ddFilePurpose" runat="server">
                            </asp:DropDownList>
                            <asp:FileUpload ID="fuAttachment" runat="server" CssClass="upload" />
                        </div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Attachment Name</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtAttachmentName" runat="server" Width="400px" CssClass="textbox"></asp:TextBox>
                            (<em>This name will identify the attachment to the customer</em>)</div>
			        </div>
		        </div>
		    </fieldset>
	    </div>
	    
	    <p>
        <asp:CheckBox ID="cbEnableEmail" runat="server" Checked="true" EnableViewState="false" Text="Send an update notification to the customer (according to their email preferences)" />
	    <br />Append Note to the email: 
            <asp:TextBox ID="txtEmailNote" runat="server" Width="458px"></asp:TextBox>
	    </p>
	    
	    <p>
            <asp:Button ID="btnSave" runat="server" Text="Save Request" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
        </p>

        <div class="fields">

            <div class="groupheader">Changes in this Request From Previous</div>
            <fieldset>
     	    <asp:Panel ID="pnlChanges" runat="server"></asp:Panel>
     	    </fieldset>
        </div>
        
        <div class="fields">
        
		    <div class="groupheader">Property Information</div>
		    <fieldset id="general">
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Working ID</div>
				        <div class="input horizontal">
                            <asp:Label CssClass="readonly" ID="lblWorkingId" runat="server" Width="370"></asp:Label></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Friendly Name:</div>
				        <div class="input horizontal">
                            <asp:Label CssClass="readonly" ID="txtClientName" runat="server" Width="370"></asp:Label></div>
			        </div>
			        <div class="field horizontal">
				        <div class="label horizontal">Tracking Code:</div>
				        <div class="input horizontal">
                            <asp:Label CssClass="readonly" ID="txtCustomerId" runat="server"></asp:Label></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Tax ID (PIN)</div>
				        <div class="input horizontal">
                            <asp:Label CssClass="readonly" ID="txtPIN" runat="server"></asp:Label></div>
			        </div>
			        <div class="field horizontal">
				        <div class="label horizontal">County</div>
				        <div class="input horizontal">
                            <asp:Label CssClass="readonly" ID="txtPropertyCounty" runat="server" Width="150"></asp:Label></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Additional PINs</div>
				        <div class="input horizontal">
                            <asp:Label CssClass="readonly" ID="txtAdditionalPins" runat="server" Width="370"></asp:Label></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Property Address</div>
				        <div class="input horizontal">
                            <asp:Label CssClass="readonly" ID="txtPropertyAddress" runat="server" Width="370"></asp:Label></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Property Address 2</div>
				        <div class="input horizontal">
                            <asp:Label CssClass="readonly" ID="txtPropertyAddress2" runat="server" Width="370"></asp:Label></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Property City</div>
				        <div class="input horizontal">
                            <asp:Label CssClass="readonly" ID="txtPropertyCity" runat="server"></asp:Label></div>
			        </div>
			        <div class="field horizontal">
				        <div class="label horizontal">State</div>
				        <div class="input horizontal">
                            <asp:Label CssClass="readonly" ID="txtPropertyState" runat="server" Width="25"></asp:Label></div>
			        </div>
			        <div class="field horizontal">
				        <div class="label horizontal">Zip</div>
				        <div class="input horizontal">
                            <asp:Label CssClass="readonly" ID="txtPropertyZip" runat="server" Width="100"></asp:Label></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal">Estimated Closing Date</div>
				        <div class="input horizontal">
                            <asp:Label CssClass="readonly" ID="txtClosingDate" runat="server" Width="100"></asp:Label></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal">Property Use</div>
				        <div class="input horizontal">
                            <asp:Label CssClass="readonly" ID="txtPropertyUse" runat="server" Width="100"></asp:Label></div>
			        </div>
		        </div>
		    </fieldset>
        </div>

 	   <asp:Panel ID="pnlDetails" runat="server"></asp:Panel>
	        
   </asp:Panel>

</asp:Content>
