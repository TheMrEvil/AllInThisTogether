using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000050 RID: 80
	[CallbackIdentity(211)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ComputeNewPlayerCompatibilityResult_t
	{
		// Token: 0x04000090 RID: 144
		public const int k_iCallback = 211;

		// Token: 0x04000091 RID: 145
		public EResult m_eResult;

		// Token: 0x04000092 RID: 146
		public int m_cPlayersThatDontLikeCandidate;

		// Token: 0x04000093 RID: 147
		public int m_cPlayersThatCandidateDoesntLike;

		// Token: 0x04000094 RID: 148
		public int m_cClanPlayersThatDontLikeCandidate;

		// Token: 0x04000095 RID: 149
		public CSteamID m_SteamIDCandidate;
	}
}
