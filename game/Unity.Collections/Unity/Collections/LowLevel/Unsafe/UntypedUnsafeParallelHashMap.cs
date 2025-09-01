using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200013B RID: 315
	public struct UntypedUnsafeParallelHashMap
	{
		// Token: 0x040003B9 RID: 953
		[NativeDisableUnsafePtrRestriction]
		private unsafe UnsafeParallelHashMapData* m_Buffer;

		// Token: 0x040003BA RID: 954
		private AllocatorManager.AllocatorHandle m_AllocatorLabel;
	}
}
