using System;
using MySql.Data.MySqlClient;
using Com.VerySimple.Phreeze;
using System.Text;

namespace Affinity
{
    public partial class Notification : Loadable
    {
        public Notification(Phreezer phreezer) : base(phreezer) { }
        public Notification(Phreezer phreezer, MySqlDataReader reader) : base(phreezer,reader) { }

        /* ~~~ PUBLIC PROPERTIES ~~~ */


        private int _id = 0;
        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        private string _subject = "";
        public string Subject
        {
            get { return this._subject; }
            set { this._subject = value; }
        }

        private string _message = "";
        public string Message
        {
            get { return this._message; }
            set { this._message = value; }
        }

        private int _originatorId = -1;
        public int OriginatorId
        {
            get { return this._originatorId; }
            set { this._originatorId = value; }
        }

        private int _status = -1;
        public int Status
        {
            get { return this._status; }
            set { this._status = value; }
        }

        private DateTime _validfrom = new DateTime(1,1,1);
        public DateTime ValidFrom
        {
            get {
                DateTime retDate = new DateTime(1, 1, 1);

                if(this._validfrom != null)
                {
                    retDate = this._validfrom;
                }
                return retDate;
            }
            set
            {
                this._validfrom = value;
            }
        }

        private DateTime _validto = new DateTime(1, 1, 1);
        public DateTime ValidTo
        {
            get
            {
                DateTime retDate = new DateTime(1, 1, 1);

                if (this._validto != null)
                {
                    retDate = this._validto;
                }
                return retDate;
            }
            set
            {
                this._validto = value;
            }
        }

        private DateTime _created = DateTime.Now;
        public DateTime Created
        {
            get { return this._created; }
            set { this._created = value; }
        }

        private DateTime _modified = DateTime.Now;
        public DateTime Modified
        {
            get { return this._modified; }
            set { this._modified = value; }
        }

        /* ~~~ CONSTRAINTS ~~~ */

        private Account _account;
        public Account Account
        {
            get
            {
                if (this._account == null)
                {
                    this._account = new Account(this.phreezer);
                    this._account.Load(this.OriginatorId);
                }
                return this._account;
            }
            set { this._account = value; }
        }

        private Accounts _accounts;
        public Accounts Accounts
        {
            get
            {
                if (this._accounts == null)
                {
                    this._accounts = new Accounts(this.phreezer);
                    NotificationAccounts notaccounts = new NotificationAccounts(this.phreezer);
                    NotificationAccountCriteria notacccrit = new NotificationAccountCriteria();
                    notacccrit.NotificationId = this.Id;
                    notaccounts.Query(notacccrit);

                    foreach (NotificationAccount notacc in notaccounts)
                    {
                        Account acct = new Account(this.phreezer);
                        acct.Load(notacc.AccountId);

                        this._accounts.Add(acct);
                    }
                }
                return this._accounts;
            }
            set {

                if (value != null && value.Count > 0)
                {
                    foreach (NotificationAccount notacc in value)
                    {
                        Account acct = new Account(this.phreezer);
                        acct.Load(notacc.AccountId);

                        this._accounts.Add(acct);
                    }
                }
            }
        }
        /* ~~~ SETS ~~~ */

        /// <summary>
        /// Returns a collection of Notification objects
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns>Notifications</returns>
        protected Notifications GetNotifications(NotificationCriteria criteria)
        {
            //criteria.OriginatorId = this.Id;
            Notifications Notifications = new Notifications(this.phreezer);
            Notifications.Query(criteria);
            return Notifications;
        }

        /* ~~~ CRUD OPERATIONS ~~~ */

        /// <summary>
        /// Assigns a value to the primary key
        /// </summary>
        /// <param name="key"></param>
        protected override void SetPrimaryKey(object key)
        {
            this.Id = (int)key;
        }


        /// <summary>
        /// Returns an SQL statement to delete this object from the DB
        /// </summary>
        /// <returns></returns>
        protected override string GetDeleteSql()
        {
            return "delete from `notification` where n_id = '" + Id.ToString() + "'";
        }

        /// <summary>
        /// reads the column values from a datareader and populates the object properties
        /// </summary>
        /// <param name="reader"></param>
        public override void Load(MySqlDataReader reader)
        {
            this.Id = Preparer.SafeInt(reader["n_id"]);
            this.OriginatorId = Preparer.SafeInt(reader["n_originator_id"]);
            this.Subject = Preparer.SafeString(reader["n_subject"]);
            this.Message = Preparer.SafeString(reader["n_message"]);
            this.ValidFrom = Preparer.SafeDateTime(reader["n_validfrom"]);
            this.ValidTo = Preparer.SafeDateTime(reader["n_validto"]);
            this.Created = Preparer.SafeDateTime(reader["n_created"]);
            this.Modified = Preparer.SafeDateTime(reader["n_modified"]);

            this.OnLoad(reader);
        }

    }
}