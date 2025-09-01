using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D8 RID: 216
	[CallbackIdentity(3418)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserSubscribedItemsListChanged_t
	{
		// Token: 0x04000299 RID: 665
		public const int k_iCallback = 3418;

		// Token: 0x0400029A RID: 666
		public AppId_t m_nAppID;
	}
}
