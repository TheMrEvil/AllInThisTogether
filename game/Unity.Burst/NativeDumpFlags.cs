using System;

namespace Unity.Burst
{
	// Token: 0x0200000D RID: 13
	[Flags]
	internal enum NativeDumpFlags
	{
		// Token: 0x040000A4 RID: 164
		None = 0,
		// Token: 0x040000A5 RID: 165
		IL = 1,
		// Token: 0x040000A6 RID: 166
		Unused = 2,
		// Token: 0x040000A7 RID: 167
		IR = 4,
		// Token: 0x040000A8 RID: 168
		IROptimized = 8,
		// Token: 0x040000A9 RID: 169
		Asm = 16,
		// Token: 0x040000AA RID: 170
		Function = 32,
		// Token: 0x040000AB RID: 171
		Analysis = 64,
		// Token: 0x040000AC RID: 172
		IRPassAnalysis = 128,
		// Token: 0x040000AD RID: 173
		ILPre = 256,
		// Token: 0x040000AE RID: 174
		IRPerEntryPoint = 512,
		// Token: 0x040000AF RID: 175
		All = 1021
	}
}
