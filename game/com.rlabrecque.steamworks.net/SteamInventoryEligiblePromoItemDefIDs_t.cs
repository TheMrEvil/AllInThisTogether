using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000075 RID: 117
	[CallbackIdentity(4703)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamInventoryEligiblePromoItemDefIDs_t
	{
		// Token: 0x0400012C RID: 300
		public const int k_iCallback = 4703;

		// Token: 0x0400012D RID: 301
		public EResult m_result;

		// Token: 0x0400012E RID: 302
		public CSteamID m_steamID;

		// Token: 0x0400012F RID: 303
		public int m_numEligiblePromoItemDefs;

		// Token: 0x04000130 RID: 304
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bCachedData;
	}
}
