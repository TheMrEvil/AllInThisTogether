using System;

namespace UnityEngine
{
	// Token: 0x02000225 RID: 549
	[Flags]
	public enum HideFlags
	{
		// Token: 0x0400081A RID: 2074
		None = 0,
		// Token: 0x0400081B RID: 2075
		HideInHierarchy = 1,
		// Token: 0x0400081C RID: 2076
		HideInInspector = 2,
		// Token: 0x0400081D RID: 2077
		DontSaveInEditor = 4,
		// Token: 0x0400081E RID: 2078
		NotEditable = 8,
		// Token: 0x0400081F RID: 2079
		DontSaveInBuild = 16,
		// Token: 0x04000820 RID: 2080
		DontUnloadUnusedAsset = 32,
		// Token: 0x04000821 RID: 2081
		DontSave = 52,
		// Token: 0x04000822 RID: 2082
		HideAndDontSave = 61
	}
}
