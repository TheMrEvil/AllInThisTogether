using System;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000094 RID: 148
	[Flags]
	public enum PrefabKind
	{
		// Token: 0x0400029A RID: 666
		None = 0,
		// Token: 0x0400029B RID: 667
		InstanceInScene = 1,
		// Token: 0x0400029C RID: 668
		InstanceInPrefab = 2,
		// Token: 0x0400029D RID: 669
		Regular = 4,
		// Token: 0x0400029E RID: 670
		Variant = 8,
		// Token: 0x0400029F RID: 671
		NonPrefabInstance = 16,
		// Token: 0x040002A0 RID: 672
		PrefabInstance = 3,
		// Token: 0x040002A1 RID: 673
		PrefabAsset = 12,
		// Token: 0x040002A2 RID: 674
		PrefabInstanceAndNonPrefabInstance = 19,
		// Token: 0x040002A3 RID: 675
		All = 31
	}
}
