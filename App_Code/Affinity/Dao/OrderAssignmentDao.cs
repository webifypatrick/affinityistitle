using System;
using MySql.Data.MySqlClient;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// This code is automatically generated from the OrderAssignment schema and
	/// should not be edited if at all possible.  All customizations and 
	/// business logic should be added to the partial class OrderAssignment.cs
	/// </summary>
	public partial class OrderAssignment : Loadable
	{


		public OrderAssignment(Phreezer phreezer) : base(phreezer) { }
		public OrderAssignment(Phreezer phreezer, MySqlDataReader reader) : base(phreezer,reader) { }

		/* ~~~ PUBLIC PROPERTIES ~~~ */


		private int _accountId = 0;
		public int AccountId
		{
			get { return this._accountId; }
			set { this._accountId = value; }
		}

		private int _orderId = 0;
		public int OrderId
		{
			get { return this._orderId; }
			set { this._orderId = value; }
		}

		private string _permissionBit = "";
		public string PermissionBit
		{
			get { return this._permissionBit; }
			set { this._permissionBit = value; }
		}

		/* ~~~ CONSTRAINTS ~~~ */

		private Account _assignedOrder;
		public Account Account
		{
			get
			{
				if (this._assignedOrder == null)
				{
					this._assignedOrder = new Account(this.phreezer);
					this._assignedOrder.Load(this.AccountId);
				}
				return this._assignedOrder;
			}
			set { this._assignedOrder = value; }
		}

		private Order _assignedAccount;
		public Order Order
		{
			get
			{
				if (this._assignedAccount == null)
				{
					this._assignedAccount = new Order(this.phreezer);
					this._assignedAccount.Load(this.OrderId);
				}
				return this._assignedAccount;
			}
			set { this._assignedAccount = value; }
		}


		/* ~~~ SETS ~~~ */


		/* ~~~ CRUD OPERATIONS ~~~ */

		/// <summary>
		/// Assigns a value to the primary key
		/// </summary>
		/// <param name="key"></param>
		protected override void SetPrimaryKey(object key)
		{
		   this.AccountId = (int)key;
		   this.OrderId = (int)key;
		}

		/// <summary>
		/// Returns a SQL statement to select this object from the DB
		/// </summary>
		/// <param name="primaryKey"></param>
		/// <returns></returns>
		protected override string GetSelectSql(object pk)
		{
			return "select * from `order_assignment` oa where oa.oa_account_id = '" + pk.ToString() + "'";
		}

		/// <summary>
		/// Returns an SQL statement to update this object in the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetUpdateSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("update `order_assignment` set");
			sb.Append("  oa_permission_bit = '" + Preparer.Escape(this.PermissionBit) + "'");
			sb.Append(" where oa_account_id = '" + Preparer.Escape(this.AccountId) + "'");
			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to insert this object into the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetInsertSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("insert into `order_assignment` (");
			sb.Append("  oa_account_id");
			sb.Append(" ,oa_order_id");
			sb.Append(" ,oa_permission_bit");
			sb.Append(" ) values (");
			sb.Append("  '" + Preparer.Escape(this.AccountId) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.OrderId) + "'");
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
			return "delete from `order_assignment` where oa_account_id = '" + AccountId.ToString() + "'";
		}

		/// <summary>
		/// reads the column values from a datareader and populates the object properties
		/// </summary>
		/// <param name="reader"></param>
		public override void Load(MySqlDataReader reader)
		{
		   this.AccountId = Preparer.SafeInt(reader["oa_account_id"]);
		   this.OrderId = Preparer.SafeInt(reader["oa_order_id"]);
		   this.PermissionBit = Preparer.SafeString(reader["oa_permission_bit"]);

			this.OnLoad(reader);
		}

	}
}