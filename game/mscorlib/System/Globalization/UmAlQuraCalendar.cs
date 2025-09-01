using System;

namespace System.Globalization
{
	/// <summary>Represents the Saudi Hijri (Um Al Qura) calendar.</summary>
	// Token: 0x0200099B RID: 2459
	[Serializable]
	public class UmAlQuraCalendar : Calendar
	{
		// Token: 0x06005872 RID: 22642 RVA: 0x0012A2BC File Offset: 0x001284BC
		private static UmAlQuraCalendar.DateMapping[] InitDateMapping()
		{
			short[] array = new short[]
			{
				746,
				1900,
				4,
				30,
				1769,
				1901,
				4,
				19,
				3794,
				1902,
				4,
				9,
				3748,
				1903,
				3,
				30,
				3402,
				1904,
				3,
				18,
				2710,
				1905,
				3,
				7,
				1334,
				1906,
				2,
				24,
				2741,
				1907,
				2,
				13,
				3498,
				1908,
				2,
				3,
				2980,
				1909,
				1,
				23,
				2889,
				1910,
				1,
				12,
				2707,
				1911,
				1,
				1,
				1323,
				1911,
				12,
				21,
				2647,
				1912,
				12,
				9,
				1206,
				1913,
				11,
				29,
				2741,
				1914,
				11,
				18,
				1450,
				1915,
				11,
				8,
				3413,
				1916,
				10,
				27,
				3370,
				1917,
				10,
				17,
				2646,
				1918,
				10,
				6,
				1198,
				1919,
				9,
				25,
				2397,
				1920,
				9,
				13,
				748,
				1921,
				9,
				3,
				1749,
				1922,
				8,
				23,
				1706,
				1923,
				8,
				13,
				1365,
				1924,
				8,
				1,
				1195,
				1925,
				7,
				21,
				2395,
				1926,
				7,
				10,
				698,
				1927,
				6,
				30,
				1397,
				1928,
				6,
				18,
				2994,
				1929,
				6,
				8,
				1892,
				1930,
				5,
				29,
				1865,
				1931,
				5,
				18,
				1621,
				1932,
				5,
				6,
				683,
				1933,
				4,
				25,
				1371,
				1934,
				4,
				14,
				2778,
				1935,
				4,
				4,
				1748,
				1936,
				3,
				24,
				3785,
				1937,
				3,
				13,
				3474,
				1938,
				3,
				3,
				3365,
				1939,
				2,
				20,
				2637,
				1940,
				2,
				9,
				685,
				1941,
				1,
				28,
				1389,
				1942,
				1,
				17,
				2922,
				1943,
				1,
				7,
				2898,
				1943,
				12,
				28,
				2725,
				1944,
				12,
				16,
				2635,
				1945,
				12,
				5,
				1175,
				1946,
				11,
				24,
				2359,
				1947,
				11,
				13,
				694,
				1948,
				11,
				2,
				1397,
				1949,
				10,
				22,
				3434,
				1950,
				10,
				12,
				3410,
				1951,
				10,
				2,
				2710,
				1952,
				9,
				20,
				2349,
				1953,
				9,
				9,
				605,
				1954,
				8,
				29,
				1245,
				1955,
				8,
				18,
				2778,
				1956,
				8,
				7,
				1492,
				1957,
				7,
				28,
				3497,
				1958,
				7,
				17,
				3410,
				1959,
				7,
				7,
				2730,
				1960,
				6,
				25,
				1238,
				1961,
				6,
				14,
				2486,
				1962,
				6,
				3,
				884,
				1963,
				5,
				24,
				1897,
				1964,
				5,
				12,
				1874,
				1965,
				5,
				2,
				1701,
				1966,
				4,
				21,
				1355,
				1967,
				4,
				10,
				2731,
				1968,
				3,
				29,
				1370,
				1969,
				3,
				19,
				2773,
				1970,
				3,
				8,
				3538,
				1971,
				2,
				26,
				3492,
				1972,
				2,
				16,
				3401,
				1973,
				2,
				4,
				2709,
				1974,
				1,
				24,
				1325,
				1975,
				1,
				13,
				2653,
				1976,
				1,
				2,
				1370,
				1976,
				12,
				22,
				2773,
				1977,
				12,
				11,
				1706,
				1978,
				12,
				1,
				1685,
				1979,
				11,
				20,
				1323,
				1980,
				11,
				8,
				2647,
				1981,
				10,
				28,
				1198,
				1982,
				10,
				18,
				2422,
				1983,
				10,
				7,
				1388,
				1984,
				9,
				26,
				2901,
				1985,
				9,
				15,
				2730,
				1986,
				9,
				5,
				2645,
				1987,
				8,
				25,
				1197,
				1988,
				8,
				13,
				2397,
				1989,
				8,
				2,
				730,
				1990,
				7,
				23,
				1497,
				1991,
				7,
				12,
				3506,
				1992,
				7,
				1,
				2980,
				1993,
				6,
				21,
				2890,
				1994,
				6,
				10,
				2645,
				1995,
				5,
				30,
				693,
				1996,
				5,
				18,
				1397,
				1997,
				5,
				7,
				2922,
				1998,
				4,
				27,
				3026,
				1999,
				4,
				17,
				3012,
				2000,
				4,
				6,
				2953,
				2001,
				3,
				26,
				2709,
				2002,
				3,
				15,
				1325,
				2003,
				3,
				4,
				1453,
				2004,
				2,
				21,
				2922,
				2005,
				2,
				10,
				1748,
				2006,
				1,
				31,
				3529,
				2007,
				1,
				20,
				3474,
				2008,
				1,
				10,
				2726,
				2008,
				12,
				29,
				2390,
				2009,
				12,
				18,
				686,
				2010,
				12,
				7,
				1389,
				2011,
				11,
				26,
				874,
				2012,
				11,
				15,
				2901,
				2013,
				11,
				4,
				2730,
				2014,
				10,
				25,
				2381,
				2015,
				10,
				14,
				1181,
				2016,
				10,
				2,
				2397,
				2017,
				9,
				21,
				698,
				2018,
				9,
				11,
				1461,
				2019,
				8,
				31,
				1450,
				2020,
				8,
				20,
				3413,
				2021,
				8,
				9,
				2714,
				2022,
				7,
				30,
				2350,
				2023,
				7,
				19,
				622,
				2024,
				7,
				7,
				1373,
				2025,
				6,
				26,
				2778,
				2026,
				6,
				16,
				1748,
				2027,
				6,
				6,
				1701,
				2028,
				5,
				25,
				1355,
				2029,
				5,
				14,
				2711,
				2030,
				5,
				3,
				1358,
				2031,
				4,
				23,
				2734,
				2032,
				4,
				11,
				1452,
				2033,
				4,
				1,
				2985,
				2034,
				3,
				21,
				3474,
				2035,
				3,
				11,
				2853,
				2036,
				2,
				28,
				1611,
				2037,
				2,
				16,
				3243,
				2038,
				2,
				5,
				1370,
				2039,
				1,
				26,
				2901,
				2040,
				1,
				15,
				1746,
				2041,
				1,
				4,
				3749,
				2041,
				12,
				24,
				3658,
				2042,
				12,
				14,
				2709,
				2043,
				12,
				3,
				1325,
				2044,
				11,
				21,
				2733,
				2045,
				11,
				10,
				876,
				2046,
				10,
				31,
				1881,
				2047,
				10,
				20,
				1746,
				2048,
				10,
				9,
				1685,
				2049,
				9,
				28,
				1325,
				2050,
				9,
				17,
				2651,
				2051,
				9,
				6,
				1210,
				2052,
				8,
				26,
				2490,
				2053,
				8,
				15,
				948,
				2054,
				8,
				5,
				2921,
				2055,
				7,
				25,
				2898,
				2056,
				7,
				14,
				2726,
				2057,
				7,
				3,
				1206,
				2058,
				6,
				22,
				2413,
				2059,
				6,
				11,
				748,
				2060,
				5,
				31,
				1753,
				2061,
				5,
				20,
				3762,
				2062,
				5,
				10,
				3412,
				2063,
				4,
				30,
				3370,
				2064,
				4,
				18,
				2646,
				2065,
				4,
				7,
				1198,
				2066,
				3,
				27,
				2413,
				2067,
				3,
				16,
				3434,
				2068,
				3,
				5,
				2900,
				2069,
				2,
				23,
				2857,
				2070,
				2,
				12,
				2707,
				2071,
				2,
				1,
				1323,
				2072,
				1,
				21,
				2647,
				2073,
				1,
				9,
				1334,
				2073,
				12,
				30,
				2741,
				2074,
				12,
				19,
				1706,
				2075,
				12,
				9,
				3731,
				2076,
				11,
				27,
				0,
				2077,
				11,
				17
			};
			UmAlQuraCalendar.DateMapping[] array2 = new UmAlQuraCalendar.DateMapping[array.Length / 4];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = new UmAlQuraCalendar.DateMapping((int)array[i * 4], (int)array[i * 4 + 1], (int)array[i * 4 + 2], (int)array[i * 4 + 3]);
			}
			return array2;
		}

