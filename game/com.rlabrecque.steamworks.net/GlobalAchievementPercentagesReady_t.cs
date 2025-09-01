using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000F2 RID: 242
	[CallbackIdentity(1110)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GlobalAchievementPercentagesReady_t
	{
		// Token: 0x040002FB RID: 763
		public const int k_iCallback = 1110;

		// Token: 0x040002FC RID: 764
		public ulong m_nGameID;

		// Token: 0x040002FD RID: 765
		public EResult m_eResult;
	}
}
