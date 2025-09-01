using System;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration defines the type of subject identifier.</summary>
	// Token: 0x0200008C RID: 140
	public enum SubjectIdentifierType
	{
		/// <summary>The type of subject identifier is unknown.</summary>
		// Token: 0x040002CB RID: 715
		Unknown,
		/// <summary>The subject is identified by the certificate issuer and serial number.</summary>
		// Token: 0x040002CC RID: 716
		IssuerAndSerialNumber,
		/// <summary>The subject is identified by the hash of the subject's public key. The hash algorithm used is determined by the signature algorithm suite in the subject's certificate.</summary>
		// Token: 0x040002CD RID: 717
		SubjectKeyIdentifier,
		/// <summary>The subject is identified as taking part in an integrity check operation that uses only a hashing algorithm.</summary>
		// Token: 0x040002CE RID: 718
		NoSignature
	}
}
