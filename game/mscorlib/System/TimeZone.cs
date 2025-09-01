using System;
using System.Globalization;
using System.Threading;

namespace System
{
	/// <summary>Represents a time zone.</summary>
	// Token: 0x02000193 RID: 403
	[Obsolete("System.TimeZone has been deprecated.  Please investigate the use of System.TimeZoneInfo instead.")]
	[Serializable]
	public abstract class TimeZone
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x00042338 File Offset: 0x00040538
		private static object InternalSyncObject
		{
			get
			{
				if (TimeZone.s_InternalSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange<object>(ref TimeZone.s_InternalSyncObject, value, null);
				}
				return TimeZone.s_InternalSyncObject;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeZone" /> class.</summary>
		// Token: 0x0600101E RID: 4126 RVA: 0x0000259F File Offset: 0x0000079F
		protected TimeZone()
		{
		}

		/// <summary>Gets the time zone of the current computer.</summary>
		/// <returns>A <see cref="T:System.TimeZone" /> object that represents the current local time zone.</returns>
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x00042364 File Offset: 0x00040564
		public static TimeZone CurrentTimeZone
		{
			get
			{
				TimeZone timeZone = TimeZone.currentTimeZone;
				if (timeZone == null)
				{
					object internalSyncObject = TimeZone.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (TimeZone.currentTimeZone == null)
						{
							TimeZone.currentTimeZone = new CurrentSystemTimeZone();
						}
						timeZone = TimeZone.currentTimeZone;
					}
				}
				return timeZone;
			}
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x000423C8 File Offset: 0x000405C8
		internal static void ResetTimeZone()
		{
			if (TimeZone.currentTimeZone != null)
			{
				object internalSyncObject = TimeZone.InternalSyncObject;
				lock (internalSyncObject)
				{
					TimeZone.currentTimeZone = null;
				}
			}
		}

		/// <summary>Gets the standard time zone name.</summary>
		/// <returns>The standard time zone name.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt was made to set this property to <see langword="null" />.</exception>
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06001021 RID: 4129
		public abstract string StandardName { get; }

		/// <summary>Gets the daylight saving time zone name.</summary>
		/// <returns>The daylight saving time zone name.</returns>
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06001022 RID: 4130
		public abstract string DaylightName { get; }

		/// <summary>Returns the Coordinated Universal Time (UTC) offset for the specified local time.</summary>
		/// <param name="time">A date and time value.</param>
		/// <returns>The Coordinated Universal Time (UTC) offset from <paramref name="time" />.</returns>
		// Token: 0x06001023 RID: 4131
		public abstract TimeSpan GetUtcOffset(DateTime time);

		/// <summary>Returns the Coordinated Universal Time (UTC) that corresponds to a specified time.</summary>
		/// <param name="time">A date and time.</param>
		/// <returns>A <see cref="T:System.DateTime" /> object whose value is the Coordinated Universal Time (UTC) that corresponds to <paramref name="time" />.</returns>
		// Token: 0x06001024 RID: 4132 RVA: 0x00042414 File Offset: 0x00040614
		public virtual DateTime ToUniversalTime(DateTime time)
		{
			if (time.Kind == DateTimeKind.Utc)
			{
				return time;
			}
			long num = time.Ticks - this.GetUtcOffset(time).Ticks;
			if (num > 3155378975999999999L)
			{
				return new DateTime(3155378975999999999L, DateTimeKind.Utc);
			}
			if (num < 0L)
			{
				return new DateTime(0L, DateTimeKind.Utc);
			}
			return new DateTime(num, DateTimeKind.Utc);
		}

		/// <summary>Returns the local time that corresponds to a specified date and time value.</summary>
		/// <param name="time">A Coordinated Universal Time (UTC) time.</param>
		/// <returns>A <see cref="T:System.DateTime" /> object whose value is the local time that corresponds to <paramref name="time" />.</returns>
		// Token: 0x06001025 RID: 4133 RVA: 0x00042478 File Offset: 0x00040678
		public virtual DateTime ToLocalTime(DateTime time)
		{
			if (time.Kind == DateTimeKind.Local)
			{
				return time;
			}
			bool isAmbiguousDst = false;
			long utcOffsetFromUniversalTime = ((CurrentSystemTimeZone)TimeZone.CurrentTimeZone).GetUtcOffsetFromUniversalTime(time, ref isAmbiguousDst);
			return new DateTime(time.Ticks + utcOffsetFromUniversalTime, DateTimeKind.Local, isAmbiguousDst);
		}

		/// <summary>Returns the daylight saving time period for a particular year.</summary>
		/// <param name="year">The year that the daylight saving time period applies to.</param>
		/// <returns>A <see cref="T:System.Globalization.DaylightTime" /> object that contains the start and end date for daylight saving time in <paramref name="year" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999.</exception>
		// Token: 0x06001026 RID: 4134
		public abstract DaylightTime GetDaylightChanges(int year);

		/// <summary>Returns a value indicating whether the specified date and time is within a daylight saving time period.</summary>
		/// <param name="time">A date and time.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="time" /> is in a daylight saving time period; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001027 RID: 4135 RVA: 0x000424B6 File Offset: 0x000406B6
		public virtual bool IsDaylightSavingTime(DateTime time)
		{
			return TimeZone.IsDaylightSavingTime(time, this.GetDaylightChanges(time.Year));
		}

		/// <summary>Returns a value indicating whether the specified date and time is within the specified daylight saving time period.</summary>
		/// <param name="time">A date and time.</param>
		/// <param name="daylightTimes">A daylight saving time period.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="time" /> is in <paramref name="daylightTimes" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="daylightTimes" /> is <see langword="null" />.</exception>
		// Token: 0x06001028 RID: 4136 RVA: 0x000424CB File Offset: 0x000406CB
		public static bool IsDaylightSavingTime(DateTime time, DaylightTime daylightTimes)
		{
			return TimeZone.CalculateUtcOffset(time, daylightTimes) != TimeSpan.Zero;
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x000424E0 File Offset: 0x000406E0
		internal static TimeSpan CalculateUtcOffset(DateTime time, DaylightTime daylightTimes)
		{
			if (daylightTimes == null)
			{
				return TimeSpan.Zero;
			}
			if (time.Kind == DateTimeKind.Utc)
			{
				return TimeSpan.Zero;
			}
			DateTime dateTime = daylightTimes.Start + daylightTimes.Delta;
			DateTime end = daylightTimes.End;
			DateTime t;
			DateTime t2;
			if (daylightTimes.Delta.Ticks > 0L)
			{
				t = end - daylightTimes.Delta;
				t2 = end;
			}
			else
			{
				t = dateTime;
				t2 = dateTime - daylightTimes.Delta;
			}
			bool flag = false;
			if (dateTime > end)
			{
				if (time >= dateTime || time < end)
				{
					flag = true;
				}
			}
			else if (time >= dateTime && time < end)
			{
				flag = true;
			}
			if (flag && time >= t && time < t2)
			{
				flag = time.IsAmbiguousDaylightSavingTime();
			}
			if (flag)
			{
				return daylightTimes.Delta;
			}
			return TimeSpan.Zero;
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x000425B9 File Offset: 0x000407B9
		internal static void ClearCachedData()
		{
			TimeZone.currentTimeZone = null;
		}

		// Token: 0x04001317 RID: 4887
		private static volatile TimeZone currentTimeZone;

		// Token: 0x04001318 RID: 4888
		private static object s_InternalSyncObject;
	}
}
