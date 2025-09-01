using System;

namespace System.Globalization
{
	/// <summary>Defines the period of daylight saving time.</summary>
	// Token: 0x02000962 RID: 2402
	[Serializable]
	public class DaylightTime
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.DaylightTime" /> class with the specified start, end, and time difference information.</summary>
		/// <param name="start">The object that represents the date and time when daylight saving time begins. The value must be in local time.</param>
		/// <param name="end">The object that represents the date and time when daylight saving time ends. The value must be in local time.</param>
		/// <param name="delta">The object that represents the difference between standard time and daylight saving time, in ticks.</param>
		// Token: 0x0600553A RID: 21818 RVA: 0x0011DE38 File Offset: 0x0011C038
		public DaylightTime(DateTime start, DateTime end, TimeSpan delta)
		{
			this._start = start;
			this._end = end;
			this._delta = delta;
		}

		/// <summary>Gets the object that represents the date and time when the daylight saving period begins.</summary>
		/// <returns>The object that represents the date and time when the daylight saving period begins. The value is in local time.</returns>
		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x0600553B RID: 21819 RVA: 0x0011DE55 File Offset: 0x0011C055
		public DateTime Start
		{
			get
			{
				return this._start;
			}
		}

		/// <summary>Gets the object that represents the date and time when the daylight saving period ends.</summary>
		/// <returns>The object that represents the date and time when the daylight saving period ends. The value is in local time.</returns>
		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x0600553C RID: 21820 RVA: 0x0011DE5D File Offset: 0x0011C05D
		public DateTime End
		{
			get
			{
				return this._end;
			}
		}

		/// <summary>Gets the time interval that represents the difference between standard time and daylight saving time.</summary>
		/// <returns>The time interval that represents the difference between standard time and daylight saving time.</returns>
		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x0600553D RID: 21821 RVA: 0x0011DE65 File Offset: 0x0011C065
		public TimeSpan Delta
		{
			get
			{
				return this._delta;
			}
		}

		// Token: 0x04003484 RID: 13444
		private readonly DateTime _start;

		// Token: 0x04003485 RID: 13445
		private readonly DateTime _end;

		// Token: 0x04003486 RID: 13446
		private readonly TimeSpan _delta;
	}
}
