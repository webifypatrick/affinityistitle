using System;
using MySql.Data.MySqlClient;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// This code is automatically generated from the Location schema and
	/// should not be edited if at all possible.  All customizations and 
	/// business logic should be added to the partial class Location.cs
	/// </summary>
	public partial class Location : Loadable
	{


		public Location(Phreezer phreezer) : base(phreezer) { }
		public Location(Phreezer phreezer, MySqlDataReader reader) : base(phreezer,reader) { }

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

		/* ~~~ CONSTRAINTS ~~~ */


		/* ~~~ SETS ~~~ */


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
			return "select * from `location` l where l.l_code = '" + pk.ToString() + "'";
		}

		/// <summary>
		/// Returns an SQL statement to update this object in the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetUpdateSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("update `location` set");
			sb.Append("  l_description = '" + Preparer.Escape(this.Description) + "'");
			sb.Append(" where l_code = '" + Preparer.Escape(this.Code) + "'");
			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to insert this object into the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetInsertSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("insert into `location` (");
			sb.Append("  l_code");
			sb.Append(" ,l_description");
			sb.Append(" ) values (");
			sb.Append("  '" + Preparer.Escape(this.Code) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Description) + "'");
			sb.Append(" )");

			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to delete this object from the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetDeleteSql()
		{
			return "delete from `location` where l_code = '" + Code.ToString() + "'";
		}

		/// <summary>
		/// reads the column values from a datareader and populates the object properties
		/// </summary>
		/// <param name="reader"></param>
		public override void Load(MySqlDataReader reader)
		{
		   this.Code = Preparer.SafeString(reader["l_code"]);
		   this.Description = Preparer.SafeString(reader["l_description"]);

			this.OnLoad(reader);
		}

	}
}