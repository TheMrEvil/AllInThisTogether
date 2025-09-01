using System;

namespace System
{
	// Token: 0x0200015B RID: 347
	[Flags]
	internal enum UnescapeMode
	{
		// Token: 0x0400063B RID: 1595
		CopyOnly = 0,
		// Token: 0x0400063C RID: 1596
		Escape = 1,
		// Token: 0x0400063D RID: 1597
		Unescape = 2,
		// Token: 0x0400063E RID: 1598
		EscapeUnescape = 3,
		// Token: 0x0400063F RID: 1599
		V1ToStringFlag = 4,
		// Token: 0x04000640 RID: 1600
		UnescapeAll = 8,
		// Token: 0x04000641 RID: 1601
		UnescapeAllOrThrow = 24
	}
}
