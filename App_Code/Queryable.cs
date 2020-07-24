using System;
using System.Reflection;
using MySql.Data.MySqlClient;


namespace Com.VerySimple.Phreeze
{
    /// <summary>
    /// Summary description for Dao
    /// </summary>
    public abstract class Queryable : System.Collections.ArrayList
    {

        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger("Com.VerySimple.Phreeze.Queryable");

		private Criteria lastCriteria;
        protected Phreezer phreezer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phreezer"></param>
        protected Queryable(Phreezer phreezer)
        {
            this.phreezer = phreezer;
        }

		/// <summary>
		/// returns the total number of rows that this query would return,
		/// ignoring any limit statements (ie paging)
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns></returns>
		public int GetQueryRowCount(Criteria criteria)
		{
            //log.Debug(criteria.GetCountSql());
            int counter = 0;

            MySqlDataReader reader = this.GetReader(criteria.GetCountSql());
            if (reader.Read())
            {
                counter = Preparer.SafeInt(reader["counter"]);
            }
			reader.Close();
			return counter;

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="criteria"></param>
		public void Query(Criteria criteria)
		{
			//log.Debug(criteria.GetSql());
			this.lastCriteria = criteria;

			MySqlDataReader reader = this.GetReader(criteria.GetSql());
			this.Consume(reader);
			if (reader != null) reader.Close();

			// if we are using paging, fill up the spaces with null.
			// this sucks, but it's the most simple way to do it with the gridview
			if (criteria.PagingEnabled)
			{

				int start = (criteria.PageSize * criteria.Page) - criteria.PageSize;

				// pad the beginning
				for (int x = 0; x < start; x++)
				{
					this.Insert(0, new PageShim());
				}

				// pad the end
				if (criteria.Page == 1 && this.Count < criteria.PageSize)
				{
					// if we're on page one and there's not enough to fill up
					// a full page, we don't need to go out and see if there's any more
				}
				else
				{
                    // we have a full page and/or are past page one, so we need to
                    // get the total record count
                    int total = this.GetQueryRowCount(criteria);
                    //int total = 10;

                    while (this.Count < total)
					{
						this.Add(new PageShim());
					}
				}
			}

		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract System.Type GetObjectType();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        public abstract void Consume(MySqlDataReader reader);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected MySqlDataReader GetReader(string sql)
        {
			return this.phreezer.ExecuteReader(sql);
			//MySqlConnection conn = this.phreezer.GetConnection();
            //MySqlCommand cmd = new MySqlCommand(sql, conn);
            //return cmd.ExecuteReader();
        }
    }

}