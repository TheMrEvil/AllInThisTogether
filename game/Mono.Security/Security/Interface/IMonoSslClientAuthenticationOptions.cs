using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Interface
{
	// Token: 0x02000040 RID: 64
	internal interface IMonoSslClientAuthenticationOptions : IMonoAuthenticationOptions
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600025F RID: 607
		// (set) Token: 0x06000260 RID: 608
		LocalCertificateSelectionCallback LocalCertificateSelectionCallback { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000261 RID: 609
		// (set) Token: 0x06000262 RID: 610
		string TargetHost { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000263 RID: 611
		// (set) Token: 0x06000264 RID: 612
		X509CertificateCollection ClientCertificates { get; set; }
	}
}
