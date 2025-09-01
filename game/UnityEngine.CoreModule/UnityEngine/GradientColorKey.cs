using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001C0 RID: 448
	[UsedByNativeCode]
	public struct GradientColorKey
	{
		// Token: 0x060013BA RID: 5050 RVA: 0x0001C6D5 File Offset: 0x0001A8D5
		public GradientColorKey(Color col, float time)
		{
			this.color = col;
			this.time = time;
		}

		// Token: 0x04000744 RID: 1860
		public Color color;

		// Token: 0x04000745 RID: 1861
		public float time;
	}
}
