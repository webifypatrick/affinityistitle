using System;
using System.Collections;
using Com.VerySimple.Phreeze;

namespace Affinity
{
    public enum RolePermission
    {
        SubmitOrders = 1,
        ManageOrders = 2,
        AdminSystem = 4,
        ReadOtherEmployeeOrders = 8,
        ModifyOtherEmployeeOrders = 16,
        AffinityStaff = 32,
        AffinityManager = 64,
        AttorneyServices = 128,
        BrokerAgent = 256
    }

    /// <summary>
    /// Summary description for AccountType
    /// </summary>
    public partial class Role
    {

		/// <summary>
		/// Parameterless constructor is here only so the object can be
		/// serialized for web services.  do not use this constructor!
		/// </summary>
		public Role() : base() { }

		
		public static string AnonymousCode = "Anonymous";
		public static string DefaultCode = "Attorney";

		protected override void OnInit()
		{
			this.Code = Role.AnonymousCode;
		}

        public bool IsAuthenticated()
        {
            return (this.Code != Role.AnonymousCode);
        }

        /// <summary>
        /// Returns true if this instance is allowed the specified RolePermission
        /// </summary>
        /// <param name="permission">RolePermission</param>
        /// <returns>boolean</returns>
        public bool HasPermission(RolePermission permission)
        {
            // bitwise calculation to see if account has permission
            return ((this.PermissionBit & (int)permission) > 0);
        }

    }
}