using System;

namespace UnityEngine.Networking.PlayerConnection
{
	// Token: 0x02000389 RID: 905
	[Serializable]
	public class MessageEventArgs
	{
		// Token: 0x06001ECB RID: 7883 RVA: 0x00002072 File Offset: 0x00000272
		public MessageEventArgs()
		{
		}

		// Token: 0x04000A21 RID: 2593
		public int playerId;

		// Token: 0x04000A22 RID: 2594
		public byte[] data;
	}
}
