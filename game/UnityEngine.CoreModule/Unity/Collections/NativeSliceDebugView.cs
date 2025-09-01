using System;

namespace Unity.Collections
{
	// Token: 0x0200009C RID: 156
	internal sealed class NativeSliceDebugView<T> where T : struct
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x00005685 File Offset: 0x00003885
		public NativeSliceDebugView(NativeSlice<T> array)
		{
			this.m_Array = array;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00005698 File Offset: 0x00003898
		public T[] Items
		{
			get
			{
				return this.m_Array.ToArray();
			}
		}

		// Token: 0x0400023A RID: 570
		private NativeSlice<T> m_Array;
	}
}
