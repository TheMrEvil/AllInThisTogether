using System;

namespace System.Security.Permissions
{
	/// <summary>Defines permission settings for type descriptors.</summary>
	// Token: 0x02000293 RID: 659
	[Flags]
	[Serializable]
	public enum TypeDescriptorPermissionFlags
	{
		/// <summary>No permission flags are set on the type descriptor.</summary>
		// Token: 0x04000BB6 RID: 2998
		NoFlags = 0,
		/// <summary>The type descriptor may be called from partially trusted code.</summary>
		// Token: 0x04000BB7 RID: 2999
		RestrictedRegistrationAccess = 1
	}
}
