using System;
using MySql.Data.MySqlClient;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// This code is automatically generated from the ExportLog schema and
	/// should not be edited if at all possible.  All customizations and 
	/// business logic should be added to the partial class ExportLog.cs
	/// </summary>
	public partial class ExportLog : Loadable
	{


		public ExportLog(Phreezer phreezer) : base(phreezer) { }
		public ExportLog(Phreezer phreezer, MySqlDataReader reader) : base(phreezer,reader) { }

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

		private string _exportFormat = "";
		public string ExportFormat
		{
			get { return this._exportFormat; }
			set { this._exportFormat = value; }
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

		private Order _order;
		public Order Order
		{
			get
			{
				if (this._order == null)
				{
					this._order = new Order(this.phreezer);
					this._order.Load(this.OrderID);
				}
				return this._order;
			}
			set { this._order = value; }
		}

		private Request _request;
		public Request Request
		{
			get
			{
				if (this._request == null)
				{
					this._request = new Request(this.phreezer);
					this._request.Load(this.RequestID);
				}
				return this._request;
			}
			set { this._request = value; }
		}


		/* ~~~ SETS ~~~ */

		/// <summary>
		/// Returns a collection of ExportLog objects
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns>ExportLogs</returns>
		protected ExportLogs GetExportLogs(ExportLogCriteria criteria)
		{
			//criteria.OriginatorId = this.Id;
			ExportLogs exportLogs = new ExportLogs(this.phreezer);
			exportLogs.Query(criteria);
			return exportLogs;
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
			return "delete from `export_log` where el_id = '" + Id.ToString() + "'";
		}

		/// <summary>
		/// reads the column values from a datareader and populates the object properties
		/// </summary>
		/// <param name="reader"></param>
		public override void Load(MySqlDataReader reader)
		{
			this.Id = Preparer.SafeInt(reader["el_id"]);
			this.AccountID = Preparer.SafeInt(reader["a_id"]);
			this.OrderID = Preparer.SafeInt(reader["o_id"]);
			this.RequestID = Preparer.SafeInt(reader["r_id"]);
			this.ExportFormat = Preparer.SafeString(reader["export_format"]);
			this.Created = Preparer.SafeDateTime(reader["el_created"]);
			this.Modified = Preparer.SafeDateTime(reader["el_modified"]);

			this.OnLoad(reader);
		}

	}
}