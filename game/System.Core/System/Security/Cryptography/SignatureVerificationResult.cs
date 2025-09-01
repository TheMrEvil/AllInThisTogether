using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies most of the result codes for signature verification. </summary>
	// Token: 0x02000373 RID: 883
	public enum SignatureVerificationResult
	{
		/// <summary>The identity of the assembly specified in the /asm:assembly/asm:assemblyIdentity node of the manifest does not match the identity of the assembly in the Authenticode signature in the /asm:assembly/ds:signature/ds:KeyInfo/msrel:RelData/r:license/r:grant/as:ManifestInformation/as:assemblyIdentity node.</summary>
		// Token: 0x04000CE0 RID: 3296
		AssemblyIdentityMismatch = 1,
		/// <summary>The digital signature of the object did not verify.</summary>
		// Token: 0x04000CE1 RID: 3297
		BadDigest = -2146869232,
		/// <summary>The signature format is invalid.</summary>
		// Token: 0x04000CE2 RID: 3298
		BadSignatureFormat = -2146762749,
		/// <summary>The basic constraint extension of a certificate has not been observed.</summary>
		// Token: 0x04000CE3 RID: 3299
		BasicConstraintsNotObserved = -2146869223,
		/// <summary>The certificate has expired.</summary>
		// Token: 0x04000CE4 RID: 3300
		CertificateExpired = -2146762495,
		/// <summary>The certificate was explicitly marked as not trusted by the user.</summary>
		// Token: 0x04000CE5 RID: 3301
		CertificateExplicitlyDistrusted = -2146762479,
		/// <summary>The certificate is missing or has an empty value for an important field, such as a subject or issuer name.</summary>
		// Token: 0x04000CE6 RID: 3302
		CertificateMalformed = -2146762488,
		/// <summary>The certificate is not trusted explicitly.</summary>
		// Token: 0x04000CE7 RID: 3303
		CertificateNotExplicitlyTrusted = -2146762748,
		/// <summary>The certificate has been revoked.</summary>
		// Token: 0x04000CE8 RID: 3304
		CertificateRevoked = -2146762484,
		/// <summary>The certificate cannot be used for signing and verification.</summary>
		// Token: 0x04000CE9 RID: 3305
		CertificateUsageNotAllowed = -2146762490,
		/// <summary>The strong name signature does not verify in the <see cref="T:System.Security.Cryptography.X509Certificates.AuthenticodeSignatureInformation" /> object. Because the strong name signature wraps the Authenticode signature, someone could replace the Authenticode signature with a signature of their choosing. To prevent this, this error code is returned if the strong name does not verify because substituting a part of the strong name signature will invalidate it.</summary>
		// Token: 0x04000CEA RID: 3306
		ContainingSignatureInvalid = 2,
		/// <summary>The chain could not be built.</summary>
		// Token: 0x04000CEB RID: 3307
		CouldNotBuildChain = -2146762486,
		/// <summary>There is a general trust failure with the certificate.</summary>
		// Token: 0x04000CEC RID: 3308
		GenericTrustFailure,
		/// <summary>The certificate has an invalid name. The name is either not included in the permitted list or is explicitly excluded.</summary>
		// Token: 0x04000CED RID: 3309
		InvalidCertificateName = -2146762476,
		/// <summary>The certificate has an invalid policy.</summary>
		// Token: 0x04000CEE RID: 3310
		InvalidCertificatePolicy = -2146762477,
		/// <summary>The certificate has an invalid role.</summary>
		// Token: 0x04000CEF RID: 3311
		InvalidCertificateRole = -2146762493,
		/// <summary>The signature of the certificate cannot be verified.</summary>
		// Token: 0x04000CF0 RID: 3312
		InvalidCertificateSignature = -2146869244,
		/// <summary>The certificate has an invalid usage.</summary>
		// Token: 0x04000CF1 RID: 3313
		InvalidCertificateUsage = -2146762480,
		/// <summary>One of the counter signatures is invalid.</summary>
		// Token: 0x04000CF2 RID: 3314
		InvalidCountersignature = -2146869245,
		/// <summary>The certificate for the signer of the message is invalid or not found.</summary>
		// Token: 0x04000CF3 RID: 3315
		InvalidSignerCertificate = -2146869246,
		/// <summary>A certificate was issued after the issuing certificate has expired.</summary>
		// Token: 0x04000CF4 RID: 3316
		InvalidTimePeriodNesting = -2146762494,
		/// <summary>The time stamp signature or certificate could not be verified or is malformed.</summary>
		// Token: 0x04000CF5 RID: 3317
		InvalidTimestamp = -2146869243,
		/// <summary>A parent of a given certificate did not issue that child certificate.</summary>
		// Token: 0x04000CF6 RID: 3318
		IssuerChainingError = -2146762489,
		/// <summary>The signature is missing.</summary>
		// Token: 0x04000CF7 RID: 3319
		MissingSignature = -2146762496,
		/// <summary>A path length constraint in the certification chain has been violated.</summary>
		// Token: 0x04000CF8 RID: 3320
		PathLengthConstraintViolated = -2146762492,
		/// <summary>The public key token from the manifest identity in the /asm:assembly/asm:AssemblyIdentity node does not match the public key token of the key that is used to sign the manifest.</summary>
		// Token: 0x04000CF9 RID: 3321
		PublicKeyTokenMismatch = 3,
		/// <summary>The publisher name from /asm:assembly/asmv2:publisherIdentity does not match the subject name of the signing certificate, or the issuer key hash from the same publisherIdentity node does not match the key hash of the signing certificate.</summary>
		// Token: 0x04000CFA RID: 3322
		PublisherMismatch,
		/// <summary>The revocation check failed.</summary>
		// Token: 0x04000CFB RID: 3323
		RevocationCheckFailure = -2146762482,
		/// <summary>A system-level error occurred while verifying trust.</summary>
		// Token: 0x04000CFC RID: 3324
		SystemError = -2146869247,
		/// <summary>A certificate contains an unknown extension that is marked critical.</summary>
		// Token: 0x04000CFD RID: 3325
		UnknownCriticalExtension = -2146762491,
		/// <summary>The certificate has an unknown trust provider.</summary>
		// Token: 0x04000CFE RID: 3326
		UnknownTrustProvider = -2146762751,
		/// <summary>The certificate has an unknown verification action.</summary>
		// Token: 0x04000CFF RID: 3327
		UnknownVerificationAction,
		/// <summary>The certification chain processed correctly, but one of the CA certificates is not trusted by the policy provider.</summary>
		// Token: 0x04000D00 RID: 3328
		UntrustedCertificationAuthority = -2146762478,
		/// <summary>The root certificate is not trusted.</summary>
		// Token: 0x04000D01 RID: 3329
		UntrustedRootCertificate = -2146762487,
		/// <summary>The test root certificate is not trusted.</summary>
		// Token: 0x04000D02 RID: 3330
		UntrustedTestRootCertificate = -2146762483,
		/// <summary>The certificate verification result is valid.</summary>
		// Token: 0x04000D03 RID: 3331
		Valid = 0
	}
}
