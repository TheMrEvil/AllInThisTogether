using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using ExitGames.Client.Photon;
using UnityEngine;

namespace Photon.Realtime
{
	// Token: 0x0200003E RID: 62
	[DisallowMultipleComponent]
	[AddComponentMenu("")]
	public class SupportLogger : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks, IInRoomCallbacks, ILobbyCallbacks, IErrorInfoCallback
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000A6B5 File Offset: 0x000088B5
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000A6BD File Offset: 0x000088BD
		public LoadBalancingClient Client
		{
			get
			{
				return this.client;
			}
			set
			{
				if (this.client != value)
				{
					if (this.client != null)
					{
						this.client.RemoveCallbackTarget(this);
					}
					this.client = value;
					if (this.client != null)
					{
						this.client.AddCallbackTarget(this);
					}
				}
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000A6F7 File Offset: 0x000088F7
		protected void Start()
		{
			this.LogBasics();
			if (this.startStopwatch == null)
			{
				this.startStopwatch = new Stopwatch();
				this.startStopwatch.Start();
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000A71D File Offset: 0x0000891D
		protected void OnDestroy()
		{
			this.Client = null;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000A728 File Offset: 0x00008928
		protected void OnApplicationPause(bool pause)
		{
			if (!this.initialOnApplicationPauseSkipped)
			{
				this.initialOnApplicationPauseSkipped = true;
				return;
			}
			UnityEngine.Debug.Log(string.Format("{0} SupportLogger OnApplicationPause({1}). Client: {2}.", this.GetFormattedTimestamp(), pause, (this.client == null) ? "null" : this.client.State.ToString()));
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000A788 File Offset: 0x00008988
		protected void OnApplicationQuit()
		{
			base.CancelInvoke();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000A790 File Offset: 0x00008990
		public void StartLogStats()
		{
			base.InvokeRepeating("LogStats", 10f, 10f);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000A7A7 File Offset: 0x000089A7
		public void StopLogStats()
		{
			base.CancelInvoke("LogStats");
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000A7B4 File Offset: 0x000089B4
		private void StartTrackValues()
		{
			base.InvokeRepeating("TrackValues", 0.5f, 0.5f);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000A7CB File Offset: 0x000089CB
		private void StopTrackValues()
		{
			base.CancelInvoke("TrackValues");
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000A7D8 File Offset: 0x000089D8
		private string GetFormattedTimestamp()
		{
			if (this.startStopwatch == null)
			{
				this.startStopwatch = new Stopwatch();
				this.startStopwatch.Start();
			}
			TimeSpan elapsed = this.startStopwatch.Elapsed;
			if (elapsed.Minutes > 0)
			{
				return string.Format("[{0}:{1}.{1}]", elapsed.Minutes, elapsed.Seconds, elapsed.Milliseconds);
			}
			return string.Format("[{0}.{1}]", elapsed.Seconds, elapsed.Milliseconds);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000A86C File Offset: 0x00008A6C
		private void TrackValues()
		{
			if (this.client != null)
			{
				int roundTripTime = this.client.LoadBalancingPeer.RoundTripTime;
				if (roundTripTime > this.pingMax)
				{
					this.pingMax = roundTripTime;
				}
				if (roundTripTime < this.pingMin)
				{
					this.pingMin = roundTripTime;
				}
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000A8B4 File Offset: 0x00008AB4
		public void LogStats()
		{
			if (this.client == null || this.client.State == ClientState.PeerCreated)
			{
				return;
			}
			if (this.LogTrafficStats)
			{
				UnityEngine.Debug.Log(string.Format("{0} SupportLogger {1} Ping min/max: {2}/{3}", new object[]
				{
					this.GetFormattedTimestamp(),
					this.client.LoadBalancingPeer.VitalStatsToString(false),
					this.pingMin,
					this.pingMax
				}));
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000A930 File Offset: 0x00008B30
		private void LogBasics()
		{
			if (this.client != null)
			{
				List<string> list = new List<string>(10);
				list.Add(Application.unityVersion);
				list.Add(Application.platform.ToString());
				list.Add("ENABLE_MONO");
				list.Add("NET_4_6");
				list.Add("UNITY_64");
				StringBuilder stringBuilder = new StringBuilder();
				string text = (string.IsNullOrEmpty(this.client.AppId) || this.client.AppId.Length < 8) ? this.client.AppId : (this.client.AppId.Substring(0, 8) + "***");
				stringBuilder.AppendFormat("{0} SupportLogger Info: ", this.GetFormattedTimestamp());
				stringBuilder.AppendFormat("AppID: \"{0}\" AppVersion: \"{1}\" Client: v{2} ({4}) Build: {3} ", new object[]
				{
					text,
					this.client.AppVersion,
					PhotonPeer.Version,
					string.Join(", ", list.ToArray()),
					this.client.LoadBalancingPeer.TargetFramework
				});
				if (this.client != null && this.client.LoadBalancingPeer != null && this.client.LoadBalancingPeer.SocketImplementation != null)
				{
					stringBuilder.AppendFormat("Socket: {0} ", this.client.LoadBalancingPeer.SocketImplementation.Name);
				}
				stringBuilder.AppendFormat("UserId: \"{0}\" AuthType: {1} AuthMode: {2} {3} ", new object[]
				{
					this.client.UserId,
					(this.client.AuthValues != null) ? this.client.AuthValues.AuthType.ToString() : "N/A",
					this.client.AuthMode,
					this.client.EncryptionMode
				});
				stringBuilder.AppendFormat("State: {0} ", this.client.State);
				stringBuilder.AppendFormat("PeerID: {0} ", this.client.LoadBalancingPeer.PeerID);
				stringBuilder.AppendFormat("NameServer: {0} Current Server: {1} IP: {2} Region: {3} ", new object[]
				{
					this.client.NameServerHost,
					this.client.CurrentServerAddress,
					this.client.LoadBalancingPeer.ServerIpAddress,
					this.client.CloudRegion
				});
				stringBuilder.AppendFormat("{0} UTC", DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));
				UnityEngine.Debug.LogWarning(stringBuilder.ToString());
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000ABD0 File Offset: 0x00008DD0
		public void OnConnected()
		{
			UnityEngine.Debug.Log(this.GetFormattedTimestamp() + " SupportLogger OnConnected().");
			this.pingMax = 0;
			this.pingMin = this.client.LoadBalancingPeer.RoundTripTime;
			this.LogBasics();
			if (this.LogTrafficStats)
			{
				this.client.LoadBalancingPeer.TrafficStatsEnabled = false;
				this.client.LoadBalancingPeer.TrafficStatsEnabled = true;
				this.StartLogStats();
			}
			this.StartTrackValues();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000AC4B File Offset: 0x00008E4B
		public void OnConnectedToMaster()
		{
			UnityEngine.Debug.Log(this.GetFormattedTimestamp() + " SupportLogger OnConnectedToMaster().");
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000AC62 File Offset: 0x00008E62
		public void OnFriendListUpdate(List<FriendInfo> friendList)
		{
			UnityEngine.Debug.Log(this.GetFormattedTimestamp() + " SupportLogger OnFriendListUpdate(friendList).");
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000AC79 File Offset: 0x00008E79
		public void OnJoinedLobby()
		{
			string formattedTimestamp = this.GetFormattedTimestamp();
			string str = " SupportLogger OnJoinedLobby(";
			TypedLobby currentLobby = this.client.CurrentLobby;
			UnityEngine.Debug.Log(formattedTimestamp + str + ((currentLobby != null) ? currentLobby.ToString() : null) + ").");
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000ACAC File Offset: 0x00008EAC
		public void OnLeftLobby()
		{
			UnityEngine.Debug.Log(this.GetFormattedTimestamp() + " SupportLogger OnLeftLobby().");
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000ACC4 File Offset: 0x00008EC4
		public void OnCreateRoomFailed(short returnCode, string message)
		{
			UnityEngine.Debug.Log(string.Concat(new string[]
			{
				this.GetFormattedTimestamp(),
				" SupportLogger OnCreateRoomFailed(",
				returnCode.ToString(),
				",",
				message,
				")."
			}));
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000AD10 File Offset: 0x00008F10
		public void OnJoinedRoom()
		{
			string[] array = new string[7];
			array[0] = this.GetFormattedTimestamp();
			array[1] = " SupportLogger OnJoinedRoom(";
			int num = 2;
			Room currentRoom = this.client.CurrentRoom;
			array[num] = ((currentRoom != null) ? currentRoom.ToString() : null);
			array[3] = "). ";
			int num2 = 4;
			TypedLobby currentLobby = this.client.CurrentLobby;
			array[num2] = ((currentLobby != null) ? currentLobby.ToString() : null);
			array[5] = " GameServer:";
			array[6] = this.client.GameServerAddress;
			UnityEngine.Debug.Log(string.Concat(array));
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000AD90 File Offset: 0x00008F90
		public void OnJoinRoomFailed(short returnCode, string message)
		{
			UnityEngine.Debug.Log(string.Concat(new string[]
			{
				this.GetFormattedTimestamp(),
				" SupportLogger OnJoinRoomFailed(",
				returnCode.ToString(),
				",",
				message,
				")."
			}));
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000ADDC File Offset: 0x00008FDC
		public void OnJoinRandomFailed(short returnCode, string message)
		{
			UnityEngine.Debug.Log(string.Concat(new string[]
			{
				this.GetFormattedTimestamp(),
				" SupportLogger OnJoinRandomFailed(",
				returnCode.ToString(),
				",",
				message,
				")."
			}));
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000AE28 File Offset: 0x00009028
		public void OnCreatedRoom()
		{
			string[] array = new string[7];
			array[0] = this.GetFormattedTimestamp();
			array[1] = " SupportLogger OnCreatedRoom(";
			int num = 2;
			Room currentRoom = this.client.CurrentRoom;
			array[num] = ((currentRoom != null) ? currentRoom.ToString() : null);
			array[3] = "). ";
			int num2 = 4;
			TypedLobby currentLobby = this.client.CurrentLobby;
			array[num2] = ((currentLobby != null) ? currentLobby.ToString() : null);
			array[5] = " GameServer:";
			array[6] = this.client.GameServerAddress;
			UnityEngine.Debug.Log(string.Concat(array));
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000AEA8 File Offset: 0x000090A8
		public void OnLeftRoom()
		{
			UnityEngine.Debug.Log(this.GetFormattedTimestamp() + " SupportLogger OnLeftRoom().");
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000AEC0 File Offset: 0x000090C0
		public void OnDisconnected(DisconnectCause cause)
		{
			this.StopLogStats();
			this.StopTrackValues();
			UnityEngine.Debug.Log(this.GetFormattedTimestamp() + " SupportLogger OnDisconnected(" + cause.ToString() + ").");
			this.LogBasics();
			this.LogStats();
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000AF0C File Offset: 0x0000910C
		public void OnRegionListReceived(RegionHandler regionHandler)
		{
			UnityEngine.Debug.Log(this.GetFormattedTimestamp() + " SupportLogger OnRegionListReceived(regionHandler).");
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000AF24 File Offset: 0x00009124
		public void OnRoomListUpdate(List<RoomInfo> roomList)
		{
			UnityEngine.Debug.Log(this.GetFormattedTimestamp() + " SupportLogger OnRoomListUpdate(roomList). roomList.Count: " + roomList.Count.ToString());
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000AF54 File Offset: 0x00009154
		public void OnPlayerEnteredRoom(Player newPlayer)
		{
			UnityEngine.Debug.Log(this.GetFormattedTimestamp() + " SupportLogger OnPlayerEnteredRoom(" + ((newPlayer != null) ? newPlayer.ToString() : null) + ").");
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000AF7D File Offset: 0x0000917D
		public void OnPlayerLeftRoom(Player otherPlayer)
		{
			UnityEngine.Debug.Log(this.GetFormattedTimestamp() + " SupportLogger OnPlayerLeftRoom(" + ((otherPlayer != null) ? otherPlayer.ToString() : null) + ").");
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000AFA6 File Offset: 0x000091A6
		public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
		{
			UnityEngine.Debug.Log(this.GetFormattedTimestamp() + " SupportLogger OnRoomPropertiesUpdate(propertiesThatChanged).");
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000AFBD File Offset: 0x000091BD
		public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
		{
			UnityEngine.Debug.Log(this.GetFormattedTimestamp() + " SupportLogger OnPlayerPropertiesUpdate(targetPlayer,changedProps).");
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000AFD4 File Offset: 0x000091D4
		public void OnMasterClientSwitched(Player newMasterClient)
		{
			UnityEngine.Debug.Log(this.GetFormattedTimestamp() + " SupportLogger OnMasterClientSwitched(" + ((newMasterClient != null) ? newMasterClient.ToString() : null) + ").");
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000AFFD File Offset: 0x000091FD
		public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
		{
			UnityEngine.Debug.Log(this.GetFormattedTimestamp() + " SupportLogger OnCustomAuthenticationResponse(" + data.ToStringFull() + ").");
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000B01F File Offset: 0x0000921F
		public void OnCustomAuthenticationFailed(string debugMessage)
		{
			UnityEngine.Debug.Log(this.GetFormattedTimestamp() + " SupportLogger OnCustomAuthenticationFailed(" + debugMessage + ").");
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000B03C File Offset: 0x0000923C
		public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
		{
			UnityEngine.Debug.Log(this.GetFormattedTimestamp() + " SupportLogger OnLobbyStatisticsUpdate(lobbyStatistics).");
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000B053 File Offset: 0x00009253
		public void OnErrorInfo(ErrorInfo errorInfo)
		{
			UnityEngine.Debug.LogError(errorInfo.ToString());
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000B060 File Offset: 0x00009260
		public SupportLogger()
		{
		}

		// Token: 0x040001FF RID: 511
		public bool LogTrafficStats = true;

		// Token: 0x04000200 RID: 512
		private LoadBalancingClient client;

		// Token: 0x04000201 RID: 513
		private Stopwatch startStopwatch;

		// Token: 0x04000202 RID: 514
		private bool initialOnApplicationPauseSkipped;

		// Token: 0x04000203 RID: 515
		private int pingMax;

		// Token: 0x04000204 RID: 516
		private int pingMin;
	}
}
