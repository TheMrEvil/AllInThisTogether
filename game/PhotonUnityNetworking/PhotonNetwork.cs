using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Photon.Pun
{
	// Token: 0x02000012 RID: 18
	public static class PhotonNetwork
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000027FE File Offset: 0x000009FE
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002805 File Offset: 0x00000A05
		public static string GameVersion
		{
			get
			{
				return PhotonNetwork.gameVersion;
			}
			set
			{
				PhotonNetwork.gameVersion = value;
				PhotonNetwork.NetworkingClient.AppVersion = string.Format("{0}_{1}", value, "2.50");
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002827 File Offset: 0x00000A27
		public static string AppVersion
		{
			get
			{
				return PhotonNetwork.NetworkingClient.AppVersion;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002833 File Offset: 0x00000A33
		// (set) Token: 0x0600002A RID: 42 RVA: 0x0000284D File Offset: 0x00000A4D
		public static ServerSettings PhotonServerSettings
		{
			get
			{
				if (PhotonNetwork.photonServerSettings == null)
				{
					PhotonNetwork.LoadOrCreateSettings(false);
				}
				return PhotonNetwork.photonServerSettings;
			}
			private set
			{
				PhotonNetwork.photonServerSettings = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002855 File Offset: 0x00000A55
		public static string ServerAddress
		{
			get
			{
				if (PhotonNetwork.NetworkingClient == null)
				{
					return "<not connected>";
				}
				return PhotonNetwork.NetworkingClient.CurrentServerAddress;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000286E File Offset: 0x00000A6E
		public static string CloudRegion
		{
			get
			{
				if (PhotonNetwork.NetworkingClient == null || !PhotonNetwork.IsConnected || PhotonNetwork.Server == ServerConnection.NameServer)
				{
					return null;
				}
				return PhotonNetwork.NetworkingClient.CloudRegion;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002892 File Offset: 0x00000A92
		public static string CurrentCluster
		{
			get
			{
				if (PhotonNetwork.NetworkingClient == null)
				{
					return null;
				}
				return PhotonNetwork.NetworkingClient.CurrentCluster;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000028A7 File Offset: 0x00000AA7
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000028B4 File Offset: 0x00000AB4
		public static string BestRegionSummaryInPreferences
		{
			get
			{
				return PlayerPrefs.GetString("PUNCloudBestRegion", null);
			}
			internal set
			{
				if (string.IsNullOrEmpty(value))
				{
					PlayerPrefs.DeleteKey("PUNCloudBestRegion");
					return;
				}
				PlayerPrefs.SetString("PUNCloudBestRegion", value.ToString());
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000028D9 File Offset: 0x00000AD9
		public static bool IsConnected
		{
			get
			{
				return PhotonNetwork.OfflineMode || (PhotonNetwork.NetworkingClient != null && PhotonNetwork.NetworkingClient.IsConnected);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000028F7 File Offset: 0x00000AF7
		public static bool IsConnectedAndReady
		{
			get
			{
				return PhotonNetwork.OfflineMode || (PhotonNetwork.NetworkingClient != null && PhotonNetwork.NetworkingClient.IsConnectedAndReady);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002915 File Offset: 0x00000B15
		public static ClientState NetworkClientState
		{
			get
			{
				if (PhotonNetwork.OfflineMode)
				{
					if (PhotonNetwork.offlineModeRoom == null)
					{
						return ClientState.ConnectedToMasterServer;
					}
					return ClientState.Joined;
				}
				else
				{
					if (PhotonNetwork.NetworkingClient == null)
					{
						return ClientState.Disconnected;
					}
					return PhotonNetwork.NetworkingClient.State;
				}
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000293F File Offset: 0x00000B3F
		public static ServerConnection Server
		{
			get
			{
				if (PhotonNetwork.OfflineMode)
				{
					if (PhotonNetwork.CurrentRoom != null)
					{
						return ServerConnection.GameServer;
					}
					return ServerConnection.MasterServer;
				}
				else
				{
					if (PhotonNetwork.NetworkingClient == null)
					{
						return ServerConnection.NameServer;
					}
					return PhotonNetwork.NetworkingClient.Server;
				}
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002966 File Offset: 0x00000B66
		// (set) Token: 0x06000035 RID: 53 RVA: 0x0000297B File Offset: 0x00000B7B
		public static AuthenticationValues AuthValues
		{
			get
			{
				if (PhotonNetwork.NetworkingClient == null)
				{
					return null;
				}
				return PhotonNetwork.NetworkingClient.AuthValues;
			}
			set
			{
				if (PhotonNetwork.NetworkingClient != null)
				{
					PhotonNetwork.NetworkingClient.AuthValues = value;
				}
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000298F File Offset: 0x00000B8F
		public static TypedLobby CurrentLobby
		{
			get
			{
				return PhotonNetwork.NetworkingClient.CurrentLobby;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000299B File Offset: 0x00000B9B
		public static Room CurrentRoom
		{
			get
			{
				if (PhotonNetwork.offlineMode)
				{
					return PhotonNetwork.offlineModeRoom;
				}
				if (PhotonNetwork.NetworkingClient != null)
				{
					return PhotonNetwork.NetworkingClient.CurrentRoom;
				}
				return null;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000029BD File Offset: 0x00000BBD
		public static Player LocalPlayer
		{
			get
			{
				if (PhotonNetwork.NetworkingClient == null)
				{
					return null;
				}
				return PhotonNetwork.NetworkingClient.LocalPlayer;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000029D2 File Offset: 0x00000BD2
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000029DE File Offset: 0x00000BDE
		public static string NickName
		{
			get
			{
				return PhotonNetwork.NetworkingClient.NickName;
			}
			set
			{
				PhotonNetwork.NetworkingClient.NickName = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000029EC File Offset: 0x00000BEC
		public static Player[] PlayerList
		{
			get
			{
				Room currentRoom = PhotonNetwork.CurrentRoom;
				if (currentRoom != null)
				{
					return (from x in currentRoom.Players.Values
					orderby x.ActorNumber
					select x).ToArray<Player>();
				}
				return new Player[0];
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002A40 File Offset: 0x00000C40
		public static Player[] PlayerListOthers
		{
			get
			{
				Room currentRoom = PhotonNetwork.CurrentRoom;
				if (currentRoom != null)
				{
					return (from x in currentRoom.Players.Values
					orderby x.ActorNumber
					where !x.IsLocal
					select x).ToArray<Player>();
				}
				return new Player[0];
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002AB5 File Offset: 0x00000CB5
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002ABC File Offset: 0x00000CBC
		public static bool OfflineMode
		{
			get
			{
				return PhotonNetwork.offlineMode;
			}
			set
			{
				if (value == PhotonNetwork.offlineMode)
				{
					return;
				}
				if (value && PhotonNetwork.IsConnected)
				{
					UnityEngine.Debug.LogError("Can't start OFFLINE mode while connected!");
					return;
				}
				if (PhotonNetwork.NetworkingClient.IsConnected)
				{
					PhotonNetwork.NetworkingClient.Disconnect();
				}
				PhotonNetwork.offlineMode = value;
				if (PhotonNetwork.offlineMode)
				{
					PhotonNetwork.NetworkingClient.ChangeLocalID(-1, true);
					PhotonNetwork.NetworkingClient.ConnectionCallbackTargets.OnConnectedToMaster();
					return;
				}
				bool flag = PhotonNetwork.offlineModeRoom != null;
				if (flag)
				{
					PhotonNetwork.LeftRoomCleanup();
				}
				PhotonNetwork.offlineModeRoom = null;
				PhotonNetwork.NetworkingClient.CurrentRoom = null;
				PhotonNetwork.NetworkingClient.ChangeLocalID(-1, false);
				if (flag)
				{
					PhotonNetwork.NetworkingClient.MatchMakingCallbackTargets.OnLeftRoom();
				}
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002B64 File Offset: 0x00000D64
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002B6B File Offset: 0x00000D6B
		public static bool AutomaticallySyncScene
		{
			get
			{
				return PhotonNetwork.automaticallySyncScene;
			}
			set
			{
				PhotonNetwork.automaticallySyncScene = value;
				if (PhotonNetwork.automaticallySyncScene && PhotonNetwork.CurrentRoom != null)
				{
					PhotonNetwork.LoadLevelIfSynced();
				}
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002B86 File Offset: 0x00000D86
		public static bool EnableLobbyStatistics
		{
			get
			{
				return PhotonNetwork.NetworkingClient.EnableLobbyStatistics;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002B92 File Offset: 0x00000D92
		public static bool InLobby
		{
			get
			{
				return PhotonNetwork.NetworkingClient.InLobby;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002B9E File Offset: 0x00000D9E
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002BAB File Offset: 0x00000DAB
		public static int SendRate
		{
			get
			{
				return 1000 / PhotonNetwork.sendFrequency;
			}
			set
			{
				PhotonNetwork.sendFrequency = 1000 / value;
				if (PhotonHandler.Instance != null)
				{
					PhotonHandler.Instance.UpdateInterval = PhotonNetwork.sendFrequency;
				}
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002BD5 File Offset: 0x00000DD5
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002BE2 File Offset: 0x00000DE2
		public static int SerializationRate
		{
			get
			{
				return 1000 / PhotonNetwork.serializationFrequency;
			}
			set
			{
				PhotonNetwork.serializationFrequency = 1000 / value;
				if (PhotonHandler.Instance != null)
				{
					PhotonHandler.Instance.UpdateIntervalOnSerialize = PhotonNetwork.serializationFrequency;
				}
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002C0C File Offset: 0x00000E0C
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002C13 File Offset: 0x00000E13
		public static bool IsMessageQueueRunning
		{
			get
			{
				return PhotonNetwork.isMessageQueueRunning;
			}
			set
			{
				PhotonNetwork.isMessageQueueRunning = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002C1B File Offset: 0x00000E1B
		public static double Time
		{
			get
			{
				if (UnityEngine.Time.frameCount == PhotonNetwork.frame)
				{
					return PhotonNetwork.frametime;
				}
				PhotonNetwork.frametime = PhotonNetwork.ServerTimestamp / 1000.0;
				PhotonNetwork.frame = UnityEngine.Time.frameCount;
				return PhotonNetwork.frametime;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002C54 File Offset: 0x00000E54
		public static int ServerTimestamp
		{
			get
			{
				if (!PhotonNetwork.OfflineMode)
				{
					return PhotonNetwork.NetworkingClient.LoadBalancingPeer.ServerTimeInMilliSeconds;
				}
				if (PhotonNetwork.StartupStopwatch != null && PhotonNetwork.StartupStopwatch.IsRunning)
				{
					return (int)PhotonNetwork.StartupStopwatch.ElapsedMilliseconds;
				}
				return Environment.TickCount;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002CB7 File Offset: 0x00000EB7
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002C91 File Offset: 0x00000E91
		public static float KeepAliveInBackground
		{
			get
			{
				if (!(PhotonHandler.Instance != null))
				{
					return 60f;
				}
				return Mathf.Round((float)PhotonHandler.Instance.KeepAliveInBackground / 1000f);
			}
			set
			{
				if (PhotonHandler.Instance != null)
				{
					PhotonHandler.Instance.KeepAliveInBackground = (int)Mathf.Round(value * 1000f);
				}
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002CE2 File Offset: 0x00000EE2
		public static bool IsMasterClient
		{
			get
			{
				return PhotonNetwork.OfflineMode || (PhotonNetwork.NetworkingClient.CurrentRoom != null && PhotonNetwork.NetworkingClient.CurrentRoom.MasterClientId == PhotonNetwork.LocalPlayer.ActorNumber);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002D18 File Offset: 0x00000F18
		public static Player MasterClient
		{
			get
			{
				if (PhotonNetwork.OfflineMode)
				{
					return PhotonNetwork.LocalPlayer;
				}
				if (PhotonNetwork.NetworkingClient == null || PhotonNetwork.NetworkingClient.CurrentRoom == null)
				{
					return null;
				}
				return PhotonNetwork.NetworkingClient.CurrentRoom.GetPlayer(PhotonNetwork.NetworkingClient.CurrentRoom.MasterClientId, false);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002D66 File Offset: 0x00000F66
		public static bool InRoom
		{
			get
			{
				return PhotonNetwork.NetworkClientState == ClientState.Joined;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002D71 File Offset: 0x00000F71
		public static int CountOfPlayersOnMaster
		{
			get
			{
				return PhotonNetwork.NetworkingClient.PlayersOnMasterCount;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002D7D File Offset: 0x00000F7D
		public static int CountOfPlayersInRooms
		{
			get
			{
				return PhotonNetwork.NetworkingClient.PlayersInRoomsCount;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002D89 File Offset: 0x00000F89
		public static int CountOfPlayers
		{
			get
			{
				return PhotonNetwork.NetworkingClient.PlayersInRoomsCount + PhotonNetwork.NetworkingClient.PlayersOnMasterCount;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002DA0 File Offset: 0x00000FA0
		public static int CountOfRooms
		{
			get
			{
				return PhotonNetwork.NetworkingClient.RoomsCount;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002DAC File Offset: 0x00000FAC
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002DBD File Offset: 0x00000FBD
		public static bool NetworkStatisticsEnabled
		{
			get
			{
				return PhotonNetwork.NetworkingClient.LoadBalancingPeer.TrafficStatsEnabled;
			}
			set
			{
				PhotonNetwork.NetworkingClient.LoadBalancingPeer.TrafficStatsEnabled = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002DCF File Offset: 0x00000FCF
		public static int ResentReliableCommands
		{
			get
			{
				return PhotonNetwork.NetworkingClient.LoadBalancingPeer.ResentReliableCommands;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002DE0 File Offset: 0x00000FE0
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002DF4 File Offset: 0x00000FF4
		public static bool CrcCheckEnabled
		{
			get
			{
				return PhotonNetwork.NetworkingClient.LoadBalancingPeer.CrcEnabled;
			}
			set
			{
				if (!PhotonNetwork.IsConnected)
				{
					PhotonNetwork.NetworkingClient.LoadBalancingPeer.CrcEnabled = value;
					return;
				}
				UnityEngine.Debug.Log("Can't change CrcCheckEnabled while being connected. CrcCheckEnabled stays " + PhotonNetwork.NetworkingClient.LoadBalancingPeer.CrcEnabled.ToString());
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002E3F File Offset: 0x0000103F
		public static int PacketLossByCrcCheck
		{
			get
			{
				return PhotonNetwork.NetworkingClient.LoadBalancingPeer.PacketLossByCrc;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002E50 File Offset: 0x00001050
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002E61 File Offset: 0x00001061
		public static int MaxResendsBeforeDisconnect
		{
			get
			{
				return PhotonNetwork.NetworkingClient.LoadBalancingPeer.SentCountAllowance;
			}
			set
			{
				if (value < 3)
				{
					value = 3;
				}
				if (value > 10)
				{
					value = 10;
				}
				PhotonNetwork.NetworkingClient.LoadBalancingPeer.SentCountAllowance = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002E83 File Offset: 0x00001083
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002E94 File Offset: 0x00001094
		public static int QuickResends
		{
			get
			{
				return (int)PhotonNetwork.NetworkingClient.LoadBalancingPeer.QuickResendAttempts;
			}
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				if (value > 3)
				{
					value = 3;
				}
				PhotonNetwork.NetworkingClient.LoadBalancingPeer.QuickResendAttempts = (byte)value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002EB5 File Offset: 0x000010B5
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002EBC File Offset: 0x000010BC
		[Obsolete("Set port overrides in ServerPortOverrides. Not used anymore!")]
		public static bool UseAlternativeUdpPorts
		{
			[CompilerGenerated]
			get
			{
				return PhotonNetwork.<UseAlternativeUdpPorts>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				PhotonNetwork.<UseAlternativeUdpPorts>k__BackingField = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002EC4 File Offset: 0x000010C4
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002EEC File Offset: 0x000010EC
		public static PhotonPortDefinition ServerPortOverrides
		{
			get
			{
				if (PhotonNetwork.NetworkingClient != null)
				{
					return PhotonNetwork.NetworkingClient.ServerPortOverrides;
				}
				return default(PhotonPortDefinition);
			}
			set
			{
				if (PhotonNetwork.NetworkingClient != null)
				{
					PhotonNetwork.NetworkingClient.ServerPortOverrides = value;
				}
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002F00 File Offset: 0x00001100
		static PhotonNetwork()
		{
			PhotonNetwork.StaticReset();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003158 File Offset: 0x00001358
		private static void StaticReset()
		{
			PhotonNetwork.monoRPCMethodsCache.Clear();
			PhotonNetwork.OfflineMode = false;
			PhotonNetwork.NetworkingClient = new LoadBalancingClient(PhotonNetwork.PhotonServerSettings.AppSettings.Protocol);
			PhotonNetwork.NetworkingClient.LoadBalancingPeer.QuickResendAttempts = 2;
			PhotonNetwork.NetworkingClient.LoadBalancingPeer.SentCountAllowance = 9;
			PhotonNetwork.NetworkingClient.EventReceived -= PhotonNetwork.OnEvent;
			PhotonNetwork.NetworkingClient.EventReceived += PhotonNetwork.OnEvent;
			PhotonNetwork.NetworkingClient.OpResponseReceived -= PhotonNetwork.OnOperation;
			PhotonNetwork.NetworkingClient.OpResponseReceived += PhotonNetwork.OnOperation;
			PhotonNetwork.NetworkingClient.StateChanged -= PhotonNetwork.OnClientStateChanged;
			PhotonNetwork.NetworkingClient.StateChanged += PhotonNetwork.OnClientStateChanged;
			PhotonNetwork.StartupStopwatch = new Stopwatch();
			PhotonNetwork.StartupStopwatch.Start();
			PhotonHandler.Instance.Client = PhotonNetwork.NetworkingClient;
			Application.runInBackground = PhotonNetwork.PhotonServerSettings.RunInBackground;
			PhotonNetwork.PrefabPool = new DefaultPool();
			PhotonNetwork.rpcShortcuts = new Dictionary<string, int>(PhotonNetwork.PhotonServerSettings.RpcList.Count);
			for (int i = 0; i < PhotonNetwork.PhotonServerSettings.RpcList.Count; i++)
			{
				string key = PhotonNetwork.PhotonServerSettings.RpcList[i];
				PhotonNetwork.rpcShortcuts[key] = i;
			}
			CustomTypes.Register();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000032C4 File Offset: 0x000014C4
		public static bool ConnectUsingSettings()
		{
			if (PhotonNetwork.PhotonServerSettings == null)
			{
				UnityEngine.Debug.LogError("Can't connect: Loading settings failed. ServerSettings asset must be in any 'Resources' folder as: PhotonServerSettings");
				return false;
			}
			return PhotonNetwork.ConnectUsingSettings(PhotonNetwork.PhotonServerSettings.AppSettings, PhotonNetwork.PhotonServerSettings.StartInOfflineMode);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000032F8 File Offset: 0x000014F8
		public static bool ConnectUsingSettings(AppSettings appSettings, bool startInOfflineMode = false)
		{
			if (PhotonNetwork.NetworkingClient.LoadBalancingPeer.PeerState != PeerStateValue.Disconnected)
			{
				UnityEngine.Debug.LogWarning("ConnectUsingSettings() failed. Can only connect while in state 'Disconnected'. Current state: " + PhotonNetwork.NetworkingClient.LoadBalancingPeer.PeerState.ToString());
				return false;
			}
			if (ConnectionHandler.AppQuits)
			{
				UnityEngine.Debug.LogWarning("Can't connect: Application is closing. Unity called OnApplicationQuit().");
				return false;
			}
			if (PhotonNetwork.PhotonServerSettings == null)
			{
				UnityEngine.Debug.LogError("Can't connect: Loading settings failed. ServerSettings asset must be in any 'Resources' folder as: PhotonServerSettings");
				return false;
			}
			PhotonNetwork.SetupLogging();
			PhotonNetwork.NetworkingClient.LoadBalancingPeer.TransportProtocol = appSettings.Protocol;
			PhotonNetwork.NetworkingClient.ExpectedProtocol = null;
			PhotonNetwork.NetworkingClient.EnableProtocolFallback = appSettings.EnableProtocolFallback;
			PhotonNetwork.NetworkingClient.AuthMode = appSettings.AuthMode;
			PhotonNetwork.IsMessageQueueRunning = true;
			PhotonNetwork.NetworkingClient.AppId = appSettings.AppIdRealtime;
			PhotonNetwork.GameVersion = appSettings.AppVersion;
			if (startInOfflineMode)
			{
				PhotonNetwork.OfflineMode = true;
				return true;
			}
			if (PhotonNetwork.OfflineMode)
			{
				PhotonNetwork.OfflineMode = false;
				UnityEngine.Debug.LogWarning("ConnectUsingSettings() disabled the offline mode. No longer offline.");
			}
			PhotonNetwork.NetworkingClient.EnableLobbyStatistics = appSettings.EnableLobbyStatistics;
			PhotonNetwork.NetworkingClient.ProxyServerAddress = appSettings.ProxyServer;
			if (appSettings.IsMasterServerAddress)
			{
				if (PhotonNetwork.AuthValues == null)
				{
					PhotonNetwork.AuthValues = new AuthenticationValues(Guid.NewGuid().ToString());
				}
				else if (string.IsNullOrEmpty(PhotonNetwork.AuthValues.UserId))
				{
					PhotonNetwork.AuthValues.UserId = Guid.NewGuid().ToString();
				}
				return PhotonNetwork.ConnectToMaster(appSettings.Server, appSettings.Port, appSettings.AppIdRealtime);
			}
			PhotonNetwork.NetworkingClient.NameServerPortInAppSettings = appSettings.Port;
			if (!appSettings.IsDefaultNameServer)
			{
				PhotonNetwork.NetworkingClient.NameServerHost = appSettings.Server;
			}
			if (appSettings.IsBestRegion)
			{
				return PhotonNetwork.ConnectToBestCloudServer();
			}
			return PhotonNetwork.ConnectToRegion(appSettings.FixedRegion);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000034D8 File Offset: 0x000016D8
		public static bool ConnectToMaster(string masterServerAddress, int port, string appID)
		{
			if (PhotonNetwork.NetworkingClient.LoadBalancingPeer.PeerState != PeerStateValue.Disconnected)
			{
				UnityEngine.Debug.LogWarning("ConnectToMaster() failed. Can only connect while in state 'Disconnected'. Current state: " + PhotonNetwork.NetworkingClient.LoadBalancingPeer.PeerState.ToString());
				return false;
			}
			if (ConnectionHandler.AppQuits)
			{
				UnityEngine.Debug.LogWarning("Can't connect: Application is closing. Unity called OnApplicationQuit().");
				return false;
			}
			if (PhotonNetwork.OfflineMode)
			{
				PhotonNetwork.OfflineMode = false;
				UnityEngine.Debug.LogWarning("ConnectToMaster() disabled the offline mode. No longer offline.");
			}
			if (!PhotonNetwork.IsMessageQueueRunning)
			{
				PhotonNetwork.IsMessageQueueRunning = true;
				UnityEngine.Debug.LogWarning("ConnectToMaster() enabled IsMessageQueueRunning. Needs to be able to dispatch incoming messages.");
			}
			PhotonNetwork.SetupLogging();
			PhotonNetwork.ConnectMethod = ConnectMethod.ConnectToMaster;
			PhotonNetwork.NetworkingClient.IsUsingNameServer = false;
			PhotonNetwork.NetworkingClient.MasterServerAddress = ((port == 0) ? masterServerAddress : (masterServerAddress + ":" + port.ToString()));
			PhotonNetwork.NetworkingClient.AppId = appID;
			return PhotonNetwork.NetworkingClient.ConnectToMasterServer();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000035B4 File Offset: 0x000017B4
		public static bool ConnectToBestCloudServer()
		{
			if (PhotonNetwork.NetworkingClient.LoadBalancingPeer.PeerState != PeerStateValue.Disconnected)
			{
				UnityEngine.Debug.LogWarning("ConnectToBestCloudServer() failed. Can only connect while in state 'Disconnected'. Current state: " + PhotonNetwork.NetworkingClient.LoadBalancingPeer.PeerState.ToString());
				return false;
			}
			if (ConnectionHandler.AppQuits)
			{
				UnityEngine.Debug.LogWarning("Can't connect: Application is closing. Unity called OnApplicationQuit().");
				return false;
			}
			PhotonNetwork.SetupLogging();
			PhotonNetwork.ConnectMethod = ConnectMethod.ConnectToBest;
			return PhotonNetwork.NetworkingClient.ConnectToNameServer();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003628 File Offset: 0x00001828
		public static bool ConnectToRegion(string region)
		{
			if (PhotonNetwork.NetworkingClient.LoadBalancingPeer.PeerState != PeerStateValue.Disconnected && PhotonNetwork.NetworkingClient.Server != ServerConnection.NameServer)
			{
				UnityEngine.Debug.LogWarning("ConnectToRegion() failed. Can only connect while in state 'Disconnected'. Current state: " + PhotonNetwork.NetworkingClient.LoadBalancingPeer.PeerState.ToString());
				return false;
			}
			if (ConnectionHandler.AppQuits)
			{
				UnityEngine.Debug.LogWarning("Can't connect: Application is closing. Unity called OnApplicationQuit().");
				return false;
			}
			PhotonNetwork.SetupLogging();
			PhotonNetwork.ConnectMethod = ConnectMethod.ConnectToRegion;
			return !string.IsNullOrEmpty(region) && PhotonNetwork.NetworkingClient.ConnectToRegionMaster(region);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000036B4 File Offset: 0x000018B4
		public static void Disconnect()
		{
			if (PhotonNetwork.OfflineMode)
			{
				PhotonNetwork.OfflineMode = false;
				PhotonNetwork.offlineModeRoom = null;
				PhotonNetwork.NetworkingClient.State = ClientState.Disconnecting;
				PhotonNetwork.NetworkingClient.OnStatusChanged(StatusCode.Disconnect);
				return;
			}
			if (PhotonNetwork.NetworkingClient == null)
			{
				return;
			}
			PhotonNetwork.NetworkingClient.Disconnect();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003704 File Offset: 0x00001904
		public static bool Reconnect()
		{
			if (string.IsNullOrEmpty(PhotonNetwork.NetworkingClient.MasterServerAddress))
			{
				UnityEngine.Debug.LogWarning("Reconnect() failed. It seems the client wasn't connected before?! Current state: " + PhotonNetwork.NetworkingClient.LoadBalancingPeer.PeerState.ToString());
				return false;
			}
			if (PhotonNetwork.NetworkingClient.LoadBalancingPeer.PeerState != PeerStateValue.Disconnected)
			{
				UnityEngine.Debug.LogWarning("Reconnect() failed. Can only connect while in state 'Disconnected'. Current state: " + PhotonNetwork.NetworkingClient.LoadBalancingPeer.PeerState.ToString());
				return false;
			}
			if (PhotonNetwork.OfflineMode)
			{
				PhotonNetwork.OfflineMode = false;
				UnityEngine.Debug.LogWarning("Reconnect() disabled the offline mode. No longer offline.");
			}
			if (!PhotonNetwork.IsMessageQueueRunning)
			{
				PhotonNetwork.IsMessageQueueRunning = true;
				UnityEngine.Debug.LogWarning("Reconnect() enabled IsMessageQueueRunning. Needs to be able to dispatch incoming messages.");
			}
			PhotonNetwork.NetworkingClient.IsUsingNameServer = false;
			return PhotonNetwork.NetworkingClient.ReconnectToMaster();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000037D2 File Offset: 0x000019D2
		public static void NetworkStatisticsReset()
		{
			PhotonNetwork.NetworkingClient.LoadBalancingPeer.TrafficStatsReset();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000037E3 File Offset: 0x000019E3
		public static string NetworkStatisticsToString()
		{
			if (PhotonNetwork.NetworkingClient == null || PhotonNetwork.OfflineMode)
			{
				return "Offline or in OfflineMode. No VitalStats available.";
			}
			return PhotonNetwork.NetworkingClient.LoadBalancingPeer.VitalStatsToString(false);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003809 File Offset: 0x00001A09
		private static bool VerifyCanUseNetwork()
		{
			if (PhotonNetwork.IsConnected)
			{
				return true;
			}
			UnityEngine.Debug.LogError("Cannot send messages when not connected. Either connect to Photon OR use offline mode!");
			return false;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000381F File Offset: 0x00001A1F
		public static int GetPing()
		{
			return PhotonNetwork.NetworkingClient.LoadBalancingPeer.RoundTripTime;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003830 File Offset: 0x00001A30
		public static void FetchServerTimestamp()
		{
			if (PhotonNetwork.NetworkingClient != null)
			{
				PhotonNetwork.NetworkingClient.LoadBalancingPeer.FetchServerTimestamp();
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003848 File Offset: 0x00001A48
		public static void SendAllOutgoingCommands()
		{
			if (!PhotonNetwork.VerifyCanUseNetwork())
			{
				return;
			}
			while (PhotonNetwork.NetworkingClient.LoadBalancingPeer.SendOutgoingCommands())
			{
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003864 File Offset: 0x00001A64
		public static bool CloseConnection(Player kickPlayer)
		{
			if (!PhotonNetwork.VerifyCanUseNetwork())
			{
				return false;
			}
			if (!PhotonNetwork.EnableCloseConnection)
			{
				UnityEngine.Debug.LogError("CloseConnection is disabled. No need to call it.");
				return false;
			}
			if (!PhotonNetwork.LocalPlayer.IsMasterClient)
			{
				UnityEngine.Debug.LogError("CloseConnection: Only the masterclient can kick another player.");
				return false;
			}
			if (kickPlayer == null)
			{
				UnityEngine.Debug.LogError("CloseConnection: No such player connected!");
				return false;
			}
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions
			{
				TargetActors = new int[]
				{
					kickPlayer.ActorNumber
				}
			};
			return PhotonNetwork.NetworkingClient.OpRaiseEvent(203, null, raiseEventOptions, SendOptions.SendReliable);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000038E7 File Offset: 0x00001AE7
		public static bool SetMasterClient(Player masterClientPlayer)
		{
			if (!PhotonNetwork.InRoom || !PhotonNetwork.VerifyCanUseNetwork() || PhotonNetwork.OfflineMode)
			{
				if (PhotonNetwork.LogLevel == PunLogLevel.Informational)
				{
					UnityEngine.Debug.Log("Can not SetMasterClient(). Not in room or in OfflineMode.");
				}
				return false;
			}
			return PhotonNetwork.CurrentRoom.SetMasterClient(masterClientPlayer);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000391D File Offset: 0x00001B1D
		public static bool JoinRandomRoom()
		{
			return PhotonNetwork.JoinRandomRoom(null, 0, MatchmakingMode.FillRoom, null, null, null);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000392A File Offset: 0x00001B2A
		public static bool JoinRandomRoom(ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties, int expectedMaxPlayers)
		{
			return PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, expectedMaxPlayers, MatchmakingMode.FillRoom, null, null, null);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003938 File Offset: 0x00001B38
		public static bool JoinRandomRoom(ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties, int expectedMaxPlayers, MatchmakingMode matchingType, TypedLobby typedLobby, string sqlLobbyFilter, string[] expectedUsers = null)
		{
			if (PhotonNetwork.OfflineMode)
			{
				if (PhotonNetwork.offlineModeRoom != null)
				{
					UnityEngine.Debug.LogError("JoinRandomRoom failed. In offline mode you still have to leave a room to enter another.");
					return false;
				}
				PhotonNetwork.EnterOfflineRoom("offline room", null, true);
				return true;
			}
			else
			{
				if (PhotonNetwork.NetworkingClient.Server != ServerConnection.MasterServer || !PhotonNetwork.IsConnectedAndReady)
				{
					UnityEngine.Debug.LogError(string.Concat(new string[]
					{
						"JoinRandomRoom failed. Client is on ",
						PhotonNetwork.NetworkingClient.Server.ToString(),
						" (must be Master Server for matchmaking)",
						PhotonNetwork.IsConnectedAndReady ? " and ready" : (" but not ready for operations (State: " + PhotonNetwork.NetworkingClient.State.ToString() + ")"),
						". Wait for callback: OnJoinedLobby or OnConnectedToMaster."
					}));
					return false;
				}
				typedLobby = (typedLobby ?? (PhotonNetwork.NetworkingClient.InLobby ? PhotonNetwork.NetworkingClient.CurrentLobby : null));
				OpJoinRandomRoomParams opJoinRandomRoomParams = new OpJoinRandomRoomParams();
				opJoinRandomRoomParams.ExpectedCustomRoomProperties = expectedCustomRoomProperties;
				opJoinRandomRoomParams.ExpectedMaxPlayers = expectedMaxPlayers;
				opJoinRandomRoomParams.MatchingType = matchingType;
				opJoinRandomRoomParams.TypedLobby = typedLobby;
				opJoinRandomRoomParams.SqlLobbyFilter = sqlLobbyFilter;
				opJoinRandomRoomParams.ExpectedUsers = expectedUsers;
				return PhotonNetwork.NetworkingClient.OpJoinRandomRoom(opJoinRandomRoomParams);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003A5C File Offset: 0x00001C5C
		public static bool JoinRandomOrCreateRoom(ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = null, byte expectedMaxPlayers = 0, MatchmakingMode matchingType = MatchmakingMode.FillRoom, TypedLobby typedLobby = null, string sqlLobbyFilter = null, string roomName = null, RoomOptions roomOptions = null, string[] expectedUsers = null)
		{
			if (PhotonNetwork.OfflineMode)
			{
				if (PhotonNetwork.offlineModeRoom != null)
				{
					UnityEngine.Debug.LogError("JoinRandomOrCreateRoom failed. In offline mode you still have to leave a room to enter another.");
					return false;
				}
				PhotonNetwork.EnterOfflineRoom("offline room", null, true);
				return true;
			}
			else
			{
				if (PhotonNetwork.NetworkingClient.Server != ServerConnection.MasterServer || !PhotonNetwork.IsConnectedAndReady)
				{
					UnityEngine.Debug.LogError(string.Concat(new string[]
					{
						"JoinRandomOrCreateRoom failed. Client is on ",
						PhotonNetwork.NetworkingClient.Server.ToString(),
						" (must be Master Server for matchmaking)",
						PhotonNetwork.IsConnectedAndReady ? " and ready" : (" but not ready for operations (State: " + PhotonNetwork.NetworkingClient.State.ToString() + ")"),
						". Wait for callback: OnJoinedLobby or OnConnectedToMaster."
					}));
					return false;
				}
				typedLobby = (typedLobby ?? (PhotonNetwork.NetworkingClient.InLobby ? PhotonNetwork.NetworkingClient.CurrentLobby : null));
				OpJoinRandomRoomParams opJoinRandomRoomParams = new OpJoinRandomRoomParams();
				opJoinRandomRoomParams.ExpectedCustomRoomProperties = expectedCustomRoomProperties;
				opJoinRandomRoomParams.ExpectedMaxPlayers = (int)expectedMaxPlayers;
				opJoinRandomRoomParams.MatchingType = matchingType;
				opJoinRandomRoomParams.TypedLobby = typedLobby;
				opJoinRandomRoomParams.SqlLobbyFilter = sqlLobbyFilter;
				opJoinRandomRoomParams.ExpectedUsers = expectedUsers;
				EnterRoomParams enterRoomParams = new EnterRoomParams();
				enterRoomParams.RoomName = roomName;
				enterRoomParams.RoomOptions = roomOptions;
				enterRoomParams.Lobby = typedLobby;
				enterRoomParams.ExpectedUsers = expectedUsers;
				return PhotonNetwork.NetworkingClient.OpJoinRandomOrCreateRoom(opJoinRandomRoomParams, enterRoomParams);
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003BA4 File Offset: 0x00001DA4
		public static bool CreateRoom(string roomName, RoomOptions roomOptions = null, TypedLobby typedLobby = null, string[] expectedUsers = null)
		{
			if (PhotonNetwork.OfflineMode)
			{
				if (PhotonNetwork.offlineModeRoom != null)
				{
					UnityEngine.Debug.LogError("CreateRoom failed. In offline mode you still have to leave a room to enter another.");
					return false;
				}
				PhotonNetwork.EnterOfflineRoom(roomName, roomOptions, true);
				return true;
			}
			else
			{
				if (PhotonNetwork.NetworkingClient.Server != ServerConnection.MasterServer || !PhotonNetwork.IsConnectedAndReady)
				{
					UnityEngine.Debug.LogError(string.Concat(new string[]
					{
						"CreateRoom failed. Client is on ",
						PhotonNetwork.NetworkingClient.Server.ToString(),
						" (must be Master Server for matchmaking)",
						PhotonNetwork.IsConnectedAndReady ? " and ready" : ("but not ready for operations (State: " + PhotonNetwork.NetworkingClient.State.ToString() + ")"),
						". Wait for callback: OnJoinedLobby or OnConnectedToMaster."
					}));
					return false;
				}
				typedLobby = (typedLobby ?? (PhotonNetwork.NetworkingClient.InLobby ? PhotonNetwork.NetworkingClient.CurrentLobby : null));
				EnterRoomParams enterRoomParams = new EnterRoomParams();
				enterRoomParams.RoomName = roomName;
				enterRoomParams.RoomOptions = roomOptions;
				enterRoomParams.Lobby = typedLobby;
				enterRoomParams.ExpectedUsers = expectedUsers;
				return PhotonNetwork.NetworkingClient.OpCreateRoom(enterRoomParams);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003CB4 File Offset: 0x00001EB4
		public static bool JoinOrCreateRoom(string roomName, RoomOptions roomOptions, TypedLobby typedLobby, string[] expectedUsers = null)
		{
			if (PhotonNetwork.OfflineMode)
			{
				if (PhotonNetwork.offlineModeRoom != null)
				{
					UnityEngine.Debug.LogError("JoinOrCreateRoom failed. In offline mode you still have to leave a room to enter another.");
					return false;
				}
				PhotonNetwork.EnterOfflineRoom(roomName, roomOptions, true);
				return true;
			}
			else
			{
				if (PhotonNetwork.NetworkingClient.Server != ServerConnection.MasterServer || !PhotonNetwork.IsConnectedAndReady)
				{
					UnityEngine.Debug.LogError(string.Concat(new string[]
					{
						"JoinOrCreateRoom failed. Client is on ",
						PhotonNetwork.NetworkingClient.Server.ToString(),
						" (must be Master Server for matchmaking)",
						PhotonNetwork.IsConnectedAndReady ? " and ready" : ("but not ready for operations (State: " + PhotonNetwork.NetworkingClient.State.ToString() + ")"),
						". Wait for callback: OnJoinedLobby or OnConnectedToMaster."
					}));
					return false;
				}
				if (string.IsNullOrEmpty(roomName))
				{
					UnityEngine.Debug.LogError("JoinOrCreateRoom failed. A roomname is required. If you don't know one, how will you join?");
					return false;
				}
				typedLobby = (typedLobby ?? (PhotonNetwork.NetworkingClient.InLobby ? PhotonNetwork.NetworkingClient.CurrentLobby : null));
				EnterRoomParams enterRoomParams = new EnterRoomParams();
				enterRoomParams.RoomName = roomName;
				enterRoomParams.RoomOptions = roomOptions;
				enterRoomParams.Lobby = typedLobby;
				enterRoomParams.PlayerProperties = PhotonNetwork.LocalPlayer.CustomProperties;
				enterRoomParams.ExpectedUsers = expectedUsers;
				return PhotonNetwork.NetworkingClient.OpJoinOrCreateRoom(enterRoomParams);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003DE8 File Offset: 0x00001FE8
		public static bool JoinRoom(string roomName, string[] expectedUsers = null)
		{
			if (PhotonNetwork.OfflineMode)
			{
				if (PhotonNetwork.offlineModeRoom != null)
				{
					UnityEngine.Debug.LogError("JoinRoom failed. In offline mode you still have to leave a room to enter another.");
					return false;
				}
				PhotonNetwork.EnterOfflineRoom(roomName, null, true);
				return true;
			}
			else
			{
				if (PhotonNetwork.NetworkingClient.Server != ServerConnection.MasterServer || !PhotonNetwork.IsConnectedAndReady)
				{
					UnityEngine.Debug.LogError(string.Concat(new string[]
					{
						"JoinRoom failed. Client is on ",
						PhotonNetwork.NetworkingClient.Server.ToString(),
						" (must be Master Server for matchmaking)",
						PhotonNetwork.IsConnectedAndReady ? " and ready" : ("but not ready for operations (State: " + PhotonNetwork.NetworkingClient.State.ToString() + ")"),
						". Wait for callback: OnJoinedLobby or OnConnectedToMaster."
					}));
					return false;
				}
				if (string.IsNullOrEmpty(roomName))
				{
					UnityEngine.Debug.LogError("JoinRoom failed. A roomname is required. If you don't know one, how will you join?");
					return false;
				}
				EnterRoomParams enterRoomParams = new EnterRoomParams();
				enterRoomParams.RoomName = roomName;
				enterRoomParams.ExpectedUsers = expectedUsers;
				return PhotonNetwork.NetworkingClient.OpJoinRoom(enterRoomParams);
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003EDC File Offset: 0x000020DC
		public static bool RejoinRoom(string roomName)
		{
			if (PhotonNetwork.OfflineMode)
			{
				UnityEngine.Debug.LogError("RejoinRoom failed due to offline mode.");
				return false;
			}
			if (PhotonNetwork.NetworkingClient.Server != ServerConnection.MasterServer || !PhotonNetwork.IsConnectedAndReady)
			{
				UnityEngine.Debug.LogError(string.Concat(new string[]
				{
					"RejoinRoom failed. Client is on ",
					PhotonNetwork.NetworkingClient.Server.ToString(),
					" (must be Master Server for matchmaking)",
					PhotonNetwork.IsConnectedAndReady ? " and ready" : ("but not ready for operations (State: " + PhotonNetwork.NetworkingClient.State.ToString() + ")"),
					". Wait for callback: OnJoinedLobby or OnConnectedToMaster."
				}));
				return false;
			}
			if (string.IsNullOrEmpty(roomName))
			{
				UnityEngine.Debug.LogError("RejoinRoom failed. A roomname is required. If you don't know one, how will you join?");
				return false;
			}
			return PhotonNetwork.NetworkingClient.OpRejoinRoom(roomName, null);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003FAC File Offset: 0x000021AC
		public static bool ReconnectAndRejoin()
		{
			if (PhotonNetwork.NetworkingClient.LoadBalancingPeer.PeerState != PeerStateValue.Disconnected)
			{
				UnityEngine.Debug.LogWarning("ReconnectAndRejoin() failed. Can only connect while in state 'Disconnected'. Current state: " + PhotonNetwork.NetworkingClient.LoadBalancingPeer.PeerState.ToString());
				return false;
			}
			if (PhotonNetwork.OfflineMode)
			{
				PhotonNetwork.OfflineMode = false;
				UnityEngine.Debug.LogWarning("ReconnectAndRejoin() disabled the offline mode. No longer offline.");
			}
			if (!PhotonNetwork.IsMessageQueueRunning)
			{
				PhotonNetwork.IsMessageQueueRunning = true;
				UnityEngine.Debug.LogWarning("ReconnectAndRejoin() enabled IsMessageQueueRunning. Needs to be able to dispatch incoming messages.");
			}
			return PhotonNetwork.NetworkingClient.ReconnectAndRejoin();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004030 File Offset: 0x00002230
		public static bool LeaveRoom(bool becomeInactive = true)
		{
			if (PhotonNetwork.OfflineMode)
			{
				PhotonNetwork.offlineModeRoom = null;
				PhotonNetwork.NetworkingClient.MatchMakingCallbackTargets.OnLeftRoom();
				PhotonNetwork.NetworkingClient.ConnectionCallbackTargets.OnConnectedToMaster();
				return true;
			}
			if (PhotonNetwork.CurrentRoom == null)
			{
				UnityEngine.Debug.LogWarning("PhotonNetwork.CurrentRoom is null. You don't have to call LeaveRoom() when you're not in one. State: " + PhotonNetwork.NetworkClientState.ToString());
			}
			else
			{
				becomeInactive = (becomeInactive && PhotonNetwork.CurrentRoom.PlayerTtl != 0);
			}
			return PhotonNetwork.NetworkingClient.OpLeaveRoom(becomeInactive, false);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000040B8 File Offset: 0x000022B8
		private static void EnterOfflineRoom(string roomName, RoomOptions roomOptions, bool createdRoom)
		{
			PhotonNetwork.offlineModeRoom = new Room(roomName, roomOptions, true);
			PhotonNetwork.NetworkingClient.ChangeLocalID(1, true);
			PhotonNetwork.offlineModeRoom.masterClientId = 1;
			PhotonNetwork.offlineModeRoom.AddPlayer(PhotonNetwork.LocalPlayer);
			PhotonNetwork.offlineModeRoom.LoadBalancingClient = PhotonNetwork.NetworkingClient;
			PhotonNetwork.NetworkingClient.CurrentRoom = PhotonNetwork.offlineModeRoom;
			if (createdRoom)
			{
				PhotonNetwork.NetworkingClient.MatchMakingCallbackTargets.OnCreatedRoom();
			}
			PhotonNetwork.NetworkingClient.MatchMakingCallbackTargets.OnJoinedRoom();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004138 File Offset: 0x00002338
		public static bool JoinLobby()
		{
			return PhotonNetwork.JoinLobby(null);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004140 File Offset: 0x00002340
		public static bool JoinLobby(TypedLobby typedLobby)
		{
			return PhotonNetwork.IsConnected && PhotonNetwork.Server == ServerConnection.MasterServer && PhotonNetwork.NetworkingClient.OpJoinLobby(typedLobby);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000415D File Offset: 0x0000235D
		public static bool LeaveLobby()
		{
			return PhotonNetwork.IsConnected && PhotonNetwork.Server == ServerConnection.MasterServer && PhotonNetwork.NetworkingClient.OpLeaveLobby();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004179 File Offset: 0x00002379
		public static bool FindFriends(string[] friendsToFind)
		{
			return PhotonNetwork.NetworkingClient != null && !PhotonNetwork.offlineMode && PhotonNetwork.NetworkingClient.OpFindFriends(friendsToFind, null);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004197 File Offset: 0x00002397
		public static bool GetCustomRoomList(TypedLobby typedLobby, string sqlLobbyFilter)
		{
			return PhotonNetwork.NetworkingClient.OpGetGameList(typedLobby, sqlLobbyFilter);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000041A8 File Offset: 0x000023A8
		public static bool SetPlayerCustomProperties(ExitGames.Client.Photon.Hashtable customProperties)
		{
			if (customProperties == null)
			{
				customProperties = new ExitGames.Client.Photon.Hashtable();
				foreach (object obj in PhotonNetwork.LocalPlayer.CustomProperties.Keys)
				{
					customProperties[(string)obj] = null;
				}
			}
			return PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties, null, null);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004224 File Offset: 0x00002424
		public static void RemovePlayerCustomProperties(string[] customPropertiesToDelete)
		{
			if (customPropertiesToDelete == null || customPropertiesToDelete.Length == 0 || PhotonNetwork.LocalPlayer.CustomProperties == null)
			{
				PhotonNetwork.LocalPlayer.CustomProperties = new ExitGames.Client.Photon.Hashtable();
				return;
			}
			foreach (string key in customPropertiesToDelete)
			{
				if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey(key))
				{
					PhotonNetwork.LocalPlayer.CustomProperties.Remove(key);
				}
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000428C File Offset: 0x0000248C
		public static bool RaiseEvent(byte eventCode, object eventContent, RaiseEventOptions raiseEventOptions, SendOptions sendOptions)
		{
			if (PhotonNetwork.offlineMode)
			{
				if (raiseEventOptions.Receivers == ReceiverGroup.Others)
				{
					return true;
				}
				EventData eventData = new EventData
				{
					Code = eventCode
				};
				eventData.Parameters[245] = eventContent;
				eventData.Parameters[254] = 1;
				PhotonNetwork.NetworkingClient.OnEvent(eventData);
				return true;
			}
			else
			{
				if (!PhotonNetwork.InRoom || eventCode >= 200)
				{
					UnityEngine.Debug.LogWarning("RaiseEvent(" + eventCode.ToString() + ") failed. Your event is not being sent! Check if your are in a Room and the eventCode must be less than 200 (0..199).");
					return false;
				}
				return PhotonNetwork.NetworkingClient.OpRaiseEvent(eventCode, eventContent, raiseEventOptions, sendOptions);
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004325 File Offset: 0x00002525
		private static bool RaiseEventInternal(byte eventCode, object eventContent, RaiseEventOptions raiseEventOptions, SendOptions sendOptions)
		{
			if (PhotonNetwork.offlineMode)
			{
				return false;
			}
			if (!PhotonNetwork.InRoom)
			{
				UnityEngine.Debug.LogWarning("RaiseEvent(" + eventCode.ToString() + ") failed. Your event is not being sent! Check if your are in a Room");
				return false;
			}
			return PhotonNetwork.NetworkingClient.OpRaiseEvent(eventCode, eventContent, raiseEventOptions, sendOptions);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004364 File Offset: 0x00002564
		public static bool AllocateViewID(PhotonView view)
		{
			if (view.ViewID != 0)
			{
				UnityEngine.Debug.LogError("AllocateViewID() can't be used for PhotonViews that already have a viewID. This view is: " + view.ToString());
				return false;
			}
			int viewID = PhotonNetwork.AllocateViewID(PhotonNetwork.LocalPlayer.ActorNumber);
			view.ViewID = viewID;
			return true;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000043A8 File Offset: 0x000025A8
		[Obsolete("Renamed. Use AllocateRoomViewID instead")]
		public static bool AllocateSceneViewID(PhotonView view)
		{
			return PhotonNetwork.AllocateRoomViewID(view);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000043B0 File Offset: 0x000025B0
		public static bool AllocateRoomViewID(PhotonView view)
		{
			if (!PhotonNetwork.IsMasterClient)
			{
				UnityEngine.Debug.LogError("Only the Master Client can AllocateRoomViewID(). Check PhotonNetwork.IsMasterClient!");
				return false;
			}
			if (view.ViewID != 0)
			{
				UnityEngine.Debug.LogError("AllocateRoomViewID() can't be used for PhotonViews that already have a viewID. This view is: " + view.ToString());
				return false;
			}
			int viewID = PhotonNetwork.AllocateViewID(0);
			view.ViewID = viewID;
			return true;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000043FE File Offset: 0x000025FE
		public static int AllocateViewID(bool roomObject)
		{
			if (roomObject && !PhotonNetwork.LocalPlayer.IsMasterClient)
			{
				UnityEngine.Debug.LogError("Only a Master Client can AllocateViewID() for room objects. This client/player is not a Master Client. Returning an invalid viewID: -1.");
				return 0;
			}
			return PhotonNetwork.AllocateViewID(roomObject ? 0 : PhotonNetwork.LocalPlayer.ActorNumber);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004430 File Offset: 0x00002630
		public static int AllocateViewID(int ownerId)
		{
			if (ownerId == 0)
			{
				int num = PhotonNetwork.lastUsedViewSubIdStatic;
				int num2 = ownerId * PhotonNetwork.MAX_VIEW_IDS;
				for (int i = 1; i < PhotonNetwork.MAX_VIEW_IDS; i++)
				{
					num = (num + 1) % PhotonNetwork.MAX_VIEW_IDS;
					if (num != 0)
					{
						int num3 = num + num2;
						if (!PhotonNetwork.photonViewList.ContainsKey(num3))
						{
							PhotonNetwork.lastUsedViewSubIdStatic = num;
							return num3;
						}
					}
				}
				throw new Exception(string.Format("AllocateViewID() failed. The room (user {0}) is out of 'room' viewIDs. It seems all available are in use.", ownerId));
			}
			int num4 = PhotonNetwork.lastUsedViewSubId;
			int num5 = ownerId * PhotonNetwork.MAX_VIEW_IDS;
			for (int j = 1; j <= PhotonNetwork.MAX_VIEW_IDS; j++)
			{
				num4 = (num4 + 1) % PhotonNetwork.MAX_VIEW_IDS;
				if (num4 != 0)
				{
					int num6 = num4 + num5;
					if (!PhotonNetwork.photonViewList.ContainsKey(num6))
					{
						PhotonNetwork.lastUsedViewSubId = num4;
						return num6;
					}
				}
			}
			throw new Exception(string.Format("AllocateViewID() failed. User {0} is out of viewIDs. It seems all available are in use.", ownerId));
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004504 File Offset: 0x00002704
		public static GameObject Instantiate(string prefabName, Vector3 position, Quaternion rotation, byte group = 0, object[] data = null)
		{
			if (PhotonNetwork.CurrentRoom == null)
			{
				UnityEngine.Debug.LogError("Can not Instantiate before the client joined/created a room. State: " + PhotonNetwork.NetworkClientState.ToString());
				return null;
			}
			return PhotonNetwork.NetworkInstantiate(new InstantiateParameters(prefabName, position, rotation, group, data, PhotonNetwork.currentLevelPrefix, null, PhotonNetwork.LocalPlayer, PhotonNetwork.ServerTimestamp), false, false);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000455E File Offset: 0x0000275E
		[Obsolete("Renamed. Use InstantiateRoomObject instead")]
		public static GameObject InstantiateSceneObject(string prefabName, Vector3 position, Quaternion rotation, byte group = 0, object[] data = null)
		{
			return PhotonNetwork.InstantiateRoomObject(prefabName, position, rotation, group, data);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000456C File Offset: 0x0000276C
		public static GameObject InstantiateRoomObject(string prefabName, Vector3 position, Quaternion rotation, byte group = 0, object[] data = null)
		{
			if (PhotonNetwork.CurrentRoom == null)
			{
				UnityEngine.Debug.LogError("Can not Instantiate before the client joined/created a room.");
				return null;
			}
			if (PhotonNetwork.LocalPlayer.IsMasterClient)
			{
				return PhotonNetwork.NetworkInstantiate(new InstantiateParameters(prefabName, position, rotation, group, data, PhotonNetwork.currentLevelPrefix, null, PhotonNetwork.LocalPlayer, PhotonNetwork.ServerTimestamp), true, false);
			}
			return null;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000045BC File Offset: 0x000027BC
		private static GameObject NetworkInstantiate(ExitGames.Client.Photon.Hashtable networkEvent, Player creator)
		{
			string prefabName = (string)networkEvent[PhotonNetwork.keyByteZero];
			int timestamp = (int)networkEvent[PhotonNetwork.keyByteSix];
			int num = (int)networkEvent[PhotonNetwork.keyByteSeven];
			Vector3 position;
			if (networkEvent.ContainsKey(PhotonNetwork.keyByteOne))
			{
				position = (Vector3)networkEvent[PhotonNetwork.keyByteOne];
			}
			else
			{
				position = Vector3.zero;
			}
			Quaternion rotation = Quaternion.identity;
			if (networkEvent.ContainsKey(PhotonNetwork.keyByteTwo))
			{
				rotation = (Quaternion)networkEvent[PhotonNetwork.keyByteTwo];
			}
			byte b = 0;
			if (networkEvent.ContainsKey(PhotonNetwork.keyByteThree))
			{
				b = (byte)networkEvent[PhotonNetwork.keyByteThree];
			}
			byte objLevelPrefix = 0;
			if (networkEvent.ContainsKey(PhotonNetwork.keyByteEight))
			{
				objLevelPrefix = (byte)networkEvent[PhotonNetwork.keyByteEight];
			}
			int[] viewIDs;
			if (networkEvent.ContainsKey(PhotonNetwork.keyByteFour))
			{
				viewIDs = (int[])networkEvent[PhotonNetwork.keyByteFour];
			}
			else
			{
				viewIDs = new int[]
				{
					num
				};
			}
			object[] data;
			if (networkEvent.ContainsKey(PhotonNetwork.keyByteFive))
			{
				data = (object[])networkEvent[PhotonNetwork.keyByteFive];
			}
			else
			{
				data = null;
			}
			if (b != 0 && !PhotonNetwork.allowedReceivingGroups.Contains(b))
			{
				return null;
			}
			return PhotonNetwork.NetworkInstantiate(new InstantiateParameters(prefabName, position, rotation, b, data, objLevelPrefix, viewIDs, creator, timestamp), false, true);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000470C File Offset: 0x0000290C
		private static GameObject NetworkInstantiate(InstantiateParameters parameters, bool roomObject = false, bool instantiateEvent = false)
		{
			GameObject gameObject = PhotonNetwork.prefabPool.Instantiate(parameters.prefabName, parameters.position, parameters.rotation);
			if (gameObject == null)
			{
				UnityEngine.Debug.LogError("Failed to network-Instantiate: " + parameters.prefabName);
				return null;
			}
			if (gameObject.activeSelf)
			{
				UnityEngine.Debug.LogWarning("PrefabPool.Instantiate() should return an inactive GameObject. " + PhotonNetwork.prefabPool.GetType().Name + " returned an active object. PrefabId: " + parameters.prefabName);
			}
			PhotonView[] photonViewsInChildren = gameObject.GetPhotonViewsInChildren();
			if (photonViewsInChildren.Length == 0)
			{
				UnityEngine.Debug.LogError("PhotonNetwork.Instantiate() can only instantiate objects with a PhotonView component. This prefab does not have one: " + parameters.prefabName);
				return null;
			}
			bool flag = !instantiateEvent && PhotonNetwork.LocalPlayer.Equals(parameters.creator);
			if (flag)
			{
				parameters.viewIDs = new int[photonViewsInChildren.Length];
			}
			for (int i = 0; i < photonViewsInChildren.Length; i++)
			{
				if (flag)
				{
					parameters.viewIDs[i] = (roomObject ? PhotonNetwork.AllocateViewID(0) : PhotonNetwork.AllocateViewID(parameters.creator.ActorNumber));
				}
				PhotonView photonView = photonViewsInChildren[i];
				photonView.ViewID = 0;
				photonView.sceneViewId = 0;
				photonView.isRuntimeInstantiated = true;
				photonView.lastOnSerializeDataSent = null;
				photonView.lastOnSerializeDataReceived = null;
				photonView.Prefix = (int)parameters.objLevelPrefix;
				photonView.InstantiationId = parameters.viewIDs[0];
				photonView.InstantiationData = parameters.data;
				photonView.ViewID = parameters.viewIDs[i];
				photonView.Group = parameters.group;
			}
			if (flag)
			{
				PhotonNetwork.SendInstantiate(parameters, roomObject);
			}
			gameObject.SetActive(true);
			if (!PhotonNetwork.PrefabsWithoutMagicCallback.Contains(parameters.prefabName))
			{
				IPunInstantiateMagicCallback[] components = gameObject.GetComponents<IPunInstantiateMagicCallback>();
				if (components.Length != 0)
				{
					PhotonMessageInfo info = new PhotonMessageInfo(parameters.creator, parameters.timestamp, photonViewsInChildren[0]);
					IPunInstantiateMagicCallback[] array = components;
					for (int j = 0; j < array.Length; j++)
					{
						array[j].OnPhotonInstantiate(info);
					}
				}
				else
				{
					PhotonNetwork.PrefabsWithoutMagicCallback.Add(parameters.prefabName);
				}
			}
			return gameObject;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000048F4 File Offset: 0x00002AF4
		internal static bool SendInstantiate(InstantiateParameters parameters, bool roomObject = false)
		{
			int num = parameters.viewIDs[0];
			PhotonNetwork.SendInstantiateEvHashtable.Clear();
			PhotonNetwork.SendInstantiateEvHashtable[PhotonNetwork.keyByteZero] = parameters.prefabName;
			if (parameters.position != Vector3.zero)
			{
				PhotonNetwork.SendInstantiateEvHashtable[PhotonNetwork.keyByteOne] = parameters.position;
			}
			if (parameters.rotation != Quaternion.identity)
			{
				PhotonNetwork.SendInstantiateEvHashtable[PhotonNetwork.keyByteTwo] = parameters.rotation;
			}
			if (parameters.group != 0)
			{
				PhotonNetwork.SendInstantiateEvHashtable[PhotonNetwork.keyByteThree] = parameters.group;
			}
			if (parameters.viewIDs.Length > 1)
			{
				PhotonNetwork.SendInstantiateEvHashtable[PhotonNetwork.keyByteFour] = parameters.viewIDs;
			}
			if (parameters.data != null)
			{
				PhotonNetwork.SendInstantiateEvHashtable[PhotonNetwork.keyByteFive] = parameters.data;
			}
			if (PhotonNetwork.currentLevelPrefix > 0)
			{
				PhotonNetwork.SendInstantiateEvHashtable[PhotonNetwork.keyByteEight] = PhotonNetwork.currentLevelPrefix;
			}
			PhotonNetwork.SendInstantiateEvHashtable[PhotonNetwork.keyByteSix] = PhotonNetwork.ServerTimestamp;
			PhotonNetwork.SendInstantiateEvHashtable[PhotonNetwork.keyByteSeven] = num;
			PhotonNetwork.SendInstantiateRaiseEventOptions.CachingOption = (roomObject ? EventCaching.AddToRoomCacheGlobal : EventCaching.AddToRoomCache);
			return PhotonNetwork.RaiseEventInternal(202, PhotonNetwork.SendInstantiateEvHashtable, PhotonNetwork.SendInstantiateRaiseEventOptions, SendOptions.SendReliable);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004A59 File Offset: 0x00002C59
		public static void Destroy(PhotonView targetView)
		{
			if (targetView != null)
			{
				PhotonNetwork.RemoveInstantiatedGO(targetView.gameObject, !PhotonNetwork.InRoom);
				return;
			}
			UnityEngine.Debug.LogError("Destroy(targetPhotonView) failed, cause targetPhotonView is null.");
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004A82 File Offset: 0x00002C82
		public static void Destroy(GameObject targetGo)
		{
			PhotonNetwork.RemoveInstantiatedGO(targetGo, !PhotonNetwork.InRoom);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004A92 File Offset: 0x00002C92
		public static void DestroyPlayerObjects(Player targetPlayer)
		{
			if (targetPlayer == null)
			{
				UnityEngine.Debug.LogError("DestroyPlayerObjects() failed, cause parameter 'targetPlayer' was null.");
			}
			PhotonNetwork.DestroyPlayerObjects(targetPlayer.ActorNumber);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004AAC File Offset: 0x00002CAC
		public static void DestroyPlayerObjects(int targetPlayerId)
		{
			if (!PhotonNetwork.VerifyCanUseNetwork())
			{
				return;
			}
			if (PhotonNetwork.LocalPlayer.IsMasterClient || targetPlayerId == PhotonNetwork.LocalPlayer.ActorNumber)
			{
				PhotonNetwork.DestroyPlayerObjects(targetPlayerId, false);
				return;
			}
			UnityEngine.Debug.LogError("DestroyPlayerObjects() failed, cause players can only destroy their own GameObjects. A Master Client can destroy anyone's. This is master: " + PhotonNetwork.IsMasterClient.ToString());
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004AFE File Offset: 0x00002CFE
		public static void DestroyAll()
		{
			if (PhotonNetwork.IsMasterClient)
			{
				PhotonNetwork.DestroyAll(false);
				return;
			}
			UnityEngine.Debug.LogError("Couldn't call DestroyAll() as only the master client is allowed to call this.");
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004B18 File Offset: 0x00002D18
		public static void RemoveRPCs(Player targetPlayer)
		{
			if (!PhotonNetwork.VerifyCanUseNetwork())
			{
				return;
			}
			if (!targetPlayer.IsLocal && !PhotonNetwork.IsMasterClient)
			{
				UnityEngine.Debug.LogError("Error; Only the MasterClient can call RemoveRPCs for other players.");
				return;
			}
			PhotonNetwork.OpCleanActorRpcBuffer(targetPlayer.ActorNumber);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004B47 File Offset: 0x00002D47
		public static void RemoveRPCs(PhotonView targetPhotonView)
		{
			if (!PhotonNetwork.VerifyCanUseNetwork())
			{
				return;
			}
			PhotonNetwork.CleanRpcBufferIfMine(targetPhotonView);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004B58 File Offset: 0x00002D58
		internal static void RPC(PhotonView view, string methodName, RpcTarget target, bool encrypt, params object[] parameters)
		{
			if (string.IsNullOrEmpty(methodName))
			{
				UnityEngine.Debug.LogError("RPC method name cannot be null or empty.");
				return;
			}
			if (!PhotonNetwork.VerifyCanUseNetwork())
			{
				return;
			}
			if (PhotonNetwork.CurrentRoom == null)
			{
				UnityEngine.Debug.LogWarning("RPCs can only be sent in rooms. Call of \"" + methodName + "\" gets executed locally only, if at all.");
				return;
			}
			if (PhotonNetwork.NetworkingClient != null)
			{
				PhotonNetwork.RPC(view, methodName, target, null, encrypt, parameters);
				return;
			}
			UnityEngine.Debug.LogWarning("Could not execute RPC " + methodName + ". Possible scene loading in progress?");
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004BC8 File Offset: 0x00002DC8
		internal static void RPC(PhotonView view, string methodName, Player targetPlayer, bool encrypt, params object[] parameters)
		{
			if (!PhotonNetwork.VerifyCanUseNetwork())
			{
				return;
			}
			if (PhotonNetwork.CurrentRoom == null)
			{
				UnityEngine.Debug.LogWarning("RPCs can only be sent in rooms. Call of \"" + methodName + "\" gets executed locally only, if at all.");
				return;
			}
			if (PhotonNetwork.LocalPlayer == null)
			{
				UnityEngine.Debug.LogError("RPC can't be sent to target Player being null! Did not send \"" + methodName + "\" call.");
			}
			if (PhotonNetwork.NetworkingClient != null)
			{
				PhotonNetwork.RPC(view, methodName, RpcTarget.Others, targetPlayer, encrypt, parameters);
				return;
			}
			UnityEngine.Debug.LogWarning("Could not execute RPC " + methodName + ". Possible scene loading in progress?");
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004C40 File Offset: 0x00002E40
		public static void SetInterestGroups(byte group, bool enabled)
		{
			if (!PhotonNetwork.VerifyCanUseNetwork())
			{
				return;
			}
			if (enabled)
			{
				byte[] enableGroups = new byte[]
				{
					group
				};
				PhotonNetwork.SetInterestGroups(null, enableGroups);
				return;
			}
			PhotonNetwork.SetInterestGroups(new byte[]
			{
				group
			}, null);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004C7B File Offset: 0x00002E7B
		public static void LoadLevel(int levelNumber)
		{
			if (ConnectionHandler.AppQuits)
			{
				return;
			}
			if (PhotonNetwork.AutomaticallySyncScene)
			{
				PhotonNetwork.SetLevelInPropsIfSynced(levelNumber);
			}
			PhotonNetwork.IsMessageQueueRunning = false;
			PhotonNetwork.loadingLevelAndPausedNetwork = true;
			PhotonNetwork._AsyncLevelLoadingOperation = SceneManager.LoadSceneAsync(levelNumber, LoadSceneMode.Single);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004CAF File Offset: 0x00002EAF
		public static void LoadLevel(string levelName)
		{
			if (ConnectionHandler.AppQuits)
			{
				return;
			}
			if (PhotonNetwork.AutomaticallySyncScene)
			{
				PhotonNetwork.SetLevelInPropsIfSynced(levelName);
			}
			PhotonNetwork.IsMessageQueueRunning = false;
			PhotonNetwork.loadingLevelAndPausedNetwork = true;
			PhotonNetwork._AsyncLevelLoadingOperation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004CDE File Offset: 0x00002EDE
		public static bool WebRpc(string name, object parameters, bool sendAuthCookie = false)
		{
			return PhotonNetwork.NetworkingClient.OpWebRpc(name, parameters, sendAuthCookie);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004CF0 File Offset: 0x00002EF0
		private static void SetupLogging()
		{
			if (PhotonNetwork.LogLevel == PunLogLevel.ErrorsOnly)
			{
				PhotonNetwork.LogLevel = PhotonNetwork.PhotonServerSettings.PunLogging;
			}
			if (PhotonNetwork.NetworkingClient.LoadBalancingPeer.DebugOut == DebugLevel.ERROR)
			{
				PhotonNetwork.NetworkingClient.LoadBalancingPeer.DebugOut = PhotonNetwork.PhotonServerSettings.AppSettings.NetworkLogging;
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004D44 File Offset: 0x00002F44
		public static void LoadOrCreateSettings(bool reload = false)
		{
			if (reload)
			{
				PhotonNetwork.photonServerSettings = null;
			}
			else if (PhotonNetwork.photonServerSettings != null)
			{
				UnityEngine.Debug.LogWarning("photonServerSettings is not null. Will not LoadOrCreateSettings().");
				return;
			}
			PhotonNetwork.photonServerSettings = (ServerSettings)Resources.Load("PhotonServerSettings", typeof(ServerSettings));
			if (PhotonNetwork.photonServerSettings != null)
			{
				return;
			}
			if (PhotonNetwork.photonServerSettings == null)
			{
				PhotonNetwork.photonServerSettings = (ServerSettings)ScriptableObject.CreateInstance("ServerSettings");
				if (PhotonNetwork.photonServerSettings == null)
				{
					UnityEngine.Debug.LogError("Failed to create ServerSettings. PUN is unable to run this way. If you deleted it from the project, reload the Editor.");
					return;
				}
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004DDC File Offset: 0x00002FDC
		[Obsolete("Use PhotonViewCollection instead for an iterable collection of current photonViews.")]
		public static PhotonView[] PhotonViews
		{
			get
			{
				PhotonView[] array = new PhotonView[PhotonNetwork.photonViewList.Count];
				int num = 0;
				foreach (PhotonView photonView in PhotonNetwork.photonViewList.Values)
				{
					array[num] = photonView;
					num++;
				}
				return array;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004E4C File Offset: 0x0000304C
		public static NonAllocDictionary<int, PhotonView>.ValueIterator PhotonViewCollection
		{
			get
			{
				return PhotonNetwork.photonViewList.Values;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004E58 File Offset: 0x00003058
		public static int ViewCount
		{
			get
			{
				return PhotonNetwork.photonViewList.Count;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000A4 RID: 164 RVA: 0x00004E64 File Offset: 0x00003064
		// (remove) Token: 0x060000A5 RID: 165 RVA: 0x00004E98 File Offset: 0x00003098
		private static event Action<PhotonView, Player> OnOwnershipRequestEv
		{
			[CompilerGenerated]
			add
			{
				Action<PhotonView, Player> action = PhotonNetwork.OnOwnershipRequestEv;
				Action<PhotonView, Player> action2;
				do
				{
					action2 = action;
					Action<PhotonView, Player> value2 = (Action<PhotonView, Player>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<PhotonView, Player>>(ref PhotonNetwork.OnOwnershipRequestEv, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<PhotonView, Player> action = PhotonNetwork.OnOwnershipRequestEv;
				Action<PhotonView, Player> action2;
				do
				{
					action2 = action;
					Action<PhotonView, Player> value2 = (Action<PhotonView, Player>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<PhotonView, Player>>(ref PhotonNetwork.OnOwnershipRequestEv, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000A6 RID: 166 RVA: 0x00004ECC File Offset: 0x000030CC
		// (remove) Token: 0x060000A7 RID: 167 RVA: 0x00004F00 File Offset: 0x00003100
		private static event Action<PhotonView, Player> OnOwnershipTransferedEv
		{
			[CompilerGenerated]
			add
			{
				Action<PhotonView, Player> action = PhotonNetwork.OnOwnershipTransferedEv;
				Action<PhotonView, Player> action2;
				do
				{
					action2 = action;
					Action<PhotonView, Player> value2 = (Action<PhotonView, Player>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<PhotonView, Player>>(ref PhotonNetwork.OnOwnershipTransferedEv, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<PhotonView, Player> action = PhotonNetwork.OnOwnershipTransferedEv;
				Action<PhotonView, Player> action2;
				do
				{
					action2 = action;
					Action<PhotonView, Player> value2 = (Action<PhotonView, Player>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<PhotonView, Player>>(ref PhotonNetwork.OnOwnershipTransferedEv, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060000A8 RID: 168 RVA: 0x00004F34 File Offset: 0x00003134
		// (remove) Token: 0x060000A9 RID: 169 RVA: 0x00004F68 File Offset: 0x00003168
		private static event Action<PhotonView, Player> OnOwnershipTransferFailedEv
		{
			[CompilerGenerated]
			add
			{
				Action<PhotonView, Player> action = PhotonNetwork.OnOwnershipTransferFailedEv;
				Action<PhotonView, Player> action2;
				do
				{
					action2 = action;
					Action<PhotonView, Player> value2 = (Action<PhotonView, Player>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<PhotonView, Player>>(ref PhotonNetwork.OnOwnershipTransferFailedEv, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<PhotonView, Player> action = PhotonNetwork.OnOwnershipTransferFailedEv;
				Action<PhotonView, Player> action2;
				do
				{
					action2 = action;
					Action<PhotonView, Player> value2 = (Action<PhotonView, Player>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<PhotonView, Player>>(ref PhotonNetwork.OnOwnershipTransferFailedEv, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004F9C File Offset: 0x0000319C
		public static void AddCallbackTarget(object target)
		{
			if (target is PhotonView)
			{
				return;
			}
			IPunOwnershipCallbacks punOwnershipCallbacks = target as IPunOwnershipCallbacks;
			if (punOwnershipCallbacks != null)
			{
				PhotonNetwork.OnOwnershipRequestEv += punOwnershipCallbacks.OnOwnershipRequest;
				PhotonNetwork.OnOwnershipTransferedEv += punOwnershipCallbacks.OnOwnershipTransfered;
				PhotonNetwork.OnOwnershipTransferFailedEv += punOwnershipCallbacks.OnOwnershipTransferFailed;
			}
			PhotonNetwork.NetworkingClient.AddCallbackTarget(target);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00005000 File Offset: 0x00003200
		public static void RemoveCallbackTarget(object target)
		{
			if (target is PhotonView || PhotonNetwork.NetworkingClient == null)
			{
				return;
			}
			IPunOwnershipCallbacks punOwnershipCallbacks = target as IPunOwnershipCallbacks;
			if (punOwnershipCallbacks != null)
			{
				PhotonNetwork.OnOwnershipRequestEv -= punOwnershipCallbacks.OnOwnershipRequest;
				PhotonNetwork.OnOwnershipTransferedEv -= punOwnershipCallbacks.OnOwnershipTransfered;
				PhotonNetwork.OnOwnershipTransferFailedEv -= punOwnershipCallbacks.OnOwnershipTransferFailed;
			}
			PhotonNetwork.NetworkingClient.RemoveCallbackTarget(target);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005068 File Offset: 0x00003268
		internal static string CallbacksToString()
		{
			string[] value = (from m in PhotonNetwork.NetworkingClient.ConnectionCallbackTargets
			select m.ToString()).ToArray<string>();
			return string.Join(", ", value);
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000050B4 File Offset: 0x000032B4
		// (set) Token: 0x060000AE RID: 174 RVA: 0x000050BB File Offset: 0x000032BB
		public static IPunPrefabPool PrefabPool
		{
			get
			{
				return PhotonNetwork.prefabPool;
			}
			set
			{
				if (value == null)
				{
					UnityEngine.Debug.LogWarning("PhotonNetwork.PrefabPool cannot be set to null. It will default back to using the 'DefaultPool' Pool");
					PhotonNetwork.prefabPool = new DefaultPool();
					return;
				}
				PhotonNetwork.prefabPool = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000050DB File Offset: 0x000032DB
		public static float LevelLoadingProgress
		{
			get
			{
				if (PhotonNetwork._AsyncLevelLoadingOperation != null)
				{
					PhotonNetwork._levelLoadingProgress = PhotonNetwork._AsyncLevelLoadingOperation.progress;
				}
				else if (PhotonNetwork._levelLoadingProgress > 0f)
				{
					PhotonNetwork._levelLoadingProgress = 1f;
				}
				return PhotonNetwork._levelLoadingProgress;
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005110 File Offset: 0x00003310
		private static void LeftRoomCleanup()
		{
			if (PhotonNetwork._AsyncLevelLoadingOperation != null)
			{
				PhotonNetwork._AsyncLevelLoadingOperation.allowSceneActivation = false;
				PhotonNetwork._AsyncLevelLoadingOperation = null;
			}
			PhotonNetwork.rpcEvent.Clear();
			bool flag = PhotonNetwork.NetworkingClient.CurrentRoom != null && PhotonNetwork.CurrentRoom.AutoCleanUp;
			PhotonNetwork.allowedReceivingGroups = new HashSet<byte>();
			PhotonNetwork.blockedSendingGroups = new HashSet<byte>();
			if (flag || PhotonNetwork.offlineModeRoom != null)
			{
				PhotonNetwork.LocalCleanupAnythingInstantiated(true);
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005180 File Offset: 0x00003380
		internal static void LocalCleanupAnythingInstantiated(bool destroyInstantiatedGameObjects)
		{
			if (destroyInstantiatedGameObjects)
			{
				HashSet<GameObject> hashSet = new HashSet<GameObject>();
				foreach (PhotonView photonView in PhotonNetwork.photonViewList.Values)
				{
					if (photonView.isRuntimeInstantiated)
					{
						hashSet.Add(photonView.gameObject);
					}
					else
					{
						photonView.ResetPhotonView(true);
					}
				}
				foreach (GameObject go in hashSet)
				{
					PhotonNetwork.RemoveInstantiatedGO(go, true);
				}
			}
			PhotonNetwork.lastUsedViewSubId = 0;
			PhotonNetwork.lastUsedViewSubIdStatic = 0;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005248 File Offset: 0x00003448
		private static void ResetPhotonViewsOnSerialize()
		{
			foreach (PhotonView photonView in PhotonNetwork.photonViewList.Values)
			{
				photonView.lastOnSerializeDataSent = null;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000052A0 File Offset: 0x000034A0
		internal static void ExecuteRpc(ExitGames.Client.Photon.Hashtable rpcData, Player sender)
		{
			if (rpcData == null || !rpcData.ContainsKey(PhotonNetwork.keyByteZero))
			{
				UnityEngine.Debug.LogError("Malformed RPC; this should never occur. Content: " + SupportClass.DictionaryToString(rpcData, true));
				return;
			}
			int num = (int)rpcData[PhotonNetwork.keyByteZero];
			int num2 = 0;
			if (rpcData.ContainsKey(PhotonNetwork.keyByteOne))
			{
				num2 = (int)((short)rpcData[PhotonNetwork.keyByteOne]);
			}
			string text;
			if (rpcData.ContainsKey(PhotonNetwork.keyByteFive))
			{
				int num3 = (int)((byte)rpcData[PhotonNetwork.keyByteFive]);
				if (num3 > PhotonNetwork.PhotonServerSettings.RpcList.Count - 1)
				{
					UnityEngine.Debug.LogError("Could not find RPC with index: " + num3.ToString() + ". Going to ignore! Check PhotonServerSettings.RpcList");
					return;
				}
				text = PhotonNetwork.PhotonServerSettings.RpcList[num3];
			}
			else
			{
				text = (string)rpcData[PhotonNetwork.keyByteThree];
			}
			object[] array = null;
			if (rpcData.ContainsKey(PhotonNetwork.keyByteFour))
			{
				array = (object[])rpcData[PhotonNetwork.keyByteFour];
			}
			PhotonView photonView = PhotonNetwork.GetPhotonView(num);
			if (photonView == null)
			{
				int num4 = num / PhotonNetwork.MAX_VIEW_IDS;
				bool flag = num4 == PhotonNetwork.NetworkingClient.LocalPlayer.ActorNumber;
				bool flag2 = sender != null && num4 == sender.ActorNumber;
				if (flag)
				{
					UnityEngine.Debug.LogWarning(string.Concat(new string[]
					{
						"Received RPC \"",
						text,
						"\" for viewID ",
						num.ToString(),
						" but this PhotonView does not exist! View was/is ours.",
						flag2 ? " Owner called." : " Remote called.",
						" By: ",
						(sender != null) ? sender.ToString() : null
					}));
					return;
				}
				UnityEngine.Debug.LogWarning(string.Concat(new string[]
				{
					"Received RPC \"",
					text,
					"\" for viewID ",
					num.ToString(),
					" but this PhotonView does not exist! Was remote PV.",
					flag2 ? " Owner called." : " Remote called.",
					" By: ",
					(sender != null) ? sender.ToString() : null,
					" Maybe GO was destroyed but RPC not cleaned up."
				}));
				return;
			}
			else
			{
				if (photonView.Prefix != num2)
				{
					UnityEngine.Debug.LogError(string.Concat(new string[]
					{
						"Received RPC \"",
						text,
						"\" on viewID ",
						num.ToString(),
						" with a prefix of ",
						num2.ToString(),
						", our prefix is ",
						photonView.Prefix.ToString(),
						". The RPC has been ignored."
					}));
					return;
				}
				if (string.IsNullOrEmpty(text))
				{
					UnityEngine.Debug.LogError("Malformed RPC; this should never occur. Content: " + SupportClass.DictionaryToString(rpcData, true));
					return;
				}
				if (PhotonNetwork.LogLevel >= PunLogLevel.Full)
				{
					UnityEngine.Debug.Log("Received RPC: " + text);
				}
				if (photonView.Group != 0 && !PhotonNetwork.allowedReceivingGroups.Contains(photonView.Group))
				{
					return;
				}
				Type[] array2 = null;
				if (array != null && array.Length != 0)
				{
					array2 = new Type[array.Length];
					int num5 = 0;
					foreach (object obj in array)
					{
						if (obj == null)
						{
							array2[num5] = null;
						}
						else
						{
							array2[num5] = obj.GetType();
						}
						num5++;
					}
				}
				int num6 = 0;
				int num7 = 0;
				if (!PhotonNetwork.UseRpcMonoBehaviourCache || photonView.RpcMonoBehaviours == null || photonView.RpcMonoBehaviours.Length == 0)
				{
					photonView.RefreshRpcMonoBehaviourCache();
				}
				for (int j = 0; j < photonView.RpcMonoBehaviours.Length; j++)
				{
					MonoBehaviour monoBehaviour = photonView.RpcMonoBehaviours[j];
					if (monoBehaviour == null)
					{
						UnityEngine.Debug.LogError("ERROR You have missing MonoBehaviours on your gameobjects!");
					}
					else
					{
						Type type = monoBehaviour.GetType();
						List<MethodInfo> list = null;
						if (!PhotonNetwork.monoRPCMethodsCache.TryGetValue(type, out list))
						{
							List<MethodInfo> methods = SupportClass.GetMethods(type, PhotonNetwork.typePunRPC);
							PhotonNetwork.monoRPCMethodsCache[type] = methods;
							list = methods;
						}
						if (list != null)
						{
							for (int k = 0; k < list.Count; k++)
							{
								MethodInfo methodInfo = list[k];
								if (methodInfo.Name.Equals(text))
								{
									ParameterInfo[] cachedParemeters = methodInfo.GetCachedParemeters();
									num7++;
									if (array == null)
									{
										if (cachedParemeters.Length == 0)
										{
											num6++;
											object obj2 = methodInfo.Invoke(monoBehaviour, null);
											IEnumerator routine;
											if (PhotonNetwork.RunRpcCoroutines && (routine = (obj2 as IEnumerator)) != null)
											{
												PhotonHandler.Instance.StartCoroutine(routine);
											}
										}
										else if (cachedParemeters.Length == 1 && cachedParemeters[0].ParameterType == typeof(PhotonMessageInfo))
										{
											int timestamp = (int)rpcData[PhotonNetwork.keyByteTwo];
											num6++;
											object obj3 = methodInfo.Invoke(monoBehaviour, new object[]
											{
												new PhotonMessageInfo(sender, timestamp, photonView)
											});
											IEnumerator routine2;
											if (PhotonNetwork.RunRpcCoroutines && (routine2 = (obj3 as IEnumerator)) != null)
											{
												PhotonHandler.Instance.StartCoroutine(routine2);
											}
										}
									}
									else if (cachedParemeters.Length == array.Length)
									{
										if (PhotonNetwork.CheckTypeMatch(cachedParemeters, array2))
										{
											num6++;
											object obj4 = methodInfo.Invoke(monoBehaviour, array);
											IEnumerator routine3;
											if (PhotonNetwork.RunRpcCoroutines && (routine3 = (obj4 as IEnumerator)) != null)
											{
												PhotonHandler.Instance.StartCoroutine(routine3);
											}
										}
									}
									else if (cachedParemeters.Length == array.Length + 1)
									{
										if (cachedParemeters[cachedParemeters.Length - 1].ParameterType == typeof(PhotonMessageInfo) && PhotonNetwork.CheckTypeMatch(cachedParemeters, array2))
										{
											int timestamp2 = (int)rpcData[PhotonNetwork.keyByteTwo];
											object[] array3 = new object[array.Length + 1];
											array.CopyTo(array3, 0);
											array3[array3.Length - 1] = new PhotonMessageInfo(sender, timestamp2, photonView);
											num6++;
											object obj5 = methodInfo.Invoke(monoBehaviour, array3);
											IEnumerator routine4;
											if (PhotonNetwork.RunRpcCoroutines && (routine4 = (obj5 as IEnumerator)) != null)
											{
												PhotonHandler.Instance.StartCoroutine(routine4);
											}
										}
									}
									else if (cachedParemeters.Length == 1 && cachedParemeters[0].ParameterType.IsArray)
									{
										num6++;
										object obj6 = methodInfo.Invoke(monoBehaviour, new object[]
										{
											array
										});
										IEnumerator routine5;
										if (PhotonNetwork.RunRpcCoroutines && (routine5 = (obj6 as IEnumerator)) != null)
										{
											PhotonHandler.Instance.StartCoroutine(routine5);
										}
									}
								}
							}
						}
					}
				}
				if (num6 != 1)
				{
					string text2 = string.Empty;
					if (array2 != null)
					{
						int num8 = array2.Length;
						foreach (Type type2 in array2)
						{
							if (text2 != string.Empty)
							{
								text2 += ", ";
							}
							if (type2 == null)
							{
								text2 += "null";
							}
							else
							{
								text2 += type2.Name;
							}
						}
					}
					GameObject context = (photonView != null) ? photonView.gameObject : null;
					if (num6 == 0)
					{
						if (num7 == 0)
						{
							UnityEngine.Debug.LogErrorFormat(context, "RPC method '{0}({2})' not found on object with PhotonView {1}. Implement as non-static. Apply [PunRPC]. Components on children are not found. Return type must be void or IEnumerator (if you enable RunRpcCoroutines). RPCs are a one-way message.", new object[]
							{
								text,
								num,
								text2
							});
							return;
						}
						UnityEngine.Debug.LogErrorFormat(context, "RPC method '{0}' found on object with PhotonView {1} but has wrong parameters. Implement as '{0}({2})'. PhotonMessageInfo is optional as final parameter.Return type must be void or IEnumerator (if you enable RunRpcCoroutines).", new object[]
						{
							text,
							num,
							text2
						});
						return;
					}
					else
					{
						UnityEngine.Debug.LogErrorFormat(context, "RPC method '{0}({2})' found {3}x on object with PhotonView {1}. Only one component should implement it.Return type must be void or IEnumerator (if you enable RunRpcCoroutines).", new object[]
						{
							text,
							num,
							text2,
							num7
						});
					}
				}
				return;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000059FC File Offset: 0x00003BFC
		private static bool CheckTypeMatch(ParameterInfo[] methodParameters, Type[] callParameterTypes)
		{
			if (methodParameters.Length < callParameterTypes.Length)
			{
				return false;
			}
			for (int i = 0; i < callParameterTypes.Length; i++)
			{
				Type parameterType = methodParameters[i].ParameterType;
				if (callParameterTypes[i] != null && !parameterType.IsAssignableFrom(callParameterTypes[i]) && (!parameterType.IsEnum || !Enum.GetUnderlyingType(parameterType).IsAssignableFrom(callParameterTypes[i])))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005A5C File Offset: 0x00003C5C
		public static void DestroyPlayerObjects(int playerId, bool localOnly)
		{
			if (playerId <= 0)
			{
				UnityEngine.Debug.LogError("Failed to Destroy objects of playerId: " + playerId.ToString());
				return;
			}
			if (!localOnly)
			{
				PhotonNetwork.OpRemoveFromServerInstantiationsOfPlayer(playerId);
				PhotonNetwork.OpCleanActorRpcBuffer(playerId);
				PhotonNetwork.SendDestroyOfPlayer(playerId);
			}
			HashSet<GameObject> hashSet = new HashSet<GameObject>();
			foreach (PhotonView photonView in PhotonNetwork.photonViewList.Values)
			{
				if (photonView == null)
				{
					UnityEngine.Debug.LogError("Null view");
				}
				else if (photonView.CreatorActorNr == playerId)
				{
					hashSet.Add(photonView.gameObject);
				}
				else if (photonView.OwnerActorNr == playerId)
				{
					Player owner = photonView.Owner;
					photonView.OwnerActorNr = photonView.CreatorActorNr;
					photonView.ControllerActorNr = photonView.CreatorActorNr;
					if (PhotonNetwork.OnOwnershipTransferedEv != null)
					{
						PhotonNetwork.OnOwnershipTransferedEv(photonView, owner);
					}
				}
			}
			foreach (GameObject go in hashSet)
			{
				PhotonNetwork.RemoveInstantiatedGO(go, true);
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005B8C File Offset: 0x00003D8C
		public static void DestroyAll(bool localOnly)
		{
			if (!localOnly)
			{
				PhotonNetwork.OpRemoveCompleteCache();
				PhotonNetwork.SendDestroyOfAll();
			}
			PhotonNetwork.LocalCleanupAnythingInstantiated(true);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005BA4 File Offset: 0x00003DA4
		internal static void RemoveInstantiatedGO(GameObject go, bool localOnly)
		{
			if (ConnectionHandler.AppQuits)
			{
				return;
			}
			if (go == null)
			{
				UnityEngine.Debug.LogError("Failed to 'network-remove' GameObject because it's null.");
				return;
			}
			go.GetComponentsInChildren<PhotonView>(true, PhotonNetwork.foundPVs);
			if (PhotonNetwork.foundPVs.Count <= 0)
			{
				UnityEngine.Debug.LogError("Failed to 'network-remove' GameObject because has no PhotonView components: " + ((go != null) ? go.ToString() : null));
				return;
			}
			PhotonView photonView = PhotonNetwork.foundPVs[0];
			if (!localOnly && !photonView.IsMine)
			{
				string str = "Failed to 'network-remove' GameObject. Client is neither owner nor MasterClient taking over for owner who left: ";
				PhotonView photonView2 = photonView;
				UnityEngine.Debug.LogError(str + ((photonView2 != null) ? photonView2.ToString() : null));
				PhotonNetwork.foundPVs.Clear();
				return;
			}
			if (!localOnly)
			{
				PhotonNetwork.ServerCleanInstantiateAndDestroy(photonView);
			}
			int creatorActorNr = photonView.CreatorActorNr;
			for (int i = PhotonNetwork.foundPVs.Count - 1; i >= 0; i--)
			{
				PhotonView photonView3 = PhotonNetwork.foundPVs[i];
				if (!(photonView3 == null))
				{
					if (i != 0 && photonView3.CreatorActorNr != creatorActorNr)
					{
						photonView3.transform.SetParent(null, true);
					}
					else
					{
						photonView3.OnPreNetDestroy(photonView);
						if (photonView3.InstantiationId >= 1)
						{
							PhotonNetwork.LocalCleanPhotonView(photonView3);
						}
						if (!localOnly)
						{
							PhotonNetwork.OpCleanRpcBuffer(photonView3);
						}
					}
				}
			}
			if (PhotonNetwork.LogLevel >= PunLogLevel.Full)
			{
				UnityEngine.Debug.Log("Network destroy Instantiated GO: " + go.name);
			}
			PhotonNetwork.foundPVs.Clear();
			go.SetActive(false);
			PhotonNetwork.prefabPool.Destroy(go);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005CF4 File Offset: 0x00003EF4
		private static void ServerCleanInstantiateAndDestroy(PhotonView photonView)
		{
			int num;
			if (photonView.isRuntimeInstantiated)
			{
				num = photonView.InstantiationId;
				PhotonNetwork.removeFilter[PhotonNetwork.keyByteSeven] = num;
				PhotonNetwork.ServerCleanOptions.CachingOption = EventCaching.RemoveFromRoomCache;
				PhotonNetwork.RaiseEventInternal(202, PhotonNetwork.removeFilter, PhotonNetwork.ServerCleanOptions, SendOptions.SendReliable);
			}
			else
			{
				num = photonView.ViewID;
			}
			PhotonNetwork.ServerCleanDestroyEvent[PhotonNetwork.keyByteZero] = num;
			PhotonNetwork.ServerCleanOptions.CachingOption = (photonView.isRuntimeInstantiated ? EventCaching.DoNotCache : EventCaching.AddToRoomCacheGlobal);
			PhotonNetwork.RaiseEventInternal(204, PhotonNetwork.ServerCleanDestroyEvent, PhotonNetwork.ServerCleanOptions, SendOptions.SendReliable);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005D98 File Offset: 0x00003F98
		private static void SendDestroyOfPlayer(int actorNr)
		{
			ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
			hashtable[PhotonNetwork.keyByteZero] = actorNr;
			PhotonNetwork.RaiseEventInternal(207, hashtable, null, SendOptions.SendReliable);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005DD0 File Offset: 0x00003FD0
		private static void SendDestroyOfAll()
		{
			ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
			hashtable[PhotonNetwork.keyByteZero] = -1;
			PhotonNetwork.RaiseEventInternal(207, hashtable, null, SendOptions.SendReliable);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005E08 File Offset: 0x00004008
		private static void OpRemoveFromServerInstantiationsOfPlayer(int actorNr)
		{
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions
			{
				CachingOption = EventCaching.RemoveFromRoomCache,
				TargetActors = new int[]
				{
					actorNr
				}
			};
			PhotonNetwork.RaiseEventInternal(202, null, raiseEventOptions, SendOptions.SendReliable);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005E46 File Offset: 0x00004046
		internal static void RequestOwnership(int viewID, int fromOwner)
		{
			PhotonNetwork.RaiseEventInternal(209, new int[]
			{
				viewID,
				fromOwner
			}, PhotonNetwork.SendToAllOptions, SendOptions.SendReliable);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005E6B File Offset: 0x0000406B
		internal static void TransferOwnership(int viewID, int playerID)
		{
			PhotonNetwork.RaiseEventInternal(210, new int[]
			{
				viewID,
				playerID
			}, PhotonNetwork.SendToAllOptions, SendOptions.SendReliable);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005E90 File Offset: 0x00004090
		internal static void OwnershipUpdate(int[] viewOwnerPairs, int targetActor = -1)
		{
			RaiseEventOptions raiseEventOptions;
			if (targetActor == -1)
			{
				raiseEventOptions = PhotonNetwork.SendToOthersOptions;
			}
			else
			{
				PhotonNetwork.SendToSingleOptions.TargetActors[0] = targetActor;
				raiseEventOptions = PhotonNetwork.SendToSingleOptions;
			}
			PhotonNetwork.RaiseEventInternal(212, viewOwnerPairs, raiseEventOptions, SendOptions.SendReliable);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005ECE File Offset: 0x000040CE
		public static bool LocalCleanPhotonView(PhotonView view)
		{
			view.removedFromLocalViewList = true;
			return PhotonNetwork.photonViewList.Remove(view.ViewID);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005EE8 File Offset: 0x000040E8
		public static PhotonView GetPhotonView(int viewID)
		{
			PhotonView result = null;
			PhotonNetwork.photonViewList.TryGetValue(viewID, out result);
			return result;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005F08 File Offset: 0x00004108
		public static void RegisterPhotonView(PhotonView netView)
		{
			if (!Application.isPlaying)
			{
				PhotonNetwork.photonViewList = new NonAllocDictionary<int, PhotonView>(29U);
				return;
			}
			if (netView.ViewID == 0)
			{
				UnityEngine.Debug.Log("PhotonView register is ignored, because viewID is 0. No id assigned yet to: " + ((netView != null) ? netView.ToString() : null));
				return;
			}
			PhotonView photonView = null;
			if (PhotonNetwork.photonViewList.TryGetValue(netView.ViewID, out photonView))
			{
				if (!(netView != photonView))
				{
					return;
				}
				UnityEngine.Debug.LogError(string.Format("PhotonView ID duplicate found: {0}. New: {1} old: {2}. Maybe one wasn't destroyed on scene load?! Check for 'DontDestroyOnLoad'. Destroying old entry, adding new.", netView.ViewID, netView, photonView));
				PhotonNetwork.RemoveInstantiatedGO(photonView.gameObject, true);
			}
			PhotonNetwork.photonViewList.Add(netView.ViewID, netView);
			netView.removedFromLocalViewList = false;
			if (PhotonNetwork.LogLevel >= PunLogLevel.Full)
			{
				UnityEngine.Debug.Log("Registered PhotonView: " + netView.ViewID.ToString());
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005FD8 File Offset: 0x000041D8
		public static void OpCleanActorRpcBuffer(int actorNumber)
		{
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions
			{
				CachingOption = EventCaching.RemoveFromRoomCache,
				TargetActors = new int[]
				{
					actorNumber
				}
			};
			PhotonNetwork.RaiseEventInternal(200, null, raiseEventOptions, SendOptions.SendReliable);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00006018 File Offset: 0x00004218
		public static void OpRemoveCompleteCacheOfPlayer(int actorNumber)
		{
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions
			{
				CachingOption = EventCaching.RemoveFromRoomCache,
				TargetActors = new int[]
				{
					actorNumber
				}
			};
			PhotonNetwork.RaiseEventInternal(0, null, raiseEventOptions, SendOptions.SendReliable);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00006054 File Offset: 0x00004254
		public static void OpRemoveCompleteCache()
		{
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions
			{
				CachingOption = EventCaching.RemoveFromRoomCache,
				Receivers = ReceiverGroup.MasterClient
			};
			PhotonNetwork.RaiseEventInternal(0, null, raiseEventOptions, SendOptions.SendReliable);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00006084 File Offset: 0x00004284
		private static void RemoveCacheOfLeftPlayers()
		{
			ParameterDictionary parameterDictionary = new ParameterDictionary(2);
			parameterDictionary[244] = 0;
			parameterDictionary[247] = 7;
			PhotonNetwork.NetworkingClient.LoadBalancingPeer.SendOperation(253, parameterDictionary, SendOptions.SendReliable);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000060D8 File Offset: 0x000042D8
		public static void CleanRpcBufferIfMine(PhotonView view)
		{
			if (view.OwnerActorNr != PhotonNetwork.NetworkingClient.LocalPlayer.ActorNumber && !PhotonNetwork.NetworkingClient.LocalPlayer.IsMasterClient)
			{
				string str = "Cannot remove cached RPCs on a PhotonView thats not ours! ";
				Player owner = view.Owner;
				UnityEngine.Debug.LogError(str + ((owner != null) ? owner.ToString() : null) + " scene: " + view.IsRoomView.ToString());
				return;
			}
			PhotonNetwork.OpCleanRpcBuffer(view);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00006148 File Offset: 0x00004348
		public static void OpCleanRpcBuffer(PhotonView view)
		{
			PhotonNetwork.rpcFilterByViewId[PhotonNetwork.keyByteZero] = view.ViewID;
			PhotonNetwork.RaiseEventInternal(200, PhotonNetwork.rpcFilterByViewId, PhotonNetwork.OpCleanRpcBufferOptions, SendOptions.SendReliable);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00006180 File Offset: 0x00004380
		public static void RemoveRPCsInGroup(int group)
		{
			foreach (PhotonView photonView in PhotonNetwork.photonViewList.Values)
			{
				if ((int)photonView.Group == group)
				{
					PhotonNetwork.CleanRpcBufferIfMine(photonView);
				}
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000061E4 File Offset: 0x000043E4
		public static bool RemoveBufferedRPCs(int viewId = 0, string methodName = null, int[] callersActorNumbers = null)
		{
			ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable(2);
			if (viewId != 0)
			{
				hashtable[PhotonNetwork.keyByteZero] = viewId;
			}
			if (!string.IsNullOrEmpty(methodName))
			{
				int num;
				if (PhotonNetwork.rpcShortcuts.TryGetValue(methodName, out num))
				{
					hashtable[PhotonNetwork.keyByteFive] = (byte)num;
				}
				else
				{
					hashtable[PhotonNetwork.keyByteThree] = methodName;
				}
			}
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
			raiseEventOptions.CachingOption = EventCaching.RemoveFromRoomCache;
			if (callersActorNumbers != null)
			{
				raiseEventOptions.TargetActors = callersActorNumbers;
			}
			return PhotonNetwork.RaiseEventInternal(200, hashtable, raiseEventOptions, SendOptions.SendReliable);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000626B File Offset: 0x0000446B
		public static void SetLevelPrefix(byte prefix)
		{
			PhotonNetwork.currentLevelPrefix = prefix;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00006274 File Offset: 0x00004474
		internal static void RPC(PhotonView view, string methodName, RpcTarget target, Player player, bool encrypt, params object[] parameters)
		{
			if (PhotonNetwork.blockedSendingGroups.Contains(view.Group))
			{
				return;
			}
			if (view.ViewID < 1)
			{
				UnityEngine.Debug.LogError(string.Concat(new string[]
				{
					"Illegal view ID:",
					view.ViewID.ToString(),
					" method: ",
					methodName,
					" GO:",
					view.gameObject.name
				}));
			}
			if (PhotonNetwork.LogLevel >= PunLogLevel.Full)
			{
				UnityEngine.Debug.Log(string.Concat(new string[]
				{
					"Sending RPC \"",
					methodName,
					"\" to target: ",
					target.ToString(),
					" or player:",
					(player != null) ? player.ToString() : null,
					"."
				}));
			}
			PhotonNetwork.rpcEvent.Clear();
			PhotonNetwork.rpcEvent[PhotonNetwork.keyByteZero] = view.ViewID;
			if (view.Prefix > 0)
			{
				PhotonNetwork.rpcEvent[PhotonNetwork.keyByteOne] = (short)view.Prefix;
			}
			PhotonNetwork.rpcEvent[PhotonNetwork.keyByteTwo] = PhotonNetwork.ServerTimestamp;
			int num = 0;
			if (PhotonNetwork.rpcShortcuts.TryGetValue(methodName, out num))
			{
				PhotonNetwork.rpcEvent[PhotonNetwork.keyByteFive] = (byte)num;
			}
			else
			{
				PhotonNetwork.rpcEvent[PhotonNetwork.keyByteThree] = methodName;
			}
			if (parameters != null && parameters.Length != 0)
			{
				PhotonNetwork.rpcEvent[PhotonNetwork.keyByteFour] = parameters;
			}
			SendOptions sendOptions = new SendOptions
			{
				Reliability = true,
				Encrypt = encrypt
			};
			if (player == null)
			{
				switch (target)
				{
				case RpcTarget.All:
					PhotonNetwork.RpcOptionsToAll.InterestGroup = view.Group;
					PhotonNetwork.RaiseEventInternal(200, PhotonNetwork.rpcEvent, PhotonNetwork.RpcOptionsToAll, sendOptions);
					PhotonNetwork.ExecuteRpc(PhotonNetwork.rpcEvent, PhotonNetwork.NetworkingClient.LocalPlayer);
					return;
				case RpcTarget.Others:
				{
					RaiseEventOptions raiseEventOptions = new RaiseEventOptions
					{
						InterestGroup = view.Group
					};
					PhotonNetwork.RaiseEventInternal(200, PhotonNetwork.rpcEvent, raiseEventOptions, sendOptions);
					return;
				}
				case RpcTarget.MasterClient:
				{
					if (PhotonNetwork.NetworkingClient.LocalPlayer.IsMasterClient)
					{
						PhotonNetwork.ExecuteRpc(PhotonNetwork.rpcEvent, PhotonNetwork.NetworkingClient.LocalPlayer);
						return;
					}
					RaiseEventOptions raiseEventOptions2 = new RaiseEventOptions
					{
						Receivers = ReceiverGroup.MasterClient
					};
					PhotonNetwork.RaiseEventInternal(200, PhotonNetwork.rpcEvent, raiseEventOptions2, sendOptions);
					return;
				}
				case RpcTarget.AllBuffered:
				{
					RaiseEventOptions raiseEventOptions3 = new RaiseEventOptions
					{
						CachingOption = EventCaching.AddToRoomCache
					};
					PhotonNetwork.RaiseEventInternal(200, PhotonNetwork.rpcEvent, raiseEventOptions3, sendOptions);
					PhotonNetwork.ExecuteRpc(PhotonNetwork.rpcEvent, PhotonNetwork.NetworkingClient.LocalPlayer);
					return;
				}
				case RpcTarget.OthersBuffered:
				{
					RaiseEventOptions raiseEventOptions4 = new RaiseEventOptions
					{
						CachingOption = EventCaching.AddToRoomCache
					};
					PhotonNetwork.RaiseEventInternal(200, PhotonNetwork.rpcEvent, raiseEventOptions4, sendOptions);
					return;
				}
				case RpcTarget.AllViaServer:
				{
					RaiseEventOptions raiseEventOptions5 = new RaiseEventOptions
					{
						InterestGroup = view.Group,
						Receivers = ReceiverGroup.All
					};
					PhotonNetwork.RaiseEventInternal(200, PhotonNetwork.rpcEvent, raiseEventOptions5, sendOptions);
					if (PhotonNetwork.OfflineMode)
					{
						PhotonNetwork.ExecuteRpc(PhotonNetwork.rpcEvent, PhotonNetwork.NetworkingClient.LocalPlayer);
						return;
					}
					break;
				}
				case RpcTarget.AllBufferedViaServer:
				{
					RaiseEventOptions raiseEventOptions6 = new RaiseEventOptions
					{
						InterestGroup = view.Group,
						Receivers = ReceiverGroup.All,
						CachingOption = EventCaching.AddToRoomCache
					};
					PhotonNetwork.RaiseEventInternal(200, PhotonNetwork.rpcEvent, raiseEventOptions6, sendOptions);
					if (PhotonNetwork.OfflineMode)
					{
						PhotonNetwork.ExecuteRpc(PhotonNetwork.rpcEvent, PhotonNetwork.NetworkingClient.LocalPlayer);
						return;
					}
					break;
				}
				default:
					UnityEngine.Debug.LogError("Unsupported target enum: " + target.ToString());
					break;
				}
				return;
			}
			if (PhotonNetwork.NetworkingClient.LocalPlayer.ActorNumber == player.ActorNumber)
			{
				PhotonNetwork.ExecuteRpc(PhotonNetwork.rpcEvent, player);
				return;
			}
			RaiseEventOptions raiseEventOptions7 = new RaiseEventOptions
			{
				TargetActors = new int[]
				{
					player.ActorNumber
				}
			};
			PhotonNetwork.RaiseEventInternal(200, PhotonNetwork.rpcEvent, raiseEventOptions7, sendOptions);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00006650 File Offset: 0x00004850
		public static void SetInterestGroups(byte[] disableGroups, byte[] enableGroups)
		{
			if (disableGroups != null)
			{
				if (disableGroups.Length == 0)
				{
					PhotonNetwork.allowedReceivingGroups.Clear();
				}
				else
				{
					foreach (byte b in disableGroups)
					{
						if (b <= 0)
						{
							UnityEngine.Debug.LogError("Error: PhotonNetwork.SetInterestGroups was called with an illegal group number: " + b.ToString() + ". The Group number should be at least 1.");
						}
						else if (PhotonNetwork.allowedReceivingGroups.Contains(b))
						{
							PhotonNetwork.allowedReceivingGroups.Remove(b);
						}
					}
				}
			}
			if (enableGroups != null)
			{
				if (enableGroups.Length == 0)
				{
					for (byte b2 = 0; b2 < 255; b2 += 1)
					{
						PhotonNetwork.allowedReceivingGroups.Add(b2);
					}
					PhotonNetwork.allowedReceivingGroups.Add(byte.MaxValue);
				}
				else
				{
					foreach (byte b3 in enableGroups)
					{
						if (b3 <= 0)
						{
							UnityEngine.Debug.LogError("Error: PhotonNetwork.SetInterestGroups was called with an illegal group number: " + b3.ToString() + ". The Group number should be at least 1.");
						}
						else
						{
							PhotonNetwork.allowedReceivingGroups.Add(b3);
						}
					}
				}
			}
			if (!PhotonNetwork.offlineMode)
			{
				PhotonNetwork.NetworkingClient.OpChangeGroups(disableGroups, enableGroups);
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00006748 File Offset: 0x00004948
		public static void SetSendingEnabled(byte group, bool enabled)
		{
			if (!enabled)
			{
				PhotonNetwork.blockedSendingGroups.Add(group);
				return;
			}
			PhotonNetwork.blockedSendingGroups.Remove(group);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00006768 File Offset: 0x00004968
		public static void SetSendingEnabled(byte[] disableGroups, byte[] enableGroups)
		{
			if (disableGroups != null)
			{
				foreach (byte item in disableGroups)
				{
					PhotonNetwork.blockedSendingGroups.Add(item);
				}
			}
			if (enableGroups != null)
			{
				foreach (byte item2 in enableGroups)
				{
					PhotonNetwork.blockedSendingGroups.Remove(item2);
				}
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000067B8 File Offset: 0x000049B8
		internal static void NewSceneLoaded()
		{
			if (PhotonNetwork.loadingLevelAndPausedNetwork)
			{
				PhotonNetwork._AsyncLevelLoadingOperation = null;
				PhotonNetwork.loadingLevelAndPausedNetwork = false;
				PhotonNetwork.IsMessageQueueRunning = true;
			}
			else
			{
				PhotonNetwork.SetLevelInPropsIfSynced(SceneManagerHelper.ActiveSceneName);
			}
			List<int> list = new List<int>();
			foreach (KeyValuePair<int, PhotonView> keyValuePair in PhotonNetwork.photonViewList)
			{
				if (keyValuePair.Value == null)
				{
					list.Add(keyValuePair.Key);
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				int key = list[i];
				PhotonNetwork.photonViewList.Remove(key);
			}
			if (list.Count > 0 && PhotonNetwork.LogLevel >= PunLogLevel.Informational)
			{
				UnityEngine.Debug.Log("New level loaded. Removed " + list.Count.ToString() + " scene view IDs from last level.");
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000068A8 File Offset: 0x00004AA8
		internal static void RunViewUpdate()
		{
			if (PhotonNetwork.OfflineMode || PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
			{
				return;
			}
			if (PhotonNetwork.CurrentRoom.Players.Count <= 1)
			{
				return;
			}
			foreach (KeyValuePair<int, PhotonView> keyValuePair in PhotonNetwork.photonViewList)
			{
				PhotonView value = keyValuePair.Value;
				if (value.Synchronization != ViewSynchronization.Off && value.IsMine && value.isActiveAndEnabled && !PhotonNetwork.blockedSendingGroups.Contains(value.Group))
				{
					List<object> list = PhotonNetwork.OnSerializeWrite(value);
					if (list != null)
					{
						PhotonNetwork.RaiseEventBatch raiseEventBatch = default(PhotonNetwork.RaiseEventBatch);
						raiseEventBatch.Reliable = (value.Synchronization == ViewSynchronization.ReliableDeltaCompressed || value.mixedModeIsReliable);
						raiseEventBatch.Group = value.Group;
						PhotonNetwork.SerializeViewBatch serializeViewBatch = null;
						if (!PhotonNetwork.serializeViewBatches.TryGetValue(raiseEventBatch, out serializeViewBatch))
						{
							serializeViewBatch = new PhotonNetwork.SerializeViewBatch(raiseEventBatch, 2);
							PhotonNetwork.serializeViewBatches.Add(raiseEventBatch, serializeViewBatch);
						}
						serializeViewBatch.Add(list);
						if (serializeViewBatch.ObjectUpdates.Count == serializeViewBatch.ObjectUpdates.Capacity)
						{
							PhotonNetwork.SendSerializeViewBatch(serializeViewBatch);
						}
					}
				}
			}
			foreach (KeyValuePair<PhotonNetwork.RaiseEventBatch, PhotonNetwork.SerializeViewBatch> keyValuePair2 in PhotonNetwork.serializeViewBatches)
			{
				PhotonNetwork.SendSerializeViewBatch(keyValuePair2.Value);
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00006A00 File Offset: 0x00004C00
		private static void SendSerializeViewBatch(PhotonNetwork.SerializeViewBatch batch)
		{
			if (batch == null || batch.ObjectUpdates.Count <= 2)
			{
				return;
			}
			PhotonNetwork.serializeRaiseEvOptions.InterestGroup = batch.Batch.Group;
			batch.ObjectUpdates[0] = PhotonNetwork.ServerTimestamp;
			batch.ObjectUpdates[1] = ((PhotonNetwork.currentLevelPrefix != 0) ? PhotonNetwork.currentLevelPrefix : null);
			PhotonNetwork.RaiseEventInternal(batch.Batch.Reliable ? 206 : 201, batch.ObjectUpdates, PhotonNetwork.serializeRaiseEvOptions, batch.Batch.Reliable ? SendOptions.SendReliable : SendOptions.SendUnreliable);
			batch.Clear();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00006AB4 File Offset: 0x00004CB4
		private static List<object> OnSerializeWrite(PhotonView view)
		{
			if (view.Synchronization == ViewSynchronization.Off)
			{
				return null;
			}
			PhotonMessageInfo info = new PhotonMessageInfo(PhotonNetwork.NetworkingClient.LocalPlayer, PhotonNetwork.ServerTimestamp, view);
			if (view.syncValues == null)
			{
				view.syncValues = new List<object>();
			}
			view.syncValues.Clear();
			PhotonNetwork.serializeStreamOut.SetWriteStream(view.syncValues, 0);
			PhotonNetwork.serializeStreamOut.SendNext(null);
			PhotonNetwork.serializeStreamOut.SendNext(null);
			PhotonNetwork.serializeStreamOut.SendNext(null);
			view.SerializeView(PhotonNetwork.serializeStreamOut, info);
			if (PhotonNetwork.serializeStreamOut.Count <= 3)
			{
				return null;
			}
			List<object> writeStream = PhotonNetwork.serializeStreamOut.GetWriteStream();
			writeStream[0] = view.ViewID;
			writeStream[1] = false;
			writeStream[2] = null;
			if (view.Synchronization == ViewSynchronization.Unreliable)
			{
				return writeStream;
			}
			if (view.Synchronization == ViewSynchronization.UnreliableOnChange)
			{
				if (PhotonNetwork.AlmostEquals(writeStream, view.lastOnSerializeDataSent))
				{
					if (view.mixedModeIsReliable)
					{
						return null;
					}
					view.mixedModeIsReliable = true;
					List<object> lastOnSerializeDataSent = view.lastOnSerializeDataSent;
					view.lastOnSerializeDataSent = writeStream;
					view.syncValues = lastOnSerializeDataSent;
				}
				else
				{
					view.mixedModeIsReliable = false;
					List<object> lastOnSerializeDataSent2 = view.lastOnSerializeDataSent;
					view.lastOnSerializeDataSent = writeStream;
					view.syncValues = lastOnSerializeDataSent2;
				}
				return writeStream;
			}
			if (view.Synchronization == ViewSynchronization.ReliableDeltaCompressed)
			{
				List<object> result = PhotonNetwork.DeltaCompressionWrite(view.lastOnSerializeDataSent, writeStream);
				List<object> lastOnSerializeDataSent3 = view.lastOnSerializeDataSent;
				view.lastOnSerializeDataSent = writeStream;
				view.syncValues = lastOnSerializeDataSent3;
				return result;
			}
			return null;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00006C18 File Offset: 0x00004E18
		private static void OnSerializeRead(object[] data, Player sender, int networkTime, short correctPrefix)
		{
			int viewID = (int)data[0];
			PhotonView photonView = PhotonNetwork.GetPhotonView(viewID);
			if (photonView == null)
			{
				if (PhotonNetwork.LogLevel >= PunLogLevel.Informational)
				{
					UnityEngine.Debug.LogWarning("Received OnSerialization for view ID " + viewID.ToString() + ". We have no such PhotonView! Ignore this if you're joining or leaving a room. State: " + PhotonNetwork.NetworkingClient.State.ToString());
				}
				return;
			}
			if (photonView.Prefix > 0 && (int)correctPrefix != photonView.Prefix)
			{
				UnityEngine.Debug.LogError(string.Concat(new string[]
				{
					"Received OnSerialization for view ID ",
					viewID.ToString(),
					" with prefix ",
					correctPrefix.ToString(),
					". Our prefix is ",
					photonView.Prefix.ToString()
				}));
				return;
			}
			if (photonView.Group != 0 && !PhotonNetwork.allowedReceivingGroups.Contains(photonView.Group))
			{
				return;
			}
			if (photonView.Synchronization == ViewSynchronization.ReliableDeltaCompressed)
			{
				object[] array = PhotonNetwork.DeltaCompressionRead(photonView.lastOnSerializeDataReceived, data);
				if (array == null)
				{
					if (PhotonNetwork.LogLevel >= PunLogLevel.Informational)
					{
						UnityEngine.Debug.Log(string.Concat(new string[]
						{
							"Skipping packet for ",
							photonView.name,
							" [",
							photonView.ViewID.ToString(),
							"] as we haven't received a full packet for delta compression yet. This is OK if it happens for the first few frames after joining a game."
						}));
					}
					return;
				}
				photonView.lastOnSerializeDataReceived = array;
				data = array;
			}
			PhotonNetwork.serializeStreamIn.SetReadStream(data, 3);
			PhotonMessageInfo info = new PhotonMessageInfo(sender, networkTime, photonView);
			photonView.DeserializeView(PhotonNetwork.serializeStreamIn, info);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00006D8C File Offset: 0x00004F8C
		private static List<object> DeltaCompressionWrite(List<object> previousContent, List<object> currentContent)
		{
			if (currentContent == null || previousContent == null || previousContent.Count != currentContent.Count)
			{
				return currentContent;
			}
			if (currentContent.Count <= 3)
			{
				return null;
			}
			previousContent[1] = false;
			int num = 0;
			Queue<int> queue = null;
			for (int i = 3; i < currentContent.Count; i++)
			{
				object obj = currentContent[i];
				object two = previousContent[i];
				if (PhotonNetwork.AlmostEquals(obj, two))
				{
					num++;
					previousContent[i] = null;
				}
				else
				{
					previousContent[i] = obj;
					if (obj == null)
					{
						if (queue == null)
						{
							queue = new Queue<int>(currentContent.Count);
						}
						queue.Enqueue(i);
					}
				}
			}
			if (num > 0)
			{
				if (num == currentContent.Count - 3)
				{
					return null;
				}
				previousContent[1] = true;
				if (queue != null)
				{
					previousContent[2] = queue.ToArray();
				}
			}
			previousContent[0] = currentContent[0];
			return previousContent;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00006E6C File Offset: 0x0000506C
		private static object[] DeltaCompressionRead(object[] lastOnSerializeDataReceived, object[] incomingData)
		{
			if (!(bool)incomingData[1])
			{
				return incomingData;
			}
			if (lastOnSerializeDataReceived == null)
			{
				return null;
			}
			int[] array = incomingData[2] as int[];
			for (int i = 3; i < incomingData.Length; i++)
			{
				if ((array == null || !array.Contains(i)) && incomingData[i] == null)
				{
					object obj = lastOnSerializeDataReceived[i];
					incomingData[i] = obj;
				}
			}
			return incomingData;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00006EBC File Offset: 0x000050BC
		private static bool AlmostEquals(IList<object> lastData, IList<object> currentContent)
		{
			if (lastData == null && currentContent == null)
			{
				return true;
			}
			if (lastData == null || currentContent == null || lastData.Count != currentContent.Count)
			{
				return false;
			}
			for (int i = 0; i < currentContent.Count; i++)
			{
				object one = currentContent[i];
				object two = lastData[i];
				if (!PhotonNetwork.AlmostEquals(one, two))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00006F14 File Offset: 0x00005114
		private static bool AlmostEquals(object one, object two)
		{
			if (one == null || two == null)
			{
				return one == null && two == null;
			}
			if (!one.Equals(two))
			{
				if (one is Vector3)
				{
					Vector3 target = (Vector3)one;
					Vector3 second = (Vector3)two;
					if (target.AlmostEquals(second, PhotonNetwork.PrecisionForVectorSynchronization))
					{
						return true;
					}
				}
				else if (one is Vector2)
				{
					Vector2 target2 = (Vector2)one;
					Vector2 second2 = (Vector2)two;
					if (target2.AlmostEquals(second2, PhotonNetwork.PrecisionForVectorSynchronization))
					{
						return true;
					}
				}
				else if (one is Quaternion)
				{
					Quaternion target3 = (Quaternion)one;
					Quaternion second3 = (Quaternion)two;
					if (target3.AlmostEquals(second3, PhotonNetwork.PrecisionForQuaternionSynchronization))
					{
						return true;
					}
				}
				else if (one is float)
				{
					float target4 = (float)one;
					float second4 = (float)two;
					if (target4.AlmostEquals(second4, PhotonNetwork.PrecisionForFloatSynchronization))
					{
						return true;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00006FD0 File Offset: 0x000051D0
		internal static bool GetMethod(MonoBehaviour monob, string methodType, out MethodInfo mi)
		{
			mi = null;
			if (monob == null || string.IsNullOrEmpty(methodType))
			{
				return false;
			}
			List<MethodInfo> methods = SupportClass.GetMethods(monob.GetType(), null);
			for (int i = 0; i < methods.Count; i++)
			{
				MethodInfo methodInfo = methods[i];
				if (methodInfo.Name.Equals(methodType))
				{
					mi = methodInfo;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00007030 File Offset: 0x00005230
		internal static void LoadLevelIfSynced()
		{
			if (!PhotonNetwork.AutomaticallySyncScene || PhotonNetwork.IsMasterClient || PhotonNetwork.CurrentRoom == null)
			{
				return;
			}
			if (!PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("curScn"))
			{
				return;
			}
			object obj = PhotonNetwork.CurrentRoom.CustomProperties["curScn"];
			if (obj is int)
			{
				if (SceneManagerHelper.ActiveSceneBuildIndex != (int)obj)
				{
					PhotonNetwork.LoadLevel((int)obj);
					return;
				}
			}
			else if (obj is string && SceneManagerHelper.ActiveSceneName != (string)obj)
			{
				PhotonNetwork.LoadLevel((string)obj);
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000070C8 File Offset: 0x000052C8
		internal static void SetLevelInPropsIfSynced(object levelId)
		{
			if (!PhotonNetwork.AutomaticallySyncScene || !PhotonNetwork.IsMasterClient || PhotonNetwork.CurrentRoom == null)
			{
				return;
			}
			if (levelId == null)
			{
				UnityEngine.Debug.LogError("Parameter levelId can't be null!");
				return;
			}
			if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("curScn"))
			{
				object obj = PhotonNetwork.CurrentRoom.CustomProperties["curScn"];
				if (levelId.Equals(obj))
				{
					return;
				}
				int activeSceneBuildIndex = SceneManagerHelper.ActiveSceneBuildIndex;
				string activeSceneName = SceneManagerHelper.ActiveSceneName;
				if ((levelId.Equals(activeSceneBuildIndex) && obj.Equals(activeSceneName)) || (levelId.Equals(activeSceneName) && obj.Equals(activeSceneBuildIndex)))
				{
					return;
				}
			}
			if (PhotonNetwork._AsyncLevelLoadingOperation != null)
			{
				if (!PhotonNetwork._AsyncLevelLoadingOperation.isDone)
				{
					UnityEngine.Debug.LogWarning("PUN cancels an ongoing async level load, as another scene should be loaded. Next scene to load: " + ((levelId != null) ? levelId.ToString() : null));
				}
				PhotonNetwork._AsyncLevelLoadingOperation.allowSceneActivation = false;
				PhotonNetwork._AsyncLevelLoadingOperation = null;
			}
			ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
			if (levelId is int)
			{
				hashtable["curScn"] = (int)levelId;
			}
			else if (levelId is string)
			{
				hashtable["curScn"] = (string)levelId;
			}
			else
			{
				UnityEngine.Debug.LogError("Parameter levelId must be int or string!");
			}
			PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable, null, null);
			PhotonNetwork.SendAllOutgoingCommands();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00007208 File Offset: 0x00005408
		private static void OnEvent(EventData photonEvent)
		{
			int sender = photonEvent.Sender;
			Player player = null;
			if (sender > 0 && PhotonNetwork.NetworkingClient.CurrentRoom != null)
			{
				player = PhotonNetwork.NetworkingClient.CurrentRoom.GetPlayer(sender, false);
			}
			byte code = photonEvent.Code;
			switch (code)
			{
			case 200:
				PhotonNetwork.ExecuteRpc(photonEvent.CustomData as ExitGames.Client.Photon.Hashtable, player);
				return;
			case 201:
			case 206:
			{
				object[] array = (object[])photonEvent[245];
				int networkTime = (int)array[0];
				short correctPrefix = (short)((array[1] != null) ? ((byte)array[1]) : 0);
				for (int i = 2; i < array.Length; i++)
				{
					object[] array2 = array[i] as object[];
					if (array2 == null)
					{
						return;
					}
					PhotonNetwork.OnSerializeRead(array2, player, networkTime, correctPrefix);
				}
				return;
			}
			case 202:
				PhotonNetwork.NetworkInstantiate((ExitGames.Client.Photon.Hashtable)photonEvent.CustomData, player);
				return;
			case 203:
				if (!PhotonNetwork.EnableCloseConnection)
				{
					string str = "CloseConnection received from ";
					Player player2 = player;
					UnityEngine.Debug.LogWarning(str + ((player2 != null) ? player2.ToString() : null) + ". PhotonNetwork.EnableCloseConnection is false. Ignoring the request (this client stays in the room).");
					return;
				}
				if (player == null || !player.IsMasterClient)
				{
					string[] array3 = new string[5];
					array3[0] = "CloseConnection received from ";
					int num = 1;
					Player player3 = player;
					array3[num] = ((player3 != null) ? player3.ToString() : null);
					array3[2] = ". That player is not the Master Client. ";
					int num2 = 3;
					Player masterClient = PhotonNetwork.MasterClient;
					array3[num2] = ((masterClient != null) ? masterClient.ToString() : null);
					array3[4] = " is.";
					UnityEngine.Debug.LogWarning(string.Concat(array3));
					return;
				}
				if (PhotonNetwork.EnableCloseConnection)
				{
					PhotonNetwork.LeaveRoom(false);
					return;
				}
				break;
			case 204:
			{
				ExitGames.Client.Photon.Hashtable hashtable = (ExitGames.Client.Photon.Hashtable)photonEvent.CustomData;
				int key = (int)hashtable[PhotonNetwork.keyByteZero];
				PhotonView photonView = null;
				if (PhotonNetwork.photonViewList.TryGetValue(key, out photonView))
				{
					PhotonNetwork.RemoveInstantiatedGO(photonView.gameObject, true);
					return;
				}
				UnityEngine.Debug.LogError("Ev Destroy Failed. Could not find PhotonView with instantiationId " + key.ToString() + ". Sent by actorNr: " + sender.ToString());
				return;
			}
			case 205:
			case 208:
			case 211:
				break;
			case 207:
			{
				ExitGames.Client.Photon.Hashtable hashtable = photonEvent.CustomData as ExitGames.Client.Photon.Hashtable;
				if (hashtable != null)
				{
					int num3 = (int)hashtable[PhotonNetwork.keyByteZero];
					if (num3 >= 0)
					{
						PhotonNetwork.DestroyPlayerObjects(num3, true);
						return;
					}
					PhotonNetwork.DestroyAll(true);
					return;
				}
				break;
			}
			case 209:
			{
				int[] array4 = (int[])photonEvent.CustomData;
				int num4 = array4[0];
				int num5 = array4[1];
				PhotonView photonView2 = PhotonNetwork.GetPhotonView(num4);
				if (photonView2 == null)
				{
					UnityEngine.Debug.LogWarning("Can't find PhotonView of incoming OwnershipRequest. ViewId not found: " + num4.ToString());
					return;
				}
				if (PhotonNetwork.LogLevel == PunLogLevel.Informational)
				{
					UnityEngine.Debug.Log(string.Format("OwnershipRequest. actorNr {0} requests view {1} from {2}. current pv owner: {3} is {4}. isMine: {6} master client: {5}", new object[]
					{
						sender,
						num4,
						num5,
						photonView2.OwnerActorNr,
						photonView2.IsOwnerActive ? "active" : "inactive",
						PhotonNetwork.MasterClient.ActorNumber,
						photonView2.IsMine
					}));
				}
				OwnershipOption ownershipTransfer = photonView2.OwnershipTransfer;
				if (ownershipTransfer != OwnershipOption.Takeover)
				{
					if (ownershipTransfer != OwnershipOption.Request)
					{
						UnityEngine.Debug.LogWarning("Ownership mode == " + photonView2.OwnershipTransfer.ToString() + ". Ignoring request.");
						return;
					}
					if (PhotonNetwork.OnOwnershipRequestEv != null)
					{
						PhotonNetwork.OnOwnershipRequestEv(photonView2, player);
						return;
					}
				}
				else
				{
					int ownerActorNr = photonView2.OwnerActorNr;
					if (num5 == ownerActorNr || (num5 == 0 && ownerActorNr == PhotonNetwork.MasterClient.ActorNumber) || ownerActorNr == 0)
					{
						Player owner = photonView2.Owner;
						photonView2.OwnerActorNr = sender;
						photonView2.ControllerActorNr = sender;
						if (PhotonNetwork.OnOwnershipTransferedEv != null)
						{
							PhotonNetwork.OnOwnershipTransferedEv(photonView2, owner);
							return;
						}
					}
					else if (PhotonNetwork.OnOwnershipTransferFailedEv != null)
					{
						PhotonNetwork.OnOwnershipTransferFailedEv(photonView2, player);
						return;
					}
				}
				break;
			}
			case 210:
			{
				int[] array5 = (int[])photonEvent.CustomData;
				int num6 = array5[0];
				int num7 = array5[1];
				if (PhotonNetwork.LogLevel >= PunLogLevel.Informational)
				{
					UnityEngine.Debug.Log(string.Concat(new string[]
					{
						"Ev OwnershipTransfer. ViewID ",
						num6.ToString(),
						" to: ",
						num7.ToString(),
						" Time: ",
						(Environment.TickCount % 1000).ToString()
					}));
				}
				PhotonView photonView3 = PhotonNetwork.GetPhotonView(num6);
				if (photonView3 != null)
				{
					if (photonView3.OwnershipTransfer == OwnershipOption.Takeover || (photonView3.OwnershipTransfer == OwnershipOption.Request && (player == photonView3.Controller || player == photonView3.Owner)))
					{
						Player owner2 = photonView3.Owner;
						photonView3.OwnerActorNr = num7;
						photonView3.ControllerActorNr = num7;
						if (PhotonNetwork.OnOwnershipTransferedEv != null)
						{
							PhotonNetwork.OnOwnershipTransferedEv(photonView3, owner2);
							return;
						}
					}
					else if (PhotonNetwork.LogLevel >= PunLogLevel.Informational)
					{
						if (photonView3.OwnershipTransfer == OwnershipOption.Request)
						{
							UnityEngine.Debug.Log(string.Concat(new string[]
							{
								"Failed incoming OwnershipTransfer attempt for '",
								photonView3.name,
								"; ",
								num6.ToString(),
								" - photonView has OwnershipTransfer set to OwnershipOption.Request, but Player attempting to change owner is not the current owner/controller."
							}));
							return;
						}
						UnityEngine.Debug.Log(string.Concat(new string[]
						{
							"Failed incoming OwnershipTransfer attempt for '",
							photonView3.name,
							"; ",
							num6.ToString(),
							" - photonView has OwnershipTransfer set to OwnershipOption.Fixed."
						}));
						return;
					}
				}
				else if (PhotonNetwork.LogLevel >= PunLogLevel.ErrorsOnly)
				{
					UnityEngine.Debug.LogErrorFormat("Failed to find a PhotonView with ID={0} for incoming OwnershipTransfer event (newOwnerActorNumber={1}), sender={2}", new object[]
					{
						num6,
						num7,
						sender
					});
					return;
				}
				break;
			}
			case 212:
			{
				PhotonNetwork.reusablePVHashset.Clear();
				int[] array6 = (int[])photonEvent.CustomData;
				int j = 0;
				int num8 = array6.Length;
				while (j < num8)
				{
					int num9 = array6[j];
					j++;
					int num10 = array6[j];
					PhotonView photonView4 = PhotonNetwork.GetPhotonView(num9);
					if (photonView4 == null)
					{
						if (PhotonNetwork.LogLevel >= PunLogLevel.ErrorsOnly)
						{
							UnityEngine.Debug.LogErrorFormat("Failed to find a PhotonView with ID={0} for incoming OwnershipUpdate event (newOwnerActorNumber={1}), sender={2}. If you load scenes, make sure to pause the message queue.", new object[]
							{
								num9,
								num10,
								sender
							});
						}
					}
					else
					{
						Player owner3 = photonView4.Owner;
						Player player4 = PhotonNetwork.CurrentRoom.GetPlayer(num10, true);
						photonView4.OwnerActorNr = num10;
						photonView4.ControllerActorNr = num10;
						PhotonNetwork.reusablePVHashset.Add(photonView4);
						if (PhotonNetwork.OnOwnershipTransferedEv != null && player4 != owner3)
						{
							PhotonNetwork.OnOwnershipTransferedEv(photonView4, owner3);
						}
					}
					j++;
				}
				foreach (PhotonView photonView5 in PhotonNetwork.PhotonViewCollection)
				{
					if (!PhotonNetwork.reusablePVHashset.Contains(photonView5))
					{
						photonView5.RebuildControllerCache(false);
					}
				}
				break;
			}
			default:
				if (code != 254)
				{
					if (code == 255)
					{
						PhotonNetwork.ResetPhotonViewsOnSerialize();
						return;
					}
				}
				else if (PhotonNetwork.CurrentRoom != null && PhotonNetwork.CurrentRoom.AutoCleanUp && (player == null || !player.IsInactive))
				{
					PhotonNetwork.DestroyPlayerObjects(sender, true);
					return;
				}
				break;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00007900 File Offset: 0x00005B00
		private static void OnOperation(OperationResponse opResponse)
		{
			byte operationCode = opResponse.OperationCode;
			if (operationCode != 220)
			{
				if (operationCode != 226)
				{
					return;
				}
				if (PhotonNetwork.Server == ServerConnection.GameServer)
				{
					PhotonNetwork.LoadLevelIfSynced();
				}
			}
			else
			{
				if (opResponse.ReturnCode != 0)
				{
					if (PhotonNetwork.LogLevel >= PunLogLevel.Full)
					{
						UnityEngine.Debug.Log("OpGetRegions failed. Will not ping any. ReturnCode: " + opResponse.ReturnCode.ToString());
					}
					return;
				}
				if (PhotonNetwork.ConnectMethod == ConnectMethod.ConnectToBest)
				{
					string bestRegionSummaryInPreferences = PhotonNetwork.BestRegionSummaryInPreferences;
					if (PhotonNetwork.LogLevel >= PunLogLevel.Informational)
					{
						UnityEngine.Debug.Log("PUN got region list. Going to ping minimum regions, based on this previous result summary: " + bestRegionSummaryInPreferences);
					}
					PhotonNetwork.NetworkingClient.RegionHandler.PingMinimumOfRegions(new Action<RegionHandler>(PhotonNetwork.OnRegionsPinged), bestRegionSummaryInPreferences);
					return;
				}
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000079A4 File Offset: 0x00005BA4
		private static void OnClientStateChanged(ClientState previousState, ClientState state)
		{
			if ((previousState == ClientState.Joined && state == ClientState.Disconnected) || (PhotonNetwork.Server == ServerConnection.GameServer && (state == ClientState.Disconnecting || state == ClientState.DisconnectingFromGameServer)))
			{
				PhotonNetwork.LeftRoomCleanup();
			}
			if (state == ClientState.ConnectedToMasterServer && PhotonNetwork._cachedRegionHandler != null)
			{
				PhotonNetwork.BestRegionSummaryInPreferences = PhotonNetwork._cachedRegionHandler.SummaryToCache;
				PhotonNetwork._cachedRegionHandler = null;
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000079F4 File Offset: 0x00005BF4
		private static void OnRegionsPinged(RegionHandler regionHandler)
		{
			if (PhotonNetwork.LogLevel >= PunLogLevel.Informational)
			{
				UnityEngine.Debug.Log(regionHandler.GetResults());
			}
			PhotonNetwork._cachedRegionHandler = regionHandler;
			if (PhotonNetwork.NetworkClientState == ClientState.ConnectedToNameServer && !PhotonNetwork.NetworkingClient.ConnectToRegionMaster(regionHandler.BestRegion.Code))
			{
				UnityEngine.Debug.LogError("PUN could not ConnectToRegionMaster successfully. Please check error messages.");
			}
		}

		// Token: 0x0400002F RID: 47
		public const string PunVersion = "2.50";

		// Token: 0x04000030 RID: 48
		private static string gameVersion;

		// Token: 0x04000031 RID: 49
		public static LoadBalancingClient NetworkingClient;

		// Token: 0x04000032 RID: 50
		public static readonly int MAX_VIEW_IDS = 1000;

		// Token: 0x04000033 RID: 51
		public const string ServerSettingsFileName = "PhotonServerSettings";

		// Token: 0x04000034 RID: 52
		private static ServerSettings photonServerSettings;

		// Token: 0x04000035 RID: 53
		private const string PlayerPrefsKey = "PUNCloudBestRegion";

		// Token: 0x04000036 RID: 54
		public static ConnectMethod ConnectMethod = ConnectMethod.NotCalled;

		// Token: 0x04000037 RID: 55
		public static PunLogLevel LogLevel = PunLogLevel.ErrorsOnly;

		// Token: 0x04000038 RID: 56
		public static bool EnableCloseConnection = false;

		// Token: 0x04000039 RID: 57
		public static float PrecisionForVectorSynchronization = 9.9E-05f;

		// Token: 0x0400003A RID: 58
		public static float PrecisionForQuaternionSynchronization = 1f;

		// Token: 0x0400003B RID: 59
		public static float PrecisionForFloatSynchronization = 0.01f;

		// Token: 0x0400003C RID: 60
		private static bool offlineMode = false;

		// Token: 0x0400003D RID: 61
		private static Room offlineModeRoom = null;

		// Token: 0x0400003E RID: 62
		private static bool automaticallySyncScene = false;

		// Token: 0x0400003F RID: 63
		private static int sendFrequency = 33;

		// Token: 0x04000040 RID: 64
		private static int serializationFrequency = 100;

		// Token: 0x04000041 RID: 65
		private static bool isMessageQueueRunning = true;

		// Token: 0x04000042 RID: 66
		private static double frametime;

		// Token: 0x04000043 RID: 67
		private static int frame;

		// Token: 0x04000044 RID: 68
		private static Stopwatch StartupStopwatch;

		// Token: 0x04000045 RID: 69
		public static float MinimalTimeScaleToDispatchInFixedUpdate = -1f;

		// Token: 0x04000046 RID: 70
		[CompilerGenerated]
		private static bool <UseAlternativeUdpPorts>k__BackingField;

		// Token: 0x04000047 RID: 71
		private static int lastUsedViewSubId = 0;

		// Token: 0x04000048 RID: 72
		private static int lastUsedViewSubIdStatic = 0;

		// Token: 0x04000049 RID: 73
		private static readonly HashSet<string> PrefabsWithoutMagicCallback = new HashSet<string>();

		// Token: 0x0400004A RID: 74
		private static readonly ExitGames.Client.Photon.Hashtable SendInstantiateEvHashtable = new ExitGames.Client.Photon.Hashtable();

		// Token: 0x0400004B RID: 75
		private static readonly RaiseEventOptions SendInstantiateRaiseEventOptions = new RaiseEventOptions();

		// Token: 0x0400004C RID: 76
		private static HashSet<byte> allowedReceivingGroups = new HashSet<byte>();

		// Token: 0x0400004D RID: 77
		private static HashSet<byte> blockedSendingGroups = new HashSet<byte>();

		// Token: 0x0400004E RID: 78
		private static HashSet<PhotonView> reusablePVHashset = new HashSet<PhotonView>();

		// Token: 0x0400004F RID: 79
		private static NonAllocDictionary<int, PhotonView> photonViewList = new NonAllocDictionary<int, PhotonView>(29U);

		// Token: 0x04000050 RID: 80
		[CompilerGenerated]
		private static Action<PhotonView, Player> OnOwnershipRequestEv;

		// Token: 0x04000051 RID: 81
		[CompilerGenerated]
		private static Action<PhotonView, Player> OnOwnershipTransferedEv;

		// Token: 0x04000052 RID: 82
		[CompilerGenerated]
		private static Action<PhotonView, Player> OnOwnershipTransferFailedEv;

		// Token: 0x04000053 RID: 83
		internal static byte currentLevelPrefix = 0;

		// Token: 0x04000054 RID: 84
		internal static bool loadingLevelAndPausedNetwork = false;

		// Token: 0x04000055 RID: 85
		internal const string CurrentSceneProperty = "curScn";

		// Token: 0x04000056 RID: 86
		internal const string CurrentScenePropertyLoadAsync = "curScnLa";

		// Token: 0x04000057 RID: 87
		private static IPunPrefabPool prefabPool;

		// Token: 0x04000058 RID: 88
		public static bool UseRpcMonoBehaviourCache;

		// Token: 0x04000059 RID: 89
		private static readonly Dictionary<Type, List<MethodInfo>> monoRPCMethodsCache = new Dictionary<Type, List<MethodInfo>>();

		// Token: 0x0400005A RID: 90
		private static Dictionary<string, int> rpcShortcuts;

		// Token: 0x0400005B RID: 91
		public static bool RunRpcCoroutines = true;

		// Token: 0x0400005C RID: 92
		private static AsyncOperation _AsyncLevelLoadingOperation;

		// Token: 0x0400005D RID: 93
		private static float _levelLoadingProgress = 0f;

		// Token: 0x0400005E RID: 94
		private static readonly Type typePunRPC = typeof(PunRPC);

		// Token: 0x0400005F RID: 95
		private static readonly Type typePhotonMessageInfo = typeof(PhotonMessageInfo);

		// Token: 0x04000060 RID: 96
		private static readonly object keyByteZero = 0;

		// Token: 0x04000061 RID: 97
		private static readonly object keyByteOne = 1;

		// Token: 0x04000062 RID: 98
		private static readonly object keyByteTwo = 2;

		// Token: 0x04000063 RID: 99
		private static readonly object keyByteThree = 3;

		// Token: 0x04000064 RID: 100
		private static readonly object keyByteFour = 4;

		// Token: 0x04000065 RID: 101
		private static readonly object keyByteFive = 5;

		// Token: 0x04000066 RID: 102
		private static readonly object keyByteSix = 6;

		// Token: 0x04000067 RID: 103
		private static readonly object keyByteSeven = 7;

		// Token: 0x04000068 RID: 104
		private static readonly object keyByteEight = 8;

		// Token: 0x04000069 RID: 105
		private static readonly object[] emptyObjectArray = new object[0];

		// Token: 0x0400006A RID: 106
		private static readonly Type[] emptyTypeArray = new Type[0];

		// Token: 0x0400006B RID: 107
		internal static List<PhotonView> foundPVs = new List<PhotonView>();

		// Token: 0x0400006C RID: 108
		private static readonly ExitGames.Client.Photon.Hashtable removeFilter = new ExitGames.Client.Photon.Hashtable();

		// Token: 0x0400006D RID: 109
		private static readonly ExitGames.Client.Photon.Hashtable ServerCleanDestroyEvent = new ExitGames.Client.Photon.Hashtable();

		// Token: 0x0400006E RID: 110
		private static readonly RaiseEventOptions ServerCleanOptions = new RaiseEventOptions
		{
			CachingOption = EventCaching.RemoveFromRoomCache
		};

		// Token: 0x0400006F RID: 111
		internal static RaiseEventOptions SendToAllOptions = new RaiseEventOptions
		{
			Receivers = ReceiverGroup.All
		};

		// Token: 0x04000070 RID: 112
		internal static RaiseEventOptions SendToOthersOptions = new RaiseEventOptions
		{
			Receivers = ReceiverGroup.Others
		};

		// Token: 0x04000071 RID: 113
		internal static RaiseEventOptions SendToSingleOptions = new RaiseEventOptions
		{
			TargetActors = new int[1]
		};

		// Token: 0x04000072 RID: 114
		private static readonly ExitGames.Client.Photon.Hashtable rpcFilterByViewId = new ExitGames.Client.Photon.Hashtable();

		// Token: 0x04000073 RID: 115
		private static readonly RaiseEventOptions OpCleanRpcBufferOptions = new RaiseEventOptions
		{
			CachingOption = EventCaching.RemoveFromRoomCache
		};

		// Token: 0x04000074 RID: 116
		private static ExitGames.Client.Photon.Hashtable rpcEvent = new ExitGames.Client.Photon.Hashtable();

		// Token: 0x04000075 RID: 117
		private static RaiseEventOptions RpcOptionsToAll = new RaiseEventOptions();

		// Token: 0x04000076 RID: 118
		public static int ObjectsInOneUpdate = 20;

		// Token: 0x04000077 RID: 119
		private static readonly PhotonStream serializeStreamOut = new PhotonStream(true, null);

		// Token: 0x04000078 RID: 120
		private static readonly PhotonStream serializeStreamIn = new PhotonStream(false, null);

		// Token: 0x04000079 RID: 121
		private static RaiseEventOptions serializeRaiseEvOptions = new RaiseEventOptions();

		// Token: 0x0400007A RID: 122
		private static readonly Dictionary<PhotonNetwork.RaiseEventBatch, PhotonNetwork.SerializeViewBatch> serializeViewBatches = new Dictionary<PhotonNetwork.RaiseEventBatch, PhotonNetwork.SerializeViewBatch>();

		// Token: 0x0400007B RID: 123
		public const int SyncViewId = 0;

		// Token: 0x0400007C RID: 124
		public const int SyncCompressed = 1;

		// Token: 0x0400007D RID: 125
		public const int SyncNullValues = 2;

		// Token: 0x0400007E RID: 126
		public const int SyncFirstValue = 3;

		// Token: 0x0400007F RID: 127
		private static RegionHandler _cachedRegionHandler;

		// Token: 0x0200002C RID: 44
		private struct RaiseEventBatch : IEquatable<PhotonNetwork.RaiseEventBatch>
		{
			// Token: 0x060001BC RID: 444 RVA: 0x0000B371 File Offset: 0x00009571
			public override int GetHashCode()
			{
				return ((int)this.Group << 1) + (this.Reliable ? 1 : 0);
			}

			// Token: 0x060001BD RID: 445 RVA: 0x0000B388 File Offset: 0x00009588
			public bool Equals(PhotonNetwork.RaiseEventBatch other)
			{
				return this.Reliable == other.Reliable && this.Group == other.Group;
			}

			// Token: 0x04000115 RID: 277
			public byte Group;

			// Token: 0x04000116 RID: 278
			public bool Reliable;
		}

		// Token: 0x0200002D RID: 45
		private class SerializeViewBatch : IEquatable<PhotonNetwork.SerializeViewBatch>, IEquatable<PhotonNetwork.RaiseEventBatch>
		{
			// Token: 0x060001BE RID: 446 RVA: 0x0000B3A8 File Offset: 0x000095A8
			public SerializeViewBatch(PhotonNetwork.RaiseEventBatch batch, int offset)
			{
				this.Batch = batch;
				this.ObjectUpdates = new List<object>(this.defaultSize);
				this.offset = offset;
				for (int i = 0; i < offset; i++)
				{
					this.ObjectUpdates.Add(null);
				}
			}

			// Token: 0x060001BF RID: 447 RVA: 0x0000B3FD File Offset: 0x000095FD
			public override int GetHashCode()
			{
				return ((int)this.Batch.Group << 1) + (this.Batch.Reliable ? 1 : 0);
			}

			// Token: 0x060001C0 RID: 448 RVA: 0x0000B41E File Offset: 0x0000961E
			public bool Equals(PhotonNetwork.SerializeViewBatch other)
			{
				return this.Equals(other.Batch);
			}

			// Token: 0x060001C1 RID: 449 RVA: 0x0000B42C File Offset: 0x0000962C
			public bool Equals(PhotonNetwork.RaiseEventBatch other)
			{
				return this.Batch.Reliable == other.Reliable && this.Batch.Group == other.Group;
			}

			// Token: 0x060001C2 RID: 450 RVA: 0x0000B458 File Offset: 0x00009658
			public override bool Equals(object obj)
			{
				PhotonNetwork.SerializeViewBatch serializeViewBatch = obj as PhotonNetwork.SerializeViewBatch;
				return serializeViewBatch != null && this.Batch.Equals(serializeViewBatch.Batch);
			}

			// Token: 0x060001C3 RID: 451 RVA: 0x0000B488 File Offset: 0x00009688
			public void Clear()
			{
				this.ObjectUpdates.Clear();
				for (int i = 0; i < this.offset; i++)
				{
					this.ObjectUpdates.Add(null);
				}
			}

			// Token: 0x060001C4 RID: 452 RVA: 0x0000B4BD File Offset: 0x000096BD
			public void Add(List<object> viewData)
			{
				if (this.ObjectUpdates.Count >= this.ObjectUpdates.Capacity)
				{
					throw new Exception("Can't add. Size exceeded.");
				}
				this.ObjectUpdates.Add(viewData);
			}

			// Token: 0x04000117 RID: 279
			public readonly PhotonNetwork.RaiseEventBatch Batch;

			// Token: 0x04000118 RID: 280
			public List<object> ObjectUpdates;

			// Token: 0x04000119 RID: 281
			private int defaultSize = PhotonNetwork.ObjectsInOneUpdate;

			// Token: 0x0400011A RID: 282
			private int offset;
		}

		// Token: 0x0200002E RID: 46
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060001C5 RID: 453 RVA: 0x0000B4EE File Offset: 0x000096EE
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060001C6 RID: 454 RVA: 0x0000B4FA File Offset: 0x000096FA
			public <>c()
			{
			}

			// Token: 0x060001C7 RID: 455 RVA: 0x0000B502 File Offset: 0x00009702
			internal int <get_PlayerList>b__47_0(Player x)
			{
				return x.ActorNumber;
			}

			// Token: 0x060001C8 RID: 456 RVA: 0x0000B50A File Offset: 0x0000970A
			internal int <get_PlayerListOthers>b__49_0(Player x)
			{
				return x.ActorNumber;
			}

			// Token: 0x060001C9 RID: 457 RVA: 0x0000B512 File Offset: 0x00009712
			internal bool <get_PlayerListOthers>b__49_1(Player x)
			{
				return !x.IsLocal;
			}

			// Token: 0x060001CA RID: 458 RVA: 0x0000B51D File Offset: 0x0000971D
			internal string <CallbacksToString>b__216_0(IConnectionCallbacks m)
			{
				return m.ToString();
			}

			// Token: 0x0400011B RID: 283
			public static readonly PhotonNetwork.<>c <>9 = new PhotonNetwork.<>c();

			// Token: 0x0400011C RID: 284
			public static Func<Player, int> <>9__47_0;

			// Token: 0x0400011D RID: 285
			public static Func<Player, int> <>9__49_0;

			// Token: 0x0400011E RID: 286
			public static Func<Player, bool> <>9__49_1;

			// Token: 0x0400011F RID: 287
			public static Func<IConnectionCallbacks, string> <>9__216_0;
		}
	}
}
