using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200010D RID: 269
	[Obsolete("UntypedUnsafeHashMap is renamed to UntypedUnsafeParallelHashMap. (UnityUpgradable) -> UntypedUnsafeParallelHashMap", false)]
	public struct UntypedUnsafeHashMap
	{
		// Token: 0x04000356 RID: 854
		[NativeDisableUnsafePtrRestriction]
		private unsafe UnsafeParallelHashMapData* m_Buffer;

		// Token: 0x04000357 RID: 855
		private AllocatorManager.AllocatorHandle m_AllocatorLabel;
	}
}
