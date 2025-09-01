using System;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x020003AD RID: 941
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum BlendMode
	{
		// Token: 0x04000A8B RID: 2699
		Zero,
		// Token: 0x04000A8C RID: 2700
		One,
		// Token: 0x04000A8D RID: 2701
		DstColor,
		// Token: 0x04000A8E RID: 2702
		SrcColor,
		// Token: 0x04000A8F RID: 2703
		OneMinusDstColor,
		// Token: 0x04000A90 RID: 2704
		SrcAlpha,
		// Token: 0x04000A91 RID: 2705
		OneMinusSrcColor,
		// Token: 0x04000A92 RID: 2706
		DstAlpha,
		// Token: 0x04000A93 RID: 2707
		OneMinusDstAlpha,
		// Token: 0x04000A94 RID: 2708
		SrcAlphaSaturate,
		// Token: 0x04000A95 RID: 2709
		OneMinusSrcAlpha
	}
}
