using System;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Interface
{
	// Token: 0x02000047 RID: 71
	// (Invoke) Token: 0x060002A4 RID: 676
	public delegate X509Certificate MonoLocalCertificateSelectionCallback(string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers);
}
