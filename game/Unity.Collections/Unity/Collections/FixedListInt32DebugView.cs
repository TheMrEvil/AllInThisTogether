using System;

namespace Unity.Collections
{
	// Token: 0x02000078 RID: 120
	[Obsolete("FixedListInt32DebugView is deprecated. (UnityUpgradable) -> FixedList32BytesDebugView<int>", true)]
	internal sealed class FixedListInt32DebugView
	{
		// Token: 0x06000344 RID: 836 RVA: 0x00008E13 File Offset: 0x00007013
		public FixedListInt32DebugView(FixedList32Bytes<int> list)
		{
			this.m_List = list;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00008E22 File Offset: 0x00007022
		public int[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000CF RID: 207
		private FixedList32Bytes<int> m_List;
	}
}
