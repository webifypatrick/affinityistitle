using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using MySql.Data.MySqlClient;
using Com.VerySimple.Phreeze;
using Com.VerySimple.Util;
using System.Text;

namespace Affinity
{
	/// <summary>
	/// Business Logic For hudLog Class
	/// </summary>
	public partial class HUDLog
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="primaryKey"></param>
		/// <returns></returns>
		protected override string GetSelectSql(object pk)
		{
			// load the hudLog
			return "select * from hud_log ul where h_id = '" + pk.ToString() + "'";
		}

		/// <summary>
		/// Persists updates to the DB.
		/// </summary>
		/// <returns></returns>
		protected override string GetUpdateSql()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("update `hud_log` set");
			sb.Append("  a_id = '" + Preparer.Escape(this.AccountID) + "'");
			sb.Append(" ,h_submission_xml = '" + Preparer.Escape(this.SubmissionXML) + "'");
			sb.Append(" ,h_modified = sysdate()");
			sb.Append(" where h_id = '" + Preparer.Escape(this.Id) + "'");

			return sb.ToString();
		}

		/// <summary>
		/// Inserts into the DB.
		/// </summary>
		/// <returns></returns>
		protected override string GetInsertSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("insert into `hud_log` (");
			sb.Append("  a_id");
			sb.Append(" ,h_submission_xml");
			sb.Append(" ,h_modified");
			sb.Append(" ,h_created");
			sb.Append(" ) values (");
			sb.Append("  '" + Preparer.Escape(this.AccountID) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.SubmissionXML) + "'");
			sb.Append(" ,sysdate()");
			sb.Append(" ,sysdate()");
			sb.Append(" )");

			return sb.ToString();
		}
	}
}