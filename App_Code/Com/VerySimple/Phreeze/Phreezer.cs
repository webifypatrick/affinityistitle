using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections;
using System.IO;
using System.Reflection;

namespace Com.VerySimple.Phreeze
{
    /// <summary>
    /// Summary description for Phreezer
    /// </summary>
    public class Phreezer
    {
        private string connectionString;
		private ArrayList openReaders = new ArrayList();

        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger("Com.VerySimple.Phreeze.Phreezer");

        public Phreezer(string connString)
        {
            this.connectionString = connString;
        }

        private MySqlConnection conn_cache;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MySqlConnection GetConnection()
        {
			if (this.conn_cache == null)
			{
				// initialize the database connection, but don't open it at this point
				log.Debug("GetConnection: Initializing connection");
				this.conn_cache = new MySqlConnection(this.connectionString);
			}
			else
			{
				log.Debug("GetConnection: Returning cached connection to " + this.conn_cache.Database + " [" + this.conn_cache.GetHashCode() + "]");
			}

            this.Open();

            return this.conn_cache;

        }

        /// <summary>
        /// 
        /// </summary>
        public void Open()
        {
            if (this.conn_cache.State != System.Data.ConnectionState.Open)
            {
                this.conn_cache.Open();
				log.Debug("Open: Opened connection to " + this.conn_cache.Database + " [" + this.conn_cache.GetHashCode() + "]");
			}
        }

        /// <summary>
        /// Closes any open readers and closes the connection to the DB
        /// </summary>
        public void Close()
        {
			if (this.conn_cache != null && this.conn_cache.State != System.Data.ConnectionState.Closed)
			{
				foreach (MySqlDataReader r in openReaders)
				{
					// take care of any open readers that have been handed out
					if (r != null && r.IsClosed == false)
					{
						log.Debug("Close: Closing open reader");
						r.Close();
					}
					this.openReaders = new ArrayList();
				}

				log.Debug("Close: Closing connection to " + this.conn_cache.Database + " [" + this.conn_cache.GetHashCode() + "]");
				this.conn_cache.Close();
			}
			else
			{
				log.Debug("Close: Connection is already closed");
			}
        }

        /// <summary>
		/// Returns a DataReader for the given sql string.  A reference to these readers will be stored
		/// in the phreezer object and will be closed when close is called
		/// </summary>
        /// <param name="sql">sql to execute</param>
        /// <returns></returns>
        public MySqlDataReader ExecuteReader(string sql)
        {
			log.Debug("ExecuteReader(sql): <div class='sql'>" + Preparer.Encode4Log(sql) + "</div>");
            MySqlCommand cmd = new MySqlCommand(sql, this.GetConnection());

			try
			{
				MySqlDataReader r = cmd.ExecuteReader();
				this.openReaders.Add(r);
				return r;
			}
			catch (Exception ex)
			{
				System.Diagnostics.StackFrame fr = new System.Diagnostics.StackFrame(1, true);
				System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(fr);
				log.Error("ExecuteReader(sql): <div class='source'>" + Preparer.Encode4Log(st.ToString()) + "</div> <div class='message'>" + Preparer.Encode4Log(ex.Message) + "</div> <div class='sql'>" + Preparer.Encode4Log(sql) + "</div>");
				throw new Exception("ExecuteReader was unable to execute the query.  Please tell the server administrator to review the error logs for details.");
			}
        }

        /// <summary>
        /// Returns a DataReader for the given sql string.  A reference to these readers will be stored
		/// in the phreezer object and will be closed when close is called
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandBehavior"></param>
        /// <returns></returns>
        public MySqlDataReader ExecuteReader(string sql, CommandBehavior commandBehavior)
        {
			log.Debug("ExecuteReader(sql,cmdb): <div class='sql'>" + Preparer.Encode4Log(sql) + "</div>");
            MySqlCommand cmd = new MySqlCommand(sql, this.GetConnection());

			try
			{
				MySqlDataReader r = cmd.ExecuteReader(commandBehavior);
				this.openReaders.Add(r);
				return r;
			}
			catch (Exception ex)
			{
				System.Diagnostics.StackFrame fr = new System.Diagnostics.StackFrame(1, true);
				System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(fr);
				log.Error("ExecuteReader(sql,cmdb): <div class='source'>" + Preparer.Encode4Log(st.ToString()) + "</div> <div class='message'>" + Preparer.Encode4Log(ex.Message) + "</div> <div class='sql'>" + Preparer.Encode4Log(sql) + "</div>");
				throw new Exception("ExecuteReader was unable to execute the query.  Please tell the server administrator to review the error logs for details.");
			}
		}

        /// <summary>
        /// Executes an insert/update/delete query and returns number of rows affected
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql)
        {
            log.Debug("ExecuteNonQuery(sql): <div class='sql'>" + Preparer.Encode4Log(sql) + "</div>");
            MySqlCommand cmd = new MySqlCommand(sql, this.GetConnection());

            try
            {
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
				System.Diagnostics.StackFrame fr = new System.Diagnostics.StackFrame(1, true);
				System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(fr);
				log.Error("ExecuteNonQuery(sql): <div class='source'>" + Preparer.Encode4Log(st.ToString()) + "</div> <div class='message'>" + Preparer.Encode4Log(ex.Message) + "</div> <div class='sql'>" + Preparer.Encode4Log(sql) + "</div>");
				throw new Exception("ExecuteNonQuery was unable to execute the query.  Please tell the server administrator to review the error logs for details.");
            }
        }

        /// <summary>
        /// Returns the last inserted autoincrement id.
        /// </summary>
        /// <returns>last autoincrement id inserted</returns>
        public int GetLastInsertId()
        {
            string sql = "select last_insert_id() as last_id";
			log.Debug("GetLastInsertId(): <div class='sql'>" + sql + "</div>");

            MySqlDataReader reader = this.ExecuteReader(sql, CommandBehavior.SingleRow);
            reader.Read();
            int last_id = int.Parse(reader["last_id"].ToString());
            reader.Close();

            return last_id;
        }

    }
}