/**
 * Extend jquery to get input when numeric amount is expected.
 * Will first attempt to remove non-numeric chars.
 * @param mixed if NaN, return this value instead
 * @return float or NaN or ifNaNValue
 */
$.prototype.valAsFloat = function(ifNaNValue)
{
	if (ifNaNValue == null) ifNaNValue = Number.NaN;
	var value = parseFloat(this.val().replace(/[^0-9|.]/g, ''));
	if (isNaN(value)) value = ifNaNValue;
	return value;
}

/**
 * Extends jquery to format currency
 * @param string numeric value
 * @param string if num isNaN return this value w/o parsing (default = 0)
 * @return string formatted as US currency
 */
$.formatCurrency = function(num,ifNaNValue) {
	num = num.toString().replace(/\$|\,/g,'');
	if(isNaN(num)) 
	{
		if (ifNaNValue)
		{
			return ifNaNValue;
		}
		else
		{
			num = "0";
		}
	}
	
	sign = (num == (num = Math.abs(num)));
	num = Math.floor(num*100+0.50000000001);
	cents = num%100;
	num = Math.floor(num/100).toString();
	if(cents<10)
	cents = "0" + cents;
	for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
	num = num.substring(0,num.length-(4*i+3))+','+
	num.substring(num.length-(4*i+3));
	return (((sign)?'':'-') + '$' + num + '.' + cents);
}

/**
 * updates the html property with a currency value
 * @param string numeric value
 * @param string if num isNaN return this value w/o parsing (default = value)
 */
$.prototype.htmlCurrency = function(value,ifNaNValue)
{
	this.html( $.formatCurrency(value,ifNaNValue ? ifNaNValue : value) );
}