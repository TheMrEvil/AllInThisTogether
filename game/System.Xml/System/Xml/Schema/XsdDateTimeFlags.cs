using System;

namespace System.Xml.Schema
{
	// Token: 0x02000605 RID: 1541
	[Flags]
	internal enum XsdDateTimeFlags
	{
		// Token: 0x04002D46 RID: 11590
		DateTime = 1,
		// Token: 0x04002D47 RID: 11591
		Time = 2,
		// Token: 0x04002D48 RID: 11592
		Date = 4,
		// Token: 0x04002D49 RID: 11593
		GYearMonth = 8,
		// Token: 0x04002D4A RID: 11594
		GYear = 16,
		// Token: 0x04002D4B RID: 11595
		GMonthDay = 32,
		// Token: 0x04002D4C RID: 11596
		GDay = 64,
		// Token: 0x04002D4D RID: 11597
		GMonth = 128,
		// Token: 0x04002D4E RID: 11598
		XdrDateTimeNoTz = 256,
		// Token: 0x04002D4F RID: 11599
		XdrDateTime = 512,
		// Token: 0x04002D50 RID: 11600
		XdrTimeNoTz = 1024,
		// Token: 0x04002D51 RID: 11601
		AllXsd = 255
	}
}
