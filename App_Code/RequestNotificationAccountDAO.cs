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
    public partial class RequestNotificationAccount : Loadable
    {


        public RequestNotificationAccount(Phreezer phreezer) : base(phreezer) { }
        public RequestNotificationAccount(Phreezer phreezer, MySqlDataReader reader) : base(phreezer, reader) { }

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

        private int _requestId = 0;
        public int RequestId
        {
            get { return this._requestId; }
            set { this._requestId = value; }
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

        private Request _request;
        public Request request
        {
            get
            {
                if (this._request == null)
                {
                    this._request = new Request(this.phreezer);
                    this._request.Load(this.RequestId);
                }
                return this._request;
            }
            set { this._request = value; }
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
            this.RequestId = (int)key;
        }

        /// <summary>
        /// Returns a SQL statement to select this object from the DB
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        protected override string GetSelectSql(object pk)
        {
            return "select * from `request_notification` rn where rn.rn_id = '" + pk.ToString() + "'";
        }

        /// <summary>
        /// Returns an SQL statement to update this object in the DB
        /// </summary>
        /// <returns></returns>
        protected override string GetUpdateSql()
        {
            StringBuilder sb = new StringBuilder();

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
            sb.Append("DELETE FROM `request_notification` WHERE r_id = '" + Preparer.Escape(this.RequestId) + "'; ");
            sb.Append("INSERT INTO `request_notification` (");
            sb.Append("  a_id");
            sb.Append(" ,r_id");
            sb.Append(" ,rn_status");
            sb.Append(" ,rn_created");
            sb.Append(" ) SELECT a_id ");
            sb.Append(" ,'" + Preparer.Escape(this.RequestId) + "'");
            sb.Append(" ,1");
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
            return "delete from `request_notification` where a_id = '" + Preparer.Escape(this.AccountId) + "' and r_id = '" + Preparer.Escape(this.RequestId) + "'";
        }

        /// <summary>
        /// reads the column values from a datareader and populates the object properties
        /// </summary>
        /// <param name="reader"></param>
        public override void Load(MySqlDataReader reader)
        {
            this.AccountId = Preparer.SafeInt(reader["a_id"]);
            this.RequestId = Preparer.SafeInt(reader["r_id"]);

            this.OnLoad(reader);
        }

    }
}