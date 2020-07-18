using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for AccountCriteria
	/// </summary>
	public class SearchAccountCriteria : Criteria
	{
		public string Username;
		public string FirstName;
		public string LastName;
		public string Company;
		public string Email;
		public string RoleCode;

		protected override void Init()
		{
			this.fields = new Hashtable();
			this.fields.Add("Id", "a_id");
			this.fields.Add("Username", "a_username");
			this.fields.Add("Password", "a_password");
			this.fields.Add("FirstName", "a_first_name");
			this.fields.Add("LastName", "a_last_name");
			this.fields.Add("StatusCode", "a_status_code");
			this.fields.Add("Created", "a_created");
			this.fields.Add("Modified", "a_modified");
			this.fields.Add("PasswordHint", "a_password_hint");
			this.fields.Add("PreferencesXml", "a_preferences_xml");
			this.fields.Add("RoleCode", "a_role_code");
			this.fields.Add("CompanyId", "a_company_id");
			this.fields.Add("InternalId", "a_internal_id");
			this.fields.Add("Email", "a_email");

		}

		protected override string GetSelectSql()
		{
			return "select * from account a inner join role r on a.a_role_code = r.r_code inner join company c on a.a_company_id = c.c_id ";
		}

		protected override string GetWhereSql()
		{
			StringBuilder sb = new StringBuilder();
			string delim = " where ";

			if (null != Username)
			{
				sb.Append(delim + "a.a_username like '%" + Preparer.Escape(Username) + "%'");
				delim = " or ";
			}

			if (null != FirstName)
			{
				sb.Append(delim + "a.a_first_name like '%" + Preparer.Escape(FirstName) + "%'");
				delim = " or ";
			}

			if (null != LastName)
			{
				sb.Append(delim + "a.a_last_name like '%" + Preparer.Escape(LastName) + "%'");
				delim = " or ";
			}

			if (null != Company)
			{
				sb.Append(delim + "c.c_name like '%" + Preparer.Escape(Company) + "%'");
				delim = " or ";
			}

			if (null != RoleCode)
			{
				sb.Append(delim + "a.a_role_code like '%" + Preparer.Escape(RoleCode) + "%'");
				delim = " or ";
			}

			{
				sb.Append(delim + "a.a_email like '%" + Preparer.Escape(Email) + "%'");
				delim = " or ";
			}

			return sb.ToString();
		}
	}
}