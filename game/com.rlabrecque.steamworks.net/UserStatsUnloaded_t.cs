using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000F0 RID: 240
	[CallbackIdentity(1108)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserStatsUnloaded_t
	{
		// Token: 0x040002F4 RID: 756
		public const int k_iCallback = 1108;

		// Token: 0x040002F5 RID: 757
		public CSteamID m_steamIDUser;
	}
}
