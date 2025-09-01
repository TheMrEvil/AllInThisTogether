using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000094 RID: 148
	public class SteamFriends : SteamClientClass<SteamFriends>
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0000B5C9 File Offset: 0x000097C9
		internal static ISteamFriends Internal
		{
			get
			{
				return SteamClientClass<SteamFriends>.Interface as ISteamFriends;
			}
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0000B5D5 File Offset: 0x000097D5
		internal override void InitializeInterface(bool server)
		{
			this.SetInterface(server, new ISteamFriends(server));
			SteamFriends.richPresence = new Dictionary<string, string>();
			this.InstallEvents();
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0000B5F8 File Offset: 0x000097F8
		internal void InstallEvents()
		{
			Dispatch.Install<PersonaStateChange_t>(delegate(PersonaStateChange_t x)
			{
				Action<Friend> onPersonaStateChange = SteamFriends.OnPersonaStateChange;
				if (onPersonaStateChange != null)
				{
					onPersonaStateChange(new Friend(x.SteamID));
				}
			}, false);
			Dispatch.Install<GameRichPresenceJoinRequested_t>(delegate(GameRichPresenceJoinRequested_t x)
			{
				Action<Friend, string> onGameRichPresenceJoinRequested = SteamFriends.OnGameRichPresenceJoinRequested;
				if (onGameRichPresenceJoinRequested != null)
				{
					onGameRichPresenceJoinRequested(new Friend(x.SteamIDFriend), x.ConnectUTF8());
				}
			}, false);
			Dispatch.Install<GameConnectedFriendChatMsg_t>(new Action<GameConnectedFriendChatMsg_t>(SteamFriends.OnFriendChatMessage), false);
			Dispatch.Install<GameOverlayActivated_t>(delegate(GameOverlayActivated_t x)
			{
				Action<bool> onGameOverlayActivated = SteamFriends.OnGameOverlayActivated;
				if (onGameOverlayActivated != null)
				{
					onGameOverlayActivated(x.Active > 0);
				}
			}, false);
			Dispatch.Install<GameServerChangeRequested_t>(delegate(GameServerChangeRequested_t x)
			{
				Action<string, string> onGameServerChangeRequested = SteamFriends.OnGameServerChangeRequested;
				if (onGameServerChangeRequested != null)
				{
					onGameServerChangeRequested(x.ServerUTF8(), x.PasswordUTF8());
				}
			}, false);
			Dispatch.Install<GameLobbyJoinRequested_t>(delegate(GameLobbyJoinRequested_t x)
			{
				Action<Lobby, SteamId> onGameLobbyJoinRequested = SteamFriends.OnGameLobbyJoinRequested;
				if (onGameLobbyJoinRequested != null)
				{
					onGameLobbyJoinRequested(new Lobby(x.SteamIDLobby), x.SteamIDFriend);
				}
			}, false);
			Dispatch.Install<FriendRichPresenceUpdate_t>(delegate(FriendRichPresenceUpdate_t x)
			{
				Action<Friend> onFriendRichPresenceUpdate = SteamFriends.OnFriendRichPresenceUpdate;
				if (onFriendRichPresenceUpdate != null)
				{
					onFriendRichPresenceUpdate(new Friend(x.SteamIDFriend));
				}
			}, false);
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600076D RID: 1901 RVA: 0x0000B700 File Offset: 0x00009900
		// (remove) Token: 0x0600076E RID: 1902 RVA: 0x0000B734 File Offset: 0x00009934
		public static event Action<Friend, string, string> OnChatMessage
		{
			[CompilerGenerated]
			add
			{
				Action<Friend, string, string> action = SteamFriends.OnChatMessage;
				Action<Friend, string, string> action2;
				do
				{
					action2 = action;
					Action<Friend, string, string> value2 = (Action<Friend, string, string>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Friend, string, string>>(ref SteamFriends.OnChatMessage, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Friend, string, string> action = SteamFriends.OnChatMessage;
				Action<Friend, string, string> action2;
				do
				{
					action2 = action;
					Action<Friend, string, string> value2 = (Action<Friend, string, string>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Friend, string, string>>(ref SteamFriends.OnChatMessage, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600076F RID: 1903 RVA: 0x0000B768 File Offset: 0x00009968
		// (remove) Token: 0x06000770 RID: 1904 RVA: 0x0000B79C File Offset: 0x0000999C
		public static event Action<Friend> OnPersonaStateChange
		{
			[CompilerGenerated]
			add
			{
				Action<Friend> action = SteamFriends.OnPersonaStateChange;
				Action<Friend> action2;
				do
				{
					action2 = action;
					Action<Friend> value2 = (Action<Friend>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Friend>>(ref SteamFriends.OnPersonaStateChange, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Friend> action = SteamFriends.OnPersonaStateChange;
				Action<Friend> action2;
				do
				{
					action2 = action;
					Action<Friend> value2 = (Action<Friend>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Friend>>(ref SteamFriends.OnPersonaStateChange, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000771 RID: 1905 RVA: 0x0000B7D0 File Offset: 0x000099D0
		// (remove) Token: 0x06000772 RID: 1906 RVA: 0x0000B804 File Offset: 0x00009A04
		public static event Action<Friend, string> OnGameRichPresenceJoinRequested
		{
			[CompilerGenerated]
			add
			{
				Action<Friend, string> action = SteamFriends.OnGameRichPresenceJoinRequested;
				Action<Friend, string> action2;
				do
				{
					action2 = action;
					Action<Friend, string> value2 = (Action<Friend, string>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Friend, string>>(ref SteamFriends.OnGameRichPresenceJoinRequested, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Friend, string> action = SteamFriends.OnGameRichPresenceJoinRequested;
				Action<Friend, string> action2;
				do
				{
					action2 = action;
					Action<Friend, string> value2 = (Action<Friend, string>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Friend, string>>(ref SteamFriends.OnGameRichPresenceJoinRequested, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000773 RID: 1907 RVA: 0x0000B838 File Offset: 0x00009A38
		// (remove) Token: 0x06000774 RID: 1908 RVA: 0x0000B86C File Offset: 0x00009A6C
		public static event Action<bool> OnGameOverlayActivated
		{
			[CompilerGenerated]
			add
			{
				Action<bool> action = SteamFriends.OnGameOverlayActivated;
				Action<bool> action2;
				do
				{
					action2 = action;
					Action<bool> value2 = (Action<bool>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<bool>>(ref SteamFriends.OnGameOverlayActivated, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<bool> action = SteamFriends.OnGameOverlayActivated;
				Action<bool> action2;
				do
				{
					action2 = action;
					Action<bool> value2 = (Action<bool>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<bool>>(ref SteamFriends.OnGameOverlayActivated, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000775 RID: 1909 RVA: 0x0000B8A0 File Offset: 0x00009AA0
		// (remove) Token: 0x06000776 RID: 1910 RVA: 0x0000B8D4 File Offset: 0x00009AD4
		public static event Action<string, string> OnGameServerChangeRequested
		{
			[CompilerGenerated]
			add
			{
				Action<string, string> action = SteamFriends.OnGameServerChangeRequested;
				Action<string, string> action2;
				do
				{
					action2 = action;
					Action<string, string> value2 = (Action<string, string>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<string, string>>(ref SteamFriends.OnGameServerChangeRequested, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<string, string> action = SteamFriends.OnGameServerChangeRequested;
				Action<string, string> action2;
				do
				{
					action2 = action;
					Action<string, string> value2 = (Action<string, string>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<string, string>>(ref SteamFriends.OnGameServerChangeRequested, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000777 RID: 1911 RVA: 0x0000B908 File Offset: 0x00009B08
		// (remove) Token: 0x06000778 RID: 1912 RVA: 0x0000B93C File Offset: 0x00009B3C
		public static event Action<Lobby, SteamId> OnGameLobbyJoinRequested
		{
			[CompilerGenerated]
			add
			{
				Action<Lobby, SteamId> action = SteamFriends.OnGameLobbyJoinRequested;
				Action<Lobby, SteamId> action2;
				do
				{
					action2 = action;
					Action<Lobby, SteamId> value2 = (Action<Lobby, SteamId>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Lobby, SteamId>>(ref SteamFriends.OnGameLobbyJoinRequested, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Lobby, SteamId> action = SteamFriends.OnGameLobbyJoinRequested;
				Action<Lobby, SteamId> action2;
				do
				{
					action2 = action;
					Action<Lobby, SteamId> value2 = (Action<Lobby, SteamId>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Lobby, SteamId>>(ref SteamFriends.OnGameLobbyJoinRequested, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000779 RID: 1913 RVA: 0x0000B970 File Offset: 0x00009B70
		// (remove) Token: 0x0600077A RID: 1914 RVA: 0x0000B9A4 File Offset: 0x00009BA4
		public static event Action<Friend> OnFriendRichPresenceUpdate
		{
			[CompilerGenerated]
			add
			{
				Action<Friend> action = SteamFriends.OnFriendRichPresenceUpdate;
				Action<Friend> action2;
				do
				{
					action2 = action;
					Action<Friend> value2 = (Action<Friend>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Friend>>(ref SteamFriends.OnFriendRichPresenceUpdate, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Friend> action = SteamFriends.OnFriendRichPresenceUpdate;
				Action<Friend> action2;
				do
				{
					action2 = action;
					Action<Friend> value2 = (Action<Friend>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Friend>>(ref SteamFriends.OnFriendRichPresenceUpdate, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0000B9D8 File Offset: 0x00009BD8
		private unsafe static void OnFriendChatMessage(GameConnectedFriendChatMsg_t data)
		{
			bool flag = SteamFriends.OnChatMessage == null;
			if (!flag)
			{
				Friend arg = new Friend(data.SteamIDUser);
				byte[] array = Helpers.TakeBuffer(32768);
				ChatEntryType chatEntryType = ChatEntryType.ChatMsg;
				byte[] array2;
				byte* value;
				if ((array2 = array) == null || array2.Length == 0)
				{
					value = null;
				}
				else
				{
					value = &array2[0];
				}
				int friendMessage = SteamFriends.Internal.GetFriendMessage(data.SteamIDUser, data.MessageID, (IntPtr)((void*)value), array.Length, ref chatEntryType);
				bool flag2 = friendMessage == 0 && chatEntryType == ChatEntryType.Invalid;
				if (!flag2)
				{
					string arg2 = chatEntryType.ToString();
					string @string = Encoding.UTF8.GetString(array, 0, friendMessage);
					SteamFriends.OnChatMessage(arg, arg2, @string);
					array2 = null;
				}
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0000BAA3 File Offset: 0x00009CA3
		private static IEnumerable<Friend> GetFriendsWithFlag(FriendFlags flag)
		{
			int num;
			for (int i = 0; i < SteamFriends.Internal.GetFriendCount((int)flag); i = num + 1)
			{
				yield return new Friend(SteamFriends.Internal.GetFriendByIndex(i, (int)flag));
				num = i;
			}
			yield break;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0000BAB4 File Offset: 0x00009CB4
		public static IEnumerable<Friend> GetFriends()
		{
			return SteamFriends.GetFriendsWithFlag(FriendFlags.Immediate);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0000BACC File Offset: 0x00009CCC
		public static IEnumerable<Friend> GetBlocked()
		{
			return SteamFriends.GetFriendsWithFlag(FriendFlags.Blocked);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0000BAE4 File Offset: 0x00009CE4
		public static IEnumerable<Friend> GetFriendsRequested()
		{
			return SteamFriends.GetFriendsWithFlag(FriendFlags.FriendshipRequested);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0000BAFC File Offset: 0x00009CFC
		public static IEnumerable<Friend> GetFriendsClanMembers()
		{
			return SteamFriends.GetFriendsWithFlag(FriendFlags.ClanMember);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0000BB14 File Offset: 0x00009D14
		public static IEnumerable<Friend> GetFriendsOnGameServer()
		{
			return SteamFriends.GetFriendsWithFlag(FriendFlags.OnGameServer);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0000BB30 File Offset: 0x00009D30
		public static IEnumerable<Friend> GetFriendsRequestingFriendship()
		{
			return SteamFriends.GetFriendsWithFlag(FriendFlags.RequestingFriendship);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0000BB4C File Offset: 0x00009D4C
		public static IEnumerable<Friend> GetPlayedWith()
		{
			int num;
			for (int i = 0; i < SteamFriends.Internal.GetCoplayFriendCount(); i = num + 1)
			{
				yield return new Friend(SteamFriends.Internal.GetCoplayFriend(i));
				num = i;
			}
			yield break;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0000BB55 File Offset: 0x00009D55
		public static IEnumerable<Friend> GetFromSource(SteamId steamid)
		{
			int num;
			for (int i = 0; i < SteamFriends.Internal.GetFriendCountFromSource(steamid); i = num + 1)
			{
				yield return new Friend(SteamFriends.Internal.GetFriendFromSourceByIndex(steamid, i));
				num = i;
			}
			yield break;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0000BB65 File Offset: 0x00009D65
		public static void OpenOverlay(string type)
		{
			SteamFriends.Internal.ActivateGameOverlay(type);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0000BB73 File Offset: 0x00009D73
		public static void OpenUserOverlay(SteamId id, string type)
		{
			SteamFriends.Internal.ActivateGameOverlayToUser(type, id);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0000BB82 File Offset: 0x00009D82
		public static void OpenStoreOverlay(AppId id)
		{
			SteamFriends.Internal.ActivateGameOverlayToStore(id.Value, OverlayToStoreFlag.None);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0000BB9B File Offset: 0x00009D9B
		public static void OpenWebOverlay(string url, bool modal = false)
		{
			SteamFriends.Internal.ActivateGameOverlayToWebPage(url, modal ? ActivateGameOverlayToWebPageMode.Modal : ActivateGameOverlayToWebPageMode.Default);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0000BBB0 File Offset: 0x00009DB0
		public static void OpenGameInviteOverlay(SteamId lobby)
		{
			SteamFriends.Internal.ActivateGameOverlayInviteDialog(lobby);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0000BBBE File Offset: 0x00009DBE
		public static void SetPlayedWith(SteamId steamid)
		{
			SteamFriends.Internal.SetPlayedWith(steamid);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0000BBCC File Offset: 0x00009DCC
		public static bool RequestUserInformation(SteamId steamid, bool nameonly = true)
		{
			return SteamFriends.Internal.RequestUserInformation(steamid, nameonly);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0000BBDC File Offset: 0x00009DDC
		internal static async Task CacheUserInformationAsync(SteamId steamid, bool nameonly)
		{
			bool flag = !SteamFriends.RequestUserInformation(steamid, nameonly);
			if (!flag)
			{
				await Task.Delay(100);
				while (SteamFriends.RequestUserInformation(steamid, nameonly))
				{
					await Task.Delay(50);
				}
				await Task.Delay(500);
			}
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0000BC2C File Offset: 0x00009E2C
		public static async Task<Image?> GetSmallAvatarAsync(SteamId steamid)
		{
			await SteamFriends.CacheUserInformationAsync(steamid, false);
			return SteamUtils.GetImage(SteamFriends.Internal.GetSmallFriendAvatar(steamid));
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0000BC74 File Offset: 0x00009E74
		public static async Task<Image?> GetMediumAvatarAsync(SteamId steamid)
		{
			await SteamFriends.CacheUserInformationAsync(steamid, false);
			return SteamUtils.GetImage(SteamFriends.Internal.GetMediumFriendAvatar(steamid));
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0000BCBC File Offset: 0x00009EBC
		public static async Task<Image?> GetLargeAvatarAsync(SteamId steamid)
		{
			await SteamFriends.CacheUserInformationAsync(steamid, false);
			int imageid;
			for (imageid = SteamFriends.Internal.GetLargeFriendAvatar(steamid); imageid == -1; imageid = SteamFriends.Internal.GetLargeFriendAvatar(steamid))
			{
				await Task.Delay(50);
			}
			return SteamUtils.GetImage(imageid);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0000BD04 File Offset: 0x00009F04
		public static string GetRichPresence(string key)
		{
			string text;
			bool flag = SteamFriends.richPresence.TryGetValue(key, out text);
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0000BD2C File Offset: 0x00009F2C
		public static bool SetRichPresence(string key, string value)
		{
			bool flag = SteamFriends.Internal.SetRichPresence(key, value);
			bool flag2 = flag;
			if (flag2)
			{
				SteamFriends.richPresence[key] = value;
			}
			return flag;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0000BD5E File Offset: 0x00009F5E
		public static void ClearRichPresence()
		{
			SteamFriends.richPresence.Clear();
			SteamFriends.Internal.ClearRichPresence();
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x0000BD77 File Offset: 0x00009F77
		// (set) Token: 0x06000794 RID: 1940 RVA: 0x0000BD7E File Offset: 0x00009F7E
		public static bool ListenForFriendsMessages
		{
			get
			{
				return SteamFriends._listenForFriendsMessages;
			}
			set
			{
				SteamFriends._listenForFriendsMessages = value;
				SteamFriends.Internal.SetListenForFriendsMessages(value);
			}
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0000BD94 File Offset: 0x00009F94
		public static async Task<bool> IsFollowing(SteamId steamID)
		{
			FriendsIsFollowing_t? friendsIsFollowing_t = await SteamFriends.Internal.IsFollowing(steamID);
			FriendsIsFollowing_t? r = friendsIsFollowing_t;
			friendsIsFollowing_t = null;
			return r.Value.IsFollowing;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0000BDDC File Offset: 0x00009FDC
		public static async Task<int> GetFollowerCount(SteamId steamID)
		{
			FriendsGetFollowerCount_t? friendsGetFollowerCount_t = await SteamFriends.Internal.GetFollowerCount(steamID);
			FriendsGetFollowerCount_t? r = friendsGetFollowerCount_t;
			friendsGetFollowerCount_t = null;
			return r.Value.Count;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0000BE24 File Offset: 0x0000A024
		public static async Task<SteamId[]> GetFollowingList()
		{
			int resultCount = 0;
			List<SteamId> steamIds = new List<SteamId>();
			FriendsEnumerateFollowingList_t? result;
			do
			{
				FriendsEnumerateFollowingList_t? friendsEnumerateFollowingList_t = await SteamFriends.Internal.EnumerateFollowingList((uint)resultCount);
				FriendsEnumerateFollowingList_t? friendsEnumerateFollowingList_t2;
				result = (friendsEnumerateFollowingList_t2 = friendsEnumerateFollowingList_t);
				bool flag = friendsEnumerateFollowingList_t2 != null;
				if (flag)
				{
					friendsEnumerateFollowingList_t = null;
					resultCount += result.Value.ResultsReturned;
					Array.ForEach<ulong>(result.Value.GSteamID, delegate(ulong id)
					{
						bool flag2 = id > 0UL;
						if (flag2)
						{
							steamIds.Add(id);
						}
					});
				}
			}
			while (result != null && resultCount < result.Value.TotalResultCount);
			return steamIds.ToArray();
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0000BE64 File Offset: 0x0000A064
		public SteamFriends()
		{
		}

		// Token: 0x040006E2 RID: 1762
		private static Dictionary<string, string> richPresence;

		// Token: 0x040006E3 RID: 1763
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Friend, string, string> OnChatMessage;

		// Token: 0x040006E4 RID: 1764
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Friend> OnPersonaStateChange;

		// Token: 0x040006E5 RID: 1765
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Friend, string> OnGameRichPresenceJoinRequested;

		// Token: 0x040006E6 RID: 1766
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<bool> OnGameOverlayActivated;

		// Token: 0x040006E7 RID: 1767
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<string, string> OnGameServerChangeRequested;

		// Token: 0x040006E8 RID: 1768
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Lobby, SteamId> OnGameLobbyJoinRequested;

		// Token: 0x040006E9 RID: 1769
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Friend> OnFriendRichPresenceUpdate;

		// Token: 0x040006EA RID: 1770
		private static bool _listenForFriendsMessages;

		// Token: 0x02000216 RID: 534
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600109B RID: 4251 RVA: 0x0001BA9E File Offset: 0x00019C9E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600109C RID: 4252 RVA: 0x0001BAAA File Offset: 0x00019CAA
			public <>c()
			{
			}

			// Token: 0x0600109D RID: 4253 RVA: 0x0001BAB3 File Offset: 0x00019CB3
			internal void <InstallEvents>b__4_0(PersonaStateChange_t x)
			{
				Action<Friend> onPersonaStateChange = SteamFriends.OnPersonaStateChange;
				if (onPersonaStateChange != null)
				{
					onPersonaStateChange(new Friend(x.SteamID));
				}
			}

			// Token: 0x0600109E RID: 4254 RVA: 0x0001BAD6 File Offset: 0x00019CD6
			internal void <InstallEvents>b__4_1(GameRichPresenceJoinRequested_t x)
			{
				Action<Friend, string> onGameRichPresenceJoinRequested = SteamFriends.OnGameRichPresenceJoinRequested;
				if (onGameRichPresenceJoinRequested != null)
				{
					onGameRichPresenceJoinRequested(new Friend(x.SteamIDFriend), x.ConnectUTF8());
				}
			}

			// Token: 0x0600109F RID: 4255 RVA: 0x0001BB00 File Offset: 0x00019D00
			internal void <InstallEvents>b__4_2(GameOverlayActivated_t x)
			{
				Action<bool> onGameOverlayActivated = SteamFriends.OnGameOverlayActivated;
				if (onGameOverlayActivated != null)
				{
					onGameOverlayActivated(x.Active > 0);
				}
			}

			// Token: 0x060010A0 RID: 4256 RVA: 0x0001BB1C File Offset: 0x00019D1C
			internal void <InstallEvents>b__4_3(GameServerChangeRequested_t x)
			{
				Action<string, string> onGameServerChangeRequested = SteamFriends.OnGameServerChangeRequested;
				if (onGameServerChangeRequested != null)
				{
					onGameServerChangeRequested(x.ServerUTF8(), x.PasswordUTF8());
				}
			}

			// Token: 0x060010A1 RID: 4257 RVA: 0x0001BB3D File Offset: 0x00019D3D
			internal void <InstallEvents>b__4_4(GameLobbyJoinRequested_t x)
			{
				Action<Lobby, SteamId> onGameLobbyJoinRequested = SteamFriends.OnGameLobbyJoinRequested;
				if (onGameLobbyJoinRequested != null)
				{
					onGameLobbyJoinRequested(new Lobby(x.SteamIDLobby), x.SteamIDFriend);
				}
			}

			// Token: 0x060010A2 RID: 4258 RVA: 0x0001BB6B File Offset: 0x00019D6B
			internal void <InstallEvents>b__4_5(FriendRichPresenceUpdate_t x)
			{
				Action<Friend> onFriendRichPresenceUpdate = SteamFriends.OnFriendRichPresenceUpdate;
				if (onFriendRichPresenceUpdate != null)
				{
					onFriendRichPresenceUpdate(new Friend(x.SteamIDFriend));
				}
			}

			// Token: 0x04000C6C RID: 3180
			public static readonly SteamFriends.<>c <>9 = new SteamFriends.<>c();

			// Token: 0x04000C6D RID: 3181
			public static Action<PersonaStateChange_t> <>9__4_0;

			// Token: 0x04000C6E RID: 3182
			public static Action<GameRichPresenceJoinRequested_t> <>9__4_1;

			// Token: 0x04000C6F RID: 3183
			public static Action<GameOverlayActivated_t> <>9__4_2;

			// Token: 0x04000C70 RID: 3184
			public static Action<GameServerChangeRequested_t> <>9__4_3;

			// Token: 0x04000C71 RID: 3185
			public static Action<GameLobbyJoinRequested_t> <>9__4_4;

			// Token: 0x04000C72 RID: 3186
			public static Action<FriendRichPresenceUpdate_t> <>9__4_5;
		}

		// Token: 0x02000217 RID: 535
		[CompilerGenerated]
		private sealed class <GetFriendsWithFlag>d__27 : IEnumerable<Friend>, IEnumerable, IEnumerator<Friend>, IDisposable, IEnumerator
		{
			// Token: 0x060010A3 RID: 4259 RVA: 0x0001BB8E File Offset: 0x00019D8E
			[DebuggerHidden]
			public <GetFriendsWithFlag>d__27(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060010A4 RID: 4260 RVA: 0x0001BBA9 File Offset: 0x00019DA9
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060010A5 RID: 4261 RVA: 0x0001BBAC File Offset: 0x00019DAC
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					int num2 = i;
					i = num2 + 1;
				}
				else
				{
					this.<>1__state = -1;
					i = 0;
				}
				if (i >= SteamFriends.Internal.GetFriendCount((int)flag))
				{
					return false;
				}
				this.<>2__current = new Friend(SteamFriends.Internal.GetFriendByIndex(i, (int)flag));
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170002EE RID: 750
			// (get) Token: 0x060010A6 RID: 4262 RVA: 0x0001BC42 File Offset: 0x00019E42
			Friend IEnumerator<Friend>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060010A7 RID: 4263 RVA: 0x0001BC4A File Offset: 0x00019E4A
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170002EF RID: 751
			// (get) Token: 0x060010A8 RID: 4264 RVA: 0x0001BC51 File Offset: 0x00019E51
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060010A9 RID: 4265 RVA: 0x0001BC60 File Offset: 0x00019E60
			[DebuggerHidden]
			IEnumerator<Friend> IEnumerable<Friend>.GetEnumerator()
			{
				SteamFriends.<GetFriendsWithFlag>d__27 <GetFriendsWithFlag>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetFriendsWithFlag>d__ = this;
				}
				else
				{
					<GetFriendsWithFlag>d__ = new SteamFriends.<GetFriendsWithFlag>d__27(0);
				}
				<GetFriendsWithFlag>d__.flag = flag;
				return <GetFriendsWithFlag>d__;
			}

			// Token: 0x060010AA RID: 4266 RVA: 0x0001BCA3 File Offset: 0x00019EA3
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Steamworks.Friend>.GetEnumerator();
			}

			// Token: 0x04000C73 RID: 3187
			private int <>1__state;

			// Token: 0x04000C74 RID: 3188
			private Friend <>2__current;

			// Token: 0x04000C75 RID: 3189
			private int <>l__initialThreadId;

			// Token: 0x04000C76 RID: 3190
			private FriendFlags flag;

			// Token: 0x04000C77 RID: 3191
			public FriendFlags <>3__flag;

			// Token: 0x04000C78 RID: 3192
			private int <i>5__1;
		}

		// Token: 0x02000218 RID: 536
		[CompilerGenerated]
		private sealed class <GetPlayedWith>d__34 : IEnumerable<Friend>, IEnumerable, IEnumerator<Friend>, IDisposable, IEnumerator
		{
			// Token: 0x060010AB RID: 4267 RVA: 0x0001BCAB File Offset: 0x00019EAB
			[DebuggerHidden]
			public <GetPlayedWith>d__34(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060010AC RID: 4268 RVA: 0x0001BCC6 File Offset: 0x00019EC6
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060010AD RID: 4269 RVA: 0x0001BCC8 File Offset: 0x00019EC8
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					int num2 = i;
					i = num2 + 1;
				}
				else
				{
					this.<>1__state = -1;
					i = 0;
				}
				if (i >= SteamFriends.Internal.GetCoplayFriendCount())
				{
					return false;
				}
				this.<>2__current = new Friend(SteamFriends.Internal.GetCoplayFriend(i));
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170002F0 RID: 752
			// (get) Token: 0x060010AE RID: 4270 RVA: 0x0001BD52 File Offset: 0x00019F52
			Friend IEnumerator<Friend>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060010AF RID: 4271 RVA: 0x0001BD5A File Offset: 0x00019F5A
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170002F1 RID: 753
			// (get) Token: 0x060010B0 RID: 4272 RVA: 0x0001BD61 File Offset: 0x00019F61
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060010B1 RID: 4273 RVA: 0x0001BD70 File Offset: 0x00019F70
			[DebuggerHidden]
			IEnumerator<Friend> IEnumerable<Friend>.GetEnumerator()
			{
				SteamFriends.<GetPlayedWith>d__34 result;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					result = this;
				}
				else
				{
					result = new SteamFriends.<GetPlayedWith>d__34(0);
				}
				return result;
			}

			// Token: 0x060010B2 RID: 4274 RVA: 0x0001BDA7 File Offset: 0x00019FA7
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Steamworks.Friend>.GetEnumerator();
			}

			// Token: 0x04000C79 RID: 3193
			private int <>1__state;

			// Token: 0x04000C7A RID: 3194
			private Friend <>2__current;

			// Token: 0x04000C7B RID: 3195
			private int <>l__initialThreadId;

			// Token: 0x04000C7C RID: 3196
			private int <i>5__1;
		}

		// Token: 0x02000219 RID: 537
		[CompilerGenerated]
		private sealed class <GetFromSource>d__35 : IEnumerable<Friend>, IEnumerable, IEnumerator<Friend>, IDisposable, IEnumerator
		{
			// Token: 0x060010B3 RID: 4275 RVA: 0x0001BDAF File Offset: 0x00019FAF
			[DebuggerHidden]
			public <GetFromSource>d__35(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060010B4 RID: 4276 RVA: 0x0001BDCA File Offset: 0x00019FCA
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060010B5 RID: 4277 RVA: 0x0001BDCC File Offset: 0x00019FCC
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					int num2 = i;
					i = num2 + 1;
				}
				else
				{
					this.<>1__state = -1;
					i = 0;
				}
				if (i >= SteamFriends.Internal.GetFriendCountFromSource(steamid))
				{
					return false;
				}
				this.<>2__current = new Friend(SteamFriends.Internal.GetFriendFromSourceByIndex(steamid, i));
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170002F2 RID: 754
			// (get) Token: 0x060010B6 RID: 4278 RVA: 0x0001BE62 File Offset: 0x0001A062
			Friend IEnumerator<Friend>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060010B7 RID: 4279 RVA: 0x0001BE6A File Offset: 0x0001A06A
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170002F3 RID: 755
			// (get) Token: 0x060010B8 RID: 4280 RVA: 0x0001BE71 File Offset: 0x0001A071
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060010B9 RID: 4281 RVA: 0x0001BE80 File Offset: 0x0001A080
			[DebuggerHidden]
			IEnumerator<Friend> IEnumerable<Friend>.GetEnumerator()
			{
				SteamFriends.<GetFromSource>d__35 <GetFromSource>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetFromSource>d__ = this;
				}
				else
				{
					<GetFromSource>d__ = new SteamFriends.<GetFromSource>d__35(0);
				}
				<GetFromSource>d__.steamid = steamid;
				return <GetFromSource>d__;
			}

			// Token: 0x060010BA RID: 4282 RVA: 0x0001BEC3 File Offset: 0x0001A0C3
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Steamworks.Friend>.GetEnumerator();
			}

			// Token: 0x04000C7D RID: 3197
			private int <>1__state;

			// Token: 0x04000C7E RID: 3198
			private Friend <>2__current;

			// Token: 0x04000C7F RID: 3199
			private int <>l__initialThreadId;

			// Token: 0x04000C80 RID: 3200
			private SteamId steamid;

			// Token: 0x04000C81 RID: 3201
			public SteamId <>3__steamid;

			// Token: 0x04000C82 RID: 3202
			private int <i>5__1;
		}

		// Token: 0x0200021A RID: 538
		[CompilerGenerated]
		private sealed class <CacheUserInformationAsync>d__43 : IAsyncStateMachine
		{
			// Token: 0x060010BB RID: 4283 RVA: 0x0001BECB File Offset: 0x0001A0CB
			public <CacheUserInformationAsync>d__43()
			{
			}

			// Token: 0x060010BC RID: 4284 RVA: 0x0001BED4 File Offset: 0x0001A0D4
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				try
				{
					TaskAwaiter taskAwaiter;
					TaskAwaiter taskAwaiter3;
					TaskAwaiter taskAwaiter4;
					switch (num)
					{
					case 0:
					{
						TaskAwaiter taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter);
						num2 = -1;
						break;
					}
					case 1:
					{
						TaskAwaiter taskAwaiter2;
						taskAwaiter3 = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter);
						num2 = -1;
						goto IL_105;
					}
					case 2:
					{
						TaskAwaiter taskAwaiter2;
						taskAwaiter4 = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter);
						num2 = -1;
						goto IL_181;
					}
					default:
					{
						bool flag = !SteamFriends.RequestUserInformation(steamid, nameonly);
						if (flag)
						{
							goto IL_1A5;
						}
						taskAwaiter = Task.Delay(100).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter taskAwaiter2 = taskAwaiter;
							SteamFriends.<CacheUserInformationAsync>d__43 <CacheUserInformationAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, SteamFriends.<CacheUserInformationAsync>d__43>(ref taskAwaiter, ref <CacheUserInformationAsync>d__);
							return;
						}
						break;
					}
					}
					taskAwaiter.GetResult();
					goto IL_10E;
					IL_105:
					taskAwaiter3.GetResult();
					IL_10E:
					if (!SteamFriends.RequestUserInformation(steamid, nameonly))
					{
						taskAwaiter4 = Task.Delay(500).GetAwaiter();
						if (!taskAwaiter4.IsCompleted)
						{
							num2 = 2;
							TaskAwaiter taskAwaiter2 = taskAwaiter4;
							SteamFriends.<CacheUserInformationAsync>d__43 <CacheUserInformationAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, SteamFriends.<CacheUserInformationAsync>d__43>(ref taskAwaiter4, ref <CacheUserInformationAsync>d__);
							return;
						}
					}
					else
					{
						taskAwaiter3 = Task.Delay(50).GetAwaiter();
						if (!taskAwaiter3.IsCompleted)
						{
							num2 = 1;
							TaskAwaiter taskAwaiter2 = taskAwaiter3;
							SteamFriends.<CacheUserInformationAsync>d__43 <CacheUserInformationAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, SteamFriends.<CacheUserInformationAsync>d__43>(ref taskAwaiter3, ref <CacheUserInformationAsync>d__);
							return;
						}
						goto IL_105;
					}
					IL_181:
					taskAwaiter4.GetResult();
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_1A5:
				num2 = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060010BD RID: 4285 RVA: 0x0001C0B8 File Offset: 0x0001A2B8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000C83 RID: 3203
			public int <>1__state;

			// Token: 0x04000C84 RID: 3204
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000C85 RID: 3205
			public SteamId steamid;

			// Token: 0x04000C86 RID: 3206
			public bool nameonly;

			// Token: 0x04000C87 RID: 3207
			private TaskAwaiter <>u__1;
		}

		// Token: 0x0200021B RID: 539
		[CompilerGenerated]
		private sealed class <GetSmallAvatarAsync>d__44 : IAsyncStateMachine
		{
			// Token: 0x060010BE RID: 4286 RVA: 0x0001C0BA File Offset: 0x0001A2BA
			public <GetSmallAvatarAsync>d__44()
			{
			}

			// Token: 0x060010BF RID: 4287 RVA: 0x0001C0C4 File Offset: 0x0001A2C4
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				Image? image;
				try
				{
					TaskAwaiter taskAwaiter;
					if (num != 0)
					{
						taskAwaiter = SteamFriends.CacheUserInformationAsync(steamid, false).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter taskAwaiter2 = taskAwaiter;
							SteamFriends.<GetSmallAvatarAsync>d__44 <GetSmallAvatarAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, SteamFriends.<GetSmallAvatarAsync>d__44>(ref taskAwaiter, ref <GetSmallAvatarAsync>d__);
							return;
						}
					}
					else
					{
						TaskAwaiter taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter);
						num2 = -1;
					}
					taskAwaiter.GetResult();
					image = SteamUtils.GetImage(SteamFriends.Internal.GetSmallFriendAvatar(steamid));
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(image);
			}

			// Token: 0x060010C0 RID: 4288 RVA: 0x0001C19C File Offset: 0x0001A39C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000C88 RID: 3208
			public int <>1__state;

			// Token: 0x04000C89 RID: 3209
			public AsyncTaskMethodBuilder<Image?> <>t__builder;

			// Token: 0x04000C8A RID: 3210
			public SteamId steamid;

			// Token: 0x04000C8B RID: 3211
			private TaskAwaiter <>u__1;
		}

		// Token: 0x0200021C RID: 540
		[CompilerGenerated]
		private sealed class <GetMediumAvatarAsync>d__45 : IAsyncStateMachine
		{
			// Token: 0x060010C1 RID: 4289 RVA: 0x0001C19E File Offset: 0x0001A39E
			public <GetMediumAvatarAsync>d__45()
			{
			}

			// Token: 0x060010C2 RID: 4290 RVA: 0x0001C1A8 File Offset: 0x0001A3A8
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				Image? image;
				try
				{
					TaskAwaiter taskAwaiter;
					if (num != 0)
					{
						taskAwaiter = SteamFriends.CacheUserInformationAsync(steamid, false).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter taskAwaiter2 = taskAwaiter;
							SteamFriends.<GetMediumAvatarAsync>d__45 <GetMediumAvatarAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, SteamFriends.<GetMediumAvatarAsync>d__45>(ref taskAwaiter, ref <GetMediumAvatarAsync>d__);
							return;
						}
					}
					else
					{
						TaskAwaiter taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter);
						num2 = -1;
					}
					taskAwaiter.GetResult();
					image = SteamUtils.GetImage(SteamFriends.Internal.GetMediumFriendAvatar(steamid));
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(image);
			}

			// Token: 0x060010C3 RID: 4291 RVA: 0x0001C280 File Offset: 0x0001A480
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000C8C RID: 3212
			public int <>1__state;

			// Token: 0x04000C8D RID: 3213
			public AsyncTaskMethodBuilder<Image?> <>t__builder;

			// Token: 0x04000C8E RID: 3214
			public SteamId steamid;

			// Token: 0x04000C8F RID: 3215
			private TaskAwaiter <>u__1;
		}

		// Token: 0x0200021D RID: 541
		[CompilerGenerated]
		private sealed class <GetLargeAvatarAsync>d__46 : IAsyncStateMachine
		{
			// Token: 0x060010C4 RID: 4292 RVA: 0x0001C282 File Offset: 0x0001A482
			public <GetLargeAvatarAsync>d__46()
			{
			}

			// Token: 0x060010C5 RID: 4293 RVA: 0x0001C28C File Offset: 0x0001A48C
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				Image? image;
				try
				{
					TaskAwaiter taskAwaiter;
					TaskAwaiter taskAwaiter3;
					if (num != 0)
					{
						if (num == 1)
						{
							TaskAwaiter taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter);
							num2 = -1;
							goto IL_F5;
						}
						taskAwaiter3 = SteamFriends.CacheUserInformationAsync(steamid, false).GetAwaiter();
						if (!taskAwaiter3.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter taskAwaiter2 = taskAwaiter3;
							SteamFriends.<GetLargeAvatarAsync>d__46 <GetLargeAvatarAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, SteamFriends.<GetLargeAvatarAsync>d__46>(ref taskAwaiter3, ref <GetLargeAvatarAsync>d__);
							return;
						}
					}
					else
					{
						TaskAwaiter taskAwaiter2;
						taskAwaiter3 = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter);
						num2 = -1;
					}
					taskAwaiter3.GetResult();
					imageid = SteamFriends.Internal.GetLargeFriendAvatar(steamid);
					goto IL_114;
					IL_F5:
					taskAwaiter.GetResult();
					imageid = SteamFriends.Internal.GetLargeFriendAvatar(steamid);
					IL_114:
					if (imageid != -1)
					{
						image = SteamUtils.GetImage(imageid);
					}
					else
					{
						taskAwaiter = Task.Delay(50).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 1;
							TaskAwaiter taskAwaiter2 = taskAwaiter;
							SteamFriends.<GetLargeAvatarAsync>d__46 <GetLargeAvatarAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, SteamFriends.<GetLargeAvatarAsync>d__46>(ref taskAwaiter, ref <GetLargeAvatarAsync>d__);
							return;
						}
						goto IL_F5;
					}
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(image);
			}

			// Token: 0x060010C6 RID: 4294 RVA: 0x0001C418 File Offset: 0x0001A618
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000C90 RID: 3216
			public int <>1__state;

			// Token: 0x04000C91 RID: 3217
			public AsyncTaskMethodBuilder<Image?> <>t__builder;

			// Token: 0x04000C92 RID: 3218
			public SteamId steamid;

			// Token: 0x04000C93 RID: 3219
			private int <imageid>5__1;

			// Token: 0x04000C94 RID: 3220
			private TaskAwaiter <>u__1;
		}

		// Token: 0x0200021E RID: 542
		[CompilerGenerated]
		private sealed class <IsFollowing>d__54 : IAsyncStateMachine
		{
			// Token: 0x060010C7 RID: 4295 RVA: 0x0001C41A File Offset: 0x0001A61A
			public <IsFollowing>d__54()
			{
			}

			// Token: 0x060010C8 RID: 4296 RVA: 0x0001C424 File Offset: 0x0001A624
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				bool isFollowing;
				try
				{
					CallResult<FriendsIsFollowing_t> callResult;
					if (num != 0)
					{
						callResult = SteamFriends.Internal.IsFollowing(steamID).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<FriendsIsFollowing_t> callResult2 = callResult;
							SteamFriends.<IsFollowing>d__54 <IsFollowing>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<FriendsIsFollowing_t>, SteamFriends.<IsFollowing>d__54>(ref callResult, ref <IsFollowing>d__);
							return;
						}
					}
					else
					{
						CallResult<FriendsIsFollowing_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<FriendsIsFollowing_t>);
						num2 = -1;
					}
					friendsIsFollowing_t = callResult.GetResult();
					r = friendsIsFollowing_t;
					friendsIsFollowing_t = null;
					isFollowing = r.Value.IsFollowing;
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(isFollowing);
			}

			// Token: 0x060010C9 RID: 4297 RVA: 0x0001C520 File Offset: 0x0001A720
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000C95 RID: 3221
			public int <>1__state;

			// Token: 0x04000C96 RID: 3222
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000C97 RID: 3223
			public SteamId steamID;

			// Token: 0x04000C98 RID: 3224
			private FriendsIsFollowing_t? <r>5__1;

			// Token: 0x04000C99 RID: 3225
			private FriendsIsFollowing_t? <>s__2;

			// Token: 0x04000C9A RID: 3226
			private CallResult<FriendsIsFollowing_t> <>u__1;
		}

		// Token: 0x0200021F RID: 543
		[CompilerGenerated]
		private sealed class <GetFollowerCount>d__55 : IAsyncStateMachine
		{
			// Token: 0x060010CA RID: 4298 RVA: 0x0001C522 File Offset: 0x0001A722
			public <GetFollowerCount>d__55()
			{
			}

			// Token: 0x060010CB RID: 4299 RVA: 0x0001C52C File Offset: 0x0001A72C
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				int count;
				try
				{
					CallResult<FriendsGetFollowerCount_t> callResult;
					if (num != 0)
					{
						callResult = SteamFriends.Internal.GetFollowerCount(steamID).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<FriendsGetFollowerCount_t> callResult2 = callResult;
							SteamFriends.<GetFollowerCount>d__55 <GetFollowerCount>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<FriendsGetFollowerCount_t>, SteamFriends.<GetFollowerCount>d__55>(ref callResult, ref <GetFollowerCount>d__);
							return;
						}
					}
					else
					{
						CallResult<FriendsGetFollowerCount_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<FriendsGetFollowerCount_t>);
						num2 = -1;
					}
					friendsGetFollowerCount_t = callResult.GetResult();
					r = friendsGetFollowerCount_t;
					friendsGetFollowerCount_t = null;
					count = r.Value.Count;
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(count);
			}

			// Token: 0x060010CC RID: 4300 RVA: 0x0001C628 File Offset: 0x0001A828
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000C9B RID: 3227
			public int <>1__state;

			// Token: 0x04000C9C RID: 3228
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000C9D RID: 3229
			public SteamId steamID;

			// Token: 0x04000C9E RID: 3230
			private FriendsGetFollowerCount_t? <r>5__1;

			// Token: 0x04000C9F RID: 3231
			private FriendsGetFollowerCount_t? <>s__2;

			// Token: 0x04000CA0 RID: 3232
			private CallResult<FriendsGetFollowerCount_t> <>u__1;
		}

		// Token: 0x02000220 RID: 544
		[CompilerGenerated]
		private sealed class <>c__DisplayClass56_0
		{
			// Token: 0x060010CD RID: 4301 RVA: 0x0001C62A File Offset: 0x0001A82A
			public <>c__DisplayClass56_0()
			{
			}

			// Token: 0x060010CE RID: 4302 RVA: 0x0001C634 File Offset: 0x0001A834
			internal void <GetFollowingList>b__0(ulong id)
			{
				bool flag = id > 0UL;
				if (flag)
				{
					this.steamIds.Add(id);
				}
			}

			// Token: 0x04000CA1 RID: 3233
			public List<SteamId> steamIds;
		}

		// Token: 0x02000221 RID: 545
		[CompilerGenerated]
		private sealed class <GetFollowingList>d__56 : IAsyncStateMachine
		{
			// Token: 0x060010CF RID: 4303 RVA: 0x0001C65D File Offset: 0x0001A85D
			public <GetFollowingList>d__56()
			{
			}

			// Token: 0x060010D0 RID: 4304 RVA: 0x0001C668 File Offset: 0x0001A868
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				SteamId[] result2;
				try
				{
					CallResult<FriendsEnumerateFollowingList_t> callResult;
					if (num == 0)
					{
						CallResult<FriendsEnumerateFollowingList_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<FriendsEnumerateFollowingList_t>);
						num2 = -1;
						goto IL_99;
					}
					CS$<>8__locals1 = new SteamFriends.<>c__DisplayClass56_0();
					resultCount = 0;
					CS$<>8__locals1.steamIds = new List<SteamId>();
					IL_31:
					callResult = SteamFriends.Internal.EnumerateFollowingList((uint)resultCount).GetAwaiter();
					if (!callResult.IsCompleted)
					{
						num2 = 0;
						CallResult<FriendsEnumerateFollowingList_t> callResult2 = callResult;
						SteamFriends.<GetFollowingList>d__56 <GetFollowingList>d__ = this;
						this.<>t__builder.AwaitOnCompleted<CallResult<FriendsEnumerateFollowingList_t>, SteamFriends.<GetFollowingList>d__56>(ref callResult, ref <GetFollowingList>d__);
						return;
					}
					IL_99:
					friendsEnumerateFollowingList_t = callResult.GetResult();
					FriendsEnumerateFollowingList_t? friendsEnumerateFollowingList_t2 = result = friendsEnumerateFollowingList_t;
					bool flag = friendsEnumerateFollowingList_t2 != null;
					if (flag)
					{
						friendsEnumerateFollowingList_t = null;
						resultCount += result.Value.ResultsReturned;
						Array.ForEach<ulong>(result.Value.GSteamID, new Action<ulong>(CS$<>8__locals1.<GetFollowingList>b__0));
					}
					if (result != null && resultCount < result.Value.TotalResultCount)
					{
						goto IL_31;
					}
					result2 = CS$<>8__locals1.steamIds.ToArray();
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x060010D1 RID: 4305 RVA: 0x0001C81C File Offset: 0x0001AA1C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000CA2 RID: 3234
			public int <>1__state;

			// Token: 0x04000CA3 RID: 3235
			public AsyncTaskMethodBuilder<SteamId[]> <>t__builder;

			// Token: 0x04000CA4 RID: 3236
			private SteamFriends.<>c__DisplayClass56_0 <>8__1;

			// Token: 0x04000CA5 RID: 3237
			private int <resultCount>5__2;

			// Token: 0x04000CA6 RID: 3238
			private FriendsEnumerateFollowingList_t? <result>5__3;

			// Token: 0x04000CA7 RID: 3239
			private FriendsEnumerateFollowingList_t? <>s__4;

			// Token: 0x04000CA8 RID: 3240
			private CallResult<FriendsEnumerateFollowingList_t> <>u__1;
		}
	}
}
