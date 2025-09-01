using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x0200001E RID: 30
	internal class ISteamMatchmakingServers : SteamInterface
	{
		// Token: 0x060003AF RID: 943 RVA: 0x0000708A File Offset: 0x0000528A
		internal ISteamMatchmakingServers(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x060003B0 RID: 944
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamMatchmakingServers_v002();

		// Token: 0x060003B1 RID: 945 RVA: 0x0000709C File Offset: 0x0000529C
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamMatchmakingServers.SteamAPI_SteamMatchmakingServers_v002();
		}

		// Token: 0x060003B2 RID: 946
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestInternetServerList")]
		private static extern HServerListRequest _RequestInternetServerList(IntPtr self, AppId iApp, [In] [Out] ref MatchMakingKeyValuePair[] ppchFilters, uint nFilters, IntPtr pRequestServersResponse);

		// Token: 0x060003B3 RID: 947 RVA: 0x000070A4 File Offset: 0x000052A4
		internal HServerListRequest RequestInternetServerList(AppId iApp, [In] [Out] ref MatchMakingKeyValuePair[] ppchFilters, uint nFilters, IntPtr pRequestServersResponse)
		{
			return ISteamMatchmakingServers._RequestInternetServerList(this.Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse);
		}

		// Token: 0x060003B4 RID: 948
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestLANServerList")]
		private static extern HServerListRequest _RequestLANServerList(IntPtr self, AppId iApp, IntPtr pRequestServersResponse);

		// Token: 0x060003B5 RID: 949 RVA: 0x000070C8 File Offset: 0x000052C8
		internal HServerListRequest RequestLANServerList(AppId iApp, IntPtr pRequestServersResponse)
		{
			return ISteamMatchmakingServers._RequestLANServerList(this.Self, iApp, pRequestServersResponse);
		}

		// Token: 0x060003B6 RID: 950
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestFriendsServerList")]
		private static extern HServerListRequest _RequestFriendsServerList(IntPtr self, AppId iApp, [In] [Out] ref MatchMakingKeyValuePair[] ppchFilters, uint nFilters, IntPtr pRequestServersResponse);

		// Token: 0x060003B7 RID: 951 RVA: 0x000070EC File Offset: 0x000052EC
		internal HServerListRequest RequestFriendsServerList(AppId iApp, [In] [Out] ref MatchMakingKeyValuePair[] ppchFilters, uint nFilters, IntPtr pRequestServersResponse)
		{
			return ISteamMatchmakingServers._RequestFriendsServerList(this.Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse);
		}

		// Token: 0x060003B8 RID: 952
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestFavoritesServerList")]
		private static extern HServerListRequest _RequestFavoritesServerList(IntPtr self, AppId iApp, [In] [Out] ref MatchMakingKeyValuePair[] ppchFilters, uint nFilters, IntPtr pRequestServersResponse);

		// Token: 0x060003B9 RID: 953 RVA: 0x00007110 File Offset: 0x00005310
		internal HServerListRequest RequestFavoritesServerList(AppId iApp, [In] [Out] ref MatchMakingKeyValuePair[] ppchFilters, uint nFilters, IntPtr pRequestServersResponse)
		{
			return ISteamMatchmakingServers._RequestFavoritesServerList(this.Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse);
		}

		// Token: 0x060003BA RID: 954
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestHistoryServerList")]
		private static extern HServerListRequest _RequestHistoryServerList(IntPtr self, AppId iApp, [In] [Out] ref MatchMakingKeyValuePair[] ppchFilters, uint nFilters, IntPtr pRequestServersResponse);

		// Token: 0x060003BB RID: 955 RVA: 0x00007134 File Offset: 0x00005334
		internal HServerListRequest RequestHistoryServerList(AppId iApp, [In] [Out] ref MatchMakingKeyValuePair[] ppchFilters, uint nFilters, IntPtr pRequestServersResponse)
		{
			return ISteamMatchmakingServers._RequestHistoryServerList(this.Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse);
		}

		// Token: 0x060003BC RID: 956
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestSpectatorServerList")]
		private static extern HServerListRequest _RequestSpectatorServerList(IntPtr self, AppId iApp, [In] [Out] ref MatchMakingKeyValuePair[] ppchFilters, uint nFilters, IntPtr pRequestServersResponse);

		// Token: 0x060003BD RID: 957 RVA: 0x00007158 File Offset: 0x00005358
		internal HServerListRequest RequestSpectatorServerList(AppId iApp, [In] [Out] ref MatchMakingKeyValuePair[] ppchFilters, uint nFilters, IntPtr pRequestServersResponse)
		{
			return ISteamMatchmakingServers._RequestSpectatorServerList(this.Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse);
		}

		// Token: 0x060003BE RID: 958
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_ReleaseRequest")]
		private static extern void _ReleaseRequest(IntPtr self, HServerListRequest hServerListRequest);

		// Token: 0x060003BF RID: 959 RVA: 0x0000717C File Offset: 0x0000537C
		internal void ReleaseRequest(HServerListRequest hServerListRequest)
		{
			ISteamMatchmakingServers._ReleaseRequest(this.Self, hServerListRequest);
		}

		// Token: 0x060003C0 RID: 960
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_GetServerDetails")]
		private static extern IntPtr _GetServerDetails(IntPtr self, HServerListRequest hRequest, int iServer);

		// Token: 0x060003C1 RID: 961 RVA: 0x0000718C File Offset: 0x0000538C
		internal gameserveritem_t GetServerDetails(HServerListRequest hRequest, int iServer)
		{
			IntPtr ptr = ISteamMatchmakingServers._GetServerDetails(this.Self, hRequest, iServer);
			return ptr.ToType<gameserveritem_t>();
		}

		// Token: 0x060003C2 RID: 962
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_CancelQuery")]
		private static extern void _CancelQuery(IntPtr self, HServerListRequest hRequest);

		// Token: 0x060003C3 RID: 963 RVA: 0x000071B2 File Offset: 0x000053B2
		internal void CancelQuery(HServerListRequest hRequest)
		{
			ISteamMatchmakingServers._CancelQuery(this.Self, hRequest);
		}

		// Token: 0x060003C4 RID: 964
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RefreshQuery")]
		private static extern void _RefreshQuery(IntPtr self, HServerListRequest hRequest);

		// Token: 0x060003C5 RID: 965 RVA: 0x000071C2 File Offset: 0x000053C2
		internal void RefreshQuery(HServerListRequest hRequest)
		{
			ISteamMatchmakingServers._RefreshQuery(this.Self, hRequest);
		}

		// Token: 0x060003C6 RID: 966
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_IsRefreshing")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _IsRefreshing(IntPtr self, HServerListRequest hRequest);

		// Token: 0x060003C7 RID: 967 RVA: 0x000071D4 File Offset: 0x000053D4
		internal bool IsRefreshing(HServerListRequest hRequest)
		{
			return ISteamMatchmakingServers._IsRefreshing(this.Self, hRequest);
		}

		// Token: 0x060003C8 RID: 968
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_GetServerCount")]
		private static extern int _GetServerCount(IntPtr self, HServerListRequest hRequest);

		// Token: 0x060003C9 RID: 969 RVA: 0x000071F4 File Offset: 0x000053F4
		internal int GetServerCount(HServerListRequest hRequest)
		{
			return ISteamMatchmakingServers._GetServerCount(this.Self, hRequest);
		}

		// Token: 0x060003CA RID: 970
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RefreshServer")]
		private static extern void _RefreshServer(IntPtr self, HServerListRequest hRequest, int iServer);

		// Token: 0x060003CB RID: 971 RVA: 0x00007214 File Offset: 0x00005414
		internal void RefreshServer(HServerListRequest hRequest, int iServer)
		{
			ISteamMatchmakingServers._RefreshServer(this.Self, hRequest, iServer);
		}

		// Token: 0x060003CC RID: 972
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_PingServer")]
		private static extern HServerQuery _PingServer(IntPtr self, uint unIP, ushort usPort, IntPtr pRequestServersResponse);

		// Token: 0x060003CD RID: 973 RVA: 0x00007228 File Offset: 0x00005428
		internal HServerQuery PingServer(uint unIP, ushort usPort, IntPtr pRequestServersResponse)
		{
			return ISteamMatchmakingServers._PingServer(this.Self, unIP, usPort, pRequestServersResponse);
		}

		// Token: 0x060003CE RID: 974
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_PlayerDetails")]
		private static extern HServerQuery _PlayerDetails(IntPtr self, uint unIP, ushort usPort, IntPtr pRequestServersResponse);

		// Token: 0x060003CF RID: 975 RVA: 0x0000724C File Offset: 0x0000544C
		internal HServerQuery PlayerDetails(uint unIP, ushort usPort, IntPtr pRequestServersResponse)
		{
			return ISteamMatchmakingServers._PlayerDetails(this.Self, unIP, usPort, pRequestServersResponse);
		}

		// Token: 0x060003D0 RID: 976
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_ServerRules")]
		private static extern HServerQuery _ServerRules(IntPtr self, uint unIP, ushort usPort, IntPtr pRequestServersResponse);

		// Token: 0x060003D1 RID: 977 RVA: 0x00007270 File Offset: 0x00005470
		internal HServerQuery ServerRules(uint unIP, ushort usPort, IntPtr pRequestServersResponse)
		{
			return ISteamMatchmakingServers._ServerRules(this.Self, unIP, usPort, pRequestServersResponse);
		}

		// Token: 0x060003D2 RID: 978
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_CancelServerQuery")]
		private static extern void _CancelServerQuery(IntPtr self, HServerQuery hServerQuery);

		// Token: 0x060003D3 RID: 979 RVA: 0x00007292 File Offset: 0x00005492
		internal void CancelServerQuery(HServerQuery hServerQuery)
		{
			ISteamMatchmakingServers._CancelServerQuery(this.Self, hServerQuery);
		}
	}
}
