using System;

namespace Unity.Collections
{
	// Token: 0x02000097 RID: 151
	internal sealed class NativeArrayDebugView<T> where T : struct
	{
		// Token: 0x060002AA RID: 682 RVA: 0x00005104 File Offset: 0x00003304
		public NativeArrayDebugView(NativeArray<T> array)
		{
			this.m_Array = array;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00005115 File Offset: 0x00003315
		public T[] Items
		{
			get
			{
				return this.m_Array.ToArray();
			}
		}

		// Token: 0x04000233 RID: 563
		private NativeArray<T> m_Array;
	}
}
