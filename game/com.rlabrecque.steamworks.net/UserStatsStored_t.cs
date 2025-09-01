using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000EA RID: 234
	[CallbackIdentity(1102)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserStatsStored_t
	{
		// Token: 0x040002DA RID: 730
		public const int k_iCallback = 1102;

		// Token: 0x040002DB RID: 731
		public ulong m_nGameID;

		// Token: 0x040002DC RID: 732
		public EResult m_eResult;
	}
}
