using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020001A9 RID: 425
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct FriendGameInfo_t
	{
		// Token: 0x04000B55 RID: 2901
		internal GameId GameID;

		// Token: 0x04000B56 RID: 2902
		internal uint GameIP;

		// Token: 0x04000B57 RID: 2903
		internal ushort GamePort;

		// Token: 0x04000B58 RID: 2904
		internal ushort QueryPort;

		// Token: 0x04000B59 RID: 2905
		internal ulong SteamIDLobby;
	}
}
