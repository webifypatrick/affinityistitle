/**
 * Requires static.js to be included before this script
 */

$(document).ready(function() {
	initForm();
});




/**
 * called only once when page is loaded
 */
function initForm()
{
	var countyOptions = new Array();
	var i = 0;
	for (var key in COUNTIES)
	{
		// $("<option/>").val(key).text(key).appendTo("#county");
		countyOptions[i] = "<option value='" + key + "'>" + key + "</option>";
		i++;
	}
	$('#county').append(countyOptions.join(''));

	var villageOptions = new Array();
	var j = 0;
	for (var key in VILLAGE_STAMPS)
	{
		// $("<option/>").val(key).text(key).appendTo("#county");
		villageOptions[j] = "<option value='" + key + "'>" + key + "</option>";
		j++;
	}
	$('#village').append(villageOptions.join(''));

}


/*
 *
 */
function setTransactionType(transactionType)
{
	setVisibility();
	
	$("#fee_finder").show("slow");
}

/*
 * 
 */
function setVisibility()
{
	var second_mortgage = $('input[name=construction_escrow]:checked').val();
	$(".construction_escrow_yes").toggle(second_mortgage == 'yes');

	var second_mortgage = $('input[name=second_mortage]:checked').val();
	$(".second_mortgage_yes").toggle(second_mortgage == 'yes');
	$(".second_mortgage_no").toggle(second_mortgage == 'no');

	var second_mortgage_insurance = $('input[name=second_mortgage_insurance]:checked').val();
	$(".second_mortgage_insurance_yes").toggle(second_mortgage_insurance == 'yes');
	$(".second_mortgage_insurance_no").toggle(second_mortgage_insurance == 'no');
	
	var transaction_type = $('input[name=transaction_type]:checked').val();
	$("."+TT_PURCHASE).hide();
	$("."+TT_REFI).hide();
	$("."+TT_AGENT_REFI).hide();
	$("."+transaction_type).show();
}

/**
 * This will check any boxes and fill out any values as required
 * on a per-county basis
 */
function checkRequiredServicesForCounty()
{
	var county_name = $('#county').val();
	
	// require PLPD if in the following counties
	if (county_name == 'Cook' ||  county_name == 'Kane' ||  county_name == 'Will' ||  county_name == 'Peoria')
	{
		$('#service_plpd')[0].checked = true;
	}
}

/*
 * This function calculates all questions on the form and shows the results
 */
