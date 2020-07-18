using System;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
    /// <summary>
    /// Summary description for HUDLogCriteria
    /// </summary>
    public class NotificationCriteria : Criteria
    {
        public int Id = -1;
        public int OriginatorID;
        public string Subject;
        public string Message;
        public DateTime ValidFrom;
        public DateTime ValidTo;
        public DateTime Created;
        public DateTime Modified;

        protected override void Init()
        {
            this.fields = new Hashtable();
            this.fields.Add("Id", "n_id");
            this.fields.Add("OriginatorID", "n_originator_id");
            this.fields.Add("Subject", "n_subject");
            this.fields.Add("Message", "n_message");
            this.fields.Add("ValidFrom", "n_validfrom");
            this.fields.Add("ValidTo", "n_validto");
            this.fields.Add("Created", "n_created");
            this.fields.Add("Modified", "n_modified");

        }

        protected override string GetSelectSql()
        {
            return "select * from notification n ";
        }

        protected override string GetWhereSql()
        {
            StringBuilder sb = new StringBuilder();
            string delim = " where ";

            /*
            if (OriginatorID > 0)
            {
                sb.Append(delim + "n.n_originator_id = '" + Preparer.Escape(OriginatorID) + "'");
                delim = " and ";
            }

            if (!Subject.Equals(""))
            {
                sb.Append(delim + "n.n_subject = '" + Preparer.Escape(Subject) + "'");
                delim = " and ";
            }

            if (!Message.Equals(""))
            {
                sb.Append(delim + "n.n_message = '" + Preparer.Escape(Message) + "'");
                delim = " and ";
            }

            if (ValidFrom != null && "1-1-1 0:0:0" != Preparer.Escape(ValidFrom))
            {
                sb.Append(delim + "n.n_validfrom = '" + Preparer.Escape(ValidFrom) + "'");
                delim = " and ";
            }

            if (ValidTo != null && "1-1-1 0:0:0" != Preparer.Escape(ValidTo))
            {
                sb.Append(delim + "n.n_validto = '" + Preparer.Escape(ValidTo) + "'");
                delim = " and ";
            }

            if ("1-1-1 0:0:0" != Preparer.Escape(Created))
            {
                sb.Append(delim + "n.n_created = '" + Preparer.Escape(Created) + "'");
                delim = " and ";
            }

            if ("1-1-1 0:0:0" != Preparer.Escape(Modified))
            {
                sb.Append(delim + "n.n_modified = '" + Preparer.Escape(Modified) + "'");
                delim = " and ";
            }
            */

            return sb.ToString();
        }
    }
}