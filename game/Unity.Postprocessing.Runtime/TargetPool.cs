using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200006E RID: 110
	internal class TargetPool
	{
		// Token: 0x0600025E RID: 606 RVA: 0x000126A2 File Offset: 0x000108A2
		internal TargetPool()
		{
			this.m_Pool = new List<int>();
			this.Get();
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000126BC File Offset: 0x000108BC
		internal int Get()
		{
			int result = this.Get(this.m_Current);
			this.m_Current++;
			return result;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000126D8 File Offset: 0x000108D8
		private int Get(int i)
		{
			int result;
			if (this.m_Pool.Count > i)
			{
				result = this.m_Pool[i];
			}
			else
			{
				while (this.m_Pool.Count <= i)
				{
					this.m_Pool.Add(Shader.PropertyToID("_TargetPool" + i.ToString()));
				}
				result = this.m_Pool[i];
			}
			return result;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0001273F File Offset: 0x0001093F
		internal void Reset()
		{
			this.m_Current = 0;
		}

		// Token: 0x040002BD RID: 701
		private readonly List<int> m_Pool;

		// Token: 0x040002BE RID: 702
		private int m_Current;
	}
}
