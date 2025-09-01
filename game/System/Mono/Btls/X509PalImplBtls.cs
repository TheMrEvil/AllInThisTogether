using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32.SafeHandles;
using Mono.Security.Interface;

namespace Mono.Btls
{
	// Token: 0x02000114 RID: 276
	internal class X509PalImplBtls : X509PalImpl
	{
		// Token: 0x060006A5 RID: 1701 RVA: 0x00012554 File Offset: 0x00010754
		public X509PalImplBtls(MonoTlsProvider provider)
		{
			this.Provider = (MonoBtlsProvider)provider;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00012568 File Offset: 0x00010768
		private MonoBtlsProvider Provider
		{
			[CompilerGenerated]
			get
			{
				return this.<Provider>k__BackingField;
			}
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00012570 File Offset: 0x00010770
		public override X509CertificateImpl Import(byte[] data)
		{
			return this.Provider.GetNativeCertificate(data, null, X509KeyStorageFlags.DefaultKeySet);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00012580 File Offset: 0x00010780
		public override X509Certificate2Impl Import(byte[] data, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags)
		{
			return this.Provider.GetNativeCertificate(data, password, keyStorageFlags);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00012590 File Offset: 0x00010790
		public override X509Certificate2Impl Import(X509Certificate cert)
		{
			return this.Provider.GetNativeCertificate(cert);
		}

		// Token: 0x04000479 RID: 1145
		[CompilerGenerated]
		private readonly MonoBtlsProvider <Provider>k__BackingField;
	}
}
