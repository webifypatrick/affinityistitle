using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for RequestStatusCriteria
	/// </summary>
	public class RequestStatusCriteria : Criteria
	{
		public string Code;
		public string Description;
		public int PermissionBit = -1;

		protected override void Init()
		{
			this.fields = new Hashtable();
			this.fields.Add("Code", "rs_code");
			this.fields.Add("Description", "rs_description");
			this.fields.Add("PermissionBit", "rs_permission_bit");
		}

		protected override string GetSelectSql()
		{
			return "select * from `request_status` rs ";
		}

		protected override string GetWhereSql()
		{
			StringBuilder sb = new StringBuilder();
			string delim = " where ";

			if (null != Code)
			{
				sb.Append(delim + "rs.rs_code = '" + Preparer.Escape(Code) + "'");
				delim = " and ";
			}

			if (null != Description)
			{
				sb.Append(delim + "rs.rs_description = '" + Preparer.Escape(Description) + "'");
				delim = " and ";
			}

			if (-1 != PermissionBit)
			{
				sb.Append(delim + "rs.rs_permission_bit = '" + Preparer.Escape(PermissionBit) + "'");
				delim = " and ";
			}

			return sb.ToString();
		}
	}
}