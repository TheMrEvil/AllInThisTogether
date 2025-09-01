using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000173 RID: 371
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamPartyBeaconLocation_t
	{
		// Token: 0x040009DE RID: 2526
		public ESteamPartyBeaconLocationType m_eType;

		// Token: 0x040009DF RID: 2527
		public ulong m_ulLocationID;
	}
}
