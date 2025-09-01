using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200000E RID: 14
	public static class SteamGameServerUGC
	{
		// Token: 0x0600018E RID: 398 RVA: 0x0000575B File Offset: 0x0000395B
		public static UGCQueryHandle_t CreateQueryUserUGCRequest(AccountID_t unAccountID, EUserUGCList eListType, EUGCMatchingUGCType eMatchingUGCType, EUserUGCListSortOrder eSortOrder, AppId_t nCreatorAppID, AppId_t nConsumerAppID, uint unPage)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (UGCQueryHandle_t)NativeMethods.ISteamUGC_CreateQueryUserUGCRequest(CSteamGameServerAPIContext.GetSteamUGC(), unAccountID, eListType, eMatchingUGCType, eSortOrder, nCreatorAppID, nConsumerAppID, unPage);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000577B File Offset: 0x0000397B
		public static UGCQueryHandle_t CreateQueryAllUGCRequest(EUGCQuery eQueryType, EUGCMatchingUGCType eMatchingeMatchingUGCTypeFileType, AppId_t nCreatorAppID, AppId_t nConsumerAppID, uint unPage)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (UGCQueryHandle_t)NativeMethods.ISteamUGC_CreateQueryAllUGCRequestPage(CSteamGameServerAPIContext.GetSteamUGC(), eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID, nConsumerAppID, unPage);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005798 File Offset: 0x00003998
		public static UGCQueryHandle_t CreateQueryAllUGCRequest(EUGCQuery eQueryType, EUGCMatchingUGCType eMatchingeMatchingUGCTypeFileType, AppId_t nCreatorAppID, AppId_t nConsumerAppID, string pchCursor = null)
		{
			InteropHelp.TestIfAvailableGameServer();
			UGCQueryHandle_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchCursor))
			{
				result = (UGCQueryHandle_t)NativeMethods.ISteamUGC_CreateQueryAllUGCRequestCursor(CSteamGameServerAPIContext.GetSteamUGC(), eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID, nConsumerAppID, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000057E4 File Offset: 0x000039E4
		public static UGCQueryHandle_t CreateQueryUGCDetailsRequest(PublishedFileId_t[] pvecPublishedFileID, uint unNumPublishedFileIDs)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (UGCQueryHandle_t)NativeMethods.ISteamUGC_CreateQueryUGCDetailsRequest(CSteamGameServerAPIContext.GetSteamUGC(), pvecPublishedFileID, unNumPublishedFileIDs);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000057FC File Offset: 0x000039FC
		public static SteamAPICall_t SendQueryUGCRequest(UGCQueryHandle_t handle)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_SendQueryUGCRequest(CSteamGameServerAPIContext.GetSteamUGC(), handle);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005813 File Offset: 0x00003A13
		public static bool GetQueryUGCResult(UGCQueryHandle_t handle, uint index, out SteamUGCDetails_t pDetails)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_GetQueryUGCResult(CSteamGameServerAPIContext.GetSteamUGC(), handle, index, out pDetails);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00005827 File Offset: 0x00003A27
		public static uint GetQueryUGCNumTags(UGCQueryHandle_t handle, uint index)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_GetQueryUGCNumTags(CSteamGameServerAPIContext.GetSteamUGC(), handle, index);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000583C File Offset: 0x00003A3C
		public static bool GetQueryUGCTag(UGCQueryHandle_t handle, uint index, uint indexTag, out string pchValue, uint cchValueSize)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cchValueSize);
			bool flag = NativeMethods.ISteamUGC_GetQueryUGCTag(CSteamGameServerAPIContext.GetSteamUGC(), handle, index, indexTag, intPtr, cchValueSize);
			pchValue = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000587C File Offset: 0x00003A7C
		public static bool GetQueryUGCTagDisplayName(UGCQueryHandle_t handle, uint index, uint indexTag, out string pchValue, uint cchValueSize)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cchValueSize);
			bool flag = NativeMethods.ISteamUGC_GetQueryUGCTagDisplayName(CSteamGameServerAPIContext.GetSteamUGC(), handle, index, indexTag, intPtr, cchValueSize);
			pchValue = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000058BC File Offset: 0x00003ABC
		public static bool GetQueryUGCPreviewURL(UGCQueryHandle_t handle, uint index, out string pchURL, uint cchURLSize)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cchURLSize);
			bool flag = NativeMethods.ISteamUGC_GetQueryUGCPreviewURL(CSteamGameServerAPIContext.GetSteamUGC(), handle, index, intPtr, cchURLSize);
			pchURL = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000058FC File Offset: 0x00003AFC
		public static bool GetQueryUGCMetadata(UGCQueryHandle_t handle, uint index, out string pchMetadata, uint cchMetadatasize)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cchMetadatasize);
			bool flag = NativeMethods.ISteamUGC_GetQueryUGCMetadata(CSteamGameServerAPIContext.GetSteamUGC(), handle, index, intPtr, cchMetadatasize);
			pchMetadata = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00005939 File Offset: 0x00003B39
		public static bool GetQueryUGCChildren(UGCQueryHandle_t handle, uint index, PublishedFileId_t[] pvecPublishedFileID, uint cMaxEntries)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_GetQueryUGCChildren(CSteamGameServerAPIContext.GetSteamUGC(), handle, index, pvecPublishedFileID, cMaxEntries);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000594E File Offset: 0x00003B4E
		public static bool GetQueryUGCStatistic(UGCQueryHandle_t handle, uint index, EItemStatistic eStatType, out ulong pStatValue)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_GetQueryUGCStatistic(CSteamGameServerAPIContext.GetSteamUGC(), handle, index, eStatType, out pStatValue);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005963 File Offset: 0x00003B63
		public static uint GetQueryUGCNumAdditionalPreviews(UGCQueryHandle_t handle, uint index)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_GetQueryUGCNumAdditionalPreviews(CSteamGameServerAPIContext.GetSteamUGC(), handle, index);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005978 File Offset: 0x00003B78
		public static bool GetQueryUGCAdditionalPreview(UGCQueryHandle_t handle, uint index, uint previewIndex, out string pchURLOrVideoID, uint cchURLSize, out string pchOriginalFileName, uint cchOriginalFileNameSize, out EItemPreviewType pPreviewType)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cchURLSize);
			IntPtr intPtr2 = Marshal.AllocHGlobal((int)cchOriginalFileNameSize);
			bool flag = NativeMethods.ISteamUGC_GetQueryUGCAdditionalPreview(CSteamGameServerAPIContext.GetSteamUGC(), handle, index, previewIndex, intPtr, cchURLSize, intPtr2, cchOriginalFileNameSize, out pPreviewType);
			pchURLOrVideoID = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			pchOriginalFileName = (flag ? InteropHelp.PtrToStringUTF8(intPtr2) : null);
			Marshal.FreeHGlobal(intPtr2);
			return flag;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000059DA File Offset: 0x00003BDA
		public static uint GetQueryUGCNumKeyValueTags(UGCQueryHandle_t handle, uint index)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_GetQueryUGCNumKeyValueTags(CSteamGameServerAPIContext.GetSteamUGC(), handle, index);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000059F0 File Offset: 0x00003BF0
		public static bool GetQueryUGCKeyValueTag(UGCQueryHandle_t handle, uint index, uint keyValueTagIndex, out string pchKey, uint cchKeySize, out string pchValue, uint cchValueSize)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cchKeySize);
			IntPtr intPtr2 = Marshal.AllocHGlobal((int)cchValueSize);
			bool flag = NativeMethods.ISteamUGC_GetQueryUGCKeyValueTag(CSteamGameServerAPIContext.GetSteamUGC(), handle, index, keyValueTagIndex, intPtr, cchKeySize, intPtr2, cchValueSize);
			pchKey = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			pchValue = (flag ? InteropHelp.PtrToStringUTF8(intPtr2) : null);
			Marshal.FreeHGlobal(intPtr2);
			return flag;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00005A50 File Offset: 0x00003C50
		public static bool GetQueryUGCKeyValueTag(UGCQueryHandle_t handle, uint index, string pchKey, out string pchValue, uint cchValueSize)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cchValueSize);
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKey))
			{
				bool flag = NativeMethods.ISteamUGC_GetQueryFirstUGCKeyValueTag(CSteamGameServerAPIContext.GetSteamUGC(), handle, index, utf8StringHandle, intPtr, cchValueSize);
				pchValue = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
				Marshal.FreeHGlobal(intPtr);
				result = flag;
			}
			return result;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00005AB8 File Offset: 0x00003CB8
		public static uint GetQueryUGCContentDescriptors(UGCQueryHandle_t handle, uint index, out EUGCContentDescriptorID pvecDescriptors, uint cMaxEntries)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_GetQueryUGCContentDescriptors(CSteamGameServerAPIContext.GetSteamUGC(), handle, index, out pvecDescriptors, cMaxEntries);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00005ACD File Offset: 0x00003CCD
		public static bool ReleaseQueryUGCRequest(UGCQueryHandle_t handle)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_ReleaseQueryUGCRequest(CSteamGameServerAPIContext.GetSteamUGC(), handle);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00005AE0 File Offset: 0x00003CE0
		public static bool AddRequiredTag(UGCQueryHandle_t handle, string pTagName)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pTagName))
			{
				result = NativeMethods.ISteamUGC_AddRequiredTag(CSteamGameServerAPIContext.GetSteamUGC(), handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00005B24 File Offset: 0x00003D24
		public static bool AddRequiredTagGroup(UGCQueryHandle_t handle, IList<string> pTagGroups)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_AddRequiredTagGroup(CSteamGameServerAPIContext.GetSteamUGC(), handle, new InteropHelp.SteamParamStringArray(pTagGroups));
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00005B44 File Offset: 0x00003D44
		public static bool AddExcludedTag(UGCQueryHandle_t handle, string pTagName)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pTagName))
			{
				result = NativeMethods.ISteamUGC_AddExcludedTag(CSteamGameServerAPIContext.GetSteamUGC(), handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00005B88 File Offset: 0x00003D88
		public static bool SetReturnOnlyIDs(UGCQueryHandle_t handle, bool bReturnOnlyIDs)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_SetReturnOnlyIDs(CSteamGameServerAPIContext.GetSteamUGC(), handle, bReturnOnlyIDs);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00005B9B File Offset: 0x00003D9B
		public static bool SetReturnKeyValueTags(UGCQueryHandle_t handle, bool bReturnKeyValueTags)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_SetReturnKeyValueTags(CSteamGameServerAPIContext.GetSteamUGC(), handle, bReturnKeyValueTags);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00005BAE File Offset: 0x00003DAE
		public static bool SetReturnLongDescription(UGCQueryHandle_t handle, bool bReturnLongDescription)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_SetReturnLongDescription(CSteamGameServerAPIContext.GetSteamUGC(), handle, bReturnLongDescription);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00005BC1 File Offset: 0x00003DC1
		public static bool SetReturnMetadata(UGCQueryHandle_t handle, bool bReturnMetadata)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_SetReturnMetadata(CSteamGameServerAPIContext.GetSteamUGC(), handle, bReturnMetadata);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00005BD4 File Offset: 0x00003DD4
		public static bool SetReturnChildren(UGCQueryHandle_t handle, bool bReturnChildren)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_SetReturnChildren(CSteamGameServerAPIContext.GetSteamUGC(), handle, bReturnChildren);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00005BE7 File Offset: 0x00003DE7
		public static bool SetReturnAdditionalPreviews(UGCQueryHandle_t handle, bool bReturnAdditionalPreviews)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_SetReturnAdditionalPreviews(CSteamGameServerAPIContext.GetSteamUGC(), handle, bReturnAdditionalPreviews);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00005BFA File Offset: 0x00003DFA
		public static bool SetReturnTotalOnly(UGCQueryHandle_t handle, bool bReturnTotalOnly)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_SetReturnTotalOnly(CSteamGameServerAPIContext.GetSteamUGC(), handle, bReturnTotalOnly);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00005C0D File Offset: 0x00003E0D
		public static bool SetReturnPlaytimeStats(UGCQueryHandle_t handle, uint unDays)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_SetReturnPlaytimeStats(CSteamGameServerAPIContext.GetSteamUGC(), handle, unDays);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00005C20 File Offset: 0x00003E20
		public static bool SetLanguage(UGCQueryHandle_t handle, string pchLanguage)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchLanguage))
			{
				result = NativeMethods.ISteamUGC_SetLanguage(CSteamGameServerAPIContext.GetSteamUGC(), handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00005C64 File Offset: 0x00003E64
		public static bool SetAllowCachedResponse(UGCQueryHandle_t handle, uint unMaxAgeSeconds)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_SetAllowCachedResponse(CSteamGameServerAPIContext.GetSteamUGC(), handle, unMaxAgeSeconds);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00005C78 File Offset: 0x00003E78
		public static bool SetCloudFileNameFilter(UGCQueryHandle_t handle, string pMatchCloudFileName)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pMatchCloudFileName))
			{
				result = NativeMethods.ISteamUGC_SetCloudFileNameFilter(CSteamGameServerAPIContext.GetSteamUGC(), handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00005CBC File Offset: 0x00003EBC
		public static bool SetMatchAnyTag(UGCQueryHandle_t handle, bool bMatchAnyTag)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_SetMatchAnyTag(CSteamGameServerAPIContext.GetSteamUGC(), handle, bMatchAnyTag);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00005CD0 File Offset: 0x00003ED0
		public static bool SetSearchText(UGCQueryHandle_t handle, string pSearchText)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pSearchText))
			{
				result = NativeMethods.ISteamUGC_SetSearchText(CSteamGameServerAPIContext.GetSteamUGC(), handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00005D14 File Offset: 0x00003F14
		public static bool SetRankedByTrendDays(UGCQueryHandle_t handle, uint unDays)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_SetRankedByTrendDays(CSteamGameServerAPIContext.GetSteamUGC(), handle, unDays);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00005D27 File Offset: 0x00003F27
		public static bool SetTimeCreatedDateRange(UGCQueryHandle_t handle, uint rtStart, uint rtEnd)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_SetTimeCreatedDateRange(CSteamGameServerAPIContext.GetSteamUGC(), handle, rtStart, rtEnd);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00005D3B File Offset: 0x00003F3B
		public static bool SetTimeUpdatedDateRange(UGCQueryHandle_t handle, uint rtStart, uint rtEnd)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_SetTimeUpdatedDateRange(CSteamGameServerAPIContext.GetSteamUGC(), handle, rtStart, rtEnd);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00005D50 File Offset: 0x00003F50
		public static bool AddRequiredKeyValueTag(UGCQueryHandle_t handle, string pKey, string pValue)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pKey))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pValue))
				{
					result = NativeMethods.ISteamUGC_AddRequiredKeyValueTag(CSteamGameServerAPIContext.GetSteamUGC(), handle, utf8StringHandle, utf8StringHandle2);
				}
			}
			return result;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00005DB4 File Offset: 0x00003FB4
		public static SteamAPICall_t RequestUGCDetails(PublishedFileId_t nPublishedFileID, uint unMaxAgeSeconds)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_RequestUGCDetails(CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID, unMaxAgeSeconds);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00005DCC File Offset: 0x00003FCC
		public static SteamAPICall_t CreateItem(AppId_t nConsumerAppId, EWorkshopFileType eFileType)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_CreateItem(CSteamGameServerAPIContext.GetSteamUGC(), nConsumerAppId, eFileType);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00005DE4 File Offset: 0x00003FE4
		public static UGCUpdateHandle_t StartItemUpdate(AppId_t nConsumerAppId, PublishedFileId_t nPublishedFileID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (UGCUpdateHandle_t)NativeMethods.ISteamUGC_StartItemUpdate(CSteamGameServerAPIContext.GetSteamUGC(), nConsumerAppId, nPublishedFileID);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00005DFC File Offset: 0x00003FFC
		public static bool SetItemTitle(UGCUpdateHandle_t handle, string pchTitle)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchTitle))
			{
				result = NativeMethods.ISteamUGC_SetItemTitle(CSteamGameServerAPIContext.GetSteamUGC(), handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00005E40 File Offset: 0x00004040
		public static bool SetItemDescription(UGCUpdateHandle_t handle, string pchDescription)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchDescription))
			{
				result = NativeMethods.ISteamUGC_SetItemDescription(CSteamGameServerAPIContext.GetSteamUGC(), handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00005E84 File Offset: 0x00004084
		public static bool SetItemUpdateLanguage(UGCUpdateHandle_t handle, string pchLanguage)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchLanguage))
			{
				result = NativeMethods.ISteamUGC_SetItemUpdateLanguage(CSteamGameServerAPIContext.GetSteamUGC(), handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00005EC8 File Offset: 0x000040C8
		public static bool SetItemMetadata(UGCUpdateHandle_t handle, string pchMetaData)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchMetaData))
			{
				result = NativeMethods.ISteamUGC_SetItemMetadata(CSteamGameServerAPIContext.GetSteamUGC(), handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00005F0C File Offset: 0x0000410C
		public static bool SetItemVisibility(UGCUpdateHandle_t handle, ERemoteStoragePublishedFileVisibility eVisibility)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_SetItemVisibility(CSteamGameServerAPIContext.GetSteamUGC(), handle, eVisibility);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00005F1F File Offset: 0x0000411F
		public static bool SetItemTags(UGCUpdateHandle_t updateHandle, IList<string> pTags)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_SetItemTags(CSteamGameServerAPIContext.GetSteamUGC(), updateHandle, new InteropHelp.SteamParamStringArray(pTags));
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00005F3C File Offset: 0x0000413C
		public static bool SetItemContent(UGCUpdateHandle_t handle, string pszContentFolder)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszContentFolder))
			{
				result = NativeMethods.ISteamUGC_SetItemContent(CSteamGameServerAPIContext.GetSteamUGC(), handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00005F80 File Offset: 0x00004180
		public static bool SetItemPreview(UGCUpdateHandle_t handle, string pszPreviewFile)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszPreviewFile))
			{
				result = NativeMethods.ISteamUGC_SetItemPreview(CSteamGameServerAPIContext.GetSteamUGC(), handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00005FC4 File Offset: 0x000041C4
		public static bool SetAllowLegacyUpload(UGCUpdateHandle_t handle, bool bAllowLegacyUpload)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_SetAllowLegacyUpload(CSteamGameServerAPIContext.GetSteamUGC(), handle, bAllowLegacyUpload);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00005FD7 File Offset: 0x000041D7
		public static bool RemoveAllItemKeyValueTags(UGCUpdateHandle_t handle)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_RemoveAllItemKeyValueTags(CSteamGameServerAPIContext.GetSteamUGC(), handle);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00005FEC File Offset: 0x000041EC
		public static bool RemoveItemKeyValueTags(UGCUpdateHandle_t handle, string pchKey)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKey))
			{
				result = NativeMethods.ISteamUGC_RemoveItemKeyValueTags(CSteamGameServerAPIContext.GetSteamUGC(), handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00006030 File Offset: 0x00004230
		public static bool AddItemKeyValueTag(UGCUpdateHandle_t handle, string pchKey, string pchValue)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKey))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchValue))
				{
					result = NativeMethods.ISteamUGC_AddItemKeyValueTag(CSteamGameServerAPIContext.GetSteamUGC(), handle, utf8StringHandle, utf8StringHandle2);
				}
			}
			return result;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00006094 File Offset: 0x00004294
		public static bool AddItemPreviewFile(UGCUpdateHandle_t handle, string pszPreviewFile, EItemPreviewType type)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszPreviewFile))
			{
				result = NativeMethods.ISteamUGC_AddItemPreviewFile(CSteamGameServerAPIContext.GetSteamUGC(), handle, utf8StringHandle, type);
			}
			return result;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000060D8 File Offset: 0x000042D8
		public static bool AddItemPreviewVideo(UGCUpdateHandle_t handle, string pszVideoID)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszVideoID))
			{
				result = NativeMethods.ISteamUGC_AddItemPreviewVideo(CSteamGameServerAPIContext.GetSteamUGC(), handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000611C File Offset: 0x0000431C
		public static bool UpdateItemPreviewFile(UGCUpdateHandle_t handle, uint index, string pszPreviewFile)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszPreviewFile))
			{
				result = NativeMethods.ISteamUGC_UpdateItemPreviewFile(CSteamGameServerAPIContext.GetSteamUGC(), handle, index, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00006160 File Offset: 0x00004360
		public static bool UpdateItemPreviewVideo(UGCUpdateHandle_t handle, uint index, string pszVideoID)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszVideoID))
			{
				result = NativeMethods.ISteamUGC_UpdateItemPreviewVideo(CSteamGameServerAPIContext.GetSteamUGC(), handle, index, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000061A4 File Offset: 0x000043A4
		public static bool RemoveItemPreview(UGCUpdateHandle_t handle, uint index)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_RemoveItemPreview(CSteamGameServerAPIContext.GetSteamUGC(), handle, index);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000061B7 File Offset: 0x000043B7
		public static bool AddContentDescriptor(UGCUpdateHandle_t handle, EUGCContentDescriptorID descid)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_AddContentDescriptor(CSteamGameServerAPIContext.GetSteamUGC(), handle, descid);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000061CA File Offset: 0x000043CA
		public static bool RemoveContentDescriptor(UGCUpdateHandle_t handle, EUGCContentDescriptorID descid)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_RemoveContentDescriptor(CSteamGameServerAPIContext.GetSteamUGC(), handle, descid);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000061E0 File Offset: 0x000043E0
		public static SteamAPICall_t SubmitItemUpdate(UGCUpdateHandle_t handle, string pchChangeNote)
		{
			InteropHelp.TestIfAvailableGameServer();
			SteamAPICall_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchChangeNote))
			{
				result = (SteamAPICall_t)NativeMethods.ISteamUGC_SubmitItemUpdate(CSteamGameServerAPIContext.GetSteamUGC(), handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00006228 File Offset: 0x00004428
		public static EItemUpdateStatus GetItemUpdateProgress(UGCUpdateHandle_t handle, out ulong punBytesProcessed, out ulong punBytesTotal)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_GetItemUpdateProgress(CSteamGameServerAPIContext.GetSteamUGC(), handle, out punBytesProcessed, out punBytesTotal);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000623C File Offset: 0x0000443C
		public static SteamAPICall_t SetUserItemVote(PublishedFileId_t nPublishedFileID, bool bVoteUp)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_SetUserItemVote(CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID, bVoteUp);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00006254 File Offset: 0x00004454
		public static SteamAPICall_t GetUserItemVote(PublishedFileId_t nPublishedFileID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_GetUserItemVote(CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000626B File Offset: 0x0000446B
		public static SteamAPICall_t AddItemToFavorites(AppId_t nAppId, PublishedFileId_t nPublishedFileID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_AddItemToFavorites(CSteamGameServerAPIContext.GetSteamUGC(), nAppId, nPublishedFileID);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00006283 File Offset: 0x00004483
		public static SteamAPICall_t RemoveItemFromFavorites(AppId_t nAppId, PublishedFileId_t nPublishedFileID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_RemoveItemFromFavorites(CSteamGameServerAPIContext.GetSteamUGC(), nAppId, nPublishedFileID);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000629B File Offset: 0x0000449B
		public static SteamAPICall_t SubscribeItem(PublishedFileId_t nPublishedFileID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_SubscribeItem(CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000062B2 File Offset: 0x000044B2
		public static SteamAPICall_t UnsubscribeItem(PublishedFileId_t nPublishedFileID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_UnsubscribeItem(CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000062C9 File Offset: 0x000044C9
		public static uint GetNumSubscribedItems()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_GetNumSubscribedItems(CSteamGameServerAPIContext.GetSteamUGC());
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000062DA File Offset: 0x000044DA
		public static uint GetSubscribedItems(PublishedFileId_t[] pvecPublishedFileID, uint cMaxEntries)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_GetSubscribedItems(CSteamGameServerAPIContext.GetSteamUGC(), pvecPublishedFileID, cMaxEntries);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000062ED File Offset: 0x000044ED
		public static uint GetItemState(PublishedFileId_t nPublishedFileID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_GetItemState(CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00006300 File Offset: 0x00004500
		public static bool GetItemInstallInfo(PublishedFileId_t nPublishedFileID, out ulong punSizeOnDisk, out string pchFolder, uint cchFolderSize, out uint punTimeStamp)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cchFolderSize);
			bool flag = NativeMethods.ISteamUGC_GetItemInstallInfo(CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID, out punSizeOnDisk, intPtr, cchFolderSize, out punTimeStamp);
			pchFolder = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000633F File Offset: 0x0000453F
		public static bool GetItemDownloadInfo(PublishedFileId_t nPublishedFileID, out ulong punBytesDownloaded, out ulong punBytesTotal)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_GetItemDownloadInfo(CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID, out punBytesDownloaded, out punBytesTotal);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00006353 File Offset: 0x00004553
		public static bool DownloadItem(PublishedFileId_t nPublishedFileID, bool bHighPriority)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_DownloadItem(CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID, bHighPriority);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006368 File Offset: 0x00004568
		public static bool BInitWorkshopForGameServer(DepotId_t unWorkshopDepotID, string pszFolder)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszFolder))
			{
				result = NativeMethods.ISteamUGC_BInitWorkshopForGameServer(CSteamGameServerAPIContext.GetSteamUGC(), unWorkshopDepotID, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000063AC File Offset: 0x000045AC
		public static void SuspendDownloads(bool bSuspend)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamUGC_SuspendDownloads(CSteamGameServerAPIContext.GetSteamUGC(), bSuspend);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000063BE File Offset: 0x000045BE
		public static SteamAPICall_t StartPlaytimeTracking(PublishedFileId_t[] pvecPublishedFileID, uint unNumPublishedFileIDs)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_StartPlaytimeTracking(CSteamGameServerAPIContext.GetSteamUGC(), pvecPublishedFileID, unNumPublishedFileIDs);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000063D6 File Offset: 0x000045D6
		public static SteamAPICall_t StopPlaytimeTracking(PublishedFileId_t[] pvecPublishedFileID, uint unNumPublishedFileIDs)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_StopPlaytimeTracking(CSteamGameServerAPIContext.GetSteamUGC(), pvecPublishedFileID, unNumPublishedFileIDs);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000063EE File Offset: 0x000045EE
		public static SteamAPICall_t StopPlaytimeTrackingForAllItems()
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_StopPlaytimeTrackingForAllItems(CSteamGameServerAPIContext.GetSteamUGC());
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00006404 File Offset: 0x00004604
		public static SteamAPICall_t AddDependency(PublishedFileId_t nParentPublishedFileID, PublishedFileId_t nChildPublishedFileID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_AddDependency(CSteamGameServerAPIContext.GetSteamUGC(), nParentPublishedFileID, nChildPublishedFileID);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000641C File Offset: 0x0000461C
		public static SteamAPICall_t RemoveDependency(PublishedFileId_t nParentPublishedFileID, PublishedFileId_t nChildPublishedFileID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_RemoveDependency(CSteamGameServerAPIContext.GetSteamUGC(), nParentPublishedFileID, nChildPublishedFileID);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006434 File Offset: 0x00004634
		public static SteamAPICall_t AddAppDependency(PublishedFileId_t nPublishedFileID, AppId_t nAppID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_AddAppDependency(CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID, nAppID);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000644C File Offset: 0x0000464C
		public static SteamAPICall_t RemoveAppDependency(PublishedFileId_t nPublishedFileID, AppId_t nAppID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_RemoveAppDependency(CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID, nAppID);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00006464 File Offset: 0x00004664
		public static SteamAPICall_t GetAppDependencies(PublishedFileId_t nPublishedFileID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_GetAppDependencies(CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000647B File Offset: 0x0000467B
		public static SteamAPICall_t DeleteItem(PublishedFileId_t nPublishedFileID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_DeleteItem(CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00006492 File Offset: 0x00004692
		public static bool ShowWorkshopEULA()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamUGC_ShowWorkshopEULA(CSteamGameServerAPIContext.GetSteamUGC());
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000064A3 File Offset: 0x000046A3
		public static SteamAPICall_t GetWorkshopEULAStatus()
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_GetWorkshopEULAStatus(CSteamGameServerAPIContext.GetSteamUGC());
		}
	}
}
