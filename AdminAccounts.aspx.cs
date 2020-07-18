using System;
using System.Text;
using System.Web.UI.WebControls;
using Com.VerySimple.Phreeze;
using MySql.Data.MySqlClient;
using System.Collections;

namespace Affinity
{
    public partial class AdminAccounts : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.RequirePermission(Affinity.RolePermission.AdminSystem);
            this.RequirePermission(Affinity.RolePermission.AffinityManager);
            ((Affinity.MasterPage)this.Master).SetLayout("Manage Accounts", MasterPage.LayoutStyle.ContentOnly);

            var searchcriteria = "";
            if (Request["srch"] != null && !Request["srch"].Equals(""))
            {
                searchcriteria = Request["srch"];
            }
            /*
            Affinity.Accounts accs = new Affinity.Accounts(this.phreezer);
            Affinity.SearchAccountCriteria crit = new Affinity.SearchAccountCriteria();
            crit.AppendToOrderBy("Username");

            if (Request["srch"] != null && !Request["srch"].Equals(""))
            {
                string searchcriteria = Request["srch"];
                crit.FirstName = searchcriteria;
                crit.LastName = searchcriteria;
                //crit. = searchcriteria;
                crit.Username = searchcriteria;
                crit.Email = searchcriteria;
                crit.Company = searchcriteria;
                crit.RoleCode = searchcriteria;
            }
            */

            using (MySqlDataReader reader = this.phreezer.ExecuteReader("select a.a_id AS Id, a.a_username AS Username, a.a_first_name AS FirstName, a.a_last_name AS LastName, a.a_email AS Email, 'Affinity Title Services, LLC' as Company, r_description AS RoleCode, a.a_modified AS Modified, a.a_business_license_id AS BusinessLicenseID, a.a_individual_license_id AS IndividualLicenseID, GROUP_CONCAT(CONCAT(aa.a_username, '^', al.change_desc, '^', CAST(al.al_modified AS char)) SEPARATOR '|') AS AccountLog from account a inner join role r on a.a_role_code = r.r_code left join account_log al on al.a_id = a.a_id left join account aa on aa.a_id = al.admin_id where a.a_email like '%" + searchcriteria + "%' or a.a_username like '%" + searchcriteria + "%' or a.a_first_name like '%" + searchcriteria + "%' or a.a_last_name like '%" + searchcriteria + "%' GROUP BY a.a_id, a.a_username, a.a_first_name, a.a_last_name, a.a_email, r_description, a.a_modified, a.a_business_license_id, a.a_individual_license_id order by a.a_username", System.Data.CommandBehavior.SingleResult))
            {
                aGrid.DataSource = reader;
                aGrid.DataBind();

                reader.Close();
            }




            //accs.Query(crit);

