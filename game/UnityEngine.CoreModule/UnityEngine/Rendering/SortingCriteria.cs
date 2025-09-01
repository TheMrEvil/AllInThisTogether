using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000414 RID: 1044
	[Flags]
	public enum SortingCriteria
	{
		// Token: 0x04000D49 RID: 3401
		None = 0,
		// Token: 0x04000D4A RID: 3402
		SortingLayer = 1,
		// Token: 0x04000D4B RID: 3403
		RenderQueue = 2,
		// Token: 0x04000D4C RID: 3404
		BackToFront = 4,
		// Token: 0x04000D4D RID: 3405
		QuantizedFrontToBack = 8,
		// Token: 0x04000D4E RID: 3406
		OptimizeStateChanges = 16,
		// Token: 0x04000D4F RID: 3407
		CanvasOrder = 32,
		// Token: 0x04000D50 RID: 3408
		RendererPriority = 64,
		// Token: 0x04000D51 RID: 3409
		CommonOpaque = 59,
		// Token: 0x04000D52 RID: 3410
		CommonTransparent = 23
	}
}
