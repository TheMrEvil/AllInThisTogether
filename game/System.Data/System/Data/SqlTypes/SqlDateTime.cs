using System;
using System.Data.Common;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents the date and time data ranging in value from January 1, 1753 to December 31, 9999 to an accuracy of 3.33 milliseconds to be stored in or retrieved from a database. The <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure has a different underlying data structure from its corresponding .NET Framework type, <see cref="T:System.DateTime" />, which can represent any time between 12:00:00 AM 1/1/0001 and 11:59:59 PM 12/31/9999, to the accuracy of 100 nanoseconds. <see cref="T:System.Data.SqlTypes.SqlDateTime" /> actually stores the relative difference to 00:00:00 AM 1/1/1900. Therefore, a conversion from "00:00:00 AM 1/1/1900" to an integer will return 0.</summary>
	// Token: 0x0200030D RID: 781
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlDateTime : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06002344 RID: 9028 RVA: 0x000A1883 File Offset: 0x0009FA83
		private SqlDateTime(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_day = 0;
			this.m_time = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure using the specified <see cref="T:System.DateTime" /> value.</summary>
		/// <param name="value">A <see langword="DateTime" /> structure.</param>
		// Token: 0x06002345 RID: 9029 RVA: 0x000A189A File Offset: 0x0009FA9A
		public SqlDateTime(DateTime value)
		{
			this = SqlDateTime.FromDateTime(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure using the supplied parameters to initialize the year, month, day.</summary>
		/// <param name="year">An integer representing the year of the of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="month">An integer value representing the month of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="day">An integer value representing the day number of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		// Token: 0x06002346 RID: 9030 RVA: 0x000A18A8 File Offset: 0x0009FAA8
		public SqlDateTime(int year, int month, int day)
		{
			this = new SqlDateTime(year, month, day, 0, 0, 0, 0.0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure using the supplied parameters to initialize the year, month, day, hour, minute, and second of the new structure.</summary>
		/// <param name="year">An integer value representing the year of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="month">An integer value representing the month of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="day">An integer value representing the day of the month of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="hour">An integer value representing the hour of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="minute">An integer value representing the minute of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="second">An integer value representing the second of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		// Token: 0x06002347 RID: 9031 RVA: 0x000A18BF File Offset: 0x0009FABF
		public SqlDateTime(int year, int month, int day, int hour, int minute, int second)
		{
			this = new SqlDateTime(year, month, day, hour, minute, second, 0.0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure using the supplied parameters to initialize the year, month, day, hour, minute, second, and millisecond of the new structure.</summary>
		/// <param name="year">An integer value representing the year of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="month">An integer value representing the month of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="day">An integer value representing the day of the month of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="hour">An integer value representing the hour of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="minute">An integer value representing the minute of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="second">An integer value representing the second of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="millisecond">An double value representing the millisecond of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		// Token: 0x06002348 RID: 9032 RVA: 0x000A18DC File Offset: 0x0009FADC
		public SqlDateTime(int year, int month, int day, int hour, int minute, int second, double millisecond)
		{
			if (year >= SqlDateTime.s_minYear && year <= SqlDateTime.s_maxYear && month >= 1 && month <= 12)
			{
				int[] array = SqlDateTime.IsLeapYear(year) ? SqlDateTime.s_daysToMonth366 : SqlDateTime.s_daysToMonth365;
				if (day >= 1 && day <= array[month] - array[month - 1])
				{
					int num = year - 1;
					int num2 = num * 365 + num / 4 - num / 100 + num / 400 + array[month - 1] + day - 1;
					num2 -= SqlDateTime.s_dayBase;
					if (num2 >= SqlDateTime.s_minDay && num2 <= SqlDateTime.s_maxDay && hour >= 0 && hour < 24 && minute >= 0 && minute < 60 && second >= 0 && second < 60 && millisecond >= 0.0 && millisecond < 1000.0)
					{
						double num3 = millisecond * SqlDateTime.s_SQLTicksPerMillisecond + 0.5;
						int num4 = hour * SqlDateTime.SQLTicksPerHour + minute * SqlDateTime.SQLTicksPerMinute + second * SqlDateTime.SQLTicksPerSecond + (int)num3;
						if (num4 > SqlDateTime.s_maxTime)
						{
							num4 = 0;
							num2++;
						}
						this = new SqlDateTime(num2, num4);
						return;
					}
				}
			}
			throw new SqlTypeException(SQLResource.InvalidDateTimeMessage);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure using the supplied parameters to initialize the year, month, day, hour, minute, second, and microsecond of the new structure.</summary>
		/// <param name="year">An integer value representing the year of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="month">An integer value representing the month of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="day">An integer value representing the day of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="hour">An integer value representing the hour of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="minute">An integer value representing the minute of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="second">An integer value representing the second of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="bilisecond">An integer value representing the microsecond (thousandths of a millisecond) of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		// Token: 0x06002349 RID: 9033 RVA: 0x000A1A1C File Offset: 0x0009FC1C
		public SqlDateTime(int year, int month, int day, int hour, int minute, int second, int bilisecond)
		{
			this = new SqlDateTime(year, month, day, hour, minute, second, (double)bilisecond / 1000.0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure using the supplied parameters.</summary>
		/// <param name="dayTicks">An integer value that represents the date as ticks.</param>
		/// <param name="timeTicks">An integer value that represents the time as ticks.</param>
		// Token: 0x0600234A RID: 9034 RVA: 0x000A1A48 File Offset: 0x0009FC48
		public SqlDateTime(int dayTicks, int timeTicks)
		{
			if (dayTicks < SqlDateTime.s_minDay || dayTicks > SqlDateTime.s_maxDay || timeTicks < SqlDateTime.s_minTime || timeTicks > SqlDateTime.s_maxTime)
			{
				this.m_fNotNull = false;
				throw new OverflowException(SQLResource.DateTimeOverflowMessage);
			}
			this.m_day = dayTicks;
			this.m_time = timeTicks;
			this.m_fNotNull = true;
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x000A1A9C File Offset: 0x0009FC9C
		internal SqlDateTime(double dblVal)
		{
			if (dblVal < (double)SqlDateTime.s_minDay || dblVal >= (double)(SqlDateTime.s_maxDay + 1))
			{
				throw new OverflowException(SQLResource.DateTimeOverflowMessage);
			}
			int num = (int)dblVal;
			int num2 = (int)((dblVal - (double)num) * (double)SqlDateTime.s_SQLTicksPerDay);
			if (num2 < 0)
			{
				num--;
				num2 += SqlDateTime.s_SQLTicksPerDay;
			}
			else if (num2 >= SqlDateTime.s_SQLTicksPerDay)
			{
				num++;
				num2 -= SqlDateTime.s_SQLTicksPerDay;
			}
			this = new SqlDateTime(num, num2);
		}

		/// <summary>Indicates whether this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure is null.</summary>
		/// <returns>
		///   <see langword="true" /> if null. Otherwise, <see langword="false" />.</returns>
		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x0600234C RID: 9036 RVA: 0x000A1B0B File Offset: 0x0009FD0B
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x000A1B18 File Offset: 0x0009FD18
		private static TimeSpan ToTimeSpan(SqlDateTime value)
		{
			long num = (long)((double)value.m_time / SqlDateTime.s_SQLTicksPerMillisecond + 0.5);
			return new TimeSpan((long)value.m_day * 864000000000L + num * 10000L);
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x000A1B5D File Offset: 0x0009FD5D
		private static DateTime ToDateTime(SqlDateTime value)
		{
			return SqlDateTime.s_SQLBaseDate.Add(SqlDateTime.ToTimeSpan(value));
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x000A1B70 File Offset: 0x0009FD70
		internal static DateTime ToDateTime(int daypart, int timepart)
		{
			if (daypart < SqlDateTime.s_minDay || daypart > SqlDateTime.s_maxDay || timepart < SqlDateTime.s_minTime || timepart > SqlDateTime.s_maxTime)
			{
				throw new OverflowException(SQLResource.DateTimeOverflowMessage);
			}
			long num = (long)daypart * 864000000000L;
			long num2 = (long)((double)timepart / SqlDateTime.s_SQLTicksPerMillisecond + 0.5) * 10000L;
			return new DateTime(SqlDateTime.s_SQLBaseDateTicks + num + num2);
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x000A1BE0 File Offset: 0x0009FDE0
		private static SqlDateTime FromTimeSpan(TimeSpan value)
		{
			if (value < SqlDateTime.s_minTimeSpan || value > SqlDateTime.s_maxTimeSpan)
			{
				throw new SqlTypeException(SQLResource.DateTimeOverflowMessage);
			}
			int num = value.Days;
			long num2 = value.Ticks - (long)num * 864000000000L;
			if (num2 < 0L)
			{
				num--;
				num2 += 864000000000L;
			}
			int num3 = (int)((double)num2 / 10000.0 * SqlDateTime.s_SQLTicksPerMillisecond + 0.5);
			if (num3 > SqlDateTime.s_maxTime)
			{
				num3 = 0;
				num++;
			}
			return new SqlDateTime(num, num3);
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x000A1C77 File Offset: 0x0009FE77
		private static SqlDateTime FromDateTime(DateTime value)
		{
			if (value == DateTime.MaxValue)
			{
				return SqlDateTime.MaxValue;
			}
			return SqlDateTime.FromTimeSpan(value.Subtract(SqlDateTime.s_SQLBaseDate));
		}

		/// <summary>Gets the value of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. This property is read-only.</summary>
		/// <returns>The value of this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The exception that is thrown when the <see langword="Value" /> property of a <see cref="N:System.Data.SqlTypes" /> structure is set to null.</exception>
		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06002352 RID: 9042 RVA: 0x000A1C9D File Offset: 0x0009FE9D
		public DateTime Value
		{
			get
			{
				if (this.m_fNotNull)
				{
					return SqlDateTime.ToDateTime(this);
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Gets the number of ticks representing the date of this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		/// <returns>The number of ticks representing the date that is contained in the <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> property of this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The exception that is thrown when the <see langword="Value" /> property of a <see cref="N:System.Data.SqlTypes" /> structure is set to null.</exception>
		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06002353 RID: 9043 RVA: 0x000A1CB8 File Offset: 0x0009FEB8
		public int DayTicks
		{
			get
			{
				if (this.m_fNotNull)
				{
					return this.m_day;
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Gets the number of ticks representing the time of this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		/// <returns>The number of ticks representing the time of this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</returns>
		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06002354 RID: 9044 RVA: 0x000A1CCE File Offset: 0x0009FECE
		public int TimeTicks
		{
			get
			{
				if (this.m_fNotNull)
				{
					return this.m_time;
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Converts a <see cref="T:System.DateTime" /> structure to a <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		/// <param name="value">A <see langword="DateTime" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> is equal to the combined <see cref="P:System.DateTime.Date" /> and <see cref="P:System.DateTime.TimeOfDay" /> properties of the supplied <see cref="T:System.DateTime" /> structure.</returns>
		// Token: 0x06002355 RID: 9045 RVA: 0x000A1CE4 File Offset: 0x0009FEE4
		public static implicit operator SqlDateTime(DateTime value)
		{
			return new SqlDateTime(value);
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure to a <see cref="T:System.DateTime" /> structure.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <returns>A <see cref="T:System.DateTime" /> object whose <see cref="P:System.DateTime.Date" /> and <see cref="P:System.DateTime.TimeOfDay" /> properties contain the same date and time values as the <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> property of the supplied <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</returns>
		// Token: 0x06002356 RID: 9046 RVA: 0x000A1CEC File Offset: 0x0009FEEC
		public static explicit operator DateTime(SqlDateTime x)
		{
			return SqlDateTime.ToDateTime(x);
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure to a <see cref="T:System.String" />.</summary>
		/// <returns>A <see langword="String" /> representing the <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> property of this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</returns>
		// Token: 0x06002357 RID: 9047 RVA: 0x000A1CF4 File Offset: 0x0009FEF4
		public override string ToString()
		{
			if (this.IsNull)
			{
				return SQLResource.NullString;
			}
			return SqlDateTime.ToDateTime(this).ToString(null);
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> representation of a date and time to its <see cref="T:System.Data.SqlTypes.SqlDateTime" /> equivalent.</summary>
		/// <param name="s">The <see langword="string" /> to be parsed.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure equal to the date and time represented by the specified <see langword="string" />.</returns>
		// Token: 0x06002358 RID: 9048 RVA: 0x000A1D24 File Offset: 0x0009FF24
		public static SqlDateTime Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlDateTime.Null;
			}
			DateTime value;
			try
			{
				value = DateTime.Parse(s, CultureInfo.InvariantCulture);
			}
			catch (FormatException)
			{
				DateTimeFormatInfo provider = (DateTimeFormatInfo)CultureInfo.CurrentCulture.GetFormat(typeof(DateTimeFormatInfo));
				value = DateTime.ParseExact(s, SqlDateTime.s_dateTimeFormats, provider, DateTimeStyles.AllowWhiteSpaces);
			}
			return new SqlDateTime(value);
		}

		/// <summary>Adds the period of time indicated by the supplied <see cref="T:System.TimeSpan" /> parameter, <paramref name="t" />, to the supplied <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="t">A <see cref="T:System.TimeSpan" /> structure.</param>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDateTime" />. If either argument is <see cref="F:System.Data.SqlTypes.SqlDateTime.Null" />, the new <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> is <see cref="F:System.Data.SqlTypes.SqlDateTime.Null" />.</returns>
		// Token: 0x06002359 RID: 9049 RVA: 0x000A1D94 File Offset: 0x0009FF94
		public static SqlDateTime operator +(SqlDateTime x, TimeSpan t)
		{
			if (!x.IsNull)
			{
				return SqlDateTime.FromDateTime(SqlDateTime.ToDateTime(x) + t);
			}
			return SqlDateTime.Null;
		}

		/// <summary>Subtracts the supplied <see cref="T:System.TimeSpan" /> structure, <paramref name="t" />, from the supplied <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="t">A <see cref="T:System.TimeSpan" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure representing the results of the subtraction.</returns>
		// Token: 0x0600235A RID: 9050 RVA: 0x000A1DB6 File Offset: 0x0009FFB6
		public static SqlDateTime operator -(SqlDateTime x, TimeSpan t)
		{
			if (!x.IsNull)
			{
				return SqlDateTime.FromDateTime(SqlDateTime.ToDateTime(x) - t);
			}
			return SqlDateTime.Null;
		}

		/// <summary>Adds a <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to the specified <see langword="TimeSpan" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> value.</param>
		/// <param name="t">A <see langword="Timespan" /> value.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> value.</returns>
		// Token: 0x0600235B RID: 9051 RVA: 0x000A1DD8 File Offset: 0x0009FFD8
		public static SqlDateTime Add(SqlDateTime x, TimeSpan t)
		{
			return x + t;
		}

		/// <summary>Subtracts the specified <see langword="Timespan" /> from this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> instance.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> value.</param>
		/// <param name="t">A <see langword="Timespan" /> value.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> value.</returns>
		// Token: 0x0600235C RID: 9052 RVA: 0x000A1DE1 File Offset: 0x0009FFE1
		public static SqlDateTime Subtract(SqlDateTime x, TimeSpan t)
		{
			return x - t;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlString" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlDateTime" />.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> is equal to the date and time represented by the <see cref="T:System.Data.SqlTypes.SqlString" /> parameter. If the <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the <see langword="Value" /> of the newly created <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure will be null.</returns>
		// Token: 0x0600235D RID: 9053 RVA: 0x000A1DEA File Offset: 0x0009FFEA
		public static explicit operator SqlDateTime(SqlString x)
		{
			if (!x.IsNull)
			{
				return SqlDateTime.Parse(x.Value);
			}
			return SqlDateTime.Null;
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x000A1E07 File Offset: 0x000A0007
		private static bool IsLeapYear(int year)
		{
			return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structures to determine whether they are equal.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <returns>
		///   <see langword="true" /> if the two values are equal. Otherwise, <see langword="false" />.</returns>
		// Token: 0x0600235F RID: 9055 RVA: 0x000A1E22 File Offset: 0x000A0022
		public static SqlBoolean operator ==(SqlDateTime x, SqlDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_day == y.m_day && x.m_time == y.m_time);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether they are not equal.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x06002360 RID: 9056 RVA: 0x000A1E60 File Offset: 0x000A0060
		public static SqlBoolean operator !=(SqlDateTime x, SqlDateTime y)
		{
			return !(x == y);
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is less than the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x06002361 RID: 9057 RVA: 0x000A1E70 File Offset: 0x000A0070
		public static SqlBoolean operator <(SqlDateTime x, SqlDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_day < y.m_day || (x.m_day == y.m_day && x.m_time < y.m_time));
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is greater than the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x06002362 RID: 9058 RVA: 0x000A1ECC File Offset: 0x000A00CC
		public static SqlBoolean operator >(SqlDateTime x, SqlDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_day > y.m_day || (x.m_day == y.m_day && x.m_time > y.m_time));
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is less than or equal to the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x06002363 RID: 9059 RVA: 0x000A1F28 File Offset: 0x000A0128
		public static SqlBoolean operator <=(SqlDateTime x, SqlDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_day < y.m_day || (x.m_day == y.m_day && x.m_time <= y.m_time));
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is greater than or equal to the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x06002364 RID: 9060 RVA: 0x000A1F88 File Offset: 0x000A0188
		public static SqlBoolean operator >=(SqlDateTime x, SqlDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_day > y.m_day || (x.m_day == y.m_day && x.m_time >= y.m_time));
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structures to determine whether they are equal.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <returns>
		///   <see langword="true" /> if the two values are equal. Otherwise, <see langword="false" />.</returns>
		// Token: 0x06002365 RID: 9061 RVA: 0x000A1FE5 File Offset: 0x000A01E5
		public static SqlBoolean Equals(SqlDateTime x, SqlDateTime y)
		{
			return x == y;
		}

		/// <summary>Performs a logical comparison of two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether they are not equal.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x06002366 RID: 9062 RVA: 0x000A1FEE File Offset: 0x000A01EE
		public static SqlBoolean NotEquals(SqlDateTime x, SqlDateTime y)
		{
			return x != y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is less than the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x06002367 RID: 9063 RVA: 0x000A1FF7 File Offset: 0x000A01F7
		public static SqlBoolean LessThan(SqlDateTime x, SqlDateTime y)
		{
			return x < y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is greater than the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x06002368 RID: 9064 RVA: 0x000A2000 File Offset: 0x000A0200
		public static SqlBoolean GreaterThan(SqlDateTime x, SqlDateTime y)
		{
			return x > y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is less than or equal to the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x06002369 RID: 9065 RVA: 0x000A2009 File Offset: 0x000A0209
		public static SqlBoolean LessThanOrEqual(SqlDateTime x, SqlDateTime y)
		{
			return x <= y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is greater than or equal to the second.</summary>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		// Token: 0x0600236A RID: 9066 RVA: 0x000A2012 File Offset: 0x000A0212
		public static SqlBoolean GreaterThanOrEqual(SqlDateTime x, SqlDateTime y)
		{
			return x >= y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A <see langword="SqlString" /> structure whose value is a string representing the date and time that is contained in this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</returns>
		// Token: 0x0600236B RID: 9067 RVA: 0x000A201B File Offset: 0x000A021B
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared.</param>
		/// <returns>A signed number that indicates the relative values of the instance and the object.  
		///   Return value  
		///
		///   Condition  
		///
		///   Less than zero  
		///
		///   This instance is less than the object.  
		///
		///   Zero  
		///
		///   This instance is the same as the object.  
		///
		///   Greater than zero  
		///
		///   This instance is greater than the object  
		///
		///  -or-  
		///
		///  The object is a null reference (<see langword="Nothing" /> as Visual Basic).</returns>
		// Token: 0x0600236C RID: 9068 RVA: 0x000A2028 File Offset: 0x000A0228
		public int CompareTo(object value)
		{
			if (value is SqlDateTime)
			{
				SqlDateTime value2 = (SqlDateTime)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlDateTime));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure to the supplied <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure and returns an indication of their relative values.</summary>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure to be compared.</param>
		/// <returns>A signed number that indicates the relative values of the instance and the object.  
		///   Return value  
		///
		///   Condition  
		///
		///   Less than zero  
		///
		///   This instance is less than <see cref="T:System.Data.SqlTypes.SqlDateTime" />.  
		///
		///   Zero  
		///
		///   This instance is the same as <see cref="T:System.Data.SqlTypes.SqlDateTime" />.  
		///
		///   Greater than zero  
		///
		///   This instance is greater than <see cref="T:System.Data.SqlTypes.SqlDateTime" />  
		///
		///  -or-  
		///
		///  <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is a null reference (<see langword="Nothing" /> in Visual Basic)</returns>
		// Token: 0x0600236D RID: 9069 RVA: 0x000A2064 File Offset: 0x000A0264
		public int CompareTo(SqlDateTime value)
		{
			if (this.IsNull)
			{
				if (!value.IsNull)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (value.IsNull)
				{
					return 1;
				}
				if (this < value)
				{
					return -1;
				}
				if (this > value)
				{
					return 1;
				}
				return 0;
			}
		}

		/// <summary>Compares the supplied object parameter to the <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> object.</summary>
		/// <param name="value">The object to be compared.</param>
		/// <returns>
		///   <see langword="true" /> if the object is an instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> and the two are equal; otherwise <see langword="false" />.</returns>
		// Token: 0x0600236E RID: 9070 RVA: 0x000A20BC File Offset: 0x000A02BC
		public override bool Equals(object value)
		{
			if (!(value is SqlDateTime))
			{
				return false;
			}
			SqlDateTime y = (SqlDateTime)value;
			if (y.IsNull || this.IsNull)
			{
				return y.IsNull && this.IsNull;
			}
			return (this == y).Value;
		}

		/// <summary>Gets the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600236F RID: 9071 RVA: 0x000A2114 File Offset: 0x000A0314
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An <see langword="XmlSchema" />.</returns>
		// Token: 0x06002370 RID: 9072 RVA: 0x00003E32 File Offset: 0x00002032
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">
		///   <see langword="XmlReader" />
		/// </param>
		// Token: 0x06002371 RID: 9073 RVA: 0x000A213C File Offset: 0x000A033C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			DateTime value = XmlConvert.ToDateTime(reader.ReadElementString(), XmlDateTimeSerializationMode.RoundtripKind);
			if (value.Kind != DateTimeKind.Unspecified)
			{
				throw new SqlTypeException(SQLResource.TimeZoneSpecifiedMessage);
			}
			SqlDateTime sqlDateTime = SqlDateTime.FromDateTime(value);
			this.m_day = sqlDateTime.DayTicks;
			this.m_time = sqlDateTime.TimeTicks;
			this.m_fNotNull = true;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">
		///   <see langword="XmlWriter" />
		/// </param>
		// Token: 0x06002372 RID: 9074 RVA: 0x000A21BD File Offset: 0x000A03BD
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this.Value, SqlDateTime.s_ISO8601_DateTimeFormat));
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		/// <returns>A <see langword="string" /> value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		// Token: 0x06002373 RID: 9075 RVA: 0x000A21F8 File Offset: 0x000A03F8
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("dateTime", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x000A220C File Offset: 0x000A040C
		// Note: this type is marked as 'beforefieldinit'.
		static SqlDateTime()
		{
		}

		// Token: 0x0400184A RID: 6218
		private bool m_fNotNull;

		// Token: 0x0400184B RID: 6219
		private int m_day;

		// Token: 0x0400184C RID: 6220
		private int m_time;

		// Token: 0x0400184D RID: 6221
		private static readonly double s_SQLTicksPerMillisecond = 0.3;

		/// <summary>A constant whose value is the number of ticks equivalent to one second.</summary>
		// Token: 0x0400184E RID: 6222
		public static readonly int SQLTicksPerSecond = 300;

		/// <summary>A constant whose value is the number of ticks equivalent to one minute.</summary>
		// Token: 0x0400184F RID: 6223
		public static readonly int SQLTicksPerMinute = SqlDateTime.SQLTicksPerSecond * 60;

		/// <summary>A constant whose value is the number of ticks equivalent to one hour.</summary>
		// Token: 0x04001850 RID: 6224
		public static readonly int SQLTicksPerHour = SqlDateTime.SQLTicksPerMinute * 60;

		// Token: 0x04001851 RID: 6225
		private static readonly int s_SQLTicksPerDay = SqlDateTime.SQLTicksPerHour * 24;

		// Token: 0x04001852 RID: 6226
		private static readonly long s_ticksPerSecond = 10000000L;

		// Token: 0x04001853 RID: 6227
		private static readonly DateTime s_SQLBaseDate = new DateTime(1900, 1, 1);

		// Token: 0x04001854 RID: 6228
		private static readonly long s_SQLBaseDateTicks = SqlDateTime.s_SQLBaseDate.Ticks;

		// Token: 0x04001855 RID: 6229
		private static readonly int s_minYear = 1753;

		// Token: 0x04001856 RID: 6230
		private static readonly int s_maxYear = 9999;

		// Token: 0x04001857 RID: 6231
		private static readonly int s_minDay = -53690;

		// Token: 0x04001858 RID: 6232
		private static readonly int s_maxDay = 2958463;

		// Token: 0x04001859 RID: 6233
		private static readonly int s_minTime = 0;

		// Token: 0x0400185A RID: 6234
		private static readonly int s_maxTime = SqlDateTime.s_SQLTicksPerDay - 1;

		// Token: 0x0400185B RID: 6235
		private static readonly int s_dayBase = 693595;

		// Token: 0x0400185C RID: 6236
		private static readonly int[] s_daysToMonth365 = new int[]
		{
			0,
			31,
			59,
			90,
			120,
			151,
			181,
			212,
			243,
			273,
			304,
			334,
			365
		};

		// Token: 0x0400185D RID: 6237
		private static readonly int[] s_daysToMonth366 = new int[]
		{
			0,
			31,
			60,
			91,
			121,
			152,
			182,
			213,
			244,
			274,
			305,
			335,
			366
		};

		// Token: 0x0400185E RID: 6238
		private static readonly DateTime s_minDateTime = new DateTime(1753, 1, 1);

		// Token: 0x0400185F RID: 6239
		private static readonly DateTime s_maxDateTime = DateTime.MaxValue;

		// Token: 0x04001860 RID: 6240
		private static readonly TimeSpan s_minTimeSpan = SqlDateTime.s_minDateTime.Subtract(SqlDateTime.s_SQLBaseDate);

		// Token: 0x04001861 RID: 6241
		private static readonly TimeSpan s_maxTimeSpan = SqlDateTime.s_maxDateTime.Subtract(SqlDateTime.s_SQLBaseDate);

		// Token: 0x04001862 RID: 6242
		private static readonly string s_ISO8601_DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

		// Token: 0x04001863 RID: 6243
		private static readonly string[] s_dateTimeFormats = new string[]
		{
			"MMM d yyyy hh:mm:ss:ffftt",
			"MMM d yyyy hh:mm:ss:fff",
			"d MMM yyyy hh:mm:ss:ffftt",
			"d MMM yyyy hh:mm:ss:fff",
			"hh:mm:ss:ffftt",
			"hh:mm:ss:fff",
			"yyMMdd",
			"yyyyMMdd"
		};

		// Token: 0x04001864 RID: 6244
		private const DateTimeStyles x_DateTimeStyle = DateTimeStyles.AllowWhiteSpaces;

		/// <summary>Represents the minimum valid date value for a <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		// Token: 0x04001865 RID: 6245
		public static readonly SqlDateTime MinValue = new SqlDateTime(SqlDateTime.s_minDay, 0);

		/// <summary>Represents the maximum valid date value for a <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		// Token: 0x04001866 RID: 6246
		public static readonly SqlDateTime MaxValue = new SqlDateTime(SqlDateTime.s_maxDay, SqlDateTime.s_maxTime);

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		// Token: 0x04001867 RID: 6247
		public static readonly SqlDateTime Null = new SqlDateTime(true);
	}
}
