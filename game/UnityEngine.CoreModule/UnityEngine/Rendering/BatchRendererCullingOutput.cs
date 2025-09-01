using System;
using Unity.Jobs;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003EB RID: 1003
	[NativeHeader("Runtime/Camera/BatchRendererGroup.h")]
	[UsedByNativeCode]
	internal struct BatchRendererCullingOutput
	{
		// Token: 0x04000C5B RID: 3163
		public JobHandle cullingJobsFence;

		// Token: 0x04000C5C RID: 3164
		public Matrix4x4 cullingMatrix;

		// Token: 0x04000C5D RID: 3165
		public unsafe Plane* cullingPlanes;

		// Token: 0x04000C5E RID: 3166
		public unsafe BatchVisibility* batchVisibility;

		// Token: 0x04000C5F RID: 3167
		public unsafe int* visibleIndices;

		// Token: 0x04000C60 RID: 3168
		public unsafe int* visibleIndicesY;

		// Token: 0x04000C61 RID: 3169
		public int cullingPlanesCount;

		// Token: 0x04000C62 RID: 3170
		public int batchVisibilityCount;

		// Token: 0x04000C63 RID: 3171
		public int visibleIndicesCount;

		// Token: 0x04000C64 RID: 3172
		public float nearPlane;
	}
}
