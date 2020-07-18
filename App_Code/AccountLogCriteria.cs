using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for AccountLogCriteria
	/// </summary>
	public class AccountLogCriteria : Criteria
	{
		public int Id = -1;
		public int AccountID;
		public int AdminID;
		public string ChangeDesc;
		public DateTime Created;
		public DateTime Modified;

		protected override void Init()
		{
			this.fields = new Hashtable();
			this.fields.Add("Id", "al_id");
			this.fields.Add("AccountID", "a_id");
			this.fields.Add("AdminID", "admin_id");
			this.fields.Add("ChangeDesc", "change_desc");
			this.fields.Add("Created", "al_created");
			this.fields.Add("Modified", "al_modified");

		}

		protected override string GetSelectSql()
		{
			return "select * from Account_log al ";
		}

		protected override string GetWhereSql()
		{
			StringBuilder sb = new StringBuilder();
			string delim = " where ";

			if (AccountID > 0)
			{
				sb.Append(delim + "el.a_id = '" + Preparer.Escape(AccountID) + "'");
				delim = " and ";
			}

			if (AdminID > 0)
			{
				sb.Append(delim + "el.admin_id = '" + Preparer.Escape(AdminID) + "'");
				delim = " and ";
			}

			if (null != ChangeDesc)
			{
				sb.Append(delim + "el.change_desc = '" + Preparer.Escape(ChangeDesc) + "'");
				delim = " and ";
			}

			if ("1-1-1 0:0:0" != Preparer.Escape(Created))
			{
				sb.Append(delim + "el.el_created = '" + Preparer.Escape(Created) + "'");
				delim = " and ";
			}

			if ("1-1-1 0:0:0" != Preparer.Escape(Modified))
			{
				sb.Append(delim + "el.el_modified = '" + Preparer.Escape(Modified) + "'");
				delim = " and ";
			}

			return sb.ToString();
		}
	}
}