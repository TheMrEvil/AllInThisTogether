using System;

namespace Mono.Security.Interface
{
	// Token: 0x0200004D RID: 77
	[Flags]
	public enum TlsProtocols
	{
		// Token: 0x04000293 RID: 659
		Zero = 0,
		// Token: 0x04000294 RID: 660
		Tls10Client = 128,
		// Token: 0x04000295 RID: 661
		Tls10Server = 64,
		// Token: 0x04000296 RID: 662
		Tls10 = 192,
		// Token: 0x04000297 RID: 663
		Tls11Client = 512,
		// Token: 0x04000298 RID: 664
		Tls11Server = 256,
		// Token: 0x04000299 RID: 665
		Tls11 = 768,
		// Token: 0x0400029A RID: 666
		Tls12Client = 2048,
		// Token: 0x0400029B RID: 667
		Tls12Server = 1024,
		// Token: 0x0400029C RID: 668
		Tls12 = 3072,
		// Token: 0x0400029D RID: 669
		ClientMask = 2688,
		// Token: 0x0400029E RID: 670
		ServerMask = 1344
	}
}
