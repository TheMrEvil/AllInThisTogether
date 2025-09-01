using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines how the certificate key can be used. If this value is not defined, the key can be used for any purpose.</summary>
	// Token: 0x020002C5 RID: 709
	[Flags]
	public enum X509KeyUsageFlags
	{
		/// <summary>No key usage parameters.</summary>
		// Token: 0x04000CA8 RID: 3240
		None = 0,
		/// <summary>The key can be used for encryption only.</summary>
		// Token: 0x04000CA9 RID: 3241
		EncipherOnly = 1,
		/// <summary>The key can be used to sign a certificate revocation list (CRL).</summary>
		// Token: 0x04000CAA RID: 3242
		CrlSign = 2,
		/// <summary>The key can be used to sign certificates.</summary>
		// Token: 0x04000CAB RID: 3243
		KeyCertSign = 4,
		/// <summary>The key can be used to determine key agreement, such as a key created using the Diffie-Hellman key agreement algorithm.</summary>
		// Token: 0x04000CAC RID: 3244
		KeyAgreement = 8,
		/// <summary>The key can be used for data encryption.</summary>
		// Token: 0x04000CAD RID: 3245
		DataEncipherment = 16,
		/// <summary>The key can be used for key encryption.</summary>
		// Token: 0x04000CAE RID: 3246
		KeyEncipherment = 32,
		/// <summary>The key can be used for authentication.</summary>
		// Token: 0x04000CAF RID: 3247
		NonRepudiation = 64,
		/// <summary>The key can be used as a digital signature.</summary>
		// Token: 0x04000CB0 RID: 3248
		DigitalSignature = 128,
		/// <summary>The key can be used for decryption only.</summary>
		// Token: 0x04000CB1 RID: 3249
		DecipherOnly = 32768
	}
}
