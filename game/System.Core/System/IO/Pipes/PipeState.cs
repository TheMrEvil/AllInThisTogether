using System;

namespace System.IO.Pipes
{
	// Token: 0x0200034F RID: 847
	internal enum PipeState
	{
		// Token: 0x04000C51 RID: 3153
		WaitingToConnect,
		// Token: 0x04000C52 RID: 3154
		Connected,
		// Token: 0x04000C53 RID: 3155
		Broken,
		// Token: 0x04000C54 RID: 3156
		Disconnected,
		// Token: 0x04000C55 RID: 3157
		Closed
	}
}
