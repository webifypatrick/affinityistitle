using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for CompanyCriteria
	/// </summary>
	public class CompanyCriteria : Criteria
	{
		public int Id = -1;
		public string Name;
		public string NameBeginsWith;
		public DateTime Created;

		protected override void Init()
		{
			this.fields = new Hashtable();
			this.fields.Add("Id", "c_id");
			this.fields.Add("Name", "c_name");
			this.fields.Add("Created", "c_created");
		}

		protected override string GetSelectSql()
		{
			return "select * from `company` c ";
		}

		protected override string GetWhereSql()
		{
			StringBuilder sb = new StringBuilder();
			string delim = " where ";

			if (-1 != Id)
			{
				sb.Append(delim + "c.c_id = '" + Preparer.Escape(Id) + "'");
				delim = " and ";
			}

			if (null != Name)
			{
				sb.Append(delim + "c.c_name = '" + Preparer.Escape(Name) + "'");
				delim = " and ";
			}

			if (null != NameBeginsWith)
			{
				sb.Append(delim + "c.c_name like '" + Preparer.Escape(NameBeginsWith) + "%'");
				delim = " and ";
			}

			if ("1-1-1 0:0:0" != Preparer.Escape(Created))
			{
				sb.Append(delim + "c.c_created = '" + Preparer.Escape(Created) + "'");
				delim = " and ";
			}

			return sb.ToString();
		}
	}
}