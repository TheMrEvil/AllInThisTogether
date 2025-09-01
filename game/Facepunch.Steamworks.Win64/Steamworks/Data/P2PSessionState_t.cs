using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020001AE RID: 430
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct P2PSessionState_t
	{
		// Token: 0x04000B65 RID: 2917
		internal byte ConnectionActive;

		// Token: 0x04000B66 RID: 2918
		internal byte Connecting;

		// Token: 0x04000B67 RID: 2919
		internal byte P2PSessionError;

		// Token: 0x04000B68 RID: 2920
		internal byte UsingRelay;

		// Token: 0x04000B69 RID: 2921
		internal int BytesQueuedForSend;

		// Token: 0x04000B6A RID: 2922
		internal int PacketsQueuedForSend;

		// Token: 0x04000B6B RID: 2923
		internal uint RemoteIP;

		// Token: 0x04000B6C RID: 2924
		internal ushort RemotePort;
	}
}
