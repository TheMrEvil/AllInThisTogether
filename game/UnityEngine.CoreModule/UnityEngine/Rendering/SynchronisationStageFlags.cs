using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003DF RID: 991
	public enum SynchronisationStageFlags
	{
		// Token: 0x04000C27 RID: 3111
		VertexProcessing = 1,
		// Token: 0x04000C28 RID: 3112
		PixelProcessing,
		// Token: 0x04000C29 RID: 3113
		ComputeProcessing = 4,
		// Token: 0x04000C2A RID: 3114
		AllGPUOperations = 7
	}
}
