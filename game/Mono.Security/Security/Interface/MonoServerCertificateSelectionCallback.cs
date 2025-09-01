using System;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Interface
{
	// Token: 0x0200003E RID: 62
	// (Invoke) Token: 0x06000252 RID: 594
	internal delegate X509Certificate MonoServerCertificateSelectionCallback(object sender, string hostName);
}
