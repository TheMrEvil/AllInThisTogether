using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000049 RID: 73
	[CallbackIdentity(203)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GSClientKick_t
	{
		// Token: 0x04000073 RID: 115
		public const int k_iCallback = 203;

		// Token: 0x04000074 RID: 116
		public CSteamID m_SteamID;

		// Token: 0x04000075 RID: 117
		public EDenyReason m_eDenyReason;
	}
}
