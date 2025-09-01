using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000B8 RID: 184
	internal enum DnsQClass : ushort
	{
		// Token: 0x040002B1 RID: 689
		Internet = 1,
		// Token: 0x040002B2 RID: 690
		IN = 1,
		// Token: 0x040002B3 RID: 691
		CSNET,
		// Token: 0x040002B4 RID: 692
		CS = 2,
		// Token: 0x040002B5 RID: 693
		CHAOS,
		// Token: 0x040002B6 RID: 694
		CH = 3,
		// Token: 0x040002B7 RID: 695
		Hesiod,
		// Token: 0x040002B8 RID: 696
		HS = 4,
		// Token: 0x040002B9 RID: 697
		None = 254,
		// Token: 0x040002BA RID: 698
		Any
	}
}
