using System;
using MySql.Data.MySqlClient;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// This code is automatically generated from the AttachmentPurpose schema and
	/// should not be edited if at all possible.  All customizations and 
	/// business logic should be added to the partial class AttachmentPurpose.cs
	/// </summary>
	public partial class AttachmentPurpose : Loadable
	{


		public AttachmentPurpose(Phreezer phreezer) : base(phreezer) { }
		public AttachmentPurpose(Phreezer phreezer, MySqlDataReader reader) : base(phreezer,reader) { }

		/* ~~~ PUBLIC PROPERTIES ~~~ */


		private string _code = "";
		public string Code
		{
			get { return this._code; }
			set { this._code = value; }
		}

		private string _description = "";
		public string Description
		{
			get { return this._description; }
			set { this._description = value; }
		}

		private bool _sendNotification = false;
		public bool SendNotification
		{
			get { return this._sendNotification; }
			set { this._sendNotification = value; }
		}

		private string _changeStatusTo = "";
		public string ChangeStatusTo
		{
			get { return this._changeStatusTo; }
			set { this._changeStatusTo = value; }
		}

		private int _permissionRequired = 0;
		public int PermissionRequired
		{
			get { return this._permissionRequired; }
			set { this._permissionRequired = value; }
		}

		private string _roles = "";
		public string Roles
		{
			get { return this._roles; }
			set { this._roles = value; }
		}

		/* ~~~ CONSTRAINTS ~~~ */


		/* ~~~ SETS ~~~ */

		/// <summary>
		/// Returns a collection of Attachment objects
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns>Attachments</returns>
		public Attachments GetAttachmentPurposes(AttachmentCriteria criteria)
		{
			criteria.PurposeCode = this.Code;
			Attachments attachmentPurposes = new Attachments(this.phreezer);
			attachmentPurposes.Query(criteria);
			return attachmentPurposes;
		}


		/* ~~~ CRUD OPERATIONS ~~~ */

		/// <summary>
		/// Assigns a value to the primary key
		/// </summary>
		/// <param name="key"></param>
		protected override void SetPrimaryKey(object key)
		{
		   this.Code = (string)key;
		}

		/// <summary>
		/// Returns a SQL statement to select this object from the DB
		/// </summary>
		/// <param name="primaryKey"></param>
		/// <returns></returns>
		protected override string GetSelectSql(object pk)
		{
			return "select * from `attachment_purpose` ap where ap.ap_code = '" + pk.ToString() + "'";
		}

		/// <summary>
		/// Returns an SQL statement to update this object in the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetUpdateSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("update `attachment_purpose` set");
			sb.Append("  ap_description = '" + Preparer.Escape(this.Description) + "'");
			sb.Append(" ,ap_send_notification = '" + Preparer.Escape(this.SendNotification) + "'");
			sb.Append(" ,ap_change_status_to = '" + Preparer.Escape(this.ChangeStatusTo) + "'");
			sb.Append(" ,ap_permission_required = '" + Preparer.Escape(this.PermissionRequired) + "'");
			sb.Append(" where ap_code = '" + Preparer.Escape(this.Code) + "'");
			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to insert this object into the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetInsertSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("insert into `attachment_purpose` (");
			sb.Append("  ap_code");
			sb.Append(" ,ap_description");
			sb.Append(" ,ap_send_notification");
			sb.Append(" ,ap_change_status_to");
			sb.Append(" ,ap_permission_required");
			sb.Append(" ) values (");
			sb.Append("  '" + Preparer.Escape(this.Code) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.Description) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.SendNotification) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.ChangeStatusTo) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.PermissionRequired) + "'");
			sb.Append(" )");

			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to delete this object from the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetDeleteSql()
		{
			return "delete from `attachment_purpose` where ap_code = '" + Code.ToString() + "'";
		}

		/// <summary>
		/// reads the column values from a datareader and populates the object properties
		/// </summary>
		/// <param name="reader"></param>
		public override void Load(MySqlDataReader reader)
		{
			this.Code = Preparer.SafeString(reader["ap_code"]);
			this.Description = Preparer.SafeString(reader["ap_description"]);
			this.SendNotification = Preparer.SafeBool(reader["ap_send_notification"]);
			this.ChangeStatusTo = Preparer.SafeString(reader["ap_change_status_to"]);
			this.PermissionRequired = Preparer.SafeInt(reader["ap_permission_required"]);
			
			// Get all associated roles for this attachment purpose
			AttachmentRole ardao = new AttachmentRole(this.phreezer);
			AttachmentRolesCriteria arcrit = new AttachmentRolesCriteria();
			arcrit.AttachmentPurposeCode = reader["ap_code"].ToString();
			AttachmentRoles aroles = ardao.GetAttachmentRoles(arcrit);
			
			IEnumerator i = aroles.GetEnumerator();
			StringBuilder sb = new StringBuilder();
			
			// put roles into a comma-delimited list
			while(i.MoveNext())
			{
				AttachmentRole r = (AttachmentRole) i.Current;
				sb.Append(r.RoleDescription + ", ");
			}
			
			// trim off comma at the end
			if(sb.Length > 2)
			{
				sb.Length = sb.Length - 2;
			}
						
			this.Roles = Preparer.SafeString(sb.ToString());

			this.OnLoad(reader);
		}

	}
}