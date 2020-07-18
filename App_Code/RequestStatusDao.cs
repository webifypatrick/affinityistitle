using System;
using MySql.Data.MySqlClient;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// This code is automatically generated from the RequestStatus schema and
	/// should not be edited if at all possible.  All customizations and 
	/// business logic should be added to the partial class RequestStatus.cs
	/// </summary>
	public partial class RequestStatus : Loadable
	{


		public RequestStatus(Phreezer phreezer) : base(phreezer) { }
		public RequestStatus(Phreezer phreezer, MySqlDataReader reader) : base(phreezer,reader) { }

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

		private int _permissionBit = 0;
		public int PermissionBit
		{
			get { return this._permissionBit; }
			set { this._permissionBit = value; }
		}

		/* ~~~ CONSTRAINTS ~~~ */


		/* ~~~ SETS ~~~ */

		/// <summary>
		/// Returns a collection of Request objects
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns>Requests</returns>
		public Requests GetStatusRequests(RequestCriteria criteria)
		{
			criteria.StatusCode = this.Code;
			Requests statusRequests = new Requests(this.phreezer);
			statusRequests.Query(criteria);
			return statusRequests;
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
			return "select * from `request_status` rs where rs.rs_code = '" + pk.ToString() + "'";
		}

		/// <summary>
		/// Returns an SQL statement to update this object in the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetUpdateSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("update `request_status` set");
			sb.Append("  rs_description = '" + Preparer.Escape(this.Description) + "'");
			sb.Append(" ,rs_permission_bit = '" + Preparer.Escape(this.PermissionBit) + "'");
			sb.Append(" where rs_code = '" + Preparer.Escape(this.Code) + "'");
			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to insert this object into the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetInsertSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("insert into `request_status` (");
			sb.Append("  rs_code");
			sb.Append(" ,rs_description");
			sb.Append(" ,rs_permission_bit");
			sb.Append(" ) values (");
			sb.Append("  '" + Preparer.Escape(this.Code) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Description) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.PermissionBit) + "'");
			sb.Append(" )");

			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to delete this object from the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetDeleteSql()
		{
			return "delete from `request_status` where rs_code = '" + Code.ToString() + "'";
		}

		/// <summary>
		/// reads the column values from a datareader and populates the object properties
		/// </summary>
		/// <param name="reader"></param>
		public override void Load(MySqlDataReader reader)
		{
		   this.Code = Preparer.SafeString(reader["rs_code"]);
		   this.Description = Preparer.SafeString(reader["rs_description"]);
		   this.PermissionBit = Preparer.SafeInt(reader["rs_permission_bit"]);

			this.OnLoad(reader);
		}

	}
}