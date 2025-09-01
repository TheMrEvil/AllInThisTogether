using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the access control rights that can be applied to named system mutex objects.</summary>
	// Token: 0x02000533 RID: 1331
	[Flags]
	public enum MutexRights
	{
		/// <summary>The right to release a named mutex.</summary>
		// Token: 0x040024B1 RID: 9393
		Modify = 1,
		/// <summary>The right to delete a named mutex.</summary>
		// Token: 0x040024B2 RID: 9394
		Delete = 65536,
		/// <summary>The right to open and copy the access rules and audit rules for a named mutex.</summary>
		// Token: 0x040024B3 RID: 9395
		ReadPermissions = 131072,
		/// <summary>The right to change the security and audit rules associated with a named mutex.</summary>
		// Token: 0x040024B4 RID: 9396
		ChangePermissions = 262144,
		/// <summary>The right to change the owner of a named mutex.</summary>
		// Token: 0x040024B5 RID: 9397
		TakeOwnership = 524288,
		/// <summary>The right to wait on a named mutex.</summary>
		// Token: 0x040024B6 RID: 9398
		Synchronize = 1048576,
		/// <summary>The right to exert full control over a named mutex, and to modify its access rules and audit rules.</summary>
		// Token: 0x040024B7 RID: 9399
		FullControl = 2031617
	}
}
