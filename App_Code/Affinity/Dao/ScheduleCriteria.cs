using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for AccountCriteria
	/// </summary>
	public class ScheduleCriteria : Criteria
	{
		public int Id;
		public DateTime First_notification;
		public DateTime Second_notification;
		public DateTime Search_package_date;

		protected override void Init()
		{
			this.fields = new Hashtable();
			this.fields.Add("Id", "s_id");
			this.fields.Add("First_notification", "s_first_notification");
			this.fields.Add("Second_notification", "s_second_notification");
			this.fields.Add("Search_package_date", "s_search_package_date");
			this.fields.Add("Created", "s_created");
			this.fields.Add("Modified", "s_modified");
			this.fields.Add("RequestID", "r_id");
			this.fields.Add("AttachmentID", "att_id");
			this.fields.Add("UploadAccountID", "ua_id");
			this.fields.Add("OrderID", "o_id");
			this.fields.Add("AccountID", "a_id");

		}

		protected override string GetSelectSql()
		{
			return "select * from `schedule` s inner join `account` a on a.a_id = s.a_id inner join `order` o on o.o_id = s.o_id inner join `request` r on r.r_id = s.r_id";
		}

		protected override string GetWhereSql()
		{
			StringBuilder sb = new StringBuilder();
			string delim = " where ";

			if ("1/1/0001 12:00:00 AM" != First_notification.ToString())
			{
				sb.Append(delim + "s.s_first_notification like '%" + Preparer.Escape(First_notification) + "%'");
				delim = " or ";
			}

			if ("1/1/0001 12:00:00 AM" != Second_notification.ToString())
			{
				sb.Append(delim + "s.s_second_notification like '%" + Preparer.Escape(Second_notification) + "%'");
				delim = " or ";
			}

			if ("1/1/0001 12:00:00 AM" != Search_package_date.ToString())
			{
				sb.Append(delim + "s.s_search_package_date like '%" + Preparer.Escape(Search_package_date) + "%'");
				delim = " or ";
			}
			return sb.ToString();
		}
	}
}