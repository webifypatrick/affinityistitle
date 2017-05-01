using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for OrderStatusCriteria
	/// </summary>
	public class OrderStatusCriteria : Criteria
	{
		public string Code;
		public string Description;
		public int PermissionBit = -1;
		public int InternalExternal = -1;
		public int IsClosed = -1;

		protected override void Init()
		{
			this.fields = new Hashtable();
			this.fields.Add("Code", "os_code");
			this.fields.Add("Description", "os_description");
			this.fields.Add("PermissionBit", "os_permission_bit");
			this.fields.Add("InternalExternal", "os_internal_external");
			this.fields.Add("IsClosed", "os_is_closed");
		}

		protected override string GetSelectSql()
		{
			return "select * from `order_status` os ";
		}

		protected override string GetWhereSql()
		{
			StringBuilder sb = new StringBuilder();
			string delim = " where ";

			if (null != Code)
			{
				sb.Append(delim + "os.os_code = '" + Preparer.Escape(Code) + "'");
				delim = " and ";
			}

			if (null != Description)
			{
				sb.Append(delim + "os.os_description = '" + Preparer.Escape(Description) + "'");
				delim = " and ";
			}

			if (-1 != PermissionBit)
			{
				sb.Append(delim + "os.os_permission_bit = '" + Preparer.Escape(PermissionBit) + "'");
				delim = " and ";
			}

			if (-1 != InternalExternal)
			{
				sb.Append(delim + "os.os_internal_external = '" + Preparer.Escape(InternalExternal) + "'");
				delim = " and ";
			}

			if (-1 != IsClosed)
			{
				sb.Append(delim + "os.os_is_closed = '" + Preparer.Escape(IsClosed) + "'");
				delim = " and ";
			}

			return sb.ToString();
		}
	}
}