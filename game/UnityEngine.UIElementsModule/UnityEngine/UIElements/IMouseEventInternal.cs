using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F6 RID: 502
	internal interface IMouseEventInternal
	{
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000FB8 RID: 4024
		// (set) Token: 0x06000FB9 RID: 4025
		bool triggeredByOS { get; set; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000FBA RID: 4026
		// (set) Token: 0x06000FBB RID: 4027
		bool recomputeTopElementUnderMouse { get; set; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000FBC RID: 4028
		// (set) Token: 0x06000FBD RID: 4029
		IPointerEvent sourcePointerEvent { get; set; }
	}
}
