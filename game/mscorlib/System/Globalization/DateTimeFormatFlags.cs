using System;

namespace System.Globalization
{
	// Token: 0x0200095A RID: 2394
	[Flags]
	internal enum DateTimeFormatFlags
	{
		// Token: 0x040033E2 RID: 13282
		None = 0,
		// Token: 0x040033E3 RID: 13283
		UseGenitiveMonth = 1,
		// Token: 0x040033E4 RID: 13284
		UseLeapYearMonth = 2,
		// Token: 0x040033E5 RID: 13285
		UseSpacesInMonthNames = 4,
		// Token: 0x040033E6 RID: 13286
		UseHebrewRule = 8,
		// Token: 0x040033E7 RID: 13287
		UseSpacesInDayNames = 16,
		// Token: 0x040033E8 RID: 13288
		UseDigitPrefixInTokens = 32,
		// Token: 0x040033E9 RID: 13289
		NotInitialized = -1
	}
}
