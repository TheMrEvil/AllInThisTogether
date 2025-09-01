using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies the padding mode to use with RSA encryption or decryption operations.</summary>
	// Token: 0x02000474 RID: 1140
	public enum RSAEncryptionPaddingMode
	{
		/// <summary>PKCS #1 v1.5.</summary>
		// Token: 0x04002121 RID: 8481
		Pkcs1,
		/// <summary>Optimal Asymmetric Encryption Padding.</summary>
		// Token: 0x04002122 RID: 8482
		Oaep
	}
}
