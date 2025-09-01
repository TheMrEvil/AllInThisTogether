using System;
using System.Security.Permissions;
using Unity;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Provides information about an Authenticode signature for a manifest. </summary>
	// Token: 0x02000371 RID: 881
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class AuthenticodeSignatureInformation
	{
		// Token: 0x06001AD5 RID: 6869 RVA: 0x0000235B File Offset: 0x0000055B
		internal AuthenticodeSignatureInformation()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the description of the signing certificate.</summary>
		/// <returns>The description of the signing certificate.</returns>
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x0005A05A File Offset: 0x0005825A
		public string Description
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the description URL of the signing certificate.</summary>
		/// <returns>The description URL of the signing certificate.</returns>
		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001AD7 RID: 6871 RVA: 0x0005A05A File Offset: 0x0005825A
		public Uri DescriptionUrl
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the hash algorithm used to compute the signature.</summary>
		/// <returns>The hash algorithm used to compute the signature.</returns>
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001AD8 RID: 6872 RVA: 0x0005A05A File Offset: 0x0005825A
		public string HashAlgorithm
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the HRESULT value from verifying the signature.</summary>
		/// <returns>The HRESULT value from verifying the signature.</returns>
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001AD9 RID: 6873 RVA: 0x0005A0F0 File Offset: 0x000582F0
		public int HResult
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the chain of certificates used to verify the Authenticode signature.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object that contains the certificate chain.</returns>
		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001ADA RID: 6874 RVA: 0x0005A05A File Offset: 0x0005825A
		public X509Chain SignatureChain
		{
			[SecuritySafeCritical]
			[StorePermission(SecurityAction.Demand, OpenStore = true, EnumerateCertificates = true)]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the certificate that signed the manifest.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the certificate.</returns>
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x0005A05A File Offset: 0x0005825A
		public X509Certificate2 SigningCertificate
		{
			[SecuritySafeCritical]
			[StorePermission(SecurityAction.Demand, OpenStore = true, EnumerateCertificates = true)]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the time stamp that was applied to the Authenticode signature.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.X509Certificates.TimestampInformation" /> object that contains the signature time stamp.</returns>
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001ADC RID: 6876 RVA: 0x0005A05A File Offset: 0x0005825A
		public TimestampInformation Timestamp
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the trustworthiness of the Authenticode signature.</summary>
		/// <returns>One of the <see cref="T:System.Security.Cryptography.X509Certificates.TrustStatus" /> values. </returns>
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001ADD RID: 6877 RVA: 0x0005A10C File Offset: 0x0005830C
		public TrustStatus TrustStatus
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return TrustStatus.Untrusted;
			}
		}

		/// <summary>Gets the result of verifying the Authenticode signature.</summary>
		/// <returns>One of the <see cref="T:System.Security.Cryptography.SignatureVerificationResult" /> values.</returns>
		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001ADE RID: 6878 RVA: 0x0005A128 File Offset: 0x00058328
		public SignatureVerificationResult VerificationResult
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return SignatureVerificationResult.Valid;
			}
		}
	}
}
