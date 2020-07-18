using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
    /// <summary>
    /// Summary description for RequestNotificationAccountCriteria
    /// </summary>
    public class RequestNotificationAccountCriteria : Criteria
    {
        public int RequestId = -1;
        public int AccountId = -1;
        public int NotificationId = -1;

        protected override void Init()
        {
            this.fields = new Hashtable();
            this.fields.Add("RequestId", "r_id");
            this.fields.Add("AccountId", "a_id");
            this.fields.Add("NotificationId", "n_id");
        }

        protected override string GetSelectSql()
        {
            return "select * from `request_notification` na join `request` r ON (na.r_id = r.r_id)  ";
        }

        protected override string GetWhereSql()
        {
            StringBuilder sb = new StringBuilder();
            string delim = " where ";

            if (-1 != AccountId)
            {
                sb.Append(delim + "na.a_id = '" + Preparer.Escape(AccountId) + "'");
                delim = " and ";
            }

            if (-1 != RequestId)
            {
                sb.Append(delim + "na.r_id = '" + Preparer.Escape(RequestId) + "'");
                delim = " and ";
            }

            if (-1 != NotificationId)
            {
                sb.Append(delim + "na.r_id = '" + Preparer.Escape(RequestId) + "'");
                delim = " and ";
            }

            return sb.ToString();
        }
    }
}