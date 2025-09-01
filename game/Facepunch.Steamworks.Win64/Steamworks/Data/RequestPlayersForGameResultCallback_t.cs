using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200010B RID: 267
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct RequestPlayersForGameResultCallback_t : ICallbackData
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x0001595E File Offset: 0x00013B5E
		public int DataSize
		{
			get
			{
				return RequestPlayersForGameResultCallback_t._datasize;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x00015965 File Offset: 0x00013B65
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RequestPlayersForGameResultCallback;
			}
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0001596C File Offset: 0x00013B6C
		// Note: this type is marked as 'beforefieldinit'.
		static RequestPlayersForGameResultCallback_t()
		{
		}

		// Token: 0x040008A1 RID: 2209
		internal Result Result;

		// Token: 0x040008A2 RID: 2210
		internal ulong LSearchID;

		// Token: 0x040008A3 RID: 2211
		internal ulong SteamIDPlayerFound;

		// Token: 0x040008A4 RID: 2212
		internal ulong SteamIDLobby;

		// Token: 0x040008A5 RID: 2213
		internal RequestPlayersForGameResultCallback_t.PlayerAcceptState_t PlayerAcceptState;

		// Token: 0x040008A6 RID: 2214
		internal int PlayerIndex;

		// Token: 0x040008A7 RID: 2215
		internal int TotalPlayersFound;

		// Token: 0x040008A8 RID: 2216
		internal int TotalPlayersAcceptedGame;

		// Token: 0x040008A9 RID: 2217
		internal int SuggestedTeamIndex;

		// Token: 0x040008AA RID: 2218
		internal ulong LUniqueGameID;

		// Token: 0x040008AB RID: 2219
		public static int _datasize = Marshal.SizeOf(typeof(RequestPlayersForGameResultCallback_t));

		// Token: 0x02000281 RID: 641
		internal enum PlayerAcceptState_t
		{
			// Token: 0x04000EFA RID: 3834
			Unknown,
			// Token: 0x04000EFB RID: 3835
			PlayerAccepted,
			// Token: 0x04000EFC RID: 3836
			PlayerDeclined
		}
	}
}
