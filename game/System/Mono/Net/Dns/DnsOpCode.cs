using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000B6 RID: 182
	internal enum DnsOpCode : byte
	{
		// Token: 0x040002A8 RID: 680
		Query,
		// Token: 0x040002A9 RID: 681
		[Obsolete]
		IQuery,
		// Token: 0x040002AA RID: 682
		Status,
		// Token: 0x040002AB RID: 683
		Notify = 4,
		// Token: 0x040002AC RID: 684
		Update
	}
}
