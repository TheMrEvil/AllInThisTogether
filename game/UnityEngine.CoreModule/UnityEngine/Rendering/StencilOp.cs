using System;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x020003B2 RID: 946
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum StencilOp
	{
		// Token: 0x04000AD0 RID: 2768
		Keep,
		// Token: 0x04000AD1 RID: 2769
		Zero,
		// Token: 0x04000AD2 RID: 2770
		Replace,
		// Token: 0x04000AD3 RID: 2771
		IncrementSaturate,
		// Token: 0x04000AD4 RID: 2772
		DecrementSaturate,
		// Token: 0x04000AD5 RID: 2773
		Invert,
		// Token: 0x04000AD6 RID: 2774
		IncrementWrap,
		// Token: 0x04000AD7 RID: 2775
		DecrementWrap
	}
}
