using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x02000067 RID: 103
	internal sealed class FixedList512BytesDebugView<[IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x060002ED RID: 749 RVA: 0x000081AF File Offset: 0x000063AF
		public FixedList512BytesDebugView(FixedList512Bytes<T> list)
		{
			this.m_List = list;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002EE RID: 750 RVA: 0x000081BE File Offset: 0x000063BE
		public T[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000C4 RID: 196
		private FixedList512Bytes<T> m_List;
	}
}
