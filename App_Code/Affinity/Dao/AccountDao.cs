using System;
using MySql.Data.MySqlClient;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// This code is automatically generated from the Account schema and
	/// should not be edited if at all possible.  All customizations and 
	/// business logic should be added to the partial class Account.cs
	/// </summary>
	public partial class Account : Loadable
	{


		public Account(Phreezer phreezer) : base(phreezer) { }
		public Account(Phreezer phreezer, MySqlDataReader reader) : base(phreezer,reader) { }

		/* ~~~ PUBLIC PROPERTIES ~~~ */


		private int _id = 0;
		public int Id
		{
			get { return this._id; }
			set { this._id = value; }
		}

		private string _username = "";
		public string Username
		{
			get { return this._username; }
			set { this._username = value; }
		}

		private string _password = "";
		public string Password
		{
			get { return this._password; }
			set { this._password = value; }
		}

		private string _firstName = "";
		public string FirstName
		{
			get { return this._firstName; }
			set { this._firstName = value; }
		}

		private string _lastName = "";
		public string LastName
		{
			get { return this._lastName; }
			set { this._lastName = value; }
		}

		private string _statusCode = "";
		public string StatusCode
		{
			get { return this._statusCode; }
			set { this._statusCode = value; }
		}

		private DateTime _created = DateTime.Now;
		public DateTime Created
		{
			get { return this._created; }
			set { this._created = value; }
		}

		private DateTime _modified = DateTime.Now;
		public DateTime Modified
		{
			get { return this._modified; }
			set { this._modified = value; }
		}

		private string _passwordHint = "";
		public string PasswordHint
		{
			get { return this._passwordHint; }
			set { this._passwordHint = value; }
		}

		private string _preferencesXml = "";
		public string PreferencesXml
		{
			get { return this._preferencesXml; }
			set { this._preferencesXml = value; }
		}

		private string _roleCode = "";
		public string RoleCode
		{
			get { return this._roleCode; }
			set { this._roleCode = value; }
		}

		private int _companyId = 0;
		public int CompanyId
		{
			get { return this._companyId; }
			set { this._companyId = value; }
		}

		private string _internalId = "";
		public string InternalId
		{
			get { return this._internalId; }
			set { this._internalId = value; }
		}

		private string _email = "";
		public string Email
		{
			get { return this._email; }
			set { this._email = value; }
		}

		private string _underwriterCodes = "";
		public string UnderwriterCodes
		{
			get { return this._underwriterCodes; }
			set { this._underwriterCodes = value; }
		}

		private string _underwriterEndorsements = "";
		public string UnderwriterEndorsements
		{
			get { return this._underwriterEndorsements; }
			set { this._underwriterEndorsements = value; }
		}

		private string _businessLicenseID = "";
		public string BusinessLicenseID
		{
			get { return this._businessLicenseID; }
			set { this._businessLicenseID = value; }
		}

		private string _individualLicenseID = "";
		public string IndividualLicenseID
		{
			get { return this._individualLicenseID; }
			set { this._individualLicenseID = value; }
		}

		private string _signature = "";
		public string Signature
		{
			get { return this._signature; }
			set { this._signature = value; }
		}

		/* ~~~ CONSTRAINTS ~~~ */

		private Company _accountCompany;
		public Company Company
		{
			get
			{
				if (this._accountCompany == null)
				{
					this._accountCompany = new Company(this.phreezer);
					this._accountCompany.Load(this.CompanyId);
				}
				return this._accountCompany;
			}
			set { this._accountCompany = value; }
		}

		private Role _accountRole;
		public Role Role
		{
			get
			{
				if (this._accountRole == null)
				{
					this._accountRole = new Role(this.phreezer);
					if (this.RoleCode == Role.AnonymousCode)
					{
						// there's no point in going to the database for this Role
						this._accountRole.Code = Role.AnonymousCode;
						this._accountRole.Description = Role.AnonymousCode;
						this._accountRole.PermissionBit = 0;
					}
					else
					{
						this._accountRole.Load(this.RoleCode);
					}
				}
				return this._accountRole;
			}
			set { this._accountRole = value; }
		}


		/* ~~~ SETS ~~~ */

		/// <summary>
		/// Returns a collection of Order objects
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns>Orders</returns>
		protected Orders GetAccountOrders(OrderCriteria criteria)
		{
			return GetAccountOrders(criteria, true);
		}

		/// <summary>
		/// Returns a collection of Order objects
		/// </summary>
		/// <param name="criteria"></param>
		/// <param name="filterOriginatorId"></param>
		/// <returns>Orders</returns>
		protected Orders GetAccountOrders(OrderCriteria criteria, bool filterOriginatorId)
		{
			if(filterOriginatorId) criteria.OriginatorId = this.Id;
			Orders accountOrders = new Orders(this.phreezer);
			accountOrders.Query(criteria);
			return accountOrders;
		}

		/// <summary>
		/// Returns a collection of OrderAssignment objects
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns>OrderAssignments</returns>
		public OrderAssignments GetAssignedOrders(OrderAssignmentCriteria criteria)
		{
			criteria.AccountId = this.Id;
			OrderAssignments assignedOrders = new OrderAssignments(this.phreezer);
			assignedOrders.Query(criteria);
			return assignedOrders;
		}


		/* ~~~ CRUD OPERATIONS ~~~ */

		/// <summary>
		/// Assigns a value to the primary key
		/// </summary>
		/// <param name="key"></param>
		protected override void SetPrimaryKey(object key)
		{
		   this.Id = (int)key;
		}


		/// <summary>
		/// Returns an SQL statement to delete this object from the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetDeleteSql()
		{
			return "delete from `account` where a_id = '" + Id.ToString() + "'";
		}

		/// <summary>
		/// reads the column values from a datareader and populates the object properties
		/// </summary>
		/// <param name="reader"></param>
		public override void Load(MySqlDataReader reader)
		{
			this.Id = Preparer.SafeInt(reader["a_id"]);
			this.Username = Preparer.SafeString(reader["a_username"]);
			this.Password = Preparer.SafeString(reader["a_password"]);
			this.FirstName = Preparer.SafeString(reader["a_first_name"]);
			this.LastName = Preparer.SafeString(reader["a_last_name"]);
			this.StatusCode = Preparer.SafeString(reader["a_status_code"]);
			this.Created = Preparer.SafeDateTime(reader["a_created"]);
			this.Modified = Preparer.SafeDateTime(reader["a_modified"]);
			this.PasswordHint = Preparer.SafeString(reader["a_password_hint"]);
			this.PreferencesXml = Preparer.SafeString(reader["a_preferences_xml"]);
			this.RoleCode = Preparer.SafeString(reader["a_role_code"]);
			this.CompanyId = Preparer.SafeInt(reader["a_company_id"]);
			this.InternalId = Preparer.SafeString(reader["a_internal_id"]);
			this.Email = Preparer.SafeString(reader["a_email"]);
			this.UnderwriterCodes = Preparer.SafeString(reader["a_underwriter_codes"]);
			this.UnderwriterEndorsements = Preparer.SafeString(reader["a_underwriter_endorsements"]);
			this.BusinessLicenseID = Preparer.SafeString(reader["a_business_license_id"]);
			this.IndividualLicenseID = Preparer.SafeString(reader["a_individual_license_id"]);
			this.Signature = Preparer.SafeString(reader["a_signature"]);

			this.OnLoad(reader);
		}

	}
}