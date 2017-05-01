using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Com.VerySimple.Util
{
    /// <summary>
    /// Summary description for Paths
    /// </summary>
    public static class Paths
    {
        /// <summary>
        /// This function returns the Application path, like http://www.JoesWebsite.com/, or 
        /// if the project is running in a virtual directory it returns the Application 
        /// path of the virtual directory, like http://www.JoesWebsite.com/forums/ .
        /// </summary>
        public static string ApplicationPath
        {
            get
            {
                string APP_PATH = System.Web.HttpContext.Current.Request.ApplicationPath.ToLower();
                if (APP_PATH == "/")      //a site
                    APP_PATH = "/";
                else if (!APP_PATH.EndsWith(@"/")) //a virtual
                    APP_PATH += @"/";

                return APP_PATH;

            }
        }


        /// <summary>
        /// This function returns the Mapped path, like "D:\Hosting\Smith\JoeSmithWebsite\"
        /// </summary>
        public static string MappedApplicationPath
        {
            get
            {
                string APP_PATH = System.Web.HttpContext.Current.Request.ApplicationPath.ToLower();
                if (APP_PATH == "/")      //a site
                    APP_PATH = "/";
                else if (!APP_PATH.EndsWith(@"/")) //a virtual
                    APP_PATH += @"/";

                string it = System.Web.HttpContext.Current.Server.MapPath(APP_PATH);
                if (!it.EndsWith(@"\"))
                    it += @"\";
                return it;
            }
        }
    }
}