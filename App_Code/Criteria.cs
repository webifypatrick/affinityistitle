using System.Collections;
using System;

namespace Com.VerySimple.Phreeze
{
    /// <summary>
    /// Base class for for Criteria
    /// </summary>
    public abstract class Criteria
    {
		/// <summary>
		/// CAUTION!  When setting this to true the the collection will be
		/// padded with PageShim objects so that gridviews and such will
		/// calculate the pagecount correctly.
		/// </summary>
		public bool PagingEnabled = false;

		// use to limit results without pagination
		public int MaxResults = 0;

		public int Page = 1;
		public int PageSize = 10;
		protected string OrderBy = "";
		protected string JoinSql = "";
		protected Hashtable fields;

		protected abstract string GetSelectSql();
		protected abstract string GetWhereSql();
		protected abstract void Init();

		/// <summary>
		/// returns the name of the field associated with the given property
		/// </summary>
		/// <param name="propName"></param>
		/// <returns></returns>
		protected string GetFieldName(string propName)
		{
			if (this.fields[propName] == null)
			{
				throw new Exception("The property name '" + propName + "' is not defined");
			}
			return this.fields[propName].ToString();
		}

		public Criteria()
		{
			this.Init();
			if (this.fields == null)
			{
				throw new Exception("Criteria did not instantiate the fields Hashtable during OnInit");
			}
		}

		public void AppendToOrderBy(string propName, bool desc)
		{
			this.OrderBy += (this.OrderBy != "") ? "," : "";
			this.OrderBy += this.GetFieldName(propName) + (desc ? " desc" : "");
		}

		public void AppendToOrderBy(string propName)
		{
			this.AppendToOrderBy(propName, false);
		}

		public string GetSql()
		{
			return this.GetSelectSql()
				+ this.GetJoinSql()
				+ this.GetWhereSql()
				+ this.GetOrderBySql()
				+ this.GetLimitSql();
		}

		public string GetCountSql()
		{
			// for counting we don't really need to sort
			// and we don't add the limit code
			return "select count(1) as counter from ("
				+ this.GetSelectSql()
				+ this.GetJoinSql()
				+ this.GetWhereSql()
				+ ") tmptable";
		}

		protected string GetLimitSql()
        {
			if (this.PagingEnabled)
            {
				int start = (PageSize * Page) - PageSize;
				int num = PageSize;
				return " limit " + start.ToString() + "," + num.ToString(); 
            }

			if (MaxResults > 0)
			{
				return " limit " + MaxResults.ToString();
			}

            return "";

        }

		protected string GetOrderBySql()
		{
			if (this.OrderBy != "")
			{
				return " order by " + this.OrderBy;
			}
			else
			{
				return "";
			}
		}

		protected string GetJoinSql()
		{
			if (this.JoinSql != "")
			{
				return " " + this.JoinSql;
			}
			else
			{
				return "";
			}
		}

    }
}