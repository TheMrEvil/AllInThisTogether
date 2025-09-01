using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies the key export policies for a key. </summary>
	// Token: 0x0200004D RID: 77
	[Flags]
	public enum CngExportPolicies
	{
		/// <summary>No export policies are established. Key export is allowed without restriction.</summary>
		// Token: 0x0400032E RID: 814
		None = 0,
		/// <summary>The private key can be exported multiple times.</summary>
		// Token: 0x0400032F RID: 815
		AllowExport = 1,
		/// <summary>The private key can be exported multiple times as plaintext.</summary>
		// Token: 0x04000330 RID: 816
		AllowPlaintextExport = 2,
		/// <summary>The private key can be exported one time for archiving purposes.</summary>
		// Token: 0x04000331 RID: 817
		AllowArchiving = 4,
		/// <summary>The private key can be exported one time as plaintext.</summary>
		// Token: 0x04000332 RID: 818
		AllowPlaintextArchiving = 8
	}
}
