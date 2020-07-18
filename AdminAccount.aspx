<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeBehind="AdminAccount.aspx.cs" Inherits="Affinity.AdminAccount" Title="Untitled Page" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="pain_one" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>Edit Account</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" Runat="Server">
    
        <asp:Panel ID="pnlForm" runat="server">
        	<script type="text/javascript" id="ATG_JQUERY" src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
        <style>
				#outer_CopyApplicationTo td
				{
					white-space: nowrap;
					font-size: 14px;
					padding: 0;
				}
      	</style>
		    <script>
		    	$(function() {
		    		
		    	});
		    	function enableDisableCheckboxes(o) {
		    		//var checkboxes = $(o.parentElement.parentElement.parentElement).find("input");
		    		//prompt("", o.parentElement.parentElement.parentElement.outerHTML)
		    		//checkboxes.removeAttr("disabled");
		    		var row = o.parentElement.parentElement;
		    		var rows = row.parentElement.rows;
		    		var len = rows.length;
		    		var i = 0;
		    		
		    		for(i = 0; i < len; i++) {
			    		if(rows[i] == row) break;
			    	}
			    	
			    	var cells = $("#UnderwritersTable")[0].rows[1].cells;
			    	var len = cells.length;
			    	
			    	if(o.checked) {
				    	for(var j = 1; j < len; j++) {
				    		$($(cells[j]).find("table")[0].rows[i].cells[0]).find("span").removeAttr("disabled");
				    		$($(cells[j]).find("table")[0].rows[i].cells[0]).find("input").removeAttr("disabled");
				    	}
				    }
				    else {
				    	for(var j = 1; j < len; j++) {
				    		$($(cells[j]).find("table")[0].rows[i].cells[0]).find("span").attr("disabled", "disabled");
				    		$($(cells[j]).find("table")[0].rows[i].cells[0]).find("input").attr("disabled", "disabled");
				    	}
				    }
		    	}
		    	
		    	function verifyCheckboxes(o) {
		    		//var checkboxes = $(o.parentElement.parentElement.parentElement).find("input");
		    		//prompt("", o.parentElement.parentElement.parentElement.outerHTML)
		    		//checkboxes.removeAttr("disabled");
		    		var row = o.parentElement.parentElement;
		    		if(row.tagName.toUpperCase() != "TR") {
		    				row = row.parentElement;
		    		}
		    		
		    		var rows = row.parentElement.rows;
		    		var len = rows.length;
		    		var i = 0;
		    		
		    		for(i = 0; i < len; i++) {
			    		if(rows[i] == row) break;
			    	}

			    	var cells = $("#UnderwritersTable")[0].rows[1].cells;
			    	var len = cells.length;
			    	
			    	var cnt = 0;
			    	for(var j = 2; j < len; j++) {
			    		if($($(cells[j]).find("table")[0].rows[i].cells[0]).find("input")[0].checked) cnt++;
			    	}
			    	
			    	if(cnt > 1) {
			    		o.checked = false;
			    		alert("You cannot select more than 1 of these.");
			    	}
		    	}
		    </script>
        <div class="fields">

		    <div class="groupheader">Account Details</div>
		    <fieldset id="Account_fields">
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Id</div>
				        <div class="input horizontal">
                            <asp:Label ID="txtId" runat="server"></asp:Label></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Username</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtUsername" runat="server" Width="110px"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtUsername" runat="server" ErrorMessage="Please specify a Username" SetFocusOnError="true"></asp:RequiredFieldValidator>

                            </div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Password</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtPassword" runat="server" Width="110px"></asp:TextBox></div>
			        </div>
			        <div class="field horizontal">
				        <div class="label horizontal">Hint (optional)</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtPasswordHint" runat="server" Width="250px"></asp:TextBox></div>
			        </div>
		        </div>		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Name (First Last)</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtFirstName" runat="server" Width="90px"></asp:TextBox></div>
			        </div>
			        <div class="field horizontal">
				        <div class="label horizontal"></div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtLastName" runat="server" Width="125px"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Email</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtEmail" runat="server" Width="234px"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Role</div>
				        <div class="input horizontal">
                            <asp:DropDownList ID="ddRole" runat="server">
                            </asp:DropDownList></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Company</div>
				        <div class="input horizontal">
                            <asp:DropDownList ID="ddCompany" runat="server">
                            </asp:DropDownList>
                </div>
	        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">SoftPro ID</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtInternalId" runat="server"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Business License ID</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtBusinessLicenseId" runat="server"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Individual License ID</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtIndividualLicenseId" runat="server"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field vertical">
				        <div class="label vertical"></div>
				        <div class="input vertical">
                            If this account is a sub account subordinate to another account, please select the Master Account that this account is subordinate to.</div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Master Account</div>
				        <div class="input horizontal">
                    <asp:DropDownList ID="ddParentAccount" runat="server"></asp:DropDownList>
                </div>
			        </div>
		        </div>

		        <div class="line">
			        <div class="field vertical">
				        <div class="label vertical"></div>
				        <div class="input vertical">
                            Record was created <asp:Label ID="txtCreated" runat="server"></asp:Label>
                            and last modified <asp:Label ID="txtModified" runat="server"></asp:Label></div>
			        </div>
		        </div>

		    </fieldset>
	    </div>
	    
	    <div class="groupheader">Authorized Underwriters</div>
	    <div id="underwritersDIV">
	    <fieldset id="underwriters" style="padding-bottom: 10px;">
	        <table id="UnderwritersTable">
	        	<tr>
	        		<td style="vertical-align:top;">
	        			<div class="input vertical"><em>Please check the underwriters that this account is authorized to use:</em></div>
	        		</td>
	        		<td style="vertical-align:top;text-align:center;">
								<div class="input vertical">END</div>
							</td>
	        		<td style="vertical-align:top;">
								<div class="input vertical"></div>
	        		</td>
	        		<td style="vertical-align:top;">
								<div class="input vertical"></div>
	        		</td>
	        		<td style="vertical-align:top;">
								<div class="input vertical"></div>
	        		</td>
	        		<td style="vertical-align:top;">
								<div class="input vertical"></div>
	        		</td>
	        		<td style="vertical-align:top;">
								<div class="input vertical"></div>
	        		</td>
	        		<td style="vertical-align:top;">
								<div class="input vertical"></div>
	        		</td>
	        	</tr>
	        	<tr>
	        		<td style="vertical-align:top;">
	        			<div class="input vertical"><asp:CheckBoxList ID="cblUnderwriterCodes" runat="server"></asp:CheckBoxList></div>
	        		</td>
	        		<td style="vertical-align:top;">
								<div class="input vertical"><asp:CheckBoxList ID="Endorse100" runat="server"></asp:CheckBoxList></div>
							</td>
	        		<td style="vertical-align:top;">
								<div class="input vertical"><asp:CheckBoxList ID="Endorse50" runat="server"></asp:CheckBoxList></div>
	        		</td>
	        		<td style="vertical-align:top;">
								<div class="input vertical"><asp:CheckBoxList ID="Endorse75" runat="server"></asp:CheckBoxList></div>
	        		</td>
	        		<td style="vertical-align:top;">
								<div class="input vertical"><asp:CheckBoxList ID="Endorse80" runat="server"></asp:CheckBoxList></div>
	        		</td>
	        		<td style="vertical-align:top;">
								<div class="input vertical"><asp:CheckBoxList ID="Endorse85" runat="server"></asp:CheckBoxList></div>
	        		</td>
	        		<td style="vertical-align:top;">
								<div class="input vertical"><asp:CheckBoxList ID="Endorse88" runat="server"></asp:CheckBoxList></div>
	        		</td>
	        		<td style="vertical-align:top;">
								<div class="input vertical"><asp:CheckBoxList ID="Endorse90" runat="server"></asp:CheckBoxList></div>
	        		</td>
				</tr>
			</table>
      </fieldset>
    	</div>
	    
        <asp:Panel ID="pnlPreferences" runat="server">
        </asp:Panel>
	    
	    <div style="clear:left">
	    <p>
            <asp:Button ID="btnSave" runat="server" Text="Save Account" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            <asp:Button ID="btnCopyAccount" runat="server" Text="Copy this Account into a new Account" OnClientClick="document.getElementById('ctl00_content_cph_txtUsername').value = prompt('Please provide the UserName for the new account.', document.getElementById('ctl00_content_cph_txtUsername').value); return true;" OnClick="btnCopyAccount_Click" style="margin-left:100px;" />
        </p>
        </div>
    </asp:Panel>


</asp:Content>


