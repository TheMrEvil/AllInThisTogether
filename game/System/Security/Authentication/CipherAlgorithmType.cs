using System;

namespace System.Security.Authentication
{
	/// <summary>Defines the possible cipher algorithms for the <see cref="T:System.Net.Security.SslStream" /> class.</summary>
	// Token: 0x0200029C RID: 668
	public enum CipherAlgorithmType
	{
		/// <summary>No encryption algorithm is used.</summary>
		// Token: 0x04000BD1 RID: 3025
		None,
		/// <summary>No encryption is used with a Null cipher algorithm.</summary>
		// Token: 0x04000BD2 RID: 3026
		Null = 24576,
		/// <summary>The Advanced Encryption Standard (AES) algorithm.</summary>
		// Token: 0x04000BD3 RID: 3027
		Aes = 26129,
		/// <summary>The Advanced Encryption Standard (AES) algorithm with a 128 bit key.</summary>
		// Token: 0x04000BD4 RID: 3028
		Aes128 = 26126,
		/// <summary>The Advanced Encryption Standard (AES) algorithm with a 192 bit key.</summary>
		// Token: 0x04000BD5 RID: 3029
		Aes192,
		/// <summary>The Advanced Encryption Standard (AES) algorithm with a 256 bit key.</summary>
		// Token: 0x04000BD6 RID: 3030
		Aes256,
		/// <summary>The Data Encryption Standard (DES) algorithm.</summary>
		// Token: 0x04000BD7 RID: 3031
		Des = 26113,
		/// <summary>Rivest's Code 2 (RC2) algorithm.</summary>
		// Token: 0x04000BD8 RID: 3032
		Rc2,
		/// <summary>Rivest's Code 4 (RC4) algorithm.</summary>
		// Token: 0x04000BD9 RID: 3033
		Rc4 = 26625,
		/// <summary>The Triple Data Encryption Standard (3DES) algorithm.</summary>
		// Token: 0x04000BDA RID: 3034
		TripleDes = 26115
	}
}
