using System;
using MySql.Data.MySqlClient;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// This code is automatically generated from the AccountLog schema and
	/// should not be edited if at all possible.  All customizations and 
	/// business logic should be added to the partial class AccountLog.cs
	/// </summary>
	public partial class AccountLog : Loadable
	{


		public AccountLog(Phreezer phreezer) : base(phreezer) { }
		public AccountLog(Phreezer phreezer, MySqlDataReader reader) : base(phreezer,reader) { }

		/* ~~~ PUBLIC PROPERTIES ~~~ */


		private int _id = 0;
		public int Id
		{
			get { return this._id; }
			set { this._id = value; }
		}

		private int _accountId = -1;
		public int AccountID
		{
			get { return this._accountId; }
			set { this._accountId = value; }
		}

		private int _adminId = -1;
		public int AdminID
		{
			get { return this._adminId; }
			set { this._adminId = value; }
		}

		private string _changeDesc = "";
		public string ChangeDesc
        {
			get { return this._changeDesc; }
			set { this._changeDesc = value; }
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

		/* ~~~ CONSTRAINTS ~~~ */

		private Account _account;
		public Account Account
		{
			get
			{
				if (this._account == null)
				{
					this._account = new Account(this.phreezer);
					this._account.Load(this.AccountID);
				}
				return this._account;
			}
			set { this._account = value; }
		}

		private Account _adminaccount;
		public Account AdminAccount
		{
			get
			{
				if (this._adminaccount == null)
				{
					this._adminaccount = new Account(this.phreezer);
					this._adminaccount.Load(this.AdminID);
				}
				return this._adminaccount;
			}
			set { this._adminaccount = value; }
		}


		/* ~~~ SETS ~~~ */

		/// <summary>
		/// Returns a collection of AccountLog objects
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns>AccountLogs</returns>
		protected AccountLogs GetAccountLogs(AccountLogCriteria criteria)
		{
			//criteria.OriginatorId = this.Id;
			AccountLogs AccountLogs = new AccountLogs(this.phreezer);
			AccountLogs.Query(criteria);
			return AccountLogs;
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
			return "delete from `Account_log` where al_id = '" + Id.ToString() + "'";
		}

		/// <summary>
		/// reads the column values from a datareader and populates the object properties
		/// </summary>
		/// <param name="reader"></param>
		public override void Load(MySqlDataReader reader)
		{
			this.Id = Preparer.SafeInt(reader["al_id"]);
			this.AccountID = Preparer.SafeInt(reader["a_id"]);
			this.AdminID = Preparer.SafeInt(reader["admin_id"]);
			this.ChangeDesc = Preparer.SafeString(reader["change_desc"]);
			this.Created = Preparer.SafeDateTime(reader["al_created"]);
			this.Modified = Preparer.SafeDateTime(reader["al_modified"]);

			this.OnLoad(reader);
		}

	}
}