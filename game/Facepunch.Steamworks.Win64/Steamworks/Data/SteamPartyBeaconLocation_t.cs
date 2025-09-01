using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020001AB RID: 427
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamPartyBeaconLocation_t
	{
		// Token: 0x04000B5C RID: 2908
		internal SteamPartyBeaconLocationType Type;

		// Token: 0x04000B5D RID: 2909
		internal ulong LocationID;
	}
}
