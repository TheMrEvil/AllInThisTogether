using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000053 RID: 83
	[CallbackIdentity(1108)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSStatsUnloaded_t
	{
		// Token: 0x0400009C RID: 156
		public const int k_iCallback = 1108;

		// Token: 0x0400009D RID: 157
		public CSteamID m_steamIDUser;
	}
}
