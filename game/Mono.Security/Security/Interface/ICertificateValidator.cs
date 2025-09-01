using System;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Interface
{
	// Token: 0x02000038 RID: 56
	public interface ICertificateValidator
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600024A RID: 586
		MonoTlsSettings Settings { get; }

		// Token: 0x0600024B RID: 587
		bool SelectClientCertificate(string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers, out X509Certificate clientCertificate);

		// Token: 0x0600024C RID: 588
		ValidationResult ValidateCertificate(string targetHost, bool serverMode, X509CertificateCollection certificates);
	}
}
