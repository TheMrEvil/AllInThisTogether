using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32.SafeHandles;

namespace Mono
{
	// Token: 0x02000033 RID: 51
	internal class X509PalImplMono : X509PalImpl
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00002F56 File Offset: 0x00001156
		public override X509CertificateImpl Import(byte[] data)
		{
			return base.ImportFallback(data);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00002F5F File Offset: 0x0000115F
		public override X509Certificate2Impl Import(byte[] data, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags)
		{
			return base.ImportFallback(data, password, keyStorageFlags);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00002F6A File Offset: 0x0000116A
		public override X509Certificate2Impl Import(X509Certificate cert)
		{
			return null;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00002F6D File Offset: 0x0000116D
		public X509PalImplMono()
		{
		}
	}
}
