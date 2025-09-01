using System;

namespace Unity.Collections
{
	// Token: 0x02000074 RID: 116
	[Obsolete("FixedListByte512DebugView is deprecated. (UnityUpgradable) -> FixedList512BytesDebugView<byte>", true)]
	internal sealed class FixedListByte512DebugView
	{
		// Token: 0x06000340 RID: 832 RVA: 0x00008DDB File Offset: 0x00006FDB
		public FixedListByte512DebugView(FixedList512Bytes<byte> list)
		{
			this.m_List = list;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00008DEA File Offset: 0x00006FEA
		public byte[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000CD RID: 205
		private FixedList512Bytes<byte> m_List;
	}
}
