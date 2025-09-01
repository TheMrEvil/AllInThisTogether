using System;
using System.Collections.Generic;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x0200002E RID: 46
	[Serializable]
	internal class MatchDesc
	{
		// Token: 0x060001EF RID: 495 RVA: 0x00005F44 File Offset: 0x00004144
		public override string ToString()
		{
			return UnityString.Format("[{0}]-networkId:0x{1},name:{2},averageEloScore:{3},maxSize:{4},currentSize:{5},isPrivate:{6},matchAttributes.Count:{7},hostNodeId:{8},directConnectInfos.Count:{9}", new object[]
			{
				base.ToString(),
				this.networkId.ToString("X"),
				this.name,
				this.averageEloScore,
				this.maxSize,
				this.currentSize,
				this.isPrivate,
				(this.matchAttributes == null) ? 0 : this.matchAttributes.Count,
				this.hostNodeId,
				this.directConnectInfos.Count
			});
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00005759 File Offset: 0x00003959
		public MatchDesc()
		{
		}

		// Token: 0x040000CD RID: 205
		public ulong networkId;

		// Token: 0x040000CE RID: 206
		public string name;

		// Token: 0x040000CF RID: 207
		public int averageEloScore;

		// Token: 0x040000D0 RID: 208
		public int maxSize;

		// Token: 0x040000D1 RID: 209
		public int currentSize;

		// Token: 0x040000D2 RID: 210
		public bool isPrivate;

		// Token: 0x040000D3 RID: 211
		public Dictionary<string, long> matchAttributes;

		// Token: 0x040000D4 RID: 212
		public NodeID hostNodeId;

		// Token: 0x040000D5 RID: 213
		public List<MatchDirectConnectInfo> directConnectInfos;
	}
}
