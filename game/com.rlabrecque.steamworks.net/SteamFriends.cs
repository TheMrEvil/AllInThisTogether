using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000005 RID: 5
	public static class SteamFriends
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002C24 File Offset: 0x00000E24
		public static string GetPersonaName()
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetPersonaName(CSteamAPIContext.GetSteamFriends()));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002C3C File Offset: 0x00000E3C
		public static SteamAPICall_t SetPersonaName(string pchPersonaName)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchPersonaName))
			{
				result = (SteamAPICall_t)NativeMethods.ISteamFriends_SetPersonaName(CSteamAPIContext.GetSteamFriends(), utf8StringHandle);
			}
			return result;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002C84 File Offset: 0x00000E84
		public static EPersonaState GetPersonaState()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetPersonaState(CSteamAPIContext.GetSteamFriends());
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002C95 File Offset: 0x00000E95
		public static int GetFriendCount(EFriendFlags iFriendFlags)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendCount(CSteamAPIContext.GetSteamFriends(), iFriendFlags);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002CA7 File Offset: 0x00000EA7
		public static CSteamID GetFriendByIndex(int iFriend, EFriendFlags iFriendFlags)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamFriends_GetFriendByIndex(CSteamAPIContext.GetSteamFriends(), iFriend, iFriendFlags);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002CBF File Offset: 0x00000EBF
		public static EFriendRelationship GetFriendRelationship(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendRelationship(CSteamAPIContext.GetSteamFriends(), steamIDFriend);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002CD1 File Offset: 0x00000ED1
		public static EPersonaState GetFriendPersonaState(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendPersonaState(CSteamAPIContext.GetSteamFriends(), steamIDFriend);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002CE3 File Offset: 0x00000EE3
		public static string GetFriendPersonaName(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetFriendPersonaName(CSteamAPIContext.GetSteamFriends(), steamIDFriend));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002CFA File Offset: 0x00000EFA
		public static bool GetFriendGamePlayed(CSteamID steamIDFriend, out FriendGameInfo_t pFriendGameInfo)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendGamePlayed(CSteamAPIContext.GetSteamFriends(), steamIDFriend, out pFriendGameInfo);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D0D File Offset: 0x00000F0D
		public static string GetFriendPersonaNameHistory(CSteamID steamIDFriend, int iPersonaName)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetFriendPersonaNameHistory(CSteamAPIContext.GetSteamFriends(), steamIDFriend, iPersonaName));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D25 File Offset: 0x00000F25
		public static int GetFriendSteamLevel(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendSteamLevel(CSteamAPIContext.GetSteamFriends(), steamIDFriend);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002D37 File Offset: 0x00000F37
		public static string GetPlayerNickname(CSteamID steamIDPlayer)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetPlayerNickname(CSteamAPIContext.GetSteamFriends(), steamIDPlayer));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D4E File Offset: 0x00000F4E
		public static int GetFriendsGroupCount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendsGroupCount(CSteamAPIContext.GetSteamFriends());
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002D5F File Offset: 0x00000F5F
		public static FriendsGroupID_t GetFriendsGroupIDByIndex(int iFG)
		{
			InteropHelp.TestIfAvailableClient();
			return (FriendsGroupID_t)NativeMethods.ISteamFriends_GetFriendsGroupIDByIndex(CSteamAPIContext.GetSteamFriends(), iFG);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002D76 File Offset: 0x00000F76
		public static string GetFriendsGroupName(FriendsGroupID_t friendsGroupID)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetFriendsGroupName(CSteamAPIContext.GetSteamFriends(), friendsGroupID));
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002D8D File Offset: 0x00000F8D
		public static int GetFriendsGroupMembersCount(FriendsGroupID_t friendsGroupID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendsGroupMembersCount(CSteamAPIContext.GetSteamFriends(), friendsGroupID);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002D9F File Offset: 0x00000F9F
		public static void GetFriendsGroupMembersList(FriendsGroupID_t friendsGroupID, CSteamID[] pOutSteamIDMembers, int nMembersCount)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamFriends_GetFriendsGroupMembersList(CSteamAPIContext.GetSteamFriends(), friendsGroupID, pOutSteamIDMembers, nMembersCount);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002DB3 File Offset: 0x00000FB3
		public static bool HasFriend(CSteamID steamIDFriend, EFriendFlags iFriendFlags)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_HasFriend(CSteamAPIContext.GetSteamFriends(), steamIDFriend, iFriendFlags);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002DC6 File Offset: 0x00000FC6
		public static int GetClanCount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetClanCount(CSteamAPIContext.GetSteamFriends());
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002DD7 File Offset: 0x00000FD7
		public static CSteamID GetClanByIndex(int iClan)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamFriends_GetClanByIndex(CSteamAPIContext.GetSteamFriends(), iClan);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002DEE File Offset: 0x00000FEE
		public static string GetClanName(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetClanName(CSteamAPIContext.GetSteamFriends(), steamIDClan));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002E05 File Offset: 0x00001005
		public static string GetClanTag(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetClanTag(CSteamAPIContext.GetSteamFriends(), steamIDClan));
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002E1C File Offset: 0x0000101C
		public static bool GetClanActivityCounts(CSteamID steamIDClan, out int pnOnline, out int pnInGame, out int pnChatting)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetClanActivityCounts(CSteamAPIContext.GetSteamFriends(), steamIDClan, out pnOnline, out pnInGame, out pnChatting);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002E31 File Offset: 0x00001031
		public static SteamAPICall_t DownloadClanActivityCounts(CSteamID[] psteamIDClans, int cClansToRequest)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamFriends_DownloadClanActivityCounts(CSteamAPIContext.GetSteamFriends(), psteamIDClans, cClansToRequest);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002E49 File Offset: 0x00001049
		public static int GetFriendCountFromSource(CSteamID steamIDSource)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendCountFromSource(CSteamAPIContext.GetSteamFriends(), steamIDSource);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002E5B File Offset: 0x0000105B
		public static CSteamID GetFriendFromSourceByIndex(CSteamID steamIDSource, int iFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamFriends_GetFriendFromSourceByIndex(CSteamAPIContext.GetSteamFriends(), steamIDSource, iFriend);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002E73 File Offset: 0x00001073
		public static bool IsUserInSource(CSteamID steamIDUser, CSteamID steamIDSource)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_IsUserInSource(CSteamAPIContext.GetSteamFriends(), steamIDUser, steamIDSource);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002E86 File Offset: 0x00001086
		public static void SetInGameVoiceSpeaking(CSteamID steamIDUser, bool bSpeaking)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamFriends_SetInGameVoiceSpeaking(CSteamAPIContext.GetSteamFriends(), steamIDUser, bSpeaking);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002E9C File Offset: 0x0000109C
		public static void ActivateGameOverlay(string pchDialog)
		{
			InteropHelp.TestIfAvailableClient();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchDialog))
			{
				NativeMethods.ISteamFriends_ActivateGameOverlay(CSteamAPIContext.GetSteamFriends(), utf8StringHandle);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002EDC File Offset: 0x000010DC
		public static void ActivateGameOverlayToUser(string pchDialog, CSteamID steamID)
		{
			InteropHelp.TestIfAvailableClient();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchDialog))
			{
				NativeMethods.ISteamFriends_ActivateGameOverlayToUser(CSteamAPIContext.GetSteamFriends(), utf8StringHandle, steamID);
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002F20 File Offset: 0x00001120
		public static void ActivateGameOverlayToWebPage(string pchURL, EActivateGameOverlayToWebPageMode eMode = EActivateGameOverlayToWebPageMode.k_EActivateGameOverlayToWebPageMode_Default)
		{
			InteropHelp.TestIfAvailableClient();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchURL))
			{
				NativeMethods.ISteamFriends_ActivateGameOverlayToWebPage(CSteamAPIContext.GetSteamFriends(), utf8StringHandle, eMode);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002F64 File Offset: 0x00001164
		public static void ActivateGameOverlayToStore(AppId_t nAppID, EOverlayToStoreFlag eFlag)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamFriends_ActivateGameOverlayToStore(CSteamAPIContext.GetSteamFriends(), nAppID, eFlag);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002F77 File Offset: 0x00001177
		public static void SetPlayedWith(CSteamID steamIDUserPlayedWith)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamFriends_SetPlayedWith(CSteamAPIContext.GetSteamFriends(), steamIDUserPlayedWith);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002F89 File Offset: 0x00001189
		public static void ActivateGameOverlayInviteDialog(CSteamID steamIDLobby)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamFriends_ActivateGameOverlayInviteDialog(CSteamAPIContext.GetSteamFriends(), steamIDLobby);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002F9B File Offset: 0x0000119B
		public static int GetSmallFriendAvatar(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetSmallFriendAvatar(CSteamAPIContext.GetSteamFriends(), steamIDFriend);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002FAD File Offset: 0x000011AD
		public static int GetMediumFriendAvatar(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetMediumFriendAvatar(CSteamAPIContext.GetSteamFriends(), steamIDFriend);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002FBF File Offset: 0x000011BF
		public static int GetLargeFriendAvatar(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetLargeFriendAvatar(CSteamAPIContext.GetSteamFriends(), steamIDFriend);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002FD1 File Offset: 0x000011D1
		public static bool RequestUserInformation(CSteamID steamIDUser, bool bRequireNameOnly)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_RequestUserInformation(CSteamAPIContext.GetSteamFriends(), steamIDUser, bRequireNameOnly);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002FE4 File Offset: 0x000011E4
		public static SteamAPICall_t RequestClanOfficerList(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamFriends_RequestClanOfficerList(CSteamAPIContext.GetSteamFriends(), steamIDClan);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002FFB File Offset: 0x000011FB
		public static CSteamID GetClanOwner(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamFriends_GetClanOwner(CSteamAPIContext.GetSteamFriends(), steamIDClan);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003012 File Offset: 0x00001212
		public static int GetClanOfficerCount(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetClanOfficerCount(CSteamAPIContext.GetSteamFriends(), steamIDClan);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003024 File Offset: 0x00001224
		public static CSteamID GetClanOfficerByIndex(CSteamID steamIDClan, int iOfficer)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamFriends_GetClanOfficerByIndex(CSteamAPIContext.GetSteamFriends(), steamIDClan, iOfficer);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000303C File Offset: 0x0000123C
		public static uint GetUserRestrictions()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetUserRestrictions(CSteamAPIContext.GetSteamFriends());
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003050 File Offset: 0x00001250
		public static bool SetRichPresence(string pchKey, string pchValue)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKey))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchValue))
				{
					result = NativeMethods.ISteamFriends_SetRichPresence(CSteamAPIContext.GetSteamFriends(), utf8StringHandle, utf8StringHandle2);
				}
			}
			return result;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000030B0 File Offset: 0x000012B0
		public static void ClearRichPresence()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamFriends_ClearRichPresence(CSteamAPIContext.GetSteamFriends());
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000030C4 File Offset: 0x000012C4
		public static string GetFriendRichPresence(CSteamID steamIDFriend, string pchKey)
		{
			InteropHelp.TestIfAvailableClient();
			string result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKey))
			{
				result = InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetFriendRichPresence(CSteamAPIContext.GetSteamFriends(), steamIDFriend, utf8StringHandle));
			}
			return result;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000310C File Offset: 0x0000130C
		public static int GetFriendRichPresenceKeyCount(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendRichPresenceKeyCount(CSteamAPIContext.GetSteamFriends(), steamIDFriend);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000311E File Offset: 0x0000131E
		public static string GetFriendRichPresenceKeyByIndex(CSteamID steamIDFriend, int iKey)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetFriendRichPresenceKeyByIndex(CSteamAPIContext.GetSteamFriends(), steamIDFriend, iKey));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003136 File Offset: 0x00001336
		public static void RequestFriendRichPresence(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamFriends_RequestFriendRichPresence(CSteamAPIContext.GetSteamFriends(), steamIDFriend);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003148 File Offset: 0x00001348
		public static bool InviteUserToGame(CSteamID steamIDFriend, string pchConnectString)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchConnectString))
			{
				result = NativeMethods.ISteamFriends_InviteUserToGame(CSteamAPIContext.GetSteamFriends(), steamIDFriend, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000318C File Offset: 0x0000138C
		public static int GetCoplayFriendCount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetCoplayFriendCount(CSteamAPIContext.GetSteamFriends());
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000319D File Offset: 0x0000139D
		public static CSteamID GetCoplayFriend(int iCoplayFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamFriends_GetCoplayFriend(CSteamAPIContext.GetSteamFriends(), iCoplayFriend);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000031B4 File Offset: 0x000013B4
		public static int GetFriendCoplayTime(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendCoplayTime(CSteamAPIContext.GetSteamFriends(), steamIDFriend);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000031C6 File Offset: 0x000013C6
		public static AppId_t GetFriendCoplayGame(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return (AppId_t)NativeMethods.ISteamFriends_GetFriendCoplayGame(CSteamAPIContext.GetSteamFriends(), steamIDFriend);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000031DD File Offset: 0x000013DD
		public static SteamAPICall_t JoinClanChatRoom(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamFriends_JoinClanChatRoom(CSteamAPIContext.GetSteamFriends(), steamIDClan);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000031F4 File Offset: 0x000013F4
		public static bool LeaveClanChatRoom(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_LeaveClanChatRoom(CSteamAPIContext.GetSteamFriends(), steamIDClan);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003206 File Offset: 0x00001406
		public static int GetClanChatMemberCount(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetClanChatMemberCount(CSteamAPIContext.GetSteamFriends(), steamIDClan);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003218 File Offset: 0x00001418
		public static CSteamID GetChatMemberByIndex(CSteamID steamIDClan, int iUser)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamFriends_GetChatMemberByIndex(CSteamAPIContext.GetSteamFriends(), steamIDClan, iUser);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003230 File Offset: 0x00001430
		public static bool SendClanChatMessage(CSteamID steamIDClanChat, string pchText)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchText))
			{
				result = NativeMethods.ISteamFriends_SendClanChatMessage(CSteamAPIContext.GetSteamFriends(), steamIDClanChat, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003274 File Offset: 0x00001474
		public static int GetClanChatMessage(CSteamID steamIDClanChat, int iMessage, out string prgchText, int cchTextMax, out EChatEntryType peChatEntryType, out CSteamID psteamidChatter)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cchTextMax);
			int num = NativeMethods.ISteamFriends_GetClanChatMessage(CSteamAPIContext.GetSteamFriends(), steamIDClanChat, iMessage, intPtr, cchTextMax, out peChatEntryType, out psteamidChatter);
			prgchText = ((num != 0) ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return num;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000032B5 File Offset: 0x000014B5
		public static bool IsClanChatAdmin(CSteamID steamIDClanChat, CSteamID steamIDUser)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_IsClanChatAdmin(CSteamAPIContext.GetSteamFriends(), steamIDClanChat, steamIDUser);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000032C8 File Offset: 0x000014C8
		public static bool IsClanChatWindowOpenInSteam(CSteamID steamIDClanChat)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_IsClanChatWindowOpenInSteam(CSteamAPIContext.GetSteamFriends(), steamIDClanChat);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000032DA File Offset: 0x000014DA
		public static bool OpenClanChatWindowInSteam(CSteamID steamIDClanChat)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_OpenClanChatWindowInSteam(CSteamAPIContext.GetSteamFriends(), steamIDClanChat);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000032EC File Offset: 0x000014EC
		public static bool CloseClanChatWindowInSteam(CSteamID steamIDClanChat)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_CloseClanChatWindowInSteam(CSteamAPIContext.GetSteamFriends(), steamIDClanChat);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000032FE File Offset: 0x000014FE
		public static bool SetListenForFriendsMessages(bool bInterceptEnabled)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_SetListenForFriendsMessages(CSteamAPIContext.GetSteamFriends(), bInterceptEnabled);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003310 File Offset: 0x00001510
		public static bool ReplyToFriendMessage(CSteamID steamIDFriend, string pchMsgToSend)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchMsgToSend))
			{
				result = NativeMethods.ISteamFriends_ReplyToFriendMessage(CSteamAPIContext.GetSteamFriends(), steamIDFriend, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003354 File Offset: 0x00001554
		public static int GetFriendMessage(CSteamID steamIDFriend, int iMessageID, out string pvData, int cubData, out EChatEntryType peChatEntryType)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cubData);
			int num = NativeMethods.ISteamFriends_GetFriendMessage(CSteamAPIContext.GetSteamFriends(), steamIDFriend, iMessageID, intPtr, cubData, out peChatEntryType);
			pvData = ((num != 0) ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return num;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003393 File Offset: 0x00001593
		public static SteamAPICall_t GetFollowerCount(CSteamID steamID)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamFriends_GetFollowerCount(CSteamAPIContext.GetSteamFriends(), steamID);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000033AA File Offset: 0x000015AA
		public static SteamAPICall_t IsFollowing(CSteamID steamID)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamFriends_IsFollowing(CSteamAPIContext.GetSteamFriends(), steamID);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000033C1 File Offset: 0x000015C1
		public static SteamAPICall_t EnumerateFollowingList(uint unStartIndex)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamFriends_EnumerateFollowingList(CSteamAPIContext.GetSteamFriends(), unStartIndex);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000033D8 File Offset: 0x000015D8
		public static bool IsClanPublic(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_IsClanPublic(CSteamAPIContext.GetSteamFriends(), steamIDClan);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000033EA File Offset: 0x000015EA
		public static bool IsClanOfficialGameGroup(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_IsClanOfficialGameGroup(CSteamAPIContext.GetSteamFriends(), steamIDClan);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000033FC File Offset: 0x000015FC
		public static int GetNumChatsWithUnreadPriorityMessages()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetNumChatsWithUnreadPriorityMessages(CSteamAPIContext.GetSteamFriends());
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000340D File Offset: 0x0000160D
		public static void ActivateGameOverlayRemotePlayTogetherInviteDialog(CSteamID steamIDLobby)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamFriends_ActivateGameOverlayRemotePlayTogetherInviteDialog(CSteamAPIContext.GetSteamFriends(), steamIDLobby);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003420 File Offset: 0x00001620
		public static bool RegisterProtocolInOverlayBrowser(string pchProtocol)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchProtocol))
			{
				result = NativeMethods.ISteamFriends_RegisterProtocolInOverlayBrowser(CSteamAPIContext.GetSteamFriends(), utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003464 File Offset: 0x00001664
		public static void ActivateGameOverlayInviteDialogConnectString(string pchConnectString)
		{
			InteropHelp.TestIfAvailableClient();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchConnectString))
			{
				NativeMethods.ISteamFriends_ActivateGameOverlayInviteDialogConnectString(CSteamAPIContext.GetSteamFriends(), utf8StringHandle);
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000034A4 File Offset: 0x000016A4
		public static SteamAPICall_t RequestEquippedProfileItems(CSteamID steamID)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamFriends_RequestEquippedProfileItems(CSteamAPIContext.GetSteamFriends(), steamID);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000034BB File Offset: 0x000016BB
		public static bool BHasEquippedProfileItem(CSteamID steamID, ECommunityProfileItemType itemType)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_BHasEquippedProfileItem(CSteamAPIContext.GetSteamFriends(), steamID, itemType);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000034CE File Offset: 0x000016CE
		public static string GetProfileItemPropertyString(CSteamID steamID, ECommunityProfileItemType itemType, ECommunityProfileItemProperty prop)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetProfileItemPropertyString(CSteamAPIContext.GetSteamFriends(), steamID, itemType, prop));
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000034E7 File Offset: 0x000016E7
		public static uint GetProfileItemPropertyUint(CSteamID steamID, ECommunityProfileItemType itemType, ECommunityProfileItemProperty prop)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetProfileItemPropertyUint(CSteamAPIContext.GetSteamFriends(), steamID, itemType, prop);
		}
	}
}
