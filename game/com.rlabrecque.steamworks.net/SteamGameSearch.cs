using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000016 RID: 22
	public static class SteamGameSearch
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x00008298 File Offset: 0x00006498
		public static EGameSearchErrorCode_t AddGameSearchParams(string pchKeyToFind, string pchValuesToFind)
		{
			InteropHelp.TestIfAvailableClient();
			EGameSearchErrorCode_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKeyToFind))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchValuesToFind))
				{
					result = NativeMethods.ISteamGameSearch_AddGameSearchParams(CSteamAPIContext.GetSteamGameSearch(), utf8StringHandle, utf8StringHandle2);
				}
			}
			return result;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x000082F8 File Offset: 0x000064F8
		public static EGameSearchErrorCode_t SearchForGameWithLobby(CSteamID steamIDLobby, int nPlayerMin, int nPlayerMax)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamGameSearch_SearchForGameWithLobby(CSteamAPIContext.GetSteamGameSearch(), steamIDLobby, nPlayerMin, nPlayerMax);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000830C File Offset: 0x0000650C
		public static EGameSearchErrorCode_t SearchForGameSolo(int nPlayerMin, int nPlayerMax)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamGameSearch_SearchForGameSolo(CSteamAPIContext.GetSteamGameSearch(), nPlayerMin, nPlayerMax);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000831F File Offset: 0x0000651F
		public static EGameSearchErrorCode_t AcceptGame()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamGameSearch_AcceptGame(CSteamAPIContext.GetSteamGameSearch());
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00008330 File Offset: 0x00006530
		public static EGameSearchErrorCode_t DeclineGame()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamGameSearch_DeclineGame(CSteamAPIContext.GetSteamGameSearch());
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00008344 File Offset: 0x00006544
		public static EGameSearchErrorCode_t RetrieveConnectionDetails(CSteamID steamIDHost, out string pchConnectionDetails, int cubConnectionDetails)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cubConnectionDetails);
			EGameSearchErrorCode_t egameSearchErrorCode_t = NativeMethods.ISteamGameSearch_RetrieveConnectionDetails(CSteamAPIContext.GetSteamGameSearch(), steamIDHost, intPtr, cubConnectionDetails);
			pchConnectionDetails = ((egameSearchErrorCode_t != (EGameSearchErrorCode_t)0) ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return egameSearchErrorCode_t;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00008380 File Offset: 0x00006580
		public static EGameSearchErrorCode_t EndGameSearch()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamGameSearch_EndGameSearch(CSteamAPIContext.GetSteamGameSearch());
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00008394 File Offset: 0x00006594
		public static EGameSearchErrorCode_t SetGameHostParams(string pchKey, string pchValue)
		{
			InteropHelp.TestIfAvailableClient();
			EGameSearchErrorCode_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKey))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchValue))
				{
					result = NativeMethods.ISteamGameSearch_SetGameHostParams(CSteamAPIContext.GetSteamGameSearch(), utf8StringHandle, utf8StringHandle2);
				}
			}
			return result;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000083F4 File Offset: 0x000065F4
		public static EGameSearchErrorCode_t SetConnectionDetails(string pchConnectionDetails, int cubConnectionDetails)
		{
			InteropHelp.TestIfAvailableClient();
			EGameSearchErrorCode_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchConnectionDetails))
			{
				result = NativeMethods.ISteamGameSearch_SetConnectionDetails(CSteamAPIContext.GetSteamGameSearch(), utf8StringHandle, cubConnectionDetails);
			}
			return result;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00008438 File Offset: 0x00006638
		public static EGameSearchErrorCode_t RequestPlayersForGame(int nPlayerMin, int nPlayerMax, int nMaxTeamSize)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamGameSearch_RequestPlayersForGame(CSteamAPIContext.GetSteamGameSearch(), nPlayerMin, nPlayerMax, nMaxTeamSize);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000844C File Offset: 0x0000664C
		public static EGameSearchErrorCode_t HostConfirmGameStart(ulong ullUniqueGameID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamGameSearch_HostConfirmGameStart(CSteamAPIContext.GetSteamGameSearch(), ullUniqueGameID);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000845E File Offset: 0x0000665E
		public static EGameSearchErrorCode_t CancelRequestPlayersForGame()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamGameSearch_CancelRequestPlayersForGame(CSteamAPIContext.GetSteamGameSearch());
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000846F File Offset: 0x0000666F
		public static EGameSearchErrorCode_t SubmitPlayerResult(ulong ullUniqueGameID, CSteamID steamIDPlayer, EPlayerResult_t EPlayerResult)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamGameSearch_SubmitPlayerResult(CSteamAPIContext.GetSteamGameSearch(), ullUniqueGameID, steamIDPlayer, EPlayerResult);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00008483 File Offset: 0x00006683
		public static EGameSearchErrorCode_t EndGame(ulong ullUniqueGameID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamGameSearch_EndGame(CSteamAPIContext.GetSteamGameSearch(), ullUniqueGameID);
		}
	}
}
