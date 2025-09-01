using System;

namespace Unity.Collections
{
	// Token: 0x0200006E RID: 110
	[Obsolete("FixedListByte32DebugView is deprecated. (UnityUpgradable) -> FixedList32BytesDebugView<byte>", true)]
	internal sealed class FixedListByte32DebugView
	{
		// Token: 0x0600033A RID: 826 RVA: 0x00008D87 File Offset: 0x00006F87
		public FixedListByte32DebugView(FixedList32Bytes<byte> list)
		{
			this.m_List = list;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00008D96 File Offset: 0x00006F96
		public byte[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000CA RID: 202
		private FixedList32Bytes<byte> m_List;
	}
}
