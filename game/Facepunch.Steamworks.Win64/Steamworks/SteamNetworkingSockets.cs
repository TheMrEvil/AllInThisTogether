using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x0200009B RID: 155
	public class SteamNetworkingSockets : SteamSharedClass<SteamNetworkingSockets>
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0000D26C File Offset: 0x0000B46C
		internal static ISteamNetworkingSockets Internal
		{
			get
			{
				return SteamSharedClass<SteamNetworkingSockets>.Interface as ISteamNetworkingSockets;
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0000D278 File Offset: 0x0000B478
		internal override void InitializeInterface(bool server)
		{
			this.SetInterface(server, new ISteamNetworkingSockets(server));
			this.InstallEvents(server);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0000D294 File Offset: 0x0000B494
		internal static SocketManager GetSocketManager(uint id)
		{
			bool flag = SteamNetworkingSockets.SocketInterfaces == null;
			SocketManager result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = id == 0U;
				if (flag2)
				{
					throw new ArgumentException("Invalid Socket");
				}
				SocketManager socketManager;
				bool flag3 = SteamNetworkingSockets.SocketInterfaces.TryGetValue(id, out socketManager);
				if (flag3)
				{
					result = socketManager;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0000D2E4 File Offset: 0x0000B4E4
		internal static void SetSocketManager(uint id, SocketManager manager)
		{
			bool flag = id == 0U;
			if (flag)
			{
				throw new ArgumentException("Invalid Socket");
			}
			SteamNetworkingSockets.SocketInterfaces[id] = manager;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0000D314 File Offset: 0x0000B514
		internal static ConnectionManager GetConnectionManager(uint id)
		{
			bool flag = SteamNetworkingSockets.ConnectionInterfaces == null;
			ConnectionManager result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = id == 0U;
				if (flag2)
				{
					result = null;
				}
				else
				{
					ConnectionManager connectionManager;
					bool flag3 = SteamNetworkingSockets.ConnectionInterfaces.TryGetValue(id, out connectionManager);
					if (flag3)
					{
						result = connectionManager;
					}
					else
					{
						result = null;
					}
				}
			}
			return result;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0000D35C File Offset: 0x0000B55C
		internal static void SetConnectionManager(uint id, ConnectionManager manager)
		{
			bool flag = id == 0U;
			if (flag)
			{
				throw new ArgumentException("Invalid Connection");
			}
			SteamNetworkingSockets.ConnectionInterfaces[id] = manager;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0000D38A File Offset: 0x0000B58A
		internal void InstallEvents(bool server)
		{
			Dispatch.Install<SteamNetConnectionStatusChangedCallback_t>(new Action<SteamNetConnectionStatusChangedCallback_t>(SteamNetworkingSockets.ConnectionStatusChanged), server);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0000D3A0 File Offset: 0x0000B5A0
		private static void ConnectionStatusChanged(SteamNetConnectionStatusChangedCallback_t data)
		{
			bool flag = data.Nfo.listenSocket.Id > 0U;
			if (flag)
			{
				SocketManager socketManager = SteamNetworkingSockets.GetSocketManager(data.Nfo.listenSocket.Id);
				if (socketManager != null)
				{
					socketManager.OnConnectionChanged(data.Conn, data.Nfo);
				}
			}
			else
			{
				ConnectionManager connectionManager = SteamNetworkingSockets.GetConnectionManager(data.Conn.Id);
				if (connectionManager != null)
				{
					connectionManager.OnConnectionChanged(data.Nfo);
				}
			}
			Action<Connection, ConnectionInfo> onConnectionStatusChanged = SteamNetworkingSockets.OnConnectionStatusChanged;
			if (onConnectionStatusChanged != null)
			{
				onConnectionStatusChanged(data.Conn, data.Nfo);
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x0600080E RID: 2062 RVA: 0x0000D43C File Offset: 0x0000B63C
		// (remove) Token: 0x0600080F RID: 2063 RVA: 0x0000D470 File Offset: 0x0000B670
		public static event Action<Connection, ConnectionInfo> OnConnectionStatusChanged
		{
			[CompilerGenerated]
			add
			{
				Action<Connection, ConnectionInfo> action = SteamNetworkingSockets.OnConnectionStatusChanged;
				Action<Connection, ConnectionInfo> action2;
				do
				{
					action2 = action;
					Action<Connection, ConnectionInfo> value2 = (Action<Connection, ConnectionInfo>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Connection, ConnectionInfo>>(ref SteamNetworkingSockets.OnConnectionStatusChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Connection, ConnectionInfo> action = SteamNetworkingSockets.OnConnectionStatusChanged;
				Action<Connection, ConnectionInfo> action2;
				do
				{
					action2 = action;
					Action<Connection, ConnectionInfo> value2 = (Action<Connection, ConnectionInfo>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Connection, ConnectionInfo>>(ref SteamNetworkingSockets.OnConnectionStatusChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0000D4A4 File Offset: 0x0000B6A4
		public static T CreateNormalSocket<T>(NetAddress address) where T : SocketManager, new()
		{
			T t = Activator.CreateInstance<T>();
			NetKeyValue[] array = Array.Empty<NetKeyValue>();
			t.Socket = SteamNetworkingSockets.Internal.CreateListenSocketIP(ref address, array.Length, array);
			t.Initialize();
			SteamNetworkingSockets.SetSocketManager(t.Socket.Id, t);
			return t;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0000D508 File Offset: 0x0000B708
		public static SocketManager CreateNormalSocket(NetAddress address, ISocketManager intrface)
		{
			NetKeyValue[] array = Array.Empty<NetKeyValue>();
			Socket socket = SteamNetworkingSockets.Internal.CreateListenSocketIP(ref address, array.Length, array);
			SocketManager socketManager = new SocketManager
			{
				Socket = socket,
				Interface = intrface
			};
			socketManager.Initialize();
			SteamNetworkingSockets.SetSocketManager(socketManager.Socket.Id, socketManager);
			return socketManager;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0000D564 File Offset: 0x0000B764
		public static T ConnectNormal<T>(NetAddress address) where T : ConnectionManager, new()
		{
			T t = Activator.CreateInstance<T>();
			NetKeyValue[] array = Array.Empty<NetKeyValue>();
			t.Connection = SteamNetworkingSockets.Internal.ConnectByIPAddress(ref address, array.Length, array);
			SteamNetworkingSockets.SetConnectionManager(t.Connection.Id, t);
			return t;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0000D5BC File Offset: 0x0000B7BC
		public static ConnectionManager ConnectNormal(NetAddress address, IConnectionManager iface)
		{
			NetKeyValue[] array = Array.Empty<NetKeyValue>();
			Connection connection = SteamNetworkingSockets.Internal.ConnectByIPAddress(ref address, array.Length, array);
			ConnectionManager connectionManager = new ConnectionManager
			{
				Connection = connection,
				Interface = iface
			};
			SteamNetworkingSockets.SetConnectionManager(connectionManager.Connection.Id, connectionManager);
			return connectionManager;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0000D610 File Offset: 0x0000B810
		public static T CreateRelaySocket<T>(int virtualport = 0) where T : SocketManager, new()
		{
			T t = Activator.CreateInstance<T>();
			NetKeyValue[] array = Array.Empty<NetKeyValue>();
			t.Socket = SteamNetworkingSockets.Internal.CreateListenSocketP2P(virtualport, array.Length, array);
			t.Initialize();
			SteamNetworkingSockets.SetSocketManager(t.Socket.Id, t);
			return t;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0000D674 File Offset: 0x0000B874
		public static T ConnectRelay<T>(SteamId serverId, int virtualport = 0) where T : ConnectionManager, new()
		{
			T t = Activator.CreateInstance<T>();
			NetIdentity netIdentity = serverId;
			NetKeyValue[] array = Array.Empty<NetKeyValue>();
			t.Connection = SteamNetworkingSockets.Internal.ConnectP2P(ref netIdentity, virtualport, array.Length, array);
			SteamNetworkingSockets.SetConnectionManager(t.Connection.Id, t);
			return t;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0000D6D2 File Offset: 0x0000B8D2
		public SteamNetworkingSockets()
		{
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0000D6DB File Offset: 0x0000B8DB
		// Note: this type is marked as 'beforefieldinit'.
		static SteamNetworkingSockets()
		{
		}

		// Token: 0x04000706 RID: 1798
		private static readonly Dictionary<uint, SocketManager> SocketInterfaces = new Dictionary<uint, SocketManager>();

		// Token: 0x04000707 RID: 1799
		private static readonly Dictionary<uint, ConnectionManager> ConnectionInterfaces = new Dictionary<uint, ConnectionManager>();

		// Token: 0x04000708 RID: 1800
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Connection, ConnectionInfo> OnConnectionStatusChanged;
	}
}
