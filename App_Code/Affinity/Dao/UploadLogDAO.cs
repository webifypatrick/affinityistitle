using System;
using MySql.Data.MySqlClient;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// This code is automatically generated from the UploadLog schema and
	/// should not be edited if at all possible.  All customizations and 
	/// business logic should be added to the partial class UploadLog.cs
	/// </summary>
	public partial class UploadLog : Loadable
	{


		public UploadLog(Phreezer phreezer) : base(phreezer) { }
		public UploadLog(Phreezer phreezer, MySqlDataReader reader) : base(phreezer,reader) { }

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

		private int _uploadaccountId = -1;
		public int UploadAccountID
		{
			get { return this._uploadaccountId; }
			set { this._uploadaccountId = value; }
		}

		private int _orderId = -1;
		public int OrderID
		{
			get { return this._orderId; }
			set { this._orderId = value; }
		}

		private int _requestId = -1;
		public int RequestID
		{
			get { return this._requestId; }
			set { this._requestId = value; }
		}

		private int _attachmentId = -1;
		public int AttachmentID
		{
			get { return this._attachmentId; }
			set { this._attachmentId = value; }
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

		private Account _uploadaccount;
		public Account UploadAccount
		{
			get
			{
				if (this._uploadaccount == null)
				{
					this._uploadaccount = new Account(this.phreezer);
					this._uploadaccount.Load(this.UploadAccountID);
				}
				return this._uploadaccount;
			}
			set { this._uploadaccount = value; }
		}

		private Attachment _attachment;
		public Attachment Attachment
		{
			get
			{
				if (this._attachment == null)
				{
					this._attachment = new Attachment(this.phreezer);
					this._attachment.Load(this.AttachmentID);
				}
				return this._attachment;
			}
			set { this._attachment = value; }
		}

		/* ~~~ SETS ~~~ */

		/// <summary>
		/// Returns a collection of UploadLog objects
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns>UploadLogs</returns>
		protected UploadLogs GetUploadLogs(UploadLogCriteria criteria)
		{
			//criteria.OriginatorId = this.Id;
			UploadLogs uploadLogs = new UploadLogs(this.phreezer);
			uploadLogs.Query(criteria);
			return uploadLogs;
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
			return "delete from `upload_log` where ul_id = '" + Id.ToString() + "'";
		}

		/// <summary>
		/// reads the column values from a datareader and populates the object properties
		/// </summary>
		/// <param name="reader"></param>
		public override void Load(MySqlDataReader reader)
		{
			this.Id = Preparer.SafeInt(reader["ul_id"]);
			this.AccountID = Preparer.SafeInt(reader["a_id"]);
			this.UploadAccountID = Preparer.SafeInt(reader["ua_id"]);
			this.OrderID = Preparer.SafeInt(reader["o_id"]);
			this.RequestID = Preparer.SafeInt(reader["r_id"]);
			this.AttachmentID = Preparer.SafeInt(reader["att_id"]);
			this.Created = Preparer.SafeDateTime(reader["ul_created"]);
			this.Modified = Preparer.SafeDateTime(reader["ul_modified"]);

			this.OnLoad(reader);
		}

	}
}