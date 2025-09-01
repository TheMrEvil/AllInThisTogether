using System;

namespace UnityEngine.AI
{
	// Token: 0x02000017 RID: 23
	[Flags]
	public enum NavMeshBuildDebugFlags
	{
		// Token: 0x04000032 RID: 50
		None = 0,
		// Token: 0x04000033 RID: 51
		InputGeometry = 1,
		// Token: 0x04000034 RID: 52
		Voxels = 2,
		// Token: 0x04000035 RID: 53
		Regions = 4,
		// Token: 0x04000036 RID: 54
		RawContours = 8,
		// Token: 0x04000037 RID: 55
		SimplifiedContours = 16,
		// Token: 0x04000038 RID: 56
		PolygonMeshes = 32,
		// Token: 0x04000039 RID: 57
		PolygonMeshesDetail = 64,
		// Token: 0x0400003A RID: 58
		All = 127
	}
}
