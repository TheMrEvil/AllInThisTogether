using System;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x0200002D RID: 45
	[Serializable]
	internal class MatchDirectConnectInfo
	{
		// Token: 0x060001ED RID: 493 RVA: 0x00005EE8 File Offset: 0x000040E8
		public override string ToString()
		{
			return UnityString.Format("[{0}]-nodeId:{1},publicAddress:{2},privateAddress:{3},hostPriority:{4}", new object[]
			{
				base.ToString(),
				this.nodeId,
				this.publicAddress,
				this.privateAddress,
				this.hostPriority
			});
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00005759 File Offset: 0x00003959
		public MatchDirectConnectInfo()
		{
		}

		// Token: 0x040000C9 RID: 201
		public NodeID nodeId;

		// Token: 0x040000CA RID: 202
		public string publicAddress;

		// Token: 0x040000CB RID: 203
		public string privateAddress;

		// Token: 0x040000CC RID: 204
		public HostPriority hostPriority;
	}
}