		/// <summary>Gets the earliest date and time supported by this calendar.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class, which is equivalent to the first moment of April 30, 1900 C.E. in the Gregorian calendar.</returns>
		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06005873 RID: 22643 RVA: 0x0012A31F File Offset: 0x0012851F
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return UmAlQuraCalendar.minDate;
			}
		}

		/// <summary>Gets the latest date and time supported by this calendar.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class, which is equivalent to the last moment of November 16, 2077 C.E. in the Gregorian calendar.</returns>
		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06005874 RID: 22644 RVA: 0x0012A326 File Offset: 0x00128526
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return UmAlQuraCalendar.maxDate;
			}
		}

		/// <summary>Gets a value indicating whether the current calendar is solar-based, lunar-based, or a combination of both.</summary>
		/// <returns>Always returns <see cref="F:System.Globalization.CalendarAlgorithmType.LunarCalendar" />.</returns>
		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06005875 RID: 22645 RVA: 0x00015831 File Offset: 0x00013A31
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.LunarCalendar;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class.</summary>
		// Token: 0x06005876 RID: 22646 RVA: 0x00122020 File Offset: 0x00120220
		public UmAlQuraCalendar()
		{
		}

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06005877 RID: 22647 RVA: 0x000224A3 File Offset: 0x000206A3
		internal override int BaseCalendarID
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06005878 RID: 22648 RVA: 0x0012A32D File Offset: 0x0012852D
		internal override int ID
		{
			get
			{
				return 23;
			}
		}

		/// <summary>Gets the number of days in the year that precedes the year that is specified by the <see cref="P:System.Globalization.UmAlQuraCalendar.MinSupportedDateTime" /> property.</summary>
		/// <returns>The number of days in the year that precedes the year specified by <see cref="P:System.Globalization.UmAlQuraCalendar.MinSupportedDateTime" />.</returns>
		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06005879 RID: 22649 RVA: 0x0012A331 File Offset: 0x00128531
		protected override int DaysInYearBeforeMinSupportedYear
		{
			get
			{
				return 355;
			}
		}

		// Token: 0x0600587A RID: 22650 RVA: 0x0012A338 File Offset: 0x00128538
		private static void ConvertHijriToGregorian(int HijriYear, int HijriMonth, int HijriDay, ref int yg, ref int mg, ref int dg)
		{
			int num = HijriDay - 1;
			int num2 = HijriYear - 1318;
			DateTime dateTime = UmAlQuraCalendar.HijriYearInfo[num2].GregorianDate;
			int num3 = UmAlQuraCalendar.HijriYearInfo[num2].HijriMonthsLengthFlags;
			for (int i = 1; i < HijriMonth; i++)
			{
				num += 29 + (num3 & 1);
				num3 >>= 1;
			}
			dateTime = dateTime.AddDays((double)num);
			yg = dateTime.Year;
			mg = dateTime.Month;
			dg = dateTime.Day;
		}

		// Token: 0x0600587B RID: 22651 RVA: 0x0012A3B8 File Offset: 0x001285B8
		private static long GetAbsoluteDateUmAlQura(int year, int month, int day)
		{
			int year2 = 0;
			int month2 = 0;
			int day2 = 0;
			UmAlQuraCalendar.ConvertHijriToGregorian(year, month, day, ref year2, ref month2, ref day2);
			return GregorianCalendar.GetAbsoluteDate(year2, month2, day2);
		}

		// Token: 0x0600587C RID: 22652 RVA: 0x0012A3E4 File Offset: 0x001285E4
		internal static void CheckTicksRange(long ticks)
		{
			if (ticks < UmAlQuraCalendar.minDate.Ticks || ticks > UmAlQuraCalendar.maxDate.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Specified time is not supported in this calendar. It should be between {0} (Gregorian date) and {1} (Gregorian date), inclusive."), UmAlQuraCalendar.minDate, UmAlQuraCalendar.maxDate));
			}
		}

		// Token: 0x0600587D RID: 22653 RVA: 0x0012A43E File Offset: 0x0012863E
		internal static void CheckEraRange(int era)
		{
			if (era != 0 && era != 1)
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("Era value was not valid."));
			}
		}

		// Token: 0x0600587E RID: 22654 RVA: 0x0012A45C File Offset: 0x0012865C
		internal static void CheckYearRange(int year, int era)
		{
			UmAlQuraCalendar.CheckEraRange(era);
			if (year < 1318 || year > 1500)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1318, 1500));
			}
		}

		// Token: 0x0600587F RID: 22655 RVA: 0x0012A4B2 File Offset: 0x001286B2
		internal static void CheckYearMonthRange(int year, int month, int era)
		{
			UmAlQuraCalendar.CheckYearRange(year, era);
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("Month must be between one and twelve."));
			}
		}

		// Token: 0x06005880 RID: 22656 RVA: 0x0012A4DC File Offset: 0x001286DC
		private static void ConvertGregorianToHijri(DateTime time, ref int HijriYear, ref int HijriMonth, ref int HijriDay)
		{
			int num = (int)((time.Ticks - UmAlQuraCalendar.minDate.Ticks) / 864000000000L) / 355;
			while (time.CompareTo(UmAlQuraCalendar.HijriYearInfo[++num].GregorianDate) > 0)
			{
			}
			if (time.CompareTo(UmAlQuraCalendar.HijriYearInfo[num].GregorianDate) != 0)
			{
				num--;
			}
			TimeSpan timeSpan = time.Subtract(UmAlQuraCalendar.HijriYearInfo[num].GregorianDate);
			int num2 = num + 1318;
			int num3 = 1;
			int num4 = 1;
			double num5 = timeSpan.TotalDays;
			int num6 = UmAlQuraCalendar.HijriYearInfo[num].HijriMonthsLengthFlags;
			int num7 = 29 + (num6 & 1);
			while (num5 >= (double)num7)
			{
				num5 -= (double)num7;
				num6 >>= 1;
				num7 = 29 + (num6 & 1);
				num3++;
			}
			num4 += (int)num5;
			HijriDay = num4;
			HijriMonth = num3;
			HijriYear = num2;
		}

		// Token: 0x06005881 RID: 22657 RVA: 0x0012A5CC File Offset: 0x001287CC
		internal virtual int GetDatePart(DateTime time, int part)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			UmAlQuraCalendar.CheckTicksRange(time.Ticks);
			UmAlQuraCalendar.ConvertGregorianToHijri(time, ref num, ref num2, ref num3);
			if (part == 0)
			{
				return num;
			}
			if (part == 2)
			{
				return num2;
			}
			if (part == 3)
			{
				return num3;
			}
			if (part == 1)
			{
				return (int)(UmAlQuraCalendar.GetAbsoluteDateUmAlQura(num, num2, num3) - UmAlQuraCalendar.GetAbsoluteDateUmAlQura(num, 1, 1) + 1L);
			}
			throw new InvalidOperationException(Environment.GetResourceString("Internal Error in DateTime and Calendar operations."));
		}

		/// <summary>Calculates a date that is a specified number of months away from a specified initial date.</summary>
		/// <param name="time">The date to which to add months. The <see cref="T:System.Globalization.UmAlQuraCalendar" /> class supports only dates from 04/30/1900 00.00.00 (Gregorian date) through 11/16/2077 23:59:59 (Gregorian date).</param>
		/// <param name="months">The positive or negative number of months to add.</param>
		/// <returns>The date yielded by adding the number of months specified by the <paramref name="months" /> parameter to the date specified by the <paramref name="time" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting date is outside the range supported by the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is less than -120,000 or greater than 120,000.  
		/// -or-  
		/// <paramref name="time" /> is outside the range supported by this calendar.</exception>
		// Token: 0x06005882 RID: 22658 RVA: 0x0012A634 File Offset: 0x00128834
		public override DateTime AddMonths(DateTime time, int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), -120000, 120000));
			}
			int num = this.GetDatePart(time, 0);
			int num2 = this.GetDatePart(time, 2);
			int num3 = this.GetDatePart(time, 3);
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
			if (num3 > 29)
			{
				int daysInMonth = this.GetDaysInMonth(num, num2);
				if (num3 > daysInMonth)
				{
					num3 = daysInMonth;
				}
			}
			UmAlQuraCalendar.CheckYearRange(num, 1);
			DateTime result = new DateTime(UmAlQuraCalendar.GetAbsoluteDateUmAlQura(num, num2, num3) * 864000000000L + time.Ticks % 864000000000L);
			Calendar.CheckAddResult(result.Ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return result;
		}

		/// <summary>Calculates a date that is a specified number of years away from a specified initial date.</summary>
		/// <param name="time">The date to which to add years. The <see cref="T:System.Globalization.UmAlQuraCalendar" /> class supports only dates from 04/30/1900 00.00.00 (Gregorian date) through 11/16/2077 23:59:59 (Gregorian date).</param>
		/// <param name="years">The positive or negative number of years to add.</param>
		/// <returns>The date yielded by adding the number of years specified by the <paramref name="years" /> parameter to the date specified by the <paramref name="time" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting date is outside the range supported by the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="years" /> is less than -10,000 or greater than 10,000.  
		/// -or-  
		/// <paramref name="time" /> is outside the range supported by this calendar.</exception>
		// Token: 0x06005883 RID: 22659 RVA: 0x00122391 File Offset: 0x00120591
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.AddMonths(time, years * 12);
		}

		/// <summary>Calculates the day of the month on which a specified date occurs.</summary>
		/// <param name="time">The date value to read. The <see cref="T:System.Globalization.UmAlQuraCalendar" /> class supports only dates from 04/30/1900 00.00.00 (Gregorian date) through 11/16/2077 23:59:59 (Gregorian date).</param>
		/// <returns>An integer from 1 through 30 that represents the day of the month specified by the <paramref name="time" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is outside the range supported by this calendar.</exception>
		// Token: 0x06005884 RID: 22660 RVA: 0x0012A730 File Offset: 0x00128930
		public override int GetDayOfMonth(DateTime time)
		{
			return this.GetDatePart(time, 3);
		}

		/// <summary>Calculates the day of the week on which a specified date occurs.</summary>
		/// <param name="time">The date value to read. The <see cref="T:System.Globalization.UmAlQuraCalendar" /> class supports only dates from 04/30/1900 00.00.00 (Gregorian date) through 11/16/2077 23:59:59 (Gregorian date).</param>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week specified by the <paramref name="time" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is outside the range supported by this calendar.</exception>
		// Token: 0x06005885 RID: 22661 RVA: 0x001223AE File Offset: 0x001205AE
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		/// <summary>Calculates the day of the year on which a specified date occurs.</summary>
		/// <param name="time">The date value to read. The <see cref="T:System.Globalization.UmAlQuraCalendar" /> class supports only dates from 04/30/1900 00.00.00 (Gregorian date) through 11/16/2077 23:59:59 (Gregorian date).</param>
		/// <returns>An integer from 1 through 355 that represents the day of the year specified by the <paramref name="time" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is outside the range supported by this calendar.</exception>
		// Token: 0x06005886 RID: 22662 RVA: 0x0012A73A File Offset: 0x0012893A
		public override int GetDayOfYear(DateTime time)
		{
			return this.GetDatePart(time, 1);
		}

		/// <summary>Calculates the number of days in the specified month of the specified year and era.</summary>
		/// <param name="year">A year.</param>
		/// <param name="month">An integer from 1 through 12 that represents a month.</param>
		/// <param name="era">An era. Specify <see langword="UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra]" /> or <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.</param>
		/// <returns>The number of days in the specified month in the specified year and era. The return value is 29 in a common year and 30 in a leap year.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, or <paramref name="era" /> is outside the range supported by the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class.</exception>
		// Token: 0x06005887 RID: 22663 RVA: 0x0012A744 File Offset: 0x00128944
		public override int GetDaysInMonth(int year, int month, int era)
		{
			UmAlQuraCalendar.CheckYearMonthRange(year, month, era);
			if ((UmAlQuraCalendar.HijriYearInfo[year - 1318].HijriMonthsLengthFlags & 1 << month - 1) == 0)
			{
				return 29;
			}
			return 30;
		}

		// Token: 0x06005888 RID: 22664 RVA: 0x0012A774 File Offset: 0x00128974
		internal static int RealGetDaysInYear(int year)
		{
			int num = 0;
			int num2 = UmAlQuraCalendar.HijriYearInfo[year - 1318].HijriMonthsLengthFlags;
			for (int i = 1; i <= 12; i++)
			{
				num += 29 + (num2 & 1);
				num2 >>= 1;
			}
			return num;
		}

		/// <summary>Calculates the number of days in the specified year of the specified era.</summary>
		/// <param name="year">A year.</param>
		/// <param name="era">An era. Specify <see langword="UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra]" /> or <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.</param>
		/// <returns>The number of days in the specified year and era. The number of days is 354 in a common year or 355 in a leap year.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class.</exception>
		// Token: 0x06005889 RID: 22665 RVA: 0x0012A7B5 File Offset: 0x001289B5
		public override int GetDaysInYear(int year, int era)
		{
			UmAlQuraCalendar.CheckYearRange(year, era);
			return UmAlQuraCalendar.RealGetDaysInYear(year);
		}

		/// <summary>Calculates the era in which a specified date occurs.</summary>
		/// <param name="time">The date value to read.</param>
		/// <returns>Always returns the <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" /> value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is outside the range supported by this calendar.</exception>
		// Token: 0x0600588A RID: 22666 RVA: 0x0012A7C4 File Offset: 0x001289C4
		public override int GetEra(DateTime time)
		{
			UmAlQuraCalendar.CheckTicksRange(time.Ticks);
			return 1;
		}

		/// <summary>Gets a list of the eras that are supported by the current <see cref="T:System.Globalization.UmAlQuraCalendar" />.</summary>
		/// <returns>An array that consists of a single element having a value that is <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.</returns>
		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x0600588B RID: 22667 RVA: 0x001238ED File Offset: 0x00121AED
		public override int[] Eras
		{
			get
			{
				return new int[]
				{
					1
				};
			}
		}

		/// <summary>Calculates the month in which a specified date occurs.</summary>
		/// <param name="time">The date value to read. The <see cref="T:System.Globalization.UmAlQuraCalendar" /> class supports only dates from 04/30/1900 00.00.00 (Gregorian date) through 11/16/2077 23:59:59 (Gregorian date).</param>
		/// <returns>An integer from 1 through 12 that represents the month in the date specified by the <paramref name="time" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is outside the range supported by this calendar.</exception>
		// Token: 0x0600588C RID: 22668 RVA: 0x0012A7D3 File Offset: 0x001289D3
		public override int GetMonth(DateTime time)
		{
			return this.GetDatePart(time, 2);
		}

		/// <summary>Calculates the number of months in the specified year of the specified era.</summary>
		/// <param name="year">A year.</param>
		/// <param name="era">An era. Specify <see langword="UmAlQuaraCalendar.Eras[UmAlQuraCalendar.CurrentEra]" /> or <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.</param>
		/// <returns>Always 12.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by this calendar.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x0600588D RID: 22669 RVA: 0x0012A7DD File Offset: 0x001289DD
		public override int GetMonthsInYear(int year, int era)
		{
			UmAlQuraCalendar.CheckYearRange(year, era);
			return 12;
		}

		/// <summary>Calculates the year of a date represented by a specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The date value to read. The <see cref="T:System.Globalization.UmAlQuraCalendar" /> class supports only dates from 04/30/1900 00.00.00 (Gregorian date) through 11/16/2077 23:59:59 (Gregorian date).</param>
		/// <returns>An integer that represents the year specified by the <paramref name="time" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is outside the range supported by this calendar.</exception>
		// Token: 0x0600588E RID: 22670 RVA: 0x0012A7E8 File Offset: 0x001289E8
		public override int GetYear(DateTime time)
		{
			return this.GetDatePart(time, 0);
		}

		/// <summary>Determines whether the specified date is a leap day.</summary>
		/// <param name="year">A year.</param>
		/// <param name="month">An integer from 1 through 12 that represents a month.</param>
		/// <param name="day">An integer from 1 through 30 that represents a day.</param>
		/// <param name="era">An era. Specify <see langword="UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra]" /> or <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified day is a leap day; otherwise, <see langword="false" />. The return value is always <see langword="false" /> because the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class does not support leap days.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, or <paramref name="era" /> is outside the range supported by the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class.</exception>
		// Token: 0x0600588F RID: 22671 RVA: 0x0012A7F4 File Offset: 0x001289F4
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			if (day >= 1 && day <= 29)
			{
				UmAlQuraCalendar.CheckYearMonthRange(year, month, era);
				return false;
			}
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Day must be between 1 and {0} for month {1}."), daysInMonth, month));
			}
			return false;
		}

		/// <summary>Calculates the leap month for a specified year and era.</summary>
		/// <param name="year">A year.</param>
		/// <param name="era">An era. Specify <see langword="UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra]" /> or <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.</param>
		/// <returns>Always 0 because the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class does not support leap months.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1318 or greater than 1450.  
		/// -or-  
		/// <paramref name="era" /> is not <see langword="UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra]" /> or <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.</exception>
		// Token: 0x06005890 RID: 22672 RVA: 0x0012A854 File Offset: 0x00128A54
		public override int GetLeapMonth(int year, int era)
		{
			UmAlQuraCalendar.CheckYearRange(year, era);
			return 0;
		}

		/// <summary>Determines whether the specified month in the specified year and era is a leap month.</summary>
		/// <param name="year">A year.</param>
		/// <param name="month">An integer from 1 through 12 that represents a month.</param>
		/// <param name="era">An era. Specify <see langword="UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra]" /> or <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.</param>
		/// <returns>Always <see langword="false" /> because the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class does not support leap months.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, or <paramref name="era" /> is outside the range supported by the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class.</exception>
		// Token: 0x06005891 RID: 22673 RVA: 0x0012A85E File Offset: 0x00128A5E
		public override bool IsLeapMonth(int year, int month, int era)
		{
			UmAlQuraCalendar.CheckYearMonthRange(year, month, era);
			return false;
		}

		/// <summary>Determines whether the specified year in the specified era is a leap year.</summary>
		/// <param name="year">A year.</param>
		/// <param name="era">An era. Specify <see langword="UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra]" /> or <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified year is a leap year; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class.</exception>
		// Token: 0x06005892 RID: 22674 RVA: 0x0012A869 File Offset: 0x00128A69
		public override bool IsLeapYear(int year, int era)
		{
			UmAlQuraCalendar.CheckYearRange(year, era);
			return UmAlQuraCalendar.RealGetDaysInYear(year) == 355;
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is set to the specified date, time, and era.</summary>
		/// <param name="year">A year.</param>
		/// <param name="month">An integer from 1 through 12 that represents a month.</param>
		/// <param name="day">An integer from 1 through 29 that represents a day.</param>
		/// <param name="hour">An integer from 0 through 23 that represents an hour.</param>
		/// <param name="minute">An integer from 0 through 59 that represents a minute.</param>
		/// <param name="second">An integer from 0 through 59 that represents a second.</param>
		/// <param name="millisecond">An integer from 0 through 999 that represents a millisecond.</param>
		/// <param name="era">An era. Specify <see langword="UmAlQuraCalendar.Eras[UmAlQuraCalendar.CurrentEra]" /> or <see cref="F:System.Globalization.UmAlQuraCalendar.UmAlQuraEra" />.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that is set to the specified date and time in the current era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, or <paramref name="era" /> is outside the range supported by the <see cref="T:System.Globalization.UmAlQuraCalendar" /> class.  
		/// -or-  
		/// <paramref name="hour" /> is less than zero or greater than 23.  
		/// -or-  
		/// <paramref name="minute" /> is less than zero or greater than 59.  
		/// -or-  
		/// <paramref name="second" /> is less than zero or greater than 59.  
		/// -or-  
		/// <paramref name="millisecond" /> is less than zero or greater than 999.</exception>
		// Token: 0x06005893 RID: 22675 RVA: 0x0012A884 File Offset: 0x00128A84
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			if (day >= 1 && day <= 29)
			{
				UmAlQuraCalendar.CheckYearMonthRange(year, month, era);
			}
			else
			{
				int daysInMonth = this.GetDaysInMonth(year, month, era);
				if (day < 1 || day > daysInMonth)
				{
					throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Day must be between 1 and {0} for month {1}."), daysInMonth, month));
				}
			}
			long absoluteDateUmAlQura = UmAlQuraCalendar.GetAbsoluteDateUmAlQura(year, month, day);
			if (absoluteDateUmAlQura >= 0L)
			{
				return new DateTime(absoluteDateUmAlQura * 864000000000L + Calendar.TimeToTicks(hour, minute, second, millisecond));
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Year, Month, and Day parameters describe an un-representable DateTime."));
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <exception cref="T:System.InvalidOperationException">This calendar is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">In a set operation, the Um Al Qura calendar year value is less than 1318 but not 99, or is greater than 1450.</exception>
		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06005894 RID: 22676 RVA: 0x00126999 File Offset: 0x00124B99
		// (set) Token: 0x06005895 RID: 22677 RVA: 0x0012A920 File Offset: 0x00128B20
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 1451);
				}
				return this.twoDigitYearMax;
			}
			set
			{
				if (value != 99 && (value < 1318 || value > 1500))
				{
					throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1318, 1500));
				}
				base.VerifyWritable();
				this.twoDigitYearMax = value;
			}
		}

		/// <summary>Converts the specified year to a four-digit year by using the <see cref="P:System.Globalization.UmAlQuraCalendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <param name="year">A 2-digit year from 0 through 99, or a 4-digit Um Al Qura calendar year from 1318 through 1450.</param>
		/// <returns>If the <paramref name="year" /> parameter is a 2-digit year, the return value is the corresponding 4-digit year. If the <paramref name="year" /> parameter is a 4-digit year, the return value is the unchanged <paramref name="year" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by this calendar.</exception>
		// Token: 0x06005896 RID: 22678 RVA: 0x0012A984 File Offset: 0x00128B84
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
			if (year < 1318 || year > 1500)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1318, 1500));
			}
			return year;
		}

		// Token: 0x06005897 RID: 22679 RVA: 0x0012A9FC File Offset: 0x00128BFC
		// Note: this type is marked as 'beforefieldinit'.
		static UmAlQuraCalendar()
		{
		}

		// Token: 0x040036B9 RID: 14009
		internal const int MinCalendarYear = 1318;

		// Token: 0x040036BA RID: 14010
		internal const int MaxCalendarYear = 1500;

		// Token: 0x040036BB RID: 14011
		private static readonly UmAlQuraCalendar.DateMapping[] HijriYearInfo = UmAlQuraCalendar.InitDateMapping();

		/// <summary>Represents the current era. This field is constant.</summary>
		// Token: 0x040036BC RID: 14012
		public const int UmAlQuraEra = 1;

		// Token: 0x040036BD RID: 14013
		internal const int DateCycle = 30;

		// Token: 0x040036BE RID: 14014
		internal const int DatePartYear = 0;

		// Token: 0x040036BF RID: 14015
		internal const int DatePartDayOfYear = 1;

		// Token: 0x040036C0 RID: 14016
		internal const int DatePartMonth = 2;

		// Token: 0x040036C1 RID: 14017
		internal const int DatePartDay = 3;

		// Token: 0x040036C2 RID: 14018
		internal static DateTime minDate = new DateTime(1900, 4, 30);

		// Token: 0x040036C3 RID: 14019
		internal static DateTime maxDate = new DateTime(new DateTime(2077, 11, 16, 23, 59, 59, 999).Ticks + 9999L);

		// Token: 0x040036C4 RID: 14020
		private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 1451;

		// Token: 0x0200099C RID: 2460
		internal struct DateMapping
		{
			// Token: 0x06005898 RID: 22680 RVA: 0x0012AA57 File Offset: 0x00128C57
			internal DateMapping(int MonthsLengthFlags, int GYear, int GMonth, int GDay)
			{
				this.HijriMonthsLengthFlags = MonthsLengthFlags;
				this.GregorianDate = new DateTime(GYear, GMonth, GDay);
			}

			// Token: 0x040036C5 RID: 14021
			internal int HijriMonthsLengthFlags;

			// Token: 0x040036C6 RID: 14022
			internal DateTime GregorianDate;
		}
	}
}
