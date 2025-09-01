using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000013 RID: 19
	[UsedByNativeCode]
	public struct PatchExtents
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00002E44 File Offset: 0x00001044
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00002E5C File Offset: 0x0000105C
		public float min
		{
			get
			{
				return this.m_min;
			}
			set
			{
				this.m_min = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00002E68 File Offset: 0x00001068
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00002E80 File Offset: 0x00001080
		public float max
		{
			get
			{
				return this.m_max;
			}
			set
			{
				this.m_max = value;
			}
		}

		// Token: 0x04000040 RID: 64
		internal float m_min;

		// Token: 0x04000041 RID: 65
		internal float m_max;
	}
}
