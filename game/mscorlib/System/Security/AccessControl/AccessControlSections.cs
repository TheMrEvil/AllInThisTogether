using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies which sections of a security descriptor to save or load.</summary>
	// Token: 0x02000501 RID: 1281
	[Flags]
	public enum AccessControlSections
	{
		/// <summary>No sections.</summary>
		// Token: 0x04002405 RID: 9221
		None = 0,
		/// <summary>The system access control list (SACL).</summary>
		// Token: 0x04002406 RID: 9222
		Audit = 1,
		/// <summary>The discretionary access control list (DACL).</summary>
		// Token: 0x04002407 RID: 9223
		Access = 2,
		/// <summary>The owner.</summary>
		// Token: 0x04002408 RID: 9224
		Owner = 4,
		/// <summary>The primary group.</summary>
		// Token: 0x04002409 RID: 9225
		Group = 8,
		/// <summary>The entire security descriptor.</summary>
		// Token: 0x0400240A RID: 9226
		All = 15
	}
}
