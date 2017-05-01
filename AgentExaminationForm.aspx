<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeFile="AgentExaminationForm.aspx.cs" Inherits="AgentExaminationForm" Title="Agent Examination Form" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="pain_one" ContentPlaceHolderID="pain_one_cph" Runat="Server">
<h2>Affinity Title Search</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>   
    
    <asp:Panel ID="pnlForm" runat="server">
        <div class="fields">
		    <div class="groupheader">Examination of Search</div>
		    <fieldset id="Account_fields">
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Agent's Name:</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtAgentsName" runat="server" Width="350px"></asp:TextBox></div>
			        </div>
			        <div class="field horizontal">
				        <div class="label horizontal">Commitment Number</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtCommitmentNumber" runat="server" Width="203px"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Property Vesting:</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtPropertyVesting" runat="server" Width="700px"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Property Address:</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtPropertyAddress" runat="server" Width="700px"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">City, State, Zip:</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtPropertyCityStateZip" runat="server" Width="700px"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Property County:</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtPropertyCounty" runat="server" Width="234px"></asp:TextBox></div>
			        </div>
			        <div class="field horizontal">
				        <div class="label horizontal">PIN:</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtPIN" runat="server" Width="420px"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Prior Year:</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtPriorYear" runat="server" Width="100px"></asp:TextBox></div>
						<div class="label horizontal" style="margin-right:10px"> general taxes are paid thru</div>
			        </div>
				        <div class="label horizontal">1st Installment:</div>
				        <div class="input horizontal" style="margin-right:6px">
                            <asp:TextBox ID="txt1stInstallment" runat="server" Width="100px"></asp:TextBox></div>
				        <div class="label horizontal">2nd Installment:</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txt2ndInstallment" runat="server" Width="100px"></asp:TextBox></div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Current Year:</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtCurrentYear" runat="server"></asp:TextBox></div>
			        </div>
			        <div class="field horizontal">
				        <div class="label horizontal" style="margin-left:8px">tax amount: 1st</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtTaxAmount1st" runat="server"></asp:TextBox></div>
			        </div>
			        <div class="field horizontal">
				        <div class="label horizontal" style="margin-left:8px">tax amount: 2nd</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtTaxAmount2nd" runat="server"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal" style="width:337px">
			        </div>
			        <div class="field horizontal">
				        <div class="label horizontal">Due Date</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtDueDate1st" runat="server"></asp:TextBox></div>
			        </div>
			        <div class="field horizontal" style="margin-left:46px">
				        <div class="label horizontal">Due Date</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtDueDate2nd" runat="server"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Tax Year of Sold taxes:</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtTaxYearofSoldtaxes" runat="server"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        Mortgage(s) of Record:
				    </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
                            <asp:TextBox ID="txtMortgagesofRecord" runat="server" Height="100px" Width="830px" TextMode="MultiLine"></asp:TextBox></div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        Other Lien(s) of Record:
				    </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
                            <asp:TextBox ID="txtOtherLiensofRecord" runat="server" Height="100px" Width="830px" TextMode="MultiLine"></asp:TextBox></div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        Building Lines:
				    </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
                            <asp:TextBox ID="txtBuildingLines" runat="server" Height="100px" Width="830px" TextMode="MultiLine"></asp:TextBox></div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        Easements:
				    </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
                            <asp:TextBox ID="txtEasements" runat="server" Height="100px" Width="830px" TextMode="MultiLine"></asp:TextBox></div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        Covenants, Conditions, Restrictions Document Numbers:
				    </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
                            <asp:TextBox ID="txtDocumentNumbers" TextMode="MultiLine" runat="server" Height="100px" Width="830px"></asp:TextBox></div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_225">Homeowners/Condo Associations:</div>
				        <div class="input horizontal">
                            <asp:RadioButton ID="radioCondoAssociationYes" GroupName="radioCondoAssociation" Text="Yes" runat="server"></asp:RadioButton>&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="radioCondoAssociationNo" GroupName="radioCondoAssociation" Text="No" runat="server"></asp:RadioButton></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="input horizontal">
                            <asp:CheckBox ID="chkSearchPackageReviewed" runat="server" Text="The search and the search package has been reviewed and examined and we have made our determination of insurability and we direct ATS to issue the title commitment in accordance with the search package."></asp:CheckBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="input horizontal">
                            <asp:CheckBox ID="chkSearchPackageReviewedAmendments" runat="server" Text="The search and the search package has been reviewed and examined and we have made our determination of insurability and we direct ATS to issue the title commitment after making the following amendments."></asp:CheckBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="input horizontal">
                            <asp:TextBox runat="server" ID="txtAmendments" Height="100px" Width="830px" TextMode="MultiLine"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="input horizontal">
							Would like to have this form
							<asp:RadioButton ID="FaxRadio" GroupName="FaxorEmailRadio" runat="server" Text="Faxed" />or 
							<asp:RadioButton ID="EmailRadio" GroupName="FaxorEmailRadio" runat="server" Checked="true" Text="Emailed" />?
                        </div>
			        </div>
		        </div>
		    </fieldset>
	    </div>
	    <p>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit Agent Examination" OnClick="btnSubmit_Click" /></p>
    </asp:Panel>
    
    <script>
    	document.getElementById("ctl00_content_cph_FaxRadio").disabled = true;
    	var setTimeoutIdx = 0;
    	
    	function setToSave() {
    		clearTimeout(setTimeoutIdx);
    		setTimeoutIdx = setTimeout(saveChanges, 3000);
    	}
    	
    	$(function(){
    		$("input, select, textarea").keypress(setToSave);
    		$("input, select, textarea").change(setToSave);
    		});
    	
    	function saveChanges() {
				$.ajax({
				  type: "POST",
				  url: document.location.href + "&ajax=1",
				  data: $('#aspnetForm').serialize(),
				  success: function(o) {},
				  dataType: "html"
				});
    	}
 		</script>
       
</asp:Content>
