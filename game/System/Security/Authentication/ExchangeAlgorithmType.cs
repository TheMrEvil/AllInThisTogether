using System;

namespace System.Security.Authentication
{
	/// <summary>Specifies the algorithm used to create keys shared by the client and server.</summary>
	// Token: 0x0200029D RID: 669
	public enum ExchangeAlgorithmType
	{
		/// <summary>No key exchange algorithm is used.</summary>
		// Token: 0x04000BDC RID: 3036
		None,
		/// <summary>The Diffie Hellman ephemeral key exchange algorithm.</summary>
		// Token: 0x04000BDD RID: 3037
		DiffieHellman = 43522,
		/// <summary>The RSA public-key exchange algorithm.</summary>
		// Token: 0x04000BDE RID: 3038
		RsaKeyX = 41984,
		/// <summary>The RSA public-key signature algorithm.</summary>
		// Token: 0x04000BDF RID: 3039
		RsaSign = 9216
	}
}
