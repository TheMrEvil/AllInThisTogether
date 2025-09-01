using System;

namespace System
{
	// Token: 0x0200012E RID: 302
	internal enum TokenType
	{
		// Token: 0x04001193 RID: 4499
		NumberToken = 1,
		// Token: 0x04001194 RID: 4500
		YearNumberToken,
		// Token: 0x04001195 RID: 4501
		Am,
		// Token: 0x04001196 RID: 4502
		Pm,
		// Token: 0x04001197 RID: 4503
		MonthToken,
		// Token: 0x04001198 RID: 4504
		EndOfString,
		// Token: 0x04001199 RID: 4505
		DayOfWeekToken,
		// Token: 0x0400119A RID: 4506
		TimeZoneToken,
		// Token: 0x0400119B RID: 4507
		EraToken,
		// Token: 0x0400119C RID: 4508
		DateWordToken,
		// Token: 0x0400119D RID: 4509
		UnknownToken,
		// Token: 0x0400119E RID: 4510
		HebrewNumber,
		// Token: 0x0400119F RID: 4511
		JapaneseEraToken,
		// Token: 0x040011A0 RID: 4512
		TEraToken,
		// Token: 0x040011A1 RID: 4513
		IgnorableSymbol,
		// Token: 0x040011A2 RID: 4514
		SEP_Unk = 256,
		// Token: 0x040011A3 RID: 4515
		SEP_End = 512,
		// Token: 0x040011A4 RID: 4516
		SEP_Space = 768,
		// Token: 0x040011A5 RID: 4517
		SEP_Am = 1024,
		// Token: 0x040011A6 RID: 4518
		SEP_Pm = 1280,
		// Token: 0x040011A7 RID: 4519
		SEP_Date = 1536,
		// Token: 0x040011A8 RID: 4520
		SEP_Time = 1792,
		// Token: 0x040011A9 RID: 4521
		SEP_YearSuff = 2048,
		// Token: 0x040011AA RID: 4522
		SEP_MonthSuff = 2304,
		// Token: 0x040011AB RID: 4523
		SEP_DaySuff = 2560,
		// Token: 0x040011AC RID: 4524
		SEP_HourSuff = 2816,
		// Token: 0x040011AD RID: 4525
		SEP_MinuteSuff = 3072,
		// Token: 0x040011AE RID: 4526
		SEP_SecondSuff = 3328,
		// Token: 0x040011AF RID: 4527
		SEP_LocalTimeMark = 3584,
		// Token: 0x040011B0 RID: 4528
		SEP_DateOrOffset = 3840,
		// Token: 0x040011B1 RID: 4529
		RegularTokenMask = 255,
		// Token: 0x040011B2 RID: 4530
		SeparatorTokenMask = 65280
	}
}
