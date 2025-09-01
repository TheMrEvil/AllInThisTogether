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
	// Token: 0x02000097 RID: 151
	public class SteamMatchmaking : SteamClientClass<SteamMatchmaking>
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x0000C664 File Offset: 0x0000A864
		internal static ISteamMatchmaking Internal
		{
			get
			{
				return SteamClientClass<SteamMatchmaking>.Interface as ISteamMatchmaking;
			}
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0000C670 File Offset: 0x0000A870
		internal override void InitializeInterface(bool server)
		{
			this.SetInterface(server, new ISteamMatchmaking(server));
			SteamMatchmaking.InstallEvents();
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0000C687 File Offset: 0x0000A887
		internal static int MaxLobbyKeyLength
		{
			get
			{
				return 255;
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0000C690 File Offset: 0x0000A890
		internal static void InstallEvents()
		{
			Dispatch.Install<LobbyInvite_t>(delegate(LobbyInvite_t x)
			{
				Action<Friend, Lobby> onLobbyInvite = SteamMatchmaking.OnLobbyInvite;
				if (onLobbyInvite != null)
				{
					onLobbyInvite(new Friend(x.SteamIDUser), new Lobby(x.SteamIDLobby));
				}
			}, false);
			Dispatch.Install<LobbyEnter_t>(delegate(LobbyEnter_t x)
			{
				Action<Lobby> onLobbyEntered = SteamMatchmaking.OnLobbyEntered;
				if (onLobbyEntered != null)
				{
					onLobbyEntered(new Lobby(x.SteamIDLobby));
				}
			}, false);
			Dispatch.Install<LobbyCreated_t>(delegate(LobbyCreated_t x)
			{
				Action<Result, Lobby> onLobbyCreated = SteamMatchmaking.OnLobbyCreated;
				if (onLobbyCreated != null)
				{
					onLobbyCreated(x.Result, new Lobby(x.SteamIDLobby));
				}
			}, false);
			Dispatch.Install<LobbyGameCreated_t>(delegate(LobbyGameCreated_t x)
			{
				Action<Lobby, uint, ushort, SteamId> onLobbyGameCreated = SteamMatchmaking.OnLobbyGameCreated;
				if (onLobbyGameCreated != null)
				{
					onLobbyGameCreated(new Lobby(x.SteamIDLobby), x.IP, x.Port, x.SteamIDGameServer);
				}
			}, false);
			Dispatch.Install<LobbyDataUpdate_t>(delegate(LobbyDataUpdate_t x)
			{
				bool flag = x.Success == 0;
				if (!flag)
				{
					bool flag2 = x.SteamIDLobby == x.SteamIDMember;
					if (flag2)
					{
						Action<Lobby> onLobbyDataChanged = SteamMatchmaking.OnLobbyDataChanged;
						if (onLobbyDataChanged != null)
						{
							onLobbyDataChanged(new Lobby(x.SteamIDLobby));
						}
					}
					else
					{
						Action<Lobby, Friend> onLobbyMemberDataChanged = SteamMatchmaking.OnLobbyMemberDataChanged;
						if (onLobbyMemberDataChanged != null)
						{
							onLobbyMemberDataChanged(new Lobby(x.SteamIDLobby), new Friend(x.SteamIDMember));
						}
					}
				}
			}, false);
			Dispatch.Install<LobbyChatUpdate_t>(delegate(LobbyChatUpdate_t x)
			{
				bool flag = (x.GfChatMemberStateChange & 1U) > 0U;
				if (flag)
				{
					Action<Lobby, Friend> onLobbyMemberJoined = SteamMatchmaking.OnLobbyMemberJoined;
					if (onLobbyMemberJoined != null)
					{
						onLobbyMemberJoined(new Lobby(x.SteamIDLobby), new Friend(x.SteamIDUserChanged));
					}
				}
				bool flag2 = (x.GfChatMemberStateChange & 2U) > 0U;
				if (flag2)
				{
					Action<Lobby, Friend> onLobbyMemberLeave = SteamMatchmaking.OnLobbyMemberLeave;
					if (onLobbyMemberLeave != null)
					{
						onLobbyMemberLeave(new Lobby(x.SteamIDLobby), new Friend(x.SteamIDUserChanged));
					}
				}
				bool flag3 = (x.GfChatMemberStateChange & 4U) > 0U;
				if (flag3)
				{
					Action<Lobby, Friend> onLobbyMemberDisconnected = SteamMatchmaking.OnLobbyMemberDisconnected;
					if (onLobbyMemberDisconnected != null)
					{
						onLobbyMemberDisconnected(new Lobby(x.SteamIDLobby), new Friend(x.SteamIDUserChanged));
					}
				}
				bool flag4 = (x.GfChatMemberStateChange & 8U) > 0U;
				if (flag4)
				{
					Action<Lobby, Friend, Friend> onLobbyMemberKicked = SteamMatchmaking.OnLobbyMemberKicked;
					if (onLobbyMemberKicked != null)
					{
						onLobbyMemberKicked(new Lobby(x.SteamIDLobby), new Friend(x.SteamIDUserChanged), new Friend(x.SteamIDMakingChange));
					}
				}
				bool flag5 = (x.GfChatMemberStateChange & 16U) > 0U;
				if (flag5)
				{
					Action<Lobby, Friend, Friend> onLobbyMemberBanned = SteamMatchmaking.OnLobbyMemberBanned;
					if (onLobbyMemberBanned != null)
					{
						onLobbyMemberBanned(new Lobby(x.SteamIDLobby), new Friend(x.SteamIDUserChanged), new Friend(x.SteamIDMakingChange));
					}
				}
			}, false);
			Dispatch.Install<LobbyChatMsg_t>(new Action<LobbyChatMsg_t>(SteamMatchmaking.OnLobbyChatMessageRecievedAPI), false);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0000C798 File Offset: 0x0000A998
		private unsafe static void OnLobbyChatMessageRecievedAPI(LobbyChatMsg_t callback)
		{
			SteamId steamid = default(SteamId);
			ChatEntryType chatEntryType = ChatEntryType.Invalid;
			byte[] array = Helpers.TakeBuffer(4096);
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
			int lobbyChatEntry = SteamMatchmaking.Internal.GetLobbyChatEntry(callback.SteamIDLobby, (int)callback.ChatID, ref steamid, (IntPtr)((void*)value), array.Length, ref chatEntryType);
			bool flag = lobbyChatEntry > 0;
			if (flag)
			{
				Action<Lobby, Friend, string> onChatMessage = SteamMatchmaking.OnChatMessage;
				if (onChatMessage != null)
				{
					onChatMessage(new Lobby(callback.SteamIDLobby), new Friend(steamid), Encoding.UTF8.GetString(array, 0, lobbyChatEntry));
				}
			}
			array2 = null;
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060007C7 RID: 1991 RVA: 0x0000C848 File Offset: 0x0000AA48
		// (remove) Token: 0x060007C8 RID: 1992 RVA: 0x0000C87C File Offset: 0x0000AA7C
		public static event Action<Friend, Lobby> OnLobbyInvite
		{
			[CompilerGenerated]
			add
			{
				Action<Friend, Lobby> action = SteamMatchmaking.OnLobbyInvite;
				Action<Friend, Lobby> action2;
				do
				{
					action2 = action;
					Action<Friend, Lobby> value2 = (Action<Friend, Lobby>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Friend, Lobby>>(ref SteamMatchmaking.OnLobbyInvite, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Friend, Lobby> action = SteamMatchmaking.OnLobbyInvite;
				Action<Friend, Lobby> action2;
				do
				{
					action2 = action;
					Action<Friend, Lobby> value2 = (Action<Friend, Lobby>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Friend, Lobby>>(ref SteamMatchmaking.OnLobbyInvite, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060007C9 RID: 1993 RVA: 0x0000C8B0 File Offset: 0x0000AAB0
		// (remove) Token: 0x060007CA RID: 1994 RVA: 0x0000C8E4 File Offset: 0x0000AAE4
		public static event Action<Lobby> OnLobbyEntered
		{
			[CompilerGenerated]
			add
			{
				Action<Lobby> action = SteamMatchmaking.OnLobbyEntered;
				Action<Lobby> action2;
				do
				{
					action2 = action;
					Action<Lobby> value2 = (Action<Lobby>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Lobby>>(ref SteamMatchmaking.OnLobbyEntered, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Lobby> action = SteamMatchmaking.OnLobbyEntered;
				Action<Lobby> action2;
				do
				{
					action2 = action;
					Action<Lobby> value2 = (Action<Lobby>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Lobby>>(ref SteamMatchmaking.OnLobbyEntered, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060007CB RID: 1995 RVA: 0x0000C918 File Offset: 0x0000AB18
		// (remove) Token: 0x060007CC RID: 1996 RVA: 0x0000C94C File Offset: 0x0000AB4C
		public static event Action<Result, Lobby> OnLobbyCreated
		{
			[CompilerGenerated]
			add
			{
				Action<Result, Lobby> action = SteamMatchmaking.OnLobbyCreated;
				Action<Result, Lobby> action2;
				do
				{
					action2 = action;
					Action<Result, Lobby> value2 = (Action<Result, Lobby>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Result, Lobby>>(ref SteamMatchmaking.OnLobbyCreated, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Result, Lobby> action = SteamMatchmaking.OnLobbyCreated;
				Action<Result, Lobby> action2;
				do
				{
					action2 = action;
					Action<Result, Lobby> value2 = (Action<Result, Lobby>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Result, Lobby>>(ref SteamMatchmaking.OnLobbyCreated, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060007CD RID: 1997 RVA: 0x0000C980 File Offset: 0x0000AB80
		// (remove) Token: 0x060007CE RID: 1998 RVA: 0x0000C9B4 File Offset: 0x0000ABB4
		public static event Action<Lobby, uint, ushort, SteamId> OnLobbyGameCreated
		{
			[CompilerGenerated]
			add
			{
				Action<Lobby, uint, ushort, SteamId> action = SteamMatchmaking.OnLobbyGameCreated;
				Action<Lobby, uint, ushort, SteamId> action2;
				do
				{
					action2 = action;
					Action<Lobby, uint, ushort, SteamId> value2 = (Action<Lobby, uint, ushort, SteamId>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Lobby, uint, ushort, SteamId>>(ref SteamMatchmaking.OnLobbyGameCreated, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Lobby, uint, ushort, SteamId> action = SteamMatchmaking.OnLobbyGameCreated;
				Action<Lobby, uint, ushort, SteamId> action2;
				do
				{
					action2 = action;
					Action<Lobby, uint, ushort, SteamId> value2 = (Action<Lobby, uint, ushort, SteamId>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Lobby, uint, ushort, SteamId>>(ref SteamMatchmaking.OnLobbyGameCreated, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060007CF RID: 1999 RVA: 0x0000C9E8 File Offset: 0x0000ABE8
		// (remove) Token: 0x060007D0 RID: 2000 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		public static event Action<Lobby> OnLobbyDataChanged
		{
			[CompilerGenerated]
			add
			{
				Action<Lobby> action = SteamMatchmaking.OnLobbyDataChanged;
				Action<Lobby> action2;
				do
				{
					action2 = action;
					Action<Lobby> value2 = (Action<Lobby>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Lobby>>(ref SteamMatchmaking.OnLobbyDataChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Lobby> action = SteamMatchmaking.OnLobbyDataChanged;
				Action<Lobby> action2;
				do
				{
					action2 = action;
					Action<Lobby> value2 = (Action<Lobby>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Lobby>>(ref SteamMatchmaking.OnLobbyDataChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060007D1 RID: 2001 RVA: 0x0000CA50 File Offset: 0x0000AC50
		// (remove) Token: 0x060007D2 RID: 2002 RVA: 0x0000CA84 File Offset: 0x0000AC84
		public static event Action<Lobby, Friend> OnLobbyMemberDataChanged
		{
			[CompilerGenerated]
			add
			{
				Action<Lobby, Friend> action = SteamMatchmaking.OnLobbyMemberDataChanged;
				Action<Lobby, Friend> action2;
				do
				{
					action2 = action;
					Action<Lobby, Friend> value2 = (Action<Lobby, Friend>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Lobby, Friend>>(ref SteamMatchmaking.OnLobbyMemberDataChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Lobby, Friend> action = SteamMatchmaking.OnLobbyMemberDataChanged;
				Action<Lobby, Friend> action2;
				do
				{
					action2 = action;
					Action<Lobby, Friend> value2 = (Action<Lobby, Friend>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Lobby, Friend>>(ref SteamMatchmaking.OnLobbyMemberDataChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060007D3 RID: 2003 RVA: 0x0000CAB8 File Offset: 0x0000ACB8
		// (remove) Token: 0x060007D4 RID: 2004 RVA: 0x0000CAEC File Offset: 0x0000ACEC
		public static event Action<Lobby, Friend> OnLobbyMemberJoined
		{
			[CompilerGenerated]
			add
			{
				Action<Lobby, Friend> action = SteamMatchmaking.OnLobbyMemberJoined;
				Action<Lobby, Friend> action2;
				do
				{
					action2 = action;
					Action<Lobby, Friend> value2 = (Action<Lobby, Friend>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Lobby, Friend>>(ref SteamMatchmaking.OnLobbyMemberJoined, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Lobby, Friend> action = SteamMatchmaking.OnLobbyMemberJoined;
				Action<Lobby, Friend> action2;
				do
				{
					action2 = action;
					Action<Lobby, Friend> value2 = (Action<Lobby, Friend>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Lobby, Friend>>(ref SteamMatchmaking.OnLobbyMemberJoined, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060007D5 RID: 2005 RVA: 0x0000CB20 File Offset: 0x0000AD20
		// (remove) Token: 0x060007D6 RID: 2006 RVA: 0x0000CB54 File Offset: 0x0000AD54
		public static event Action<Lobby, Friend> OnLobbyMemberLeave
		{
			[CompilerGenerated]
			add
			{
				Action<Lobby, Friend> action = SteamMatchmaking.OnLobbyMemberLeave;
				Action<Lobby, Friend> action2;
				do
				{
					action2 = action;
					Action<Lobby, Friend> value2 = (Action<Lobby, Friend>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Lobby, Friend>>(ref SteamMatchmaking.OnLobbyMemberLeave, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Lobby, Friend> action = SteamMatchmaking.OnLobbyMemberLeave;
				Action<Lobby, Friend> action2;
				do
				{
					action2 = action;
					Action<Lobby, Friend> value2 = (Action<Lobby, Friend>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Lobby, Friend>>(ref SteamMatchmaking.OnLobbyMemberLeave, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060007D7 RID: 2007 RVA: 0x0000CB88 File Offset: 0x0000AD88
		// (remove) Token: 0x060007D8 RID: 2008 RVA: 0x0000CBBC File Offset: 0x0000ADBC
		public static event Action<Lobby, Friend> OnLobbyMemberDisconnected
		{
			[CompilerGenerated]
			add
			{
				Action<Lobby, Friend> action = SteamMatchmaking.OnLobbyMemberDisconnected;
				Action<Lobby, Friend> action2;
				do
				{
					action2 = action;
					Action<Lobby, Friend> value2 = (Action<Lobby, Friend>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Lobby, Friend>>(ref SteamMatchmaking.OnLobbyMemberDisconnected, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Lobby, Friend> action = SteamMatchmaking.OnLobbyMemberDisconnected;
				Action<Lobby, Friend> action2;
				do
				{
					action2 = action;
					Action<Lobby, Friend> value2 = (Action<Lobby, Friend>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Lobby, Friend>>(ref SteamMatchmaking.OnLobbyMemberDisconnected, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060007D9 RID: 2009 RVA: 0x0000CBF0 File Offset: 0x0000ADF0
		// (remove) Token: 0x060007DA RID: 2010 RVA: 0x0000CC24 File Offset: 0x0000AE24
		public static event Action<Lobby, Friend, Friend> OnLobbyMemberKicked
		{
			[CompilerGenerated]
			add
			{
				Action<Lobby, Friend, Friend> action = SteamMatchmaking.OnLobbyMemberKicked;
				Action<Lobby, Friend, Friend> action2;
				do
				{
					action2 = action;
					Action<Lobby, Friend, Friend> value2 = (Action<Lobby, Friend, Friend>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Lobby, Friend, Friend>>(ref SteamMatchmaking.OnLobbyMemberKicked, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Lobby, Friend, Friend> action = SteamMatchmaking.OnLobbyMemberKicked;
				Action<Lobby, Friend, Friend> action2;
				do
				{
					action2 = action;
					Action<Lobby, Friend, Friend> value2 = (Action<Lobby, Friend, Friend>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Lobby, Friend, Friend>>(ref SteamMatchmaking.OnLobbyMemberKicked, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060007DB RID: 2011 RVA: 0x0000CC58 File Offset: 0x0000AE58
		// (remove) Token: 0x060007DC RID: 2012 RVA: 0x0000CC8C File Offset: 0x0000AE8C
		public static event Action<Lobby, Friend, Friend> OnLobbyMemberBanned
		{
			[CompilerGenerated]
			add
			{
				Action<Lobby, Friend, Friend> action = SteamMatchmaking.OnLobbyMemberBanned;
				Action<Lobby, Friend, Friend> action2;
				do
				{
					action2 = action;
					Action<Lobby, Friend, Friend> value2 = (Action<Lobby, Friend, Friend>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Lobby, Friend, Friend>>(ref SteamMatchmaking.OnLobbyMemberBanned, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Lobby, Friend, Friend> action = SteamMatchmaking.OnLobbyMemberBanned;
				Action<Lobby, Friend, Friend> action2;
				do
				{
					action2 = action;
					Action<Lobby, Friend, Friend> value2 = (Action<Lobby, Friend, Friend>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Lobby, Friend, Friend>>(ref SteamMatchmaking.OnLobbyMemberBanned, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060007DD RID: 2013 RVA: 0x0000CCC0 File Offset: 0x0000AEC0
		// (remove) Token: 0x060007DE RID: 2014 RVA: 0x0000CCF4 File Offset: 0x0000AEF4
		public static event Action<Lobby, Friend, string> OnChatMessage
		{
			[CompilerGenerated]
			add
			{
				Action<Lobby, Friend, string> action = SteamMatchmaking.OnChatMessage;
				Action<Lobby, Friend, string> action2;
				do
				{
					action2 = action;
					Action<Lobby, Friend, string> value2 = (Action<Lobby, Friend, string>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Lobby, Friend, string>>(ref SteamMatchmaking.OnChatMessage, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Lobby, Friend, string> action = SteamMatchmaking.OnChatMessage;
				Action<Lobby, Friend, string> action2;
				do
				{
					action2 = action;
					Action<Lobby, Friend, string> value2 = (Action<Lobby, Friend, string>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Lobby, Friend, string>>(ref SteamMatchmaking.OnChatMessage, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x0000CD28 File Offset: 0x0000AF28
		public static LobbyQuery LobbyList
		{
			get
			{
				return default(LobbyQuery);
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0000CD40 File Offset: 0x0000AF40
		public static async Task<Lobby?> CreateLobbyAsync(int maxMembers = 100)
		{
			LobbyCreated_t? lobbyCreated_t = await SteamMatchmaking.Internal.CreateLobby(LobbyType.Invisible, maxMembers);
			LobbyCreated_t? lobby = lobbyCreated_t;
			lobbyCreated_t = null;
			Lobby? result;
			if (lobby == null || lobby.Value.Result != Result.OK)
			{
				result = null;
			}
			else
			{
				result = new Lobby?(new Lobby
				{
					Id = lobby.Value.SteamIDLobby
				});
			}
			return result;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0000CD88 File Offset: 0x0000AF88
		public static async Task<Lobby?> JoinLobbyAsync(SteamId lobbyId)
		{
			LobbyEnter_t? lobbyEnter_t = await SteamMatchmaking.Internal.JoinLobby(lobbyId);
			LobbyEnter_t? lobby = lobbyEnter_t;
			lobbyEnter_t = null;
			Lobby? result;
			if (lobby == null)
			{
				result = null;
			}
			else
			{
				result = new Lobby?(new Lobby
				{
					Id = lobby.Value.SteamIDLobby
				});
			}
			return result;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0000CDCF File Offset: 0x0000AFCF
		public static IEnumerable<ServerInfo> GetFavoriteServers()
		{
			int count = SteamMatchmaking.Internal.GetFavoriteGameCount();
			int i = 0;
			while (i < count)
			{
				uint timeplayed = 0U;
				uint flags = 0U;
				ushort qport = 0;
				ushort cport = 0;
				uint ip = 0U;
				AppId appid = default(AppId);
				bool favoriteGame = SteamMatchmaking.Internal.GetFavoriteGame(i, ref appid, ref ip, ref cport, ref qport, ref flags, ref timeplayed);
				if (favoriteGame)
				{
					bool flag = (flags & 1U) == 0U;
					if (!flag)
					{
						yield return new ServerInfo(ip, cport, qport, timeplayed);
					}
				}
				IL_EE:
				int num = i;
				i = num + 1;
				continue;
				goto IL_EE;
			}
			yield break;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0000CDD8 File Offset: 0x0000AFD8
		public static IEnumerable<ServerInfo> GetHistoryServers()
		{
			int count = SteamMatchmaking.Internal.GetFavoriteGameCount();
			int i = 0;
			while (i < count)
			{
				uint timeplayed = 0U;
				uint flags = 0U;
				ushort qport = 0;
				ushort cport = 0;
				uint ip = 0U;
				AppId appid = default(AppId);
				bool favoriteGame = SteamMatchmaking.Internal.GetFavoriteGame(i, ref appid, ref ip, ref cport, ref qport, ref flags, ref timeplayed);
				if (favoriteGame)
				{
					bool flag = (flags & 2U) == 0U;
					if (!flag)
					{
						yield return new ServerInfo(ip, cport, qport, timeplayed);
					}
				}
				IL_EE:
				int num = i;
				i = num + 1;
				continue;
				goto IL_EE;
			}
			yield break;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0000CDE1 File Offset: 0x0000AFE1
		public SteamMatchmaking()
		{
		}

		// Token: 0x040006F6 RID: 1782
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Friend, Lobby> OnLobbyInvite;

		// Token: 0x040006F7 RID: 1783
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Lobby> OnLobbyEntered;

		// Token: 0x040006F8 RID: 1784
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Result, Lobby> OnLobbyCreated;

		// Token: 0x040006F9 RID: 1785
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Lobby, uint, ushort, SteamId> OnLobbyGameCreated;

		// Token: 0x040006FA RID: 1786
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Lobby> OnLobbyDataChanged;

		// Token: 0x040006FB RID: 1787
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Lobby, Friend> OnLobbyMemberDataChanged;

		// Token: 0x040006FC RID: 1788
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Lobby, Friend> OnLobbyMemberJoined;

		// Token: 0x040006FD RID: 1789
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Lobby, Friend> OnLobbyMemberLeave;

		// Token: 0x040006FE RID: 1790
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Lobby, Friend> OnLobbyMemberDisconnected;

		// Token: 0x040006FF RID: 1791
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Lobby, Friend, Friend> OnLobbyMemberKicked;

		// Token: 0x04000700 RID: 1792
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Lobby, Friend, Friend> OnLobbyMemberBanned;

		// Token: 0x04000701 RID: 1793
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Lobby, Friend, string> OnChatMessage;

		// Token: 0x0200022F RID: 559
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001107 RID: 4359 RVA: 0x0001D9DE File Offset: 0x0001BBDE
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001108 RID: 4360 RVA: 0x0001D9EA File Offset: 0x0001BBEA
			public <>c()
			{
			}

			// Token: 0x06001109 RID: 4361 RVA: 0x0001D9F3 File Offset: 0x0001BBF3
			internal void <InstallEvents>b__5_0(LobbyInvite_t x)
			{
				Action<Friend, Lobby> onLobbyInvite = SteamMatchmaking.OnLobbyInvite;
				if (onLobbyInvite != null)
				{
					onLobbyInvite(new Friend(x.SteamIDUser), new Lobby(x.SteamIDLobby));
				}
			}

			// Token: 0x0600110A RID: 4362 RVA: 0x0001DA26 File Offset: 0x0001BC26
			internal void <InstallEvents>b__5_1(LobbyEnter_t x)
			{
				Action<Lobby> onLobbyEntered = SteamMatchmaking.OnLobbyEntered;
				if (onLobbyEntered != null)
				{
					onLobbyEntered(new Lobby(x.SteamIDLobby));
				}
			}

			// Token: 0x0600110B RID: 4363 RVA: 0x0001DA49 File Offset: 0x0001BC49
			internal void <InstallEvents>b__5_2(LobbyCreated_t x)
			{
				Action<Result, Lobby> onLobbyCreated = SteamMatchmaking.OnLobbyCreated;
				if (onLobbyCreated != null)
				{
					onLobbyCreated(x.Result, new Lobby(x.SteamIDLobby));
				}
			}

			// Token: 0x0600110C RID: 4364 RVA: 0x0001DA72 File Offset: 0x0001BC72
			internal void <InstallEvents>b__5_3(LobbyGameCreated_t x)
			{
				Action<Lobby, uint, ushort, SteamId> onLobbyGameCreated = SteamMatchmaking.OnLobbyGameCreated;
				if (onLobbyGameCreated != null)
				{
					onLobbyGameCreated(new Lobby(x.SteamIDLobby), x.IP, x.Port, x.SteamIDGameServer);
				}
			}

			// Token: 0x0600110D RID: 4365 RVA: 0x0001DAAC File Offset: 0x0001BCAC
			internal void <InstallEvents>b__5_4(LobbyDataUpdate_t x)
			{
				bool flag = x.Success == 0;
				if (!flag)
				{
					bool flag2 = x.SteamIDLobby == x.SteamIDMember;
					if (flag2)
					{
						Action<Lobby> onLobbyDataChanged = SteamMatchmaking.OnLobbyDataChanged;
						if (onLobbyDataChanged != null)
						{
							onLobbyDataChanged(new Lobby(x.SteamIDLobby));
						}
					}
					else
					{
						Action<Lobby, Friend> onLobbyMemberDataChanged = SteamMatchmaking.OnLobbyMemberDataChanged;
						if (onLobbyMemberDataChanged != null)
						{
							onLobbyMemberDataChanged(new Lobby(x.SteamIDLobby), new Friend(x.SteamIDMember));
						}
					}
				}
			}

			// Token: 0x0600110E RID: 4366 RVA: 0x0001DB30 File Offset: 0x0001BD30
			internal void <InstallEvents>b__5_5(LobbyChatUpdate_t x)
			{
				bool flag = (x.GfChatMemberStateChange & 1U) > 0U;
				if (flag)
				{
					Action<Lobby, Friend> onLobbyMemberJoined = SteamMatchmaking.OnLobbyMemberJoined;
					if (onLobbyMemberJoined != null)
					{
						onLobbyMemberJoined(new Lobby(x.SteamIDLobby), new Friend(x.SteamIDUserChanged));
					}
				}
				bool flag2 = (x.GfChatMemberStateChange & 2U) > 0U;
				if (flag2)
				{
					Action<Lobby, Friend> onLobbyMemberLeave = SteamMatchmaking.OnLobbyMemberLeave;
					if (onLobbyMemberLeave != null)
					{
						onLobbyMemberLeave(new Lobby(x.SteamIDLobby), new Friend(x.SteamIDUserChanged));
					}
				}
				bool flag3 = (x.GfChatMemberStateChange & 4U) > 0U;
				if (flag3)
				{
					Action<Lobby, Friend> onLobbyMemberDisconnected = SteamMatchmaking.OnLobbyMemberDisconnected;
					if (onLobbyMemberDisconnected != null)
					{
						onLobbyMemberDisconnected(new Lobby(x.SteamIDLobby), new Friend(x.SteamIDUserChanged));
					}
				}
				bool flag4 = (x.GfChatMemberStateChange & 8U) > 0U;
				if (flag4)
				{
					Action<Lobby, Friend, Friend> onLobbyMemberKicked = SteamMatchmaking.OnLobbyMemberKicked;
					if (onLobbyMemberKicked != null)
					{
						onLobbyMemberKicked(new Lobby(x.SteamIDLobby), new Friend(x.SteamIDUserChanged), new Friend(x.SteamIDMakingChange));
					}
				}
				bool flag5 = (x.GfChatMemberStateChange & 16U) > 0U;
				if (flag5)
				{
					Action<Lobby, Friend, Friend> onLobbyMemberBanned = SteamMatchmaking.OnLobbyMemberBanned;
					if (onLobbyMemberBanned != null)
					{
						onLobbyMemberBanned(new Lobby(x.SteamIDLobby), new Friend(x.SteamIDUserChanged), new Friend(x.SteamIDMakingChange));
					}
				}
			}

			// Token: 0x04000D0D RID: 3341
			public static readonly SteamMatchmaking.<>c <>9 = new SteamMatchmaking.<>c();

			// Token: 0x04000D0E RID: 3342
			public static Action<LobbyInvite_t> <>9__5_0;

			// Token: 0x04000D0F RID: 3343
			public static Action<LobbyEnter_t> <>9__5_1;

			// Token: 0x04000D10 RID: 3344
			public static Action<LobbyCreated_t> <>9__5_2;

			// Token: 0x04000D11 RID: 3345
			public static Action<LobbyGameCreated_t> <>9__5_3;

			// Token: 0x04000D12 RID: 3346
			public static Action<LobbyDataUpdate_t> <>9__5_4;

			// Token: 0x04000D13 RID: 3347
			public static Action<LobbyChatUpdate_t> <>9__5_5;
		}

		// Token: 0x02000230 RID: 560
		[CompilerGenerated]
		private sealed class <CreateLobbyAsync>d__45 : IAsyncStateMachine
		{
			// Token: 0x0600110F RID: 4367 RVA: 0x0001DCA1 File Offset: 0x0001BEA1
			public <CreateLobbyAsync>d__45()
			{
			}

			// Token: 0x06001110 RID: 4368 RVA: 0x0001DCAC File Offset: 0x0001BEAC
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				Lobby? result;
				try
				{
					CallResult<LobbyCreated_t> callResult;
					if (num != 0)
					{
						callResult = SteamMatchmaking.Internal.CreateLobby(LobbyType.Invisible, maxMembers).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<LobbyCreated_t> callResult2 = callResult;
							SteamMatchmaking.<CreateLobbyAsync>d__45 <CreateLobbyAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<LobbyCreated_t>, SteamMatchmaking.<CreateLobbyAsync>d__45>(ref callResult, ref <CreateLobbyAsync>d__);
							return;
						}
					}
					else
					{
						CallResult<LobbyCreated_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<LobbyCreated_t>);
						num2 = -1;
					}
					lobbyCreated_t = callResult.GetResult();
					lobby = lobbyCreated_t;
					lobbyCreated_t = null;
					bool flag = lobby == null || lobby.Value.Result != Result.OK;
					if (flag)
					{
						result = null;
					}
					else
					{
						result = new Lobby?(new Lobby
						{
							Id = lobby.Value.SteamIDLobby
						});
					}
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001111 RID: 4369 RVA: 0x0001DDF8 File Offset: 0x0001BFF8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000D14 RID: 3348
			public int <>1__state;

			// Token: 0x04000D15 RID: 3349
			public AsyncTaskMethodBuilder<Lobby?> <>t__builder;

			// Token: 0x04000D16 RID: 3350
			public int maxMembers;

			// Token: 0x04000D17 RID: 3351
			private LobbyCreated_t? <lobby>5__1;

			// Token: 0x04000D18 RID: 3352
			private LobbyCreated_t? <>s__2;

			// Token: 0x04000D19 RID: 3353
			private CallResult<LobbyCreated_t> <>u__1;
		}

		// Token: 0x02000231 RID: 561
		[CompilerGenerated]
		private sealed class <JoinLobbyAsync>d__46 : IAsyncStateMachine
		{
			// Token: 0x06001112 RID: 4370 RVA: 0x0001DDFA File Offset: 0x0001BFFA
			public <JoinLobbyAsync>d__46()
			{
			}

			// Token: 0x06001113 RID: 4371 RVA: 0x0001DE04 File Offset: 0x0001C004
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				Lobby? result;
				try
				{
					CallResult<LobbyEnter_t> callResult;
					if (num != 0)
					{
						callResult = SteamMatchmaking.Internal.JoinLobby(lobbyId).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<LobbyEnter_t> callResult2 = callResult;
							SteamMatchmaking.<JoinLobbyAsync>d__46 <JoinLobbyAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<LobbyEnter_t>, SteamMatchmaking.<JoinLobbyAsync>d__46>(ref callResult, ref <JoinLobbyAsync>d__);
							return;
						}
					}
					else
					{
						CallResult<LobbyEnter_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<LobbyEnter_t>);
						num2 = -1;
					}
					lobbyEnter_t = callResult.GetResult();
					lobby = lobbyEnter_t;
					lobbyEnter_t = null;
					bool flag = lobby == null;
					if (flag)
					{
						result = null;
					}
					else
					{
						result = new Lobby?(new Lobby
						{
							Id = lobby.Value.SteamIDLobby
						});
					}
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001114 RID: 4372 RVA: 0x0001DF38 File Offset: 0x0001C138
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000D1A RID: 3354
			public int <>1__state;

			// Token: 0x04000D1B RID: 3355
			public AsyncTaskMethodBuilder<Lobby?> <>t__builder;

			// Token: 0x04000D1C RID: 3356
			public SteamId lobbyId;

			// Token: 0x04000D1D RID: 3357
			private LobbyEnter_t? <lobby>5__1;

			// Token: 0x04000D1E RID: 3358
			private LobbyEnter_t? <>s__2;

			// Token: 0x04000D1F RID: 3359
			private CallResult<LobbyEnter_t> <>u__1;
		}

		// Token: 0x02000232 RID: 562
		[CompilerGenerated]
		private sealed class <GetFavoriteServers>d__47 : IEnumerable<ServerInfo>, IEnumerable, IEnumerator<ServerInfo>, IDisposable, IEnumerator
		{
			// Token: 0x06001115 RID: 4373 RVA: 0x0001DF3A File Offset: 0x0001C13A
			[DebuggerHidden]
			public <GetFavoriteServers>d__47(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06001116 RID: 4374 RVA: 0x0001DF55 File Offset: 0x0001C155
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06001117 RID: 4375 RVA: 0x0001DF58 File Offset: 0x0001C158
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num == 0)
				{
					this.<>1__state = -1;
					count = SteamMatchmaking.Internal.GetFavoriteGameCount();
					i = 0;
					goto IL_FE;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				IL_ED:
				IL_EE:
				int num2 = i;
				i = num2 + 1;
				IL_FE:
				if (i >= count)
				{
					return false;
				}
				timeplayed = 0U;
				flags = 0U;
				qport = 0;
				cport = 0;
				ip = 0U;
				appid = default(AppId);
				bool favoriteGame = SteamMatchmaking.Internal.GetFavoriteGame(i, ref appid, ref ip, ref cport, ref qport, ref flags, ref timeplayed);
				if (!favoriteGame)
				{
					goto IL_ED;
				}
				bool flag = (flags & 1U) == 0U;
				if (flag)
				{
					goto IL_EE;
				}
				this.<>2__current = new ServerInfo(ip, cport, qport, timeplayed);
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170002F6 RID: 758
			// (get) Token: 0x06001118 RID: 4376 RVA: 0x0001E07B File Offset: 0x0001C27B
			ServerInfo IEnumerator<ServerInfo>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001119 RID: 4377 RVA: 0x0001E083 File Offset: 0x0001C283
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170002F7 RID: 759
			// (get) Token: 0x0600111A RID: 4378 RVA: 0x0001E08A File Offset: 0x0001C28A
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600111B RID: 4379 RVA: 0x0001E098 File Offset: 0x0001C298
			[DebuggerHidden]
			IEnumerator<ServerInfo> IEnumerable<ServerInfo>.GetEnumerator()
			{
				SteamMatchmaking.<GetFavoriteServers>d__47 result;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					result = this;
				}
				else
				{
					result = new SteamMatchmaking.<GetFavoriteServers>d__47(0);
				}
				return result;
			}

			// Token: 0x0600111C RID: 4380 RVA: 0x0001E0CF File Offset: 0x0001C2CF
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Steamworks.Data.ServerInfo>.GetEnumerator();
			}

			// Token: 0x04000D20 RID: 3360
			private int <>1__state;

			// Token: 0x04000D21 RID: 3361
			private ServerInfo <>2__current;

			// Token: 0x04000D22 RID: 3362
			private int <>l__initialThreadId;

			// Token: 0x04000D23 RID: 3363
			private int <count>5__1;

			// Token: 0x04000D24 RID: 3364
			private int <i>5__2;

			// Token: 0x04000D25 RID: 3365
			private uint <timeplayed>5__3;

			// Token: 0x04000D26 RID: 3366
			private uint <flags>5__4;

			// Token: 0x04000D27 RID: 3367
			private ushort <qport>5__5;

			// Token: 0x04000D28 RID: 3368
			private ushort <cport>5__6;

			// Token: 0x04000D29 RID: 3369
			private uint <ip>5__7;

			// Token: 0x04000D2A RID: 3370
			private AppId <appid>5__8;
		}

		// Token: 0x02000233 RID: 563
		[CompilerGenerated]
		private sealed class <GetHistoryServers>d__48 : IEnumerable<ServerInfo>, IEnumerable, IEnumerator<ServerInfo>, IDisposable, IEnumerator
		{
			// Token: 0x0600111D RID: 4381 RVA: 0x0001E0D7 File Offset: 0x0001C2D7
			[DebuggerHidden]
			public <GetHistoryServers>d__48(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600111E RID: 4382 RVA: 0x0001E0F2 File Offset: 0x0001C2F2
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600111F RID: 4383 RVA: 0x0001E0F4 File Offset: 0x0001C2F4
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num == 0)
				{
					this.<>1__state = -1;
					count = SteamMatchmaking.Internal.GetFavoriteGameCount();
					i = 0;
					goto IL_FE;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				IL_ED:
				IL_EE:
				int num2 = i;
				i = num2 + 1;
				IL_FE:
				if (i >= count)
				{
					return false;
				}
				timeplayed = 0U;
				flags = 0U;
				qport = 0;
				cport = 0;
				ip = 0U;
				appid = default(AppId);
				bool favoriteGame = SteamMatchmaking.Internal.GetFavoriteGame(i, ref appid, ref ip, ref cport, ref qport, ref flags, ref timeplayed);
				if (!favoriteGame)
				{
					goto IL_ED;
				}
				bool flag = (flags & 2U) == 0U;
				if (flag)
				{
					goto IL_EE;
				}
				this.<>2__current = new ServerInfo(ip, cport, qport, timeplayed);
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170002F8 RID: 760
			// (get) Token: 0x06001120 RID: 4384 RVA: 0x0001E217 File Offset: 0x0001C417
			ServerInfo IEnumerator<ServerInfo>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001121 RID: 4385 RVA: 0x0001E21F File Offset: 0x0001C41F
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170002F9 RID: 761
			// (get) Token: 0x06001122 RID: 4386 RVA: 0x0001E226 File Offset: 0x0001C426
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001123 RID: 4387 RVA: 0x0001E234 File Offset: 0x0001C434
			[DebuggerHidden]
			IEnumerator<ServerInfo> IEnumerable<ServerInfo>.GetEnumerator()
			{
				SteamMatchmaking.<GetHistoryServers>d__48 result;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					result = this;
				}
				else
				{
					result = new SteamMatchmaking.<GetHistoryServers>d__48(0);
				}
				return result;
			}

			// Token: 0x06001124 RID: 4388 RVA: 0x0001E26B File Offset: 0x0001C46B
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Steamworks.Data.ServerInfo>.GetEnumerator();
			}

			// Token: 0x04000D2B RID: 3371
			private int <>1__state;

			// Token: 0x04000D2C RID: 3372
			private ServerInfo <>2__current;

			// Token: 0x04000D2D RID: 3373
			private int <>l__initialThreadId;

			// Token: 0x04000D2E RID: 3374
			private int <count>5__1;

			// Token: 0x04000D2F RID: 3375
			private int <i>5__2;

			// Token: 0x04000D30 RID: 3376
			private uint <timeplayed>5__3;

			// Token: 0x04000D31 RID: 3377
			private uint <flags>5__4;

			// Token: 0x04000D32 RID: 3378
			private ushort <qport>5__5;

			// Token: 0x04000D33 RID: 3379
			private ushort <cport>5__6;

			// Token: 0x04000D34 RID: 3380
			private uint <ip>5__7;

			// Token: 0x04000D35 RID: 3381
			private AppId <appid>5__8;
		}
	}
}
