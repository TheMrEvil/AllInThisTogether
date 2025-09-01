using System;

namespace Mono.CSharp
{
	// Token: 0x0200019E RID: 414
	[Flags]
	public enum ResolveFlags
	{
		// Token: 0x04000947 RID: 2375
		VariableOrValue = 1,
		// Token: 0x04000948 RID: 2376
		Type = 2,
		// Token: 0x04000949 RID: 2377
		MethodGroup = 4,
		// Token: 0x0400094A RID: 2378
		TypeParameter = 8,
		// Token: 0x0400094B RID: 2379
		MaskExprClass = 15
	}
}
