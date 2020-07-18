<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeBehind="AdminFees.aspx.cs" Inherits="Affinity.AdminFees" Title="Admin Fees" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>Title Fees</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <p>
      <p id="Message" runat="server" style="font-size:18px;color:green;margin-left:10px;font-weight:bold;"></p>
	    <div class="fields">
		    <div id="group_path" class="groupshell" >
		        <div id="group_path_header" class="groupheader">Insurance Rates</div>
		        <fieldset id="group_insurance_rates_fieldset" runat="server">

                    <p><em>Update all Insurance Rates at once:</em></p>
								
								<table>
									<tr>
		                    <td style="text-align:right;padding-right:10px;width:400px;">
		                        Starting with $100,000 or less, the starting rate is:
		                    </td>
		                    <td>
		                        <asp:TextBox ID="StartingRate" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
		                    </td>
									</tr>
                    	<tr>
                    		<td style="text-align:right;padding-right:10px;width:400px;">
                        		Increment each rate by:
                        	</td>
                        	<td>
                        		<asp:TextBox ID="RateIncrement" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
                        	</td>
                       </tr>
                    </table>
                    <p><asp:Button ID="RateIncrementButton" runat="server" Text="Change Insurance Rates" OnClick="btnChangeInsuranceRates_Click" /></p>

                    <p>
                        OR
                    </p>
                
                   <p><em>Update 1 Insurance Rate at a time:</em></p>
								<table>
									<tr>
		                    <td style="text-align:right;padding-right:10px;width:400px;">
											Insurance Rate Level:
										</td>
                    		<td>
                    			<select Width="256px" ID="InsuranceRate" runat="server" onchange="$('#ctl00_content_cph_RateChange').val($(this.options[this.selectedIndex]).attr('rate'));"></select>
                    		</td>
                    	</tr>
                    	<tr>
		                    <td style="text-align:right;padding-right:10px;width:400px;">
											Change Rate To:
										</td>
                    		<td>
                    			<asp:TextBox ID="RateChange" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
                    		</td>
                    	</tr>
                    </table>
                    <p><asp:Button ID="InsuranceRateUpdateButton" runat="server" Text="Update Rate" OnClick="btnChangeInsuranceOneRate_Click" /></p>
                </fieldset>
            </div>
			    <div id="group_path" class="groupshell" >
		        <div id="group_path_header" class="groupheader">Fees</div>
		        <fieldset id="group_fee_fieldset" runat="server">
								<table>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Abstract Fee/Search Fee:
										</td>
              		<td>
              			<asp:TextBox ID="AbstractFee" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
              </table>
              <p><asp:Button ID="AbstractFeeButton" runat="server" Text="Update Fee" OnClick="btnChangeFee_Click" /></p>
			      </fieldset>
			    </div>
			    <div id="group_path" class="groupshell" >
		        <div id="group_path_header" class="groupheader">Mortgage Policy/Endorsement Fees</div>
		        <fieldset id="group_mortgage_fieldset" runat="server">
								<table>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Simultaneously Issued Mortgage Policy:
										</td>
              		<td>
              			<asp:TextBox ID="MortgagePolicy" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											ARM Endorsement:
										</td>
              		<td>
              			<asp:TextBox ID="ARMEndorsement" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Balloon Endorsement:
										</td>
              		<td>
              			<asp:TextBox ID="BalloonEndorsement" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Condominium Endorsement:
										</td>
              		<td>
              			<asp:TextBox ID="CondominiumEndorsement" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											EPA Endorsement:
										</td>
              		<td>
              			<asp:TextBox ID="EPAEndorsement" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Location Endorsement:
										</td>
              		<td>
              			<asp:TextBox ID="LocationEndorsement" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											PUD Endorsement:
										</td>
              		<td>
              			<asp:TextBox ID="PUDEndorsement" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Revolving Credit Mortgage Endorsement:
										</td>
              		<td>
              			<asp:TextBox ID="RevolvingCreditMortgageEndorsement" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											CPL Lender Endorsement:
										</td>
              		<td>
              			<asp:TextBox ID="CPLLenderEndorsement" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											CPL Buyer Endorsement:
										</td>
              		<td>
              			<asp:TextBox ID="CPLBuyerEndorsement" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											CPL Seller Endorsement:
										</td>
              		<td>
              			<asp:TextBox ID="CPLSellerEndorsement" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
              </table>
              <p><asp:Button ID="EndorsementFeesButton" runat="server" Text="Update Endorsement Fee" OnClick="btnChangeEndorsementFees_Click" /></p>
			      </fieldset>
			    </div>
 			    <div id="group_path" class="groupshell" >
		        <div id="group_path_header" class="groupheader">Escrow Services: Residential Closing Fees</div>
		        <fieldset id="group_escrow_fieldset" runat="server">
								<table>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											$100,000 or less:
										</td>
              		<td>
              			<asp:TextBox ID="Level100" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											$100,001 to $150,000:
										</td>
              		<td>
              			<asp:TextBox ID="Level150" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											$150,001 to $200,000:
										</td>
              		<td>
              			<asp:TextBox ID="Level200" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											$200,001 to $250,000:
										</td>
              		<td>
              			<asp:TextBox ID="Level250" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											$250,001 to $300,000:
										</td>
              		<td>
              			<asp:TextBox ID="Level300" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											$300,001 to $400,000:
										</td>
              		<td>
              			<asp:TextBox ID="Level400" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											$400,001 to $500,000:
										</td>
              		<td>
              			<asp:TextBox ID="Level500" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Lender Closing Statement:
										</td>
              		<td>
              			<asp:TextBox ID="LenderClosingStatement" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
              </table>
              <p><asp:Button ID="ResidentialClosingFeeButton" runat="server" Text="Update Residential Closing Fees" OnClick="btnResidentialClosingFee_Click" /></p>
			      </fieldset>
			    </div>
			    <div id="group_path" class="groupshell" >
		        <div id="group_path_header" class="groupheader">Refinance Title Insurance Rates</div>
		        <fieldset id="group_refinance_fieldset" runat="server">
								<table>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Refinance Title Insurance Rate:
										</td>
              		<td>
              			<asp:TextBox ID="RefinanceTitleInsuranceRate" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Refinance Title Insurance Minimum Fee:
										</td>
              		<td>
              			<asp:TextBox ID="RefinanceTitleInsuranceMinimumFee" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
              </table>
              <p><asp:Button ID="RefinanceButton" runat="server" Text="Update Refinance Title Insurance Rates" OnClick="btnRefinance_Click" /></p>
			      </fieldset>
			    </div>
			    <div id="group_path" class="groupshell" >
		        <div id="group_path_header" class="groupheader">Interim Risk Protection</div>
		        <fieldset id="group_interim_fieldset" runat="server">
								<table>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Fee:
										</td>
              		<td>
              			<asp:TextBox ID="InterimRiskFee" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Fee for each $1,000 of coverage in excess of $100,000:
										</td>
              		<td>
              			<asp:TextBox ID="InterimRiskOver100000Fee" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
              </table>
              <p><asp:Button ID="InterimRiskFeeButton" runat="server" Text="Update Interim Risk Protection" OnClick="btnInterimRiskFee_Click" /></p>
			      </fieldset>
			    </div>
			    <div id="group_path" class="groupshell" >
		        <div id="group_path_header" class="groupheader">Other Settlement Services Fees</div>
		        <fieldset id="group_settlement_fieldset" runat="server">
								<table>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											After Hours Closing Fee (per hour):
										</td>
              		<td>
              			<asp:TextBox ID="AfterHoursClosingFee" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Chain of Title:
										</td>
              		<td>
              			<asp:TextBox ID="ChainofTitle" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Commitment Later Date:
										</td>
              		<td>
              			<asp:TextBox ID="CommitmentLaterDate" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Dry Closing Fee:
										</td>
              		<td>
              			<asp:TextBox ID="DryClosingFee" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											E-Mail Package Handling Fee:
										</td>
              		<td>
              			<asp:TextBox ID="EMailPackageHandlingFee" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Joint Order Escrows:
										</td>
              		<td>
              			<asp:TextBox ID="JointOrderEscrows" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Overnight Handling Fee (each):
										</td>
              		<td>
              			<asp:TextBox ID="OvernightHandlingFee" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											PLPD Compliance Processing Fee:
										</td>
              		<td>
              			<asp:TextBox ID="PLPDComplianceProcessingFee" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Policy Update Date:
										</td>
              		<td>
              			<asp:TextBox ID="PolicyUpdateDate" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											In Home Remote Closing Fee:
										</td>
              		<td>
              			<asp:TextBox ID="InHomeRemoteClosingFee" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Title Indemnities Processing Fee:
										</td>
              		<td>
              			<asp:TextBox ID="TitleIndemnitiesProcessingFee" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Tax Payment Handling Fee:
										</td>
              		<td>
              			<asp:TextBox ID="TaxPaymentHandlingFee" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Chicago Water Certification Processing Fee:
										</td>
              		<td>
              			<asp:TextBox ID="ChicagoWaterCertificationProcessingFee" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Wire Incoming/ Outgoing Transfer Fee:
										</td>
              		<td>
              			<asp:TextBox ID="WireIncomingOutgoingTransferFee" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Certified Checks Fee:
										</td>
              		<td>
              			<asp:TextBox ID="CertifiedChecksFee" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
              </table>
              <p><asp:Button ID="OtherSettlementServicesFeesButton" runat="server" Text="Update Other Settlement Services Fees" OnClick="btnOtherSettlementServicesFees_Click" /></p>
			      </fieldset>
			    </div>
			    <div id="group_path" class="groupshell" >
		        <div id="group_path_header" class="groupheader">Second/Equity Mortgage Rates</div>
		        <fieldset id="group_equity_fieldset" runat="server">
								<table>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Up to $100,000.00:
										</td>
              		<td>
              			<asp:TextBox ID="Upto100000" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											$100,001 to $110,000:
										</td>
              		<td>
              			<asp:TextBox ID="Level110000" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											$110,001 to $120,000:
										</td>
              		<td>
              			<asp:TextBox ID="Level120000" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											$120,001 to $130,000:
										</td>
              		<td>
              			<asp:TextBox ID="Level130000" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											$130,001 to $140,000:
										</td>
              		<td>
              			<asp:TextBox ID="Level140000" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											$140,001 to $150,000:
										</td>
              		<td>
              			<asp:TextBox ID="Level150000" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											$150,001 to $160,000:
										</td>
              		<td>
              			<asp:TextBox ID="Level160000" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											$160,001 to $170,000:
										</td>
              		<td>
              			<asp:TextBox ID="Level170000" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											$170,001 to $180,000:
										</td>
              		<td>
              			<asp:TextBox ID="Level180000" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											$180,001 to $190,000:
										</td>
              		<td>
              			<asp:TextBox ID="Level190000" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											$190,001 to $200,000:
										</td>
              		<td>
              			<asp:TextBox ID="Level200000" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											for every $1,000 over $200,000 up to $500,000:
										</td>
              		<td>
              			<asp:TextBox ID="Over200000" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Agency Closing Services:
										</td>
              		<td>
              			<asp:TextBox ID="AgencyClosingServices" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
									<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Endorsements (All):
										</td>
              		<td>
              			<asp:TextBox ID="EndorsementsAll" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
              </table>
              <p><asp:Button ID="SecondEquityMortgageRatesButton" runat="server" Text="Update Second/Equity Mortgage Rates" OnClick="btnSecondEquityMortgageRates_Click" /></p>
			      </fieldset>
			    </div>
			    <div id="group_path" class="groupshell" >
		        <div id="group_path_header" class="groupheader">New RESPA Good Faith Estimate (GFE) and standard fees</div>
		        <fieldset id="group_gfe_fieldset" runat="server">
								<table>
								<tr>
									<td>&nbsp;</td>
									<td>PURCHASE</td>
									<td>AGENT REFI</td>
									<td>REFI</td>
								</tr>
								<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											1st Mortgage Closing Fee:
									</td>
              		<td>
              			<asp:TextBox ID="FirstMortgageClosingFeePurchase" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              		<td>
              			<asp:TextBox ID="FirstMortgageClosingFeeAgentRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              		<td>
              			<asp:TextBox ID="FirstMortgageClosingFeeRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
								<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											2nd Mortgage Closing Fee:
										</td>
              		<td>
              			<asp:TextBox ID="SecondMortgageClosingFeePurchase" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
               		<td>
              			<asp:TextBox ID="SecondMortgageClosingFeeRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              		<td>
              			<asp:TextBox ID="SecondMortgageClosingFeeAgentRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
             		</tr>
								<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Abstract Fee:
									</td>
              		<td>
              			<asp:TextBox ID="AbstractFeePurchase" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              		<td>
              			<asp:TextBox ID="AbstractFeeAgentRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              		<td>
              			<asp:TextBox ID="AbstractFeeRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
								<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Email Package Handling Fee:
									</td>
              		<td>
              			<asp:TextBox ID="EmailPackageHandlingFeePurchase" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
               		<td>
              			<asp:TextBox ID="EmailPackageHandlingFeeAgentRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              		<td>
              			<asp:TextBox ID="EmailPackageHandlingFeeRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
             		</tr>
								<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											1st Loan Policy:
									</td>
              		<td>
              			<asp:TextBox ID="FirstLoanPolicyPurchase" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
               		<td>
              			<asp:TextBox ID="FirstLoanPolicyAgentRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              		<td>
              			<asp:TextBox ID="FirstLoanPolicyRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
	             	</tr>
								<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											2nd Loan Policy:
									</td>
              		<td>
              			<asp:TextBox ID="SecondLoanPolicyPurchase" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              		<td>
              			<asp:TextBox ID="SecondLoanPolicyAgentRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              		<td>
              			<asp:TextBox ID="SecondLoanPolicyRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
								<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Endorsements:
									</td>
              		<td>
              			<asp:TextBox ID="EndorsementsPurchase" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              		<td>
              			<asp:TextBox ID="EndorsementsAgentRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              		<td>
              			<asp:TextBox ID="EndorsementsRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
								<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											PLDP Compliance Processing Fee:
									</td>
              		<td>
              			<asp:TextBox ID="PLDPComplianceProcessingFeePurchase" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
               		<td>
              			<asp:TextBox ID="PLDPComplianceProcessingFeeAgentRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              		<td>
              			<asp:TextBox ID="PLDPComplianceProcessingFeeRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
								</tr>
								<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Chain of Title:
									</td>
              		<td>
              			<asp:TextBox ID="ChainofTitlePurchase" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              		<td>
              			<asp:TextBox ID="ChainofTitleAgentRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              		<td>
              			<asp:TextBox ID="ChainofTitleRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
								<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Policy Update Fee:
									</td>
              		<td>
              			<asp:TextBox ID="PolicyUpdateFeePurchase" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              		<td>
              			<asp:TextBox ID="PolicyUpdateFeeAgentRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              		<td>
              			<asp:TextBox ID="PolicyUpdateFeeRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
								<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Overnight Handling Fee (each):
									</td>
              		<td>
              			<asp:TextBox ID="OvernightHandlingFeeEachPurchase" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              		<td>
              			<asp:TextBox ID="OvernightHandlingFeeEachAgentRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              		<td>
              			<asp:TextBox ID="OvernightHandlingFeeEachRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
								<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Wire Transfer Fee (each):
									</td>
              		<td>
              			<asp:TextBox ID="WireTransferFeeEachPurchase" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              		<td>
              			<asp:TextBox ID="WireTransferFeeEachAgentRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              		<td>
              			<asp:TextBox ID="WireTransferFeeEachRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
								<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											State Policy Fee (each):
									</td>
              		<td>
              			<asp:TextBox ID="StatePolicyFeeEachPurchase" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              		<td>
              			<asp:TextBox ID="StatePolicyFeeEachAgentRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              		<td>
              			<asp:TextBox ID="StatePolicyFeeEachRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
								<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											CPL Endorsements:
									</td>
              		<td>
              			<asp:TextBox ID="CPLEndorsementsPurchase" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
               		<td>
              			<asp:TextBox ID="CPLEndorsementsAgentRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              		<td>
              			<asp:TextBox ID="CPLEndorsementsRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
								<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Deed, Mortgage, Release Recording (estimate):
										</td>
              		<td>
              			<asp:TextBox ID="DeedMortgageReleaseRecordingEstimatePurchase" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
               		<td>
              			<asp:TextBox ID="DeedMortgageReleaseRecordingEstimateAgentRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              		<td>
              			<asp:TextBox ID="DeedMortgageReleaseRecordingEstimateRefinance" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
								<tr>
		              <td colspan="4">
											&nbsp;
										</td>
              	</tr>
								<tr>
		              <td style="text-align:right;padding-right:10px;width:400px;">
											Refi rates per thousand over conforming:
										</td>
              		<td>
              			<asp:TextBox ID="RefiRatesPerThousandOverConforming" runat="server" CssClass="textbox text" Width="256px"></asp:TextBox>
              		</td>
              	</tr>
              </table>
              <p><asp:Button ID="GFEButton" runat="server" Text="Update New RESPA Good Faith Estimate Fee" OnClick="btnGFE_Click" /></p>
			      </fieldset>
			    </div>
       </div>
    </p>    
</asp:Content>

