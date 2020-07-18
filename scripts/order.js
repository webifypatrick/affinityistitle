// these are included and used by survey forms for copying and clearing address records

var isIndiana = false;

// formats the field as currency
// shows an alert if format is not valid
function verifyCurrency(field)
{
    var num = field.value.toString().replace(/\$|\,/g,'');
    if(isNaN(num))
    {
        alert('Please verify that the currency amount is a valid number');
    }
    else
    {
        field.value = formatCurrency(num,'');
    }
}

// format the provided field as such: ##-##-###-###-####
// shows an alert if format is not valid
function verifyPIN(field)
{
    field.value = formatPIN(field.value,true);
}

// formats comma-separated list of pins
function verifyAdditionalPINs(field)
{
    var pins = field.value.split(",");
    field.value = "";
    var delim = "";
    
    for (i=0; i < pins.length; i++)
    {
        field.value += delim + formatPIN(pins[i],false);
        delim = ",";
    }
    
    
}

// return a pin formatted as such: ##-##-###-###-####
// 
function formatPIN(val, showAlert)
{
    val = val.replace(/-/g, "");
    var result = val;
    
    if(!isIndiana)
    {
	    if (val.length == 14)
	    {
	        result = val.substr(0,2) + '-'
	        + val.substr(2,2) + '-'
	        + val.substr(4,3) + '-'
	        + val.substr(7,3) + '-'
	        + val.substr(10,4);
	    }
	    else if (val.length == 16)
	    {
	        result = val.substr(0,2) + '-'
	        + val.substr(2,2) + '-'
	        + val.substr(4,2) + '-'
	        + val.substr(6,3) + '-'
	        + val.substr(9,3) + '-'
	        + val.substr(12,4);
	    }
	    else
	    {
	        if (showAlert)
	        {
	            alert('Please verify that this PIN is correct.  (It should be 14 or 16 digits)');
	        }
	    }
	  }
	  else {
	    if (val.length == 18)
	    {
	        result = val.substr(0,2) + '-'
	        + val.substr(2,4) + '-'
	        + val.substr(6,4) + '-'
	        + val.substr(10,4) + '-'
	        + val.substr(14,4);
	    }
	    else {
	        if (showAlert)
	        {
	            alert('Please verify that this PIN is correct.  (It should be 18 digits)');
	        }
	    }
	  }
    
    return result;
}

function verifyPhone(field)
{
    field.value = formatPhone(field.value,true);
}

function formatPhone(val, showAlert)
{
    // remove all non-numeric characters
    var result = val.replace(/[^0-9]*/gi,"");
    
    if (result.length < 10)
    {
        if (showAlert)
        {
            alert('Please verify that this Phone is correct.  (It should be at least 10 digits)');
        }
    }
    else
    {
        if (result.substr(0,1) == "1")
        {
            result = result.substr(1);
        }
        
        var tmp = '(' + result.substr(0,3) + ')'
        + result.substr(3,3) + '-'
        + result.substr(6,4);
        
        if (result.length > 10)
        {
            tmp += ' x' + result.substr(10);
        }
        
        result = tmp;
    }
    
    return result.trim();
}

// formats nubmer as currency.  second parameter is the char desired for dollar sign
function formatCurrency(num,ds) {
    num = num.toString().replace(/\$|\,/g,'');
    if(isNaN(num))
    num = "0";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num*100+0.50000000001);
    cents = num%100;
    num = Math.floor(num/100).toString();
    if(cents<10)
    cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
    num = num.substring(0,num.length-(4*i+3))+','+
    num.substring(num.length-(4*i+3));
    return (((sign)?'':'-') + ds + num + '.' + cents);
}

/* 
 * This function is triggered onload by the CopyApplicationTo checkboxes.  ASP encodes the variable
 * names so we have to search the whole form for the checkboxes.  When each checkbox is found,
 * it triggers the auto-population - if the destination 'Name' field is blank - that way we don't
 * overwrite in case the user has already submitted this and made changes
 */
function triggerCopyApplicationTo()
{
    setTimeout("doTriggerCopyApplicationTo()",10);
}

function doTriggerCopyApplicationTo()
{
    var frm = document.getElementById("aspnetForm");
    
    for (var i = 0; i < frm.elements.length; i++)
    {
        if (frm.elements[i].type == "checkbox" && frm.elements[i].id.indexOf('CopyApplicationTo') > 0)
        {
            if (frm.elements[i].checked)
            {
                var label = frm.elements[i].nextSibling.firstChild.nodeValue;
                if (label == '(B) Attorney' || label == 'Buyers Attorney')
                {
                    if (findTag(frm,'BuyersAttorneyName').value == '')
                    {
                        copyAddress(frm.elements[i],'Applicant','BuyersAttorney');
                    }
                }
                else if (label == '(S) Attorney' || label == 'Sellers Attorney')
                {
                    if (findTag(frm,'SellersAttorneyName').value == '')
                    {
                        copyAddress(frm.elements[i],'Applicant','SellersAttorney');
                    }
                }
                else if (label == 'Mortage Broker')
                {
                    if (findTag(frm,'BrokerName').value == '')
                    {
                        copyAddress(frm.elements[i],'Applicant','Broker');
                    }
                }
                else if (label == 'Listing Agent')
                {
                    if (findTag(frm,'BrokerName').value == '')
                    {
                        copyAddress(frm.elements[i],'Applicant','ListingRealtor');
                    }
                }
                else if (label == 'Buyers Agent')
                {
                    if (findTag(frm,'BrokerName').value == '')
                    {
                        copyAddress(frm.elements[i],'Applicant','SellersRealtor');
                    }
                }
                else
                {
                    alert('System Admin: Please check the label for CopyApplicationTo checkbox # ' + label);
                }
            }

        }
    }


}


