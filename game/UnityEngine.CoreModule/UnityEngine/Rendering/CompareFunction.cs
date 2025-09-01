using System;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x020003AF RID: 943
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum CompareFunction
	{
		// Token: 0x04000ABC RID: 2748
		Disabled,
		// Token: 0x04000ABD RID: 2749
		Never,
		// Token: 0x04000ABE RID: 2750
		Less,
		// Token: 0x04000ABF RID: 2751
		Equal,
		// Token: 0x04000AC0 RID: 2752
		LessEqual,
		// Token: 0x04000AC1 RID: 2753
		Greater,
		// Token: 0x04000AC2 RID: 2754
		NotEqual,
		// Token: 0x04000AC3 RID: 2755
		GreaterEqual,
		// Token: 0x04000AC4 RID: 2756
		Always
	}
}
