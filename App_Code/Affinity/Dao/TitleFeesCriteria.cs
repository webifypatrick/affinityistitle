using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Summary description for TitleFeesCriteria
	/// </summary>
    public class TitleFeesCriteria : Criteria
	{
		public string Id;
    public string FeeType;
    public string Name;
    public decimal Fee;
    public DateTime Modified;
    public DateTime Created;

		/// <summary>
		/// Codes accepts a comma-separated list of titlefee codes
		/// </summary>

		protected override void Init()
		{
			this.fields = new Hashtable();
			this.fields.Add("Id", "tf_id");
      this.fields.Add("FeeType", "tf_type");
      this.fields.Add("Name", "tf_name");
      this.fields.Add("Fee", "tf_fee");
      this.fields.Add("Modified", "tf_modified");
      this.fields.Add("Created", "tf_created");
  	}

		protected override string GetSelectSql()
		{
			return "select * from `title_fees` tf ";
		}

		protected override string GetWhereSql()
		{
			StringBuilder sb = new StringBuilder();
			string delim = " where ";

       if (null != Id)
			{
        sb.Append(delim + "tf.tf_id = '" + Preparer.Escape(Id) + "'");
				delim = " and ";
			}

      if (null != FeeType)
      {
          sb.Append(delim + "tf.tf_type = '" + Preparer.Escape(FeeType) + "'");
          delim = " and ";
      }

      if (null != Name)
      {
          sb.Append(delim + "tf.tf_name = '" + Preparer.Escape(Name) + "'");
          delim = " and ";
      }

      if (0 != Fee)
      {
          sb.Append(delim + "tf.tf_fee = '" + Preparer.Escape(Fee) + "'");
          delim = " and ";
      }
/*
      if (null != Modified)
      {
          sb.Append(delim + "tf.tf_modified = '" + Preparer.Escape(Modified) + "'");
          delim = " and ";
      }

      if (null != Created)
      {
          sb.Append(delim + "tf.tf_created = '" + Preparer.Escape(Created) + "'");
          delim = " and ";
      }
*/
			return sb.ToString();
		}
	}
}