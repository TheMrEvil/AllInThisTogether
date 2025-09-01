using System;
using ExitGames.Client.Photon;

namespace Photon.Realtime
{
	// Token: 0x02000021 RID: 33
	public class EnterRoomParams
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00008398 File Offset: 0x00006598
		public EnterRoomParams()
		{
		}

		// Token: 0x040000C4 RID: 196
		public string RoomName;

		// Token: 0x040000C5 RID: 197
		public RoomOptions RoomOptions;

		// Token: 0x040000C6 RID: 198
		public TypedLobby Lobby;

		// Token: 0x040000C7 RID: 199
		public Hashtable PlayerProperties;

		// Token: 0x040000C8 RID: 200
		protected internal bool OnGameServer = true;

		// Token: 0x040000C9 RID: 201
		protected internal JoinMode JoinMode;

		// Token: 0x040000CA RID: 202
		public string[] ExpectedUsers;

		// Token: 0x040000CB RID: 203
		public object Ticket;
	}
}
