using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines the status of an X509 chain.</summary>
	// Token: 0x020002C2 RID: 706
	[Flags]
	public enum X509ChainStatusFlags
	{
		/// <summary>Specifies that the X509 chain has no errors.</summary>
		// Token: 0x04000C78 RID: 3192
		NoError = 0,
		/// <summary>Specifies that the X509 chain is not valid due to an invalid time value, such as a value that indicates an expired certificate.</summary>
		// Token: 0x04000C79 RID: 3193
		NotTimeValid = 1,
		/// <summary>Deprecated. Specifies that the CA (certificate authority) certificate and the issued certificate have validity periods that are not nested. For example, the CA cert can be valid from January 1 to December 1 and the issued certificate from January 2 to December 2, which would mean the validity periods are not nested.</summary>
		// Token: 0x04000C7A RID: 3194
		NotTimeNested = 2,
		/// <summary>Specifies that the X509 chain is invalid due to a revoked certificate.</summary>
		// Token: 0x04000C7B RID: 3195
		Revoked = 4,
		/// <summary>Specifies that the X509 chain is invalid due to an invalid certificate signature.</summary>
		// Token: 0x04000C7C RID: 3196
		NotSignatureValid = 8,
		/// <summary>Specifies that the key usage is not valid.</summary>
		// Token: 0x04000C7D RID: 3197
		NotValidForUsage = 16,
		/// <summary>Specifies that the X509 chain is invalid due to an untrusted root certificate.</summary>
		// Token: 0x04000C7E RID: 3198
		UntrustedRoot = 32,
		/// <summary>Specifies that it is not possible to determine whether the certificate has been revoked. This can be due to the certificate revocation list (CRL) being offline or unavailable.</summary>
		// Token: 0x04000C7F RID: 3199
		RevocationStatusUnknown = 64,
		/// <summary>Specifies that the X509 chain could not be built.</summary>
		// Token: 0x04000C80 RID: 3200
		Cyclic = 128,
		/// <summary>Specifies that the X509 chain is invalid due to an invalid extension.</summary>
		// Token: 0x04000C81 RID: 3201
		InvalidExtension = 256,
		/// <summary>Specifies that the X509 chain is invalid due to invalid policy constraints.</summary>
		// Token: 0x04000C82 RID: 3202
		InvalidPolicyConstraints = 512,
		/// <summary>Specifies that the X509 chain is invalid due to invalid basic constraints.</summary>
		// Token: 0x04000C83 RID: 3203
		InvalidBasicConstraints = 1024,
		/// <summary>Specifies that the X509 chain is invalid due to invalid name constraints.</summary>
		// Token: 0x04000C84 RID: 3204
		InvalidNameConstraints = 2048,
		/// <summary>Specifies that the certificate does not have a supported name constraint or has a name constraint that is unsupported.</summary>
		// Token: 0x04000C85 RID: 3205
		HasNotSupportedNameConstraint = 4096,
		/// <summary>Specifies that the certificate has an undefined name constraint.</summary>
		// Token: 0x04000C86 RID: 3206
		HasNotDefinedNameConstraint = 8192,
		/// <summary>Specifies that the certificate has an impermissible name constraint.</summary>
		// Token: 0x04000C87 RID: 3207
		HasNotPermittedNameConstraint = 16384,
		/// <summary>Specifies that the X509 chain is invalid because a certificate has excluded a name constraint.</summary>
		// Token: 0x04000C88 RID: 3208
		HasExcludedNameConstraint = 32768,
		/// <summary>Specifies that the X509 chain could not be built up to the root certificate.</summary>
		// Token: 0x04000C89 RID: 3209
		PartialChain = 65536,
		/// <summary>Specifies that the certificate trust list (CTL) is not valid because of an invalid time value, such as one that indicates that the CTL has expired.</summary>
		// Token: 0x04000C8A RID: 3210
		CtlNotTimeValid = 131072,
		/// <summary>Specifies that the certificate trust list (CTL) contains an invalid signature.</summary>
		// Token: 0x04000C8B RID: 3211
		CtlNotSignatureValid = 262144,
		/// <summary>Specifies that the certificate trust list (CTL) is not valid for this use.</summary>
		// Token: 0x04000C8C RID: 3212
		CtlNotValidForUsage = 524288,
		/// <summary>Specifies that the online certificate revocation list (CRL) the X509 chain relies on is currently offline.</summary>
		// Token: 0x04000C8D RID: 3213
		OfflineRevocation = 16777216,
		/// <summary>Specifies that there is no certificate policy extension in the certificate. This error would occur if a group policy has specified that all certificates must have a certificate policy.</summary>
		// Token: 0x04000C8E RID: 3214
		NoIssuanceChainPolicy = 33554432,
		/// <summary>Specifies that the certificate is explicitly distrusted.</summary>
		// Token: 0x04000C8F RID: 3215
		ExplicitDistrust = 67108864,
		/// <summary>Specifies that the certificate does not support a critical extension.</summary>
		// Token: 0x04000C90 RID: 3216
		HasNotSupportedCriticalExtension = 134217728,
		/// <summary>Specifies that the certificate has not been strong signed. Typically, this indicates that the MD2 or MD5 hashing algorithms were used to create a hash of the certificate.</summary>
		// Token: 0x04000C91 RID: 3217
		HasWeakSignature = 1048576
	}
}
