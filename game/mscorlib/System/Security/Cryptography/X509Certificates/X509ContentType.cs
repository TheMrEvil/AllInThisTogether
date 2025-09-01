using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the format of an X.509 certificate.</summary>
	// Token: 0x020004D7 RID: 1239
	public enum X509ContentType
	{
		/// <summary>An unknown X.509 certificate.</summary>
		// Token: 0x04002288 RID: 8840
		Unknown,
		/// <summary>A single X.509 certificate.</summary>
		// Token: 0x04002289 RID: 8841
		Cert,
		/// <summary>A single serialized X.509 certificate.</summary>
		// Token: 0x0400228A RID: 8842
		SerializedCert,
		/// <summary>A PFX-formatted certificate. The <see langword="Pfx" /> value is identical to the <see langword="Pkcs12" /> value.</summary>
		// Token: 0x0400228B RID: 8843
		Pfx,
		/// <summary>A PKCS #12-formatted certificate. The <see langword="Pkcs12" /> value is identical to the <see langword="Pfx" /> value.</summary>
		// Token: 0x0400228C RID: 8844
		Pkcs12 = 3,
		/// <summary>A serialized store.</summary>
		// Token: 0x0400228D RID: 8845
		SerializedStore,
		/// <summary>A PKCS #7-formatted certificate.</summary>
		// Token: 0x0400228E RID: 8846
		Pkcs7,
		/// <summary>An Authenticode X.509 certificate.</summary>
		// Token: 0x0400228F RID: 8847
		Authenticode
	}
}
