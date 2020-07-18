using System;
using MySql.Data.MySqlClient;
using System.Text;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
	/// <summary>
	/// This code is automatically generated from the Account schema and
	/// should not be edited if at all possible.  All customizations and 
	/// business logic should be added to the partial class Account.cs
	/// </summary>
	public partial class SiteContentByRole : Loadable
	{


		public SiteContentByRole(Phreezer phreezer) : base(phreezer) { }
		public SiteContentByRole(Phreezer phreezer, MySqlDataReader reader) : base(phreezer,reader) { }

		/* ~~~ PUBLIC PROPERTIES ~~~ */


		private int _id = 0;
		public int Id
		{
			get { return this._id; }
			set { this._id = value; }
		}

		private string _roleCode = "";
		public string RoleCode
		{
			get { return this._roleCode; }
			set { this._roleCode = value; }
		}

		private Role _accountRole;
		public Role Role
		{
			get
			{
				if (this._accountRole == null)
				{
					this._accountRole = new Role(this.phreezer);
					if (this.RoleCode == Role.AnonymousCode)
					{
						// there's no point in going to the database for this Role
						this._accountRole.Code = Role.AnonymousCode;
						this._accountRole.Description = Role.AnonymousCode;
						this._accountRole.PermissionBit = 0;
					}
					else
					{
						this._accountRole.Load(this.RoleCode);
					}
				}
				return this._accountRole;
			}
			set { this._accountRole = value; }
		}

		/* ~~~ CRUD OPERATIONS ~~~ */

		/// <summary>
		/// Assigns a value to the primary key
		/// </summary>
		/// <param name="key"></param>
		protected override void SetPrimaryKey(object key)
		{
		   this.Id = (int)key;
		}


		/// <summary>
		/// Returns an SQL statement to delete this object from the DB
		/// </summary>
		/// <returns></returns>
		protected override string GetDeleteSql()
		{
			return "delete from `site_content_roles` where scr_id = '" + Id.ToString() + "'";
		}

		/// <summary>
		/// reads the column values from a datareader and populates the object properties
		/// </summary>
		/// <param name="reader"></param>
		public override void Load(MySqlDataReader reader)
		{
			this.Id = Preparer.SafeInt(reader["scr_id"]);
			this.RoleCode = Preparer.SafeString(reader["scr_role_code"]);

			this.OnLoad(reader);
		}

	}
}