using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000104 RID: 260
	internal sealed class UnsafePtrListDebugView
	{
		// Token: 0x060009D2 RID: 2514 RVA: 0x0001D9B4 File Offset: 0x0001BBB4
		public UnsafePtrListDebugView(UnsafePtrList data)
		{
			this.Data = data;
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x0001D9C4 File Offset: 0x0001BBC4
		public unsafe IntPtr[] Items
		{
			get
			{
				IntPtr[] array = new IntPtr[this.Data.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (IntPtr)(*(IntPtr*)(this.Data.Ptr + (IntPtr)i * (IntPtr)sizeof(void*) / (IntPtr)sizeof(void*)));
				}
				return array;
			}
		}

		// Token: 0x0400033A RID: 826
		private UnsafePtrList Data;
	}
}
