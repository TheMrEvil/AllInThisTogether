using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200001B RID: 27
	internal class ISteamMatchmakingPlayersResponse : SteamInterface
	{
		// Token: 0x0600039A RID: 922 RVA: 0x00006FC2 File Offset: 0x000051C2
		internal ISteamMatchmakingPlayersResponse(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x0600039B RID: 923
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingPlayersResponse_AddPlayerToList")]
		private static extern void _AddPlayerToList(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, int nScore, float flTimePlayed);

		// Token: 0x0600039C RID: 924 RVA: 0x00006FD4 File Offset: 0x000051D4
		internal void AddPlayerToList([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchName, int nScore, float flTimePlayed)
		{
			ISteamMatchmakingPlayersResponse._AddPlayerToList(this.Self, pchName, nScore, flTimePlayed);
		}

		// Token: 0x0600039D RID: 925
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingPlayersResponse_PlayersFailedToRespond")]
		private static extern void _PlayersFailedToRespond(IntPtr self);

		// Token: 0x0600039E RID: 926 RVA: 0x00006FE6 File Offset: 0x000051E6
		internal void PlayersFailedToRespond()
		{
			ISteamMatchmakingPlayersResponse._PlayersFailedToRespond(this.Self);
		}

		// Token: 0x0600039F RID: 927
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingPlayersResponse_PlayersRefreshComplete")]
		private static extern void _PlayersRefreshComplete(IntPtr self);

		// Token: 0x060003A0 RID: 928 RVA: 0x00006FF5 File Offset: 0x000051F5
		internal void PlayersRefreshComplete()
		{
			ISteamMatchmakingPlayersResponse._PlayersRefreshComplete(this.Self);
		}
	}
}
