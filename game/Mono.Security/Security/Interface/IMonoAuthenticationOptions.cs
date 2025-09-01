using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Interface
{
	// Token: 0x0200003F RID: 63
	internal interface IMonoAuthenticationOptions
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000255 RID: 597
		// (set) Token: 0x06000256 RID: 598
		bool AllowRenegotiation { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000257 RID: 599
		// (set) Token: 0x06000258 RID: 600
		RemoteCertificateValidationCallback RemoteCertificateValidationCallback { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000259 RID: 601
		// (set) Token: 0x0600025A RID: 602
		SslProtocols EnabledSslProtocols { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600025B RID: 603
		// (set) Token: 0x0600025C RID: 604
		EncryptionPolicy EncryptionPolicy { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600025D RID: 605
		// (set) Token: 0x0600025E RID: 606
		X509RevocationMode CertificateRevocationCheckMode { get; set; }
	}
}
