using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the inheritance and auditing behavior of an access control entry (ACE).</summary>
	// Token: 0x02000506 RID: 1286
	[Flags]
	public enum AceFlags : byte
	{
		/// <summary>No ACE flags are set.</summary>
		// Token: 0x04002412 RID: 9234
		None = 0,
		/// <summary>The access mask is propagated onto child leaf objects.</summary>
		// Token: 0x04002413 RID: 9235
		ObjectInherit = 1,
		/// <summary>The access mask is propagated to child container objects.</summary>
		// Token: 0x04002414 RID: 9236
		ContainerInherit = 2,
		/// <summary>The access checks do not apply to the object; they only apply to its children.</summary>
		// Token: 0x04002415 RID: 9237
		NoPropagateInherit = 4,
		/// <summary>The access mask is propagated only to child objects. This includes both container and leaf child objects.</summary>
		// Token: 0x04002416 RID: 9238
		InheritOnly = 8,
		/// <summary>A logical <see langword="OR" /> of <see cref="F:System.Security.AccessControl.AceFlags.ObjectInherit" />, <see cref="F:System.Security.AccessControl.AceFlags.ContainerInherit" />, <see cref="F:System.Security.AccessControl.AceFlags.NoPropagateInherit" />, and <see cref="F:System.Security.AccessControl.AceFlags.InheritOnly" />.</summary>
		// Token: 0x04002417 RID: 9239
		InheritanceFlags = 15,
		/// <summary>An ACE is inherited from a parent container rather than being explicitly set for an object.</summary>
		// Token: 0x04002418 RID: 9240
		Inherited = 16,
		/// <summary>Successful access attempts are audited.</summary>
		// Token: 0x04002419 RID: 9241
		SuccessfulAccess = 64,
		/// <summary>Failed access attempts are audited.</summary>
		// Token: 0x0400241A RID: 9242
		FailedAccess = 128,
		/// <summary>All access attempts are audited.</summary>
		// Token: 0x0400241B RID: 9243
		AuditFlags = 192
	}
}
