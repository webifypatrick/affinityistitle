
/** 
 * Sets the value of all checkboxes in a checkbox group
 *
 * @param Element frm reference to form Element
 * @param string cbGroupName name of the checkbox group
 * @param bool newValue true to check all, or false to uncheck
 * @return Element
 */
function checkAll(frm, cbGroupName, newValue)
{
    for (i = 0; i < frm.elements.length; i++)
    {
        if (frm.elements[i].type == "checkbox" && frm.elements[i].name == cbGroupName)
        {
            frm.elements[i].checked = newValue;
        }
    }
}

/** 
 * Returns the dom object given the id string
 *
 * @param string tagId the id of the dom item
 * @return Element
 */
function getTag(tagId)
{
    return ( document.getElementById ? document.getElementById(tagId) : document.all(tagId) );
}

/** 
 * Checks specified fields on the given form.  if any of the specified
 * fields are blank, then it opens an alert box and returns false.
 * otherwise it returns true.
 * Drop-downs that are validated assume first option is not valid (ie "select one")
 * Checkbox groups & multi-selects are not validated
 *
 * Example: onsubmit="return requireFields(this,['field1','field2'],['msg1','msg2'])"
 *
 * @param  Form  reference to form object to be validated
 * @param  Array array of fieldname to be tested
 * @param  Array array of error messages for each field
 * @return bool
 */
function requireFields(objForm, arrFieldNames, arrErrorMessage) 
{
	if (arrFieldNames.length != arrErrorMessage.length) 
	{
		alert('requireFields error: arrays do not match');
		return false;
	}

	var nElements = objForm.elements.length;
	var objElement, strErrorMessage;

	// loop through all the form elements
	for (var nNum = 0; nNum < nElements; nNum++) 
	{
		objElement = objForm.elements[nNum];

		// loop through the required fields array
		for (var nInner = 0; nInner < arrFieldNames.length; nInner++) 
		{

			// if this element is in the required fields array, then check to see
			// if it is empty.  if so, alert and return false
			if (arrFieldNames[nInner] == objElement.name) 
			{
				if (arrErrorMessage[nInner] != '') 
				{
					strErrorMessage = arrErrorMessage[nInner];
				} 
				else 
				{
					strErrorMessage = objElement.name + ' is required.';
				}

				if ((objElement.type == "text" || objElement.type == "password") && (objElement.value == "" || objElement.value == null)) 
				{
					alert(strErrorMessage);
					objElement.focus();
					return false
				} 
				else if ((objElement.type == "textarea") && (objElement.value == "" || objElement.value == null)) 
				{
					alert(strErrorMessage);
					return false
				} 
				else if (objElement.type == "select-one" && objElement.selectedIndex == 0)
				{
					alert(strErrorMessage);
					objElement.focus();
					return false
				} 
				else if (objElement.type == "radio" && radioButtonFieldIsComplete(objElement) == false)
				{
					alert(strErrorMessage);
					return false
				}
			}

		}

	}
	
	// if we made it all the way to the end, then there were no problems
	return true;
}


/**
 * Checks that the specified radio button group has an option selected
 *
 * @param  RadioButtonGroup  reference to radio button object to be validated
 * @return bool
 */
function radioButtonFieldIsComplete(objRadioButtonGroup) 
{

	// Loop from zero to the one minus the number of radio button selections
	for (var counter = 0; counter < objRadioButtonGroup.length; counter++) 
	{
		// If a radio button has been selected it will return true
		if (objRadioButtonGroup[counter].checked) 
		{
			return true;
		}
	}

	// if we made it this far, then none were checked
	return false;
}


/**
 * Returns an array containing all selected/checked values for a form element.
 *
 * @param  Element objElement
 * @return array
 */
function getValues(objElement) 
{
	var i;
	var objGroup;
	var vals = new Array();
	var eType = objElement.type;
	if ((objElement.type == "checkbox") || (objElement.type == "radio")) 
	{
		objGroup = getFormElements(objElement.form, objElement.name);
		
		for (i = 0, j = 0; i < objGroup.length; i++) 
		{
			if (eType == objGroup[i].type) 
			{
				if (objGroup[i].checked) 
				{
					vals[j++] = objGroup[i].value;
				}
			}
		}
	} 
	else if (objElement.type == "select-multiple") 
	{
		objGroup = objElement.options;
		for (i = 0, j = 0; i < objGroup.length; i++) 
		{
			if (objGroup[i].selected) 
			{
				vals[j++] = objGroup[i].value;
			}
		}
	} 
	else 
	{
		vals[0] = objElement.value;
	}
	
	return vals;
}

/** ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
  * Toggle Functions
  * Helper functions to toggle visibility and/or images.  
  * Toggling is fun!
  * @author verysimple, inc. <www.verysimple.com>
  */

/** Toggles the visibility of the given tag
  * @param tagid the id of the dom item to toggle
  * @return void
  */
function toggle(tagid)
{
	var divtag = getTag(tagid);
	
	divtag.style.display = (divtag.style.display == 'none') ? '' : 'none';

}

/** Toggles the visibility of the given tag
  * @param tagid the id of the dom item to toggle
  * @return void
  */
function setVisibility(tagid, isvisible)
{
	var divtag = getTag(tagid);
	
	divtag.style.display = (isvisible) ? 'none' : '';

}

/** Toggles an image between two graphics
  * @param tagid the id of the dom item to toggle
  * @param url1 first of the two image rls being toggled
  * @param url2 second of the two image rls being toggled
  * @return void
  */
function toggleImage(tagid,url1,url2)
{
	var img = getTag(tagid);
	
	// we have to get the end of the scr string because the browser may
	// report back to us the full url & we won't get a match
	if (img.src.substr(img.src.length-url1.length,url1.length) == url1)
	{
	    img.src = url2;
	}
	else
	{
	    img.src = url1
	}
	
}

/** Sets an objects visibility to visible
  * @param tagid the id of the dom item to toggle
  * @return void
  */
function show(tagid)
{
	var divtag = getTag(tagid);
	
	divtag.style.display = '';

}

/** Sets an objects visibility to hidden
  * @param tagid the id of the dom item to toggle
  * @return void
  */
function hide(tagid)
{
	var divtag = getTag(tagid);
	
	divtag.style.display = 'none';

}

function convertDate(dt, eur)
{
	dt = trimWhiteSpace(dt);
	var i;
	var m = new Array("jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec");
	var oldDt = (dt == "")? "" : dt;

	// format the date into mm/dd/yyyy or dd/mm/yyyy
	dt = dt.replace(/[\.\/,\\-]/g, " ").replace(/[\s]+/g, "/");
	var datarray = dt.split("/");
	
	// converts year to fullyear
	if(typeof datarray[2] != "undefined")
	{
		datarray[2] = (datarray[2].length != 2)? datarray[2] : (parseInt(datarray[2]) < 49)? "20" + datarray[2] : "19" + datarray[2];
	}
	  
	// converts dates from dd/mm/yyyy to mm/dd/yyyy
	if (eur)
	{
		var d = datarray[0];
		datarray[0] = datarray[1];
		datarray[1] = d;
	}
	  
	// looks for months entered as strings and converts them to a number
	if (isNaN(datarray[0]) && typeof datarray[0] != 'undefined')
	{
		for (i = 0; i < 12; i++ )
		{
			if (datarray[0].toLowerCase().indexOf(m[i]) != -1)
			{
				dt = (i + 1) + "/" + datarray[1] + "/" + datarray[2];
				break;
			}
		}
	}
	else
	{
		dt = datarray[0] + "/" + datarray[1] + "/" + datarray[2];
	}
	
	dt = (dt.indexOf("undefined") == -1)? dt : oldDt;	
	
	return dt;
}

// checks date, fromdate and todate with european validation
function isDate(dat, from, to, eur)
{
	var ret = false;

	var i;

	// convert date to mm/dd/yyyy, setup date array and test date
	dat = convertDate(trimWhiteSpace(dat), eur);
	var datarray = dat.split("/");
	var testdat = new Date(Date.parse(dat));
	if (datarray[0] != (testdat.getMonth()+1) || datarray[1] != testdat.getDate() || datarray[2] != testdat.getFullYear() || datarray[0] == '' || datarray[1] == '' || datarray[2] == '')
	{
		return ret;
	}
	
	// test for valid date, from date and to date.
	ret = !((from && Date.parse(dat) <= Date.parse(convertDate(from, eur))) || (to && Date.parse(dat) >= Date.parse(convertDate(to, eur))));
	
	return ret;
}

// trim whitespace from a string.  t attribute can specify left ('l') or right ('r') trimming
function trimWhiteSpace(s,t)
{
	var ret = "";

	ret = (s)? (t == 'l')? s.replace(/\s*/, '') : (t == 'r')? s.replace(/(\s+)$/, '') : s.replace(/\s*/, '').replace(/(\s+)$/, '') : '';
	
	return ret;
}
