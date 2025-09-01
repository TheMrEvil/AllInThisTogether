using System;

namespace System.Globalization
{
	// Token: 0x02000967 RID: 2407
	internal enum HebrewNumberParsingState
	{
		// Token: 0x04003491 RID: 13457
		InvalidHebrewNumber,
		// Token: 0x04003492 RID: 13458
		NotHebrewDigit,
		// Token: 0x04003493 RID: 13459
		FoundEndOfHebrewNumber,
		// Token: 0x04003494 RID: 13460
		ContinueParsing
	}
}
