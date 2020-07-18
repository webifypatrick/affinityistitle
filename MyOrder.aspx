<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeBehind="MyOrder.aspx.cs" Inherits="Affinity.MyOrder" Title="Untitled Page" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="pane_one" ContentPlaceHolderID="pain_one_cph" Runat="Server">
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" Runat="Server">
    <asp:Panel ID="pnlForm" runat="server" EnableViewState="false">
    <h2>Details For Order 
        <asp:Label ID="lblWorkingId" runat="server" Text=""></asp:Label></h2>
    </asp:Panel>
 
    <asp:Panel ID="pnlActions" runat="server">
    </asp:Panel>
    <p><a href="MyAccount.aspx" class="order">Return to My Account...</a></p>
             
        <div class="fields">
        
		    <div class="groupheader">Property Information [<asp:HyperLink ID="lnkChange" runat="server">Edit</asp:HyperLink>]</div>
		    <fieldset id="general">
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
			        <div id="IdentifierNumberDIV" runat="server" class="field horizontal">
				        <div class="label horizontal">Identifier Number:</div>
				        <div class="input horizontal">
               		<asp:Label CssClass="readonly" ID="txtIdentifierNumber" runat="server"></asp:Label>
                </div>
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
		    </fieldset>
    
		    <div class="groupheader">Attachments</div>
		    <fieldset id="attachments">

                <p><em>Please note that you may customize the way attachments are displayed in your browser
                on the "My Preferences" page.  If you experience problems downloading attachments, you 
                may try selecting a different download option.</em></p>

                <asp:Panel ID="pnlAttachments" runat="server">
                </asp:Panel>
                <asp:Panel ID="pnlStandardAttachments" runat="server">
	                <div><a id="BlankControlledOwners" runat="server" href="downloads/Disclosure_Owner.doc" class="download">Blank Controlled Business Disclosure Statement - For Owners</a></div>
	                <div><a id="BlankControlledAgents" runat="server" href="downloads/Disclosure_Agent.doc" class="download">Blank Controlled Business Disclosure Statement -  For Agents</a></div>
	                <div><a href="downloads/Examination_Agent.doc" class="download">Blank Examination Form - For Agents</a></div>
	                <div><a href="AgentExaminationForm.aspx?id=<%=NoNull.GetString(Request["id"]) %>" class="download">Online Examination Form - For Agents</a></div>
                </asp:Panel>
                <div><a href="UploadFile.aspx?id=<%=NoNull.GetString(Request["id"]) %>" class="download">Upload File</a></div>
                
       		</fieldset>
      		
            <asp:Panel ID="pnlRequests" runat="server">
            </asp:Panel>
		    
		    <div class="groupheader">Request History</div>
		    <fieldset id="history">
		        <p>Below is the request history for this order:</p>
    
                <asp:GridView 
                    ID="rGrid" 
                    EnableSortingAndPagingCallbacks="true" 
                    AllowPaging="false" AllowSorting="false" 
                    GridLines="None" 
                    runat="server" 
                    CssClass="subtle" 
                    AutoGenerateColumns="false">
                    <AlternatingRowStyle CssClass="odd" />
                        <Columns>
                            <asp:BoundField DataField="RequestTypeCode" HeaderText="Request Type" />

                            <asp:BoundField DataField="StatusCode" HeaderText="Status" />
                            <asp:BoundField DataField="Created" HeaderText="Submitted" DataFormatString="{0:M/dd/yyyy hh:mm tt}" HtmlEncode="false" />
                        </Columns>
                </asp:GridView>
      		</fieldset>
	    </div>
</asp:Content>


