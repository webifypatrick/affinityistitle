using Com.VerySimple.Phreeze;
using Com.VerySimple.Util;
using System.Collections;

namespace Affinity
{
	/// <summary>
	/// Business Logic For SystemSetting Class
	/// </summary>
	public partial class SystemSetting
	{
		private Hashtable settingCache;
		public static string DefaultCode = "SYSTEM";

		/// <summary>
		/// 
		/// </summary>
		public Hashtable Settings
		{
			get
			{
				if (this.settingCache == null)
				{
					this.settingCache = XmlForm.GetResponseHashtable(this.Data);
				}
				return this.settingCache;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string GetSetting(string key)
		{
			return (this.Settings[key] == null) ? "" : this.Settings[key].ToString();
		}
	}
}