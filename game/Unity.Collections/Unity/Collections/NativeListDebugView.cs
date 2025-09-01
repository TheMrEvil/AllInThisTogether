using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x020000B6 RID: 182
	internal sealed class NativeListDebugView<[IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x06000707 RID: 1799 RVA: 0x0001607F File Offset: 0x0001427F
		public NativeListDebugView(NativeList<T> array)
		{
			this.m_Array = array;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x00016090 File Offset: 0x00014290
		public T[] Items
		{
			get
			{
				return this.m_Array.AsArray().ToArray();
			}
		}

		// Token: 0x04000284 RID: 644
		private NativeList<T> m_Array;
	}
}
