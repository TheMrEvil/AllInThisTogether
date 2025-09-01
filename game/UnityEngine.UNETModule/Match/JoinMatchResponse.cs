using System;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	internal class JoinMatchResponse : BasicResponse
	{
		// Token: 0x060001CA RID: 458 RVA: 0x00005B08 File Offset: 0x00003D08
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

		// Token: 0x060001CB RID: 459 RVA: 0x00005B96 File Offset: 0x00003D96
		public JoinMatchResponse()
		{
		}

		// Token: 0x040000B4 RID: 180
		public string address;

		// Token: 0x040000B5 RID: 181
		public int port;

		// Token: 0x040000B6 RID: 182
		public int domain = 0;

		// Token: 0x040000B7 RID: 183
		public ulong networkId;

		// Token: 0x040000B8 RID: 184
		public string accessTokenString;

		// Token: 0x040000B9 RID: 185
		public NodeID nodeId;

		// Token: 0x040000BA RID: 186
		public bool usingRelay;
	}
}
