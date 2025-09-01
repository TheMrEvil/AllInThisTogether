using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies the padding mode to use with RSA signature creation or verification operations.</summary>
	// Token: 0x02000476 RID: 1142
	public enum RSASignaturePaddingMode
	{
		/// <summary>PKCS #1 v1.5</summary>
		// Token: 0x04002127 RID: 8487
		Pkcs1,
		/// <summary>Probabilistic Signature Scheme</summary>
		// Token: 0x04002128 RID: 8488
		Pss
	}
}
