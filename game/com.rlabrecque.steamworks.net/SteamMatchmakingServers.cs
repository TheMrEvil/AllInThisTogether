using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000015 RID: 21
	public static class SteamMatchmakingServers
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x000080AD File Offset: 0x000062AD
		public static HServerListRequest RequestInternetServerList(AppId_t iApp, MatchMakingKeyValuePair_t[] ppchFilters, uint nFilters, ISteamMatchmakingServerListResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerListRequest)NativeMethods.ISteamMatchmakingServers_RequestInternetServerList(CSteamAPIContext.GetSteamMatchmakingServers(), iApp, new MMKVPMarshaller(ppchFilters), nFilters, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000080D6 File Offset: 0x000062D6
		public static HServerListRequest RequestLANServerList(AppId_t iApp, ISteamMatchmakingServerListResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerListRequest)NativeMethods.ISteamMatchmakingServers_RequestLANServerList(CSteamAPIContext.GetSteamMatchmakingServers(), iApp, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x000080F3 File Offset: 0x000062F3
		public static HServerListRequest RequestFriendsServerList(AppId_t iApp, MatchMakingKeyValuePair_t[] ppchFilters, uint nFilters, ISteamMatchmakingServerListResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerListRequest)NativeMethods.ISteamMatchmakingServers_RequestFriendsServerList(CSteamAPIContext.GetSteamMatchmakingServers(), iApp, new MMKVPMarshaller(ppchFilters), nFilters, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000811C File Offset: 0x0000631C
		public static HServerListRequest RequestFavoritesServerList(AppId_t iApp, MatchMakingKeyValuePair_t[] ppchFilters, uint nFilters, ISteamMatchmakingServerListResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerListRequest)NativeMethods.ISteamMatchmakingServers_RequestFavoritesServerList(CSteamAPIContext.GetSteamMatchmakingServers(), iApp, new MMKVPMarshaller(ppchFilters), nFilters, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00008145 File Offset: 0x00006345
		public static HServerListRequest RequestHistoryServerList(AppId_t iApp, MatchMakingKeyValuePair_t[] ppchFilters, uint nFilters, ISteamMatchmakingServerListResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerListRequest)NativeMethods.ISteamMatchmakingServers_RequestHistoryServerList(CSteamAPIContext.GetSteamMatchmakingServers(), iApp, new MMKVPMarshaller(ppchFilters), nFilters, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000816E File Offset: 0x0000636E
		public static HServerListRequest RequestSpectatorServerList(AppId_t iApp, MatchMakingKeyValuePair_t[] ppchFilters, uint nFilters, ISteamMatchmakingServerListResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerListRequest)NativeMethods.ISteamMatchmakingServers_RequestSpectatorServerList(CSteamAPIContext.GetSteamMatchmakingServers(), iApp, new MMKVPMarshaller(ppchFilters), nFilters, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00008197 File Offset: 0x00006397
		public static void ReleaseRequest(HServerListRequest hServerListRequest)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMatchmakingServers_ReleaseRequest(CSteamAPIContext.GetSteamMatchmakingServers(), hServerListRequest);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000081A9 File Offset: 0x000063A9
		public static gameserveritem_t GetServerDetails(HServerListRequest hRequest, int iServer)
		{
			InteropHelp.TestIfAvailableClient();
			return (gameserveritem_t)Marshal.PtrToStructure(NativeMethods.ISteamMatchmakingServers_GetServerDetails(CSteamAPIContext.GetSteamMatchmakingServers(), hRequest, iServer), typeof(gameserveritem_t));
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000081D0 File Offset: 0x000063D0
		public static void CancelQuery(HServerListRequest hRequest)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMatchmakingServers_CancelQuery(CSteamAPIContext.GetSteamMatchmakingServers(), hRequest);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x000081E2 File Offset: 0x000063E2
		public static void RefreshQuery(HServerListRequest hRequest)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMatchmakingServers_RefreshQuery(CSteamAPIContext.GetSteamMatchmakingServers(), hRequest);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000081F4 File Offset: 0x000063F4
		public static bool IsRefreshing(HServerListRequest hRequest)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMatchmakingServers_IsRefreshing(CSteamAPIContext.GetSteamMatchmakingServers(), hRequest);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00008206 File Offset: 0x00006406
		public static int GetServerCount(HServerListRequest hRequest)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMatchmakingServers_GetServerCount(CSteamAPIContext.GetSteamMatchmakingServers(), hRequest);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00008218 File Offset: 0x00006418
		public static void RefreshServer(HServerListRequest hRequest, int iServer)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMatchmakingServers_RefreshServer(CSteamAPIContext.GetSteamMatchmakingServers(), hRequest, iServer);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000822B File Offset: 0x0000642B
		public static HServerQuery PingServer(uint unIP, ushort usPort, ISteamMatchmakingPingResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerQuery)NativeMethods.ISteamMatchmakingServers_PingServer(CSteamAPIContext.GetSteamMatchmakingServers(), unIP, usPort, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00008249 File Offset: 0x00006449
		public static HServerQuery PlayerDetails(uint unIP, ushort usPort, ISteamMatchmakingPlayersResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerQuery)NativeMethods.ISteamMatchmakingServers_PlayerDetails(CSteamAPIContext.GetSteamMatchmakingServers(), unIP, usPort, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00008267 File Offset: 0x00006467
		public static HServerQuery ServerRules(uint unIP, ushort usPort, ISteamMatchmakingRulesResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerQuery)NativeMethods.ISteamMatchmakingServers_ServerRules(CSteamAPIContext.GetSteamMatchmakingServers(), unIP, usPort, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00008285 File Offset: 0x00006485
		public static void CancelServerQuery(HServerQuery hServerQuery)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMatchmakingServers_CancelServerQuery(CSteamAPIContext.GetSteamMatchmakingServers(), hServerQuery);
		}
	}
}
