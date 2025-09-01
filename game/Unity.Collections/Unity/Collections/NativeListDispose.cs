using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000B4 RID: 180
	[NativeContainer]
	[BurstCompatible]
	internal struct NativeListDispose
	{
		// Token: 0x06000705 RID: 1797 RVA: 0x00016058 File Offset: 0x00014258
		public unsafe void Dispose()
		{
			UnsafeList<int>* listData = (UnsafeList<int>*)this.m_ListData;
			UnsafeList<int>.Destroy(listData);
		}

		// Token: 0x04000282 RID: 642
		[NativeDisableUnsafePtrRestriction]
		public unsafe UntypedUnsafeList* m_ListData;
	}
}
