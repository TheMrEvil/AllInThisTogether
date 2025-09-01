using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000086 RID: 134
	[CallbackIdentity(5212)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RequestPlayersForGameResultCallback_t
	{
		// Token: 0x04000178 RID: 376
		public const int k_iCallback = 5212;

		// Token: 0x04000179 RID: 377
		public EResult m_eResult;

		// Token: 0x0400017A RID: 378
		public ulong m_ullSearchID;

		// Token: 0x0400017B RID: 379
		public CSteamID m_SteamIDPlayerFound;

		// Token: 0x0400017C RID: 380
		public CSteamID m_SteamIDLobby;

		// Token: 0x0400017D RID: 381
		public PlayerAcceptState_t m_ePlayerAcceptState;

		// Token: 0x0400017E RID: 382
		public int m_nPlayerIndex;

		// Token: 0x0400017F RID: 383
		public int m_nTotalPlayersFound;

		// Token: 0x04000180 RID: 384
		public int m_nTotalPlayersAcceptedGame;

		// Token: 0x04000181 RID: 385
		public int m_nSuggestedTeamIndex;

		// Token: 0x04000182 RID: 386
		public ulong m_ullUniqueGameID;
	}
}
