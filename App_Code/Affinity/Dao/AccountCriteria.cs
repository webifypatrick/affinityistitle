using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for AccountCriteria
	/// </summary>
	public class AccountCriteria : Criteria
	{
		public int Id = -1;
		public string Username;
		public string Password;
		public string FirstName;
		public string LastName;
		public string StatusCode;
		public DateTime Created;
		public DateTime Modified;
		public string PasswordHint;
		public string PreferencesXml;
		public string RoleCode;
		public int CompanyId = -1;
		public string InternalId;
		public string Email;
		public string BusinessLicenseID;
		public string IndividualLicenseID;

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
			this.fields.Add("BusinessLicenseID", "a_business_license_id");
			this.fields.Add("IndividualLicenseID", "a_individual_license_id");

		}

		protected override string GetSelectSql()
		{
			return "select * from account a inner join role r on a.a_role_code = r.r_code ";
		}

		protected override string GetWhereSql()
		{
			StringBuilder sb = new StringBuilder();
			string delim = " where ";

			if (-1 != Id)
			{
				sb.Append(delim + "a.a_id = '" + Preparer.Escape(Id) + "'");
				delim = " and ";
			}

			if (null != Username)
			{
				sb.Append(delim + "a.a_username = '" + Preparer.Escape(Username) + "'");
				delim = " and ";
			}

			if (null != Password)
			{
				sb.Append(delim + "a.a_password = '" + Preparer.Escape(Password) + "'");
				delim = " and ";
			}

			if (null != FirstName)
			{
				sb.Append(delim + "a.a_first_name = '" + Preparer.Escape(FirstName) + "'");
				delim = " and ";
			}

			if (null != LastName)
			{
				sb.Append(delim + "a.a_last_name = '" + Preparer.Escape(LastName) + "'");
				delim = " and ";
			}

			if (null != StatusCode)
			{
				sb.Append(delim + "a.a_status_code = '" + Preparer.Escape(StatusCode) + "'");
				delim = " and ";
			}

			if ("1-1-1 0:0:0" != Preparer.Escape(Created))
			{
				sb.Append(delim + "a.a_created = '" + Preparer.Escape(Created) + "'");
				delim = " and ";
			}

			if ("1-1-1 0:0:0" != Preparer.Escape(Modified))
			{
				sb.Append(delim + "a.a_modified = '" + Preparer.Escape(Modified) + "'");
				delim = " and ";
			}

			if (null != PasswordHint)
			{
				sb.Append(delim + "a.a_password_hint = '" + Preparer.Escape(PasswordHint) + "'");
				delim = " and ";
			}

			if (null != PreferencesXml)
			{
				sb.Append(delim + "a.a_preferences_xml = '" + Preparer.Escape(PreferencesXml) + "'");
				delim = " and ";
			}

			if (null != RoleCode)
			{
				sb.Append(delim + "a.a_role_code = '" + Preparer.Escape(RoleCode) + "'");
				delim = " and ";
			}

			if (-1 != CompanyId)
			{
				sb.Append(delim + "a.a_company_id = '" + Preparer.Escape(CompanyId) + "'");
				delim = " and ";
			}

			if (null != InternalId)
			{
				sb.Append(delim + "a.a_internal_id = '" + Preparer.Escape(InternalId) + "'");
				delim = " and ";
			}

			if (null != Email)
			{
				sb.Append(delim + "a.a_email = '" + Preparer.Escape(Email) + "'");
				delim = " and ";
			}

			if (null != BusinessLicenseID)
			{
				sb.Append(delim + "a.a_individual_license_id = '" + Preparer.Escape(BusinessLicenseID) + "'");
				delim = " and ";
			}

			if (null != IndividualLicenseID)
			{
				sb.Append(delim + "a.a_individual_license_id = '" + Preparer.Escape(IndividualLicenseID) + "'");
				delim = " and ";
			}

			return sb.ToString();
		}
	}
}