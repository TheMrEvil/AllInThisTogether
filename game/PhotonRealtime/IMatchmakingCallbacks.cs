using System;
using System.Collections.Generic;

namespace Photon.Realtime
{
	// Token: 0x02000011 RID: 17
	public interface IMatchmakingCallbacks
	{
		// Token: 0x060000C8 RID: 200
		void OnFriendListUpdate(List<FriendInfo> friendList);

		// Token: 0x060000C9 RID: 201
		void OnCreatedRoom();

		// Token: 0x060000CA RID: 202
		void OnCreateRoomFailed(short returnCode, string message);

		// Token: 0x060000CB RID: 203
		void OnJoinedRoom();

		// Token: 0x060000CC RID: 204
		void OnJoinRoomFailed(short returnCode, string message);

		// Token: 0x060000CD RID: 205
		void OnJoinRandomFailed(short returnCode, string message);

		// Token: 0x060000CE RID: 206
		void OnLeftRoom();
	}
}
