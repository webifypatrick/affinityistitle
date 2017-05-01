using System;
using MySql.Data.MySqlClient;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// This code is automatically generated from the Zipcode schema and
	/// should not be edited if at all possible.  All customizations and 
	/// business logic should be added to the partial class Zipcode.cs
	/// </summary>
	public partial class Zipcode : Loadable
	{


		public Zipcode(Phreezer phreezer) : base(phreezer) { }
		public Zipcode(Phreezer phreezer, MySqlDataReader reader) : base(phreezer,reader) { }

		/* ~~~ PUBLIC PROPERTIES ~~~ */


		private string _zip = "";
		public string Zip
		{
			get { return this._zip; }
			set { this._zip = value; }
		}

		private string _city = "";
		public string City
		{
			get { return this._city; }
			set { this._city = value; }
		}

		private string _state = "";
		public string State
		{
			get { return this._state; }
			set { this._state = value; }
		}

        private string _county = "";
        public string County
        {
            get { return this._county; }
            set { this._county = value; }
        }

        private string _fipsCode = "";
        public string FipsCode
        {
            get { return this._fipsCode; }
            set { this._fipsCode = value; }
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
		   this.Zip = (string)key;
		}

		/// <summary>
		/// Returns a SQL statement to select this object from the DB
		/// </summary>
		/// <param name="primaryKey"></param>
		/// <returns></returns>
		protected override string GetSelectSql(object pk)
		{
			return "select * from `zipcode` z  where z.Zip = '" + pk.ToString() + "'";
		}

		/// <summary>
		/// Returns an SQL statement to update this object in the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetUpdateSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("update `zipcode` set");
			sb.Append("  City = '" + Preparer.Escape(this.City) + "'");
			sb.Append(" ,State = '" + Preparer.Escape(this.State) + "'");
            sb.Append(" ,County = '" + Preparer.Escape(this.County) + "'");
            sb.Append(" ,FipsCode = '" + Preparer.Escape(this.FipsCode) + "'");
            sb.Append(" where Zip = '" + Preparer.Escape(this.Zip) + "'");
			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to insert this object into the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetInsertSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("insert into `zipcode` (");
			sb.Append("  Zip");
			sb.Append(" ,City");
			sb.Append(" ,State");
            sb.Append(" ,County");
            sb.Append(" ,FipsCode");
            sb.Append(" ) values (");
			sb.Append("  '" + Preparer.Escape(this.Zip) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.City) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.State) + "'");
            sb.Append(" ,'" + Preparer.Escape(this.County) + "'");
            sb.Append(" ,'" + Preparer.Escape(this.FipsCode) + "'");
            sb.Append(" )");

			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to delete this object from the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetDeleteSql()
		{
			return "delete from `zipcode` where Zip = '" + Zip.ToString() + "'";
		}

		/// <summary>
		/// reads the column values from a datareader and populates the object properties
		/// </summary>
		/// <param name="reader"></param>
		public override void Load(MySqlDataReader reader)
		{
		   this.Zip = Preparer.SafeString(reader["Zip"]);
		   this.City = Preparer.SafeString(reader["City"]);
		   this.State = Preparer.SafeString(reader["State"]);
           this.County = Preparer.SafeString(reader["County"]);
           this.FipsCode = Preparer.SafeString(reader["FipsCode"]);

			this.OnLoad(reader);
		}

	}
}