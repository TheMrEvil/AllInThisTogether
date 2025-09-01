using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x02000095 RID: 149
	[NativeContainer]
	internal struct NativeArrayDispose
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x000050E0 File Offset: 0x000032E0
		public void Dispose()
		{
			UnsafeUtility.Free(this.m_Buffer, this.m_AllocatorLabel);
		}

		// Token: 0x04000230 RID: 560
		[NativeDisableUnsafePtrRestriction]
		internal unsafe void* m_Buffer;

		// Token: 0x04000231 RID: 561
		internal Allocator m_AllocatorLabel;
	}
}
