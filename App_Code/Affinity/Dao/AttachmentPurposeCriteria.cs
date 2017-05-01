using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for AttachmentPurposeCriteria
	/// </summary>
	public class AttachmentPurposeCriteria : Criteria
	{
		public string Code;
		public string Description;
		public int SendNotification = -1;
		public string ChangeStatusTo;

		protected override void Init()
		{
			this.fields = new Hashtable();
			this.fields.Add("Code", "ap_code");
			this.fields.Add("Description", "ap_description");
			this.fields.Add("SendNotification", "ap_send_notification");
			this.fields.Add("ChangeStatusTo", "ap_change_status_to");
		}

		protected override string GetSelectSql()
		{
			return "select * from `attachment_purpose` ap ";
		}

		protected override string GetWhereSql()
		{
			StringBuilder sb = new StringBuilder();
			string delim = " where ";

			if (null != Code)
			{
				sb.Append(delim + "ap.ap_code = '" + Preparer.Escape(Code) + "'");
				delim = " and ";
			}

			if (null != Description)
			{
				sb.Append(delim + "ap.ap_description = '" + Preparer.Escape(Description) + "'");
				delim = " and ";
			}

			if (-1 != SendNotification)
			{
				sb.Append(delim + "ap.ap_send_notification = '" + Preparer.Escape(SendNotification) + "'");
				delim = " and ";
			}

			if (null != ChangeStatusTo)
			{
				sb.Append(delim + "ap.ap_change_status_to = '" + Preparer.Escape(ChangeStatusTo) + "'");
				delim = " and ";
			}

			return sb.ToString();
		}
	}
}