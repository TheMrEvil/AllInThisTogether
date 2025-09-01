using System;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000068 RID: 104
	public struct JobRanges
	{
		// Token: 0x04000184 RID: 388
		internal int BatchSize;

		// Token: 0x04000185 RID: 389
		internal int NumJobs;

		// Token: 0x04000186 RID: 390
		public int TotalIterationCount;

		// Token: 0x04000187 RID: 391
		internal int NumPhases;

		// Token: 0x04000188 RID: 392
		internal IntPtr StartEndIndex;

		// Token: 0x04000189 RID: 393
		internal IntPtr PhaseData;
	}
}
