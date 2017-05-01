using System;
using System.Xml.Serialization;
using MySql.Data.MySqlClient;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// This code is automatically generated from the Order schema and
	/// should not be edited if at all possible.  All customizations and 
	/// business logic should be added to the partial class Order.cs
	/// </summary>
	public partial class Order : Loadable
	{


		public Order(Phreezer phreezer) : base(phreezer) { }
		public Order(Phreezer phreezer, MySqlDataReader reader) : base(phreezer,reader) { }

		/* ~~~ PUBLIC PROPERTIES ~~~ */


		private int _id = 0;
		public int Id
		{
			get { return this._id; }
			set { this._id = value; }
		}

		private string _internalId = "";
		public string InternalId
		{
			get { return this._internalId; }
			set { this._internalId = value; }
		}

		private string _internalATSId = "";
		public string InternalATSId
		{
			get { return this._internalATSId; }
			set { this._internalATSId = value; }
		}

		private string _customerId = "";
		public string CustomerId
		{
			get { return this._customerId; }
			set { this._customerId = value; }
		}

		private string _pin = "";
		public string Pin
		{
			get { return this._pin; }
			set { this._pin = value; }
		}

		private string _additionalPins = "";
		public string AdditionalPins
		{
			get { return this._additionalPins; }
			set { this._additionalPins = value; }
		}

		private string _propertyAddress = "";
		public string PropertyAddress
		{
			get { return this._propertyAddress; }
			set { this._propertyAddress = value; }
		}

		private string _propertyAddress2 = "";
		public string PropertyAddress2
		{
			get { return this._propertyAddress2; }
			set { this._propertyAddress2 = value; }
		}

		private string _propertyCity = "";
		public string PropertyCity
		{
			get { return this._propertyCity; }
			set { this._propertyCity = value; }
		}

		private string _propertyState = "";
		public string PropertyState
		{
			get { return this._propertyState; }
			set { this._propertyState = value; }
		}

		private string _propertyZip = "";
		public string PropertyZip
		{
			get { return this._propertyZip; }
			set { this._propertyZip = value; }
		}

		private string _propertyCounty = "";
		public string PropertyCounty
		{
			get { return this._propertyCounty; }
			set { this._propertyCounty = value; }
		}

		private string _propertyUse = "";
		public string PropertyUse
		{
			get { return this._propertyUse; }
			set { this._propertyUse = value; }
		}

		private string _internalStatusCode = "";
		public string InternalStatusCode
		{
			get { return this._internalStatusCode; }
			set { this._internalStatusCode = value; }
		}

		private string _customerStatusCode = "";
		public string CustomerStatusCode
		{
			get { return this._customerStatusCode; }
			set { this._customerStatusCode = value; }
		}

		private int _originatorId = 0;
		public int OriginatorId
		{
			get { return this._originatorId; }
			set { this._originatorId = value; }
		}

		private DateTime _created = DateTime.Now;
		public DateTime Created
		{
			get { return this._created; }
			set { this._created = value; }
		}

		private DateTime _modified = DateTime.Now;
		public DateTime Modified
		{
			get { return this._modified; }
			set { this._modified = value; }
		}

		private DateTime _closingDate = DateTime.Now;
		public DateTime ClosingDate
		{
			get { return this._closingDate; }
			set { this._closingDate = value; }
		}

		private string _clientName = "";
		public string ClientName
		{
			get { return this._clientName; }
			set { this._clientName = value; }
		}

		private int _orderUploadLogId = 0;
		public int OrderUploadLogId
		{
			get { return this._orderUploadLogId; }
			set { this._orderUploadLogId = value; }
		}

		/* ~~~ CONSTRAINTS ~~~ */

		private Account _accountOrder;
		public Account Account
		{
			get
			{
				if (this._accountOrder == null)
				{
					this._accountOrder = new Account(this.phreezer);
					this._accountOrder.Load(this.OriginatorId);
				}
				return this._accountOrder;
			}
			set { this._accountOrder = value; }
		}

		private OrderStatus _customerStatus;
		[XmlIgnore]
		public OrderStatus CustomerStatus
		{
			get
			{
				if (this._customerStatus == null)
				{
					this._customerStatus = new OrderStatus(this.phreezer);
					this._customerStatus.Load(this.CustomerStatusCode);
				}
				return this._customerStatus;
			}
			set { this._customerStatus = value; }
		}

		private OrderStatus _internalStatus;
		[XmlIgnore]
		public OrderStatus InternalStatus
		{
			get
			{
				if (this._internalStatus == null)
				{
					this._internalStatus = new OrderStatus(this.phreezer);
					this._internalStatus.Load(this.InternalStatusCode);
				}
				return this._internalStatus;
			}
			set { this._internalStatus = value; }
		}


		/* ~~~ SETS ~~~ */

		/// <summary>
		/// Returns a collection of OrderAssignment objects
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns>OrderAssignments</returns>
		public OrderAssignments GetAssignedAccounts(OrderAssignmentCriteria criteria)
		{
			criteria.OrderId = this.Id;
			OrderAssignments assignedAccounts = new OrderAssignments(this.phreezer);
			assignedAccounts.Query(criteria);
			return assignedAccounts;
		}

		/// <summary>
		/// Returns a collection of Request objects
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns>Requests</returns>
		public Requests GetOrderRequests(RequestCriteria criteria)
		{
			criteria.OrderId = this.Id;
			Requests orderRequests = new Requests(this.phreezer);
			orderRequests.Query(criteria);
			return orderRequests;
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
			return "select * from `order` o where o.o_id = '" + pk.ToString() + "'";
		}

		/// <summary>
		/// Returns an SQL statement to update this object in the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetUpdateSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("update `order` set");
			sb.Append("  o_internal_id = '" + Preparer.Escape(this.InternalId) + "'");
			sb.Append(" ,o_internal_ats_id = '" + Preparer.Escape(this.InternalATSId) + "'");
			sb.Append(" ,o_customer_id = '" + Preparer.Escape(this.CustomerId) + "'");
			sb.Append(" ,o_pin = '" + Preparer.Escape(this.Pin) + "'");
			sb.Append(" ,o_additional_pins = '" + Preparer.Escape(this.AdditionalPins) + "'");
			sb.Append(" ,o_property_address = '" + Preparer.Escape(this.PropertyAddress) + "'");
			sb.Append(" ,o_property_address_2 = '" + Preparer.Escape(this.PropertyAddress2) + "'");
			sb.Append(" ,o_property_city = '" + Preparer.Escape(this.PropertyCity) + "'");
			sb.Append(" ,o_property_state = '" + Preparer.Escape(this.PropertyState) + "'");
			sb.Append(" ,o_property_zip = '" + Preparer.Escape(this.PropertyZip) + "'");
			sb.Append(" ,o_property_county = '" + Preparer.Escape(this.PropertyCounty) + "'");
			sb.Append(" ,o_property_use = '" + Preparer.Escape(this.PropertyUse) + "'");
			sb.Append(" ,o_internal_status_code = '" + Preparer.Escape(this.InternalStatusCode) + "'");
			sb.Append(" ,o_customer_status_code = '" + Preparer.Escape(this.CustomerStatusCode) + "'");
			sb.Append(" ,o_originator_id = '" + Preparer.Escape(this.OriginatorId) + "'");
			sb.Append(" ,o_created = '" + Preparer.Escape(this.Created) + "'");
			sb.Append(" ,o_modified = '" + Preparer.Escape(this.Modified) + "'");
			sb.Append(" ,o_closing_date = '" + Preparer.Escape(this.ClosingDate) + "'");
			sb.Append(" ,o_client_name = '" + Preparer.Escape(this.ClientName) + "'");
			sb.Append(" ,o_oul_id = '" + Preparer.Escape(this.OrderUploadLogId) + "'");
			sb.Append(" where o_id = '" + Preparer.Escape(this.Id) + "'");
			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to insert this object into the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetInsertSql()
		{
			string state = this.PropertyState.Trim();
			if(state.Length > 2)
			{
				state = state.Substring(0, 2);
			}
			StringBuilder sb = new StringBuilder();
			sb.Append("insert into `order` (");
			sb.Append("  o_internal_id");
			sb.Append(" ,o_internal_ats_id");
			sb.Append(" ,o_customer_id");
			sb.Append(" ,o_pin");
			sb.Append(" ,o_additional_pins");
			sb.Append(" ,o_property_address");
			sb.Append(" ,o_property_address_2");
			sb.Append(" ,o_property_city");
			sb.Append(" ,o_property_state");
			sb.Append(" ,o_property_zip");
			sb.Append(" ,o_property_county");
			sb.Append(" ,o_property_use");
			sb.Append(" ,o_internal_status_code");
			sb.Append(" ,o_customer_status_code");
			sb.Append(" ,o_originator_id");
			sb.Append(" ,o_created");
			sb.Append(" ,o_modified");
			sb.Append(" ,o_closing_date");
			sb.Append(" ,o_client_name");
			sb.Append(" ,o_oul_id");
			sb.Append(" ) values (");
			sb.Append("  '" + Preparer.Escape(this.InternalId) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.InternalATSId) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.CustomerId) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Pin) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.AdditionalPins) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.PropertyAddress) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.PropertyAddress2) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.PropertyCity) + "'");
			sb.Append(" ,'" + Preparer.Escape(state) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.PropertyZip) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.PropertyCounty) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.PropertyUse) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.InternalStatusCode) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.CustomerStatusCode) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.OriginatorId) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Created) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Modified) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.ClosingDate) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.ClientName) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.OrderUploadLogId) + "'");
			sb.Append(" )");

			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to delete this object from the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetDeleteSql()
		{
			return "delete from `order` where o_id = '" + Id.ToString() + "'";
		}

		/// <summary>
		/// reads the column values from a datareader and populates the object properties
		/// </summary>
		/// <param name="reader"></param>
		public override void Load(MySqlDataReader reader)
		{
		   this.Id = Preparer.SafeInt(reader["o_id"]);
		   this.InternalId = Preparer.SafeString(reader["o_internal_id"]);
		   this.InternalATSId = Preparer.SafeString(reader["o_internal_ats_id"]);
		   this.CustomerId = Preparer.SafeString(reader["o_customer_id"]);
		   this.Pin = Preparer.SafeString(reader["o_pin"]);
		   this.AdditionalPins = Preparer.SafeString(reader["o_additional_pins"]);
		   this.PropertyAddress = Preparer.SafeString(reader["o_property_address"]);
		   this.PropertyAddress2 = Preparer.SafeString(reader["o_property_address_2"]);
		   this.PropertyCity = Preparer.SafeString(reader["o_property_city"]);
		   this.PropertyState = Preparer.SafeString(reader["o_property_state"]);
		   this.PropertyZip = Preparer.SafeString(reader["o_property_zip"]);
		   this.PropertyCounty = Preparer.SafeString(reader["o_property_county"]);
		   this.PropertyUse = Preparer.SafeString(reader["o_property_use"]);
		   this.InternalStatusCode = Preparer.SafeString(reader["o_internal_status_code"]);
		   this.CustomerStatusCode = Preparer.SafeString(reader["o_customer_status_code"]);
		   this.OriginatorId = Preparer.SafeInt(reader["o_originator_id"]);
		   this.Created = Preparer.SafeDateTime(reader["o_created"]);
		   this.Modified = Preparer.SafeDateTime(reader["o_modified"]);
		   this.ClosingDate = Preparer.SafeDateTime(reader["o_closing_date"]);
		   this.ClientName = Preparer.SafeString(reader["o_client_name"]);
		   this.OrderUploadLogId = Preparer.SafeInt(reader["o_oul_id"]);

			this.OnLoad(reader);
		}

	}
}