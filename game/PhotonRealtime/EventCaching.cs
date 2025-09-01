using System;

namespace Photon.Realtime
{
	// Token: 0x0200002B RID: 43
	public enum EventCaching : byte
	{
		// Token: 0x04000172 RID: 370
		DoNotCache,
		// Token: 0x04000173 RID: 371
		[Obsolete]
		MergeCache,
		// Token: 0x04000174 RID: 372
		[Obsolete]
		ReplaceCache,
		// Token: 0x04000175 RID: 373
		[Obsolete]
		RemoveCache,
		// Token: 0x04000176 RID: 374
		AddToRoomCache,
		// Token: 0x04000177 RID: 375
		AddToRoomCacheGlobal,
		// Token: 0x04000178 RID: 376
		RemoveFromRoomCache,
		// Token: 0x04000179 RID: 377
		RemoveFromRoomCacheForActorsLeft,
		// Token: 0x0400017A RID: 378
		SliceIncreaseIndex = 10,
		// Token: 0x0400017B RID: 379
		SliceSetIndex,
		// Token: 0x0400017C RID: 380
		SlicePurgeIndex,
		// Token: 0x0400017D RID: 381
		SlicePurgeUpToIndex
	}
}
