using System;

namespace Unity.Profiling.LowLevel
{
	// Token: 0x02000050 RID: 80
	public enum ProfilerMarkerDataType : byte
	{
		// Token: 0x04000146 RID: 326
		Int32 = 2,
		// Token: 0x04000147 RID: 327
		UInt32,
		// Token: 0x04000148 RID: 328
		Int64,
		// Token: 0x04000149 RID: 329
		UInt64,
		// Token: 0x0400014A RID: 330
		Float,
		// Token: 0x0400014B RID: 331
		Double,
		// Token: 0x0400014C RID: 332
		String16 = 9,
		// Token: 0x0400014D RID: 333
		Blob8 = 11
	}
}
