using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using MySql.Data.MySqlClient;
using Com.VerySimple.Phreeze;
using Com.VerySimple.Util;
using System.Text;

namespace Affinity
{

	/// <summary>
	/// Business Logic For Account Class
	/// </summary>
	public partial class Account : IXmlFormCallback
	{
		private Hashtable prefCache;

		/// <summary>
		/// Parameterless constructor is here only so the object can be
		/// serialized for web services.  do not use this constructor!
		/// </summary>
		public Account() : base() { }


		/// <summary>
		/// alias for LastName, FirstName
		/// </summary>
		public string FullName
		{
			get
			{
				return this.LastName + ", " + this.FirstName;
			}
		}

		protected override void OnInit()
		{
			this.RoleCode = Role.AnonymousCode;
		}

		/// <summary>
		/// 
		/// </summary>
		[XmlIgnore]
		public Hashtable Preferences
		{
			get 
			{
				if (this.prefCache == null)
				{
					this.prefCache = XmlForm.GetResponseHashtable(this.PreferencesXml);
				}
				return this.prefCache;
			}
		}

		/// <summary>
		/// After updating preferences, clears the cache
		/// </summary>
		public void ClearPreferenceCache()
		{
			this.prefCache = null;
		}

		/// <summary>
		/// Returns the Preference with the key name, if not defined or blank, returns empty string
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string GetPreference(string key)
		{
			return this.GetPreference(key, "");
		}

		/// <summary>
		/// Returns the preference with the key name.  if not defined or blank, returns defaultVal
		/// </summary>
		/// <param name="key"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public string GetPreference(string key, string defaultVal)
		{
			return (this.Preferences[key] == null || this.Preferences[key].Equals("")) ? defaultVal : this.Preferences[key].ToString();
		}


		/// <summary>
		/// Onload is used to hide the password and instantiate the Role
		/// </summary>
		/// <param name="reader"></param>
		protected override void OnLoad(MySqlDataReader reader)
		{
			// never show the password
			this.Password = "";

			// instantiate the role
			this.Role = new Role(this.phreezer, reader);
		}

		/// <summary>
		/// This deletes any orders that do not have a request attached to them
		/// for this account.  This is done in the event that the current user
		/// has been using the back button and not completing orders
		/// </summary>
		public void CleanUpOrphanOrders()
		{
			string sql = "select o.* from `order` o"
			+ " left join request r on r.r_order_id = o.o_id"
			+ " where r.r_order_id is null"
			+ " and o.o_originator_id = '" + this.Id + "'";

			MySqlDataReader r = this.phreezer.ExecuteReader(sql);
			Orders orders = new Orders(this.phreezer);
			orders.Consume(r);
			r.Close();

			foreach (Order o in orders)
			{
				o.Delete();
			}


		}

		/// <summary>
		/// Return the underwriters that this account has access to use
		/// TODO: implmenet assignment of accounts to underwriters.  currently
		/// this returns all underwriters for everyone
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns></returns>
		public Underwriters GetUnderwriters()
		{
			Underwriters uw = new Underwriters(this.phreezer);
			UnderwriterCriteria uwc = new UnderwriterCriteria();
			uwc.Codes = this.UnderwriterCodes.Replace(",NLTIC", "").Replace(",DIR-NLTIC", "").Replace(",TICOR", "").Replace(",Dir-TICOR", "");
			uwc.AppendToOrderBy("Description",true);
			uw.Query(uwc);
			return uw;
		}


		/// <summary>
		/// Alias for GetAccountOrders
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns></returns>
		public Orders GetOrders(OrderCriteria criteria)
		{
			return GetOrders(criteria, true);
		}

		/// <summary>
		/// Alias for GetAccountOrders
		/// </summary>
		/// <param name="criteria"></param>
		/// <param name="filterOriginatorId"></param>
		/// <returns></returns>
		public Orders GetOrders(OrderCriteria criteria, bool filterOriginatorId)
		{
			return this.GetAccountOrders(criteria, filterOriginatorId);
		}

