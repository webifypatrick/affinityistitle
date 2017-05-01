using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for RequestTypeCriteria
	/// </summary>
	public class RequestTypeCriteria : Criteria
	{
		public string Code;
		public string Description;
		public string Definition;
		public int IsActive = -1;

		protected override void Init()
		{
			this.fields = new Hashtable();
			this.fields.Add("Code", "rt_code");
			this.fields.Add("Description", "rt_description");
			this.fields.Add("Definition", "rt_definition");
			this.fields.Add("IsActive", "rt_is_active");
		}

		protected override string GetSelectSql()
		{
			return "select * from `request_type` rt ";
		}

		protected override string GetWhereSql()
		{
			StringBuilder sb = new StringBuilder();
			string delim = " where ";

			if (null != Code)
			{
				sb.Append(delim + "rt.rt_code = '" + Preparer.Escape(Code) + "'");
				delim = " and ";
			}

			if (null != Description)
			{
				sb.Append(delim + "rt.rt_description = '" + Preparer.Escape(Description) + "'");
				delim = " and ";
			}

			if (null != Definition)
			{
				sb.Append(delim + "rt.rt_definition = '" + Preparer.Escape(Definition) + "'");
				delim = " and ";
			}

			if (-1 != IsActive)
			{
				sb.Append(delim + "rt.rt_is_active = '" + Preparer.Escape(IsActive) + "'");
				delim = " and ";
			}

			return sb.ToString();
		}
	}
}