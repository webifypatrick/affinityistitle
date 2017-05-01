using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for OrderCriteria
	/// </summary>
	public class OrderCriteria : Criteria
	{
		public int Id = -1;
		public int LessThanId = -1;
		public string InternalId;
		public string InternalATSId;
		public string CustomerId;
		public string Pin;
		public string PropertyAddress;
		public string PropertyCity;
		public string PropertyState;
		public string PropertyZip;
		public string PropertyCounty;
		public string PropertyUse;
		public string InternalStatusCode;
		public string CustomerStatusCode;
		public int OriginatorId = -1;
		public DateTime Created;
		public DateTime Modified;
		public DateTime ClosingDate;
		public string ClientName;

		public Order FindDuplicateOf = null;
		public int IdNotEquals = -1;
		public int OriginatorIdNotEquals = -1;

		public int CompanyId = -1;

		/// <summary>
		/// Exclude orders withInternalStatusCode = Closed
		/// </summary>
		public bool HideInternalClosed = false;

		/// <summary>
		/// Exclude orders with CustomerStatusCode = Closed
		/// </summary>
		public bool HideCustomerClosed = false;

		/// <summary>
		/// Omit orders with CustomerStatusCode = Closed
		/// and ClosedDate > 14 days in the past
		/// and Modified > 7 days in the past
		/// </summary>
		public bool HideInactive = false;
		
		public int OrderUploadLogId = -1;

		public string SearchQuery;

		protected override void Init()
		{
			this.fields = new Hashtable();
			this.fields.Add("Id", "o_id");
			this.fields.Add("InternalId", "o_internal_id");
			this.fields.Add("InternalATSId", "o_internal_ats_id");
			this.fields.Add("CustomerId", "o_customer_id");
			this.fields.Add("Pin", "o_pin");
			this.fields.Add("AdditionalPins", "o_additional_pins");
			this.fields.Add("PropertyAddress", "o_property_address");
			this.fields.Add("PropertyAddress2", "o_property_address_2");
			this.fields.Add("PropertyCity", "o_property_city");
			this.fields.Add("PropertyState", "o_property_state");
			this.fields.Add("PropertyZip", "o_property_zip");
			this.fields.Add("PropertyCounty", "o_property_county");
			this.fields.Add("PropertyUse", "o_property_use");
			this.fields.Add("InternalStatusCode", "o_internal_status_code");
			this.fields.Add("CustomerStatusCode", "o_customer_status_code");
			this.fields.Add("OriginatorId", "o_originator_id");
			this.fields.Add("Created", "o_created");
			this.fields.Add("Modified", "o_modified");
			this.fields.Add("ClosingDate", "o_closing_date");
			this.fields.Add("ClientName", "o_client_name");
			this.fields.Add("OrderUploadLogId", "o_oul_id");
		}

		/// <summary>
		/// This is customized to pull in the company id from the originator table
		/// </summary>
		/// <returns></returns>
		protected override string GetSelectSql()
		{
			return "select o.*, a.a_company_id from `order` o inner join account a on o.o_originator_id = a.a_id ";
		}

		protected override string GetWhereSql()
		{
			StringBuilder sb = new StringBuilder();
			string delim = " where ";

			if (FindDuplicateOf != null)
			{
				sb.Append(delim + " ( ");
				sb.Append(" ( ");
				sb.Append(" lower(o.o_property_address) = '" + Preparer.Escape(FindDuplicateOf.PropertyAddress.ToLower()) + "'");
				sb.Append(" and o.o_property_address != ''");
				sb.Append(" and lower(o.o_property_city) = '" + Preparer.Escape(FindDuplicateOf.PropertyCity.ToLower()) + "'");
				sb.Append(" and lower(o.o_property_state) = '" + Preparer.Escape(FindDuplicateOf.PropertyState.ToLower()) + "'");
				sb.Append(" and lower(o.o_property_zip) = '" + Preparer.Escape(FindDuplicateOf.PropertyZip.ToLower()) + "'");
				sb.Append(" ) or (");
				sb.Append(" lower(o.o_pin) = '" + Preparer.Escape(FindDuplicateOf.Pin.ToLower()) + "'");
				sb.Append(" and o.o_pin != ''");
				sb.Append(" ) ");
				sb.Append(" ) ");
				sb.Append(" and o.o_id != '" + Preparer.Escape(FindDuplicateOf.Id) + "'");
				delim = " and ";
			}

			if (-1 != IdNotEquals)
			{
				sb.Append(delim + "o.o_id != '" + Preparer.Escape(IdNotEquals) + "'");
				delim = " and ";
			}

			if (-1 != OriginatorIdNotEquals)
			{
				sb.Append(delim + "o.o_originator_id != '" + Preparer.Escape(OriginatorIdNotEquals) + "'");
				delim = " and ";
			}

			if (null != SearchQuery && SearchQuery != "")
			{
				string searchq = Preparer.Escape(SearchQuery.ToLower().Replace("aff", "").Replace("-", "").Replace("web", "").Trim());
				sb.Append(delim + " (");
				sb.Append(" CAST(o_id as CHAR) like '%" + searchq + "%'");
				sb.Append(" or REPLACE(REPLACE(REPLACE(lower(o.o_internal_id), 'aff', ''), '-', ''), 'web', '') like '%" + searchq + "%'");
				sb.Append(" or REPLACE(REPLACE(REPLACE(lower(o.o_customer_id), 'aff', ''), '-', ''), 'web', '') like '%" + searchq + "%'");
				sb.Append(" or REPLACE(REPLACE(REPLACE(lower(o.o_client_name), 'aff', ''), '-', ''), 'web', '') like '%" + searchq + "%'");
				sb.Append(" or REPLACE(REPLACE(REPLACE(lower(o.o_property_address), 'aff', ''), '-', ''), 'web', '') like '%" + searchq + "%'");
				sb.Append(" or REPLACE(REPLACE(REPLACE(lower(o.o_property_city), 'aff', ''), '-', ''), 'web', '') like '%" + searchq + "%'");
				sb.Append(" or REPLACE(REPLACE(REPLACE(lower(o.o_pin), 'aff', ''), '-', ''), 'web', '') like '%" + searchq + "%'");
				sb.Append(") ");
				delim = " and ";
			}

			if (HideInternalClosed)
			{
				sb.Append(delim + "o.o_internal_status_code != '" + Preparer.Escape(Affinity.OrderStatus.ClosedCode) + "'");
				delim = " and ";
				sb.Append(delim + "o.o_internal_status_code != '" + Preparer.Escape(Affinity.OrderStatus.PendingCode) + "'");
			}

			if (HideCustomerClosed)
			{
				sb.Append(delim + "o.o_customer_status_code != '" + Preparer.Escape(Affinity.OrderStatus.ClosedCode) + "'");
				delim = " and ";
				sb.Append(delim + "o.o_customer_status_code != '" + Preparer.Escape(Affinity.OrderStatus.PendingCode) + "'");
			}

			if (HideInactive)
			{
				sb.Append(delim);
				sb.Append("(");
				sb.Append("o.o_customer_status_code != '" + Preparer.Escape(Affinity.OrderStatus.ClosedCode) + "'");
				sb.Append(" or o.o_closing_date > date_add(sysdate(), interval -14 day)");
				sb.Append(" or o.o_modified > date_add(sysdate(), interval -7 day)");
				sb.Append(")");
				delim = " and ";
			
			}

			if (-1 != CompanyId)
			{
				sb.Append(delim + "a.a_company_id = '" + Preparer.Escape(CompanyId) + "'");
				delim = " and ";
			}

			if (-1 != Id)
			{
				sb.Append(delim + "o.o_id = '" + Preparer.Escape(Id) + "'");
				delim = " and ";
			}

			if (-1 != LessThanId)
			{
				sb.Append(delim + "(o.o_id < '" + Preparer.Escape(LessThanId) + "' OR '" + Preparer.Escape(LessThanId) + "' = '0')");
				delim = " and ";
			}

			if (null != InternalId)
			{
				sb.Append(delim + "o.o_internal_id = '" + Preparer.Escape(InternalId) + "'");
				delim = " and ";
			}

			if (null != InternalATSId)
			{
				sb.Append(delim + "o.o_internal_ats_id = '" + Preparer.Escape(InternalATSId) + "'");
				delim = " and ";
			}

			if (null != CustomerId)
			{
				sb.Append(delim + "o.o_customer_id = '" + Preparer.Escape(CustomerId) + "'");
				delim = " and ";
			}

			if (null != Pin)
			{
				sb.Append(delim + "o.o_pin = '" + Preparer.Escape(Pin) + "' and o.o_pin <> ''");
				delim = " and ";
			}

			if (null != PropertyAddress)
			{
				sb.Append(delim + "o.o_property_address = '" + Preparer.Escape(PropertyAddress) + "'");
				delim = " and ";
			}

			if (null != PropertyCity)
			{
				sb.Append(delim + "o.o_property_city = '" + Preparer.Escape(PropertyCity) + "'");
				delim = " and ";
			}

			if (null != PropertyState)
			{
				sb.Append(delim + "o.o_property_state = '" + Preparer.Escape(PropertyState) + "'");
				delim = " and ";
			}

			if (null != PropertyZip)
			{
				sb.Append(delim + "o.o_property_zip = '" + Preparer.Escape(PropertyZip) + "'");
				delim = " and ";
			}

			if (null != PropertyCounty)
			{
				sb.Append(delim + "o.o_property_county = '" + Preparer.Escape(PropertyCounty) + "'");
				delim = " and ";
			}

			if (null != PropertyUse)
			{
				sb.Append(delim + "o.o_property_use = '" + Preparer.Escape(PropertyUse) + "'");
				delim = " and ";
			}

			if (null != InternalStatusCode)
			{
				sb.Append(delim + "o.o_internal_status_code = '" + Preparer.Escape(InternalStatusCode) + "'");
				delim = " and ";
			}

			if (null != CustomerStatusCode)
			{
				sb.Append(delim + "o.o_customer_status_code = '" + Preparer.Escape(CustomerStatusCode) + "'");
				delim = " and ";
			}

			if (-1 != OriginatorId)
			{
				sb.Append(delim + "o.o_originator_id = '" + Preparer.Escape(OriginatorId) + "'");
				delim = " and ";
			}

			if ("1-1-1 0:0:0" != Preparer.Escape(Created))
			{
				sb.Append(delim + "o.o_created = '" + Preparer.Escape(Created) + "'");
				delim = " and ";
			}

			if ("1-1-1 0:0:0" != Preparer.Escape(Modified))
			{
				sb.Append(delim + "o.o_modified = '" + Preparer.Escape(Modified) + "'");
				delim = " and ";
			}

			if ("1-1-1 0:0:0" != Preparer.Escape(ClosingDate))
			{
				sb.Append(delim + "o.o_closing_date = '" + Preparer.Escape(ClosingDate) + "'");
				delim = " and ";
			}

			if (null != ClientName)
			{
				sb.Append(delim + "o.o_client_name = '" + Preparer.Escape(ClientName) + "'");
				delim = " and ";
			}

			if(1 == OrderUploadLogId)
			{
				sb.Append(delim + "o.o_oul_id > '0'");
				delim = " and ";
			}
			else if (-1 != OrderUploadLogId)
			{
				sb.Append(delim + "o.o_oul_id = '" + Preparer.Escape(OrderUploadLogId) + "'");
				delim = " and ";
			}

			return sb.ToString();
		}
	}
}