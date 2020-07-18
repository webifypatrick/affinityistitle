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
	/// Business Logic For UploadLog Class
	/// </summary>
	public partial class UploadLog
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="primaryKey"></param>
		/// <returns></returns>
		protected override string GetSelectSql(object pk)
		{
			// load the UploadLog
			return "select * from upload_log ul where ul_id = '" + pk.ToString() + "'";
		}

		/// <summary>
		/// Persists updates to the DB.
		/// </summary>
		/// <returns></returns>
		protected override string GetUpdateSql()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("update `upload_log` set");
			sb.Append("  a_id = '" + Preparer.Escape(this.AccountID) + "'");
			sb.Append(" ,ua_id = '" + Preparer.Escape(this.UploadAccountID) + "'");
			sb.Append(" ,o_id = '" + Preparer.Escape(this.OrderID) + "'");
			sb.Append(" ,r_id = '" + Preparer.Escape(this.RequestID) + "'");
			sb.Append(" ,att_id = '" + Preparer.Escape(this.AttachmentID) + "'");
			sb.Append(" ,ul_modified = sysdate()");
			sb.Append(" where ul_id = '" + Preparer.Escape(this.Id) + "'");

			return sb.ToString();
		}

		/// <summary>
		/// Inserts into the DB.
		/// </summary>
		/// <returns></returns>
		protected override string GetInsertSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("insert into `upload_log` (");
			sb.Append("  a_id");
			sb.Append(" ,ua_id");
			sb.Append(" ,o_id");
			sb.Append(" ,r_id");
			sb.Append(" ,att_id");
			sb.Append(" ,ul_modified");
			sb.Append(" ,ul_created");
			sb.Append(" ) values (");
			sb.Append("  '" + Preparer.Escape(this.AccountID) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.UploadAccountID) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.OrderID) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.RequestID) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.AttachmentID) + "'");
			sb.Append(" ,sysdate()");
			sb.Append(" ,sysdate()");
			sb.Append(" )");

			return sb.ToString();
		}
	}
}