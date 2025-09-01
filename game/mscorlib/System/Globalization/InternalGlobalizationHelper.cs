using System;

namespace System.Globalization
{
	// Token: 0x0200096C RID: 2412
	internal class InternalGlobalizationHelper
	{
		// Token: 0x06005547 RID: 21831 RVA: 0x0011E328 File Offset: 0x0011C528
		internal static long TimeToTicks(int hour, int minute, int second)
		{
			long num = (long)hour * 3600L + (long)minute * 60L + (long)second;
			if (num > 922337203685L || num < -922337203685L)
			{
				throw new ArgumentOutOfRangeException(null, "TimeSpan overflowed because the duration is too long.");
			}
			return num * 10000000L;
		}

		// Token: 0x06005548 RID: 21832 RVA: 0x0000259F File Offset: 0x0000079F
		public InternalGlobalizationHelper()
		{
		}

		// Token: 0x040034BC RID: 13500
		internal const long TicksPerMillisecond = 10000L;

		// Token: 0x040034BD RID: 13501
		internal const long TicksPerTenthSecond = 1000000L;

		// Token: 0x040034BE RID: 13502
		internal const long TicksPerSecond = 10000000L;

		// Token: 0x040034BF RID: 13503
		internal const long MaxSeconds = 922337203685L;

		// Token: 0x040034C0 RID: 13504
		internal const long MinSeconds = -922337203685L;

		// Token: 0x040034C1 RID: 13505
		private const int DaysPerYear = 365;

		// Token: 0x040034C2 RID: 13506
		private const int DaysPer4Years = 1461;

		// Token: 0x040034C3 RID: 13507
		private const int DaysPer100Years = 36524;

		// Token: 0x040034C4 RID: 13508
		private const int DaysPer400Years = 146097;

		// Token: 0x040034C5 RID: 13509
		private const int DaysTo10000 = 3652059;

		// Token: 0x040034C6 RID: 13510
		private const long TicksPerMinute = 600000000L;

		// Token: 0x040034C7 RID: 13511
		private const long TicksPerHour = 36000000000L;

		// Token: 0x040034C8 RID: 13512
		private const long TicksPerDay = 864000000000L;

		// Token: 0x040034C9 RID: 13513
		internal const long MaxTicks = 3155378975999999999L;

		// Token: 0x040034CA RID: 13514
		internal const long MinTicks = 0L;

		// Token: 0x040034CB RID: 13515
		internal const long MaxMilliSeconds = 922337203685477L;

		// Token: 0x040034CC RID: 13516
		internal const long MinMilliSeconds = -922337203685477L;

		// Token: 0x040034CD RID: 13517
		internal const int StringBuilderDefaultCapacity = 16;

		// Token: 0x040034CE RID: 13518
		internal const long MaxOffset = 504000000000L;

		// Token: 0x040034CF RID: 13519
		internal const long MinOffset = -504000000000L;
	}
}
