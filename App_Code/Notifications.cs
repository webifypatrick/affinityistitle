using MySql.Data.MySqlClient;
using Com.VerySimple.Phreeze;

namespace Affinity
{
    /// <summary>
    /// Used for querying and storing a collection of HUDLog objects
    /// </summary>
    public class Notifications : Queryable
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phreezer"></param>
        public Notifications(Phreezer phreezer)
            : base(phreezer)
        {
        }

        /// <summary>
        /// returns the type of object that this will store
        /// </summary>
        /// <returns></returns>
        public override System.Type GetObjectType()
        {
            return typeof(Notification);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        public override void Consume(MySqlDataReader reader)
        {
            while (reader.Read())
            {
                this.Add(new Notification(this.phreezer, reader));
            }
        }
    }
}