using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x02000062 RID: 98
	internal sealed class FixedList128BytesDebugView<[IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x000075F3 File Offset: 0x000057F3
		public FixedList128BytesDebugView(FixedList128Bytes<T> list)
		{
			this.m_List = list;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00007602 File Offset: 0x00005802
		public T[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000BF RID: 191
		private FixedList128Bytes<T> m_List;
	}
}
