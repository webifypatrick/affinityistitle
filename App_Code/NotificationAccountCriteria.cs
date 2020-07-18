using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
    /// <summary>
    /// Summary description for NotificationAccountCriteria
    /// </summary>
    public class NotificationAccountCriteria : Criteria
    {
        public int AccountId = -1;
        public int NotificationId = -1;
        public int IsRead;

        protected override void Init()
        {
            this.fields = new Hashtable();
            this.fields.Add("AccountId", "a_id");
            this.fields.Add("NotificationId", "n_id");
            this.fields.Add("IsRead", "is_read");
        }

        protected override string GetSelectSql()
        {
            return "select * from `notification_account` na join `notification` n ON (n.n_id = na.n_id AND n.n_status = 1)  ";
        }

        protected override string GetWhereSql()
        {
            StringBuilder sb = new StringBuilder();
            string delim = " where na.na_status = 1 and ";

            if (-1 != AccountId)
            {
                sb.Append(delim + "na.a_id = '" + Preparer.Escape(AccountId) + "'");
                delim = " and ";
            }

            if (-1 != NotificationId)
            {
                sb.Append(delim + "na.n_id = '" + Preparer.Escape(NotificationId) + "'");
                delim = " and ";
            }

            if (null != IsRead)
            {
                sb.Append(delim + "na.is_read = '" + Preparer.Escape(IsRead) + "'");
                delim = " and ";
            }

            sb.Append(delim + "(n.n_validfrom <= sysdate() OR n.n_validfrom IS NULL OR n.n_validfrom = '1/1/1')");
            delim = " and ";
            sb.Append(delim + "(n.n_validto >= sysdate() OR n.n_validto IS NULL OR n.n_validto = '1/1/1')");

            return sb.ToString();
        }
    }
}