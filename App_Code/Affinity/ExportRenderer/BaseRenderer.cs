using System;
using System.Collections;
using System.Text;

namespace Affinity.ExportRenderer
{
	/// <summary>
	/// Base interface for export renderers
	/// </summary>
	public class BaseRenderer : IBaseRenderer
	{
		protected Affinity.Request request;
		protected Hashtable settings;
		protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(PFTRenderer));

		public BaseRenderer(Affinity.Request req, Hashtable sys)
		{
			this.request = req;
			this.settings = sys;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public virtual string GetMimeType() 
		{
			return "text/plain"; 
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public virtual string GetFileName()
		{
			return this.request.Order.WorkingId + ".txt";
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="keyAttribute"></param>
		/// <returns></returns>
		public virtual string Render(string keyAttribute)
		{
			throw new Exception("Render() is not implemented");
			return "";
		}

		/// <summary>
		/// Converts the given HashTable to a string of key/value pairs per row
		/// </summary>
		/// <param name="ht"></param>
		/// <returns></returns>
		protected string HashTableToString(Hashtable ht)
		{
			StringBuilder sb = new StringBuilder();
			ArrayList keys = new ArrayList(ht.Keys);

			keys.Sort();
			foreach (string key in keys)
			{
				sb.Append(key.ToString() + "=" + ht[key].ToString().Replace("\r", " ").Replace("\n", " ") + "\r\n");
			}

			return sb.ToString();
		}
		
		protected void FormatDateField(Hashtable ht, string fieldName)
		{
			if (ht.ContainsKey(fieldName) && ht[fieldName] != "")
			{
				try
				{
					DateTime dt = DateTime.Parse(ht[fieldName].ToString());
					ht[fieldName] = dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0') + "/" + dt.Year.ToString();
				}
				catch(Exception ex)
				{
					if(fieldName.Equals("TX06DUDT")) {
						ht.Remove(fieldName);
					}
					log.Error("Error Formatting " + fieldName + " for Request ID " + this.request.Id.ToString(), ex);
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		protected string GetSystemSetting(string key)
		{
			return (string)this.settings[key];
		}

	}
}