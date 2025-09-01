using System;

namespace ExitGames.Client.Photon
{
	// Token: 0x0200001E RID: 30
	public enum PeerStateValue : byte
	{
		// Token: 0x0400010F RID: 271
		Disconnected,
		// Token: 0x04000110 RID: 272
		Connecting,
		// Token: 0x04000111 RID: 273
		InitializingApplication = 10,
		// Token: 0x04000112 RID: 274
		Connected = 3,
		// Token: 0x04000113 RID: 275
		Disconnecting
	}
}
