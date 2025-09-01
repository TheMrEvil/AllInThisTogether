using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Persists an 8-byte <see cref="T:System.DateTime" /> constant for a field or parameter.</summary>
	// Token: 0x020007EC RID: 2028
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[Serializable]
	public sealed class DateTimeConstantAttribute : CustomConstantAttribute
	{
		/// <summary>Initializes a new instance of the <see langword="DateTimeConstantAttribute" /> class with the number of 100-nanosecond ticks that represent the date and time of this instance.</summary>
		/// <param name="ticks">The number of 100-nanosecond ticks that represent the date and time of this instance.</param>
		// Token: 0x060045F3 RID: 17907 RVA: 0x000E574F File Offset: 0x000E394F
		public DateTimeConstantAttribute(long ticks)
		{
			this._date = new DateTime(ticks);
		}

		/// <summary>Gets the number of 100-nanosecond ticks that represent the date and time of this instance.</summary>
		/// <returns>The number of 100-nanosecond ticks that represent the date and time of this instance.</returns>
		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x060045F4 RID: 17908 RVA: 0x000E5763 File Offset: 0x000E3963
		public override object Value
		{
			get
			{
				return this._date;
			}
		}

		// Token: 0x04002D36 RID: 11574
		private DateTime _date;
	}
}
