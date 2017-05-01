using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for OrderAssignmentCriteria
	/// </summary>
	public class OrderAssignmentCriteria : Criteria
	{
		public int AccountId = -1;
		public int OrderId = -1;
		public string PermissionBit;

		protected override void Init()
		{
			this.fields = new Hashtable();
			this.fields.Add("AccountId", "oa_account_id");
			this.fields.Add("OrderId", "oa_order_id");
			this.fields.Add("PermissionBit", "oa_permission_bit");
		}

		protected override string GetSelectSql()
		{
			return "select * from `order_assignment` oa ";
		}

		protected override string GetWhereSql()
		{
			StringBuilder sb = new StringBuilder();
			string delim = " where ";

			if (-1 != AccountId)
			{
				sb.Append(delim + "oa.oa_account_id = '" + Preparer.Escape(AccountId) + "'");
				delim = " and ";
			}

			if (-1 != OrderId)
			{
				sb.Append(delim + "oa.oa_order_id = '" + Preparer.Escape(OrderId) + "'");
				delim = " and ";
			}

			if (null != PermissionBit)
			{
				sb.Append(delim + "oa.oa_permission_bit = '" + Preparer.Escape(PermissionBit) + "'");
				delim = " and ";
			}

			return sb.ToString();
		}
	}
}