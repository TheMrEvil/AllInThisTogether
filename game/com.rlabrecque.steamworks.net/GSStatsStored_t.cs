using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000052 RID: 82
	[CallbackIdentity(1801)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GSStatsStored_t
	{
		// Token: 0x04000099 RID: 153
		public const int k_iCallback = 1801;

		// Token: 0x0400009A RID: 154
		public EResult m_eResult;

		// Token: 0x0400009B RID: 155
		public CSteamID m_steamIDUser;
	}
}
