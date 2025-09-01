using System;
using System.Collections.Generic;

namespace UnityEngine.Networking.Match
{
	// Token: 0x0200002F RID: 47
	[Serializable]
	internal class ListMatchResponse : BasicResponse
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x00006004 File Offset: 0x00004204
		public ListMatchResponse()
		{
			this.matches = new List<MatchDesc>();
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006019 File Offset: 0x00004219
		public ListMatchResponse(List<MatchDesc> otherMatches)
		{
			this.matches = otherMatches;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000602C File Offset: 0x0000422C
		public override string ToString()
		{
			return UnityString.Format("[{0}]-matches.Count:{1}", new object[]
			{
				base.ToString(),
				(this.matches == null) ? 0 : this.matches.Count
			});
		}

		// Token: 0x040000D6 RID: 214
		public List<MatchDesc> matches;
	}
}
