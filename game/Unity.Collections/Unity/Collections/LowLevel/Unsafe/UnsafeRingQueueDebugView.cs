using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000148 RID: 328
	internal sealed class UnsafeRingQueueDebugView<[IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x06000BDF RID: 3039 RVA: 0x00023673 File Offset: 0x00021873
		public UnsafeRingQueueDebugView(UnsafeRingQueue<T> data)
		{
			this.Data = data;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x00023684 File Offset: 0x00021884
		public unsafe T[] Items
		{
			get
			{
				T[] array = new T[this.Data.Length];
				int read = this.Data.Control.Read;
				int capacity = this.Data.Control.Capacity;
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.Data.Ptr[(IntPtr)((read + i) % capacity) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
				}
				return array;
			}
		}

		// Token: 0x040003D1 RID: 977
		private UnsafeRingQueue<T> Data;
	}
}