function calculateResults()
{
	checkRequiredServicesForCounty();

	var purchase_price = $('#purchase_price').valAsFloat(0);
	var loan_amount = $('#loan_amount').valAsFloat(0);
	var transaction_amount = purchase_price > loan_amount ? purchase_price : loan_amount;
	var transaction_type = $('input[name=transaction_type]:checked').val();
	var construction_escrow = $('input[name=construction_escrow]:checked').val();
	var closing_fee = getClosingFee(transaction_type,transaction_amount);
	var second_mortgage = $('input[name=second_mortage]:checked').val();
	var second_mortgage_amount = $('#second_mortgage_amount').valAsFloat(0);
	var endorsement_fee = getEndorsementsRate(transaction_type,transaction_amount);

	$('#total_closing_fee').htmlCurrency(closing_fee);
	$('#total_first_loan_policy').html("Simultaniously Issued");
	$('#total_endorsements').htmlCurrency(endorsement_fee);
	
	$('#total_first_loan_policy').htmlCurrency(getFirstLoanPolicy(loan_amount,purchase_price,transaction_type));

	var owner_policy = getOwnerPolicy(transaction_amount,transaction_type);
	$('#total_owner_policy').htmlCurrency(owner_policy);
	
	if (construction_escrow == 'yes')
	{
		$('#construction_escrow_fee').htmlCurrency(getConstructionEscrow(transaction_amount,transaction_type));	
	}
	else
	{
		$('#construction_escrow_fee').htmlCurrency(0);	
	}
	
	$('.service_total').each(
		function(index)
		{
			var id = this.id.replace('service_total_','');
			calculateServiceFee(id,transaction_type);
		}
	);
	
	// mortgage, riders and recording
	$('#total_recording').htmlCurrency( getMortgageRecordingFee() );
	
	if (transaction_type == TT_PURCHASE)
	{
		// set up the form items specific to purchase
		$('#total_closing_fee_label').html('Residential Closing Fee');
		if (second_mortgage == 'yes') $('#total_second_closing_fee').htmlCurrency(SECOND_MORTGAGE_RATE);
		$('#total_abstract_fee').htmlCurrency(ABSTRACT_FEE);

		$('#total_village_stamp_fee').htmlCurrency( getVillageStampFee(transaction_amount, transaction_type) );

		$('#total_first_loan_policy_label').html('Simultaneously Issued 1st Loan Policy');

		if (second_mortgage == 'yes')
		{
			$('#total_state_policy_fee_label').html('State Policy Fee (Owner + Mortgage + 2nd)');
			$('#total_state_policy_fee').htmlCurrency( STATE_POLICY_FEE * 3 );
			$('#total_second_loan_policy').htmlCurrency(getSecondMortgatePolicy(second_mortgage_amount,transaction_type));
			$('#total_second_mortgage_endorsements').htmlCurrency( (endorsement_fee > 0 ? SECOND_MORTGAGE_ENDORSEMENT_RATE : 0) );
			$('#total_second_mortgage_recording').htmlCurrency( getSecondMortgageRecordingFee() );
		}
		else
		{
			$('#total_state_policy_fee_label').html('State Policy Fee (Owner + Mortgage)');
			$('#total_state_policy_fee').htmlCurrency( STATE_POLICY_FEE * 2 );
			$('#total_second_loan_policy').htmlCurrency('0');
			$('#total_second_mortgage_endorsements').htmlCurrency(0);
			$('#total_second_mortgage_recording').htmlCurrency(0);
		}

	}
	else
	{	
		// set up form items speific to refi / agent refi
		$('#total_closing_fee_label').html('1st Mortgage Closing Fee');
		if (second_mortgage == 'yes') $('#total_second_closing_fee').htmlCurrency(SECOND_REFI_RATE);
		
		$('#total_village_stamp_fee').htmlCurrency(0.00);
		
		$('#total_first_loan_policy_label').html('1st Loan Policy');

		if (second_mortgage == 'yes')
		{
			$('#total_state_policy_fee_label').html('State Policy Fee (Mortgage + 2nd)');
			$('#total_state_policy_fee').htmlCurrency( STATE_POLICY_FEE * 2 );
			$('#total_second_loan_policy').htmlCurrency(getSecondMortgatePolicy(second_mortgage_amount,transaction_type));
			$('#total_second_mortgage_endorsements').htmlCurrency( (endorsement_fee > 0 ? SECOND_MORTGAGE_ENDORSEMENT_RATE : 0) );
			$('#total_second_mortgage_recording').htmlCurrency( getSecondMortgageRecordingFee() );
		}
		else
		{
			$('#total_state_policy_fee_label').html('State Policy Fee (Mortgage)');
			$('#total_state_policy_fee').htmlCurrency( STATE_POLICY_FEE );
			$('#total_second_loan_policy').htmlCurrency('0');
			$('#total_second_mortgage_endorsements').htmlCurrency(0);
			$('#total_second_mortgage_recording').htmlCurrency(0);
		}

		if (transaction_type == TT_REFI)
		{
			// form items specific to refi
		}
		else
		{
			// form items specific to agent refi
			$('#total_abstract_fee').htmlCurrency(ABSTRACT_FEE);
		}

	}

	// lastly caculcate the grand total
	$('#grand_total').htmlCurrency( getGrandTotal() );

	setVisibility();
	
	showResults();

}

function showResults()
{
	$("#results").show("fast",
		function()
		{
			$(window).scrollTo( 'max' );
		}
	);
	
}

function showQuestions()
{
	$("#fee_finder").show("fast");
}

/*
 * inspects all spans with class="total," converts them to currency
 * and returns the total
 */
function getGrandTotal()
{
	// first get the base rate
	var total = 0;
	
	// now add the additional amount
	$('.total').each(
		function(index)
		{
			var sub_total_html = $(this).html();
			var sub_total = parseFloat(sub_total_html.replace(/[^0-9|.]/g, ''));
			if (isNaN(sub_total)) sub_total = 0;
			total += sub_total;
		}
	);

	return total;
}

/**
 *
 */
function getVillageStampFee(transactionAmount,transactionType)
{
	var village_name = $('#village').val();
		
	// COUNTIES is defined in FeeFinderJS.aspx
	var village_formula = VILLAGE_STAMPS[ village_name ];
	
	// if no village is selected we can't calculate the fee
	if (!village_formula) return 'No Village Selected';
	
	// return 0 here to prevend divide-by-zero error
	if (village_formula.buyerStep == 0) return 0;
	
	return Math.floor( transactionAmount / village_formula.buyerStep ) * village_formula.buyerMultiplier;

}

