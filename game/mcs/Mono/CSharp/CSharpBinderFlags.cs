using System;

namespace Mono.CSharp
{
	// Token: 0x0200015F RID: 351
	[Flags]
	public enum CSharpBinderFlags
	{
		// Token: 0x04000773 RID: 1907
		None = 0,
		// Token: 0x04000774 RID: 1908
		CheckedContext = 1,
		// Token: 0x04000775 RID: 1909
		InvokeSimpleName = 2,
		// Token: 0x04000776 RID: 1910
		InvokeSpecialName = 4,
		// Token: 0x04000777 RID: 1911
		BinaryOperationLogical = 8,
		// Token: 0x04000778 RID: 1912
		ConvertExplicit = 16,
		// Token: 0x04000779 RID: 1913
		ConvertArrayIndex = 32,
		// Token: 0x0400077A RID: 1914
		ResultIndexed = 64,
		// Token: 0x0400077B RID: 1915
		ValueFromCompoundAssignment = 128,
		// Token: 0x0400077C RID: 1916
		ResultDiscarded = 256
	}
}
