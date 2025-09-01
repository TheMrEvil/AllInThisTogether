using System;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKeyType" /> enumeration defines how a subject is identified.</summary>
	// Token: 0x0200008B RID: 139
	public enum SubjectIdentifierOrKeyType
	{
		/// <summary>The type is unknown.</summary>
		// Token: 0x040002C6 RID: 710
		Unknown,
		/// <summary>The subject is identified by the certificate issuer and serial number.</summary>
		// Token: 0x040002C7 RID: 711
		IssuerAndSerialNumber,
		/// <summary>The subject is identified by the hash of the subject key.</summary>
		// Token: 0x040002C8 RID: 712
		SubjectKeyIdentifier,
		/// <summary>The subject is identified by the public key.</summary>
		// Token: 0x040002C9 RID: 713
		PublicKeyInfo
	}
}
