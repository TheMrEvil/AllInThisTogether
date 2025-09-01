using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000172 RID: 370
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamItemDetails_t
	{
		// Token: 0x040009DA RID: 2522
		public SteamItemInstanceID_t m_itemId;

		// Token: 0x040009DB RID: 2523
		public SteamItemDef_t m_iDefinition;

		// Token: 0x040009DC RID: 2524
		public ushort m_unQuantity;

		// Token: 0x040009DD RID: 2525
		public ushort m_unFlags;
	}
}
