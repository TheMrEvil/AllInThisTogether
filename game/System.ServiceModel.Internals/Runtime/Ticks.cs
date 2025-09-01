using System;
using System.Runtime.Interop;
using System.Security;

namespace System.Runtime
{
	// Token: 0x0200002F RID: 47
	internal static class Ticks
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00006534 File Offset: 0x00004734
		public static long Now
		{
			[SecuritySafeCritical]
			get
			{
				long result;
				UnsafeNativeMethods.GetSystemTimeAsFileTime(out result);
				return result;
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006549 File Offset: 0x00004749
		public static long FromMilliseconds(int milliseconds)
		{
			return checked(unchecked((long)milliseconds) * 10000L);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006554 File Offset: 0x00004754
		public static int ToMilliseconds(long ticks)
		{
			return checked((int)(ticks / 10000L));
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000655F File Offset: 0x0000475F
		public static long FromTimeSpan(TimeSpan duration)
		{
			return duration.Ticks;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006568 File Offset: 0x00004768
		public static TimeSpan ToTimeSpan(long ticks)
		{
			return new TimeSpan(ticks);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006570 File Offset: 0x00004770
		public static long Add(long firstTicks, long secondTicks)
		{
			if (firstTicks == 9223372036854775807L || firstTicks == -9223372036854775808L)
			{
				return firstTicks;
			}
			if (secondTicks == 9223372036854775807L || secondTicks == -9223372036854775808L)
			{
				return secondTicks;
			}
			if (firstTicks >= 0L && 9223372036854775807L - firstTicks <= secondTicks)
			{
				return 9223372036854775806L;
			}
			if (firstTicks <= 0L && -9223372036854775808L - firstTicks >= secondTicks)
			{
				return -9223372036854775807L;
			}
			return checked(firstTicks + secondTicks);
		}
	}
}
