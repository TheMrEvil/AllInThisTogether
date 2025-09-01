using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies how much of the X.509 certificate chain should be included in the X.509 data.</summary>
	// Token: 0x020002C4 RID: 708
	public enum X509IncludeOption
	{
		/// <summary>No X.509 chain information is included.</summary>
		// Token: 0x04000CA3 RID: 3235
		None,
		/// <summary>The entire X.509 chain is included except for the root certificate.</summary>
		// Token: 0x04000CA4 RID: 3236
		ExcludeRoot,
		/// <summary>Only the end certificate is included in the X.509 chain information.</summary>
		// Token: 0x04000CA5 RID: 3237
		EndCertOnly,
		/// <summary>The entire X.509 chain is included.</summary>
		// Token: 0x04000CA6 RID: 3238
		WholeChain
	}
}
