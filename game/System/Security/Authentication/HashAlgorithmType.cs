using System;

namespace System.Security.Authentication
{
	/// <summary>Specifies the algorithm used for generating message authentication codes (MACs).</summary>
	// Token: 0x0200029E RID: 670
	public enum HashAlgorithmType
	{
		/// <summary>No hashing algorithm is used.</summary>
		// Token: 0x04000BE1 RID: 3041
		None,
		/// <summary>The Message Digest 5 (MD5) hashing algorithm.</summary>
		// Token: 0x04000BE2 RID: 3042
		Md5 = 32771,
		/// <summary>The Secure Hashing Algorithm (SHA1).</summary>
		// Token: 0x04000BE3 RID: 3043
		Sha1,
		/// <summary>The Secure Hashing Algorithm 2 (SHA-2), using a 256-bit digest.</summary>
		// Token: 0x04000BE4 RID: 3044
		Sha256 = 32780,
		/// <summary>The Secure Hashing Algorithm 2 (SHA-2), using a 384-bit digest.</summary>
		// Token: 0x04000BE5 RID: 3045
		Sha384,
		/// <summary>The Secure Hashing Algorithm 2 (SHA-2), using a 512-bit digest.</summary>
		// Token: 0x04000BE6 RID: 3046
		Sha512
	}
}
