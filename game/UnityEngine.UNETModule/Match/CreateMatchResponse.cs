using System;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x02000026 RID: 38
	internal class CreateMatchResponse : BasicResponse
	{
		// Token: 0x060001BB RID: 443 RVA: 0x00005968 File Offset: 0x00003B68
		public override string ToString()
		{
			return UnityString.Format("[{0}]-address:{1},port:{2},networkId:0x{3},accessTokenString.IsEmpty:{4},nodeId:0x{5},usingRelay:{6}", new object[]
			{
				base.ToString(),
				this.address,
				this.port,
				this.networkId.ToString("X"),
				string.IsNullOrEmpty(this.accessTokenString),
				this.nodeId.ToString("X"),
				this.usingRelay
			});
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000059F6 File Offset: 0x00003BF6
		public CreateMatchResponse()
		{
		}

		// Token: 0x040000A8 RID: 168
		public string address;

		// Token: 0x040000A9 RID: 169
		public int port;

		// Token: 0x040000AA RID: 170
		public int domain = 0;

		// Token: 0x040000AB RID: 171
		public ulong networkId;

		// Token: 0x040000AC RID: 172
		public string accessTokenString;

		// Token: 0x040000AD RID: 173
		public NodeID nodeId;

		// Token: 0x040000AE RID: 174
		public bool usingRelay;
	}
}
