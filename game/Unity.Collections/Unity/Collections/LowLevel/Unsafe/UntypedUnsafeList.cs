using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000122 RID: 290
	internal struct UntypedUnsafeList
	{
		// Token: 0x0400037A RID: 890
		[NativeDisableUnsafePtrRestriction]
		public unsafe void* Ptr;

		// Token: 0x0400037B RID: 891
		public int m_length;

		// Token: 0x0400037C RID: 892
		public int m_capacity;

		// Token: 0x0400037D RID: 893
		public AllocatorManager.AllocatorHandle Allocator;

		// Token: 0x0400037E RID: 894
		internal int obsolete_length;

		// Token: 0x0400037F RID: 895
		internal int obsolete_capacity;
	}
}
