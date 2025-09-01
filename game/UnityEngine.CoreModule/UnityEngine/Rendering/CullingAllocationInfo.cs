using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003FA RID: 1018
	internal struct CullingAllocationInfo
	{
		// Token: 0x04000CCC RID: 3276
		public unsafe VisibleLight* visibleLightsPtr;

		// Token: 0x04000CCD RID: 3277
		public unsafe VisibleLight* visibleOffscreenVertexLightsPtr;

		// Token: 0x04000CCE RID: 3278
		public unsafe VisibleReflectionProbe* visibleReflectionProbesPtr;

		// Token: 0x04000CCF RID: 3279
		public int visibleLightCount;

		// Token: 0x04000CD0 RID: 3280
		public int visibleOffscreenVertexLightCount;

		// Token: 0x04000CD1 RID: 3281
		public int visibleReflectionProbeCount;
	}
}
