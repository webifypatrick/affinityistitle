using System.Collections;
using System.Text;
using MySql.Data.MySqlClient;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// Used for querying and storing a collection of Underwriter objects
	/// </summary>
	public class TitleFees : Queryable
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="phreezer"></param>
        public TitleFees(Phreezer phreezer)
			: base(phreezer)
		{
		}

		/// <summary>
		/// returns the type of object that this will store
		/// </summary>
		/// <returns></returns>
		public override System.Type GetObjectType()
		{
			return typeof(TitleFee);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="reader"></param>
		public override void Consume(MySqlDataReader reader)
		{
			while (reader.Read())
			{
				this.Add(new TitleFee(this.phreezer, reader));
			}
		}
	}
}