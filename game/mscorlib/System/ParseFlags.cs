using System;

namespace System
{
	// Token: 0x0200012B RID: 299
	[Flags]
	internal enum ParseFlags
	{
		// Token: 0x04001168 RID: 4456
		HaveYear = 1,
		// Token: 0x04001169 RID: 4457
		HaveMonth = 2,
		// Token: 0x0400116A RID: 4458
		HaveDay = 4,
		// Token: 0x0400116B RID: 4459
		HaveHour = 8,
		// Token: 0x0400116C RID: 4460
		HaveMinute = 16,
		// Token: 0x0400116D RID: 4461
		HaveSecond = 32,
		// Token: 0x0400116E RID: 4462
		HaveTime = 64,
		// Token: 0x0400116F RID: 4463
		HaveDate = 128,
		// Token: 0x04001170 RID: 4464
		TimeZoneUsed = 256,
		// Token: 0x04001171 RID: 4465
		TimeZoneUtc = 512,
		// Token: 0x04001172 RID: 4466
		ParsedMonthName = 1024,
		// Token: 0x04001173 RID: 4467
		CaptureOffset = 2048,
		// Token: 0x04001174 RID: 4468
		YearDefault = 4096,
		// Token: 0x04001175 RID: 4469
		Rfc1123Pattern = 8192,
		// Token: 0x04001176 RID: 4470
		UtcSortPattern = 16384
	}
}
