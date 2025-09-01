using System;

namespace Unity.Collections
{
	// Token: 0x0200008A RID: 138
	[Obsolete("FixedListFloat4096DebugView is deprecated. (UnityUpgradable) -> FixedList4096BytesDebugView<float>", true)]
	internal sealed class FixedListFloat4096DebugView
	{
		// Token: 0x06000356 RID: 854 RVA: 0x00008F0F File Offset: 0x0000710F
		public FixedListFloat4096DebugView(FixedList4096Bytes<float> list)
		{
			this.m_List = list;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00008F1E File Offset: 0x0000711E
		public float[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000D8 RID: 216
		private FixedList4096Bytes<float> m_List;
	}
}
