using System;

namespace Unity.Collections
{
	// Token: 0x02000082 RID: 130
	[Obsolete("FixedListFloat32DebugView is deprecated. (UnityUpgradable) -> FixedList32BytesDebugView<float>", true)]
	internal sealed class FixedListFloat32DebugView
	{
		// Token: 0x0600034E RID: 846 RVA: 0x00008E9F File Offset: 0x0000709F
		public FixedListFloat32DebugView(FixedList32Bytes<float> list)
		{
			this.m_List = list;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00008EAE File Offset: 0x000070AE
		public float[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000D4 RID: 212
		private FixedList32Bytes<float> m_List;
	}
}
