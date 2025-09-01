using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.PrincipalPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x0200044E RID: 1102
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class PrincipalPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.PrincipalPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x06002CB2 RID: 11442 RVA: 0x000A08D9 File Offset: 0x0009EAD9
		public PrincipalPermissionAttribute(SecurityAction action) : base(action)
		{
			this.authenticated = true;
		}

		/// <summary>Gets or sets a value indicating whether the current principal has been authenticated by the underlying role-based security provider.</summary>
		/// <returns>
		///   <see langword="true" /> if the current principal has been authenticated; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06002CB3 RID: 11443 RVA: 0x000A08E9 File Offset: 0x0009EAE9
		// (set) Token: 0x06002CB4 RID: 11444 RVA: 0x000A08F1 File Offset: 0x0009EAF1
		public bool Authenticated
		{
			get
			{
				return this.authenticated;
			}
			set
			{
				this.authenticated = value;
			}
		}

		/// <summary>Gets or sets the name of the identity associated with the current principal.</summary>
		/// <returns>A name to match against that provided by the underlying role-based security provider.</returns>
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06002CB5 RID: 11445 RVA: 0x000A08FA File Offset: 0x0009EAFA
		// (set) Token: 0x06002CB6 RID: 11446 RVA: 0x000A0902 File Offset: 0x0009EB02
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets membership in a specified security role.</summary>
		/// <returns>The name of a role from the underlying role-based security provider.</returns>
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06002CB7 RID: 11447 RVA: 0x000A090B File Offset: 0x0009EB0B
		// (set) Token: 0x06002CB8 RID: 11448 RVA: 0x000A0913 File Offset: 0x0009EB13
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

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.PrincipalPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.PrincipalPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06002CB9 RID: 11449 RVA: 0x000A091C File Offset: 0x0009EB1C
		public override IPermission CreatePermission()
		{
			PrincipalPermission result;
			if (base.Unrestricted)
			{
				result = new PrincipalPermission(PermissionState.Unrestricted);
			}
			else
			{
				result = new PrincipalPermission(this.name, this.role, this.authenticated);
			}
			return result;
		}

		// Token: 0x04002071 RID: 8305
		private bool authenticated;

		// Token: 0x04002072 RID: 8306
		private string name;

		// Token: 0x04002073 RID: 8307
		private string role;
	}
}
