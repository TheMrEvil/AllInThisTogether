using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Identifies the level of trustworthiness that is assigned to the signature for a manifest.</summary>
	// Token: 0x02000061 RID: 97
	public enum TrustStatus
	{
		/// <summary>The signature was created by an explicitly distrusted publisher.</summary>
		// Token: 0x0400036D RID: 877
		Untrusted,
		/// <summary>The identity is not known and the signature is invalid. Because there is no verified signature, an identity cannot be determined.</summary>
		// Token: 0x0400036E RID: 878
		UnknownIdentity,
		/// <summary>The identity is known and the signature is valid. A valid Authenticode signature provides an identity.</summary>
		// Token: 0x0400036F RID: 879
		KnownIdentity,
		/// <summary>The signature is valid and was created by an explicitly trusted publisher.</summary>
		// Token: 0x04000370 RID: 880
		Trusted
	}
}
