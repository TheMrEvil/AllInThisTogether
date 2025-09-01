using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Represents an instant in time, typically expressed as a date and time of day.</summary>
	// Token: 0x0200010C RID: 268
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public readonly struct DateTime : IComparable, IFormattable, IConvertible, IComparable<DateTime>, IEquatable<DateTime>, ISerializable, ISpanFormattable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to a specified number of ticks.</summary>
		/// <param name="ticks">A date and time expressed in the number of 100-nanosecond intervals that have elapsed since January 1, 0001 at 00:00:00.000 in the Gregorian calendar.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="ticks" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x060009A7 RID: 2471 RVA: 0x0002593B File Offset: 0x00023B3B
		public DateTime(long ticks)
		{
			if (ticks < 0L || ticks > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("ticks", "Ticks must be between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.");
			}
			this._dateData = (ulong)ticks;
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00025965 File Offset: 0x00023B65
		private DateTime(ulong dateData)
		{
			this._dateData = dateData;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to a specified number of ticks and to Coordinated Universal Time (UTC) or local time.</summary>
		/// <param name="ticks">A date and time expressed in the number of 100-nanosecond intervals that have elapsed since January 1, 0001 at 00:00:00.000 in the Gregorian calendar.</param>
		/// <param name="kind">One of the enumeration values that indicates whether <paramref name="ticks" /> specifies a local time, Coordinated Universal Time (UTC), or neither.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="ticks" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="kind" /> is not one of the <see cref="T:System.DateTimeKind" /> values.</exception>
		// Token: 0x060009A9 RID: 2473 RVA: 0x00025970 File Offset: 0x00023B70
		public DateTime(long ticks, DateTimeKind kind)
		{
			if (ticks < 0L || ticks > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("ticks", "Ticks must be between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.");
			}
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException("Invalid DateTimeKind value.", "kind");
			}
			this._dateData = (ulong)(ticks | (long)kind << 62);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x000259C4 File Offset: 0x00023BC4
		internal DateTime(long ticks, DateTimeKind kind, bool isAmbiguousDst)
		{
			if (ticks < 0L || ticks > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("ticks", "Ticks must be between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.");
			}
			this._dateData = (ulong)(ticks | (isAmbiguousDst ? -4611686018427387904L : long.MinValue));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, and day.</summary>
		/// <param name="year">The year (1 through 9999).</param>
		/// <param name="month">The month (1 through 12).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999.  
		/// -or-  
		/// <paramref name="month" /> is less than 1 or greater than 12.  
		/// -or-  
		/// <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.</exception>
		// Token: 0x060009AB RID: 2475 RVA: 0x00025A11 File Offset: 0x00023C11
		public DateTime(int year, int month, int day)
		{
			this._dateData = (ulong)DateTime.DateToTicks(year, month, day);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, and day for the specified calendar.</summary>
		/// <param name="year">The year (1 through the number of years in <paramref name="calendar" />).</param>
		/// <param name="month">The month (1 through the number of months in <paramref name="calendar" />).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="calendar">The calendar that is used to interpret <paramref name="year" />, <paramref name="month" />, and <paramref name="day" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="calendar" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is not in the range supported by <paramref name="calendar" />.  
		/// -or-  
		/// <paramref name="month" /> is less than 1 or greater than the number of months in <paramref name="calendar" />.  
		/// -or-  
		/// <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.</exception>
		// Token: 0x060009AC RID: 2476 RVA: 0x00025A21 File Offset: 0x00023C21
		public DateTime(int year, int month, int day, Calendar calendar)
		{
			this = new DateTime(year, month, day, 0, 0, 0, calendar);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, and second.</summary>
		/// <param name="year">The year (1 through 9999).</param>
		/// <param name="month">The month (1 through 12).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="hour">The hours (0 through 23).</param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999.  
		/// -or-  
		/// <paramref name="month" /> is less than 1 or greater than 12.  
		/// -or-  
		/// <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.  
		/// -or-  
		/// <paramref name="hour" /> is less than 0 or greater than 23.  
		/// -or-  
		/// <paramref name="minute" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="second" /> is less than 0 or greater than 59.</exception>
		// Token: 0x060009AD RID: 2477 RVA: 0x00025A31 File Offset: 0x00023C31
		public DateTime(int year, int month, int day, int hour, int minute, int second)
		{
			this._dateData = (ulong)(DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, second, and Coordinated Universal Time (UTC) or local time.</summary>
		/// <param name="year">The year (1 through 9999).</param>
		/// <param name="month">The month (1 through 12).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="hour">The hours (0 through 23).</param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <param name="kind">One of the enumeration values that indicates whether <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, <paramref name="hour" />, <paramref name="minute" /> and <paramref name="second" /> specify a local time, Coordinated Universal Time (UTC), or neither.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999.  
		/// -or-  
		/// <paramref name="month" /> is less than 1 or greater than 12.  
		/// -or-  
		/// <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.  
		/// -or-  
		/// <paramref name="hour" /> is less than 0 or greater than 23.  
		/// -or-  
		/// <paramref name="minute" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="second" /> is less than 0 or greater than 59.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="kind" /> is not one of the <see cref="T:System.DateTimeKind" /> values.</exception>
		// Token: 0x060009AE RID: 2478 RVA: 0x00025A50 File Offset: 0x00023C50
		public DateTime(int year, int month, int day, int hour, int minute, int second, DateTimeKind kind)
		{
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException("Invalid DateTimeKind value.", "kind");
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			this._dateData = (ulong)(num | (long)kind << 62);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, and second for the specified calendar.</summary>
		/// <param name="year">The year (1 through the number of years in <paramref name="calendar" />).</param>
		/// <param name="month">The month (1 through the number of months in <paramref name="calendar" />).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="hour">The hours (0 through 23).</param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <param name="calendar">The calendar that is used to interpret <paramref name="year" />, <paramref name="month" />, and <paramref name="day" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="calendar" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is not in the range supported by <paramref name="calendar" />.  
		/// -or-  
		/// <paramref name="month" /> is less than 1 or greater than the number of months in <paramref name="calendar" />.  
		/// -or-  
		/// <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.  
		/// -or-  
		/// <paramref name="hour" /> is less than 0 or greater than 23  
		/// -or-  
		/// <paramref name="minute" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="second" /> is less than 0 or greater than 59.</exception>
		// Token: 0x060009AF RID: 2479 RVA: 0x00025A9C File Offset: 0x00023C9C
		public DateTime(int year, int month, int day, int hour, int minute, int second, Calendar calendar)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			this._dateData = (ulong)calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, second, and millisecond.</summary>
		/// <param name="year">The year (1 through 9999).</param>
		/// <param name="month">The month (1 through 12).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="hour">The hours (0 through 23).</param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <param name="millisecond">The milliseconds (0 through 999).</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999.  
		/// -or-  
		/// <paramref name="month" /> is less than 1 or greater than 12.  
		/// -or-  
		/// <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.  
		/// -or-  
		/// <paramref name="hour" /> is less than 0 or greater than 23.  
		/// -or-  
		/// <paramref name="minute" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="second" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="millisecond" /> is less than 0 or greater than 999.</exception>
		// Token: 0x060009B0 RID: 2480 RVA: 0x00025AD8 File Offset: 0x00023CD8
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
		{
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", SR.Format("Valid values are between {0} and {1}, inclusive.", 0, 999));
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException("Combination of arguments to the DateTime constructor is out of the legal range.");
			}
			this._dateData = (ulong)num;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, second, millisecond, and Coordinated Universal Time (UTC) or local time.</summary>
		/// <param name="year">The year (1 through 9999).</param>
		/// <param name="month">The month (1 through 12).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="hour">The hours (0 through 23).</param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <param name="millisecond">The milliseconds (0 through 999).</param>
		/// <param name="kind">One of the enumeration values that indicates whether <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, <paramref name="hour" />, <paramref name="minute" />, <paramref name="second" />, and <paramref name="millisecond" /> specify a local time, Coordinated Universal Time (UTC), or neither.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999.  
		/// -or-  
		/// <paramref name="month" /> is less than 1 or greater than 12.  
		/// -or-  
		/// <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.  
		/// -or-  
		/// <paramref name="hour" /> is less than 0 or greater than 23.  
		/// -or-  
		/// <paramref name="minute" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="second" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="millisecond" /> is less than 0 or greater than 999.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="kind" /> is not one of the <see cref="T:System.DateTimeKind" /> values.</exception>
		// Token: 0x060009B1 RID: 2481 RVA: 0x00025B60 File Offset: 0x00023D60
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, DateTimeKind kind)
		{
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", SR.Format("Valid values are between {0} and {1}, inclusive.", 0, 999));
			}
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException("Invalid DateTimeKind value.", "kind");
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException("Combination of arguments to the DateTime constructor is out of the legal range.");
			}
			this._dateData = (ulong)(num | (long)kind << 62);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, second, and millisecond for the specified calendar.</summary>
		/// <param name="year">The year (1 through the number of years in <paramref name="calendar" />).</param>
		/// <param name="month">The month (1 through the number of months in <paramref name="calendar" />).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="hour">The hours (0 through 23).</param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <param name="millisecond">The milliseconds (0 through 999).</param>
		/// <param name="calendar">The calendar that is used to interpret <paramref name="year" />, <paramref name="month" />, and <paramref name="day" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="calendar" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is not in the range supported by <paramref name="calendar" />.  
		/// -or-  
		/// <paramref name="month" /> is less than 1 or greater than the number of months in <paramref name="calendar" />.  
		/// -or-  
		/// <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.  
		/// -or-  
		/// <paramref name="hour" /> is less than 0 or greater than 23.  
		/// -or-  
		/// <paramref name="minute" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="second" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="millisecond" /> is less than 0 or greater than 999.</exception>
		// Token: 0x060009B2 RID: 2482 RVA: 0x00025C08 File Offset: 0x00023E08
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", SR.Format("Valid values are between {0} and {1}, inclusive.", 0, 999));
			}
			long num = calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks;
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException("Combination of arguments to the DateTime constructor is out of the legal range.");
			}
			this._dateData = (ulong)num;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, second, millisecond, and Coordinated Universal Time (UTC) or local time for the specified calendar.</summary>
		/// <param name="year">The year (1 through the number of years in <paramref name="calendar" />).</param>
		/// <param name="month">The month (1 through the number of months in <paramref name="calendar" />).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="hour">The hours (0 through 23).</param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <param name="millisecond">The milliseconds (0 through 999).</param>
		/// <param name="calendar">The calendar that is used to interpret <paramref name="year" />, <paramref name="month" />, and <paramref name="day" />.</param>
		/// <param name="kind">One of the enumeration values that indicates whether <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, <paramref name="hour" />, <paramref name="minute" />, <paramref name="second" />, and <paramref name="millisecond" /> specify a local time, Coordinated Universal Time (UTC), or neither.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="calendar" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is not in the range supported by <paramref name="calendar" />.  
		/// -or-  
		/// <paramref name="month" /> is less than 1 or greater than the number of months in <paramref name="calendar" />.  
		/// -or-  
		/// <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.  
		/// -or-  
		/// <paramref name="hour" /> is less than 0 or greater than 23.  
		/// -or-  
		/// <paramref name="minute" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="second" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="millisecond" /> is less than 0 or greater than 999.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="kind" /> is not one of the <see cref="T:System.DateTimeKind" /> values.</exception>
		// Token: 0x060009B3 RID: 2483 RVA: 0x00025CA4 File Offset: 0x00023EA4
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar, DateTimeKind kind)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", SR.Format("Valid values are between {0} and {1}, inclusive.", 0, 999));
			}
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException("Invalid DateTimeKind value.", "kind");
			}
			long num = calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks;
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException("Combination of arguments to the DateTime constructor is out of the legal range.");
			}
			this._dateData = (ulong)(num | (long)kind << 62);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00025D60 File Offset: 0x00023F60
		private DateTime(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			bool flag = false;
			bool flag2 = false;
			long dateData = 0L;
			ulong dateData2 = 0UL;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "ticks"))
				{
					if (name == "dateData")
					{
						dateData2 = Convert.ToUInt64(enumerator.Value, CultureInfo.InvariantCulture);
						flag2 = true;
					}
				}
				else
				{
					dateData = Convert.ToInt64(enumerator.Value, CultureInfo.InvariantCulture);
					flag = true;
				}
			}
			if (flag2)
			{
				this._dateData = dateData2;
			}
			else
			{
				if (!flag)
				{
					throw new SerializationException("Invalid serialized DateTime data. Unable to find 'ticks' or 'dateData'.");
				}
				this._dateData = (ulong)dateData;
			}
			long internalTicks = this.InternalTicks;
			if (internalTicks < 0L || internalTicks > 3155378975999999999L)
			{
				throw new SerializationException("Invalid serialized DateTime data. Ticks must be between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.");
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x00025E32 File Offset: 0x00024032
		internal long InternalTicks
		{
			get
			{
				return (long)(this._dateData & 4611686018427387903UL);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x00025E44 File Offset: 0x00024044
		private ulong InternalKind
		{
			get
			{
				return this._dateData & 13835058055282163712UL;
			}
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the value of the specified <see cref="T:System.TimeSpan" /> to the value of this instance.</summary>
		/// <param name="value">A positive or negative time interval.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the time interval represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x060009B7 RID: 2487 RVA: 0x00025E56 File Offset: 0x00024056
		public DateTime Add(TimeSpan value)
		{
			return this.AddTicks(value._ticks);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00025E64 File Offset: 0x00024064
		private DateTime Add(double value, int scale)
		{
			long num = (long)(value * (double)scale + ((value >= 0.0) ? 0.5 : -0.5));
			if (num <= -315537897600000L || num >= 315537897600000L)
			{
				throw new ArgumentOutOfRangeException("value", "Value to add was out of range.");
			}
			return this.AddTicks(num * 10000L);
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of days to the value of this instance.</summary>
		/// <param name="value">A number of whole and fractional days. The <paramref name="value" /> parameter can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of days represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x060009B9 RID: 2489 RVA: 0x00025ECE File Offset: 0x000240CE
		public DateTime AddDays(double value)
		{
			return this.Add(value, 86400000);
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of hours to the value of this instance.</summary>
		/// <param name="value">A number of whole and fractional hours. The <paramref name="value" /> parameter can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of hours represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x060009BA RID: 2490 RVA: 0x00025EDC File Offset: 0x000240DC
		public DateTime AddHours(double value)
		{
			return this.Add(value, 3600000);
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of milliseconds to the value of this instance.</summary>
		/// <param name="value">A number of whole and fractional milliseconds. The <paramref name="value" /> parameter can be negative or positive. Note that this value is rounded to the nearest integer.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of milliseconds represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x060009BB RID: 2491 RVA: 0x00025EEA File Offset: 0x000240EA
		public DateTime AddMilliseconds(double value)
		{
			return this.Add(value, 1);
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of minutes to the value of this instance.</summary>
		/// <param name="value">A number of whole and fractional minutes. The <paramref name="value" /> parameter can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of minutes represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x060009BC RID: 2492 RVA: 0x00025EF4 File Offset: 0x000240F4
		public DateTime AddMinutes(double value)
		{
			return this.Add(value, 60000);
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of months to the value of this instance.</summary>
		/// <param name="months">A number of months. The <paramref name="months" /> parameter can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and <paramref name="months" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.  
		///  -or-  
		///  <paramref name="months" /> is less than -120,000 or greater than 120,000.</exception>
		// Token: 0x060009BD RID: 2493 RVA: 0x00025F04 File Offset: 0x00024104
		public DateTime AddMonths(int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", "Months value must be between +/-120000.");
			}
			int num;
			int num2;
			int num3;
			this.GetDatePart(out num, out num2, out num3);
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
			if (num < 1 || num > 9999)
			{
				throw new ArgumentOutOfRangeException("months", "The added or subtracted value results in an un-representable DateTime.");
			}
			int num5 = DateTime.DaysInMonth(num, num2);
			if (num3 > num5)
			{
				num3 = num5;
			}
			return new DateTime((ulong)(DateTime.DateToTicks(num, num2, num3) + this.InternalTicks % 864000000000L | (long)this.InternalKind));
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of seconds to the value of this instance.</summary>
		/// <param name="value">A number of whole and fractional seconds. The <paramref name="value" /> parameter can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of seconds represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x060009BE RID: 2494 RVA: 0x00025FBD File Offset: 0x000241BD
		public DateTime AddSeconds(double value)
		{
			return this.Add(value, 1000);
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of ticks to the value of this instance.</summary>
		/// <param name="value">A number of 100-nanosecond ticks. The <paramref name="value" /> parameter can be positive or negative.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the time represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x060009BF RID: 2495 RVA: 0x00025FCC File Offset: 0x000241CC
		public DateTime AddTicks(long value)
		{
			long internalTicks = this.InternalTicks;
			if (value > 3155378975999999999L - internalTicks || value < 0L - internalTicks)
			{
				throw new ArgumentOutOfRangeException("value", "The added or subtracted value results in an un-representable DateTime.");
			}
			return new DateTime((ulong)(internalTicks + value | (long)this.InternalKind));
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of years to the value of this instance.</summary>
		/// <param name="value">A number of years. The <paramref name="value" /> parameter can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of years represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> or the resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x060009C0 RID: 2496 RVA: 0x00026014 File Offset: 0x00024214
		public DateTime AddYears(int value)
		{
			if (value < -10000 || value > 10000)
			{
				throw new ArgumentOutOfRangeException("years", "Years value must be between +/-10000.");
			}
			return this.AddMonths(value * 12);
		}

		/// <summary>Compares two instances of <see cref="T:System.DateTime" /> and returns an integer that indicates whether the first instance is earlier than, the same as, or later than the second instance.</summary>
		/// <param name="t1">The first object to compare.</param>
		/// <param name="t2">The second object to compare.</param>
		/// <returns>A signed number indicating the relative values of <paramref name="t1" /> and <paramref name="t2" />.  
		///   Value Type  
		///
		///   Condition  
		///
		///   Less than zero  
		///
		///  <paramref name="t1" /> is earlier than <paramref name="t2" />.  
		///
		///   Zero  
		///
		///  <paramref name="t1" /> is the same as <paramref name="t2" />.  
		///
		///   Greater than zero  
		///
		///  <paramref name="t1" /> is later than <paramref name="t2" />.</returns>
		// Token: 0x060009C1 RID: 2497 RVA: 0x00026040 File Offset: 0x00024240
		public static int Compare(DateTime t1, DateTime t2)
		{
			long internalTicks = t1.InternalTicks;
			long internalTicks2 = t2.InternalTicks;
			if (internalTicks > internalTicks2)
			{
				return 1;
			}
			if (internalTicks < internalTicks2)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Compares the value of this instance to a specified object that contains a specified <see cref="T:System.DateTime" /> value, and returns an integer that indicates whether this instance is earlier than, the same as, or later than the specified <see cref="T:System.DateTime" /> value.</summary>
		/// <param name="value">A boxed object to compare, or <see langword="null" />.</param>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.  
		///   Value  
		///
		///   Description  
		///
		///   Less than zero  
		///
		///   This instance is earlier than <paramref name="value" />.  
		///
		///   Zero  
		///
		///   This instance is the same as <paramref name="value" />.  
		///
		///   Greater than zero  
		///
		///   This instance is later than <paramref name="value" />, or <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.DateTime" />.</exception>
		// Token: 0x060009C2 RID: 2498 RVA: 0x0002606A File Offset: 0x0002426A
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is DateTime))
			{
				throw new ArgumentException("Object must be of type DateTime.");
			}
			return DateTime.Compare(this, (DateTime)value);
		}

		/// <summary>Compares the value of this instance to a specified <see cref="T:System.DateTime" /> value and returns an integer that indicates whether this instance is earlier than, the same as, or later than the specified <see cref="T:System.DateTime" /> value.</summary>
		/// <param name="value">The object to compare to the current instance.</param>
		/// <returns>A signed number indicating the relative values of this instance and the <paramref name="value" /> parameter.  
		///   Value  
		///
		///   Description  
		///
		///   Less than zero  
		///
		///   This instance is earlier than <paramref name="value" />.  
		///
		///   Zero  
		///
		///   This instance is the same as <paramref name="value" />.  
		///
		///   Greater than zero  
		///
		///   This instance is later than <paramref name="value" />.</returns>
		// Token: 0x060009C3 RID: 2499 RVA: 0x00026095 File Offset: 0x00024295
		public int CompareTo(DateTime value)
		{
			return DateTime.Compare(this, value);
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x000260A4 File Offset: 0x000242A4
		private static long DateToTicks(int year, int month, int day)
		{
			if (year >= 1 && year <= 9999 && month >= 1 && month <= 12)
			{
				int[] array = DateTime.IsLeapYear(year) ? DateTime.s_daysToMonth366 : DateTime.s_daysToMonth365;
				if (day >= 1 && day <= array[month] - array[month - 1])
				{
					int num = year - 1;
					return (long)(num * 365 + num / 4 - num / 100 + num / 400 + array[month - 1] + day - 1) * 864000000000L;
				}
			}
			throw new ArgumentOutOfRangeException(null, "Year, Month, and Day parameters describe an un-representable DateTime.");
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00026128 File Offset: 0x00024328
		private static long TimeToTicks(int hour, int minute, int second)
		{
			if (hour >= 0 && hour < 24 && minute >= 0 && minute < 60 && second >= 0 && second < 60)
			{
				return TimeSpan.TimeToTicks(hour, minute, second);
			}
			throw new ArgumentOutOfRangeException(null, "Hour, Minute, and Second parameters describe an un-representable DateTime.");
		}

		/// <summary>Returns the number of days in the specified month and year.</summary>
		/// <param name="year">The year.</param>
		/// <param name="month">The month (a number ranging from 1 to 12).</param>
		/// <returns>The number of days in <paramref name="month" /> for the specified <paramref name="year" />.  
		///  For example, if <paramref name="month" /> equals 2 for February, the return value is 28 or 29 depending upon whether <paramref name="year" /> is a leap year.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="month" /> is less than 1 or greater than 12.  
		/// -or-  
		/// <paramref name="year" /> is less than 1 or greater than 9999.</exception>
		// Token: 0x060009C6 RID: 2502 RVA: 0x0002615C File Offset: 0x0002435C
		public static int DaysInMonth(int year, int month)
		{
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", "Month must be between one and twelve.");
			}
			int[] array = DateTime.IsLeapYear(year) ? DateTime.s_daysToMonth366 : DateTime.s_daysToMonth365;
			return array[month] - array[month - 1];
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x000261A0 File Offset: 0x000243A0
		internal static long DoubleDateToTicks(double value)
		{
			if (value >= 2958466.0 || value <= -657435.0)
			{
				throw new ArgumentException(" Not a legal OleAut date.");
			}
			long num = (long)(value * 86400000.0 + ((value >= 0.0) ? 0.5 : -0.5));
			if (num < 0L)
			{
				num -= num % 86400000L * 2L;
			}
			num += 59926435200000L;
			if (num < 0L || num >= 315537897600000L)
			{
				throw new ArgumentException("OleAut date did not convert to a DateTime correctly.");
			}
			return num * 10000L;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <param name="value">The object to compare to this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is an instance of <see cref="T:System.DateTime" /> and equals the value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060009C8 RID: 2504 RVA: 0x00026244 File Offset: 0x00024444
		public override bool Equals(object value)
		{
			return value is DateTime && this.InternalTicks == ((DateTime)value).InternalTicks;
		}

		/// <summary>Returns a value indicating whether the value of this instance is equal to the value of the specified <see cref="T:System.DateTime" /> instance.</summary>
		/// <param name="value">The object to compare to this instance.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="value" /> parameter equals the value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060009C9 RID: 2505 RVA: 0x00026271 File Offset: 0x00024471
		public bool Equals(DateTime value)
		{
			return this.InternalTicks == value.InternalTicks;
		}

		/// <summary>Returns a value indicating whether two <see cref="T:System.DateTime" /> instances  have the same date and time value.</summary>
		/// <param name="t1">The first object to compare.</param>
		/// <param name="t2">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the two values are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060009CA RID: 2506 RVA: 0x00026282 File Offset: 0x00024482
		public static bool Equals(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks == t2.InternalTicks;
		}

		/// <summary>Deserializes a 64-bit binary value and recreates an original serialized <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="dateData">A 64-bit signed integer that encodes the <see cref="P:System.DateTime.Kind" /> property in a 2-bit field and the <see cref="P:System.DateTime.Ticks" /> property in a 62-bit field.</param>
		/// <returns>An object that is equivalent to the <see cref="T:System.DateTime" /> object that was serialized by the <see cref="M:System.DateTime.ToBinary" /> method.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dateData" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x060009CB RID: 2507 RVA: 0x00026294 File Offset: 0x00024494
		public static DateTime FromBinary(long dateData)
		{
			if ((dateData & -9223372036854775808L) == 0L)
			{
				return DateTime.FromBinaryRaw(dateData);
			}
			long num = dateData & 4611686018427387903L;
			if (num > 4611685154427387904L)
			{
				num -= 4611686018427387904L;
			}
			bool isAmbiguousDst = false;
			long ticks;
			if (num < 0L)
			{
				ticks = TimeZoneInfo.GetLocalUtcOffset(DateTime.MinValue, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
			}
			else if (num > 3155378975999999999L)
			{
				ticks = TimeZoneInfo.GetLocalUtcOffset(DateTime.MaxValue, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
			}
			else
			{
				DateTime time = new DateTime(num, DateTimeKind.Utc);
				bool flag = false;
				ticks = TimeZoneInfo.GetUtcOffsetFromUtc(time, TimeZoneInfo.Local, out flag, out isAmbiguousDst).Ticks;
			}
			num += ticks;
			if (num < 0L)
			{
				num += 864000000000L;
			}
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException("The binary data must result in a DateTime with ticks between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.", "dateData");
			}
			return new DateTime(num, DateTimeKind.Local, isAmbiguousDst);
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0002637C File Offset: 0x0002457C
		internal static DateTime FromBinaryRaw(long dateData)
		{
			long num = dateData & 4611686018427387903L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException("The binary data must result in a DateTime with ticks between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.", "dateData");
			}
			return new DateTime((ulong)dateData);
		}

		/// <summary>Converts the specified Windows file time to an equivalent local time.</summary>
		/// <param name="fileTime">A Windows file time expressed in ticks.</param>
		/// <returns>An object that represents the local time equivalent of the date and time represented by the <paramref name="fileTime" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="fileTime" /> is less than 0 or represents a time greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x060009CD RID: 2509 RVA: 0x000263BC File Offset: 0x000245BC
		public static DateTime FromFileTime(long fileTime)
		{
			return DateTime.FromFileTimeUtc(fileTime).ToLocalTime();
		}

		/// <summary>Converts the specified Windows file time to an equivalent UTC time.</summary>
		/// <param name="fileTime">A Windows file time expressed in ticks.</param>
		/// <returns>An object that represents the UTC time equivalent of the date and time represented by the <paramref name="fileTime" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="fileTime" /> is less than 0 or represents a time greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x060009CE RID: 2510 RVA: 0x000263D7 File Offset: 0x000245D7
		public static DateTime FromFileTimeUtc(long fileTime)
		{
			if (fileTime < 0L || fileTime > 2650467743999999999L)
			{
				throw new ArgumentOutOfRangeException("fileTime", "Not a valid Win32 FileTime.");
			}
			return new DateTime(fileTime + 504911232000000000L, DateTimeKind.Utc);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> equivalent to the specified OLE Automation Date.</summary>
		/// <param name="d">An OLE Automation Date value.</param>
		/// <returns>An object that represents the same date and time as <paramref name="d" />.</returns>
		/// <exception cref="T:System.ArgumentException">The date is not a valid OLE Automation Date value.</exception>
		// Token: 0x060009CF RID: 2511 RVA: 0x0002640B File Offset: 0x0002460B
		public static DateTime FromOADate(double d)
		{
			return new DateTime(DateTime.DoubleDateToTicks(d), DateTimeKind.Unspecified);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the current <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="info">The object to populate with data.</param>
		/// <param name="context">The destination for this serialization. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x060009D0 RID: 2512 RVA: 0x00026419 File Offset: 0x00024619
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("ticks", this.InternalTicks);
			info.AddValue("dateData", this._dateData);
		}

		/// <summary>Indicates whether this instance of <see cref="T:System.DateTime" /> is within the daylight saving time range for the current time zone.</summary>
		/// <returns>
		///   <see langword="true" /> if the value of the <see cref="P:System.DateTime.Kind" /> property is <see cref="F:System.DateTimeKind.Local" /> or <see cref="F:System.DateTimeKind.Unspecified" /> and the value of this instance of <see cref="T:System.DateTime" /> is within the daylight saving time range for the local time zone; <see langword="false" /> if <see cref="P:System.DateTime.Kind" /> is <see cref="F:System.DateTimeKind.Utc" />.</returns>
		// Token: 0x060009D1 RID: 2513 RVA: 0x0002644B File Offset: 0x0002464B
		public bool IsDaylightSavingTime()
		{
			return this.Kind != DateTimeKind.Utc && TimeZoneInfo.Local.IsDaylightSavingTime(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
		}

		/// <summary>Creates a new <see cref="T:System.DateTime" /> object that has the same number of ticks as the specified <see cref="T:System.DateTime" />, but is designated as either local time, Coordinated Universal Time (UTC), or neither, as indicated by the specified <see cref="T:System.DateTimeKind" /> value.</summary>
		/// <param name="value">A date and time.</param>
		/// <param name="kind">One of the enumeration values that indicates whether the new object represents local time, UTC, or neither.</param>
		/// <returns>A new object that has the same number of ticks as the object represented by the <paramref name="value" /> parameter and the <see cref="T:System.DateTimeKind" /> value specified by the <paramref name="kind" /> parameter.</returns>
		// Token: 0x060009D2 RID: 2514 RVA: 0x00026469 File Offset: 0x00024669
		public static DateTime SpecifyKind(DateTime value, DateTimeKind kind)
		{
			return new DateTime(value.InternalTicks, kind);
		}

		/// <summary>Serializes the current <see cref="T:System.DateTime" /> object to a 64-bit binary value that subsequently can be used to recreate the <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>A 64-bit signed integer that encodes the <see cref="P:System.DateTime.Kind" /> and <see cref="P:System.DateTime.Ticks" /> properties.</returns>
		// Token: 0x060009D3 RID: 2515 RVA: 0x00026478 File Offset: 0x00024678
		public long ToBinary()
		{
			if (this.Kind == DateTimeKind.Local)
			{
				TimeSpan localUtcOffset = TimeZoneInfo.GetLocalUtcOffset(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
				long num = this.Ticks - localUtcOffset.Ticks;
				if (num < 0L)
				{
					num = 4611686018427387904L + num;
				}
				return num | long.MinValue;
			}
			return (long)this._dateData;
		}

		/// <summary>Gets the date component of this instance.</summary>
		/// <returns>A new object with the same date as this instance, and the time value set to 12:00:00 midnight (00:00:00).</returns>
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x000264CD File Offset: 0x000246CD
		public DateTime Date
		{
			get
			{
				long internalTicks = this.InternalTicks;
				return new DateTime((ulong)(internalTicks - internalTicks % 864000000000L | (long)this.InternalKind));
			}
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x000264F0 File Offset: 0x000246F0
		private int GetDatePart(int part)
		{
			int i = (int)(this.InternalTicks / 864000000000L);
			int num = i / 146097;
			i -= num * 146097;
			int num2 = i / 36524;
			if (num2 == 4)
			{
				num2 = 3;
			}
			i -= num2 * 36524;
			int num3 = i / 1461;
			i -= num3 * 1461;
			int num4 = i / 365;
			if (num4 == 4)
			{
				num4 = 3;
			}
			if (part == 0)
			{
				return num * 400 + num2 * 100 + num3 * 4 + num4 + 1;
			}
			i -= num4 * 365;
			if (part == 1)
			{
				return i + 1;
			}
			int[] array = (num4 == 3 && (num3 != 24 || num2 == 3)) ? DateTime.s_daysToMonth366 : DateTime.s_daysToMonth365;
			int num5 = (i >> 5) + 1;
			while (i >= array[num5])
			{
				num5++;
			}
			if (part == 2)
			{
				return num5;
			}
			return i - array[num5 - 1] + 1;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x000265D8 File Offset: 0x000247D8
		internal void GetDatePart(out int year, out int month, out int day)
		{
			int i = (int)(this.InternalTicks / 864000000000L);
			int num = i / 146097;
			i -= num * 146097;
			int num2 = i / 36524;
			if (num2 == 4)
			{
				num2 = 3;
			}
			i -= num2 * 36524;
			int num3 = i / 1461;
			i -= num3 * 1461;
			int num4 = i / 365;
			if (num4 == 4)
			{
				num4 = 3;
			}
			year = num * 400 + num2 * 100 + num3 * 4 + num4 + 1;
			i -= num4 * 365;
			int[] array = (num4 == 3 && (num3 != 24 || num2 == 3)) ? DateTime.s_daysToMonth366 : DateTime.s_daysToMonth365;
			int num5 = (i >> 5) + 1;
			while (i >= array[num5])
			{
				num5++;
			}
			month = num5;
			day = i - array[num5 - 1] + 1;
		}

		/// <summary>Gets the day of the month represented by this instance.</summary>
		/// <returns>The day component, expressed as a value between 1 and 31.</returns>
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x000266B2 File Offset: 0x000248B2
		public int Day
		{
			get
			{
				return this.GetDatePart(3);
			}
		}

		/// <summary>Gets the day of the week represented by this instance.</summary>
		/// <returns>An enumerated constant that indicates the day of the week of this <see cref="T:System.DateTime" /> value.</returns>
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x000266BB File Offset: 0x000248BB
		public DayOfWeek DayOfWeek
		{
			get
			{
				return (DayOfWeek)((this.InternalTicks / 864000000000L + 1L) % 7L);
			}
		}

		/// <summary>Gets the day of the year represented by this instance.</summary>
		/// <returns>The day of the year, expressed as a value between 1 and 366.</returns>
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x000266D4 File Offset: 0x000248D4
		public int DayOfYear
		{
			get
			{
				return this.GetDatePart(1);
			}
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060009DA RID: 2522 RVA: 0x000266E0 File Offset: 0x000248E0
		public override int GetHashCode()
		{
			long internalTicks = this.InternalTicks;
			return (int)internalTicks ^ (int)(internalTicks >> 32);
		}

		/// <summary>Gets the hour component of the date represented by this instance.</summary>
		/// <returns>The hour component, expressed as a value between 0 and 23.</returns>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x000266FC File Offset: 0x000248FC
		public int Hour
		{
			get
			{
				return (int)(this.InternalTicks / 36000000000L % 24L);
			}
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00026713 File Offset: 0x00024913
		internal bool IsAmbiguousDaylightSavingTime()
		{
			return this.InternalKind == 13835058055282163712UL;
		}

		/// <summary>Gets a value that indicates whether the time represented by this instance is based on local time, Coordinated Universal Time (UTC), or neither.</summary>
		/// <returns>One of the enumeration values that indicates what the current time represents. The default is <see cref="F:System.DateTimeKind.Unspecified" />.</returns>
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x00026728 File Offset: 0x00024928
		public DateTimeKind Kind
		{
			get
			{
				ulong internalKind = this.InternalKind;
				if (internalKind == 0UL)
				{
					return DateTimeKind.Unspecified;
				}
				if (internalKind != 4611686018427387904UL)
				{
					return DateTimeKind.Local;
				}
				return DateTimeKind.Utc;
			}
		}

		/// <summary>Gets the milliseconds component of the date represented by this instance.</summary>
		/// <returns>The milliseconds component, expressed as a value between 0 and 999.</returns>
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x00026752 File Offset: 0x00024952
		public int Millisecond
		{
			get
			{
				return (int)(this.InternalTicks / 10000L % 1000L);
			}
		}

		/// <summary>Gets the minute component of the date represented by this instance.</summary>
		/// <returns>The minute component, expressed as a value between 0 and 59.</returns>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x00026769 File Offset: 0x00024969
		public int Minute
		{
			get
			{
				return (int)(this.InternalTicks / 600000000L % 60L);
			}
		}

		/// <summary>Gets the month component of the date represented by this instance.</summary>
		/// <returns>The month component, expressed as a value between 1 and 12.</returns>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0002677D File Offset: 0x0002497D
		public int Month
		{
			get
			{
				return this.GetDatePart(2);
			}
		}

		/// <summary>Gets a <see cref="T:System.DateTime" /> object that is set to the current date and time on this computer, expressed as the local time.</summary>
		/// <returns>An object whose value is the current local date and time.</returns>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00026788 File Offset: 0x00024988
		public static DateTime Now
		{
			get
			{
				DateTime utcNow = DateTime.UtcNow;
				bool isAmbiguousDst = false;
				long ticks = TimeZoneInfo.GetDateTimeNowUtcOffsetFromUtc(utcNow, out isAmbiguousDst).Ticks;
				long num = utcNow.Ticks + ticks;
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
		}

		/// <summary>Gets the seconds component of the date represented by this instance.</summary>
		/// <returns>The seconds component, expressed as a value between 0 and 59.</returns>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x000267EB File Offset: 0x000249EB
		public int Second
		{
			get
			{
				return (int)(this.InternalTicks / 10000000L % 60L);
			}
		}

		/// <summary>Gets the number of ticks that represent the date and time of this instance.</summary>
		/// <returns>The number of ticks that represent the date and time of this instance. The value is between <see langword="DateTime.MinValue.Ticks" /> and <see langword="DateTime.MaxValue.Ticks" />.</returns>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x000267FF File Offset: 0x000249FF
		public long Ticks
		{
			get
			{
				return this.InternalTicks;
			}
		}

		/// <summary>Gets the time of day for this instance.</summary>
		/// <returns>A time interval that represents the fraction of the day that has elapsed since midnight.</returns>
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x00026807 File Offset: 0x00024A07
		public TimeSpan TimeOfDay
		{
			get
			{
				return new TimeSpan(this.InternalTicks % 864000000000L);
			}
		}

		/// <summary>Gets the current date.</summary>
		/// <returns>An object that is set to today's date, with the time component set to 00:00:00.</returns>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x00026820 File Offset: 0x00024A20
		public static DateTime Today
		{
			get
			{
				return DateTime.Now.Date;
			}
		}

		/// <summary>Gets the year component of the date represented by this instance.</summary>
		/// <returns>The year, between 1 and 9999.</returns>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0002683A File Offset: 0x00024A3A
		public int Year
		{
			get
			{
				return this.GetDatePart(0);
			}
		}

		/// <summary>Returns an indication whether the specified year is a leap year.</summary>
		/// <param name="year">A 4-digit year.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="year" /> is a leap year; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999.</exception>
		// Token: 0x060009E7 RID: 2535 RVA: 0x00026843 File Offset: 0x00024A43
		public static bool IsLeapYear(int year)
		{
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", "Year must be between 1 and 9999.");
			}
			return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
		}

		/// <summary>Converts the string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent by using the conventions of the current thread culture.</summary>
		/// <param name="s">A string that contains a date and time to convert. See The string to parse for more information.</param>
		/// <returns>An object that is equivalent to the date and time contained in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not contain a valid string representation of a date and time.</exception>
		// Token: 0x060009E8 RID: 2536 RVA: 0x0002687A File Offset: 0x00024A7A
		public static DateTime Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return DateTimeParse.Parse(s, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None);
		}

		/// <summary>Converts the string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent by using culture-specific format information.</summary>
		/// <param name="s">A string that contains a date and time to convert. See The string to parse for more information.</param>
		/// <param name="provider">An object that supplies culture-specific format information about <paramref name="s" />.  See Parsing and cultural conventions</param>
		/// <returns>An object that is equivalent to the date and time contained in <paramref name="s" /> as specified by <paramref name="provider" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not contain a valid string representation of a date and time.</exception>
		// Token: 0x060009E9 RID: 2537 RVA: 0x00026897 File Offset: 0x00024A97
		public static DateTime Parse(string s, IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return DateTimeParse.Parse(s, DateTimeFormatInfo.GetInstance(provider), DateTimeStyles.None);
		}

		/// <summary>Converts the string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent by using culture-specific format information and a formatting style.</summary>
		/// <param name="s">A string that contains a date and time to convert. See The string to parse for more information.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />.  See Parsing and cultural conventions</param>
		/// <param name="styles">A bitwise combination of the enumeration values that indicates the style elements that can be present in <paramref name="s" /> for the parse operation to succeed, and that defines how to interpret the parsed date in relation to the current time zone or the current date. A typical value to specify is <see cref="F:System.Globalization.DateTimeStyles.None" />.</param>
		/// <returns>An object that is equivalent to the date and time contained in <paramref name="s" />, as specified by <paramref name="provider" /> and <paramref name="styles" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not contain a valid string representation of a date and time.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> contains an invalid combination of <see cref="T:System.Globalization.DateTimeStyles" /> values. For example, both <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />.</exception>
		// Token: 0x060009EA RID: 2538 RVA: 0x000268B5 File Offset: 0x00024AB5
		public static DateTime Parse(string s, IFormatProvider provider, DateTimeStyles styles)
		{
			DateTimeFormatInfo.ValidateStyles(styles, "styles");
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return DateTimeParse.Parse(s, DateTimeFormatInfo.GetInstance(provider), styles);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x000268DE File Offset: 0x00024ADE
		public static DateTime Parse(ReadOnlySpan<char> s, IFormatProvider provider = null, DateTimeStyles styles = DateTimeStyles.None)
		{
			DateTimeFormatInfo.ValidateStyles(styles, "styles");
			return DateTimeParse.Parse(s, DateTimeFormatInfo.GetInstance(provider), styles);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent using the specified format and culture-specific format information. The format of the string representation must match the specified format exactly.</summary>
		/// <param name="s">A string that contains a date and time to convert.</param>
		/// <param name="format">A format specifier that defines the required format of <paramref name="s" />. For more information, see the Remarks section.</param>
		/// <param name="provider">An object that supplies culture-specific format information about <paramref name="s" />.</param>
		/// <returns>An object that is equivalent to the date and time contained in <paramref name="s" />, as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> or <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> or <paramref name="format" /> is an empty string.  
		/// -or-  
		/// <paramref name="s" /> does not contain a date and time that corresponds to the pattern specified in <paramref name="format" />.  
		/// -or-  
		/// The hour component and the AM/PM designator in <paramref name="s" /> do not agree.</exception>
		// Token: 0x060009EC RID: 2540 RVA: 0x000268F8 File Offset: 0x00024AF8
		public static DateTime ParseExact(string s, string format, IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			if (format == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);
			}
			return DateTimeParse.ParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), DateTimeStyles.None);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent using the specified format, culture-specific format information, and style. The format of the string representation must match the specified format exactly or an exception is thrown.</summary>
		/// <param name="s">A string containing a date and time to convert.</param>
		/// <param name="format">A format specifier that defines the required format of <paramref name="s" />. For more information, see the Remarks section.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />.</param>
		/// <param name="style">A bitwise combination of the enumeration values that provides additional information about <paramref name="s" />, about style elements that may be present in <paramref name="s" />, or about the conversion from <paramref name="s" /> to a <see cref="T:System.DateTime" /> value. A typical value to specify is <see cref="F:System.Globalization.DateTimeStyles.None" />.</param>
		/// <returns>An object that is equivalent to the date and time contained in <paramref name="s" />, as specified by <paramref name="format" />, <paramref name="provider" />, and <paramref name="style" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> or <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> or <paramref name="format" /> is an empty string.  
		/// -or-  
		/// <paramref name="s" /> does not contain a date and time that corresponds to the pattern specified in <paramref name="format" />.  
		/// -or-  
		/// The hour component and the AM/PM designator in <paramref name="s" /> do not agree.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> contains an invalid combination of <see cref="T:System.Globalization.DateTimeStyles" /> values. For example, both <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />.</exception>
		// Token: 0x060009ED RID: 2541 RVA: 0x00026926 File Offset: 0x00024B26
		public static DateTime ParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			if (format == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);
			}
			return DateTimeParse.ParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), style);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0002695F File Offset: 0x00024B5F
		public static DateTime ParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, IFormatProvider provider, DateTimeStyles style = DateTimeStyles.None)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			return DateTimeParse.ParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), style);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent using the specified array of formats, culture-specific format information, and style. The format of the string representation must match at least one of the specified formats exactly or an exception is thrown.</summary>
		/// <param name="s">A string that contains a date and time to convert.</param>
		/// <param name="formats">An array of allowable formats of <paramref name="s" />. For more information, see the Remarks section.</param>
		/// <param name="provider">An object that supplies culture-specific format information about <paramref name="s" />.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.DateTimeStyles.None" />.</param>
		/// <returns>An object that is equivalent to the date and time contained in <paramref name="s" />, as specified by <paramref name="formats" />, <paramref name="provider" />, and <paramref name="style" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> or <paramref name="formats" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is an empty string.  
		/// -or-  
		/// an element of <paramref name="formats" /> is an empty string.  
		/// -or-  
		/// <paramref name="s" /> does not contain a date and time that corresponds to any element of <paramref name="formats" />.  
		/// -or-  
		/// The hour component and the AM/PM designator in <paramref name="s" /> do not agree.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> contains an invalid combination of <see cref="T:System.Globalization.DateTimeStyles" /> values. For example, both <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />.</exception>
		// Token: 0x060009EF RID: 2543 RVA: 0x0002697A File Offset: 0x00024B7A
		public static DateTime ParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return DateTimeParse.ParseExactMultiple(s, formats, DateTimeFormatInfo.GetInstance(provider), style);
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x000269A4 File Offset: 0x00024BA4
		public static DateTime ParseExact(ReadOnlySpan<char> s, string[] formats, IFormatProvider provider, DateTimeStyles style = DateTimeStyles.None)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			return DateTimeParse.ParseExactMultiple(s, formats, DateTimeFormatInfo.GetInstance(provider), style);
		}

		/// <summary>Subtracts the specified date and time from this instance.</summary>
		/// <param name="value">The date and time value to subtract.</param>
		/// <returns>A time interval that is equal to the date and time represented by this instance minus the date and time represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The result is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x060009F1 RID: 2545 RVA: 0x000269BF File Offset: 0x00024BBF
		public TimeSpan Subtract(DateTime value)
		{
			return new TimeSpan(this.InternalTicks - value.InternalTicks);
		}

		/// <summary>Subtracts the specified duration from this instance.</summary>
		/// <param name="value">The time interval to subtract.</param>
		/// <returns>An object that is equal to the date and time represented by this instance minus the time interval represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The result is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x060009F2 RID: 2546 RVA: 0x000269D4 File Offset: 0x00024BD4
		public DateTime Subtract(TimeSpan value)
		{
			long internalTicks = this.InternalTicks;
			long ticks = value._ticks;
			if (internalTicks < ticks || internalTicks - 3155378975999999999L > ticks)
			{
				throw new ArgumentOutOfRangeException("value", "The added or subtracted value results in an un-representable DateTime.");
			}
			return new DateTime((ulong)(internalTicks - ticks | (long)this.InternalKind));
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00026A20 File Offset: 0x00024C20
		private static double TicksToOADate(long value)
		{
			if (value == 0L)
			{
				return 0.0;
			}
			if (value < 864000000000L)
			{
				value += 599264352000000000L;
			}
			if (value < 31241376000000000L)
			{
				throw new OverflowException(" Not a legal OleAut date.");
			}
			long num = (value - 599264352000000000L) / 10000L;
			if (num < 0L)
			{
				long num2 = num % 86400000L;
				if (num2 != 0L)
				{
					num -= (86400000L + num2) * 2L;
				}
			}
			return (double)num / 86400000.0;
		}

		/// <summary>Converts the value of this instance to the equivalent OLE Automation date.</summary>
		/// <returns>A double-precision floating-point number that contains an OLE Automation date equivalent to the value of this instance.</returns>
		/// <exception cref="T:System.OverflowException">The value of this instance cannot be represented as an OLE Automation Date.</exception>
		// Token: 0x060009F4 RID: 2548 RVA: 0x00026AA8 File Offset: 0x00024CA8
		public double ToOADate()
		{
			return DateTime.TicksToOADate(this.InternalTicks);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to a Windows file time.</summary>
		/// <returns>The value of the current <see cref="T:System.DateTime" /> object expressed as a Windows file time.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting file time would represent a date and time before 12:00 midnight January 1, 1601 C.E. UTC.</exception>
		// Token: 0x060009F5 RID: 2549 RVA: 0x00026AB8 File Offset: 0x00024CB8
		public long ToFileTime()
		{
			return this.ToUniversalTime().ToFileTimeUtc();
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to a Windows file time.</summary>
		/// <returns>The value of the current <see cref="T:System.DateTime" /> object expressed as a Windows file time.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting file time would represent a date and time before 12:00 midnight January 1, 1601 C.E. UTC.</exception>
		// Token: 0x060009F6 RID: 2550 RVA: 0x00026AD4 File Offset: 0x00024CD4
		public long ToFileTimeUtc()
		{
			long num = (((this.InternalKind & 9223372036854775808UL) != 0UL) ? this.ToUniversalTime().InternalTicks : this.InternalTicks) - 504911232000000000L;
			if (num < 0L)
			{
				throw new ArgumentOutOfRangeException(null, "Not a valid Win32 FileTime.");
			}
			return num;
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to local time.</summary>
		/// <returns>An object whose <see cref="P:System.DateTime.Kind" /> property is <see cref="F:System.DateTimeKind.Local" />, and whose value is the local time equivalent to the value of the current <see cref="T:System.DateTime" /> object, or <see cref="F:System.DateTime.MaxValue" /> if the converted value is too large to be represented by a <see cref="T:System.DateTime" /> object, or <see cref="F:System.DateTime.MinValue" /> if the converted value is too small to be represented as a <see cref="T:System.DateTime" /> object.</returns>
		// Token: 0x060009F7 RID: 2551 RVA: 0x00026B24 File Offset: 0x00024D24
		public DateTime ToLocalTime()
		{
			return this.ToLocalTime(false);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00026B30 File Offset: 0x00024D30
		internal DateTime ToLocalTime(bool throwOnOverflow)
		{
			if (this.Kind == DateTimeKind.Local)
			{
				return this;
			}
			bool flag = false;
			bool isAmbiguousDst = false;
			long ticks = TimeZoneInfo.GetUtcOffsetFromUtc(this, TimeZoneInfo.Local, out flag, out isAmbiguousDst).Ticks;
			long num = this.Ticks + ticks;
			if (num > 3155378975999999999L)
			{
				if (throwOnOverflow)
				{
					throw new ArgumentException("Specified argument was out of the range of valid values.");
				}
				return new DateTime(3155378975999999999L, DateTimeKind.Local);
			}
			else
			{
				if (num >= 0L)
				{
					return new DateTime(num, DateTimeKind.Local, isAmbiguousDst);
				}
				if (throwOnOverflow)
				{
					throw new ArgumentException("Specified argument was out of the range of valid values.");
				}
				return new DateTime(0L, DateTimeKind.Local);
			}
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent long date string representation.</summary>
		/// <returns>A string that contains the long date string representation of the current <see cref="T:System.DateTime" /> object.</returns>
		// Token: 0x060009F9 RID: 2553 RVA: 0x00026BC6 File Offset: 0x00024DC6
		public string ToLongDateString()
		{
			return DateTimeFormat.Format(this, "D", null);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent long time string representation.</summary>
		/// <returns>A string that contains the long time string representation of the current <see cref="T:System.DateTime" /> object.</returns>
		// Token: 0x060009FA RID: 2554 RVA: 0x00026BD9 File Offset: 0x00024DD9
		public string ToLongTimeString()
		{
			return DateTimeFormat.Format(this, "T", null);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent short date string representation.</summary>
		/// <returns>A string that contains the short date string representation of the current <see cref="T:System.DateTime" /> object.</returns>
		// Token: 0x060009FB RID: 2555 RVA: 0x00026BEC File Offset: 0x00024DEC
		public string ToShortDateString()
		{
			return DateTimeFormat.Format(this, "d", null);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent short time string representation.</summary>
		/// <returns>A string that contains the short time string representation of the current <see cref="T:System.DateTime" /> object.</returns>
		// Token: 0x060009FC RID: 2556 RVA: 0x00026BFF File Offset: 0x00024DFF
		public string ToShortTimeString()
		{
			return DateTimeFormat.Format(this, "t", null);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent string representation using the formatting conventions of the current culture.</summary>
		/// <returns>A string representation of the value of the current <see cref="T:System.DateTime" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by the current culture.</exception>
		// Token: 0x060009FD RID: 2557 RVA: 0x00026C12 File Offset: 0x00024E12
		public override string ToString()
		{
			return DateTimeFormat.Format(this, null, null);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent string representation using the specified format and the formatting conventions of the current culture.</summary>
		/// <param name="format">A standard or custom date and time format string.</param>
		/// <returns>A string representation of value of the current <see cref="T:System.DateTime" /> object as specified by <paramref name="format" />.</returns>
		/// <exception cref="T:System.FormatException">The length of <paramref name="format" /> is 1, and it is not one of the format specifier characters defined for <see cref="T:System.Globalization.DateTimeFormatInfo" />.  
		///  -or-  
		///  <paramref name="format" /> does not contain a valid custom format pattern.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by the current culture.</exception>
		// Token: 0x060009FE RID: 2558 RVA: 0x00026C21 File Offset: 0x00024E21
		public string ToString(string format)
		{
			return DateTimeFormat.Format(this, format, null);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A string representation of value of the current <see cref="T:System.DateTime" /> object as specified by <paramref name="provider" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by <paramref name="provider" />.</exception>
		// Token: 0x060009FF RID: 2559 RVA: 0x00026C30 File Offset: 0x00024E30
		public string ToString(IFormatProvider provider)
		{
			return DateTimeFormat.Format(this, null, provider);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <param name="format">A standard or custom date and time format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A string representation of value of the current <see cref="T:System.DateTime" /> object as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <exception cref="T:System.FormatException">The length of <paramref name="format" /> is 1, and it is not one of the format specifier characters defined for <see cref="T:System.Globalization.DateTimeFormatInfo" />.  
		///  -or-  
		///  <paramref name="format" /> does not contain a valid custom format pattern.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by <paramref name="provider" />.</exception>
		// Token: 0x06000A00 RID: 2560 RVA: 0x00026C3F File Offset: 0x00024E3F
		public string ToString(string format, IFormatProvider provider)
		{
			return DateTimeFormat.Format(this, format, provider);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00026C4E File Offset: 0x00024E4E
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), IFormatProvider provider = null)
		{
			return DateTimeFormat.TryFormat(this, destination, out charsWritten, format, provider);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to Coordinated Universal Time (UTC).</summary>
		/// <returns>An object whose <see cref="P:System.DateTime.Kind" /> property is <see cref="F:System.DateTimeKind.Utc" />, and whose value is the UTC equivalent to the value of the current <see cref="T:System.DateTime" /> object, or <see cref="F:System.DateTime.MaxValue" /> if the converted value is too large to be represented by a <see cref="T:System.DateTime" /> object, or <see cref="F:System.DateTime.MinValue" /> if the converted value is too small to be represented by a <see cref="T:System.DateTime" /> object.</returns>
		// Token: 0x06000A02 RID: 2562 RVA: 0x00026C60 File Offset: 0x00024E60
		public DateTime ToUniversalTime()
		{
			return TimeZoneInfo.ConvertTimeToUtc(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent and returns a value that indicates whether the conversion succeeded.</summary>
		/// <param name="s">A string containing a date and time to convert.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.DateTime" /> value equivalent to the date and time contained in <paramref name="s" />, if the conversion succeeded, or <see cref="F:System.DateTime.MinValue" /> if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is <see langword="null" />, is an empty string (""), or does not contain a valid string representation of a date and time. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="s" /> parameter was converted successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A03 RID: 2563 RVA: 0x00026C6E File Offset: 0x00024E6E
		public static bool TryParse(string s, out DateTime result)
		{
			if (s == null)
			{
				result = default(DateTime);
				return false;
			}
			return DateTimeParse.TryParse(s, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out result);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00026C8E File Offset: 0x00024E8E
		public static bool TryParse(ReadOnlySpan<char> s, out DateTime result)
		{
			return DateTimeParse.TryParse(s, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out result);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent using the specified culture-specific format information and formatting style, and returns a value that indicates whether the conversion succeeded.</summary>
		/// <param name="s">A string containing a date and time to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />.</param>
		/// <param name="styles">A bitwise combination of enumeration values that defines how to interpret the parsed date in relation to the current time zone or the current date. A typical value to specify is <see cref="F:System.Globalization.DateTimeStyles.None" />.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.DateTime" /> value equivalent to the date and time contained in <paramref name="s" />, if the conversion succeeded, or <see cref="F:System.DateTime.MinValue" /> if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is <see langword="null" />, is an empty string (""), or does not contain a valid string representation of a date and time. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="s" /> parameter was converted successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> is not a valid <see cref="T:System.Globalization.DateTimeStyles" /> value.  
		/// -or-  
		/// <paramref name="styles" /> contains an invalid combination of <see cref="T:System.Globalization.DateTimeStyles" /> values (for example, both <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />).</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="provider" /> is a neutral culture and cannot be used in a parsing operation.</exception>
		// Token: 0x06000A05 RID: 2565 RVA: 0x00026C9D File Offset: 0x00024E9D
		public static bool TryParse(string s, IFormatProvider provider, DateTimeStyles styles, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(styles, "styles");
			if (s == null)
			{
				result = default(DateTime);
				return false;
			}
			return DateTimeParse.TryParse(s, DateTimeFormatInfo.GetInstance(provider), styles, out result);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00026CC9 File Offset: 0x00024EC9
		public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider provider, DateTimeStyles styles, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(styles, "styles");
			return DateTimeParse.TryParse(s, DateTimeFormatInfo.GetInstance(provider), styles, out result);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent using the specified format, culture-specific format information, and style. The format of the string representation must match the specified format exactly. The method returns a value that indicates whether the conversion succeeded.</summary>
		/// <param name="s">A string containing a date and time to convert.</param>
		/// <param name="format">The required format of <paramref name="s" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />.</param>
		/// <param name="style">A bitwise combination of one or more enumeration values that indicate the permitted format of <paramref name="s" />.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.DateTime" /> value equivalent to the date and time contained in <paramref name="s" />, if the conversion succeeded, or <see cref="F:System.DateTime.MinValue" /> if the conversion failed. The conversion fails if either the <paramref name="s" /> or <paramref name="format" /> parameter is <see langword="null" />, is an empty string, or does not contain a date and time that correspond to the pattern specified in <paramref name="format" />. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="s" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> is not a valid <see cref="T:System.Globalization.DateTimeStyles" /> value.  
		/// -or-  
		/// <paramref name="styles" /> contains an invalid combination of <see cref="T:System.Globalization.DateTimeStyles" /> values (for example, both <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />).</exception>
		// Token: 0x06000A07 RID: 2567 RVA: 0x00026CE4 File Offset: 0x00024EE4
		public static bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			if (s == null || format == null)
			{
				result = default(DateTime);
				return false;
			}
			return DateTimeParse.TryParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), style, out result);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00026D1B File Offset: 0x00024F1B
		public static bool TryParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, IFormatProvider provider, DateTimeStyles style, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			return DateTimeParse.TryParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), style, out result);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent using the specified array of formats, culture-specific format information, and style. The format of the string representation must match at least one of the specified formats exactly. The method returns a value that indicates whether the conversion succeeded.</summary>
		/// <param name="s">A string that contains a date and time to convert.</param>
		/// <param name="formats">An array of allowable formats of <paramref name="s" />.</param>
		/// <param name="provider">An object that supplies culture-specific format information about <paramref name="s" />.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.DateTimeStyles.None" />.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.DateTime" /> value equivalent to the date and time contained in <paramref name="s" />, if the conversion succeeded, or <see cref="F:System.DateTime.MinValue" /> if the conversion failed. The conversion fails if <paramref name="s" /> or <paramref name="formats" /> is <see langword="null" />, <paramref name="s" /> or an element of <paramref name="formats" /> is an empty string, or the format of <paramref name="s" /> is not exactly as specified by at least one of the format patterns in <paramref name="formats" />. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="s" /> parameter was converted successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> is not a valid <see cref="T:System.Globalization.DateTimeStyles" /> value.  
		/// -or-  
		/// <paramref name="styles" /> contains an invalid combination of <see cref="T:System.Globalization.DateTimeStyles" /> values (for example, both <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />).</exception>
		// Token: 0x06000A09 RID: 2569 RVA: 0x00026D38 File Offset: 0x00024F38
		public static bool TryParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			if (s == null)
			{
				result = default(DateTime);
				return false;
			}
			return DateTimeParse.TryParseExactMultiple(s, formats, DateTimeFormatInfo.GetInstance(provider), style, out result);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00026D67 File Offset: 0x00024F67
		public static bool TryParseExact(ReadOnlySpan<char> s, string[] formats, IFormatProvider provider, DateTimeStyles style, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			return DateTimeParse.TryParseExactMultiple(s, formats, DateTimeFormatInfo.GetInstance(provider), style, out result);
		}

		/// <summary>Adds a specified time interval to a specified date and time, yielding a new date and time.</summary>
		/// <param name="d">The date and time value to add.</param>
		/// <param name="t">The time interval to add.</param>
		/// <returns>An object that is the sum of the values of <paramref name="d" /> and <paramref name="t" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x06000A0B RID: 2571 RVA: 0x00026D84 File Offset: 0x00024F84
		public static DateTime operator +(DateTime d, TimeSpan t)
		{
			long internalTicks = d.InternalTicks;
			long ticks = t._ticks;
			if (ticks > 3155378975999999999L - internalTicks || ticks < 0L - internalTicks)
			{
				throw new ArgumentOutOfRangeException("t", "The added or subtracted value results in an un-representable DateTime.");
			}
			return new DateTime((ulong)(internalTicks + ticks | (long)d.InternalKind));
		}

		/// <summary>Subtracts a specified time interval from a specified date and time and returns a new date and time.</summary>
		/// <param name="d">The date and time value to subtract from.</param>
		/// <param name="t">The time interval to subtract.</param>
		/// <returns>An object whose value is the value of <paramref name="d" /> minus the value of <paramref name="t" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x06000A0C RID: 2572 RVA: 0x00026DD8 File Offset: 0x00024FD8
		public static DateTime operator -(DateTime d, TimeSpan t)
		{
			long internalTicks = d.InternalTicks;
			long ticks = t._ticks;
			if (internalTicks < ticks || internalTicks - 3155378975999999999L > ticks)
			{
				throw new ArgumentOutOfRangeException("t", "The added or subtracted value results in an un-representable DateTime.");
			}
			return new DateTime((ulong)(internalTicks - ticks | (long)d.InternalKind));
		}

		/// <summary>Subtracts a specified date and time from another specified date and time and returns a time interval.</summary>
		/// <param name="d1">The date and time value to subtract from (the minuend).</param>
		/// <param name="d2">The date and time value to subtract (the subtrahend).</param>
		/// <returns>The time interval between <paramref name="d1" /> and <paramref name="d2" />; that is, <paramref name="d1" /> minus <paramref name="d2" />.</returns>
		// Token: 0x06000A0D RID: 2573 RVA: 0x00026E26 File Offset: 0x00025026
		public static TimeSpan operator -(DateTime d1, DateTime d2)
		{
			return new TimeSpan(d1.InternalTicks - d2.InternalTicks);
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.DateTime" /> are equal.</summary>
		/// <param name="d1">The first object to compare.</param>
		/// <param name="d2">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d1" /> and <paramref name="d2" /> represent the same date and time; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A0E RID: 2574 RVA: 0x00026282 File Offset: 0x00024482
		public static bool operator ==(DateTime d1, DateTime d2)
		{
			return d1.InternalTicks == d2.InternalTicks;
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.DateTime" /> are not equal.</summary>
		/// <param name="d1">The first object to compare.</param>
		/// <param name="d2">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d1" /> and <paramref name="d2" /> do not represent the same date and time; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A0F RID: 2575 RVA: 0x00026E3C File Offset: 0x0002503C
		public static bool operator !=(DateTime d1, DateTime d2)
		{
			return d1.InternalTicks != d2.InternalTicks;
		}

		/// <summary>Determines whether one specified <see cref="T:System.DateTime" /> is earlier than another specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="t1">The first object to compare.</param>
		/// <param name="t2">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="t1" /> is earlier than <paramref name="t2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A10 RID: 2576 RVA: 0x00026E51 File Offset: 0x00025051
		public static bool operator <(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks < t2.InternalTicks;
		}

		/// <summary>Determines whether one specified <see cref="T:System.DateTime" /> represents a date and time that is the same as or earlier than another specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="t1">The first object to compare.</param>
		/// <param name="t2">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="t1" /> is the same as or earlier than <paramref name="t2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A11 RID: 2577 RVA: 0x00026E63 File Offset: 0x00025063
		public static bool operator <=(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks <= t2.InternalTicks;
		}

		/// <summary>Determines whether one specified <see cref="T:System.DateTime" /> is later than another specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="t1">The first object to compare.</param>
		/// <param name="t2">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="t1" /> is later than <paramref name="t2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A12 RID: 2578 RVA: 0x00026E78 File Offset: 0x00025078
		public static bool operator >(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks > t2.InternalTicks;
		}

		/// <summary>Determines whether one specified <see cref="T:System.DateTime" /> represents a date and time that is the same as or later than another specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="t1">The first object to compare.</param>
		/// <param name="t2">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="t1" /> is the same as or later than <paramref name="t2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A13 RID: 2579 RVA: 0x00026E8A File Offset: 0x0002508A
		public static bool operator >=(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks >= t2.InternalTicks;
		}

		/// <summary>Converts the value of this instance to all the string representations supported by the standard date and time format specifiers.</summary>
		/// <returns>A string array where each element is the representation of the value of this instance formatted with one of the standard date and time format specifiers.</returns>
		// Token: 0x06000A14 RID: 2580 RVA: 0x00026E9F File Offset: 0x0002509F
		public string[] GetDateTimeFormats()
		{
			return this.GetDateTimeFormats(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of this instance to all the string representations supported by the standard date and time format specifiers and the specified culture-specific formatting information.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information about this instance.</param>
		/// <returns>A string array where each element is the representation of the value of this instance formatted with one of the standard date and time format specifiers.</returns>
		// Token: 0x06000A15 RID: 2581 RVA: 0x00026EAC File Offset: 0x000250AC
		public string[] GetDateTimeFormats(IFormatProvider provider)
		{
			return DateTimeFormat.GetAllDateTimes(this, DateTimeFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the value of this instance to all the string representations supported by the specified standard date and time format specifier.</summary>
		/// <param name="format">A standard date and time format string.</param>
		/// <returns>A string array where each element is the representation of the value of this instance formatted with the <paramref name="format" /> standard date and time format specifier.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid standard date and time format specifier character.</exception>
		// Token: 0x06000A16 RID: 2582 RVA: 0x00026EBF File Offset: 0x000250BF
		public string[] GetDateTimeFormats(char format)
		{
			return this.GetDateTimeFormats(format, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of this instance to all the string representations supported by the specified standard date and time format specifier and culture-specific formatting information.</summary>
		/// <param name="format">A date and time format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about this instance.</param>
		/// <returns>A string array where each element is the representation of the value of this instance formatted with one of the standard date and time format specifiers.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid standard date and time format specifier character.</exception>
		// Token: 0x06000A17 RID: 2583 RVA: 0x00026ECD File Offset: 0x000250CD
		public string[] GetDateTimeFormats(char format, IFormatProvider provider)
		{
			return DateTimeFormat.GetAllDateTimes(this, format, DateTimeFormatInfo.GetInstance(provider));
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.DateTime" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.DateTime" />.</returns>
		// Token: 0x06000A18 RID: 2584 RVA: 0x00026EE1 File Offset: 0x000250E1
		public TypeCode GetTypeCode()
		{
			return TypeCode.DateTime;
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000A19 RID: 2585 RVA: 0x00026EE5 File Offset: 0x000250E5
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Boolean"));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000A1A RID: 2586 RVA: 0x00026F00 File Offset: 0x00025100
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Char"));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000A1B RID: 2587 RVA: 0x00026F1B File Offset: 0x0002511B
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "SByte"));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000A1C RID: 2588 RVA: 0x00026F36 File Offset: 0x00025136
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Byte"));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000A1D RID: 2589 RVA: 0x00026F51 File Offset: 0x00025151
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Int16"));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000A1E RID: 2590 RVA: 0x00026F6C File Offset: 0x0002516C
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "UInt16"));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000A1F RID: 2591 RVA: 0x00026F87 File Offset: 0x00025187
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Int32"));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000A20 RID: 2592 RVA: 0x00026FA2 File Offset: 0x000251A2
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "UInt32"));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000A21 RID: 2593 RVA: 0x00026FBD File Offset: 0x000251BD
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Int64"));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000A22 RID: 2594 RVA: 0x00026FD8 File Offset: 0x000251D8
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "UInt64"));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000A23 RID: 2595 RVA: 0x00026FF3 File Offset: 0x000251F3
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Single"));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000A24 RID: 2596 RVA: 0x0002700E File Offset: 0x0002520E
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Double"));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000A25 RID: 2597 RVA: 0x00027029 File Offset: 0x00025229
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Decimal"));
		}

		/// <summary>Returns the current <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The current object.</returns>
		// Token: 0x06000A26 RID: 2598 RVA: 0x00027044 File Offset: 0x00025244
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>Converts the current <see cref="T:System.DateTime" /> object to an object of a specified type.</summary>
		/// <param name="type">The desired type.</param>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>An object of the type specified by the <paramref name="type" /> parameter, with a value equivalent to the current <see cref="T:System.DateTime" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DateTime" /> type.</exception>
		// Token: 0x06000A27 RID: 2599 RVA: 0x0002704C File Offset: 0x0002524C
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00027060 File Offset: 0x00025260
		internal static bool TryCreate(int year, int month, int day, int hour, int minute, int second, int millisecond, out DateTime result)
		{
			result = DateTime.MinValue;
			if (year < 1 || year > 9999 || month < 1 || month > 12)
			{
				return false;
			}
			int[] array = DateTime.IsLeapYear(year) ? DateTime.s_daysToMonth366 : DateTime.s_daysToMonth365;
			if (day < 1 || day > array[month] - array[month - 1])
			{
				return false;
			}
			if (hour < 0 || hour >= 24 || minute < 0 || minute >= 60 || second < 0 || second >= 60)
			{
				return false;
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				return false;
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				return false;
			}
			result = new DateTime(num, DateTimeKind.Unspecified);
			return true;
		}

		/// <summary>Gets a <see cref="T:System.DateTime" /> object that is set to the current date and time on this computer, expressed as the Coordinated Universal Time (UTC).</summary>
		/// <returns>An object whose value is the current UTC date and time.</returns>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x0002712B File Offset: 0x0002532B
		public static DateTime UtcNow
		{
			get
			{
				return new DateTime((ulong)(DateTime.GetSystemTimeAsFileTime() + 504911232000000000L | 4611686018427387904L));
			}
		}

		// Token: 0x06000A2A RID: 2602
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long GetSystemTimeAsFileTime();

		// Token: 0x06000A2B RID: 2603 RVA: 0x0002714B File Offset: 0x0002534B
		internal long ToBinaryRaw()
		{
			return (long)this._dateData;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00027154 File Offset: 0x00025354
		// Note: this type is marked as 'beforefieldinit'.
		static DateTime()
		{
		}

		// Token: 0x04001093 RID: 4243
		private const long TicksPerMillisecond = 10000L;

		// Token: 0x04001094 RID: 4244
		private const long TicksPerSecond = 10000000L;

		// Token: 0x04001095 RID: 4245
		private const long TicksPerMinute = 600000000L;

		// Token: 0x04001096 RID: 4246
		private const long TicksPerHour = 36000000000L;

		// Token: 0x04001097 RID: 4247
		private const long TicksPerDay = 864000000000L;

		// Token: 0x04001098 RID: 4248
		private const int MillisPerSecond = 1000;

		// Token: 0x04001099 RID: 4249
		private const int MillisPerMinute = 60000;

		// Token: 0x0400109A RID: 4250
		private const int MillisPerHour = 3600000;

		// Token: 0x0400109B RID: 4251
		private const int MillisPerDay = 86400000;

		// Token: 0x0400109C RID: 4252
		private const int DaysPerYear = 365;

		// Token: 0x0400109D RID: 4253
		private const int DaysPer4Years = 1461;

		// Token: 0x0400109E RID: 4254
		private const int DaysPer100Years = 36524;

		// Token: 0x0400109F RID: 4255
		private const int DaysPer400Years = 146097;

		// Token: 0x040010A0 RID: 4256
		private const int DaysTo1601 = 584388;

		// Token: 0x040010A1 RID: 4257
		private const int DaysTo1899 = 693593;

		// Token: 0x040010A2 RID: 4258
		internal const int DaysTo1970 = 719162;

		// Token: 0x040010A3 RID: 4259
		private const int DaysTo10000 = 3652059;

		// Token: 0x040010A4 RID: 4260
		internal const long MinTicks = 0L;

		// Token: 0x040010A5 RID: 4261
		internal const long MaxTicks = 3155378975999999999L;

		// Token: 0x040010A6 RID: 4262
		private const long MaxMillis = 315537897600000L;

		// Token: 0x040010A7 RID: 4263
		internal const long UnixEpochTicks = 621355968000000000L;

		// Token: 0x040010A8 RID: 4264
		private const long FileTimeOffset = 504911232000000000L;

		// Token: 0x040010A9 RID: 4265
		private const long DoubleDateOffset = 599264352000000000L;

		// Token: 0x040010AA RID: 4266
		private const long OADateMinAsTicks = 31241376000000000L;

		// Token: 0x040010AB RID: 4267
		private const double OADateMinAsDouble = -657435.0;

		// Token: 0x040010AC RID: 4268
		private const double OADateMaxAsDouble = 2958466.0;

		// Token: 0x040010AD RID: 4269
		private const int DatePartYear = 0;

		// Token: 0x040010AE RID: 4270
		private const int DatePartDayOfYear = 1;

		// Token: 0x040010AF RID: 4271
		private const int DatePartMonth = 2;

		// Token: 0x040010B0 RID: 4272
		private const int DatePartDay = 3;

		// Token: 0x040010B1 RID: 4273
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

		// Token: 0x040010B2 RID: 4274
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

		/// <summary>Represents the smallest possible value of <see cref="T:System.DateTime" />. This field is read-only.</summary>
		// Token: 0x040010B3 RID: 4275
		public static readonly DateTime MinValue = new DateTime(0L, DateTimeKind.Unspecified);

		/// <summary>Represents the largest possible value of <see cref="T:System.DateTime" />. This field is read-only.</summary>
		// Token: 0x040010B4 RID: 4276
		public static readonly DateTime MaxValue = new DateTime(3155378975999999999L, DateTimeKind.Unspecified);

		// Token: 0x040010B5 RID: 4277
		public static readonly DateTime UnixEpoch = new DateTime(621355968000000000L, DateTimeKind.Utc);

		// Token: 0x040010B6 RID: 4278
		private const ulong TicksMask = 4611686018427387903UL;

		// Token: 0x040010B7 RID: 4279
		private const ulong FlagsMask = 13835058055282163712UL;

		// Token: 0x040010B8 RID: 4280
		private const ulong LocalMask = 9223372036854775808UL;

		// Token: 0x040010B9 RID: 4281
		private const long TicksCeiling = 4611686018427387904L;

		// Token: 0x040010BA RID: 4282
		private const ulong KindUnspecified = 0UL;

		// Token: 0x040010BB RID: 4283
		private const ulong KindUtc = 4611686018427387904UL;

		// Token: 0x040010BC RID: 4284
		private const ulong KindLocal = 9223372036854775808UL;

		// Token: 0x040010BD RID: 4285
		private const ulong KindLocalAmbiguousDst = 13835058055282163712UL;

		// Token: 0x040010BE RID: 4286
		private const int KindShift = 62;

		// Token: 0x040010BF RID: 4287
		private const string TicksField = "ticks";

		// Token: 0x040010C0 RID: 4288
		private const string DateDataField = "dateData";

		// Token: 0x040010C1 RID: 4289
		private readonly ulong _dateData;
	}
}
