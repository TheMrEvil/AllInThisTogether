using System;

namespace System.Security.Permissions
{
	/// <summary>Specifies the permitted access to registry keys and values.</summary>
	// Token: 0x0200042F RID: 1071
	[Flags]
	public enum RegistryPermissionAccess
	{
		/// <summary>
		///   <see cref="F:System.Security.Permissions.RegistryPermissionAccess.Create" />, <see cref="F:System.Security.Permissions.RegistryPermissionAccess.Read" />, and <see cref="F:System.Security.Permissions.RegistryPermissionAccess.Write" /> access to registry variables. <see cref="F:System.Security.Permissions.RegistryPermissionAccess.AllAccess" /> represents multiple <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values and causes an <see cref="T:System.ArgumentException" /> when used as the <paramref name="access" /> parameter for the <see cref="M:System.Security.Permissions.RegistryPermission.GetPathList(System.Security.Permissions.RegistryPermissionAccess)" /> method, which expects a single value.</summary>
		// Token: 0x04001FF6 RID: 8182
		AllAccess = 7,
		/// <summary>Create access to registry variables.</summary>
		// Token: 0x04001FF7 RID: 8183
		Create = 4,
		/// <summary>No access to registry variables. <see cref="F:System.Security.Permissions.RegistryPermissionAccess.NoAccess" /> represents no valid <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values and causes an <see cref="T:System.ArgumentException" /> when used as the parameter for <see cref="M:System.Security.Permissions.RegistryPermission.GetPathList(System.Security.Permissions.RegistryPermissionAccess)" />, which expects a single value.</summary>
		// Token: 0x04001FF8 RID: 8184
		NoAccess = 0,
		/// <summary>Read access to registry variables.</summary>
		// Token: 0x04001FF9 RID: 8185
		Read = 1,
		/// <summary>Write access to registry variables.</summary>
		// Token: 0x04001FFA RID: 8186
		Write = 2
	}
}
