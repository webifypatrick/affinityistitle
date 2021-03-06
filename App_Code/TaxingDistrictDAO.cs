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
	public partial class TaxingDistrict : Loadable
	{


		public TaxingDistrict(Phreezer phreezer) : base(phreezer) { }
		public TaxingDistrict(Phreezer phreezer, MySqlDataReader reader) : base(phreezer,reader) { }

		/* ~~~ PUBLIC PROPERTIES ~~~ */

		private string _taxing_district = "";
		public string Taxing_district
		{
			get { return this._taxing_district; }
			set { this._taxing_district = value; }
		}

		private string _type = "";
		public string Type
		{
			get { return this._type; }
			set { this._type = value; }
		}

		private string _county = "";
		public string County
		{
			get { return this._county; }
			set { this._county = value; }
		}

		private string _liable_party = "";
		public string Liable_party
		{
			get { return this._liable_party; }
			set { this._liable_party = value; }
		}

		private string _amount = "";
		public string Amount
		{
			get { return this._amount; }
			set { this._amount = value; }
		}

		private string _where = "";
		public string Where
		{
			get { return this._where; }
			set { this._where = value; }
		}

		private string _address = "";
		public string Address
		{
			get { return this._address; }
			set { this._address = value; }
		}

		private string _csz = "";
		public string CSZ
		{
			get { return this._csz; }
			set { this._csz = value; }
		}

		private string _phone = "";
		public string Phone
		{
			get { return this._phone; }
			set { this._phone = value; }
		}

		private string _website = "";
		public string Website
		{
			get { return this._website; }
			set { this._website = value; }
		}

		private string _stamp_exempt = "";
		public string Stamp_exempt
		{
			get { return this._stamp_exempt; }
			set { this._stamp_exempt = value; }
		}

		private string _notes = "";
		public string Notes
		{
			get { return this._notes; }
			set { this._notes = value; }
		}

		private int _stamp_required = 0;
		public int Stamp_required
		{
			get { return this._stamp_required; }
			set { this._stamp_required = value; }
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
		   this.Taxing_district = key.ToString();
		}

		/// <summary>
		/// Returns a SQL statement to select this object from the DB
		/// </summary>
		/// <param name="primaryKey"></param>
		/// <returns></returns>
		protected override string GetSelectSql(object pk)
		{
			return "select * from `taxing_district` t where t.taxing_district = '" + pk.ToString() + "'";
		}

		/// <summary>
		/// Returns an SQL statement to update this object in the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetUpdateSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("update `taxing_district` set");
			sb.Append("  type = '" + Preparer.Escape(this.Type) + "'");
			sb.Append(" ,county = '" + Preparer.Escape(this.County) + "'");
			sb.Append(" ,liable_party = '" + Preparer.Escape(this.Liable_party) + "'");
			sb.Append(" ,amount = '" + Preparer.Escape(this.Amount) + "'");
			sb.Append(" ,where = '" + Preparer.Escape(this.Where) + "'");
			sb.Append(" ,address = '" + Preparer.Escape(this.Address) + "'");
			sb.Append(" ,csz = '" + Preparer.Escape(this.CSZ) + "'");
			sb.Append(" ,phone = '" + Preparer.Escape(this.Phone) + "'");
			sb.Append(" ,website = '" + Preparer.Escape(this.Website) + "'");
			sb.Append(" ,stamp_exempt = '" + Preparer.Escape(this.Stamp_exempt) + "'");
			sb.Append(" ,notes = '" + Preparer.Escape(this.Notes) + "'");
			sb.Append(" ,stamp_required = '" + Preparer.Escape(this.Stamp_required) + "'");
			sb.Append(" where taxing_district = '" + Preparer.Escape(this.Taxing_district) + "'");
			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to insert this object into the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetInsertSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("insert into `taxing_district` (");
			sb.Append("  taxing_district");
			sb.Append(" ,type");
			sb.Append(" ,county");
			sb.Append(" ,liable_party");
			sb.Append(" ,amount");
			sb.Append(" ,where");
			sb.Append(" ,address");
			sb.Append(" ,csz");
			sb.Append(" ,phone");
			sb.Append(" ,website");
			sb.Append(" ,stamp_exempt");
			sb.Append(" ,notes");
			sb.Append(" ,stamp_required");
			sb.Append(" ) values (");
			sb.Append("  '" + Preparer.Escape(this.Taxing_district) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Type) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.County) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Liable_party) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Amount) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Where) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Address) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.CSZ) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Phone) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Website) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Stamp_exempt) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Notes) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Stamp_required) + "'");
			sb.Append(" )");

			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to delete this object from the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetDeleteSql()
		{
			return "delete from `taxing_district` where taxing_district = '" + Taxing_district.ToString() + "'";
		}

		/// <summary>
		/// reads the column values from a datareader and populates the object properties
		/// </summary>
		/// <param name="reader"></param>
		public override void Load(MySqlDataReader reader)
		{
			if(reader.HasRows)
			{
				this.Taxing_district = Preparer.SafeString(reader["taxing_district"]);
				this.Type = Preparer.SafeString(reader["type"]);
				this.County = Preparer.SafeString(reader["county"]);
				this.Liable_party = Preparer.SafeString(reader["liable_party"]);
				this.Amount = Preparer.SafeString(reader["amount"]);
				this.Where = Preparer.SafeString(reader["where"]);
				this.Address = Preparer.SafeString(reader["address"]);
				this.CSZ = Preparer.SafeString(reader["csz"]);
				this.Phone = Preparer.SafeString(reader["phone"]);
				this.Website = Preparer.SafeString(reader["website"]);
				this.Stamp_exempt = Preparer.SafeString(reader["stamp_exempt"]);
				this.Notes = Preparer.SafeString(reader["notes"]);
				this.Stamp_required = Preparer.SafeInt(reader["stamp_required"]);

				this.OnLoad(reader);
			}
		}

	}
}