using System;

namespace System.Globalization
{
	/// <summary>Defines the formatting options that customize string parsing for some date and time parsing methods.</summary>
	// Token: 0x02000961 RID: 2401
	[Flags]
	public enum DateTimeStyles
	{
		/// <summary>Default formatting options must be used. This value represents the default style for the <see cref="M:System.DateTime.Parse(System.String)" />, <see cref="M:System.DateTime.ParseExact(System.String,System.String,System.IFormatProvider)" />, and <see cref="M:System.DateTime.TryParse(System.String,System.DateTime@)" /> methods.</summary>
		// Token: 0x0400347A RID: 13434
		None = 0,
		/// <summary>Leading white-space characters must be ignored during parsing, except if they occur in the <see cref="T:System.Globalization.DateTimeFormatInfo" /> format patterns.</summary>
		// Token: 0x0400347B RID: 13435
		AllowLeadingWhite = 1,
		/// <summary>Trailing white-space characters must be ignored during parsing, except if they occur in the <see cref="T:System.Globalization.DateTimeFormatInfo" /> format patterns.</summary>
		// Token: 0x0400347C RID: 13436
		AllowTrailingWhite = 2,
		/// <summary>Extra white-space characters in the middle of the string must be ignored during parsing, except if they occur in the <see cref="T:System.Globalization.DateTimeFormatInfo" /> format patterns.</summary>
		// Token: 0x0400347D RID: 13437
		AllowInnerWhite = 4,
		/// <summary>Extra white-space characters anywhere in the string must be ignored during parsing, except if they occur in the <see cref="T:System.Globalization.DateTimeFormatInfo" /> format patterns. This value is a combination of the <see cref="F:System.Globalization.DateTimeStyles.AllowLeadingWhite" />, <see cref="F:System.Globalization.DateTimeStyles.AllowTrailingWhite" />, and <see cref="F:System.Globalization.DateTimeStyles.AllowInnerWhite" /> values.</summary>
		// Token: 0x0400347E RID: 13438
		AllowWhiteSpaces = 7,
		/// <summary>If the parsed string contains only the time and not the date, the parsing methods assume the Gregorian date with year = 1, month = 1, and day = 1. If this value is not used, the current date is assumed.</summary>
		// Token: 0x0400347F RID: 13439
		NoCurrentDateDefault = 8,
		/// <summary>Date and time are returned as a Coordinated Universal Time (UTC). If the input string denotes a local time, through a time zone specifier or <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" />, the date and time are converted from the local time to UTC. If the input string denotes a UTC time, through a time zone specifier or <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />, no conversion occurs. If the input string does not denote a local or UTC time, no conversion occurs and the resulting <see cref="P:System.DateTime.Kind" /> property is <see cref="F:System.DateTimeKind.Unspecified" />.</summary>
		// Token: 0x04003480 RID: 13440
		AdjustToUniversal = 16,
		/// <summary>If no time zone is specified in the parsed string, the string is assumed to denote a local time.</summary>
		// Token: 0x04003481 RID: 13441
		AssumeLocal = 32,
		/// <summary>If no time zone is specified in the parsed string, the string is assumed to denote a UTC.</summary>
		// Token: 0x04003482 RID: 13442
		AssumeUniversal = 64,
		/// <summary>The <see cref="T:System.DateTimeKind" /> field of a date is preserved when a <see cref="T:System.DateTime" /> object is converted to a string using the "o" or "r" standard format specifier, and the string is then converted back to a <see cref="T:System.DateTime" /> object.</summary>
		// Token: 0x04003483 RID: 13443
		RoundtripKind = 128
	}
}
