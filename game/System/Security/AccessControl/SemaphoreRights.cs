using System;
using System.Runtime.InteropServices;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the access control rights that can be applied to named system semaphore objects.</summary>
	// Token: 0x02000290 RID: 656
	[Flags]
	[ComVisible(false)]
	public enum SemaphoreRights
	{
		/// <summary>The right to release a named semaphore.</summary>
		// Token: 0x04000BAE RID: 2990
		Modify = 2,
		/// <summary>The right to delete a named semaphore.</summary>
		// Token: 0x04000BAF RID: 2991
		Delete = 65536,
		/// <summary>The right to open and copy the access rules and audit rules for a named semaphore.</summary>
		// Token: 0x04000BB0 RID: 2992
		ReadPermissions = 131072,
		/// <summary>The right to change the security and audit rules associated with a named semaphore.</summary>
		// Token: 0x04000BB1 RID: 2993
		ChangePermissions = 262144,
		/// <summary>The right to change the owner of a named semaphore.</summary>
		// Token: 0x04000BB2 RID: 2994
		TakeOwnership = 524288,
		/// <summary>The right to wait on a named semaphore.</summary>
		// Token: 0x04000BB3 RID: 2995
		Synchronize = 1048576,
		/// <summary>The right to exert full control over a named semaphore, and to modify its access rules and audit rules.</summary>
		// Token: 0x04000BB4 RID: 2996
		FullControl = 2031619
	}
}
