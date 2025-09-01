using System;

namespace Unity.Profiling
{
	// Token: 0x02000046 RID: 70
	[Flags]
	public enum ProfilerCounterOptions : ushort
	{
		// Token: 0x0400011C RID: 284
		None = 0,
		// Token: 0x0400011D RID: 285
		FlushOnEndOfFrame = 2,
		// Token: 0x0400011E RID: 286
		ResetToZeroOnFlush = 4
	}
}
