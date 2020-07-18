<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeBehind="FeeFinder.aspx.cs" Inherits="FeeFinder" Title="GFE Calculator" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pain_one_cph" runat="server">
	<link href="feefinder/style.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="feefinder/jquery-1.3.2.js"></script>
	<script type="text/javascript" src="feefinder/jquery.scrollTo.js"></script>
	<script type="text/javascript" src="feefinder/jquery-overrides.js"></script>
	<script type="text/javascript" src="FeeFinderJS.aspx"></script>
	<script type="text/javascript" src="feefinder/static.js"></script>
	<script type="text/javascript" src="feefinder/feefinder.js"></script>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">
	<div id="trans_type">
		<h2>Please specify the transaction type:</h2>
		<p>
		<input type="radio" name="transaction_type" value="purchase" onclick="setTransactionType(this.value);" /> Purchase
		<input type="radio" name="transaction_type" value="refi" onclick="setTransactionType(this.value);" /> Refinance
		<input type="radio" name="transaction_type" value="agent_refi" onclick="setTransactionType(this.value);" /> Agent Refinance
		</p>
	</div>

	<div id="fee_finder" style="display:none;page-break-after:always">

	<h2>Transaction Information</h2>

	<!-- Escrow Services -->

	<h3>Borrower's Name</h3>
	<p><input type="text" id="borrowers_name" name="borrowers_name" style="width: 400px;" /></p>

	<h3>In what county is the property located:</h3>
	<p><select name="county" id="county" onchange="checkRequiredServicesForCounty()">
		<option>Select County...</option>
		<!-- the rest will be filled in via javascript -->
	</select></p>

	<p><em>NOTE: For properties within Cook, Kane, Will or Peoria county PLPD Compliance Processing (see checkbox below) is required.</em></p>

	<h3>In what village is the property located:</h3>
	<p><select name="village" id="village">
		<option>Select Village...</option>
		<!-- the rest will be filled in via javascript -->
	</select></p>

	<h3>Property Address</h3>
	<p><input type="text" id="property_address_1" name="property_address_1" style="width: 300px;" /></p>
	<p><input type="text" id="property_address_2" name="property_address_2" style="width: 300px;" /></p>

	<!--
	<p><select name="property_city" id="property_city">
		<option>Select City...</option>
		<option>(Please select a county first)</option>
	</select></p>
	-->
	
	<p><input type="text" id="property_city" name="property_city" style="width: 160px;" />
	,<input type="text" id="property_state" name="property_state" value="IL" style="width: 30px;" />
	<input type="text" id="property_zip" name="property_zip" style="width: 85px;" /></p>

	<div class="purchase">
		<h3>Purchase Price</h3>
		<p>$ <input type="text" id="purchase_price" name="purchase_price" style="width: 100px; text-align: right;" /></p>
	</div>

	<h3>Loan Amount</h3>
	<p>$ <input type="text" id="loan_amount" name="loan_amount" style="width: 100px; text-align: right;" /></p>

	<h3>Is there a second mortgage?</h3>

	<p>
	<input type="radio" name="second_mortage" value="yes" onclick="$('.second_mortgage_yes').show('slow');" /> Yes
	<input type="radio" name="second_mortage" value="no" checked="checked" onclick="$('.second_mortgage_yes').hide('slow');" /> No
	</p>

	<div class="second_mortgage_yes subquestion" style="display:none;">
		<h3>Amount of second mortgage:</h3>
		<p>$ <input type="text" id="second_mortgage_amount" name="second_mortgage_amount" style="width: 100px; text-align: right;" /></p>

		<!--
		<h3>Is secondary mortgage insurance required?</h3>
		<p>
		<input type="radio" name="second_mortgage_insurance" value="yes"  /> Yes
		<input type="radio" name="second_mortgage_insurance" value="no" checked="checked" /> No
		</p>
		-->

	</div>

	<h2>Escrow Services</h2>

	<!-- no calculation required -->
	<h3>Will proceeds from the loan be used for home improvements?</h3>
	<p>
	<input type="radio" name="construction_escrow" value="yes"  /> Yes
	<input type="radio" name="construction_escrow" value="no" checked="checked" /> No
	</p>

	<div class="purchase" style="display:none;">

		<!-- need amount - if anything entered it will show up as a line item -->
		<h3>If buyer is responsible for any customary seller settlement costs, specify amount:</h3>
		<p>$ <input type="text" id="buyer_settlement_costs" name="buyer_settlement_costs" value="0.0" style="width: 100px; text-align: right;" /></p>

		<!-- need amount - if anything entered it will show up as a credit (negative amount) line item -->
		<h3>If any credits allowed from seller to buyer, specify the amount:</h3>
		<p>$ <input type="text" id="credits_from_seller" name="credits_from_seller" value="0.0" style="width: 100px; text-align: right;" /></p>

		<!--
		<h3 class="missing">Earnest Money:</h3>
		<p>$ <input type="text" name="earnest_money" style="width: 100px; text-align: right;" /></p>
		-->
	</div>

	<!-- need amount - if anything entered it will show up as a line item -->
	<h3>Buyers Attorney Fee:</h3>
	<p>$ <input type="text" id="buyers_attorney_fee" name="buyers_attorney_fee" value="0.0" style="width: 100px; text-align: right;" /></p>

	<h2>Mortgage Policy/Endorsement</h2>

	<h3>Please select which of the following endorsements will be required:</h3>

	<div><input type="checkbox" class="endorsement" id="endorsement_condominium" name="endorsement_condominium" /> Condominium Endorsement</div>
	<div><input type="checkbox" class="endorsement" id="endorsement_planned_unit" name="endorsement_planned_unit" /> Planned Unit Endorsement</div> <!-- $150 -->
	<div><input type="checkbox" class="endorsement" id="endorsement_variable_rate" name="endorsement_variable_rate" /> Variable Rate/ARM Endorsement</div>
	<div><input type="checkbox" class="endorsement" id="endorsement_negative_amortization" name="endorsement_negative_amortization" /> Negative Amortization</div>  <!-- $150 -->
	<div><input type="checkbox" class="endorsement" id="endorsement_manufacturing_housing" name="endorsement_manufacturing_housing" /> Manufacturing Housing</div>  <!-- $150 -->
	<div><input type="checkbox" class="endorsement" id="endorsement_epa" name="endorsement_epa" /> EPA Endorsement</div>
	<div><input type="checkbox" class="endorsement" id="endorsement_restrinctions_encroachments_minerals" name="endorsement_restrinctions_encroachments_minerals" /> Restrictions, Encroachments, Minerals Endorsement</div>  <!-- $150 -->
	<div><input type="checkbox" class="endorsement" id="endorsement_location" name="endorsement_location" /> Location Endorsement</div>
	<div><input type="checkbox" class="endorsement" id="endorsement_balloon" name="endorsement_balloon" /> Balloon Endorsement</div>
	<div><input type="checkbox" class="endorsement" id="endorsement_pud" name="endorsement_pud" /> PUD Endorsement</div>
	<div><input type="checkbox" class="endorsement" id="endorsement_revolving_credit" name="endorsement_revolving_credit" /> Revolving Credit Mortgage Endorsement</div>
	<div><input type="checkbox" class="endorsement" id="endorsement_other" name="endorsement_other" /> Other</div>  <!-- "contact us" -->

	<h2>Recording:</h2>

	<h3>Number of loans/closing statements required:</h3>
	<p><input type="text" id="number_of_statements" name="number_of_statements" value="1" style="width:50px;" /></p>

	<h3>How many pages is the standard mortgage (without riders):</h3>
	<p><input class="documents" id="pages_standard_mortgage" name="pages_standard_mortgage" value="15" style="width:50px;" /></p>

	<!-- each needs a quantity and will have "contact us" -->
	<h3>Specify the number of pages of each type to be recorded at time of dispersement:</h3>
	<div><input class="documents" name="pages_assignment" id="pages_assignment" style="width: 30px;" value="0" /> Assignment</div>
	<div><input class="documents" name="pages_deeds" id="pages_deeds" style="width: 30px;" value="2" /> Deed</div>
	<div><input class="documents" name="pages_release" id="pages_release" style="width: 30px;" value="2" /> Release</div>
	<div><input class="documents" name="pages_power_of_attorney" id="pages_power_of_attorney" style="width: 30px;" value="0" /> Power of Attorney</div>
	<div><input class="documents" name="pages_subordination" id="pages_subordination" style="width: 30px;" value="0" /> Subordination</div>
	<div><input class="documents" name="pages_assignment_of_rents" id="pages_assignment_of_rents" style="width: 30px;" value="0" /> Assignment of Rents</div>
	<div><input class="documents" name="pages_other" id="pages_other" style="width: 30px;" value="0" /> Other</div>

	<!-- each needs a quantity and will have "contact us" -->
	<h3>Specify the number of pages of each applicable rider to be attached to the standard mortgage:</h3>
	<div><input class="riders" name="pages_rider_legal_description" id="pages_rider_legal_description" style="width: 30px;" value="0" /> Legal Description</div>
	<div><input class="riders" name="pages_rider_balloon" id="pages_rider_balloon" style="width: 30px;" value="0" /> Balloon</div>
	<div><input class="riders" name="pages_rider_condominium" id="pages_rider_condominium" style="width: 30px;" value="0" /> Condominium</div>
	<div><input class="riders" name="pages_rider_homestead" id="pages_rider_homestead" style="width: 30px;" value="0" /> Homestead</div>
	<div><input class="riders" name="pages_rider_occupancy" id="pages_rider_occupancy" style="width: 30px;" value="0" /> Occupancy</div>
	<div><input class="riders" name="pages_rider_planned_unit_development" id="pages_rider_planned_unit_development" style="width: 30px;" value="0" /> Planned Unit Development</div>
	<div><input class="riders" name="pages_rider_second_home" id="pages_rider_second_home" style="width: 30px;" value="0" /> Second Home</div>
	<div><input class="riders" name="pages_rider_trust" id="pages_rider_trust" style="width: 30px;" value="0" /> Trust</div>
	<div><input class="riders" name="pages_rider_variable_rate" id="pages_rider_variable_rate" style="width: 30px;" value="0" /> Variable Rate/ARM</div>
	<div><input class="riders" name="pages_rider_other" id="pages_rider_other" style="width: 30px;" value="0" /> Other</div>

	<div class="second_mortgage_yes" style="display:none;">
		<h3>Specify the number of pages for the second mortgage:</h3>
		<p><input type="text" id="second_mortgage_pages" name="second_mortgage_pages" value="0" style="width: 30px; text-align: right;" /></p>
	</div>

	<h2>Other Settlement Services</h2>

	<h3>Please select which of the following will be required:</h3>

	<div><input type="checkbox" id="service_after_hours_closing" name="service_after_hours_closing" /> After Hours Closing</div>
	<div><input type="checkbox" id="service_chain_of_title" name="service_chain_of_title" /> Chain of Title</div>
	<div><input type="checkbox" id="service_commitment_later" name="service_commitment_later" /> Commitment Later Date</div>
	<div><input type="checkbox" id="service_dry_closing"  name="service_dry_closing" /> Dry Closing</div>
	<div><input type="checkbox" id="service_email_handling" name="service_email_handling" /> E-Mail Package Handling</div>
	<div><input type="checkbox" id="service_joint_order_escrows" name="service_joint_order_escrows" /> Joint Order Escrows</div>
	<div><input type="checkbox" id="service_plpd" name="service_plpd" /> PLPD Compliance Processing</div>
	<div><input type="checkbox" id="service_policy_update_date" name="service_policy_update_date" /> Policy Update Date</div>
	<div><input type="checkbox" id="service_in_home_closing" name="service_in_home_closing" /> In-Home Remote Closing</div>
	<div><input type="checkbox" id="service_title_indemnities_processing" name="service_title_indemnities_processing" /> Title Indemnities Processing (Require Affinity Title to holdback additional funds at the time of disbursement)</div>
	<div><input type="checkbox" id="service_tax_payment_handling" name="service_tax_payment_handling" /> Tax Payment Handling (Require Affinity Title to insure over (pay) the next tax installment due)</div>
	<div><input type="checkbox" id="service_chicago_water" name="service_chicago_water" /> Chicago Water Certification Processing</div>

	<div><input type="checkbox" id="service_overnight_handling" name="service_overnight_handling" /> Overnight Handling
	- Number of Payoffs: <input type="text" id="number_payoffs_overnight" name="number_payoffs_overnight" value="1" style="width:30px;" /></div>

	<div><input type="checkbox" id="service_wire_transfer" name="service_wire_transfer" /> Wire Transfer
	- Number of Payoffs: <input type="text" id="number_payoffs_wire" name="number_payoffs_wire" value="1" style="width:30px;" /></div>

	<div><input type="checkbox" id="service_certified_checks" name="service_certified_checks" /> Certified Checks</div>

	<p><input type="submit" value="Calculate Fees" onclick="calculateResults(); return false;" /></p>

	</div>

	<div id="results" style="display:none;">
		<h2 class="purchase">New RESPA Good Faith Estimate (GFE) and Standard: Purchase</h2>
		<h2 class="refi">New RESPA Good Faith Estimate (GFE) and Standard: Refinance</h2>
		<h2 class="agent_refi">New RESPA Good Faith Estimate (GFE) and Standard: Agent Refinance</h2>

		<table id="total_table">
		<tr><td><span id="total_closing_fee_label"></span></td><td class="currency"><span class="total" id="total_closing_fee"></span></td></tr>	
		<tr class="second_mortgage_yes"><td>2nd Mortgage Closing Fee</td><td class="currency"><span class="total" id="total_second_closing_fee"></span></td></tr>	
		<tr class="purchase agent_refi"><td>Abstract Fee</td><td class="currency"><span class="total" id="total_abstract_fee"></span></td></tr>	
		<tr class="purchase"><td>Owner Policy</td><td class="currency"><span class="total" id="total_owner_policy"></span></td></tr>	
		<tr><td><span id="total_first_loan_policy_label"></span></td><td class="currency"><span class="total" id="total_first_loan_policy"></span></td></tr>	
		<tr class="second_mortgage_yes"><td>2nd Loan Policy</td><td class="currency"><span class="total" id="total_second_loan_policy"></span></td></tr>	
		<tr><td>Endorsements</td><td class="currency"><span class="total" id="total_endorsements"></span></td></tr>	
		<tr class="second_mortgage_yes"><td>2nd Mortgage Endorsements</td><td class="currency"><span class="total" id="total_second_mortgage_endorsements"></span></td></tr>	
		<tr><td><span id="total_state_policy_fee_label"></span></td><td class="currency"><span class="total" id="total_state_policy_fee"></span></td></tr>	
		<tr><td>Mortage Recording Fees (estimate)</td><td class="currency"><span class="total" id="total_recording"></span></td></tr>	
		<tr class="second_mortgage_yes"><td>2nd Mortgage Recording Fees (estimate)</td><td class="currency"><span class="total" id="total_second_mortgage_recording"></span></td></tr>	

		<tr class="construction_escrow_yes"><td>Construction Escrow Fee</td><td class="currency"><span class="total" id="construction_escrow_fee"></span></td></tr>	

		<tr class="service_total" id="service_total_after_hours_closing"><td>After Hours Closing</td><td class="currency"><span class="total" id="total_after_hours_closing"></span></td></tr>
		<tr class="service_total" id="service_total_chain_of_title"><td>Chain of Title</td><td class="currency"><span class="total" id="total_chain_of_title"></span></td></tr>
		<tr class="service_total" id="service_total_commitment_later"><td>Commitment Later Date</td><td class="currency"><span class="total" id="total_committment_later"></span></td></tr>
		<tr class="service_total" id="service_total_dry_closing"><td>Dry Closing</td><td class="currency"><span class="total" id="total_dry_closing"></span></td></tr>
		<tr class="service_total" id="service_total_email_handling"><td>E-Mail Package Handling</td><td class="currency"><span class="total" id="total_email_handling"></span></td></tr>
		<tr class="service_total" id="service_total_joint_order_escrows"><td>Joint Order Escrows</td><td class="currency"><span class="total" id="total_joint_order_escrows"></span></td></tr>
		<tr class="service_total" id="service_total_plpd"><td>PLPD Compliance Processing</td><td class="currency"><span class="total" id="total_plpd"></span></td></tr>
		<tr class="service_total" id="service_total_policy_update_date"><td>Policy Update Date</td><td class="currency"><span class="total" id="total_policy_update_date"></span></td></tr>
		<tr class="service_total" id="service_total_in_home_closing"><td>In-Home Remote Closing Fee</td><td class="currency"><span class="total" id="total_in_home_closing"></span></td></tr>
		<tr class="service_total" id="service_total_title_indemnities_processing"><td>Title Indemnities Processing</td><td class="currency"><span class="total" id="total_title_indemnities_processing"></span></td></tr>
		<tr class="service_total" id="service_total_tax_payment_handling"><td>Tax Payment Handling</td><td class="currency"><span class="total" id="total_tax_payment_handling"></span></td></tr>
		<tr class="service_total" id="service_total_chicago_water"><td>Chicago Water Certification Processing</td><td class="currency"><span class="total" id="total_chicago_water"></span></td></tr>
		<tr class="service_total" id="service_total_overnight_handling"><td>Overnight Handling</td><td class="currency"><span class="total" id="total_overnight_handling"></span></td></tr>
		<tr class="service_total" id="service_total_wire_transfer"><td>Wire Transfer Fee</td><td class="currency"><span class="total" id="total_wire_transfer"></span></td></tr>
		<tr class="service_total" id="service_total_certified_checks"><td>Certified Checks Fee</td><td class="currency"><span class="total" id="total_certified_checks"></span></td></tr>

		<tr class="purchase"><td>Village Transfer Stamp (amount for which buyer is responsible)</td><td class="currency"><span class="total" id="total_village_stamp_fee"></span></td></tr>	

		<tr class="footer"><td>TOTAL</td><td class="currency"><span id="grand_total"></span></td></tr>

		</table>

		<div class="box">
			<h4>Please print this GFE and either:
			Send PDF via Email to "closing [at] affinityistitle.com"
			-or- 
			Fax to 1-847-257-8014</h4>
		</div>
		
		<p><input type="submit" value="Send To Printer" onclick="window.print();return false;" /></p>
		
		<p><a href='FeeFinder.aspx' onclick="return confirm('Are you sure you want to clear the form?');">Clear Form and Start Over...</a></p>

	</div>
</asp:Content>