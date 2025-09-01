using System;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x0200007E RID: 126
	[NativeHeader("Runtime/File/AsyncReadManagerMetrics.h")]
	public enum ProcessingState
	{
		// Token: 0x040001D4 RID: 468
		Unknown,
		// Token: 0x040001D5 RID: 469
		InQueue,
		// Token: 0x040001D6 RID: 470
		Reading,
		// Token: 0x040001D7 RID: 471
		Completed,
		// Token: 0x040001D8 RID: 472
		Failed,
		// Token: 0x040001D9 RID: 473
		Canceled
	}
}
