using System;

namespace Unity.Audio
{
	// Token: 0x02000007 RID: 7
	internal struct DSPGraphExecutionNode
	{
		// Token: 0x04000008 RID: 8
		public unsafe void* ReflectionData;

		// Token: 0x04000009 RID: 9
		public unsafe void* JobStructData;

		// Token: 0x0400000A RID: 10
		public unsafe void* JobData;

		// Token: 0x0400000B RID: 11
		public unsafe void* ResourceContext;

		// Token: 0x0400000C RID: 12
		public int FunctionIndex;

		// Token: 0x0400000D RID: 13
		public int FenceIndex;

		// Token: 0x0400000E RID: 14
		public int FenceCount;
	}
}
