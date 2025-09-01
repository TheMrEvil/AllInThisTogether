using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x0200002C RID: 44
	internal class ISteamUGC : SteamInterface
	{
		// Token: 0x06000581 RID: 1409 RVA: 0x00008D4C File Offset: 0x00006F4C
		internal ISteamUGC(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x06000582 RID: 1410
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamUGC_v014();

		// Token: 0x06000583 RID: 1411 RVA: 0x00008D5E File Offset: 0x00006F5E
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamUGC.SteamAPI_SteamUGC_v014();
		}

		// Token: 0x06000584 RID: 1412
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamGameServerUGC_v014();

		// Token: 0x06000585 RID: 1413 RVA: 0x00008D65 File Offset: 0x00006F65
		public override IntPtr GetServerInterfacePointer()
		{
			return ISteamUGC.SteamAPI_SteamGameServerUGC_v014();
		}

		// Token: 0x06000586 RID: 1414
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_CreateQueryUserUGCRequest")]
		private static extern UGCQueryHandle_t _CreateQueryUserUGCRequest(IntPtr self, AccountID_t unAccountID, UserUGCList eListType, UgcType eMatchingUGCType, UserUGCListSortOrder eSortOrder, AppId nCreatorAppID, AppId nConsumerAppID, uint unPage);

		// Token: 0x06000587 RID: 1415 RVA: 0x00008D6C File Offset: 0x00006F6C
		internal UGCQueryHandle_t CreateQueryUserUGCRequest(AccountID_t unAccountID, UserUGCList eListType, UgcType eMatchingUGCType, UserUGCListSortOrder eSortOrder, AppId nCreatorAppID, AppId nConsumerAppID, uint unPage)
		{
			return ISteamUGC._CreateQueryUserUGCRequest(this.Self, unAccountID, eListType, eMatchingUGCType, eSortOrder, nCreatorAppID, nConsumerAppID, unPage);
		}

		// Token: 0x06000588 RID: 1416
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_CreateQueryAllUGCRequestPage")]
		private static extern UGCQueryHandle_t _CreateQueryAllUGCRequest(IntPtr self, UGCQuery eQueryType, UgcType eMatchingeMatchingUGCTypeFileType, AppId nCreatorAppID, AppId nConsumerAppID, uint unPage);

		// Token: 0x06000589 RID: 1417 RVA: 0x00008D98 File Offset: 0x00006F98
		internal UGCQueryHandle_t CreateQueryAllUGCRequest(UGCQuery eQueryType, UgcType eMatchingeMatchingUGCTypeFileType, AppId nCreatorAppID, AppId nConsumerAppID, uint unPage)
		{
			return ISteamUGC._CreateQueryAllUGCRequest(this.Self, eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID, nConsumerAppID, unPage);
		}

		// Token: 0x0600058A RID: 1418
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_CreateQueryAllUGCRequestCursor")]
		private static extern UGCQueryHandle_t _CreateQueryAllUGCRequest(IntPtr self, UGCQuery eQueryType, UgcType eMatchingeMatchingUGCTypeFileType, AppId nCreatorAppID, AppId nConsumerAppID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchCursor);

		// Token: 0x0600058B RID: 1419 RVA: 0x00008DC0 File Offset: 0x00006FC0
		internal UGCQueryHandle_t CreateQueryAllUGCRequest(UGCQuery eQueryType, UgcType eMatchingeMatchingUGCTypeFileType, AppId nCreatorAppID, AppId nConsumerAppID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchCursor)
		{
			return ISteamUGC._CreateQueryAllUGCRequest(this.Self, eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID, nConsumerAppID, pchCursor);
		}

		// Token: 0x0600058C RID: 1420
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_CreateQueryUGCDetailsRequest")]
		private static extern UGCQueryHandle_t _CreateQueryUGCDetailsRequest(IntPtr self, [In] [Out] PublishedFileId[] pvecPublishedFileID, uint unNumPublishedFileIDs);

		// Token: 0x0600058D RID: 1421 RVA: 0x00008DE8 File Offset: 0x00006FE8
		internal UGCQueryHandle_t CreateQueryUGCDetailsRequest([In] [Out] PublishedFileId[] pvecPublishedFileID, uint unNumPublishedFileIDs)
		{
			return ISteamUGC._CreateQueryUGCDetailsRequest(this.Self, pvecPublishedFileID, unNumPublishedFileIDs);
		}

		// Token: 0x0600058E RID: 1422
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SendQueryUGCRequest")]
		private static extern SteamAPICall_t _SendQueryUGCRequest(IntPtr self, UGCQueryHandle_t handle);

		// Token: 0x0600058F RID: 1423 RVA: 0x00008E0C File Offset: 0x0000700C
		internal CallResult<SteamUGCQueryCompleted_t> SendQueryUGCRequest(UGCQueryHandle_t handle)
		{
			SteamAPICall_t call = ISteamUGC._SendQueryUGCRequest(this.Self, handle);
			return new CallResult<SteamUGCQueryCompleted_t>(call, base.IsServer);
		}

		// Token: 0x06000590 RID: 1424
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCResult")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetQueryUGCResult(IntPtr self, UGCQueryHandle_t handle, uint index, ref SteamUGCDetails_t pDetails);

		// Token: 0x06000591 RID: 1425 RVA: 0x00008E38 File Offset: 0x00007038
		internal bool GetQueryUGCResult(UGCQueryHandle_t handle, uint index, ref SteamUGCDetails_t pDetails)
		{
			return ISteamUGC._GetQueryUGCResult(this.Self, handle, index, ref pDetails);
		}

		// Token: 0x06000592 RID: 1426
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCPreviewURL")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetQueryUGCPreviewURL(IntPtr self, UGCQueryHandle_t handle, uint index, IntPtr pchURL, uint cchURLSize);

		// Token: 0x06000593 RID: 1427 RVA: 0x00008E5C File Offset: 0x0000705C
		internal bool GetQueryUGCPreviewURL(UGCQueryHandle_t handle, uint index, out string pchURL)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			bool result = ISteamUGC._GetQueryUGCPreviewURL(this.Self, handle, index, intPtr, 32768U);
			pchURL = Helpers.MemoryToString(intPtr);
			return result;
		}

		// Token: 0x06000594 RID: 1428
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCMetadata")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetQueryUGCMetadata(IntPtr self, UGCQueryHandle_t handle, uint index, IntPtr pchMetadata, uint cchMetadatasize);

		// Token: 0x06000595 RID: 1429 RVA: 0x00008E94 File Offset: 0x00007094
		internal bool GetQueryUGCMetadata(UGCQueryHandle_t handle, uint index, out string pchMetadata)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			bool result = ISteamUGC._GetQueryUGCMetadata(this.Self, handle, index, intPtr, 32768U);
			pchMetadata = Helpers.MemoryToString(intPtr);
			return result;
		}

		// Token: 0x06000596 RID: 1430
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCChildren")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetQueryUGCChildren(IntPtr self, UGCQueryHandle_t handle, uint index, [In] [Out] PublishedFileId[] pvecPublishedFileID, uint cMaxEntries);

		// Token: 0x06000597 RID: 1431 RVA: 0x00008ECC File Offset: 0x000070CC
		internal bool GetQueryUGCChildren(UGCQueryHandle_t handle, uint index, [In] [Out] PublishedFileId[] pvecPublishedFileID, uint cMaxEntries)
		{
			return ISteamUGC._GetQueryUGCChildren(this.Self, handle, index, pvecPublishedFileID, cMaxEntries);
		}

		// Token: 0x06000598 RID: 1432
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCStatistic")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetQueryUGCStatistic(IntPtr self, UGCQueryHandle_t handle, uint index, ItemStatistic eStatType, ref ulong pStatValue);

		// Token: 0x06000599 RID: 1433 RVA: 0x00008EF0 File Offset: 0x000070F0
		internal bool GetQueryUGCStatistic(UGCQueryHandle_t handle, uint index, ItemStatistic eStatType, ref ulong pStatValue)
		{
			return ISteamUGC._GetQueryUGCStatistic(this.Self, handle, index, eStatType, ref pStatValue);
		}

		// Token: 0x0600059A RID: 1434
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCNumAdditionalPreviews")]
		private static extern uint _GetQueryUGCNumAdditionalPreviews(IntPtr self, UGCQueryHandle_t handle, uint index);

		// Token: 0x0600059B RID: 1435 RVA: 0x00008F14 File Offset: 0x00007114
		internal uint GetQueryUGCNumAdditionalPreviews(UGCQueryHandle_t handle, uint index)
		{
			return ISteamUGC._GetQueryUGCNumAdditionalPreviews(this.Self, handle, index);
		}

		// Token: 0x0600059C RID: 1436
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCAdditionalPreview")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetQueryUGCAdditionalPreview(IntPtr self, UGCQueryHandle_t handle, uint index, uint previewIndex, IntPtr pchURLOrVideoID, uint cchURLSize, IntPtr pchOriginalFileName, uint cchOriginalFileNameSize, ref ItemPreviewType pPreviewType);

		// Token: 0x0600059D RID: 1437 RVA: 0x00008F38 File Offset: 0x00007138
		internal bool GetQueryUGCAdditionalPreview(UGCQueryHandle_t handle, uint index, uint previewIndex, out string pchURLOrVideoID, out string pchOriginalFileName, ref ItemPreviewType pPreviewType)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			IntPtr intPtr2 = Helpers.TakeMemory();
			bool result = ISteamUGC._GetQueryUGCAdditionalPreview(this.Self, handle, index, previewIndex, intPtr, 32768U, intPtr2, 32768U, ref pPreviewType);
			pchURLOrVideoID = Helpers.MemoryToString(intPtr);
			pchOriginalFileName = Helpers.MemoryToString(intPtr2);
			return result;
		}

		// Token: 0x0600059E RID: 1438
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCNumKeyValueTags")]
		private static extern uint _GetQueryUGCNumKeyValueTags(IntPtr self, UGCQueryHandle_t handle, uint index);

		// Token: 0x0600059F RID: 1439 RVA: 0x00008F88 File Offset: 0x00007188
		internal uint GetQueryUGCNumKeyValueTags(UGCQueryHandle_t handle, uint index)
		{
			return ISteamUGC._GetQueryUGCNumKeyValueTags(this.Self, handle, index);
		}

		// Token: 0x060005A0 RID: 1440
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCKeyValueTag")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetQueryUGCKeyValueTag(IntPtr self, UGCQueryHandle_t handle, uint index, uint keyValueTagIndex, IntPtr pchKey, uint cchKeySize, IntPtr pchValue, uint cchValueSize);

		// Token: 0x060005A1 RID: 1441 RVA: 0x00008FAC File Offset: 0x000071AC
		internal bool GetQueryUGCKeyValueTag(UGCQueryHandle_t handle, uint index, uint keyValueTagIndex, out string pchKey, out string pchValue)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			IntPtr intPtr2 = Helpers.TakeMemory();
			bool result = ISteamUGC._GetQueryUGCKeyValueTag(this.Self, handle, index, keyValueTagIndex, intPtr, 32768U, intPtr2, 32768U);
			pchKey = Helpers.MemoryToString(intPtr);
			pchValue = Helpers.MemoryToString(intPtr2);
			return result;
		}

		// Token: 0x060005A2 RID: 1442
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryFirstUGCKeyValueTag")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetQueryUGCKeyValueTag(IntPtr self, UGCQueryHandle_t handle, uint index, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey, IntPtr pchValue, uint cchValueSize);

		// Token: 0x060005A3 RID: 1443 RVA: 0x00008FF8 File Offset: 0x000071F8
		internal bool GetQueryUGCKeyValueTag(UGCQueryHandle_t handle, uint index, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey, out string pchValue)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			bool result = ISteamUGC._GetQueryUGCKeyValueTag(this.Self, handle, index, pchKey, intPtr, 32768U);
			pchValue = Helpers.MemoryToString(intPtr);
			return result;
		}

		// Token: 0x060005A4 RID: 1444
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_ReleaseQueryUGCRequest")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _ReleaseQueryUGCRequest(IntPtr self, UGCQueryHandle_t handle);

		// Token: 0x060005A5 RID: 1445 RVA: 0x00009030 File Offset: 0x00007230
		internal bool ReleaseQueryUGCRequest(UGCQueryHandle_t handle)
		{
			return ISteamUGC._ReleaseQueryUGCRequest(this.Self, handle);
		}

		// Token: 0x060005A6 RID: 1446
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddRequiredTag")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _AddRequiredTag(IntPtr self, UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pTagName);

		// Token: 0x060005A7 RID: 1447 RVA: 0x00009050 File Offset: 0x00007250
		internal bool AddRequiredTag(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pTagName)
		{
			return ISteamUGC._AddRequiredTag(this.Self, handle, pTagName);
		}

		// Token: 0x060005A8 RID: 1448
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddRequiredTagGroup")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _AddRequiredTagGroup(IntPtr self, UGCQueryHandle_t handle, ref SteamParamStringArray_t pTagGroups);

		// Token: 0x060005A9 RID: 1449 RVA: 0x00009074 File Offset: 0x00007274
		internal bool AddRequiredTagGroup(UGCQueryHandle_t handle, ref SteamParamStringArray_t pTagGroups)
		{
			return ISteamUGC._AddRequiredTagGroup(this.Self, handle, ref pTagGroups);
		}

		// Token: 0x060005AA RID: 1450
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddExcludedTag")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _AddExcludedTag(IntPtr self, UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pTagName);

		// Token: 0x060005AB RID: 1451 RVA: 0x00009098 File Offset: 0x00007298
		internal bool AddExcludedTag(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pTagName)
		{
			return ISteamUGC._AddExcludedTag(this.Self, handle, pTagName);
		}

		// Token: 0x060005AC RID: 1452
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetReturnOnlyIDs")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetReturnOnlyIDs(IntPtr self, UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.U1)] bool bReturnOnlyIDs);

		// Token: 0x060005AD RID: 1453 RVA: 0x000090BC File Offset: 0x000072BC
		internal bool SetReturnOnlyIDs(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.U1)] bool bReturnOnlyIDs)
		{
			return ISteamUGC._SetReturnOnlyIDs(this.Self, handle, bReturnOnlyIDs);
		}

		// Token: 0x060005AE RID: 1454
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetReturnKeyValueTags")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetReturnKeyValueTags(IntPtr self, UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.U1)] bool bReturnKeyValueTags);

		// Token: 0x060005AF RID: 1455 RVA: 0x000090E0 File Offset: 0x000072E0
		internal bool SetReturnKeyValueTags(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.U1)] bool bReturnKeyValueTags)
		{
			return ISteamUGC._SetReturnKeyValueTags(this.Self, handle, bReturnKeyValueTags);
		}

		// Token: 0x060005B0 RID: 1456
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetReturnLongDescription")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetReturnLongDescription(IntPtr self, UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.U1)] bool bReturnLongDescription);

		// Token: 0x060005B1 RID: 1457 RVA: 0x00009104 File Offset: 0x00007304
		internal bool SetReturnLongDescription(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.U1)] bool bReturnLongDescription)
		{
			return ISteamUGC._SetReturnLongDescription(this.Self, handle, bReturnLongDescription);
		}

		// Token: 0x060005B2 RID: 1458
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetReturnMetadata")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetReturnMetadata(IntPtr self, UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.U1)] bool bReturnMetadata);

		// Token: 0x060005B3 RID: 1459 RVA: 0x00009128 File Offset: 0x00007328
		internal bool SetReturnMetadata(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.U1)] bool bReturnMetadata)
		{
			return ISteamUGC._SetReturnMetadata(this.Self, handle, bReturnMetadata);
		}

		// Token: 0x060005B4 RID: 1460
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetReturnChildren")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetReturnChildren(IntPtr self, UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.U1)] bool bReturnChildren);

		// Token: 0x060005B5 RID: 1461 RVA: 0x0000914C File Offset: 0x0000734C
		internal bool SetReturnChildren(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.U1)] bool bReturnChildren)
		{
			return ISteamUGC._SetReturnChildren(this.Self, handle, bReturnChildren);
		}

		// Token: 0x060005B6 RID: 1462
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetReturnAdditionalPreviews")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetReturnAdditionalPreviews(IntPtr self, UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.U1)] bool bReturnAdditionalPreviews);

		// Token: 0x060005B7 RID: 1463 RVA: 0x00009170 File Offset: 0x00007370
		internal bool SetReturnAdditionalPreviews(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.U1)] bool bReturnAdditionalPreviews)
		{
			return ISteamUGC._SetReturnAdditionalPreviews(this.Self, handle, bReturnAdditionalPreviews);
		}

		// Token: 0x060005B8 RID: 1464
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetReturnTotalOnly")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetReturnTotalOnly(IntPtr self, UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.U1)] bool bReturnTotalOnly);

		// Token: 0x060005B9 RID: 1465 RVA: 0x00009194 File Offset: 0x00007394
		internal bool SetReturnTotalOnly(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.U1)] bool bReturnTotalOnly)
		{
			return ISteamUGC._SetReturnTotalOnly(this.Self, handle, bReturnTotalOnly);
		}

		// Token: 0x060005BA RID: 1466
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetReturnPlaytimeStats")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetReturnPlaytimeStats(IntPtr self, UGCQueryHandle_t handle, uint unDays);

		// Token: 0x060005BB RID: 1467 RVA: 0x000091B8 File Offset: 0x000073B8
		internal bool SetReturnPlaytimeStats(UGCQueryHandle_t handle, uint unDays)
		{
			return ISteamUGC._SetReturnPlaytimeStats(this.Self, handle, unDays);
		}

		// Token: 0x060005BC RID: 1468
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetLanguage")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetLanguage(IntPtr self, UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchLanguage);

		// Token: 0x060005BD RID: 1469 RVA: 0x000091DC File Offset: 0x000073DC
		internal bool SetLanguage(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchLanguage)
		{
			return ISteamUGC._SetLanguage(this.Self, handle, pchLanguage);
		}

		// Token: 0x060005BE RID: 1470
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetAllowCachedResponse")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetAllowCachedResponse(IntPtr self, UGCQueryHandle_t handle, uint unMaxAgeSeconds);

		// Token: 0x060005BF RID: 1471 RVA: 0x00009200 File Offset: 0x00007400
		internal bool SetAllowCachedResponse(UGCQueryHandle_t handle, uint unMaxAgeSeconds)
		{
			return ISteamUGC._SetAllowCachedResponse(this.Self, handle, unMaxAgeSeconds);
		}

		// Token: 0x060005C0 RID: 1472
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetCloudFileNameFilter")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetCloudFileNameFilter(IntPtr self, UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pMatchCloudFileName);

		// Token: 0x060005C1 RID: 1473 RVA: 0x00009224 File Offset: 0x00007424
		internal bool SetCloudFileNameFilter(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pMatchCloudFileName)
		{
			return ISteamUGC._SetCloudFileNameFilter(this.Self, handle, pMatchCloudFileName);
		}

		// Token: 0x060005C2 RID: 1474
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetMatchAnyTag")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetMatchAnyTag(IntPtr self, UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.U1)] bool bMatchAnyTag);

		// Token: 0x060005C3 RID: 1475 RVA: 0x00009248 File Offset: 0x00007448
		internal bool SetMatchAnyTag(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.U1)] bool bMatchAnyTag)
		{
			return ISteamUGC._SetMatchAnyTag(this.Self, handle, bMatchAnyTag);
		}

		// Token: 0x060005C4 RID: 1476
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetSearchText")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetSearchText(IntPtr self, UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pSearchText);

		// Token: 0x060005C5 RID: 1477 RVA: 0x0000926C File Offset: 0x0000746C
		internal bool SetSearchText(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pSearchText)
		{
			return ISteamUGC._SetSearchText(this.Self, handle, pSearchText);
		}

		// Token: 0x060005C6 RID: 1478
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetRankedByTrendDays")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetRankedByTrendDays(IntPtr self, UGCQueryHandle_t handle, uint unDays);

		// Token: 0x060005C7 RID: 1479 RVA: 0x00009290 File Offset: 0x00007490
		internal bool SetRankedByTrendDays(UGCQueryHandle_t handle, uint unDays)
		{
			return ISteamUGC._SetRankedByTrendDays(this.Self, handle, unDays);
		}

		// Token: 0x060005C8 RID: 1480
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddRequiredKeyValueTag")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _AddRequiredKeyValueTag(IntPtr self, UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pKey, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pValue);

		// Token: 0x060005C9 RID: 1481 RVA: 0x000092B4 File Offset: 0x000074B4
		internal bool AddRequiredKeyValueTag(UGCQueryHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pKey, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pValue)
		{
			return ISteamUGC._AddRequiredKeyValueTag(this.Self, handle, pKey, pValue);
		}

		// Token: 0x060005CA RID: 1482
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_RequestUGCDetails")]
		private static extern SteamAPICall_t _RequestUGCDetails(IntPtr self, PublishedFileId nPublishedFileID, uint unMaxAgeSeconds);

		// Token: 0x060005CB RID: 1483 RVA: 0x000092D8 File Offset: 0x000074D8
		internal CallResult<SteamUGCRequestUGCDetailsResult_t> RequestUGCDetails(PublishedFileId nPublishedFileID, uint unMaxAgeSeconds)
		{
			SteamAPICall_t call = ISteamUGC._RequestUGCDetails(this.Self, nPublishedFileID, unMaxAgeSeconds);
			return new CallResult<SteamUGCRequestUGCDetailsResult_t>(call, base.IsServer);
		}

		// Token: 0x060005CC RID: 1484
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_CreateItem")]
		private static extern SteamAPICall_t _CreateItem(IntPtr self, AppId nConsumerAppId, WorkshopFileType eFileType);

		// Token: 0x060005CD RID: 1485 RVA: 0x00009304 File Offset: 0x00007504
		internal CallResult<CreateItemResult_t> CreateItem(AppId nConsumerAppId, WorkshopFileType eFileType)
		{
			SteamAPICall_t call = ISteamUGC._CreateItem(this.Self, nConsumerAppId, eFileType);
			return new CallResult<CreateItemResult_t>(call, base.IsServer);
		}

		// Token: 0x060005CE RID: 1486
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_StartItemUpdate")]
		private static extern UGCUpdateHandle_t _StartItemUpdate(IntPtr self, AppId nConsumerAppId, PublishedFileId nPublishedFileID);

		// Token: 0x060005CF RID: 1487 RVA: 0x00009330 File Offset: 0x00007530
		internal UGCUpdateHandle_t StartItemUpdate(AppId nConsumerAppId, PublishedFileId nPublishedFileID)
		{
			return ISteamUGC._StartItemUpdate(this.Self, nConsumerAppId, nPublishedFileID);
		}

		// Token: 0x060005D0 RID: 1488
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemTitle")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetItemTitle(IntPtr self, UGCUpdateHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchTitle);

		// Token: 0x060005D1 RID: 1489 RVA: 0x00009354 File Offset: 0x00007554
		internal bool SetItemTitle(UGCUpdateHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchTitle)
		{
			return ISteamUGC._SetItemTitle(this.Self, handle, pchTitle);
		}

		// Token: 0x060005D2 RID: 1490
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemDescription")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetItemDescription(IntPtr self, UGCUpdateHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchDescription);

		// Token: 0x060005D3 RID: 1491 RVA: 0x00009378 File Offset: 0x00007578
		internal bool SetItemDescription(UGCUpdateHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchDescription)
		{
			return ISteamUGC._SetItemDescription(this.Self, handle, pchDescription);
		}

		// Token: 0x060005D4 RID: 1492
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemUpdateLanguage")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetItemUpdateLanguage(IntPtr self, UGCUpdateHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchLanguage);

		// Token: 0x060005D5 RID: 1493 RVA: 0x0000939C File Offset: 0x0000759C
		internal bool SetItemUpdateLanguage(UGCUpdateHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchLanguage)
		{
			return ISteamUGC._SetItemUpdateLanguage(this.Self, handle, pchLanguage);
		}

		// Token: 0x060005D6 RID: 1494
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemMetadata")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetItemMetadata(IntPtr self, UGCUpdateHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchMetaData);

		// Token: 0x060005D7 RID: 1495 RVA: 0x000093C0 File Offset: 0x000075C0
		internal bool SetItemMetadata(UGCUpdateHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchMetaData)
		{
			return ISteamUGC._SetItemMetadata(this.Self, handle, pchMetaData);
		}

		// Token: 0x060005D8 RID: 1496
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemVisibility")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetItemVisibility(IntPtr self, UGCUpdateHandle_t handle, RemoteStoragePublishedFileVisibility eVisibility);

		// Token: 0x060005D9 RID: 1497 RVA: 0x000093E4 File Offset: 0x000075E4
		internal bool SetItemVisibility(UGCUpdateHandle_t handle, RemoteStoragePublishedFileVisibility eVisibility)
		{
			return ISteamUGC._SetItemVisibility(this.Self, handle, eVisibility);
		}

		// Token: 0x060005DA RID: 1498
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemTags")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetItemTags(IntPtr self, UGCUpdateHandle_t updateHandle, ref SteamParamStringArray_t pTags);

		// Token: 0x060005DB RID: 1499 RVA: 0x00009408 File Offset: 0x00007608
		internal bool SetItemTags(UGCUpdateHandle_t updateHandle, ref SteamParamStringArray_t pTags)
		{
			return ISteamUGC._SetItemTags(this.Self, updateHandle, ref pTags);
		}

		// Token: 0x060005DC RID: 1500
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemContent")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetItemContent(IntPtr self, UGCUpdateHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszContentFolder);

		// Token: 0x060005DD RID: 1501 RVA: 0x0000942C File Offset: 0x0000762C
		internal bool SetItemContent(UGCUpdateHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszContentFolder)
		{
			return ISteamUGC._SetItemContent(this.Self, handle, pszContentFolder);
		}

		// Token: 0x060005DE RID: 1502
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemPreview")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetItemPreview(IntPtr self, UGCUpdateHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszPreviewFile);

		// Token: 0x060005DF RID: 1503 RVA: 0x00009450 File Offset: 0x00007650
		internal bool SetItemPreview(UGCUpdateHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszPreviewFile)
		{
			return ISteamUGC._SetItemPreview(this.Self, handle, pszPreviewFile);
		}

		// Token: 0x060005E0 RID: 1504
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetAllowLegacyUpload")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetAllowLegacyUpload(IntPtr self, UGCUpdateHandle_t handle, [MarshalAs(UnmanagedType.U1)] bool bAllowLegacyUpload);

		// Token: 0x060005E1 RID: 1505 RVA: 0x00009474 File Offset: 0x00007674
		internal bool SetAllowLegacyUpload(UGCUpdateHandle_t handle, [MarshalAs(UnmanagedType.U1)] bool bAllowLegacyUpload)
		{
			return ISteamUGC._SetAllowLegacyUpload(this.Self, handle, bAllowLegacyUpload);
		}

		// Token: 0x060005E2 RID: 1506
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_RemoveAllItemKeyValueTags")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _RemoveAllItemKeyValueTags(IntPtr self, UGCUpdateHandle_t handle);

		// Token: 0x060005E3 RID: 1507 RVA: 0x00009498 File Offset: 0x00007698
		internal bool RemoveAllItemKeyValueTags(UGCUpdateHandle_t handle)
		{
			return ISteamUGC._RemoveAllItemKeyValueTags(this.Self, handle);
		}

		// Token: 0x060005E4 RID: 1508
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_RemoveItemKeyValueTags")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _RemoveItemKeyValueTags(IntPtr self, UGCUpdateHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey);

		// Token: 0x060005E5 RID: 1509 RVA: 0x000094B8 File Offset: 0x000076B8
		internal bool RemoveItemKeyValueTags(UGCUpdateHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey)
		{
			return ISteamUGC._RemoveItemKeyValueTags(this.Self, handle, pchKey);
		}

		// Token: 0x060005E6 RID: 1510
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddItemKeyValueTag")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _AddItemKeyValueTag(IntPtr self, UGCUpdateHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchValue);

		// Token: 0x060005E7 RID: 1511 RVA: 0x000094DC File Offset: 0x000076DC
		internal bool AddItemKeyValueTag(UGCUpdateHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchValue)
		{
			return ISteamUGC._AddItemKeyValueTag(this.Self, handle, pchKey, pchValue);
		}

		// Token: 0x060005E8 RID: 1512
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddItemPreviewFile")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _AddItemPreviewFile(IntPtr self, UGCUpdateHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszPreviewFile, ItemPreviewType type);

		// Token: 0x060005E9 RID: 1513 RVA: 0x00009500 File Offset: 0x00007700
		internal bool AddItemPreviewFile(UGCUpdateHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszPreviewFile, ItemPreviewType type)
		{
			return ISteamUGC._AddItemPreviewFile(this.Self, handle, pszPreviewFile, type);
		}

		// Token: 0x060005EA RID: 1514
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddItemPreviewVideo")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _AddItemPreviewVideo(IntPtr self, UGCUpdateHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszVideoID);

		// Token: 0x060005EB RID: 1515 RVA: 0x00009524 File Offset: 0x00007724
		internal bool AddItemPreviewVideo(UGCUpdateHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszVideoID)
		{
			return ISteamUGC._AddItemPreviewVideo(this.Self, handle, pszVideoID);
		}

		// Token: 0x060005EC RID: 1516
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_UpdateItemPreviewFile")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _UpdateItemPreviewFile(IntPtr self, UGCUpdateHandle_t handle, uint index, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszPreviewFile);

		// Token: 0x060005ED RID: 1517 RVA: 0x00009548 File Offset: 0x00007748
		internal bool UpdateItemPreviewFile(UGCUpdateHandle_t handle, uint index, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszPreviewFile)
		{
			return ISteamUGC._UpdateItemPreviewFile(this.Self, handle, index, pszPreviewFile);
		}

		// Token: 0x060005EE RID: 1518
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_UpdateItemPreviewVideo")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _UpdateItemPreviewVideo(IntPtr self, UGCUpdateHandle_t handle, uint index, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszVideoID);

		// Token: 0x060005EF RID: 1519 RVA: 0x0000956C File Offset: 0x0000776C
		internal bool UpdateItemPreviewVideo(UGCUpdateHandle_t handle, uint index, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszVideoID)
		{
			return ISteamUGC._UpdateItemPreviewVideo(this.Self, handle, index, pszVideoID);
		}

		// Token: 0x060005F0 RID: 1520
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_RemoveItemPreview")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _RemoveItemPreview(IntPtr self, UGCUpdateHandle_t handle, uint index);

		// Token: 0x060005F1 RID: 1521 RVA: 0x00009590 File Offset: 0x00007790
		internal bool RemoveItemPreview(UGCUpdateHandle_t handle, uint index)
		{
			return ISteamUGC._RemoveItemPreview(this.Self, handle, index);
		}

		// Token: 0x060005F2 RID: 1522
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SubmitItemUpdate")]
		private static extern SteamAPICall_t _SubmitItemUpdate(IntPtr self, UGCUpdateHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchChangeNote);

		// Token: 0x060005F3 RID: 1523 RVA: 0x000095B4 File Offset: 0x000077B4
		internal CallResult<SubmitItemUpdateResult_t> SubmitItemUpdate(UGCUpdateHandle_t handle, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchChangeNote)
		{
			SteamAPICall_t call = ISteamUGC._SubmitItemUpdate(this.Self, handle, pchChangeNote);
			return new CallResult<SubmitItemUpdateResult_t>(call, base.IsServer);
		}

		// Token: 0x060005F4 RID: 1524
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetItemUpdateProgress")]
		private static extern ItemUpdateStatus _GetItemUpdateProgress(IntPtr self, UGCUpdateHandle_t handle, ref ulong punBytesProcessed, ref ulong punBytesTotal);

		// Token: 0x060005F5 RID: 1525 RVA: 0x000095E0 File Offset: 0x000077E0
		internal ItemUpdateStatus GetItemUpdateProgress(UGCUpdateHandle_t handle, ref ulong punBytesProcessed, ref ulong punBytesTotal)
		{
			return ISteamUGC._GetItemUpdateProgress(this.Self, handle, ref punBytesProcessed, ref punBytesTotal);
		}

		// Token: 0x060005F6 RID: 1526
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetUserItemVote")]
		private static extern SteamAPICall_t _SetUserItemVote(IntPtr self, PublishedFileId nPublishedFileID, [MarshalAs(UnmanagedType.U1)] bool bVoteUp);

		// Token: 0x060005F7 RID: 1527 RVA: 0x00009604 File Offset: 0x00007804
		internal CallResult<SetUserItemVoteResult_t> SetUserItemVote(PublishedFileId nPublishedFileID, [MarshalAs(UnmanagedType.U1)] bool bVoteUp)
		{
			SteamAPICall_t call = ISteamUGC._SetUserItemVote(this.Self, nPublishedFileID, bVoteUp);
			return new CallResult<SetUserItemVoteResult_t>(call, base.IsServer);
		}

		// Token: 0x060005F8 RID: 1528
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetUserItemVote")]
		private static extern SteamAPICall_t _GetUserItemVote(IntPtr self, PublishedFileId nPublishedFileID);

		// Token: 0x060005F9 RID: 1529 RVA: 0x00009630 File Offset: 0x00007830
		internal CallResult<GetUserItemVoteResult_t> GetUserItemVote(PublishedFileId nPublishedFileID)
		{
			SteamAPICall_t call = ISteamUGC._GetUserItemVote(this.Self, nPublishedFileID);
			return new CallResult<GetUserItemVoteResult_t>(call, base.IsServer);
		}

		// Token: 0x060005FA RID: 1530
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddItemToFavorites")]
		private static extern SteamAPICall_t _AddItemToFavorites(IntPtr self, AppId nAppId, PublishedFileId nPublishedFileID);

		// Token: 0x060005FB RID: 1531 RVA: 0x0000965C File Offset: 0x0000785C
		internal CallResult<UserFavoriteItemsListChanged_t> AddItemToFavorites(AppId nAppId, PublishedFileId nPublishedFileID)
		{
			SteamAPICall_t call = ISteamUGC._AddItemToFavorites(this.Self, nAppId, nPublishedFileID);
			return new CallResult<UserFavoriteItemsListChanged_t>(call, base.IsServer);
		}

		// Token: 0x060005FC RID: 1532
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_RemoveItemFromFavorites")]
		private static extern SteamAPICall_t _RemoveItemFromFavorites(IntPtr self, AppId nAppId, PublishedFileId nPublishedFileID);

		// Token: 0x060005FD RID: 1533 RVA: 0x00009688 File Offset: 0x00007888
		internal CallResult<UserFavoriteItemsListChanged_t> RemoveItemFromFavorites(AppId nAppId, PublishedFileId nPublishedFileID)
		{
			SteamAPICall_t call = ISteamUGC._RemoveItemFromFavorites(this.Self, nAppId, nPublishedFileID);
			return new CallResult<UserFavoriteItemsListChanged_t>(call, base.IsServer);
		}

		// Token: 0x060005FE RID: 1534
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SubscribeItem")]
		private static extern SteamAPICall_t _SubscribeItem(IntPtr self, PublishedFileId nPublishedFileID);

		// Token: 0x060005FF RID: 1535 RVA: 0x000096B4 File Offset: 0x000078B4
		internal CallResult<RemoteStorageSubscribePublishedFileResult_t> SubscribeItem(PublishedFileId nPublishedFileID)
		{
			SteamAPICall_t call = ISteamUGC._SubscribeItem(this.Self, nPublishedFileID);
			return new CallResult<RemoteStorageSubscribePublishedFileResult_t>(call, base.IsServer);
		}

		// Token: 0x06000600 RID: 1536
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_UnsubscribeItem")]
		private static extern SteamAPICall_t _UnsubscribeItem(IntPtr self, PublishedFileId nPublishedFileID);

		// Token: 0x06000601 RID: 1537 RVA: 0x000096E0 File Offset: 0x000078E0
		internal CallResult<RemoteStorageUnsubscribePublishedFileResult_t> UnsubscribeItem(PublishedFileId nPublishedFileID)
		{
			SteamAPICall_t call = ISteamUGC._UnsubscribeItem(this.Self, nPublishedFileID);
			return new CallResult<RemoteStorageUnsubscribePublishedFileResult_t>(call, base.IsServer);
		}

		// Token: 0x06000602 RID: 1538
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetNumSubscribedItems")]
		private static extern uint _GetNumSubscribedItems(IntPtr self);

		// Token: 0x06000603 RID: 1539 RVA: 0x0000970C File Offset: 0x0000790C
		internal uint GetNumSubscribedItems()
		{
			return ISteamUGC._GetNumSubscribedItems(this.Self);
		}

		// Token: 0x06000604 RID: 1540
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetSubscribedItems")]
		private static extern uint _GetSubscribedItems(IntPtr self, [In] [Out] PublishedFileId[] pvecPublishedFileID, uint cMaxEntries);

		// Token: 0x06000605 RID: 1541 RVA: 0x0000972C File Offset: 0x0000792C
		internal uint GetSubscribedItems([In] [Out] PublishedFileId[] pvecPublishedFileID, uint cMaxEntries)
		{
			return ISteamUGC._GetSubscribedItems(this.Self, pvecPublishedFileID, cMaxEntries);
		}

		// Token: 0x06000606 RID: 1542
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetItemState")]
		private static extern uint _GetItemState(IntPtr self, PublishedFileId nPublishedFileID);

		// Token: 0x06000607 RID: 1543 RVA: 0x00009750 File Offset: 0x00007950
		internal uint GetItemState(PublishedFileId nPublishedFileID)
		{
			return ISteamUGC._GetItemState(this.Self, nPublishedFileID);
		}

		// Token: 0x06000608 RID: 1544
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetItemInstallInfo")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetItemInstallInfo(IntPtr self, PublishedFileId nPublishedFileID, ref ulong punSizeOnDisk, IntPtr pchFolder, uint cchFolderSize, ref uint punTimeStamp);

		// Token: 0x06000609 RID: 1545 RVA: 0x00009770 File Offset: 0x00007970
		internal bool GetItemInstallInfo(PublishedFileId nPublishedFileID, ref ulong punSizeOnDisk, out string pchFolder, ref uint punTimeStamp)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			bool result = ISteamUGC._GetItemInstallInfo(this.Self, nPublishedFileID, ref punSizeOnDisk, intPtr, 32768U, ref punTimeStamp);
			pchFolder = Helpers.MemoryToString(intPtr);
			return result;
		}

		// Token: 0x0600060A RID: 1546
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetItemDownloadInfo")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetItemDownloadInfo(IntPtr self, PublishedFileId nPublishedFileID, ref ulong punBytesDownloaded, ref ulong punBytesTotal);

		// Token: 0x0600060B RID: 1547 RVA: 0x000097A8 File Offset: 0x000079A8
		internal bool GetItemDownloadInfo(PublishedFileId nPublishedFileID, ref ulong punBytesDownloaded, ref ulong punBytesTotal)
		{
			return ISteamUGC._GetItemDownloadInfo(this.Self, nPublishedFileID, ref punBytesDownloaded, ref punBytesTotal);
		}

		// Token: 0x0600060C RID: 1548
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_DownloadItem")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _DownloadItem(IntPtr self, PublishedFileId nPublishedFileID, [MarshalAs(UnmanagedType.U1)] bool bHighPriority);

		// Token: 0x0600060D RID: 1549 RVA: 0x000097CC File Offset: 0x000079CC
		internal bool DownloadItem(PublishedFileId nPublishedFileID, [MarshalAs(UnmanagedType.U1)] bool bHighPriority)
		{
			return ISteamUGC._DownloadItem(this.Self, nPublishedFileID, bHighPriority);
		}

		// Token: 0x0600060E RID: 1550
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_BInitWorkshopForGameServer")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BInitWorkshopForGameServer(IntPtr self, DepotId_t unWorkshopDepotID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszFolder);

		// Token: 0x0600060F RID: 1551 RVA: 0x000097F0 File Offset: 0x000079F0
		internal bool BInitWorkshopForGameServer(DepotId_t unWorkshopDepotID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszFolder)
		{
			return ISteamUGC._BInitWorkshopForGameServer(this.Self, unWorkshopDepotID, pszFolder);
		}

		// Token: 0x06000610 RID: 1552
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SuspendDownloads")]
		private static extern void _SuspendDownloads(IntPtr self, [MarshalAs(UnmanagedType.U1)] bool bSuspend);

		// Token: 0x06000611 RID: 1553 RVA: 0x00009811 File Offset: 0x00007A11
		internal void SuspendDownloads([MarshalAs(UnmanagedType.U1)] bool bSuspend)
		{
			ISteamUGC._SuspendDownloads(this.Self, bSuspend);
		}

		// Token: 0x06000612 RID: 1554
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_StartPlaytimeTracking")]
		private static extern SteamAPICall_t _StartPlaytimeTracking(IntPtr self, [In] [Out] PublishedFileId[] pvecPublishedFileID, uint unNumPublishedFileIDs);

		// Token: 0x06000613 RID: 1555 RVA: 0x00009824 File Offset: 0x00007A24
		internal CallResult<StartPlaytimeTrackingResult_t> StartPlaytimeTracking([In] [Out] PublishedFileId[] pvecPublishedFileID, uint unNumPublishedFileIDs)
		{
			SteamAPICall_t call = ISteamUGC._StartPlaytimeTracking(this.Self, pvecPublishedFileID, unNumPublishedFileIDs);
			return new CallResult<StartPlaytimeTrackingResult_t>(call, base.IsServer);
		}

		// Token: 0x06000614 RID: 1556
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_StopPlaytimeTracking")]
		private static extern SteamAPICall_t _StopPlaytimeTracking(IntPtr self, [In] [Out] PublishedFileId[] pvecPublishedFileID, uint unNumPublishedFileIDs);

		// Token: 0x06000615 RID: 1557 RVA: 0x00009850 File Offset: 0x00007A50
		internal CallResult<StopPlaytimeTrackingResult_t> StopPlaytimeTracking([In] [Out] PublishedFileId[] pvecPublishedFileID, uint unNumPublishedFileIDs)
		{
			SteamAPICall_t call = ISteamUGC._StopPlaytimeTracking(this.Self, pvecPublishedFileID, unNumPublishedFileIDs);
			return new CallResult<StopPlaytimeTrackingResult_t>(call, base.IsServer);
		}

		// Token: 0x06000616 RID: 1558
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_StopPlaytimeTrackingForAllItems")]
		private static extern SteamAPICall_t _StopPlaytimeTrackingForAllItems(IntPtr self);

		// Token: 0x06000617 RID: 1559 RVA: 0x0000987C File Offset: 0x00007A7C
		internal CallResult<StopPlaytimeTrackingResult_t> StopPlaytimeTrackingForAllItems()
		{
			SteamAPICall_t call = ISteamUGC._StopPlaytimeTrackingForAllItems(this.Self);
			return new CallResult<StopPlaytimeTrackingResult_t>(call, base.IsServer);
		}

		// Token: 0x06000618 RID: 1560
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddDependency")]
		private static extern SteamAPICall_t _AddDependency(IntPtr self, PublishedFileId nParentPublishedFileID, PublishedFileId nChildPublishedFileID);

		// Token: 0x06000619 RID: 1561 RVA: 0x000098A8 File Offset: 0x00007AA8
		internal CallResult<AddUGCDependencyResult_t> AddDependency(PublishedFileId nParentPublishedFileID, PublishedFileId nChildPublishedFileID)
		{
			SteamAPICall_t call = ISteamUGC._AddDependency(this.Self, nParentPublishedFileID, nChildPublishedFileID);
			return new CallResult<AddUGCDependencyResult_t>(call, base.IsServer);
		}

		// Token: 0x0600061A RID: 1562
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_RemoveDependency")]
		private static extern SteamAPICall_t _RemoveDependency(IntPtr self, PublishedFileId nParentPublishedFileID, PublishedFileId nChildPublishedFileID);

		// Token: 0x0600061B RID: 1563 RVA: 0x000098D4 File Offset: 0x00007AD4
		internal CallResult<RemoveUGCDependencyResult_t> RemoveDependency(PublishedFileId nParentPublishedFileID, PublishedFileId nChildPublishedFileID)
		{
			SteamAPICall_t call = ISteamUGC._RemoveDependency(this.Self, nParentPublishedFileID, nChildPublishedFileID);
			return new CallResult<RemoveUGCDependencyResult_t>(call, base.IsServer);
		}

		// Token: 0x0600061C RID: 1564
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddAppDependency")]
		private static extern SteamAPICall_t _AddAppDependency(IntPtr self, PublishedFileId nPublishedFileID, AppId nAppID);

		// Token: 0x0600061D RID: 1565 RVA: 0x00009900 File Offset: 0x00007B00
		internal CallResult<AddAppDependencyResult_t> AddAppDependency(PublishedFileId nPublishedFileID, AppId nAppID)
		{
			SteamAPICall_t call = ISteamUGC._AddAppDependency(this.Self, nPublishedFileID, nAppID);
			return new CallResult<AddAppDependencyResult_t>(call, base.IsServer);
		}

		// Token: 0x0600061E RID: 1566
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_RemoveAppDependency")]
		private static extern SteamAPICall_t _RemoveAppDependency(IntPtr self, PublishedFileId nPublishedFileID, AppId nAppID);

		// Token: 0x0600061F RID: 1567 RVA: 0x0000992C File Offset: 0x00007B2C
		internal CallResult<RemoveAppDependencyResult_t> RemoveAppDependency(PublishedFileId nPublishedFileID, AppId nAppID)
		{
			SteamAPICall_t call = ISteamUGC._RemoveAppDependency(this.Self, nPublishedFileID, nAppID);
			return new CallResult<RemoveAppDependencyResult_t>(call, base.IsServer);
		}

		// Token: 0x06000620 RID: 1568
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetAppDependencies")]
		private static extern SteamAPICall_t _GetAppDependencies(IntPtr self, PublishedFileId nPublishedFileID);

		// Token: 0x06000621 RID: 1569 RVA: 0x00009958 File Offset: 0x00007B58
		internal CallResult<GetAppDependenciesResult_t> GetAppDependencies(PublishedFileId nPublishedFileID)
		{
			SteamAPICall_t call = ISteamUGC._GetAppDependencies(this.Self, nPublishedFileID);
			return new CallResult<GetAppDependenciesResult_t>(call, base.IsServer);
		}

		// Token: 0x06000622 RID: 1570
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_DeleteItem")]
		private static extern SteamAPICall_t _DeleteItem(IntPtr self, PublishedFileId nPublishedFileID);

		// Token: 0x06000623 RID: 1571 RVA: 0x00009984 File Offset: 0x00007B84
		internal CallResult<DeleteItemResult_t> DeleteItem(PublishedFileId nPublishedFileID)
		{
			SteamAPICall_t call = ISteamUGC._DeleteItem(this.Self, nPublishedFileID);
			return new CallResult<DeleteItemResult_t>(call, base.IsServer);
		}
	}
}
