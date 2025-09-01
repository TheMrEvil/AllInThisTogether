using System;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	[Flags]
	public enum MeshColliderCookingOptions
	{
		// Token: 0x04000016 RID: 22
		None = 0,
		// Token: 0x04000017 RID: 23
		[Obsolete("No longer used because the problem this was trying to solve is gone since Unity 2018.3", true)]
		InflateConvexMesh = 1,
		// Token: 0x04000018 RID: 24
		CookForFasterSimulation = 2,
		// Token: 0x04000019 RID: 25
		EnableMeshCleaning = 4,
		// Token: 0x0400001A RID: 26
		WeldColocatedVertices = 8,
		// Token: 0x0400001B RID: 27
		UseFastMidphase = 16
	}
}
