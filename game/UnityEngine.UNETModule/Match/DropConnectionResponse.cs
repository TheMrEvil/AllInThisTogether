using System;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x0200002B RID: 43
	[Serializable]
	internal class DropConnectionResponse : Response
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x00005CD4 File Offset: 0x00003ED4
		public override string ToString()
		{
			return UnityString.Format("[{0}]-networkId:{1}", new object[]
			{
				base.ToString(),
				this.networkId.ToString("X")
			});
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000057DE File Offset: 0x000039DE
		public DropConnectionResponse()
		{
		}

		// Token: 0x040000BE RID: 190
		public ulong networkId;

		// Token: 0x040000BF RID: 191
		public NodeID nodeId;
	}
}
