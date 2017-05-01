using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for SystemSettingCriteria
	/// </summary>
	public class SystemSettingCriteria : Criteria
	{
		public string Code;
		public string Description;
		public string Data;

		protected override void Init()
		{
			this.fields = new Hashtable();
			this.fields.Add("Code", "ss_code");
			this.fields.Add("Description", "ss_description");
			this.fields.Add("Data", "ss_data");
		}

		protected override string GetSelectSql()
		{
			return "select * from `system_setting` ss ";
		}

		protected override string GetWhereSql()
		{
			StringBuilder sb = new StringBuilder();
			string delim = " where ";

			if (null != Code)
			{
				sb.Append(delim + "ss.ss_code = '" + Preparer.Escape(Code) + "'");
				delim = " and ";
			}

			if (null != Description)
			{
				sb.Append(delim + "ss.ss_description = '" + Preparer.Escape(Description) + "'");
				delim = " and ";
			}

			if (null != Data)
			{
				sb.Append(delim + "ss.ss_data = '" + Preparer.Escape(Data) + "'");
				delim = " and ";
			}

			return sb.ToString();
		}
	}
}