using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using ExitGames.Client.Photon;
using UnityEngine;

namespace Photon.Realtime
{
	// Token: 0x0200000E RID: 14
	public class LoadBalancingClient : IPhotonPeerListener
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002F93 File Offset: 0x00001193
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002F9B File Offset: 0x0000119B
		public LoadBalancingPeer LoadBalancingPeer
		{
			[CompilerGenerated]
			get
			{
				return this.<LoadBalancingPeer>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<LoadBalancingPeer>k__BackingField = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002FA4 File Offset: 0x000011A4
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002FB1 File Offset: 0x000011B1
		public SerializationProtocol SerializationProtocol
		{
			get
			{
				return this.LoadBalancingPeer.SerializationProtocolType;
			}
			set
			{
				this.LoadBalancingPeer.SerializationProtocolType = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002FBF File Offset: 0x000011BF
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002FC7 File Offset: 0x000011C7
		public string AppVersion
		{
			[CompilerGenerated]
			get
			{
				return this.<AppVersion>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AppVersion>k__BackingField = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002FD0 File Offset: 0x000011D0
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002FD8 File Offset: 0x000011D8
		public string AppId
		{
			[CompilerGenerated]
			get
			{
				return this.<AppId>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AppId>k__BackingField = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002FE1 File Offset: 0x000011E1
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002FE9 File Offset: 0x000011E9
		public ClientAppType ClientType
		{
			[CompilerGenerated]
			get
			{
				return this.<ClientType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ClientType>k__BackingField = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002FF2 File Offset: 0x000011F2
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002FFA File Offset: 0x000011FA
		public AuthenticationValues AuthValues
		{
			[CompilerGenerated]
			get
			{
				return this.<AuthValues>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AuthValues>k__BackingField = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003003 File Offset: 0x00001203
		// (set) Token: 0x06000049 RID: 73 RVA: 0x0000300B File Offset: 0x0000120B
		public ConnectionProtocol? ExpectedProtocol
		{
			[CompilerGenerated]
			get
			{
				return this.<ExpectedProtocol>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ExpectedProtocol>k__BackingField = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003014 File Offset: 0x00001214
		private object TokenForInit
		{
			get
			{
				if (this.AuthMode == AuthModeOption.Auth)
				{
					return null;
				}
				if (this.AuthValues == null)
				{
					return null;
				}
				return this.AuthValues.Token;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00003035 File Offset: 0x00001235
		// (set) Token: 0x0600004C RID: 76 RVA: 0x0000303D File Offset: 0x0000123D
		public bool IsUsingNameServer
		{
			[CompilerGenerated]
			get
			{
				return this.<IsUsingNameServer>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsUsingNameServer>k__BackingField = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003046 File Offset: 0x00001246
		public string NameServerAddress
		{
			get
			{
				return this.GetNameServerAddress();
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000304E File Offset: 0x0000124E
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00003056 File Offset: 0x00001256
		[Obsolete("Set port overrides in ServerPortOverrides. Not used anymore!")]
		public bool UseAlternativeUdpPorts
		{
			[CompilerGenerated]
			get
			{
				return this.<UseAlternativeUdpPorts>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UseAlternativeUdpPorts>k__BackingField = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000050 RID: 80 RVA: 0x0000305F File Offset: 0x0000125F
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00003067 File Offset: 0x00001267
		public bool EnableProtocolFallback
		{
			[CompilerGenerated]
			get
			{
				return this.<EnableProtocolFallback>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EnableProtocolFallback>k__BackingField = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003070 File Offset: 0x00001270
		public string CurrentServerAddress
		{
			get
			{
				return this.LoadBalancingPeer.ServerAddress;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000307D File Offset: 0x0000127D
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00003085 File Offset: 0x00001285
		public string MasterServerAddress
		{
			[CompilerGenerated]
			get
			{
				return this.<MasterServerAddress>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MasterServerAddress>k__BackingField = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000308E File Offset: 0x0000128E
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00003096 File Offset: 0x00001296
		public string GameServerAddress
		{
			[CompilerGenerated]
			get
			{
				return this.<GameServerAddress>k__BackingField;
			}
			[CompilerGenerated]
			protected internal set
			{
				this.<GameServerAddress>k__BackingField = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000057 RID: 87 RVA: 0x0000309F File Offset: 0x0000129F
		// (set) Token: 0x06000058 RID: 88 RVA: 0x000030A7 File Offset: 0x000012A7
		public ServerConnection Server
		{
			[CompilerGenerated]
			get
			{
				return this.<Server>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Server>k__BackingField = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000030B0 File Offset: 0x000012B0
		// (set) Token: 0x0600005A RID: 90 RVA: 0x000030B8 File Offset: 0x000012B8
		public int ConnectCount
		{
			[CompilerGenerated]
			get
			{
				return this.<ConnectCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ConnectCount>k__BackingField = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000030C1 File Offset: 0x000012C1
		// (set) Token: 0x0600005C RID: 92 RVA: 0x000030CC File Offset: 0x000012CC
		public ClientState State
		{
			get
			{
				return this.state;
			}
			set
			{
				if (this.state == value)
				{
					return;
				}
				ClientState arg = this.state;
				this.state = value;
				if (this.StateChanged != null)
				{
					this.StateChanged(arg, this.state);
				}
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000310B File Offset: 0x0000130B
		public bool IsConnected
		{
			get
			{
				return this.LoadBalancingPeer != null && this.State != ClientState.PeerCreated && this.State != ClientState.Disconnected;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005E RID: 94 RVA: 0x0000312C File Offset: 0x0000132C
		public bool IsConnectedAndReady
		{
			get
			{
				if (this.LoadBalancingPeer == null)
				{
					return false;
				}
				switch (this.State)
				{
				case ClientState.PeerCreated:
				case ClientState.Authenticating:
				case ClientState.DisconnectingFromMasterServer:
				case ClientState.ConnectingToGameServer:
				case ClientState.Joining:
				case ClientState.Leaving:
				case ClientState.DisconnectingFromGameServer:
				case ClientState.ConnectingToMasterServer:
				case ClientState.Disconnecting:
				case ClientState.Disconnected:
				case ClientState.ConnectingToNameServer:
				case ClientState.DisconnectingFromNameServer:
					return false;
				}
				return true;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600005F RID: 95 RVA: 0x000031A4 File Offset: 0x000013A4
		// (remove) Token: 0x06000060 RID: 96 RVA: 0x000031DC File Offset: 0x000013DC
		public event Action<ClientState, ClientState> StateChanged
		{
			[CompilerGenerated]
			add
			{
				Action<ClientState, ClientState> action = this.StateChanged;
				Action<ClientState, ClientState> action2;
				do
				{
					action2 = action;
					Action<ClientState, ClientState> value2 = (Action<ClientState, ClientState>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ClientState, ClientState>>(ref this.StateChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ClientState, ClientState> action = this.StateChanged;
				Action<ClientState, ClientState> action2;
				do
				{
					action2 = action;
					Action<ClientState, ClientState> value2 = (Action<ClientState, ClientState>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ClientState, ClientState>>(ref this.StateChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000061 RID: 97 RVA: 0x00003214 File Offset: 0x00001414
		// (remove) Token: 0x06000062 RID: 98 RVA: 0x0000324C File Offset: 0x0000144C
		public event Action<EventData> EventReceived
		{
			[CompilerGenerated]
			add
			{
				Action<EventData> action = this.EventReceived;
				Action<EventData> action2;
				do
				{
					action2 = action;
					Action<EventData> value2 = (Action<EventData>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<EventData>>(ref this.EventReceived, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<EventData> action = this.EventReceived;
				Action<EventData> action2;
				do
				{
					action2 = action;
					Action<EventData> value2 = (Action<EventData>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<EventData>>(ref this.EventReceived, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000063 RID: 99 RVA: 0x00003284 File Offset: 0x00001484
		// (remove) Token: 0x06000064 RID: 100 RVA: 0x000032BC File Offset: 0x000014BC
		public event Action<OperationResponse> OpResponseReceived
		{
			[CompilerGenerated]
			add
			{
				Action<OperationResponse> action = this.OpResponseReceived;
				Action<OperationResponse> action2;
				do
				{
					action2 = action;
					Action<OperationResponse> value2 = (Action<OperationResponse>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<OperationResponse>>(ref this.OpResponseReceived, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<OperationResponse> action = this.OpResponseReceived;
				Action<OperationResponse> action2;
				do
				{
					action2 = action;
					Action<OperationResponse> value2 = (Action<OperationResponse>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<OperationResponse>>(ref this.OpResponseReceived, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000032F1 File Offset: 0x000014F1
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000032F9 File Offset: 0x000014F9
		public DisconnectCause DisconnectedCause
		{
			[CompilerGenerated]
			get
			{
				return this.<DisconnectedCause>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<DisconnectedCause>k__BackingField = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003302 File Offset: 0x00001502
		public bool InLobby
		{
			get
			{
				return this.State == ClientState.JoinedLobby;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000330D File Offset: 0x0000150D
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00003315 File Offset: 0x00001515
		public TypedLobby CurrentLobby
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentLobby>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<CurrentLobby>k__BackingField = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000331E File Offset: 0x0000151E
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00003326 File Offset: 0x00001526
		public Player LocalPlayer
		{
			[CompilerGenerated]
			get
			{
				return this.<LocalPlayer>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<LocalPlayer>k__BackingField = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000332F File Offset: 0x0000152F
		// (set) Token: 0x0600006D RID: 109 RVA: 0x0000333C File Offset: 0x0000153C
		public string NickName
		{
			get
			{
				return this.LocalPlayer.NickName;
			}
			set
			{
				if (this.LocalPlayer == null)
				{
					return;
				}
				this.LocalPlayer.NickName = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003353 File Offset: 0x00001553
		// (set) Token: 0x0600006F RID: 111 RVA: 0x0000336A File Offset: 0x0000156A
		public string UserId
		{
			get
			{
				if (this.AuthValues != null)
				{
					return this.AuthValues.UserId;
				}
				return null;
			}
			set
			{
				if (this.AuthValues == null)
				{
					this.AuthValues = new AuthenticationValues();
				}
				this.AuthValues.UserId = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000338B File Offset: 0x0000158B
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00003393 File Offset: 0x00001593
		public Room CurrentRoom
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentRoom>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CurrentRoom>k__BackingField = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000339C File Offset: 0x0000159C
		public bool InRoom
		{
			get
			{
				return this.state == ClientState.Joined && this.CurrentRoom != null;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000033B3 File Offset: 0x000015B3
		// (set) Token: 0x06000074 RID: 116 RVA: 0x000033BB File Offset: 0x000015BB
		public int PlayersOnMasterCount
		{
			[CompilerGenerated]
			get
			{
				return this.<PlayersOnMasterCount>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<PlayersOnMasterCount>k__BackingField = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000033C4 File Offset: 0x000015C4
		// (set) Token: 0x06000076 RID: 118 RVA: 0x000033CC File Offset: 0x000015CC
		public int PlayersInRoomsCount
		{
			[CompilerGenerated]
			get
			{
				return this.<PlayersInRoomsCount>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<PlayersInRoomsCount>k__BackingField = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000033D5 File Offset: 0x000015D5
		// (set) Token: 0x06000078 RID: 120 RVA: 0x000033DD File Offset: 0x000015DD
		public int RoomsCount
		{
			[CompilerGenerated]
			get
			{
				return this.<RoomsCount>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<RoomsCount>k__BackingField = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000033E6 File Offset: 0x000015E6
		public bool IsFetchingFriendList
		{
			get
			{
				return this.friendListRequested != null;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000033F1 File Offset: 0x000015F1
		// (set) Token: 0x0600007B RID: 123 RVA: 0x000033F9 File Offset: 0x000015F9
		public string CloudRegion
		{
			[CompilerGenerated]
			get
			{
				return this.<CloudRegion>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CloudRegion>k__BackingField = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003402 File Offset: 0x00001602
		// (set) Token: 0x0600007D RID: 125 RVA: 0x0000340A File Offset: 0x0000160A
		public string CurrentCluster
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentCluster>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CurrentCluster>k__BackingField = value;
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003414 File Offset: 0x00001614
		public LoadBalancingClient(ConnectionProtocol protocol = ConnectionProtocol.Udp)
		{
			this.ConnectionCallbackTargets = new ConnectionCallbacksContainer(this);
			this.MatchMakingCallbackTargets = new MatchMakingCallbacksContainer(this);
			this.InRoomCallbackTargets = new InRoomCallbacksContainer(this);
			this.LobbyCallbackTargets = new LobbyCallbacksContainer(this);
			this.WebRpcCallbackTargets = new WebRpcCallbacksContainer(this);
			this.ErrorInfoCallbackTargets = new ErrorInfoCallbacksContainer(this);
			this.LoadBalancingPeer = new LoadBalancingPeer(this, protocol);
			this.LoadBalancingPeer.OnDisconnectMessage += this.OnDisconnectMessageReceived;
			this.SerializationProtocol = SerializationProtocol.GpBinaryV18;
			this.LocalPlayer = this.CreatePlayer(string.Empty, -1, true, null);
			CustomTypesUnity.Register();
			this.State = ClientState.PeerCreated;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000034ED File Offset: 0x000016ED
		public LoadBalancingClient(string masterAddress, string appId, string gameVersion, ConnectionProtocol protocol = ConnectionProtocol.Udp) : this(protocol)
		{
			this.MasterServerAddress = masterAddress;
			this.AppId = appId;
			this.AppVersion = gameVersion;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000350C File Offset: 0x0000170C
		private string GetNameServerAddress()
		{
			int port = 0;
			LoadBalancingClient.ProtocolToNameServerPort.TryGetValue(this.LoadBalancingPeer.TransportProtocol, out port);
			if (this.NameServerPortInAppSettings != 0)
			{
				this.DebugReturn(DebugLevel.INFO, string.Format("Using NameServerPortInAppSettings: {0}", this.NameServerPortInAppSettings));
				port = this.NameServerPortInAppSettings;
			}
			if (this.ServerPortOverrides.NameServerPort > 0)
			{
				port = (int)this.ServerPortOverrides.NameServerPort;
			}
			return this.ToProtocolAddress(this.NameServerHost, port, this.LoadBalancingPeer.TransportProtocol);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003590 File Offset: 0x00001790
		private string ToProtocolAddress(string address, int port, ConnectionProtocol protocol)
		{
			string str = string.Empty;
			switch (protocol)
			{
			case ConnectionProtocol.Udp:
			case ConnectionProtocol.Tcp:
				return string.Format("{0}:{1}", address, port);
			case ConnectionProtocol.WebSocket:
				str = "ws://";
				goto IL_5E;
			case ConnectionProtocol.WebSocketSecure:
				str = "wss://";
				goto IL_5E;
			}
			throw new ArgumentOutOfRangeException(string.Format("Can not handle protocol: {0}.", protocol));
			IL_5E:
			Uri uri = new Uri(str + address);
			string text = string.Format("{0}://{1}:{2}{3}", new object[]
			{
				uri.Scheme,
				uri.Host,
				port,
				uri.AbsolutePath
			});
			if (this.AddressRewriter != null)
			{
				text = this.AddressRewriter(text, ServerConnection.NameServer);
			}
			return text;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003654 File Offset: 0x00001854
		public virtual bool ConnectUsingSettings(AppSettings appSettings)
		{
			if (this.LoadBalancingPeer.PeerState != PeerStateValue.Disconnected)
			{
				this.DebugReturn(DebugLevel.WARNING, "ConnectUsingSettings() failed. Can only connect while in state 'Disconnected'. Current state: " + this.LoadBalancingPeer.PeerState.ToString());
				return false;
			}
			if (appSettings == null)
			{
				this.DebugReturn(DebugLevel.ERROR, "ConnectUsingSettings failed. The appSettings can't be null.'");
				return false;
			}
			switch (this.ClientType)
			{
			case ClientAppType.Realtime:
				this.AppId = appSettings.AppIdRealtime;
				break;
			case ClientAppType.Voice:
				this.AppId = appSettings.AppIdVoice;
				break;
			case ClientAppType.Fusion:
				this.AppId = appSettings.AppIdFusion;
				break;
			}
			this.AppVersion = appSettings.AppVersion;
			this.IsUsingNameServer = appSettings.UseNameServer;
			this.CloudRegion = appSettings.FixedRegion;
			this.connectToBestRegion = string.IsNullOrEmpty(this.CloudRegion);
			this.EnableLobbyStatistics = appSettings.EnableLobbyStatistics;
			this.LoadBalancingPeer.DebugOut = appSettings.NetworkLogging;
			this.AuthMode = appSettings.AuthMode;
			if (appSettings.AuthMode == AuthModeOption.AuthOnceWss)
			{
				this.LoadBalancingPeer.TransportProtocol = ConnectionProtocol.WebSocketSecure;
				this.ExpectedProtocol = new ConnectionProtocol?(appSettings.Protocol);
			}
			else
			{
				this.LoadBalancingPeer.TransportProtocol = appSettings.Protocol;
				this.ExpectedProtocol = null;
			}
			this.EnableProtocolFallback = appSettings.EnableProtocolFallback;
			this.bestRegionSummaryFromStorage = appSettings.BestRegionSummaryFromStorage;
			this.DisconnectedCause = DisconnectCause.None;
			this.DisconnectMessage = null;
			this.SystemConnectionSummary = null;
			if (this.IsUsingNameServer)
			{
				this.Server = ServerConnection.NameServer;
				if (!appSettings.IsDefaultNameServer)
				{
					this.NameServerHost = appSettings.Server;
				}
				this.ProxyServerAddress = appSettings.ProxyServer;
				this.NameServerPortInAppSettings = appSettings.Port;
				if (!this.LoadBalancingPeer.Connect(this.NameServerAddress, this.ProxyServerAddress, this.AppId, this.TokenForInit, null))
				{
					return false;
				}
				this.State = ClientState.ConnectingToNameServer;
			}
			else
			{
				this.Server = ServerConnection.MasterServer;
				int port = appSettings.IsDefaultPort ? 5055 : appSettings.Port;
				this.MasterServerAddress = this.ToProtocolAddress(appSettings.Server, port, this.LoadBalancingPeer.TransportProtocol);
				if (!this.LoadBalancingPeer.Connect(this.MasterServerAddress, this.ProxyServerAddress, this.AppId, this.TokenForInit, null))
				{
					return false;
				}
				this.State = ClientState.ConnectingToMasterServer;
			}
			return true;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000389A File Offset: 0x00001A9A
		[Obsolete("Use ConnectToMasterServer() instead.")]
		public bool Connect()
		{
			return this.ConnectToMasterServer();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000038A4 File Offset: 0x00001AA4
		public virtual bool ConnectToMasterServer()
		{
			if (this.LoadBalancingPeer.PeerState != PeerStateValue.Disconnected)
			{
				this.DebugReturn(DebugLevel.WARNING, "ConnectToMasterServer() failed. Can only connect while in state 'Disconnected'. Current state: " + this.LoadBalancingPeer.PeerState.ToString());
				return false;
			}
			if (this.AuthMode != AuthModeOption.Auth && this.TokenForInit == null)
			{
				this.DebugReturn(DebugLevel.ERROR, "Connect() failed. Can't connect to MasterServer with Token == null in AuthMode: " + this.AuthMode.ToString());
				return false;
			}
			this.DisconnectedCause = DisconnectCause.None;
			this.DisconnectMessage = null;
			this.SystemConnectionSummary = null;
			if (this.LoadBalancingPeer.Connect(this.MasterServerAddress, this.ProxyServerAddress, this.AppId, this.TokenForInit, null))
			{
				this.connectToBestRegion = false;
				this.State = ClientState.ConnectingToMasterServer;
				this.Server = ServerConnection.MasterServer;
				return true;
			}
			return false;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003974 File Offset: 0x00001B74
		public bool ConnectToNameServer()
		{
			if (this.LoadBalancingPeer.PeerState != PeerStateValue.Disconnected)
			{
				this.DebugReturn(DebugLevel.WARNING, "ConnectToNameServer() failed. Can only connect while in state 'Disconnected'. Current state: " + this.LoadBalancingPeer.PeerState.ToString());
				return false;
			}
			this.IsUsingNameServer = true;
			this.CloudRegion = null;
			if (this.AuthMode == AuthModeOption.AuthOnceWss)
			{
				if (this.ExpectedProtocol == null)
				{
					this.ExpectedProtocol = new ConnectionProtocol?(this.LoadBalancingPeer.TransportProtocol);
				}
				this.LoadBalancingPeer.TransportProtocol = ConnectionProtocol.WebSocketSecure;
			}
			this.DisconnectedCause = DisconnectCause.None;
			this.DisconnectMessage = null;
			this.SystemConnectionSummary = null;
			if (this.LoadBalancingPeer.Connect(this.NameServerAddress, this.ProxyServerAddress, "NameServer", this.TokenForInit, null))
			{
				this.connectToBestRegion = false;
				this.State = ClientState.ConnectingToNameServer;
				this.Server = ServerConnection.NameServer;
				return true;
			}
			return false;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003A58 File Offset: 0x00001C58
		public bool ConnectToRegionMaster(string region)
		{
			if (string.IsNullOrEmpty(region))
			{
				this.DebugReturn(DebugLevel.ERROR, "ConnectToRegionMaster() failed. The region can not be null or empty.");
				return false;
			}
			this.IsUsingNameServer = true;
			if (this.State == ClientState.Authenticating)
			{
				if (this.LoadBalancingPeer.DebugOut >= DebugLevel.INFO)
				{
					this.DebugReturn(DebugLevel.INFO, "ConnectToRegionMaster() will skip calling authenticate, as the current state is 'Authenticating'. Just wait for the result.");
				}
				return true;
			}
			if (this.State == ClientState.ConnectedToNameServer)
			{
				this.CloudRegion = region;
				bool flag = this.CallAuthenticate();
				if (flag)
				{
					this.State = ClientState.Authenticating;
				}
				return flag;
			}
			this.LoadBalancingPeer.Disconnect();
			this.CloudRegion = region;
			if (this.AuthMode == AuthModeOption.AuthOnceWss)
			{
				if (this.ExpectedProtocol == null)
				{
					this.ExpectedProtocol = new ConnectionProtocol?(this.LoadBalancingPeer.TransportProtocol);
				}
				this.LoadBalancingPeer.TransportProtocol = ConnectionProtocol.WebSocketSecure;
			}
			this.connectToBestRegion = false;
			this.DisconnectedCause = DisconnectCause.None;
			this.DisconnectMessage = null;
			this.SystemConnectionSummary = null;
			if (!this.LoadBalancingPeer.Connect(this.NameServerAddress, this.ProxyServerAddress, "NameServer", null, null))
			{
				return false;
			}
			this.State = ClientState.ConnectingToNameServer;
			this.Server = ServerConnection.NameServer;
			return true;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003B64 File Offset: 0x00001D64
		[Conditional("UNITY_WEBGL")]
		private void CheckConnectSetupWebGl()
		{
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003B68 File Offset: 0x00001D68
		private bool Connect(string serverAddress, string proxyServerAddress, ServerConnection serverType)
		{
			if (this.State == ClientState.Disconnecting)
			{
				this.DebugReturn(DebugLevel.ERROR, "Connect() failed. Can't connect while disconnecting (still). Current state: " + this.State.ToString());
				return false;
			}
			if (this.AuthMode != AuthModeOption.Auth && serverType != ServerConnection.NameServer && this.TokenForInit == null)
			{
				this.DebugReturn(DebugLevel.ERROR, "Connect() failed. Can't connect to " + serverType.ToString() + " with Token == null in AuthMode: " + this.AuthMode.ToString());
				return false;
			}
			this.DisconnectedCause = DisconnectCause.None;
			this.SystemConnectionSummary = null;
			bool flag = this.LoadBalancingPeer.Connect(serverAddress, proxyServerAddress, this.AppId, this.TokenForInit, null);
			if (flag)
			{
				this.Server = serverType;
				switch (serverType)
				{
				case ServerConnection.MasterServer:
					this.State = ClientState.ConnectingToMasterServer;
					break;
				case ServerConnection.GameServer:
					this.State = ClientState.ConnectingToGameServer;
					break;
				case ServerConnection.NameServer:
					this.State = ClientState.ConnectingToNameServer;
					break;
				}
			}
			return flag;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003C54 File Offset: 0x00001E54
		public bool ReconnectToMaster()
		{
			if (this.LoadBalancingPeer.PeerState != PeerStateValue.Disconnected)
			{
				this.DebugReturn(DebugLevel.WARNING, "ReconnectToMaster() failed. Can only connect while in state 'Disconnected'. Current state: " + this.LoadBalancingPeer.PeerState.ToString());
				return false;
			}
			if (string.IsNullOrEmpty(this.MasterServerAddress))
			{
				this.DebugReturn(DebugLevel.WARNING, "ReconnectToMaster() failed. MasterServerAddress is null or empty.");
				return false;
			}
			if (this.tokenCache == null)
			{
				this.DebugReturn(DebugLevel.WARNING, "ReconnectToMaster() failed. It seems the client doesn't have any previous authentication token to re-connect.");
				return false;
			}
			if (this.AuthValues == null)
			{
				this.DebugReturn(DebugLevel.WARNING, "ReconnectToMaster() with AuthValues == null is not correct!");
				this.AuthValues = new AuthenticationValues();
			}
			this.AuthValues.Token = this.tokenCache;
			return this.Connect(this.MasterServerAddress, this.ProxyServerAddress, ServerConnection.MasterServer);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003D10 File Offset: 0x00001F10
		public bool ReconnectAndRejoin()
		{
			if (this.LoadBalancingPeer.PeerState != PeerStateValue.Disconnected)
			{
				this.DebugReturn(DebugLevel.WARNING, "ReconnectAndRejoin() failed. Can only connect while in state 'Disconnected'. Current state: " + this.LoadBalancingPeer.PeerState.ToString());
				return false;
			}
			if (string.IsNullOrEmpty(this.GameServerAddress))
			{
				this.DebugReturn(DebugLevel.WARNING, "ReconnectAndRejoin() failed. It seems the client wasn't connected to a game server before (no address).");
				return false;
			}
			if (this.enterRoomParamsCache == null)
			{
				this.DebugReturn(DebugLevel.WARNING, "ReconnectAndRejoin() failed. It seems the client doesn't have any previous room to re-join.");
				return false;
			}
			if (this.tokenCache == null)
			{
				this.DebugReturn(DebugLevel.WARNING, "ReconnectAndRejoin() failed. It seems the client doesn't have any previous authentication token to re-connect.");
				return false;
			}
			if (this.AuthValues == null)
			{
				this.AuthValues = new AuthenticationValues();
			}
			this.AuthValues.Token = this.tokenCache;
			if (!string.IsNullOrEmpty(this.GameServerAddress) && this.enterRoomParamsCache != null)
			{
				this.lastJoinType = JoinType.JoinRoom;
				this.enterRoomParamsCache.JoinMode = JoinMode.RejoinOnly;
				this.enterRoomParamsCache.Ticket = null;
				return this.Connect(this.GameServerAddress, this.ProxyServerAddress, ServerConnection.GameServer);
			}
			return false;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003E0A File Offset: 0x0000200A
		public void Disconnect()
		{
			this.Disconnect(DisconnectCause.DisconnectByClientLogic);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003E14 File Offset: 0x00002014
		internal void Disconnect(DisconnectCause cause)
		{
			if (this.State == ClientState.Disconnecting || this.State == ClientState.PeerCreated)
			{
				this.DebugReturn(DebugLevel.INFO, string.Concat(new string[]
				{
					"Disconnect() call gets skipped due to State ",
					this.State.ToString(),
					". DisconnectedCause: ",
					this.DisconnectedCause.ToString(),
					" Parameter cause: ",
					cause.ToString()
				}));
				return;
			}
			if (this.State != ClientState.Disconnected)
			{
				this.State = ClientState.Disconnecting;
				this.DisconnectedCause = cause;
				this.LoadBalancingPeer.Disconnect();
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003EC0 File Offset: 0x000020C0
		private void DisconnectToReconnect()
		{
			switch (this.Server)
			{
			case ServerConnection.MasterServer:
				this.State = ClientState.DisconnectingFromMasterServer;
				break;
			case ServerConnection.GameServer:
				this.State = ClientState.DisconnectingFromGameServer;
				break;
			case ServerConnection.NameServer:
				this.State = ClientState.DisconnectingFromNameServer;
				break;
			}
			this.LoadBalancingPeer.Disconnect();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003F10 File Offset: 0x00002110
		public void SimulateConnectionLoss(bool simulateTimeout)
		{
			this.DebugReturn(DebugLevel.WARNING, "SimulateConnectionLoss() set to: " + simulateTimeout.ToString());
			if (simulateTimeout)
			{
				this.LoadBalancingPeer.NetworkSimulationSettings.IncomingLossPercentage = 100;
				this.LoadBalancingPeer.NetworkSimulationSettings.OutgoingLossPercentage = 100;
			}
			this.LoadBalancingPeer.IsSimulationEnabled = simulateTimeout;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003F68 File Offset: 0x00002168
		private bool CallAuthenticate()
		{
			if (this.IsUsingNameServer && this.Server != ServerConnection.NameServer && (this.AuthValues == null || this.AuthValues.Token == null))
			{
				this.DebugReturn(DebugLevel.ERROR, string.Concat(new string[]
				{
					"Authenticate without Token is only allowed on Name Server. Connecting to: ",
					this.Server.ToString(),
					" on: ",
					this.CurrentServerAddress,
					". State: ",
					this.State.ToString()
				}));
				return false;
			}
			if (this.AuthMode == AuthModeOption.Auth)
			{
				return this.CheckIfOpCanBeSent(230, this.Server, "Authenticate") && this.LoadBalancingPeer.OpAuthenticate(this.AppId, this.AppVersion, this.AuthValues, this.CloudRegion, this.EnableLobbyStatistics && this.Server == ServerConnection.MasterServer);
			}
			if (!this.CheckIfOpCanBeSent(231, this.Server, "AuthenticateOnce"))
			{
				return false;
			}
			ConnectionProtocol expectedProtocol = (this.ExpectedProtocol != null) ? this.ExpectedProtocol.Value : this.LoadBalancingPeer.TransportProtocol;
			return this.LoadBalancingPeer.OpAuthenticateOnce(this.AppId, this.AppVersion, this.AuthValues, this.CloudRegion, this.EncryptionMode, expectedProtocol);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000040CB File Offset: 0x000022CB
		public void Service()
		{
			if (this.LoadBalancingPeer != null)
			{
				this.LoadBalancingPeer.Service();
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000040E0 File Offset: 0x000022E0
		private bool OpGetRegions()
		{
			return this.CheckIfOpCanBeSent(220, this.Server, "GetRegions") && this.LoadBalancingPeer.OpGetRegions(this.AppId);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004110 File Offset: 0x00002310
		public bool OpFindFriends(string[] friendsToFind, FindFriendsOptions options = null)
		{
			if (!this.CheckIfOpCanBeSent(222, this.Server, "FindFriends"))
			{
				return false;
			}
			if (this.IsFetchingFriendList)
			{
				this.DebugReturn(DebugLevel.WARNING, "OpFindFriends skipped: already fetching friends list.");
				return false;
			}
			if (friendsToFind == null || friendsToFind.Length == 0)
			{
				this.DebugReturn(DebugLevel.ERROR, "OpFindFriends skipped: friendsToFind array is null or empty.");
				return false;
			}
			if (friendsToFind.Length > 512)
			{
				this.DebugReturn(DebugLevel.ERROR, string.Format("OpFindFriends skipped: friendsToFind array exceeds allowed length of {0}.", 512));
				return false;
			}
			List<string> list = new List<string>(friendsToFind.Length);
			for (int i = 0; i < friendsToFind.Length; i++)
			{
				string text = friendsToFind[i];
				if (string.IsNullOrEmpty(text))
				{
					this.DebugReturn(DebugLevel.WARNING, string.Format("friendsToFind array contains a null or empty UserId, element at position {0} skipped.", i));
				}
				else if (text.Equals(this.UserId))
				{
					this.DebugReturn(DebugLevel.WARNING, string.Format("friendsToFind array contains local player's UserId \"{0}\", element at position {1} skipped.", text, i));
				}
				else if (list.Contains(text))
				{
					this.DebugReturn(DebugLevel.WARNING, string.Format("friendsToFind array contains duplicate UserId \"{0}\", element at position {1} skipped.", text, i));
				}
				else
				{
					list.Add(text);
				}
			}
			if (list.Count == 0)
			{
				this.DebugReturn(DebugLevel.ERROR, "OpFindFriends skipped: friends list to find is empty.");
				return false;
			}
			string[] array = list.ToArray();
			bool flag = this.LoadBalancingPeer.OpFindFriends(array, options);
			this.friendListRequested = (flag ? array : null);
			return flag;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004260 File Offset: 0x00002460
		public bool OpJoinLobby(TypedLobby lobby)
		{
			if (!this.CheckIfOpCanBeSent(229, this.Server, "JoinLobby"))
			{
				return false;
			}
			if (lobby == null)
			{
				lobby = TypedLobby.Default;
			}
			bool flag = this.LoadBalancingPeer.OpJoinLobby(lobby);
			if (flag)
			{
				this.CurrentLobby = lobby;
				this.State = ClientState.JoiningLobby;
			}
			return flag;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000042AE File Offset: 0x000024AE
		public bool OpLeaveLobby()
		{
			return this.CheckIfOpCanBeSent(228, this.Server, "LeaveLobby") && this.LoadBalancingPeer.OpLeaveLobby();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000042D8 File Offset: 0x000024D8
		public bool OpJoinRandomRoom(OpJoinRandomRoomParams opJoinRandomRoomParams = null)
		{
			if (!this.CheckIfOpCanBeSent(225, this.Server, "JoinRandomGame"))
			{
				return false;
			}
			if (opJoinRandomRoomParams == null)
			{
				opJoinRandomRoomParams = new OpJoinRandomRoomParams();
			}
			this.enterRoomParamsCache = new EnterRoomParams();
			this.enterRoomParamsCache.Lobby = opJoinRandomRoomParams.TypedLobby;
			this.enterRoomParamsCache.ExpectedUsers = opJoinRandomRoomParams.ExpectedUsers;
			this.enterRoomParamsCache.Ticket = opJoinRandomRoomParams.Ticket;
			bool flag = this.LoadBalancingPeer.OpJoinRandomRoom(opJoinRandomRoomParams);
			if (flag)
			{
				this.lastJoinType = JoinType.JoinRandomRoom;
				this.State = ClientState.Joining;
			}
			return flag;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004364 File Offset: 0x00002564
		public bool OpJoinRandomOrCreateRoom(OpJoinRandomRoomParams opJoinRandomRoomParams, EnterRoomParams createRoomParams)
		{
			if (!this.CheckIfOpCanBeSent(225, this.Server, "OpJoinRandomOrCreateRoom"))
			{
				return false;
			}
			if (opJoinRandomRoomParams == null)
			{
				opJoinRandomRoomParams = new OpJoinRandomRoomParams();
			}
			if (createRoomParams == null)
			{
				createRoomParams = new EnterRoomParams();
			}
			createRoomParams.JoinMode = JoinMode.CreateIfNotExists;
			this.enterRoomParamsCache = createRoomParams;
			this.enterRoomParamsCache.Lobby = opJoinRandomRoomParams.TypedLobby;
			this.enterRoomParamsCache.ExpectedUsers = opJoinRandomRoomParams.ExpectedUsers;
			if (opJoinRandomRoomParams.Ticket != null)
			{
				this.enterRoomParamsCache.Ticket = opJoinRandomRoomParams.Ticket;
			}
			bool flag = this.LoadBalancingPeer.OpJoinRandomOrCreateRoom(opJoinRandomRoomParams, createRoomParams);
			if (flag)
			{
				this.lastJoinType = JoinType.JoinRandomOrCreateRoom;
				this.State = ClientState.Joining;
			}
			return flag;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004408 File Offset: 0x00002608
		public bool OpCreateRoom(EnterRoomParams enterRoomParams)
		{
			if (!this.CheckIfOpCanBeSent(227, this.Server, "CreateGame"))
			{
				return false;
			}
			bool flag = this.Server == ServerConnection.GameServer;
			enterRoomParams.OnGameServer = flag;
			if (!flag)
			{
				this.enterRoomParamsCache = enterRoomParams;
			}
			bool flag2 = this.LoadBalancingPeer.OpCreateRoom(enterRoomParams);
			if (flag2)
			{
				this.lastJoinType = JoinType.CreateRoom;
				this.State = ClientState.Joining;
			}
			return flag2;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004468 File Offset: 0x00002668
		public bool OpJoinOrCreateRoom(EnterRoomParams enterRoomParams)
		{
			if (!this.CheckIfOpCanBeSent(226, this.Server, "JoinOrCreateRoom"))
			{
				return false;
			}
			bool flag = this.Server == ServerConnection.GameServer;
			enterRoomParams.JoinMode = JoinMode.CreateIfNotExists;
			enterRoomParams.OnGameServer = flag;
			if (!flag)
			{
				this.enterRoomParamsCache = enterRoomParams;
			}
			bool flag2 = this.LoadBalancingPeer.OpJoinRoom(enterRoomParams);
			if (flag2)
			{
				this.lastJoinType = JoinType.JoinOrCreateRoom;
				this.State = ClientState.Joining;
			}
			return flag2;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000044D0 File Offset: 0x000026D0
		public bool OpJoinRoom(EnterRoomParams enterRoomParams)
		{
			if (!this.CheckIfOpCanBeSent(226, this.Server, "JoinRoom"))
			{
				return false;
			}
			bool flag = this.Server == ServerConnection.GameServer;
			enterRoomParams.OnGameServer = flag;
			if (!flag)
			{
				this.enterRoomParamsCache = enterRoomParams;
			}
			bool flag2 = this.LoadBalancingPeer.OpJoinRoom(enterRoomParams);
			if (flag2)
			{
				this.lastJoinType = ((enterRoomParams.JoinMode == JoinMode.CreateIfNotExists) ? JoinType.JoinOrCreateRoom : JoinType.JoinRoom);
				this.State = ClientState.Joining;
			}
			return flag2;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000453C File Offset: 0x0000273C
		public bool OpRejoinRoom(string roomName, object ticket = null)
		{
			if (!this.CheckIfOpCanBeSent(226, this.Server, "RejoinRoom"))
			{
				return false;
			}
			bool onGameServer = this.Server == ServerConnection.GameServer;
			EnterRoomParams enterRoomParams = new EnterRoomParams();
			enterRoomParams.RoomName = roomName;
			enterRoomParams.OnGameServer = onGameServer;
			enterRoomParams.JoinMode = JoinMode.RejoinOnly;
			enterRoomParams.Ticket = ticket;
			this.enterRoomParamsCache = enterRoomParams;
			bool flag = this.LoadBalancingPeer.OpJoinRoom(enterRoomParams);
			if (flag)
			{
				this.lastJoinType = JoinType.JoinRoom;
				this.State = ClientState.Joining;
			}
			return flag;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000045B4 File Offset: 0x000027B4
		public bool OpLeaveRoom(bool becomeInactive, bool sendAuthCookie = false)
		{
			if (!this.CheckIfOpCanBeSent(254, this.Server, "LeaveRoom"))
			{
				return false;
			}
			if (this.LoadBalancingPeer.OpLeaveRoom(becomeInactive, sendAuthCookie))
			{
				this.State = ClientState.Leaving;
				this.GameServerAddress = string.Empty;
				this.enterRoomParamsCache = null;
				return true;
			}
			return false;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004608 File Offset: 0x00002808
		public bool OpGetGameList(TypedLobby typedLobby, string sqlLobbyFilter)
		{
			if (!this.CheckIfOpCanBeSent(217, this.Server, "GetGameList"))
			{
				return false;
			}
			if (string.IsNullOrEmpty(sqlLobbyFilter))
			{
				this.DebugReturn(DebugLevel.ERROR, "Operation GetGameList requires a filter.");
				return false;
			}
			if (typedLobby.Type != LobbyType.SqlLobby)
			{
				this.DebugReturn(DebugLevel.ERROR, "Operation GetGameList can only be used for lobbies of type SqlLobby.");
				return false;
			}
			return this.LoadBalancingPeer.OpGetGameList(typedLobby, sqlLobbyFilter);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000466C File Offset: 0x0000286C
		public bool OpSetCustomPropertiesOfActor(int actorNr, Hashtable propertiesToSet, Hashtable expectedProperties = null, WebFlags webFlags = null)
		{
			if (propertiesToSet == null || propertiesToSet.Count == 0)
			{
				this.DebugReturn(DebugLevel.ERROR, "OpSetCustomPropertiesOfActor() failed. propertiesToSet must not be null nor empty.");
				return false;
			}
			if (this.CurrentRoom == null)
			{
				if (expectedProperties == null && webFlags == null && this.LocalPlayer != null && this.LocalPlayer.ActorNumber == actorNr)
				{
					return this.LocalPlayer.SetCustomProperties(propertiesToSet, null, null);
				}
				if (this.LoadBalancingPeer.DebugOut >= DebugLevel.ERROR)
				{
					this.DebugReturn(DebugLevel.ERROR, "OpSetCustomPropertiesOfActor() failed. To use expectedProperties or webForward, you have to be in a room. State: " + this.State.ToString());
				}
				return false;
			}
			else
			{
				Hashtable hashtable = new Hashtable();
				hashtable.MergeStringKeys(propertiesToSet);
				if (hashtable.Count == 0)
				{
					this.DebugReturn(DebugLevel.ERROR, "OpSetCustomPropertiesOfActor() failed. Only string keys allowed for custom properties.");
					return false;
				}
				return this.OpSetPropertiesOfActor(actorNr, hashtable, expectedProperties, webFlags);
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000472C File Offset: 0x0000292C
		protected internal bool OpSetPropertiesOfActor(int actorNr, Hashtable actorProperties, Hashtable expectedProperties = null, WebFlags webFlags = null)
		{
			if (!this.CheckIfOpCanBeSent(252, this.Server, "SetProperties"))
			{
				return false;
			}
			if (actorProperties == null || actorProperties.Count == 0)
			{
				this.DebugReturn(DebugLevel.ERROR, "OpSetPropertiesOfActor() failed. actorProperties must not be null nor empty.");
				return false;
			}
			bool flag = this.LoadBalancingPeer.OpSetPropertiesOfActor(actorNr, actorProperties, expectedProperties, webFlags);
			if (flag && !this.CurrentRoom.BroadcastPropertiesChangeToAll && (expectedProperties == null || expectedProperties.Count == 0))
			{
				Player player = this.CurrentRoom.GetPlayer(actorNr, false);
				if (player != null)
				{
					player.InternalCacheProperties(actorProperties);
					this.InRoomCallbackTargets.OnPlayerPropertiesUpdate(player, actorProperties);
				}
			}
			return flag;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000047BC File Offset: 0x000029BC
		public bool OpSetCustomPropertiesOfRoom(Hashtable propertiesToSet, Hashtable expectedProperties = null, WebFlags webFlags = null)
		{
			if (propertiesToSet == null || propertiesToSet.Count == 0)
			{
				this.DebugReturn(DebugLevel.ERROR, "OpSetCustomPropertiesOfRoom() failed. propertiesToSet must not be null nor empty.");
				return false;
			}
			Hashtable hashtable = new Hashtable();
			hashtable.MergeStringKeys(propertiesToSet);
			if (hashtable.Count == 0)
			{
				this.DebugReturn(DebugLevel.ERROR, "OpSetCustomPropertiesOfRoom() failed. Only string keys are allowed for custom properties.");
				return false;
			}
			return this.OpSetPropertiesOfRoom(hashtable, expectedProperties, webFlags);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004810 File Offset: 0x00002A10
		protected internal bool OpSetPropertyOfRoom(byte propCode, object value)
		{
			Hashtable hashtable = new Hashtable();
			hashtable[propCode] = value;
			return this.OpSetPropertiesOfRoom(hashtable, null, null);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004834 File Offset: 0x00002A34
		protected internal bool OpSetPropertiesOfRoom(Hashtable gameProperties, Hashtable expectedProperties = null, WebFlags webFlags = null)
		{
			if (!this.CheckIfOpCanBeSent(252, this.Server, "SetProperties"))
			{
				return false;
			}
			if (gameProperties == null || gameProperties.Count == 0)
			{
				this.DebugReturn(DebugLevel.ERROR, "OpSetPropertiesOfRoom() failed. gameProperties must not be null nor empty.");
				return false;
			}
			bool flag = this.LoadBalancingPeer.OpSetPropertiesOfRoom(gameProperties, expectedProperties, webFlags);
			if (flag && !this.CurrentRoom.BroadcastPropertiesChangeToAll && (expectedProperties == null || expectedProperties.Count == 0))
			{
				this.CurrentRoom.InternalCacheProperties(gameProperties);
				this.InRoomCallbackTargets.OnRoomPropertiesUpdate(gameProperties);
			}
			return flag;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000048B5 File Offset: 0x00002AB5
		public virtual bool OpRaiseEvent(byte eventCode, object customEventContent, RaiseEventOptions raiseEventOptions, SendOptions sendOptions)
		{
			return this.CheckIfOpCanBeSent(253, this.Server, "RaiseEvent") && this.LoadBalancingPeer.OpRaiseEvent(eventCode, customEventContent, raiseEventOptions, sendOptions);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000048E1 File Offset: 0x00002AE1
		public virtual bool OpChangeGroups(byte[] groupsToRemove, byte[] groupsToAdd)
		{
			return this.CheckIfOpCanBeSent(248, this.Server, "ChangeGroups") && this.LoadBalancingPeer.OpChangeGroups(groupsToRemove, groupsToAdd);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000490C File Offset: 0x00002B0C
		private void ReadoutProperties(Hashtable gameProperties, Hashtable actorProperties, int targetActorNr)
		{
			if (this.CurrentRoom != null && gameProperties != null)
			{
				this.CurrentRoom.InternalCacheProperties(gameProperties);
				if (this.InRoom)
				{
					this.InRoomCallbackTargets.OnRoomPropertiesUpdate(gameProperties);
				}
			}
			if (actorProperties != null && actorProperties.Count > 0)
			{
				if (targetActorNr > 0)
				{
					Player player = this.CurrentRoom.GetPlayer(targetActorNr, false);
					if (player != null)
					{
						Hashtable hashtable = this.ReadoutPropertiesForActorNr(actorProperties, targetActorNr);
						player.InternalCacheProperties(hashtable);
						this.InRoomCallbackTargets.OnPlayerPropertiesUpdate(player, hashtable);
						return;
					}
				}
				else
				{
					foreach (object obj in actorProperties.Keys)
					{
						int num = (int)obj;
						if (num != 0)
						{
							Hashtable hashtable2 = (Hashtable)actorProperties[obj];
							string actorName = (string)hashtable2[byte.MaxValue];
							Player player2 = this.CurrentRoom.GetPlayer(num, false);
							if (player2 == null)
							{
								player2 = this.CreatePlayer(actorName, num, false, hashtable2);
								this.CurrentRoom.StorePlayer(player2);
							}
							player2.InternalCacheProperties(hashtable2);
						}
					}
				}
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004A30 File Offset: 0x00002C30
		private Hashtable ReadoutPropertiesForActorNr(Hashtable actorProperties, int actorNr)
		{
			if (actorProperties.ContainsKey(actorNr))
			{
				return (Hashtable)actorProperties[actorNr];
			}
			return actorProperties;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004A54 File Offset: 0x00002C54
		public void ChangeLocalID(int newId, bool applyUserId = false)
		{
			if (this.LocalPlayer == null)
			{
				this.DebugReturn(DebugLevel.ERROR, "loadBalancingClient.LocalPlayer is null. It should be set in constructor and not changed. Failed to ChangeLocalID.");
				return;
			}
			if (applyUserId && string.IsNullOrEmpty(this.LocalPlayer.UserId))
			{
				this.LocalPlayer.UserId = ((this.AuthValues == null || string.IsNullOrEmpty(this.AuthValues.UserId)) ? default(Guid).ToString() : this.AuthValues.UserId);
			}
			if (this.CurrentRoom == null)
			{
				this.LocalPlayer.ChangeLocalID(newId);
				this.LocalPlayer.RoomReference = null;
				return;
			}
			this.CurrentRoom.RemovePlayer(this.LocalPlayer);
			this.LocalPlayer.ChangeLocalID(newId);
			this.CurrentRoom.StorePlayer(this.LocalPlayer);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004B24 File Offset: 0x00002D24
		private void GameEnteredOnGameServer(OperationResponse operationResponse)
		{
			this.CurrentRoom = this.CreateRoom(this.enterRoomParamsCache.RoomName, this.enterRoomParamsCache.RoomOptions);
			this.CurrentRoom.LoadBalancingClient = this;
			int newId = (int)operationResponse[254];
			this.ChangeLocalID(newId, false);
			if (operationResponse.Parameters.ContainsKey(252))
			{
				int[] actorsInGame = (int[])operationResponse.Parameters[252];
				this.UpdatedActorList(actorsInGame);
			}
			Hashtable actorProperties = (Hashtable)operationResponse[249];
			Hashtable gameProperties = (Hashtable)operationResponse[248];
			this.ReadoutProperties(gameProperties, actorProperties, 0);
			object obj;
			if (operationResponse.Parameters.TryGetValue(191, out obj))
			{
				this.CurrentRoom.InternalCacheRoomFlags((int)obj);
			}
			if (this.CurrentRoom.SuppressRoomEvents)
			{
				this.State = ClientState.Joined;
				this.LocalPlayer.UpdateNickNameOnJoined();
				if (this.lastJoinType == JoinType.CreateRoom || (this.lastJoinType == JoinType.JoinOrCreateRoom && this.LocalPlayer.ActorNumber == 1))
				{
					this.MatchMakingCallbackTargets.OnCreatedRoom();
				}
				this.MatchMakingCallbackTargets.OnJoinedRoom();
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004C50 File Offset: 0x00002E50
		private void UpdatedActorList(int[] actorsInGame)
		{
			if (actorsInGame != null)
			{
				foreach (int num in actorsInGame)
				{
					if (num != 0 && this.CurrentRoom.GetPlayer(num, false) == null)
					{
						this.CurrentRoom.StorePlayer(this.CreatePlayer(string.Empty, num, false, null));
					}
				}
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004CA0 File Offset: 0x00002EA0
		protected internal virtual Player CreatePlayer(string actorName, int actorNumber, bool isLocal, Hashtable actorProperties)
		{
			return new Player(actorName, actorNumber, isLocal, actorProperties);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004CAC File Offset: 0x00002EAC
		protected internal virtual Room CreateRoom(string roomName, RoomOptions opt)
		{
			return new Room(roomName, opt, false);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004CB8 File Offset: 0x00002EB8
		private bool CheckIfOpAllowedOnServer(byte opCode, ServerConnection serverConnection)
		{
			switch (serverConnection)
			{
			case ServerConnection.MasterServer:
				switch (opCode)
				{
				case 217:
				case 218:
				case 219:
				case 221:
				case 222:
				case 225:
				case 226:
				case 227:
				case 228:
				case 229:
				case 230:
				case 231:
					return true;
				}
				break;
			case ServerConnection.GameServer:
				if (opCode <= 227)
				{
					if (opCode - 218 > 1 && opCode - 226 > 1)
					{
						break;
					}
				}
				else if (opCode - 230 > 1 && opCode != 248 && opCode - 251 > 3)
				{
					break;
				}
				return true;
			case ServerConnection.NameServer:
				if (opCode == 218 || opCode == 220 || opCode - 230 <= 1)
				{
					return true;
				}
				break;
			default:
				throw new ArgumentOutOfRangeException("serverConnection", serverConnection, null);
			}
			return false;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004D94 File Offset: 0x00002F94
		private bool CheckIfOpCanBeSent(byte opCode, ServerConnection serverConnection, string opName)
		{
			if (this.LoadBalancingPeer == null)
			{
				this.DebugReturn(DebugLevel.ERROR, string.Format("Operation {0} ({1}) can't be sent because peer is null", opName, opCode));
				return false;
			}
			if (!this.CheckIfOpAllowedOnServer(opCode, serverConnection))
			{
				if (this.LoadBalancingPeer.DebugOut >= DebugLevel.ERROR)
				{
					this.DebugReturn(DebugLevel.ERROR, string.Format("Operation {0} ({1}) not allowed on current server ({2})", opName, opCode, serverConnection));
				}
				return false;
			}
			if (!this.CheckIfClientIsReadyToCallOperation(opCode))
			{
				DebugLevel debugLevel = DebugLevel.ERROR;
				if (opCode == 253 && (this.State == ClientState.Leaving || this.State == ClientState.Disconnecting || this.State == ClientState.DisconnectingFromGameServer))
				{
					debugLevel = DebugLevel.INFO;
				}
				if (this.LoadBalancingPeer.DebugOut >= debugLevel)
				{
					this.DebugReturn(debugLevel, string.Format("Operation {0} ({1}) not called because client is not connected or not ready yet, client state: {2}", opName, opCode, Enum.GetName(typeof(ClientState), this.State)));
				}
				return false;
			}
			if (this.LoadBalancingPeer.PeerState != PeerStateValue.Connected)
			{
				this.DebugReturn(DebugLevel.ERROR, string.Format("Operation {0} ({1}) can't be sent because peer is not connected, peer state: {2}", opName, opCode, this.LoadBalancingPeer.PeerState));
				return false;
			}
			return true;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004EAC File Offset: 0x000030AC
		private bool CheckIfClientIsReadyToCallOperation(byte opCode)
		{
			switch (opCode)
			{
			case 217:
			case 221:
			case 222:
			case 225:
			case 229:
				return this.State == ClientState.ConnectedToMasterServer || this.InLobby;
			case 218:
			case 219:
			case 223:
			case 224:
				break;
			case 220:
				return this.State == ClientState.ConnectedToNameServer;
			case 226:
			case 227:
				return this.State == ClientState.ConnectedToMasterServer || this.InLobby || this.State == ClientState.ConnectedToGameServer;
			case 228:
				return this.InLobby;
			case 230:
			case 231:
				return this.IsConnectedAndReady || this.State == ClientState.ConnectingToNameServer || this.State == ClientState.ConnectingToMasterServer || this.State == ClientState.ConnectingToGameServer;
			default:
				if (opCode == 248 || opCode - 251 <= 3)
				{
					return this.InRoom;
				}
				break;
			}
			return this.IsConnected;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004F90 File Offset: 0x00003190
		public virtual void DebugReturn(DebugLevel level, string message)
		{
			if (this.LoadBalancingPeer.DebugOut != DebugLevel.ALL && level > this.LoadBalancingPeer.DebugOut)
			{
				return;
			}
			if (level == DebugLevel.ERROR)
			{
				UnityEngine.Debug.LogError(message);
				return;
			}
			if (level == DebugLevel.WARNING)
			{
				UnityEngine.Debug.LogWarning(message);
				return;
			}
			if (level == DebugLevel.INFO)
			{
				UnityEngine.Debug.Log(message);
				return;
			}
			if (level == DebugLevel.ALL)
			{
				UnityEngine.Debug.Log(message);
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004FE8 File Offset: 0x000031E8
		private void CallbackRoomEnterFailed(OperationResponse operationResponse)
		{
			if (operationResponse.ReturnCode != 0)
			{
				if (operationResponse.OperationCode == 226)
				{
					this.MatchMakingCallbackTargets.OnJoinRoomFailed(operationResponse.ReturnCode, operationResponse.DebugMessage);
					return;
				}
				if (operationResponse.OperationCode == 227)
				{
					this.MatchMakingCallbackTargets.OnCreateRoomFailed(operationResponse.ReturnCode, operationResponse.DebugMessage);
					return;
				}
				if (operationResponse.OperationCode == 225)
				{
					this.MatchMakingCallbackTargets.OnJoinRandomFailed(operationResponse.ReturnCode, operationResponse.DebugMessage);
				}
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000506C File Offset: 0x0000326C
		public virtual void OnOperationResponse(OperationResponse operationResponse)
		{
			if (operationResponse.Parameters.ContainsKey(221))
			{
				if (this.AuthValues == null)
				{
					this.AuthValues = new AuthenticationValues();
				}
				this.AuthValues.Token = operationResponse.Parameters[221];
				this.tokenCache = this.AuthValues.Token;
			}
			if (operationResponse.ReturnCode == 32743)
			{
				this.Disconnect(DisconnectCause.DisconnectByOperationLimit);
			}
			byte operationCode = operationResponse.OperationCode;
			switch (operationCode)
			{
			case 217:
				if (operationResponse.ReturnCode != 0)
				{
					this.DebugReturn(DebugLevel.ERROR, "GetGameList failed: " + operationResponse.ToStringFull());
				}
				else
				{
					List<RoomInfo> list = new List<RoomInfo>();
					Hashtable hashtable = (Hashtable)operationResponse[222];
					foreach (object obj in hashtable.Keys)
					{
						string text = (string)obj;
						list.Add(new RoomInfo(text, (Hashtable)hashtable[text]));
					}
					this.LobbyCallbackTargets.OnRoomListUpdate(list);
				}
				break;
			case 218:
			case 221:
			case 223:
			case 224:
				break;
			case 219:
				this.WebRpcCallbackTargets.OnWebRpcResponse(operationResponse);
				break;
			case 220:
				if (operationResponse.ReturnCode == 32767)
				{
					this.DebugReturn(DebugLevel.ERROR, string.Format("GetRegions failed. AppId is unknown on the (cloud) server. " + operationResponse.DebugMessage, Array.Empty<object>()));
					this.Disconnect(DisconnectCause.InvalidAuthentication);
				}
				else if (operationResponse.ReturnCode != 0)
				{
					this.DebugReturn(DebugLevel.ERROR, "GetRegions failed. Can't provide regions list. ReturnCode: " + operationResponse.ReturnCode.ToString() + ": " + operationResponse.DebugMessage);
					this.Disconnect(DisconnectCause.InvalidAuthentication);
				}
				else
				{
					if (this.RegionHandler == null)
					{
						this.RegionHandler = new RegionHandler(this.ServerPortOverrides.MasterServerPort);
					}
					if (this.RegionHandler.IsPinging)
					{
						this.DebugReturn(DebugLevel.WARNING, "Received an response for OpGetRegions while the RegionHandler is pinging regions already. Skipping this response in favor of completing the current region-pinging.");
						return;
					}
					this.RegionHandler.SetRegions(operationResponse, this);
					this.ConnectionCallbackTargets.OnRegionListReceived(this.RegionHandler);
					if (this.connectToBestRegion)
					{
						this.RegionHandler.PingMinimumOfRegions(new Action<RegionHandler>(this.OnRegionPingCompleted), this.bestRegionSummaryFromStorage);
					}
				}
				break;
			case 222:
				if (operationResponse.ReturnCode != 0)
				{
					this.DebugReturn(DebugLevel.ERROR, "OpFindFriends failed: " + operationResponse.ToStringFull());
					this.friendListRequested = null;
				}
				else
				{
					bool[] array = operationResponse[1] as bool[];
					string[] array2 = operationResponse[2] as string[];
					List<FriendInfo> list2 = new List<FriendInfo>(this.friendListRequested.Length);
					for (int i = 0; i < this.friendListRequested.Length; i++)
					{
						list2.Insert(i, new FriendInfo
						{
							UserId = this.friendListRequested[i],
							Room = array2[i],
							IsOnline = array[i]
						});
					}
					this.friendListRequested = null;
					this.MatchMakingCallbackTargets.OnFriendListUpdate(list2);
				}
				break;
			case 225:
			case 226:
			case 227:
				if (operationResponse.ReturnCode != 0)
				{
					if (this.Server == ServerConnection.GameServer)
					{
						this.failedRoomEntryOperation = operationResponse;
						this.DisconnectToReconnect();
					}
					else
					{
						this.State = (this.InLobby ? ClientState.JoinedLobby : ClientState.ConnectedToMasterServer);
						this.CallbackRoomEnterFailed(operationResponse);
					}
				}
				else if (this.Server == ServerConnection.GameServer)
				{
					this.GameEnteredOnGameServer(operationResponse);
				}
				else
				{
					this.GameServerAddress = (string)operationResponse[230];
					if (this.ServerPortOverrides.GameServerPort != 0)
					{
						this.GameServerAddress = LoadBalancingClient.ReplacePortWithAlternative(this.GameServerAddress, this.ServerPortOverrides.GameServerPort);
					}
					if (this.AddressRewriter != null)
					{
						this.GameServerAddress = this.AddressRewriter(this.GameServerAddress, ServerConnection.GameServer);
					}
					string text2 = operationResponse[byte.MaxValue] as string;
					if (!string.IsNullOrEmpty(text2))
					{
						this.enterRoomParamsCache.RoomName = text2;
					}
					this.DisconnectToReconnect();
				}
				break;
			case 228:
				this.State = ClientState.ConnectedToMasterServer;
				this.LobbyCallbackTargets.OnLeftLobby();
				break;
			case 229:
				this.State = ClientState.JoinedLobby;
				this.LobbyCallbackTargets.OnJoinedLobby();
				break;
			case 230:
			case 231:
				if (operationResponse.Parameters.ContainsKey(187))
				{
					this.TelemetryEnabled = (bool)operationResponse[187];
				}
				if (operationResponse.ReturnCode != 0)
				{
					this.DebugReturn(DebugLevel.ERROR, string.Concat(new string[]
					{
						operationResponse.ToStringFull(),
						" Server: ",
						this.Server.ToString(),
						" Address: ",
						this.LoadBalancingPeer.ServerAddress
					}));
					short returnCode = operationResponse.ReturnCode;
					if (returnCode - -3 > 1)
					{
						switch (returnCode)
						{
						case 32753:
							this.DisconnectedCause = DisconnectCause.AuthenticationTicketExpired;
							break;
						case 32754:
							break;
						case 32755:
							this.DisconnectedCause = DisconnectCause.CustomAuthenticationFailed;
							this.ConnectionCallbackTargets.OnCustomAuthenticationFailed(operationResponse.DebugMessage);
							break;
						case 32756:
							this.DisconnectedCause = DisconnectCause.InvalidRegion;
							break;
						case 32757:
							this.DisconnectedCause = DisconnectCause.MaxCcuReached;
							break;
						default:
							if (returnCode == 32767)
							{
								this.DisconnectedCause = DisconnectCause.InvalidAuthentication;
							}
							break;
						}
					}
					else
					{
						this.DisconnectedCause = DisconnectCause.OperationNotAllowedInCurrentState;
					}
					this.DisconnectMessage = string.Format("Op: {0} ReturnCode: {1} '{2}'", operationResponse.OperationCode, operationResponse.ReturnCode, operationResponse.DebugMessage);
					this.Disconnect(this.DisconnectedCause);
				}
				else
				{
					if (this.Server == ServerConnection.NameServer || this.Server == ServerConnection.MasterServer)
					{
						if (operationResponse.Parameters.ContainsKey(225))
						{
							string text3 = (string)operationResponse.Parameters[225];
							if (!string.IsNullOrEmpty(text3))
							{
								this.UserId = text3;
								this.LocalPlayer.UserId = text3;
								this.DebugReturn(DebugLevel.INFO, string.Format("Received your UserID from server. Updating local value to: {0}", this.UserId));
							}
						}
						if (operationResponse.Parameters.ContainsKey(202))
						{
							this.NickName = (string)operationResponse.Parameters[202];
							this.DebugReturn(DebugLevel.INFO, string.Format("Received your NickName from server. Updating local value to: {0}", this.NickName));
						}
						if (operationResponse.Parameters.ContainsKey(192))
						{
							this.SetupEncryption((Dictionary<byte, object>)operationResponse.Parameters[192]);
						}
					}
					if (this.Server == ServerConnection.NameServer)
					{
						if (this.AuthMode == AuthModeOption.AuthOnceWss && this.ExpectedProtocol != null)
						{
							this.DebugReturn(DebugLevel.INFO, string.Format("AuthOnceWss mode. Auth response switches TransportProtocol to ExpectedProtocol: {0}.", this.ExpectedProtocol));
							this.LoadBalancingPeer.TransportProtocol = this.ExpectedProtocol.Value;
							this.ExpectedProtocol = null;
						}
						string text4 = operationResponse[196] as string;
						if (!string.IsNullOrEmpty(text4))
						{
							this.CurrentCluster = text4;
						}
						this.MasterServerAddress = (operationResponse[230] as string);
						if (this.ServerPortOverrides.MasterServerPort != 0)
						{
							this.MasterServerAddress = LoadBalancingClient.ReplacePortWithAlternative(this.MasterServerAddress, this.ServerPortOverrides.MasterServerPort);
						}
						if (this.AddressRewriter != null)
						{
							this.MasterServerAddress = this.AddressRewriter(this.MasterServerAddress, ServerConnection.MasterServer);
						}
						this.DisconnectToReconnect();
					}
					else if (this.Server == ServerConnection.MasterServer)
					{
						this.State = ClientState.ConnectedToMasterServer;
						if (this.failedRoomEntryOperation == null)
						{
							this.ConnectionCallbackTargets.OnConnectedToMaster();
						}
						else
						{
							this.CallbackRoomEnterFailed(this.failedRoomEntryOperation);
							this.failedRoomEntryOperation = null;
						}
						if (this.AuthMode != AuthModeOption.Auth)
						{
							this.LoadBalancingPeer.OpSettings(this.EnableLobbyStatistics);
						}
					}
					else if (this.Server == ServerConnection.GameServer)
					{
						this.State = ClientState.Joining;
						if (this.enterRoomParamsCache.JoinMode == JoinMode.RejoinOnly)
						{
							this.enterRoomParamsCache.PlayerProperties = null;
						}
						else
						{
							Hashtable hashtable2 = new Hashtable();
							hashtable2.Merge(this.LocalPlayer.CustomProperties);
							if (!string.IsNullOrEmpty(this.LocalPlayer.NickName))
							{
								hashtable2[byte.MaxValue] = this.LocalPlayer.NickName;
							}
							this.enterRoomParamsCache.PlayerProperties = hashtable2;
						}
						this.enterRoomParamsCache.OnGameServer = true;
						if (this.lastJoinType == JoinType.JoinRoom || this.lastJoinType == JoinType.JoinRandomRoom || this.lastJoinType == JoinType.JoinRandomOrCreateRoom || this.lastJoinType == JoinType.JoinOrCreateRoom)
						{
							this.LoadBalancingPeer.OpJoinRoom(this.enterRoomParamsCache);
							break;
						}
						if (this.lastJoinType == JoinType.CreateRoom)
						{
							this.LoadBalancingPeer.OpCreateRoom(this.enterRoomParamsCache);
							break;
						}
						break;
					}
					Dictionary<string, object> dictionary = (Dictionary<string, object>)operationResponse[245];
					if (dictionary != null)
					{
						this.ConnectionCallbackTargets.OnCustomAuthenticationResponse(dictionary);
					}
				}
				break;
			default:
				if (operationCode == 254)
				{
					this.DisconnectToReconnect();
				}
				break;
			}
			if (this.OpResponseReceived != null)
			{
				this.OpResponseReceived(operationResponse);
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005974 File Offset: 0x00003B74
		public virtual void OnStatusChanged(StatusCode statusCode)
		{
			switch (statusCode)
			{
			case StatusCode.SecurityExceptionOnConnect:
			case StatusCode.ExceptionOnConnect:
			case StatusCode.EncryptionFailedToEstablish:
			{
				this.SystemConnectionSummary = new SystemConnectionSummary(this);
				this.DebugReturn(DebugLevel.ERROR, string.Format("Connection lost. OnStatusChanged to {0}. Client state was: {1}. {2}", statusCode, this.State, this.SystemConnectionSummary.ToString()));
				this.DisconnectedCause = DisconnectCause.ExceptionOnConnect;
				ClientState clientState = ClientState.Disconnecting;
				if (this.State == ClientState.ConnectingToNameServer && this.EnableProtocolFallback && this.LoadBalancingPeer.UsedProtocol != ConnectionProtocol.WebSocketSecure)
				{
					clientState = ClientState.ConnectWithFallbackProtocol;
				}
				this.State = clientState;
				return;
			}
			case StatusCode.Connect:
			{
				int connectCount = this.ConnectCount;
				this.ConnectCount = connectCount + 1;
				this.telemetrySent = false;
				if (this.State == ClientState.ConnectingToNameServer)
				{
					if (this.LoadBalancingPeer.DebugOut >= DebugLevel.ALL)
					{
						this.DebugReturn(DebugLevel.ALL, "Connected to nameserver.");
					}
					this.Server = ServerConnection.NameServer;
					if (this.AuthValues != null)
					{
						this.AuthValues.Token = null;
					}
				}
				if (this.State == ClientState.ConnectingToGameServer)
				{
					if (this.LoadBalancingPeer.DebugOut >= DebugLevel.ALL)
					{
						this.DebugReturn(DebugLevel.ALL, "Connected to gameserver.");
					}
					this.Server = ServerConnection.GameServer;
				}
				if (this.State == ClientState.ConnectingToMasterServer)
				{
					if (this.LoadBalancingPeer.DebugOut >= DebugLevel.ALL)
					{
						this.DebugReturn(DebugLevel.ALL, "Connected to masterserver.");
					}
					this.Server = ServerConnection.MasterServer;
					this.ConnectionCallbackTargets.OnConnected();
				}
				if (this.LoadBalancingPeer.TransportProtocol != ConnectionProtocol.WebSocketSecure)
				{
					if (this.Server == ServerConnection.NameServer || this.AuthMode == AuthModeOption.Auth)
					{
						this.LoadBalancingPeer.EstablishEncryption();
						return;
					}
					return;
				}
				break;
			}
			case StatusCode.Disconnect:
			{
				this.friendListRequested = null;
				bool flag = this.CurrentRoom != null;
				this.CurrentRoom = null;
				this.ChangeLocalID(-1, false);
				if (this.Server == ServerConnection.GameServer && flag)
				{
					this.MatchMakingCallbackTargets.OnLeftRoom();
				}
				if (this.ExpectedProtocol != null)
				{
					ConnectionProtocol transportProtocol = this.LoadBalancingPeer.TransportProtocol;
					ConnectionProtocol? expectedProtocol = this.ExpectedProtocol;
					if (!(transportProtocol == expectedProtocol.GetValueOrDefault() & expectedProtocol != null))
					{
						this.DebugReturn(DebugLevel.INFO, string.Format("On disconnect switches TransportProtocol to ExpectedProtocol: {0}.", this.ExpectedProtocol));
						this.LoadBalancingPeer.TransportProtocol = this.ExpectedProtocol.Value;
						this.ExpectedProtocol = null;
					}
				}
				ClientState clientState2 = this.State;
				if (clientState2 != ClientState.PeerCreated)
				{
					if (clientState2 != ClientState.DisconnectingFromMasterServer)
					{
						switch (clientState2)
						{
						case ClientState.DisconnectingFromGameServer:
						case ClientState.DisconnectingFromNameServer:
							this.ConnectToMasterServer();
							return;
						case ClientState.Disconnecting:
							goto IL_32F;
						case ClientState.Disconnected:
							return;
						case ClientState.ConnectWithFallbackProtocol:
							this.EnableProtocolFallback = false;
							this.LoadBalancingPeer.TransportProtocol = ConnectionProtocol.WebSocketSecure;
							this.NameServerPortInAppSettings = 0;
							this.ServerPortOverrides = default(PhotonPortDefinition);
							if (!this.LoadBalancingPeer.Connect(this.NameServerAddress, this.ProxyServerAddress, this.AppId, this.TokenForInit, null))
							{
								return;
							}
							this.State = ClientState.ConnectingToNameServer;
							return;
						}
						string text = "";
						this.DebugReturn(DebugLevel.WARNING, string.Concat(new string[]
						{
							"Got a unexpected Disconnect in LoadBalancingClient State: ",
							this.State.ToString(),
							". Server: ",
							this.Server.ToString(),
							" Trace: ",
							text
						}));
						if (this.AuthValues != null)
						{
							this.AuthValues.Token = null;
						}
						this.State = ClientState.Disconnected;
						this.ConnectionCallbackTargets.OnDisconnected(this.DisconnectedCause);
						return;
					}
					this.Connect(this.GameServerAddress, this.ProxyServerAddress, ServerConnection.GameServer);
					return;
				}
				IL_32F:
				if (this.AuthValues != null)
				{
					this.AuthValues.Token = null;
				}
				this.State = ClientState.Disconnected;
				this.ConnectionCallbackTargets.OnDisconnected(this.DisconnectedCause);
				return;
			}
			case StatusCode.Exception:
			case StatusCode.SendError:
			case StatusCode.ExceptionOnReceive:
				this.SystemConnectionSummary = new SystemConnectionSummary(this);
				this.DebugReturn(DebugLevel.ERROR, string.Format("Connection lost. OnStatusChanged to {0}. Client state was: {1}. {2}", statusCode, this.State, this.SystemConnectionSummary.ToString()));
				this.DisconnectedCause = DisconnectCause.Exception;
				this.State = ClientState.Disconnecting;
				return;
			case (StatusCode)1027:
			case (StatusCode)1028:
			case (StatusCode)1029:
			case (StatusCode)1031:
			case (StatusCode)1032:
			case (StatusCode)1033:
			case (StatusCode)1034:
			case (StatusCode)1035:
			case (StatusCode)1036:
			case (StatusCode)1037:
			case (StatusCode)1038:
			case (StatusCode)1045:
			case (StatusCode)1046:
			case (StatusCode)1047:
				return;
			case StatusCode.TimeoutDisconnect:
			{
				this.SystemConnectionSummary = new SystemConnectionSummary(this);
				this.DebugReturn(DebugLevel.ERROR, string.Format("Connection lost. OnStatusChanged to {0}. Client state was: {1}. {2}", statusCode, this.State, this.SystemConnectionSummary.ToString()));
				this.DisconnectedCause = DisconnectCause.ClientTimeout;
				ClientState clientState = ClientState.Disconnecting;
				if (this.State == ClientState.ConnectingToNameServer && this.EnableProtocolFallback && this.LoadBalancingPeer.UsedProtocol != ConnectionProtocol.WebSocketSecure)
				{
					clientState = ClientState.ConnectWithFallbackProtocol;
				}
				this.State = clientState;
				return;
			}
			case StatusCode.DisconnectByServerTimeout:
				this.SystemConnectionSummary = new SystemConnectionSummary(this);
				this.DebugReturn(DebugLevel.ERROR, string.Format("Connection lost. OnStatusChanged to {0}. Client state was: {1}. {2}", statusCode, this.State, this.SystemConnectionSummary.ToString()));
				this.DisconnectedCause = DisconnectCause.ServerTimeout;
				this.State = ClientState.Disconnecting;
				return;
			case StatusCode.DisconnectByServerUserLimit:
				this.DebugReturn(DebugLevel.ERROR, "This connection was rejected due to the apps CCU limit.");
				this.DisconnectedCause = DisconnectCause.MaxCcuReached;
				this.State = ClientState.Disconnecting;
				return;
			case StatusCode.DisconnectByServerLogic:
				this.DisconnectedCause = DisconnectCause.DisconnectByServerLogic;
				this.State = ClientState.Disconnecting;
				return;
			case StatusCode.DisconnectByServerReasonUnknown:
				this.DisconnectedCause = DisconnectCause.DisconnectByServerReasonUnknown;
				this.State = ClientState.Disconnecting;
				return;
			case StatusCode.EncryptionEstablished:
				break;
			case StatusCode.ServerAddressInvalid:
				this.DisconnectedCause = DisconnectCause.ServerAddressInvalid;
				this.State = ClientState.Disconnecting;
				return;
			case StatusCode.DnsExceptionOnConnect:
				this.DisconnectedCause = DisconnectCause.DnsExceptionOnConnect;
				this.State = ClientState.Disconnecting;
				return;
			default:
				return;
			}
			if (this.Server == ServerConnection.NameServer)
			{
				this.State = ClientState.ConnectedToNameServer;
				if (string.IsNullOrEmpty(this.CloudRegion))
				{
					this.OpGetRegions();
					return;
				}
			}
			else if (this.AuthMode == AuthModeOption.AuthOnce || this.AuthMode == AuthModeOption.AuthOnceWss)
			{
				return;
			}
			if (this.CallAuthenticate())
			{
				this.State = ClientState.Authenticating;
				return;
			}
			this.DebugReturn(DebugLevel.ERROR, "OpAuthenticate failed. Check log output and AuthValues. State: " + this.State.ToString());
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005F5C File Offset: 0x0000415C
		public virtual void OnEvent(EventData photonEvent)
		{
			int sender = photonEvent.Sender;
			Player player = (this.CurrentRoom != null) ? this.CurrentRoom.GetPlayer(sender, false) : null;
			byte code = photonEvent.Code;
			switch (code)
			{
			case 223:
				if (this.AuthValues == null)
				{
					this.AuthValues = new AuthenticationValues();
				}
				this.AuthValues.Token = photonEvent[221];
				this.tokenCache = this.AuthValues.Token;
				break;
			case 224:
			{
				string[] array = photonEvent[213] as string[];
				int[] array2 = photonEvent[229] as int[];
				int[] array3 = photonEvent[228] as int[];
				ByteArraySlice byteArraySlice = photonEvent[212] as ByteArraySlice;
				bool flag = byteArraySlice != null;
				byte[] array4;
				if (flag)
				{
					array4 = byteArraySlice.Buffer;
				}
				else
				{
					array4 = (photonEvent[212] as byte[]);
				}
				this.lobbyStatistics.Clear();
				for (int i = 0; i < array.Length; i++)
				{
					TypedLobbyInfo typedLobbyInfo = new TypedLobbyInfo();
					typedLobbyInfo.Name = array[i];
					typedLobbyInfo.Type = (LobbyType)array4[i];
					typedLobbyInfo.PlayerCount = array2[i];
					typedLobbyInfo.RoomCount = array3[i];
					this.lobbyStatistics.Add(typedLobbyInfo);
				}
				if (flag)
				{
					byteArraySlice.Release();
				}
				this.LobbyCallbackTargets.OnLobbyStatisticsUpdate(this.lobbyStatistics);
				break;
			}
			case 225:
			case 227:
			case 228:
				break;
			case 226:
				this.PlayersInRoomsCount = (int)photonEvent[229];
				this.RoomsCount = (int)photonEvent[228];
				this.PlayersOnMasterCount = (int)photonEvent[227];
				break;
			case 229:
			case 230:
			{
				List<RoomInfo> list = new List<RoomInfo>();
				Hashtable hashtable = (Hashtable)photonEvent[222];
				foreach (object obj in hashtable.Keys)
				{
					string text = (string)obj;
					list.Add(new RoomInfo(text, (Hashtable)hashtable[text]));
				}
				this.LobbyCallbackTargets.OnRoomListUpdate(list);
				break;
			}
			default:
				switch (code)
				{
				case 251:
					this.ErrorInfoCallbackTargets.OnErrorInfo(new ErrorInfo(photonEvent));
					break;
				case 253:
				{
					int num = 0;
					if (photonEvent.Parameters.ContainsKey(253))
					{
						num = (int)photonEvent[253];
					}
					Hashtable gameProperties = null;
					Hashtable actorProperties = null;
					if (num == 0)
					{
						gameProperties = (Hashtable)photonEvent[251];
					}
					else
					{
						actorProperties = (Hashtable)photonEvent[251];
					}
					this.ReadoutProperties(gameProperties, actorProperties, num);
					break;
				}
				case 254:
					if (player != null)
					{
						bool flag2 = false;
						if (photonEvent.Parameters.ContainsKey(233))
						{
							flag2 = (bool)photonEvent.Parameters[233];
						}
						player.IsInactive = flag2;
						player.HasRejoined = false;
						if (!flag2)
						{
							this.CurrentRoom.RemovePlayer(sender);
						}
					}
					if (photonEvent.Parameters.ContainsKey(203))
					{
						int num2 = (int)photonEvent[203];
						if (num2 != 0)
						{
							this.CurrentRoom.masterClientId = num2;
							this.InRoomCallbackTargets.OnMasterClientSwitched(this.CurrentRoom.GetPlayer(num2, false));
						}
					}
					this.InRoomCallbackTargets.OnPlayerLeftRoom(player);
					break;
				case 255:
				{
					Hashtable hashtable2 = (Hashtable)photonEvent[249];
					if (player == null)
					{
						if (sender > 0)
						{
							player = this.CreatePlayer(string.Empty, sender, false, hashtable2);
							this.CurrentRoom.StorePlayer(player);
						}
					}
					else
					{
						player.InternalCacheProperties(hashtable2);
						player.IsInactive = false;
						player.HasRejoined = (sender != this.LocalPlayer.ActorNumber);
					}
					if (sender == this.LocalPlayer.ActorNumber)
					{
						int[] actorsInGame = (int[])photonEvent[252];
						this.UpdatedActorList(actorsInGame);
						player.HasRejoined = (this.enterRoomParamsCache != null && this.enterRoomParamsCache.JoinMode == JoinMode.RejoinOnly);
						this.State = ClientState.Joined;
						this.LocalPlayer.UpdateNickNameOnJoined();
						if (this.lastJoinType == JoinType.CreateRoom || (this.lastJoinType == JoinType.JoinOrCreateRoom && this.LocalPlayer.ActorNumber == 1))
						{
							this.MatchMakingCallbackTargets.OnCreatedRoom();
						}
						this.MatchMakingCallbackTargets.OnJoinedRoom();
					}
					else
					{
						this.InRoomCallbackTargets.OnPlayerEnteredRoom(player);
					}
					break;
				}
				}
				break;
			}
			this.UpdateCallbackTargets();
			if (this.EventReceived != null)
			{
				this.EventReceived(photonEvent);
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00006428 File Offset: 0x00004628
		public virtual void OnMessage(object message)
		{
			this.DebugReturn(DebugLevel.ALL, string.Format("got OnMessage {0}", message));
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000643C File Offset: 0x0000463C
		private void OnDisconnectMessageReceived(DisconnectMessage obj)
		{
			this.DebugReturn(DebugLevel.ERROR, string.Format("Got DisconnectMessage. Code: {0} Msg: \"{1}\". Debug Info: {2}", obj.Code, obj.DebugMessage, obj.Parameters.ToStringFull()));
			this.DisconnectMessage = string.Format("DisconnectMessage {0}: {1}", obj.Code, obj.DebugMessage);
			this.Disconnect(DisconnectCause.DisconnectByDisconnectMessage);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000649F File Offset: 0x0000469F
		private void OnRegionPingCompleted(RegionHandler regionHandler)
		{
			this.SummaryToCache = regionHandler.SummaryToCache;
			this.ConnectToRegionMaster(regionHandler.BestRegion.Code);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000064C0 File Offset: 0x000046C0
		protected internal static string ReplacePortWithAlternative(string address, ushort replacementPort)
		{
			if (string.IsNullOrEmpty(address) || replacementPort == 0)
			{
				return address;
			}
			if (address.StartsWith("ws"))
			{
				return new UriBuilder(address)
				{
					Port = (int)replacementPort
				}.ToString();
			}
			UriBuilder uriBuilder = new UriBuilder("scheme://" + address);
			return string.Format("{0}:{1}", uriBuilder.Host, replacementPort);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00006524 File Offset: 0x00004724
		private void SetupEncryption(Dictionary<byte, object> encryptionData)
		{
			EncryptionMode encryptionMode = (EncryptionMode)((byte)encryptionData[0]);
			if (encryptionMode == EncryptionMode.PayloadEncryption)
			{
				byte[] secret = (byte[])encryptionData[1];
				this.LoadBalancingPeer.InitPayloadEncryption(secret);
				return;
			}
			if (encryptionMode != EncryptionMode.DatagramEncryptionGCM)
			{
				throw new ArgumentOutOfRangeException();
			}
			byte[] encryptionSecret = (byte[])encryptionData[1];
			this.LoadBalancingPeer.InitDatagramEncryption(encryptionSecret, null, true, true);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00006588 File Offset: 0x00004788
		public bool OpWebRpc(string uriPath, object parameters, bool sendAuthCookie = false)
		{
			if (string.IsNullOrEmpty(uriPath))
			{
				this.DebugReturn(DebugLevel.ERROR, "WebRPC method name must not be null nor empty.");
				return false;
			}
			if (!this.CheckIfOpCanBeSent(219, this.Server, "WebRpc"))
			{
				return false;
			}
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			dictionary.Add(209, uriPath);
			if (parameters != null)
			{
				dictionary.Add(208, parameters);
			}
			if (sendAuthCookie)
			{
				dictionary.Add(234, 2);
			}
			return this.LoadBalancingPeer.SendOperation(219, dictionary, SendOptions.SendReliable);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00006610 File Offset: 0x00004810
		public void AddCallbackTarget(object target)
		{
			this.callbackTargetChanges.Enqueue(new LoadBalancingClient.CallbackTargetChange(target, true));
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00006624 File Offset: 0x00004824
		public void RemoveCallbackTarget(object target)
		{
			this.callbackTargetChanges.Enqueue(new LoadBalancingClient.CallbackTargetChange(target, false));
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00006638 File Offset: 0x00004838
		protected internal void UpdateCallbackTargets()
		{
			while (this.callbackTargetChanges.Count > 0)
			{
				LoadBalancingClient.CallbackTargetChange callbackTargetChange = this.callbackTargetChanges.Dequeue();
				if (callbackTargetChange.AddTarget)
				{
					if (this.callbackTargets.Contains(callbackTargetChange.Target))
					{
						continue;
					}
					this.callbackTargets.Add(callbackTargetChange.Target);
				}
				else
				{
					if (!this.callbackTargets.Contains(callbackTargetChange.Target))
					{
						continue;
					}
					this.callbackTargets.Remove(callbackTargetChange.Target);
				}
				this.UpdateCallbackTarget<IInRoomCallbacks>(callbackTargetChange, this.InRoomCallbackTargets);
				this.UpdateCallbackTarget<IConnectionCallbacks>(callbackTargetChange, this.ConnectionCallbackTargets);
				this.UpdateCallbackTarget<IMatchmakingCallbacks>(callbackTargetChange, this.MatchMakingCallbackTargets);
				this.UpdateCallbackTarget<ILobbyCallbacks>(callbackTargetChange, this.LobbyCallbackTargets);
				this.UpdateCallbackTarget<IWebRpcCallback>(callbackTargetChange, this.WebRpcCallbackTargets);
				this.UpdateCallbackTarget<IErrorInfoCallback>(callbackTargetChange, this.ErrorInfoCallbackTargets);
				IOnEventCallback onEventCallback = callbackTargetChange.Target as IOnEventCallback;
				if (onEventCallback != null)
				{
					if (callbackTargetChange.AddTarget)
					{
						this.EventReceived += onEventCallback.OnEvent;
					}
					else
					{
						this.EventReceived -= onEventCallback.OnEvent;
					}
				}
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00006750 File Offset: 0x00004950
		private void UpdateCallbackTarget<T>(LoadBalancingClient.CallbackTargetChange change, List<T> container) where T : class
		{
			T t = change.Target as T;
			if (t != null)
			{
				if (change.AddTarget)
				{
					container.Add(t);
					return;
				}
				container.Remove(t);
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000678E File Offset: 0x0000498E
		// Note: this type is marked as 'beforefieldinit'.
		static LoadBalancingClient()
		{
		}

		// Token: 0x04000070 RID: 112
		[CompilerGenerated]
		private LoadBalancingPeer <LoadBalancingPeer>k__BackingField;

		// Token: 0x04000071 RID: 113
		[CompilerGenerated]
		private string <AppVersion>k__BackingField;

		// Token: 0x04000072 RID: 114
		[CompilerGenerated]
		private string <AppId>k__BackingField;

		// Token: 0x04000073 RID: 115
		[CompilerGenerated]
		private ClientAppType <ClientType>k__BackingField;

		// Token: 0x04000074 RID: 116
		[CompilerGenerated]
		private AuthenticationValues <AuthValues>k__BackingField;

		// Token: 0x04000075 RID: 117
		public AuthModeOption AuthMode;

		// Token: 0x04000076 RID: 118
		public EncryptionMode EncryptionMode;

		// Token: 0x04000077 RID: 119
		[CompilerGenerated]
		private ConnectionProtocol? <ExpectedProtocol>k__BackingField;

		// Token: 0x04000078 RID: 120
		private object tokenCache;

		// Token: 0x04000079 RID: 121
		[CompilerGenerated]
		private bool <IsUsingNameServer>k__BackingField;

		// Token: 0x0400007A RID: 122
		public string NameServerHost = "ns.photonengine.io";

		// Token: 0x0400007B RID: 123
		private static readonly Dictionary<ConnectionProtocol, int> ProtocolToNameServerPort = new Dictionary<ConnectionProtocol, int>
		{
			{
				ConnectionProtocol.Udp,
				5058
			},
			{
				ConnectionProtocol.Tcp,
				4533
			},
			{
				ConnectionProtocol.WebSocket,
				80
			},
			{
				ConnectionProtocol.WebSocketSecure,
				443
			}
		};

		// Token: 0x0400007C RID: 124
		[CompilerGenerated]
		private bool <UseAlternativeUdpPorts>k__BackingField;

		// Token: 0x0400007D RID: 125
		public PhotonPortDefinition ServerPortOverrides;

		// Token: 0x0400007E RID: 126
		[CompilerGenerated]
		private bool <EnableProtocolFallback>k__BackingField;

		// Token: 0x0400007F RID: 127
		[CompilerGenerated]
		private string <MasterServerAddress>k__BackingField;

		// Token: 0x04000080 RID: 128
		[CompilerGenerated]
		private string <GameServerAddress>k__BackingField;

		// Token: 0x04000081 RID: 129
		public Func<string, ServerConnection, string> AddressRewriter;

		// Token: 0x04000082 RID: 130
		[CompilerGenerated]
		private ServerConnection <Server>k__BackingField;

		// Token: 0x04000083 RID: 131
		public string ProxyServerAddress;

		// Token: 0x04000084 RID: 132
		[CompilerGenerated]
		private int <ConnectCount>k__BackingField;

		// Token: 0x04000085 RID: 133
		private ClientState state;

		// Token: 0x04000086 RID: 134
		[CompilerGenerated]
		private Action<ClientState, ClientState> StateChanged;

		// Token: 0x04000087 RID: 135
		[CompilerGenerated]
		private Action<EventData> EventReceived;

		// Token: 0x04000088 RID: 136
		[CompilerGenerated]
		private Action<OperationResponse> OpResponseReceived;

		// Token: 0x04000089 RID: 137
		public ConnectionCallbacksContainer ConnectionCallbackTargets;

		// Token: 0x0400008A RID: 138
		public MatchMakingCallbacksContainer MatchMakingCallbackTargets;

		// Token: 0x0400008B RID: 139
		internal InRoomCallbacksContainer InRoomCallbackTargets;

		// Token: 0x0400008C RID: 140
		internal LobbyCallbacksContainer LobbyCallbackTargets;

		// Token: 0x0400008D RID: 141
		internal WebRpcCallbacksContainer WebRpcCallbackTargets;

		// Token: 0x0400008E RID: 142
		internal ErrorInfoCallbacksContainer ErrorInfoCallbackTargets;

		// Token: 0x0400008F RID: 143
		[CompilerGenerated]
		private DisconnectCause <DisconnectedCause>k__BackingField;

		// Token: 0x04000090 RID: 144
		public string DisconnectMessage;

		// Token: 0x04000091 RID: 145
		public bool TelemetryEnabled;

		// Token: 0x04000092 RID: 146
		private bool telemetrySent;

		// Token: 0x04000093 RID: 147
		public SystemConnectionSummary SystemConnectionSummary;

		// Token: 0x04000094 RID: 148
		[CompilerGenerated]
		private TypedLobby <CurrentLobby>k__BackingField;

		// Token: 0x04000095 RID: 149
		public bool EnableLobbyStatistics;

		// Token: 0x04000096 RID: 150
		private readonly List<TypedLobbyInfo> lobbyStatistics = new List<TypedLobbyInfo>();

		// Token: 0x04000097 RID: 151
		[CompilerGenerated]
		private Player <LocalPlayer>k__BackingField;

		// Token: 0x04000098 RID: 152
		[CompilerGenerated]
		private Room <CurrentRoom>k__BackingField;

		// Token: 0x04000099 RID: 153
		[CompilerGenerated]
		private int <PlayersOnMasterCount>k__BackingField;

		// Token: 0x0400009A RID: 154
		[CompilerGenerated]
		private int <PlayersInRoomsCount>k__BackingField;

		// Token: 0x0400009B RID: 155
		[CompilerGenerated]
		private int <RoomsCount>k__BackingField;

		// Token: 0x0400009C RID: 156
		private JoinType lastJoinType;

		// Token: 0x0400009D RID: 157
		private EnterRoomParams enterRoomParamsCache;

		// Token: 0x0400009E RID: 158
		private OperationResponse failedRoomEntryOperation;

		// Token: 0x0400009F RID: 159
		private const int FriendRequestListMax = 512;

		// Token: 0x040000A0 RID: 160
		private string[] friendListRequested;

		// Token: 0x040000A1 RID: 161
		[CompilerGenerated]
		private string <CloudRegion>k__BackingField;

		// Token: 0x040000A2 RID: 162
		[CompilerGenerated]
		private string <CurrentCluster>k__BackingField;

		// Token: 0x040000A3 RID: 163
		public RegionHandler RegionHandler;

		// Token: 0x040000A4 RID: 164
		private string bestRegionSummaryFromStorage;

		// Token: 0x040000A5 RID: 165
		public string SummaryToCache;

		// Token: 0x040000A6 RID: 166
		private bool connectToBestRegion = true;

		// Token: 0x040000A7 RID: 167
		private readonly Queue<LoadBalancingClient.CallbackTargetChange> callbackTargetChanges = new Queue<LoadBalancingClient.CallbackTargetChange>();

		// Token: 0x040000A8 RID: 168
		private readonly HashSet<object> callbackTargets = new HashSet<object>();

		// Token: 0x040000A9 RID: 169
		public int NameServerPortInAppSettings;

		// Token: 0x02000043 RID: 67
		private class EncryptionDataParameters
		{
			// Token: 0x06000224 RID: 548 RVA: 0x0000B648 File Offset: 0x00009848
			public EncryptionDataParameters()
			{
			}

			// Token: 0x0400021C RID: 540
			public const byte Mode = 0;

			// Token: 0x0400021D RID: 541
			public const byte Secret1 = 1;

			// Token: 0x0400021E RID: 542
			public const byte Secret2 = 2;
		}

		// Token: 0x02000044 RID: 68
		private class CallbackTargetChange
		{
			// Token: 0x06000225 RID: 549 RVA: 0x0000B650 File Offset: 0x00009850
			public CallbackTargetChange(object target, bool addTarget)
			{
				this.Target = target;
				this.AddTarget = addTarget;
			}

			// Token: 0x0400021F RID: 543
			public readonly object Target;

			// Token: 0x04000220 RID: 544
			public readonly bool AddTarget;
		}
	}
}
