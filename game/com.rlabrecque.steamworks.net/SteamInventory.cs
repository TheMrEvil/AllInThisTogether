using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000013 RID: 19
	public static class SteamInventory
	{
		// Token: 0x06000279 RID: 633 RVA: 0x000075DE File Offset: 0x000057DE
		public static EResult GetResultStatus(SteamInventoryResult_t resultHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_GetResultStatus(CSteamAPIContext.GetSteamInventory(), resultHandle);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000075F0 File Offset: 0x000057F0
		public static bool GetResultItems(SteamInventoryResult_t resultHandle, SteamItemDetails_t[] pOutItemsArray, ref uint punOutItemsArraySize)
		{
			InteropHelp.TestIfAvailableClient();
			if (pOutItemsArray != null && (long)pOutItemsArray.Length != (long)((ulong)punOutItemsArraySize))
			{
				throw new ArgumentException("pOutItemsArray must be the same size as punOutItemsArraySize!");
			}
			return NativeMethods.ISteamInventory_GetResultItems(CSteamAPIContext.GetSteamInventory(), resultHandle, pOutItemsArray, ref punOutItemsArraySize);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000761C File Offset: 0x0000581C
		public static bool GetResultItemProperty(SteamInventoryResult_t resultHandle, uint unItemIndex, string pchPropertyName, out string pchValueBuffer, ref uint punValueBufferSizeOut)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal((int)punValueBufferSizeOut);
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchPropertyName))
			{
				bool flag = NativeMethods.ISteamInventory_GetResultItemProperty(CSteamAPIContext.GetSteamInventory(), resultHandle, unItemIndex, utf8StringHandle, intPtr, ref punValueBufferSizeOut);
				pchValueBuffer = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
				Marshal.FreeHGlobal(intPtr);
				result = flag;
			}
			return result;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00007684 File Offset: 0x00005884
		public static uint GetResultTimestamp(SteamInventoryResult_t resultHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_GetResultTimestamp(CSteamAPIContext.GetSteamInventory(), resultHandle);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00007696 File Offset: 0x00005896
		public static bool CheckResultSteamID(SteamInventoryResult_t resultHandle, CSteamID steamIDExpected)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_CheckResultSteamID(CSteamAPIContext.GetSteamInventory(), resultHandle, steamIDExpected);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000076A9 File Offset: 0x000058A9
		public static void DestroyResult(SteamInventoryResult_t resultHandle)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInventory_DestroyResult(CSteamAPIContext.GetSteamInventory(), resultHandle);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000076BB File Offset: 0x000058BB
		public static bool GetAllItems(out SteamInventoryResult_t pResultHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_GetAllItems(CSteamAPIContext.GetSteamInventory(), out pResultHandle);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000076CD File Offset: 0x000058CD
		public static bool GetItemsByID(out SteamInventoryResult_t pResultHandle, SteamItemInstanceID_t[] pInstanceIDs, uint unCountInstanceIDs)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_GetItemsByID(CSteamAPIContext.GetSteamInventory(), out pResultHandle, pInstanceIDs, unCountInstanceIDs);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000076E1 File Offset: 0x000058E1
		public static bool SerializeResult(SteamInventoryResult_t resultHandle, byte[] pOutBuffer, out uint punOutBufferSize)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_SerializeResult(CSteamAPIContext.GetSteamInventory(), resultHandle, pOutBuffer, out punOutBufferSize);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000076F5 File Offset: 0x000058F5
		public static bool DeserializeResult(out SteamInventoryResult_t pOutResultHandle, byte[] pBuffer, uint unBufferSize, bool bRESERVED_MUST_BE_FALSE = false)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_DeserializeResult(CSteamAPIContext.GetSteamInventory(), out pOutResultHandle, pBuffer, unBufferSize, bRESERVED_MUST_BE_FALSE);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000770A File Offset: 0x0000590A
		public static bool GenerateItems(out SteamInventoryResult_t pResultHandle, SteamItemDef_t[] pArrayItemDefs, uint[] punArrayQuantity, uint unArrayLength)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_GenerateItems(CSteamAPIContext.GetSteamInventory(), out pResultHandle, pArrayItemDefs, punArrayQuantity, unArrayLength);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000771F File Offset: 0x0000591F
		public static bool GrantPromoItems(out SteamInventoryResult_t pResultHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_GrantPromoItems(CSteamAPIContext.GetSteamInventory(), out pResultHandle);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00007731 File Offset: 0x00005931
		public static bool AddPromoItem(out SteamInventoryResult_t pResultHandle, SteamItemDef_t itemDef)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_AddPromoItem(CSteamAPIContext.GetSteamInventory(), out pResultHandle, itemDef);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00007744 File Offset: 0x00005944
		public static bool AddPromoItems(out SteamInventoryResult_t pResultHandle, SteamItemDef_t[] pArrayItemDefs, uint unArrayLength)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_AddPromoItems(CSteamAPIContext.GetSteamInventory(), out pResultHandle, pArrayItemDefs, unArrayLength);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00007758 File Offset: 0x00005958
		public static bool ConsumeItem(out SteamInventoryResult_t pResultHandle, SteamItemInstanceID_t itemConsume, uint unQuantity)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_ConsumeItem(CSteamAPIContext.GetSteamInventory(), out pResultHandle, itemConsume, unQuantity);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000776C File Offset: 0x0000596C
		public static bool ExchangeItems(out SteamInventoryResult_t pResultHandle, SteamItemDef_t[] pArrayGenerate, uint[] punArrayGenerateQuantity, uint unArrayGenerateLength, SteamItemInstanceID_t[] pArrayDestroy, uint[] punArrayDestroyQuantity, uint unArrayDestroyLength)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_ExchangeItems(CSteamAPIContext.GetSteamInventory(), out pResultHandle, pArrayGenerate, punArrayGenerateQuantity, unArrayGenerateLength, pArrayDestroy, punArrayDestroyQuantity, unArrayDestroyLength);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00007787 File Offset: 0x00005987
		public static bool TransferItemQuantity(out SteamInventoryResult_t pResultHandle, SteamItemInstanceID_t itemIdSource, uint unQuantity, SteamItemInstanceID_t itemIdDest)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_TransferItemQuantity(CSteamAPIContext.GetSteamInventory(), out pResultHandle, itemIdSource, unQuantity, itemIdDest);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000779C File Offset: 0x0000599C
		public static void SendItemDropHeartbeat()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInventory_SendItemDropHeartbeat(CSteamAPIContext.GetSteamInventory());
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000077AD File Offset: 0x000059AD
		public static bool TriggerItemDrop(out SteamInventoryResult_t pResultHandle, SteamItemDef_t dropListDefinition)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_TriggerItemDrop(CSteamAPIContext.GetSteamInventory(), out pResultHandle, dropListDefinition);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000077C0 File Offset: 0x000059C0
		public static bool TradeItems(out SteamInventoryResult_t pResultHandle, CSteamID steamIDTradePartner, SteamItemInstanceID_t[] pArrayGive, uint[] pArrayGiveQuantity, uint nArrayGiveLength, SteamItemInstanceID_t[] pArrayGet, uint[] pArrayGetQuantity, uint nArrayGetLength)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_TradeItems(CSteamAPIContext.GetSteamInventory(), out pResultHandle, steamIDTradePartner, pArrayGive, pArrayGiveQuantity, nArrayGiveLength, pArrayGet, pArrayGetQuantity, nArrayGetLength);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x000077E8 File Offset: 0x000059E8
		public static bool LoadItemDefinitions()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_LoadItemDefinitions(CSteamAPIContext.GetSteamInventory());
		}

		// Token: 0x0600028E RID: 654 RVA: 0x000077F9 File Offset: 0x000059F9
		public static bool GetItemDefinitionIDs(SteamItemDef_t[] pItemDefIDs, ref uint punItemDefIDsArraySize)
		{
			InteropHelp.TestIfAvailableClient();
			if (pItemDefIDs != null && (long)pItemDefIDs.Length != (long)((ulong)punItemDefIDsArraySize))
			{
				throw new ArgumentException("pItemDefIDs must be the same size as punItemDefIDsArraySize!");
			}
			return NativeMethods.ISteamInventory_GetItemDefinitionIDs(CSteamAPIContext.GetSteamInventory(), pItemDefIDs, ref punItemDefIDsArraySize);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00007824 File Offset: 0x00005A24
		public static bool GetItemDefinitionProperty(SteamItemDef_t iDefinition, string pchPropertyName, out string pchValueBuffer, ref uint punValueBufferSizeOut)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal((int)punValueBufferSizeOut);
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchPropertyName))
			{
				bool flag = NativeMethods.ISteamInventory_GetItemDefinitionProperty(CSteamAPIContext.GetSteamInventory(), iDefinition, utf8StringHandle, intPtr, ref punValueBufferSizeOut);
				pchValueBuffer = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
				Marshal.FreeHGlobal(intPtr);
				result = flag;
			}
			return result;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00007888 File Offset: 0x00005A88
		public static SteamAPICall_t RequestEligiblePromoItemDefinitionsIDs(CSteamID steamID)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamInventory_RequestEligiblePromoItemDefinitionsIDs(CSteamAPIContext.GetSteamInventory(), steamID);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000789F File Offset: 0x00005A9F
		public static bool GetEligiblePromoItemDefinitionIDs(CSteamID steamID, SteamItemDef_t[] pItemDefIDs, ref uint punItemDefIDsArraySize)
		{
			InteropHelp.TestIfAvailableClient();
			if (pItemDefIDs != null && (long)pItemDefIDs.Length != (long)((ulong)punItemDefIDsArraySize))
			{
				throw new ArgumentException("pItemDefIDs must be the same size as punItemDefIDsArraySize!");
			}
			return NativeMethods.ISteamInventory_GetEligiblePromoItemDefinitionIDs(CSteamAPIContext.GetSteamInventory(), steamID, pItemDefIDs, ref punItemDefIDsArraySize);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000078CA File Offset: 0x00005ACA
		public static SteamAPICall_t StartPurchase(SteamItemDef_t[] pArrayItemDefs, uint[] punArrayQuantity, uint unArrayLength)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamInventory_StartPurchase(CSteamAPIContext.GetSteamInventory(), pArrayItemDefs, punArrayQuantity, unArrayLength);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000078E3 File Offset: 0x00005AE3
		public static SteamAPICall_t RequestPrices()
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamInventory_RequestPrices(CSteamAPIContext.GetSteamInventory());
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000078F9 File Offset: 0x00005AF9
		public static uint GetNumItemsWithPrices()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_GetNumItemsWithPrices(CSteamAPIContext.GetSteamInventory());
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000790C File Offset: 0x00005B0C
		public static bool GetItemsWithPrices(SteamItemDef_t[] pArrayItemDefs, ulong[] pCurrentPrices, ulong[] pBasePrices, uint unArrayLength)
		{
			InteropHelp.TestIfAvailableClient();
			if (pArrayItemDefs != null && (long)pArrayItemDefs.Length != (long)((ulong)unArrayLength))
			{
				throw new ArgumentException("pArrayItemDefs must be the same size as unArrayLength!");
			}
			if (pCurrentPrices != null && (long)pCurrentPrices.Length != (long)((ulong)unArrayLength))
			{
				throw new ArgumentException("pCurrentPrices must be the same size as unArrayLength!");
			}
			if (pBasePrices != null && (long)pBasePrices.Length != (long)((ulong)unArrayLength))
			{
				throw new ArgumentException("pBasePrices must be the same size as unArrayLength!");
			}
			return NativeMethods.ISteamInventory_GetItemsWithPrices(CSteamAPIContext.GetSteamInventory(), pArrayItemDefs, pCurrentPrices, pBasePrices, unArrayLength);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000796E File Offset: 0x00005B6E
		public static bool GetItemPrice(SteamItemDef_t iDefinition, out ulong pCurrentPrice, out ulong pBasePrice)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_GetItemPrice(CSteamAPIContext.GetSteamInventory(), iDefinition, out pCurrentPrice, out pBasePrice);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00007982 File Offset: 0x00005B82
		public static SteamInventoryUpdateHandle_t StartUpdateProperties()
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamInventoryUpdateHandle_t)NativeMethods.ISteamInventory_StartUpdateProperties(CSteamAPIContext.GetSteamInventory());
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00007998 File Offset: 0x00005B98
		public static bool RemoveProperty(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string pchPropertyName)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchPropertyName))
			{
				result = NativeMethods.ISteamInventory_RemoveProperty(CSteamAPIContext.GetSteamInventory(), handle, nItemID, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000079DC File Offset: 0x00005BDC
		public static bool SetProperty(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string pchPropertyName, string pchPropertyValue)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchPropertyName))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchPropertyValue))
				{
					result = NativeMethods.ISteamInventory_SetPropertyString(CSteamAPIContext.GetSteamInventory(), handle, nItemID, utf8StringHandle, utf8StringHandle2);
				}
			}
			return result;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00007A40 File Offset: 0x00005C40
		public static bool SetProperty(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string pchPropertyName, bool bValue)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchPropertyName))
			{
				result = NativeMethods.ISteamInventory_SetPropertyBool(CSteamAPIContext.GetSteamInventory(), handle, nItemID, utf8StringHandle, bValue);
			}
			return result;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00007A88 File Offset: 0x00005C88
		public static bool SetProperty(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string pchPropertyName, long nValue)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchPropertyName))
			{
				result = NativeMethods.ISteamInventory_SetPropertyInt64(CSteamAPIContext.GetSteamInventory(), handle, nItemID, utf8StringHandle, nValue);
			}
			return result;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00007AD0 File Offset: 0x00005CD0
		public static bool SetProperty(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string pchPropertyName, float flValue)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchPropertyName))
			{
				result = NativeMethods.ISteamInventory_SetPropertyFloat(CSteamAPIContext.GetSteamInventory(), handle, nItemID, utf8StringHandle, flValue);
			}
			return result;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00007B18 File Offset: 0x00005D18
		public static bool SubmitUpdateProperties(SteamInventoryUpdateHandle_t handle, out SteamInventoryResult_t pResultHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_SubmitUpdateProperties(CSteamAPIContext.GetSteamInventory(), handle, out pResultHandle);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00007B2C File Offset: 0x00005D2C
		public static bool InspectItem(out SteamInventoryResult_t pResultHandle, string pchItemToken)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchItemToken))
			{
				result = NativeMethods.ISteamInventory_InspectItem(CSteamAPIContext.GetSteamInventory(), out pResultHandle, utf8StringHandle);
			}
			return result;
		}
	}
}
