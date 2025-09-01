using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Net.Mime
{
	// Token: 0x02000813 RID: 2067
	internal class SmtpDateTime
	{
		// Token: 0x060041D9 RID: 16857 RVA: 0x000E35A8 File Offset: 0x000E17A8
		internal static Dictionary<string, TimeSpan> InitializeShortHandLookups()
		{
			return new Dictionary<string, TimeSpan>
			{
				{
					"UT",
					TimeSpan.Zero
				},
				{
					"GMT",
					TimeSpan.Zero
				},
				{
					"EDT",
					new TimeSpan(-4, 0, 0)
				},
				{
					"EST",
					new TimeSpan(-5, 0, 0)
				},
				{
					"CDT",
					new TimeSpan(-5, 0, 0)
				},
				{
					"CST",
					new TimeSpan(-6, 0, 0)
				},
				{
					"MDT",
					new TimeSpan(-6, 0, 0)
				},
				{
					"MST",
					new TimeSpan(-7, 0, 0)
				},
				{
					"PDT",
					new TimeSpan(-7, 0, 0)
				},
				{
					"PST",
					new TimeSpan(-8, 0, 0)
				}
			};
		}

		// Token: 0x060041DA RID: 16858 RVA: 0x000E367C File Offset: 0x000E187C
		internal SmtpDateTime(DateTime value)
		{
			this._date = value;
			switch (value.Kind)
			{
			case DateTimeKind.Unspecified:
				this._unknownTimeZone = true;
				return;
			case DateTimeKind.Utc:
				this._timeZone = TimeSpan.Zero;
				return;
			case DateTimeKind.Local:
			{
				TimeSpan utcOffset = TimeZoneInfo.Local.GetUtcOffset(value);
				this._timeZone = this.ValidateAndGetSanitizedTimeSpan(utcOffset);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x060041DB RID: 16859 RVA: 0x000E36E0 File Offset: 0x000E18E0
		internal SmtpDateTime(string value)
		{
			string timeZoneString;
			this._date = this.ParseValue(value, out timeZoneString);
			if (!this.TryParseTimeZoneString(timeZoneString, out this._timeZone))
			{
				this._unknownTimeZone = true;
			}
		}

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x060041DC RID: 16860 RVA: 0x000E3718 File Offset: 0x000E1918
		internal DateTime Date
		{
			get
			{
				if (this._unknownTimeZone)
				{
					return DateTime.SpecifyKind(this._date, DateTimeKind.Unspecified);
				}
				DateTimeOffset dateTimeOffset = new DateTimeOffset(this._date, this._timeZone);
				return dateTimeOffset.LocalDateTime;
			}
		}

		// Token: 0x060041DD RID: 16861 RVA: 0x000E3754 File Offset: 0x000E1954
		public override string ToString()
		{
			return string.Format("{0} {1}", this.FormatDate(this._date), this._unknownTimeZone ? "-0000" : this.TimeSpanToOffset(this._timeZone));
		}

		// Token: 0x060041DE RID: 16862 RVA: 0x000E3788 File Offset: 0x000E1988
		internal void ValidateAndGetTimeZoneOffsetValues(string offset, out bool positive, out int hours, out int minutes)
		{
			if (offset.Length != 5)
			{
				throw new FormatException("The date is in an invalid format.");
			}
			positive = offset.StartsWith("+", StringComparison.Ordinal);
			if (!int.TryParse(offset.Substring(1, 2), NumberStyles.None, CultureInfo.InvariantCulture, out hours))
			{
				throw new FormatException("The date is in an invalid format.");
			}
			if (!int.TryParse(offset.Substring(3, 2), NumberStyles.None, CultureInfo.InvariantCulture, out minutes))
			{
				throw new FormatException("The date is in an invalid format.");
			}
			if (minutes > 59)
			{
				throw new FormatException("The date is in an invalid format.");
			}
		}

		// Token: 0x060041DF RID: 16863 RVA: 0x000E380C File Offset: 0x000E1A0C
		internal void ValidateTimeZoneShortHandValue(string value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				if (!char.IsLetter(value, i))
				{
					throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", value));
				}
			}
		}

		// Token: 0x060041E0 RID: 16864 RVA: 0x000E3844 File Offset: 0x000E1A44
		internal string FormatDate(DateTime value)
		{
			return value.ToString("ddd, dd MMM yyyy HH:mm:ss", CultureInfo.InvariantCulture);
		}

		// Token: 0x060041E1 RID: 16865 RVA: 0x000E3858 File Offset: 0x000E1A58
		internal DateTime ParseValue(string data, out string timeZone)
		{
			if (string.IsNullOrEmpty(data))
			{
				throw new FormatException("The date is in an invalid format.");
			}
			int num = data.IndexOf(':');
			if (num == -1)
			{
				throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data));
			}
			int num2 = data.IndexOfAny(SmtpDateTime.s_allowedWhiteSpaceChars, num);
			if (num2 == -1)
			{
				throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data));
			}
			DateTime result;
			if (!DateTime.TryParseExact(data.Substring(0, num2).Trim(), SmtpDateTime.s_validDateTimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out result))
			{
				throw new FormatException("The date is in an invalid format.");
			}
			string text = data.Substring(num2).Trim();
			int num3 = text.IndexOfAny(SmtpDateTime.s_allowedWhiteSpaceChars);
			if (num3 != -1)
			{
				text = text.Substring(0, num3);
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new FormatException("The date is in an invalid format.");
			}
			timeZone = text;
			return result;
		}

		// Token: 0x060041E2 RID: 16866 RVA: 0x000E3924 File Offset: 0x000E1B24
		internal bool TryParseTimeZoneString(string timeZoneString, out TimeSpan timeZone)
		{
			if (timeZoneString == "-0000")
			{
				timeZone = TimeSpan.Zero;
				return false;
			}
			if (timeZoneString[0] == '+' || timeZoneString[0] == '-')
			{
				bool flag;
				int num;
				int num2;
				this.ValidateAndGetTimeZoneOffsetValues(timeZoneString, out flag, out num, out num2);
				if (!flag)
				{
					if (num != 0)
					{
						num *= -1;
					}
					else if (num2 != 0)
					{
						num2 *= -1;
					}
				}
				timeZone = new TimeSpan(num, num2, 0);
				return true;
			}
			this.ValidateTimeZoneShortHandValue(timeZoneString);
			return SmtpDateTime.s_timeZoneOffsetLookup.TryGetValue(timeZoneString, out timeZone);
		}

		// Token: 0x060041E3 RID: 16867 RVA: 0x000E39A4 File Offset: 0x000E1BA4
		internal TimeSpan ValidateAndGetSanitizedTimeSpan(TimeSpan span)
		{
			TimeSpan result = new TimeSpan(span.Days, span.Hours, span.Minutes, 0, 0);
			if (Math.Abs(result.Ticks) > 3599400000000L)
			{
				throw new FormatException("The date is in an invalid format.");
			}
			return result;
		}

		// Token: 0x060041E4 RID: 16868 RVA: 0x000E39F4 File Offset: 0x000E1BF4
		internal string TimeSpanToOffset(TimeSpan span)
		{
			if (span.Ticks == 0L)
			{
				return "+0000";
			}
			uint num = (uint)Math.Abs(Math.Floor(span.TotalHours));
			uint num2 = (uint)Math.Abs(span.Minutes);
			string str = (span.Ticks > 0L) ? "+" : "-";
			if (num < 10U)
			{
				str += "0";
			}
			str += num.ToString();
			if (num2 < 10U)
			{
				str += "0";
			}
			return str + num2.ToString();
		}

		// Token: 0x060041E5 RID: 16869 RVA: 0x000E3A88 File Offset: 0x000E1C88
		// Note: this type is marked as 'beforefieldinit'.
		static SmtpDateTime()
		{
		}

		// Token: 0x040027F3 RID: 10227
		internal const string UnknownTimeZoneDefaultOffset = "-0000";

		// Token: 0x040027F4 RID: 10228
		internal const string UtcDefaultTimeZoneOffset = "+0000";

		// Token: 0x040027F5 RID: 10229
		internal const int OffsetLength = 5;

		// Token: 0x040027F6 RID: 10230
		internal const int MaxMinuteValue = 59;

		// Token: 0x040027F7 RID: 10231
		internal const string DateFormatWithDayOfWeek = "ddd, dd MMM yyyy HH:mm:ss";

		// Token: 0x040027F8 RID: 10232
		internal const string DateFormatWithoutDayOfWeek = "dd MMM yyyy HH:mm:ss";

		// Token: 0x040027F9 RID: 10233
		internal const string DateFormatWithDayOfWeekAndNoSeconds = "ddd, dd MMM yyyy HH:mm";

		// Token: 0x040027FA RID: 10234
		internal const string DateFormatWithoutDayOfWeekAndNoSeconds = "dd MMM yyyy HH:mm";

		// Token: 0x040027FB RID: 10235
		internal static readonly string[] s_validDateTimeFormats = new string[]
		{
			"ddd, dd MMM yyyy HH:mm:ss",
			"dd MMM yyyy HH:mm:ss",
			"ddd, dd MMM yyyy HH:mm",
			"dd MMM yyyy HH:mm"
		};

		// Token: 0x040027FC RID: 10236
		internal static readonly char[] s_allowedWhiteSpaceChars = new char[]
		{
			' ',
			'\t'
		};

		// Token: 0x040027FD RID: 10237
		internal static readonly Dictionary<string, TimeSpan> s_timeZoneOffsetLookup = SmtpDateTime.InitializeShortHandLookups();

		// Token: 0x040027FE RID: 10238
		internal const long TimeSpanMaxTicks = 3599400000000L;

		// Token: 0x040027FF RID: 10239
		internal const int OffsetMaxValue = 9959;

		// Token: 0x04002800 RID: 10240
		private readonly DateTime _date;

		// Token: 0x04002801 RID: 10241
		private readonly TimeSpan _timeZone;

		// Token: 0x04002802 RID: 10242
		private readonly bool _unknownTimeZone;
	}
}
