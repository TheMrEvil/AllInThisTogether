using System;

namespace System.Security.AccessControl
{
	/// <summary>These flags affect the security descriptor behavior.</summary>
	// Token: 0x0200051A RID: 1306
	[Flags]
	public enum ControlFlags
	{
		/// <summary>No control flags.</summary>
		// Token: 0x04002461 RID: 9313
		None = 0,
		/// <summary>Specifies that the owner <see cref="T:System.Security.Principal.SecurityIdentifier" /> was obtained by a defaulting mechanism. Set by resource managers only; should not be set by callers.</summary>
		// Token: 0x04002462 RID: 9314
		OwnerDefaulted = 1,
		/// <summary>Specifies that the group <see cref="T:System.Security.Principal.SecurityIdentifier" /> was obtained by a defaulting mechanism. Set by resource managers only; should not be set by callers.</summary>
		// Token: 0x04002463 RID: 9315
		GroupDefaulted = 2,
		/// <summary>Specifies that the DACL is not <see langword="null" />. Set by resource managers or users.</summary>
		// Token: 0x04002464 RID: 9316
		DiscretionaryAclPresent = 4,
		/// <summary>Specifies that the DACL was obtained by a defaulting mechanism. Set by resource managers only.</summary>
		// Token: 0x04002465 RID: 9317
		DiscretionaryAclDefaulted = 8,
		/// <summary>Specifies that the SACL is not <see langword="null" />. Set by resource managers or users.</summary>
		// Token: 0x04002466 RID: 9318
		SystemAclPresent = 16,
		/// <summary>Specifies that the SACL was obtained by a defaulting mechanism. Set by resource managers only.</summary>
		// Token: 0x04002467 RID: 9319
		SystemAclDefaulted = 32,
		/// <summary>Ignored.</summary>
		// Token: 0x04002468 RID: 9320
		DiscretionaryAclUntrusted = 64,
		/// <summary>Ignored.</summary>
		// Token: 0x04002469 RID: 9321
		ServerSecurity = 128,
		/// <summary>Ignored.</summary>
		// Token: 0x0400246A RID: 9322
		DiscretionaryAclAutoInheritRequired = 256,
		/// <summary>Ignored.</summary>
		// Token: 0x0400246B RID: 9323
		SystemAclAutoInheritRequired = 512,
		/// <summary>Specifies that the Discretionary Access Control List (DACL) has been automatically inherited from the parent. Set by resource managers only.</summary>
		// Token: 0x0400246C RID: 9324
		DiscretionaryAclAutoInherited = 1024,
		/// <summary>Specifies that the System Access Control List (SACL) has been automatically inherited from the parent. Set by resource managers only.</summary>
		// Token: 0x0400246D RID: 9325
		SystemAclAutoInherited = 2048,
		/// <summary>Specifies that the resource manager prevents auto-inheritance. Set by resource managers or users.</summary>
		// Token: 0x0400246E RID: 9326
		DiscretionaryAclProtected = 4096,
		/// <summary>Specifies that the resource manager prevents auto-inheritance. Set by resource managers or users.</summary>
		// Token: 0x0400246F RID: 9327
		SystemAclProtected = 8192,
		/// <summary>Specifies that the contents of the Reserved field are valid.</summary>
		// Token: 0x04002470 RID: 9328
		RMControlValid = 16384,
		/// <summary>Specifies that the security descriptor binary representation is in the self-relative format.  This flag is always set.</summary>
		// Token: 0x04002471 RID: 9329
		SelfRelative = 32768
	}
}
