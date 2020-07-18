using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for RoleCriteria
	/// </summary>
	public class RoleCriteria : Criteria
	{
		public string Code;
		public string Description;
		public int PermissionBit = -1;

		protected override void Init()
		{
			this.fields = new Hashtable();
			this.fields.Add("Code", "r_code");
			this.fields.Add("Description", "r_description");
			this.fields.Add("PermissionBit", "r_permission_bit");
		}

		protected override string GetSelectSql()
		{
			return "select * from `role` r ";
		}

		protected override string GetWhereSql()
		{
			StringBuilder sb = new StringBuilder();
			string delim = " where ";

			if (null != Code)
			{
				sb.Append(delim + "r.r_code = '" + Preparer.Escape(Code) + "'");
				delim = " and ";
			}

			if (null != Description)
			{
				sb.Append(delim + "r.r_description = '" + Preparer.Escape(Description) + "'");
				delim = " and ";
			}

			if (-1 != PermissionBit)
			{
				sb.Append(delim + "r.r_permission_bit = '" + Preparer.Escape(PermissionBit) + "'");
				delim = " and ";
			}

			return sb.ToString();
		}
	}
}