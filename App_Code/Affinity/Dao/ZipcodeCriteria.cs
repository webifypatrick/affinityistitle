using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for ZipcodeCriteria
	/// </summary>
	public class ZipcodeCriteria : Criteria
	{
		public string Zip;
		public string ZipBeginsWith;
		public string City;
		public string CityBeginsWith;
		public string State;
        public string County;
        public string CountyBeginsWith;

        public string FipsCode;


		public bool DistinctCity = false;
		public bool DistinctCounty = false;

		protected override void Init()
		{
			this.fields = new Hashtable();
			this.fields.Add("Zip", "Zip");
			this.fields.Add("City", "City");
			this.fields.Add("State", "State");
            this.fields.Add("County", "County");
            this.fields.Add("FipsCode", "FipsCode");
        }

		protected override string GetSelectSql()
		{
			if (DistinctCity)
			{
				return "select distinct '-' as Zip, City, State, '-' as County, '-' as FipsCode from `zipcode` z ";
			}

			if (DistinctCounty)
			{
                return "select distinct '-' as Zip, '-' as City, State, County, FipsCode from `zipcode` z ";
			}

			return "select * from `zipcode` z ";
		}

		protected override string GetWhereSql()
		{
			StringBuilder sb = new StringBuilder();
			string delim = " where ";

			if (null != Zip)
			{
				sb.Append(delim + "z.Zip = '" + Preparer.Escape(Zip) + "'");
				delim = " and ";
			}

			if (null != ZipBeginsWith)
			{
				sb.Append(delim + "z.Zip like '" + Preparer.Escape(ZipBeginsWith) + "%'");
				delim = " and ";
			}

			if (null != City)
			{
				sb.Append(delim + "z.City = '" + Preparer.Escape(City) + "'");
				delim = " and ";
			}

			if (null != CityBeginsWith)
			{
				sb.Append(delim + "z.City like '" + Preparer.Escape(CityBeginsWith) + "%'");
				delim = " and ";
			}

			if (null != State)
			{
				sb.Append(delim + "z.State = '" + Preparer.Escape(State) + "'");
				delim = " and ";
			}

            if (null != County)
            {
                sb.Append(delim + "z.County = '" + Preparer.Escape(County) + "'");
                delim = " and ";
            }

            if (null != FipsCode)
            {
                sb.Append(delim + "z.FipsCode = '" + Preparer.Escape(FipsCode) + "'");
                delim = " and ";
            }

			if (null != CountyBeginsWith)
			{
				sb.Append(delim + "z.County like '" + Preparer.Escape(CountyBeginsWith) + "%'");
				delim = " and ";
			}

			return sb.ToString();
		}
	}
}