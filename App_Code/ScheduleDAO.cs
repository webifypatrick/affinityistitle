using System;
using MySql.Data.MySqlClient;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// This code is automatically generated from the Company schema and
	/// should not be edited if at all possible.  All customizations and 
	/// business logic should be added to the partial class Company.cs
	/// </summary>
	public partial class Schedule : Loadable
	{


		public Schedule(Phreezer phreezer) : base(phreezer) { }
		public Schedule(Phreezer phreezer, MySqlDataReader reader) : base(phreezer,reader) { }

		/* ~~~ PUBLIC PROPERTIES ~~~ */

		private int _id = 0;
		public int Id
		{
			get { return this._id; }
			set { this._id = value; }
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

		private int _accountId = -1;
		public int AccountID
		{
			get { return this._accountId; }
			set { this._accountId = value; }
		}

		private int _upload_accountId = -1;
		public int UploadAccountID
		{
			get { return this._upload_accountId; }
			set { this._upload_accountId = value; }
		}

		private DateTime _search_package_date = DateTime.Now;
        public DateTime Search_package_date
        {
            get { return this._search_package_date; }
            set { this._search_package_date = value; }
        }

        private DateTime _first_notification;
        public DateTime First_notification
        {
            get { return this._first_notification; }
            set { this._first_notification = value; }
        }

        private DateTime _second_notification;
        public DateTime Second_notification
        {
            get { return this._second_notification; }
            set { this._second_notification = value; }
        }

        /* ~~~ CONSTRAINTS ~~~ */


        /* ~~~ SETS ~~~ */

        /* ~~~ CRUD OPERATIONS ~~~ */

        /// <summary>
        /// Assigns a value to the primary key
        /// </summary>
        /// <param name="key"></param>
        protected override void SetPrimaryKey(object key)
		{
			int id = 0;
			int.TryParse(key.ToString(), out id);
			Id = id;
		}

		/// <summary>
		/// Returns a SQL statement to select this object from the DB
		/// </summary>
		/// <param name="pk"></param>
		/// <returns></returns>
		protected override string GetSelectSql(object pk)
		{
			return "select * from `schedule` s where s.Id = " + pk.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to update this object in the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetUpdateSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("update `schedule` set");
			sb.Append("  a_id = '" + Preparer.Escape(this.AccountID) + "'");
            sb.Append(" ,o_id = '" + Preparer.Escape(this.OrderID) + "'");
            sb.Append(" ,r_id = '" + Preparer.Escape(this.RequestID) + "'");
            sb.Append(" ,att_id = '" + Preparer.Escape(this.AttachmentID) + "'");
            sb.Append(" ,ua_id = '" + Preparer.Escape(this.UploadAccountID) + "'");
            sb.Append(" ,s_search_package_date = '" + Preparer.Escape(this.Search_package_date) + "'");
            sb.Append(" ,s_first_notification = '" + Preparer.Escape(this.First_notification) + "'");
            sb.Append(" ,s_second_notification = '" + Preparer.Escape(this.Second_notification) + "'");
            sb.Append(" ,s_modified = '" + Preparer.Escape(this.Modified) + "'");
			sb.Append(" where s_id = '" + Preparer.Escape(this.Id) + "'");
			return sb.ToString();
		}

        /// <summary>
        /// Returns an SQL statement to insert this object into the DB
        /// </summary>
        /// <returns></returns>
        protected override string GetInsertSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("insert into `schedule` (");
			sb.Append("  a_id");
			sb.Append(" ,o_id");
			sb.Append(" ,r_id");
			sb.Append(" ,att_id");
			sb.Append(" ,ua_id");
			sb.Append(" ,s_search_package_date");
			sb.Append(" ,s_modified");
			sb.Append(" ,s_created");
			sb.Append(" ) values (");
			sb.Append("  '" + Preparer.Escape(this.AccountID) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.OrderID) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.RequestID) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.AttachmentID) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.UploadAccountID) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Search_package_date) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Modified) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Created) + "'");
			sb.Append(" )");

			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to delete this object from the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetDeleteSql()
		{
			return "delete from `schedule` where s_id = '" + Id.ToString() + "'";
		}

		/// <summary>
		/// reads the column values from a datareader and populates the object properties
		/// </summary>
		/// <param name="reader"></param>
		public override void Load(MySqlDataReader reader)
		{
			if(reader.HasRows)
			{
        this.Id = Preparer.SafeInt(reader["s_id"]);
        this.AccountID = Preparer.SafeInt(reader["a_id"]);
        this.OrderID = Preparer.SafeInt(reader["o_id"]);
				this.RequestID = Preparer.SafeInt(reader["r_id"]);
				this.AttachmentID = Preparer.SafeInt(reader["att_id"]);
				this.UploadAccountID = Preparer.SafeInt(reader["ua_id"]);
				this.Search_package_date = Preparer.SafeDateTime(reader["s_search_package_date"]);
				this.First_notification = Preparer.SafeDateTime(reader["s_first_notification"]);
				this.Second_notification = Preparer.SafeDateTime(reader["s_second_notification"]);
				this.Modified = Preparer.SafeDateTime(reader["s_modified"]);
				this.Created = Preparer.SafeDateTime(reader["s_created"]);

				this.OnLoad(reader);
			}
		}

	}
}