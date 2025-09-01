using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000009 RID: 9
	public static class SteamGameServerInventory
	{
		// Token: 0x060000FE RID: 254 RVA: 0x0000462D File Offset: 0x0000282D
		public static EResult GetResultStatus(SteamInventoryResult_t resultHandle)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_GetResultStatus(CSteamGameServerAPIContext.GetSteamInventory(), resultHandle);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000463F File Offset: 0x0000283F
		public static bool GetResultItems(SteamInventoryResult_t resultHandle, SteamItemDetails_t[] pOutItemsArray, ref uint punOutItemsArraySize)
		{
			InteropHelp.TestIfAvailableGameServer();
			if (pOutItemsArray != null && (long)pOutItemsArray.Length != (long)((ulong)punOutItemsArraySize))
			{
				throw new ArgumentException("pOutItemsArray must be the same size as punOutItemsArraySize!");
			}
			return NativeMethods.ISteamInventory_GetResultItems(CSteamGameServerAPIContext.GetSteamInventory(), resultHandle, pOutItemsArray, ref punOutItemsArraySize);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000466C File Offset: 0x0000286C
		public static bool GetResultItemProperty(SteamInventoryResult_t resultHandle, uint unItemIndex, string pchPropertyName, out string pchValueBuffer, ref uint punValueBufferSizeOut)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal((int)punValueBufferSizeOut);
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchPropertyName))
			{
				bool flag = NativeMethods.ISteamInventory_GetResultItemProperty(CSteamGameServerAPIContext.GetSteamInventory(), resultHandle, unItemIndex, utf8StringHandle, intPtr, ref punValueBufferSizeOut);
				pchValueBuffer = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
				Marshal.FreeHGlobal(intPtr);
				result = flag;
			}
			return result;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000046D4 File Offset: 0x000028D4
		public static uint GetResultTimestamp(SteamInventoryResult_t resultHandle)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_GetResultTimestamp(CSteamGameServerAPIContext.GetSteamInventory(), resultHandle);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000046E6 File Offset: 0x000028E6
		public static bool CheckResultSteamID(SteamInventoryResult_t resultHandle, CSteamID steamIDExpected)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_CheckResultSteamID(CSteamGameServerAPIContext.GetSteamInventory(), resultHandle, steamIDExpected);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000046F9 File Offset: 0x000028F9
		public static void DestroyResult(SteamInventoryResult_t resultHandle)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamInventory_DestroyResult(CSteamGameServerAPIContext.GetSteamInventory(), resultHandle);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000470B File Offset: 0x0000290B
		public static bool GetAllItems(out SteamInventoryResult_t pResultHandle)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_GetAllItems(CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000471D File Offset: 0x0000291D
		public static bool GetItemsByID(out SteamInventoryResult_t pResultHandle, SteamItemInstanceID_t[] pInstanceIDs, uint unCountInstanceIDs)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_GetItemsByID(CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, pInstanceIDs, unCountInstanceIDs);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004731 File Offset: 0x00002931
		public static bool SerializeResult(SteamInventoryResult_t resultHandle, byte[] pOutBuffer, out uint punOutBufferSize)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_SerializeResult(CSteamGameServerAPIContext.GetSteamInventory(), resultHandle, pOutBuffer, out punOutBufferSize);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004745 File Offset: 0x00002945
		public static bool DeserializeResult(out SteamInventoryResult_t pOutResultHandle, byte[] pBuffer, uint unBufferSize, bool bRESERVED_MUST_BE_FALSE = false)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_DeserializeResult(CSteamGameServerAPIContext.GetSteamInventory(), out pOutResultHandle, pBuffer, unBufferSize, bRESERVED_MUST_BE_FALSE);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000475A File Offset: 0x0000295A
		public static bool GenerateItems(out SteamInventoryResult_t pResultHandle, SteamItemDef_t[] pArrayItemDefs, uint[] punArrayQuantity, uint unArrayLength)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_GenerateItems(CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, pArrayItemDefs, punArrayQuantity, unArrayLength);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000476F File Offset: 0x0000296F
		public static bool GrantPromoItems(out SteamInventoryResult_t pResultHandle)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_GrantPromoItems(CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004781 File Offset: 0x00002981
		public static bool AddPromoItem(out SteamInventoryResult_t pResultHandle, SteamItemDef_t itemDef)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_AddPromoItem(CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, itemDef);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004794 File Offset: 0x00002994
		public static bool AddPromoItems(out SteamInventoryResult_t pResultHandle, SteamItemDef_t[] pArrayItemDefs, uint unArrayLength)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_AddPromoItems(CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, pArrayItemDefs, unArrayLength);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000047A8 File Offset: 0x000029A8
		public static bool ConsumeItem(out SteamInventoryResult_t pResultHandle, SteamItemInstanceID_t itemConsume, uint unQuantity)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_ConsumeItem(CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, itemConsume, unQuantity);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000047BC File Offset: 0x000029BC
		public static bool ExchangeItems(out SteamInventoryResult_t pResultHandle, SteamItemDef_t[] pArrayGenerate, uint[] punArrayGenerateQuantity, uint unArrayGenerateLength, SteamItemInstanceID_t[] pArrayDestroy, uint[] punArrayDestroyQuantity, uint unArrayDestroyLength)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_ExchangeItems(CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, pArrayGenerate, punArrayGenerateQuantity, unArrayGenerateLength, pArrayDestroy, punArrayDestroyQuantity, unArrayDestroyLength);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000047D7 File Offset: 0x000029D7
		public static bool TransferItemQuantity(out SteamInventoryResult_t pResultHandle, SteamItemInstanceID_t itemIdSource, uint unQuantity, SteamItemInstanceID_t itemIdDest)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_TransferItemQuantity(CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, itemIdSource, unQuantity, itemIdDest);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000047EC File Offset: 0x000029EC
		public static void SendItemDropHeartbeat()
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamInventory_SendItemDropHeartbeat(CSteamGameServerAPIContext.GetSteamInventory());
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000047FD File Offset: 0x000029FD
		public static bool TriggerItemDrop(out SteamInventoryResult_t pResultHandle, SteamItemDef_t dropListDefinition)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_TriggerItemDrop(CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, dropListDefinition);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004810 File Offset: 0x00002A10
		public static bool TradeItems(out SteamInventoryResult_t pResultHandle, CSteamID steamIDTradePartner, SteamItemInstanceID_t[] pArrayGive, uint[] pArrayGiveQuantity, uint nArrayGiveLength, SteamItemInstanceID_t[] pArrayGet, uint[] pArrayGetQuantity, uint nArrayGetLength)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_TradeItems(CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, steamIDTradePartner, pArrayGive, pArrayGiveQuantity, nArrayGiveLength, pArrayGet, pArrayGetQuantity, nArrayGetLength);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004838 File Offset: 0x00002A38
		public static bool LoadItemDefinitions()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_LoadItemDefinitions(CSteamGameServerAPIContext.GetSteamInventory());
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004849 File Offset: 0x00002A49
		public static bool GetItemDefinitionIDs(SteamItemDef_t[] pItemDefIDs, ref uint punItemDefIDsArraySize)
		{
			InteropHelp.TestIfAvailableGameServer();
			if (pItemDefIDs != null && (long)pItemDefIDs.Length != (long)((ulong)punItemDefIDsArraySize))
			{
				throw new ArgumentException("pItemDefIDs must be the same size as punItemDefIDsArraySize!");
			}
			return NativeMethods.ISteamInventory_GetItemDefinitionIDs(CSteamGameServerAPIContext.GetSteamInventory(), pItemDefIDs, ref punItemDefIDsArraySize);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004874 File Offset: 0x00002A74
		public static bool GetItemDefinitionProperty(SteamItemDef_t iDefinition, string pchPropertyName, out string pchValueBuffer, ref uint punValueBufferSizeOut)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal((int)punValueBufferSizeOut);
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchPropertyName))
			{
				bool flag = NativeMethods.ISteamInventory_GetItemDefinitionProperty(CSteamGameServerAPIContext.GetSteamInventory(), iDefinition, utf8StringHandle, intPtr, ref punValueBufferSizeOut);
				pchValueBuffer = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
				Marshal.FreeHGlobal(intPtr);
				result = flag;
			}
			return result;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000048D8 File Offset: 0x00002AD8
		public static SteamAPICall_t RequestEligiblePromoItemDefinitionsIDs(CSteamID steamID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamInventory_RequestEligiblePromoItemDefinitionsIDs(CSteamGameServerAPIContext.GetSteamInventory(), steamID);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000048EF File Offset: 0x00002AEF
		public static bool GetEligiblePromoItemDefinitionIDs(CSteamID steamID, SteamItemDef_t[] pItemDefIDs, ref uint punItemDefIDsArraySize)
		{
			InteropHelp.TestIfAvailableGameServer();
			if (pItemDefIDs != null && (long)pItemDefIDs.Length != (long)((ulong)punItemDefIDsArraySize))
			{
				throw new ArgumentException("pItemDefIDs must be the same size as punItemDefIDsArraySize!");
			}
			return NativeMethods.ISteamInventory_GetEligiblePromoItemDefinitionIDs(CSteamGameServerAPIContext.GetSteamInventory(), steamID, pItemDefIDs, ref punItemDefIDsArraySize);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000491A File Offset: 0x00002B1A
		public static SteamAPICall_t StartPurchase(SteamItemDef_t[] pArrayItemDefs, uint[] punArrayQuantity, uint unArrayLength)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamInventory_StartPurchase(CSteamGameServerAPIContext.GetSteamInventory(), pArrayItemDefs, punArrayQuantity, unArrayLength);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004933 File Offset: 0x00002B33
		public static SteamAPICall_t RequestPrices()
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamInventory_RequestPrices(CSteamGameServerAPIContext.GetSteamInventory());
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004949 File Offset: 0x00002B49
		public static uint GetNumItemsWithPrices()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_GetNumItemsWithPrices(CSteamGameServerAPIContext.GetSteamInventory());
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000495C File Offset: 0x00002B5C
		public static bool GetItemsWithPrices(SteamItemDef_t[] pArrayItemDefs, ulong[] pCurrentPrices, ulong[] pBasePrices, uint unArrayLength)
		{
			InteropHelp.TestIfAvailableGameServer();
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
			return NativeMethods.ISteamInventory_GetItemsWithPrices(CSteamGameServerAPIContext.GetSteamInventory(), pArrayItemDefs, pCurrentPrices, pBasePrices, unArrayLength);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000049BE File Offset: 0x00002BBE
		public static bool GetItemPrice(SteamItemDef_t iDefinition, out ulong pCurrentPrice, out ulong pBasePrice)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_GetItemPrice(CSteamGameServerAPIContext.GetSteamInventory(), iDefinition, out pCurrentPrice, out pBasePrice);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000049D2 File Offset: 0x00002BD2
		public static SteamInventoryUpdateHandle_t StartUpdateProperties()
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamInventoryUpdateHandle_t)NativeMethods.ISteamInventory_StartUpdateProperties(CSteamGameServerAPIContext.GetSteamInventory());
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000049E8 File Offset: 0x00002BE8
		public static bool RemoveProperty(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string pchPropertyName)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchPropertyName))
			{
				result = NativeMethods.ISteamInventory_RemoveProperty(CSteamGameServerAPIContext.GetSteamInventory(), handle, nItemID, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004A2C File Offset: 0x00002C2C
		public static bool SetProperty(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string pchPropertyName, string pchPropertyValue)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchPropertyName))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchPropertyValue))
				{
					result = NativeMethods.ISteamInventory_SetPropertyString(CSteamGameServerAPIContext.GetSteamInventory(), handle, nItemID, utf8StringHandle, utf8StringHandle2);
				}
			}
			return result;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004A90 File Offset: 0x00002C90
		public static bool SetProperty(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string pchPropertyName, bool bValue)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchPropertyName))
			{
				result = NativeMethods.ISteamInventory_SetPropertyBool(CSteamGameServerAPIContext.GetSteamInventory(), handle, nItemID, utf8StringHandle, bValue);
			}
			return result;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004AD8 File Offset: 0x00002CD8
		public static bool SetProperty(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string pchPropertyName, long nValue)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchPropertyName))
			{
				result = NativeMethods.ISteamInventory_SetPropertyInt64(CSteamGameServerAPIContext.GetSteamInventory(), handle, nItemID, utf8StringHandle, nValue);
			}
			return result;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004B20 File Offset: 0x00002D20
		public static bool SetProperty(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string pchPropertyName, float flValue)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchPropertyName))
			{
				result = NativeMethods.ISteamInventory_SetPropertyFloat(CSteamGameServerAPIContext.GetSteamInventory(), handle, nItemID, utf8StringHandle, flValue);
			}
			return result;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004B68 File Offset: 0x00002D68
		public static bool SubmitUpdateProperties(SteamInventoryUpdateHandle_t handle, out SteamInventoryResult_t pResultHandle)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_SubmitUpdateProperties(CSteamGameServerAPIContext.GetSteamInventory(), handle, out pResultHandle);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004B7C File Offset: 0x00002D7C
		public static bool InspectItem(out SteamInventoryResult_t pResultHandle, string pchItemToken)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchItemToken))
			{
				result = NativeMethods.ISteamInventory_InspectItem(CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, utf8StringHandle);
			}
			return result;
		}
	}
}
