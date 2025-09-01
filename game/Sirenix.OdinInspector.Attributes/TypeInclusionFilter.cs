using System;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200009B RID: 155
	[Flags]
	public enum TypeInclusionFilter
	{
		// Token: 0x040008BD RID: 2237
		None = 0,
		// Token: 0x040008BE RID: 2238
		IncludeConcreteTypes = 1,
		// Token: 0x040008BF RID: 2239
		IncludeGenerics = 2,
		// Token: 0x040008C0 RID: 2240
		IncludeAbstracts = 4,
		// Token: 0x040008C1 RID: 2241
		IncludeInterfaces = 8,
		// Token: 0x040008C2 RID: 2242
		IncludeAll = 15
	}
}
