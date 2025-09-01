using System;

namespace System.Security
{
	/// <summary>Specifies the default partial-trust visibility for code that is marked with the <see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" /> (APTCA) attribute.</summary>
	// Token: 0x020003D1 RID: 977
	public enum PartialTrustVisibilityLevel
	{
		/// <summary>The assembly can always be called by partial-trust code.</summary>
		// Token: 0x04001E9C RID: 7836
		VisibleToAllHosts,
		/// <summary>The assembly has been audited for partial trust, but it is not visible to partial-trust code in all hosts. To make the assembly visible to partial-trust code, add it to the <see cref="P:System.AppDomainSetup.PartialTrustVisibleAssemblies" /> property.</summary>
		// Token: 0x04001E9D RID: 7837
		NotVisibleByDefault
	}
}
