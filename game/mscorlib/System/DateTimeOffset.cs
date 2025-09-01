using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Represents a point in time, typically expressed as a date and time of day, relative to Coordinated Universal Time (UTC).</summary>
	// Token: 0x0200010E RID: 270
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public readonly struct DateTimeOffset : IComparable, IFormattable, IComparable<DateTimeOffset>, IEquatable<DateTimeOffset>, ISerializable, IDeserializationCallback, ISpanFormattable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DateTimeOffset" /> structure using the specified number of ticks and offset.</summary>
		/// <param name="ticks">A date and time expressed as the number of 100-nanosecond intervals that have elapsed since 12:00:00 midnight on January 1, 0001.</param>
		/// <param name="offset">The time's offset from Coordinated Universal Time (UTC).</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> is not specified in whole minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.DateTimeOffset.UtcDateTime" /> property is earlier than <see cref="F:System.DateTimeOffset.MinValue" /> or later than <see cref="F:System.DateTimeOffset.MaxValue" />.  
		///  -or-  
		///  <paramref name="ticks" /> is less than <see langword="DateTimeOffset.MinValue.Ticks" /> or greater than <see langword="DateTimeOffset.MaxValue.Ticks" />.  
		///  -or-  
		///  <paramref name="Offset" /> s less than -14 hours or greater than 14 hours.</exception>
		// Token: 0x06000A2D RID: 2605 RVA: 0x000271C4 File Offset: 0x000253C4
		public DateTimeOffset(long ticks, TimeSpan offset)
		{
			this._offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			DateTime dateTime = new DateTime(ticks);
			this._dateTime = DateTimeOffset.ValidateDate(dateTime, offset);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTimeOffset" /> structure using the specified <see cref="T:System.DateTime" /> value.</summary>
		/// <param name="dateTime">A date and time.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The Coordinated Universal Time (UTC) date and time that results from applying the offset is earlier than <see cref="F:System.DateTimeOffset.MinValue" />.  
		///  -or-  
		///  The UTC date and time that results from applying the offset is later than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000A2E RID: 2606 RVA: 0x000271F4 File Offset: 0x000253F4
		public DateTimeOffset(DateTime dateTime)
		{
			TimeSpan localUtcOffset;
			if (dateTime.Kind != DateTimeKind.Utc)
			{
				localUtcOffset = TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
			}
			else
			{
				localUtcOffset = new TimeSpan(0L);
			}
			this._offsetMinutes = DateTimeOffset.ValidateOffset(localUtcOffset);
			this._dateTime = DateTimeOffset.ValidateDate(dateTime, localUtcOffset);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTimeOffset" /> structure using the specified <see cref="T:System.DateTime" /> value and offset.</summary>
		/// <param name="dateTime">A date and time.</param>
		/// <param name="offset">The time's offset from Coordinated Universal Time (UTC).</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dateTime.Kind" /> equals <see cref="F:System.DateTimeKind.Utc" /> and <paramref name="offset" /> does not equal zero.  
		/// -or-  
		/// <paramref name="dateTime.Kind" /> equals <see cref="F:System.DateTimeKind.Local" /> and <paramref name="offset" /> does not equal the offset of the system's local time zone.  
		/// -or-  
		/// <paramref name="offset" /> is not specified in whole minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than -14 hours or greater than 14 hours.  
		/// -or-  
		/// <see cref="P:System.DateTimeOffset.UtcDateTime" /> is less than <see cref="F:System.DateTimeOffset.MinValue" /> or greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000A2F RID: 2607 RVA: 0x00027238 File Offset: 0x00025438
		public DateTimeOffset(DateTime dateTime, TimeSpan offset)
		{
			if (dateTime.Kind == DateTimeKind.Local)
			{
				if (offset != TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime))
				{
					throw new ArgumentException("The UTC Offset of the local dateTime parameter does not match the offset argument.", "offset");
				}
			}
			else if (dateTime.Kind == DateTimeKind.Utc && offset != TimeSpan.Zero)
			{
				throw new ArgumentException("The UTC Offset for Utc DateTime instances must be 0.", "offset");
			}
			this._offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			this._dateTime = DateTimeOffset.ValidateDate(dateTime, offset);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTimeOffset" /> structure using the specified year, month, day, hour, minute, second, and offset.</summary>
		/// <param name="year">The year (1 through 9999).</param>
		/// <param name="month">The month (1 through 12).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="hour">The hours (0 through 23).</param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <param name="offset">The time's offset from Coordinated Universal Time (UTC).</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> does not represent whole minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than one or greater than 9999.  
		/// -or-  
		/// <paramref name="month" /> is less than one or greater than 12.  
		/// -or-  
		/// <paramref name="day" /> is less than one or greater than the number of days in <paramref name="month" />.  
		/// -or-  
		/// <paramref name="hour" /> is less than zero or greater than 23.  
		/// -or-  
		/// <paramref name="minute" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="second" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="offset" /> is less than -14 hours or greater than 14 hours.  
		/// -or-  
		/// The <see cref="P:System.DateTimeOffset.UtcDateTime" /> property is earlier than <see cref="F:System.DateTimeOffset.MinValue" /> or later than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000A30 RID: 2608 RVA: 0x000272AE File Offset: 0x000254AE
		public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, TimeSpan offset)
		{
			this._offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			this._dateTime = DateTimeOffset.ValidateDate(new DateTime(year, month, day, hour, minute, second), offset);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTimeOffset" /> structure using the specified year, month, day, hour, minute, second, millisecond, and offset.</summary>
		/// <param name="year">The year (1 through 9999).</param>
		/// <param name="month">The month (1 through 12).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="hour">The hours (0 through 23).</param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <param name="millisecond">The milliseconds (0 through 999).</param>
		/// <param name="offset">The time's offset from Coordinated Universal Time (UTC).</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> does not represent whole minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than one or greater than 9999.  
		/// -or-  
		/// <paramref name="month" /> is less than one or greater than 12.  
		/// -or-  
		/// <paramref name="day" /> is less than one or greater than the number of days in <paramref name="month" />.  
		/// -or-  
		/// <paramref name="hour" /> is less than zero or greater than 23.  
		/// -or-  
		/// <paramref name="minute" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="second" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="millisecond" /> is less than 0 or greater than 999.  
		/// -or-  
		/// <paramref name="offset" /> is less than -14 or greater than 14.  
		/// -or-  
		/// The <see cref="P:System.DateTimeOffset.UtcDateTime" /> property is earlier than <see cref="F:System.DateTimeOffset.MinValue" /> or later than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000A31 RID: 2609 RVA: 0x000272D8 File Offset: 0x000254D8
		public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, int millisecond, TimeSpan offset)
		{
			this._offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			this._dateTime = DateTimeOffset.ValidateDate(new DateTime(year, month, day, hour, minute, second, millisecond), offset);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTimeOffset" /> structure using the specified year, month, day, hour, minute, second, millisecond, and offset of a specified calendar.</summary>
		/// <param name="year">The year.</param>
		/// <param name="month">The month (1 through 12).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="hour">The hours (0 through 23).</param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <param name="millisecond">The milliseconds (0 through 999).</param>
		/// <param name="calendar">The calendar that is used to interpret <paramref name="year" />, <paramref name="month" />, and <paramref name="day" />.</param>
		/// <param name="offset">The time's offset from Coordinated Universal Time (UTC).</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> does not represent whole minutes.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="calendar" /> cannot be <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than the <paramref name="calendar" /> parameter's <see langword="MinSupportedDateTime.Year" /> or greater than <see langword="MaxSupportedDateTime.Year" />.  
		/// -or-  
		/// <paramref name="month" /> is either less than or greater than the number of months in <paramref name="year" /> in the <paramref name="calendar" />.  
		/// -or-  
		/// <paramref name="day" /> is less than one or greater than the number of days in <paramref name="month" />.  
		/// -or-  
		/// <paramref name="hour" /> is less than zero or greater than 23.  
		/// -or-  
		/// <paramref name="minute" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="second" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="millisecond" /> is less than 0 or greater than 999.  
		/// -or-  
		/// <paramref name="offset" /> is less than -14 hours or greater than 14 hours.  
		/// -or-  
		/// The <paramref name="year" />, <paramref name="month" />, and <paramref name="day" /> parameters cannot be represented as a date and time value.  
		/// -or-  
		/// The <see cref="P:System.DateTimeOffset.UtcDateTime" /> property is earlier than <see cref="F:System.DateTimeOffset.MinValue" /> or later than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000A32 RID: 2610 RVA: 0x00027304 File Offset: 0x00025504
		public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar, TimeSpan offset)
		{
			this._offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			this._dateTime = DateTimeOffset.ValidateDate(new DateTime(year, month, day, hour, minute, second, millisecond, calendar), offset);
		}

		/// <summary>Gets a <see cref="T:System.DateTimeOffset" /> object that is set to the current date and time on the current computer, with the offset set to the local time's offset from Coordinated Universal Time (UTC).</summary>
		/// <returns>A <see cref="T:System.DateTimeOffset" /> object whose date and time is the current local time and whose offset is the local time zone's offset from Coordinated Universal Time (UTC).</returns>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x0002733D File Offset: 0x0002553D
		public static DateTimeOffset Now
		{
			get
			{
				return new DateTimeOffset(DateTime.Now);
			}
		}

		/// <summary>Gets a <see cref="T:System.DateTimeOffset" /> object whose date and time are set to the current Coordinated Universal Time (UTC) date and time and whose offset is <see cref="F:System.TimeSpan.Zero" />.</summary>
		/// <returns>An object whose date and time is the current Coordinated Universal Time (UTC) and whose offset is <see cref="F:System.TimeSpan.Zero" />.</returns>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x00027349 File Offset: 0x00025549
		public static DateTimeOffset UtcNow
		{
			get
			{
				return new DateTimeOffset(DateTime.UtcNow);
			}
		}

		/// <summary>Gets a <see cref="T:System.DateTime" /> value that represents the date and time of the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The date and time of the current <see cref="T:System.DateTimeOffset" /> object.</returns>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x00027355 File Offset: 0x00025555
		public DateTime DateTime
		{
			get
			{
				return this.ClockDateTime;
			}
		}

		/// <summary>Gets a <see cref="T:System.DateTime" /> value that represents the Coordinated Universal Time (UTC) date and time of the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The Coordinated Universal Time (UTC) date and time of the current <see cref="T:System.DateTimeOffset" /> object.</returns>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x0002735D File Offset: 0x0002555D
		public DateTime UtcDateTime
		{
			get
			{
				return DateTime.SpecifyKind(this._dateTime, DateTimeKind.Utc);
			}
		}

		/// <summary>Gets a <see cref="T:System.DateTime" /> value that represents the local date and time of the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The local date and time of the current <see cref="T:System.DateTimeOffset" /> object.</returns>
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x0002736C File Offset: 0x0002556C
		public DateTime LocalDateTime
		{
			get
			{
				return this.UtcDateTime.ToLocalTime();
			}
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTimeOffset" /> object to the date and time specified by an offset value.</summary>
		/// <param name="offset">The offset to convert the <see cref="T:System.DateTimeOffset" /> value to.</param>
		/// <returns>An object that is equal to the original <see cref="T:System.DateTimeOffset" /> object (that is, their <see cref="M:System.DateTimeOffset.ToUniversalTime" /> methods return identical points in time) but whose <see cref="P:System.DateTimeOffset.Offset" /> property is set to <paramref name="offset" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTimeOffset" /> object has a <see cref="P:System.DateTimeOffset.DateTime" /> value earlier than <see cref="F:System.DateTimeOffset.MinValue" />.  
		///  -or-  
		///  The resulting <see cref="T:System.DateTimeOffset" /> object has a <see cref="P:System.DateTimeOffset.DateTime" /> value later than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than -14 hours.  
		/// -or-  
		/// <paramref name="offset" /> is greater than 14 hours.</exception>
		// Token: 0x06000A38 RID: 2616 RVA: 0x00027388 File Offset: 0x00025588
		public DateTimeOffset ToOffset(TimeSpan offset)
		{
			return new DateTimeOffset((this._dateTime + offset).Ticks, offset);
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x000273B0 File Offset: 0x000255B0
		private DateTime ClockDateTime
		{
			get
			{
				return new DateTime((this._dateTime + this.Offset).Ticks, DateTimeKind.Unspecified);
			}
		}

		/// <summary>Gets a <see cref="T:System.DateTime" /> value that represents the date component of the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> value that represents the date component of the current <see cref="T:System.DateTimeOffset" /> object.</returns>
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x000273DC File Offset: 0x000255DC
		public DateTime Date
		{
			get
			{
				return this.ClockDateTime.Date;
			}
		}

		/// <summary>Gets the day of the month represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The day component of the current <see cref="T:System.DateTimeOffset" /> object, expressed as a value between 1 and 31.</returns>
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x000273F8 File Offset: 0x000255F8
		public int Day
		{
			get
			{
				return this.ClockDateTime.Day;
			}
		}

		/// <summary>Gets the day of the week represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>One of the enumeration values that indicates the day of the week of the current <see cref="T:System.DateTimeOffset" /> object.</returns>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x00027414 File Offset: 0x00025614
		public DayOfWeek DayOfWeek
		{
			get
			{
				return this.ClockDateTime.DayOfWeek;
			}
		}

		/// <summary>Gets the day of the year represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The day of the year of the current <see cref="T:System.DateTimeOffset" /> object, expressed as a value between 1 and 366.</returns>
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x00027430 File Offset: 0x00025630
		public int DayOfYear
		{
			get
			{
				return this.ClockDateTime.DayOfYear;
			}
		}

		/// <summary>Gets the hour component of the time represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The hour component of the current <see cref="T:System.DateTimeOffset" /> object. This property uses a 24-hour clock; the value ranges from 0 to 23.</returns>
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x0002744C File Offset: 0x0002564C
		public int Hour
		{
			get
			{
				return this.ClockDateTime.Hour;
			}
		}

		/// <summary>Gets the millisecond component of the time represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The millisecond component of the current <see cref="T:System.DateTimeOffset" /> object, expressed as an integer between 0 and 999.</returns>
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x00027468 File Offset: 0x00025668
		public int Millisecond
		{
			get
			{
				return this.ClockDateTime.Millisecond;
			}
		}

		/// <summary>Gets the minute component of the time represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The minute component of the current <see cref="T:System.DateTimeOffset" /> object, expressed as an integer between 0 and 59.</returns>
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x00027484 File Offset: 0x00025684
		public int Minute
		{
			get
			{
				return this.ClockDateTime.Minute;
			}
		}

		/// <summary>Gets the month component of the date represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The month component of the current <see cref="T:System.DateTimeOffset" /> object, expressed as an integer between 1 and 12.</returns>
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x000274A0 File Offset: 0x000256A0
		public int Month
		{
			get
			{
				return this.ClockDateTime.Month;
			}
		}

		/// <summary>Gets the time's offset from Coordinated Universal Time (UTC).</summary>
		/// <returns>The difference between the current <see cref="T:System.DateTimeOffset" /> object's time value and Coordinated Universal Time (UTC).</returns>
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x000274BB File Offset: 0x000256BB
		public TimeSpan Offset
		{
			get
			{
				return new TimeSpan(0, (int)this._offsetMinutes, 0);
			}
		}

		/// <summary>Gets the second component of the clock time represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The second component of the <see cref="T:System.DateTimeOffset" /> object, expressed as an integer value between 0 and 59.</returns>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x000274CC File Offset: 0x000256CC
		public int Second
		{
			get
			{
				return this.ClockDateTime.Second;
			}
		}

		/// <summary>Gets the number of ticks that represents the date and time of the current <see cref="T:System.DateTimeOffset" /> object in clock time.</summary>
		/// <returns>The number of ticks in the <see cref="T:System.DateTimeOffset" /> object's clock time.</returns>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x000274E8 File Offset: 0x000256E8
		public long Ticks
		{
			get
			{
				return this.ClockDateTime.Ticks;
			}
		}

		/// <summary>Gets the number of ticks that represents the date and time of the current <see cref="T:System.DateTimeOffset" /> object in Coordinated Universal Time (UTC).</summary>
		/// <returns>The number of ticks in the <see cref="T:System.DateTimeOffset" /> object's Coordinated Universal Time (UTC).</returns>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x00027504 File Offset: 0x00025704
		public long UtcTicks
		{
			get
			{
				return this.UtcDateTime.Ticks;
			}
		}

		/// <summary>Gets the time of day for the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The time interval of the current date that has elapsed since midnight.</returns>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x00027520 File Offset: 0x00025720
		public TimeSpan TimeOfDay
		{
			get
			{
				return this.ClockDateTime.TimeOfDay;
			}
		}

		/// <summary>Gets the year component of the date represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The year component of the current <see cref="T:System.DateTimeOffset" /> object, expressed as an integer value between 0 and 9999.</returns>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x0002753C File Offset: 0x0002573C
		public int Year
		{
			get
			{
				return this.ClockDateTime.Year;
			}
		}

		/// <summary>Returns a new <see cref="T:System.DateTimeOffset" /> object that adds a specified time interval to the value of this instance.</summary>
		/// <param name="timeSpan">A <see cref="T:System.TimeSpan" /> object that represents a positive or a negative time interval.</param>
		/// <returns>An object whose value is the sum of the date and time represented by the current <see cref="T:System.DateTimeOffset" /> object and the time interval represented by <paramref name="timeSpan" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTimeOffset" /> value is less than <see cref="F:System.DateTimeOffset.MinValue" />.  
		///  -or-  
		///  The resulting <see cref="T:System.DateTimeOffset" /> value is greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000A48 RID: 2632 RVA: 0x00027558 File Offset: 0x00025758
		public DateTimeOffset Add(TimeSpan timeSpan)
		{
			return new DateTimeOffset(this.ClockDateTime.Add(timeSpan), this.Offset);
		}

		/// <summary>Returns a new <see cref="T:System.DateTimeOffset" /> object that adds a specified number of whole and fractional days to the value of this instance.</summary>
		/// <param name="days">A number of whole and fractional days. The number can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by the current <see cref="T:System.DateTimeOffset" /> object and the number of days represented by <paramref name="days" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTimeOffset" /> value is less than <see cref="F:System.DateTimeOffset.MinValue" />.  
		///  -or-  
		///  The resulting <see cref="T:System.DateTimeOffset" /> value is greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000A49 RID: 2633 RVA: 0x00027580 File Offset: 0x00025780
		public DateTimeOffset AddDays(double days)
		{
			return new DateTimeOffset(this.ClockDateTime.AddDays(days), this.Offset);
		}

		/// <summary>Returns a new <see cref="T:System.DateTimeOffset" /> object that adds a specified number of whole and fractional hours to the value of this instance.</summary>
		/// <param name="hours">A number of whole and fractional hours. The number can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by the current <see cref="T:System.DateTimeOffset" /> object and the number of hours represented by <paramref name="hours" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTimeOffset" /> value is less than <see cref="F:System.DateTimeOffset.MinValue" />.  
		///  -or-  
		///  The resulting <see cref="T:System.DateTimeOffset" /> value is greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000A4A RID: 2634 RVA: 0x000275A8 File Offset: 0x000257A8
		public DateTimeOffset AddHours(double hours)
		{
			return new DateTimeOffset(this.ClockDateTime.AddHours(hours), this.Offset);
		}

		/// <summary>Returns a new <see cref="T:System.DateTimeOffset" /> object that adds a specified number of milliseconds to the value of this instance.</summary>
		/// <param name="milliseconds">A number of whole and fractional milliseconds. The number can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by the current <see cref="T:System.DateTimeOffset" /> object and the number of whole milliseconds represented by <paramref name="milliseconds" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTimeOffset" /> value is less than <see cref="F:System.DateTimeOffset.MinValue" />.  
		///  -or-  
		///  The resulting <see cref="T:System.DateTimeOffset" /> value is greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000A4B RID: 2635 RVA: 0x000275D0 File Offset: 0x000257D0
		public DateTimeOffset AddMilliseconds(double milliseconds)
		{
			return new DateTimeOffset(this.ClockDateTime.AddMilliseconds(milliseconds), this.Offset);
		}

		/// <summary>Returns a new <see cref="T:System.DateTimeOffset" /> object that adds a specified number of whole and fractional minutes to the value of this instance.</summary>
		/// <param name="minutes">A number of whole and fractional minutes. The number can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by the current <see cref="T:System.DateTimeOffset" /> object and the number of minutes represented by <paramref name="minutes" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTimeOffset" /> value is less than <see cref="F:System.DateTimeOffset.MinValue" />.  
		///  -or-  
		///  The resulting <see cref="T:System.DateTimeOffset" /> value is greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000A4C RID: 2636 RVA: 0x000275F8 File Offset: 0x000257F8
		public DateTimeOffset AddMinutes(double minutes)
		{
			return new DateTimeOffset(this.ClockDateTime.AddMinutes(minutes), this.Offset);
		}

		/// <summary>Returns a new <see cref="T:System.DateTimeOffset" /> object that adds a specified number of months to the value of this instance.</summary>
		/// <param name="months">A number of whole months. The number can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by the current <see cref="T:System.DateTimeOffset" /> object and the number of months represented by <paramref name="months" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTimeOffset" /> value is less than <see cref="F:System.DateTimeOffset.MinValue" />.  
		///  -or-  
		///  The resulting <see cref="T:System.DateTimeOffset" /> value is greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000A4D RID: 2637 RVA: 0x00027620 File Offset: 0x00025820
		public DateTimeOffset AddMonths(int months)
		{
			return new DateTimeOffset(this.ClockDateTime.AddMonths(months), this.Offset);
		}

		/// <summary>Returns a new <see cref="T:System.DateTimeOffset" /> object that adds a specified number of whole and fractional seconds to the value of this instance.</summary>
		/// <param name="seconds">A number of whole and fractional seconds. The number can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by the current <see cref="T:System.DateTimeOffset" /> object and the number of seconds represented by <paramref name="seconds" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTimeOffset" /> value is less than <see cref="F:System.DateTimeOffset.MinValue" />.  
		///  -or-  
		///  The resulting <see cref="T:System.DateTimeOffset" /> value is greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000A4E RID: 2638 RVA: 0x00027648 File Offset: 0x00025848
		public DateTimeOffset AddSeconds(double seconds)
		{
			return new DateTimeOffset(this.ClockDateTime.AddSeconds(seconds), this.Offset);
		}

		/// <summary>Returns a new <see cref="T:System.DateTimeOffset" /> object that adds a specified number of ticks to the value of this instance.</summary>
		/// <param name="ticks">A number of 100-nanosecond ticks. The number can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by the current <see cref="T:System.DateTimeOffset" /> object and the number of ticks represented by <paramref name="ticks" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTimeOffset" /> value is less than <see cref="F:System.DateTimeOffset.MinValue" />.  
		///  -or-  
		///  The resulting <see cref="T:System.DateTimeOffset" /> value is greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000A4F RID: 2639 RVA: 0x00027670 File Offset: 0x00025870
		public DateTimeOffset AddTicks(long ticks)
		{
			return new DateTimeOffset(this.ClockDateTime.AddTicks(ticks), this.Offset);
		}

		/// <summary>Returns a new <see cref="T:System.DateTimeOffset" /> object that adds a specified number of years to the value of this instance.</summary>
		/// <param name="years">A number of years. The number can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by the current <see cref="T:System.DateTimeOffset" /> object and the number of years represented by <paramref name="years" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTimeOffset" /> value is less than <see cref="F:System.DateTimeOffset.MinValue" />.  
		///  -or-  
		///  The resulting <see cref="T:System.DateTimeOffset" /> value is greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000A50 RID: 2640 RVA: 0x00027698 File Offset: 0x00025898
		public DateTimeOffset AddYears(int years)
		{
			return new DateTimeOffset(this.ClockDateTime.AddYears(years), this.Offset);
		}

		/// <summary>Compares two <see cref="T:System.DateTimeOffset" /> objects and indicates whether the first is earlier than the second, equal to the second, or later than the second.</summary>
		/// <param name="first">The first object to compare.</param>
		/// <param name="second">The second object to compare.</param>
		/// <returns>A signed integer that indicates whether the value of the <paramref name="first" /> parameter is earlier than, later than, or the same time as the value of the <paramref name="second" /> parameter, as the following table shows.  
		///   Return value  
		///
		///   Meaning  
		///
		///   Less than zero  
		///
		///  <paramref name="first" /> is earlier than <paramref name="second" />.  
		///
		///   Zero  
		///
		///  <paramref name="first" /> is equal to <paramref name="second" />.  
		///
		///   Greater than zero  
		///
		///  <paramref name="first" /> is later than <paramref name="second" />.</returns>
		// Token: 0x06000A51 RID: 2641 RVA: 0x000276BF File Offset: 0x000258BF
		public static int Compare(DateTimeOffset first, DateTimeOffset second)
		{
			return DateTime.Compare(first.UtcDateTime, second.UtcDateTime);
		}

		/// <summary>Compares the value of the current <see cref="T:System.DateTimeOffset" /> object with another object of the same type.</summary>
		/// <param name="obj">The object to compare with the current <see cref="T:System.DateTimeOffset" /> object.</param>
		/// <returns>A 32-bit signed integer that indicates whether the current <see cref="T:System.DateTimeOffset" /> object is less than, equal to, or greater than <paramref name="obj" />. The return values of the method are interpreted as follows:  
		///   Return Value  
		///
		///   Description  
		///
		///   Less than zero  
		///
		///   The current <see cref="T:System.DateTimeOffset" /> object is less than (earlier than) <paramref name="obj" />.  
		///
		///   Zero  
		///
		///   The current <see cref="T:System.DateTimeOffset" /> object is equal to (the same point in time as) <paramref name="obj" />.  
		///
		///   Greater than zero  
		///
		///   The current <see cref="T:System.DateTimeOffset" /> object is greater than (later than) <paramref name="obj" />.</returns>
		// Token: 0x06000A52 RID: 2642 RVA: 0x000276D4 File Offset: 0x000258D4
		int IComparable.CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is DateTimeOffset))
			{
				throw new ArgumentException("Object must be of type DateTimeOffset.");
			}
			DateTime utcDateTime = ((DateTimeOffset)obj).UtcDateTime;
			DateTime utcDateTime2 = this.UtcDateTime;
			if (utcDateTime2 > utcDateTime)
			{
				return 1;
			}
			if (utcDateTime2 < utcDateTime)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Compares the current <see cref="T:System.DateTimeOffset" /> object to a specified <see cref="T:System.DateTimeOffset" /> object and indicates whether the current object is earlier than, the same as, or later than the second <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <param name="other">An object to compare with the current <see cref="T:System.DateTimeOffset" /> object.</param>
		/// <returns>A signed integer that indicates the relationship between the current <see cref="T:System.DateTimeOffset" /> object and <paramref name="other" />, as the following table shows.  
		///   Return Value  
		///
		///   Description  
		///
		///   Less than zero  
		///
		///   The current <see cref="T:System.DateTimeOffset" /> object is earlier than <paramref name="other" />.  
		///
		///   Zero  
		///
		///   The current <see cref="T:System.DateTimeOffset" /> object is the same as <paramref name="other" />.  
		///
		///   Greater than zero.  
		///
		///   The current <see cref="T:System.DateTimeOffset" /> object is later than <paramref name="other" />.</returns>
		// Token: 0x06000A53 RID: 2643 RVA: 0x00027728 File Offset: 0x00025928
		public int CompareTo(DateTimeOffset other)
		{
			DateTime utcDateTime = other.UtcDateTime;
			DateTime utcDateTime2 = this.UtcDateTime;
			if (utcDateTime2 > utcDateTime)
			{
				return 1;
			}
			if (utcDateTime2 < utcDateTime)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Determines whether a <see cref="T:System.DateTimeOffset" /> object represents the same point in time as a specified object.</summary>
		/// <param name="obj">The object to compare to the current <see cref="T:System.DateTimeOffset" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="obj" /> parameter is a <see cref="T:System.DateTimeOffset" /> object and represents the same point in time as the current <see cref="T:System.DateTimeOffset" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A54 RID: 2644 RVA: 0x0002775C File Offset: 0x0002595C
		public override bool Equals(object obj)
		{
			return obj is DateTimeOffset && this.UtcDateTime.Equals(((DateTimeOffset)obj).UtcDateTime);
		}

		/// <summary>Determines whether the current <see cref="T:System.DateTimeOffset" /> object represents the same point in time as a specified <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <param name="other">An object to compare to the current <see cref="T:System.DateTimeOffset" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if both <see cref="T:System.DateTimeOffset" /> objects have the same <see cref="P:System.DateTimeOffset.UtcDateTime" /> value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A55 RID: 2645 RVA: 0x00027790 File Offset: 0x00025990
		public bool Equals(DateTimeOffset other)
		{
			return this.UtcDateTime.Equals(other.UtcDateTime);
		}

		/// <summary>Determines whether the current <see cref="T:System.DateTimeOffset" /> object represents the same time and has the same offset as a specified <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <param name="other">The object to compare to the current <see cref="T:System.DateTimeOffset" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.DateTimeOffset" /> object and <paramref name="other" /> have the same date and time value and the same <see cref="P:System.DateTimeOffset.Offset" /> value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A56 RID: 2646 RVA: 0x000277B4 File Offset: 0x000259B4
		public bool EqualsExact(DateTimeOffset other)
		{
			return this.ClockDateTime == other.ClockDateTime && this.Offset == other.Offset && this.ClockDateTime.Kind == other.ClockDateTime.Kind;
		}

		/// <summary>Determines whether two specified <see cref="T:System.DateTimeOffset" /> objects represent the same point in time.</summary>
		/// <param name="first">The first object to compare.</param>
		/// <param name="second">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the two <see cref="T:System.DateTimeOffset" /> objects have the same <see cref="P:System.DateTimeOffset.UtcDateTime" /> value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A57 RID: 2647 RVA: 0x0002780A File Offset: 0x00025A0A
		public static bool Equals(DateTimeOffset first, DateTimeOffset second)
		{
			return DateTime.Equals(first.UtcDateTime, second.UtcDateTime);
		}

		/// <summary>Converts the specified Windows file time to an equivalent local time.</summary>
		/// <param name="fileTime">A Windows file time, expressed in ticks.</param>
		/// <returns>An object that represents the date and time of <paramref name="fileTime" /> with the offset set to the local time offset.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="filetime" /> is less than zero.  
		/// -or-  
		/// <paramref name="filetime" /> is greater than <see langword="DateTimeOffset.MaxValue.Ticks" />.</exception>
		// Token: 0x06000A58 RID: 2648 RVA: 0x0002781F File Offset: 0x00025A1F
		public static DateTimeOffset FromFileTime(long fileTime)
		{
			return new DateTimeOffset(DateTime.FromFileTime(fileTime));
		}

		/// <summary>Converts a Unix time expressed as the number of seconds that have elapsed since 1970-01-01T00:00:00Z to a <see cref="T:System.DateTimeOffset" /> value.</summary>
		/// <param name="seconds">A Unix time, expressed as the number of seconds that have elapsed since 1970-01-01T00:00:00Z (January 1, 1970, at 12:00 AM UTC). For Unix times before this date, its value is negative.</param>
		/// <returns>A date and time value that represents the same moment in time as the Unix time.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="seconds" /> is less than  -62,135,596,800.  
		/// -or-  
		/// <paramref name="seconds" /> is greater than 253,402,300,799.</exception>
		// Token: 0x06000A59 RID: 2649 RVA: 0x0002782C File Offset: 0x00025A2C
		public static DateTimeOffset FromUnixTimeSeconds(long seconds)
		{
			if (seconds < -62135596800L || seconds > 253402300799L)
			{
				throw new ArgumentOutOfRangeException("seconds", SR.Format("Valid values are between {0} and {1}, inclusive.", -62135596800L, 253402300799L));
			}
			return new DateTimeOffset(seconds * 10000000L + 621355968000000000L, TimeSpan.Zero);
		}

		/// <summary>Converts a Unix time expressed as the number of milliseconds that have elapsed since 1970-01-01T00:00:00Z to a <see cref="T:System.DateTimeOffset" /> value.</summary>
		/// <param name="milliseconds">A Unix time, expressed as the number of milliseconds that have elapsed since 1970-01-01T00:00:00Z (January 1, 1970, at 12:00 AM UTC). For Unix times before this date, its value is negative.</param>
		/// <returns>A date and time value that represents the same moment in time as the Unix time.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="milliseconds" /> is less than  -62,135,596,800,000.  
		/// -or-  
		/// <paramref name="milliseconds" /> is greater than 253,402,300,799,999.</exception>
		// Token: 0x06000A5A RID: 2650 RVA: 0x000278A0 File Offset: 0x00025AA0
		public static DateTimeOffset FromUnixTimeMilliseconds(long milliseconds)
		{
			if (milliseconds < -62135596800000L || milliseconds > 253402300799999L)
			{
				throw new ArgumentOutOfRangeException("milliseconds", SR.Format("Valid values are between {0} and {1}, inclusive.", -62135596800000L, 253402300799999L));
			}
			return new DateTimeOffset(milliseconds * 10000L + 621355968000000000L, TimeSpan.Zero);
		}

		/// <summary>Runs when the deserialization of an object has been completed.</summary>
		/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.</param>
		// Token: 0x06000A5B RID: 2651 RVA: 0x00027914 File Offset: 0x00025B14
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			try
			{
				DateTimeOffset.ValidateOffset(this.Offset);
				DateTimeOffset.ValidateDate(this.ClockDateTime, this.Offset);
			}
			catch (ArgumentException innerException)
			{
				throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", innerException);
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data required to serialize the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <param name="info">The object to populate with data.</param>
		/// <param name="context">The destination for this serialization (see <see cref="T:System.Runtime.Serialization.StreamingContext" />).</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06000A5C RID: 2652 RVA: 0x00027960 File Offset: 0x00025B60
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("DateTime", this._dateTime);
			info.AddValue("OffsetMinutes", this._offsetMinutes);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00027994 File Offset: 0x00025B94
		private DateTimeOffset(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._dateTime = (DateTime)info.GetValue("DateTime", typeof(DateTime));
			this._offsetMinutes = (short)info.GetValue("OffsetMinutes", typeof(short));
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000A5E RID: 2654 RVA: 0x000279F0 File Offset: 0x00025BF0
		public override int GetHashCode()
		{
			return this.UtcDateTime.GetHashCode();
		}

		/// <summary>Converts the specified string representation of a date, time, and offset to its <see cref="T:System.DateTimeOffset" /> equivalent.</summary>
		/// <param name="input">A string that contains a date and time to convert.</param>
		/// <returns>An object that is equivalent to the date and time that is contained in <paramref name="input" />.</returns>
		/// <exception cref="T:System.ArgumentException">The offset is greater than 14 hours or less than -14 hours.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> does not contain a valid string representation of a date and time.  
		/// -or-  
		/// <paramref name="input" /> contains the string representation of an offset value without a date or time.</exception>
		// Token: 0x06000A5F RID: 2655 RVA: 0x00027A0C File Offset: 0x00025C0C
		public static DateTimeOffset Parse(string input)
		{
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			TimeSpan offset;
			return new DateTimeOffset(DateTimeParse.Parse(input, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out offset).Ticks, offset);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTimeOffset" /> equivalent using the specified culture-specific format information.</summary>
		/// <param name="input">A string that contains a date and time to convert.</param>
		/// <param name="formatProvider">An object that provides culture-specific format information about <paramref name="input" />.</param>
		/// <returns>An object that is equivalent to the date and time that is contained in <paramref name="input" />, as specified by <paramref name="formatProvider" />.</returns>
		/// <exception cref="T:System.ArgumentException">The offset is greater than 14 hours or less than -14 hours.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> does not contain a valid string representation of a date and time.  
		/// -or-  
		/// <paramref name="input" /> contains the string representation of an offset value without a date or time.</exception>
		// Token: 0x06000A60 RID: 2656 RVA: 0x00027A44 File Offset: 0x00025C44
		public static DateTimeOffset Parse(string input, IFormatProvider formatProvider)
		{
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			return DateTimeOffset.Parse(input, formatProvider, DateTimeStyles.None);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTimeOffset" /> equivalent using the specified culture-specific format information and formatting style.</summary>
		/// <param name="input">A string that contains a date and time to convert.</param>
		/// <param name="formatProvider">An object that provides culture-specific format information about <paramref name="input" />.</param>
		/// <param name="styles">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="input" />. A typical value to specify is <see cref="F:System.Globalization.DateTimeStyles.None" />.</param>
		/// <returns>An object that is equivalent to the date and time that is contained in <paramref name="input" /> as specified by <paramref name="formatProvider" /> and <paramref name="styles" />.</returns>
		/// <exception cref="T:System.ArgumentException">The offset is greater than 14 hours or less than -14 hours.  
		///  -or-  
		///  <paramref name="styles" /> is not a valid <see cref="T:System.Globalization.DateTimeStyles" /> value.  
		///  -or-  
		///  <paramref name="styles" /> includes an unsupported <see cref="T:System.Globalization.DateTimeStyles" /> value.  
		///  -or-  
		///  <paramref name="styles" /> includes <see cref="T:System.Globalization.DateTimeStyles" /> values that cannot be used together.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> does not contain a valid string representation of a date and time.  
		/// -or-  
		/// <paramref name="input" /> contains the string representation of an offset value without a date or time.</exception>
		// Token: 0x06000A61 RID: 2657 RVA: 0x00027A58 File Offset: 0x00025C58
		public static DateTimeOffset Parse(string input, IFormatProvider formatProvider, DateTimeStyles styles)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			TimeSpan offset;
			return new DateTimeOffset(DateTimeParse.Parse(input, DateTimeFormatInfo.GetInstance(formatProvider), styles, out offset).Ticks, offset);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00027AA0 File Offset: 0x00025CA0
		public static DateTimeOffset Parse(ReadOnlySpan<char> input, IFormatProvider formatProvider = null, DateTimeStyles styles = DateTimeStyles.None)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			TimeSpan offset;
			return new DateTimeOffset(DateTimeParse.Parse(input, DateTimeFormatInfo.GetInstance(formatProvider), styles, out offset).Ticks, offset);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTimeOffset" /> equivalent using the specified format and culture-specific format information. The format of the string representation must match the specified format exactly.</summary>
		/// <param name="input">A string that contains a date and time to convert.</param>
		/// <param name="format">A format specifier that defines the expected format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information about <paramref name="input" />.</param>
		/// <returns>An object that is equivalent to the date and time that is contained in <paramref name="input" /> as specified by <paramref name="format" /> and <paramref name="formatProvider" />.</returns>
		/// <exception cref="T:System.ArgumentException">The offset is greater than 14 hours or less than -14 hours.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is an empty string ("").  
		/// -or-  
		/// <paramref name="input" /> does not contain a valid string representation of a date and time.  
		/// -or-  
		/// <paramref name="format" /> is an empty string.  
		/// -or-  
		/// The hour component and the AM/PM designator in <paramref name="input" /> do not agree.</exception>
		// Token: 0x06000A63 RID: 2659 RVA: 0x00027AD7 File Offset: 0x00025CD7
		public static DateTimeOffset ParseExact(string input, string format, IFormatProvider formatProvider)
		{
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			if (format == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);
			}
			return DateTimeOffset.ParseExact(input, format, formatProvider, DateTimeStyles.None);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTimeOffset" /> equivalent using the specified format, culture-specific format information, and style. The format of the string representation must match the specified format exactly.</summary>
		/// <param name="input">A string that contains a date and time to convert.</param>
		/// <param name="format">A format specifier that defines the expected format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information about <paramref name="input" />.</param>
		/// <param name="styles">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="input" />.</param>
		/// <returns>An object that is equivalent to the date and time that is contained in the <paramref name="input" /> parameter, as specified by the <paramref name="format" />, <paramref name="formatProvider" />, and <paramref name="styles" /> parameters.</returns>
		/// <exception cref="T:System.ArgumentException">The offset is greater than 14 hours or less than -14 hours.  
		///  -or-  
		///  The <paramref name="styles" /> parameter includes an unsupported value.  
		///  -or-  
		///  The <paramref name="styles" /> parameter contains <see cref="T:System.Globalization.DateTimeStyles" /> values that cannot be used together.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is an empty string ("").  
		/// -or-  
		/// <paramref name="input" /> does not contain a valid string representation of a date and time.  
		/// -or-  
		/// <paramref name="format" /> is an empty string.  
		/// -or-  
		/// The hour component and the AM/PM designator in <paramref name="input" /> do not agree.</exception>
		// Token: 0x06000A64 RID: 2660 RVA: 0x00027AF8 File Offset: 0x00025CF8
		public static DateTimeOffset ParseExact(string input, string format, IFormatProvider formatProvider, DateTimeStyles styles)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			if (format == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);
			}
			TimeSpan offset;
			return new DateTimeOffset(DateTimeParse.ParseExact(input, format, DateTimeFormatInfo.GetInstance(formatProvider), styles, out offset).Ticks, offset);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00027B50 File Offset: 0x00025D50
		public static DateTimeOffset ParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, IFormatProvider formatProvider, DateTimeStyles styles = DateTimeStyles.None)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			TimeSpan offset;
			return new DateTimeOffset(DateTimeParse.ParseExact(input, format, DateTimeFormatInfo.GetInstance(formatProvider), styles, out offset).Ticks, offset);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTimeOffset" /> equivalent using the specified formats, culture-specific format information, and style. The format of the string representation must match one of the specified formats exactly.</summary>
		/// <param name="input">A string that contains a date and time to convert.</param>
		/// <param name="formats">An array of format specifiers that define the expected formats of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information about <paramref name="input" />.</param>
		/// <param name="styles">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="input" />.</param>
		/// <returns>An object that is equivalent to the date and time that is contained in the <paramref name="input" /> parameter, as specified by the <paramref name="formats" />, <paramref name="formatProvider" />, and <paramref name="styles" /> parameters.</returns>
		/// <exception cref="T:System.ArgumentException">The offset is greater than 14 hours or less than -14 hours.  
		///  -or-  
		///  <paramref name="styles" /> includes an unsupported value.  
		///  -or-  
		///  The <paramref name="styles" /> parameter contains <see cref="T:System.Globalization.DateTimeStyles" /> values that cannot be used together.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is an empty string ("").  
		/// -or-  
		/// <paramref name="input" /> does not contain a valid string representation of a date and time.  
		/// -or-  
		/// No element of <paramref name="formats" /> contains a valid format specifier.  
		/// -or-  
		/// The hour component and the AM/PM designator in <paramref name="input" /> do not agree.</exception>
		// Token: 0x06000A66 RID: 2662 RVA: 0x00027B88 File Offset: 0x00025D88
		public static DateTimeOffset ParseExact(string input, string[] formats, IFormatProvider formatProvider, DateTimeStyles styles)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			TimeSpan offset;
			return new DateTimeOffset(DateTimeParse.ParseExactMultiple(input, formats, DateTimeFormatInfo.GetInstance(formatProvider), styles, out offset).Ticks, offset);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00027BD0 File Offset: 0x00025DD0
		public static DateTimeOffset ParseExact(ReadOnlySpan<char> input, string[] formats, IFormatProvider formatProvider, DateTimeStyles styles = DateTimeStyles.None)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			TimeSpan offset;
			return new DateTimeOffset(DateTimeParse.ParseExactMultiple(input, formats, DateTimeFormatInfo.GetInstance(formatProvider), styles, out offset).Ticks, offset);
		}

		/// <summary>Subtracts a <see cref="T:System.DateTimeOffset" /> value that represents a specific date and time from the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <param name="value">An object that represents the value to subtract.</param>
		/// <returns>An object that specifies the interval between the two <see cref="T:System.DateTimeOffset" /> objects.</returns>
		// Token: 0x06000A68 RID: 2664 RVA: 0x00027C08 File Offset: 0x00025E08
		public TimeSpan Subtract(DateTimeOffset value)
		{
			return this.UtcDateTime.Subtract(value.UtcDateTime);
		}

		/// <summary>Subtracts a specified time interval from the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <param name="value">The time interval to subtract.</param>
		/// <returns>An object that is equal to the date and time represented by the current <see cref="T:System.DateTimeOffset" /> object, minus the time interval represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTimeOffset" /> value is less than <see cref="F:System.DateTimeOffset.MinValue" />.  
		///  -or-  
		///  The resulting <see cref="T:System.DateTimeOffset" /> value is greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000A69 RID: 2665 RVA: 0x00027C2C File Offset: 0x00025E2C
		public DateTimeOffset Subtract(TimeSpan value)
		{
			return new DateTimeOffset(this.ClockDateTime.Subtract(value), this.Offset);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTimeOffset" /> object to a Windows file time.</summary>
		/// <returns>The value of the current <see cref="T:System.DateTimeOffset" /> object, expressed as a Windows file time.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting file time would represent a date and time before midnight on January 1, 1601 C.E. Coordinated Universal Time (UTC).</exception>
		// Token: 0x06000A6A RID: 2666 RVA: 0x00027C54 File Offset: 0x00025E54
		public long ToFileTime()
		{
			return this.UtcDateTime.ToFileTime();
		}

		/// <summary>Returns the number of seconds that have elapsed since 1970-01-01T00:00:00Z.</summary>
		/// <returns>The number of seconds that have elapsed since 1970-01-01T00:00:00Z.</returns>
		// Token: 0x06000A6B RID: 2667 RVA: 0x00027C70 File Offset: 0x00025E70
		public long ToUnixTimeSeconds()
		{
			return this.UtcDateTime.Ticks / 10000000L - 62135596800L;
		}

		/// <summary>Returns the number of milliseconds that have elapsed since 1970-01-01T00:00:00.000Z.</summary>
		/// <returns>The number of milliseconds that have elapsed since 1970-01-01T00:00:00.000Z.</returns>
		// Token: 0x06000A6C RID: 2668 RVA: 0x00027C9C File Offset: 0x00025E9C
		public long ToUnixTimeMilliseconds()
		{
			return this.UtcDateTime.Ticks / 10000L - 62135596800000L;
		}

		/// <summary>Converts the current <see cref="T:System.DateTimeOffset" /> object to a <see cref="T:System.DateTimeOffset" /> object that represents the local time.</summary>
		/// <returns>An object that represents the date and time of the current <see cref="T:System.DateTimeOffset" /> object converted to local time.</returns>
		// Token: 0x06000A6D RID: 2669 RVA: 0x00027CC8 File Offset: 0x00025EC8
		public DateTimeOffset ToLocalTime()
		{
			return this.ToLocalTime(false);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00027CD4 File Offset: 0x00025ED4
		internal DateTimeOffset ToLocalTime(bool throwOnOverflow)
		{
			return new DateTimeOffset(this.UtcDateTime.ToLocalTime(throwOnOverflow));
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTimeOffset" /> object to its equivalent string representation.</summary>
		/// <returns>A string representation of a <see cref="T:System.DateTimeOffset" /> object that includes the offset appended at the end of the string.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by the current culture.</exception>
		// Token: 0x06000A6F RID: 2671 RVA: 0x00027CF5 File Offset: 0x00025EF5
		public override string ToString()
		{
			return DateTimeFormat.Format(this.ClockDateTime, null, null, this.Offset);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTimeOffset" /> object to its equivalent string representation using the specified format.</summary>
		/// <param name="format">A format string.</param>
		/// <returns>A string representation of the value of the current <see cref="T:System.DateTimeOffset" /> object, as specified by <paramref name="format" />.</returns>
		/// <exception cref="T:System.FormatException">The length of <paramref name="format" /> is one, and it is not one of the standard format specifier characters defined for <see cref="T:System.Globalization.DateTimeFormatInfo" />.  
		///  -or-  
		///  <paramref name="format" /> does not contain a valid custom format pattern.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by the current culture.</exception>
		// Token: 0x06000A70 RID: 2672 RVA: 0x00027D0A File Offset: 0x00025F0A
		public string ToString(string format)
		{
			return DateTimeFormat.Format(this.ClockDateTime, format, null, this.Offset);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTimeOffset" /> object to its equivalent string representation using the specified culture-specific formatting information.</summary>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A string representation of the value of the current <see cref="T:System.DateTimeOffset" /> object, as specified by <paramref name="formatProvider" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by <paramref name="formatProvider" />.</exception>
		// Token: 0x06000A71 RID: 2673 RVA: 0x00027D1F File Offset: 0x00025F1F
		public string ToString(IFormatProvider formatProvider)
		{
			return DateTimeFormat.Format(this.ClockDateTime, null, formatProvider, this.Offset);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTimeOffset" /> object to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <param name="format">A format string.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A string representation of the value of the current <see cref="T:System.DateTimeOffset" /> object, as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <exception cref="T:System.FormatException">The length of <paramref name="format" /> is one, and it is not one of the standard format specifier characters defined for <see cref="T:System.Globalization.DateTimeFormatInfo" />.  
		///  -or-  
		///  <paramref name="format" /> does not contain a valid custom format pattern.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by <paramref name="formatProvider" />.</exception>
		// Token: 0x06000A72 RID: 2674 RVA: 0x00027D34 File Offset: 0x00025F34
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return DateTimeFormat.Format(this.ClockDateTime, format, formatProvider, this.Offset);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00027D49 File Offset: 0x00025F49
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), IFormatProvider formatProvider = null)
		{
			return DateTimeFormat.TryFormat(this.ClockDateTime, destination, out charsWritten, format, formatProvider, this.Offset);
		}

		/// <summary>Converts the current <see cref="T:System.DateTimeOffset" /> object to a <see cref="T:System.DateTimeOffset" /> value that represents the Coordinated Universal Time (UTC).</summary>
		/// <returns>An object that represents the date and time of the current <see cref="T:System.DateTimeOffset" /> object converted to Coordinated Universal Time (UTC).</returns>
		// Token: 0x06000A74 RID: 2676 RVA: 0x00027D61 File Offset: 0x00025F61
		public DateTimeOffset ToUniversalTime()
		{
			return new DateTimeOffset(this.UtcDateTime);
		}

		/// <summary>Tries to converts a specified string representation of a date and time to its <see cref="T:System.DateTimeOffset" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
		/// <param name="input">A string that contains a date and time to convert.</param>
		/// <param name="result">When the method returns, contains the <see cref="T:System.DateTimeOffset" /> equivalent to the date and time of <paramref name="input" />, if the conversion succeeded, or <see cref="F:System.DateTimeOffset.MinValue" />, if the conversion failed. The conversion fails if the <paramref name="input" /> parameter is <see langword="null" /> or does not contain a valid string representation of a date and time. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="input" /> parameter is successfully converted; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A75 RID: 2677 RVA: 0x00027D70 File Offset: 0x00025F70
		public static bool TryParse(string input, out DateTimeOffset result)
		{
			DateTime dateTime;
			TimeSpan offset;
			bool result2 = DateTimeParse.TryParse(input, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out dateTime, out offset);
			result = new DateTimeOffset(dateTime.Ticks, offset);
			return result2;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00027DA8 File Offset: 0x00025FA8
		public static bool TryParse(ReadOnlySpan<char> input, out DateTimeOffset result)
		{
			DateTime dateTime;
			TimeSpan offset;
			bool result2 = DateTimeParse.TryParse(input, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out dateTime, out offset);
			result = new DateTimeOffset(dateTime.Ticks, offset);
			return result2;
		}

		/// <summary>Tries to convert a specified string representation of a date and time to its <see cref="T:System.DateTimeOffset" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
		/// <param name="input">A string that contains a date and time to convert.</param>
		/// <param name="formatProvider">An object that provides culture-specific formatting information about <paramref name="input" />.</param>
		/// <param name="styles">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="input" />.</param>
		/// <param name="result">When the method returns, contains the <see cref="T:System.DateTimeOffset" /> value equivalent to the date and time of <paramref name="input" />, if the conversion succeeded, or <see cref="F:System.DateTimeOffset.MinValue" />, if the conversion failed. The conversion fails if the <paramref name="input" /> parameter is <see langword="null" /> or does not contain a valid string representation of a date and time. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="input" /> parameter is successfully converted; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> includes an undefined <see cref="T:System.Globalization.DateTimeStyles" /> value.  
		/// -or-  
		/// <see cref="F:System.Globalization.DateTimeStyles.NoCurrentDateDefault" /> is not supported.  
		/// -or-  
		/// <paramref name="styles" /> includes mutually exclusive <see cref="T:System.Globalization.DateTimeStyles" /> values.</exception>
		// Token: 0x06000A77 RID: 2679 RVA: 0x00027DD8 File Offset: 0x00025FD8
		public static bool TryParse(string input, IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			if (input == null)
			{
				result = default(DateTimeOffset);
				return false;
			}
			DateTime dateTime;
			TimeSpan offset;
			bool result2 = DateTimeParse.TryParse(input, DateTimeFormatInfo.GetInstance(formatProvider), styles, out dateTime, out offset);
			result = new DateTimeOffset(dateTime.Ticks, offset);
			return result2;
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00027E28 File Offset: 0x00026028
		public static bool TryParse(ReadOnlySpan<char> input, IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			DateTime dateTime;
			TimeSpan offset;
			bool result2 = DateTimeParse.TryParse(input, DateTimeFormatInfo.GetInstance(formatProvider), styles, out dateTime, out offset);
			result = new DateTimeOffset(dateTime.Ticks, offset);
			return result2;
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTimeOffset" /> equivalent using the specified format, culture-specific format information, and style. The format of the string representation must match the specified format exactly.</summary>
		/// <param name="input">A string that contains a date and time to convert.</param>
		/// <param name="format">A format specifier that defines the required format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information about <paramref name="input" />.</param>
		/// <param name="styles">A bitwise combination of enumeration values that indicates the permitted format of input. A typical value to specify is <see langword="None" />.</param>
		/// <param name="result">When the method returns, contains the <see cref="T:System.DateTimeOffset" /> equivalent to the date and time of <paramref name="input" />, if the conversion succeeded, or <see cref="F:System.DateTimeOffset.MinValue" />, if the conversion failed. The conversion fails if the <paramref name="input" /> parameter is <see langword="null" />, or does not contain a valid string representation of a date and time in the expected format defined by <paramref name="format" /> and provider. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="input" /> parameter is successfully converted; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> includes an undefined <see cref="T:System.Globalization.DateTimeStyles" /> value.  
		/// -or-  
		/// <see cref="F:System.Globalization.DateTimeStyles.NoCurrentDateDefault" /> is not supported.  
		/// -or-  
		/// <paramref name="styles" /> includes mutually exclusive <see cref="T:System.Globalization.DateTimeStyles" /> values.</exception>
		// Token: 0x06000A79 RID: 2681 RVA: 0x00027E68 File Offset: 0x00026068
		public static bool TryParseExact(string input, string format, IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			if (input == null || format == null)
			{
				result = default(DateTimeOffset);
				return false;
			}
			DateTime dateTime;
			TimeSpan offset;
			bool result2 = DateTimeParse.TryParseExact(input, format, DateTimeFormatInfo.GetInstance(formatProvider), styles, out dateTime, out offset);
			result = new DateTimeOffset(dateTime.Ticks, offset);
			return result2;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00027EC4 File Offset: 0x000260C4
		public static bool TryParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			DateTime dateTime;
			TimeSpan offset;
			bool result2 = DateTimeParse.TryParseExact(input, format, DateTimeFormatInfo.GetInstance(formatProvider), styles, out dateTime, out offset);
			result = new DateTimeOffset(dateTime.Ticks, offset);
			return result2;
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTimeOffset" /> equivalent using the specified array of formats, culture-specific format information, and style. The format of the string representation must match one of the specified formats exactly.</summary>
		/// <param name="input">A string that contains a date and time to convert.</param>
		/// <param name="formats">An array that defines the expected formats of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information about <paramref name="input" />.</param>
		/// <param name="styles">A bitwise combination of enumeration values that indicates the permitted format of input. A typical value to specify is <see langword="None" />.</param>
		/// <param name="result">When the method returns, contains the <see cref="T:System.DateTimeOffset" /> equivalent to the date and time of <paramref name="input" />, if the conversion succeeded, or <see cref="F:System.DateTimeOffset.MinValue" />, if the conversion failed. The conversion fails if the <paramref name="input" /> does not contain a valid string representation of a date and time, or does not contain the date and time in the expected format defined by <paramref name="format" />, or if <paramref name="formats" /> is <see langword="null" />. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="input" /> parameter is successfully converted; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> includes an undefined <see cref="T:System.Globalization.DateTimeStyles" /> value.  
		/// -or-  
		/// <see cref="F:System.Globalization.DateTimeStyles.NoCurrentDateDefault" /> is not supported.  
		/// -or-  
		/// <paramref name="styles" /> includes mutually exclusive <see cref="T:System.Globalization.DateTimeStyles" /> values.</exception>
		// Token: 0x06000A7B RID: 2683 RVA: 0x00027F04 File Offset: 0x00026104
		public static bool TryParseExact(string input, string[] formats, IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			if (input == null)
			{
				result = default(DateTimeOffset);
				return false;
			}
			DateTime dateTime;
			TimeSpan offset;
			bool result2 = DateTimeParse.TryParseExactMultiple(input, formats, DateTimeFormatInfo.GetInstance(formatProvider), styles, out dateTime, out offset);
			result = new DateTimeOffset(dateTime.Ticks, offset);
			return result2;
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00027F58 File Offset: 0x00026158
		public static bool TryParseExact(ReadOnlySpan<char> input, string[] formats, IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			DateTime dateTime;
			TimeSpan offset;
			bool result2 = DateTimeParse.TryParseExactMultiple(input, formats, DateTimeFormatInfo.GetInstance(formatProvider), styles, out dateTime, out offset);
			result = new DateTimeOffset(dateTime.Ticks, offset);
			return result2;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00027F98 File Offset: 0x00026198
		private static short ValidateOffset(TimeSpan offset)
		{
			long ticks = offset.Ticks;
			if (ticks % 600000000L != 0L)
			{
				throw new ArgumentException("Offset must be specified in whole minutes.", "offset");
			}
			if (ticks < -504000000000L || ticks > 504000000000L)
			{
				throw new ArgumentOutOfRangeException("offset", "Offset must be within plus or minus 14 hours.");
			}
			return (short)(offset.Ticks / 600000000L);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00028000 File Offset: 0x00026200
		private static DateTime ValidateDate(DateTime dateTime, TimeSpan offset)
		{
			long num = dateTime.Ticks - offset.Ticks;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("offset", "The UTC time represented when the offset is applied must be between year 0 and 10,000.");
			}
			return new DateTime(num, DateTimeKind.Unspecified);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00028048 File Offset: 0x00026248
		private static DateTimeStyles ValidateStyles(DateTimeStyles style, string parameterName)
		{
			if ((style & ~(DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal | DateTimeStyles.AssumeUniversal | DateTimeStyles.RoundtripKind)) != DateTimeStyles.None)
			{
				throw new ArgumentException("An undefined DateTimeStyles value is being used.", parameterName);
			}
			if ((style & DateTimeStyles.AssumeLocal) != DateTimeStyles.None && (style & DateTimeStyles.AssumeUniversal) != DateTimeStyles.None)
			{
				throw new ArgumentException("The DateTimeStyles values AssumeLocal and AssumeUniversal cannot be used together.", parameterName);
			}
			if ((style & DateTimeStyles.NoCurrentDateDefault) != DateTimeStyles.None)
			{
				throw new ArgumentException("The DateTimeStyles value 'NoCurrentDateDefault' is not allowed when parsing DateTimeOffset.", parameterName);
			}
			style &= ~DateTimeStyles.RoundtripKind;
			style &= ~DateTimeStyles.AssumeLocal;
			return style;
		}

		/// <summary>Defines an implicit conversion of a <see cref="T:System.DateTime" /> object to a <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <param name="dateTime">The object to convert.</param>
		/// <returns>The converted object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The Coordinated Universal Time (UTC) date and time that results from applying the offset is earlier than <see cref="F:System.DateTimeOffset.MinValue" />.  
		///  -or-  
		///  The UTC date and time that results from applying the offset is later than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000A80 RID: 2688 RVA: 0x000280A3 File Offset: 0x000262A3
		public static implicit operator DateTimeOffset(DateTime dateTime)
		{
			return new DateTimeOffset(dateTime);
		}

		/// <summary>Adds a specified time interval to a <see cref="T:System.DateTimeOffset" /> object that has a specified date and time, and yields a <see cref="T:System.DateTimeOffset" /> object that has new a date and time.</summary>
		/// <param name="dateTimeOffset">The object to add the time interval to.</param>
		/// <param name="timeSpan">The time interval to add.</param>
		/// <returns>An object whose value is the sum of the values of <paramref name="dateTimeTz" /> and <paramref name="timeSpan" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTimeOffset" /> value is less than <see cref="F:System.DateTimeOffset.MinValue" />.  
		///  -or-  
		///  The resulting <see cref="T:System.DateTimeOffset" /> value is greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000A81 RID: 2689 RVA: 0x000280AB File Offset: 0x000262AB
		public static DateTimeOffset operator +(DateTimeOffset dateTimeOffset, TimeSpan timeSpan)
		{
			return new DateTimeOffset(dateTimeOffset.ClockDateTime + timeSpan, dateTimeOffset.Offset);
		}

		/// <summary>Subtracts a specified time interval from a specified date and time, and yields a new date and time.</summary>
		/// <param name="dateTimeOffset">The date and time object to subtract from.</param>
		/// <param name="timeSpan">The time interval to subtract.</param>
		/// <returns>An object that is equal to the value of <paramref name="dateTimeOffset" /> minus <paramref name="timeSpan" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTimeOffset" /> value is less than <see cref="F:System.DateTimeOffset.MinValue" /> or greater than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000A82 RID: 2690 RVA: 0x000280C6 File Offset: 0x000262C6
		public static DateTimeOffset operator -(DateTimeOffset dateTimeOffset, TimeSpan timeSpan)
		{
			return new DateTimeOffset(dateTimeOffset.ClockDateTime - timeSpan, dateTimeOffset.Offset);
		}

		/// <summary>Subtracts one <see cref="T:System.DateTimeOffset" /> object from another and yields a time interval.</summary>
		/// <param name="left">The minuend.</param>
		/// <param name="right">The subtrahend.</param>
		/// <returns>An object that represents the difference between <paramref name="left" /> and <paramref name="right" />.</returns>
		// Token: 0x06000A83 RID: 2691 RVA: 0x000280E1 File Offset: 0x000262E1
		public static TimeSpan operator -(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime - right.UtcDateTime;
		}

		/// <summary>Determines whether two specified <see cref="T:System.DateTimeOffset" /> objects represent the same point in time.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if both <see cref="T:System.DateTimeOffset" /> objects have the same <see cref="P:System.DateTimeOffset.UtcDateTime" /> value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A84 RID: 2692 RVA: 0x000280F6 File Offset: 0x000262F6
		public static bool operator ==(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime == right.UtcDateTime;
		}

		/// <summary>Determines whether two specified <see cref="T:System.DateTimeOffset" /> objects refer to different points in time.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> do not have the same <see cref="P:System.DateTimeOffset.UtcDateTime" /> value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A85 RID: 2693 RVA: 0x0002810B File Offset: 0x0002630B
		public static bool operator !=(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime != right.UtcDateTime;
		}

		/// <summary>Determines whether one specified <see cref="T:System.DateTimeOffset" /> object is less than a second specified <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.DateTimeOffset.UtcDateTime" /> value of <paramref name="left" /> is earlier than the <see cref="P:System.DateTimeOffset.UtcDateTime" /> value of <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A86 RID: 2694 RVA: 0x00028120 File Offset: 0x00026320
		public static bool operator <(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime < right.UtcDateTime;
		}

		/// <summary>Determines whether one specified <see cref="T:System.DateTimeOffset" /> object is less than a second specified <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.DateTimeOffset.UtcDateTime" /> value of <paramref name="left" /> is earlier than the <see cref="P:System.DateTimeOffset.UtcDateTime" /> value of <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A87 RID: 2695 RVA: 0x00028135 File Offset: 0x00026335
		public static bool operator <=(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime <= right.UtcDateTime;
		}

		/// <summary>Determines whether one specified <see cref="T:System.DateTimeOffset" /> object is greater than (or later than) a second specified <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.DateTimeOffset.UtcDateTime" /> value of <paramref name="left" /> is later than the <see cref="P:System.DateTimeOffset.UtcDateTime" /> value of <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A88 RID: 2696 RVA: 0x0002814A File Offset: 0x0002634A
		public static bool operator >(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime > right.UtcDateTime;
		}

		/// <summary>Determines whether one specified <see cref="T:System.DateTimeOffset" /> object is greater than or equal to a second specified <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.DateTimeOffset.UtcDateTime" /> value of <paramref name="left" /> is the same as or later than the <see cref="P:System.DateTimeOffset.UtcDateTime" /> value of <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A89 RID: 2697 RVA: 0x0002815F File Offset: 0x0002635F
		public static bool operator >=(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime >= right.UtcDateTime;
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00028174 File Offset: 0x00026374
		// Note: this type is marked as 'beforefieldinit'.
		static DateTimeOffset()
		{
		}

		// Token: 0x040010C6 RID: 4294
		internal const long MaxOffset = 504000000000L;

		// Token: 0x040010C7 RID: 4295
		internal const long MinOffset = -504000000000L;

		// Token: 0x040010C8 RID: 4296
		private const long UnixEpochSeconds = 62135596800L;

		// Token: 0x040010C9 RID: 4297
		private const long UnixEpochMilliseconds = 62135596800000L;

		// Token: 0x040010CA RID: 4298
		internal const long UnixMinSeconds = -62135596800L;

		// Token: 0x040010CB RID: 4299
		internal const long UnixMaxSeconds = 253402300799L;

		/// <summary>Represents the earliest possible <see cref="T:System.DateTimeOffset" /> value. This field is read-only.</summary>
		// Token: 0x040010CC RID: 4300
		public static readonly DateTimeOffset MinValue = new DateTimeOffset(0L, TimeSpan.Zero);

		/// <summary>Represents the greatest possible value of <see cref="T:System.DateTimeOffset" />. This field is read-only.</summary>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="F:System.DateTime.MaxValue" /> is outside the range of the current or specified culture's default calendar.</exception>
		// Token: 0x040010CD RID: 4301
		public static readonly DateTimeOffset MaxValue = new DateTimeOffset(3155378975999999999L, TimeSpan.Zero);

		// Token: 0x040010CE RID: 4302
		public static readonly DateTimeOffset UnixEpoch = new DateTimeOffset(621355968000000000L, TimeSpan.Zero);

		// Token: 0x040010CF RID: 4303
		private readonly DateTime _dateTime;

		// Token: 0x040010D0 RID: 4304
		private readonly short _offsetMinutes;
	}
}
