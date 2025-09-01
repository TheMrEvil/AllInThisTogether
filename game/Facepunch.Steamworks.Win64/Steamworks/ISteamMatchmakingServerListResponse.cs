using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x0200001D RID: 29
	internal class ISteamMatchmakingServerListResponse : SteamInterface
	{
		// Token: 0x060003A8 RID: 936 RVA: 0x00007045 File Offset: 0x00005245
		internal ISteamMatchmakingServerListResponse(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x060003A9 RID: 937
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServerListResponse_ServerResponded")]
		private static extern void _ServerResponded(IntPtr self, HServerListRequest hRequest, int iServer);

		// Token: 0x060003AA RID: 938 RVA: 0x00007057 File Offset: 0x00005257
		internal void ServerResponded(HServerListRequest hRequest, int iServer)
		{
			ISteamMatchmakingServerListResponse._ServerResponded(this.Self, hRequest, iServer);
		}

		// Token: 0x060003AB RID: 939
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServerListResponse_ServerFailedToRespond")]
		private static extern void _ServerFailedToRespond(IntPtr self, HServerListRequest hRequest, int iServer);

		// Token: 0x060003AC RID: 940 RVA: 0x00007068 File Offset: 0x00005268
		internal void ServerFailedToRespond(HServerListRequest hRequest, int iServer)
		{
			ISteamMatchmakingServerListResponse._ServerFailedToRespond(this.Self, hRequest, iServer);
		}

		// Token: 0x060003AD RID: 941
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServerListResponse_RefreshComplete")]
		private static extern void _RefreshComplete(IntPtr self, HServerListRequest hRequest, MatchMakingServerResponse response);

		// Token: 0x060003AE RID: 942 RVA: 0x00007079 File Offset: 0x00005279
		internal void RefreshComplete(HServerListRequest hRequest, MatchMakingServerResponse response)
		{
			ISteamMatchmakingServerListResponse._RefreshComplete(this.Self, hRequest, response);
		}
	}
}
