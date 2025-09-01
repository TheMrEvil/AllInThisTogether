using System;
using System.Collections.Generic;

namespace Photon.Realtime
{
	// Token: 0x02000017 RID: 23
	public class MatchMakingCallbacksContainer : List<IMatchmakingCallbacks>, IMatchmakingCallbacks
	{
		// Token: 0x060000DE RID: 222 RVA: 0x000069E8 File Offset: 0x00004BE8
		public MatchMakingCallbacksContainer(LoadBalancingClient client)
		{
			this.client = client;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000069F8 File Offset: 0x00004BF8
		public void OnCreatedRoom()
		{
			this.client.UpdateCallbackTargets();
			foreach (IMatchmakingCallbacks matchmakingCallbacks in this)
			{
				matchmakingCallbacks.OnCreatedRoom();
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00006A50 File Offset: 0x00004C50
		public void OnJoinedRoom()
		{
			this.client.UpdateCallbackTargets();
			foreach (IMatchmakingCallbacks matchmakingCallbacks in this)
			{
				matchmakingCallbacks.OnJoinedRoom();
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00006AA8 File Offset: 0x00004CA8
		public void OnCreateRoomFailed(short returnCode, string message)
		{
			this.client.UpdateCallbackTargets();
			foreach (IMatchmakingCallbacks matchmakingCallbacks in this)
			{
				matchmakingCallbacks.OnCreateRoomFailed(returnCode, message);
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00006B00 File Offset: 0x00004D00
		public void OnJoinRandomFailed(short returnCode, string message)
		{
			this.client.UpdateCallbackTargets();
			foreach (IMatchmakingCallbacks matchmakingCallbacks in this)
			{
				matchmakingCallbacks.OnJoinRandomFailed(returnCode, message);
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006B58 File Offset: 0x00004D58
		public void OnJoinRoomFailed(short returnCode, string message)
		{
			this.client.UpdateCallbackTargets();
			foreach (IMatchmakingCallbacks matchmakingCallbacks in this)
			{
				matchmakingCallbacks.OnJoinRoomFailed(returnCode, message);
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00006BB0 File Offset: 0x00004DB0
		public void OnLeftRoom()
		{
			this.client.UpdateCallbackTargets();
			foreach (IMatchmakingCallbacks matchmakingCallbacks in this)
			{
				matchmakingCallbacks.OnLeftRoom();
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00006C08 File Offset: 0x00004E08
		public void OnFriendListUpdate(List<FriendInfo> friendList)
		{
			this.client.UpdateCallbackTargets();
			foreach (IMatchmakingCallbacks matchmakingCallbacks in this)
			{
				matchmakingCallbacks.OnFriendListUpdate(friendList);
			}
		}

		// Token: 0x040000AB RID: 171
		private readonly LoadBalancingClient client;
	}
}
