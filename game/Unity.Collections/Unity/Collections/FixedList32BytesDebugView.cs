using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x02000058 RID: 88
	internal sealed class FixedList32BytesDebugView<[IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x0600020C RID: 524 RVA: 0x00005E7B File Offset: 0x0000407B
		public FixedList32BytesDebugView(FixedList32Bytes<T> list)
		{
			this.m_List = list;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00005E8A File Offset: 0x0000408A
		public T[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000B5 RID: 181
		private FixedList32Bytes<T> m_List;
	}
}
