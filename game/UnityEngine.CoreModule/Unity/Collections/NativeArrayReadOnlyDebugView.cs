using System;

namespace Unity.Collections
{
	// Token: 0x02000098 RID: 152
	internal sealed class NativeArrayReadOnlyDebugView<T> where T : struct
	{
		// Token: 0x060002AC RID: 684 RVA: 0x00005122 File Offset: 0x00003322
		public NativeArrayReadOnlyDebugView(NativeArray<T>.ReadOnly array)
		{
			this.m_Array = array;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00005133 File Offset: 0x00003333
		public T[] Items
		{
			get
			{
				return this.m_Array.ToArray();
			}
		}

		// Token: 0x04000234 RID: 564
		private NativeArray<T>.ReadOnly m_Array;
	}
}
