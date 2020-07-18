using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for AttachmentRolesCriteria
	/// </summary>
	public class AttachmentRolesCriteria : Criteria
	{
		public string RoleCode;
		public string AttachmentPurposeCode;

		protected override void Init()
		{
			this.fields = new Hashtable();
			this.fields.Add("RoleCode", "r_code");
			this.fields.Add("AttachmentPurposeCode", "ap_code");

		}

		protected override string GetSelectSql()
		{
			return "select * from attachment_roles_descriptions";
		}

		protected override string GetWhereSql()
		{
			StringBuilder sb = new StringBuilder();
			string delim = " where ";

			if (null != RoleCode)
			{
				sb.Append(delim + "r_code = '" + Preparer.Escape(RoleCode) + "'");
				delim = " and ";
			}

			if (null != AttachmentPurposeCode)
			{
				sb.Append(delim + "ap_code = '" + Preparer.Escape(AttachmentPurposeCode) + "'");
				delim = " and ";
			}

			return sb.ToString();
		}
	}
}