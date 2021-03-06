using System;
using MySql.Data.MySqlClient;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// This code is automatically generated from the HUDLog schema and
	/// should not be edited if at all possible.  All customizations and 
	/// business logic should be added to the partial class HUDLog.cs
	/// </summary>
	public partial class HUDLog : Loadable
	{


		public HUDLog(Phreezer phreezer) : base(phreezer) { }
		public HUDLog(Phreezer phreezer, MySqlDataReader reader) : base(phreezer,reader) { }

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

		private string _submissionXML = "";
		public string SubmissionXML
		{
			get { return this._submissionXML; }
			set { this._submissionXML = value; }
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

		/* ~~~ SETS ~~~ */

		/// <summary>
		/// Returns a collection of HUDLog objects
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns>HUDLogs</returns>
		protected HUDLogs GetHUDLogs(HUDLogCriteria criteria)
		{
			//criteria.OriginatorId = this.Id;
			HUDLogs HUDLogs = new HUDLogs(this.phreezer);
			HUDLogs.Query(criteria);
			return HUDLogs;
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
			return "delete from `hud_log` where h_id = '" + Id.ToString() + "'";
		}

		/// <summary>
		/// reads the column values from a datareader and populates the object properties
		/// </summary>
		/// <param name="reader"></param>
		public override void Load(MySqlDataReader reader)
		{
			this.Id = Preparer.SafeInt(reader["h_id"]);
			this.AccountID = Preparer.SafeInt(reader["a_id"]);
			this.SubmissionXML = Preparer.SafeString(reader["h_submission_xml"]);
			this.Created = Preparer.SafeDateTime(reader["h_created"]);
			this.Modified = Preparer.SafeDateTime(reader["h_modified"]);

			this.OnLoad(reader);
		}

	}
}