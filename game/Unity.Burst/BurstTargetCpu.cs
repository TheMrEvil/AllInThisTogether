using System;

namespace Unity.Burst
{
	// Token: 0x0200000C RID: 12
	internal enum BurstTargetCpu
	{
		// Token: 0x04000096 RID: 150
		Auto,
		// Token: 0x04000097 RID: 151
		X86_SSE2,
		// Token: 0x04000098 RID: 152
		X86_SSE4,
		// Token: 0x04000099 RID: 153
		X64_SSE2,
		// Token: 0x0400009A RID: 154
		X64_SSE4,
		// Token: 0x0400009B RID: 155
		AVX,
		// Token: 0x0400009C RID: 156
		AVX2,
		// Token: 0x0400009D RID: 157
		WASM32,
		// Token: 0x0400009E RID: 158
		ARMV7A_NEON32,
		// Token: 0x0400009F RID: 159
		ARMV8A_AARCH64,
		// Token: 0x040000A0 RID: 160
		THUMB2_NEON32,
		// Token: 0x040000A1 RID: 161
		ARMV8A_AARCH64_HALFFP,
		// Token: 0x040000A2 RID: 162
		ARMV9A
	}
}
