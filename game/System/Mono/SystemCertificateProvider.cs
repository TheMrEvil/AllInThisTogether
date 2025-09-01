using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.Win32.SafeHandles;
using Mono.Btls;
using Mono.Net.Security;
using Mono.Security.Interface;

namespace Mono
{
	// Token: 0x02000030 RID: 48
	internal class SystemCertificateProvider : ISystemCertificateProvider
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002CB8 File Offset: 0x00000EB8
		public MonoTlsProvider Provider
		{
			get
			{
				SystemCertificateProvider.EnsureInitialized();
				return SystemCertificateProvider.provider;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002CC4 File Offset: 0x00000EC4
		private static X509PalImpl GetX509Pal()
		{
			MonoTlsProvider monoTlsProvider = SystemCertificateProvider.provider;
			Guid? guid = (monoTlsProvider != null) ? new Guid?(monoTlsProvider.ID) : null;
			Guid btlsId = Mono.Net.Security.MonoTlsProviderFactory.BtlsId;
			if (guid != null && (guid == null || guid.GetValueOrDefault() == btlsId))
			{
				return new X509PalImplBtls(SystemCertificateProvider.provider);
			}
			return new X509PalImplMono();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002D30 File Offset: 0x00000F30
		private static void EnsureInitialized()
		{
			object obj = SystemCertificateProvider.syncRoot;
			lock (obj)
			{
				if (Interlocked.CompareExchange(ref SystemCertificateProvider.initialized, 1, 0) == 0)
				{
					SystemCertificateProvider.provider = Mono.Security.Interface.MonoTlsProviderFactory.GetProvider();
					SystemCertificateProvider.x509pal = SystemCertificateProvider.GetX509Pal();
				}
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00002D90 File Offset: 0x00000F90
		public X509PalImpl X509Pal
		{
			get
			{
				SystemCertificateProvider.EnsureInitialized();
				return SystemCertificateProvider.x509pal;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002D9C File Offset: 0x00000F9C
		public X509CertificateImpl Import(byte[] data, CertificateImportFlags importFlags = CertificateImportFlags.None)
		{
			if (data == null || data.Length == 0)
			{
				return null;
			}
			if ((importFlags & CertificateImportFlags.DisableNativeBackend) == CertificateImportFlags.None)
			{
				X509CertificateImpl x509CertificateImpl = this.X509Pal.Import(data);
				if (x509CertificateImpl != null)
				{
					return x509CertificateImpl;
				}
			}
			if ((importFlags & CertificateImportFlags.DisableAutomaticFallback) != CertificateImportFlags.None)
			{
				return null;
			}
			return this.X509Pal.ImportFallback(data);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002DDE File Offset: 0x00000FDE
		X509CertificateImpl ISystemCertificateProvider.Import(byte[] data, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags, CertificateImportFlags importFlags)
		{
			return this.Import(data, password, keyStorageFlags, importFlags);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002DEC File Offset: 0x00000FEC
		public X509Certificate2Impl Import(byte[] data, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags, CertificateImportFlags importFlags = CertificateImportFlags.None)
		{
			if (data == null || data.Length == 0)
			{
				return null;
			}
			if ((importFlags & CertificateImportFlags.DisableNativeBackend) == CertificateImportFlags.None)
			{
				X509Certificate2Impl x509Certificate2Impl = this.X509Pal.Import(data, password, keyStorageFlags);
				if (x509Certificate2Impl != null)
				{
					return x509Certificate2Impl;
				}
			}
			if ((importFlags & CertificateImportFlags.DisableAutomaticFallback) != CertificateImportFlags.None)
			{
				return null;
			}
			return this.X509Pal.ImportFallback(data, password, keyStorageFlags);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002E34 File Offset: 0x00001034
		X509CertificateImpl ISystemCertificateProvider.Import(X509Certificate cert, CertificateImportFlags importFlags)
		{
			return this.Import(cert, importFlags);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002E40 File Offset: 0x00001040
		public X509Certificate2Impl Import(X509Certificate cert, CertificateImportFlags importFlags = CertificateImportFlags.None)
		{
			if (cert.Impl == null)
			{
				return null;
			}
			X509Certificate2Impl x509Certificate2Impl = cert.Impl as X509Certificate2Impl;
			if (x509Certificate2Impl != null)
			{
				return (X509Certificate2Impl)x509Certificate2Impl.Clone();
			}
			if ((importFlags & CertificateImportFlags.DisableNativeBackend) == CertificateImportFlags.None)
			{
				x509Certificate2Impl = this.X509Pal.Import(cert);
				if (x509Certificate2Impl != null)
				{
					return x509Certificate2Impl;
				}
			}
			if ((importFlags & CertificateImportFlags.DisableAutomaticFallback) != CertificateImportFlags.None)
			{
				return null;
			}
			return this.X509Pal.ImportFallback(cert.GetRawCertData());
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000219B File Offset: 0x0000039B
		public SystemCertificateProvider()
		{
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00002EA1 File Offset: 0x000010A1
		// Note: this type is marked as 'beforefieldinit'.
		static SystemCertificateProvider()
		{
		}

		// Token: 0x0400011D RID: 285
		private static MonoTlsProvider provider;

		// Token: 0x0400011E RID: 286
		private static int initialized;

		// Token: 0x0400011F RID: 287
		private static X509PalImpl x509pal;

		// Token: 0x04000120 RID: 288
		private static object syncRoot = new object();
	}
}
