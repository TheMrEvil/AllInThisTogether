using System;
using Photon.Realtime;
using UnityEngine;

namespace Photon.Pun.UtilityScripts
{
	// Token: 0x02000005 RID: 5
	public class StatesGui : MonoBehaviour
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002898 File Offset: 0x00000A98
		private void Awake()
		{
			if (StatesGui.Instance != null)
			{
				UnityEngine.Object.DestroyImmediate(base.gameObject);
				return;
			}
			if (this.DontDestroy)
			{
				StatesGui.Instance = this;
				UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			}
			if (this.EventsIn)
			{
				PhotonNetwork.NetworkingClient.LoadBalancingPeer.TrafficStatsEnabled = true;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000028EF File Offset: 0x00000AEF
		private void OnDisable()
		{
			if (this.DontDestroy && StatesGui.Instance == this)
			{
				StatesGui.Instance = null;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000290C File Offset: 0x00000B0C
		private void OnGUI()
		{
			if (PhotonNetwork.NetworkingClient == null || PhotonNetwork.NetworkingClient.LoadBalancingPeer == null || PhotonNetwork.NetworkingClient.LoadBalancingPeer.TrafficStatsIncoming == null)
			{
				return;
			}
			float x = (float)Screen.width / this.native_width;
			float y = (float)Screen.height / this.native_height;
			GUI.matrix = Matrix4x4.TRS(new Vector3(0f, 0f, 0f), Quaternion.identity, new Vector3(x, y, 1f));
			Rect rect = new Rect(this.GuiOffset);
			if (rect.x < 0f)
			{
				rect.x = (float)Screen.width - rect.width;
			}
			this.GuiRect.xMin = rect.x;
			this.GuiRect.yMin = rect.y;
			this.GuiRect.xMax = rect.x + rect.width;
			this.GuiRect.yMax = rect.y + rect.height;
			GUILayout.BeginArea(this.GuiRect);
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			if (this.ServerTimestamp)
			{
				GUILayout.Label(((double)PhotonNetwork.ServerTimestamp / 1000.0).ToString("F3"), Array.Empty<GUILayoutOption>());
			}
			if (this.Server)
			{
				GUILayout.Label(PhotonNetwork.ServerAddress + " " + PhotonNetwork.Server.ToString(), Array.Empty<GUILayoutOption>());
			}
			if (this.DetailedConnection)
			{
				GUILayout.Label(PhotonNetwork.NetworkClientState.ToString(), Array.Empty<GUILayoutOption>());
			}
			if (this.AppVersion)
			{
				GUILayout.Label(PhotonNetwork.NetworkingClient.AppVersion, Array.Empty<GUILayoutOption>());
			}
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			if (this.UserId)
			{
				GUILayout.Label("UID: " + ((PhotonNetwork.AuthValues != null) ? PhotonNetwork.AuthValues.UserId : "no UserId"), Array.Empty<GUILayoutOption>());
				GUILayout.Label("UserId:" + PhotonNetwork.LocalPlayer.UserId, Array.Empty<GUILayoutOption>());
			}
			GUILayout.EndHorizontal();
			if (this.Room)
			{
				if (PhotonNetwork.InRoom)
				{
					GUILayout.Label(this.RoomProps ? PhotonNetwork.CurrentRoom.ToStringFull() : PhotonNetwork.CurrentRoom.ToString(), Array.Empty<GUILayoutOption>());
				}
				else
				{
					GUILayout.Label("not in room", Array.Empty<GUILayoutOption>());
				}
			}
			if (this.EventsIn)
			{
				int fragmentCommandCount = PhotonNetwork.NetworkingClient.LoadBalancingPeer.TrafficStatsIncoming.FragmentCommandCount;
				GUILayout.Label("Events Received: " + PhotonNetwork.NetworkingClient.LoadBalancingPeer.TrafficStatsGameLevel.EventCount.ToString() + " Fragments: " + fragmentCommandCount.ToString(), Array.Empty<GUILayoutOption>());
			}
			if (this.LocalPlayer)
			{
				GUILayout.Label(this.PlayerToString(PhotonNetwork.LocalPlayer), Array.Empty<GUILayoutOption>());
			}
			if (this.Others)
			{
				foreach (Player player in PhotonNetwork.PlayerListOthers)
				{
					GUILayout.Label(this.PlayerToString(player), Array.Empty<GUILayoutOption>());
				}
			}
			if (this.ExpectedUsers && PhotonNetwork.InRoom)
			{
				GUILayout.Label("Expected: " + ((PhotonNetwork.CurrentRoom.ExpectedUsers != null) ? PhotonNetwork.CurrentRoom.ExpectedUsers.Length : 0).ToString() + " " + ((PhotonNetwork.CurrentRoom.ExpectedUsers != null) ? string.Join(",", PhotonNetwork.CurrentRoom.ExpectedUsers) : ""), Array.Empty<GUILayoutOption>());
			}
			if (this.Buttons)
			{
				if (!PhotonNetwork.IsConnected && GUILayout.Button("Connect", Array.Empty<GUILayoutOption>()))
				{
					PhotonNetwork.ConnectUsingSettings();
				}
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				if (PhotonNetwork.IsConnected && GUILayout.Button("Disconnect", Array.Empty<GUILayoutOption>()))
				{
					PhotonNetwork.Disconnect();
				}
				if (PhotonNetwork.IsConnected && GUILayout.Button("Close Socket", Array.Empty<GUILayoutOption>()))
				{
					PhotonNetwork.NetworkingClient.LoadBalancingPeer.StopThread();
				}
				GUILayout.EndHorizontal();
				if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom && GUILayout.Button("Leave", Array.Empty<GUILayoutOption>()))
				{
					PhotonNetwork.LeaveRoom(true);
				}
				if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerTtl > 0 && GUILayout.Button("Leave(abandon)", Array.Empty<GUILayoutOption>()))
				{
					PhotonNetwork.LeaveRoom(false);
				}
				if (PhotonNetwork.IsConnected && !PhotonNetwork.InRoom && GUILayout.Button("Join Random", Array.Empty<GUILayoutOption>()))
				{
					PhotonNetwork.JoinRandomRoom();
				}
				if (PhotonNetwork.IsConnected && !PhotonNetwork.InRoom && GUILayout.Button("Create Room", Array.Empty<GUILayoutOption>()))
				{
					PhotonNetwork.CreateRoom(null, null, null, null);
				}
			}
			GUILayout.EndArea();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002DC8 File Offset: 0x00000FC8
		private string PlayerToString(Player player)
		{
			if (PhotonNetwork.NetworkingClient == null)
			{
				Debug.LogError("nwp is null");
				return "";
			}
			return string.Format("#{0:00} '{1}'{5} {4}{2} {3} {6}", new object[]
			{
				player.ActorNumber.ToString() + "/userId:<" + player.UserId + ">",
				player.NickName,
				player.IsMasterClient ? "(master)" : "",
				this.PlayerProps ? player.CustomProperties.ToStringFull() : "",
				(PhotonNetwork.LocalPlayer.ActorNumber == player.ActorNumber) ? "(you)" : "",
				player.UserId,
				player.IsInactive ? " / Is Inactive" : ""
			});
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002EA0 File Offset: 0x000010A0
		public StatesGui()
		{
		}

		// Token: 0x0400000F RID: 15
		public Rect GuiOffset = new Rect(250f, 0f, 300f, 300f);

		// Token: 0x04000010 RID: 16
		public bool DontDestroy = true;

		// Token: 0x04000011 RID: 17
		public bool ServerTimestamp;

		// Token: 0x04000012 RID: 18
		public bool DetailedConnection;

		// Token: 0x04000013 RID: 19
		public bool Server;

		// Token: 0x04000014 RID: 20
		public bool AppVersion;

		// Token: 0x04000015 RID: 21
		public bool UserId;

		// Token: 0x04000016 RID: 22
		public bool Room;

		// Token: 0x04000017 RID: 23
		public bool RoomProps;

		// Token: 0x04000018 RID: 24
		public bool EventsIn;

		// Token: 0x04000019 RID: 25
		public bool LocalPlayer;

		// Token: 0x0400001A RID: 26
		public bool PlayerProps;

		// Token: 0x0400001B RID: 27
		public bool Others;

		// Token: 0x0400001C RID: 28
		public bool Buttons;

		// Token: 0x0400001D RID: 29
		public bool ExpectedUsers;

		// Token: 0x0400001E RID: 30
		private Rect GuiRect;

		// Token: 0x0400001F RID: 31
		private static StatesGui Instance;

		// Token: 0x04000020 RID: 32
		private float native_width = 800f;

		// Token: 0x04000021 RID: 33
		private float native_height = 480f;
	}
}