		/// <summary>
		/// Gets all orders from any other person within my company
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns></returns>
		public Orders GetCompanyOrders(OrderCriteria criteria)
		{
			criteria.CompanyId = this.CompanyId;
			Orders accountOrders = new Orders(this.phreezer);
			accountOrders.Query(criteria);
			return accountOrders;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="primaryKey"></param>
		/// <returns></returns>
		protected override string GetSelectSql(object pk)
		{
			// load the account and a type at the same time because we'll
			// almost always need it
			return "select * from account a inner join role r on a.a_role_code = r.r_code where a_id = '" + pk.ToString() + "'";
		}

		/// <summary>
		/// Persists updates to the DB.  Note that the password field is not updated.
		/// SetPassword() must be called to set the password
		/// </summary>
		/// <returns></returns>
		protected override string GetUpdateSql()
		{
			StringBuilder sb = new StringBuilder();

			// note password and created are not updated here
			sb.Append("update `account` set");
			sb.Append("  a_username = '" + Preparer.Escape(this.Username) + "'");
			//sb.Append(" ,a_password = '" + Preparer.Escape(this.Password) + "'");
			sb.Append(" ,a_first_name = '" + Preparer.Escape(this.FirstName) + "'");
			sb.Append(" ,a_last_name = '" + Preparer.Escape(this.LastName) + "'");
			sb.Append(" ,a_status_code = '" + Preparer.Escape(this.StatusCode) + "'");
			//sb.Append(" ,a_created = '" + Preparer.Escape(this.Created) + "'");
			sb.Append(" ,a_modified = sysdate()");
			sb.Append(" ,a_password_hint = '" + Preparer.Escape(this.PasswordHint) + "'");
			sb.Append(" ,a_preferences_xml = '" + Preparer.Escape(this.PreferencesXml) + "'");
			sb.Append(" ,a_role_code = '" + Preparer.Escape(this.RoleCode) + "'");
			sb.Append(" ,a_company_id = '" + Preparer.Escape(this.CompanyId) + "'");
			sb.Append(" ,a_internal_id = '" + Preparer.Escape(this.InternalId) + "'");
			sb.Append(" ,a_email = '" + Preparer.Escape(this.Email) + "'");
			sb.Append(" ,a_underwriter_codes = '" + Preparer.Escape(this.UnderwriterCodes) + "'");
			sb.Append(" ,a_underwriter_endorsements = '" + Preparer.Escape(this.UnderwriterEndorsements) + "'");
			sb.Append(" ,a_business_license_id = '" + Preparer.Escape(this.BusinessLicenseID) + "'");
			sb.Append(" ,a_individual_license_id = '" + Preparer.Escape(this.IndividualLicenseID) + "'");
			sb.Append(" ,a_signature = '" + Preparer.Escape(this.Signature) + "'");
			sb.Append(" where a_id = '" + Preparer.Escape(this.Id) + "'");

			return sb.ToString();
		}

		/// <summary>
		/// Inserts into the DB. Note that password field is not inserted
		/// SetPassword() must be called to set the password
		/// </summary>
		/// <returns></returns>
		protected override string GetInsertSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("insert into `account` (");
			sb.Append("  a_username");
			sb.Append(" ,a_password");
			sb.Append(" ,a_first_name");
			sb.Append(" ,a_last_name");
			sb.Append(" ,a_status_code");
			sb.Append(" ,a_created");
			sb.Append(" ,a_modified");
			sb.Append(" ,a_password_hint");
			sb.Append(" ,a_preferences_xml");
			sb.Append(" ,a_role_code");
			sb.Append(" ,a_company_id");
			sb.Append(" ,a_internal_id");
			sb.Append(" ,a_email");
			sb.Append(" ,a_underwriter_codes");
			sb.Append(" ,a_underwriter_endorsements");
			sb.Append(" ,a_business_license_id");
			sb.Append(" ,a_individual_license_id");
			sb.Append(" ,a_signature");
			sb.Append(" ) values (");
			sb.Append("  '" + Preparer.Escape(this.Username) + "'");
			sb.Append(" ,''");
			sb.Append(" ,'" + Preparer.Escape(this.FirstName) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.LastName) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.StatusCode) + "'");
			sb.Append(" ,sysdate()");
			sb.Append(" ,sysdate()");
			sb.Append(" ,'" + Preparer.Escape(this.PasswordHint) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.PreferencesXml) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.RoleCode) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.CompanyId) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.InternalId) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Email) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.UnderwriterCodes) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.UnderwriterEndorsements) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.BusinessLicenseID) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.IndividualLicenseID) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Signature) + "'");
			sb.Append(" )");

			return sb.ToString();
		}

		/// <summary>
		/// used for IXmlFormCallback
		/// </summary>
		/// <param name="field"></param>
		/// <param name="param"></param>
		/// <returns></returns>
		public Hashtable GetFormOptions(XmlNode field, string param)
		{
			Hashtable ht = new Hashtable();

			switch (param)
			{
				case "UNDERWRITER":
					Underwriters uw;

					// if ID = 0 this is a new account, so show all underwriters
					if (this.Id == 0)
					{
						uw = new Underwriters(this.phreezer);
						uw.Query(new UnderwriterCriteria());
					}
					else
					{
						uw = this.GetUnderwriters();
					}

					foreach (Underwriter u in uw)
					{
						ht.Add(u.Code, u.Description);
					}
					break;
				default:
					ht.Add("UNKNOWN", "UNKNOWN PARAM: " + param);
					break;
			}

			return ht;
		}

		/// <summary>
		/// used for IXmlFormCallback
		/// </summary>
		/// <param name="field"></param>
		/// <param name="param"></param>
		/// <returns></returns>
		public string GetFormDefault(XmlNode field, string param)
		{
			switch (param)
			{
				case "NOW":
					return DateTime.Now.ToShortDateString();
					break;
				default:
					return "UNKNOWN PARAM: " + param;
					break;
			}
		}

		/// <summary>
		/// Attempts to login using the given username/password.  if success, the a
		/// is loaded and true is returned.  otherwise returns false
		/// </summary>
		/// <param name="reader"></param>
		public bool Login(string username, string password)
		{

			string sql = "select * from account a "
			+ " inner join role r on a.a_role_code = r.r_code "
			+ " where a_username = '" + Preparer.Escape(username) + "'"
			+ " and a.a_password = aes_encrypt('" + Preparer.Escape(password) + "','" + Preparer.Escape(username) + "')";

			MySqlDataReader reader = this.phreezer.ExecuteReader(sql, System.Data.CommandBehavior.SingleRow);

			bool loginOk = false;

			if (reader.HasRows)
			{
				reader.Read();
				this.Load(reader);
				loginOk = true;
			}

			reader.Close();

			return loginOk;
		}

		/// <summary>
		/// persists the password for the a
		/// </summary>
		public void SetPassword()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(" update account set");
			sb.Append(" a_password = aes_encrypt('" + Preparer.Escape(this.Password) + "','" + Preparer.Escape(this.Username) + "')");
			sb.Append(" where a_id = '" + Preparer.Escape(this.Id) + "'");

			// set password fields back to blank
			this.Password = "";

			this.phreezer.ExecuteNonQuery(sb.ToString());
		}
	
	}
}