using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	// Token: 0x0200011E RID: 286
	internal static class DateTimeFormat
	{
		// Token: 0x06000AF3 RID: 2803 RVA: 0x000289B8 File Offset: 0x00026BB8
		internal static void FormatDigits(StringBuilder outputBuffer, int value, int len)
		{
			DateTimeFormat.FormatDigits(outputBuffer, value, len, false);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x000289C4 File Offset: 0x00026BC4
		internal unsafe static void FormatDigits(StringBuilder outputBuffer, int value, int len, bool overrideLengthLimit)
		{
			if (!overrideLengthLimit && len > 2)
			{
				len = 2;
			}
			char* ptr = stackalloc char[(UIntPtr)32];
			char* ptr2 = ptr + 16;
			int num = value;
			do
			{
				*(--ptr2) = (char)(num % 10 + 48);
				num /= 10;
			}
			while (num != 0 && ptr2 != ptr);
			int num2 = (int)((long)(ptr + 16 - ptr2));
			while (num2 < len && ptr2 != ptr)
			{
				*(--ptr2) = '0';
				num2++;
			}
			outputBuffer.Append(ptr2, num2);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00028A32 File Offset: 0x00026C32
		private static void HebrewFormatDigits(StringBuilder outputBuffer, int digits)
		{
			outputBuffer.Append(HebrewNumber.ToString(digits));
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00028A44 File Offset: 0x00026C44
		internal unsafe static int ParseRepeatPattern(ReadOnlySpan<char> format, int pos, char patternChar)
		{
			int length = format.Length;
			int num = pos + 1;
			while (num < length && *format[num] == (ushort)patternChar)
			{
				num++;
			}
			return num - pos;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00028A76 File Offset: 0x00026C76
		private static string FormatDayOfWeek(int dayOfWeek, int repeat, DateTimeFormatInfo dtfi)
		{
			if (repeat == 3)
			{
				return dtfi.GetAbbreviatedDayName((DayOfWeek)dayOfWeek);
			}
			return dtfi.GetDayName((DayOfWeek)dayOfWeek);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00028A8B File Offset: 0x00026C8B
		private static string FormatMonth(int month, int repeatCount, DateTimeFormatInfo dtfi)
		{
			if (repeatCount == 3)
			{
				return dtfi.GetAbbreviatedMonthName(month);
			}
			return dtfi.GetMonthName(month);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00028AA0 File Offset: 0x00026CA0
		private static string FormatHebrewMonthName(DateTime time, int month, int repeatCount, DateTimeFormatInfo dtfi)
		{
			if (dtfi.Calendar.IsLeapYear(dtfi.Calendar.GetYear(time)))
			{
				return dtfi.internalGetMonthName(month, MonthNameStyles.LeapYear, repeatCount == 3);
			}
			if (month >= 7)
			{
				month++;
			}
			if (repeatCount == 3)
			{
				return dtfi.GetAbbreviatedMonthName(month);
			}
			return dtfi.GetMonthName(month);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00028AF0 File Offset: 0x00026CF0
		internal unsafe static int ParseQuoteString(ReadOnlySpan<char> format, int pos, StringBuilder result)
		{
			int length = format.Length;
			int num = pos;
			char c = (char)(*format[pos++]);
			bool flag = false;
			while (pos < length)
			{
				char c2 = (char)(*format[pos++]);
				if (c2 == c)
				{
					flag = true;
					break;
				}
				if (c2 == '\\')
				{
					if (pos >= length)
					{
						throw new FormatException("Input string was not in a correct format.");
					}
					result.Append((char)(*format[pos++]));
				}
				else
				{
					result.Append(c2);
				}
			}
			if (!flag)
			{
				throw new FormatException(string.Format(CultureInfo.CurrentCulture, "Cannot find a matching quote character for the character '{0}'.", c));
			}
			return pos - num;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00028B8D File Offset: 0x00026D8D
		internal unsafe static int ParseNextChar(ReadOnlySpan<char> format, int pos)
		{
			if (pos >= format.Length - 1)
			{
				return -1;
			}
			return (int)(*format[pos + 1]);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00028BA8 File Offset: 0x00026DA8
		private unsafe static bool IsUseGenitiveForm(ReadOnlySpan<char> format, int index, int tokenLen, char patternToMatch)
		{
			int num = 0;
			int num2 = index - 1;
			while (num2 >= 0 && *format[num2] != (ushort)patternToMatch)
			{
				num2--;
			}
			if (num2 >= 0)
			{
				while (--num2 >= 0 && *format[num2] == (ushort)patternToMatch)
				{
					num++;
				}
				if (num <= 1)
				{
					return true;
				}
			}
			num2 = index + tokenLen;
			while (num2 < format.Length && *format[num2] != (ushort)patternToMatch)
			{
				num2++;
			}
			if (num2 < format.Length)
			{
				num = 0;
				while (++num2 < format.Length && *format[num2] == (ushort)patternToMatch)
				{
					num++;
				}
				if (num <= 1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00028C48 File Offset: 0x00026E48
		private unsafe static StringBuilder FormatCustomized(DateTime dateTime, ReadOnlySpan<char> format, DateTimeFormatInfo dtfi, TimeSpan offset, StringBuilder result)
		{
			Calendar calendar = dtfi.Calendar;
			bool flag = false;
			if (result == null)
			{
				flag = true;
				result = StringBuilderCache.Acquire(16);
			}
			bool flag2 = !GlobalizationMode.Invariant && (ushort)calendar.ID == 8;
			bool flag3 = !GlobalizationMode.Invariant && (ushort)calendar.ID == 3;
			bool timeOnly = true;
			int i = 0;
			while (i < format.Length)
			{
				char c = (char)(*format[i]);
				int num2;
				if (c <= 'K')
				{
					if (c <= '/')
					{
						if (c <= '%')
						{
							if (c != '"')
							{
								if (c != '%')
								{
									goto IL_686;
								}
								int num = DateTimeFormat.ParseNextChar(format, i);
								if (num >= 0 && num != 37)
								{
									char c2 = (char)num;
									DateTimeFormat.FormatCustomized(dateTime, MemoryMarshal.CreateReadOnlySpan<char>(ref c2, 1), dtfi, offset, result);
									num2 = 2;
									goto IL_693;
								}
								if (flag)
								{
									StringBuilderCache.Release(result);
								}
								throw new FormatException("Input string was not in a correct format.");
							}
						}
						else if (c != '\'')
						{
							if (c != '/')
							{
								goto IL_686;
							}
							result.Append(dtfi.DateSeparator);
							num2 = 1;
							goto IL_693;
						}
						num2 = DateTimeFormat.ParseQuoteString(format, i, result);
					}
					else if (c <= 'F')
					{
						if (c != ':')
						{
							if (c != 'F')
							{
								goto IL_686;
							}
							goto IL_209;
						}
						else
						{
							result.Append(dtfi.TimeSeparator);
							num2 = 1;
						}
					}
					else if (c != 'H')
					{
						if (c != 'K')
						{
							goto IL_686;
						}
						num2 = 1;
						DateTimeFormat.FormatCustomizedRoundripTimeZone(dateTime, offset, result);
					}
					else
					{
						num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
						DateTimeFormat.FormatDigits(result, dateTime.Hour, num2);
					}
				}
				else if (c <= 'm')
				{
					if (c <= '\\')
					{
						if (c != 'M')
						{
							if (c != '\\')
							{
								goto IL_686;
							}
							int num = DateTimeFormat.ParseNextChar(format, i);
							if (num < 0)
							{
								if (flag)
								{
									StringBuilderCache.Release(result);
								}
								throw new FormatException("Input string was not in a correct format.");
							}
							result.Append((char)num);
							num2 = 2;
						}
						else
						{
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							int month = calendar.GetMonth(dateTime);
							if (num2 <= 2)
							{
								if (flag2 && !GlobalizationMode.Invariant)
								{
									DateTimeFormat.HebrewFormatDigits(result, month);
								}
								else
								{
									DateTimeFormat.FormatDigits(result, month, num2);
								}
							}
							else if (flag2 && !GlobalizationMode.Invariant)
							{
								result.Append(DateTimeFormat.FormatHebrewMonthName(dateTime, month, num2, dtfi));
							}
							else if ((dtfi.FormatFlags & DateTimeFormatFlags.UseGenitiveMonth) != DateTimeFormatFlags.None && num2 >= 4)
							{
								result.Append(dtfi.internalGetMonthName(month, DateTimeFormat.IsUseGenitiveForm(format, i, num2, 'd') ? MonthNameStyles.Genitive : MonthNameStyles.Regular, false));
							}
							else
							{
								result.Append(DateTimeFormat.FormatMonth(month, num2, dtfi));
							}
							timeOnly = false;
						}
					}
					else
					{
						switch (c)
						{
						case 'd':
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							if (num2 <= 2)
							{
								int dayOfMonth = calendar.GetDayOfMonth(dateTime);
								if (flag2 && !GlobalizationMode.Invariant)
								{
									DateTimeFormat.HebrewFormatDigits(result, dayOfMonth);
								}
								else
								{
									DateTimeFormat.FormatDigits(result, dayOfMonth, num2);
								}
							}
							else
							{
								int dayOfWeek = (int)calendar.GetDayOfWeek(dateTime);
								result.Append(DateTimeFormat.FormatDayOfWeek(dayOfWeek, num2, dtfi));
							}
							timeOnly = false;
							break;
						case 'e':
							goto IL_686;
						case 'f':
							goto IL_209;
						case 'g':
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							result.Append(dtfi.GetEraName(calendar.GetEra(dateTime)));
							break;
						case 'h':
						{
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							int num3 = dateTime.Hour % 12;
							if (num3 == 0)
							{
								num3 = 12;
							}
							DateTimeFormat.FormatDigits(result, num3, num2);
							break;
						}
						default:
							if (c != 'm')
							{
								goto IL_686;
							}
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							DateTimeFormat.FormatDigits(result, dateTime.Minute, num2);
							break;
						}
					}
				}
				else if (c <= 't')
				{
					if (c != 's')
					{
						if (c != 't')
						{
							goto IL_686;
						}
						num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
						if (num2 == 1)
						{
							if (dateTime.Hour < 12)
							{
								if (dtfi.AMDesignator.Length >= 1)
								{
									result.Append(dtfi.AMDesignator[0]);
								}
							}
							else if (dtfi.PMDesignator.Length >= 1)
							{
								result.Append(dtfi.PMDesignator[0]);
							}
						}
						else
						{
							result.Append((dateTime.Hour < 12) ? dtfi.AMDesignator : dtfi.PMDesignator);
						}
					}
					else
					{
						num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
						DateTimeFormat.FormatDigits(result, dateTime.Second, num2);
					}
				}
				else if (c != 'y')
				{
					if (c != 'z')
					{
						goto IL_686;
					}
					num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
					DateTimeFormat.FormatCustomizedTimeZone(dateTime, offset, format, num2, timeOnly, result);
				}
				else
				{
					int year = calendar.GetYear(dateTime);
					num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
					if (flag3 && !AppContextSwitches.FormatJapaneseFirstYearAsANumber && year == 1 && i + num2 < format.Length - 1 && *format[i + num2] == 39 && *format[i + num2 + 1] == (ushort)"年"[0])
					{
						result.Append("元"[0]);
					}
					else if (dtfi.HasForceTwoDigitYears)
					{
						DateTimeFormat.FormatDigits(result, year, (num2 <= 2) ? num2 : 2);
					}
					else if (flag2 && !GlobalizationMode.Invariant)
					{
						DateTimeFormat.HebrewFormatDigits(result, year);
					}
					else if (num2 <= 2)
					{
						DateTimeFormat.FormatDigits(result, year % 100, num2);
					}
					else
					{
						string format2 = "D" + num2.ToString();
						result.Append(year.ToString(format2, CultureInfo.InvariantCulture));
					}
					timeOnly = false;
				}
				IL_693:
				i += num2;
				continue;
				IL_209:
				num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
				if (num2 > 7)
				{
					if (flag)
					{
						StringBuilderCache.Release(result);
					}
					throw new FormatException("Input string was not in a correct format.");
				}
				long num4 = dateTime.Ticks % 10000000L;
				num4 /= (long)Math.Pow(10.0, (double)(7 - num2));
				if (c == 'f')
				{
					result.Append(((int)num4).ToString(DateTimeFormat.fixedNumberFormats[num2 - 1], CultureInfo.InvariantCulture));
					goto IL_693;
				}
				int num5 = num2;
				while (num5 > 0 && num4 % 10L == 0L)
				{
					num4 /= 10L;
					num5--;
				}
				if (num5 > 0)
				{
					result.Append(((int)num4).ToString(DateTimeFormat.fixedNumberFormats[num5 - 1], CultureInfo.InvariantCulture));
					goto IL_693;
				}
				if (result.Length > 0 && result[result.Length - 1] == '.')
				{
					result.Remove(result.Length - 1, 1);
					goto IL_693;
				}
				goto IL_693;
				IL_686:
				result.Append(c);
				num2 = 1;
				goto IL_693;
			}
			return result;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00029300 File Offset: 0x00027500
		private static void FormatCustomizedTimeZone(DateTime dateTime, TimeSpan offset, ReadOnlySpan<char> format, int tokenLen, bool timeOnly, StringBuilder result)
		{
			if (offset == DateTimeFormat.NullOffset)
			{
				if (timeOnly && dateTime.Ticks < 864000000000L)
				{
					offset = TimeZoneInfo.GetLocalUtcOffset(DateTime.Now, TimeZoneInfoOptions.NoThrowOnInvalidTime);
				}
				else if (dateTime.Kind == DateTimeKind.Utc)
				{
					offset = TimeSpan.Zero;
				}
				else
				{
					offset = TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
				}
			}
			if (offset >= TimeSpan.Zero)
			{
				result.Append('+');
			}
			else
			{
				result.Append('-');
				offset = offset.Negate();
			}
			if (tokenLen <= 1)
			{
				result.AppendFormat(CultureInfo.InvariantCulture, "{0:0}", offset.Hours);
				return;
			}
			result.AppendFormat(CultureInfo.InvariantCulture, "{0:00}", offset.Hours);
			if (tokenLen >= 3)
			{
				result.AppendFormat(CultureInfo.InvariantCulture, ":{0:00}", offset.Minutes);
			}
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x000293EC File Offset: 0x000275EC
		private static void FormatCustomizedRoundripTimeZone(DateTime dateTime, TimeSpan offset, StringBuilder result)
		{
			if (offset == DateTimeFormat.NullOffset)
			{
				DateTimeKind kind = dateTime.Kind;
				if (kind == DateTimeKind.Utc)
				{
					result.Append("Z");
					return;
				}
				if (kind != DateTimeKind.Local)
				{
					return;
				}
				offset = TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
			}
			if (offset >= TimeSpan.Zero)
			{
				result.Append('+');
			}
			else
			{
				result.Append('-');
				offset = offset.Negate();
			}
			DateTimeFormat.Append2DigitNumber(result, offset.Hours);
			result.Append(':');
			DateTimeFormat.Append2DigitNumber(result, offset.Minutes);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0002947C File Offset: 0x0002767C
		private static void Append2DigitNumber(StringBuilder result, int val)
		{
			result.Append((char)(48 + val / 10));
			result.Append((char)(48 + val % 10));
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0002949C File Offset: 0x0002769C
		internal unsafe static string GetRealFormat(ReadOnlySpan<char> format, DateTimeFormatInfo dtfi)
		{
			char c = (char)(*format[0]);
			if (c > 'U')
			{
				if (c != 'Y')
				{
					switch (c)
					{
					case 'd':
						return dtfi.ShortDatePattern;
					case 'e':
						goto IL_15B;
					case 'f':
						return dtfi.LongDatePattern + " " + dtfi.ShortTimePattern;
					case 'g':
						return dtfi.GeneralShortTimePattern;
					default:
						switch (c)
						{
						case 'm':
							goto IL_10B;
						case 'n':
						case 'p':
						case 'q':
						case 'v':
						case 'w':
						case 'x':
							goto IL_15B;
						case 'o':
							goto IL_114;
						case 'r':
							goto IL_11C;
						case 's':
							return dtfi.SortableDateTimePattern;
						case 't':
							return dtfi.ShortTimePattern;
						case 'u':
							return dtfi.UniversalSortableDateTimePattern;
						case 'y':
							break;
						default:
							goto IL_15B;
						}
						break;
					}
				}
				return dtfi.YearMonthPattern;
			}
			switch (c)
			{
			case 'D':
				return dtfi.LongDatePattern;
			case 'E':
				goto IL_15B;
			case 'F':
				return dtfi.FullDateTimePattern;
			case 'G':
				return dtfi.GeneralLongTimePattern;
			default:
				switch (c)
				{
				case 'M':
					break;
				case 'N':
				case 'P':
				case 'Q':
				case 'S':
					goto IL_15B;
				case 'O':
					goto IL_114;
				case 'R':
					goto IL_11C;
				case 'T':
					return dtfi.LongTimePattern;
				case 'U':
					return dtfi.FullDateTimePattern;
				default:
					goto IL_15B;
				}
				break;
			}
			IL_10B:
			return dtfi.MonthDayPattern;
			IL_114:
			return "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK";
			IL_11C:
			return dtfi.RFC1123Pattern;
			IL_15B:
			throw new FormatException("Input string was not in a correct format.");
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00029610 File Offset: 0x00027810
		private unsafe static string ExpandPredefinedFormat(ReadOnlySpan<char> format, ref DateTime dateTime, ref DateTimeFormatInfo dtfi, ref TimeSpan offset)
		{
			char c = (char)(*format[0]);
			if (c <= 'R')
			{
				if (c != 'O')
				{
					if (c != 'R')
					{
						goto IL_15D;
					}
					goto IL_5C;
				}
			}
			else if (c != 'U')
			{
				switch (c)
				{
				case 'o':
					break;
				case 'p':
				case 'q':
				case 't':
					goto IL_15D;
				case 'r':
					goto IL_5C;
				case 's':
					dtfi = DateTimeFormatInfo.InvariantInfo;
					goto IL_15D;
				case 'u':
					if (offset != DateTimeFormat.NullOffset)
					{
						dateTime -= offset;
					}
					else if (dateTime.Kind == DateTimeKind.Local)
					{
						DateTimeFormat.InvalidFormatForLocal(format, dateTime);
					}
					dtfi = DateTimeFormatInfo.InvariantInfo;
					goto IL_15D;
				default:
					goto IL_15D;
				}
			}
			else
			{
				if (offset != DateTimeFormat.NullOffset)
				{
					throw new FormatException("Input string was not in a correct format.");
				}
				dtfi = (DateTimeFormatInfo)dtfi.Clone();
				if (dtfi.Calendar.GetType() != typeof(GregorianCalendar))
				{
					dtfi.Calendar = GregorianCalendar.GetDefaultInstance();
				}
				dateTime = dateTime.ToUniversalTime();
				goto IL_15D;
			}
			dtfi = DateTimeFormatInfo.InvariantInfo;
			goto IL_15D;
			IL_5C:
			if (offset != DateTimeFormat.NullOffset)
			{
				dateTime -= offset;
			}
			else if (dateTime.Kind == DateTimeKind.Local)
			{
				DateTimeFormat.InvalidFormatForLocal(format, dateTime);
			}
			dtfi = DateTimeFormatInfo.InvariantInfo;
			IL_15D:
			return DateTimeFormat.GetRealFormat(format, dtfi);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00029782 File Offset: 0x00027982
		internal static string Format(DateTime dateTime, string format, IFormatProvider provider)
		{
			return DateTimeFormat.Format(dateTime, format, provider, DateTimeFormat.NullOffset);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00029794 File Offset: 0x00027994
		internal unsafe static string Format(DateTime dateTime, string format, IFormatProvider provider, TimeSpan offset)
		{
			if (format != null && format.Length == 1)
			{
				char c = format[0];
				if (c <= 'R')
				{
					if (c != 'O')
					{
						if (c != 'R')
						{
							goto IL_93;
						}
						goto IL_6E;
					}
				}
				else if (c != 'o')
				{
					if (c != 'r')
					{
						goto IL_93;
					}
					goto IL_6E;
				}
				Span<char> destination = new Span<char>(stackalloc byte[(UIntPtr)66], 33);
				int length;
				DateTimeFormat.TryFormatO(dateTime, offset, destination, out length);
				return destination.Slice(0, length).ToString();
				IL_6E:
				string text = string.FastAllocateString(29);
				int num;
				DateTimeFormat.TryFormatR(dateTime, offset, new Span<char>(text.GetRawStringData(), text.Length), out num);
				return text;
			}
			IL_93:
			DateTimeFormatInfo instance = DateTimeFormatInfo.GetInstance(provider);
			return StringBuilderCache.GetStringAndRelease(DateTimeFormat.FormatStringBuilder(dateTime, format, instance, offset));
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0002984E File Offset: 0x00027A4E
		internal static bool TryFormat(DateTime dateTime, Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider)
		{
			return DateTimeFormat.TryFormat(dateTime, destination, out charsWritten, format, provider, DateTimeFormat.NullOffset);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00029860 File Offset: 0x00027A60
		internal unsafe static bool TryFormat(DateTime dateTime, Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider, TimeSpan offset)
		{
			if (format.Length == 1)
			{
				char c = (char)(*format[0]);
				if (c <= 'R')
				{
					if (c != 'O')
					{
						if (c != 'R')
						{
							goto IL_47;
						}
						goto IL_3C;
					}
				}
				else if (c != 'o')
				{
					if (c != 'r')
					{
						goto IL_47;
					}
					goto IL_3C;
				}
				return DateTimeFormat.TryFormatO(dateTime, offset, destination, out charsWritten);
				IL_3C:
				return DateTimeFormat.TryFormatR(dateTime, offset, destination, out charsWritten);
			}
			IL_47:
			DateTimeFormatInfo instance = DateTimeFormatInfo.GetInstance(provider);
			StringBuilder stringBuilder = DateTimeFormat.FormatStringBuilder(dateTime, format, instance, offset);
			bool flag = stringBuilder.Length <= destination.Length;
			if (flag)
			{
				stringBuilder.CopyTo(0, destination, stringBuilder.Length);
				charsWritten = stringBuilder.Length;
			}
			else
			{
				charsWritten = 0;
			}
			StringBuilderCache.Release(stringBuilder);
			return flag;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00029900 File Offset: 0x00027B00
		private static StringBuilder FormatStringBuilder(DateTime dateTime, ReadOnlySpan<char> format, DateTimeFormatInfo dtfi, TimeSpan offset)
		{
			if (format.Length == 0)
			{
				bool flag = false;
				if (dateTime.Ticks < 864000000000L)
				{
					CalendarId calendarId = (CalendarId)dtfi.Calendar.ID;
					switch (calendarId)
					{
					case CalendarId.JAPAN:
					case CalendarId.TAIWAN:
					case CalendarId.HIJRI:
					case CalendarId.HEBREW:
						break;
					case CalendarId.KOREA:
					case CalendarId.THAI:
						goto IL_62;
					default:
						if (calendarId != CalendarId.JULIAN && calendarId - CalendarId.PERSIAN > 1)
						{
							goto IL_62;
						}
						break;
					}
					flag = true;
					dtfi = DateTimeFormatInfo.InvariantInfo;
				}
				IL_62:
				if (offset == DateTimeFormat.NullOffset)
				{
					format = (flag ? "s" : "G");
				}
				else
				{
					format = (flag ? "yyyy'-'MM'-'ddTHH':'mm':'ss zzz" : dtfi.DateTimeOffsetPattern);
				}
			}
			if (format.Length == 1)
			{
				format = DateTimeFormat.ExpandPredefinedFormat(format, ref dateTime, ref dtfi, ref offset);
			}
			return DateTimeFormat.FormatCustomized(dateTime, format, dtfi, offset, null);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x000299D4 File Offset: 0x00027BD4
		private unsafe static bool TryFormatO(DateTime dateTime, TimeSpan offset, Span<char> destination, out int charsWritten)
		{
			int num = 27;
			DateTimeKind dateTimeKind = DateTimeKind.Local;
			if (offset == DateTimeFormat.NullOffset)
			{
				dateTimeKind = dateTime.Kind;
				if (dateTimeKind == DateTimeKind.Local)
				{
					offset = TimeZoneInfo.Local.GetUtcOffset(dateTime);
					num += 6;
				}
				else if (dateTimeKind == DateTimeKind.Utc)
				{
					num++;
				}
			}
			else
			{
				num += 6;
			}
			if (destination.Length < num)
			{
				charsWritten = 0;
				return false;
			}
			charsWritten = num;
			ref char ptr = ref destination[26];
			DateTimeFormat.WriteFourDecimalDigits((uint)dateTime.Year, destination, 0);
			*destination[4] = '-';
			DateTimeFormat.WriteTwoDecimalDigits((uint)dateTime.Month, destination, 5);
			*destination[7] = '-';
			DateTimeFormat.WriteTwoDecimalDigits((uint)dateTime.Day, destination, 8);
			*destination[10] = 'T';
			DateTimeFormat.WriteTwoDecimalDigits((uint)dateTime.Hour, destination, 11);
			*destination[13] = ':';
			DateTimeFormat.WriteTwoDecimalDigits((uint)dateTime.Minute, destination, 14);
			*destination[16] = ':';
			DateTimeFormat.WriteTwoDecimalDigits((uint)dateTime.Second, destination, 17);
			*destination[19] = '.';
			DateTimeFormat.WriteDigits((ulong)((uint)(dateTime.Ticks % 10000000L)), destination.Slice(20, 7));
			if (dateTimeKind == DateTimeKind.Local)
			{
				char c;
				if (offset < default(TimeSpan))
				{
					c = '-';
					offset = TimeSpan.FromTicks(-offset.Ticks);
				}
				else
				{
					c = '+';
				}
				DateTimeFormat.WriteTwoDecimalDigits((uint)offset.Minutes, destination, 31);
				*destination[30] = ':';
				DateTimeFormat.WriteTwoDecimalDigits((uint)offset.Hours, destination, 28);
				*destination[27] = c;
			}
			else if (dateTimeKind == DateTimeKind.Utc)
			{
				*destination[27] = 'Z';
			}
			return true;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00029B6C File Offset: 0x00027D6C
		private unsafe static bool TryFormatR(DateTime dateTime, TimeSpan offset, Span<char> destination, out int charsWritten)
		{
			if (28 >= destination.Length)
			{
				charsWritten = 0;
				return false;
			}
			if (offset != DateTimeFormat.NullOffset)
			{
				dateTime -= offset;
			}
			int value;
			int num;
			int value2;
			dateTime.GetDatePart(out value, out num, out value2);
			string text = DateTimeFormat.InvariantAbbreviatedDayNames[(int)dateTime.DayOfWeek];
			string text2 = DateTimeFormat.InvariantAbbreviatedMonthNames[num - 1];
			*destination[0] = text[0];
			*destination[1] = text[1];
			*destination[2] = text[2];
			*destination[3] = ',';
			*destination[4] = ' ';
			DateTimeFormat.WriteTwoDecimalDigits((uint)value2, destination, 5);
			*destination[7] = ' ';
			*destination[8] = text2[0];
			*destination[9] = text2[1];
			*destination[10] = text2[2];
			*destination[11] = ' ';
			DateTimeFormat.WriteFourDecimalDigits((uint)value, destination, 12);
			*destination[16] = ' ';
			DateTimeFormat.WriteTwoDecimalDigits((uint)dateTime.Hour, destination, 17);
			*destination[19] = ':';
			DateTimeFormat.WriteTwoDecimalDigits((uint)dateTime.Minute, destination, 20);
			*destination[22] = ':';
			DateTimeFormat.WriteTwoDecimalDigits((uint)dateTime.Second, destination, 23);
			*destination[25] = ' ';
			*destination[26] = 'G';
			*destination[27] = 'M';
			*destination[28] = 'T';
			charsWritten = 29;
			return true;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00029CF0 File Offset: 0x00027EF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void WriteTwoDecimalDigits(uint value, Span<char> destination, int offset)
		{
			uint num = 48U + value;
			value /= 10U;
			*destination[offset + 1] = (char)(num - value * 10U);
			*destination[offset] = (char)(48U + value);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00029D28 File Offset: 0x00027F28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void WriteFourDecimalDigits(uint value, Span<char> buffer, int startingIndex = 0)
		{
			uint num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 3] = (char)(num - value * 10U);
			num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 2] = (char)(num - value * 10U);
			num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 1] = (char)(num - value * 10U);
			*buffer[startingIndex] = (char)(48U + value);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00029D9C File Offset: 0x00027F9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void WriteDigits(ulong value, Span<char> buffer)
		{
			for (int i = buffer.Length - 1; i >= 1; i--)
			{
				ulong num = 48UL + value;
				value /= 10UL;
				*buffer[i] = (char)(num - value * 10UL);
			}
			*buffer[0] = (char)(48UL + value);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00029DEC File Offset: 0x00027FEC
		internal static string[] GetAllDateTimes(DateTime dateTime, char format, DateTimeFormatInfo dtfi)
		{
			string[] allDateTimePatterns;
			string[] array;
			if (format <= 'U')
			{
				switch (format)
				{
				case 'D':
				case 'F':
				case 'G':
					break;
				case 'E':
					goto IL_138;
				default:
					switch (format)
					{
					case 'M':
					case 'T':
						break;
					case 'N':
					case 'P':
					case 'Q':
					case 'S':
						goto IL_138;
					case 'O':
					case 'R':
						goto IL_11E;
					case 'U':
					{
						DateTime dateTime2 = dateTime.ToUniversalTime();
						allDateTimePatterns = dtfi.GetAllDateTimePatterns(format);
						array = new string[allDateTimePatterns.Length];
						for (int i = 0; i < allDateTimePatterns.Length; i++)
						{
							array[i] = DateTimeFormat.Format(dateTime2, allDateTimePatterns[i], dtfi);
						}
						return array;
					}
					default:
						goto IL_138;
					}
					break;
				}
			}
			else if (format != 'Y')
			{
				switch (format)
				{
				case 'd':
				case 'f':
				case 'g':
					break;
				case 'e':
					goto IL_138;
				default:
					switch (format)
					{
					case 'm':
					case 't':
					case 'y':
						break;
					case 'n':
					case 'p':
					case 'q':
					case 'v':
					case 'w':
					case 'x':
						goto IL_138;
					case 'o':
					case 'r':
					case 's':
					case 'u':
						goto IL_11E;
					default:
						goto IL_138;
					}
					break;
				}
			}
			allDateTimePatterns = dtfi.GetAllDateTimePatterns(format);
			array = new string[allDateTimePatterns.Length];
			for (int j = 0; j < allDateTimePatterns.Length; j++)
			{
				array[j] = DateTimeFormat.Format(dateTime, allDateTimePatterns[j], dtfi);
			}
			return array;
			IL_11E:
			return new string[]
			{
				DateTimeFormat.Format(dateTime, new string(format, 1), dtfi)
			};
			IL_138:
			throw new FormatException("Input string was not in a correct format.");
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00029F40 File Offset: 0x00028140
		internal static string[] GetAllDateTimes(DateTime dateTime, DateTimeFormatInfo dtfi)
		{
			List<string> list = new List<string>(132);
			for (int i = 0; i < DateTimeFormat.allStandardFormats.Length; i++)
			{
				string[] allDateTimes = DateTimeFormat.GetAllDateTimes(dateTime, DateTimeFormat.allStandardFormats[i], dtfi);
				for (int j = 0; j < allDateTimes.Length; j++)
				{
					list.Add(allDateTimes[j]);
				}
			}
			string[] array = new string[list.Count];
			list.CopyTo(0, array, 0, list.Count);
			return array;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00004BF9 File Offset: 0x00002DF9
		internal static void InvalidFormatForLocal(ReadOnlySpan<char> format, DateTime dateTime)
		{
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00029FB4 File Offset: 0x000281B4
		// Note: this type is marked as 'beforefieldinit'.
		static DateTimeFormat()
		{
		}

		// Token: 0x040010E5 RID: 4325
		internal const int MaxSecondsFractionDigits = 7;

		// Token: 0x040010E6 RID: 4326
		internal static readonly TimeSpan NullOffset = TimeSpan.MinValue;

		// Token: 0x040010E7 RID: 4327
		internal static char[] allStandardFormats = new char[]
		{
			'd',
			'D',
			'f',
			'F',
			'g',
			'G',
			'm',
			'M',
			'o',
			'O',
			'r',
			'R',
			's',
			't',
			'T',
			'u',
			'U',
			'y',
			'Y'
		};

		// Token: 0x040010E8 RID: 4328
		internal const string RoundtripFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK";

		// Token: 0x040010E9 RID: 4329
		internal const string RoundtripDateTimeUnfixed = "yyyy'-'MM'-'ddTHH':'mm':'ss zzz";

		// Token: 0x040010EA RID: 4330
		private const int DEFAULT_ALL_DATETIMES_SIZE = 132;

		// Token: 0x040010EB RID: 4331
		internal static readonly DateTimeFormatInfo InvariantFormatInfo = CultureInfo.InvariantCulture.DateTimeFormat;

		// Token: 0x040010EC RID: 4332
		internal static readonly string[] InvariantAbbreviatedMonthNames = DateTimeFormat.InvariantFormatInfo.AbbreviatedMonthNames;

		// Token: 0x040010ED RID: 4333
		internal static readonly string[] InvariantAbbreviatedDayNames = DateTimeFormat.InvariantFormatInfo.AbbreviatedDayNames;

		// Token: 0x040010EE RID: 4334
		internal const string Gmt = "GMT";

		// Token: 0x040010EF RID: 4335
		internal static string[] fixedNumberFormats = new string[]
		{
			"0",
			"00",
			"000",
			"0000",
			"00000",
			"000000",
			"0000000"
		};
	}
}
