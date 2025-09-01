using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x0200006C RID: 108
	internal sealed class FixedList4096BytesDebugView<[IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x06000338 RID: 824 RVA: 0x00008D6B File Offset: 0x00006F6B
		public FixedList4096BytesDebugView(FixedList4096Bytes<T> list)
		{
			this.m_List = list;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00008D7A File Offset: 0x00006F7A
		public T[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000C9 RID: 201
		private FixedList4096Bytes<T> m_List;
	}
}
