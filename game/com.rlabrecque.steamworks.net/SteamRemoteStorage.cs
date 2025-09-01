using System;
using System.Collections.Generic;

namespace Steamworks
{
	// Token: 0x02000020 RID: 32
	public static class SteamRemoteStorage
	{
		// Token: 0x0600038C RID: 908 RVA: 0x000094B8 File Offset: 0x000076B8
		public static bool FileWrite(string pchFile, byte[] pvData, int cubData)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = NativeMethods.ISteamRemoteStorage_FileWrite(CSteamAPIContext.GetSteamRemoteStorage(), utf8StringHandle, pvData, cubData);
			}
			return result;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000094FC File Offset: 0x000076FC
		public static int FileRead(string pchFile, byte[] pvData, int cubDataToRead)
		{
			InteropHelp.TestIfAvailableClient();
			int result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = NativeMethods.ISteamRemoteStorage_FileRead(CSteamAPIContext.GetSteamRemoteStorage(), utf8StringHandle, pvData, cubDataToRead);
			}
			return result;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00009540 File Offset: 0x00007740
		public static SteamAPICall_t FileWriteAsync(string pchFile, byte[] pvData, uint cubData)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_FileWriteAsync(CSteamAPIContext.GetSteamRemoteStorage(), utf8StringHandle, pvData, cubData);
			}
			return result;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000958C File Offset: 0x0000778C
		public static SteamAPICall_t FileReadAsync(string pchFile, uint nOffset, uint cubToRead)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_FileReadAsync(CSteamAPIContext.GetSteamRemoteStorage(), utf8StringHandle, nOffset, cubToRead);
			}
			return result;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000095D8 File Offset: 0x000077D8
		public static bool FileReadAsyncComplete(SteamAPICall_t hReadCall, byte[] pvBuffer, uint cubToRead)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_FileReadAsyncComplete(CSteamAPIContext.GetSteamRemoteStorage(), hReadCall, pvBuffer, cubToRead);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000095EC File Offset: 0x000077EC
		public static bool FileForget(string pchFile)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = NativeMethods.ISteamRemoteStorage_FileForget(CSteamAPIContext.GetSteamRemoteStorage(), utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00009630 File Offset: 0x00007830
		public static bool FileDelete(string pchFile)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = NativeMethods.ISteamRemoteStorage_FileDelete(CSteamAPIContext.GetSteamRemoteStorage(), utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00009674 File Offset: 0x00007874
		public static SteamAPICall_t FileShare(string pchFile)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_FileShare(CSteamAPIContext.GetSteamRemoteStorage(), utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x000096BC File Offset: 0x000078BC
		public static bool SetSyncPlatforms(string pchFile, ERemoteStoragePlatform eRemoteStoragePlatform)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = NativeMethods.ISteamRemoteStorage_SetSyncPlatforms(CSteamAPIContext.GetSteamRemoteStorage(), utf8StringHandle, eRemoteStoragePlatform);
			}
			return result;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00009700 File Offset: 0x00007900
		public static UGCFileWriteStreamHandle_t FileWriteStreamOpen(string pchFile)
		{
			InteropHelp.TestIfAvailableClient();
			UGCFileWriteStreamHandle_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = (UGCFileWriteStreamHandle_t)NativeMethods.ISteamRemoteStorage_FileWriteStreamOpen(CSteamAPIContext.GetSteamRemoteStorage(), utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00009748 File Offset: 0x00007948
		public static bool FileWriteStreamWriteChunk(UGCFileWriteStreamHandle_t writeHandle, byte[] pvData, int cubData)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_FileWriteStreamWriteChunk(CSteamAPIContext.GetSteamRemoteStorage(), writeHandle, pvData, cubData);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000975C File Offset: 0x0000795C
		public static bool FileWriteStreamClose(UGCFileWriteStreamHandle_t writeHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_FileWriteStreamClose(CSteamAPIContext.GetSteamRemoteStorage(), writeHandle);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000976E File Offset: 0x0000796E
		public static bool FileWriteStreamCancel(UGCFileWriteStreamHandle_t writeHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_FileWriteStreamCancel(CSteamAPIContext.GetSteamRemoteStorage(), writeHandle);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00009780 File Offset: 0x00007980
		public static bool FileExists(string pchFile)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = NativeMethods.ISteamRemoteStorage_FileExists(CSteamAPIContext.GetSteamRemoteStorage(), utf8StringHandle);
			}
			return result;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x000097C4 File Offset: 0x000079C4
		public static bool FilePersisted(string pchFile)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = NativeMethods.ISteamRemoteStorage_FilePersisted(CSteamAPIContext.GetSteamRemoteStorage(), utf8StringHandle);
			}
			return result;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00009808 File Offset: 0x00007A08
		public static int GetFileSize(string pchFile)
		{
			InteropHelp.TestIfAvailableClient();
			int result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = NativeMethods.ISteamRemoteStorage_GetFileSize(CSteamAPIContext.GetSteamRemoteStorage(), utf8StringHandle);
			}
			return result;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000984C File Offset: 0x00007A4C
		public static long GetFileTimestamp(string pchFile)
		{
			InteropHelp.TestIfAvailableClient();
			long result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = NativeMethods.ISteamRemoteStorage_GetFileTimestamp(CSteamAPIContext.GetSteamRemoteStorage(), utf8StringHandle);
			}
			return result;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00009890 File Offset: 0x00007A90
		public static ERemoteStoragePlatform GetSyncPlatforms(string pchFile)
		{
			InteropHelp.TestIfAvailableClient();
			ERemoteStoragePlatform result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = NativeMethods.ISteamRemoteStorage_GetSyncPlatforms(CSteamAPIContext.GetSteamRemoteStorage(), utf8StringHandle);
			}
			return result;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000098D4 File Offset: 0x00007AD4
		public static int GetFileCount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_GetFileCount(CSteamAPIContext.GetSteamRemoteStorage());
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000098E5 File Offset: 0x00007AE5
		public static string GetFileNameAndSize(int iFile, out int pnFileSizeInBytes)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamRemoteStorage_GetFileNameAndSize(CSteamAPIContext.GetSteamRemoteStorage(), iFile, out pnFileSizeInBytes));
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000098FD File Offset: 0x00007AFD
		public static bool GetQuota(out ulong pnTotalBytes, out ulong puAvailableBytes)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_GetQuota(CSteamAPIContext.GetSteamRemoteStorage(), out pnTotalBytes, out puAvailableBytes);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00009910 File Offset: 0x00007B10
		public static bool IsCloudEnabledForAccount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_IsCloudEnabledForAccount(CSteamAPIContext.GetSteamRemoteStorage());
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00009921 File Offset: 0x00007B21
		public static bool IsCloudEnabledForApp()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_IsCloudEnabledForApp(CSteamAPIContext.GetSteamRemoteStorage());
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00009932 File Offset: 0x00007B32
		public static void SetCloudEnabledForApp(bool bEnabled)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamRemoteStorage_SetCloudEnabledForApp(CSteamAPIContext.GetSteamRemoteStorage(), bEnabled);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00009944 File Offset: 0x00007B44
		public static SteamAPICall_t UGCDownload(UGCHandle_t hContent, uint unPriority)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_UGCDownload(CSteamAPIContext.GetSteamRemoteStorage(), hContent, unPriority);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000995C File Offset: 0x00007B5C
		public static bool GetUGCDownloadProgress(UGCHandle_t hContent, out int pnBytesDownloaded, out int pnBytesExpected)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_GetUGCDownloadProgress(CSteamAPIContext.GetSteamRemoteStorage(), hContent, out pnBytesDownloaded, out pnBytesExpected);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00009970 File Offset: 0x00007B70
		public static bool GetUGCDetails(UGCHandle_t hContent, out AppId_t pnAppID, out string ppchName, out int pnFileSizeInBytes, out CSteamID pSteamIDOwner)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr nativeUtf;
			bool flag = NativeMethods.ISteamRemoteStorage_GetUGCDetails(CSteamAPIContext.GetSteamRemoteStorage(), hContent, out pnAppID, out nativeUtf, out pnFileSizeInBytes, out pSteamIDOwner);
			ppchName = (flag ? InteropHelp.PtrToStringUTF8(nativeUtf) : null);
			return flag;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x000099A3 File Offset: 0x00007BA3
		public static int UGCRead(UGCHandle_t hContent, byte[] pvData, int cubDataToRead, uint cOffset, EUGCReadAction eAction)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_UGCRead(CSteamAPIContext.GetSteamRemoteStorage(), hContent, pvData, cubDataToRead, cOffset, eAction);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x000099BA File Offset: 0x00007BBA
		public static int GetCachedUGCCount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_GetCachedUGCCount(CSteamAPIContext.GetSteamRemoteStorage());
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000099CB File Offset: 0x00007BCB
		public static UGCHandle_t GetCachedUGCHandle(int iCachedContent)
		{
			InteropHelp.TestIfAvailableClient();
			return (UGCHandle_t)NativeMethods.ISteamRemoteStorage_GetCachedUGCHandle(CSteamAPIContext.GetSteamRemoteStorage(), iCachedContent);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000099E4 File Offset: 0x00007BE4
		public static SteamAPICall_t PublishWorkshopFile(string pchFile, string pchPreviewFile, AppId_t nConsumerAppId, string pchTitle, string pchDescription, ERemoteStoragePublishedFileVisibility eVisibility, IList<string> pTags, EWorkshopFileType eWorkshopFileType)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchPreviewFile))
				{
					using (InteropHelp.UTF8StringHandle utf8StringHandle3 = new InteropHelp.UTF8StringHandle(pchTitle))
					{
						using (InteropHelp.UTF8StringHandle utf8StringHandle4 = new InteropHelp.UTF8StringHandle(pchDescription))
						{
							result = (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_PublishWorkshopFile(CSteamAPIContext.GetSteamRemoteStorage(), utf8StringHandle, utf8StringHandle2, nConsumerAppId, utf8StringHandle3, utf8StringHandle4, eVisibility, new InteropHelp.SteamParamStringArray(pTags), eWorkshopFileType);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00009A9C File Offset: 0x00007C9C
		public static PublishedFileUpdateHandle_t CreatePublishedFileUpdateRequest(PublishedFileId_t unPublishedFileId)
		{
			InteropHelp.TestIfAvailableClient();
			return (PublishedFileUpdateHandle_t)NativeMethods.ISteamRemoteStorage_CreatePublishedFileUpdateRequest(CSteamAPIContext.GetSteamRemoteStorage(), unPublishedFileId);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00009AB4 File Offset: 0x00007CB4
		public static bool UpdatePublishedFileFile(PublishedFileUpdateHandle_t updateHandle, string pchFile)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFile))
			{
				result = NativeMethods.ISteamRemoteStorage_UpdatePublishedFileFile(CSteamAPIContext.GetSteamRemoteStorage(), updateHandle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00009AF8 File Offset: 0x00007CF8
		public static bool UpdatePublishedFilePreviewFile(PublishedFileUpdateHandle_t updateHandle, string pchPreviewFile)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchPreviewFile))
			{
				result = NativeMethods.ISteamRemoteStorage_UpdatePublishedFilePreviewFile(CSteamAPIContext.GetSteamRemoteStorage(), updateHandle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00009B3C File Offset: 0x00007D3C
		public static bool UpdatePublishedFileTitle(PublishedFileUpdateHandle_t updateHandle, string pchTitle)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchTitle))
			{
				result = NativeMethods.ISteamRemoteStorage_UpdatePublishedFileTitle(CSteamAPIContext.GetSteamRemoteStorage(), updateHandle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00009B80 File Offset: 0x00007D80
		public static bool UpdatePublishedFileDescription(PublishedFileUpdateHandle_t updateHandle, string pchDescription)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchDescription))
			{
				result = NativeMethods.ISteamRemoteStorage_UpdatePublishedFileDescription(CSteamAPIContext.GetSteamRemoteStorage(), updateHandle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00009BC4 File Offset: 0x00007DC4
		public static bool UpdatePublishedFileVisibility(PublishedFileUpdateHandle_t updateHandle, ERemoteStoragePublishedFileVisibility eVisibility)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_UpdatePublishedFileVisibility(CSteamAPIContext.GetSteamRemoteStorage(), updateHandle, eVisibility);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00009BD7 File Offset: 0x00007DD7
		public static bool UpdatePublishedFileTags(PublishedFileUpdateHandle_t updateHandle, IList<string> pTags)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_UpdatePublishedFileTags(CSteamAPIContext.GetSteamRemoteStorage(), updateHandle, new InteropHelp.SteamParamStringArray(pTags));
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00009BF4 File Offset: 0x00007DF4
		public static SteamAPICall_t CommitPublishedFileUpdate(PublishedFileUpdateHandle_t updateHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_CommitPublishedFileUpdate(CSteamAPIContext.GetSteamRemoteStorage(), updateHandle);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00009C0B File Offset: 0x00007E0B
		public static SteamAPICall_t GetPublishedFileDetails(PublishedFileId_t unPublishedFileId, uint unMaxSecondsOld)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_GetPublishedFileDetails(CSteamAPIContext.GetSteamRemoteStorage(), unPublishedFileId, unMaxSecondsOld);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00009C23 File Offset: 0x00007E23
		public static SteamAPICall_t DeletePublishedFile(PublishedFileId_t unPublishedFileId)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_DeletePublishedFile(CSteamAPIContext.GetSteamRemoteStorage(), unPublishedFileId);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00009C3A File Offset: 0x00007E3A
		public static SteamAPICall_t EnumerateUserPublishedFiles(uint unStartIndex)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_EnumerateUserPublishedFiles(CSteamAPIContext.GetSteamRemoteStorage(), unStartIndex);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00009C51 File Offset: 0x00007E51
		public static SteamAPICall_t SubscribePublishedFile(PublishedFileId_t unPublishedFileId)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_SubscribePublishedFile(CSteamAPIContext.GetSteamRemoteStorage(), unPublishedFileId);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00009C68 File Offset: 0x00007E68
		public static SteamAPICall_t EnumerateUserSubscribedFiles(uint unStartIndex)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_EnumerateUserSubscribedFiles(CSteamAPIContext.GetSteamRemoteStorage(), unStartIndex);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00009C7F File Offset: 0x00007E7F
		public static SteamAPICall_t UnsubscribePublishedFile(PublishedFileId_t unPublishedFileId)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_UnsubscribePublishedFile(CSteamAPIContext.GetSteamRemoteStorage(), unPublishedFileId);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00009C98 File Offset: 0x00007E98
		public static bool UpdatePublishedFileSetChangeDescription(PublishedFileUpdateHandle_t updateHandle, string pchChangeDescription)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchChangeDescription))
			{
				result = NativeMethods.ISteamRemoteStorage_UpdatePublishedFileSetChangeDescription(CSteamAPIContext.GetSteamRemoteStorage(), updateHandle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00009CDC File Offset: 0x00007EDC
		public static SteamAPICall_t GetPublishedItemVoteDetails(PublishedFileId_t unPublishedFileId)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_GetPublishedItemVoteDetails(CSteamAPIContext.GetSteamRemoteStorage(), unPublishedFileId);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00009CF3 File Offset: 0x00007EF3
		public static SteamAPICall_t UpdateUserPublishedItemVote(PublishedFileId_t unPublishedFileId, bool bVoteUp)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_UpdateUserPublishedItemVote(CSteamAPIContext.GetSteamRemoteStorage(), unPublishedFileId, bVoteUp);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00009D0B File Offset: 0x00007F0B
		public static SteamAPICall_t GetUserPublishedItemVoteDetails(PublishedFileId_t unPublishedFileId)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_GetUserPublishedItemVoteDetails(CSteamAPIContext.GetSteamRemoteStorage(), unPublishedFileId);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00009D22 File Offset: 0x00007F22
		public static SteamAPICall_t EnumerateUserSharedWorkshopFiles(CSteamID steamId, uint unStartIndex, IList<string> pRequiredTags, IList<string> pExcludedTags)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_EnumerateUserSharedWorkshopFiles(CSteamAPIContext.GetSteamRemoteStorage(), steamId, unStartIndex, new InteropHelp.SteamParamStringArray(pRequiredTags), new InteropHelp.SteamParamStringArray(pExcludedTags));
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00009D50 File Offset: 0x00007F50
		public static SteamAPICall_t PublishVideo(EWorkshopVideoProvider eVideoProvider, string pchVideoAccount, string pchVideoIdentifier, string pchPreviewFile, AppId_t nConsumerAppId, string pchTitle, string pchDescription, ERemoteStoragePublishedFileVisibility eVisibility, IList<string> pTags)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchVideoAccount))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchVideoIdentifier))
				{
					using (InteropHelp.UTF8StringHandle utf8StringHandle3 = new InteropHelp.UTF8StringHandle(pchPreviewFile))
					{
						using (InteropHelp.UTF8StringHandle utf8StringHandle4 = new InteropHelp.UTF8StringHandle(pchTitle))
						{
							using (InteropHelp.UTF8StringHandle utf8StringHandle5 = new InteropHelp.UTF8StringHandle(pchDescription))
							{
								result = (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_PublishVideo(CSteamAPIContext.GetSteamRemoteStorage(), eVideoProvider, utf8StringHandle, utf8StringHandle2, utf8StringHandle3, nConsumerAppId, utf8StringHandle4, utf8StringHandle5, eVisibility, new InteropHelp.SteamParamStringArray(pTags));
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00009E28 File Offset: 0x00008028
		public static SteamAPICall_t SetUserPublishedFileAction(PublishedFileId_t unPublishedFileId, EWorkshopFileAction eAction)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_SetUserPublishedFileAction(CSteamAPIContext.GetSteamRemoteStorage(), unPublishedFileId, eAction);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00009E40 File Offset: 0x00008040
		public static SteamAPICall_t EnumeratePublishedFilesByUserAction(EWorkshopFileAction eAction, uint unStartIndex)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_EnumeratePublishedFilesByUserAction(CSteamAPIContext.GetSteamRemoteStorage(), eAction, unStartIndex);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00009E58 File Offset: 0x00008058
		public static SteamAPICall_t EnumeratePublishedWorkshopFiles(EWorkshopEnumerationType eEnumerationType, uint unStartIndex, uint unCount, uint unDays, IList<string> pTags, IList<string> pUserTags)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_EnumeratePublishedWorkshopFiles(CSteamAPIContext.GetSteamRemoteStorage(), eEnumerationType, unStartIndex, unCount, unDays, new InteropHelp.SteamParamStringArray(pTags), new InteropHelp.SteamParamStringArray(pUserTags));
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00009E8C File Offset: 0x0000808C
		public static SteamAPICall_t UGCDownloadToLocation(UGCHandle_t hContent, string pchLocation, uint unPriority)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchLocation))
			{
				result = (SteamAPICall_t)NativeMethods.ISteamRemoteStorage_UGCDownloadToLocation(CSteamAPIContext.GetSteamRemoteStorage(), hContent, utf8StringHandle, unPriority);
			}
			return result;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00009ED8 File Offset: 0x000080D8
		public static int GetLocalFileChangeCount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_GetLocalFileChangeCount(CSteamAPIContext.GetSteamRemoteStorage());
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00009EE9 File Offset: 0x000080E9
		public static string GetLocalFileChange(int iFile, out ERemoteStorageLocalFileChange pEChangeType, out ERemoteStorageFilePathType pEFilePathType)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamRemoteStorage_GetLocalFileChange(CSteamAPIContext.GetSteamRemoteStorage(), iFile, out pEChangeType, out pEFilePathType));
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00009F02 File Offset: 0x00008102
		public static bool BeginFileWriteBatch()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_BeginFileWriteBatch(CSteamAPIContext.GetSteamRemoteStorage());
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00009F13 File Offset: 0x00008113
		public static bool EndFileWriteBatch()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemoteStorage_EndFileWriteBatch(CSteamAPIContext.GetSteamRemoteStorage());
		}
	}
}
