using System;
using MySql.Data.MySqlClient;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// This code is automatically generated from the RequestType schema and
	/// should not be edited if at all possible.  All customizations and 
	/// business logic should be added to the partial class RequestType.cs
	/// </summary>
	public partial class RequestType : Loadable
	{


		public RequestType(Phreezer phreezer) : base(phreezer) { }
		public RequestType(Phreezer phreezer, MySqlDataReader reader) : base(phreezer,reader) { }

		/* ~~~ PUBLIC PROPERTIES ~~~ */


		private string _code = "";
		public string Code
		{
			get { return this._code; }
			set { this._code = value; }
		}

		private string _description = "";
		public string Description
		{
			get { return this._description; }
			set { this._description = value; }
		}

		private string _definition = "";
		public string Definition
		{
			get { return this._definition; }
			set { this._definition = value; }
		}

		private string _exportFormats = "";
		public string ExportFormats
		{
			get { return this._exportFormats; }
			set { this._exportFormats = value; }
		}

		private bool _isActive = false;
		public bool IsActive
		{
			get { return this._isActive; }
			set { this._isActive = value; }
		}

		/* ~~~ CONSTRAINTS ~~~ */


		/* ~~~ SETS ~~~ */

		/// <summary>
		/// Returns a collection of Request objects
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns>Requests</returns>
		public Requests GetTypeRequests(RequestCriteria criteria)
		{
			criteria.RequestTypeCode = this.Code;
			Requests typeRequests = new Requests(this.phreezer);
			typeRequests.Query(criteria);
			return typeRequests;
		}


		/* ~~~ CRUD OPERATIONS ~~~ */

		/// <summary>
		/// Assigns a value to the primary key
		/// </summary>
		/// <param name="key"></param>
		protected override void SetPrimaryKey(object key)
		{
		   this.Code = (string)key;
		}

		/// <summary>
		/// Returns a SQL statement to select this object from the DB
		/// </summary>
		/// <param name="primaryKey"></param>
		/// <returns></returns>
		protected override string GetSelectSql(object pk)
		{
			return "select * from `request_type` rt where rt.rt_code = '" + pk.ToString() + "'";
		}

		/// <summary>
		/// Returns an SQL statement to update this object in the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetUpdateSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("update `request_type` set");
			sb.Append("  rt_description = '" + Preparer.Escape(this.Description) + "'");
			sb.Append(" ,rt_export_formats = '" + Preparer.Escape(this.ExportFormats) + "'");
			sb.Append(" ,rt_definition = '" + Preparer.Escape(this.Definition) + "'");
			sb.Append(" ,rt_is_active = '" + Preparer.Escape(this.IsActive) + "'");
			sb.Append(" where rt_code = '" + Preparer.Escape(this.Code) + "'");
			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to insert this object into the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetInsertSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("insert into `request_type` (");
			sb.Append("  rt_code");
			sb.Append(" ,rt_description");
			sb.Append(" ,rt_export_formats");
			sb.Append(" ,rt_definition");
			sb.Append(" ,rt_is_active");
			sb.Append(" ) values (");
			sb.Append("  '" + Preparer.Escape(this.Code) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Description) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.ExportFormats) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Definition) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.IsActive) + "'");
			sb.Append(" )");

			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to delete this object from the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetDeleteSql()
		{
			return "delete from `request_type` where rt_code = '" + Code.ToString() + "'";
		}

		/// <summary>
		/// reads the column values from a datareader and populates the object properties
		/// </summary>
		/// <param name="reader"></param>
		public override void Load(MySqlDataReader reader)
		{
		   this.Code = Preparer.SafeString(reader["rt_code"]);
		   this.Description = Preparer.SafeString(reader["rt_description"]);
		   this.ExportFormats = Preparer.SafeString(reader["rt_export_formats"]);
		   this.Definition = Preparer.SafeString(reader["rt_definition"]);
		   this.IsActive = Preparer.SafeBool(reader["rt_is_active"]);

			this.OnLoad(reader);
		}

	}
}