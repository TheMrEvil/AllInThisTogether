using System;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Interface
{
	// Token: 0x02000046 RID: 70
	// (Invoke) Token: 0x060002A0 RID: 672
	public delegate bool MonoRemoteCertificateValidationCallback(string targetHost, X509Certificate certificate, X509Chain chain, MonoSslPolicyErrors sslPolicyErrors);
}
