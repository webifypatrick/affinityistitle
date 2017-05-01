<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="MyOrderSubmit.aspx.cs" Inherits="MyOrderSubmit" MasterPageFile="~/AffinityTemplate.master"%>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2 id="header" runat="server">Submit a New Order</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:Panel ID="pnlForm" runat="server" EnableViewState="false">
    <asp:HiddenField ID="isDupOrderWarnedHdn" runat="server" Value="false" />
        <div class="fields">
		    <div class="groupheader">Personalize This Order</div>
		    
		    <fieldset id="general">
		    
		        <div class="line">
			        <div class="field vertical">
				        <div class="label vertical">Friendly Name:<br />
				        <em>Enter a name for this order (such as your client's name) that will be easily identifiable.</em>
				        </div>
				        <div class="input vertical">
                            <asp:TextBox ID="txtClientName" runat="server" Width="370" CssClass="textbox"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field vertical">
				        <div class="label vertical">Tracking Code (optional):<br />
				        <em>If have a Tracking Code that you use internally, you may enter it here to syncronize your records.</em>
				        </div>
				        <div class="input vertical">
                            <asp:TextBox ID="txtCustomerId" runat="server" CssClass="textbox"></asp:TextBox></div>
			        </div>
		        </div>
		        
		        <div class="line">
			        <div class="field vertical">
				        <div class="label vertical">Estimated Closing Date:<br />
				        <em>If you know the closing date, enter it here.  Otherwise please provide an estimated date.</em></div>
				        <div class="input vertical">
                            <asp:TextBox ID="txtClosingDate" runat="server" Width="100" CssClass="textbox date">
                            </asp:TextBox>
                            <ajaxToolkit:CalendarExtender 
                                ID="txtClosingDateCal" 
                                runat="server"
                                PopupButtonID = "txtClosingDateImg"
                                Format="MM/dd/yyyy"
                                Animated="true"
                                TargetControlID="txtClosingDate" ></ajaxToolkit:CalendarExtender>
                            <asp:Image 
                                ID="txtClosingDateImg"  
                                runat="server" 
                                ImageUrl = "images/ico_calendar.gif"
                                CssClass = "calendar_button" />
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please provide an estimated closing date" ControlToValidate="txtClosingDate"></asp:RequiredFieldValidator>
                       </div>
			        </div>
		        </div>
		        
	        	<div id="messageDIV" style="display:none;position:absolute;top:0px;left:0px;width:100%;height:100%;"><div id="message" style="margin:250px auto; width:500px;height:300px;border:1px solid #AAAAAA;background:#FFFFFF;">
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
			        <div class="field vertical">
				        <div class="label vertical">Is this a purchase or refinance?:<br />
				        <em></em></div>
				        <div class="input vertical">
                            <asp:RadioButton ID="radioPurchase" GroupName="radioIsPurchaseRefinance" Checked="true" Text="Purchase" runat="server">
                            </asp:RadioButton>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="radioRefinance" GroupName="radioIsPurchaseRefinance" Text="Refinance" runat="server">
                            </asp:RadioButton>
                       </div>
			        </div>
		        </div>
		    </fieldset>

		    <div class="groupheader">Property Information</div>
		    
		    <fieldset id="propinfo">
		    
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Tax ID (PIN)</div>
				        <div class="input horizontal">
				        
                            <asp:TextBox ID="txtPIN" runat="server" CssClass="textbox"></asp:TextBox>
                            
                            </div>
                            <div class="label horizontal">County</div>
                            <asp:TextBox ID="txtPropertyCounty" runat="server" Width="150" CssClass="textbox"></asp:TextBox>
			        
                            <ajaxToolkit:AutoCompleteExtender 
                                runat="server" 
                                ID="txtPropertyCounty_ac" 
                                TargetControlID="txtPropertyCounty"
                                ServiceMethod="GetCounties"
                                ServicePath="AutoComplete.asmx" 
                                MinimumPrefixLength="1" 
                                CompletionInterval="500"
                                EnableCaching="true"
                                CompletionSetCount="12" />
			        
			        </div>
		        </div>
		        
		        
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Additional PINs</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtAdditionalPins" runat="server" Width="370" CssClass="textbox"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Property Address</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtPropertyAddress" runat="server" Width="370" CssClass="textbox"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Property Address 2</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtPropertyAddress2" runat="server" Width="370" CssClass="textbox"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Property City</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtPropertyCity" runat="server" CssClass="textbox"></asp:TextBox>
                            
                            <ajaxToolkit:AutoCompleteExtender 
                                runat="server" 
                                ID="txtPropertyCity_ac" 
                                TargetControlID="txtPropertyCity"
                                ServiceMethod="GetCities"
                                ServicePath="AutoComplete.asmx" 
                                MinimumPrefixLength="1" 
                                CompletionInterval="500"
                                EnableCaching="true"
                                CompletionSetCount="12" />

                            
                            </div>
			        </div>
			        <div class="field horizontal">
				        <div class="label horizontal">State</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtPropertyState" runat="server" Text="IL" Width="25" CssClass="textbox"></asp:TextBox></div>
			        </div>
			        <div class="field horizontal">
				        <div class="label horizontal">Zip</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtPropertyZip" runat="server" Width="100" CssClass="textbox"></asp:TextBox>
                            
                            <ajaxToolkit:AutoCompleteExtender 
                                runat="server" 
                                ID="txtPropertyZip_ac" 
                                TargetControlID="txtPropertyZip"
                                ServiceMethod="GetZips"
                                ServicePath="AutoComplete.asmx" 
                                MinimumPrefixLength="2" 
                                CompletionInterval="500"
                                EnableCaching="true"
                                CompletionSetCount="12" />

                            </div>
			        </div>
		        </div>
		        




		        
		    </fieldset>
	    </div>
    </asp:Panel>
    
    <asp:Panel ID="pnlResults" runat="server" EnableViewState="false" Visible="false">
    </asp:Panel>

    <p>
        <asp:Button ID="btnSubmit" runat="server" Text="Continue..." OnClick="btnSubmit_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" /></p>

</asp:Content>