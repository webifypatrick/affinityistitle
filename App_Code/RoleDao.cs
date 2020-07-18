using System;
using MySql.Data.MySqlClient;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// This code is automatically generated from the Role schema and
	/// should not be edited if at all possible.  All customizations and 
	/// business logic should be added to the partial class Role.cs
	/// </summary>
	public partial class Role : Loadable
	{


		public Role(Phreezer phreezer) : base(phreezer) { }
		public Role(Phreezer phreezer, MySqlDataReader reader) : base(phreezer,reader) { }

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
		/// Returns a collection of Account objects
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns>Accounts</returns>
		public Accounts GetAccountRoles(AccountCriteria criteria)
		{
			criteria.RoleCode = this.Code;
			Accounts accountRoles = new Accounts(this.phreezer);
			accountRoles.Query(criteria);
			return accountRoles;
		}

		/// <summary>
		/// Returns a collection of Role objects
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns>Roles</returns>
		public Roles GetRoles(RoleCriteria criteria)
		{
			criteria.Code = this.Code;
			Roles roles = new Roles(this.phreezer);
			roles.Query(criteria);
			return roles;
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
			return "select * from `role` r where r.r_code = '" + pk.ToString() + "'";
		}

		/// <summary>
		/// Returns an SQL statement to update this object in the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetUpdateSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("update `role` set");
			sb.Append("  r_description = '" + Preparer.Escape(this.Description) + "'");
			sb.Append(" ,r_permission_bit = '" + Preparer.Escape(this.PermissionBit) + "'");
			sb.Append(" where r_code = '" + Preparer.Escape(this.Code) + "'");
			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to insert this object into the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetInsertSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("insert into `role` (");
			sb.Append("  r_code");
			sb.Append(" ,r_description");
			sb.Append(" ,r_permission_bit");
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
			return "delete from `role` where r_code = '" + Code.ToString() + "'";
		}

		/// <summary>
		/// reads the column values from a datareader and populates the object properties
		/// </summary>
		/// <param name="reader"></param>
		public override void Load(MySqlDataReader reader)
		{
		   this.Code = Preparer.SafeString(reader["r_code"]);
		   this.Description = Preparer.SafeString(reader["r_description"]);
		   this.PermissionBit = Preparer.SafeInt(reader["r_permission_bit"]);

			this.OnLoad(reader);
		}

	}
}