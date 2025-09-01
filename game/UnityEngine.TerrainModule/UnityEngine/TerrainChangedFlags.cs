using System;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	[Flags]
	public enum TerrainChangedFlags
	{
		// Token: 0x04000002 RID: 2
		Heightmap = 1,
		// Token: 0x04000003 RID: 3
		TreeInstances = 2,
		// Token: 0x04000004 RID: 4
		DelayedHeightmapUpdate = 4,
		// Token: 0x04000005 RID: 5
		FlushEverythingImmediately = 8,
		// Token: 0x04000006 RID: 6
		RemoveDirtyDetailsImmediately = 16,
		// Token: 0x04000007 RID: 7
		HeightmapResolution = 32,
		// Token: 0x04000008 RID: 8
		Holes = 64,
		// Token: 0x04000009 RID: 9
		DelayedHolesUpdate = 128,
		// Token: 0x0400000A RID: 10
		WillBeDestroyed = 256
	}
}
