using System;

namespace System.Net.Security
{
	/// <summary>The EncryptionPolicy to use.</summary>
	// Token: 0x02000859 RID: 2137
	public enum EncryptionPolicy
	{
		/// <summary>Require encryption and never allow a NULL cipher.</summary>
		// Token: 0x0400291B RID: 10523
		RequireEncryption,
		/// <summary>Prefer that full encryption be used, but allow a NULL cipher (no encryption) if the server agrees.</summary>
		// Token: 0x0400291C RID: 10524
		AllowNoEncryption,
		/// <summary>Allow no encryption and request that a NULL cipher be used if the other endpoint can handle a NULL cipher.</summary>
		// Token: 0x0400291D RID: 10525
		NoEncryption
	}
}
