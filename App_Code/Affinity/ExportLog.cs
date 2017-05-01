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
	/// Business Logic For ExportLog Class
	/// </summary>
	public partial class ExportLog
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="primaryKey"></param>
		/// <returns></returns>
		protected override string GetSelectSql(object pk)
		{
			// load the ExportLog
			return "select * from export_log el where el_id = '" + pk.ToString() + "'";
		}

		/// <summary>
		/// Persists updates to the DB.
		/// </summary>
		/// <returns></returns>
		protected override string GetUpdateSql()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("update `export_log` set");
			sb.Append("  a_id = '" + Preparer.Escape(this.AccountID) + "'");
			sb.Append(" ,o_id = '" + Preparer.Escape(this.OrderID) + "'");
			sb.Append(" ,r_id = '" + Preparer.Escape(this.RequestID) + "'");
			sb.Append(" ,export_format = '" + Preparer.Escape(this.ExportFormat) + "'");
			sb.Append(" ,el_modified = sysdate()");
			sb.Append(" where el_id = '" + Preparer.Escape(this.Id) + "'");

			return sb.ToString();
		}

		/// <summary>
		/// Inserts into the DB.
		/// </summary>
		/// <returns></returns>
		protected override string GetInsertSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("insert into `export_log` (");
			sb.Append("  a_id");
			sb.Append(" ,o_id");
			sb.Append(" ,r_id");
			sb.Append(" ,export_format");
			sb.Append(" ,el_modified");
			sb.Append(" ,el_created");
			sb.Append(" ) values (");
			sb.Append("  '" + Preparer.Escape(this.AccountID) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.OrderID) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.RequestID) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.ExportFormat) + "'");
			sb.Append(" ,sysdate()");
			sb.Append(" ,sysdate()");
			sb.Append(" )");

			return sb.ToString();
		}
	}
}