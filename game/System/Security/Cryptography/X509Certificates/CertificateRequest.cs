using System;
using System.Collections.ObjectModel;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002E9 RID: 745
	public sealed class CertificateRequest
	{
		// Token: 0x060017C9 RID: 6089 RVA: 0x0005E6FC File Offset: 0x0005C8FC
		public CertificateRequest(X500DistinguishedName subjectName, ECDsa key, HashAlgorithmName hashAlgorithm)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x0005E6FC File Offset: 0x0005C8FC
		public CertificateRequest(X500DistinguishedName subjectName, RSA key, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x0005E6FC File Offset: 0x0005C8FC
		public CertificateRequest(X500DistinguishedName subjectName, PublicKey publicKey, HashAlgorithmName hashAlgorithm)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x0005E6FC File Offset: 0x0005C8FC
		public CertificateRequest(string subjectName, ECDsa key, HashAlgorithmName hashAlgorithm)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x0005E6FC File Offset: 0x0005C8FC
		public CertificateRequest(string subjectName, RSA key, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x00011F54 File Offset: 0x00010154
		public Collection<X509Extension> CertificateExtensions
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060017CF RID: 6095 RVA: 0x0005E70C File Offset: 0x0005C90C
		public HashAlgorithmName HashAlgorithm
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x00011F54 File Offset: 0x00010154
		public PublicKey PublicKey
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060017D1 RID: 6097 RVA: 0x00011F54 File Offset: 0x00010154
		public X500DistinguishedName SubjectName
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x00011F54 File Offset: 0x00010154
		public X509Certificate2 Create(X500DistinguishedName issuerName, X509SignatureGenerator generator, DateTimeOffset notBefore, DateTimeOffset notAfter, byte[] serialNumber)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x00011F54 File Offset: 0x00010154
		public X509Certificate2 Create(X509Certificate2 issuerCertificate, DateTimeOffset notBefore, DateTimeOffset notAfter, byte[] serialNumber)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x00011F54 File Offset: 0x00010154
		public X509Certificate2 CreateSelfSigned(DateTimeOffset notBefore, DateTimeOffset notAfter)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x00011F54 File Offset: 0x00010154
		public byte[] CreateSigningRequest()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x00011F54 File Offset: 0x00010154
		public byte[] CreateSigningRequest(X509SignatureGenerator signatureGenerator)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
