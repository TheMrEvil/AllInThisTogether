using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000082 RID: 130
	[CallbackIdentity(516)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct FavoritesListAccountsUpdated_t
	{
		// Token: 0x04000165 RID: 357
		public const int k_iCallback = 516;

		// Token: 0x04000166 RID: 358
		public EResult m_eResult;
	}
}
