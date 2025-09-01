using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000042 RID: 66
	[Flags]
	public enum ClearFlag
	{
		// Token: 0x040001A6 RID: 422
		None = 0,
		// Token: 0x040001A7 RID: 423
		Color = 1,
		// Token: 0x040001A8 RID: 424
		Depth = 2,
		// Token: 0x040001A9 RID: 425
		Stencil = 4,
		// Token: 0x040001AA RID: 426
		DepthStencil = 6,
		// Token: 0x040001AB RID: 427
		ColorStencil = 5,
		// Token: 0x040001AC RID: 428
		All = 7
	}
}
