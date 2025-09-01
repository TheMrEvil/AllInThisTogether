using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using Unity;

namespace System.Security.Cryptography
{
	/// <summary>Provides information for a manifest signature. </summary>
	// Token: 0x02000370 RID: 880
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManifestSignatureInformation
	{
		// Token: 0x06001ACE RID: 6862 RVA: 0x0000235B File Offset: 0x0000055B
		internal ManifestSignatureInformation()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the Authenticode signature information for a manifest. </summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.AuthenticodeSignatureInformation" /> object that contains Authenticode signature information for the manifest, or <see langword="null" /> if there is no signature.</returns>
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x0005A05A File Offset: 0x0005825A
		public AuthenticodeSignatureInformation AuthenticodeSignature
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the type of a manifest.</summary>
		/// <returns>One of the <see cref="T:System.Security.ManifestKinds" /> values.</returns>
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x0005A0D4 File Offset: 0x000582D4
		public ManifestKinds Manifest
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return ManifestKinds.None;
			}
		}

		/// <summary>Gets the details of the strong name signature of a manifest.</summary>
		/// <returns>A <see cref="P:System.Security.Cryptography.ManifestSignatureInformation.StrongNameSignature" /> object that contains the signature, or <see langword="null" /> if there is no strong name signature.</returns>
		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001AD1 RID: 6865 RVA: 0x0005A05A File Offset: 0x0005825A
		public StrongNameSignatureInformation StrongNameSignature
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gathers and verifies information about the signatures of manifests that belong to a specified activation context.</summary>
		/// <param name="application">The activation context of the manifest. Activation contexts belong to an application and contain multiple manifests.</param>
		/// <returns>A collection that contains a <see cref="T:System.Security.Cryptography.ManifestSignatureInformation" /> object for each manifest that is verified.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="application" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001AD2 RID: 6866 RVA: 0x0005A05A File Offset: 0x0005825A
		public static ManifestSignatureInformationCollection VerifySignature(ActivationContext application)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gathers and verifies information about the signatures of manifests that belong to a specified activation context and manifest type.</summary>
		/// <param name="application">The activation context of the manifest. Activation contexts belong to an application and contain multiple manifests.</param>
		/// <param name="manifests">The type of manifest. This parameter specifies which manifests in the activation context you want to verify.</param>
		/// <returns>A collection that contains a <see cref="T:System.Security.Cryptography.ManifestSignatureInformation" /> object for each manifest that is verified.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="application" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001AD3 RID: 6867 RVA: 0x0005A05A File Offset: 0x0005825A
		public static ManifestSignatureInformationCollection VerifySignature(ActivationContext application, ManifestKinds manifests)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gathers and verifies information about the signatures of manifests that belong to a specified activation context and manifest type, and allows certificates to be selected for revocation.</summary>
		/// <param name="application">The application context of the manifests. Activation contexts belong to an application and contain multiple manifests.</param>
		/// <param name="manifests">The type of manifest. This parameter specifies which manifests in the activation context you want to verify.</param>
		/// <param name="revocationFlag">One of the enumeration values that specifies which certificates in the chain are checked for revocation. The default is <see cref="F:System.Security.Cryptography.X509Certificates.X509RevocationFlag.ExcludeRoot" />.</param>
		/// <param name="revocationMode">Determines whether the X.509 verification should look online for revocation lists. </param>
		/// <returns>A collection that contains a <see cref="T:System.Security.Cryptography.ManifestSignatureInformation" /> object for each manifest that is verified.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="application" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">A value specified for the <paramref name="revocationFlag" /> or <paramref name="revocationMode" /> parameter is invalid.</exception>
		// Token: 0x06001AD4 RID: 6868 RVA: 0x0005A05A File Offset: 0x0005825A
		[SecuritySafeCritical]
		public static ManifestSignatureInformationCollection VerifySignature(ActivationContext application, ManifestKinds manifests, X509RevocationFlag revocationFlag, X509RevocationMode revocationMode)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