            //aGrid.DataSource = accs;
            //aGrid.DataBind();
        }

        protected void gvaGrid_rowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gav = (GridView)e.Row.FindControl("alGrid");

                /*
                Affinity.AccountLogs al = new Affinity.AccountLogs(this.phreezer);
                // populate current exports grid
                Affinity.AccountLogCriteria alc = new Affinity.AccountLogCriteria();
                Affinity.Account a = (Affinity.Account)e.Row.DataItem;
                alc.AccountID = a.Id;
                al.Query(alc);

                gav.DataSource = al;
                */

                System.Data.DataTable table = new System.Data.DataTable();
                var column = new System.Data.DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "UserName";
                table.Columns.Add(column);

                column = new System.Data.DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "Description";
                table.Columns.Add(column);

                column = new System.Data.DataColumn();
                column.DataType = System.Type.GetType("System.DateTime");
                column.ColumnName = "CreateDate";
                table.Columns.Add(column);

                var accountLogRows = e.Row.Cells[10].Text.Split('|');

                foreach (var row in accountLogRows)
                {
                    var newrow = table.NewRow();
                    var cells = row.Split('^');
                    var username = "";
                    var desc = "";
                    DateTime dt = DateTime.Now;

                    if (cells.Length > 0) username = cells[0];
                    if (cells.Length > 1) desc = cells[1];
                    if (cells.Length > 2) DateTime.TryParse(cells[2], out dt);

                    newrow["UserName"] = username;
                    newrow["Description"] = desc;
                    newrow["CreateDate"] = dt;

                    table.Rows.Add(newrow);
                }

                gav.DataSource = table;
                gav.DataBind();
            }
        }

        protected void gvalGrid_rowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Parent.Parent.Parent.Parent.FindControl("lblAccountHistory").Visible = true;
                e.Row.Parent.Parent.Parent.Parent.FindControl("pnlAccountHistory").Visible = true;
            }
        }
    }

    public class AccountLogCriteria : Criteria
    {
        public int Id = -1;
        public int AccountID;
        public int AdminID;
        public string ChangeDesc;
        public DateTime Created;
        public DateTime Modified;

        protected override void Init()
        {
            this.fields = new Hashtable();
            this.fields.Add("Id", "al_id");
            this.fields.Add("AccountID", "a_id");
            this.fields.Add("AdminID", "admin_id");
            this.fields.Add("ChangeDesc", "change_desc");
            this.fields.Add("Created", "al_created");
            this.fields.Add("Modified", "al_modified");

        }

        protected override string GetSelectSql()
        {
            return "select * from Account_log al ";
        }

        protected override string GetWhereSql()
        {
            StringBuilder sb = new StringBuilder();
            string delim = " where ";

            if (AccountID > 0)
            {
                sb.Append(delim + "al.a_id = '" + Preparer.Escape(AccountID) + "'");
                delim = " and ";
            }

            if (AdminID > 0)
            {
                sb.Append(delim + "al.admin_id = '" + Preparer.Escape(AdminID) + "'");
                delim = " and ";
            }

            if (null != ChangeDesc)
            {
                sb.Append(delim + "al.change_desc = '" + Preparer.Escape(ChangeDesc) + "'");
                delim = " and ";
            }

            if ("1-1-1 0:0:0" != Preparer.Escape(Created))
            {
                sb.Append(delim + "al.al_created = '" + Preparer.Escape(Created) + "'");
                delim = " and ";
            }

            if ("1-1-1 0:0:0" != Preparer.Escape(Modified))
            {
                sb.Append(delim + "al.al_modified = '" + Preparer.Escape(Modified) + "'");
                delim = " and ";
            }

            return sb.ToString();
        }
    }

    public partial class AccountLog : Loadable
    {


        public AccountLog(Phreezer phreezer) : base(phreezer) { }
        public AccountLog(Phreezer phreezer, MySqlDataReader reader) : base(phreezer, reader) { }

        /* ~~~ PUBLIC PROPERTIES ~~~ */


        private int _id = 0;
        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        private int _accountId = -1;
        public int AccountID
        {
            get { return this._accountId; }
            set { this._accountId = value; }
        }

        private int _adminId = -1;
        public int AdminID
        {
            get { return this._adminId; }
            set { this._adminId = value; }
        }

        private string _changeDesc = "";
        public string ChangeDesc
        {
            get { return this._changeDesc; }
            set { this._changeDesc = value; }
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
                    this._account.Load(this.AccountID);
                }
                return this._account;
            }
            set { this._account = value; }
        }

        private Account _adminaccount;
        public Account AdminAccount
        {
            get
            {
                if (this._adminaccount == null)
                {
                    this._adminaccount = new Account(this.phreezer);
                    this._adminaccount.Load(this.AdminID);
                }
                return this._adminaccount;
            }
            set { this._adminaccount = value; }
        }


        /* ~~~ SETS ~~~ */

        /// <summary>
        /// Returns a collection of AccountLog objects
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns>AccountLogs</returns>
        protected AccountLogs GetAccountLogs(AccountLogCriteria criteria)
        {
            //criteria.OriginatorId = this.Id;
            AccountLogs AccountLogs = new AccountLogs(this.phreezer);
            AccountLogs.Query(criteria);
            return AccountLogs;
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
            return "delete from `Account_log` where al_id = '" + Id.ToString() + "'";
        }

        /// <summary>
        /// reads the column values from a datareader and populates the object properties
        /// </summary>
        /// <param name="reader"></param>
        public override void Load(MySqlDataReader reader)
        {
            this.Id = Preparer.SafeInt(reader["al_id"]);
            this.AccountID = Preparer.SafeInt(reader["a_id"]);
            this.AdminID = Preparer.SafeInt(reader["admin_id"]);
            this.ChangeDesc = Preparer.SafeString(reader["change_desc"]);
            this.Created = Preparer.SafeDateTime(reader["al_created"]);
            this.Modified = Preparer.SafeDateTime(reader["al_modified"]);

            this.OnLoad(reader);
        }

    }

    public class AccountLogs : Queryable
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phreezer"></param>
        public AccountLogs(Phreezer phreezer)
            : base(phreezer)
        {
        }

        /// <summary>
        /// returns the type of object that this will store
        /// </summary>
        /// <returns></returns>
        public override System.Type GetObjectType()
        {
            return typeof(AccountLog);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        public override void Consume(MySqlDataReader reader)
        {
            while (reader.Read())
            {
                this.Add(new AccountLog(this.phreezer, reader));
            }
        }
    }

    public partial class AccountLog
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        protected override string GetSelectSql(object pk)
        {
            // load the AccountLog
            return "select * from Account_log al where al_id = '" + pk.ToString() + "'";
        }

        /// <summary>
        /// Persists updates to the DB.
        /// </summary>
        /// <returns></returns>
        protected override string GetUpdateSql()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("update `Account_log` set");
            sb.Append("  a_id = '" + Preparer.Escape(this.AccountID) + "'");
            sb.Append(" ,admin_id = '" + Preparer.Escape(this.AdminID) + "'");
            sb.Append(" ,change_desc = '" + Preparer.Escape(this.ChangeDesc) + "'");
            sb.Append(" ,al_modified = sysdate()");
            sb.Append(" where al_id = '" + Preparer.Escape(this.Id) + "'");

            return sb.ToString();
        }

        /// <summary>
        /// Inserts into the DB.
        /// </summary>
        /// <returns></returns>
        protected override string GetInsertSql()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into `Account_log` (");
            sb.Append("  a_id");
            sb.Append(" ,admin_id");
            sb.Append(" ,change_desc");
            sb.Append(" ,al_modified");
            sb.Append(" ,al_created");
            sb.Append(" ) values (");
            sb.Append("  '" + Preparer.Escape(this.AccountID) + "'");
            sb.Append(" ,'" + Preparer.Escape(this.AdminID) + "'");
            sb.Append(" ,'" + Preparer.Escape(this.ChangeDesc) + "'");
            sb.Append(" ,sysdate()");
            sb.Append(" ,sysdate()");
            sb.Append(" )");

            return sb.ToString();
        }
    }


}