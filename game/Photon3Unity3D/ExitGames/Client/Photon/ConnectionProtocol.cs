using System;

namespace ExitGames.Client.Photon
{
	// Token: 0x0200001F RID: 31
	public enum ConnectionProtocol : byte
	{
		// Token: 0x04000115 RID: 277
		Udp,
		// Token: 0x04000116 RID: 278
		Tcp,
		// Token: 0x04000117 RID: 279
		WebSocket = 4,
		// Token: 0x04000118 RID: 280
		WebSocketSecure
	}
}
