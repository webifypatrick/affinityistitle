using System;
using MySql.Data.MySqlClient;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// This code is automatically generated from the TitleFee schema and
	/// should not be edited if at all possible.  All customizations and 
	/// business logic should be added to the partial class TitleFee.cs
	/// </summary>
	public partial class TitleFee : Loadable
	{


		public TitleFee(Phreezer phreezer) : base(phreezer) { }
        public TitleFee(Phreezer phreezer, MySqlDataReader reader) : base(phreezer, reader) { }

		/* ~~~ PUBLIC PROPERTIES ~~~ */


		private string _id = null;
		public string Id
		{
            get { return this._id; }
            set { this._id = value; }
		}

        private string _type = null;
        public string FeeType
        {
            get { return this._type; }
            set { this._type = value; }
        }

        private string _name = null;
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        private string _state = null;
        public string State
        {
            get { return this._state; }
            set { this._state = value; }
        }

        private decimal _fee = 0;
        public decimal Fee
        {
            get { return this._fee; }
            set { this._fee = value; }
        }

        private DateTime _modified = new DateTime();
        public DateTime Modified
        {
            get { return this._modified; }
            set { this._modified = value; }
        }

        private DateTime _created = new DateTime();
        public DateTime Created
        {
            get { return this._created; }
            set { this._created = value; }
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
		   this.Id = (string)key;
		}

		/// <summary>
		/// Returns a SQL statement to select this object from the DB
		/// </summary>
		/// <param name="primaryKey"></param>
		/// <returns></returns>
		protected override string GetSelectSql(object pk)
		{
			return "select * from `title_fees` t where t.tf_id = '" + pk.ToString() + "'";
		}

		/// <summary>
		/// Returns an SQL statement to update this object in the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetUpdateSql()
		{
			StringBuilder sb = new StringBuilder();
      sb.Append("update `title_fees` set");
      sb.Append("  tf_type = '" + Preparer.Escape(this.FeeType) + "',");
      sb.Append("  tf_name = '" + Preparer.Escape(this.Name) + "',");
      sb.Append("  tf_fee = '" + Preparer.Escape(this.Fee) + "',");
      sb.Append("  tf_modified = '" + Preparer.Escape(this.Modified) + "'");
      sb.Append(" where tf_id = '" + Preparer.Escape(this.Id) + "'");
			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to insert this object into the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetInsertSql()
		{
			StringBuilder sb = new StringBuilder();
      sb.Append("insert into `title_fees` (");
      sb.Append("  tf_type");
      sb.Append(" ,tf_name");
      sb.Append(" ,tf_fee");
      sb.Append(" ,tf_modified");
      sb.Append(" ,tf_created");
      sb.Append(" ) values (");
			sb.Append("  '" + Preparer.Escape(this.FeeType) + "'");
      sb.Append(" ,'" + Preparer.Escape(this.Name) + "'");
      sb.Append(" ,'" + Preparer.Escape(this.Fee) + "'");
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
			return "delete from `title_fees` where tf_id = '" + Id.ToString() + "'";
		}

		/// <summary>
		/// reads the column values from a datareader and populates the object properties
		/// </summary>
		/// <param name="reader"></param>
		public override void Load(MySqlDataReader reader)
		{
		   this.Id = Preparer.SafeString(reader["tf_id"]);
       this.FeeType = Preparer.SafeString(reader["tf_type"]);
       this.Name = Preparer.SafeString(reader["tf_name"]);
       this.Fee = Preparer.SafeDecimal(reader["tf_fee"]);
       this.Modified = Preparer.SafeDateTime(reader["tf_modified"]);
       this.Created = Preparer.SafeDateTime(reader["tf_created"]);

			this.OnLoad(reader);
		}

	}
}