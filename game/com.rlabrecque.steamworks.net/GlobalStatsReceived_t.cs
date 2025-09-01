using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000F4 RID: 244
	[CallbackIdentity(1112)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GlobalStatsReceived_t
	{
		// Token: 0x04000301 RID: 769
		public const int k_iCallback = 1112;

		// Token: 0x04000302 RID: 770
		public ulong m_nGameID;

		// Token: 0x04000303 RID: 771
		public EResult m_eResult;
	}
}
