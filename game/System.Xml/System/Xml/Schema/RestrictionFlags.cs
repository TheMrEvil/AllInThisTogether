using System;

namespace System.Xml.Schema
{
	// Token: 0x0200050C RID: 1292
	[Flags]
	internal enum RestrictionFlags
	{
		// Token: 0x0400270A RID: 9994
		Length = 1,
		// Token: 0x0400270B RID: 9995
		MinLength = 2,
		// Token: 0x0400270C RID: 9996
		MaxLength = 4,
		// Token: 0x0400270D RID: 9997
		Pattern = 8,
		// Token: 0x0400270E RID: 9998
		Enumeration = 16,
		// Token: 0x0400270F RID: 9999
		WhiteSpace = 32,
		// Token: 0x04002710 RID: 10000
		MaxInclusive = 64,
		// Token: 0x04002711 RID: 10001
		MaxExclusive = 128,
		// Token: 0x04002712 RID: 10002
		MinInclusive = 256,
		// Token: 0x04002713 RID: 10003
		MinExclusive = 512,
		// Token: 0x04002714 RID: 10004
		TotalDigits = 1024,
		// Token: 0x04002715 RID: 10005
		FractionDigits = 2048
	}
}
