using System;
using MySql.Data.MySqlClient;

namespace Com.VerySimple.Phreeze
{

    /// <summary>
    /// Summary description for Loadable
    /// </summary>
    public abstract class Loadable
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger("Com.VerySimple.Phreeze.Loadable");
        
        protected Phreezer phreezer;
		protected virtual void OnInit() { }
		protected virtual void OnPreSave(bool isInsert) { }
		protected virtual void OnSave(bool isInsert) { }
		protected virtual void OnDelete() { }
		protected virtual void OnLoad(MySqlDataReader reader) { }

		/// <summary>
		/// 
		/// </summary>
		public Loadable()
		{
		}

		public Phreezer GetPhreezer()
		{
			return this.phreezer;
		}
		
		/// <summary>
        /// 
        /// </summary>
        /// <param name="phreezer"></param>
        public Loadable(Phreezer phreezer)
        {
            this.phreezer = phreezer;
			this.OnInit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phreezer"></param>
        /// <param name="reader"></param>
        public Loadable(Phreezer phreezer, MySqlDataReader reader)
        {
            this.phreezer = phreezer;
			this.OnInit();
            this.Load(reader);
        }

		/// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        public abstract void Load(MySqlDataReader reader);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        protected abstract void SetPrimaryKey(object key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        protected abstract string GetSelectSql(object pk);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract string GetUpdateSql();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract string GetInsertSql();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract string GetDeleteSql();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="primaryKey"></param>
        public void Load(object primaryKey)
        {
			if (this.phreezer == null)
			{
				throw new Exception("Phreezer argument was not provided on construction.  Unable to Load " + this.GetType() + " object" );
			}

            MySqlDataReader reader = this.phreezer.ExecuteReader(this.GetSelectSql(primaryKey));
            if (!reader.Read())
            {
				reader.Close();
                throw new DataMisalignedException("Record with Id " + primaryKey + " does not exist");
            }
            this.Load(reader);
            if (reader != null && reader.IsClosed == false) reader.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
			this.OnPreSave(false);
			int r = this.phreezer.ExecuteNonQuery(this.GetUpdateSql());
			this.OnSave(false);
			return r;
        }

        /// <summary>
        /// Inserts the new record and sets the primary key to the last auto_insert id.
        /// </summary>
        /// <param name="phreezer"></param>
        /// <returns>last insert id</returns>
        public int Insert()
        {
            return this.Insert(true);
		}

        /// <summary>
        /// Inserts the new record
        /// </summary>
        /// <param name="pkIsAutoIncrement">If true, sets the primary key to the last auto_insert value</param>
        /// <returns>last insert id or 0</returns>
        public int Insert(bool pkIsAutoIncrement)
        {
			this.OnPreSave(true);
			this.phreezer.ExecuteNonQuery(this.GetInsertSql());
            int newId = 0;
            if (pkIsAutoIncrement)
            {
                newId = this.phreezer.GetLastInsertId();
                this.SetPrimaryKey(newId);
            }
			this.OnSave(true);
            return newId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
			int r = this.phreezer.ExecuteNonQuery(this.GetDeleteSql());
			this.OnDelete();
			return r;
        }



    }
}