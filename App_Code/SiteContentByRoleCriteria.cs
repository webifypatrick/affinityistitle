using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for AccountCriteria
	/// </summary>
	public class SiteContentByRoleCriteria : Criteria
	{
		public int Id = -1;
		public string RoleCode = "";

		protected override void Init()
		{
			this.fields = new Hashtable();
			this.fields.Add("Id", "scr_id");
		}

		protected override string GetSelectSql()
		{
			return "select * from site_content_roles scr inner join role r on scr.scr_role_code = r.r_code ";
		}

		protected override string GetWhereSql()
		{
			StringBuilder sb = new StringBuilder();
			string delim = " where ";

			if (-1 != Id)
			{
				sb.Append(delim + "scr.scr_id = '" + Preparer.Escape(Id) + "'");
				delim = " and ";
			}

			if (null != RoleCode)
			{
				sb.Append(delim + "scr.scr_role_code = '" + Preparer.Escape(RoleCode) + "'");
				delim = " and ";
			}

			return sb.ToString();
		}
	}
}