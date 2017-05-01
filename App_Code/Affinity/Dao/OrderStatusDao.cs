using System;
using MySql.Data.MySqlClient;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// This code is automatically generated from the OrderStatus schema and
	/// should not be edited if at all possible.  All customizations and 
	/// business logic should be added to the partial class OrderStatus.cs
	/// </summary>
	public partial class OrderStatus : Loadable
	{


		public OrderStatus(Phreezer phreezer) : base(phreezer) { }
		public OrderStatus(Phreezer phreezer, MySqlDataReader reader) : base(phreezer,reader) { }

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

		private bool _internalExternal = false;
		public bool InternalExternal
		{
			get { return this._internalExternal; }
			set { this._internalExternal = value; }
		}

		private bool _isClosed = false;
		public bool IsClosed
		{
			get { return this._isClosed; }
			set { this._isClosed = value; }
		}

		/* ~~~ CONSTRAINTS ~~~ */


		/* ~~~ SETS ~~~ */

		/// <summary>
		/// Returns a collection of Order objects
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns>Orders</returns>
		public Orders GetCustomerStatuss(OrderCriteria criteria)
		{
			criteria.CustomerStatusCode = this.Code;
			Orders customerStatuss = new Orders(this.phreezer);
			customerStatuss.Query(criteria);
			return customerStatuss;
		}

		/// <summary>
		/// Returns a collection of Order objects
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns>Orders</returns>
		public Orders GetInternalStatuss(OrderCriteria criteria)
		{
			criteria.InternalStatusCode = this.Code;
			Orders internalStatuss = new Orders(this.phreezer);
			internalStatuss.Query(criteria);
			return internalStatuss;
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
			return "select * from `order_status` os where os.os_code = '" + pk.ToString() + "'";
		}

		/// <summary>
		/// Returns an SQL statement to update this object in the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetUpdateSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("update `order_status` set");
			sb.Append("  os_description = '" + Preparer.Escape(this.Description) + "'");
			sb.Append(" ,os_permission_bit = '" + Preparer.Escape(this.PermissionBit) + "'");
			sb.Append(" ,os_internal_external = '" + Preparer.Escape(this.InternalExternal) + "'");
			sb.Append(" ,os_is_closed = '" + Preparer.Escape(this.IsClosed) + "'");
			sb.Append(" where os_code = '" + Preparer.Escape(this.Code) + "'");
			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to insert this object into the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetInsertSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("insert into `order_status` (");
			sb.Append("  os_code");
			sb.Append(" ,os_description");
			sb.Append(" ,os_permission_bit");
			sb.Append(" ,os_internal_external");
			sb.Append(" ,os_is_closed");
			sb.Append(" ) values (");
			sb.Append("  '" + Preparer.Escape(this.Code) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Description) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.PermissionBit) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.InternalExternal) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.IsClosed) + "'");
			sb.Append(" )");

			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to delete this object from the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetDeleteSql()
		{
			return "delete from `order_status` where os_code = '" + Code.ToString() + "'";
		}

		/// <summary>
		/// reads the column values from a datareader and populates the object properties
		/// </summary>
		/// <param name="reader"></param>
		public override void Load(MySqlDataReader reader)
		{
		   this.Code = Preparer.SafeString(reader["os_code"]);
		   this.Description = Preparer.SafeString(reader["os_description"]);
		   this.PermissionBit = Preparer.SafeInt(reader["os_permission_bit"]);
		   this.InternalExternal = Preparer.SafeBool(reader["os_internal_external"]);
		   this.IsClosed = Preparer.SafeBool(reader["os_is_closed"]);

			this.OnLoad(reader);
		}

	}
}