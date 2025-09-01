using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020006F0 RID: 1776
	[Flags]
	internal enum StartIPOptions
	{
		// Token: 0x0400218C RID: 8588
		Both = 3,
		// Token: 0x0400218D RID: 8589
		None = 0,
		// Token: 0x0400218E RID: 8590
		StartIPv4 = 1,
		// Token: 0x0400218F RID: 8591
		StartIPv6 = 2
	}
}
