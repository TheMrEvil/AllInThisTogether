using System;
using ExitGames.Client.Photon;

namespace Photon.Realtime
{
	// Token: 0x02000020 RID: 32
	public class OpJoinRandomRoomParams
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00008390 File Offset: 0x00006590
		public OpJoinRandomRoomParams()
		{
		}

		// Token: 0x040000BD RID: 189
		public Hashtable ExpectedCustomRoomProperties;

		// Token: 0x040000BE RID: 190
		public int ExpectedMaxPlayers;

		// Token: 0x040000BF RID: 191
		public MatchmakingMode MatchingType;

		// Token: 0x040000C0 RID: 192
		public TypedLobby TypedLobby;

		// Token: 0x040000C1 RID: 193
		public string SqlLobbyFilter;

		// Token: 0x040000C2 RID: 194
		public string[] ExpectedUsers;

		// Token: 0x040000C3 RID: 195
		public object Ticket;
	}
}
