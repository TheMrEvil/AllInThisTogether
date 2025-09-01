using System;
using System.Security.Permissions;
using Unity;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Provides details about the time stamp that was applied to an Authenticode signature for a manifest. </summary>
	// Token: 0x02000372 RID: 882
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class TimestampInformation
	{
		// Token: 0x06001ADF RID: 6879 RVA: 0x0000235B File Offset: 0x0000055B
		internal TimestampInformation()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the hash algorithm used to compute the time stamp signature.</summary>
		/// <returns>The hash algorithm used to compute the time stamp signature.</returns>
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x0005A05A File Offset: 0x0005825A
		public string HashAlgorithm
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the HRESULT value that results from verifying the signature.</summary>
		/// <returns>The HRESULT value that results from verifying the signature.</returns>
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x0005A144 File Offset: 0x00058344
		public int HResult
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets a value indicating whether the time stamp of the signature is valid.</summary>
		/// <returns>
		///     <see langword="true" /> if the time stamp is valid; otherwise, <see langword="false" />. </returns>
		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x0005A160 File Offset: 0x00058360
		public bool IsValid
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets the chain of certificates used to verify the time stamp of the signature.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object that represents the certificate chain.</returns>
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x0005A05A File Offset: 0x0005825A
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

		/// <summary>Gets the certificate that signed the time stamp.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the certificate.</returns>
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x0005A05A File Offset: 0x0005825A
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

		/// <summary>Gets the time stamp that was applied to the signature.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that represents the time stamp.</returns>
		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x0005A17C File Offset: 0x0005837C
		public DateTime Timestamp
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(DateTime);
			}
		}

		/// <summary>Gets the result of verifying the time stamp signature.</summary>
		/// <returns>One of the <see cref="T:System.Security.Cryptography.SignatureVerificationResult" /> values.</returns>
		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x0005A198 File Offset: 0x00058398
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
