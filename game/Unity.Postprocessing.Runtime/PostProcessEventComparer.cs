using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200005C RID: 92
	internal struct PostProcessEventComparer : IEqualityComparer<PostProcessEvent>
	{
		// Token: 0x0600014F RID: 335 RVA: 0x0000C1AA File Offset: 0x0000A3AA
		public bool Equals(PostProcessEvent x, PostProcessEvent y)
		{
			return x == y;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000C1B0 File Offset: 0x0000A3B0
		public int GetHashCode(PostProcessEvent obj)
		{
			return (int)obj;
		}
	}
}