function copyAddress(cb,f,t)
{
    setTimeout("doCopyAddress('"+cb.id+"','"+f+"','"+t+"')",10);
}


function doCopyAddress(cbId,f,t)
{
    var cb = document.getElementById(cbId);
    var frm = cb.form;
    
    if (cb.checked)
    {
        copyValue(frm,f,t,'Name');
        copyValue(frm,f,t,'Address');
        copyValue(frm,f,t,'Address2');
        copyValue(frm,f,t,'City');
        copyValue(frm,f,t,'State');
        copyValue(frm,f,t,'Zip');
        copyValue(frm,f,t,'Phone');
        copyValue(frm,f,t,'Fax');
        copyValue(frm,f,t,'Email');
        copyValue(frm,f,t,'BusinessLicenseID');
        copyValue(frm,f,t,'IndividualLicenseID');
        
        // special cases where to/from fields don't match exactly
        if (t == 'Broker' && f == 'Applicant')
        {
            from = findTag(frm,f + 'AttorneyName');
            to = findTag(frm,t+'LoanOfficer');
            to.value = from.value;
        }
        else if (f == 'Applicant')
        {
            from = findTag(frm,f + 'AttorneyName');
            to = findTag(frm,t+'AttentionTo');
            to.value = from.value;
        }
        else
        {
            copyValue(frm,f,t,'AttentionTo');
        }
        
    
    }
    else
    {
        clearValue(frm,t,'Name');
        clearValue(frm,t,'Address');
        clearValue(frm,t,'Address2');
        clearValue(frm,t,'City');
        clearValue(frm,t,'State');
        clearValue(frm,t,'Zip');
        clearValue(frm,t,'Phone');
        clearValue(frm,t,'Fax');
        clearValue(frm,t,'Email');
        clearValue(frm,t,'BusinessLicenseID');
        clearValue(frm,t,'IndividualLicenseID');

        if (t == 'Broker')
        {
            clearValue(frm,t,'LoanOfficer');
        }
        else
        {
            clearValue(frm,t,'AttentionTo');
        }


    }
}

// sets the radio group to the specified value
function setRadioValue(frm,name,val)
{
    var field = findTag(frm,name);
    
    for (var i = 0; i < frm.elements.length; i++)
    {
        if (frm.elements[i].type == "radio" && frm.elements[i].name == field.name)
        {
            frm.elements[i].checked = (frm.elements[i].value == val);
        }
    }
}

// sets the radio group to the specified value
function setCheckboxValue(frm,name,val)
{
	/*
    var field = findTag(frm,name);
    
    for (var i = 0; i < frm.elements.length; i++)
    {
        if (frm.elements[i].type == "checkbox" && frm.elements[i].name == field.name)
        {
            frm.elements[i].checked = val;
        }
    }
  */
  
  $(frm).find("#_ctl0_content_cph_field_" + name + "_0").prop("checked", val);
}


function clearValue(frm,t,field)
{
    var to = (field != "BusinessLicenseID" && field != "IndividualLicenseID")? findTag(frm,f+field) : findTag(frm,field);
    
    to.value = "";
}

function copyValue(frm,f,t,field)
{
    var from = (field != "BusinessLicenseID" && field != "IndividualLicenseID")? findTag(frm,f+field) : findTag(frm,field);
    var to = findTag(frm,t+field);
    
    to.value = from.value;
}

/**
* locate a field in the .net form.  .net addes a bunch of crud
* to the beginning so we have to strip that out
*/
function findTag(frm,fname)
{
    var buff = "";
    for (var i = 0; i < frm.elements.length; i++)
    {
        var name = "" + frm.elements[i].name;
        var suff = name.split("field_");
        
        if (suff.length > 0)
        {
            if (suff[1] == fname && frm.elements[i].type != "hidden")
            {
                // return as soon as we find it
                return frm.elements[i];
            }
            else if (frm.elements[i].type == "checkbox" && typeof suff[1] != "undefined")
            {
                // if this is a checkbox, .net appends $0 $1 etc. to the end
                var cbn = suff[1].split("$");
                
                if (cbn[0] == fname)
                {
                    return frm.elements[i];
                }
            }
            
            
        }
    }
    
    // we didn't return so the fields wasn't found
    alert('Error: Field ' + fname + ' was not found');
}