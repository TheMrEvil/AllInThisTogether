using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000128 RID: 296
	internal sealed class UnsafeListTDebugView<[IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x06000AE2 RID: 2786 RVA: 0x000205A4 File Offset: 0x0001E7A4
		public UnsafeListTDebugView(UnsafeList<T> data)
		{
			this.Data = data;
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x000205B4 File Offset: 0x0001E7B4
		public unsafe T[] Items
		{
			get
			{
				T[] array = new T[this.Data.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.Data.Ptr[(IntPtr)i * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
				}
				return array;
			}
		}

		// Token: 0x0400038C RID: 908
		private UnsafeList<T> Data;
	}
}
