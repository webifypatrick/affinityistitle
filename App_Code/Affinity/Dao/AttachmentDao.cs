using System;
using MySql.Data.MySqlClient;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// This code is automatically generated from the Attachment schema and
	/// should not be edited if at all possible.  All customizations and 
	/// business logic should be added to the partial class Attachment.cs
	/// </summary>
	public partial class Attachment : Loadable
	{


		public Attachment(Phreezer phreezer) : base(phreezer) { }
		public Attachment(Phreezer phreezer, MySqlDataReader reader) : base(phreezer,reader) { }

		/* ~~~ PUBLIC PROPERTIES ~~~ */


		private int _id = 0;
		public int Id
		{
			get { return this._id; }
			set { this._id = value; }
		}

		private int _requestId = 0;
		public int RequestId
		{
			get { return this._requestId; }
			set { this._requestId = value; }
		}

		private string _name = "";
		public string Name
		{
			get { return this._name; }
			set { this._name = value; }
		}

		private string _mimeType = "";
		public string MimeType
		{
			get { return this._mimeType; }
			set { this._mimeType = value; }
		}

		private int _sizeKb = 0;
		public int SizeKb
		{
			get { return this._sizeKb; }
			set { this._sizeKb = value; }
		}

		private DateTime _created = DateTime.Now;
		public DateTime Created
		{
			get { return this._created; }
			set { this._created = value; }
		}

		private string _filepath = "";
		public string Filepath
		{
			get { return this._filepath; }
			set { this._filepath = value; }
		}

		private string _purposeCode = "";
		public string PurposeCode
		{
			get { return this._purposeCode; }
			set { this._purposeCode = value; }
		}

		/* ~~~ CONSTRAINTS ~~~ */

		private AttachmentPurpose _attachmentPurpose;
		public AttachmentPurpose AttachmentPurpose
		{
			get
			{
				if (this._attachmentPurpose == null)
				{
					this._attachmentPurpose = new AttachmentPurpose(this.phreezer);
					this._attachmentPurpose.Load(this.PurposeCode);
				}
				return this._attachmentPurpose;
			}
			set { this._attachmentPurpose = value; }
		}

		private Request _attachmentRequest;
		public Request Request
		{
			get
			{
				if (this._attachmentRequest == null)
				{
					this._attachmentRequest = new Request(this.phreezer);
					this._attachmentRequest.Load(this.RequestId);
				}
				return this._attachmentRequest;
			}
			set { this._attachmentRequest = value; }
		}


		/* ~~~ SETS ~~~ */


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
			return "select * from `attachment` att"
				+ " inner join `request` r on att.att_request_id = r.r_id"
				+ " inner join `order` o on r.r_order_id = o.o_id"
				+ " where att.att_id = '" + pk.ToString() + "'";
		}

		/// <summary>
		/// Returns an SQL statement to update this object in the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetUpdateSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("update `attachment` set");
			sb.Append("  att_request_id = '" + Preparer.Escape(this.RequestId) + "'");
			sb.Append(" ,att_name = '" + Preparer.Escape(this.Name) + "'");
			sb.Append(" ,att_mime_type = '" + Preparer.Escape(this.MimeType) + "'");
			sb.Append(" ,att_size_kb = '" + Preparer.Escape(this.SizeKb) + "'");
			sb.Append(" ,att_created = '" + Preparer.Escape(this.Created) + "'");
			sb.Append(" ,att_filepath = '" + Preparer.Escape(this.Filepath) + "'");
			sb.Append(" ,att_purpose_code = '" + Preparer.Escape(this.PurposeCode) + "'");
			sb.Append(" where att_id = '" + Preparer.Escape(this.Id) + "'");
			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to insert this object into the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetInsertSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("insert into `attachment` (");
			sb.Append("  att_request_id");
			sb.Append(" ,att_name");
			sb.Append(" ,att_mime_type");
			sb.Append(" ,att_size_kb");
			sb.Append(" ,att_created");
			sb.Append(" ,att_filepath");
			sb.Append(" ,att_purpose_code");
			sb.Append(" ) values (");
			sb.Append("  '" + Preparer.Escape(this.RequestId) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Name) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.MimeType) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.SizeKb) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Created) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Filepath) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.PurposeCode) + "'");
			sb.Append(" )");

			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to delete this object from the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetDeleteSql()
		{
			return "delete from `attachment` where att_id = '" + Id.ToString() + "'";
		}

		/// <summary>
		/// reads the column values from a datareader and populates the object properties
		/// </summary>
		/// <param name="reader"></param>
		public override void Load(MySqlDataReader reader)
		{
		   this.Id = Preparer.SafeInt(reader["att_id"]);
		   this.RequestId = Preparer.SafeInt(reader["att_request_id"]);
		   this.Name = Preparer.SafeString(reader["att_name"]);
		   this.MimeType = Preparer.SafeString(reader["att_mime_type"]);
		   this.SizeKb = Preparer.SafeInt(reader["att_size_kb"]);
		   this.Created = Preparer.SafeDateTime(reader["att_created"]);
		   this.Filepath = Preparer.SafeString(reader["att_filepath"]);
		   this.PurposeCode = Preparer.SafeString(reader["att_purpose_code"]);

			this.OnLoad(reader);
		}

	}
}