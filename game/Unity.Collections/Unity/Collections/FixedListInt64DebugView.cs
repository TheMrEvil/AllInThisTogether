using System;

namespace Unity.Collections
{
	// Token: 0x0200007A RID: 122
	[Obsolete("FixedListInt64DebugView is deprecated. (UnityUpgradable) -> FixedList64BytesDebugView<int>", true)]
	internal sealed class FixedListInt64DebugView
	{
		// Token: 0x06000346 RID: 838 RVA: 0x00008E2F File Offset: 0x0000702F
		public FixedListInt64DebugView(FixedList64Bytes<int> list)
		{
			this.m_List = list;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00008E3E File Offset: 0x0000703E
		public int[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000D0 RID: 208
		private FixedList64Bytes<int> m_List;
	}
}
