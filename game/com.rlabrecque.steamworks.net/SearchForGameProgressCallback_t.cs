using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000083 RID: 131
	[CallbackIdentity(5201)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SearchForGameProgressCallback_t
	{
		// Token: 0x04000167 RID: 359
		public const int k_iCallback = 5201;

		// Token: 0x04000168 RID: 360
		public ulong m_ullSearchID;

		// Token: 0x04000169 RID: 361
		public EResult m_eResult;

		// Token: 0x0400016A RID: 362
		public CSteamID m_lobbyID;

		// Token: 0x0400016B RID: 363
		public CSteamID m_steamIDEndedSearch;

		// Token: 0x0400016C RID: 364
		public int m_nSecondsRemainingEstimate;

		// Token: 0x0400016D RID: 365
		public int m_cPlayersSearching;
	}
}
