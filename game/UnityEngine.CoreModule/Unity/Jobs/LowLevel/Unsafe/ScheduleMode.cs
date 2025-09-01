using System;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000069 RID: 105
	public enum ScheduleMode
	{
		// Token: 0x0400018B RID: 395
		Run,
		// Token: 0x0400018C RID: 396
		[Obsolete("Batched is obsolete, use Parallel or Single depending on job type. (UnityUpgradable) -> Parallel", false)]
		Batched,
		// Token: 0x0400018D RID: 397
		Parallel = 1,
		// Token: 0x0400018E RID: 398
		Single
	}
}
