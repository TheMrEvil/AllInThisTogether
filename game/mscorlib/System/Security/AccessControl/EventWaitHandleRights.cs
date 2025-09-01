using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the access control rights that can be applied to named system event objects.</summary>
	// Token: 0x02000525 RID: 1317
	[Flags]
	public enum EventWaitHandleRights
	{
		/// <summary>The right to set or reset the signaled state of a named event.</summary>
		// Token: 0x04002486 RID: 9350
		Modify = 2,
		/// <summary>The right to delete a named event.</summary>
		// Token: 0x04002487 RID: 9351
		Delete = 65536,
		/// <summary>The right to open and copy the access rules and audit rules for a named event.</summary>
		// Token: 0x04002488 RID: 9352
		ReadPermissions = 131072,
		/// <summary>The right to change the security and audit rules associated with a named event.</summary>
		// Token: 0x04002489 RID: 9353
		ChangePermissions = 262144,
		/// <summary>The right to change the owner of a named event.</summary>
		// Token: 0x0400248A RID: 9354
		TakeOwnership = 524288,
		/// <summary>The right to wait on a named event.</summary>
		// Token: 0x0400248B RID: 9355
		Synchronize = 1048576,
		/// <summary>The right to exert full control over a named event, and to modify its access rules and audit rules.</summary>
		// Token: 0x0400248C RID: 9356
		FullControl = 2031619
	}
}
