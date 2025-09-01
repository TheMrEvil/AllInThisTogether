using System;

namespace Photon.Realtime
{
	// Token: 0x02000007 RID: 7
	public enum ClientState
	{
		// Token: 0x0400002A RID: 42
		PeerCreated,
		// Token: 0x0400002B RID: 43
		Authenticating,
		// Token: 0x0400002C RID: 44
		Authenticated,
		// Token: 0x0400002D RID: 45
		JoiningLobby,
		// Token: 0x0400002E RID: 46
		JoinedLobby,
		// Token: 0x0400002F RID: 47
		DisconnectingFromMasterServer,
		// Token: 0x04000030 RID: 48
		[Obsolete("Renamed to DisconnectingFromMasterServer")]
		DisconnectingFromMasterserver = 5,
		// Token: 0x04000031 RID: 49
		ConnectingToGameServer,
		// Token: 0x04000032 RID: 50
		[Obsolete("Renamed to ConnectingToGameServer")]
		ConnectingToGameserver = 6,
		// Token: 0x04000033 RID: 51
		ConnectedToGameServer,
		// Token: 0x04000034 RID: 52
		[Obsolete("Renamed to ConnectedToGameServer")]
		ConnectedToGameserver = 7,
		// Token: 0x04000035 RID: 53
		Joining,
		// Token: 0x04000036 RID: 54
		Joined,
		// Token: 0x04000037 RID: 55
		Leaving,
		// Token: 0x04000038 RID: 56
		DisconnectingFromGameServer,
		// Token: 0x04000039 RID: 57
		[Obsolete("Renamed to DisconnectingFromGameServer")]
		DisconnectingFromGameserver = 11,
		// Token: 0x0400003A RID: 58
		ConnectingToMasterServer,
		// Token: 0x0400003B RID: 59
		[Obsolete("Renamed to ConnectingToMasterServer.")]
		ConnectingToMasterserver = 12,
		// Token: 0x0400003C RID: 60
		Disconnecting,
		// Token: 0x0400003D RID: 61
		Disconnected,
		// Token: 0x0400003E RID: 62
		ConnectedToMasterServer,
		// Token: 0x0400003F RID: 63
		[Obsolete("Renamed to ConnectedToMasterServer.")]
		ConnectedToMasterserver = 15,
		// Token: 0x04000040 RID: 64
		[Obsolete("Renamed to ConnectedToMasterServer.")]
		ConnectedToMaster = 15,
		// Token: 0x04000041 RID: 65
		ConnectingToNameServer,
		// Token: 0x04000042 RID: 66
		ConnectedToNameServer,
		// Token: 0x04000043 RID: 67
		DisconnectingFromNameServer,
		// Token: 0x04000044 RID: 68
		ConnectWithFallbackProtocol
	}
}
