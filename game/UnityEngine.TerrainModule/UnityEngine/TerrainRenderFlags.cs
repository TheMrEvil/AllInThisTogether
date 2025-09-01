using System;

namespace UnityEngine
{
	// Token: 0x02000005 RID: 5
	[Flags]
	public enum TerrainRenderFlags
	{
		// Token: 0x0400000C RID: 12
		[Obsolete("TerrainRenderFlags.heightmap is obsolete, use TerrainRenderFlags.Heightmap instead. (UnityUpgradable) -> Heightmap")]
		heightmap = 1,
		// Token: 0x0400000D RID: 13
		[Obsolete("TerrainRenderFlags.trees is obsolete, use TerrainRenderFlags.Trees instead. (UnityUpgradable) -> Trees")]
		trees = 2,
		// Token: 0x0400000E RID: 14
		[Obsolete("TerrainRenderFlags.details is obsolete, use TerrainRenderFlags.Details instead. (UnityUpgradable) -> Details")]
		details = 4,
		// Token: 0x0400000F RID: 15
		[Obsolete("TerrainRenderFlags.all is obsolete, use TerrainRenderFlags.All instead. (UnityUpgradable) -> All")]
		all = 7,
		// Token: 0x04000010 RID: 16
		Heightmap = 1,
		// Token: 0x04000011 RID: 17
		Trees = 2,
		// Token: 0x04000012 RID: 18
		Details = 4,
		// Token: 0x04000013 RID: 19
		All = 7
	}
}
