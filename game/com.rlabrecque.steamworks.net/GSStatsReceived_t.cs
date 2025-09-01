using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000051 RID: 81
	[CallbackIdentity(1800)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GSStatsReceived_t
	{
		// Token: 0x04000096 RID: 150
		public const int k_iCallback = 1800;

		// Token: 0x04000097 RID: 151
		public EResult m_eResult;

		// Token: 0x04000098 RID: 152
		public CSteamID m_steamIDUser;
	}
}
