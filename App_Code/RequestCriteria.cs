using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for RequestCriteria
	/// </summary>
	public class RequestCriteria : Criteria
	{
		public int Id = -1;
		public string RequestTypeCode;
		public int OrderId = -1;
		public int OriginatorId = -1;
		public DateTime Created;
		public string StatusCode;
		public string Xml;
		public int IsCurrent = -1;
		public string Note;

		public int IdLessThan = -1;

		protected override void Init()
		{
			this.fields = new Hashtable();
			this.fields.Add("Id", "r_id");
			this.fields.Add("RequestTypeCode", "r_request_type_code");
			this.fields.Add("OrderId", "r_order_id");
			this.fields.Add("OriginatorId", "r_originator_id");
			this.fields.Add("Created", "r_created");
			this.fields.Add("StatusCode", "r_status_code");
			this.fields.Add("Xml", "r_xml");
			this.fields.Add("IsCurrent", "r_is_current");
			this.fields.Add("Note", "r_note");
		}

		protected override string GetSelectSql()
		{
			return "select * from `request` r ";
		}

		protected override string GetWhereSql()
		{
			StringBuilder sb = new StringBuilder();
			string delim = " where ";

			if (-1 != IdLessThan)
			{
				sb.Append(delim + "r.r_id < '" + Preparer.Escape(IdLessThan) + "'");
				delim = " and ";
			}

			if (-1 != Id)
			{
				sb.Append(delim + "r.r_id = '" + Preparer.Escape(Id) + "'");
				delim = " and ";
			}

			if (null != RequestTypeCode)
			{
				sb.Append(delim + "r.r_request_type_code = '" + Preparer.Escape(RequestTypeCode) + "'");
				delim = " and ";
			}

			if (-1 != OrderId)
			{
				sb.Append(delim + "r.r_order_id = '" + Preparer.Escape(OrderId) + "'");
				delim = " and ";
			}

			if (-1 != OriginatorId)
			{
				sb.Append(delim + "r.r_originator_id = '" + Preparer.Escape(OriginatorId) + "'");
				delim = " and ";
			}

			if ("1-1-1 0:0:0" != Preparer.Escape(Created))
			{
				sb.Append(delim + "r.r_created = '" + Preparer.Escape(Created) + "'");
				delim = " and ";
			}

			if (null != StatusCode)
			{
				sb.Append(delim + "r.r_status_code = '" + Preparer.Escape(StatusCode) + "'");
				delim = " and ";
			}

			if (null != Xml)
			{
				sb.Append(delim + "r.r_xml = '" + Preparer.Escape(Xml) + "'");
				delim = " and ";
			}

			if (-1 != IsCurrent)
			{
				sb.Append(delim + "r.r_is_current = '" + Preparer.Escape(IsCurrent) + "'");
				delim = " and ";
			}

			if (null != Note)
			{
				sb.Append(delim + "r.r_note = '" + Preparer.Escape(Note) + "'");
				delim = " and ";
			}

			return sb.ToString();
		}
	}
}