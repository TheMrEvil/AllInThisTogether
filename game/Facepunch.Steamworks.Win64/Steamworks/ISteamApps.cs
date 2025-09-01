using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x0200000E RID: 14
	internal class ISteamApps : SteamInterface
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00003CAC File Offset: 0x00001EAC
		internal ISteamApps(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x06000038 RID: 56
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamApps_v008();

		// Token: 0x06000039 RID: 57 RVA: 0x00003CBE File Offset: 0x00001EBE
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamApps.SteamAPI_SteamApps_v008();
		}

		// Token: 0x0600003A RID: 58
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamGameServerApps_v008();

		// Token: 0x0600003B RID: 59 RVA: 0x00003CC5 File Offset: 0x00001EC5
		public override IntPtr GetServerInterfacePointer()
		{
			return ISteamApps.SteamAPI_SteamGameServerApps_v008();
		}

		// Token: 0x0600003C RID: 60
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsSubscribed")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BIsSubscribed(IntPtr self);

		// Token: 0x0600003D RID: 61 RVA: 0x00003CCC File Offset: 0x00001ECC
		internal bool BIsSubscribed()
		{
			return ISteamApps._BIsSubscribed(this.Self);
		}

		// Token: 0x0600003E RID: 62
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsLowViolence")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BIsLowViolence(IntPtr self);

		// Token: 0x0600003F RID: 63 RVA: 0x00003CEC File Offset: 0x00001EEC
		internal bool BIsLowViolence()
		{
			return ISteamApps._BIsLowViolence(this.Self);
		}

		// Token: 0x06000040 RID: 64
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsCybercafe")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BIsCybercafe(IntPtr self);

		// Token: 0x06000041 RID: 65 RVA: 0x00003D0C File Offset: 0x00001F0C
		internal bool BIsCybercafe()
		{
			return ISteamApps._BIsCybercafe(this.Self);
		}

		// Token: 0x06000042 RID: 66
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsVACBanned")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BIsVACBanned(IntPtr self);

		// Token: 0x06000043 RID: 67 RVA: 0x00003D2C File Offset: 0x00001F2C
		internal bool BIsVACBanned()
		{
			return ISteamApps._BIsVACBanned(this.Self);
		}

		// Token: 0x06000044 RID: 68
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetCurrentGameLanguage")]
		private static extern Utf8StringPointer _GetCurrentGameLanguage(IntPtr self);

		// Token: 0x06000045 RID: 69 RVA: 0x00003D4C File Offset: 0x00001F4C
		internal string GetCurrentGameLanguage()
		{
			Utf8StringPointer p = ISteamApps._GetCurrentGameLanguage(this.Self);
			return p;
		}

		// Token: 0x06000046 RID: 70
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetAvailableGameLanguages")]
		private static extern Utf8StringPointer _GetAvailableGameLanguages(IntPtr self);

		// Token: 0x06000047 RID: 71 RVA: 0x00003D70 File Offset: 0x00001F70
		internal string GetAvailableGameLanguages()
		{
			Utf8StringPointer p = ISteamApps._GetAvailableGameLanguages(this.Self);
			return p;
		}

		// Token: 0x06000048 RID: 72
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsSubscribedApp")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BIsSubscribedApp(IntPtr self, AppId appID);

		// Token: 0x06000049 RID: 73 RVA: 0x00003D94 File Offset: 0x00001F94
		internal bool BIsSubscribedApp(AppId appID)
		{
			return ISteamApps._BIsSubscribedApp(this.Self, appID);
		}

		// Token: 0x0600004A RID: 74
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsDlcInstalled")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BIsDlcInstalled(IntPtr self, AppId appID);

		// Token: 0x0600004B RID: 75 RVA: 0x00003DB4 File Offset: 0x00001FB4
		internal bool BIsDlcInstalled(AppId appID)
		{
			return ISteamApps._BIsDlcInstalled(this.Self, appID);
		}

		// Token: 0x0600004C RID: 76
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetEarliestPurchaseUnixTime")]
		private static extern uint _GetEarliestPurchaseUnixTime(IntPtr self, AppId nAppID);

		// Token: 0x0600004D RID: 77 RVA: 0x00003DD4 File Offset: 0x00001FD4
		internal uint GetEarliestPurchaseUnixTime(AppId nAppID)
		{
			return ISteamApps._GetEarliestPurchaseUnixTime(this.Self, nAppID);
		}

		// Token: 0x0600004E RID: 78
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsSubscribedFromFreeWeekend")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BIsSubscribedFromFreeWeekend(IntPtr self);

		// Token: 0x0600004F RID: 79 RVA: 0x00003DF4 File Offset: 0x00001FF4
		internal bool BIsSubscribedFromFreeWeekend()
		{
			return ISteamApps._BIsSubscribedFromFreeWeekend(this.Self);
		}

		// Token: 0x06000050 RID: 80
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetDLCCount")]
		private static extern int _GetDLCCount(IntPtr self);

		// Token: 0x06000051 RID: 81 RVA: 0x00003E14 File Offset: 0x00002014
		internal int GetDLCCount()
		{
			return ISteamApps._GetDLCCount(this.Self);
		}

		// Token: 0x06000052 RID: 82
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BGetDLCDataByIndex")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BGetDLCDataByIndex(IntPtr self, int iDLC, ref AppId pAppID, [MarshalAs(UnmanagedType.U1)] ref bool pbAvailable, IntPtr pchName, int cchNameBufferSize);

		// Token: 0x06000053 RID: 83 RVA: 0x00003E34 File Offset: 0x00002034
		internal bool BGetDLCDataByIndex(int iDLC, ref AppId pAppID, [MarshalAs(UnmanagedType.U1)] ref bool pbAvailable, out string pchName)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			bool result = ISteamApps._BGetDLCDataByIndex(this.Self, iDLC, ref pAppID, ref pbAvailable, intPtr, 32768);
			pchName = Helpers.MemoryToString(intPtr);
			return result;
		}

		// Token: 0x06000054 RID: 84
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_InstallDLC")]
		private static extern void _InstallDLC(IntPtr self, AppId nAppID);

		// Token: 0x06000055 RID: 85 RVA: 0x00003E6B File Offset: 0x0000206B
		internal void InstallDLC(AppId nAppID)
		{
			ISteamApps._InstallDLC(this.Self, nAppID);
		}

		// Token: 0x06000056 RID: 86
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_UninstallDLC")]
		private static extern void _UninstallDLC(IntPtr self, AppId nAppID);

		// Token: 0x06000057 RID: 87 RVA: 0x00003E7B File Offset: 0x0000207B
		internal void UninstallDLC(AppId nAppID)
		{
			ISteamApps._UninstallDLC(this.Self, nAppID);
		}

		// Token: 0x06000058 RID: 88
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_RequestAppProofOfPurchaseKey")]
		private static extern void _RequestAppProofOfPurchaseKey(IntPtr self, AppId nAppID);

		// Token: 0x06000059 RID: 89 RVA: 0x00003E8B File Offset: 0x0000208B
		internal void RequestAppProofOfPurchaseKey(AppId nAppID)
		{
			ISteamApps._RequestAppProofOfPurchaseKey(this.Self, nAppID);
		}

		// Token: 0x0600005A RID: 90
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetCurrentBetaName")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetCurrentBetaName(IntPtr self, IntPtr pchName, int cchNameBufferSize);

		// Token: 0x0600005B RID: 91 RVA: 0x00003E9C File Offset: 0x0000209C
		internal bool GetCurrentBetaName(out string pchName)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			bool result = ISteamApps._GetCurrentBetaName(this.Self, intPtr, 32768);
			pchName = Helpers.MemoryToString(intPtr);
			return result;
		}

		// Token: 0x0600005C RID: 92
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_MarkContentCorrupt")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _MarkContentCorrupt(IntPtr self, [MarshalAs(UnmanagedType.U1)] bool bMissingFilesOnly);

		// Token: 0x0600005D RID: 93 RVA: 0x00003ED0 File Offset: 0x000020D0
		internal bool MarkContentCorrupt([MarshalAs(UnmanagedType.U1)] bool bMissingFilesOnly)
		{
			return ISteamApps._MarkContentCorrupt(this.Self, bMissingFilesOnly);
		}

		// Token: 0x0600005E RID: 94
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetInstalledDepots")]
		private static extern uint _GetInstalledDepots(IntPtr self, AppId appID, [In] [Out] DepotId_t[] pvecDepots, uint cMaxDepots);

		// Token: 0x0600005F RID: 95 RVA: 0x00003EF0 File Offset: 0x000020F0
		internal uint GetInstalledDepots(AppId appID, [In] [Out] DepotId_t[] pvecDepots, uint cMaxDepots)
		{
			return ISteamApps._GetInstalledDepots(this.Self, appID, pvecDepots, cMaxDepots);
		}

		// Token: 0x06000060 RID: 96
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetAppInstallDir")]
		private static extern uint _GetAppInstallDir(IntPtr self, AppId appID, IntPtr pchFolder, uint cchFolderBufferSize);

		// Token: 0x06000061 RID: 97 RVA: 0x00003F14 File Offset: 0x00002114
		internal uint GetAppInstallDir(AppId appID, out string pchFolder)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			uint result = ISteamApps._GetAppInstallDir(this.Self, appID, intPtr, 32768U);
			pchFolder = Helpers.MemoryToString(intPtr);
			return result;
		}

		// Token: 0x06000062 RID: 98
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsAppInstalled")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BIsAppInstalled(IntPtr self, AppId appID);

		// Token: 0x06000063 RID: 99 RVA: 0x00003F48 File Offset: 0x00002148
		internal bool BIsAppInstalled(AppId appID)
		{
			return ISteamApps._BIsAppInstalled(this.Self, appID);
		}

		// Token: 0x06000064 RID: 100
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetAppOwner")]
		private static extern SteamId _GetAppOwner(IntPtr self);

		// Token: 0x06000065 RID: 101 RVA: 0x00003F68 File Offset: 0x00002168
		internal SteamId GetAppOwner()
		{
			return ISteamApps._GetAppOwner(this.Self);
		}

		// Token: 0x06000066 RID: 102
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetLaunchQueryParam")]
		private static extern Utf8StringPointer _GetLaunchQueryParam(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey);

		// Token: 0x06000067 RID: 103 RVA: 0x00003F88 File Offset: 0x00002188
		internal string GetLaunchQueryParam([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey)
		{
			Utf8StringPointer p = ISteamApps._GetLaunchQueryParam(this.Self, pchKey);
			return p;
		}

		// Token: 0x06000068 RID: 104
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetDlcDownloadProgress")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetDlcDownloadProgress(IntPtr self, AppId nAppID, ref ulong punBytesDownloaded, ref ulong punBytesTotal);

		// Token: 0x06000069 RID: 105 RVA: 0x00003FB0 File Offset: 0x000021B0
		internal bool GetDlcDownloadProgress(AppId nAppID, ref ulong punBytesDownloaded, ref ulong punBytesTotal)
		{
			return ISteamApps._GetDlcDownloadProgress(this.Self, nAppID, ref punBytesDownloaded, ref punBytesTotal);
		}

		// Token: 0x0600006A RID: 106
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetAppBuildId")]
		private static extern int _GetAppBuildId(IntPtr self);

		// Token: 0x0600006B RID: 107 RVA: 0x00003FD4 File Offset: 0x000021D4
		internal int GetAppBuildId()
		{
			return ISteamApps._GetAppBuildId(this.Self);
		}

		// Token: 0x0600006C RID: 108
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_RequestAllProofOfPurchaseKeys")]
		private static extern void _RequestAllProofOfPurchaseKeys(IntPtr self);

		// Token: 0x0600006D RID: 109 RVA: 0x00003FF3 File Offset: 0x000021F3
		internal void RequestAllProofOfPurchaseKeys()
		{
			ISteamApps._RequestAllProofOfPurchaseKeys(this.Self);
		}

		// Token: 0x0600006E RID: 110
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetFileDetails")]
		private static extern SteamAPICall_t _GetFileDetails(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszFileName);

		// Token: 0x0600006F RID: 111 RVA: 0x00004004 File Offset: 0x00002204
		internal CallResult<FileDetailsResult_t> GetFileDetails([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszFileName)
		{
			SteamAPICall_t call = ISteamApps._GetFileDetails(this.Self, pszFileName);
			return new CallResult<FileDetailsResult_t>(call, base.IsServer);
		}

		// Token: 0x06000070 RID: 112
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetLaunchCommandLine")]
		private static extern int _GetLaunchCommandLine(IntPtr self, IntPtr pszCommandLine, int cubCommandLine);

		// Token: 0x06000071 RID: 113 RVA: 0x00004030 File Offset: 0x00002230
		internal int GetLaunchCommandLine(out string pszCommandLine)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			int result = ISteamApps._GetLaunchCommandLine(this.Self, intPtr, 32768);
			pszCommandLine = Helpers.MemoryToString(intPtr);
			return result;
		}

		// Token: 0x06000072 RID: 114
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsSubscribedFromFamilySharing")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BIsSubscribedFromFamilySharing(IntPtr self);

		// Token: 0x06000073 RID: 115 RVA: 0x00004064 File Offset: 0x00002264
		internal bool BIsSubscribedFromFamilySharing()
		{
			return ISteamApps._BIsSubscribedFromFamilySharing(this.Self);
		}
	}
}
