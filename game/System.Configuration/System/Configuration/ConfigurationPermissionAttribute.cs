using System;
using System.Security;
using System.Security.Permissions;

namespace System.Configuration
{
	/// <summary>Creates a <see cref="T:System.Configuration.ConfigurationPermission" /> object that either grants or denies the marked target permission to access sections of configuration files.</summary>
	// Token: 0x02000028 RID: 40
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class ConfigurationPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationPermissionAttribute" /> class.</summary>
		/// <param name="action">The security action represented by an enumeration member of <see cref="T:System.Security.Permissions.SecurityAction" />. Determines the permission state of the attribute.</param>
		// Token: 0x06000162 RID: 354 RVA: 0x000062BF File Offset: 0x000044BF
		public ConfigurationPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Creates and returns an object that implements the <see cref="T:System.Security.IPermission" /> interface.</summary>
		/// <returns>An object that implements <see cref="T:System.Security.IPermission" />.</returns>
		// Token: 0x06000163 RID: 355 RVA: 0x000062C8 File Offset: 0x000044C8
		public override IPermission CreatePermission()
		{
			return new ConfigurationPermission(base.Unrestricted ? PermissionState.Unrestricted : PermissionState.None);
		}
	}
}
