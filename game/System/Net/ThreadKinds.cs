using System;

namespace System.Net
{
	// Token: 0x0200062F RID: 1583
	[Flags]
	internal enum ThreadKinds
	{
		// Token: 0x04001D3C RID: 7484
		Unknown = 0,
		// Token: 0x04001D3D RID: 7485
		User = 1,
		// Token: 0x04001D3E RID: 7486
		System = 2,
		// Token: 0x04001D3F RID: 7487
		Sync = 4,
		// Token: 0x04001D40 RID: 7488
		Async = 8,
		// Token: 0x04001D41 RID: 7489
		Timer = 16,
		// Token: 0x04001D42 RID: 7490
		CompletionPort = 32,
		// Token: 0x04001D43 RID: 7491
		Worker = 64,
		// Token: 0x04001D44 RID: 7492
		Finalization = 128,
		// Token: 0x04001D45 RID: 7493
		Other = 256,
		// Token: 0x04001D46 RID: 7494
		OwnerMask = 3,
		// Token: 0x04001D47 RID: 7495
		SyncMask = 12,
		// Token: 0x04001D48 RID: 7496
		SourceMask = 496,
		// Token: 0x04001D49 RID: 7497
		SafeSources = 352,
		// Token: 0x04001D4A RID: 7498
		ThreadPool = 96
	}
}
