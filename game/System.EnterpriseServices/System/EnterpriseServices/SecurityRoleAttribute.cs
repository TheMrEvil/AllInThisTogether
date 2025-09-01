using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Configures a role for an application or component. This class cannot be inherited.</summary>
	// Token: 0x02000045 RID: 69
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface, AllowMultiple = true)]
	[ComVisible(false)]
	public sealed class SecurityRoleAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.SecurityRoleAttribute" /> class and sets the <see cref="P:System.EnterpriseServices.SecurityRoleAttribute.Role" /> property.</summary>
		/// <param name="role">A security role for the application, component, interface, or method.</param>
		// Token: 0x060000F9 RID: 249 RVA: 0x0000254F File Offset: 0x0000074F
		public SecurityRoleAttribute(string role) : this(role, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.SecurityRoleAttribute" /> class and sets the <see cref="P:System.EnterpriseServices.SecurityRoleAttribute.Role" /> and <see cref="P:System.EnterpriseServices.SecurityRoleAttribute.SetEveryoneAccess" /> properties.</summary>
		/// <param name="role">A security role for the application, component, interface, or method.</param>
		/// <param name="everyone">
		///   <see langword="true" /> to require that the newly created role have the Everyone user group added as a user; otherwise, <see langword="false" />.</param>
		// Token: 0x060000FA RID: 250 RVA: 0x00002559 File Offset: 0x00000759
		public SecurityRoleAttribute(string role, bool everyone)
		{
			this.description = string.Empty;
			this.everyone = everyone;
			this.role = role;
		}

		/// <summary>Gets or sets the role description.</summary>
		/// <returns>The description for the role.</returns>
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000257A File Offset: 0x0000077A
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00002582 File Offset: 0x00000782
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		/// <summary>Gets or sets the security role.</summary>
		/// <returns>The security role applied to an application, component, interface, or method.</returns>
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000258B File Offset: 0x0000078B
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00002593 File Offset: 0x00000793
		public string Role
		{
			get
			{
				return this.role;
			}
			set
			{
				this.role = value;
			}
		}

		/// <summary>Sets a value indicating whether to add the Everyone user group as a user.</summary>
		/// <returns>
		///   <see langword="true" /> to require that a newly created role have the Everyone user group added as a user (roles that already exist on the application are not modified); otherwise, <see langword="false" /> to suppress adding the Everyone user group as a user.</returns>
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000259C File Offset: 0x0000079C
		// (set) Token: 0x06000100 RID: 256 RVA: 0x000025A4 File Offset: 0x000007A4
		public bool SetEveryoneAccess
		{
			get
			{
				return this.everyone;
			}
			set
			{
				this.everyone = value;
			}
		}

		// Token: 0x04000083 RID: 131
		private string description;

		// Token: 0x04000084 RID: 132
		private bool everyone;

		// Token: 0x04000085 RID: 133
		private string role;
	}
}
