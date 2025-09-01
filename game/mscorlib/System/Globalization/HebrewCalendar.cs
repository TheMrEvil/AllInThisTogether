using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Represents the Hebrew calendar.</summary>
	// Token: 0x0200098C RID: 2444
	[ComVisible(true)]
	[Serializable]
	public class HebrewCalendar : Calendar
	{
		/// <summary>Gets the earliest date and time supported by the <see cref="T:System.Globalization.HebrewCalendar" /> type.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.HebrewCalendar" /> type, which is equivalent to the first moment of January, 1, 1583 C.E. in the Gregorian calendar.</returns>
		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x060056C1 RID: 22209 RVA: 0x0012592E File Offset: 0x00123B2E
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return HebrewCalendar.calendarMinValue;
			}
		}

		/// <summary>Gets the latest date and time supported by the <see cref="T:System.Globalization.HebrewCalendar" /> type.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.HebrewCalendar" /> type, which is equivalent to the last moment of September, 29, 2239 C.E. in the Gregorian calendar.</returns>
		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x060056C2 RID: 22210 RVA: 0x00125935 File Offset: 0x00123B35
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return HebrewCalendar.calendarMaxValue;
			}
		}

		/// <summary>Gets a value that indicates whether the current calendar is solar-based, lunar-based, or a combination of both.</summary>
		/// <returns>Always returns <see cref="F:System.Globalization.CalendarAlgorithmType.LunisolarCalendar" />.</returns>
		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x060056C3 RID: 22211 RVA: 0x000221D2 File Offset: 0x000203D2
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.LunisolarCalendar;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.HebrewCalendar" /> class.</summary>
		// Token: 0x060056C4 RID: 22212 RVA: 0x00122020 File Offset: 0x00120220
		public HebrewCalendar()
		{
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x060056C5 RID: 22213 RVA: 0x00047F71 File Offset: 0x00046171
		internal override int ID
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x060056C6 RID: 22214 RVA: 0x0012593C File Offset: 0x00123B3C
		private static void CheckHebrewYearValue(int y, int era, string varName)
		{
			HebrewCalendar.CheckEraRange(era);
			if (y > 5999 || y < 5343)
			{
				throw new ArgumentOutOfRangeException(varName, string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 5343, 5999));
			}
		}

		// Token: 0x060056C7 RID: 22215 RVA: 0x00125990 File Offset: 0x00123B90
		private void CheckHebrewMonthValue(int year, int month, int era)
		{
			int monthsInYear = this.GetMonthsInYear(year, era);
			if (month < 1 || month > monthsInYear)
			{
				throw new ArgumentOutOfRangeException("month", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, monthsInYear));
			}
		}

		// Token: 0x060056C8 RID: 22216 RVA: 0x001259DC File Offset: 0x00123BDC
		private void CheckHebrewDayValue(int year, int month, int day, int era)
		{
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, daysInMonth));
			}
		}

		// Token: 0x060056C9 RID: 22217 RVA: 0x00125A27 File Offset: 0x00123C27
		internal static void CheckEraRange(int era)
		{
			if (era != 0 && era != HebrewCalendar.HebrewEra)
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("Era value was not valid."));
			}
		}

		// Token: 0x060056CA RID: 22218 RVA: 0x00125A4C File Offset: 0x00123C4C
		private static void CheckTicksRange(long ticks)
		{
			if (ticks < HebrewCalendar.calendarMinValue.Ticks || ticks > HebrewCalendar.calendarMaxValue.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Specified time is not supported in this calendar. It should be between {0} (Gregorian date) and {1} (Gregorian date), inclusive."), HebrewCalendar.calendarMinValue, HebrewCalendar.calendarMaxValue));
			}
		}

		// Token: 0x060056CB RID: 22219 RVA: 0x00125AA6 File Offset: 0x00123CA6
		internal static int GetResult(HebrewCalendar.__DateBuffer result, int part)
		{
			switch (part)
			{
			case 0:
				return result.year;
			case 2:
				return result.month;
			case 3:
				return result.day;
			}
			throw new InvalidOperationException(Environment.GetResourceString("Internal Error in DateTime and Calendar operations."));
		}

		// Token: 0x060056CC RID: 22220 RVA: 0x00125AE4 File Offset: 0x00123CE4
		internal static int GetLunarMonthDay(int gregorianYear, HebrewCalendar.__DateBuffer lunarDate)
		{
			int num = gregorianYear - 1583;
			if (num < 0 || num > 656)
			{
				throw new ArgumentOutOfRangeException("gregorianYear");
			}
			num *= 2;
			lunarDate.day = HebrewCalendar.HebrewTable[num];
			int result = HebrewCalendar.HebrewTable[num + 1];
			int day = lunarDate.day;
			if (day != 0)
			{
				switch (day)
				{
				case 30:
					lunarDate.month = 3;
					break;
				case 31:
					lunarDate.month = 5;
					lunarDate.day = 2;
					break;
				case 32:
					lunarDate.month = 5;
					lunarDate.day = 3;
					break;
				case 33:
					lunarDate.month = 3;
					lunarDate.day = 29;
					break;
				default:
					lunarDate.month = 4;
					break;
				}
			}
			else
			{
				lunarDate.month = 5;
				lunarDate.day = 1;
			}
			return result;
		}

		// Token: 0x060056CD RID: 22221 RVA: 0x00125BA4 File Offset: 0x00123DA4
		internal virtual int GetDatePart(long ticks, int part)
		{
			HebrewCalendar.CheckTicksRange(ticks);
			DateTime dateTime = new DateTime(ticks);
			int year = dateTime.Year;
			int month = dateTime.Month;
			int day = dateTime.Day;
			HebrewCalendar.__DateBuffer _DateBuffer = new HebrewCalendar.__DateBuffer();
			_DateBuffer.year = year + 3760;
			int num = HebrewCalendar.GetLunarMonthDay(year, _DateBuffer);
			HebrewCalendar.__DateBuffer _DateBuffer2 = new HebrewCalendar.__DateBuffer();
			_DateBuffer2.year = _DateBuffer.year;
			_DateBuffer2.month = _DateBuffer.month;
			_DateBuffer2.day = _DateBuffer.day;
			long absoluteDate = GregorianCalendar.GetAbsoluteDate(year, month, day);
			if (month == 1 && day == 1)
			{
				return HebrewCalendar.GetResult(_DateBuffer2, part);
			}
			long num2 = absoluteDate - GregorianCalendar.GetAbsoluteDate(year, 1, 1);
			if (num2 + (long)_DateBuffer.day <= (long)HebrewCalendar.LunarMonthLen[num, _DateBuffer.month])
			{
				_DateBuffer2.day += (int)num2;
				return HebrewCalendar.GetResult(_DateBuffer2, part);
			}
			_DateBuffer2.month++;
			_DateBuffer2.day = 1;
			num2 -= (long)(HebrewCalendar.LunarMonthLen[num, _DateBuffer.month] - _DateBuffer.day);
			if (num2 > 1L)
			{
				while (num2 > (long)HebrewCalendar.LunarMonthLen[num, _DateBuffer2.month])
				{
					long num3 = num2;
					int[,] lunarMonthLen = HebrewCalendar.LunarMonthLen;
					int num4 = num;
					HebrewCalendar.__DateBuffer _DateBuffer3 = _DateBuffer2;
					int month2 = _DateBuffer3.month;
					_DateBuffer3.month = month2 + 1;
					num2 = num3 - (long)lunarMonthLen[num4, month2];
					if (_DateBuffer2.month > 13 || HebrewCalendar.LunarMonthLen[num, _DateBuffer2.month] == 0)
					{
						_DateBuffer2.year++;
						num = HebrewCalendar.HebrewTable[(year + 1 - 1583) * 2 + 1];
						_DateBuffer2.month = 1;
					}
				}
				_DateBuffer2.day += (int)(num2 - 1L);
			}
			return HebrewCalendar.GetResult(_DateBuffer2, part);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of months away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add <paramref name="months" />.</param>
		/// <param name="months">The number of months to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of months to the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is less than -120,000 or greater than 120,000.</exception>
		// Token: 0x060056CE RID: 22222 RVA: 0x00125D70 File Offset: 0x00123F70
		public override DateTime AddMonths(DateTime time, int months)
		{
			DateTime result;
			try
			{
				int num = this.GetDatePart(time.Ticks, 0);
				int datePart = this.GetDatePart(time.Ticks, 2);
				int num2 = this.GetDatePart(time.Ticks, 3);
				int i;
				if (months >= 0)
				{
					int monthsInYear;
					for (i = datePart + months; i > (monthsInYear = this.GetMonthsInYear(num, 0)); i -= monthsInYear)
					{
						num++;
					}
				}
				else if ((i = datePart + months) <= 0)
				{
					months = -months;
					months -= datePart;
					num--;
					int monthsInYear;
					while (months > (monthsInYear = this.GetMonthsInYear(num, 0)))
					{
						num--;
						months -= monthsInYear;
					}
					monthsInYear = this.GetMonthsInYear(num, 0);
					i = monthsInYear - months;
				}
				int daysInMonth = this.GetDaysInMonth(num, i);
				if (num2 > daysInMonth)
				{
					num2 = daysInMonth;
				}
				result = this.ToDateTime(num, i, num2, 0, 0, 0, 0);
				result = new DateTime(result.Ticks + time.Ticks % 864000000000L);
			}
			catch (ArgumentException)
			{
				throw new ArgumentOutOfRangeException("months", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Value to add was out of range."), Array.Empty<object>()));
			}
			return result;
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of years away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add <paramref name="years" />.</param>
		/// <param name="years">The number of years to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of years to the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range.</exception>
		// Token: 0x060056CF RID: 22223 RVA: 0x00125E88 File Offset: 0x00124088
		public override DateTime AddYears(DateTime time, int years)
		{
			int num = this.GetDatePart(time.Ticks, 0);
			int num2 = this.GetDatePart(time.Ticks, 2);
			int num3 = this.GetDatePart(time.Ticks, 3);
			num += years;
			HebrewCalendar.CheckHebrewYearValue(num, 0, "years");
			int monthsInYear = this.GetMonthsInYear(num, 0);
			if (num2 > monthsInYear)
			{
				num2 = monthsInYear;
			}
			int daysInMonth = this.GetDaysInMonth(num, num2);
			if (num3 > daysInMonth)
			{
				num3 = daysInMonth;
			}
			long ticks = this.ToDateTime(num, num2, num3, 0, 0, 0, 0).Ticks + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(ticks);
		}

		/// <summary>Returns the day of the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 1 to 30 that represents the day of the month in the specified <see cref="T:System.DateTime" />.</returns>
		// Token: 0x060056D0 RID: 22224 RVA: 0x00125F32 File Offset: 0x00124132
		public override int GetDayOfMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 3);
		}

		/// <summary>Returns the day of the week in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in the specified <see cref="T:System.DateTime" />.</returns>
		// Token: 0x060056D1 RID: 22225 RVA: 0x001223AE File Offset: 0x001205AE
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		// Token: 0x060056D2 RID: 22226 RVA: 0x00125F42 File Offset: 0x00124142
		internal static int GetHebrewYearType(int year, int era)
		{
			HebrewCalendar.CheckHebrewYearValue(year, era, "year");
			return HebrewCalendar.HebrewTable[(year - 3760 - 1583) * 2 + 1];
		}

		/// <summary>Returns the day of the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 1 to 385 that represents the day of the year in the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is earlier than September 17, 1583 in the Gregorian calendar, or greater than <see cref="P:System.Globalization.HebrewCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x060056D3 RID: 22227 RVA: 0x00125F68 File Offset: 0x00124168
		public override int GetDayOfYear(DateTime time)
		{
			int year = this.GetYear(time);
			DateTime dateTime;
			if (year == 5343)
			{
				dateTime = new DateTime(1582, 9, 27);
			}
			else
			{
				dateTime = this.ToDateTime(year, 1, 1, 0, 0, 0, 0, 0);
			}
			return (int)((time.Ticks - dateTime.Ticks) / 864000000000L) + 1;
		}

		/// <summary>Returns the number of days in the specified month in the specified year in the specified era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">An integer from 1 to 13 that represents the month.</param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see langword="Calendar.Eras[Calendar.CurrentEra]" />.</param>
		/// <returns>The number of days in the specified month in the specified year in the specified era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, or <paramref name="era" /> is outside the range supported by the current <see cref="T:System.Globalization.HebrewCalendar" /> object.</exception>
		// Token: 0x060056D4 RID: 22228 RVA: 0x00125FC4 File Offset: 0x001241C4
		public override int GetDaysInMonth(int year, int month, int era)
		{
			HebrewCalendar.CheckEraRange(era);
			int hebrewYearType = HebrewCalendar.GetHebrewYearType(year, era);
			this.CheckHebrewMonthValue(year, month, era);
			int num = HebrewCalendar.LunarMonthLen[hebrewYearType, month];
			if (num == 0)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("Month must be between one and twelve."));
			}
			return num;
		}

		/// <summary>Returns the number of days in the specified year in the specified era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see langword="HebrewCalendar.Eras[Calendar.CurrentEra]" />.</param>
		/// <returns>The number of days in the specified year in the specified era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by the current <see cref="T:System.Globalization.HebrewCalendar" /> object.</exception>
		// Token: 0x060056D5 RID: 22229 RVA: 0x0012600C File Offset: 0x0012420C
		public override int GetDaysInYear(int year, int era)
		{
			HebrewCalendar.CheckEraRange(era);
			int hebrewYearType = HebrewCalendar.GetHebrewYearType(year, era);
			if (hebrewYearType < 4)
			{
				return 352 + hebrewYearType;
			}
			return 382 + (hebrewYearType - 3);
		}

		/// <summary>Returns the era in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer that represents the era in the specified <see cref="T:System.DateTime" />. The return value is always <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" />.</returns>
		// Token: 0x060056D6 RID: 22230 RVA: 0x0012603C File Offset: 0x0012423C
		public override int GetEra(DateTime time)
		{
			return HebrewCalendar.HebrewEra;
		}

		/// <summary>Gets the list of eras in the <see cref="T:System.Globalization.HebrewCalendar" />.</summary>
		/// <returns>An array of integers that represents the eras in the <see cref="T:System.Globalization.HebrewCalendar" /> type. The return value is always an array containing one element equal to <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" />.</returns>
		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x060056D7 RID: 22231 RVA: 0x00126043 File Offset: 0x00124243
		public override int[] Eras
		{
			get
			{
				return new int[]
				{
					HebrewCalendar.HebrewEra
				};
			}
		}

		/// <summary>Returns the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 1 to 13 that represents the month in the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is less than <see cref="P:System.Globalization.HebrewCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.HebrewCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x060056D8 RID: 22232 RVA: 0x00126053 File Offset: 0x00124253
		public override int GetMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 2);
		}

		/// <summary>Returns the number of months in the specified year in the specified era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see langword="HebrewCalendar.Eras[Calendar.CurrentEra]" />.</param>
		/// <returns>The number of months in the specified year in the specified era. The return value is either 12 in a common year, or 13 in a leap year.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by the current <see cref="T:System.Globalization.HebrewCalendar" /> object.</exception>
		// Token: 0x060056D9 RID: 22233 RVA: 0x00126063 File Offset: 0x00124263
		public override int GetMonthsInYear(int year, int era)
		{
			if (!this.IsLeapYear(year, era))
			{
				return 12;
			}
			return 13;
		}

		/// <summary>Returns the year in the specified <see cref="T:System.DateTime" /> value.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer that represents the year in the specified <see cref="T:System.DateTime" /> value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is outside the range supported by the current <see cref="T:System.Globalization.HebrewCalendar" /> object.</exception>
		// Token: 0x060056DA RID: 22234 RVA: 0x00126074 File Offset: 0x00124274
		public override int GetYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 0);
		}

		/// <summary>Determines whether the specified date in the specified era is a leap day.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">An integer from 1 to 13 that represents the month.</param>
		/// <param name="day">An integer from 1 to 30 that represents the day.</param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see langword="HebrewCalendar.Eras[Calendar.CurrentEra]" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified day is a leap day; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x060056DB RID: 22235 RVA: 0x00126084 File Offset: 0x00124284
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			if (this.IsLeapMonth(year, month, era))
			{
				this.CheckHebrewDayValue(year, month, day, era);
				return true;
			}
			if (this.IsLeapYear(year, 0) && month == 6 && day == 30)
			{
				return true;
			}
			this.CheckHebrewDayValue(year, month, day, era);
			return false;
		}

		/// <summary>Calculates the leap month for a specified year and era.</summary>
		/// <param name="year">A year.</param>
		/// <param name="era">An era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see langword="HebrewCalendar.Eras[Calendar.CurrentEra]" />.</param>
		/// <returns>A positive integer that indicates the leap month in the specified year and era. The return value is 7 if the <paramref name="year" /> and <paramref name="era" /> parameters specify a leap year, or 0 if the year is not a leap year.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is not <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see langword="HebrewCalendar.Eras[Calendar.CurrentEra]" />.  
		/// -or-  
		/// <paramref name="year" /> is less than the Hebrew calendar year 5343 or greater than the Hebrew calendar year 5999.</exception>
		// Token: 0x060056DC RID: 22236 RVA: 0x001260C0 File Offset: 0x001242C0
		public override int GetLeapMonth(int year, int era)
		{
			if (this.IsLeapYear(year, era))
			{
				return 7;
			}
			return 0;
		}

		/// <summary>Determines whether the specified month in the specified year in the specified era is a leap month.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">An integer from 1 to 13 that represents the month.</param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see langword="HebrewCalendar.Eras[Calendar.CurrentEra]" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified month is a leap month; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x060056DD RID: 22237 RVA: 0x001260CF File Offset: 0x001242CF
		public override bool IsLeapMonth(int year, int month, int era)
		{
			bool flag = this.IsLeapYear(year, era);
			this.CheckHebrewMonthValue(year, month, era);
			return flag && month == 7;
		}

		/// <summary>Determines whether the specified year in the specified era is a leap year.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see langword="HebrewCalendar.Eras[Calendar.CurrentEra]" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified year is a leap year; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x060056DE RID: 22238 RVA: 0x001260EB File Offset: 0x001242EB
		public override bool IsLeapYear(int year, int era)
		{
			HebrewCalendar.CheckHebrewYearValue(year, era, "year");
			return (7L * (long)year + 1L) % 19L < 7L;
		}

		// Token: 0x060056DF RID: 22239 RVA: 0x0012610C File Offset: 0x0012430C
		private static int GetDayDifference(int lunarYearType, int month1, int day1, int month2, int day2)
		{
			if (month1 == month2)
			{
				return day1 - day2;
			}
			bool flag = month1 > month2;
			if (flag)
			{
				int num = month1;
				int num2 = day1;
				month1 = month2;
				day1 = day2;
				month2 = num;
				day2 = num2;
			}
			int num3 = HebrewCalendar.LunarMonthLen[lunarYearType, month1] - day1;
			month1++;
			while (month1 < month2)
			{
				num3 += HebrewCalendar.LunarMonthLen[lunarYearType, month1++];
			}
			num3 += day2;
			if (!flag)
			{
				return -num3;
			}
			return num3;
		}

		// Token: 0x060056E0 RID: 22240 RVA: 0x00126174 File Offset: 0x00124374
		private static DateTime HebrewToGregorian(int hebrewYear, int hebrewMonth, int hebrewDay, int hour, int minute, int second, int millisecond)
		{
			int num = hebrewYear - 3760;
			HebrewCalendar.__DateBuffer _DateBuffer = new HebrewCalendar.__DateBuffer();
			int lunarMonthDay = HebrewCalendar.GetLunarMonthDay(num, _DateBuffer);
			if (hebrewMonth == _DateBuffer.month && hebrewDay == _DateBuffer.day)
			{
				return new DateTime(num, 1, 1, hour, minute, second, millisecond);
			}
			int dayDifference = HebrewCalendar.GetDayDifference(lunarMonthDay, hebrewMonth, hebrewDay, _DateBuffer.month, _DateBuffer.day);
			DateTime dateTime = new DateTime(num, 1, 1);
			return new DateTime(dateTime.Ticks + (long)dayDifference * 864000000000L + Calendar.TimeToTicks(hour, minute, second, millisecond));
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is set to the specified date and time in the specified era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">An integer from 1 to 13 that represents the month.</param>
		/// <param name="day">An integer from 1 to 30 that represents the day.</param>
		/// <param name="hour">An integer from 0 to 23 that represents the hour.</param>
		/// <param name="minute">An integer from 0 to 59 that represents the minute.</param>
		/// <param name="second">An integer from 0 to 59 that represents the second.</param>
		/// <param name="millisecond">An integer from 0 to 999 that represents the millisecond.</param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see langword="HebrewCalendar.Eras[Calendar.CurrentEra]" />.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that is set to the specified date and time in the current era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" /> or <paramref name="era" /> is outside the range supported by the current <see cref="T:System.Globalization.HebrewCalendar" /> object.  
		/// -or-  
		/// <paramref name="hour" /> is less than 0 or greater than 23.  
		/// -or-  
		/// <paramref name="minute" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="second" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="millisecond" /> is less than 0 or greater than 999.</exception>
		// Token: 0x060056E1 RID: 22241 RVA: 0x00126200 File Offset: 0x00124400
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			HebrewCalendar.CheckHebrewYearValue(year, era, "year");
			this.CheckHebrewMonthValue(year, month, era);
			this.CheckHebrewDayValue(year, month, day, era);
			DateTime result = HebrewCalendar.HebrewToGregorian(year, month, day, hour, minute, second, millisecond);
			HebrewCalendar.CheckTicksRange(result.Ticks);
			return result;
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Globalization.HebrewCalendar" /> object is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">In a set operation, the Hebrew calendar year value is less than 5343 but is not 99, or the year value is greater than 5999.</exception>
		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x060056E2 RID: 22242 RVA: 0x0012624D File Offset: 0x0012444D
		// (set) Token: 0x060056E3 RID: 22243 RVA: 0x00126274 File Offset: 0x00124474
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 5790);
				}
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value != 99)
				{
					HebrewCalendar.CheckHebrewYearValue(value, HebrewCalendar.HebrewEra, "value");
				}
				this.twoDigitYearMax = value;
			}
		}

		/// <summary>Converts the specified year to a 4-digit year by using the <see cref="P:System.Globalization.HebrewCalendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <param name="year">A 2-digit year from 0 through 99, or a 4-digit Hebrew calendar year from 5343 through 5999.</param>
		/// <returns>If the <paramref name="year" /> parameter is a 2-digit year, the return value is the corresponding 4-digit year. If the <paramref name="year" /> parameter is a 4-digit year, the return value is the unchanged <paramref name="year" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 0.  
		/// -or-  
		/// <paramref name="year" /> is less than <see cref="P:System.Globalization.HebrewCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.HebrewCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x060056E4 RID: 22244 RVA: 0x00126298 File Offset: 0x00124498
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
			if (year > 5999 || year < 5343)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 5343, 5999));
			}
			return year;
		}

		// Token: 0x060056E5 RID: 22245 RVA: 0x00126310 File Offset: 0x00124510
		// Note: this type is marked as 'beforefieldinit'.
		static HebrewCalendar()
		{
		}

		/// <summary>Represents the current era. This field is constant.</summary>
		// Token: 0x04003616 RID: 13846
		public static readonly int HebrewEra = 1;

		// Token: 0x04003617 RID: 13847
		internal const int DatePartYear = 0;

		// Token: 0x04003618 RID: 13848
		internal const int DatePartDayOfYear = 1;

		// Token: 0x04003619 RID: 13849
		internal const int DatePartMonth = 2;

		// Token: 0x0400361A RID: 13850
		internal const int DatePartDay = 3;

		// Token: 0x0400361B RID: 13851
		internal const int DatePartDayOfWeek = 4;

		// Token: 0x0400361C RID: 13852
		private const int HebrewYearOf1AD = 3760;

		// Token: 0x0400361D RID: 13853
		private const int FirstGregorianTableYear = 1583;

		// Token: 0x0400361E RID: 13854
		private const int LastGregorianTableYear = 2239;

		// Token: 0x0400361F RID: 13855
		private const int TABLESIZE = 656;

		// Token: 0x04003620 RID: 13856
		private const int MinHebrewYear = 5343;

		// Token: 0x04003621 RID: 13857
		private const int MaxHebrewYear = 5999;

		// Token: 0x04003622 RID: 13858
		private static readonly int[] HebrewTable = new int[]
		{
			7,
			3,
			17,
			3,
			0,
			4,
			11,
			2,
			21,
			6,
			1,
			3,
			13,
			2,
			25,
			4,
			5,
			3,
			16,
			2,
			27,
			6,
			9,
			1,
			20,
			2,
			0,
			6,
			11,
			3,
			23,
			4,
			4,
			2,
			14,
			3,
			27,
			4,
			8,
			2,
			18,
			3,
			28,
			6,
			11,
			1,
			22,
			5,
			2,
			3,
			12,
			3,
			25,
			4,
			6,
			2,
			16,
			3,
			26,
			6,
			8,
			2,
			20,
			1,
			0,
			6,
			11,
			2,
			24,
			4,
			4,
			3,
			15,
			2,
			25,
			6,
			8,
			1,
			19,
			2,
			29,
			6,
			9,
			3,
			22,
			4,
			3,
			2,
			13,
			3,
			25,
			4,
			6,
			3,
			17,
			2,
			27,
			6,
			7,
			3,
			19,
			2,
			31,
			4,
			11,
			3,
			23,
			4,
			5,
			2,
			15,
			3,
			25,
			6,
			6,
			2,
			19,
			1,
			29,
			6,
			10,
			2,
			22,
			4,
			3,
			3,
			14,
			2,
			24,
			6,
			6,
			1,
			17,
			3,
			28,
			5,
			8,
			3,
			20,
			1,
			32,
			5,
			12,
			3,
			22,
			6,
			4,
			1,
			16,
			2,
			26,
			6,
			6,
			3,
			17,
			2,
			0,
			4,
			10,
			3,
			22,
			4,
			3,
			2,
			14,
			3,
			24,
			6,
			5,
			2,
			17,
			1,
			28,
			6,
			9,
			2,
			19,
			3,
			31,
			4,
			13,
			2,
			23,
			6,
			3,
			3,
			15,
			1,
			27,
			5,
			7,
			3,
			17,
			3,
			29,
			4,
			11,
			2,
			21,
			6,
			3,
			1,
			14,
			2,
			25,
			6,
			5,
			3,
			16,
			2,
			28,
			4,
			9,
			3,
			20,
			2,
			0,
			6,
			12,
			1,
			23,
			6,
			4,
			2,
			14,
			3,
			26,
			4,
			8,
			2,
			18,
			3,
			0,
			4,
			10,
			3,
			21,
			5,
			1,
			3,
			13,
			1,
			24,
			5,
			5,
			3,
			15,
			3,
			27,
			4,
			8,
			2,
			19,
			3,
			29,
			6,
			10,
			2,
			22,
			4,
			3,
			3,
			14,
			2,
			26,
			4,
			6,
			3,
			18,
			2,
			28,
			6,
			10,
			1,
			20,
			6,
			2,
			2,
			12,
			3,
			24,
			4,
			5,
			2,
			16,
			3,
			28,
			4,
			8,
			3,
			19,
			2,
			0,
			6,
			12,
			1,
			23,
			5,
			3,
			3,
			14,
			3,
			26,
			4,
			7,
			2,
			17,
			3,
			28,
			6,
			9,
			2,
			21,
			4,
			1,
			3,
			13,
			2,
			25,
			4,
			5,
			3,
			16,
			2,
			27,
			6,
			9,
			1,
			19,
			3,
			0,
			5,
			11,
			3,
			23,
			4,
			4,
			2,
			14,
			3,
			25,
			6,
			7,
			1,
			18,
			2,
			28,
			6,
			9,
			3,
			21,
			4,
			2,
			2,
			12,
			3,
			25,
			4,
			6,
			2,
			16,
			3,
			26,
			6,
			8,
			2,
			20,
			1,
			0,
			6,
			11,
			2,
			22,
			6,
			4,
			1,
			15,
			2,
			25,
			6,
			6,
			3,
			18,
			1,
			29,
			5,
			9,
			3,
			22,
			4,
			2,
			3,
			13,
			2,
			23,
			6,
			4,
			3,
			15,
			2,
			27,
			4,
			7,
			3,
			19,
			2,
			31,
			4,
			11,
			3,
			21,
			6,
			3,
			2,
			15,
			1,
			25,
			6,
			6,
			2,
			17,
			3,
			29,
			4,
			10,
			2,
			20,
			6,
			3,
			1,
			13,
			3,
			24,
			5,
			4,
			3,
			16,
			1,
			27,
			5,
			7,
			3,
			17,
			3,
			0,
			4,
			11,
			2,
			21,
			6,
			1,
			3,
			13,
			2,
			25,
			4,
			5,
			3,
			16,
			2,
			29,
			4,
			9,
			3,
			19,
			6,
			30,
			2,
			13,
			1,
			23,
			6,
			4,
			2,
			14,
			3,
			27,
			4,
			8,
			2,
			18,
			3,
			0,
			4,
			11,
			3,
			22,
			5,
			2,
			3,
			14,
			1,
			26,
			5,
			6,
			3,
			16,
			3,
			28,
			4,
			10,
			2,
			20,
			6,
			30,
			3,
			11,
			2,
			24,
			4,
			4,
			3,
			15,
			2,
			25,
			6,
			8,
			1,
			19,
			2,
			29,
			6,
			9,
			3,
			22,
			4,
			3,
			2,
			13,
			3,
			25,
			4,
			7,
			2,
			17,
			3,
			27,
			6,
			9,
			1,
			21,
			5,
			1,
			3,
			11,
			3,
			23,
			4,
			5,
			2,
			15,
			3,
			25,
			6,
			6,
			2,
			19,
			1,
			29,
			6,
			10,
			2,
			22,
			4,
			3,
			3,
			14,
			2,
			24,
			6,
			6,
			1,
			18,
			2,
			28,
			6,
			8,
			3,
			20,
			4,
			2,
			2,
			12,
			3,
			24,
			4,
			4,
			3,
			16,
			2,
			26,
			6,
			6,
			3,
			17,
			2,
			0,
			4,
			10,
			3,
			22,
			4,
			3,
			2,
			14,
			3,
			24,
			6,
			5,
			2,
			17,
			1,
			28,
			6,
			9,
			2,
			21,
			4,
			1,
			3,
			13,
			2,
			23,
			6,
			5,
			1,
			15,
			3,
			27,
			5,
			7,
			3,
			19,
			1,
			0,
			5,
			10,
			3,
			22,
			4,
			2,
			3,
			13,
			2,
			24,
			6,
			4,
			3,
			15,
			2,
			27,
			4,
			8,
			3,
			20,
			4,
			1,
			2,
			11,
			3,
			22,
			6,
			3,
			2,
			15,
			1,
			25,
			6,
			7,
			2,
			17,
			3,
			29,
			4,
			10,
			2,
			21,
			6,
			1,
			3,
			13,
			1,
			24,
			5,
			5,
			3,
			15,
			3,
			27,
			4,
			8,
			2,
			19,
			6,
			1,
			1,
			12,
			2,
			22,
			6,
			3,
			3,
			14,
			2,
			26,
			4,
			6,
			3,
			18,
			2,
			28,
			6,
			10,
			1,
			20,
			6,
			2,
			2,
			12,
			3,
			24,
			4,
			5,
			2,
			16,
			3,
			28,
			4,
			9,
			2,
			19,
			6,
			30,
			3,
			12,
			1,
			23,
			5,
			3,
			3,
			14,
			3,
			26,
			4,
			7,
			2,
			17,
			3,
			28,
			6,
			9,
			2,
			21,
			4,
			1,
			3,
			13,
			2,
			25,
			4,
			5,
			3,
			16,
			2,
			27,
			6,
			9,
			1,
			19,
			6,
			30,
			2,
			11,
			3,
			23,
			4,
			4,
			2,
			14,
			3,
			27,
			4,
			7,
			3,
			18,
			2,
			28,
			6,
			11,
			1,
			22,
			5,
			2,
			3,
			12,
			3,
			25,
			4,
			6,
			2,
			16,
			3,
			26,
			6,
			8,
			2,
			20,
			4,
			30,
			3,
			11,
			2,
			24,
			4,
			4,
			3,
			15,
			2,
			25,
			6,
			8,
			1,
			18,
			3,
			29,
			5,
			9,
			3,
			22,
			4,
			3,
			2,
			13,
			3,
			23,
			6,
			6,
			1,
			17,
			2,
			27,
			6,
			7,
			3,
			20,
			4,
			1,
			2,
			11,
			3,
			23,
			4,
			5,
			2,
			15,
			3,
			25,
			6,
			6,
			2,
			19,
			1,
			29,
			6,
			10,
			2,
			20,
			6,
			3,
			1,
			14,
			2,
			24,
			6,
			4,
			3,
			17,
			1,
			28,
			5,
			8,
			3,
			20,
			4,
			1,
			3,
			12,
			2,
			22,
			6,
			2,
			3,
			14,
			2,
			26,
			4,
			6,
			3,
			17,
			2,
			0,
			4,
			10,
			3,
			20,
			6,
			1,
			2,
			14,
			1,
			24,
			6,
			5,
			2,
			15,
			3,
			28,
			4,
			9,
			2,
			19,
			6,
			1,
			1,
			12,
			3,
			23,
			5,
			3,
			3,
			15,
			1,
			27,
			5,
			7,
			3,
			17,
			3,
			29,
			4,
			11,
			2,
			21,
			6,
			1,
			3,
			12,
			2,
			25,
			4,
			5,
			3,
			16,
			2,
			28,
			4,
			9,
			3,
			19,
			6,
			30,
			2,
			12,
			1,
			23,
			6,
			4,
			2,
			14,
			3,
			26,
			4,
			8,
			2,
			18,
			3,
			0,
			4,
			10,
			3,
			22,
			5,
			2,
			3,
			14,
			1,
			25,
			5,
			6,
			3,
			16,
			3,
			28,
			4,
			9,
			2,
			20,
			6,
			30,
			3,
			11,
			2,
			23,
			4,
			4,
			3,
			15,
			2,
			27,
			4,
			7,
			3,
			19,
			2,
			29,
			6,
			11,
			1,
			21,
			6,
			3,
			2,
			13,
			3,
			25,
			4,
			6,
			2,
			17,
			3,
			27,
			6,
			9,
			1,
			20,
			5,
			30,
			3,
			10,
			3,
			22,
			4,
			3,
			2,
			14,
			3,
			24,
			6,
			5,
			2,
			17,
			1,
			28,
			6,
			9,
			2,
			21,
			4,
			1,
			3,
			13,
			2,
			23,
			6,
			5,
			1,
			16,
			2,
			27,
			6,
			7,
			3,
			19,
			4,
			30,
			2,
			11,
			3,
			23,
			4,
			3,
			3,
			14,
			2,
			25,
			6,
			5,
			3,
			16,
			2,
			28,
			4,
			9,
			3,
			21,
			4,
			2,
			2,
			12,
			3,
			23,
			6,
			4,
			2,
			16,
			1,
			26,
			6,
			8,
			2,
			20,
			4,
			30,
			3,
			11,
			2,
			22,
			6,
			4,
			1,
			14,
			3,
			25,
			5,
			6,
			3,
			18,
			1,
			29,
			5,
			9,
			3,
			22,
			4,
			2,
			3,
			13,
			2,
			23,
			6,
			4,
			3,
			15,
			2,
			27,
			4,
			7,
			3,
			20,
			4,
			1,
			2,
			11,
			3,
			21,
			6,
			3,
			2,
			15,
			1,
			25,
			6,
			6,
			2,
			17,
			3,
			29,
			4,
			10,
			2,
			20,
			6,
			3,
			1,
			13,
			3,
			24,
			5,
			4,
			3,
			17,
			1,
			28,
			5,
			8,
			3,
			18,
			6,
			1,
			1,
			12,
			2,
			22,
			6,
			2,
			3,
			14,
			2,
			26,
			4,
			6,
			3,
			17,
			2,
			28,
			6,
			10,
			1,
			20,
			6,
			1,
			2,
			12,
			3,
			24,
			4,
			5,
			2,
			15,
			3,
			28,
			4,
			9,
			2,
			19,
			6,
			33,
			3,
			12,
			1,
			23,
			5,
			3,
			3,
			13,
			3,
			25,
			4,
			6,
			2,
			16,
			3,
			26,
			6,
			8,
			2,
			20,
			4,
			30,
			3,
			11,
			2,
			24,
			4,
			4,
			3,
			15,
			2,
			25,
			6,
			8,
			1,
			18,
			6,
			33,
			2,
			9,
			3,
			22,
			4,
			3,
			2,
			13,
			3,
			25,
			4,
			6,
			3,
			17,
			2,
			27,
			6,
			9,
			1,
			21,
			5,
			1,
			3,
			11,
			3,
			23,
			4,
			5,
			2,
			15,
			3,
			25,
			6,
			6,
			2,
			19,
			4,
			33,
			3,
			10,
			2,
			22,
			4,
			3,
			3,
			14,
			2,
			24,
			6,
			6,
			1
		};

		// Token: 0x04003623 RID: 13859
		private static readonly int[,] LunarMonthLen = new int[,]
		{
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			},
			{
				0,
				30,
				29,
				29,
				29,
				30,
				29,
				30,
				29,
				30,
				29,
				30,
				29,
				0
			},
			{
				0,
				30,
				29,
				30,
				29,
				30,
				29,
				30,
				29,
				30,
				29,
				30,
				29,
				0
			},
			{
				0,
				30,
				30,
				30,
				29,
				30,
				29,
				30,
				29,
				30,
				29,
				30,
				29,
				0
			},
			{
				0,
				30,
				29,
				29,
				29,
				30,
				30,
				29,
				30,
				29,
				30,
				29,
				30,
				29
			},
			{
				0,
				30,
				29,
				30,
				29,
				30,
				30,
				29,
				30,
				29,
				30,
				29,
				30,
				29
			},
			{
				0,
				30,
				30,
				30,
				29,
				30,
				30,
				29,
				30,
				29,
				30,
				29,
				30,
				29
			}
		};

		// Token: 0x04003624 RID: 13860
		internal static readonly DateTime calendarMinValue = new DateTime(1583, 1, 1);

		// Token: 0x04003625 RID: 13861
		internal static readonly DateTime calendarMaxValue = new DateTime(new DateTime(2239, 9, 29, 23, 59, 59, 999).Ticks + 9999L);

		// Token: 0x04003626 RID: 13862
		private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 5790;

		// Token: 0x0200098D RID: 2445
		internal class __DateBuffer
		{
			// Token: 0x060056E6 RID: 22246 RVA: 0x0000259F File Offset: 0x0000079F
			public __DateBuffer()
			{
			}

			// Token: 0x04003627 RID: 13863
			internal int year;

			// Token: 0x04003628 RID: 13864
			internal int month;

			// Token: 0x04003629 RID: 13865
			internal int day;
		}
	}
}
