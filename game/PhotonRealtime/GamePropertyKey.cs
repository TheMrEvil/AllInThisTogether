using System;

namespace Photon.Realtime
{
	// Token: 0x02000024 RID: 36
	public class GamePropertyKey
	{
		// Token: 0x06000117 RID: 279 RVA: 0x000083B7 File Offset: 0x000065B7
		public GamePropertyKey()
		{
		}

		// Token: 0x040000ED RID: 237
		public const byte MaxPlayers = 255;

		// Token: 0x040000EE RID: 238
		public const byte MaxPlayersInt = 243;

		// Token: 0x040000EF RID: 239
		public const byte IsVisible = 254;

		// Token: 0x040000F0 RID: 240
		public const byte IsOpen = 253;

		// Token: 0x040000F1 RID: 241
		public const byte PlayerCount = 252;

		// Token: 0x040000F2 RID: 242
		public const byte Removed = 251;

		// Token: 0x040000F3 RID: 243
		public const byte PropsListedInLobby = 250;

		// Token: 0x040000F4 RID: 244
		public const byte CleanupCacheOnLeave = 249;

		// Token: 0x040000F5 RID: 245
		public const byte MasterClientId = 248;

		// Token: 0x040000F6 RID: 246
		public const byte ExpectedUsers = 247;

		// Token: 0x040000F7 RID: 247
		public const byte PlayerTtl = 246;

		// Token: 0x040000F8 RID: 248
		public const byte EmptyRoomTtl = 245;
	}
}
