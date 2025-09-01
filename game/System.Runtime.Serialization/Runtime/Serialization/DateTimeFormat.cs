using System;
using System.Globalization;

namespace System.Runtime.Serialization
{
	/// <summary>Specifies date-time format options.</summary>
	// Token: 0x020000CC RID: 204
	public class DateTimeFormat
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.DateTimeFormat" /> class using the format string.</summary>
		/// <param name="formatString">The format string.</param>
		// Token: 0x06000BF4 RID: 3060 RVA: 0x00031B46 File Offset: 0x0002FD46
		public DateTimeFormat(string formatString) : this(formatString, DateTimeFormatInfo.CurrentInfo)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.DateTimeFormat" /> class using the format string and format provider.</summary>
		/// <param name="formatString">The format sting.</param>
		/// <param name="formatProvider">The format provider.</param>
		// Token: 0x06000BF5 RID: 3061 RVA: 0x00031B54 File Offset: 0x0002FD54
		public DateTimeFormat(string formatString, IFormatProvider formatProvider)
		{
			if (formatString == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("formatString");
			}
			if (formatProvider == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("formatProvider");
			}
			this.formatString = formatString;
			this.formatProvider = formatProvider;
			this.dateTimeStyles = DateTimeStyles.RoundtripKind;
		}

		/// <summary>Gets the format strings to control the formatting produced when a date or time is represented as a string.</summary>
		/// <returns>The format strings to control the formatting produced when a date or time is represented as a string.</returns>
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x00031B91 File Offset: 0x0002FD91
		public string FormatString
		{
			get
			{
				return this.formatString;
			}
		}

		/// <summary>Gets an object that controls formatting.</summary>
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x00031B99 File Offset: 0x0002FD99
		public IFormatProvider FormatProvider
		{
			get
			{
				return this.formatProvider;
			}
		}

		/// <summary>Gets or sets the formatting options that customize string parsing for some date and time parsing methods.</summary>
		/// <returns>The formatting options that customize string parsing for some date and time parsing methods.</returns>
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x00031BA1 File Offset: 0x0002FDA1
		// (set) Token: 0x06000BF9 RID: 3065 RVA: 0x00031BA9 File Offset: 0x0002FDA9
		public DateTimeStyles DateTimeStyles
		{
			get
			{
				return this.dateTimeStyles;
			}
			set
			{
				this.dateTimeStyles = value;
			}
		}

		// Token: 0x040004C5 RID: 1221
		private string formatString;

		// Token: 0x040004C6 RID: 1222
		private IFormatProvider formatProvider;

		// Token: 0x040004C7 RID: 1223
		private DateTimeStyles dateTimeStyles;
	}
}
