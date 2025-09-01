using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000316 RID: 790
	[Flags]
	internal enum RenderDataDirtyTypes
	{
		// Token: 0x04000B94 RID: 2964
		None = 0,
		// Token: 0x04000B95 RID: 2965
		Transform = 1,
		// Token: 0x04000B96 RID: 2966
		ClipRectSize = 2,
		// Token: 0x04000B97 RID: 2967
		Clipping = 4,
		// Token: 0x04000B98 RID: 2968
		ClippingHierarchy = 8,
		// Token: 0x04000B99 RID: 2969
		Visuals = 16,
		// Token: 0x04000B9A RID: 2970
		VisualsHierarchy = 32,
		// Token: 0x04000B9B RID: 2971
		Opacity = 64,
		// Token: 0x04000B9C RID: 2972
		OpacityHierarchy = 128,
		// Token: 0x04000B9D RID: 2973
		Color = 256
	}
}
