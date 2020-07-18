using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Helper class for dealing with values that you don't want to be null
/// </summary>
public class NoNull
{
	public static string GetString(object val)
	{
		return GetString(val, "");
	}

	public static string GetString(object val, string defaultVal)
	{
		return (val == null) ? defaultVal : val.ToString();
	}

	public static int GetInt(object val, int defaultVal)
	{
		return (val == null) ? defaultVal : int.Parse( val.ToString() );
	}
}
