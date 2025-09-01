using System;

namespace UnityEngine.Analytics
{
	// Token: 0x0200000E RID: 14
	[Flags]
	public enum AnalyticsEventPriority
	{
		// Token: 0x0400001E RID: 30
		FlushQueueFlag = 1,
		// Token: 0x0400001F RID: 31
		CacheImmediatelyFlag = 2,
		// Token: 0x04000020 RID: 32
		AllowInStopModeFlag = 4,
		// Token: 0x04000021 RID: 33
		SendImmediateFlag = 8,
		// Token: 0x04000022 RID: 34
		NoCachingFlag = 16,
		// Token: 0x04000023 RID: 35
		NoRetryFlag = 32,
		// Token: 0x04000024 RID: 36
		NormalPriorityEvent = 0,
		// Token: 0x04000025 RID: 37
		NormalPriorityEvent_WithCaching = 2,
		// Token: 0x04000026 RID: 38
		NormalPriorityEvent_NoRetryNoCaching = 48,
		// Token: 0x04000027 RID: 39
		HighPriorityEvent = 1,
		// Token: 0x04000028 RID: 40
		HighPriorityEvent_InStopMode = 5,
		// Token: 0x04000029 RID: 41
		HighestPriorityEvent = 9,
		// Token: 0x0400002A RID: 42
		HighestPriorityEvent_NoRetryNoCaching = 49
	}
}
