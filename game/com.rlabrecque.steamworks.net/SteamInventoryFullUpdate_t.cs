using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000073 RID: 115
	[CallbackIdentity(4701)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamInventoryFullUpdate_t
	{
		// Token: 0x04000129 RID: 297
		public const int k_iCallback = 4701;

		// Token: 0x0400012A RID: 298
		public SteamInventoryResult_t m_handle;
	}
}
