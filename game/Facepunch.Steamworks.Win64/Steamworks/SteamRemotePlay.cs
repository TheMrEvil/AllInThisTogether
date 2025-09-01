using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x0200009F RID: 159
	public class SteamRemotePlay : SteamClientClass<SteamRemotePlay>
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x0000DDDD File Offset: 0x0000BFDD
		internal static ISteamRemotePlay Internal
		{
			get
			{
				return SteamClientClass<SteamRemotePlay>.Interface as ISteamRemotePlay;
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0000DDE9 File Offset: 0x0000BFE9
		internal override void InitializeInterface(bool server)
		{
			this.SetInterface(server, new ISteamRemotePlay(server));
			this.InstallEvents(server);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0000DE04 File Offset: 0x0000C004
		internal void InstallEvents(bool server)
		{
			Dispatch.Install<SteamRemotePlaySessionConnected_t>(delegate(SteamRemotePlaySessionConnected_t x)
			{
				Action<RemotePlaySession> onSessionConnected = SteamRemotePlay.OnSessionConnected;
				if (onSessionConnected != null)
				{
					onSessionConnected(x.SessionID);
				}
			}, server);
			Dispatch.Install<SteamRemotePlaySessionDisconnected_t>(delegate(SteamRemotePlaySessionDisconnected_t x)
			{
				Action<RemotePlaySession> onSessionDisconnected = SteamRemotePlay.OnSessionDisconnected;
				if (onSessionDisconnected != null)
				{
					onSessionDisconnected(x.SessionID);
				}
			}, server);
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000856 RID: 2134 RVA: 0x0000DE60 File Offset: 0x0000C060
		// (remove) Token: 0x06000857 RID: 2135 RVA: 0x0000DE94 File Offset: 0x0000C094
		public static event Action<RemotePlaySession> OnSessionConnected
		{
			[CompilerGenerated]
			add
			{
				Action<RemotePlaySession> action = SteamRemotePlay.OnSessionConnected;
				Action<RemotePlaySession> action2;
				do
				{
					action2 = action;
					Action<RemotePlaySession> value2 = (Action<RemotePlaySession>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<RemotePlaySession>>(ref SteamRemotePlay.OnSessionConnected, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<RemotePlaySession> action = SteamRemotePlay.OnSessionConnected;
				Action<RemotePlaySession> action2;
				do
				{
					action2 = action;
					Action<RemotePlaySession> value2 = (Action<RemotePlaySession>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<RemotePlaySession>>(ref SteamRemotePlay.OnSessionConnected, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000858 RID: 2136 RVA: 0x0000DEC8 File Offset: 0x0000C0C8
		// (remove) Token: 0x06000859 RID: 2137 RVA: 0x0000DEFC File Offset: 0x0000C0FC
		public static event Action<RemotePlaySession> OnSessionDisconnected
		{
			[CompilerGenerated]
			add
			{
				Action<RemotePlaySession> action = SteamRemotePlay.OnSessionDisconnected;
				Action<RemotePlaySession> action2;
				do
				{
					action2 = action;
					Action<RemotePlaySession> value2 = (Action<RemotePlaySession>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<RemotePlaySession>>(ref SteamRemotePlay.OnSessionDisconnected, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<RemotePlaySession> action = SteamRemotePlay.OnSessionDisconnected;
				Action<RemotePlaySession> action2;
				do
				{
					action2 = action;
					Action<RemotePlaySession> value2 = (Action<RemotePlaySession>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<RemotePlaySession>>(ref SteamRemotePlay.OnSessionDisconnected, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x0000DF2F File Offset: 0x0000C12F
		public static int SessionCount
		{
			get
			{
				return (int)SteamRemotePlay.Internal.GetSessionCount();
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0000DF3B File Offset: 0x0000C13B
		public static RemotePlaySession GetSession(int index)
		{
			return SteamRemotePlay.Internal.GetSessionID(index).Value;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0000DF52 File Offset: 0x0000C152
		public static bool SendInvite(SteamId steamid)
		{
			return SteamRemotePlay.Internal.BSendRemotePlayTogetherInvite(steamid);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0000DF5F File Offset: 0x0000C15F
		public SteamRemotePlay()
		{
		}

		// Token: 0x04000711 RID: 1809
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<RemotePlaySession> OnSessionConnected;

		// Token: 0x04000712 RID: 1810
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<RemotePlaySession> OnSessionDisconnected;

		// Token: 0x0200023C RID: 572
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001142 RID: 4418 RVA: 0x0001E5AB File Offset: 0x0001C7AB
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001143 RID: 4419 RVA: 0x0001E5B7 File Offset: 0x0001C7B7
			public <>c()
			{
			}

			// Token: 0x06001144 RID: 4420 RVA: 0x0001E5C0 File Offset: 0x0001C7C0
			internal void <InstallEvents>b__3_0(SteamRemotePlaySessionConnected_t x)
			{
				Action<RemotePlaySession> onSessionConnected = SteamRemotePlay.OnSessionConnected;
				if (onSessionConnected != null)
				{
					onSessionConnected(x.SessionID);
				}
			}

			// Token: 0x06001145 RID: 4421 RVA: 0x0001E5DE File Offset: 0x0001C7DE
			internal void <InstallEvents>b__3_1(SteamRemotePlaySessionDisconnected_t x)
			{
				Action<RemotePlaySession> onSessionDisconnected = SteamRemotePlay.OnSessionDisconnected;
				if (onSessionDisconnected != null)
				{
					onSessionDisconnected(x.SessionID);
				}
			}

			// Token: 0x04000D4E RID: 3406
			public static readonly SteamRemotePlay.<>c <>9 = new SteamRemotePlay.<>c();

			// Token: 0x04000D4F RID: 3407
			public static Action<SteamRemotePlaySessionConnected_t> <>9__3_0;

			// Token: 0x04000D50 RID: 3408
			public static Action<SteamRemotePlaySessionDisconnected_t> <>9__3_1;
		}
	}
}
