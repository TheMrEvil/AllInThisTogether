using System;
using System.Collections.Generic;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000025 RID: 37
	public class DisconnectMessage
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x0000D138 File Offset: 0x0000B338
		public DisconnectMessage()
		{
		}

		// Token: 0x04000169 RID: 361
		public short Code;

		// Token: 0x0400016A RID: 362
		public string DebugMessage;

		// Token: 0x0400016B RID: 363
		public Dictionary<byte, object> Parameters;
	}
}
