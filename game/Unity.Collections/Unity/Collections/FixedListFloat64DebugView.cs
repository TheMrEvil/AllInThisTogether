using System;

namespace Unity.Collections
{
	// Token: 0x02000084 RID: 132
	[Obsolete("FixedListFloat64DebugView is deprecated. (UnityUpgradable) -> FixedList64BytesDebugView<float>", true)]
	internal sealed class FixedListFloat64DebugView
	{
		// Token: 0x06000350 RID: 848 RVA: 0x00008EBB File Offset: 0x000070BB
		public FixedListFloat64DebugView(FixedList64Bytes<float> list)
		{
			this.m_List = list;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00008ECA File Offset: 0x000070CA
		public float[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000D5 RID: 213
		private FixedList64Bytes<float> m_List;
	}
}
