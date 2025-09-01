using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000362 RID: 866
	[Flags]
	internal enum InternalTaskOptions
	{
		// Token: 0x04001D0E RID: 7438
		None = 0,
		// Token: 0x04001D0F RID: 7439
		InternalOptionsMask = 65280,
		// Token: 0x04001D10 RID: 7440
		ContinuationTask = 512,
		// Token: 0x04001D11 RID: 7441
		PromiseTask = 1024,
		// Token: 0x04001D12 RID: 7442
		LazyCancellation = 4096,
		// Token: 0x04001D13 RID: 7443
		QueuedByRuntime = 8192,
		// Token: 0x04001D14 RID: 7444
		DoNotDispose = 16384
	}
}
