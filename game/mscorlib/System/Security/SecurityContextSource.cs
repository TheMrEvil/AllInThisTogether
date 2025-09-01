using System;

namespace System.Security
{
	/// <summary>Identifies the source for the security context.</summary>
	// Token: 0x020003D9 RID: 985
	public enum SecurityContextSource
	{
		/// <summary>The current application domain is the source for the security context.</summary>
		// Token: 0x04001EA9 RID: 7849
		CurrentAppDomain,
		/// <summary>The current assembly is the source for the security context.</summary>
		// Token: 0x04001EAA RID: 7850
		CurrentAssembly
	}
}
