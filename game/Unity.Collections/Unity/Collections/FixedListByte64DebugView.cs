using System;

namespace Unity.Collections
{
	// Token: 0x02000070 RID: 112
	[Obsolete("FixedListByte64DebugView is deprecated. (UnityUpgradable) -> FixedList64BytesDebugView<byte>", true)]
	internal sealed class FixedListByte64DebugView
	{
		// Token: 0x0600033C RID: 828 RVA: 0x00008DA3 File Offset: 0x00006FA3
		public FixedListByte64DebugView(FixedList64Bytes<byte> list)
		{
			this.m_List = list;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00008DB2 File Offset: 0x00006FB2
		public byte[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000CB RID: 203
		private FixedList64Bytes<byte> m_List;
	}
}
