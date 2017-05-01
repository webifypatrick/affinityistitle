using System;
using MySql.Data.MySqlClient;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// This code is automatically generated from the Content schema and
	/// should not be edited if at all possible.  All customizations and 
	/// business logic should be added to the partial class Content.cs
	/// </summary>
	public partial class Content : Loadable
	{


		public Content(Phreezer phreezer) : base(phreezer) { }
		public Content(Phreezer phreezer, MySqlDataReader reader) : base(phreezer,reader) { }

		/* ~~~ PUBLIC PROPERTIES ~~~ */


		private string _code = "";
		public string Code
		{
			get { return this._code; }
			set { this._code = value; }
		}

		private string _metaTitle = "";
		public string MetaTitle
		{
			get { return this._metaTitle; }
			set { this._metaTitle = value; }
		}

		private string _metaKeywords = "";
		public string MetaKeywords
		{
			get { return this._metaKeywords; }
			set { this._metaKeywords = value; }
		}

		private string _metaDescription = "";
		public string MetaDescription
		{
			get { return this._metaDescription; }
			set { this._metaDescription = value; }
		}

		private string _header = "";
		public string Header
		{
			get { return this._header; }
			set { this._header = value; }
		}

		private string _body = "";
		public string Body
		{
			get { return this._body; }
			set { this._body = value; }
		}

		private DateTime _modified = DateTime.Now;
		public DateTime Modified
		{
			get { return this._modified; }
			set { this._modified = value; }
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
			return "select * from `content` ct where ct.ct_code = '" + pk.ToString() + "'";
		}

		/// <summary>
		/// Returns an SQL statement to update this object in the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetUpdateSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("update `content` set");
			sb.Append("  ct_meta_title = '" + Preparer.Escape(this.MetaTitle) + "'");
			sb.Append(" ,ct_meta_keywords = '" + Preparer.Escape(this.MetaKeywords) + "'");
			sb.Append(" ,ct_meta_description = '" + Preparer.Escape(this.MetaDescription) + "'");
			sb.Append(" ,ct_header = '" + Preparer.Escape(this.Header) + "'");
			sb.Append(" ,ct_body = '" + Preparer.Escape(this.Body) + "'");
			sb.Append(" ,ct_modified = '" + Preparer.Escape(this.Modified) + "'");
			sb.Append(" where ct_code = '" + Preparer.Escape(this.Code) + "'");
			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to insert this object into the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetInsertSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("insert into `content` (");
			sb.Append("  ct_code");
			sb.Append(" ,ct_meta_title");
			sb.Append(" ,ct_meta_keywords");
			sb.Append(" ,ct_meta_description");
			sb.Append(" ,ct_header");
			sb.Append(" ,ct_body");
			sb.Append(" ,ct_modified");
			sb.Append(" ) values (");
			sb.Append("  '" + Preparer.Escape(this.Code) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.MetaTitle) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.MetaKeywords) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.MetaDescription) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Header) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Body) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Modified) + "'");
			sb.Append(" )");

			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to delete this object from the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetDeleteSql()
		{
			return "delete from `content` where ct_code = '" + Code.ToString() + "'";
		}

		/// <summary>
		/// reads the column values from a datareader and populates the object properties
		/// </summary>
		/// <param name="reader"></param>
		public override void Load(MySqlDataReader reader)
		{
		   this.Code = Preparer.SafeString(reader["ct_code"]);
		   this.MetaTitle = Preparer.SafeString(reader["ct_meta_title"]);
		   this.MetaKeywords = Preparer.SafeString(reader["ct_meta_keywords"]);
		   this.MetaDescription = Preparer.SafeString(reader["ct_meta_description"]);
		   this.Header = Preparer.SafeString(reader["ct_header"]);
		   this.Body = Preparer.SafeString(reader["ct_body"]);
		   this.Modified = Preparer.SafeDateTime(reader["ct_modified"]);

			this.OnLoad(reader);
		}

	}
}