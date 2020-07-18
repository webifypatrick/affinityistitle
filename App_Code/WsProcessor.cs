using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

using System.Configuration;

using Com.VerySimple.Phreeze;
using Com.VerySimple.Util;
using System.Xml;

/// <summary>
/// Summary description for WsProcessor
/// </summary>
[WebService(Namespace = "http://www.affinityistitle.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WsProcessor : System.Web.Services.WebService
{

	public WsProcessor()
	{

		//Uncomment the following line if using designed components 
		//InitializeComponent(); 
	}


	/// <summary>
	/// Allows the web service consumer to authenticate and receive a WsToken in order to
	/// access functions in the web service
	/// </summary>
	/// <param name="username"></param>
	/// <param name="password"></param>
	/// <returns>Affinity.WsToken</returns>
	[WebMethod]
	[System.Xml.Serialization.XmlInclude(typeof(Affinity.WsToken))]
	public Affinity.WsToken Authenticate(string username, string password)
	{
		Affinity.WsToken token = new Affinity.WsToken();
		token.Username = username;
		token.SessionId = "";

		return token;
	}



	/// <summary>
	/// Returns a list of all new requests that have not yet been "confirmed"
	/// </summary>
	/// <param name="token">Affinity.WsToken</param>
	/// <returns>Affinity.Requests</returns>
	[WebMethod]
	[System.Xml.Serialization.XmlInclude(typeof(Affinity.Request))]
	public Affinity.Requests GetNewRequests(Affinity.WsToken token)
	{
		Phreezer phreezer = new Phreezer(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString);
		Affinity.Requests rs = new Affinity.Requests(phreezer);

		try
		{
			rs.Query(new Affinity.RequestCriteria());
		}
		catch (Exception)
		{
		}
		finally
		{
			phreezer.Close();
		}

		return rs;
	}


	/// <summary>
	/// Syncs a web order with data from SoftPro
	/// </summary>
	/// <param name="token">Affinity.WsToken</param>
	/// <param name="doc">XmlDocument using the Affinity request schema</param>
	/// <returns>Affinity.WsResponse</returns>
	[WebMethod]
	public Affinity.WsResponse SyncRequest(Affinity.WsToken token, System.Xml.XmlDocument doc)
	{
		Affinity.WsResponse resp = new Affinity.WsResponse();

		Phreezer phreezer = new Phreezer(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString);

		try
		{
			Hashtable ht = new Hashtable();
			XmlNodeList fields = doc.GetElementsByTagName("field");

			// enumerate all the fields and convert to a hashtable
			foreach (XmlNode field in fields)
			{
				ht.Add(XmlForm.GetAttribute(field, "sp_id"), field.InnerText);
			}

			if (ht.ContainsKey("WEB_ID") == false || ht["WEB_ID"].Equals("") )
			{
				throw new Exception("WEB_ID is required");
			}

			if (ht.ContainsKey("AFF_ID") == false || ht["AFF_ID"].Equals("") )
			{
				throw new Exception("AFF_ID is required");
			}

			Affinity.Order order = new Affinity.Order(phreezer);
			order.Load(ht["WEB_ID"]);

			if (order.InternalId.Equals(""))
			{
				// the order doesn't have an AFF ID so this is a confirmation

				// we have to get the system settings to pass them in to the order confirm method
				Hashtable settings = (Hashtable)Application[Affinity.SystemSetting.DefaultCode];

				resp = order.Confirm(ht["AFF_ID"].ToString(), settings);

			}
			else
			{
				resp.IsSuccess = true;
				resp.ActionWasTaken = false;
				resp.Message = "No action was taken";
			}

		}
		catch (Exception ex)
		{
			resp.Message = ex.Message;
		}
		finally
		{
			phreezer.Close();
		}

		return resp;
	}
}

