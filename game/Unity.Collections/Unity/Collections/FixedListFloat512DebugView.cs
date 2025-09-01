using System;

namespace Unity.Collections
{
	// Token: 0x02000088 RID: 136
	[Obsolete("FixedListFloat512DebugView is deprecated. (UnityUpgradable) -> FixedList512BytesDebugView<float>", true)]
	internal sealed class FixedListFloat512DebugView
	{
		// Token: 0x06000354 RID: 852 RVA: 0x00008EF3 File Offset: 0x000070F3
		public FixedListFloat512DebugView(FixedList512Bytes<float> list)
		{
			this.m_List = list;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00008F02 File Offset: 0x00007102
		public float[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000D7 RID: 215
		private FixedList512Bytes<float> m_List;
	}
}
