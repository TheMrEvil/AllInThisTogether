using System;

namespace Unity.Collections
{
	// Token: 0x0200007C RID: 124
	[Obsolete("FixedListInt128DebugView is deprecated. (UnityUpgradable) -> FixedList128BytesDebugView<int>", true)]
	internal sealed class FixedListInt128DebugView
	{
		// Token: 0x06000348 RID: 840 RVA: 0x00008E4B File Offset: 0x0000704B
		public FixedListInt128DebugView(FixedList128Bytes<int> list)
		{
			this.m_List = list;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00008E5A File Offset: 0x0000705A
		public int[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000D1 RID: 209
		private FixedList128Bytes<int> m_List;
	}
}
