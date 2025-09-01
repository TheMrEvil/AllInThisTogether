using System;

namespace Unity.Collections
{
	// Token: 0x02000080 RID: 128
	[Obsolete("FixedListInt4096DebugView is deprecated. (UnityUpgradable) -> FixedList4096BytesDebugView<int>", true)]
	internal sealed class FixedListInt4096DebugView
	{
		// Token: 0x0600034C RID: 844 RVA: 0x00008E83 File Offset: 0x00007083
		public FixedListInt4096DebugView(FixedList4096Bytes<int> list)
		{
			this.m_List = list;
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00008E92 File Offset: 0x00007092
		public int[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000D3 RID: 211
		private FixedList4096Bytes<int> m_List;
	}
}
