using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies the cryptographic operations that a Cryptography Next Generation (CNG) key may be used with. </summary>
	// Token: 0x02000051 RID: 81
	[Flags]
	public enum CngKeyUsages
	{
		/// <summary>No usage values are assigned to the key.</summary>
		// Token: 0x04000340 RID: 832
		None = 0,
		/// <summary>The key can be used for encryption and decryption.</summary>
		// Token: 0x04000341 RID: 833
		Decryption = 1,
		/// <summary>The key can be used for signing and verification.</summary>
		// Token: 0x04000342 RID: 834
		Signing = 2,
		/// <summary>The key can be used for secret agreement generation and key exchange.</summary>
		// Token: 0x04000343 RID: 835
		KeyAgreement = 4,
		/// <summary>The key can be used for all purposes.</summary>
		// Token: 0x04000344 RID: 836
		AllUsages = 16777215
	}
}
