<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeBehind="AdminOrder.aspx.cs" Inherits="Affinity.AdminOrder" Title="Untitled Page" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="pain_one" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>Process Order</h2>
</asp:Content>


<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<script>
	jQuery(function() {
				var $obj = $("#ctl00_content_cph_txtInternalId");
        // If this function exists...
        var val = $obj.val();
        $obj.val(""); //clear the value of the element
				$obj.val(val); 
	});
</script>
    <p><a href="AdminOrders.aspx" class="back">Return to All Orders...</a></p>

   <asp:Panel ID="pnlForm" runat="server">
        <div class="fields">

		    <div class="groupheader">Requests</div>
			<p><a href="Export.aspx?id=<%=Request["id"].ToString()%>&format=entire">Export Entire Order</a></p>
		    <fieldset id="requests">
		    <div><em>All activity is listed in descending chronological order with the most current at the top of the list.
		    The changes made from one request to the next are viewable by clicking on "Process Request."</em></div>
		    <div ID="PreviousOrderNotice" runat="server" visible="false" class="notice"></div>
                <asp:GridView 
                    ID="rGrid" 
                    EnableSortingAndPagingCallbacks="false" 
                    AllowPaging="false" AllowSorting="false" 
                    GridLines="None" 
                    runat="server" 
                    CssClass="subtle"
                    OnRowDataBound="gvrGrid_rowDataBound"
                    AutoGenerateColumns="false">
                    <AlternatingRowStyle CssClass="odd" />
                        <Columns>
                            <asp:HyperLinkField
                                Text="Process"
                                DataNavigateUrlFields="Id"
                                DataNavigateUrlFormatString="AdminRequest.aspx?id={0}"
                                ControlStyle-CssClass="process_request" />

                            <asp:TemplateField HeaderText="" SortExpression="Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lbTest" runat="server" Text="Label"><a href="" onclick="return false;" class="export">Export Request...</a></asp:Label>
                                        <asp:Panel ID="pnlExport" runat="server" CssClass="export_panel">
											<%# System.Text.RegularExpressions.Regex.Replace("," + DataBinder.Eval(Container.DataItem, "RequestType.ExportFormats").ToString().Replace("{ID}", DataBinder.Eval(Container.DataItem, "Id").ToString()), ",([^=]*)=([^,]*)", "<div>+ <a href=\"$2\" onclick=\"AjaxControlToolkit.PopupControlBehavior.__VisiblePopup.hidePopup();return true;\">Export $1...</a></div>", System.Text.RegularExpressions.RegexOptions.ECMAScript) %>
                                            <div>+ <a href="#" onclick="AjaxControlToolkit.PopupControlBehavior.__VisiblePopup.hidePopup();return false;">Cancel</a></div>
                                        </asp:Panel>
                                    <ajaxToolkit:PopupControlExtender ID="PopEx" runat="server"
                                        TargetControlID="lbTest"
                                        PopupControlID="pnlExport"
                                        Position="Bottom" />
                               </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="RequestTypeCode" HeaderText="Request Type" />

                            <asp:BoundField DataField="StatusCode" HeaderText="Status" />
                            <asp:BoundField DataField="Created" HeaderText="Submitted" DataFormatString="{0:M/dd/yyyy hh:mm tt}" HtmlEncode="true" />
                            <asp:TemplateField HeaderText="Export History" SortExpression="">
								<ItemTemplate>
									<asp:Label ID="lblExportHistory" runat="server" Visible="false" Text="Export History"><a href="" onclick="var obj = parentNode.nextSibling.childNodes[2]; if(typeof obj == 'undefined'){obj = parentNode.nextSibling.childNodes[1];} if(obj.scrollHeight > 200){obj.style.height = '200px'; } else {obj.style.height = 'auto';} return false;" class="page">Export History...</a></asp:Label><asp:Panel ID="pnlExportHistory" Visible="false" runat="server" CssClass="export_panel"><table cellpadding="0" cellspacing="0" border="0"><tr><th nowrap style="font-size:12px;width:61px">Username</th><th nowrap style="font-size:12px;width:133px">Export Format</th><th nowrap style="font-size:12px;width:92px">Create Date</th><th nowrap style="font-size:12px;width:10px"><a href='#' onclick='AjaxControlToolkit.PopupControlBehavior.__VisiblePopup.hidePopup();return false;'>Close</a></th></tr></table><asp:Panel ID="elGridPanel" ScrollBars="Auto" Height="0" runat="server">
										<asp:GridView
											ID="elGrid"
											OnRowDataBound="gvelGrid_rowDataBound"
											EnableSortingAndPagingCallbacks="false" 
											AllowPaging="false" AllowSorting="false" 
											GridLines="None" 
											BorderWidth = "0"
											ShowHeader="false"
											Width="350"
											runat="server" 
											AutoGenerateColumns="false">
											<EmptyDataTemplate>
											<a href="#" onclick="AjaxControlToolkit.PopupControlBehavior.__VisiblePopup.hidePopup();return false;">No History</a>
											</EmptyDataTemplate>
											<AlternatingRowStyle CssClass="odd" />
											<Columns>
												 <asp:TemplateField ItemStyle-Width="65" HeaderText="Username" SortExpression="">
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "Account.Username")%>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:BoundField  ItemStyle-Width="150" DataField="ExportFormat" HeaderText="Export Format" />
												<asp:BoundField ItemStyle-Width="135" DataField="Created" HeaderText="Create Date" />
											</Columns>
										</asp:GridView>
									</asp:Panel>
									</asp:Panel>
                                    <ajaxToolkit:PopupControlExtender ID="PopExExportHistory" runat="server" 
                                        TargetControlID="lblExportHistory"
                                        PopupControlID="pnlExportHistory"
                                        Position="Bottom" />
								</ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Upload History" SortExpression="">
								<ItemTemplate>
									<asp:Label ID="lblUploadHistory" runat="server" Visible="false" Text="Upload History"><a href="" onclick="var obj = parentNode.nextSibling.childNodes[2]; if(typeof obj == 'undefined'){obj = parentNode.nextSibling.childNodes[1];} if(obj.scrollHeight > 200){obj.style.height = '200px'; } else {obj.style.height = 'auto';} return false;" class="page">Upload History...</a></asp:Label><asp:Panel ID="pnlUploadHistory" Visible="false" runat="server" CssClass="export_panel"><table cellpadding="0" cellspacing="0" border="0"><tr><th nowrap="nowrap" style="font-size:12px;width:61px">Username</th><th nowrap style="font-size:12px;width:60px">Name</th><th nowrap="nowrap" style="font-size:12px;width:43px">MIME</th><th nowrap style="font-size:12px;width:120px">Create Date</th><th nowrap="nowrap" style="font-size:12px;width:10px"><a href='#' onclick='AjaxControlToolkit.PopupControlBehavior.__VisiblePopup.hidePopup();return false;'>Close</a></th></tr></table><asp:Panel ID="ulGridPanel" ScrollBars="Auto" Height="0" runat="server">							
										<asp:GridView
											ID="ulGrid"
											OnRowDataBound="gvulGrid_rowDataBound"
											EnableSortingAndPagingCallbacks="false" 
											AllowPaging="false" AllowSorting="false" 
											GridLines="None" 
											BorderWidth = "0"
											ShowHeader="false"
											Width="350"
											runat="server" 
											AutoGenerateColumns="false">
											<EmptyDataTemplate>
											<a href="#" onclick="AjaxControlToolkit.PopupControlBehavior.__VisiblePopup.hidePopup();return false;">No History</a>
											</EmptyDataTemplate>
											<AlternatingRowStyle CssClass="odd" />
											<Columns>
												 <asp:TemplateField ItemStyle-Width="95" HeaderText="Username" SortExpression="">
													<ItemTemplate>
														<%# ((int.Parse(DataBinder.Eval(Container.DataItem, "UploadAccountID").ToString()) > 0) ? DataBinder.Eval(Container.DataItem, "UploadAccount.Username") : DataBinder.Eval(Container.DataItem, "Account.Username"))%>
													</ItemTemplate>
												</asp:TemplateField>
												 <asp:TemplateField ItemStyle-Width="100" HeaderText="Name" SortExpression="">
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "Attachment.Name")%>
													</ItemTemplate>
												</asp:TemplateField>
												 <asp:TemplateField ItemStyle-Width="80" HeaderText="MIME" SortExpression="">
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem, "Attachment.MimeType")%>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:BoundField ItemStyle-Width="250" DataField="Created" HeaderText="Create Date" />
											</Columns>
										</asp:GridView>
									</asp:Panel>
									</asp:Panel>
                                    <ajaxToolkit:PopupControlExtender ID="PopExUploadHistory" runat="server" 
                                        TargetControlID="lblUploadHistory"
                                        PopupControlID="pnlUploadHistory"
                                        Position="Bottom" />
								</ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                </asp:GridView>
		    </fieldset>
		    


		    <div class="groupheader">Attachments</div>
		    <fieldset id="attachments">
                <asp:Panel ID="pnlAttachments" runat="server">
                </asp:Panel>
      		</fieldset>
      		
		    <div class="groupheader">Order Details</div>
		    <fieldset id="Order_fields">
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Web Id</div>
				        <div class="input horizontal">
                            <asp:Label ID="txtId" runat="server"></asp:Label></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">AFF ID</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtInternalId" runat="server"></asp:TextBox>
                            <asp:Button ID="btnConfirm" runat="server" Text="Confirm Order" OnClick="btnConfirm_Click" />
                            <em>(Assigns AFF ID and Notifies Customer)</em>
                            
                            </div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">ATS ID</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtInternalATSId" runat="server"></asp:TextBox>
                            <em>(Assigns ATS ID)</em>
                            
                            </div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Status Code</div>
				        <div class="input horizontal">
                            <asp:Label ID="lblStatus" runat="server">
                            </asp:Label></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Originator</div>
				        <div class="input horizontal">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="txtOriginator" runat="server">
                                </asp:Label>
                                <asp:Button ID="btnReAssign" runat="server" Text="Re-Assign" OnClick="btnReAssign_Click" visible="false" />
                                
                                <asp:DropDownList ID="ddNewOriginator" runat="server" Visible="false">
                                </asp:DropDownList>
                                <asp:Button ID="btnDoReAssign" runat="server" Text="OK" Visible="false" OnClick="btnDoReAssign_Click" />
                                
                            </ContentTemplate>
                            </asp:UpdatePanel>
                           </div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Closing Date</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtClosingDate" runat="server"></asp:TextBox></div>
			        </div>
		        </div>
	        	<div id="messageDIV" style="display:none;position:absolute;top:0px;left:0px;width:100%;height:100%;"><div id="message" style="margin:400px auto; width:500px;height:300px;border:1px solid #AAAAAA;background:#FFFFFF;">
	        		<div style="height:30px;width:100%;background:#EEEEEE"><div style="margin-top:5px;margin-left:20px;float:left;">Property Use</div><div onclick="document.getElementById('messageDIV').style.display = 'none';" style="float:right;margin-right:10px;color:#0000FF;cursor:pointer;">x</div></div>
	        		<div style="padding:20px;">
	        			All policies insuring title to real property must be classified as either residential or nonresidential(do not classify policies as "other"). Residential policies mean title insurance policies that insure the title to real property having a house, individual condominium unit, mobile home permanently affixed to real estate, or other dwelling unit intended principally for the occupancy of from one to four (1–4) families, but does not include multi-family structures intended for the use of 5+ families, undeveloped lots, or real estate intended principally for business, commercial, industrial, religious, educational or agricultural purposes even if some portion of the real estate is used for residential purposes.
	        		</div>
	        	</div></div>
		        <div class="line">
			        <div class="field vertical">
				        <div class="label vertical">Property Use: &nbsp;&nbsp;<a href="#" onclick="document.getElementById('messageDIV').style.display = 'block'; return false;">?</a>
				        <em></em></div>
				        <div class="input vertical">
                            <asp:RadioButton ID="radioResidential" GroupName="radioIsResidential" Checked="true" Text="Residential" runat="server">
                            </asp:RadioButton>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="radioNonResidential" GroupName="radioIsResidential" Text="Non-Residential" runat="server">
                            </asp:RadioButton>
                       </div>

			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">
                            Tax ID (PIN)</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtPin" runat="server"></asp:TextBox>
                            County
                            <asp:TextBox ID="txtPropertyCounty" runat="server"></asp:TextBox>
                        </div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Additional PINs</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtAdditionalPins" runat="server" Width="370px"></asp:TextBox>
                            (<em>Separate PINs with a comma</em>)</div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Property Address</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtPropertyAddress" runat="server" Width="370px"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Property Address 2</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtPropertyAddress2" runat="server" Width="370px"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Property City</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtPropertyCity" runat="server"></asp:TextBox>
                            State <asp:TextBox ID="txtPropertyState" runat="server" Width="44px"></asp:TextBox>
                            Zip <asp:TextBox ID="txtPropertyZip" runat="server" Width="95px"></asp:TextBox></div>
			        </div>
		        </div>
		        
		        <div class="line">
                    Order Submitted: <asp:Label ID="txtCreated" runat="server"></asp:Label>,
                    Last Modified:<asp:Label ID="txtModified" runat="server"></asp:Label>
                </div>

		    </fieldset>

		    <div class="groupheader">Personalized Settings</div>
		    <fieldset id="personalized">
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal">Client Name</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtClientName" runat="server"></asp:TextBox></div>
			        </div>
			        <div class="field horizontal">
				        <div class="label horizontal">Tracking Code</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtCustomerId" runat="server"></asp:TextBox></div>
			        </div>
			    </div>
			</fieldset>
		    
	    </div> <!-- /fields -->

	    <p>
            <asp:Button ID="btnSave" runat="server" Text="Save Order" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" /></p>
            
        <p>
            <asp:Button ID="btnClose" runat="server" Text="Close This Order" OnClick="btnClose_Click" />
        </p>
            
    </asp:Panel>
		<asp:Panel ID="pnlContentScript" runat="server"></asp:Panel>
</asp:Content>

