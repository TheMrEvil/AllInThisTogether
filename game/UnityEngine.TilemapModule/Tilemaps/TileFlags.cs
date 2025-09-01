using System;

namespace UnityEngine.Tilemaps
{
	// Token: 0x0200000D RID: 13
	[Flags]
	public enum TileFlags
	{
		// Token: 0x0400002B RID: 43
		None = 0,
		// Token: 0x0400002C RID: 44
		LockColor = 1,
		// Token: 0x0400002D RID: 45
		LockTransform = 2,
		// Token: 0x0400002E RID: 46
		InstantiateGameObjectRuntimeOnly = 4,
		// Token: 0x0400002F RID: 47
		KeepGameObjectRuntimeOnly = 8,
		// Token: 0x04000030 RID: 48
		LockAll = 3
	}
}
