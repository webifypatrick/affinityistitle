using Com.VerySimple.Phreeze;
using System.Text;

namespace Affinity
{
    public partial class Notification
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        protected override string GetSelectSql(object pk)
        {
            // load the hudLog
            return "select * from notification n where n_id = '" + pk.ToString() + "'";
        }

        /// <summary>
        /// Persists updates to the DB.
        /// </summary>
        /// <returns></returns>
        protected override string GetUpdateSql()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("update `notification` set");
            sb.Append("  n_originator_id = '" + Preparer.Escape(this.OriginatorId) + "'");
            sb.Append(" ,n_subject = '" + Preparer.Escape(this.Subject) + "'");
            sb.Append(" ,n_message = '" + Preparer.Escape(this.Message) + "'");
            sb.Append(" ,n_validfrom = '" + Preparer.Escape(this.ValidFrom) + "'");
            sb.Append(" ,n_validto = '" + Preparer.Escape(this.ValidTo) + "'");
            sb.Append(" ,n_modified = sysdate()");
            sb.Append(" where n_id = '" + Preparer.Escape(this.Id) + "'");

            return sb.ToString();
        }

        /// <summary>
        /// Inserts into the DB.
        /// </summary>
        /// <returns></returns>
        protected override string GetInsertSql()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into `notification` (");
            sb.Append("  n_originator_id");
            sb.Append(" ,n_subject");
            sb.Append(" ,n_message");
            sb.Append(" ,n_validfrom");
            sb.Append(" ,n_validto");
            sb.Append(" ,n_modified");
            sb.Append(" ,n_created");
            sb.Append(" ) values (");
            sb.Append("  '" + Preparer.Escape(this.OriginatorId) + "'");
            sb.Append(" ,'" + Preparer.Escape(this.Subject) + "'");
            sb.Append(" ,'" + Preparer.Escape(this.Message) + "'");
            sb.Append(" ,'" + Preparer.Escape(this.ValidFrom) + "'");
            sb.Append(" ,'" + Preparer.Escape(this.ValidTo) + "'");
            sb.Append(" ,sysdate()");
            sb.Append(" ,sysdate()");
            sb.Append(" )");

            return sb.ToString();
        }
    }
}