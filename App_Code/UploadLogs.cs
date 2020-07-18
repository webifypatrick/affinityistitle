using System.Collections;
using System.Text;
using MySql.Data.MySqlClient;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Used for querying and storing a collection of UploadLog objects
	/// </summary>
	public class UploadLogs : Queryable
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="phreezer"></param>
		public UploadLogs(Phreezer phreezer)
			: base(phreezer)
		{
		}

		/// <summary>
		/// returns the type of object that this will store
		/// </summary>
		/// <returns></returns>
		public override System.Type GetObjectType()
		{
			return typeof(UploadLog);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="reader"></param>
		public override void Consume(MySqlDataReader reader)
		{
			while (reader.Read())
			{
				this.Add(new UploadLog(this.phreezer, reader));
			}
		}
	}
}