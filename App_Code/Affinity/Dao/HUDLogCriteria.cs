using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for HUDLogCriteria
	/// </summary>
	public class HUDLogCriteria : Criteria
	{
		public int Id = -1;
		public int AccountID;
		public string SubmissionXML;
		public DateTime Created;
		public DateTime Modified;

		protected override void Init()
		{
			this.fields = new Hashtable();
			this.fields.Add("Id", "h_id");
			this.fields.Add("AccountID", "a_id");
			this.fields.Add("SubmissionXML", "h_submission_xml");
			this.fields.Add("Created", "h_created");
			this.fields.Add("Modified", "h_modified");

		}

		protected override string GetSelectSql()
		{
			return "select * from hud_log ul ";
		}

		protected override string GetWhereSql()
		{
			StringBuilder sb = new StringBuilder();
			string delim = " where ";

			if (AccountID > 0)
			{
				sb.Append(delim + "h.a_id = '" + Preparer.Escape(AccountID) + "'");
				delim = " and ";
			}

			if (!SubmissionXML.Equals(""))
			{
				sb.Append(delim + "h.h_submission_xml = '" + Preparer.Escape(SubmissionXML) + "'");
				delim = " and ";
			}

			if ("1-1-1 0:0:0" != Preparer.Escape(Created))
			{
				sb.Append(delim + "h.h_created = '" + Preparer.Escape(Created) + "'");
				delim = " and ";
			}

			if ("1-1-1 0:0:0" != Preparer.Escape(Modified))
			{
				sb.Append(delim + "h.h_modified = '" + Preparer.Escape(Modified) + "'");
				delim = " and ";
			}

			return sb.ToString();
		}
	}
}