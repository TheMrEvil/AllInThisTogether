using System;

namespace System.Net
{
	// Token: 0x02000585 RID: 1413
	internal class ReceiveState
	{
		// Token: 0x06002DC5 RID: 11717 RVA: 0x0009CDC4 File Offset: 0x0009AFC4
		internal ReceiveState(CommandStream connection)
		{
			this.Connection = connection;
			this.Resp = new ResponseDescription();
			this.Buffer = new byte[1024];
			this.ValidThrough = 0;
		}

		// Token: 0x0400191D RID: 6429
		private const int bufferSize = 1024;

		// Token: 0x0400191E RID: 6430
		internal ResponseDescription Resp;

		// Token: 0x0400191F RID: 6431
		internal int ValidThrough;

		// Token: 0x04001920 RID: 6432
		internal byte[] Buffer;

		// Token: 0x04001921 RID: 6433
		internal CommandStream Connection;
	}
}
