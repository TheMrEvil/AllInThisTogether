using System;

namespace Unity.Collections
{
	// Token: 0x02000076 RID: 118
	[Obsolete("FixedListByte4096DebugView is deprecated. (UnityUpgradable) -> FixedList4096BytesDebugView<byte>", true)]
	internal sealed class FixedListByte4096DebugView
	{
		// Token: 0x06000342 RID: 834 RVA: 0x00008DF7 File Offset: 0x00006FF7
		public FixedListByte4096DebugView(FixedList4096Bytes<byte> list)
		{
			this.m_List = list;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00008E06 File Offset: 0x00007006
		public byte[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000CE RID: 206
		private FixedList4096Bytes<byte> m_List;
	}
}
