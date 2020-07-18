using Com.VerySimple.Phreeze;
using System.Text;

namespace Affinity
{
	/// <summary>
	/// Business Logic For AccountLog Class
	/// </summary>
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