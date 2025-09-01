using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32.SafeHandles;

namespace Mono
{
	// Token: 0x02000043 RID: 67
	internal interface ISystemCertificateProvider
	{
		// Token: 0x060000CF RID: 207
		X509CertificateImpl Import(byte[] data, CertificateImportFlags importFlags = CertificateImportFlags.None);

		// Token: 0x060000D0 RID: 208
		X509CertificateImpl Import(byte[] data, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags, CertificateImportFlags importFlags = CertificateImportFlags.None);

		// Token: 0x060000D1 RID: 209
		X509CertificateImpl Import(X509Certificate cert, CertificateImportFlags importFlags = CertificateImportFlags.None);
	}
}
