using System;
using System.Collections;
using System.Globalization;

namespace System
{
	// Token: 0x02000109 RID: 265
	[Obsolete("System.CurrentSystemTimeZone has been deprecated.  Please investigate the use of System.TimeZoneInfo.Local instead.")]
	[Serializable]
	internal class CurrentSystemTimeZone : TimeZone
	{
		// Token: 0x06000984 RID: 2436 RVA: 0x00025530 File Offset: 0x00023730
		internal CurrentSystemTimeZone()
		{
			TimeZoneInfo local = TimeZoneInfo.Local;
			this.m_ticksOffset = local.BaseUtcOffset.Ticks;
			this.m_standardName = local.StandardName;
			this.m_daylightName = local.DaylightName;
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00025580 File Offset: 0x00023780
		public override string StandardName
		{
			get
			{
				return this.m_standardName;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x00025588 File Offset: 0x00023788
		public override string DaylightName
		{
			get
			{
				return this.m_daylightName;
			}
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00025590 File Offset: 0x00023790
		internal long GetUtcOffsetFromUniversalTime(DateTime time, ref bool isAmbiguousLocalDst)
		{
			TimeSpan timeSpan = new TimeSpan(this.m_ticksOffset);
			DaylightTime daylightChanges = this.GetDaylightChanges(time.Year);
			isAmbiguousLocalDst = false;
			if (daylightChanges == null || daylightChanges.Delta.Ticks == 0L)
			{
				return timeSpan.Ticks;
			}
			DateTime dateTime = daylightChanges.Start - timeSpan;
			DateTime dateTime2 = daylightChanges.End - timeSpan - daylightChanges.Delta;
			DateTime t;
			DateTime t2;
			if (daylightChanges.Delta.Ticks > 0L)
			{
				t = dateTime2 - daylightChanges.Delta;
				t2 = dateTime2;
			}
			else
			{
				t = dateTime;
				t2 = dateTime - daylightChanges.Delta;
			}
			bool flag;
			if (dateTime > dateTime2)
			{
				flag = (time < dateTime2 || time >= dateTime);
			}
			else
			{
				flag = (time >= dateTime && time < dateTime2);
			}
			if (flag)
			{
				timeSpan += daylightChanges.Delta;
				if (time >= t && time < t2)
				{
					isAmbiguousLocalDst = true;
				}
			}
			return timeSpan.Ticks;
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0002569C File Offset: 0x0002389C
		public override DateTime ToLocalTime(DateTime time)
		{
			if (time.Kind == DateTimeKind.Local)
			{
				return time;
			}
			bool isAmbiguousDst = false;
			long utcOffsetFromUniversalTime = this.GetUtcOffsetFromUniversalTime(time, ref isAmbiguousDst);
			long num = time.Ticks + utcOffsetFromUniversalTime;
			if (num > 3155378975999999999L)
			{
				return new DateTime(3155378975999999999L, DateTimeKind.Local);
			}
			if (num < 0L)
			{
				return new DateTime(0L, DateTimeKind.Local);
			}
			return new DateTime(num, DateTimeKind.Local, isAmbiguousDst);
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x000256FD File Offset: 0x000238FD
		public override DaylightTime GetDaylightChanges(int year)
		{
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", SR.Format("Valid values are between {0} and {1}, inclusive.", 1, 9999));
			}
			return this.GetCachedDaylightChanges(year);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00025738 File Offset: 0x00023938
		private static DaylightTime CreateDaylightChanges(int year)
		{
			DaylightTime daylightTime = null;
			if (TimeZoneInfo.Local.SupportsDaylightSavingTime)
			{
				foreach (TimeZoneInfo.AdjustmentRule adjustmentRule in TimeZoneInfo.Local.GetAdjustmentRules())
				{
					if (adjustmentRule.DateStart.Year <= year && adjustmentRule.DateEnd.Year >= year && adjustmentRule.DaylightDelta != TimeSpan.Zero)
					{
						DateTime start = TimeZoneInfo.TransitionTimeToDateTime(year, adjustmentRule.DaylightTransitionStart);
						DateTime end = TimeZoneInfo.TransitionTimeToDateTime(year, adjustmentRule.DaylightTransitionEnd);
						TimeSpan daylightDelta = adjustmentRule.DaylightDelta;
						daylightTime = new DaylightTime(start, end, daylightDelta);
						break;
					}
				}
			}
			if (daylightTime == null)
			{
				daylightTime = new DaylightTime(DateTime.MinValue, DateTime.MinValue, TimeSpan.Zero);
			}
			return daylightTime;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x000257F8 File Offset: 0x000239F8
		public override TimeSpan GetUtcOffset(DateTime time)
		{
			if (time.Kind == DateTimeKind.Utc)
			{
				return TimeSpan.Zero;
			}
			return new TimeSpan(TimeZone.CalculateUtcOffset(time, this.GetDaylightChanges(time.Year)).Ticks + this.m_ticksOffset);
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0002583C File Offset: 0x00023A3C
		private DaylightTime GetCachedDaylightChanges(int year)
		{
			object key = year;
			if (!this.m_CachedDaylightChanges.Contains(key))
			{
				DaylightTime value = CurrentSystemTimeZone.CreateDaylightChanges(year);
				Hashtable cachedDaylightChanges = this.m_CachedDaylightChanges;
				lock (cachedDaylightChanges)
				{
					if (!this.m_CachedDaylightChanges.Contains(key))
					{
						this.m_CachedDaylightChanges.Add(key, value);
					}
				}
			}
			return (DaylightTime)this.m_CachedDaylightChanges[key];
		}

		// Token: 0x0400108E RID: 4238
		private long m_ticksOffset;

		// Token: 0x0400108F RID: 4239
		private string m_standardName;

		// Token: 0x04001090 RID: 4240
		private string m_daylightName;

		// Token: 0x04001091 RID: 4241
		private readonly Hashtable m_CachedDaylightChanges = new Hashtable();
	}
}
