using System;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000019 RID: 25
	public enum ConnectionStateValue : byte
	{
		// Token: 0x040000C8 RID: 200
		Disconnected,
		// Token: 0x040000C9 RID: 201
		Connecting,
		// Token: 0x040000CA RID: 202
		Connected = 3,
		// Token: 0x040000CB RID: 203
		Disconnecting,
		// Token: 0x040000CC RID: 204
		AcknowledgingDisconnect,
		// Token: 0x040000CD RID: 205
		Zombie
	}
}
