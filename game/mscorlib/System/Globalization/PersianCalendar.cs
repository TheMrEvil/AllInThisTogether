using System;

namespace System.Globalization
{
	/// <summary>Represents the Persian calendar.</summary>
	// Token: 0x02000982 RID: 2434
	[Serializable]
	public class PersianCalendar : Calendar
	{
		/// <summary>Gets the earliest date and time supported by the <see cref="T:System.Globalization.PersianCalendar" /> class.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.PersianCalendar" /> class.</returns>
		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x060055CF RID: 21967 RVA: 0x00122012 File Offset: 0x00120212
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return PersianCalendar.minDate;
			}
		}

		/// <summary>Gets the latest date and time supported by the <see cref="T:System.Globalization.PersianCalendar" /> class.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.PersianCalendar" /> class.</returns>
		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x060055D0 RID: 21968 RVA: 0x00122019 File Offset: 0x00120219
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return PersianCalendar.maxDate;
			}
		}

		/// <summary>Gets a value indicating whether the current calendar is solar-based, lunar-based, or lunisolar-based.</summary>
		/// <returns>Always returns <see cref="F:System.Globalization.CalendarAlgorithmType.SolarCalendar" />.</returns>
		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x060055D1 RID: 21969 RVA: 0x000040F7 File Offset: 0x000022F7
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.PersianCalendar" /> class.</summary>
		// Token: 0x060055D2 RID: 21970 RVA: 0x00122020 File Offset: 0x00120220
		public PersianCalendar()
		{
		}

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x060055D3 RID: 21971 RVA: 0x000040F7 File Offset: 0x000022F7
		internal override int BaseCalendarID
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x060055D4 RID: 21972 RVA: 0x00122028 File Offset: 0x00120228
		internal override int ID
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x060055D5 RID: 21973 RVA: 0x0012202C File Offset: 0x0012022C
		private long GetAbsoluteDatePersian(int year, int month, int day)
		{
			if (year >= 1 && year <= 9378 && month >= 1 && month <= 12)
			{
				int num = PersianCalendar.DaysInPreviousMonths(month) + day - 1;
				int num2 = (int)(365.242189 * (double)(year - 1));
				return CalendricalCalculationsHelper.PersianNewYearOnOrBefore(PersianCalendar.PersianEpoch + (long)num2 + 180L) + (long)num;
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Year, Month, and Day parameters describe an un-representable DateTime."));
		}

		// Token: 0x060055D6 RID: 21974 RVA: 0x00122094 File Offset: 0x00120294
		internal static void CheckTicksRange(long ticks)
		{
			if (ticks < PersianCalendar.minDate.Ticks || ticks > PersianCalendar.maxDate.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Specified time is not supported in this calendar. It should be between {0} (Gregorian date) and {1} (Gregorian date), inclusive."), PersianCalendar.minDate, PersianCalendar.maxDate));
			}
		}

		// Token: 0x060055D7 RID: 21975 RVA: 0x001220EE File Offset: 0x001202EE
		internal static void CheckEraRange(int era)
		{
			if (era != 0 && era != PersianCalendar.PersianEra)
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("Era value was not valid."));
			}
		}

		// Token: 0x060055D8 RID: 21976 RVA: 0x00122110 File Offset: 0x00120310
		internal static void CheckYearRange(int year, int era)
		{
			PersianCalendar.CheckEraRange(era);
			if (year < 1 || year > 9378)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, 9378));
			}
		}

		// Token: 0x060055D9 RID: 21977 RVA: 0x00122160 File Offset: 0x00120360
		internal static void CheckYearMonthRange(int year, int month, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			if (year == 9378 && month > 10)
			{
				throw new ArgumentOutOfRangeException("month", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, 10));
			}
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("Month must be between one and twelve."));
			}
		}

		// Token: 0x060055DA RID: 21978 RVA: 0x001221CC File Offset: 0x001203CC
		private static int MonthFromOrdinalDay(int ordinalDay)
		{
			int num = 0;
			while (ordinalDay > PersianCalendar.DaysToMonth[num])
			{
				num++;
			}
			return num;
		}

		// Token: 0x060055DB RID: 21979 RVA: 0x001221EC File Offset: 0x001203EC
		private static int DaysInPreviousMonths(int month)
		{
			month--;
			return PersianCalendar.DaysToMonth[month];
		}

		// Token: 0x060055DC RID: 21980 RVA: 0x001221FC File Offset: 0x001203FC
		internal int GetDatePart(long ticks, int part)
		{
			PersianCalendar.CheckTicksRange(ticks);
			long num = ticks / 864000000000L + 1L;
			int num2 = (int)Math.Floor((double)(CalendricalCalculationsHelper.PersianNewYearOnOrBefore(num) - PersianCalendar.PersianEpoch) / 365.242189 + 0.5) + 1;
			if (part == 0)
			{
				return num2;
			}
			int num3 = (int)(num - CalendricalCalculationsHelper.GetNumberOfDays(this.ToDateTime(num2, 1, 1, 0, 0, 0, 0, 1)));
			if (part == 1)
			{
				return num3;
			}
			int num4 = PersianCalendar.MonthFromOrdinalDay(num3);
			if (part == 2)
			{
				return num4;
			}
			int result = num3 - PersianCalendar.DaysInPreviousMonths(num4);
			if (part == 3)
			{
				return result;
			}
			throw new InvalidOperationException(Environment.GetResourceString("Internal Error in DateTime and Calendar operations."));
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> object that is offset the specified number of months from the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add months.</param>
		/// <param name="months">The positive or negative number of months to add.</param>
		/// <returns>A <see cref="T:System.DateTime" /> object that represents the date yielded by adding the number of months specified by the <paramref name="months" /> parameter to the date specified by the <paramref name="time" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is less than -120,000 or greater than 120,000.</exception>
		// Token: 0x060055DD RID: 21981 RVA: 0x00122298 File Offset: 0x00120498
		public override DateTime AddMonths(DateTime time, int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), -120000, 120000));
			}
			int num = this.GetDatePart(time.Ticks, 0);
			int num2 = this.GetDatePart(time.Ticks, 2);
			int num3 = this.GetDatePart(time.Ticks, 3);
			int num4 = num2 - 1 + months;
			if (num4 >= 0)
			{
				num2 = num4 % 12 + 1;
				num += num4 / 12;
			}
			else
			{
				num2 = 12 + (num4 + 1) % 12;
				num += (num4 - 11) / 12;
			}
			int daysInMonth = this.GetDaysInMonth(num, num2);
			if (num3 > daysInMonth)
			{
				num3 = daysInMonth;
			}
			long ticks = this.GetAbsoluteDatePersian(num, num2, num3) * 864000000000L + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(ticks);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> object that is offset the specified number of years from the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add years.</param>
		/// <param name="years">The positive or negative number of years to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> object that results from adding the specified number of years to the specified <see cref="T:System.DateTime" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="years" /> is less than -10,000 or greater than 10,000.</exception>
		// Token: 0x060055DE RID: 21982 RVA: 0x00122391 File Offset: 0x00120591
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.AddMonths(time, years * 12);
		}

		/// <summary>Returns the day of the month in the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 1 through 31 that represents the day of the month in the specified <see cref="T:System.DateTime" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="time" /> parameter represents a date less than <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x060055DF RID: 21983 RVA: 0x0012239E File Offset: 0x0012059E
		public override int GetDayOfMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 3);
		}

		/// <summary>Returns the day of the week in the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in the specified <see cref="T:System.DateTime" /> object.</returns>
		// Token: 0x060055E0 RID: 21984 RVA: 0x001223AE File Offset: 0x001205AE
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		/// <summary>Returns the day of the year in the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 1 through 366 that represents the day of the year in the specified <see cref="T:System.DateTime" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="time" /> parameter represents a date less than <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x060055E1 RID: 21985 RVA: 0x001223C7 File Offset: 0x001205C7
		public override int GetDayOfYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 1);
		}

		/// <summary>Returns the number of days in the specified month of the specified year and era.</summary>
		/// <param name="year">An integer from 1 through 9378 that represents the year.</param>
		/// <param name="month">An integer that represents the month, and ranges from 1 through 12 if <paramref name="year" /> is not 9378, or 1 through 10 if <paramref name="year" /> is 9378.</param>
		/// <param name="era">An integer from 0 through 1 that represents the era.</param>
		/// <returns>The number of days in the specified month of the specified year and era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x060055E2 RID: 21986 RVA: 0x001223D8 File Offset: 0x001205D8
		public override int GetDaysInMonth(int year, int month, int era)
		{
			PersianCalendar.CheckYearMonthRange(year, month, era);
			if (month == 10 && year == 9378)
			{
				return 13;
			}
			int num = PersianCalendar.DaysToMonth[month] - PersianCalendar.DaysToMonth[month - 1];
			if (month == 12 && !this.IsLeapYear(year))
			{
				num--;
			}
			return num;
		}

		/// <summary>Returns the number of days in the specified year of the specified era.</summary>
		/// <param name="year">An integer from 1 through 9378 that represents the year.</param>
		/// <param name="era">An integer from 0 through 1 that represents the era.</param>
		/// <returns>The number of days in the specified year and era. The number of days is 365 in a common year or 366 in a leap year.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x060055E3 RID: 21987 RVA: 0x00122422 File Offset: 0x00120622
		public override int GetDaysInYear(int year, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			if (year == 9378)
			{
				return PersianCalendar.DaysToMonth[9] + 13;
			}
			if (!this.IsLeapYear(year, 0))
			{
				return 365;
			}
			return 366;
		}

		/// <summary>Returns the era in the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>Always returns <see cref="F:System.Globalization.PersianCalendar.PersianEra" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="time" /> parameter represents a date less than <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x060055E4 RID: 21988 RVA: 0x00122454 File Offset: 0x00120654
		public override int GetEra(DateTime time)
		{
			PersianCalendar.CheckTicksRange(time.Ticks);
			return PersianCalendar.PersianEra;
		}

		/// <summary>Gets the list of eras in a <see cref="T:System.Globalization.PersianCalendar" /> object.</summary>
		/// <returns>An array of integers that represents the eras in a <see cref="T:System.Globalization.PersianCalendar" /> object. The array consists of a single element having a value of <see cref="F:System.Globalization.PersianCalendar.PersianEra" />.</returns>
		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x060055E5 RID: 21989 RVA: 0x00122467 File Offset: 0x00120667
		public override int[] Eras
		{
			get
			{
				return new int[]
				{
					PersianCalendar.PersianEra
				};
			}
		}

		/// <summary>Returns the month in the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 1 through 12 that represents the month in the specified <see cref="T:System.DateTime" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="time" /> parameter represents a date less than <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x060055E6 RID: 21990 RVA: 0x00122477 File Offset: 0x00120677
		public override int GetMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 2);
		}

		/// <summary>Returns the number of months in the specified year of the specified era.</summary>
		/// <param name="year">An integer from 1 through 9378 that represents the year.</param>
		/// <param name="era">An integer from 0 through 1 that represents the era.</param>
		/// <returns>Returns 10 if the <paramref name="year" /> parameter is 9378; otherwise, always returns 12.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x060055E7 RID: 21991 RVA: 0x00122487 File Offset: 0x00120687
		public override int GetMonthsInYear(int year, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			if (year == 9378)
			{
				return 10;
			}
			return 12;
		}

		/// <summary>Returns the year in the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 1 through 9378 that represents the year in the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="time" /> parameter represents a date less than <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x060055E8 RID: 21992 RVA: 0x0012249D File Offset: 0x0012069D
		public override int GetYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 0);
		}

		/// <summary>Determines whether the specified date is a leap day.</summary>
		/// <param name="year">An integer from 1 through 9378 that represents the year.</param>
		/// <param name="month">An integer that represents the month and ranges from 1 through 12 if <paramref name="year" /> is not 9378, or 1 through 10 if <paramref name="year" /> is 9378.</param>
		/// <param name="day">An integer from 1 through 31 that represents the day.</param>
		/// <param name="era">An integer from 0 through 1 that represents the era.</param>
		/// <returns>
		///   <see langword="true" /> if the specified day is a leap day; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x060055E9 RID: 21993 RVA: 0x001224B0 File Offset: 0x001206B0
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Day must be between 1 and {0} for month {1}."), daysInMonth, month));
			}
			return this.IsLeapYear(year, era) && month == 12 && day == 30;
		}

		/// <summary>Returns the leap month for a specified year and era.</summary>
		/// <param name="year">An integer from 1 through 9378 that represents the year to convert.</param>
		/// <param name="era">An integer from 0 through 1 that represents the era.</param>
		/// <returns>The return value is always 0.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x060055EA RID: 21994 RVA: 0x00122512 File Offset: 0x00120712
		public override int GetLeapMonth(int year, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			return 0;
		}

		/// <summary>Determines whether the specified month in the specified year and era is a leap month.</summary>
		/// <param name="year">An integer from 1 through 9378 that represents the year.</param>
		/// <param name="month">An integer that represents the month and ranges from 1 through 12 if <paramref name="year" /> is not 9378, or 1 through 10 if <paramref name="year" /> is 9378.</param>
		/// <param name="era">An integer from 0 through 1 that represents the era.</param>
		/// <returns>Always returns <see langword="false" /> because the <see cref="T:System.Globalization.PersianCalendar" /> class does not support the notion of a leap month.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x060055EB RID: 21995 RVA: 0x0012251C File Offset: 0x0012071C
		public override bool IsLeapMonth(int year, int month, int era)
		{
			PersianCalendar.CheckYearMonthRange(year, month, era);
			return false;
		}

		/// <summary>Determines whether the specified year in the specified era is a leap year.</summary>
		/// <param name="year">An integer from 1 through 9378 that represents the year.</param>
		/// <param name="era">An integer from 0 through 1 that represents the era.</param>
		/// <returns>
		///   <see langword="true" /> if the specified year is a leap year; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x060055EC RID: 21996 RVA: 0x00122527 File Offset: 0x00120727
		public override bool IsLeapYear(int year, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			return year != 9378 && this.GetAbsoluteDatePersian(year + 1, 1, 1) - this.GetAbsoluteDatePersian(year, 1, 1) == 366L;
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> object that is set to the specified date, time, and era.</summary>
		/// <param name="year">An integer from 1 through 9378 that represents the year.</param>
		/// <param name="month">An integer from 1 through 12 that represents the month.</param>
		/// <param name="day">An integer from 1 through 31 that represents the day.</param>
		/// <param name="hour">An integer from 0 through 23 that represents the hour.</param>
		/// <param name="minute">An integer from 0 through 59 that represents the minute.</param>
		/// <param name="second">An integer from 0 through 59 that represents the second.</param>
		/// <param name="millisecond">An integer from 0 through 999 that represents the millisecond.</param>
		/// <param name="era">An integer from 0 through 1 that represents the era.</param>
		/// <returns>A <see cref="T:System.DateTime" /> object that is set to the specified date and time in the current era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, <paramref name="hour" />, <paramref name="minute" />, <paramref name="second" />, <paramref name="millisecond" />, or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x060055ED RID: 21997 RVA: 0x00122558 File Offset: 0x00120758
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Day must be between 1 and {0} for month {1}."), daysInMonth, month));
			}
			long absoluteDatePersian = this.GetAbsoluteDatePersian(year, month, day);
			if (absoluteDatePersian >= 0L)
			{
				return new DateTime(absoluteDatePersian * 864000000000L + Calendar.TimeToTicks(hour, minute, second, millisecond));
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Year, Month, and Day parameters describe an un-representable DateTime."));
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <exception cref="T:System.InvalidOperationException">This calendar is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than 100 or greater than 9378.</exception>
		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x060055EE RID: 21998 RVA: 0x001225E1 File Offset: 0x001207E1
		// (set) Token: 0x060055EF RID: 21999 RVA: 0x00122608 File Offset: 0x00120808
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 1410);
				}
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > 9378)
				{
					throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 99, 9378));
				}
				this.twoDigitYearMax = value;
			}
		}

		/// <summary>Converts the specified year to a four-digit year representation.</summary>
		/// <param name="year">An integer from 1 through 9378 that represents the year to convert.</param>
		/// <returns>An integer that contains the four-digit representation of <paramref name="year" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 0 or greater than 9378.</exception>
		// Token: 0x060055F0 RID: 22000 RVA: 0x00122660 File Offset: 0x00120860
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Non-negative number required."));
			}
			if (year < 100)
			{
				return base.ToFourDigitYear(year);
			}
			if (year > 9378)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, 9378));
			}
			return year;
		}

		// Token: 0x060055F1 RID: 22001 RVA: 0x001226CC File Offset: 0x001208CC
		// Note: this type is marked as 'beforefieldinit'.
		static PersianCalendar()
		{
		}

		/// <summary>Represents the current era. This field is constant.</summary>
		// Token: 0x04003577 RID: 13687
		public static readonly int PersianEra = 1;

		// Token: 0x04003578 RID: 13688
		internal static long PersianEpoch = new DateTime(622, 3, 22).Ticks / 864000000000L;

		// Token: 0x04003579 RID: 13689
		private const int ApproximateHalfYear = 180;

		// Token: 0x0400357A RID: 13690
		internal const int DatePartYear = 0;

		// Token: 0x0400357B RID: 13691
		internal const int DatePartDayOfYear = 1;

		// Token: 0x0400357C RID: 13692
		internal const int DatePartMonth = 2;

		// Token: 0x0400357D RID: 13693
		internal const int DatePartDay = 3;

		// Token: 0x0400357E RID: 13694
		internal const int MonthsPerYear = 12;

		// Token: 0x0400357F RID: 13695
		internal static int[] DaysToMonth = new int[]
		{
			0,
			31,
			62,
			93,
			124,
			155,
			186,
			216,
			246,
			276,
			306,
			336,
			366
		};

		// Token: 0x04003580 RID: 13696
		internal const int MaxCalendarYear = 9378;

		// Token: 0x04003581 RID: 13697
		internal const int MaxCalendarMonth = 10;

		// Token: 0x04003582 RID: 13698
		internal const int MaxCalendarDay = 13;

		// Token: 0x04003583 RID: 13699
		internal static DateTime minDate = new DateTime(622, 3, 22);

		// Token: 0x04003584 RID: 13700
		internal static DateTime maxDate = DateTime.MaxValue;

		// Token: 0x04003585 RID: 13701
		private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 1410;
	}
}
