using System;

namespace Steamworks
{
	// Token: 0x02000190 RID: 400
	internal static class CSteamAPIContext
	{
		// Token: 0x0600090B RID: 2315 RVA: 0x0000D7D8 File Offset: 0x0000B9D8
		internal static void Clear()
		{
			CSteamAPIContext.m_pSteamClient = IntPtr.Zero;
			CSteamAPIContext.m_pSteamUser = IntPtr.Zero;
			CSteamAPIContext.m_pSteamFriends = IntPtr.Zero;
			CSteamAPIContext.m_pSteamUtils = IntPtr.Zero;
			CSteamAPIContext.m_pSteamMatchmaking = IntPtr.Zero;
			CSteamAPIContext.m_pSteamUserStats = IntPtr.Zero;
			CSteamAPIContext.m_pSteamApps = IntPtr.Zero;
			CSteamAPIContext.m_pSteamMatchmakingServers = IntPtr.Zero;
			CSteamAPIContext.m_pSteamNetworking = IntPtr.Zero;
			CSteamAPIContext.m_pSteamRemoteStorage = IntPtr.Zero;
			CSteamAPIContext.m_pSteamHTTP = IntPtr.Zero;
			CSteamAPIContext.m_pSteamScreenshots = IntPtr.Zero;
			CSteamAPIContext.m_pSteamGameSearch = IntPtr.Zero;
			CSteamAPIContext.m_pSteamMusic = IntPtr.Zero;
			CSteamAPIContext.m_pController = IntPtr.Zero;
			CSteamAPIContext.m_pSteamUGC = IntPtr.Zero;
			CSteamAPIContext.m_pSteamAppList = IntPtr.Zero;
			CSteamAPIContext.m_pSteamMusic = IntPtr.Zero;
			CSteamAPIContext.m_pSteamMusicRemote = IntPtr.Zero;
			CSteamAPIContext.m_pSteamHTMLSurface = IntPtr.Zero;
			CSteamAPIContext.m_pSteamInventory = IntPtr.Zero;
			CSteamAPIContext.m_pSteamVideo = IntPtr.Zero;
			CSteamAPIContext.m_pSteamParentalSettings = IntPtr.Zero;
			CSteamAPIContext.m_pSteamInput = IntPtr.Zero;
			CSteamAPIContext.m_pSteamParties = IntPtr.Zero;
			CSteamAPIContext.m_pSteamRemotePlay = IntPtr.Zero;
			CSteamAPIContext.m_pSteamNetworkingUtils = IntPtr.Zero;
			CSteamAPIContext.m_pSteamNetworkingSockets = IntPtr.Zero;
			CSteamAPIContext.m_pSteamNetworkingMessages = IntPtr.Zero;
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0000D908 File Offset: 0x0000BB08
		internal static bool Init()
		{
			HSteamUser hsteamUser = SteamAPI.GetHSteamUser();
			HSteamPipe hsteamPipe = SteamAPI.GetHSteamPipe();
			if (hsteamPipe == (HSteamPipe)0)
			{
				return false;
			}
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle("SteamClient020"))
			{
				CSteamAPIContext.m_pSteamClient = NativeMethods.SteamInternal_CreateInterface(utf8StringHandle);
			}
			if (CSteamAPIContext.m_pSteamClient == IntPtr.Zero)
			{
				return false;
			}
			CSteamAPIContext.m_pSteamUser = SteamClient.GetISteamUser(hsteamUser, hsteamPipe, "SteamUser023");
			if (CSteamAPIContext.m_pSteamUser == IntPtr.Zero)
			{
				return false;
			}
			CSteamAPIContext.m_pSteamFriends = SteamClient.GetISteamFriends(hsteamUser, hsteamPipe, "SteamFriends017");
			if (CSteamAPIContext.m_pSteamFriends == IntPtr.Zero)
			{
				return false;
			}
			CSteamAPIContext.m_pSteamUtils = SteamClient.GetISteamUtils(hsteamPipe, "SteamUtils010");
			if (CSteamAPIContext.m_pSteamUtils == IntPtr.Zero)
			{
				return false;
			}
			CSteamAPIContext.m_pSteamMatchmaking = SteamClient.GetISteamMatchmaking(hsteamUser, hsteamPipe, "SteamMatchMaking009");
			if (CSteamAPIContext.m_pSteamMatchmaking == IntPtr.Zero)
			{
				return false;
			}
			CSteamAPIContext.m_pSteamMatchmakingServers = SteamClient.GetISteamMatchmakingServers(hsteamUser, hsteamPipe, "SteamMatchMakingServers002");
			if (CSteamAPIContext.m_pSteamMatchmakingServers == IntPtr.Zero)
			{
				return false;
			}
			CSteamAPIContext.m_pSteamUserStats = SteamClient.GetISteamUserStats(hsteamUser, hsteamPipe, "STEAMUSERSTATS_INTERFACE_VERSION012");
			if (CSteamAPIContext.m_pSteamUserStats == IntPtr.Zero)
			{
				return false;
			}
			CSteamAPIContext.m_pSteamApps = SteamClient.GetISteamApps(hsteamUser, hsteamPipe, "STEAMAPPS_INTERFACE_VERSION008");
			if (CSteamAPIContext.m_pSteamApps == IntPtr.Zero)
			{
				return false;
			}
			CSteamAPIContext.m_pSteamNetworking = SteamClient.GetISteamNetworking(hsteamUser, hsteamPipe, "SteamNetworking006");
			if (CSteamAPIContext.m_pSteamNetworking == IntPtr.Zero)
			{
				return false;
			}
			CSteamAPIContext.m_pSteamRemoteStorage = SteamClient.GetISteamRemoteStorage(hsteamUser, hsteamPipe, "STEAMREMOTESTORAGE_INTERFACE_VERSION016");
			if (CSteamAPIContext.m_pSteamRemoteStorage == IntPtr.Zero)
			{
				return false;
			}
			CSteamAPIContext.m_pSteamScreenshots = SteamClient.GetISteamScreenshots(hsteamUser, hsteamPipe, "STEAMSCREENSHOTS_INTERFACE_VERSION003");
			if (CSteamAPIContext.m_pSteamScreenshots == IntPtr.Zero)
			{
				return false;
			}
			CSteamAPIContext.m_pSteamGameSearch = SteamClient.GetISteamGameSearch(hsteamUser, hsteamPipe, "SteamMatchGameSearch001");
			if (CSteamAPIContext.m_pSteamGameSearch == IntPtr.Zero)
			{
				return false;
			}
			CSteamAPIContext.m_pSteamHTTP = SteamClient.GetISteamHTTP(hsteamUser, hsteamPipe, "STEAMHTTP_INTERFACE_VERSION003");
			if (CSteamAPIContext.m_pSteamHTTP == IntPtr.Zero)
			{
				return false;
			}
			CSteamAPIContext.m_pSteamUGC = SteamClient.GetISteamUGC(hsteamUser, hsteamPipe, "STEAMUGC_INTERFACE_VERSION017");
			if (CSteamAPIContext.m_pSteamUGC == IntPtr.Zero)
			{
				return false;
			}
			CSteamAPIContext.m_pSteamAppList = SteamClient.GetISteamAppList(hsteamUser, hsteamPipe, "STEAMAPPLIST_INTERFACE_VERSION001");
			if (CSteamAPIContext.m_pSteamAppList == IntPtr.Zero)
			{
				return false;
			}
			CSteamAPIContext.m_pSteamMusic = SteamClient.GetISteamMusic(hsteamUser, hsteamPipe, "STEAMMUSIC_INTERFACE_VERSION001");
			if (CSteamAPIContext.m_pSteamMusic == IntPtr.Zero)
			{
				return false;
			}
			CSteamAPIContext.m_pSteamMusicRemote = SteamClient.GetISteamMusicRemote(hsteamUser, hsteamPipe, "STEAMMUSICREMOTE_INTERFACE_VERSION001");
			if (CSteamAPIContext.m_pSteamMusicRemote == IntPtr.Zero)
			{
				return false;
			}
			CSteamAPIContext.m_pSteamHTMLSurface = SteamClient.GetISteamHTMLSurface(hsteamUser, hsteamPipe, "STEAMHTMLSURFACE_INTERFACE_VERSION_005");
			if (CSteamAPIContext.m_pSteamHTMLSurface == IntPtr.Zero)
			{
				return false;
			}
			CSteamAPIContext.m_pSteamInventory = SteamClient.GetISteamInventory(hsteamUser, hsteamPipe, "STEAMINVENTORY_INTERFACE_V003");
			if (CSteamAPIContext.m_pSteamInventory == IntPtr.Zero)
			{
				return false;
			}
			CSteamAPIContext.m_pSteamVideo = SteamClient.GetISteamVideo(hsteamUser, hsteamPipe, "STEAMVIDEO_INTERFACE_V002");
			if (CSteamAPIContext.m_pSteamVideo == IntPtr.Zero)
			{
				return false;
			}
			CSteamAPIContext.m_pSteamParentalSettings = SteamClient.GetISteamParentalSettings(hsteamUser, hsteamPipe, "STEAMPARENTALSETTINGS_INTERFACE_VERSION001");
			if (CSteamAPIContext.m_pSteamParentalSettings == IntPtr.Zero)
			{
				return false;
			}
			CSteamAPIContext.m_pSteamInput = SteamClient.GetISteamInput(hsteamUser, hsteamPipe, "SteamInput006");
			if (CSteamAPIContext.m_pSteamInput == IntPtr.Zero)
			{
				return false;
			}
			CSteamAPIContext.m_pSteamParties = SteamClient.GetISteamParties(hsteamUser, hsteamPipe, "SteamParties002");
			if (CSteamAPIContext.m_pSteamParties == IntPtr.Zero)
			{
				return false;
			}
			CSteamAPIContext.m_pSteamRemotePlay = SteamClient.GetISteamRemotePlay(hsteamUser, hsteamPipe, "STEAMREMOTEPLAY_INTERFACE_VERSION001");
			if (CSteamAPIContext.m_pSteamRemotePlay == IntPtr.Zero)
			{
				return false;
			}
			using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle("SteamNetworkingUtils004"))
			{
				CSteamAPIContext.m_pSteamNetworkingUtils = ((NativeMethods.SteamInternal_FindOrCreateUserInterface(hsteamUser, utf8StringHandle2) != IntPtr.Zero) ? NativeMethods.SteamInternal_FindOrCreateUserInterface(hsteamUser, utf8StringHandle2) : NativeMethods.SteamInternal_FindOrCreateGameServerInterface(hsteamUser, utf8StringHandle2));
			}
			if (CSteamAPIContext.m_pSteamNetworkingUtils == IntPtr.Zero)
			{
				return false;
			}
			using (InteropHelp.UTF8StringHandle utf8StringHandle3 = new InteropHelp.UTF8StringHandle("SteamNetworkingSockets012"))
			{
				CSteamAPIContext.m_pSteamNetworkingSockets = NativeMethods.SteamInternal_FindOrCreateUserInterface(hsteamUser, utf8StringHandle3);
			}
			if (CSteamAPIContext.m_pSteamNetworkingSockets == IntPtr.Zero)
			{
				return false;
			}
			using (InteropHelp.UTF8StringHandle utf8StringHandle4 = new InteropHelp.UTF8StringHandle("SteamNetworkingMessages002"))
			{
				CSteamAPIContext.m_pSteamNetworkingMessages = NativeMethods.SteamInternal_FindOrCreateUserInterface(hsteamUser, utf8StringHandle4);
			}
			return !(CSteamAPIContext.m_pSteamNetworkingMessages == IntPtr.Zero);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0000DD9C File Offset: 0x0000BF9C
		internal static IntPtr GetSteamClient()
		{
			return CSteamAPIContext.m_pSteamClient;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0000DDA3 File Offset: 0x0000BFA3
		internal static IntPtr GetSteamUser()
		{
			return CSteamAPIContext.m_pSteamUser;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0000DDAA File Offset: 0x0000BFAA
		internal static IntPtr GetSteamFriends()
		{
			return CSteamAPIContext.m_pSteamFriends;
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0000DDB1 File Offset: 0x0000BFB1
		internal static IntPtr GetSteamUtils()
		{
			return CSteamAPIContext.m_pSteamUtils;
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0000DDB8 File Offset: 0x0000BFB8
		internal static IntPtr GetSteamMatchmaking()
		{
			return CSteamAPIContext.m_pSteamMatchmaking;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0000DDBF File Offset: 0x0000BFBF
		internal static IntPtr GetSteamUserStats()
		{
			return CSteamAPIContext.m_pSteamUserStats;
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0000DDC6 File Offset: 0x0000BFC6
		internal static IntPtr GetSteamApps()
		{
			return CSteamAPIContext.m_pSteamApps;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0000DDCD File Offset: 0x0000BFCD
		internal static IntPtr GetSteamMatchmakingServers()
		{
			return CSteamAPIContext.m_pSteamMatchmakingServers;
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0000DDD4 File Offset: 0x0000BFD4
		internal static IntPtr GetSteamNetworking()
		{
			return CSteamAPIContext.m_pSteamNetworking;
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0000DDDB File Offset: 0x0000BFDB
		internal static IntPtr GetSteamRemoteStorage()
		{
			return CSteamAPIContext.m_pSteamRemoteStorage;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0000DDE2 File Offset: 0x0000BFE2
		internal static IntPtr GetSteamScreenshots()
		{
			return CSteamAPIContext.m_pSteamScreenshots;
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0000DDE9 File Offset: 0x0000BFE9
		internal static IntPtr GetSteamGameSearch()
		{
			return CSteamAPIContext.m_pSteamGameSearch;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0000DDF0 File Offset: 0x0000BFF0
		internal static IntPtr GetSteamHTTP()
		{
			return CSteamAPIContext.m_pSteamHTTP;
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0000DDF7 File Offset: 0x0000BFF7
		internal static IntPtr GetSteamController()
		{
			return CSteamAPIContext.m_pController;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0000DDFE File Offset: 0x0000BFFE
		internal static IntPtr GetSteamUGC()
		{
			return CSteamAPIContext.m_pSteamUGC;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0000DE05 File Offset: 0x0000C005
		internal static IntPtr GetSteamAppList()
		{
			return CSteamAPIContext.m_pSteamAppList;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0000DE0C File Offset: 0x0000C00C
		internal static IntPtr GetSteamMusic()
		{
			return CSteamAPIContext.m_pSteamMusic;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0000DE13 File Offset: 0x0000C013
		internal static IntPtr GetSteamMusicRemote()
		{
			return CSteamAPIContext.m_pSteamMusicRemote;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0000DE1A File Offset: 0x0000C01A
		internal static IntPtr GetSteamHTMLSurface()
		{
			return CSteamAPIContext.m_pSteamHTMLSurface;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0000DE21 File Offset: 0x0000C021
		internal static IntPtr GetSteamInventory()
		{
			return CSteamAPIContext.m_pSteamInventory;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0000DE28 File Offset: 0x0000C028
		internal static IntPtr GetSteamVideo()
		{
			return CSteamAPIContext.m_pSteamVideo;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0000DE2F File Offset: 0x0000C02F
		internal static IntPtr GetSteamParentalSettings()
		{
			return CSteamAPIContext.m_pSteamParentalSettings;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0000DE36 File Offset: 0x0000C036
		internal static IntPtr GetSteamInput()
		{
			return CSteamAPIContext.m_pSteamInput;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0000DE3D File Offset: 0x0000C03D
		internal static IntPtr GetSteamParties()
		{
			return CSteamAPIContext.m_pSteamParties;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0000DE44 File Offset: 0x0000C044
		internal static IntPtr GetSteamRemotePlay()
		{
			return CSteamAPIContext.m_pSteamRemotePlay;
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0000DE4B File Offset: 0x0000C04B
		internal static IntPtr GetSteamNetworkingUtils()
		{
			return CSteamAPIContext.m_pSteamNetworkingUtils;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0000DE52 File Offset: 0x0000C052
		internal static IntPtr GetSteamNetworkingSockets()
		{
			return CSteamAPIContext.m_pSteamNetworkingSockets;
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0000DE59 File Offset: 0x0000C059
		internal static IntPtr GetSteamNetworkingMessages()
		{
			return CSteamAPIContext.m_pSteamNetworkingMessages;
		}

		// Token: 0x04000A59 RID: 2649
		private static IntPtr m_pSteamClient;

		// Token: 0x04000A5A RID: 2650
		private static IntPtr m_pSteamUser;

		// Token: 0x04000A5B RID: 2651
		private static IntPtr m_pSteamFriends;

		// Token: 0x04000A5C RID: 2652
		private static IntPtr m_pSteamUtils;

		// Token: 0x04000A5D RID: 2653
		private static IntPtr m_pSteamMatchmaking;

		// Token: 0x04000A5E RID: 2654
		private static IntPtr m_pSteamUserStats;

		// Token: 0x04000A5F RID: 2655
		private static IntPtr m_pSteamApps;

		// Token: 0x04000A60 RID: 2656
		private static IntPtr m_pSteamMatchmakingServers;

		// Token: 0x04000A61 RID: 2657
		private static IntPtr m_pSteamNetworking;

		// Token: 0x04000A62 RID: 2658
		private static IntPtr m_pSteamRemoteStorage;

		// Token: 0x04000A63 RID: 2659
		private static IntPtr m_pSteamScreenshots;

		// Token: 0x04000A64 RID: 2660
		private static IntPtr m_pSteamGameSearch;

		// Token: 0x04000A65 RID: 2661
		private static IntPtr m_pSteamHTTP;

		// Token: 0x04000A66 RID: 2662
		private static IntPtr m_pController;

		// Token: 0x04000A67 RID: 2663
		private static IntPtr m_pSteamUGC;

		// Token: 0x04000A68 RID: 2664
		private static IntPtr m_pSteamAppList;

		// Token: 0x04000A69 RID: 2665
		private static IntPtr m_pSteamMusic;

		// Token: 0x04000A6A RID: 2666
		private static IntPtr m_pSteamMusicRemote;

		// Token: 0x04000A6B RID: 2667
		private static IntPtr m_pSteamHTMLSurface;

		// Token: 0x04000A6C RID: 2668
		private static IntPtr m_pSteamInventory;

		// Token: 0x04000A6D RID: 2669
		private static IntPtr m_pSteamVideo;

		// Token: 0x04000A6E RID: 2670
		private static IntPtr m_pSteamParentalSettings;

		// Token: 0x04000A6F RID: 2671
		private static IntPtr m_pSteamInput;

		// Token: 0x04000A70 RID: 2672
		private static IntPtr m_pSteamParties;

		// Token: 0x04000A71 RID: 2673
		private static IntPtr m_pSteamRemotePlay;

		// Token: 0x04000A72 RID: 2674
		private static IntPtr m_pSteamNetworkingUtils;

		// Token: 0x04000A73 RID: 2675
		private static IntPtr m_pSteamNetworkingSockets;

		// Token: 0x04000A74 RID: 2676
		private static IntPtr m_pSteamNetworkingMessages;
	}
}
