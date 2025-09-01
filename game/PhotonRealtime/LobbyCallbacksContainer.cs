using System;
using System.Collections.Generic;

namespace Photon.Realtime
{
	// Token: 0x02000019 RID: 25
	internal class LobbyCallbacksContainer : List<ILobbyCallbacks>, ILobbyCallbacks
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00006E28 File Offset: 0x00005028
		public LobbyCallbacksContainer(LoadBalancingClient client)
		{
			this.client = client;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00006E38 File Offset: 0x00005038
		public void OnJoinedLobby()
		{
			this.client.UpdateCallbackTargets();
			foreach (ILobbyCallbacks lobbyCallbacks in this)
			{
				lobbyCallbacks.OnJoinedLobby();
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00006E90 File Offset: 0x00005090
		public void OnLeftLobby()
		{
			this.client.UpdateCallbackTargets();
			foreach (ILobbyCallbacks lobbyCallbacks in this)
			{
				lobbyCallbacks.OnLeftLobby();
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006EE8 File Offset: 0x000050E8
		public void OnRoomListUpdate(List<RoomInfo> roomList)
		{
			this.client.UpdateCallbackTargets();
			foreach (ILobbyCallbacks lobbyCallbacks in this)
			{
				lobbyCallbacks.OnRoomListUpdate(roomList);
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006F40 File Offset: 0x00005140
		public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
		{
			this.client.UpdateCallbackTargets();
			foreach (ILobbyCallbacks lobbyCallbacks in this)
			{
				lobbyCallbacks.OnLobbyStatisticsUpdate(lobbyStatistics);
			}
		}

		// Token: 0x040000AD RID: 173
		private readonly LoadBalancingClient client;
	}
}
