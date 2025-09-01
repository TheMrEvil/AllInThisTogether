using System;
using System.Globalization;

namespace System
{
	/// <summary>Represents a time interval.</summary>
	// Token: 0x02000192 RID: 402
	[Serializable]
	public readonly struct TimeSpan : IComparable, IComparable<TimeSpan>, IEquatable<TimeSpan>, IFormattable, ISpanFormattable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.TimeSpan" /> structure to the specified number of ticks.</summary>
		/// <param name="ticks">A time period expressed in 100-nanosecond units.</param>
		// Token: 0x06000FCF RID: 4047 RVA: 0x00041A3B File Offset: 0x0003FC3B
		public TimeSpan(long ticks)
		{
			this._ticks = ticks;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeSpan" /> structure to a specified number of hours, minutes, and seconds.</summary>
		/// <param name="hours">Number of hours.</param>
		/// <param name="minutes">Number of minutes.</param>
		/// <param name="seconds">Number of seconds.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The parameters specify a <see cref="T:System.TimeSpan" /> value less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.</exception>
		// Token: 0x06000FD0 RID: 4048 RVA: 0x00041A44 File Offset: 0x0003FC44
		public TimeSpan(int hours, int minutes, int seconds)
		{
			this._ticks = TimeSpan.TimeToTicks(hours, minutes, seconds);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeSpan" /> structure to a specified number of days, hours, minutes, and seconds.</summary>
		/// <param name="days">Number of days.</param>
		/// <param name="hours">Number of hours.</param>
		/// <param name="minutes">Number of minutes.</param>
		/// <param name="seconds">Number of seconds.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The parameters specify a <see cref="T:System.TimeSpan" /> value less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.</exception>
		// Token: 0x06000FD1 RID: 4049 RVA: 0x00041A54 File Offset: 0x0003FC54
		public TimeSpan(int days, int hours, int minutes, int seconds)
		{
			this = new TimeSpan(days, hours, minutes, seconds, 0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeSpan" /> structure to a specified number of days, hours, minutes, seconds, and milliseconds.</summary>
		/// <param name="days">Number of days.</param>
		/// <param name="hours">Number of hours.</param>
		/// <param name="minutes">Number of minutes.</param>
		/// <param name="seconds">Number of seconds.</param>
		/// <param name="milliseconds">Number of milliseconds.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The parameters specify a <see cref="T:System.TimeSpan" /> value less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.</exception>
		// Token: 0x06000FD2 RID: 4050 RVA: 0x00041A64 File Offset: 0x0003FC64
		public TimeSpan(int days, int hours, int minutes, int seconds, int milliseconds)
		{
			long num = ((long)days * 3600L * 24L + (long)hours * 3600L + (long)minutes * 60L + (long)seconds) * 1000L + (long)milliseconds;
			if (num > 922337203685477L || num < -922337203685477L)
			{
				throw new ArgumentOutOfRangeException(null, "TimeSpan overflowed because the duration is too long.");
			}
			this._ticks = num * 10000L;
		}

		/// <summary>Gets the number of ticks that represent the value of the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The number of ticks contained in this instance.</returns>
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x00041AD1 File Offset: 0x0003FCD1
		public long Ticks
		{
			get
			{
				return this._ticks;
			}
		}

		/// <summary>Gets the days component of the time interval represented by the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The day component of this instance. The return value can be positive or negative.</returns>
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00041AD9 File Offset: 0x0003FCD9
		public int Days
		{
			get
			{
				return (int)(this._ticks / 864000000000L);
			}
		}

		/// <summary>Gets the hours component of the time interval represented by the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The hour component of the current <see cref="T:System.TimeSpan" /> structure. The return value ranges from -23 through 23.</returns>
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00041AEC File Offset: 0x0003FCEC
		public int Hours
		{
			get
			{
				return (int)(this._ticks / 36000000000L % 24L);
			}
		}

		/// <summary>Gets the milliseconds component of the time interval represented by the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The millisecond component of the current <see cref="T:System.TimeSpan" /> structure. The return value ranges from -999 through 999.</returns>
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x00041B03 File Offset: 0x0003FD03
		public int Milliseconds
		{
			get
			{
				return (int)(this._ticks / 10000L % 1000L);
			}
		}

		/// <summary>Gets the minutes component of the time interval represented by the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The minute component of the current <see cref="T:System.TimeSpan" /> structure. The return value ranges from -59 through 59.</returns>
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x00041B1A File Offset: 0x0003FD1A
		public int Minutes
		{
			get
			{
				return (int)(this._ticks / 600000000L % 60L);
			}
		}

		/// <summary>Gets the seconds component of the time interval represented by the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The second component of the current <see cref="T:System.TimeSpan" /> structure. The return value ranges from -59 through 59.</returns>
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x00041B2E File Offset: 0x0003FD2E
		public int Seconds
		{
			get
			{
				return (int)(this._ticks / 10000000L % 60L);
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional days.</summary>
		/// <returns>The total number of days represented by this instance.</returns>
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x00041B42 File Offset: 0x0003FD42
		public double TotalDays
		{
			get
			{
				return (double)this._ticks * 1.1574074074074074E-12;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional hours.</summary>
		/// <returns>The total number of hours represented by this instance.</returns>
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x00041B55 File Offset: 0x0003FD55
		public double TotalHours
		{
			get
			{
				return (double)this._ticks * 2.7777777777777777E-11;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional milliseconds.</summary>
		/// <returns>The total number of milliseconds represented by this instance.</returns>
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x00041B68 File Offset: 0x0003FD68
		public double TotalMilliseconds
		{
			get
			{
				double num = (double)this._ticks * 0.0001;
				if (num > 922337203685477.0)
				{
					return 922337203685477.0;
				}
				if (num < -922337203685477.0)
				{
					return -922337203685477.0;
				}
				return num;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional minutes.</summary>
		/// <returns>The total number of minutes represented by this instance.</returns>
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00041BB4 File Offset: 0x0003FDB4
		public double TotalMinutes
		{
			get
			{
				return (double)this._ticks * 1.6666666666666667E-09;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional seconds.</summary>
		/// <returns>The total number of seconds represented by this instance.</returns>
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x00041BC7 File Offset: 0x0003FDC7
		public double TotalSeconds
		{
			get
			{
				return (double)this._ticks * 1E-07;
			}
		}

		/// <summary>Returns a new <see cref="T:System.TimeSpan" /> object whose value is the sum of the specified <see cref="T:System.TimeSpan" /> object and this instance.</summary>
		/// <param name="ts">The time interval to add.</param>
		/// <returns>A new object that represents the value of this instance plus the value of <paramref name="ts" />.</returns>
		/// <exception cref="T:System.OverflowException">The resulting <see cref="T:System.TimeSpan" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.</exception>
		// Token: 0x06000FDE RID: 4062 RVA: 0x00041BDC File Offset: 0x0003FDDC
		public TimeSpan Add(TimeSpan ts)
		{
			long num = this._ticks + ts._ticks;
			if (this._ticks >> 63 == ts._ticks >> 63 && this._ticks >> 63 != num >> 63)
			{
				throw new OverflowException("TimeSpan overflowed because the duration is too long.");
			}
			return new TimeSpan(num);
		}

		/// <summary>Compares two <see cref="T:System.TimeSpan" /> values and returns an integer that indicates whether the first value is shorter than, equal to, or longer than the second value.</summary>
		/// <param name="t1">The first time interval to compare.</param>
		/// <param name="t2">The second time interval to compare.</param>
		/// <returns>One of the following values.  
		///   Value  
		///
		///   Description  
		///
		///   -1  
		///
		///  <paramref name="t1" /> is shorter than <paramref name="t2" />.  
		///
		///   0  
		///
		///  <paramref name="t1" /> is equal to <paramref name="t2" />.  
		///
		///   1  
		///
		///  <paramref name="t1" /> is longer than <paramref name="t2" />.</returns>
		// Token: 0x06000FDF RID: 4063 RVA: 0x00041C2B File Offset: 0x0003FE2B
		public static int Compare(TimeSpan t1, TimeSpan t2)
		{
			if (t1._ticks > t2._ticks)
			{
				return 1;
			}
			if (t1._ticks < t2._ticks)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Compares this instance to a specified object and returns an integer that indicates whether this instance is shorter than, equal to, or longer than the specified object.</summary>
		/// <param name="value">An object to compare, or <see langword="null" />.</param>
		/// <returns>One of the following values.  
		///   Value  
		///
		///   Description  
		///
		///   -1  
		///
		///   This instance is shorter than <paramref name="value" />.  
		///
		///   0  
		///
		///   This instance is equal to <paramref name="value" />.  
		///
		///   1  
		///
		///   This instance is longer than <paramref name="value" />.  
		///
		///  -or-  
		///
		///  <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.TimeSpan" />.</exception>
		// Token: 0x06000FE0 RID: 4064 RVA: 0x00041C50 File Offset: 0x0003FE50
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is TimeSpan))
			{
				throw new ArgumentException("Object must be of type TimeSpan.");
			}
			long ticks = ((TimeSpan)value)._ticks;
			if (this._ticks > ticks)
			{
				return 1;
			}
			if (this._ticks < ticks)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Compares this instance to a specified <see cref="T:System.TimeSpan" /> object and returns an integer that indicates whether this instance is shorter than, equal to, or longer than the <see cref="T:System.TimeSpan" /> object.</summary>
		/// <param name="value">An object to compare to this instance.</param>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.  
		///   Value  
		///
		///   Description  
		///
		///   A negative integer  
		///
		///   This instance is shorter than <paramref name="value" />.  
		///
		///   Zero  
		///
		///   This instance is equal to <paramref name="value" />.  
		///
		///   A positive integer  
		///
		///   This instance is longer than <paramref name="value" />.</returns>
		// Token: 0x06000FE1 RID: 4065 RVA: 0x00041C98 File Offset: 0x0003FE98
		public int CompareTo(TimeSpan value)
		{
			long ticks = value._ticks;
			if (this._ticks > ticks)
			{
				return 1;
			}
			if (this._ticks < ticks)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of days, where the specification is accurate to the nearest millisecond.</summary>
		/// <param name="value">A number of days, accurate to the nearest millisecond.</param>
		/// <returns>An object that represents <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />.</exception>
		// Token: 0x06000FE2 RID: 4066 RVA: 0x00041CC3 File Offset: 0x0003FEC3
		public static TimeSpan FromDays(double value)
		{
			return TimeSpan.Interval(value, 86400000);
		}

		/// <summary>Returns a new <see cref="T:System.TimeSpan" /> object whose value is the absolute value of the current <see cref="T:System.TimeSpan" /> object.</summary>
		/// <returns>A new object whose value is the absolute value of the current <see cref="T:System.TimeSpan" /> object.</returns>
		/// <exception cref="T:System.OverflowException">The value of this instance is <see cref="F:System.TimeSpan.MinValue" />.</exception>
		// Token: 0x06000FE3 RID: 4067 RVA: 0x00041CD0 File Offset: 0x0003FED0
		public TimeSpan Duration()
		{
			if (this.Ticks == TimeSpan.MinValue.Ticks)
			{
				throw new OverflowException("The duration cannot be returned for TimeSpan.MinValue because the absolute value of TimeSpan.MinValue exceeds the value of TimeSpan.MaxValue.");
			}
			return new TimeSpan((this._ticks >= 0L) ? this._ticks : (-this._ticks));
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <param name="value">An object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is a <see cref="T:System.TimeSpan" /> object that represents the same time interval as the current <see cref="T:System.TimeSpan" /> structure; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000FE4 RID: 4068 RVA: 0x00041D0D File Offset: 0x0003FF0D
		public override bool Equals(object value)
		{
			return value is TimeSpan && this._ticks == ((TimeSpan)value)._ticks;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified <see cref="T:System.TimeSpan" /> object.</summary>
		/// <param name="obj">An object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> represents the same time interval as this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000FE5 RID: 4069 RVA: 0x00041D2C File Offset: 0x0003FF2C
		public bool Equals(TimeSpan obj)
		{
			return this._ticks == obj._ticks;
		}

		/// <summary>Returns a value that indicates whether two specified instances of <see cref="T:System.TimeSpan" /> are equal.</summary>
		/// <param name="t1">The first time interval to compare.</param>
		/// <param name="t2">The second time interval to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the values of <paramref name="t1" /> and <paramref name="t2" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000FE6 RID: 4070 RVA: 0x00041D2C File Offset: 0x0003FF2C
		public static bool Equals(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks == t2._ticks;
		}

		/// <summary>Returns a hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000FE7 RID: 4071 RVA: 0x00041D3C File Offset: 0x0003FF3C
		public override int GetHashCode()
		{
			return (int)this._ticks ^ (int)(this._ticks >> 32);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of hours, where the specification is accurate to the nearest millisecond.</summary>
		/// <param name="value">A number of hours accurate to the nearest millisecond.</param>
		/// <returns>An object that represents <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />.</exception>
		// Token: 0x06000FE8 RID: 4072 RVA: 0x00041D50 File Offset: 0x0003FF50
		public static TimeSpan FromHours(double value)
		{
			return TimeSpan.Interval(value, 3600000);
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00041D60 File Offset: 0x0003FF60
		private static TimeSpan Interval(double value, int scale)
		{
			if (double.IsNaN(value))
			{
				throw new ArgumentException("TimeSpan does not accept floating point Not-a-Number values.");
			}
			double num = value * (double)scale + ((value >= 0.0) ? 0.5 : -0.5);
			if (num > 922337203685477.0 || num < -922337203685477.0)
			{
				throw new OverflowException("TimeSpan overflowed because the duration is too long.");
			}
			return new TimeSpan((long)num * 10000L);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of milliseconds.</summary>
		/// <param name="value">A number of milliseconds.</param>
		/// <returns>An object that represents <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />.</exception>
		// Token: 0x06000FEA RID: 4074 RVA: 0x00041DD7 File Offset: 0x0003FFD7
		public static TimeSpan FromMilliseconds(double value)
		{
			return TimeSpan.Interval(value, 1);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of minutes, where the specification is accurate to the nearest millisecond.</summary>
		/// <param name="value">A number of minutes, accurate to the nearest millisecond.</param>
		/// <returns>An object that represents <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />.</exception>
		// Token: 0x06000FEB RID: 4075 RVA: 0x00041DE0 File Offset: 0x0003FFE0
		public static TimeSpan FromMinutes(double value)
		{
			return TimeSpan.Interval(value, 60000);
		}

		/// <summary>Returns a new <see cref="T:System.TimeSpan" /> object whose value is the negated value of this instance.</summary>
		/// <returns>A new object with the same numeric value as this instance, but with the opposite sign.</returns>
		/// <exception cref="T:System.OverflowException">The negated value of this instance cannot be represented by a <see cref="T:System.TimeSpan" />; that is, the value of this instance is <see cref="F:System.TimeSpan.MinValue" />.</exception>
		// Token: 0x06000FEC RID: 4076 RVA: 0x00041DED File Offset: 0x0003FFED
		public TimeSpan Negate()
		{
			if (this.Ticks == TimeSpan.MinValue.Ticks)
			{
				throw new OverflowException("Negating the minimum value of a twos complement number is invalid.");
			}
			return new TimeSpan(-this._ticks);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of seconds, where the specification is accurate to the nearest millisecond.</summary>
		/// <param name="value">A number of seconds, accurate to the nearest millisecond.</param>
		/// <returns>An object that represents <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />.</exception>
		// Token: 0x06000FED RID: 4077 RVA: 0x00041E18 File Offset: 0x00040018
		public static TimeSpan FromSeconds(double value)
		{
			return TimeSpan.Interval(value, 1000);
		}

		/// <summary>Returns a new <see cref="T:System.TimeSpan" /> object whose value is the difference between the specified <see cref="T:System.TimeSpan" /> object and this instance.</summary>
		/// <param name="ts">The time interval to be subtracted.</param>
		/// <returns>A new time interval whose value is the result of the value of this instance minus the value of <paramref name="ts" />.</returns>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.</exception>
		// Token: 0x06000FEE RID: 4078 RVA: 0x00041E28 File Offset: 0x00040028
		public TimeSpan Subtract(TimeSpan ts)
		{
			long num = this._ticks - ts._ticks;
			if (this._ticks >> 63 != ts._ticks >> 63 && this._ticks >> 63 != num >> 63)
			{
				throw new OverflowException("TimeSpan overflowed because the duration is too long.");
			}
			return new TimeSpan(num);
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00041E77 File Offset: 0x00040077
		public TimeSpan Multiply(double factor)
		{
			return this * factor;
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00041E85 File Offset: 0x00040085
		public TimeSpan Divide(double divisor)
		{
			return this / divisor;
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00041E93 File Offset: 0x00040093
		public double Divide(TimeSpan ts)
		{
			return this / ts;
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified time, where the specification is in units of ticks.</summary>
		/// <param name="value">A number of ticks that represent a time.</param>
		/// <returns>An object that represents <paramref name="value" />.</returns>
		// Token: 0x06000FF2 RID: 4082 RVA: 0x00041EA1 File Offset: 0x000400A1
		public static TimeSpan FromTicks(long value)
		{
			return new TimeSpan(value);
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x00041EAC File Offset: 0x000400AC
		internal static long TimeToTicks(int hour, int minute, int second)
		{
			long num = (long)hour * 3600L + (long)minute * 60L + (long)second;
			if (num > 922337203685L || num < -922337203685L)
			{
				throw new ArgumentOutOfRangeException(null, "TimeSpan overflowed because the duration is too long.");
			}
			return num * 10000000L;
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00041EF9 File Offset: 0x000400F9
		private static void ValidateStyles(TimeSpanStyles style, string parameterName)
		{
			if (style != TimeSpanStyles.None && style != TimeSpanStyles.AssumeNegative)
			{
				throw new ArgumentException("An undefined TimeSpanStyles value is being used.", parameterName);
			}
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent.</summary>
		/// <param name="s">A string that specifies the time interval to convert.</param>
		/// <returns>A time interval that corresponds to <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> has an invalid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.  
		/// -or-  
		/// At least one of the days, hours, minutes, or seconds components is outside its valid range.</exception>
		// Token: 0x06000FF5 RID: 4085 RVA: 0x00041F0E File Offset: 0x0004010E
		public static TimeSpan Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			return TimeSpanParse.Parse(s, null);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified culture-specific format information.</summary>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A time interval that corresponds to <paramref name="input" />, as specified by <paramref name="formatProvider" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> has an invalid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="input" /> represents a number that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.  
		/// -or-  
		/// At least one of the days, hours, minutes, or seconds components in <paramref name="input" /> is outside its valid range.</exception>
		// Token: 0x06000FF6 RID: 4086 RVA: 0x00041F26 File Offset: 0x00040126
		public static TimeSpan Parse(string input, IFormatProvider formatProvider)
		{
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			return TimeSpanParse.Parse(input, formatProvider);
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00041F3E File Offset: 0x0004013E
		public static TimeSpan Parse(ReadOnlySpan<char> input, IFormatProvider formatProvider = null)
		{
			return TimeSpanParse.Parse(input, formatProvider);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified format and culture-specific format information. The format of the string representation must match the specified format exactly.</summary>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="format">A standard or custom format string that defines the required format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
		/// <returns>A time interval that corresponds to <paramref name="input" />, as specified by <paramref name="format" /> and <paramref name="formatProvider" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> has an invalid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="input" /> represents a number that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.  
		/// -or-  
		/// At least one of the days, hours, minutes, or seconds components in <paramref name="input" /> is outside its valid range.</exception>
		// Token: 0x06000FF8 RID: 4088 RVA: 0x00041F47 File Offset: 0x00040147
		public static TimeSpan ParseExact(string input, string format, IFormatProvider formatProvider)
		{
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			if (format == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);
			}
			return TimeSpanParse.ParseExact(input, format, formatProvider, TimeSpanStyles.None);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified array of format strings and culture-specific format information. The format of the string representation must match one of the specified formats exactly.</summary>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="formats">A array of standard or custom format strings that defines the required format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
		/// <returns>A time interval that corresponds to <paramref name="input" />, as specified by <paramref name="formats" /> and <paramref name="formatProvider" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> has an invalid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="input" /> represents a number that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.  
		/// -or-  
		/// At least one of the days, hours, minutes, or seconds components in <paramref name="input" /> is outside its valid range.</exception>
		// Token: 0x06000FF9 RID: 4089 RVA: 0x00041F70 File Offset: 0x00040170
		public static TimeSpan ParseExact(string input, string[] formats, IFormatProvider formatProvider)
		{
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			return TimeSpanParse.ParseExactMultiple(input, formats, formatProvider, TimeSpanStyles.None);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified format, culture-specific format information, and styles. The format of the string representation must match the specified format exactly.</summary>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="format">A standard or custom format string that defines the required format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
		/// <param name="styles">A bitwise combination of enumeration values that defines the style elements that may be present in <paramref name="input" />.</param>
		/// <returns>A time interval that corresponds to <paramref name="input" />, as specified by <paramref name="format" />, <paramref name="formatProvider" />, and <paramref name="styles" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> is an invalid <see cref="T:System.Globalization.TimeSpanStyles" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> has an invalid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="input" /> represents a number that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.  
		/// -or-  
		/// At least one of the days, hours, minutes, or seconds components in <paramref name="input" /> is outside its valid range.</exception>
		// Token: 0x06000FFA RID: 4090 RVA: 0x00041F8A File Offset: 0x0004018A
		public static TimeSpan ParseExact(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles)
		{
			TimeSpan.ValidateStyles(styles, "styles");
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			if (format == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);
			}
			return TimeSpanParse.ParseExact(input, format, formatProvider, styles);
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00041FBE File Offset: 0x000401BE
		public static TimeSpan ParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, IFormatProvider formatProvider, TimeSpanStyles styles = TimeSpanStyles.None)
		{
			TimeSpan.ValidateStyles(styles, "styles");
			return TimeSpanParse.ParseExact(input, format, formatProvider, styles);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified formats, culture-specific format information, and styles. The format of the string representation must match one of the specified formats exactly.</summary>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="formats">A array of standard or custom format strings that define the required format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
		/// <param name="styles">A bitwise combination of enumeration values that defines the style elements that may be present in input.</param>
		/// <returns>A time interval that corresponds to <paramref name="input" />, as specified by <paramref name="formats" />, <paramref name="formatProvider" />, and <paramref name="styles" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> is an invalid <see cref="T:System.Globalization.TimeSpanStyles" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> has an invalid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="input" /> represents a number that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.  
		/// -or-  
		/// At least one of the days, hours, minutes, or seconds components in <paramref name="input" /> is outside its valid range.</exception>
		// Token: 0x06000FFC RID: 4092 RVA: 0x00041FD4 File Offset: 0x000401D4
		public static TimeSpan ParseExact(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles)
		{
			TimeSpan.ValidateStyles(styles, "styles");
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			return TimeSpanParse.ParseExactMultiple(input, formats, formatProvider, styles);
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00041FF9 File Offset: 0x000401F9
		public static TimeSpan ParseExact(ReadOnlySpan<char> input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles = TimeSpanStyles.None)
		{
			TimeSpan.ValidateStyles(styles, "styles");
			return TimeSpanParse.ParseExactMultiple(input, formats, formatProvider, styles);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent and returns a value that indicates whether the conversion succeeded.</summary>
		/// <param name="s">A string that specifies the time interval to convert.</param>
		/// <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="s" />, or <see cref="F:System.TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="s" /> was converted successfully; otherwise, <see langword="false" />. This operation returns <see langword="false" /> if the <paramref name="s" /> parameter is <see langword="null" /> or <see cref="F:System.String.Empty" />, has an invalid format, represents a time interval that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />, or has at least one days, hours, minutes, or seconds component outside its valid range.</returns>
		// Token: 0x06000FFE RID: 4094 RVA: 0x0004200F File Offset: 0x0004020F
		public static bool TryParse(string s, out TimeSpan result)
		{
			if (s == null)
			{
				result = default(TimeSpan);
				return false;
			}
			return TimeSpanParse.TryParse(s, null, out result);
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0004202A File Offset: 0x0004022A
		public static bool TryParse(ReadOnlySpan<char> s, out TimeSpan result)
		{
			return TimeSpanParse.TryParse(s, null, out result);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified culture-specific formatting information, and returns a value that indicates whether the conversion succeeded.</summary>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
		/// <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="input" />, or <see cref="F:System.TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> was converted successfully; otherwise, <see langword="false" />. This operation returns <see langword="false" /> if the <paramref name="input" /> parameter is <see langword="null" /> or <see cref="F:System.String.Empty" />, has an invalid format, represents a time interval that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />, or has at least one days, hours, minutes, or seconds component outside its valid range.</returns>
		// Token: 0x06001000 RID: 4096 RVA: 0x00042034 File Offset: 0x00040234
		public static bool TryParse(string input, IFormatProvider formatProvider, out TimeSpan result)
		{
			if (input == null)
			{
				result = default(TimeSpan);
				return false;
			}
			return TimeSpanParse.TryParse(input, formatProvider, out result);
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0004204F File Offset: 0x0004024F
		public static bool TryParse(ReadOnlySpan<char> input, IFormatProvider formatProvider, out TimeSpan result)
		{
			return TimeSpanParse.TryParse(input, formatProvider, out result);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified format and culture-specific format information, and returns a value that indicates whether the conversion succeeded. The format of the string representation must match the specified format exactly.</summary>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="format">A standard or custom format string that defines the required format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
		/// <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="input" />, or <see cref="F:System.TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001002 RID: 4098 RVA: 0x00042059 File Offset: 0x00040259
		public static bool TryParseExact(string input, string format, IFormatProvider formatProvider, out TimeSpan result)
		{
			if (input == null || format == null)
			{
				result = default(TimeSpan);
				return false;
			}
			return TimeSpanParse.TryParseExact(input, format, formatProvider, TimeSpanStyles.None, out result);
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0004207E File Offset: 0x0004027E
		public static bool TryParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, IFormatProvider formatProvider, out TimeSpan result)
		{
			return TimeSpanParse.TryParseExact(input, format, formatProvider, TimeSpanStyles.None, out result);
		}

		/// <summary>Converts the specified string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified formats and culture-specific format information, and returns a value that indicates whether the conversion succeeded. The format of the string representation must match one of the specified formats exactly.</summary>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="formats">A array of standard or custom format strings that define the acceptable formats of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
		/// <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="input" />, or <see cref="F:System.TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001004 RID: 4100 RVA: 0x0004208A File Offset: 0x0004028A
		public static bool TryParseExact(string input, string[] formats, IFormatProvider formatProvider, out TimeSpan result)
		{
			if (input == null)
			{
				result = default(TimeSpan);
				return false;
			}
			return TimeSpanParse.TryParseExactMultiple(input, formats, formatProvider, TimeSpanStyles.None, out result);
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x000420A7 File Offset: 0x000402A7
		public static bool TryParseExact(ReadOnlySpan<char> input, string[] formats, IFormatProvider formatProvider, out TimeSpan result)
		{
			return TimeSpanParse.TryParseExactMultiple(input, formats, formatProvider, TimeSpanStyles.None, out result);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified format, culture-specific format information, and styles, and returns a value that indicates whether the conversion succeeded. The format of the string representation must match the specified format exactly.</summary>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="format">A standard or custom format string that defines the required format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
		/// <param name="styles">One or more enumeration values that indicate the style of <paramref name="input" />.</param>
		/// <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="input" />, or <see cref="F:System.TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001006 RID: 4102 RVA: 0x000420B3 File Offset: 0x000402B3
		public static bool TryParseExact(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpan.ValidateStyles(styles, "styles");
			if (input == null || format == null)
			{
				result = default(TimeSpan);
				return false;
			}
			return TimeSpanParse.TryParseExact(input, format, formatProvider, styles, out result);
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x000420E5 File Offset: 0x000402E5
		public static bool TryParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpan.ValidateStyles(styles, "styles");
			return TimeSpanParse.TryParseExact(input, format, formatProvider, styles, out result);
		}

		/// <summary>Converts the specified string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified formats, culture-specific format information, and styles, and returns a value that indicates whether the conversion succeeded. The format of the string representation must match one of the specified formats exactly.</summary>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="formats">A array of standard or custom format strings that define the acceptable formats of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
		/// <param name="styles">One or more enumeration values that indicate the style of <paramref name="input" />.</param>
		/// <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="input" />, or <see cref="F:System.TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001008 RID: 4104 RVA: 0x000420FD File Offset: 0x000402FD
		public static bool TryParseExact(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpan.ValidateStyles(styles, "styles");
			if (input == null)
			{
				result = default(TimeSpan);
				return false;
			}
			return TimeSpanParse.TryParseExactMultiple(input, formats, formatProvider, styles, out result);
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x00042127 File Offset: 0x00040327
		public static bool TryParseExact(ReadOnlySpan<char> input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpan.ValidateStyles(styles, "styles");
			return TimeSpanParse.TryParseExactMultiple(input, formats, formatProvider, styles, out result);
		}

		/// <summary>Converts the value of the current <see cref="T:System.TimeSpan" /> object to its equivalent string representation.</summary>
		/// <returns>The string representation of the current <see cref="T:System.TimeSpan" /> value.</returns>
		// Token: 0x0600100A RID: 4106 RVA: 0x0004213F File Offset: 0x0004033F
		public override string ToString()
		{
			return TimeSpanFormat.Format(this, null, null);
		}

		/// <summary>Converts the value of the current <see cref="T:System.TimeSpan" /> object to its equivalent string representation by using the specified format.</summary>
		/// <param name="format">A standard or custom <see cref="T:System.TimeSpan" /> format string.</param>
		/// <returns>The string representation of the current <see cref="T:System.TimeSpan" /> value in the format specified by the <paramref name="format" /> parameter.</returns>
		/// <exception cref="T:System.FormatException">The <paramref name="format" /> parameter is not recognized or is not supported.</exception>
		// Token: 0x0600100B RID: 4107 RVA: 0x0004214E File Offset: 0x0004034E
		public string ToString(string format)
		{
			return TimeSpanFormat.Format(this, format, null);
		}

		/// <summary>Converts the value of the current <see cref="T:System.TimeSpan" /> object to its equivalent string representation by using the specified format and culture-specific formatting information.</summary>
		/// <param name="format">A standard or custom <see cref="T:System.TimeSpan" /> format string.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the current <see cref="T:System.TimeSpan" /> value, as specified by <paramref name="format" /> and <paramref name="formatProvider" />.</returns>
		/// <exception cref="T:System.FormatException">The <paramref name="format" /> parameter is not recognized or is not supported.</exception>
		// Token: 0x0600100C RID: 4108 RVA: 0x0004215D File Offset: 0x0004035D
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return TimeSpanFormat.Format(this, format, formatProvider);
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0004216C File Offset: 0x0004036C
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), IFormatProvider formatProvider = null)
		{
			return TimeSpanFormat.TryFormat(this, destination, out charsWritten, format, formatProvider);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> whose value is the negated value of the specified instance.</summary>
		/// <param name="t">The time interval to be negated.</param>
		/// <returns>An object that has the same numeric value as this instance, but the opposite sign.</returns>
		/// <exception cref="T:System.OverflowException">The negated value of this instance cannot be represented by a <see cref="T:System.TimeSpan" />; that is, the value of this instance is <see cref="F:System.TimeSpan.MinValue" />.</exception>
		// Token: 0x0600100E RID: 4110 RVA: 0x0004217E File Offset: 0x0004037E
		public static TimeSpan operator -(TimeSpan t)
		{
			if (t._ticks == TimeSpan.MinValue._ticks)
			{
				throw new OverflowException("Negating the minimum value of a twos complement number is invalid.");
			}
			return new TimeSpan(-t._ticks);
		}

		/// <summary>Subtracts a specified <see cref="T:System.TimeSpan" /> from another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="t1">The minuend.</param>
		/// <param name="t2">The subtrahend.</param>
		/// <returns>An object whose value is the result of the value of <paramref name="t1" /> minus the value of <paramref name="t2" />.</returns>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.</exception>
		// Token: 0x0600100F RID: 4111 RVA: 0x000421A9 File Offset: 0x000403A9
		public static TimeSpan operator -(TimeSpan t1, TimeSpan t2)
		{
			return t1.Subtract(t2);
		}

		/// <summary>Returns the specified instance of <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="t">The time interval to return.</param>
		/// <returns>The time interval specified by <paramref name="t" />.</returns>
		// Token: 0x06001010 RID: 4112 RVA: 0x0000270D File Offset: 0x0000090D
		public static TimeSpan operator +(TimeSpan t)
		{
			return t;
		}

		/// <summary>Adds two specified <see cref="T:System.TimeSpan" /> instances.</summary>
		/// <param name="t1">The first time interval to add.</param>
		/// <param name="t2">The second time interval to add.</param>
		/// <returns>An object whose value is the sum of the values of <paramref name="t1" /> and <paramref name="t2" />.</returns>
		/// <exception cref="T:System.OverflowException">The resulting <see cref="T:System.TimeSpan" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.</exception>
		// Token: 0x06001011 RID: 4113 RVA: 0x000421B3 File Offset: 0x000403B3
		public static TimeSpan operator +(TimeSpan t1, TimeSpan t2)
		{
			return t1.Add(t2);
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x000421C0 File Offset: 0x000403C0
		public static TimeSpan operator *(TimeSpan timeSpan, double factor)
		{
			if (double.IsNaN(factor))
			{
				throw new ArgumentException("TimeSpan does not accept floating point Not-a-Number values.", "factor");
			}
			double num = Math.Round((double)timeSpan.Ticks * factor);
			if (num > 9.223372036854776E+18 | num < -9.223372036854776E+18)
			{
				throw new OverflowException("TimeSpan overflowed because the duration is too long.");
			}
			return TimeSpan.FromTicks((long)num);
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00042222 File Offset: 0x00040422
		public static TimeSpan operator *(double factor, TimeSpan timeSpan)
		{
			return timeSpan * factor;
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0004222C File Offset: 0x0004042C
		public static TimeSpan operator /(TimeSpan timeSpan, double divisor)
		{
			if (double.IsNaN(divisor))
			{
				throw new ArgumentException("TimeSpan does not accept floating point Not-a-Number values.", "divisor");
			}
			double num = Math.Round((double)timeSpan.Ticks / divisor);
			if ((num > 9.223372036854776E+18 | num < -9.223372036854776E+18) || double.IsNaN(num))
			{
				throw new OverflowException("TimeSpan overflowed because the duration is too long.");
			}
			return TimeSpan.FromTicks((long)num);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00042296 File Offset: 0x00040496
		public static double operator /(TimeSpan t1, TimeSpan t2)
		{
			return (double)t1.Ticks / (double)t2.Ticks;
		}

		/// <summary>Indicates whether two <see cref="T:System.TimeSpan" /> instances are equal.</summary>
		/// <param name="t1">The first time interval to compare.</param>
		/// <param name="t2">The second time interval to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the values of <paramref name="t1" /> and <paramref name="t2" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001016 RID: 4118 RVA: 0x00041D2C File Offset: 0x0003FF2C
		public static bool operator ==(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks == t2._ticks;
		}

		/// <summary>Indicates whether two <see cref="T:System.TimeSpan" /> instances are not equal.</summary>
		/// <param name="t1">The first time interval to compare.</param>
		/// <param name="t2">The second time interval to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the values of <paramref name="t1" /> and <paramref name="t2" /> are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001017 RID: 4119 RVA: 0x000422A9 File Offset: 0x000404A9
		public static bool operator !=(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks != t2._ticks;
		}

		/// <summary>Indicates whether a specified <see cref="T:System.TimeSpan" /> is less than another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="t1">The first time interval to compare.</param>
		/// <param name="t2">The second time interval to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="t1" /> is less than the value of <paramref name="t2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001018 RID: 4120 RVA: 0x000422BC File Offset: 0x000404BC
		public static bool operator <(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks < t2._ticks;
		}

		/// <summary>Indicates whether a specified <see cref="T:System.TimeSpan" /> is less than or equal to another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="t1">The first time interval to compare.</param>
		/// <param name="t2">The second time interval to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="t1" /> is less than or equal to the value of <paramref name="t2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001019 RID: 4121 RVA: 0x000422CC File Offset: 0x000404CC
		public static bool operator <=(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks <= t2._ticks;
		}

		/// <summary>Indicates whether a specified <see cref="T:System.TimeSpan" /> is greater than another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="t1">The first time interval to compare.</param>
		/// <param name="t2">The second time interval to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="t1" /> is greater than the value of <paramref name="t2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600101A RID: 4122 RVA: 0x000422DF File Offset: 0x000404DF
		public static bool operator >(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks > t2._ticks;
		}

		/// <summary>Indicates whether a specified <see cref="T:System.TimeSpan" /> is greater than or equal to another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="t1">The first time interval to compare.</param>
		/// <param name="t2">The second time interval to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="t1" /> is greater than or equal to the value of <paramref name="t2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600101B RID: 4123 RVA: 0x000422EF File Offset: 0x000404EF
		public static bool operator >=(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks >= t2._ticks;
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00042302 File Offset: 0x00040502
		// Note: this type is marked as 'beforefieldinit'.
		static TimeSpan()
		{
		}

		/// <summary>Represents the number of ticks in 1 millisecond. This field is constant.</summary>
		// Token: 0x04001300 RID: 4864
		public const long TicksPerMillisecond = 10000L;

		// Token: 0x04001301 RID: 4865
		private const double MillisecondsPerTick = 0.0001;

		/// <summary>Represents the number of ticks in 1 second.</summary>
		// Token: 0x04001302 RID: 4866
		public const long TicksPerSecond = 10000000L;

		// Token: 0x04001303 RID: 4867
		private const double SecondsPerTick = 1E-07;

		/// <summary>Represents the number of ticks in 1 minute. This field is constant.</summary>
		// Token: 0x04001304 RID: 4868
		public const long TicksPerMinute = 600000000L;

		// Token: 0x04001305 RID: 4869
		private const double MinutesPerTick = 1.6666666666666667E-09;

		/// <summary>Represents the number of ticks in 1 hour. This field is constant.</summary>
		// Token: 0x04001306 RID: 4870
		public const long TicksPerHour = 36000000000L;

		// Token: 0x04001307 RID: 4871
		private const double HoursPerTick = 2.7777777777777777E-11;

		/// <summary>Represents the number of ticks in 1 day. This field is constant.</summary>
		// Token: 0x04001308 RID: 4872
		public const long TicksPerDay = 864000000000L;

		// Token: 0x04001309 RID: 4873
		private const double DaysPerTick = 1.1574074074074074E-12;

		// Token: 0x0400130A RID: 4874
		private const int MillisPerSecond = 1000;

		// Token: 0x0400130B RID: 4875
		private const int MillisPerMinute = 60000;

		// Token: 0x0400130C RID: 4876
		private const int MillisPerHour = 3600000;

		// Token: 0x0400130D RID: 4877
		private const int MillisPerDay = 86400000;

		// Token: 0x0400130E RID: 4878
		internal const long MaxSeconds = 922337203685L;

		// Token: 0x0400130F RID: 4879
		internal const long MinSeconds = -922337203685L;

		// Token: 0x04001310 RID: 4880
		internal const long MaxMilliSeconds = 922337203685477L;

		// Token: 0x04001311 RID: 4881
		internal const long MinMilliSeconds = -922337203685477L;

		// Token: 0x04001312 RID: 4882
		internal const long TicksPerTenthSecond = 1000000L;

		/// <summary>Represents the zero <see cref="T:System.TimeSpan" /> value. This field is read-only.</summary>
		// Token: 0x04001313 RID: 4883
		public static readonly TimeSpan Zero = new TimeSpan(0L);

		/// <summary>Represents the maximum <see cref="T:System.TimeSpan" /> value. This field is read-only.</summary>
		// Token: 0x04001314 RID: 4884
		public static readonly TimeSpan MaxValue = new TimeSpan(long.MaxValue);

		/// <summary>Represents the minimum <see cref="T:System.TimeSpan" /> value. This field is read-only.</summary>
		// Token: 0x04001315 RID: 4885
		public static readonly TimeSpan MinValue = new TimeSpan(long.MinValue);

		// Token: 0x04001316 RID: 4886
		internal readonly long _ticks;
	}
}