/**
 * Formula for Purchase "Simultaneously Issued 1st Loan Policy" = FIRST_LOAN_POLICY_RATE['purchase']
 * + $2.00 per every $1,000 loan amount is over the purchase price
 *
 * Formula for Re-Fi "1st Loan Policy" = FIRST_LOAN_POLICY_RATE['refi']
 * + $1.50 per every $1,000 loan amount is over CONFORMING_LOAN_AMOUNT
*/
function getFirstLoanPolicy(loan_amount,purchase_amount,transaction_type)
{
	var rate = FIRST_LOAN_POLICY_RATE[transaction_type];
	var adjustment = 0;
	
	if (transaction_type == TT_PURCHASE)
	{
		if (loan_amount > purchase_amount)
			adjustment = Math.floor( (loan_amount - purchase_amount) / 1000 ) * POLICY_PURCHASE_CONFORM;
	}
	else
	{
		if (loan_amount > CONFORMING_LOAN_AMOUNT)
			adjustment = Math.floor((loan_amount - CONFORMING_LOAN_AMOUNT) / 1000) * POLICY_REFI_CONFORM;
	}
		
	if (isNaN(adjustment) || adjustment < 0) adjustment = 0;
	
	return rate + adjustment;
		
}

/**
 * 
 */
function calculateServiceFee(id, transaction_type)
{
	var tr = $('#service_total_' + id);
	var span = $('#total_' + id);
	var is_checked = $('input[name=service_' + id + ']').is(':checked');

	if (is_checked)
	{
		var formula = SERVICE_RATES[id];
		var total = formula[transaction_type] * (formula.multiplier ? $('#'+formula.multiplier).valAsFloat() : 1);
		span.htmlCurrency(total);
		tr.show();
	}
	else
	{
		span.htmlCurrency('0');
		tr.hide();
	}
}

/**
 *
 */
function getNumberOfRiderPages()
{
	// first get the base rate
	var pages = 0;
	
	// now add the additional amount
	$('.riders').each(
		function(index)
		{
			pages += $(this).valAsFloat();
		}
	);

	return pages;
}

/**
 *
 */
function getSecondMortgageRecordingFee()
{
	var county_name = $('#county').val();
		
	// COUNTIES is defined in FeeFinderJS.aspx
	var county_formula = COUNTIES[ county_name ];
	
	if (!county_formula)
	{
		return 'No County Selected';
	}
	
	var pages = $("#second_mortgage_pages").valAsFloat();
	
	// the standard rate for this doc
	var doc_rate = county_formula.baseFee;
	
	// if there's more pages than the base # pages for this county, 
	// then add the appropriate fee for each additional page
	if (pages - county_formula.basePages > 0)
	{
		doc_rate += county_formula.additionalPageFee * (pages - county_formula.basePages)
	}
	
	return doc_rate;

}

/**
 * Totals up the recording costs for the mortage.
 * The rider pages are added to pages_standard_mortgage
 */
function getMortgageRecordingFee()
{
	// first get the base rate
	var rate = 0;
	var rider_pages = getNumberOfRiderPages();
	var county_name = $('#county').val();
		
	// COUNTIES is defined in FeeFinderJS.aspx
	var county_formula = COUNTIES[ county_name ];
	
	if (!county_formula)
	{
		return 'No County Selected';
	}

	// now add the additional amount
	$('.documents').each(
		function(index)
		{
			var pages = $(this).valAsFloat();
			
			if (pages > 0)
			{
				// add rider pages to standard mortgage
				if (this.id == 'pages_standard_mortgage') pages += rider_pages;
				
				// the standard rate for this doc
				var doc_rate = county_formula.baseFee;
				
				// if there's more pages than the base # pages for this county, 
				// then add the appropriate fee for each additional page
				if (pages - county_formula.basePages > 0)
				{
					doc_rate += county_formula.additionalPageFee * (pages - county_formula.basePages)
				}
				
				rate += doc_rate;
			}
		}
	);
		
	return rate;
}

/**
 *
 */
function getEndorsementsRate(transaction_type,transaction_amount)
{
	// first get the base rate
	var rate = 0;
	var endorsements_exist = false;
	
	// now add the additional amount
	$('.endorsement').each(
		function(index)
		{
			var id = this.id.replace('endorsement_','');
			var is_checked = $('input[name=endorsement_' + id + ']').is(':checked');

			if (is_checked && rate != 'Contact Us')
			{
				endorsements_exist = true;
				var formula = ENDORSEMENT_RATES[id];
				var total = formula[transaction_type] * (formula.multiplier ? $('#'+formula.multiplier).valAsFloat() : 1);
				
				// if we hit one of these we can't calculate the total automatically
				if (formula.contact_us) 
				{
					rate = 'Contact Us';
				}
				else
				{
					rate += total;
				}
			}
		}
	);
	
	// base rate is set if there were any endorsements checked
	if (endorsements_exist && rate != 'Contact Us') rate += ENDORSEMENT_BASE_RATE[transaction_type];
	
	return rate;
}

