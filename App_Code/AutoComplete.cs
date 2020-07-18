using System.Web.Services;

using System.Configuration;

using Com.VerySimple.Phreeze;

/// <summary>
/// Summary description for AutoComplete
/// </summary>
[WebService(Namespace = "http://www.affinityistitle.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class AutoComplete : System.Web.Services.WebService
{

    public AutoComplete () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

	[System.Web.Services.WebMethod]
	[System.Web.Script.Services.ScriptMethod]
	public string[] GetCompanies(string prefixText, int count)
	{
		Phreezer phreezer = new Phreezer(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString);
        string[] returnval = new string[1];

        try { 
		    Affinity.Companys cs = new Affinity.Companys(phreezer);
		    Affinity.CompanyCriteria cc = new Affinity.CompanyCriteria();
		    cc.NameBeginsWith = prefixText;
		    cc.AppendToOrderBy("Name");
		    cc.MaxResults = count;

		    cs.Query(cc);
		    returnval = new string[cs.Count];

		    int i = 0;
		    foreach (Affinity.Company c in cs)
		    {
			    returnval[i++] = c.Name;
		    }
        }
        catch (System.Exception ex)
        {
            log4net.LogManager.GetLogger("Application_Error").Error("AutoComplete.cs:GetCompanies: " + ex.Message);
        }
        finally
        {
            phreezer.Close();
        }

        return returnval;
	}

	[System.Web.Services.WebMethod]
	[System.Web.Script.Services.ScriptMethod]
	public string[] GetCounties(string prefixText, int count)
	{
		Phreezer phreezer = new Phreezer(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString);
		Affinity.Zipcodes zs = new Affinity.Zipcodes(phreezer);
		Affinity.ZipcodeCriteria zc = new Affinity.ZipcodeCriteria();
		zc.CountyBeginsWith = prefixText;
		zc.DistinctCounty = true;
		zc.AppendToOrderBy("County");
		zc.MaxResults = count;

		zs.Query(zc);
		string[] returnval = new string[zs.Count];

		int i = 0;
		foreach (Affinity.Zipcode z in zs)
		{
			returnval[i++] = z.County;
		}

		phreezer.Close();

		return returnval;
	}

	[System.Web.Services.WebMethod]
	[System.Web.Script.Services.ScriptMethod]
	public string[] GetCities(string prefixText, int count)
	{
		Phreezer phreezer = new Phreezer(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString);
		Affinity.Zipcodes zs = new Affinity.Zipcodes(phreezer);
		Affinity.ZipcodeCriteria zc = new Affinity.ZipcodeCriteria();
		zc.CityBeginsWith = prefixText;
		zc.DistinctCity = true;
		zc.AppendToOrderBy("City");
		zc.MaxResults = count;

		zs.Query(zc);
		string[] returnval = new string[zs.Count];

		int i = 0;
		foreach (Affinity.Zipcode z in zs)
		{
			returnval[i++] = z.City;
		}

		phreezer.Close();

		return returnval;
	}


	[System.Web.Services.WebMethod]
	[System.Web.Script.Services.ScriptMethod]
	public string[] GetZips(string prefixText, int count)
	{
		Phreezer phreezer = new Phreezer(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString);
		Affinity.Zipcodes zs = new Affinity.Zipcodes(phreezer);
		Affinity.ZipcodeCriteria zc = new Affinity.ZipcodeCriteria();
		zc.ZipBeginsWith = prefixText;
		zc.AppendToOrderBy("Zip");
		zc.MaxResults = count;

		zs.Query(zc);
		string[] returnval = new string[zs.Count];

		int i = 0;
		foreach (Affinity.Zipcode z in zs)
		{
			returnval[i++] = z.Zip;
		}

		phreezer.Close();

		return returnval;
	}

}

