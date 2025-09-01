using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000088 RID: 136
	[CallbackIdentity(5214)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SubmitPlayerResultResultCallback_t
	{
		// Token: 0x04000187 RID: 391
		public const int k_iCallback = 5214;

		// Token: 0x04000188 RID: 392
		public EResult m_eResult;

		// Token: 0x04000189 RID: 393
		public ulong ullUniqueGameID;

		// Token: 0x0400018A RID: 394
		public CSteamID steamIDPlayer;
	}
}
