using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000014 RID: 20
	public static class SteamMatchmaking
	{
		// Token: 0x0600029F RID: 671 RVA: 0x00007B70 File Offset: 0x00005D70
		public static int GetFavoriteGameCount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMatchmaking_GetFavoriteGameCount(CSteamAPIContext.GetSteamMatchmaking());
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00007B81 File Offset: 0x00005D81
		public static bool GetFavoriteGame(int iGame, out AppId_t pnAppID, out uint pnIP, out ushort pnConnPort, out ushort pnQueryPort, out uint punFlags, out uint pRTime32LastPlayedOnServer)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMatchmaking_GetFavoriteGame(CSteamAPIContext.GetSteamMatchmaking(), iGame, out pnAppID, out pnIP, out pnConnPort, out pnQueryPort, out punFlags, out pRTime32LastPlayedOnServer);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00007B9C File Offset: 0x00005D9C
		public static int AddFavoriteGame(AppId_t nAppID, uint nIP, ushort nConnPort, ushort nQueryPort, uint unFlags, uint rTime32LastPlayedOnServer)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMatchmaking_AddFavoriteGame(CSteamAPIContext.GetSteamMatchmaking(), nAppID, nIP, nConnPort, nQueryPort, unFlags, rTime32LastPlayedOnServer);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00007BB5 File Offset: 0x00005DB5
		public static bool RemoveFavoriteGame(AppId_t nAppID, uint nIP, ushort nConnPort, ushort nQueryPort, uint unFlags)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMatchmaking_RemoveFavoriteGame(CSteamAPIContext.GetSteamMatchmaking(), nAppID, nIP, nConnPort, nQueryPort, unFlags);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00007BCC File Offset: 0x00005DCC
		public static SteamAPICall_t RequestLobbyList()
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamMatchmaking_RequestLobbyList(CSteamAPIContext.GetSteamMatchmaking());
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00007BE4 File Offset: 0x00005DE4
		public static void AddRequestLobbyListStringFilter(string pchKeyToMatch, string pchValueToMatch, ELobbyComparison eComparisonType)
		{
			InteropHelp.TestIfAvailableClient();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKeyToMatch))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchValueToMatch))
				{
					NativeMethods.ISteamMatchmaking_AddRequestLobbyListStringFilter(CSteamAPIContext.GetSteamMatchmaking(), utf8StringHandle, utf8StringHandle2, eComparisonType);
				}
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00007C44 File Offset: 0x00005E44
		public static void AddRequestLobbyListNumericalFilter(string pchKeyToMatch, int nValueToMatch, ELobbyComparison eComparisonType)
		{
			InteropHelp.TestIfAvailableClient();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKeyToMatch))
			{
				NativeMethods.ISteamMatchmaking_AddRequestLobbyListNumericalFilter(CSteamAPIContext.GetSteamMatchmaking(), utf8StringHandle, nValueToMatch, eComparisonType);
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00007C88 File Offset: 0x00005E88
		public static void AddRequestLobbyListNearValueFilter(string pchKeyToMatch, int nValueToBeCloseTo)
		{
			InteropHelp.TestIfAvailableClient();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKeyToMatch))
			{
				NativeMethods.ISteamMatchmaking_AddRequestLobbyListNearValueFilter(CSteamAPIContext.GetSteamMatchmaking(), utf8StringHandle, nValueToBeCloseTo);
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00007CCC File Offset: 0x00005ECC
		public static void AddRequestLobbyListFilterSlotsAvailable(int nSlotsAvailable)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMatchmaking_AddRequestLobbyListFilterSlotsAvailable(CSteamAPIContext.GetSteamMatchmaking(), nSlotsAvailable);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00007CDE File Offset: 0x00005EDE
		public static void AddRequestLobbyListDistanceFilter(ELobbyDistanceFilter eLobbyDistanceFilter)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMatchmaking_AddRequestLobbyListDistanceFilter(CSteamAPIContext.GetSteamMatchmaking(), eLobbyDistanceFilter);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00007CF0 File Offset: 0x00005EF0
		public static void AddRequestLobbyListResultCountFilter(int cMaxResults)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMatchmaking_AddRequestLobbyListResultCountFilter(CSteamAPIContext.GetSteamMatchmaking(), cMaxResults);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00007D02 File Offset: 0x00005F02
		public static void AddRequestLobbyListCompatibleMembersFilter(CSteamID steamIDLobby)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMatchmaking_AddRequestLobbyListCompatibleMembersFilter(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00007D14 File Offset: 0x00005F14
		public static CSteamID GetLobbyByIndex(int iLobby)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamMatchmaking_GetLobbyByIndex(CSteamAPIContext.GetSteamMatchmaking(), iLobby);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00007D2B File Offset: 0x00005F2B
		public static SteamAPICall_t CreateLobby(ELobbyType eLobbyType, int cMaxMembers)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamMatchmaking_CreateLobby(CSteamAPIContext.GetSteamMatchmaking(), eLobbyType, cMaxMembers);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00007D43 File Offset: 0x00005F43
		public static SteamAPICall_t JoinLobby(CSteamID steamIDLobby)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamMatchmaking_JoinLobby(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00007D5A File Offset: 0x00005F5A
		public static void LeaveLobby(CSteamID steamIDLobby)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMatchmaking_LeaveLobby(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00007D6C File Offset: 0x00005F6C
		public static bool InviteUserToLobby(CSteamID steamIDLobby, CSteamID steamIDInvitee)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMatchmaking_InviteUserToLobby(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, steamIDInvitee);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00007D7F File Offset: 0x00005F7F
		public static int GetNumLobbyMembers(CSteamID steamIDLobby)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMatchmaking_GetNumLobbyMembers(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00007D91 File Offset: 0x00005F91
		public static CSteamID GetLobbyMemberByIndex(CSteamID steamIDLobby, int iMember)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamMatchmaking_GetLobbyMemberByIndex(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, iMember);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00007DAC File Offset: 0x00005FAC
		public static string GetLobbyData(CSteamID steamIDLobby, string pchKey)
		{
			InteropHelp.TestIfAvailableClient();
			string result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKey))
			{
				result = InteropHelp.PtrToStringUTF8(NativeMethods.ISteamMatchmaking_GetLobbyData(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, utf8StringHandle));
			}
			return result;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00007DF4 File Offset: 0x00005FF4
		public static bool SetLobbyData(CSteamID steamIDLobby, string pchKey, string pchValue)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKey))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchValue))
				{
					result = NativeMethods.ISteamMatchmaking_SetLobbyData(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, utf8StringHandle, utf8StringHandle2);
				}
			}
			return result;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00007E58 File Offset: 0x00006058
		public static int GetLobbyDataCount(CSteamID steamIDLobby)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMatchmaking_GetLobbyDataCount(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00007E6C File Offset: 0x0000606C
		public static bool GetLobbyDataByIndex(CSteamID steamIDLobby, int iLobbyData, out string pchKey, int cchKeyBufferSize, out string pchValue, int cchValueBufferSize)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cchKeyBufferSize);
			IntPtr intPtr2 = Marshal.AllocHGlobal(cchValueBufferSize);
			bool flag = NativeMethods.ISteamMatchmaking_GetLobbyDataByIndex(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, iLobbyData, intPtr, cchKeyBufferSize, intPtr2, cchValueBufferSize);
			pchKey = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			pchValue = (flag ? InteropHelp.PtrToStringUTF8(intPtr2) : null);
			Marshal.FreeHGlobal(intPtr2);
			return flag;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00007ECC File Offset: 0x000060CC
		public static bool DeleteLobbyData(CSteamID steamIDLobby, string pchKey)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKey))
			{
				result = NativeMethods.ISteamMatchmaking_DeleteLobbyData(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00007F10 File Offset: 0x00006110
		public static string GetLobbyMemberData(CSteamID steamIDLobby, CSteamID steamIDUser, string pchKey)
		{
			InteropHelp.TestIfAvailableClient();
			string result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKey))
			{
				result = InteropHelp.PtrToStringUTF8(NativeMethods.ISteamMatchmaking_GetLobbyMemberData(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, steamIDUser, utf8StringHandle));
			}
			return result;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00007F5C File Offset: 0x0000615C
		public static void SetLobbyMemberData(CSteamID steamIDLobby, string pchKey, string pchValue)
		{
			InteropHelp.TestIfAvailableClient();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKey))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchValue))
				{
					NativeMethods.ISteamMatchmaking_SetLobbyMemberData(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, utf8StringHandle, utf8StringHandle2);
				}
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00007FBC File Offset: 0x000061BC
		public static bool SendLobbyChatMsg(CSteamID steamIDLobby, byte[] pvMsgBody, int cubMsgBody)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMatchmaking_SendLobbyChatMsg(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, pvMsgBody, cubMsgBody);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00007FD0 File Offset: 0x000061D0
		public static int GetLobbyChatEntry(CSteamID steamIDLobby, int iChatID, out CSteamID pSteamIDUser, byte[] pvData, int cubData, out EChatEntryType peChatEntryType)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMatchmaking_GetLobbyChatEntry(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, iChatID, out pSteamIDUser, pvData, cubData, out peChatEntryType);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00007FE9 File Offset: 0x000061E9
		public static bool RequestLobbyData(CSteamID steamIDLobby)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMatchmaking_RequestLobbyData(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00007FFB File Offset: 0x000061FB
		public static void SetLobbyGameServer(CSteamID steamIDLobby, uint unGameServerIP, ushort unGameServerPort, CSteamID steamIDGameServer)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMatchmaking_SetLobbyGameServer(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, unGameServerIP, unGameServerPort, steamIDGameServer);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00008010 File Offset: 0x00006210
		public static bool GetLobbyGameServer(CSteamID steamIDLobby, out uint punGameServerIP, out ushort punGameServerPort, out CSteamID psteamIDGameServer)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMatchmaking_GetLobbyGameServer(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, out punGameServerIP, out punGameServerPort, out psteamIDGameServer);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00008025 File Offset: 0x00006225
		public static bool SetLobbyMemberLimit(CSteamID steamIDLobby, int cMaxMembers)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMatchmaking_SetLobbyMemberLimit(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, cMaxMembers);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00008038 File Offset: 0x00006238
		public static int GetLobbyMemberLimit(CSteamID steamIDLobby)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMatchmaking_GetLobbyMemberLimit(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000804A File Offset: 0x0000624A
		public static bool SetLobbyType(CSteamID steamIDLobby, ELobbyType eLobbyType)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMatchmaking_SetLobbyType(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, eLobbyType);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000805D File Offset: 0x0000625D
		public static bool SetLobbyJoinable(CSteamID steamIDLobby, bool bLobbyJoinable)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMatchmaking_SetLobbyJoinable(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, bLobbyJoinable);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00008070 File Offset: 0x00006270
		public static CSteamID GetLobbyOwner(CSteamID steamIDLobby)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamMatchmaking_GetLobbyOwner(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00008087 File Offset: 0x00006287
		public static bool SetLobbyOwner(CSteamID steamIDLobby, CSteamID steamIDNewOwner)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMatchmaking_SetLobbyOwner(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, steamIDNewOwner);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000809A File Offset: 0x0000629A
		public static bool SetLinkedLobby(CSteamID steamIDLobby, CSteamID steamIDLobbyDependent)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMatchmaking_SetLinkedLobby(CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, steamIDLobbyDependent);
		}
	}
}
