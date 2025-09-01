using System;

namespace UnityEngine.XR
{
	// Token: 0x02000013 RID: 19
	internal static class TimeConverter
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003738 File Offset: 0x00001938
		public static DateTime now
		{
			get
			{
				return DateTime.Now;
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003750 File Offset: 0x00001950
		public static long LocalDateTimeToUnixTimeMilliseconds(DateTime date)
		{
			return Convert.ToInt64((date.ToUniversalTime() - TimeConverter.s_Epoch).TotalMilliseconds);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003780 File Offset: 0x00001980
		public static DateTime UnixTimeMillisecondsToLocalDateTime(long unixTimeInMilliseconds)
		{
			return TimeConverter.s_Epoch.AddMilliseconds((double)unixTimeInMilliseconds).ToLocalTime();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000037A9 File Offset: 0x000019A9
		// Note: this type is marked as 'beforefieldinit'.
		static TimeConverter()
		{
		}

		// Token: 0x0400009C RID: 156
		private static readonly DateTime s_Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	}
}
