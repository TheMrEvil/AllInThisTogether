using System;

namespace System.Security.Permissions
{
	/// <summary>Specifies whether a permission should have all or no access to resources at creation.</summary>
	// Token: 0x0200042D RID: 1069
	public enum PermissionState
	{
		/// <summary>No access to the resource protected by the permission.</summary>
		// Token: 0x04001FEC RID: 8172
		None,
		/// <summary>Full access to the resource protected by the permission.</summary>
		// Token: 0x04001FED RID: 8173
		Unrestricted
	}
}
