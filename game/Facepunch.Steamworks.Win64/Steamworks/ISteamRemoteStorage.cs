using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000029 RID: 41
	internal class ISteamRemoteStorage : SteamInterface
	{
		// Token: 0x0600051A RID: 1306 RVA: 0x00008700 File Offset: 0x00006900
		internal ISteamRemoteStorage(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x0600051B RID: 1307
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamRemoteStorage_v014();

		// Token: 0x0600051C RID: 1308 RVA: 0x00008712 File Offset: 0x00006912
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamRemoteStorage.SteamAPI_SteamRemoteStorage_v014();
		}

		// Token: 0x0600051D RID: 1309
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWrite")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _FileWrite(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile, IntPtr pvData, int cubData);

		// Token: 0x0600051E RID: 1310 RVA: 0x0000871C File Offset: 0x0000691C
		internal bool FileWrite([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile, IntPtr pvData, int cubData)
		{
			return ISteamRemoteStorage._FileWrite(this.Self, pchFile, pvData, cubData);
		}

		// Token: 0x0600051F RID: 1311
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileRead")]
		private static extern int _FileRead(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile, IntPtr pvData, int cubDataToRead);

		// Token: 0x06000520 RID: 1312 RVA: 0x00008740 File Offset: 0x00006940
		internal int FileRead([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile, IntPtr pvData, int cubDataToRead)
		{
			return ISteamRemoteStorage._FileRead(this.Self, pchFile, pvData, cubDataToRead);
		}

		// Token: 0x06000521 RID: 1313
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteAsync")]
		private static extern SteamAPICall_t _FileWriteAsync(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile, IntPtr pvData, uint cubData);

		// Token: 0x06000522 RID: 1314 RVA: 0x00008764 File Offset: 0x00006964
		internal CallResult<RemoteStorageFileWriteAsyncComplete_t> FileWriteAsync([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile, IntPtr pvData, uint cubData)
		{
			SteamAPICall_t call = ISteamRemoteStorage._FileWriteAsync(this.Self, pchFile, pvData, cubData);
			return new CallResult<RemoteStorageFileWriteAsyncComplete_t>(call, base.IsServer);
		}

		// Token: 0x06000523 RID: 1315
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileReadAsync")]
		private static extern SteamAPICall_t _FileReadAsync(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile, uint nOffset, uint cubToRead);

		// Token: 0x06000524 RID: 1316 RVA: 0x00008794 File Offset: 0x00006994
		internal CallResult<RemoteStorageFileReadAsyncComplete_t> FileReadAsync([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile, uint nOffset, uint cubToRead)
		{
			SteamAPICall_t call = ISteamRemoteStorage._FileReadAsync(this.Self, pchFile, nOffset, cubToRead);
			return new CallResult<RemoteStorageFileReadAsyncComplete_t>(call, base.IsServer);
		}

		// Token: 0x06000525 RID: 1317
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileReadAsyncComplete")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _FileReadAsyncComplete(IntPtr self, SteamAPICall_t hReadCall, IntPtr pvBuffer, uint cubToRead);

		// Token: 0x06000526 RID: 1318 RVA: 0x000087C4 File Offset: 0x000069C4
		internal bool FileReadAsyncComplete(SteamAPICall_t hReadCall, IntPtr pvBuffer, uint cubToRead)
		{
			return ISteamRemoteStorage._FileReadAsyncComplete(this.Self, hReadCall, pvBuffer, cubToRead);
		}

		// Token: 0x06000527 RID: 1319
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileForget")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _FileForget(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile);

		// Token: 0x06000528 RID: 1320 RVA: 0x000087E8 File Offset: 0x000069E8
		internal bool FileForget([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile)
		{
			return ISteamRemoteStorage._FileForget(this.Self, pchFile);
		}

		// Token: 0x06000529 RID: 1321
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileDelete")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _FileDelete(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile);

		// Token: 0x0600052A RID: 1322 RVA: 0x00008808 File Offset: 0x00006A08
		internal bool FileDelete([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile)
		{
			return ISteamRemoteStorage._FileDelete(this.Self, pchFile);
		}

		// Token: 0x0600052B RID: 1323
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileShare")]
		private static extern SteamAPICall_t _FileShare(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile);

		// Token: 0x0600052C RID: 1324 RVA: 0x00008828 File Offset: 0x00006A28
		internal CallResult<RemoteStorageFileShareResult_t> FileShare([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile)
		{
			SteamAPICall_t call = ISteamRemoteStorage._FileShare(this.Self, pchFile);
			return new CallResult<RemoteStorageFileShareResult_t>(call, base.IsServer);
		}

		// Token: 0x0600052D RID: 1325
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_SetSyncPlatforms")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetSyncPlatforms(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile, RemoteStoragePlatform eRemoteStoragePlatform);

		// Token: 0x0600052E RID: 1326 RVA: 0x00008854 File Offset: 0x00006A54
		internal bool SetSyncPlatforms([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile, RemoteStoragePlatform eRemoteStoragePlatform)
		{
			return ISteamRemoteStorage._SetSyncPlatforms(this.Self, pchFile, eRemoteStoragePlatform);
		}

		// Token: 0x0600052F RID: 1327
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteStreamOpen")]
		private static extern UGCFileWriteStreamHandle_t _FileWriteStreamOpen(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile);

		// Token: 0x06000530 RID: 1328 RVA: 0x00008878 File Offset: 0x00006A78
		internal UGCFileWriteStreamHandle_t FileWriteStreamOpen([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile)
		{
			return ISteamRemoteStorage._FileWriteStreamOpen(this.Self, pchFile);
		}

		// Token: 0x06000531 RID: 1329
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteStreamWriteChunk")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _FileWriteStreamWriteChunk(IntPtr self, UGCFileWriteStreamHandle_t writeHandle, IntPtr pvData, int cubData);

		// Token: 0x06000532 RID: 1330 RVA: 0x00008898 File Offset: 0x00006A98
		internal bool FileWriteStreamWriteChunk(UGCFileWriteStreamHandle_t writeHandle, IntPtr pvData, int cubData)
		{
			return ISteamRemoteStorage._FileWriteStreamWriteChunk(this.Self, writeHandle, pvData, cubData);
		}

		// Token: 0x06000533 RID: 1331
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteStreamClose")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _FileWriteStreamClose(IntPtr self, UGCFileWriteStreamHandle_t writeHandle);

		// Token: 0x06000534 RID: 1332 RVA: 0x000088BC File Offset: 0x00006ABC
		internal bool FileWriteStreamClose(UGCFileWriteStreamHandle_t writeHandle)
		{
			return ISteamRemoteStorage._FileWriteStreamClose(this.Self, writeHandle);
		}

		// Token: 0x06000535 RID: 1333
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteStreamCancel")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _FileWriteStreamCancel(IntPtr self, UGCFileWriteStreamHandle_t writeHandle);

		// Token: 0x06000536 RID: 1334 RVA: 0x000088DC File Offset: 0x00006ADC
		internal bool FileWriteStreamCancel(UGCFileWriteStreamHandle_t writeHandle)
		{
			return ISteamRemoteStorage._FileWriteStreamCancel(this.Self, writeHandle);
		}

		// Token: 0x06000537 RID: 1335
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileExists")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _FileExists(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile);

		// Token: 0x06000538 RID: 1336 RVA: 0x000088FC File Offset: 0x00006AFC
		internal bool FileExists([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile)
		{
			return ISteamRemoteStorage._FileExists(this.Self, pchFile);
		}

		// Token: 0x06000539 RID: 1337
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FilePersisted")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _FilePersisted(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile);

		// Token: 0x0600053A RID: 1338 RVA: 0x0000891C File Offset: 0x00006B1C
		internal bool FilePersisted([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile)
		{
			return ISteamRemoteStorage._FilePersisted(this.Self, pchFile);
		}

		// Token: 0x0600053B RID: 1339
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetFileSize")]
		private static extern int _GetFileSize(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile);

		// Token: 0x0600053C RID: 1340 RVA: 0x0000893C File Offset: 0x00006B3C
		internal int GetFileSize([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile)
		{
			return ISteamRemoteStorage._GetFileSize(this.Self, pchFile);
		}

		// Token: 0x0600053D RID: 1341
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetFileTimestamp")]
		private static extern long _GetFileTimestamp(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile);

		// Token: 0x0600053E RID: 1342 RVA: 0x0000895C File Offset: 0x00006B5C
		internal long GetFileTimestamp([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile)
		{
			return ISteamRemoteStorage._GetFileTimestamp(this.Self, pchFile);
		}

		// Token: 0x0600053F RID: 1343
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetSyncPlatforms")]
		private static extern RemoteStoragePlatform _GetSyncPlatforms(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile);

		// Token: 0x06000540 RID: 1344 RVA: 0x0000897C File Offset: 0x00006B7C
		internal RemoteStoragePlatform GetSyncPlatforms([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchFile)
		{
			return ISteamRemoteStorage._GetSyncPlatforms(this.Self, pchFile);
		}

		// Token: 0x06000541 RID: 1345
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetFileCount")]
		private static extern int _GetFileCount(IntPtr self);

		// Token: 0x06000542 RID: 1346 RVA: 0x0000899C File Offset: 0x00006B9C
		internal int GetFileCount()
		{
			return ISteamRemoteStorage._GetFileCount(this.Self);
		}

		// Token: 0x06000543 RID: 1347
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetFileNameAndSize")]
		private static extern Utf8StringPointer _GetFileNameAndSize(IntPtr self, int iFile, ref int pnFileSizeInBytes);

		// Token: 0x06000544 RID: 1348 RVA: 0x000089BC File Offset: 0x00006BBC
		internal string GetFileNameAndSize(int iFile, ref int pnFileSizeInBytes)
		{
			Utf8StringPointer p = ISteamRemoteStorage._GetFileNameAndSize(this.Self, iFile, ref pnFileSizeInBytes);
			return p;
		}

		// Token: 0x06000545 RID: 1349
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetQuota")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetQuota(IntPtr self, ref ulong pnTotalBytes, ref ulong puAvailableBytes);

		// Token: 0x06000546 RID: 1350 RVA: 0x000089E4 File Offset: 0x00006BE4
		internal bool GetQuota(ref ulong pnTotalBytes, ref ulong puAvailableBytes)
		{
			return ISteamRemoteStorage._GetQuota(this.Self, ref pnTotalBytes, ref puAvailableBytes);
		}

		// Token: 0x06000547 RID: 1351
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_IsCloudEnabledForAccount")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _IsCloudEnabledForAccount(IntPtr self);

		// Token: 0x06000548 RID: 1352 RVA: 0x00008A08 File Offset: 0x00006C08
		internal bool IsCloudEnabledForAccount()
		{
			return ISteamRemoteStorage._IsCloudEnabledForAccount(this.Self);
		}

		// Token: 0x06000549 RID: 1353
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_IsCloudEnabledForApp")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _IsCloudEnabledForApp(IntPtr self);

		// Token: 0x0600054A RID: 1354 RVA: 0x00008A28 File Offset: 0x00006C28
		internal bool IsCloudEnabledForApp()
		{
			return ISteamRemoteStorage._IsCloudEnabledForApp(this.Self);
		}

		// Token: 0x0600054B RID: 1355
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_SetCloudEnabledForApp")]
		private static extern void _SetCloudEnabledForApp(IntPtr self, [MarshalAs(UnmanagedType.U1)] bool bEnabled);

		// Token: 0x0600054C RID: 1356 RVA: 0x00008A47 File Offset: 0x00006C47
		internal void SetCloudEnabledForApp([MarshalAs(UnmanagedType.U1)] bool bEnabled)
		{
			ISteamRemoteStorage._SetCloudEnabledForApp(this.Self, bEnabled);
		}

		// Token: 0x0600054D RID: 1357
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UGCDownload")]
		private static extern SteamAPICall_t _UGCDownload(IntPtr self, UGCHandle_t hContent, uint unPriority);

		// Token: 0x0600054E RID: 1358 RVA: 0x00008A58 File Offset: 0x00006C58
		internal CallResult<RemoteStorageDownloadUGCResult_t> UGCDownload(UGCHandle_t hContent, uint unPriority)
		{
			SteamAPICall_t call = ISteamRemoteStorage._UGCDownload(this.Self, hContent, unPriority);
			return new CallResult<RemoteStorageDownloadUGCResult_t>(call, base.IsServer);
		}

		// Token: 0x0600054F RID: 1359
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetUGCDownloadProgress")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetUGCDownloadProgress(IntPtr self, UGCHandle_t hContent, ref int pnBytesDownloaded, ref int pnBytesExpected);

		// Token: 0x06000550 RID: 1360 RVA: 0x00008A84 File Offset: 0x00006C84
		internal bool GetUGCDownloadProgress(UGCHandle_t hContent, ref int pnBytesDownloaded, ref int pnBytesExpected)
		{
			return ISteamRemoteStorage._GetUGCDownloadProgress(this.Self, hContent, ref pnBytesDownloaded, ref pnBytesExpected);
		}

		// Token: 0x06000551 RID: 1361
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetUGCDetails")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetUGCDetails(IntPtr self, UGCHandle_t hContent, ref AppId pnAppID, [In] [Out] ref char[] ppchName, ref int pnFileSizeInBytes, ref SteamId pSteamIDOwner);

		// Token: 0x06000552 RID: 1362 RVA: 0x00008AA8 File Offset: 0x00006CA8
		internal bool GetUGCDetails(UGCHandle_t hContent, ref AppId pnAppID, [In] [Out] ref char[] ppchName, ref int pnFileSizeInBytes, ref SteamId pSteamIDOwner)
		{
			return ISteamRemoteStorage._GetUGCDetails(this.Self, hContent, ref pnAppID, ref ppchName, ref pnFileSizeInBytes, ref pSteamIDOwner);
		}

		// Token: 0x06000553 RID: 1363
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UGCRead")]
		private static extern int _UGCRead(IntPtr self, UGCHandle_t hContent, IntPtr pvData, int cubDataToRead, uint cOffset, UGCReadAction eAction);

		// Token: 0x06000554 RID: 1364 RVA: 0x00008AD0 File Offset: 0x00006CD0
		internal int UGCRead(UGCHandle_t hContent, IntPtr pvData, int cubDataToRead, uint cOffset, UGCReadAction eAction)
		{
			return ISteamRemoteStorage._UGCRead(this.Self, hContent, pvData, cubDataToRead, cOffset, eAction);
		}

		// Token: 0x06000555 RID: 1365
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetCachedUGCCount")]
		private static extern int _GetCachedUGCCount(IntPtr self);

		// Token: 0x06000556 RID: 1366 RVA: 0x00008AF8 File Offset: 0x00006CF8
		internal int GetCachedUGCCount()
		{
			return ISteamRemoteStorage._GetCachedUGCCount(this.Self);
		}

		// Token: 0x06000557 RID: 1367
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetCachedUGCHandle")]
		private static extern UGCHandle_t _GetCachedUGCHandle(IntPtr self, int iCachedContent);

		// Token: 0x06000558 RID: 1368 RVA: 0x00008B18 File Offset: 0x00006D18
		internal UGCHandle_t GetCachedUGCHandle(int iCachedContent)
		{
			return ISteamRemoteStorage._GetCachedUGCHandle(this.Self, iCachedContent);
		}

		// Token: 0x06000559 RID: 1369
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UGCDownloadToLocation")]
		private static extern SteamAPICall_t _UGCDownloadToLocation(IntPtr self, UGCHandle_t hContent, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchLocation, uint unPriority);

		// Token: 0x0600055A RID: 1370 RVA: 0x00008B38 File Offset: 0x00006D38
		internal CallResult<RemoteStorageDownloadUGCResult_t> UGCDownloadToLocation(UGCHandle_t hContent, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchLocation, uint unPriority)
		{
			SteamAPICall_t call = ISteamRemoteStorage._UGCDownloadToLocation(this.Self, hContent, pchLocation, unPriority);
			return new CallResult<RemoteStorageDownloadUGCResult_t>(call, base.IsServer);
		}
	}
}
