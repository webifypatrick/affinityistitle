using MySql.Data.MySqlClient;
using System.Text;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// This code is automatically generated from the AttachmentRole schema and
	/// should not be edited if at all possible.  All customizations and 
	/// business logic should be added to the partial class AttachmentRole.cs
	/// </summary>
	public partial class AttachmentRole : Loadable
	{


		public AttachmentRole(Phreezer phreezer) : base(phreezer) { }
		public AttachmentRole(Phreezer phreezer, MySqlDataReader reader) : base(phreezer,reader) { }

		/* ~~~ PUBLIC PROPERTIES ~~~ */


		private string _rolecode = "";
		public string RoleCode
		{
			get { return this._rolecode; }
			set { this._rolecode = value; }
		}

		private string _attachmentpurposecode = "";
		public string AttachmentPurposeCode
		{
			get { return this._attachmentpurposecode; }
			set { this._attachmentpurposecode = value; }
		}

		private string _roledescription = "";
		public string RoleDescription
		{
			get { return this._roledescription; }
			set { this._roledescription = value; }
		}

		private string _attachmentpurposedescription = "";
		public string AttachmentPurposeDescription
		{
			get { return this._attachmentpurposedescription; }
			set { this._attachmentpurposedescription = value; }
		}

		/* ~~~ CONSTRAINTS ~~~ */


		/* ~~~ SETS ~~~ */

		/// <summary>
		/// Returns a collection of AttachmentRole objects
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns>Accounts</returns>
		public AttachmentRoles GetAttachmentRoles(AttachmentRolesCriteria criteria)
		{
			Phreezer p = new Phreezer(System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString);
			AttachmentRoles attachmentRoles = new AttachmentRoles(p);
            try { 
			    attachmentRoles.Query(criteria);
            }
            catch (System.Exception ex)
            {
                log4net.LogManager.GetLogger("Application_Error").Error("AttachmentRolesDAO.cs:GetAttachmentRoles: " + ex.Message);
            }
            finally
            {
                p.Close();
            }
            return attachmentRoles;
		}


		/* ~~~ CRUD OPERATIONS ~~~ */

		/// <summary>
		/// Assigns a value to the primary key
		/// </summary>
		/// <param name="key"></param>
		protected override void SetPrimaryKey(object key)
		{
		   this.AttachmentPurposeCode = (string)key;
		}

		/// <summary>
		/// Returns a SQL statement to select this object from the DB
		/// </summary>
		/// <param name="primaryKey"></param>
		/// <returns></returns>
		protected override string GetSelectSql(object pk)
		{
			return "select * from `attachment_roles_descriptions` ard where ard.ap_code = '" + pk.ToString() + "'";
		}

		/// <summary>
		/// Returns an SQL statement to update this object in the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetUpdateSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("update `attachment_roles` set");
			sb.Append("  r_code= '" + Preparer.Escape(this.RoleCode) + "'");
			sb.Append(" where ap_code = '" + Preparer.Escape(this.AttachmentPurposeCode) + "'");
			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to insert this object into the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetInsertSql()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("insert into `attachment_roles` (");
			sb.Append("  ap_code");
			sb.Append(" ,r_code");
			sb.Append(" ) values (");
			sb.Append("  '" + Preparer.Escape(this.AttachmentPurposeCode) + "'");
			sb.Append(" ,'" + Preparer.Escape(this.RoleCode) + "'");
			sb.Append(" )");

			return sb.ToString();
		}

		/// <summary>
		/// Returns an SQL statement to delete this object from the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetDeleteSql()
		{
			return "delete from `attachment_roles` where ap_code = '" + Preparer.Escape(this.AttachmentPurposeCode) + "' and r_code = '" + Preparer.Escape(this.RoleCode) + "'";
		}

		/// <summary>
		/// reads the column values from a datareader and populates the object properties
		/// </summary>
		/// <param name="reader"></param>
		public override void Load(MySqlDataReader reader)
		{
			this.RoleCode = Preparer.SafeString(reader["ap_code"]);
			this.RoleDescription = Preparer.SafeString(reader["r_description"]);
			this.AttachmentPurposeCode = Preparer.SafeString(reader["ap_code"]);
			this.AttachmentPurposeDescription = Preparer.SafeString(reader["ap_description"]);

			this.OnLoad(reader);
		}

	}
}