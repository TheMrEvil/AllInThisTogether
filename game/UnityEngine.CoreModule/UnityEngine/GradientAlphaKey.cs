using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001C1 RID: 449
	[UsedByNativeCode]
	public struct GradientAlphaKey
	{
		// Token: 0x060013BB RID: 5051 RVA: 0x0001C6E6 File Offset: 0x0001A8E6
		public GradientAlphaKey(float alpha, float time)
		{
			this.alpha = alpha;
			this.time = time;
		}

		// Token: 0x04000746 RID: 1862
		public float alpha;

		// Token: 0x04000747 RID: 1863
		public float time;
	}
}
