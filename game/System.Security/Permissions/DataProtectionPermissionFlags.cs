using System;

namespace System.Security.Permissions
{
	/// <summary>Specifies the access permissions for encrypting data and memory.</summary>
	// Token: 0x0200000B RID: 11
	[Flags]
	[Serializable]
	public enum DataProtectionPermissionFlags
	{
		/// <summary>No protection abilities.</summary>
		// Token: 0x0400007A RID: 122
		NoFlags = 0,
		/// <summary>The ability to encrypt data.</summary>
		// Token: 0x0400007B RID: 123
		ProtectData = 1,
		/// <summary>The ability to unencrypt data.</summary>
		// Token: 0x0400007C RID: 124
		UnprotectData = 2,
		/// <summary>The ability to encrypt memory.</summary>
		// Token: 0x0400007D RID: 125
		ProtectMemory = 4,
		/// <summary>The ability to unencrypt memory.</summary>
		// Token: 0x0400007E RID: 126
		UnprotectMemory = 8,
		/// <summary>The ability to encrypt data, encrypt memory, unencrypt data, and unencrypt memory.</summary>
		// Token: 0x0400007F RID: 127
		AllFlags = 15
	}
}
