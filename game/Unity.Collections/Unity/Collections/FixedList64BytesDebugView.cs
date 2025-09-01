using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x0200005D RID: 93
	internal sealed class FixedList64BytesDebugView<[IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x06000257 RID: 599 RVA: 0x00006A37 File Offset: 0x00004C37
		public FixedList64BytesDebugView(FixedList64Bytes<T> list)
		{
			this.m_List = list;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00006A46 File Offset: 0x00004C46
		public T[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000BA RID: 186
		private FixedList64Bytes<T> m_List;
	}
}
