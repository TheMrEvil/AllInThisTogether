using System;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Interface
{
	// Token: 0x02000041 RID: 65
	internal interface IMonoSslServerAuthenticationOptions : IMonoAuthenticationOptions
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000265 RID: 613
		// (set) Token: 0x06000266 RID: 614
		bool ClientCertificateRequired { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000267 RID: 615
		// (set) Token: 0x06000268 RID: 616
		MonoServerCertificateSelectionCallback ServerCertificateSelectionCallback { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000269 RID: 617
		// (set) Token: 0x0600026A RID: 618
		X509Certificate ServerCertificate { get; set; }
	}
}
