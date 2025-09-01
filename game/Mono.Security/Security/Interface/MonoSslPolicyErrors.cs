using System;

namespace Mono.Security.Interface
{
	// Token: 0x02000044 RID: 68
	[Flags]
	public enum MonoSslPolicyErrors
	{
		// Token: 0x04000271 RID: 625
		None = 0,
		// Token: 0x04000272 RID: 626
		RemoteCertificateNotAvailable = 1,
		// Token: 0x04000273 RID: 627
		RemoteCertificateNameMismatch = 2,
		// Token: 0x04000274 RID: 628
		RemoteCertificateChainErrors = 4
	}
}
