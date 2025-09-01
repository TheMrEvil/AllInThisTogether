using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003C4 RID: 964
	[Flags]
	public enum RenderTargetFlags
	{
		// Token: 0x04000B81 RID: 2945
		None = 0,
		// Token: 0x04000B82 RID: 2946
		ReadOnlyDepth = 1,
		// Token: 0x04000B83 RID: 2947
		ReadOnlyStencil = 2,
		// Token: 0x04000B84 RID: 2948
		ReadOnlyDepthStencil = 3
	}
}
