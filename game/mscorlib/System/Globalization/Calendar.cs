using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Globalization
{
	/// <summary>Represents time in divisions, such as weeks, months, and years.</summary>
	// Token: 0x02000983 RID: 2435
	[ComVisible(true)]
	[Serializable]
	public abstract class Calendar : ICloneable
	{
		/// <summary>Gets the earliest date and time supported by this <see cref="T:System.Globalization.Calendar" /> object.</summary>
		/// <returns>The earliest date and time supported by this calendar. The default is <see cref="F:System.DateTime.MinValue" />.</returns>
		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x060055F2 RID: 22002 RVA: 0x00122736 File Offset: 0x00120936
		[ComVisible(false)]
		public virtual DateTime MinSupportedDateTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		/// <summary>Gets the latest date and time supported by this <see cref="T:System.Globalization.Calendar" /> object.</summary>
		/// <returns>The latest date and time supported by this calendar. The default is <see cref="F:System.DateTime.MaxValue" />.</returns>
		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x060055F3 RID: 22003 RVA: 0x0012273D File Offset: 0x0012093D
		[ComVisible(false)]
		public virtual DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.Calendar" /> class.</summary>
		// Token: 0x060055F4 RID: 22004 RVA: 0x00122744 File Offset: 0x00120944
		protected Calendar()
		{
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x060055F5 RID: 22005 RVA: 0x0012275A File Offset: 0x0012095A
		internal virtual int ID
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x060055F6 RID: 22006 RVA: 0x0012275D File Offset: 0x0012095D
		internal virtual int BaseCalendarID
		{
			get
			{
				return this.ID;
			}
		}

		/// <summary>Gets a value indicating whether the current calendar is solar-based, lunar-based, or a combination of both.</summary>
		/// <returns>One of the <see cref="T:System.Globalization.CalendarAlgorithmType" /> values.</returns>
		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x060055F7 RID: 22007 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		[ComVisible(false)]
		public virtual CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.Unknown;
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Globalization.Calendar" /> object is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Globalization.Calendar" /> object is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x060055F8 RID: 22008 RVA: 0x00122765 File Offset: 0x00120965
		[ComVisible(false)]
		public bool IsReadOnly
		{
			get
			{
				return this.m_isReadOnly;
			}
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Globalization.Calendar" /> object.</summary>
		/// <returns>A new instance of <see cref="T:System.Object" /> that is the memberwise clone of the current <see cref="T:System.Globalization.Calendar" /> object.</returns>
		// Token: 0x060055F9 RID: 22009 RVA: 0x0012276D File Offset: 0x0012096D
		[ComVisible(false)]
		public virtual object Clone()
		{
			object obj = base.MemberwiseClone();
			((Calendar)obj).SetReadOnlyState(false);
			return obj;
		}

		/// <summary>Returns a read-only version of the specified <see cref="T:System.Globalization.Calendar" /> object.</summary>
		/// <param name="calendar">A <see cref="T:System.Globalization.Calendar" /> object.</param>
		/// <returns>The <see cref="T:System.Globalization.Calendar" /> object specified by the <paramref name="calendar" /> parameter, if <paramref name="calendar" /> is read-only.  
		///  -or-  
		///  A read-only memberwise clone of the <see cref="T:System.Globalization.Calendar" /> object specified by <paramref name="calendar" />, if <paramref name="calendar" /> is not read-only.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="calendar" /> is <see langword="null" />.</exception>
		// Token: 0x060055FA RID: 22010 RVA: 0x00122781 File Offset: 0x00120981
		[ComVisible(false)]
		public static Calendar ReadOnly(Calendar calendar)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			if (calendar.IsReadOnly)
			{
				return calendar;
			}
			Calendar calendar2 = (Calendar)calendar.MemberwiseClone();
			calendar2.SetReadOnlyState(true);
			return calendar2;
		}

		// Token: 0x060055FB RID: 22011 RVA: 0x001227AD File Offset: 0x001209AD
		internal void VerifyWritable()
		{
			if (this.m_isReadOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
			}
		}

		// Token: 0x060055FC RID: 22012 RVA: 0x001227C7 File Offset: 0x001209C7
		internal void SetReadOnlyState(bool readOnly)
		{
			this.m_isReadOnly = readOnly;
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x060055FD RID: 22013 RVA: 0x001227D0 File Offset: 0x001209D0
		internal virtual int CurrentEraValue
		{
			get
			{
				if (this.m_currentEraValue == -1)
				{
					this.m_currentEraValue = CalendarData.GetCalendarData(this.BaseCalendarID).iCurrentEra;
				}
				return this.m_currentEraValue;
			}
		}

		// Token: 0x060055FE RID: 22014 RVA: 0x001227F7 File Offset: 0x001209F7
		internal static void CheckAddResult(long ticks, DateTime minValue, DateTime maxValue)
		{
			if (ticks < minValue.Ticks || ticks > maxValue.Ticks)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("The result is out of the supported range for this calendar. The result should be between {0} (Gregorian date) and {1} (Gregorian date), inclusive."), minValue, maxValue));
			}
		}

		// Token: 0x060055FF RID: 22015 RVA: 0x00122834 File Offset: 0x00120A34
		internal DateTime Add(DateTime time, double value, int scale)
		{
			double num = value * (double)scale + ((value >= 0.0) ? 0.5 : -0.5);
			if (num <= -315537897600000.0 || num >= 315537897600000.0)
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Value to add was out of range."));
			}
			long num2 = (long)num;
			long ticks = time.Ticks + num2 * 10000L;
			Calendar.CheckAddResult(ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(ticks);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of milliseconds away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to add milliseconds to.</param>
		/// <param name="milliseconds">The number of milliseconds to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of milliseconds to the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="milliseconds" /> is outside the supported range of the <see cref="T:System.DateTime" /> return value.</exception>
		// Token: 0x06005600 RID: 22016 RVA: 0x001228BE File Offset: 0x00120ABE
		public virtual DateTime AddMilliseconds(DateTime time, double milliseconds)
		{
			return this.Add(time, milliseconds, 1);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of days away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add days.</param>
		/// <param name="days">The number of days to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of days to the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="days" /> is outside the supported range of the <see cref="T:System.DateTime" /> return value.</exception>
		// Token: 0x06005601 RID: 22017 RVA: 0x001228C9 File Offset: 0x00120AC9
		public virtual DateTime AddDays(DateTime time, int days)
		{
			return this.Add(time, (double)days, 86400000);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of hours away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add hours.</param>
		/// <param name="hours">The number of hours to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of hours to the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="hours" /> is outside the supported range of the <see cref="T:System.DateTime" /> return value.</exception>
		// Token: 0x06005602 RID: 22018 RVA: 0x001228D9 File Offset: 0x00120AD9
		public virtual DateTime AddHours(DateTime time, int hours)
		{
			return this.Add(time, (double)hours, 3600000);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of minutes away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add minutes.</param>
		/// <param name="minutes">The number of minutes to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of minutes to the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="minutes" /> is outside the supported range of the <see cref="T:System.DateTime" /> return value.</exception>
		// Token: 0x06005603 RID: 22019 RVA: 0x001228E9 File Offset: 0x00120AE9
		public virtual DateTime AddMinutes(DateTime time, int minutes)
		{
			return this.Add(time, (double)minutes, 60000);
		}

		/// <summary>When overridden in a derived class, returns a <see cref="T:System.DateTime" /> that is the specified number of months away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add months.</param>
		/// <param name="months">The number of months to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of months to the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is outside the supported range of the <see cref="T:System.DateTime" /> return value.</exception>
		// Token: 0x06005604 RID: 22020
		public abstract DateTime AddMonths(DateTime time, int months);

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of seconds away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add seconds.</param>
		/// <param name="seconds">The number of seconds to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of seconds to the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="seconds" /> is outside the supported range of the <see cref="T:System.DateTime" /> return value.</exception>
		// Token: 0x06005605 RID: 22021 RVA: 0x001228F9 File Offset: 0x00120AF9
		public virtual DateTime AddSeconds(DateTime time, int seconds)
		{
			return this.Add(time, (double)seconds, 1000);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of weeks away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add weeks.</param>
		/// <param name="weeks">The number of weeks to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of weeks to the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="weeks" /> is outside the supported range of the <see cref="T:System.DateTime" /> return value.</exception>
		// Token: 0x06005606 RID: 22022 RVA: 0x00122909 File Offset: 0x00120B09
		public virtual DateTime AddWeeks(DateTime time, int weeks)
		{
			return this.AddDays(time, weeks * 7);
		}

		/// <summary>When overridden in a derived class, returns a <see cref="T:System.DateTime" /> that is the specified number of years away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add years.</param>
		/// <param name="years">The number of years to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of years to the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="years" /> is outside the supported range of the <see cref="T:System.DateTime" /> return value.</exception>
		// Token: 0x06005607 RID: 22023
		public abstract DateTime AddYears(DateTime time, int years);

		/// <summary>When overridden in a derived class, returns the day of the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>A positive integer that represents the day of the month in the <paramref name="time" /> parameter.</returns>
		// Token: 0x06005608 RID: 22024
		public abstract int GetDayOfMonth(DateTime time);

		/// <summary>When overridden in a derived class, returns the day of the week in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in the <paramref name="time" /> parameter.</returns>
		// Token: 0x06005609 RID: 22025
		public abstract DayOfWeek GetDayOfWeek(DateTime time);

		/// <summary>When overridden in a derived class, returns the day of the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>A positive integer that represents the day of the year in the <paramref name="time" /> parameter.</returns>
		// Token: 0x0600560A RID: 22026
		public abstract int GetDayOfYear(DateTime time);

		/// <summary>Returns the number of days in the specified month and year of the current era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">A positive integer that represents the month.</param>
		/// <returns>The number of days in the specified month in the specified year in the current era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="month" /> is outside the range supported by the calendar.</exception>
		// Token: 0x0600560B RID: 22027 RVA: 0x00122915 File Offset: 0x00120B15
		public virtual int GetDaysInMonth(int year, int month)
		{
			return this.GetDaysInMonth(year, month, 0);
		}

		/// <summary>When overridden in a derived class, returns the number of days in the specified month, year, and era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">A positive integer that represents the month.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>The number of days in the specified month in the specified year in the specified era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="month" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="era" /> is outside the range supported by the calendar.</exception>
		// Token: 0x0600560C RID: 22028
		public abstract int GetDaysInMonth(int year, int month, int era);

		/// <summary>Returns the number of days in the specified year of the current era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <returns>The number of days in the specified year in the current era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.</exception>
		// Token: 0x0600560D RID: 22029 RVA: 0x00122920 File Offset: 0x00120B20
		public virtual int GetDaysInYear(int year)
		{
			return this.GetDaysInYear(year, 0);
		}

		/// <summary>When overridden in a derived class, returns the number of days in the specified year and era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>The number of days in the specified year in the specified era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="era" /> is outside the range supported by the calendar.</exception>
		// Token: 0x0600560E RID: 22030
		public abstract int GetDaysInYear(int year, int era);

		/// <summary>When overridden in a derived class, returns the era in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer that represents the era in <paramref name="time" />.</returns>
		// Token: 0x0600560F RID: 22031
		public abstract int GetEra(DateTime time);

		/// <summary>When overridden in a derived class, gets the list of eras in the current calendar.</summary>
		/// <returns>An array of integers that represents the eras in the current calendar.</returns>
		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x06005610 RID: 22032
		public abstract int[] Eras { get; }

		/// <summary>Returns the hours value in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 0 to 23 that represents the hour in <paramref name="time" />.</returns>
		// Token: 0x06005611 RID: 22033 RVA: 0x0012292A File Offset: 0x00120B2A
		public virtual int GetHour(DateTime time)
		{
			return (int)(time.Ticks / 36000000000L % 24L);
		}

		/// <summary>Returns the milliseconds value in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>A double-precision floating-point number from 0 to 999 that represents the milliseconds in the <paramref name="time" /> parameter.</returns>
		// Token: 0x06005612 RID: 22034 RVA: 0x00122942 File Offset: 0x00120B42
		public virtual double GetMilliseconds(DateTime time)
		{
			return (double)(time.Ticks / 10000L % 1000L);
		}

		/// <summary>Returns the minutes value in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 0 to 59 that represents the minutes in <paramref name="time" />.</returns>
		// Token: 0x06005613 RID: 22035 RVA: 0x0012295A File Offset: 0x00120B5A
		public virtual int GetMinute(DateTime time)
		{
			return (int)(time.Ticks / 600000000L % 60L);
		}

		/// <summary>When overridden in a derived class, returns the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>A positive integer that represents the month in <paramref name="time" />.</returns>
		// Token: 0x06005614 RID: 22036
		public abstract int GetMonth(DateTime time);

		/// <summary>Returns the number of months in the specified year in the current era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <returns>The number of months in the specified year in the current era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06005615 RID: 22037 RVA: 0x0012296F File Offset: 0x00120B6F
		public virtual int GetMonthsInYear(int year)
		{
			return this.GetMonthsInYear(year, 0);
		}

		/// <summary>When overridden in a derived class, returns the number of months in the specified year in the specified era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>The number of months in the specified year in the specified era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="era" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06005616 RID: 22038
		public abstract int GetMonthsInYear(int year, int era);

		/// <summary>Returns the seconds value in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 0 to 59 that represents the seconds in <paramref name="time" />.</returns>
		// Token: 0x06005617 RID: 22039 RVA: 0x00122979 File Offset: 0x00120B79
		public virtual int GetSecond(DateTime time)
		{
			return (int)(time.Ticks / 10000000L % 60L);
		}

		// Token: 0x06005618 RID: 22040 RVA: 0x00122990 File Offset: 0x00120B90
		internal int GetFirstDayWeekOfYear(DateTime time, int firstDayOfWeek)
		{
			int num = this.GetDayOfYear(time) - 1;
			int num2 = (this.GetDayOfWeek(time) - (DayOfWeek)(num % 7) - firstDayOfWeek + 14) % 7;
			return (num + num2) / 7 + 1;
		}

		// Token: 0x06005619 RID: 22041 RVA: 0x001229C4 File Offset: 0x00120BC4
		private int GetWeekOfYearFullDays(DateTime time, int firstDayOfWeek, int fullDays)
		{
			int num = this.GetDayOfYear(time) - 1;
			int num2 = this.GetDayOfWeek(time) - (DayOfWeek)(num % 7);
			int num3 = (firstDayOfWeek - num2 + 14) % 7;
			if (num3 != 0 && num3 >= fullDays)
			{
				num3 -= 7;
			}
			int num4 = num - num3;
			if (num4 >= 0)
			{
				return num4 / 7 + 1;
			}
			if (time <= this.MinSupportedDateTime.AddDays((double)num))
			{
				return this.GetWeekOfYearOfMinSupportedDateTime(firstDayOfWeek, fullDays);
			}
			return this.GetWeekOfYearFullDays(time.AddDays((double)(-(double)(num + 1))), firstDayOfWeek, fullDays);
		}

		// Token: 0x0600561A RID: 22042 RVA: 0x00122A40 File Offset: 0x00120C40
		private int GetWeekOfYearOfMinSupportedDateTime(int firstDayOfWeek, int minimumDaysInFirstWeek)
		{
			int num = this.GetDayOfYear(this.MinSupportedDateTime) - 1;
			int num2 = this.GetDayOfWeek(this.MinSupportedDateTime) - (DayOfWeek)(num % 7);
			int num3 = (firstDayOfWeek + 7 - num2) % 7;
			if (num3 == 0 || num3 >= minimumDaysInFirstWeek)
			{
				return 1;
			}
			int num4 = this.DaysInYearBeforeMinSupportedYear - 1;
			int num5 = num2 - 1 - num4 % 7;
			int num6 = (firstDayOfWeek - num5 + 14) % 7;
			int num7 = num4 - num6;
			if (num6 >= minimumDaysInFirstWeek)
			{
				num7 += 7;
			}
			return num7 / 7 + 1;
		}

		/// <summary>Gets the number of days in the year that precedes the year that is specified by the <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> property.</summary>
		/// <returns>The number of days in the year that precedes the year specified by <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" />.</returns>
		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x0600561B RID: 22043 RVA: 0x00122AB2 File Offset: 0x00120CB2
		protected virtual int DaysInYearBeforeMinSupportedYear
		{
			get
			{
				return 365;
			}
		}

		/// <summary>Returns the week of the year that includes the date in the specified <see cref="T:System.DateTime" /> value.</summary>
		/// <param name="time">A date and time value.</param>
		/// <param name="rule">An enumeration value that defines a calendar week.</param>
		/// <param name="firstDayOfWeek">An enumeration value that represents the first day of the week.</param>
		/// <returns>A positive integer that represents the week of the year that includes the date in the <paramref name="time" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is earlier than <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> or later than <see cref="P:System.Globalization.Calendar.MaxSupportedDateTime" />.  
		/// -or-  
		/// <paramref name="firstDayOfWeek" /> is not a valid <see cref="T:System.DayOfWeek" /> value.  
		/// -or-  
		/// <paramref name="rule" /> is not a valid <see cref="T:System.Globalization.CalendarWeekRule" /> value.</exception>
		// Token: 0x0600561C RID: 22044 RVA: 0x00122ABC File Offset: 0x00120CBC
		public virtual int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			if (firstDayOfWeek < DayOfWeek.Sunday || firstDayOfWeek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException("firstDayOfWeek", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[]
				{
					DayOfWeek.Sunday,
					DayOfWeek.Saturday
				}));
			}
			switch (rule)
			{
			case CalendarWeekRule.FirstDay:
				return this.GetFirstDayWeekOfYear(time, (int)firstDayOfWeek);
			case CalendarWeekRule.FirstFullWeek:
				return this.GetWeekOfYearFullDays(time, (int)firstDayOfWeek, 7);
			case CalendarWeekRule.FirstFourDayWeek:
				return this.GetWeekOfYearFullDays(time, (int)firstDayOfWeek, 4);
			default:
				throw new ArgumentOutOfRangeException("rule", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[]
				{
					CalendarWeekRule.FirstDay,
					CalendarWeekRule.FirstFourDayWeek
				}));
			}
		}

		/// <summary>When overridden in a derived class, returns the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer that represents the year in <paramref name="time" />.</returns>
		// Token: 0x0600561D RID: 22045
		public abstract int GetYear(DateTime time);

		/// <summary>Determines whether the specified date in the current era is a leap day.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">A positive integer that represents the month.</param>
		/// <param name="day">A positive integer that represents the day.</param>
		/// <returns>
		///   <see langword="true" /> if the specified day is a leap day; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="month" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="day" /> is outside the range supported by the calendar.</exception>
		// Token: 0x0600561E RID: 22046 RVA: 0x00122B5B File Offset: 0x00120D5B
		public virtual bool IsLeapDay(int year, int month, int day)
		{
			return this.IsLeapDay(year, month, day, 0);
		}

		/// <summary>When overridden in a derived class, determines whether the specified date in the specified era is a leap day.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">A positive integer that represents the month.</param>
		/// <param name="day">A positive integer that represents the day.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>
		///   <see langword="true" /> if the specified day is a leap day; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="month" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="day" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="era" /> is outside the range supported by the calendar.</exception>
		// Token: 0x0600561F RID: 22047
		public abstract bool IsLeapDay(int year, int month, int day, int era);

		/// <summary>Determines whether the specified month in the specified year in the current era is a leap month.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">A positive integer that represents the month.</param>
		/// <returns>
		///   <see langword="true" /> if the specified month is a leap month; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="month" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06005620 RID: 22048 RVA: 0x00122B67 File Offset: 0x00120D67
		public virtual bool IsLeapMonth(int year, int month)
		{
			return this.IsLeapMonth(year, month, 0);
		}

		/// <summary>When overridden in a derived class, determines whether the specified month in the specified year in the specified era is a leap month.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">A positive integer that represents the month.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>
		///   <see langword="true" /> if the specified month is a leap month; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="month" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="era" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06005621 RID: 22049
		public abstract bool IsLeapMonth(int year, int month, int era);

		/// <summary>Calculates the leap month for a specified year.</summary>
		/// <param name="year">A year.</param>
		/// <returns>A positive integer that indicates the leap month in the specified year.  
		///  -or-  
		///  Zero if this calendar does not support a leap month or if the <paramref name="year" /> parameter does not represent a leap year.</returns>
		// Token: 0x06005622 RID: 22050 RVA: 0x00122B72 File Offset: 0x00120D72
		[ComVisible(false)]
		public virtual int GetLeapMonth(int year)
		{
			return this.GetLeapMonth(year, 0);
		}

		/// <summary>Calculates the leap month for a specified year and era.</summary>
		/// <param name="year">A year.</param>
		/// <param name="era">An era.</param>
		/// <returns>A positive integer that indicates the leap month in the specified year and era.  
		///  -or-  
		///  Zero if this calendar does not support a leap month or if the <paramref name="year" /> and <paramref name="era" /> parameters do not specify a leap year.</returns>
		// Token: 0x06005623 RID: 22051 RVA: 0x00122B7C File Offset: 0x00120D7C
		[ComVisible(false)]
		public virtual int GetLeapMonth(int year, int era)
		{
			if (!this.IsLeapYear(year, era))
			{
				return 0;
			}
			int monthsInYear = this.GetMonthsInYear(year, era);
			for (int i = 1; i <= monthsInYear; i++)
			{
				if (this.IsLeapMonth(year, i, era))
				{
					return i;
				}
			}
			return 0;
		}

		/// <summary>Determines whether the specified year in the current era is a leap year.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <returns>
		///   <see langword="true" /> if the specified year is a leap year; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06005624 RID: 22052 RVA: 0x00122BB8 File Offset: 0x00120DB8
		public virtual bool IsLeapYear(int year)
		{
			return this.IsLeapYear(year, 0);
		}

		/// <summary>When overridden in a derived class, determines whether the specified year in the specified era is a leap year.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>
		///   <see langword="true" /> if the specified year is a leap year; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="era" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06005625 RID: 22053
		public abstract bool IsLeapYear(int year, int era);

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is set to the specified date and time in the current era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">A positive integer that represents the month.</param>
		/// <param name="day">A positive integer that represents the day.</param>
		/// <param name="hour">An integer from 0 to 23 that represents the hour.</param>
		/// <param name="minute">An integer from 0 to 59 that represents the minute.</param>
		/// <param name="second">An integer from 0 to 59 that represents the second.</param>
		/// <param name="millisecond">An integer from 0 to 999 that represents the millisecond.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that is set to the specified date and time in the current era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="month" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="day" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="hour" /> is less than zero or greater than 23.  
		/// -or-  
		/// <paramref name="minute" /> is less than zero or greater than 59.  
		/// -or-  
		/// <paramref name="second" /> is less than zero or greater than 59.  
		/// -or-  
		/// <paramref name="millisecond" /> is less than zero or greater than 999.</exception>
		// Token: 0x06005626 RID: 22054 RVA: 0x00122BC4 File Offset: 0x00120DC4
		public virtual DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
		{
			return this.ToDateTime(year, month, day, hour, minute, second, millisecond, 0);
		}

		/// <summary>When overridden in a derived class, returns a <see cref="T:System.DateTime" /> that is set to the specified date and time in the specified era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">A positive integer that represents the month.</param>
		/// <param name="day">A positive integer that represents the day.</param>
		/// <param name="hour">An integer from 0 to 23 that represents the hour.</param>
		/// <param name="minute">An integer from 0 to 59 that represents the minute.</param>
		/// <param name="second">An integer from 0 to 59 that represents the second.</param>
		/// <param name="millisecond">An integer from 0 to 999 that represents the millisecond.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that is set to the specified date and time in the current era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="month" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="day" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="hour" /> is less than zero or greater than 23.  
		/// -or-  
		/// <paramref name="minute" /> is less than zero or greater than 59.  
		/// -or-  
		/// <paramref name="second" /> is less than zero or greater than 59.  
		/// -or-  
		/// <paramref name="millisecond" /> is less than zero or greater than 999.  
		/// -or-  
		/// <paramref name="era" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06005627 RID: 22055
		public abstract DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era);

		// Token: 0x06005628 RID: 22056 RVA: 0x00122BE4 File Offset: 0x00120DE4
		internal virtual bool TryToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era, out DateTime result)
		{
			result = DateTime.MinValue;
			bool result2;
			try
			{
				result = this.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
				result2 = true;
			}
			catch (ArgumentException)
			{
				result2 = false;
			}
			return result2;
		}

		// Token: 0x06005629 RID: 22057 RVA: 0x00017C70 File Offset: 0x00015E70
		internal virtual bool IsValidYear(int year, int era)
		{
			return year >= this.GetYear(this.MinSupportedDateTime) && year <= this.GetYear(this.MaxSupportedDateTime);
		}

		// Token: 0x0600562A RID: 22058 RVA: 0x00017C50 File Offset: 0x00015E50
		internal virtual bool IsValidMonth(int year, int month, int era)
		{
			return this.IsValidYear(year, era) && month >= 1 && month <= this.GetMonthsInYear(year, era);
		}

		// Token: 0x0600562B RID: 22059 RVA: 0x00017C2C File Offset: 0x00015E2C
		internal virtual bool IsValidDay(int year, int month, int day, int era)
		{
			return this.IsValidMonth(year, month, era) && day >= 1 && day <= this.GetDaysInMonth(year, month, era);
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Globalization.Calendar" /> object is read-only.</exception>
		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x0600562C RID: 22060 RVA: 0x00122C34 File Offset: 0x00120E34
		// (set) Token: 0x0600562D RID: 22061 RVA: 0x00122C3C File Offset: 0x00120E3C
		public virtual int TwoDigitYearMax
		{
			get
			{
				return this.twoDigitYearMax;
			}
			set
			{
				this.VerifyWritable();
				this.twoDigitYearMax = value;
			}
		}

		/// <summary>Converts the specified year to a four-digit year by using the <see cref="P:System.Globalization.Calendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <param name="year">A two-digit or four-digit integer that represents the year to convert.</param>
		/// <returns>An integer that contains the four-digit representation of <paramref name="year" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.</exception>
		// Token: 0x0600562E RID: 22062 RVA: 0x00122C4C File Offset: 0x00120E4C
		public virtual int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Non-negative number required."));
			}
			if (year < 100)
			{
				return (this.TwoDigitYearMax / 100 - ((year > this.TwoDigitYearMax % 100) ? 1 : 0)) * 100 + year;
			}
			return year;
		}

		// Token: 0x0600562F RID: 22063 RVA: 0x00122C98 File Offset: 0x00120E98
		internal static long TimeToTicks(int hour, int minute, int second, int millisecond)
		{
			if (hour < 0 || hour >= 24 || minute < 0 || minute >= 60 || second < 0 || second >= 60)
			{
				throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Hour, Minute, and Second parameters describe an un-representable DateTime."));
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 999));
			}
			return TimeSpan.TimeToTicks(hour, minute, second) + (long)millisecond * 10000L;
		}

		// Token: 0x06005630 RID: 22064 RVA: 0x00122D20 File Offset: 0x00120F20
		[SecuritySafeCritical]
		internal static int GetSystemTwoDigitYearSetting(int CalID, int defaultYearValue)
		{
			int num = CalendarData.nativeGetTwoDigitYearMax(CalID);
			if (num < 0)
			{
				num = defaultYearValue;
			}
			return num;
		}

		// Token: 0x04003586 RID: 13702
		internal const long TicksPerMillisecond = 10000L;

		// Token: 0x04003587 RID: 13703
		internal const long TicksPerSecond = 10000000L;

		// Token: 0x04003588 RID: 13704
		internal const long TicksPerMinute = 600000000L;

		// Token: 0x04003589 RID: 13705
		internal const long TicksPerHour = 36000000000L;

		// Token: 0x0400358A RID: 13706
		internal const long TicksPerDay = 864000000000L;

		// Token: 0x0400358B RID: 13707
		internal const int MillisPerSecond = 1000;

		// Token: 0x0400358C RID: 13708
		internal const int MillisPerMinute = 60000;

		// Token: 0x0400358D RID: 13709
		internal const int MillisPerHour = 3600000;

		// Token: 0x0400358E RID: 13710
		internal const int MillisPerDay = 86400000;

		// Token: 0x0400358F RID: 13711
		internal const int DaysPerYear = 365;

		// Token: 0x04003590 RID: 13712
		internal const int DaysPer4Years = 1461;

		// Token: 0x04003591 RID: 13713
		internal const int DaysPer100Years = 36524;

		// Token: 0x04003592 RID: 13714
		internal const int DaysPer400Years = 146097;

		// Token: 0x04003593 RID: 13715
		internal const int DaysTo10000 = 3652059;

		// Token: 0x04003594 RID: 13716
		internal const long MaxMillis = 315537897600000L;

		// Token: 0x04003595 RID: 13717
		internal const int CAL_GREGORIAN = 1;

		// Token: 0x04003596 RID: 13718
		internal const int CAL_GREGORIAN_US = 2;

		// Token: 0x04003597 RID: 13719
		internal const int CAL_JAPAN = 3;

		// Token: 0x04003598 RID: 13720
		internal const int CAL_TAIWAN = 4;

		// Token: 0x04003599 RID: 13721
		internal const int CAL_KOREA = 5;

		// Token: 0x0400359A RID: 13722
		internal const int CAL_HIJRI = 6;

		// Token: 0x0400359B RID: 13723
		internal const int CAL_THAI = 7;

		// Token: 0x0400359C RID: 13724
		internal const int CAL_HEBREW = 8;

		// Token: 0x0400359D RID: 13725
		internal const int CAL_GREGORIAN_ME_FRENCH = 9;

		// Token: 0x0400359E RID: 13726
		internal const int CAL_GREGORIAN_ARABIC = 10;

		// Token: 0x0400359F RID: 13727
		internal const int CAL_GREGORIAN_XLIT_ENGLISH = 11;

		// Token: 0x040035A0 RID: 13728
		internal const int CAL_GREGORIAN_XLIT_FRENCH = 12;

		// Token: 0x040035A1 RID: 13729
		internal const int CAL_JULIAN = 13;

		// Token: 0x040035A2 RID: 13730
		internal const int CAL_JAPANESELUNISOLAR = 14;

		// Token: 0x040035A3 RID: 13731
		internal const int CAL_CHINESELUNISOLAR = 15;

		// Token: 0x040035A4 RID: 13732
		internal const int CAL_SAKA = 16;

		// Token: 0x040035A5 RID: 13733
		internal const int CAL_LUNAR_ETO_CHN = 17;

		// Token: 0x040035A6 RID: 13734
		internal const int CAL_LUNAR_ETO_KOR = 18;

		// Token: 0x040035A7 RID: 13735
		internal const int CAL_LUNAR_ETO_ROKUYOU = 19;

		// Token: 0x040035A8 RID: 13736
		internal const int CAL_KOREANLUNISOLAR = 20;

		// Token: 0x040035A9 RID: 13737
		internal const int CAL_TAIWANLUNISOLAR = 21;

		// Token: 0x040035AA RID: 13738
		internal const int CAL_PERSIAN = 22;

		// Token: 0x040035AB RID: 13739
		internal const int CAL_UMALQURA = 23;

		// Token: 0x040035AC RID: 13740
		internal int m_currentEraValue = -1;

		// Token: 0x040035AD RID: 13741
		[OptionalField(VersionAdded = 2)]
		private bool m_isReadOnly;

		/// <summary>Represents the current era of the current calendar. The value of this field is 0.</summary>
		// Token: 0x040035AE RID: 13742
		public const int CurrentEra = 0;

		// Token: 0x040035AF RID: 13743
		internal int twoDigitYearMax = -1;
	}
}
