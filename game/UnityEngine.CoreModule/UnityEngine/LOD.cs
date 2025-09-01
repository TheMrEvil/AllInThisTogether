using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000199 RID: 409
	[UsedByNativeCode]
	public struct LOD
	{
		// Token: 0x06000EF9 RID: 3833 RVA: 0x00012ED5 File Offset: 0x000110D5
		public LOD(float screenRelativeTransitionHeight, Renderer[] renderers)
		{
			this.screenRelativeTransitionHeight = screenRelativeTransitionHeight;
			this.fadeTransitionWidth = 0f;
			this.renderers = renderers;
		}

		// Token: 0x040005A8 RID: 1448
		public float screenRelativeTransitionHeight;

		// Token: 0x040005A9 RID: 1449
		public float fadeTransitionWidth;

		// Token: 0x040005AA RID: 1450
		public Renderer[] renderers;
	}
}
