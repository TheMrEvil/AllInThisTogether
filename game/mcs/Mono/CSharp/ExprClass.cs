using System;

namespace Mono.CSharp
{
	// Token: 0x0200019D RID: 413
	public enum ExprClass : byte
	{
		// Token: 0x0400093B RID: 2363
		Unresolved,
		// Token: 0x0400093C RID: 2364
		Value,
		// Token: 0x0400093D RID: 2365
		Variable,
		// Token: 0x0400093E RID: 2366
		Namespace,
		// Token: 0x0400093F RID: 2367
		Type,
		// Token: 0x04000940 RID: 2368
		TypeParameter,
		// Token: 0x04000941 RID: 2369
		MethodGroup,
		// Token: 0x04000942 RID: 2370
		PropertyAccess,
		// Token: 0x04000943 RID: 2371
		EventAccess,
		// Token: 0x04000944 RID: 2372
		IndexerAccess,
		// Token: 0x04000945 RID: 2373
		Nothing
	}
}
