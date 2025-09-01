using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Realtime;

namespace Photon.Pun
{
	// Token: 0x02000017 RID: 23
	public class MonoBehaviourPunCallbacks : MonoBehaviourPun, IConnectionCallbacks, IMatchmakingCallbacks, IInRoomCallbacks, ILobbyCallbacks, IWebRpcCallback, IErrorInfoCallback
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00008817 File Offset: 0x00006A17
		public virtual void OnEnable()
		{
			PhotonNetwork.AddCallbackTarget(this);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000881F File Offset: 0x00006A1F
		public virtual void OnDisable()
		{
			PhotonNetwork.RemoveCallbackTarget(this);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00008827 File Offset: 0x00006A27
		public virtual void OnConnected()
		{
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00008829 File Offset: 0x00006A29
		public virtual void OnLeftRoom()
		{
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000882B File Offset: 0x00006A2B
		public virtual void OnMasterClientSwitched(Player newMasterClient)
		{
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000882D File Offset: 0x00006A2D
		public virtual void OnCreateRoomFailed(short returnCode, string message)
		{
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000882F File Offset: 0x00006A2F
		public virtual void OnJoinRoomFailed(short returnCode, string message)
		{
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00008831 File Offset: 0x00006A31
		public virtual void OnCreatedRoom()
		{
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00008833 File Offset: 0x00006A33
		public virtual void OnJoinedLobby()
		{
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00008835 File Offset: 0x00006A35
		public virtual void OnLeftLobby()
		{
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00008837 File Offset: 0x00006A37
		public virtual void OnDisconnected(DisconnectCause cause)
		{
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00008839 File Offset: 0x00006A39
		public virtual void OnRegionListReceived(RegionHandler regionHandler)
		{
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000883B File Offset: 0x00006A3B
		public virtual void OnRoomListUpdate(List<RoomInfo> roomList)
		{
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000883D File Offset: 0x00006A3D
		public virtual void OnJoinedRoom()
		{
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000883F File Offset: 0x00006A3F
		public virtual void OnPlayerEnteredRoom(Player newPlayer)
		{
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00008841 File Offset: 0x00006A41
		public virtual void OnPlayerLeftRoom(Player otherPlayer)
		{
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00008843 File Offset: 0x00006A43
		public virtual void OnJoinRandomFailed(short returnCode, string message)
		{
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00008845 File Offset: 0x00006A45
		public virtual void OnConnectedToMaster()
		{
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00008847 File Offset: 0x00006A47
		public virtual void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
		{
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00008849 File Offset: 0x00006A49
		public virtual void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
		{
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000884B File Offset: 0x00006A4B
		public virtual void OnFriendListUpdate(List<FriendInfo> friendList)
		{
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000884D File Offset: 0x00006A4D
		public virtual void OnCustomAuthenticationResponse(Dictionary<string, object> data)
		{
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000884F File Offset: 0x00006A4F
		public virtual void OnCustomAuthenticationFailed(string debugMessage)
		{
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00008851 File Offset: 0x00006A51
		public virtual void OnWebRpcResponse(OperationResponse response)
		{
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00008853 File Offset: 0x00006A53
		public virtual void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
		{
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00008855 File Offset: 0x00006A55
		public virtual void OnErrorInfo(ErrorInfo errorInfo)
		{
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00008857 File Offset: 0x00006A57
		public MonoBehaviourPunCallbacks()
		{
		}
	}
}
