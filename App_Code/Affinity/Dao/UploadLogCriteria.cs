using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for UploadLogCriteria
	/// </summary>
	public class UploadLogCriteria : Criteria
	{
		public int Id = -1;
		public int AccountID;
		public int UploadAccountID;
		public int OrderID;
		public int RequestID;
		public int AttachmentID;
		public DateTime Created;
		public DateTime Modified;

		protected override void Init()
		{
			this.fields = new Hashtable();
			this.fields.Add("Id", "ul_id");
			this.fields.Add("AccountID", "a_id");
			this.fields.Add("UploadAccountID", "ua_id");
			this.fields.Add("OrderID", "o_id");
			this.fields.Add("RequestID", "r_id");
			this.fields.Add("AttachmentID", "att_id");
			this.fields.Add("Created", "ul_created");
			this.fields.Add("Modified", "ul_modified");

		}

		protected override string GetSelectSql()
		{
			return "select * from upload_log ul ";
		}

		protected override string GetWhereSql()
		{
			StringBuilder sb = new StringBuilder();
			string delim = " where ";

			if (AccountID > 0)
			{
				sb.Append(delim + "ul.a_id = '" + Preparer.Escape(AccountID) + "'");
				delim = " and ";
			}

			if (UploadAccountID > 0)
			{
				sb.Append(delim + "ul.ua_id = '" + Preparer.Escape(UploadAccountID) + "'");
				delim = " and ";
			}

			if (AttachmentID > 0)
			{
				sb.Append(delim + "ul.att_id = '" + Preparer.Escape(AttachmentID) + "'");
				delim = " and ";
			}

			if (OrderID > 0)
			{
				sb.Append(delim + "ul.o_id = '" + Preparer.Escape(OrderID) + "'");
				delim = " and ";
			}

			if (RequestID > 0)
			{
				sb.Append(delim + "ul.r_id = '" + Preparer.Escape(RequestID) + "'");
				delim = " and ";
			}

			if ("1-1-1 0:0:0" != Preparer.Escape(Created))
			{
				sb.Append(delim + "ul.ul_created = '" + Preparer.Escape(Created) + "'");
				delim = " and ";
			}

			if ("1-1-1 0:0:0" != Preparer.Escape(Modified))
			{
				sb.Append(delim + "ul.ul_modified = '" + Preparer.Escape(Modified) + "'");
				delim = " and ";
			}

			return sb.ToString();
		}
	}
}