/**
 * returns the rate
 */
function getClosingFee(transaction_type, value)
{
	if (transaction_type != TT_PURCHASE)
	{
		return REFI_RATE;
	}

	if (value > 500000)
	{
		// for rates over 500,000 use the max rate and add $50 for each additional 50k
		var adjustment = Math.floor( (value - 500000) / 50000 ) * 50;
		var maxRate = RESIDENTIAL_RATES[RESIDENTIAL_RATES.length-1].rate;
		return maxRate + adjustment;
	}

	// lookup this amount in the rates table
	for (var i in RESIDENTIAL_RATES)
	{
		if (RESIDENTIAL_RATES[i].min <= value && RESIDENTIAL_RATES[i].max >= value)
		{
			return RESIDENTIAL_RATES[i].rate;
		}
	}
	
	// couldn't find the rate
	// alert('Unable to calcualate rate.  Please check mortgage/loan amount.');
	return "Unable to Calculate";
}


/**
 * returns the rate
 */
function getOwnerPolicy(value, transaction_type)
{
	if (transaction_type != TT_PURCHASE)
	{
		return 0;
	}

	if (value > 910000)
	{
		// for rates over 500,000 use the max rate and add $50 for each additional 50k
		// alert('Your loan amount is greater than $910,000.  Please contact ATS for your owner policy rate.');
		return "Contact ATS";
	}
	
	// lookup this amount in the rates table
	for (var i in INSURANCE_RATES)
	{
		if (INSURANCE_RATES[i].min <= value && INSURANCE_RATES[i].max >= value)
		{
			return INSURANCE_RATES[i].rate;
		}
	}
	
	// couldn't find the rate
	// alert('Unable to calcualate policy.  Please check mortgage/loan amount.');
	return "Unable to Calculate";
}

/**
 * if loan is used for construction, escrow is required
 */
function getConstructionEscrow(value, transaction_type)
{
	
	// lookup this amount in the rates table
	for (var i in CONSTRUCTION_ESCROW_RATES)
	{
		if (CONSTRUCTION_ESCROW_RATES[i].min <= value && CONSTRUCTION_ESCROW_RATES[i].max >= value)
		{
			var amount = (value * CONSTRUCTION_ESCROW_RATES[i].multiplier) + CONSTRUCTION_ESCROW_RATES[i].rate;
			
			// return the calculated amount or the floor, whichver is greater
			return (amount > CONSTRUCTION_ESCROW_RATES[i].floor)
				? amount
				: CONSTRUCTION_ESCROW_RATES[i].floor;
		}
	}
	
	// couldn't find the rate
	return "Unable to Calculate";
}

function getSecondMortgatePolicy(loan_amount, transaction_type)
{

	if (transaction_type != TT_PURCHASE)
	{
		return SECOND_MORTGATE_INSURANCE_REFI_RATE;
	}

	value = loan_amount;
	
	if (value == 0)
	{
		return 0;
	}
	
	if (value > 500000)
	{
		// for rates over 200,000 use the max rate and add $50 for each additional 50k
		// alert('Your loan amount is greater than $910,000.  Please contact ATS for your owner policy rate.');
		return "Contact ATS";
	}
	
	if (value > 200000)
	{
		// Add $.50 for every $1,000 over $200,000 up to $500,000
		var adjustment = Math.floor( (value - 200000) / 1000 ) * 0.50;
		var maxRate = SECOND_MORTGATE_INSURANCE_RATES[SECOND_MORTGATE_INSURANCE_RATES.length-1].rate;
		return maxRate + adjustment;
	}
	
	// lookup this amount in the rates table
	for (var i in SECOND_MORTGATE_INSURANCE_RATES)
	{
		if (SECOND_MORTGATE_INSURANCE_RATES[i].min <= value && SECOND_MORTGATE_INSURANCE_RATES[i].max >= value)
		{
			return SECOND_MORTGATE_INSURANCE_RATES[i].rate;
		}
	}
	
	// couldn't find the rate
	// alert('Unable to calcualate policy.  Please check mortgage/loan amount.');
	return "Unable to Calculate";

}