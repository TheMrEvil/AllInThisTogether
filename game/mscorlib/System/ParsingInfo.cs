using System;
using System.Globalization;

namespace System
{
	// Token: 0x0200012D RID: 301
	internal struct ParsingInfo
	{
		// Token: 0x06000B96 RID: 2966 RVA: 0x0002F66F File Offset: 0x0002D86F
		internal void Init()
		{
			this.dayOfWeek = -1;
			this.timeMark = DateTimeParse.TM.NotSet;
		}

		// Token: 0x04001189 RID: 4489
		internal Calendar calendar;

		// Token: 0x0400118A RID: 4490
		internal int dayOfWeek;

		// Token: 0x0400118B RID: 4491
		internal DateTimeParse.TM timeMark;

		// Token: 0x0400118C RID: 4492
		internal bool fUseHour12;

		// Token: 0x0400118D RID: 4493
		internal bool fUseTwoDigitYear;

		// Token: 0x0400118E RID: 4494
		internal bool fAllowInnerWhite;

		// Token: 0x0400118F RID: 4495
		internal bool fAllowTrailingWhite;

		// Token: 0x04001190 RID: 4496
		internal bool fCustomNumberParser;

		// Token: 0x04001191 RID: 4497
		internal DateTimeParse.MatchNumberDelegate parseNumberDelegate;
	}
}
