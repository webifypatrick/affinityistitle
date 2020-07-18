using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for LocationCriteria
	/// </summary>
	public class LocationCriteria : Criteria
	{
		public string Code;
		public string Description;

		protected override void Init()
		{
			this.fields = new Hashtable();
			this.fields.Add("Code", "l_code");
			this.fields.Add("Description", "l_description");
		}

		protected override string GetSelectSql()
		{
			return "select * from `location` l ";
		}

		protected override string GetWhereSql()
		{
			StringBuilder sb = new StringBuilder();
			string delim = " where ";

			if (null != Code)
			{
				sb.Append(delim + "l.l_code = '" + Preparer.Escape(Code) + "'");
				delim = " and ";
			}

			if (null != Description)
			{
				sb.Append(delim + "l.l_description = '" + Preparer.Escape(Description) + "'");
				delim = " and ";
			}

			return sb.ToString();
		}
	}
}