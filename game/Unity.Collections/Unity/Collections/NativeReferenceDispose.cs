using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000D3 RID: 211
	[NativeContainer]
	internal struct NativeReferenceDispose
	{
		// Token: 0x060007D2 RID: 2002 RVA: 0x00017EE3 File Offset: 0x000160E3
		public void Dispose()
		{
			Memory.Unmanaged.Free(this.m_Data, this.m_AllocatorLabel);
		}

		// Token: 0x040002B5 RID: 693
		[NativeDisableUnsafePtrRestriction]
		internal unsafe void* m_Data;

		// Token: 0x040002B6 RID: 694
		internal AllocatorManager.AllocatorHandle m_AllocatorLabel;
	}
}
