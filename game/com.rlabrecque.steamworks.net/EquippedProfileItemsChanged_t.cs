using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000043 RID: 67
	[CallbackIdentity(350)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct EquippedProfileItemsChanged_t
	{
		// Token: 0x0400005F RID: 95
		public const int k_iCallback = 350;

		// Token: 0x04000060 RID: 96
		public CSteamID m_steamID;
	}
}
