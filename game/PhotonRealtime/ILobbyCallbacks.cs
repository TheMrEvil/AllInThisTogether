using System;
using System.Collections.Generic;

namespace Photon.Realtime
{
	// Token: 0x02000010 RID: 16
	public interface ILobbyCallbacks
	{
		// Token: 0x060000C4 RID: 196
		void OnJoinedLobby();

		// Token: 0x060000C5 RID: 197
		void OnLeftLobby();

		// Token: 0x060000C6 RID: 198
		void OnRoomListUpdate(List<RoomInfo> roomList);

		// Token: 0x060000C7 RID: 199
		void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics);
	}
}
