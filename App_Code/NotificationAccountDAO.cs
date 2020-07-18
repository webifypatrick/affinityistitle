using MySql.Data.MySqlClient;
using System.Text;
using Com.VerySimple.Phreeze;

namespace Affinity
{
    /// <summary>
    /// This code is automatically generated from the OrderAssignment schema and
    /// should not be edited if at all possible.  All customizations and 
    /// business logic should be added to the partial class OrderAssignment.cs
    /// </summary>
    public partial class NotificationAccount : Loadable
    {


        public NotificationAccount(Phreezer phreezer) : base(phreezer) { }
        public NotificationAccount(Phreezer phreezer, MySqlDataReader reader) : base(phreezer, reader) { }

        /* ~~~ PUBLIC PROPERTIES ~~~ */


        private int _accountId = 0;
        public int AccountId
        {
            get { return this._accountId; }
            set { this._accountId = value; }
        }

        string _accountIds = "";
        public string AccountIds
        {
            get { return this._accountIds; }
            set { this._accountIds = value; }
        }

        private int _notificationId = 0;
        public int NotificationId
        {
            get { return this._notificationId; }
            set { this._notificationId = value; }
        }

        private int _isRead = 0;
        public int IsRead
        {
            get { return this._isRead; }
            set { this._isRead = value; }
        }

        /* ~~~ CONSTRAINTS ~~~ */

        private Account _assignedaccount;
        public Account AssignedAccount
        {
            get
            {
                if (this._assignedaccount == null)
                {
                    this._assignedaccount = new Account(this.phreezer);
                    this._assignedaccount.Load(this.AccountId);
                }
                return this._assignedaccount;
            }
            set { this._assignedaccount = value; }
        }

        private Notification _notification;
        public Notification Note
        {
            get
            {
                if (this._notification == null)
                {
                    this._notification = new Notification(this.phreezer);
                    this._notification.Load(this.NotificationId);
                }
                return this._notification;
            }
            set { this._notification = value; }
        }


        /* ~~~ SETS ~~~ */


        /* ~~~ CRUD OPERATIONS ~~~ */

        /// <summary>
        /// Assigns a value to the primary key
        /// </summary>
        /// <param name="key"></param>
        protected override void SetPrimaryKey(object key)
        {
            this.AccountId = (int)key;
            this.NotificationId = (int)key;
        }

        /// <summary>
        /// Returns a SQL statement to select this object from the DB
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        protected override string GetSelectSql(object pk)
        {
            return "select * from `notification_account` na where na.na_id = '" + pk.ToString() + "'";
        }

        /// <summary>
        /// Returns an SQL statement to update this object in the DB
        /// </summary>
        /// <returns></returns>
        protected override string GetUpdateSql()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("update `notification_account` set");
            sb.Append("  is_read = '" + Preparer.Escape(this.IsRead) + "'");
            sb.Append(" where a_id = '" + Preparer.Escape(this.AccountId) + "' and n_id = '" + Preparer.Escape(this.NotificationId) + "'");
            return sb.ToString();
        }

        /// <summary>
        /// Returns an SQL statement to insert this object into the DB
        /// </summary>
        /// <returns></returns>
        protected override string GetInsertSql()
        {
            if(this.AccountIds == null || this.AccountIds.Equals(""))
            {
                this.AccountIds = this.AccountId.ToString();
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM `notification_account` WHERE n_id = '" + Preparer.Escape(this.NotificationId) + "'; ");
            sb.Append("INSERT INTO `notification_account` (");
            sb.Append("  a_id");
            sb.Append(" ,n_id");
            sb.Append(" ,is_read");
            sb.Append(" ,na_status");
            sb.Append(" ,na_created");
            sb.Append(" ,na_modified");
            sb.Append(" ) SELECT a_id ");
            //sb.Append("  '" + Preparer.Escape(this.AccountId) + "'");
            sb.Append(" ,'" + Preparer.Escape(this.NotificationId) + "'");
            sb.Append(" ,'" + Preparer.Escape(this.IsRead) + "'");
            sb.Append(" ,1");
            sb.Append(" ,sysdate()");
            sb.Append(" ,sysdate()");
            sb.Append(" FROM account WHERE a_id IN (" + this.AccountIds + ")");

            return sb.ToString();
        }

        /// <summary>
        /// Returns an SQL statement to delete this object from the DB
        /// </summary>
        /// <returns></returns>
        protected override string GetDeleteSql()
        {
            return "delete from `notification_account` where a_id = '" + Preparer.Escape(this.AccountId) + "' and n_id = '" + Preparer.Escape(this.NotificationId) + "'";
        }

        /// <summary>
        /// reads the column values from a datareader and populates the object properties
        /// </summary>
        /// <param name="reader"></param>
        public override void Load(MySqlDataReader reader)
        {
            this.AccountId = Preparer.SafeInt(reader["a_id"]);
            this.NotificationId = Preparer.SafeInt(reader["n_id"]);
            this.IsRead = Preparer.SafeInt(reader["is_read"]);

            this.OnLoad(reader);
        }

    }
}