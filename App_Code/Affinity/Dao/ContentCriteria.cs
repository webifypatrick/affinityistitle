using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for ContentCriteria
	/// </summary>
	public class ContentCriteria : Criteria
	{
		public string Code;
		public string MetaTitle;
		public string MetaKeywords;
		public string MetaDescription;
		public string Header;
		public string Body;
		public DateTime Modified;

		protected override void Init()
		{
			this.fields = new Hashtable();
			this.fields.Add("Code", "ct_code");
			this.fields.Add("MetaTitle", "ct_meta_title");
			this.fields.Add("MetaKeywords", "ct_meta_keywords");
			this.fields.Add("MetaDescription", "ct_meta_description");
			this.fields.Add("Header", "ct_header");
			this.fields.Add("Body", "ct_body");
			this.fields.Add("Modified", "ct_modified");
		}

		protected override string GetSelectSql()
		{
			return "select * from `content` ct ";
		}

		protected override string GetWhereSql()
		{
			StringBuilder sb = new StringBuilder();
			string delim = " where ";

			if (null != Code)
			{
				sb.Append(delim + "ct.ct_code = '" + Preparer.Escape(Code) + "'");
				delim = " and ";
			}

			if (null != MetaTitle)
			{
				sb.Append(delim + "ct.ct_meta_title = '" + Preparer.Escape(MetaTitle) + "'");
				delim = " and ";
			}

			if (null != MetaKeywords)
			{
				sb.Append(delim + "ct.ct_meta_keywords = '" + Preparer.Escape(MetaKeywords) + "'");
				delim = " and ";
			}

			if (null != MetaDescription)
			{
				sb.Append(delim + "ct.ct_meta_description = '" + Preparer.Escape(MetaDescription) + "'");
				delim = " and ";
			}

			if (null != Header)
			{
				sb.Append(delim + "ct.ct_header = '" + Preparer.Escape(Header) + "'");
				delim = " and ";
			}

			if (null != Body)
			{
				sb.Append(delim + "ct.ct_body = '" + Preparer.Escape(Body) + "'");
				delim = " and ";
			}

			if ("1-1-1 0:0:0" != Preparer.Escape(Modified))
			{
				sb.Append(delim + "ct.ct_modified = '" + Preparer.Escape(Modified) + "'");
				delim = " and ";
			}

			return sb.ToString();
		}
	}
}