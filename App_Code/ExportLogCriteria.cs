using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for ExportLogCriteria
	/// </summary>
	public class ExportLogCriteria : Criteria
	{
		public int Id = -1;
		public int AccountID;
		public int OrderID;
		public int RequestID;
		public string ExportFormat;
		public DateTime Created;
		public DateTime Modified;

		protected override void Init()
		{
			this.fields = new Hashtable();
			this.fields.Add("Id", "el_id");
			this.fields.Add("AccountID", "a_id");
			this.fields.Add("OrderID", "o_id");
			this.fields.Add("RequestID", "r_id");
			this.fields.Add("ExportFormat", "export_format");
			this.fields.Add("Created", "el_created");
			this.fields.Add("Modified", "el_modified");

		}

		protected override string GetSelectSql()
		{
			return "select * from export_log el ";
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

			if (OrderID > 0)
			{
				sb.Append(delim + "el.o_id = '" + Preparer.Escape(OrderID) + "'");
				delim = " and ";
			}

			if (RequestID > 0)
			{
				sb.Append(delim + "el.r_id = '" + Preparer.Escape(RequestID) + "'");
				delim = " and ";
			}

			if (null != ExportFormat)
			{
				sb.Append(delim + "el.export_format = '" + Preparer.Escape(ExportFormat) + "'");
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