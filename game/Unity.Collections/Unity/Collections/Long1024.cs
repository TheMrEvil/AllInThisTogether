using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x02000043 RID: 67
	internal struct Long1024 : IIndexable<long>
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00004596 File Offset: 0x00002796
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00002C2B File Offset: 0x00000E2B
		public int Length
		{
			get
			{
				return 1024;
			}
			set
			{
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000045A0 File Offset: 0x000027A0
		public unsafe ref long ElementAt(int index)
		{
			fixed (Long512* ptr = &this.f0)
			{
				return UnsafeUtility.AsRef<long>((void*)((byte*)ptr + (IntPtr)index * 8));
			}
		}

		// Token: 0x04000094 RID: 148
		internal Long512 f0;

		// Token: 0x04000095 RID: 149
		internal Long512 f1;
	}
}
