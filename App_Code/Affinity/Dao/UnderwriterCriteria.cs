using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for UnderwriterCriteria
	/// </summary>
	public class UnderwriterCriteria : Criteria
	{
		public string Code;
		public string Description;

		/// <summary>
		/// Codes accepts a comma-separated list of underwriter codes
		/// </summary>
		public string Codes;

		protected override void Init()
		{
			this.fields = new Hashtable();
			this.fields.Add("Code", "u_code");
			this.fields.Add("Description", "u_description");
		}

		protected override string GetSelectSql()
		{
			return "select * from `underwriter` u ";
		}

		protected override string GetWhereSql()
		{
			StringBuilder sb = new StringBuilder();
			string delim = " where ";

			if (null != Codes)
			{
				// we accept a comma-separated list like so: code1,code2,code3
				// so we'll convert this to sql " in ('code1','code2','code3')"
				string[] carr = Codes.Split(",".ToCharArray());
				string ccode = "''";
				foreach (string c in carr)
				{
					ccode += ",'" + Preparer.Escape(c.Trim()) + "'";
				}

				sb.Append(delim + "u.u_code in (" + ccode + ")");
				delim = " and ";
			}

			if (null != Code)
			{
				sb.Append(delim + "u.u_code = '" + Preparer.Escape(Code) + "'");
				delim = " and ";
			}

			if (null != Description)
			{
				sb.Append(delim + "u.u_description = '" + Preparer.Escape(Description) + "'");
				delim = " and ";
			}

			return sb.ToString();
		}
	}
}