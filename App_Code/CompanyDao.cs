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
	public partial class Company : Loadable
	{


		public Company(Phreezer phreezer) : base(phreezer) { }
		public Company(Phreezer phreezer, MySqlDataReader reader) : base(phreezer,reader) { }

		/* ~~~ PUBLIC PROPERTIES ~~~ */


		private int _id = 0;
		public int Id
		{
			get { return this._id; }
			set { this._id = value; }
		}

		private string _name = "";
		public string Name
		{
			get { return this._name; }
			set { this._name = value; }
		}

		private DateTime _created = DateTime.Now;
		public DateTime Created
		{
			get { return this._created; }
			set { this._created = value; }
		}

		/* ~~~ CONSTRAINTS ~~~ */


		/* ~~~ SETS ~~~ */

		/// <summary>
		/// Returns a collection of Account objects
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns>Accounts</returns>
		public Accounts GetAccountCompanys(AccountCriteria criteria)
		{
			criteria.CompanyId = this.Id;
			Accounts accountCompanys = new Accounts(this.phreezer);
			accountCompanys.Query(criteria);
			return accountCompanys;
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
			return "select * from `company` c where c.c_id = '" + pk.ToString() + "'";
		}

		/// <summary>
		/// Returns an SQL statement to update this object in the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetUpdateSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("update `company` set");
			sb.Append("  c_name = '" + Preparer.Escape(this.Name) + "'");
			sb.Append(" ,c_created = '" + Preparer.Escape(this.Created) + "'");
			sb.Append(" where c_id = '" + Preparer.Escape(this.Id) + "'");
			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to insert this object into the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetInsertSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("insert into `company` (");
			sb.Append("  c_name");
			sb.Append(" ,c_created");
			sb.Append(" ) values (");
			sb.Append("  '" + Preparer.Escape(this.Name) + "'");
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
			return "delete from `company` where c_id = '" + Id.ToString() + "'";
		}

		/// <summary>
		/// reads the column values from a datareader and populates the object properties
		/// </summary>
		/// <param name="reader"></param>
		public override void Load(MySqlDataReader reader)
		{
		   this.Id = Preparer.SafeInt(reader["c_id"]);
		   this.Name = Preparer.SafeString(reader["c_name"]);
		   this.Created = Preparer.SafeDateTime(reader["c_created"]);

			this.OnLoad(reader);
		}

	}
}