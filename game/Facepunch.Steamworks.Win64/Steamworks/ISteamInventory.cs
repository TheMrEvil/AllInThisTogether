using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000018 RID: 24
	internal class ISteamInventory : SteamInterface
	{
		// Token: 0x060002F7 RID: 759 RVA: 0x00006544 File Offset: 0x00004744
		internal ISteamInventory(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x060002F8 RID: 760
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamInventory_v003();

		// Token: 0x060002F9 RID: 761 RVA: 0x00006556 File Offset: 0x00004756
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamInventory.SteamAPI_SteamInventory_v003();
		}

		// Token: 0x060002FA RID: 762
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamGameServerInventory_v003();

		// Token: 0x060002FB RID: 763 RVA: 0x0000655D File Offset: 0x0000475D
		public override IntPtr GetServerInterfacePointer()
		{
			return ISteamInventory.SteamAPI_SteamGameServerInventory_v003();
		}

		// Token: 0x060002FC RID: 764
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetResultStatus")]
		private static extern Result _GetResultStatus(IntPtr self, SteamInventoryResult_t resultHandle);

		// Token: 0x060002FD RID: 765 RVA: 0x00006564 File Offset: 0x00004764
		internal Result GetResultStatus(SteamInventoryResult_t resultHandle)
		{
			return ISteamInventory._GetResultStatus(this.Self, resultHandle);
		}

		// Token: 0x060002FE RID: 766
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetResultItems")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetResultItems(IntPtr self, SteamInventoryResult_t resultHandle, [In] [Out] SteamItemDetails_t[] pOutItemsArray, ref uint punOutItemsArraySize);

		// Token: 0x060002FF RID: 767 RVA: 0x00006584 File Offset: 0x00004784
		internal bool GetResultItems(SteamInventoryResult_t resultHandle, [In] [Out] SteamItemDetails_t[] pOutItemsArray, ref uint punOutItemsArraySize)
		{
			return ISteamInventory._GetResultItems(this.Self, resultHandle, pOutItemsArray, ref punOutItemsArraySize);
		}

		// Token: 0x06000300 RID: 768
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetResultItemProperty")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetResultItemProperty(IntPtr self, SteamInventoryResult_t resultHandle, uint unItemIndex, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchPropertyName, IntPtr pchValueBuffer, ref uint punValueBufferSizeOut);

		// Token: 0x06000301 RID: 769 RVA: 0x000065A8 File Offset: 0x000047A8
		internal bool GetResultItemProperty(SteamInventoryResult_t resultHandle, uint unItemIndex, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchPropertyName, out string pchValueBuffer, ref uint punValueBufferSizeOut)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			bool result = ISteamInventory._GetResultItemProperty(this.Self, resultHandle, unItemIndex, pchPropertyName, intPtr, ref punValueBufferSizeOut);
			pchValueBuffer = Helpers.MemoryToString(intPtr);
			return result;
		}

		// Token: 0x06000302 RID: 770
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetResultTimestamp")]
		private static extern uint _GetResultTimestamp(IntPtr self, SteamInventoryResult_t resultHandle);

		// Token: 0x06000303 RID: 771 RVA: 0x000065DC File Offset: 0x000047DC
		internal uint GetResultTimestamp(SteamInventoryResult_t resultHandle)
		{
			return ISteamInventory._GetResultTimestamp(this.Self, resultHandle);
		}

		// Token: 0x06000304 RID: 772
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_CheckResultSteamID")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _CheckResultSteamID(IntPtr self, SteamInventoryResult_t resultHandle, SteamId steamIDExpected);

		// Token: 0x06000305 RID: 773 RVA: 0x000065FC File Offset: 0x000047FC
		internal bool CheckResultSteamID(SteamInventoryResult_t resultHandle, SteamId steamIDExpected)
		{
			return ISteamInventory._CheckResultSteamID(this.Self, resultHandle, steamIDExpected);
		}

		// Token: 0x06000306 RID: 774
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_DestroyResult")]
		private static extern void _DestroyResult(IntPtr self, SteamInventoryResult_t resultHandle);

		// Token: 0x06000307 RID: 775 RVA: 0x0000661D File Offset: 0x0000481D
		internal void DestroyResult(SteamInventoryResult_t resultHandle)
		{
			ISteamInventory._DestroyResult(this.Self, resultHandle);
		}

		// Token: 0x06000308 RID: 776
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetAllItems")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetAllItems(IntPtr self, ref SteamInventoryResult_t pResultHandle);

		// Token: 0x06000309 RID: 777 RVA: 0x00006630 File Offset: 0x00004830
		internal bool GetAllItems(ref SteamInventoryResult_t pResultHandle)
		{
			return ISteamInventory._GetAllItems(this.Self, ref pResultHandle);
		}

		// Token: 0x0600030A RID: 778
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetItemsByID")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetItemsByID(IntPtr self, ref SteamInventoryResult_t pResultHandle, ref InventoryItemId pInstanceIDs, uint unCountInstanceIDs);

		// Token: 0x0600030B RID: 779 RVA: 0x00006650 File Offset: 0x00004850
		internal bool GetItemsByID(ref SteamInventoryResult_t pResultHandle, ref InventoryItemId pInstanceIDs, uint unCountInstanceIDs)
		{
			return ISteamInventory._GetItemsByID(this.Self, ref pResultHandle, ref pInstanceIDs, unCountInstanceIDs);
		}

		// Token: 0x0600030C RID: 780
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_SerializeResult")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SerializeResult(IntPtr self, SteamInventoryResult_t resultHandle, IntPtr pOutBuffer, ref uint punOutBufferSize);

		// Token: 0x0600030D RID: 781 RVA: 0x00006674 File Offset: 0x00004874
		internal bool SerializeResult(SteamInventoryResult_t resultHandle, IntPtr pOutBuffer, ref uint punOutBufferSize)
		{
			return ISteamInventory._SerializeResult(this.Self, resultHandle, pOutBuffer, ref punOutBufferSize);
		}

		// Token: 0x0600030E RID: 782
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_DeserializeResult")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _DeserializeResult(IntPtr self, ref SteamInventoryResult_t pOutResultHandle, IntPtr pBuffer, uint unBufferSize, [MarshalAs(UnmanagedType.U1)] bool bRESERVED_MUST_BE_FALSE);

		// Token: 0x0600030F RID: 783 RVA: 0x00006698 File Offset: 0x00004898
		internal bool DeserializeResult(ref SteamInventoryResult_t pOutResultHandle, IntPtr pBuffer, uint unBufferSize, [MarshalAs(UnmanagedType.U1)] bool bRESERVED_MUST_BE_FALSE)
		{
			return ISteamInventory._DeserializeResult(this.Self, ref pOutResultHandle, pBuffer, unBufferSize, bRESERVED_MUST_BE_FALSE);
		}

		// Token: 0x06000310 RID: 784
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GenerateItems")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GenerateItems(IntPtr self, ref SteamInventoryResult_t pResultHandle, [In] [Out] InventoryDefId[] pArrayItemDefs, [In] [Out] uint[] punArrayQuantity, uint unArrayLength);

		// Token: 0x06000311 RID: 785 RVA: 0x000066BC File Offset: 0x000048BC
		internal bool GenerateItems(ref SteamInventoryResult_t pResultHandle, [In] [Out] InventoryDefId[] pArrayItemDefs, [In] [Out] uint[] punArrayQuantity, uint unArrayLength)
		{
			return ISteamInventory._GenerateItems(this.Self, ref pResultHandle, pArrayItemDefs, punArrayQuantity, unArrayLength);
		}

		// Token: 0x06000312 RID: 786
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GrantPromoItems")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GrantPromoItems(IntPtr self, ref SteamInventoryResult_t pResultHandle);

		// Token: 0x06000313 RID: 787 RVA: 0x000066E0 File Offset: 0x000048E0
		internal bool GrantPromoItems(ref SteamInventoryResult_t pResultHandle)
		{
			return ISteamInventory._GrantPromoItems(this.Self, ref pResultHandle);
		}

		// Token: 0x06000314 RID: 788
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_AddPromoItem")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _AddPromoItem(IntPtr self, ref SteamInventoryResult_t pResultHandle, InventoryDefId itemDef);

		// Token: 0x06000315 RID: 789 RVA: 0x00006700 File Offset: 0x00004900
		internal bool AddPromoItem(ref SteamInventoryResult_t pResultHandle, InventoryDefId itemDef)
		{
			return ISteamInventory._AddPromoItem(this.Self, ref pResultHandle, itemDef);
		}

		// Token: 0x06000316 RID: 790
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_AddPromoItems")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _AddPromoItems(IntPtr self, ref SteamInventoryResult_t pResultHandle, [In] [Out] InventoryDefId[] pArrayItemDefs, uint unArrayLength);

		// Token: 0x06000317 RID: 791 RVA: 0x00006724 File Offset: 0x00004924
		internal bool AddPromoItems(ref SteamInventoryResult_t pResultHandle, [In] [Out] InventoryDefId[] pArrayItemDefs, uint unArrayLength)
		{
			return ISteamInventory._AddPromoItems(this.Self, ref pResultHandle, pArrayItemDefs, unArrayLength);
		}

		// Token: 0x06000318 RID: 792
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_ConsumeItem")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _ConsumeItem(IntPtr self, ref SteamInventoryResult_t pResultHandle, InventoryItemId itemConsume, uint unQuantity);

		// Token: 0x06000319 RID: 793 RVA: 0x00006748 File Offset: 0x00004948
		internal bool ConsumeItem(ref SteamInventoryResult_t pResultHandle, InventoryItemId itemConsume, uint unQuantity)
		{
			return ISteamInventory._ConsumeItem(this.Self, ref pResultHandle, itemConsume, unQuantity);
		}

		// Token: 0x0600031A RID: 794
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_ExchangeItems")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _ExchangeItems(IntPtr self, ref SteamInventoryResult_t pResultHandle, [In] [Out] InventoryDefId[] pArrayGenerate, [In] [Out] uint[] punArrayGenerateQuantity, uint unArrayGenerateLength, [In] [Out] InventoryItemId[] pArrayDestroy, [In] [Out] uint[] punArrayDestroyQuantity, uint unArrayDestroyLength);

		// Token: 0x0600031B RID: 795 RVA: 0x0000676C File Offset: 0x0000496C
		internal bool ExchangeItems(ref SteamInventoryResult_t pResultHandle, [In] [Out] InventoryDefId[] pArrayGenerate, [In] [Out] uint[] punArrayGenerateQuantity, uint unArrayGenerateLength, [In] [Out] InventoryItemId[] pArrayDestroy, [In] [Out] uint[] punArrayDestroyQuantity, uint unArrayDestroyLength)
		{
			return ISteamInventory._ExchangeItems(this.Self, ref pResultHandle, pArrayGenerate, punArrayGenerateQuantity, unArrayGenerateLength, pArrayDestroy, punArrayDestroyQuantity, unArrayDestroyLength);
		}

		// Token: 0x0600031C RID: 796
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_TransferItemQuantity")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _TransferItemQuantity(IntPtr self, ref SteamInventoryResult_t pResultHandle, InventoryItemId itemIdSource, uint unQuantity, InventoryItemId itemIdDest);

		// Token: 0x0600031D RID: 797 RVA: 0x00006798 File Offset: 0x00004998
		internal bool TransferItemQuantity(ref SteamInventoryResult_t pResultHandle, InventoryItemId itemIdSource, uint unQuantity, InventoryItemId itemIdDest)
		{
			return ISteamInventory._TransferItemQuantity(this.Self, ref pResultHandle, itemIdSource, unQuantity, itemIdDest);
		}

		// Token: 0x0600031E RID: 798
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_SendItemDropHeartbeat")]
		private static extern void _SendItemDropHeartbeat(IntPtr self);

		// Token: 0x0600031F RID: 799 RVA: 0x000067BC File Offset: 0x000049BC
		internal void SendItemDropHeartbeat()
		{
			ISteamInventory._SendItemDropHeartbeat(this.Self);
		}

		// Token: 0x06000320 RID: 800
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_TriggerItemDrop")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _TriggerItemDrop(IntPtr self, ref SteamInventoryResult_t pResultHandle, InventoryDefId dropListDefinition);

		// Token: 0x06000321 RID: 801 RVA: 0x000067CC File Offset: 0x000049CC
		internal bool TriggerItemDrop(ref SteamInventoryResult_t pResultHandle, InventoryDefId dropListDefinition)
		{
			return ISteamInventory._TriggerItemDrop(this.Self, ref pResultHandle, dropListDefinition);
		}

		// Token: 0x06000322 RID: 802
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_TradeItems")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _TradeItems(IntPtr self, ref SteamInventoryResult_t pResultHandle, SteamId steamIDTradePartner, [In] [Out] InventoryItemId[] pArrayGive, [In] [Out] uint[] pArrayGiveQuantity, uint nArrayGiveLength, [In] [Out] InventoryItemId[] pArrayGet, [In] [Out] uint[] pArrayGetQuantity, uint nArrayGetLength);

		// Token: 0x06000323 RID: 803 RVA: 0x000067F0 File Offset: 0x000049F0
		internal bool TradeItems(ref SteamInventoryResult_t pResultHandle, SteamId steamIDTradePartner, [In] [Out] InventoryItemId[] pArrayGive, [In] [Out] uint[] pArrayGiveQuantity, uint nArrayGiveLength, [In] [Out] InventoryItemId[] pArrayGet, [In] [Out] uint[] pArrayGetQuantity, uint nArrayGetLength)
		{
			return ISteamInventory._TradeItems(this.Self, ref pResultHandle, steamIDTradePartner, pArrayGive, pArrayGiveQuantity, nArrayGiveLength, pArrayGet, pArrayGetQuantity, nArrayGetLength);
		}

		// Token: 0x06000324 RID: 804
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_LoadItemDefinitions")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _LoadItemDefinitions(IntPtr self);

		// Token: 0x06000325 RID: 805 RVA: 0x0000681C File Offset: 0x00004A1C
		internal bool LoadItemDefinitions()
		{
			return ISteamInventory._LoadItemDefinitions(this.Self);
		}

		// Token: 0x06000326 RID: 806
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetItemDefinitionIDs")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetItemDefinitionIDs(IntPtr self, [In] [Out] InventoryDefId[] pItemDefIDs, ref uint punItemDefIDsArraySize);

		// Token: 0x06000327 RID: 807 RVA: 0x0000683C File Offset: 0x00004A3C
		internal bool GetItemDefinitionIDs([In] [Out] InventoryDefId[] pItemDefIDs, ref uint punItemDefIDsArraySize)
		{
			return ISteamInventory._GetItemDefinitionIDs(this.Self, pItemDefIDs, ref punItemDefIDsArraySize);
		}

		// Token: 0x06000328 RID: 808
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetItemDefinitionProperty")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetItemDefinitionProperty(IntPtr self, InventoryDefId iDefinition, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchPropertyName, IntPtr pchValueBuffer, ref uint punValueBufferSizeOut);

		// Token: 0x06000329 RID: 809 RVA: 0x00006860 File Offset: 0x00004A60
		internal bool GetItemDefinitionProperty(InventoryDefId iDefinition, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchPropertyName, out string pchValueBuffer, ref uint punValueBufferSizeOut)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			bool result = ISteamInventory._GetItemDefinitionProperty(this.Self, iDefinition, pchPropertyName, intPtr, ref punValueBufferSizeOut);
			pchValueBuffer = Helpers.MemoryToString(intPtr);
			return result;
		}

		// Token: 0x0600032A RID: 810
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_RequestEligiblePromoItemDefinitionsIDs")]
		private static extern SteamAPICall_t _RequestEligiblePromoItemDefinitionsIDs(IntPtr self, SteamId steamID);

		// Token: 0x0600032B RID: 811 RVA: 0x00006894 File Offset: 0x00004A94
		internal CallResult<SteamInventoryEligiblePromoItemDefIDs_t> RequestEligiblePromoItemDefinitionsIDs(SteamId steamID)
		{
			SteamAPICall_t call = ISteamInventory._RequestEligiblePromoItemDefinitionsIDs(this.Self, steamID);
			return new CallResult<SteamInventoryEligiblePromoItemDefIDs_t>(call, base.IsServer);
		}

		// Token: 0x0600032C RID: 812
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetEligiblePromoItemDefinitionIDs")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetEligiblePromoItemDefinitionIDs(IntPtr self, SteamId steamID, [In] [Out] InventoryDefId[] pItemDefIDs, ref uint punItemDefIDsArraySize);

		// Token: 0x0600032D RID: 813 RVA: 0x000068C0 File Offset: 0x00004AC0
		internal bool GetEligiblePromoItemDefinitionIDs(SteamId steamID, [In] [Out] InventoryDefId[] pItemDefIDs, ref uint punItemDefIDsArraySize)
		{
			return ISteamInventory._GetEligiblePromoItemDefinitionIDs(this.Self, steamID, pItemDefIDs, ref punItemDefIDsArraySize);
		}

		// Token: 0x0600032E RID: 814
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_StartPurchase")]
		private static extern SteamAPICall_t _StartPurchase(IntPtr self, [In] [Out] InventoryDefId[] pArrayItemDefs, [In] [Out] uint[] punArrayQuantity, uint unArrayLength);

		// Token: 0x0600032F RID: 815 RVA: 0x000068E4 File Offset: 0x00004AE4
		internal CallResult<SteamInventoryStartPurchaseResult_t> StartPurchase([In] [Out] InventoryDefId[] pArrayItemDefs, [In] [Out] uint[] punArrayQuantity, uint unArrayLength)
		{
			SteamAPICall_t call = ISteamInventory._StartPurchase(this.Self, pArrayItemDefs, punArrayQuantity, unArrayLength);
			return new CallResult<SteamInventoryStartPurchaseResult_t>(call, base.IsServer);
		}

		// Token: 0x06000330 RID: 816
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_RequestPrices")]
		private static extern SteamAPICall_t _RequestPrices(IntPtr self);

		// Token: 0x06000331 RID: 817 RVA: 0x00006914 File Offset: 0x00004B14
		internal CallResult<SteamInventoryRequestPricesResult_t> RequestPrices()
		{
			SteamAPICall_t call = ISteamInventory._RequestPrices(this.Self);
			return new CallResult<SteamInventoryRequestPricesResult_t>(call, base.IsServer);
		}

		// Token: 0x06000332 RID: 818
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetNumItemsWithPrices")]
		private static extern uint _GetNumItemsWithPrices(IntPtr self);

		// Token: 0x06000333 RID: 819 RVA: 0x00006940 File Offset: 0x00004B40
		internal uint GetNumItemsWithPrices()
		{
			return ISteamInventory._GetNumItemsWithPrices(this.Self);
		}

		// Token: 0x06000334 RID: 820
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetItemsWithPrices")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetItemsWithPrices(IntPtr self, [In] [Out] InventoryDefId[] pArrayItemDefs, [In] [Out] ulong[] pCurrentPrices, [In] [Out] ulong[] pBasePrices, uint unArrayLength);

		// Token: 0x06000335 RID: 821 RVA: 0x00006960 File Offset: 0x00004B60
		internal bool GetItemsWithPrices([In] [Out] InventoryDefId[] pArrayItemDefs, [In] [Out] ulong[] pCurrentPrices, [In] [Out] ulong[] pBasePrices, uint unArrayLength)
		{
			return ISteamInventory._GetItemsWithPrices(this.Self, pArrayItemDefs, pCurrentPrices, pBasePrices, unArrayLength);
		}

		// Token: 0x06000336 RID: 822
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetItemPrice")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetItemPrice(IntPtr self, InventoryDefId iDefinition, ref ulong pCurrentPrice, ref ulong pBasePrice);

		// Token: 0x06000337 RID: 823 RVA: 0x00006984 File Offset: 0x00004B84
		internal bool GetItemPrice(InventoryDefId iDefinition, ref ulong pCurrentPrice, ref ulong pBasePrice)
		{
			return ISteamInventory._GetItemPrice(this.Self, iDefinition, ref pCurrentPrice, ref pBasePrice);
		}

		// Token: 0x06000338 RID: 824
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_StartUpdateProperties")]
		private static extern SteamInventoryUpdateHandle_t _StartUpdateProperties(IntPtr self);

		// Token: 0x06000339 RID: 825 RVA: 0x000069A8 File Offset: 0x00004BA8
		internal SteamInventoryUpdateHandle_t StartUpdateProperties()
		{
			return ISteamInventory._StartUpdateProperties(this.Self);
		}

		// Token: 0x0600033A RID: 826
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_RemoveProperty")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _RemoveProperty(IntPtr self, SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchPropertyName);

		// Token: 0x0600033B RID: 827 RVA: 0x000069C8 File Offset: 0x00004BC8
		internal bool RemoveProperty(SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchPropertyName)
		{
			return ISteamInventory._RemoveProperty(this.Self, handle, nItemID, pchPropertyName);
		}

		// Token: 0x0600033C RID: 828
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_SetPropertyString")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetProperty(IntPtr self, SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchPropertyName, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchPropertyValue);

		// Token: 0x0600033D RID: 829 RVA: 0x000069EC File Offset: 0x00004BEC
		internal bool SetProperty(SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchPropertyName, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchPropertyValue)
		{
			return ISteamInventory._SetProperty(this.Self, handle, nItemID, pchPropertyName, pchPropertyValue);
		}

		// Token: 0x0600033E RID: 830
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_SetPropertyBool")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetProperty(IntPtr self, SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchPropertyName, [MarshalAs(UnmanagedType.U1)] bool bValue);

		// Token: 0x0600033F RID: 831 RVA: 0x00006A10 File Offset: 0x00004C10
		internal bool SetProperty(SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchPropertyName, [MarshalAs(UnmanagedType.U1)] bool bValue)
		{
			return ISteamInventory._SetProperty(this.Self, handle, nItemID, pchPropertyName, bValue);
		}

		// Token: 0x06000340 RID: 832
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_SetPropertyInt64")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetProperty(IntPtr self, SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchPropertyName, long nValue);

		// Token: 0x06000341 RID: 833 RVA: 0x00006A34 File Offset: 0x00004C34
		internal bool SetProperty(SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchPropertyName, long nValue)
		{
			return ISteamInventory._SetProperty(this.Self, handle, nItemID, pchPropertyName, nValue);
		}

		// Token: 0x06000342 RID: 834
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_SetPropertyFloat")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetProperty(IntPtr self, SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchPropertyName, float flValue);

		// Token: 0x06000343 RID: 835 RVA: 0x00006A58 File Offset: 0x00004C58
		internal bool SetProperty(SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchPropertyName, float flValue)
		{
			return ISteamInventory._SetProperty(this.Self, handle, nItemID, pchPropertyName, flValue);
		}

		// Token: 0x06000344 RID: 836
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_SubmitUpdateProperties")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SubmitUpdateProperties(IntPtr self, SteamInventoryUpdateHandle_t handle, ref SteamInventoryResult_t pResultHandle);

		// Token: 0x06000345 RID: 837 RVA: 0x00006A7C File Offset: 0x00004C7C
		internal bool SubmitUpdateProperties(SteamInventoryUpdateHandle_t handle, ref SteamInventoryResult_t pResultHandle)
		{
			return ISteamInventory._SubmitUpdateProperties(this.Self, handle, ref pResultHandle);
		}
	}
}
