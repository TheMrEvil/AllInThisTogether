using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000012 RID: 18
	internal class ISteamGameSearch : SteamInterface
	{
		// Token: 0x0600019B RID: 411 RVA: 0x000052F7 File Offset: 0x000034F7
		internal ISteamGameSearch(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x0600019C RID: 412
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamGameSearch_v001();

		// Token: 0x0600019D RID: 413 RVA: 0x00005309 File Offset: 0x00003509
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamGameSearch.SteamAPI_SteamGameSearch_v001();
		}

		// Token: 0x0600019E RID: 414
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameSearch_AddGameSearchParams")]
		private static extern GameSearchErrorCode_t _AddGameSearchParams(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKeyToFind, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchValuesToFind);

		// Token: 0x0600019F RID: 415 RVA: 0x00005310 File Offset: 0x00003510
		internal GameSearchErrorCode_t AddGameSearchParams([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKeyToFind, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchValuesToFind)
		{
			return ISteamGameSearch._AddGameSearchParams(this.Self, pchKeyToFind, pchValuesToFind);
		}

		// Token: 0x060001A0 RID: 416
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameSearch_SearchForGameWithLobby")]
		private static extern GameSearchErrorCode_t _SearchForGameWithLobby(IntPtr self, SteamId steamIDLobby, int nPlayerMin, int nPlayerMax);

		// Token: 0x060001A1 RID: 417 RVA: 0x00005334 File Offset: 0x00003534
		internal GameSearchErrorCode_t SearchForGameWithLobby(SteamId steamIDLobby, int nPlayerMin, int nPlayerMax)
		{
			return ISteamGameSearch._SearchForGameWithLobby(this.Self, steamIDLobby, nPlayerMin, nPlayerMax);
		}

		// Token: 0x060001A2 RID: 418
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameSearch_SearchForGameSolo")]
		private static extern GameSearchErrorCode_t _SearchForGameSolo(IntPtr self, int nPlayerMin, int nPlayerMax);

		// Token: 0x060001A3 RID: 419 RVA: 0x00005358 File Offset: 0x00003558
		internal GameSearchErrorCode_t SearchForGameSolo(int nPlayerMin, int nPlayerMax)
		{
			return ISteamGameSearch._SearchForGameSolo(this.Self, nPlayerMin, nPlayerMax);
		}

		// Token: 0x060001A4 RID: 420
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameSearch_AcceptGame")]
		private static extern GameSearchErrorCode_t _AcceptGame(IntPtr self);

		// Token: 0x060001A5 RID: 421 RVA: 0x0000537C File Offset: 0x0000357C
		internal GameSearchErrorCode_t AcceptGame()
		{
			return ISteamGameSearch._AcceptGame(this.Self);
		}

		// Token: 0x060001A6 RID: 422
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameSearch_DeclineGame")]
		private static extern GameSearchErrorCode_t _DeclineGame(IntPtr self);

		// Token: 0x060001A7 RID: 423 RVA: 0x0000539C File Offset: 0x0000359C
		internal GameSearchErrorCode_t DeclineGame()
		{
			return ISteamGameSearch._DeclineGame(this.Self);
		}

		// Token: 0x060001A8 RID: 424
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameSearch_RetrieveConnectionDetails")]
		private static extern GameSearchErrorCode_t _RetrieveConnectionDetails(IntPtr self, SteamId steamIDHost, IntPtr pchConnectionDetails, int cubConnectionDetails);

		// Token: 0x060001A9 RID: 425 RVA: 0x000053BC File Offset: 0x000035BC
		internal GameSearchErrorCode_t RetrieveConnectionDetails(SteamId steamIDHost, out string pchConnectionDetails)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			GameSearchErrorCode_t result = ISteamGameSearch._RetrieveConnectionDetails(this.Self, steamIDHost, intPtr, 32768);
			pchConnectionDetails = Helpers.MemoryToString(intPtr);
			return result;
		}

		// Token: 0x060001AA RID: 426
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameSearch_EndGameSearch")]
		private static extern GameSearchErrorCode_t _EndGameSearch(IntPtr self);

		// Token: 0x060001AB RID: 427 RVA: 0x000053F0 File Offset: 0x000035F0
		internal GameSearchErrorCode_t EndGameSearch()
		{
			return ISteamGameSearch._EndGameSearch(this.Self);
		}

		// Token: 0x060001AC RID: 428
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameSearch_SetGameHostParams")]
		private static extern GameSearchErrorCode_t _SetGameHostParams(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchValue);

		// Token: 0x060001AD RID: 429 RVA: 0x00005410 File Offset: 0x00003610
		internal GameSearchErrorCode_t SetGameHostParams([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchKey, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchValue)
		{
			return ISteamGameSearch._SetGameHostParams(this.Self, pchKey, pchValue);
		}

		// Token: 0x060001AE RID: 430
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameSearch_SetConnectionDetails")]
		private static extern GameSearchErrorCode_t _SetConnectionDetails(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchConnectionDetails, int cubConnectionDetails);

		// Token: 0x060001AF RID: 431 RVA: 0x00005434 File Offset: 0x00003634
		internal GameSearchErrorCode_t SetConnectionDetails([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pchConnectionDetails, int cubConnectionDetails)
		{
			return ISteamGameSearch._SetConnectionDetails(this.Self, pchConnectionDetails, cubConnectionDetails);
		}

		// Token: 0x060001B0 RID: 432
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameSearch_RequestPlayersForGame")]
		private static extern GameSearchErrorCode_t _RequestPlayersForGame(IntPtr self, int nPlayerMin, int nPlayerMax, int nMaxTeamSize);

		// Token: 0x060001B1 RID: 433 RVA: 0x00005458 File Offset: 0x00003658
		internal GameSearchErrorCode_t RequestPlayersForGame(int nPlayerMin, int nPlayerMax, int nMaxTeamSize)
		{
			return ISteamGameSearch._RequestPlayersForGame(this.Self, nPlayerMin, nPlayerMax, nMaxTeamSize);
		}

		// Token: 0x060001B2 RID: 434
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameSearch_HostConfirmGameStart")]
		private static extern GameSearchErrorCode_t _HostConfirmGameStart(IntPtr self, ulong ullUniqueGameID);

		// Token: 0x060001B3 RID: 435 RVA: 0x0000547C File Offset: 0x0000367C
		internal GameSearchErrorCode_t HostConfirmGameStart(ulong ullUniqueGameID)
		{
			return ISteamGameSearch._HostConfirmGameStart(this.Self, ullUniqueGameID);
		}

		// Token: 0x060001B4 RID: 436
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameSearch_CancelRequestPlayersForGame")]
		private static extern GameSearchErrorCode_t _CancelRequestPlayersForGame(IntPtr self);

		// Token: 0x060001B5 RID: 437 RVA: 0x0000549C File Offset: 0x0000369C
		internal GameSearchErrorCode_t CancelRequestPlayersForGame()
		{
			return ISteamGameSearch._CancelRequestPlayersForGame(this.Self);
		}

		// Token: 0x060001B6 RID: 438
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameSearch_SubmitPlayerResult")]
		private static extern GameSearchErrorCode_t _SubmitPlayerResult(IntPtr self, ulong ullUniqueGameID, SteamId steamIDPlayer, PlayerResult_t EPlayerResult);

		// Token: 0x060001B7 RID: 439 RVA: 0x000054BC File Offset: 0x000036BC
		internal GameSearchErrorCode_t SubmitPlayerResult(ulong ullUniqueGameID, SteamId steamIDPlayer, PlayerResult_t EPlayerResult)
		{
			return ISteamGameSearch._SubmitPlayerResult(this.Self, ullUniqueGameID, steamIDPlayer, EPlayerResult);
		}

		// Token: 0x060001B8 RID: 440
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameSearch_EndGame")]
		private static extern GameSearchErrorCode_t _EndGame(IntPtr self, ulong ullUniqueGameID);

		// Token: 0x060001B9 RID: 441 RVA: 0x000054E0 File Offset: 0x000036E0
		internal GameSearchErrorCode_t EndGame(ulong ullUniqueGameID)
		{
			return ISteamGameSearch._EndGame(this.Self, ullUniqueGameID);
		}
	}
}
