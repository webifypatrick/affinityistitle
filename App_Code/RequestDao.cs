using System;
using System.Xml.Serialization;
using MySql.Data.MySqlClient;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// This code is automatically generated from the Request schema and
	/// should not be edited if at all possible.  All customizations and 
	/// business logic should be added to the partial class Request.cs
	/// </summary>
	[Serializable]
	public partial class Request : Loadable
	{

		public Request(Phreezer phreezer) : base(phreezer) { }
		public Request(Phreezer phreezer, MySqlDataReader reader) : base(phreezer, reader) { }

		/* ~~~ PUBLIC PROPERTIES ~~~ */


		private int _id = 0;
		public int Id
		{
			get { return this._id; }
			set { this._id = value; }
		}

		private string _requestTypeCode = "";
		public string RequestTypeCode
		{
			get { return this._requestTypeCode; }
			set { this._requestTypeCode = value; }
		}

		private int _orderId = 0;
		public int OrderId
		{
			get { return this._orderId; }
			set { this._orderId = value; }
		}

		private int _originatorId = 0;
		public int OriginatorId
		{
			get { return this._originatorId; }
			set { this._originatorId = value; }
		}

		private DateTime _created = DateTime.Now;
		public DateTime Created
		{
			get { return this._created; }
			set { this._created = value; }
		}

		private string _statusCode = "";
		public string StatusCode
		{
			get { return this._statusCode; }
			set { this._statusCode = value; }
		}

		private string _xml = "";
		public string Xml
		{
			get { return this._xml; }
			set { this._xml = value; }
		}

		private bool _isCurrent = false;
		public bool IsCurrent
		{
			get { return this._isCurrent; }
			set { this._isCurrent = value; }
		}

		private string _note = "";
		public string Note
		{
			get { return this._note; }
			set { this._note = value; }
		}

		/* ~~~ CONSTRAINTS ~~~ */

		private Account _originator;
		[XmlIgnore]
		public Account Account
		{
			get
			{
				if (this._originator == null)
				{
					this._originator = new Account(this.phreezer);
					this._originator.Load(this.OriginatorId);
				}
				return this._originator;
			}
			set { this._originator = value; }
		}

		private Order _orderRequest;
		public Order Order
		{
			get
			{
				if (this._orderRequest == null)
				{
					this._orderRequest = new Order(this.phreezer);
					this._orderRequest.Load(this.OrderId);
				}
				return this._orderRequest;
			}
			set { this._orderRequest = value; }
		}

		private RequestStatus _statusRequest;
		[XmlIgnore]
		public RequestStatus RequestStatus
		{
			get
			{
				if (this._statusRequest == null)
				{
					this._statusRequest = new RequestStatus(this.phreezer);
					this._statusRequest.Load(this.StatusCode);
				}
				return this._statusRequest;
			}
			set { this._statusRequest = value; }
		}

		private RequestType _typeRequest;
		[XmlIgnore]
		public RequestType RequestType
		{
			get
			{
				if (this._typeRequest == null)
				{
					this._typeRequest = new RequestType(this.phreezer);
					this._typeRequest.Load(this.RequestTypeCode);
				}
				return this._typeRequest;
			}
			set { this._typeRequest = value; }
		}


		/* ~~~ SETS ~~~ */

		/// <summary>
		/// Returns a collection of Attachment objects
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns>Attachments</returns>
		public Attachments GetAttachmentRequests(AttachmentCriteria criteria)
		{
			criteria.RequestId = this.Id;
			Attachments attachmentRequests = new Attachments(this.phreezer);
			attachmentRequests.Query(criteria);
			return attachmentRequests;
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
		/// Returns a SQL statement to select this object from the DB
		/// </summary>
		/// <param name="primaryKey"></param>
		/// <returns></returns>
		protected override string GetSelectSql(object pk)
		{
			return "select * from `request` r where r.r_id = '" + pk.ToString() + "'";
		}

		/// <summary>
		/// Returns an SQL statement to update this object in the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetUpdateSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("update `request` set");
			sb.Append("  r_request_type_code = '" + Preparer.Escape(this.RequestTypeCode) + "'");
			sb.Append(" ,r_order_id = '" + Preparer.Escape(this.OrderId) + "'");
			sb.Append(" ,r_originator_id = '" + Preparer.Escape(this.OriginatorId) + "'");
			sb.Append(" ,r_created = '" + Preparer.Escape(this.Created) + "'");
			sb.Append(" ,r_status_code = '" + Preparer.Escape(this.StatusCode) + "'");
			sb.Append(" ,r_xml = '" + Preparer.Escape(this.Xml) + "'");
			sb.Append(" ,r_is_current = '" + Preparer.Escape(this.IsCurrent) + "'");
			sb.Append(" ,r_note = '" + Preparer.Escape(this.Note) + "'");
			sb.Append(" where r_id = '" + Preparer.Escape(this.Id) + "'");
			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to insert this object into the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetInsertSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("insert into `request` (");
			sb.Append("  r_request_type_code");
			sb.Append(" ,r_order_id");
			sb.Append(" ,r_originator_id");
			sb.Append(" ,r_created");
			sb.Append(" ,r_status_code");
			sb.Append(" ,r_xml");
			sb.Append(" ,r_is_current");
			sb.Append(" ,r_note");
			sb.Append(" ) values (");
			sb.Append("  '" + Preparer.Escape(this.RequestTypeCode) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.OrderId) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.OriginatorId) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Created) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.StatusCode) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Xml) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.IsCurrent) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Note) + "'");
			sb.Append(" )");

			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to delete this object from the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetDeleteSql()
		{
			return "delete from `request` where r_id = '" + Id.ToString() + "'";
		}

		/// <summary>
		/// reads the column values from a datareader and populates the object properties
		/// </summary>
		/// <param name="reader"></param>
		public override void Load(MySqlDataReader reader)
		{
			this.Id = Preparer.SafeInt(reader["r_id"]);
			this.RequestTypeCode = Preparer.SafeString(reader["r_request_type_code"]);
			this.OrderId = Preparer.SafeInt(reader["r_order_id"]);
			this.OriginatorId = Preparer.SafeInt(reader["r_originator_id"]);
			this.Created = Preparer.SafeDateTime(reader["r_created"]);
			this.StatusCode = Preparer.SafeString(reader["r_status_code"]);
			this.Xml = Preparer.SafeString(reader["r_xml"]);
			this.IsCurrent = Preparer.SafeBool(reader["r_is_current"]);
			this.Note = Preparer.SafeString(reader["r_note"]);

			this.OnLoad(reader);
		}

	}
}