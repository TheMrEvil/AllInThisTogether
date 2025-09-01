using System;

namespace System
{
	// Token: 0x020001FD RID: 509
	[Serializable]
	internal enum InternalGCCollectionMode
	{
		// Token: 0x0400153B RID: 5435
		NonBlocking = 1,
		// Token: 0x0400153C RID: 5436
		Blocking,
		// Token: 0x0400153D RID: 5437
		Optimized = 4,
		// Token: 0x0400153E RID: 5438
		Compacting = 8
	}
}
