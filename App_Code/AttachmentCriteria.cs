using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for AttachmentCriteria
	/// </summary>
	public class AttachmentCriteria : Criteria
	{
		public int Id = -1;
		public int RequestId = -1;
		public string Name;
		public string MimeType;
		public int SizeKb = -1;
		public DateTime Created;
		public string Filepath;
		public string PurposeCode;

		public int OrderId = -1;

		protected override void Init()
		{
			this.fields = new Hashtable();
			this.fields.Add("Id", "att_id");
			this.fields.Add("RequestId", "att_request_id");
			this.fields.Add("Name", "att_name");
			this.fields.Add("MimeType", "att_mime_type");
			this.fields.Add("SizeKb", "att_size_kb");
			this.fields.Add("Created", "att_created");
			this.fields.Add("Filepath", "att_filepath");
			this.fields.Add("PurposeCode", "att_purpose_code");
		}

		protected override string GetSelectSql()
		{
			return "select * from `attachment` att"
				+ " inner join `request` r on att.att_request_id = r.r_id"
				+ " inner join `order` o on r.r_order_id = o.o_id";
		}

		protected override string GetWhereSql()
		{
			StringBuilder sb = new StringBuilder();
			string delim = " where ";

			if (-1 != OrderId)
			{
				sb.Append(delim + "o.o_id = '" + Preparer.Escape(OrderId) + "'");
				delim = " and ";
			}

			if (-1 != Id)
			{
				sb.Append(delim + "att.att_id = '" + Preparer.Escape(Id) + "'");
				delim = " and ";
			}

			if (-1 != RequestId)
			{
				sb.Append(delim + "att.att_request_id = '" + Preparer.Escape(RequestId) + "'");
				delim = " and ";
			}

			if (null != Name)
			{
				sb.Append(delim + "att.att_name = '" + Preparer.Escape(Name) + "'");
				delim = " and ";
			}

			if (null != MimeType)
			{
				sb.Append(delim + "att.att_mime_type = '" + Preparer.Escape(MimeType) + "'");
				delim = " and ";
			}

			if (-1 != SizeKb)
			{
				sb.Append(delim + "att.att_size_kb = '" + Preparer.Escape(SizeKb) + "'");
				delim = " and ";
			}

			if ("1-1-1 0:0:0" != Preparer.Escape(Created))
			{
				sb.Append(delim + "att.att_created = '" + Preparer.Escape(Created) + "'");
				delim = " and ";
			}

			if (null != Filepath)
			{
				sb.Append(delim + "att.att_filepath = '" + Preparer.Escape(Filepath) + "'");
				delim = " and ";
			}

			if (null != PurposeCode)
			{
				sb.Append(delim + "att.att_purpose_code = '" + Preparer.Escape(PurposeCode) + "'");
				delim = " and ";
			}

			return sb.ToString();
		}
	}
}