using System;
using System.Data;
using System.Configuration;
using Com.VerySimple.Phreeze;
using System.Collections;

/// <summary>
/// This is the base class for all pages that initialiizes the phreezer property
/// </summary>
public class PageBase : System.Web.UI.Page
{
    protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(PageBase));

    protected Phreezer phreezer;
    private Affinity.Account account;

	public static string accountSessionVar = "ACCOUNT";
	public static string activeApplicationVar = "ACTIVE";

    /// <summary>
    /// The constructor makes sure that PageBase_Load is called
    /// prior to Page_Load of any inherited page
    /// </summary>
    public PageBase()
    {
        // make sure page load gets called so we can have access to the 
        // session and all that good stuff before the inherited page does
        this.Load += new System.EventHandler(this.PageBase_Load);
        this.Unload += new System.EventHandler(this.PageBase_Unload);
        this.Init += new System.EventHandler(this.PageBase_Init);
    }

    public void SetAccount(Affinity.Account acc)
    {
        Session[PageBase.accountSessionVar] = acc;
        this.account = acc;
		if (acc != null)
		{
			log.Info("Session started for " + acc.Username);
		}
    }

	/// <summary>
	/// returns the system setting with the given key
	/// </summary>
	/// <param name="key"></param>
	/// <returns></returns>
	public string GetSystemSetting(string key)
	{
		Hashtable ht = this.GetSystemSettings();
		
		string returnValue = "";
		if(ht == null)
		{
			this.Crash(310, "System Setting is missing.");
		}
		else
		{
			if(ht[key] == null)
			{
				this.Crash(310, "The following System Setting was missing: " + key);
			}
			else
			{
				returnValue = (string)ht[key];
			}
		}

		return returnValue;
	}

	/// <summary>
	/// Returns all system settings as a Hashtable
	/// </summary>
	/// <returns></returns>
	public Hashtable GetSystemSettings()
	{
		Hashtable ht = (Hashtable)Application[Affinity.SystemSetting.DefaultCode];
		return ht;
	}

    public Affinity.Account GetAccount()
    {
        if (this.account == null)
        {
            this.account = (Session[PageBase.accountSessionVar] != null) ? (Affinity.Account)Session[PageBase.accountSessionVar] : new Affinity.Account(this.phreezer);
        }
        return this.account;
    }

    /// <summary>
    /// If the current user doesn't have the permission required, redirect to the login page 
    /// is issued.
    /// </summary>
    /// <param name="permission">permission to be tested</param>
    public void RequirePermission(Affinity.RolePermission permission)
    {
        if (!this.GetAccount().Role.HasPermission(permission))
        {
			if (Session[PageBase.accountSessionVar] == null)
			{
				this.Crash(310, "Your session has expired due to inactivity");
			}
			else
			{
				this.Crash(301, "Access denied to user " + this.GetAccount().Username + ".  You do not have permission to access this page");
			}
        }
    }

	/// <summary>
	/// logs an error, redirects user to error page and terminates
	/// </summary>
	/// <param name="code"></param>
	/// <param name="feedback"></param>
	public void Crash(int code, string feedback)
	{
		if (code == 310)
		{
			log.Warn(this.GetType().ToString() + " Error " + code + ": " + feedback.Replace("\n", " "));
		}
		else
		{
			log.Error(this.GetType().ToString() + " Error " + code + ": " + feedback.Replace("\n", " "));
		}

		Response.Redirect("Error.aspx?code="+code+"&feedback="+Server.UrlEncode(feedback), true);
        Response.End();
	}

	/// <summary>
	/// Redirects the user using javascript so the back button is not easily usable
	/// </summary>
	/// <param name="url"></param>
	public void Redirect(string url)
	{
		Response.Redirect("Redirect.aspx?url=" + Server.UrlEncode(url));
		Response.End();
	}

	/// <summary>
	/// Marks a user as being currently active
	/// </summary>
	/// <param name="username"></param>
	public void AddActiveUser(string username)
	{
		Hashtable ht = (Hashtable)Application[PageBase.activeApplicationVar];

		if (username.Equals(""))
		{
			// do not log anonymous clicks
		}
		else if (ht.ContainsKey(username))
		{
			ht[username] = DateTime.Now;
		}
		else
		{
			ht.Add(username,DateTime.Now);
		}
	}

	/// <summary>
	/// Removes a user from the currently active list
	/// </summary>
	/// <param name="username"></param>
	public void DeleteActiveUser(string username)
	{
		log.Info("Session ended for " + username);
		Hashtable ht = (Hashtable)Application[PageBase.activeApplicationVar];
		if (ht.ContainsKey(username))
		{
			ht.Remove(username);
		}
	}

	/// <summary>
	/// Returns all active users based on their last click
	/// </summary>
	/// <returns></returns>
	public Hashtable GetActiveUsers()
	{
		Hashtable ht = (Hashtable)Application[PageBase.activeApplicationVar];
		ArrayList remove = null;

		foreach (string key in ht.Keys)
		{
			DateTime dt = (DateTime)ht[key];
			DateTime adjusted = dt.AddMinutes(Session.Timeout);

			// see if there are any we need to remove
			if (adjusted.CompareTo(DateTime.Now) < 0)
			{
				if (remove == null) {remove = new ArrayList();}
				remove.Add(key);
			}

		}

		// if there were any expired, remove them
		if (remove != null)
		{
			foreach (string rk in remove)
			{
				ht.Remove(rk);
			}
		} 
		
		return ht;
	}

    /// <summary>
    /// the phreezer object is initialized on page init so that it is available as early
    /// as possible.  this way the inherited page can override this method to create
    /// dynamically loaded persistable controls
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void PageBase_Init(object sender, System.EventArgs e)
    {
        this.phreezer = new Phreezer(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString);
    }

    /// <summary>
    /// at the moment this doesn't do anything, however this gives us the oportunity to
    /// execute just prior to the page load with access to all the rendered controls
    /// of the inherited page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void PageBase_Load(object sender, System.EventArgs e)
    {
		// track this click
		this.AddActiveUser(this.GetAccount().Username);
	}

    /// <summary>
    /// the phreezer connection is unloaded at this point
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void PageBase_Unload(object sender, System.EventArgs e)
    {
        // when the pagebase is being unloaded, make sure the connection is closed
		if (this.phreezer != null)
		{
			this.phreezer.Close();
		}
		else
		{
			log.Warn("this.phreezer was not instantiated at PageBase.PageBase_Unload");
		}
    }
}
