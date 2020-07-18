using MySql.Data.MySqlClient;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Used for querying and storing a collection of AccountLog objects
	/// </summary>
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
}