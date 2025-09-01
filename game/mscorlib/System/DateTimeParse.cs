using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace System
{
	// Token: 0x0200011F RID: 287
	internal static class DateTimeParse
	{
		// Token: 0x06000B11 RID: 2833 RVA: 0x0002A054 File Offset: 0x00028254
		internal static DateTime ParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, DateTimeFormatInfo dtfi, DateTimeStyles style)
		{
			DateTimeResult dateTimeResult = default(DateTimeResult);
			dateTimeResult.Init(s);
			if (DateTimeParse.TryParseExact(s, format, dtfi, style, ref dateTimeResult))
			{
				return dateTimeResult.parsedDate;
			}
			throw DateTimeParse.GetDateTimeParseException(ref dateTimeResult);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0002A08C File Offset: 0x0002828C
		internal static DateTime ParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, DateTimeFormatInfo dtfi, DateTimeStyles style, out TimeSpan offset)
		{
			DateTimeResult dateTimeResult = default(DateTimeResult);
			offset = TimeSpan.Zero;
			dateTimeResult.Init(s);
			dateTimeResult.flags |= ParseFlags.CaptureOffset;
			if (DateTimeParse.TryParseExact(s, format, dtfi, style, ref dateTimeResult))
			{
				offset = dateTimeResult.timeZoneOffset;
				return dateTimeResult.parsedDate;
			}
			throw DateTimeParse.GetDateTimeParseException(ref dateTimeResult);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0002A0F0 File Offset: 0x000282F0
		internal static bool TryParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, DateTimeFormatInfo dtfi, DateTimeStyles style, out DateTime result)
		{
			result = DateTime.MinValue;
			DateTimeResult dateTimeResult = default(DateTimeResult);
			dateTimeResult.Init(s);
			if (DateTimeParse.TryParseExact(s, format, dtfi, style, ref dateTimeResult))
			{
				result = dateTimeResult.parsedDate;
				return true;
			}
			return false;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0002A138 File Offset: 0x00028338
		internal static bool TryParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, DateTimeFormatInfo dtfi, DateTimeStyles style, out DateTime result, out TimeSpan offset)
		{
			result = DateTime.MinValue;
			offset = TimeSpan.Zero;
			DateTimeResult dateTimeResult = default(DateTimeResult);
			dateTimeResult.Init(s);
			dateTimeResult.flags |= ParseFlags.CaptureOffset;
			if (DateTimeParse.TryParseExact(s, format, dtfi, style, ref dateTimeResult))
			{
				result = dateTimeResult.parsedDate;
				offset = dateTimeResult.timeZoneOffset;
				return true;
			}
			return false;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0002A1A7 File Offset: 0x000283A7
		internal static bool TryParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, DateTimeFormatInfo dtfi, DateTimeStyles style, ref DateTimeResult result)
		{
			if (s.Length == 0)
			{
				result.SetFailure(ParseFailureKind.FormatWithParameter, "String was not recognized as a valid DateTime.");
				return false;
			}
			if (format.Length == 0)
			{
				result.SetBadFormatSpecifierFailure();
				return false;
			}
			return DateTimeParse.DoStrictParse(s, format, style, dtfi, ref result);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0002A1E0 File Offset: 0x000283E0
		internal static DateTime ParseExactMultiple(ReadOnlySpan<char> s, string[] formats, DateTimeFormatInfo dtfi, DateTimeStyles style)
		{
			DateTimeResult dateTimeResult = default(DateTimeResult);
			dateTimeResult.Init(s);
			if (DateTimeParse.TryParseExactMultiple(s, formats, dtfi, style, ref dateTimeResult))
			{
				return dateTimeResult.parsedDate;
			}
			throw DateTimeParse.GetDateTimeParseException(ref dateTimeResult);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0002A218 File Offset: 0x00028418
		internal static DateTime ParseExactMultiple(ReadOnlySpan<char> s, string[] formats, DateTimeFormatInfo dtfi, DateTimeStyles style, out TimeSpan offset)
		{
			DateTimeResult dateTimeResult = default(DateTimeResult);
			offset = TimeSpan.Zero;
			dateTimeResult.Init(s);
			dateTimeResult.flags |= ParseFlags.CaptureOffset;
			if (DateTimeParse.TryParseExactMultiple(s, formats, dtfi, style, ref dateTimeResult))
			{
				offset = dateTimeResult.timeZoneOffset;
				return dateTimeResult.parsedDate;
			}
			throw DateTimeParse.GetDateTimeParseException(ref dateTimeResult);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0002A27C File Offset: 0x0002847C
		internal static bool TryParseExactMultiple(ReadOnlySpan<char> s, string[] formats, DateTimeFormatInfo dtfi, DateTimeStyles style, out DateTime result, out TimeSpan offset)
		{
			result = DateTime.MinValue;
			offset = TimeSpan.Zero;
			DateTimeResult dateTimeResult = default(DateTimeResult);
			dateTimeResult.Init(s);
			dateTimeResult.flags |= ParseFlags.CaptureOffset;
			if (DateTimeParse.TryParseExactMultiple(s, formats, dtfi, style, ref dateTimeResult))
			{
				result = dateTimeResult.parsedDate;
				offset = dateTimeResult.timeZoneOffset;
				return true;
			}
			return false;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0002A2EC File Offset: 0x000284EC
		internal static bool TryParseExactMultiple(ReadOnlySpan<char> s, string[] formats, DateTimeFormatInfo dtfi, DateTimeStyles style, out DateTime result)
		{
			result = DateTime.MinValue;
			DateTimeResult dateTimeResult = default(DateTimeResult);
			dateTimeResult.Init(s);
			if (DateTimeParse.TryParseExactMultiple(s, formats, dtfi, style, ref dateTimeResult))
			{
				result = dateTimeResult.parsedDate;
				return true;
			}
			return false;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0002A334 File Offset: 0x00028534
		internal static bool TryParseExactMultiple(ReadOnlySpan<char> s, string[] formats, DateTimeFormatInfo dtfi, DateTimeStyles style, ref DateTimeResult result)
		{
			if (formats == null)
			{
				result.SetFailure(ParseFailureKind.ArgumentNull, "String reference not set to an instance of a String.", null, "formats");
				return false;
			}
			if (s.Length == 0)
			{
				result.SetFailure(ParseFailureKind.FormatWithParameter, "String was not recognized as a valid DateTime.");
				return false;
			}
			if (formats.Length == 0)
			{
				result.SetFailure(ParseFailureKind.Format, "Format_NoFormatSpecifier");
				return false;
			}
			for (int i = 0; i < formats.Length; i++)
			{
				if (formats[i] == null || formats[i].Length == 0)
				{
					result.SetBadFormatSpecifierFailure();
					return false;
				}
				DateTimeResult dateTimeResult = default(DateTimeResult);
				dateTimeResult.Init(s);
				dateTimeResult.flags = result.flags;
				if (DateTimeParse.TryParseExact(s, formats[i], dtfi, style, ref dateTimeResult))
				{
					result.parsedDate = dateTimeResult.parsedDate;
					result.timeZoneOffset = dateTimeResult.timeZoneOffset;
					return true;
				}
			}
			result.SetBadDateTimeFailure();
			return false;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0002A400 File Offset: 0x00028600
		private unsafe static bool MatchWord(ref __DTString str, string target)
		{
			if (target.Length > str.Value.Length - str.Index)
			{
				return false;
			}
			if (str.CompareInfo.Compare(str.Value.Slice(str.Index, target.Length), target, CompareOptions.IgnoreCase) != 0)
			{
				return false;
			}
			int num = str.Index + target.Length;
			if (num < str.Value.Length && char.IsLetter((char)(*str.Value[num])))
			{
				return false;
			}
			str.Index = num;
			if (str.Index < str.Length)
			{
				str.m_current = (char)(*str.Value[str.Index]);
			}
			return true;
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0002A4B1 File Offset: 0x000286B1
		private static bool GetTimeZoneName(ref __DTString str)
		{
			return DateTimeParse.MatchWord(ref str, "GMT") || DateTimeParse.MatchWord(ref str, "Z");
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0002A4D2 File Offset: 0x000286D2
		internal static bool IsDigit(char ch)
		{
			return ch - '0' <= '\t';
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0002A4E0 File Offset: 0x000286E0
		private static bool ParseFraction(ref __DTString str, out double result)
		{
			result = 0.0;
			double num = 0.1;
			int num2 = 0;
			char current;
			while (str.GetNext() && DateTimeParse.IsDigit(current = str.m_current))
			{
				result += (double)(current - '0') * num;
				num *= 0.1;
				num2++;
			}
			return num2 > 0;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0002A540 File Offset: 0x00028740
		private static bool ParseTimeZone(ref __DTString str, ref TimeSpan result)
		{
			int num = 0;
			DTSubString subString = str.GetSubString();
			if (subString.length != 1)
			{
				return false;
			}
			char c = subString[0];
			if (c != '+' && c != '-')
			{
				return false;
			}
			str.ConsumeSubString(subString);
			subString = str.GetSubString();
			if (subString.type != DTSubStringType.Number)
			{
				return false;
			}
			int value = subString.value;
			int length = subString.length;
			int hours;
			if (length == 1 || length == 2)
			{
				hours = value;
				str.ConsumeSubString(subString);
				subString = str.GetSubString();
				if (subString.length == 1 && subString[0] == ':')
				{
					str.ConsumeSubString(subString);
					subString = str.GetSubString();
					if (subString.type != DTSubStringType.Number || subString.length < 1 || subString.length > 2)
					{
						return false;
					}
					num = subString.value;
					str.ConsumeSubString(subString);
				}
			}
			else
			{
				if (length != 3 && length != 4)
				{
					return false;
				}
				hours = value / 100;
				num = value % 100;
				str.ConsumeSubString(subString);
			}
			if (num < 0 || num >= 60)
			{
				return false;
			}
			result = new TimeSpan(hours, num, 0);
			if (c == '-')
			{
				result = result.Negate();
			}
			return true;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0002A658 File Offset: 0x00028858
		private unsafe static bool HandleTimeZone(ref __DTString str, ref DateTimeResult result)
		{
			if (str.Index < str.Length - 1)
			{
				char c = (char)(*str.Value[str.Index]);
				int num = 0;
				while (char.IsWhiteSpace(c) && str.Index + num < str.Length - 1)
				{
					num++;
					c = (char)(*str.Value[str.Index + num]);
				}
				if (c == '+' || c == '-')
				{
					str.Index += num;
					if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags)0)
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					result.flags |= ParseFlags.TimeZoneUsed;
					if (!DateTimeParse.ParseTimeZone(ref str, ref result.timeZoneOffset))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0002A714 File Offset: 0x00028914
		private unsafe static bool Lex(DateTimeParse.DS dps, ref __DTString str, ref DateTimeToken dtok, ref DateTimeRawInfo raw, ref DateTimeResult result, ref DateTimeFormatInfo dtfi, DateTimeStyles styles)
		{
			dtok.dtt = DateTimeParse.DTT.Unk;
			TokenType tokenType;
			int num;
			str.GetRegularToken(out tokenType, out num, dtfi);
			switch (tokenType)
			{
			case TokenType.NumberToken:
			case TokenType.YearNumberToken:
				if (raw.numCount == 3 || num == -1)
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				if (dps == DateTimeParse.DS.T_NNt && str.Index < str.Length - 1 && *str.Value[str.Index] == 46)
				{
					DateTimeParse.ParseFraction(ref str, out raw.fraction);
				}
				if ((dps == DateTimeParse.DS.T_NNt || dps == DateTimeParse.DS.T_Nt) && str.Index < str.Length - 1 && !DateTimeParse.HandleTimeZone(ref str, ref result))
				{
					return false;
				}
				dtok.num = num;
				if (tokenType != TokenType.YearNumberToken)
				{
					int index;
					char current;
					TokenType separatorToken;
					TokenType tokenType2 = separatorToken = str.GetSeparatorToken(dtfi, out index, out current);
					if (separatorToken > TokenType.SEP_YearSuff)
					{
						if (separatorToken <= TokenType.SEP_HourSuff)
						{
							if (separatorToken == TokenType.SEP_MonthSuff || separatorToken == TokenType.SEP_DaySuff)
							{
								dtok.dtt = DateTimeParse.DTT.NumDatesuff;
								dtok.suffix = tokenType2;
								break;
							}
							if (separatorToken != TokenType.SEP_HourSuff)
							{
								goto IL_59F;
							}
						}
						else if (separatorToken <= TokenType.SEP_SecondSuff)
						{
							if (separatorToken != TokenType.SEP_MinuteSuff && separatorToken != TokenType.SEP_SecondSuff)
							{
								goto IL_59F;
							}
						}
						else
						{
							if (separatorToken == TokenType.SEP_LocalTimeMark)
							{
								dtok.dtt = DateTimeParse.DTT.NumLocalTimeMark;
								raw.AddNumber(dtok.num);
								break;
							}
							if (separatorToken != TokenType.SEP_DateOrOffset)
							{
								goto IL_59F;
							}
							if (DateTimeParse.dateParsingStates[(int)dps][4] == DateTimeParse.DS.ERROR && DateTimeParse.dateParsingStates[(int)dps][3] > DateTimeParse.DS.ERROR)
							{
								str.Index = index;
								str.m_current = current;
								dtok.dtt = DateTimeParse.DTT.NumSpace;
							}
							else
							{
								dtok.dtt = DateTimeParse.DTT.NumDatesep;
							}
							raw.AddNumber(dtok.num);
							break;
						}
						dtok.dtt = DateTimeParse.DTT.NumTimesuff;
						dtok.suffix = tokenType2;
						break;
					}
					if (separatorToken <= TokenType.SEP_Am)
					{
						if (separatorToken == TokenType.SEP_End)
						{
							dtok.dtt = DateTimeParse.DTT.NumEnd;
							raw.AddNumber(dtok.num);
							break;
						}
						if (separatorToken == TokenType.SEP_Space)
						{
							dtok.dtt = DateTimeParse.DTT.NumSpace;
							raw.AddNumber(dtok.num);
							break;
						}
						if (separatorToken != TokenType.SEP_Am)
						{
							goto IL_59F;
						}
					}
					else if (separatorToken <= TokenType.SEP_Date)
					{
						if (separatorToken != TokenType.SEP_Pm)
						{
							if (separatorToken != TokenType.SEP_Date)
							{
								goto IL_59F;
							}
							dtok.dtt = DateTimeParse.DTT.NumDatesep;
							raw.AddNumber(dtok.num);
							break;
						}
					}
					else if (separatorToken != TokenType.SEP_Time)
					{
						if (separatorToken != TokenType.SEP_YearSuff)
						{
							goto IL_59F;
						}
						try
						{
							dtok.num = dtfi.Calendar.ToFourDigitYear(num);
						}
						catch (ArgumentOutOfRangeException)
						{
							result.SetBadDateTimeFailure();
							return false;
						}
						dtok.dtt = DateTimeParse.DTT.NumDatesuff;
						dtok.suffix = tokenType2;
						break;
					}
					else
					{
						if (raw.hasSameDateAndTimeSeparators && (dps == DateTimeParse.DS.D_Y || dps == DateTimeParse.DS.D_YN || dps == DateTimeParse.DS.D_YNd || dps == DateTimeParse.DS.D_YM || dps == DateTimeParse.DS.D_YMd))
						{
							dtok.dtt = DateTimeParse.DTT.NumDatesep;
							raw.AddNumber(dtok.num);
							break;
						}
						dtok.dtt = DateTimeParse.DTT.NumTimesep;
						raw.AddNumber(dtok.num);
						break;
					}
					if (raw.timeMark != DateTimeParse.TM.NotSet)
					{
						result.SetBadDateTimeFailure();
						break;
					}
					raw.timeMark = ((tokenType2 == TokenType.SEP_Am) ? DateTimeParse.TM.AM : DateTimeParse.TM.PM);
					dtok.dtt = DateTimeParse.DTT.NumAmpm;
					if (dps == DateTimeParse.DS.D_NN && !DateTimeParse.ProcessTerminalState(DateTimeParse.DS.DX_NN, ref str, ref result, ref styles, ref raw, dtfi))
					{
						return false;
					}
					raw.AddNumber(dtok.num);
					if ((dps == DateTimeParse.DS.T_NNt || dps == DateTimeParse.DS.T_Nt) && !DateTimeParse.HandleTimeZone(ref str, ref result))
					{
						return false;
					}
					break;
					IL_59F:
					result.SetBadDateTimeFailure();
					return false;
				}
				if (raw.year == -1)
				{
					raw.year = num;
					int index;
					char current;
					TokenType separatorToken;
					TokenType tokenType2 = separatorToken = str.GetSeparatorToken(dtfi, out index, out current);
					if (separatorToken <= TokenType.SEP_Time)
					{
						if (separatorToken <= TokenType.SEP_Am)
						{
							if (separatorToken == TokenType.SEP_End)
							{
								dtok.dtt = DateTimeParse.DTT.YearEnd;
								return true;
							}
							if (separatorToken == TokenType.SEP_Space)
							{
								dtok.dtt = DateTimeParse.DTT.YearSpace;
								return true;
							}
							if (separatorToken != TokenType.SEP_Am)
							{
								goto IL_2B4;
							}
						}
						else if (separatorToken != TokenType.SEP_Pm)
						{
							if (separatorToken == TokenType.SEP_Date)
							{
								dtok.dtt = DateTimeParse.DTT.YearDateSep;
								return true;
							}
							if (separatorToken != TokenType.SEP_Time)
							{
								goto IL_2B4;
							}
							if (!raw.hasSameDateAndTimeSeparators)
							{
								result.SetBadDateTimeFailure();
								return false;
							}
							dtok.dtt = DateTimeParse.DTT.YearDateSep;
							return true;
						}
						if (raw.timeMark == DateTimeParse.TM.NotSet)
						{
							raw.timeMark = ((tokenType2 == TokenType.SEP_Am) ? DateTimeParse.TM.AM : DateTimeParse.TM.PM);
							dtok.dtt = DateTimeParse.DTT.YearSpace;
							return true;
						}
						result.SetBadDateTimeFailure();
						return true;
					}
					else
					{
						if (separatorToken > TokenType.SEP_DaySuff)
						{
							if (separatorToken <= TokenType.SEP_MinuteSuff)
							{
								if (separatorToken != TokenType.SEP_HourSuff && separatorToken != TokenType.SEP_MinuteSuff)
								{
									goto IL_2B4;
								}
							}
							else if (separatorToken != TokenType.SEP_SecondSuff)
							{
								if (separatorToken != TokenType.SEP_DateOrOffset)
								{
									goto IL_2B4;
								}
								if (DateTimeParse.dateParsingStates[(int)dps][13] == DateTimeParse.DS.ERROR && DateTimeParse.dateParsingStates[(int)dps][12] > DateTimeParse.DS.ERROR)
								{
									str.Index = index;
									str.m_current = current;
									dtok.dtt = DateTimeParse.DTT.YearSpace;
									return true;
								}
								dtok.dtt = DateTimeParse.DTT.YearDateSep;
								return true;
							}
							dtok.dtt = DateTimeParse.DTT.NumTimesuff;
							dtok.suffix = tokenType2;
							return true;
						}
						if (separatorToken == TokenType.SEP_YearSuff || separatorToken == TokenType.SEP_MonthSuff || separatorToken == TokenType.SEP_DaySuff)
						{
							dtok.dtt = DateTimeParse.DTT.NumDatesuff;
							dtok.suffix = tokenType2;
							return true;
						}
					}
					IL_2B4:
					result.SetBadDateTimeFailure();
					return false;
				}
				result.SetBadDateTimeFailure();
				return false;
			case TokenType.Am:
			case TokenType.Pm:
				if (raw.timeMark != DateTimeParse.TM.NotSet)
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				raw.timeMark = (DateTimeParse.TM)num;
				break;
			case TokenType.MonthToken:
			{
				if (raw.month != -1)
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				int index;
				char current;
				TokenType separatorToken;
				TokenType tokenType2 = separatorToken = str.GetSeparatorToken(dtfi, out index, out current);
				if (separatorToken <= TokenType.SEP_Space)
				{
					if (separatorToken == TokenType.SEP_End)
					{
						dtok.dtt = DateTimeParse.DTT.MonthEnd;
						goto IL_7F7;
					}
					if (separatorToken == TokenType.SEP_Space)
					{
						dtok.dtt = DateTimeParse.DTT.MonthSpace;
						goto IL_7F7;
					}
				}
				else
				{
					if (separatorToken == TokenType.SEP_Date)
					{
						dtok.dtt = DateTimeParse.DTT.MonthDatesep;
						goto IL_7F7;
					}
					if (separatorToken != TokenType.SEP_Time)
					{
						if (separatorToken == TokenType.SEP_DateOrOffset)
						{
							if (DateTimeParse.dateParsingStates[(int)dps][8] == DateTimeParse.DS.ERROR && DateTimeParse.dateParsingStates[(int)dps][7] > DateTimeParse.DS.ERROR)
							{
								str.Index = index;
								str.m_current = current;
								dtok.dtt = DateTimeParse.DTT.MonthSpace;
								goto IL_7F7;
							}
							dtok.dtt = DateTimeParse.DTT.MonthDatesep;
							goto IL_7F7;
						}
					}
					else
					{
						if (!raw.hasSameDateAndTimeSeparators)
						{
							result.SetBadDateTimeFailure();
							return false;
						}
						dtok.dtt = DateTimeParse.DTT.MonthDatesep;
						goto IL_7F7;
					}
				}
				result.SetBadDateTimeFailure();
				return false;
				IL_7F7:
				raw.month = num;
				break;
			}
			case TokenType.EndOfString:
				dtok.dtt = DateTimeParse.DTT.End;
				break;
			case TokenType.DayOfWeekToken:
				if (raw.dayOfWeek != -1)
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				raw.dayOfWeek = num;
				dtok.dtt = DateTimeParse.DTT.DayOfWeek;
				break;
			case TokenType.TimeZoneToken:
				if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags)0)
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				dtok.dtt = DateTimeParse.DTT.TimeZone;
				result.flags |= ParseFlags.TimeZoneUsed;
				result.timeZoneOffset = new TimeSpan(0L);
				result.flags |= ParseFlags.TimeZoneUtc;
				break;
			case TokenType.EraToken:
				if (result.era == -1)
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				result.era = num;
				dtok.dtt = DateTimeParse.DTT.Era;
				break;
			case TokenType.UnknownToken:
				if (char.IsLetter(str.m_current))
				{
					result.SetFailure(ParseFailureKind.FormatWithOriginalDateTimeAndParameter, "Format_UnknownDateTimeWord", str.Index);
					return false;
				}
				if ((str.m_current == '-' || str.m_current == '+') && (result.flags & ParseFlags.TimeZoneUsed) == (ParseFlags)0)
				{
					int index2 = str.Index;
					if (DateTimeParse.ParseTimeZone(ref str, ref result.timeZoneOffset))
					{
						result.flags |= ParseFlags.TimeZoneUsed;
						return true;
					}
					str.Index = index2;
				}
				if (DateTimeParse.VerifyValidPunctuation(ref str))
				{
					return true;
				}
				result.SetBadDateTimeFailure();
				return false;
			case TokenType.HebrewNumber:
			{
				int index;
				char current;
				TokenType separatorToken;
				TokenType tokenType2;
				if (num < 100)
				{
					dtok.num = num;
					raw.AddNumber(dtok.num);
					tokenType2 = (separatorToken = str.GetSeparatorToken(dtfi, out index, out current));
					if (separatorToken <= TokenType.SEP_Space)
					{
						if (separatorToken == TokenType.SEP_End)
						{
							dtok.dtt = DateTimeParse.DTT.NumEnd;
							break;
						}
						if (separatorToken != TokenType.SEP_Space)
						{
							goto IL_6F5;
						}
					}
					else if (separatorToken != TokenType.SEP_Date)
					{
						if (separatorToken != TokenType.SEP_DateOrOffset)
						{
							goto IL_6F5;
						}
						if (DateTimeParse.dateParsingStates[(int)dps][4] == DateTimeParse.DS.ERROR && DateTimeParse.dateParsingStates[(int)dps][3] > DateTimeParse.DS.ERROR)
						{
							str.Index = index;
							str.m_current = current;
							dtok.dtt = DateTimeParse.DTT.NumSpace;
							break;
						}
						dtok.dtt = DateTimeParse.DTT.NumDatesep;
						break;
					}
					dtok.dtt = DateTimeParse.DTT.NumDatesep;
					break;
					IL_6F5:
					result.SetBadDateTimeFailure();
					return false;
				}
				if (raw.year != -1)
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				raw.year = num;
				tokenType2 = (separatorToken = str.GetSeparatorToken(dtfi, out index, out current));
				if (separatorToken != TokenType.SEP_End)
				{
					if (separatorToken != TokenType.SEP_Space)
					{
						if (separatorToken == TokenType.SEP_DateOrOffset)
						{
							if (DateTimeParse.dateParsingStates[(int)dps][12] > DateTimeParse.DS.ERROR)
							{
								str.Index = index;
								str.m_current = current;
								dtok.dtt = DateTimeParse.DTT.YearSpace;
								break;
							}
						}
						result.SetBadDateTimeFailure();
						return false;
					}
					dtok.dtt = DateTimeParse.DTT.YearSpace;
				}
				else
				{
					dtok.dtt = DateTimeParse.DTT.YearEnd;
				}
				break;
			}
			case TokenType.JapaneseEraToken:
				if (GlobalizationMode.Invariant)
				{
					throw new PlatformNotSupportedException();
				}
				result.calendar = DateTimeParse.GetJapaneseCalendarDefaultInstance();
				dtfi = DateTimeFormatInfo.GetJapaneseCalendarDTFI();
				if (result.era == -1)
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				result.era = num;
				dtok.dtt = DateTimeParse.DTT.Era;
				break;
			case TokenType.TEraToken:
				if (GlobalizationMode.Invariant)
				{
					throw new PlatformNotSupportedException();
				}
				result.calendar = DateTimeParse.GetTaiwanCalendarDefaultInstance();
				dtfi = DateTimeFormatInfo.GetTaiwanCalendarDTFI();
				if (result.era == -1)
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				result.era = num;
				dtok.dtt = DateTimeParse.DTT.Era;
				break;
			}
			return true;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0002B108 File Offset: 0x00029308
		private static Calendar GetJapaneseCalendarDefaultInstance()
		{
			if (GlobalizationMode.Invariant)
			{
				throw new PlatformNotSupportedException();
			}
			return JapaneseCalendar.GetDefaultInstance();
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0002B11C File Offset: 0x0002931C
		internal static Calendar GetTaiwanCalendarDefaultInstance()
		{
			if (GlobalizationMode.Invariant)
			{
				throw new PlatformNotSupportedException();
			}
			return TaiwanCalendar.GetDefaultInstance();
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0002B130 File Offset: 0x00029330
		private unsafe static bool VerifyValidPunctuation(ref __DTString str)
		{
			char c = (char)(*str.Value[str.Index]);
			if (c == '#')
			{
				bool flag = false;
				bool flag2 = false;
				for (int i = 0; i < str.Length; i++)
				{
					c = (char)(*str.Value[i]);
					if (c == '#')
					{
						if (flag)
						{
							if (flag2)
							{
								return false;
							}
							flag2 = true;
						}
						else
						{
							flag = true;
						}
					}
					else if (c == '\0')
					{
						if (!flag2)
						{
							return false;
						}
					}
					else if (!char.IsWhiteSpace(c) && (!flag || flag2))
					{
						return false;
					}
				}
				if (!flag2)
				{
					return false;
				}
				str.GetNext();
				return true;
			}
			else
			{
				if (c == '\0')
				{
					for (int j = str.Index; j < str.Length; j++)
					{
						if (*str.Value[j] != 0)
						{
							return false;
						}
					}
					str.Index = str.Length;
					return true;
				}
				return false;
			}
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0002B1F4 File Offset: 0x000293F4
		private static bool GetYearMonthDayOrder(string datePattern, DateTimeFormatInfo dtfi, out int order)
		{
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			int num4 = 0;
			bool flag = false;
			int num5 = 0;
			while (num5 < datePattern.Length && num4 < 3)
			{
				char c = datePattern[num5];
				if (c == '\\' || c == '%')
				{
					num5++;
				}
				else
				{
					if (c == '\'' || c == '"')
					{
						flag = !flag;
					}
					if (!flag)
					{
						if (c == 'y')
						{
							num = num4++;
							while (num5 + 1 < datePattern.Length)
							{
								if (datePattern[num5 + 1] != 'y')
								{
									break;
								}
								num5++;
							}
						}
						else if (c == 'M')
						{
							num2 = num4++;
							while (num5 + 1 < datePattern.Length)
							{
								if (datePattern[num5 + 1] != 'M')
								{
									break;
								}
								num5++;
							}
						}
						else if (c == 'd')
						{
							int num6 = 1;
							while (num5 + 1 < datePattern.Length && datePattern[num5 + 1] == 'd')
							{
								num6++;
								num5++;
							}
							if (num6 <= 2)
							{
								num3 = num4++;
							}
						}
					}
				}
				num5++;
			}
			if (num == 0 && num2 == 1 && num3 == 2)
			{
				order = 0;
				return true;
			}
			if (num2 == 0 && num3 == 1 && num == 2)
			{
				order = 1;
				return true;
			}
			if (num3 == 0 && num2 == 1 && num == 2)
			{
				order = 2;
				return true;
			}
			if (num == 0 && num3 == 1 && num2 == 2)
			{
				order = 3;
				return true;
			}
			order = -1;
			return false;
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0002B348 File Offset: 0x00029548
		private static bool GetYearMonthOrder(string pattern, DateTimeFormatInfo dtfi, out int order)
		{
			int num = -1;
			int num2 = -1;
			int num3 = 0;
			bool flag = false;
			int num4 = 0;
			while (num4 < pattern.Length && num3 < 2)
			{
				char c = pattern[num4];
				if (c == '\\' || c == '%')
				{
					num4++;
				}
				else
				{
					if (c == '\'' || c == '"')
					{
						flag = !flag;
					}
					if (!flag)
					{
						if (c == 'y')
						{
							num = num3++;
							while (num4 + 1 < pattern.Length)
							{
								if (pattern[num4 + 1] != 'y')
								{
									break;
								}
								num4++;
							}
						}
						else if (c == 'M')
						{
							num2 = num3++;
							while (num4 + 1 < pattern.Length && pattern[num4 + 1] == 'M')
							{
								num4++;
							}
						}
					}
				}
				num4++;
			}
			if (num == 0 && num2 == 1)
			{
				order = 4;
				return true;
			}
			if (num2 == 0 && num == 1)
			{
				order = 5;
				return true;
			}
			order = -1;
			return false;
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0002B428 File Offset: 0x00029628
		private static bool GetMonthDayOrder(string pattern, DateTimeFormatInfo dtfi, out int order)
		{
			int num = -1;
			int num2 = -1;
			int num3 = 0;
			bool flag = false;
			int num4 = 0;
			while (num4 < pattern.Length && num3 < 2)
			{
				char c = pattern[num4];
				if (c == '\\' || c == '%')
				{
					num4++;
				}
				else
				{
					if (c == '\'' || c == '"')
					{
						flag = !flag;
					}
					if (!flag)
					{
						if (c == 'd')
						{
							int num5 = 1;
							while (num4 + 1 < pattern.Length && pattern[num4 + 1] == 'd')
							{
								num5++;
								num4++;
							}
							if (num5 <= 2)
							{
								num2 = num3++;
							}
						}
						else if (c == 'M')
						{
							num = num3++;
							while (num4 + 1 < pattern.Length && pattern[num4 + 1] == 'M')
							{
								num4++;
							}
						}
					}
				}
				num4++;
			}
			if (num == 0 && num2 == 1)
			{
				order = 6;
				return true;
			}
			if (num2 == 0 && num == 1)
			{
				order = 7;
				return true;
			}
			order = -1;
			return false;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0002B51C File Offset: 0x0002971C
		private static bool TryAdjustYear(ref DateTimeResult result, int year, out int adjustedYear)
		{
			if (year < 100)
			{
				try
				{
					year = result.calendar.ToFourDigitYear(year);
				}
				catch (ArgumentOutOfRangeException)
				{
					adjustedYear = -1;
					return false;
				}
			}
			adjustedYear = year;
			return true;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0002B55C File Offset: 0x0002975C
		private static bool SetDateYMD(ref DateTimeResult result, int year, int month, int day)
		{
			if (result.calendar.IsValidDay(year, month, day, result.era))
			{
				result.SetDate(year, month, day);
				return true;
			}
			return false;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0002B580 File Offset: 0x00029780
		private static bool SetDateMDY(ref DateTimeResult result, int month, int day, int year)
		{
			return DateTimeParse.SetDateYMD(ref result, year, month, day);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0002B58B File Offset: 0x0002978B
		private static bool SetDateDMY(ref DateTimeResult result, int day, int month, int year)
		{
			return DateTimeParse.SetDateYMD(ref result, year, month, day);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0002B596 File Offset: 0x00029796
		private static bool SetDateYDM(ref DateTimeResult result, int year, int day, int month)
		{
			return DateTimeParse.SetDateYMD(ref result, year, month, day);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0002B5A1 File Offset: 0x000297A1
		private static void GetDefaultYear(ref DateTimeResult result, ref DateTimeStyles styles)
		{
			result.Year = result.calendar.GetYear(DateTimeParse.GetDateTimeNow(ref result, ref styles));
			result.flags |= ParseFlags.YearDefault;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0002B5CC File Offset: 0x000297CC
		private static bool GetDayOfNN(ref DateTimeResult result, ref DateTimeStyles styles, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
		{
			if ((result.flags & ParseFlags.HaveDate) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int number = raw.GetNumber(0);
			int number2 = raw.GetNumber(1);
			DateTimeParse.GetDefaultYear(ref result, ref styles);
			int num;
			if (!DateTimeParse.GetMonthDayOrder(dtfi.MonthDayPattern, dtfi, out num))
			{
				result.SetFailure(ParseFailureKind.FormatWithParameter, "Could not determine the order of year, month, and date from '{0}'.", dtfi.MonthDayPattern);
				return false;
			}
			if (num == 6)
			{
				if (DateTimeParse.SetDateYMD(ref result, result.Year, number, number2))
				{
					result.flags |= ParseFlags.HaveDate;
					return true;
				}
			}
			else if (DateTimeParse.SetDateYMD(ref result, result.Year, number2, number))
			{
				result.flags |= ParseFlags.HaveDate;
				return true;
			}
			result.SetBadDateTimeFailure();
			return false;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0002B678 File Offset: 0x00029878
		private static bool GetDayOfNNN(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
		{
			if ((result.flags & ParseFlags.HaveDate) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int number = raw.GetNumber(0);
			int number2 = raw.GetNumber(1);
			int number3 = raw.GetNumber(2);
			int num;
			if (!DateTimeParse.GetYearMonthDayOrder(dtfi.ShortDatePattern, dtfi, out num))
			{
				result.SetFailure(ParseFailureKind.FormatWithParameter, "Could not determine the order of year, month, and date from '{0}'.", dtfi.ShortDatePattern);
				return false;
			}
			int year;
			if (num == 0)
			{
				if (DateTimeParse.TryAdjustYear(ref result, number, out year) && DateTimeParse.SetDateYMD(ref result, year, number2, number3))
				{
					result.flags |= ParseFlags.HaveDate;
					return true;
				}
			}
			else if (num == 1)
			{
				if (DateTimeParse.TryAdjustYear(ref result, number3, out year) && DateTimeParse.SetDateMDY(ref result, number, number2, year))
				{
					result.flags |= ParseFlags.HaveDate;
					return true;
				}
			}
			else if (num == 2)
			{
				if (DateTimeParse.TryAdjustYear(ref result, number3, out year) && DateTimeParse.SetDateDMY(ref result, number, number2, year))
				{
					result.flags |= ParseFlags.HaveDate;
					return true;
				}
			}
			else if (num == 3 && DateTimeParse.TryAdjustYear(ref result, number, out year) && DateTimeParse.SetDateYDM(ref result, year, number2, number3))
			{
				result.flags |= ParseFlags.HaveDate;
				return true;
			}
			result.SetBadDateTimeFailure();
			return false;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0002B794 File Offset: 0x00029994
		private static bool GetDayOfMN(ref DateTimeResult result, ref DateTimeStyles styles, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
		{
			if ((result.flags & ParseFlags.HaveDate) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int num;
			if (!DateTimeParse.GetMonthDayOrder(dtfi.MonthDayPattern, dtfi, out num))
			{
				result.SetFailure(ParseFailureKind.FormatWithParameter, "Could not determine the order of year, month, and date from '{0}'.", dtfi.MonthDayPattern);
				return false;
			}
			if (num == 7)
			{
				int num2;
				if (!DateTimeParse.GetYearMonthOrder(dtfi.YearMonthPattern, dtfi, out num2))
				{
					result.SetFailure(ParseFailureKind.FormatWithParameter, "Could not determine the order of year, month, and date from '{0}'.", dtfi.YearMonthPattern);
					return false;
				}
				if (num2 == 5)
				{
					int year;
					if (!DateTimeParse.TryAdjustYear(ref result, raw.GetNumber(0), out year) || !DateTimeParse.SetDateYMD(ref result, year, raw.month, 1))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					return true;
				}
			}
			DateTimeParse.GetDefaultYear(ref result, ref styles);
			if (!DateTimeParse.SetDateYMD(ref result, result.Year, raw.month, raw.GetNumber(0)))
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			return true;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0002B860 File Offset: 0x00029A60
		private static bool GetHebrewDayOfNM(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
		{
			int num;
			if (!DateTimeParse.GetMonthDayOrder(dtfi.MonthDayPattern, dtfi, out num))
			{
				result.SetFailure(ParseFailureKind.FormatWithParameter, "Could not determine the order of year, month, and date from '{0}'.", dtfi.MonthDayPattern);
				return false;
			}
			result.Month = raw.month;
			if ((num == 7 || num == 6) && result.calendar.IsValidDay(result.Year, result.Month, raw.GetNumber(0), result.era))
			{
				result.Day = raw.GetNumber(0);
				return true;
			}
			result.SetBadDateTimeFailure();
			return false;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0002B8E4 File Offset: 0x00029AE4
		private static bool GetDayOfNM(ref DateTimeResult result, ref DateTimeStyles styles, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
		{
			if ((result.flags & ParseFlags.HaveDate) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int num;
			if (!DateTimeParse.GetMonthDayOrder(dtfi.MonthDayPattern, dtfi, out num))
			{
				result.SetFailure(ParseFailureKind.FormatWithParameter, "Could not determine the order of year, month, and date from '{0}'.", dtfi.MonthDayPattern);
				return false;
			}
			if (num == 6)
			{
				int num2;
				if (!DateTimeParse.GetYearMonthOrder(dtfi.YearMonthPattern, dtfi, out num2))
				{
					result.SetFailure(ParseFailureKind.FormatWithParameter, "Could not determine the order of year, month, and date from '{0}'.", dtfi.YearMonthPattern);
					return false;
				}
				if (num2 == 4)
				{
					int year;
					if (!DateTimeParse.TryAdjustYear(ref result, raw.GetNumber(0), out year) || !DateTimeParse.SetDateYMD(ref result, year, raw.month, 1))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					return true;
				}
			}
			DateTimeParse.GetDefaultYear(ref result, ref styles);
			if (!DateTimeParse.SetDateYMD(ref result, result.Year, raw.month, raw.GetNumber(0)))
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			return true;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0002B9B0 File Offset: 0x00029BB0
		private static bool GetDayOfMNN(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
		{
			if ((result.flags & ParseFlags.HaveDate) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int number = raw.GetNumber(0);
			int number2 = raw.GetNumber(1);
			int num;
			if (!DateTimeParse.GetYearMonthDayOrder(dtfi.ShortDatePattern, dtfi, out num))
			{
				result.SetFailure(ParseFailureKind.FormatWithParameter, "Could not determine the order of year, month, and date from '{0}'.", dtfi.ShortDatePattern);
				return false;
			}
			if (num == 1)
			{
				int year;
				if (DateTimeParse.TryAdjustYear(ref result, number2, out year) && result.calendar.IsValidDay(year, raw.month, number, result.era))
				{
					result.SetDate(year, raw.month, number);
					result.flags |= ParseFlags.HaveDate;
					return true;
				}
				if (DateTimeParse.TryAdjustYear(ref result, number, out year) && result.calendar.IsValidDay(year, raw.month, number2, result.era))
				{
					result.SetDate(year, raw.month, number2);
					result.flags |= ParseFlags.HaveDate;
					return true;
				}
			}
			else if (num == 0)
			{
				int year;
				if (DateTimeParse.TryAdjustYear(ref result, number, out year) && result.calendar.IsValidDay(year, raw.month, number2, result.era))
				{
					result.SetDate(year, raw.month, number2);
					result.flags |= ParseFlags.HaveDate;
					return true;
				}
				if (DateTimeParse.TryAdjustYear(ref result, number2, out year) && result.calendar.IsValidDay(year, raw.month, number, result.era))
				{
					result.SetDate(year, raw.month, number);
					result.flags |= ParseFlags.HaveDate;
					return true;
				}
			}
			else if (num == 2)
			{
				int year;
				if (DateTimeParse.TryAdjustYear(ref result, number2, out year) && result.calendar.IsValidDay(year, raw.month, number, result.era))
				{
					result.SetDate(year, raw.month, number);
					result.flags |= ParseFlags.HaveDate;
					return true;
				}
				if (DateTimeParse.TryAdjustYear(ref result, number, out year) && result.calendar.IsValidDay(year, raw.month, number2, result.era))
				{
					result.SetDate(year, raw.month, number2);
					result.flags |= ParseFlags.HaveDate;
					return true;
				}
			}
			result.SetBadDateTimeFailure();
			return false;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0002BBCC File Offset: 0x00029DCC
		private static bool GetDayOfYNN(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
		{
			if ((result.flags & ParseFlags.HaveDate) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int number = raw.GetNumber(0);
			int number2 = raw.GetNumber(1);
			int num;
			if (DateTimeParse.GetYearMonthDayOrder(dtfi.ShortDatePattern, dtfi, out num) && num == 3)
			{
				if (DateTimeParse.SetDateYMD(ref result, raw.year, number2, number))
				{
					result.flags |= ParseFlags.HaveDate;
					return true;
				}
			}
			else if (DateTimeParse.SetDateYMD(ref result, raw.year, number, number2))
			{
				result.flags |= ParseFlags.HaveDate;
				return true;
			}
			result.SetBadDateTimeFailure();
			return false;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0002BC5C File Offset: 0x00029E5C
		private static bool GetDayOfNNY(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
		{
			if ((result.flags & ParseFlags.HaveDate) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int number = raw.GetNumber(0);
			int number2 = raw.GetNumber(1);
			int num;
			if (!DateTimeParse.GetYearMonthDayOrder(dtfi.ShortDatePattern, dtfi, out num))
			{
				result.SetFailure(ParseFailureKind.FormatWithParameter, "Could not determine the order of year, month, and date from '{0}'.", dtfi.ShortDatePattern);
				return false;
			}
			if (num == 1 || num == 0)
			{
				if (DateTimeParse.SetDateYMD(ref result, raw.year, number, number2))
				{
					result.flags |= ParseFlags.HaveDate;
					return true;
				}
			}
			else if (DateTimeParse.SetDateYMD(ref result, raw.year, number2, number))
			{
				result.flags |= ParseFlags.HaveDate;
				return true;
			}
			result.SetBadDateTimeFailure();
			return false;
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0002BD04 File Offset: 0x00029F04
		private static bool GetDayOfYMN(ref DateTimeResult result, ref DateTimeRawInfo raw)
		{
			if ((result.flags & ParseFlags.HaveDate) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			if (DateTimeParse.SetDateYMD(ref result, raw.year, raw.month, raw.GetNumber(0)))
			{
				result.flags |= ParseFlags.HaveDate;
				return true;
			}
			result.SetBadDateTimeFailure();
			return false;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0002BD5C File Offset: 0x00029F5C
		private static bool GetDayOfYN(ref DateTimeResult result, ref DateTimeRawInfo raw)
		{
			if ((result.flags & ParseFlags.HaveDate) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			if (DateTimeParse.SetDateYMD(ref result, raw.year, raw.GetNumber(0), 1))
			{
				result.flags |= ParseFlags.HaveDate;
				return true;
			}
			result.SetBadDateTimeFailure();
			return false;
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0002BDB0 File Offset: 0x00029FB0
		private static bool GetDayOfYM(ref DateTimeResult result, ref DateTimeRawInfo raw)
		{
			if ((result.flags & ParseFlags.HaveDate) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			if (DateTimeParse.SetDateYMD(ref result, raw.year, raw.month, 1))
			{
				result.flags |= ParseFlags.HaveDate;
				return true;
			}
			result.SetBadDateTimeFailure();
			return false;
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0002BE00 File Offset: 0x0002A000
		private static void AdjustTimeMark(DateTimeFormatInfo dtfi, ref DateTimeRawInfo raw)
		{
			if (raw.timeMark == DateTimeParse.TM.NotSet && dtfi.AMDesignator != null && dtfi.PMDesignator != null)
			{
				if (dtfi.AMDesignator.Length == 0 && dtfi.PMDesignator.Length != 0)
				{
					raw.timeMark = DateTimeParse.TM.AM;
				}
				if (dtfi.PMDesignator.Length == 0 && dtfi.AMDesignator.Length != 0)
				{
					raw.timeMark = DateTimeParse.TM.PM;
				}
			}
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0002BE68 File Offset: 0x0002A068
		private static bool AdjustHour(ref int hour, DateTimeParse.TM timeMark)
		{
			if (timeMark != DateTimeParse.TM.NotSet)
			{
				if (timeMark == DateTimeParse.TM.AM)
				{
					if (hour < 0 || hour > 12)
					{
						return false;
					}
					hour = ((hour == 12) ? 0 : hour);
				}
				else
				{
					if (hour < 0 || hour > 23)
					{
						return false;
					}
					if (hour < 12)
					{
						hour += 12;
					}
				}
			}
			return true;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0002BEA8 File Offset: 0x0002A0A8
		private static bool GetTimeOfN(ref DateTimeResult result, ref DateTimeRawInfo raw)
		{
			if ((result.flags & ParseFlags.HaveTime) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			if (raw.timeMark == DateTimeParse.TM.NotSet)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			result.Hour = raw.GetNumber(0);
			result.flags |= ParseFlags.HaveTime;
			return true;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0002BEE8 File Offset: 0x0002A0E8
		private static bool GetTimeOfNN(ref DateTimeResult result, ref DateTimeRawInfo raw)
		{
			if ((result.flags & ParseFlags.HaveTime) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			result.Hour = raw.GetNumber(0);
			result.Minute = raw.GetNumber(1);
			result.flags |= ParseFlags.HaveTime;
			return true;
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0002BF24 File Offset: 0x0002A124
		private static bool GetTimeOfNNN(ref DateTimeResult result, ref DateTimeRawInfo raw)
		{
			if ((result.flags & ParseFlags.HaveTime) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			result.Hour = raw.GetNumber(0);
			result.Minute = raw.GetNumber(1);
			result.Second = raw.GetNumber(2);
			result.flags |= ParseFlags.HaveTime;
			return true;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0002BF78 File Offset: 0x0002A178
		private static bool GetDateOfDSN(ref DateTimeResult result, ref DateTimeRawInfo raw)
		{
			if (raw.numCount != 1 || result.Day != -1)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			result.Day = raw.GetNumber(0);
			return true;
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0002BFA4 File Offset: 0x0002A1A4
		private static bool GetDateOfNDS(ref DateTimeResult result, ref DateTimeRawInfo raw)
		{
			if (result.Month == -1)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			if (result.Year != -1)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			if (!DateTimeParse.TryAdjustYear(ref result, raw.GetNumber(0), out result.Year))
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			result.Day = 1;
			return true;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0002BFF8 File Offset: 0x0002A1F8
		private static bool GetDateOfNNDS(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
		{
			if ((result.flags & ParseFlags.HaveYear) != (ParseFlags)0)
			{
				if ((result.flags & ParseFlags.HaveMonth) == (ParseFlags)0 && (result.flags & ParseFlags.HaveDay) == (ParseFlags)0 && DateTimeParse.TryAdjustYear(ref result, raw.year, out result.Year) && DateTimeParse.SetDateYMD(ref result, result.Year, raw.GetNumber(0), raw.GetNumber(1)))
				{
					return true;
				}
			}
			else if ((result.flags & ParseFlags.HaveMonth) != (ParseFlags)0 && (result.flags & ParseFlags.HaveYear) == (ParseFlags)0 && (result.flags & ParseFlags.HaveDay) == (ParseFlags)0)
			{
				int num;
				if (!DateTimeParse.GetYearMonthDayOrder(dtfi.ShortDatePattern, dtfi, out num))
				{
					result.SetFailure(ParseFailureKind.FormatWithParameter, "Could not determine the order of year, month, and date from '{0}'.", dtfi.ShortDatePattern);
					return false;
				}
				int year;
				if (num == 0)
				{
					if (DateTimeParse.TryAdjustYear(ref result, raw.GetNumber(0), out year) && DateTimeParse.SetDateYMD(ref result, year, result.Month, raw.GetNumber(1)))
					{
						return true;
					}
				}
				else if (DateTimeParse.TryAdjustYear(ref result, raw.GetNumber(1), out year) && DateTimeParse.SetDateYMD(ref result, year, result.Month, raw.GetNumber(0)))
				{
					return true;
				}
			}
			result.SetBadDateTimeFailure();
			return false;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0002C108 File Offset: 0x0002A308
		private static bool ProcessDateTimeSuffix(ref DateTimeResult result, ref DateTimeRawInfo raw, ref DateTimeToken dtok)
		{
			TokenType suffix = dtok.suffix;
			if (suffix <= TokenType.SEP_DaySuff)
			{
				if (suffix != TokenType.SEP_YearSuff)
				{
					if (suffix != TokenType.SEP_MonthSuff)
					{
						if (suffix == TokenType.SEP_DaySuff)
						{
							if ((result.flags & ParseFlags.HaveDay) != (ParseFlags)0)
							{
								return false;
							}
							result.flags |= ParseFlags.HaveDay;
							result.Day = dtok.num;
						}
					}
					else
					{
						if ((result.flags & ParseFlags.HaveMonth) != (ParseFlags)0)
						{
							return false;
						}
						result.flags |= ParseFlags.HaveMonth;
						result.Month = (raw.month = dtok.num);
					}
				}
				else
				{
					if ((result.flags & ParseFlags.HaveYear) != (ParseFlags)0)
					{
						return false;
					}
					result.flags |= ParseFlags.HaveYear;
					result.Year = (raw.year = dtok.num);
				}
			}
			else if (suffix != TokenType.SEP_HourSuff)
			{
				if (suffix != TokenType.SEP_MinuteSuff)
				{
					if (suffix == TokenType.SEP_SecondSuff)
					{
						if ((result.flags & ParseFlags.HaveSecond) != (ParseFlags)0)
						{
							return false;
						}
						result.flags |= ParseFlags.HaveSecond;
						result.Second = dtok.num;
					}
				}
				else
				{
					if ((result.flags & ParseFlags.HaveMinute) != (ParseFlags)0)
					{
						return false;
					}
					result.flags |= ParseFlags.HaveMinute;
					result.Minute = dtok.num;
				}
			}
			else
			{
				if ((result.flags & ParseFlags.HaveHour) != (ParseFlags)0)
				{
					return false;
				}
				result.flags |= ParseFlags.HaveHour;
				result.Hour = dtok.num;
			}
			return true;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0002C264 File Offset: 0x0002A464
		internal static bool ProcessHebrewTerminalState(DateTimeParse.DS dps, ref __DTString str, ref DateTimeResult result, ref DateTimeStyles styles, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
		{
			switch (dps)
			{
			case DateTimeParse.DS.DX_MN:
			case DateTimeParse.DS.DX_NM:
				DateTimeParse.GetDefaultYear(ref result, ref styles);
				if (!dtfi.YearMonthAdjustment(ref result.Year, ref raw.month, true))
				{
					result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "The DateTime represented by the string is not supported in calendar {0}.");
					return false;
				}
				if (!GlobalizationMode.Invariant && !DateTimeParse.GetHebrewDayOfNM(ref result, ref raw, dtfi))
				{
					return false;
				}
				goto IL_1BC;
			case DateTimeParse.DS.DX_MNN:
				raw.year = raw.GetNumber(1);
				if (!dtfi.YearMonthAdjustment(ref raw.year, ref raw.month, true))
				{
					result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "The DateTime represented by the string is not supported in calendar {0}.");
					return false;
				}
				if (!DateTimeParse.GetDayOfMNN(ref result, ref raw, dtfi))
				{
					return false;
				}
				goto IL_1BC;
			case DateTimeParse.DS.DX_YMN:
				if (!dtfi.YearMonthAdjustment(ref raw.year, ref raw.month, true))
				{
					result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "The DateTime represented by the string is not supported in calendar {0}.");
					return false;
				}
				if (!DateTimeParse.GetDayOfYMN(ref result, ref raw))
				{
					return false;
				}
				goto IL_1BC;
			case DateTimeParse.DS.DX_YM:
				if (!dtfi.YearMonthAdjustment(ref raw.year, ref raw.month, true))
				{
					result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "The DateTime represented by the string is not supported in calendar {0}.");
					return false;
				}
				if (!DateTimeParse.GetDayOfYM(ref result, ref raw))
				{
					return false;
				}
				goto IL_1BC;
			case DateTimeParse.DS.TX_N:
				if (!DateTimeParse.GetTimeOfN(ref result, ref raw))
				{
					return false;
				}
				goto IL_1BC;
			case DateTimeParse.DS.TX_NN:
				if (!DateTimeParse.GetTimeOfNN(ref result, ref raw))
				{
					return false;
				}
				goto IL_1BC;
			case DateTimeParse.DS.TX_NNN:
				if (!DateTimeParse.GetTimeOfNNN(ref result, ref raw))
				{
					return false;
				}
				goto IL_1BC;
			case DateTimeParse.DS.DX_NNY:
				if (raw.year < 1000)
				{
					raw.year += 5000;
				}
				if (!DateTimeParse.GetDayOfNNY(ref result, ref raw, dtfi))
				{
					return false;
				}
				if (!dtfi.YearMonthAdjustment(ref result.Year, ref raw.month, true))
				{
					result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "The DateTime represented by the string is not supported in calendar {0}.");
					return false;
				}
				goto IL_1BC;
			}
			result.SetBadDateTimeFailure();
			return false;
			IL_1BC:
			if (dps > DateTimeParse.DS.ERROR)
			{
				raw.numCount = 0;
			}
			return true;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0002C43C File Offset: 0x0002A63C
		internal static bool ProcessTerminalState(DateTimeParse.DS dps, ref __DTString str, ref DateTimeResult result, ref DateTimeStyles styles, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
		{
			bool flag = true;
			switch (dps)
			{
			case DateTimeParse.DS.DX_NN:
				flag = DateTimeParse.GetDayOfNN(ref result, ref styles, ref raw, dtfi);
				break;
			case DateTimeParse.DS.DX_NNN:
				flag = DateTimeParse.GetDayOfNNN(ref result, ref raw, dtfi);
				break;
			case DateTimeParse.DS.DX_MN:
				flag = DateTimeParse.GetDayOfMN(ref result, ref styles, ref raw, dtfi);
				break;
			case DateTimeParse.DS.DX_NM:
				flag = DateTimeParse.GetDayOfNM(ref result, ref styles, ref raw, dtfi);
				break;
			case DateTimeParse.DS.DX_MNN:
				flag = DateTimeParse.GetDayOfMNN(ref result, ref raw, dtfi);
				break;
			case DateTimeParse.DS.DX_DS:
				flag = true;
				break;
			case DateTimeParse.DS.DX_DSN:
				flag = DateTimeParse.GetDateOfDSN(ref result, ref raw);
				break;
			case DateTimeParse.DS.DX_NDS:
				flag = DateTimeParse.GetDateOfNDS(ref result, ref raw);
				break;
			case DateTimeParse.DS.DX_NNDS:
				flag = DateTimeParse.GetDateOfNNDS(ref result, ref raw, dtfi);
				break;
			case DateTimeParse.DS.DX_YNN:
				flag = DateTimeParse.GetDayOfYNN(ref result, ref raw, dtfi);
				break;
			case DateTimeParse.DS.DX_YMN:
				flag = DateTimeParse.GetDayOfYMN(ref result, ref raw);
				break;
			case DateTimeParse.DS.DX_YN:
				flag = DateTimeParse.GetDayOfYN(ref result, ref raw);
				break;
			case DateTimeParse.DS.DX_YM:
				flag = DateTimeParse.GetDayOfYM(ref result, ref raw);
				break;
			case DateTimeParse.DS.TX_N:
				flag = DateTimeParse.GetTimeOfN(ref result, ref raw);
				break;
			case DateTimeParse.DS.TX_NN:
				flag = DateTimeParse.GetTimeOfNN(ref result, ref raw);
				break;
			case DateTimeParse.DS.TX_NNN:
				flag = DateTimeParse.GetTimeOfNNN(ref result, ref raw);
				break;
			case DateTimeParse.DS.TX_TS:
				flag = true;
				break;
			case DateTimeParse.DS.DX_NNY:
				flag = DateTimeParse.GetDayOfNNY(ref result, ref raw, dtfi);
				break;
			}
			if (!flag)
			{
				return false;
			}
			if (dps > DateTimeParse.DS.ERROR)
			{
				raw.numCount = 0;
			}
			return true;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0002C590 File Offset: 0x0002A790
		internal static DateTime Parse(ReadOnlySpan<char> s, DateTimeFormatInfo dtfi, DateTimeStyles styles)
		{
			DateTimeResult dateTimeResult = default(DateTimeResult);
			dateTimeResult.Init(s);
			if (DateTimeParse.TryParse(s, dtfi, styles, ref dateTimeResult))
			{
				return dateTimeResult.parsedDate;
			}
			throw DateTimeParse.GetDateTimeParseException(ref dateTimeResult);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0002C5C8 File Offset: 0x0002A7C8
		internal static DateTime Parse(ReadOnlySpan<char> s, DateTimeFormatInfo dtfi, DateTimeStyles styles, out TimeSpan offset)
		{
			DateTimeResult dateTimeResult = default(DateTimeResult);
			dateTimeResult.Init(s);
			dateTimeResult.flags |= ParseFlags.CaptureOffset;
			if (DateTimeParse.TryParse(s, dtfi, styles, ref dateTimeResult))
			{
				offset = dateTimeResult.timeZoneOffset;
				return dateTimeResult.parsedDate;
			}
			throw DateTimeParse.GetDateTimeParseException(ref dateTimeResult);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0002C61C File Offset: 0x0002A81C
		internal static bool TryParse(ReadOnlySpan<char> s, DateTimeFormatInfo dtfi, DateTimeStyles styles, out DateTime result)
		{
			result = DateTime.MinValue;
			DateTimeResult dateTimeResult = default(DateTimeResult);
			dateTimeResult.Init(s);
			if (DateTimeParse.TryParse(s, dtfi, styles, ref dateTimeResult))
			{
				result = dateTimeResult.parsedDate;
				return true;
			}
			return false;
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0002C660 File Offset: 0x0002A860
		internal static bool TryParse(ReadOnlySpan<char> s, DateTimeFormatInfo dtfi, DateTimeStyles styles, out DateTime result, out TimeSpan offset)
		{
			result = DateTime.MinValue;
			offset = TimeSpan.Zero;
			DateTimeResult dateTimeResult = default(DateTimeResult);
			dateTimeResult.Init(s);
			dateTimeResult.flags |= ParseFlags.CaptureOffset;
			if (DateTimeParse.TryParse(s, dtfi, styles, ref dateTimeResult))
			{
				result = dateTimeResult.parsedDate;
				offset = dateTimeResult.timeZoneOffset;
				return true;
			}
			return false;
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0002C6CC File Offset: 0x0002A8CC
		internal unsafe static bool TryParse(ReadOnlySpan<char> s, DateTimeFormatInfo dtfi, DateTimeStyles styles, ref DateTimeResult result)
		{
			if (s.Length == 0)
			{
				result.SetFailure(ParseFailureKind.FormatWithParameter, "String was not recognized as a valid DateTime.");
				return false;
			}
			DateTimeParse.DS ds = DateTimeParse.DS.BEGIN;
			bool flag = false;
			DateTimeToken dateTimeToken = default(DateTimeToken);
			dateTimeToken.suffix = TokenType.SEP_Unk;
			DateTimeRawInfo dateTimeRawInfo = default(DateTimeRawInfo);
			int* numberBuffer = stackalloc int[(UIntPtr)12];
			dateTimeRawInfo.Init(numberBuffer);
			dateTimeRawInfo.hasSameDateAndTimeSeparators = dtfi.DateSeparator.Equals(dtfi.TimeSeparator, StringComparison.Ordinal);
			result.calendar = dtfi.Calendar;
			result.era = 0;
			__DTString _DTString = new __DTString(s, dtfi);
			_DTString.GetNext();
			while (DateTimeParse.Lex(ds, ref _DTString, ref dateTimeToken, ref dateTimeRawInfo, ref result, ref dtfi, styles))
			{
				if (dateTimeToken.dtt != DateTimeParse.DTT.Unk)
				{
					if (dateTimeToken.suffix != TokenType.SEP_Unk)
					{
						if (!DateTimeParse.ProcessDateTimeSuffix(ref result, ref dateTimeRawInfo, ref dateTimeToken))
						{
							result.SetBadDateTimeFailure();
							return false;
						}
						dateTimeToken.suffix = TokenType.SEP_Unk;
					}
					if (dateTimeToken.dtt == DateTimeParse.DTT.NumLocalTimeMark)
					{
						if (ds == DateTimeParse.DS.D_YNd || ds == DateTimeParse.DS.D_YN)
						{
							return DateTimeParse.ParseISO8601(ref dateTimeRawInfo, ref _DTString, styles, ref result);
						}
						result.SetBadDateTimeFailure();
						return false;
					}
					else
					{
						if (dateTimeRawInfo.hasSameDateAndTimeSeparators)
						{
							if (dateTimeToken.dtt == DateTimeParse.DTT.YearEnd || dateTimeToken.dtt == DateTimeParse.DTT.YearSpace || dateTimeToken.dtt == DateTimeParse.DTT.YearDateSep)
							{
								if (ds == DateTimeParse.DS.T_Nt)
								{
									ds = DateTimeParse.DS.D_Nd;
								}
								if (ds == DateTimeParse.DS.T_NNt)
								{
									ds = DateTimeParse.DS.D_NNd;
								}
							}
							bool flag2 = _DTString.AtEnd();
							if (DateTimeParse.dateParsingStates[(int)ds][(int)dateTimeToken.dtt] == DateTimeParse.DS.ERROR || flag2)
							{
								DateTimeParse.DTT dtt = dateTimeToken.dtt;
								switch (dtt)
								{
								case DateTimeParse.DTT.NumDatesep:
									dateTimeToken.dtt = (flag2 ? DateTimeParse.DTT.NumEnd : DateTimeParse.DTT.NumSpace);
									break;
								case DateTimeParse.DTT.NumTimesep:
									dateTimeToken.dtt = (flag2 ? DateTimeParse.DTT.NumEnd : DateTimeParse.DTT.NumSpace);
									break;
								case DateTimeParse.DTT.MonthEnd:
								case DateTimeParse.DTT.MonthSpace:
									break;
								case DateTimeParse.DTT.MonthDatesep:
									dateTimeToken.dtt = (flag2 ? DateTimeParse.DTT.MonthEnd : DateTimeParse.DTT.MonthSpace);
									break;
								default:
									if (dtt == DateTimeParse.DTT.YearDateSep)
									{
										dateTimeToken.dtt = (flag2 ? DateTimeParse.DTT.YearEnd : DateTimeParse.DTT.YearSpace);
									}
									break;
								}
							}
						}
						ds = DateTimeParse.dateParsingStates[(int)ds][(int)dateTimeToken.dtt];
						if (ds == DateTimeParse.DS.ERROR)
						{
							result.SetBadDateTimeFailure();
							return false;
						}
						if (ds > DateTimeParse.DS.ERROR)
						{
							if ((dtfi.FormatFlags & DateTimeFormatFlags.UseHebrewRule) != DateTimeFormatFlags.None)
							{
								if (!GlobalizationMode.Invariant && !DateTimeParse.ProcessHebrewTerminalState(ds, ref _DTString, ref result, ref styles, ref dateTimeRawInfo, dtfi))
								{
									return false;
								}
							}
							else if (!DateTimeParse.ProcessTerminalState(ds, ref _DTString, ref result, ref styles, ref dateTimeRawInfo, dtfi))
							{
								return false;
							}
							flag = true;
							ds = DateTimeParse.DS.BEGIN;
						}
					}
				}
				if (dateTimeToken.dtt == DateTimeParse.DTT.End || dateTimeToken.dtt == DateTimeParse.DTT.NumEnd || dateTimeToken.dtt == DateTimeParse.DTT.MonthEnd)
				{
					if (!flag)
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					DateTimeParse.AdjustTimeMark(dtfi, ref dateTimeRawInfo);
					if (!DateTimeParse.AdjustHour(ref result.Hour, dateTimeRawInfo.timeMark))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					bool bTimeOnly = result.Year == -1 && result.Month == -1 && result.Day == -1;
					if (!DateTimeParse.CheckDefaultDateTime(ref result, ref result.calendar, styles))
					{
						return false;
					}
					DateTime dateTime;
					if (!result.calendar.TryToDateTime(result.Year, result.Month, result.Day, result.Hour, result.Minute, result.Second, 0, result.era, out dateTime))
					{
						result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "The DateTime represented by the string is not supported in calendar {0}.");
						return false;
					}
					if (dateTimeRawInfo.fraction > 0.0)
					{
						dateTime = dateTime.AddTicks((long)Math.Round(dateTimeRawInfo.fraction * 10000000.0));
					}
					if (dateTimeRawInfo.dayOfWeek != -1 && dateTimeRawInfo.dayOfWeek != (int)result.calendar.GetDayOfWeek(dateTime))
					{
						result.SetFailure(ParseFailureKind.FormatWithOriginalDateTime, "String was not recognized as a valid DateTime because the day of week was incorrect.");
						return false;
					}
					result.parsedDate = dateTime;
					return DateTimeParse.DetermineTimeZoneAdjustments(ref _DTString, ref result, styles, bTimeOnly);
				}
			}
			return false;
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0002CA30 File Offset: 0x0002AC30
		private static bool DetermineTimeZoneAdjustments(ref __DTString str, ref DateTimeResult result, DateTimeStyles styles, bool bTimeOnly)
		{
			if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags)0)
			{
				return DateTimeParse.DateTimeOffsetTimeZonePostProcessing(ref str, ref result, styles);
			}
			long ticks = result.timeZoneOffset.Ticks;
			if (ticks < -504000000000L || ticks > 504000000000L)
			{
				result.SetFailure(ParseFailureKind.FormatWithOriginalDateTime, "The time zone offset must be within plus or minus 14 hours.");
				return false;
			}
			if ((result.flags & ParseFlags.TimeZoneUsed) == (ParseFlags)0)
			{
				if ((styles & DateTimeStyles.AssumeLocal) != DateTimeStyles.None)
				{
					if ((styles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.None)
					{
						result.parsedDate = DateTime.SpecifyKind(result.parsedDate, DateTimeKind.Local);
						return true;
					}
					result.flags |= ParseFlags.TimeZoneUsed;
					result.timeZoneOffset = TimeZoneInfo.GetLocalUtcOffset(result.parsedDate, TimeZoneInfoOptions.NoThrowOnInvalidTime);
				}
				else
				{
					if ((styles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.None)
					{
						return true;
					}
					if ((styles & DateTimeStyles.AdjustToUniversal) != DateTimeStyles.None)
					{
						result.parsedDate = DateTime.SpecifyKind(result.parsedDate, DateTimeKind.Utc);
						return true;
					}
					result.flags |= ParseFlags.TimeZoneUsed;
					result.timeZoneOffset = TimeSpan.Zero;
				}
			}
			if ((styles & DateTimeStyles.RoundtripKind) != DateTimeStyles.None && (result.flags & ParseFlags.TimeZoneUtc) != (ParseFlags)0)
			{
				result.parsedDate = DateTime.SpecifyKind(result.parsedDate, DateTimeKind.Utc);
				return true;
			}
			if ((styles & DateTimeStyles.AdjustToUniversal) != DateTimeStyles.None)
			{
				return DateTimeParse.AdjustTimeZoneToUniversal(ref result);
			}
			return DateTimeParse.AdjustTimeZoneToLocal(ref result, bTimeOnly);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0002CB58 File Offset: 0x0002AD58
		private static bool DateTimeOffsetTimeZonePostProcessing(ref __DTString str, ref DateTimeResult result, DateTimeStyles styles)
		{
			if ((result.flags & ParseFlags.TimeZoneUsed) == (ParseFlags)0)
			{
				if ((styles & DateTimeStyles.AssumeUniversal) != DateTimeStyles.None)
				{
					result.timeZoneOffset = TimeSpan.Zero;
				}
				else
				{
					result.timeZoneOffset = TimeZoneInfo.GetLocalUtcOffset(result.parsedDate, TimeZoneInfoOptions.NoThrowOnInvalidTime);
				}
			}
			long ticks = result.timeZoneOffset.Ticks;
			long num = result.parsedDate.Ticks - ticks;
			if (num < 0L || num > 3155378975999999999L)
			{
				result.SetFailure(ParseFailureKind.FormatWithOriginalDateTime, "The UTC representation of the date falls outside the year range 1-9999.");
				return false;
			}
			if (ticks < -504000000000L || ticks > 504000000000L)
			{
				result.SetFailure(ParseFailureKind.FormatWithOriginalDateTime, "The time zone offset must be within plus or minus 14 hours.");
				return false;
			}
			if ((styles & DateTimeStyles.AdjustToUniversal) != DateTimeStyles.None)
			{
				if ((result.flags & ParseFlags.TimeZoneUsed) == (ParseFlags)0 && (styles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.None)
				{
					bool result2 = DateTimeParse.AdjustTimeZoneToUniversal(ref result);
					result.timeZoneOffset = TimeSpan.Zero;
					return result2;
				}
				result.parsedDate = new DateTime(num, DateTimeKind.Utc);
				result.timeZoneOffset = TimeSpan.Zero;
			}
			return true;
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0002CC3C File Offset: 0x0002AE3C
		private static bool AdjustTimeZoneToUniversal(ref DateTimeResult result)
		{
			long num = result.parsedDate.Ticks;
			num -= result.timeZoneOffset.Ticks;
			if (num < 0L)
			{
				num += 864000000000L;
			}
			if (num < 0L || num > 3155378975999999999L)
			{
				result.SetFailure(ParseFailureKind.FormatWithOriginalDateTime, "The DateTime represented by the string is out of range.");
				return false;
			}
			result.parsedDate = new DateTime(num, DateTimeKind.Utc);
			return true;
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0002CCA4 File Offset: 0x0002AEA4
		private static bool AdjustTimeZoneToLocal(ref DateTimeResult result, bool bTimeOnly)
		{
			long num = result.parsedDate.Ticks;
			TimeZoneInfo local = TimeZoneInfo.Local;
			bool isAmbiguousDst = false;
			if (num < 864000000000L)
			{
				num -= result.timeZoneOffset.Ticks;
				num += local.GetUtcOffset(bTimeOnly ? DateTime.Now : result.parsedDate, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
				if (num < 0L)
				{
					num += 864000000000L;
				}
			}
			else
			{
				num -= result.timeZoneOffset.Ticks;
				if (num < 0L || num > 3155378975999999999L)
				{
					num += local.GetUtcOffset(result.parsedDate, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
				}
				else
				{
					DateTime time = new DateTime(num, DateTimeKind.Utc);
					bool flag = false;
					num += TimeZoneInfo.GetUtcOffsetFromUtc(time, TimeZoneInfo.Local, out flag, out isAmbiguousDst).Ticks;
				}
			}
			if (num < 0L || num > 3155378975999999999L)
			{
				result.parsedDate = DateTime.MinValue;
				result.SetFailure(ParseFailureKind.FormatWithOriginalDateTime, "The DateTime represented by the string is out of range.");
				return false;
			}
			result.parsedDate = new DateTime(num, DateTimeKind.Local, isAmbiguousDst);
			return true;
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0002CDB0 File Offset: 0x0002AFB0
		private static bool ParseISO8601(ref DateTimeRawInfo raw, ref __DTString str, DateTimeStyles styles, ref DateTimeResult result)
		{
			if (raw.year >= 0 && raw.GetNumber(0) >= 0)
			{
				raw.GetNumber(1);
			}
			str.Index--;
			int second = 0;
			double num = 0.0;
			str.SkipWhiteSpaces();
			int hour;
			if (!DateTimeParse.ParseDigits(ref str, 2, out hour))
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			str.SkipWhiteSpaces();
			if (!str.Match(':'))
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			str.SkipWhiteSpaces();
			int minute;
			if (!DateTimeParse.ParseDigits(ref str, 2, out minute))
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			str.SkipWhiteSpaces();
			if (str.Match(':'))
			{
				str.SkipWhiteSpaces();
				if (!DateTimeParse.ParseDigits(ref str, 2, out second))
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				if (str.Match('.'))
				{
					if (!DateTimeParse.ParseFraction(ref str, out num))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					str.Index--;
				}
				str.SkipWhiteSpaces();
			}
			if (str.GetNext())
			{
				char @char = str.GetChar();
				if (@char == '+' || @char == '-')
				{
					result.flags |= ParseFlags.TimeZoneUsed;
					if (!DateTimeParse.ParseTimeZone(ref str, ref result.timeZoneOffset))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
				}
				else if (@char == 'Z' || @char == 'z')
				{
					result.flags |= ParseFlags.TimeZoneUsed;
					result.timeZoneOffset = TimeSpan.Zero;
					result.flags |= ParseFlags.TimeZoneUtc;
				}
				else
				{
					str.Index--;
				}
				str.SkipWhiteSpaces();
				if (str.Match('#'))
				{
					if (!DateTimeParse.VerifyValidPunctuation(ref str))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					str.SkipWhiteSpaces();
				}
				if (str.Match('\0') && !DateTimeParse.VerifyValidPunctuation(ref str))
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				if (str.GetNext())
				{
					result.SetBadDateTimeFailure();
					return false;
				}
			}
			DateTime parsedDate;
			if (!GregorianCalendar.GetDefaultInstance().TryToDateTime(raw.year, raw.GetNumber(0), raw.GetNumber(1), hour, minute, second, 0, result.era, out parsedDate))
			{
				result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "The DateTime represented by the string is not supported in calendar {0}.");
				return false;
			}
			parsedDate = parsedDate.AddTicks((long)Math.Round(num * 10000000.0));
			result.parsedDate = parsedDate;
			return DateTimeParse.DetermineTimeZoneAdjustments(ref str, ref result, styles, false);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0002CFD0 File Offset: 0x0002B1D0
		internal static bool MatchHebrewDigits(ref __DTString str, int digitLen, out int number)
		{
			number = 0;
			HebrewNumberParsingContext hebrewNumberParsingContext = new HebrewNumberParsingContext(0);
			HebrewNumberParsingState hebrewNumberParsingState = HebrewNumberParsingState.ContinueParsing;
			while (hebrewNumberParsingState == HebrewNumberParsingState.ContinueParsing && str.GetNext())
			{
				hebrewNumberParsingState = HebrewNumber.ParseByChar(str.GetChar(), ref hebrewNumberParsingContext);
			}
			if (hebrewNumberParsingState == HebrewNumberParsingState.FoundEndOfHebrewNumber)
			{
				number = hebrewNumberParsingContext.result;
				return true;
			}
			return false;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0002D015 File Offset: 0x0002B215
		internal static bool ParseDigits(ref __DTString str, int digitLen, out int result)
		{
			if (digitLen == 1)
			{
				return DateTimeParse.ParseDigits(ref str, 1, 2, out result);
			}
			return DateTimeParse.ParseDigits(ref str, digitLen, digitLen, out result);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0002D030 File Offset: 0x0002B230
		internal static bool ParseDigits(ref __DTString str, int minDigitLen, int maxDigitLen, out int result)
		{
			int num = 0;
			int index = str.Index;
			int i;
			for (i = 0; i < maxDigitLen; i++)
			{
				if (!str.GetNextDigit())
				{
					str.Index--;
					break;
				}
				num = num * 10 + str.GetDigit();
			}
			result = num;
			if (i < minDigitLen)
			{
				str.Index = index;
				return false;
			}
			return true;
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0002D084 File Offset: 0x0002B284
		private static bool ParseFractionExact(ref __DTString str, int maxDigitLen, ref double result)
		{
			if (!str.GetNextDigit())
			{
				str.Index--;
				return false;
			}
			result = (double)str.GetDigit();
			int i;
			for (i = 1; i < maxDigitLen; i++)
			{
				if (!str.GetNextDigit())
				{
					str.Index--;
					break;
				}
				result = result * 10.0 + (double)str.GetDigit();
			}
			result /= (double)TimeSpanParse.Pow10(i);
			return i == maxDigitLen;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0002D0F8 File Offset: 0x0002B2F8
		private static bool ParseSign(ref __DTString str, ref bool result)
		{
			if (!str.GetNext())
			{
				return false;
			}
			char @char = str.GetChar();
			if (@char == '+')
			{
				result = true;
				return true;
			}
			if (@char == '-')
			{
				result = false;
				return true;
			}
			return false;
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0002D12C File Offset: 0x0002B32C
		private static bool ParseTimeZoneOffset(ref __DTString str, int len, ref TimeSpan result)
		{
			bool flag = true;
			int num = 0;
			int hours;
			if (len - 1 <= 1)
			{
				if (!DateTimeParse.ParseSign(ref str, ref flag))
				{
					return false;
				}
				if (!DateTimeParse.ParseDigits(ref str, len, out hours))
				{
					return false;
				}
			}
			else
			{
				if (!DateTimeParse.ParseSign(ref str, ref flag))
				{
					return false;
				}
				if (!DateTimeParse.ParseDigits(ref str, 1, out hours))
				{
					return false;
				}
				if (str.Match(":"))
				{
					if (!DateTimeParse.ParseDigits(ref str, 2, out num))
					{
						return false;
					}
				}
				else
				{
					str.Index--;
					if (!DateTimeParse.ParseDigits(ref str, 2, out num))
					{
						return false;
					}
				}
			}
			if (num < 0 || num >= 60)
			{
				return false;
			}
			result = new TimeSpan(hours, num, 0);
			if (!flag)
			{
				result = result.Negate();
			}
			return true;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0002D1D0 File Offset: 0x0002B3D0
		private static bool MatchAbbreviatedMonthName(ref __DTString str, DateTimeFormatInfo dtfi, ref int result)
		{
			int num = 0;
			result = -1;
			if (str.GetNext())
			{
				int num2 = (dtfi.GetMonthName(13).Length == 0) ? 12 : 13;
				for (int i = 1; i <= num2; i++)
				{
					string abbreviatedMonthName = dtfi.GetAbbreviatedMonthName(i);
					int length = abbreviatedMonthName.Length;
					if ((dtfi.HasSpacesInMonthNames ? str.MatchSpecifiedWords(abbreviatedMonthName, false, ref length) : str.MatchSpecifiedWord(abbreviatedMonthName)) && length > num)
					{
						num = length;
						result = i;
					}
				}
				if ((dtfi.FormatFlags & DateTimeFormatFlags.UseLeapYearMonth) != DateTimeFormatFlags.None)
				{
					int num3 = str.MatchLongestWords(dtfi.internalGetLeapYearMonthNames(), ref num);
					if (num3 >= 0)
					{
						result = num3 + 1;
					}
				}
			}
			if (result > 0)
			{
				str.Index += num - 1;
				return true;
			}
			return false;
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0002D280 File Offset: 0x0002B480
		private static bool MatchMonthName(ref __DTString str, DateTimeFormatInfo dtfi, ref int result)
		{
			int num = 0;
			result = -1;
			if (str.GetNext())
			{
				int num2 = (dtfi.GetMonthName(13).Length == 0) ? 12 : 13;
				for (int i = 1; i <= num2; i++)
				{
					string monthName = dtfi.GetMonthName(i);
					int length = monthName.Length;
					if ((dtfi.HasSpacesInMonthNames ? str.MatchSpecifiedWords(monthName, false, ref length) : str.MatchSpecifiedWord(monthName)) && length > num)
					{
						num = length;
						result = i;
					}
				}
				if ((dtfi.FormatFlags & DateTimeFormatFlags.UseGenitiveMonth) != DateTimeFormatFlags.None)
				{
					int num3 = str.MatchLongestWords(dtfi.MonthGenitiveNames, ref num);
					if (num3 >= 0)
					{
						result = num3 + 1;
					}
				}
				if ((dtfi.FormatFlags & DateTimeFormatFlags.UseLeapYearMonth) != DateTimeFormatFlags.None)
				{
					int num4 = str.MatchLongestWords(dtfi.internalGetLeapYearMonthNames(), ref num);
					if (num4 >= 0)
					{
						result = num4 + 1;
					}
				}
			}
			if (result > 0)
			{
				str.Index += num - 1;
				return true;
			}
			return false;
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0002D358 File Offset: 0x0002B558
		private static bool MatchAbbreviatedDayName(ref __DTString str, DateTimeFormatInfo dtfi, ref int result)
		{
			int num = 0;
			result = -1;
			if (str.GetNext())
			{
				for (DayOfWeek dayOfWeek = DayOfWeek.Sunday; dayOfWeek <= DayOfWeek.Saturday; dayOfWeek++)
				{
					string abbreviatedDayName = dtfi.GetAbbreviatedDayName(dayOfWeek);
					int length = abbreviatedDayName.Length;
					if ((dtfi.HasSpacesInDayNames ? str.MatchSpecifiedWords(abbreviatedDayName, false, ref length) : str.MatchSpecifiedWord(abbreviatedDayName)) && length > num)
					{
						num = length;
						result = (int)dayOfWeek;
					}
				}
			}
			if (result >= 0)
			{
				str.Index += num - 1;
				return true;
			}
			return false;
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0002D3C8 File Offset: 0x0002B5C8
		private static bool MatchDayName(ref __DTString str, DateTimeFormatInfo dtfi, ref int result)
		{
			int num = 0;
			result = -1;
			if (str.GetNext())
			{
				for (DayOfWeek dayOfWeek = DayOfWeek.Sunday; dayOfWeek <= DayOfWeek.Saturday; dayOfWeek++)
				{
					string dayName = dtfi.GetDayName(dayOfWeek);
					int length = dayName.Length;
					if ((dtfi.HasSpacesInDayNames ? str.MatchSpecifiedWords(dayName, false, ref length) : str.MatchSpecifiedWord(dayName)) && length > num)
					{
						num = length;
						result = (int)dayOfWeek;
					}
				}
			}
			if (result >= 0)
			{
				str.Index += num - 1;
				return true;
			}
			return false;
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0002D438 File Offset: 0x0002B638
		private static bool MatchEraName(ref __DTString str, DateTimeFormatInfo dtfi, ref int result)
		{
			if (str.GetNext())
			{
				int[] eras = dtfi.Calendar.Eras;
				if (eras != null)
				{
					for (int i = 0; i < eras.Length; i++)
					{
						string text = dtfi.GetEraName(eras[i]);
						if (str.MatchSpecifiedWord(text))
						{
							str.Index += text.Length - 1;
							result = eras[i];
							return true;
						}
						text = dtfi.GetAbbreviatedEraName(eras[i]);
						if (str.MatchSpecifiedWord(text))
						{
							str.Index += text.Length - 1;
							result = eras[i];
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0002D4C4 File Offset: 0x0002B6C4
		private static bool MatchTimeMark(ref __DTString str, DateTimeFormatInfo dtfi, ref DateTimeParse.TM result)
		{
			result = DateTimeParse.TM.NotSet;
			if (dtfi.AMDesignator.Length == 0)
			{
				result = DateTimeParse.TM.AM;
			}
			if (dtfi.PMDesignator.Length == 0)
			{
				result = DateTimeParse.TM.PM;
			}
			if (str.GetNext())
			{
				string text = dtfi.AMDesignator;
				if (text.Length > 0 && str.MatchSpecifiedWord(text))
				{
					str.Index += text.Length - 1;
					result = DateTimeParse.TM.AM;
					return true;
				}
				text = dtfi.PMDesignator;
				if (text.Length > 0 && str.MatchSpecifiedWord(text))
				{
					str.Index += text.Length - 1;
					result = DateTimeParse.TM.PM;
					return true;
				}
				str.Index--;
			}
			return result != DateTimeParse.TM.NotSet;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0002D570 File Offset: 0x0002B770
		private static bool MatchAbbreviatedTimeMark(ref __DTString str, DateTimeFormatInfo dtfi, ref DateTimeParse.TM result)
		{
			if (str.GetNext())
			{
				string amdesignator = dtfi.AMDesignator;
				if (amdesignator.Length > 0 && str.GetChar() == amdesignator[0])
				{
					result = DateTimeParse.TM.AM;
					return true;
				}
				string pmdesignator = dtfi.PMDesignator;
				if (pmdesignator.Length > 0 && str.GetChar() == pmdesignator[0])
				{
					result = DateTimeParse.TM.PM;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0002D5CE File Offset: 0x0002B7CE
		private static bool CheckNewValue(ref int currentValue, int newValue, char patternChar, ref DateTimeResult result)
		{
			if (currentValue == -1)
			{
				currentValue = newValue;
				return true;
			}
			if (newValue != currentValue)
			{
				result.SetFailure(ParseFailureKind.FormatWithParameter, "DateTime pattern '{0}' appears more than once with different values.", patternChar);
				return false;
			}
			return true;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0002D5F4 File Offset: 0x0002B7F4
		private static DateTime GetDateTimeNow(ref DateTimeResult result, ref DateTimeStyles styles)
		{
			if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags)0)
			{
				if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags)0)
				{
					return new DateTime(DateTime.UtcNow.Ticks + result.timeZoneOffset.Ticks, DateTimeKind.Unspecified);
				}
				if ((styles & DateTimeStyles.AssumeUniversal) != DateTimeStyles.None)
				{
					return DateTime.UtcNow;
				}
			}
			return DateTime.Now;
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0002D650 File Offset: 0x0002B850
		private static bool CheckDefaultDateTime(ref DateTimeResult result, ref Calendar cal, DateTimeStyles styles)
		{
			if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags)0 && (result.Month != -1 || result.Day != -1) && (result.Year == -1 || (result.flags & ParseFlags.YearDefault) != (ParseFlags)0) && (result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags)0)
			{
				result.SetFailure(ParseFailureKind.FormatWithOriginalDateTime, "There must be at least a partial date with a year present in the input.");
				return false;
			}
			if (result.Year == -1 || result.Month == -1 || result.Day == -1)
			{
				DateTime dateTimeNow = DateTimeParse.GetDateTimeNow(ref result, ref styles);
				if (result.Month == -1 && result.Day == -1)
				{
					if (result.Year == -1)
					{
						if ((styles & DateTimeStyles.NoCurrentDateDefault) != DateTimeStyles.None)
						{
							cal = GregorianCalendar.GetDefaultInstance();
							result.Year = (result.Month = (result.Day = 1));
						}
						else
						{
							result.Year = cal.GetYear(dateTimeNow);
							result.Month = cal.GetMonth(dateTimeNow);
							result.Day = cal.GetDayOfMonth(dateTimeNow);
						}
					}
					else
					{
						result.Month = 1;
						result.Day = 1;
					}
				}
				else
				{
					if (result.Year == -1)
					{
						result.Year = cal.GetYear(dateTimeNow);
					}
					if (result.Month == -1)
					{
						result.Month = 1;
					}
					if (result.Day == -1)
					{
						result.Day = 1;
					}
				}
			}
			if (result.Hour == -1)
			{
				result.Hour = 0;
			}
			if (result.Minute == -1)
			{
				result.Minute = 0;
			}
			if (result.Second == -1)
			{
				result.Second = 0;
			}
			if (result.era == -1)
			{
				result.era = 0;
			}
			return true;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0002D7D0 File Offset: 0x0002B9D0
		private unsafe static string ExpandPredefinedFormat(ReadOnlySpan<char> format, ref DateTimeFormatInfo dtfi, ref ParsingInfo parseInfo, ref DateTimeResult result)
		{
			char c = (char)(*format[0]);
			if (c <= 'R')
			{
				if (c != 'O')
				{
					if (c != 'R')
					{
						goto IL_153;
					}
					goto IL_67;
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
					goto IL_153;
				case 'r':
					goto IL_67;
				case 's':
					dtfi = DateTimeFormatInfo.InvariantInfo;
					parseInfo.calendar = GregorianCalendar.GetDefaultInstance();
					goto IL_153;
				case 'u':
					parseInfo.calendar = GregorianCalendar.GetDefaultInstance();
					dtfi = DateTimeFormatInfo.InvariantInfo;
					if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags)0)
					{
						result.flags |= ParseFlags.UtcSortPattern;
						goto IL_153;
					}
					goto IL_153;
				default:
					goto IL_153;
				}
			}
			else
			{
				parseInfo.calendar = GregorianCalendar.GetDefaultInstance();
				result.flags |= ParseFlags.TimeZoneUsed;
				result.timeZoneOffset = new TimeSpan(0L);
				result.flags |= ParseFlags.TimeZoneUtc;
				if (dtfi.Calendar.GetType() != typeof(GregorianCalendar))
				{
					dtfi = (DateTimeFormatInfo)dtfi.Clone();
					dtfi.Calendar = GregorianCalendar.GetDefaultInstance();
					goto IL_153;
				}
				goto IL_153;
			}
			parseInfo.calendar = GregorianCalendar.GetDefaultInstance();
			dtfi = DateTimeFormatInfo.InvariantInfo;
			goto IL_153;
			IL_67:
			parseInfo.calendar = GregorianCalendar.GetDefaultInstance();
			dtfi = DateTimeFormatInfo.InvariantInfo;
			if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags)0)
			{
				result.flags |= ParseFlags.Rfc1123Pattern;
			}
			IL_153:
			return DateTimeFormat.GetRealFormat(format, dtfi);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0002D938 File Offset: 0x0002BB38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool ParseJapaneseEraStart(ref __DTString str, DateTimeFormatInfo dtfi)
		{
			if (AppContextSwitches.EnforceLegacyJapaneseDateParsing || dtfi.Calendar.ID != 3 || !str.GetNext())
			{
				return false;
			}
			if (str.m_current != "元"[0])
			{
				str.Index--;
				return false;
			}
			return true;
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0002D988 File Offset: 0x0002BB88
		private unsafe static bool ParseByFormat(ref __DTString str, ref __DTString format, ref ParsingInfo parseInfo, DateTimeFormatInfo dtfi, ref DateTimeResult result)
		{
			int num = 0;
			int newValue = 0;
			int newValue2 = 0;
			int newValue3 = 0;
			int newValue4 = 0;
			int newValue5 = 0;
			int newValue6 = 0;
			int newValue7 = 0;
			double num2 = 0.0;
			DateTimeParse.TM tm = DateTimeParse.TM.AM;
			char @char = format.GetChar();
			if (@char <= 'K')
			{
				if (@char <= '.')
				{
					if (@char <= '%')
					{
						if (@char != '"')
						{
							if (@char != '%')
							{
								goto IL_93B;
							}
							if (format.Index >= format.Value.Length - 1 || *format.Value[format.Index + 1] == 37)
							{
								result.SetBadFormatSpecifierFailure(format.Value);
								return false;
							}
							return true;
						}
					}
					else if (@char != '\'')
					{
						if (@char != '.')
						{
							goto IL_93B;
						}
						if (str.Match(@char))
						{
							return true;
						}
						if (format.GetNext() && format.Match('F'))
						{
							format.GetRepeatCount();
							return true;
						}
						result.SetBadDateTimeFailure();
						return false;
					}
					StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
					if (!DateTimeParse.TryParseQuoteString(format.Value, format.Index, stringBuilder, out num))
					{
						result.SetFailure(ParseFailureKind.FormatWithParameter, "Cannot find a matching quote character for the character '{0}'.", @char);
						StringBuilderCache.Release(stringBuilder);
						return false;
					}
					format.Index += num - 1;
					string stringAndRelease = StringBuilderCache.GetStringAndRelease(stringBuilder);
					for (int i = 0; i < stringAndRelease.Length; i++)
					{
						if (stringAndRelease[i] == ' ' && parseInfo.fAllowInnerWhite)
						{
							str.SkipWhiteSpaces();
						}
						else if (!str.Match(stringAndRelease[i]))
						{
							result.SetBadDateTimeFailure();
							return false;
						}
					}
					if ((result.flags & ParseFlags.CaptureOffset) == (ParseFlags)0)
					{
						return true;
					}
					if ((result.flags & ParseFlags.Rfc1123Pattern) != (ParseFlags)0 && stringAndRelease == "GMT")
					{
						result.flags |= ParseFlags.TimeZoneUsed;
						result.timeZoneOffset = TimeSpan.Zero;
						return true;
					}
					if ((result.flags & ParseFlags.UtcSortPattern) != (ParseFlags)0 && stringAndRelease == "Z")
					{
						result.flags |= ParseFlags.TimeZoneUsed;
						result.timeZoneOffset = TimeSpan.Zero;
						return true;
					}
					return true;
				}
				else if (@char <= ':')
				{
					if (@char != '/')
					{
						if (@char != ':')
						{
							goto IL_93B;
						}
						if (((dtfi.TimeSeparator.Length > 1 && dtfi.TimeSeparator[0] == ':') || !str.Match(':')) && !str.Match(dtfi.TimeSeparator))
						{
							result.SetBadDateTimeFailure();
							return false;
						}
						return true;
					}
					else
					{
						if (((dtfi.DateSeparator.Length > 1 && dtfi.DateSeparator[0] == '/') || !str.Match('/')) && !str.Match(dtfi.DateSeparator))
						{
							result.SetBadDateTimeFailure();
							return false;
						}
						return true;
					}
				}
				else if (@char != 'F')
				{
					if (@char != 'H')
					{
						if (@char != 'K')
						{
							goto IL_93B;
						}
						if (str.Match('Z'))
						{
							if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags)0 && result.timeZoneOffset != TimeSpan.Zero)
							{
								result.SetFailure(ParseFailureKind.FormatWithParameter, "DateTime pattern '{0}' appears more than once with different values.", 'K');
								return false;
							}
							result.flags |= ParseFlags.TimeZoneUsed;
							result.timeZoneOffset = new TimeSpan(0L);
							result.flags |= ParseFlags.TimeZoneUtc;
							return true;
						}
						else
						{
							if (!str.Match('+') && !str.Match('-'))
							{
								return true;
							}
							str.Index--;
							TimeSpan timeSpan = new TimeSpan(0L);
							if (!DateTimeParse.ParseTimeZoneOffset(ref str, 3, ref timeSpan))
							{
								result.SetBadDateTimeFailure();
								return false;
							}
							if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags)0 && timeSpan != result.timeZoneOffset)
							{
								result.SetFailure(ParseFailureKind.FormatWithParameter, "DateTime pattern '{0}' appears more than once with different values.", 'K');
								return false;
							}
							result.timeZoneOffset = timeSpan;
							result.flags |= ParseFlags.TimeZoneUsed;
							return true;
						}
					}
					else
					{
						num = format.GetRepeatCount();
						if (!DateTimeParse.ParseDigits(ref str, (num < 2) ? 1 : 2, out newValue5))
						{
							result.SetBadDateTimeFailure();
							return false;
						}
						if (!DateTimeParse.CheckNewValue(ref result.Hour, newValue5, @char, ref result))
						{
							return false;
						}
						return true;
					}
				}
			}
			else if (@char <= 'h')
			{
				if (@char <= 'Z')
				{
					if (@char != 'M')
					{
						if (@char != 'Z')
						{
							goto IL_93B;
						}
						if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags)0 && result.timeZoneOffset != TimeSpan.Zero)
						{
							result.SetFailure(ParseFailureKind.FormatWithParameter, "DateTime pattern '{0}' appears more than once with different values.", 'Z');
							return false;
						}
						result.flags |= ParseFlags.TimeZoneUsed;
						result.timeZoneOffset = new TimeSpan(0L);
						result.flags |= ParseFlags.TimeZoneUtc;
						str.Index++;
						if (!DateTimeParse.GetTimeZoneName(ref str))
						{
							result.SetBadDateTimeFailure();
							return false;
						}
						str.Index--;
						return true;
					}
					else
					{
						num = format.GetRepeatCount();
						if (num <= 2)
						{
							if (!DateTimeParse.ParseDigits(ref str, num, out newValue2) && (!parseInfo.fCustomNumberParser || !parseInfo.parseNumberDelegate(ref str, num, out newValue2)))
							{
								result.SetBadDateTimeFailure();
								return false;
							}
						}
						else
						{
							if (num == 3)
							{
								if (!DateTimeParse.MatchAbbreviatedMonthName(ref str, dtfi, ref newValue2))
								{
									result.SetBadDateTimeFailure();
									return false;
								}
							}
							else if (!DateTimeParse.MatchMonthName(ref str, dtfi, ref newValue2))
							{
								result.SetBadDateTimeFailure();
								return false;
							}
							result.flags |= ParseFlags.ParsedMonthName;
						}
						if (!DateTimeParse.CheckNewValue(ref result.Month, newValue2, @char, ref result))
						{
							return false;
						}
						return true;
					}
				}
				else if (@char != '\\')
				{
					switch (@char)
					{
					case 'd':
						num = format.GetRepeatCount();
						if (num <= 2)
						{
							if (!DateTimeParse.ParseDigits(ref str, num, out newValue3) && (!parseInfo.fCustomNumberParser || !parseInfo.parseNumberDelegate(ref str, num, out newValue3)))
							{
								result.SetBadDateTimeFailure();
								return false;
							}
							if (!DateTimeParse.CheckNewValue(ref result.Day, newValue3, @char, ref result))
							{
								return false;
							}
							return true;
						}
						else
						{
							if (num == 3)
							{
								if (!DateTimeParse.MatchAbbreviatedDayName(ref str, dtfi, ref newValue4))
								{
									result.SetBadDateTimeFailure();
									return false;
								}
							}
							else if (!DateTimeParse.MatchDayName(ref str, dtfi, ref newValue4))
							{
								result.SetBadDateTimeFailure();
								return false;
							}
							if (!DateTimeParse.CheckNewValue(ref parseInfo.dayOfWeek, newValue4, @char, ref result))
							{
								return false;
							}
							return true;
						}
						break;
					case 'e':
						goto IL_93B;
					case 'f':
						break;
					case 'g':
						num = format.GetRepeatCount();
						if (!DateTimeParse.MatchEraName(ref str, dtfi, ref result.era))
						{
							result.SetBadDateTimeFailure();
							return false;
						}
						return true;
					case 'h':
						parseInfo.fUseHour12 = true;
						num = format.GetRepeatCount();
						if (!DateTimeParse.ParseDigits(ref str, (num < 2) ? 1 : 2, out newValue5))
						{
							result.SetBadDateTimeFailure();
							return false;
						}
						if (!DateTimeParse.CheckNewValue(ref result.Hour, newValue5, @char, ref result))
						{
							return false;
						}
						return true;
					default:
						goto IL_93B;
					}
				}
				else
				{
					if (!format.GetNext())
					{
						result.SetBadFormatSpecifierFailure(format.Value);
						return false;
					}
					if (!str.Match(format.GetChar()))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					return true;
				}
			}
			else if (@char <= 's')
			{
				if (@char != 'm')
				{
					if (@char != 's')
					{
						goto IL_93B;
					}
					num = format.GetRepeatCount();
					if (!DateTimeParse.ParseDigits(ref str, (num < 2) ? 1 : 2, out newValue7))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					if (!DateTimeParse.CheckNewValue(ref result.Second, newValue7, @char, ref result))
					{
						return false;
					}
					return true;
				}
				else
				{
					num = format.GetRepeatCount();
					if (!DateTimeParse.ParseDigits(ref str, (num < 2) ? 1 : 2, out newValue6))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					if (!DateTimeParse.CheckNewValue(ref result.Minute, newValue6, @char, ref result))
					{
						return false;
					}
					return true;
				}
			}
			else if (@char != 't')
			{
				if (@char != 'y')
				{
					if (@char != 'z')
					{
						goto IL_93B;
					}
					num = format.GetRepeatCount();
					TimeSpan timeSpan2 = new TimeSpan(0L);
					if (!DateTimeParse.ParseTimeZoneOffset(ref str, num, ref timeSpan2))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags)0 && timeSpan2 != result.timeZoneOffset)
					{
						result.SetFailure(ParseFailureKind.FormatWithParameter, "DateTime pattern '{0}' appears more than once with different values.", 'z');
						return false;
					}
					result.timeZoneOffset = timeSpan2;
					result.flags |= ParseFlags.TimeZoneUsed;
					return true;
				}
				else
				{
					num = format.GetRepeatCount();
					bool flag;
					if (DateTimeParse.ParseJapaneseEraStart(ref str, dtfi))
					{
						newValue = 1;
						flag = true;
					}
					else if (dtfi.HasForceTwoDigitYears)
					{
						flag = DateTimeParse.ParseDigits(ref str, 1, 4, out newValue);
					}
					else
					{
						if (num <= 2)
						{
							parseInfo.fUseTwoDigitYear = true;
						}
						flag = DateTimeParse.ParseDigits(ref str, num, out newValue);
					}
					if (!flag && parseInfo.fCustomNumberParser)
					{
						flag = parseInfo.parseNumberDelegate(ref str, num, out newValue);
					}
					if (!flag)
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					if (!DateTimeParse.CheckNewValue(ref result.Year, newValue, @char, ref result))
					{
						return false;
					}
					return true;
				}
			}
			else
			{
				num = format.GetRepeatCount();
				if (num == 1)
				{
					if (!DateTimeParse.MatchAbbreviatedTimeMark(ref str, dtfi, ref tm))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
				}
				else if (!DateTimeParse.MatchTimeMark(ref str, dtfi, ref tm))
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				if (parseInfo.timeMark == DateTimeParse.TM.NotSet)
				{
					parseInfo.timeMark = tm;
					return true;
				}
				if (parseInfo.timeMark != tm)
				{
					result.SetFailure(ParseFailureKind.FormatWithParameter, "DateTime pattern '{0}' appears more than once with different values.", @char);
					return false;
				}
				return true;
			}
			num = format.GetRepeatCount();
			if (num > 7)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			if (!DateTimeParse.ParseFractionExact(ref str, num, ref num2) && @char == 'f')
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			if (result.fraction < 0.0)
			{
				result.fraction = num2;
				return true;
			}
			if (num2 != result.fraction)
			{
				result.SetFailure(ParseFailureKind.FormatWithParameter, "DateTime pattern '{0}' appears more than once with different values.", @char);
				return false;
			}
			return true;
			IL_93B:
			if (@char == ' ')
			{
				if (!parseInfo.fAllowInnerWhite && !str.Match(@char))
				{
					if (parseInfo.fAllowTrailingWhite && format.GetNext() && DateTimeParse.ParseByFormat(ref str, ref format, ref parseInfo, dtfi, ref result))
					{
						return true;
					}
					result.SetBadDateTimeFailure();
					return false;
				}
			}
			else if (format.MatchSpecifiedWord("GMT"))
			{
				format.Index += "GMT".Length - 1;
				result.flags |= ParseFlags.TimeZoneUsed;
				result.timeZoneOffset = TimeSpan.Zero;
				if (!str.Match("GMT"))
				{
					result.SetBadDateTimeFailure();
					return false;
				}
			}
			else if (!str.Match(@char))
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			return true;
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0002E380 File Offset: 0x0002C580
		internal unsafe static bool TryParseQuoteString(ReadOnlySpan<char> format, int pos, StringBuilder result, out int returnValue)
		{
			returnValue = 0;
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
						return false;
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
				return false;
			}
			returnValue = pos - num;
			return true;
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0002E404 File Offset: 0x0002C604
		private unsafe static bool DoStrictParse(ReadOnlySpan<char> s, ReadOnlySpan<char> formatParam, DateTimeStyles styles, DateTimeFormatInfo dtfi, ref DateTimeResult result)
		{
			ParsingInfo parsingInfo = default(ParsingInfo);
			parsingInfo.Init();
			parsingInfo.calendar = dtfi.Calendar;
			parsingInfo.fAllowInnerWhite = ((styles & DateTimeStyles.AllowInnerWhite) > DateTimeStyles.None);
			parsingInfo.fAllowTrailingWhite = ((styles & DateTimeStyles.AllowTrailingWhite) > DateTimeStyles.None);
			if (formatParam.Length == 1)
			{
				if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags)0 && *formatParam[0] == 85)
				{
					result.SetBadFormatSpecifierFailure(formatParam);
					return false;
				}
				formatParam = DateTimeParse.ExpandPredefinedFormat(formatParam, ref dtfi, ref parsingInfo, ref result);
			}
			result.calendar = parsingInfo.calendar;
			if (!GlobalizationMode.Invariant && (ushort)parsingInfo.calendar.ID == 8)
			{
				LazyInitializer.EnsureInitialized<DateTimeParse.MatchNumberDelegate>(ref DateTimeParse.m_hebrewNumberParser, () => new DateTimeParse.MatchNumberDelegate(DateTimeParse.MatchHebrewDigits));
				parsingInfo.parseNumberDelegate = DateTimeParse.m_hebrewNumberParser;
				parsingInfo.fCustomNumberParser = true;
			}
			result.Hour = (result.Minute = (result.Second = -1));
			__DTString _DTString = new __DTString(formatParam, dtfi, false);
			__DTString _DTString2 = new __DTString(s, dtfi, false);
			if (parsingInfo.fAllowTrailingWhite)
			{
				_DTString.TrimTail();
				_DTString.RemoveTrailingInQuoteSpaces();
				_DTString2.TrimTail();
			}
			if ((styles & DateTimeStyles.AllowLeadingWhite) != DateTimeStyles.None)
			{
				_DTString.SkipWhiteSpaces();
				_DTString.RemoveLeadingInQuoteSpaces();
				_DTString2.SkipWhiteSpaces();
			}
			while (_DTString.GetNext())
			{
				if (parsingInfo.fAllowInnerWhite)
				{
					_DTString2.SkipWhiteSpaces();
				}
				if (!DateTimeParse.ParseByFormat(ref _DTString2, ref _DTString, ref parsingInfo, dtfi, ref result))
				{
					return false;
				}
			}
			if (_DTString2.Index < _DTString2.Value.Length - 1)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			if (parsingInfo.fUseTwoDigitYear && (dtfi.FormatFlags & DateTimeFormatFlags.UseHebrewRule) == DateTimeFormatFlags.None)
			{
				if (result.Year >= 100)
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				try
				{
					result.Year = parsingInfo.calendar.ToFourDigitYear(result.Year);
				}
				catch (ArgumentOutOfRangeException)
				{
					result.SetBadDateTimeFailure();
					return false;
				}
			}
			if (parsingInfo.fUseHour12)
			{
				if (parsingInfo.timeMark == DateTimeParse.TM.NotSet)
				{
					parsingInfo.timeMark = DateTimeParse.TM.AM;
				}
				if (result.Hour > 12)
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				if (parsingInfo.timeMark == DateTimeParse.TM.AM)
				{
					if (result.Hour == 12)
					{
						result.Hour = 0;
					}
				}
				else
				{
					result.Hour = ((result.Hour == 12) ? 12 : (result.Hour + 12));
				}
			}
			else if ((parsingInfo.timeMark == DateTimeParse.TM.AM && result.Hour >= 12) || (parsingInfo.timeMark == DateTimeParse.TM.PM && result.Hour < 12))
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			bool flag = result.Year == -1 && result.Month == -1 && result.Day == -1;
			if (!DateTimeParse.CheckDefaultDateTime(ref result, ref parsingInfo.calendar, styles))
			{
				return false;
			}
			if (!flag && dtfi.HasYearMonthAdjustment && !dtfi.YearMonthAdjustment(ref result.Year, ref result.Month, (result.flags & ParseFlags.ParsedMonthName) > (ParseFlags)0))
			{
				result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "The DateTime represented by the string is not supported in calendar {0}.");
				return false;
			}
			if (!parsingInfo.calendar.TryToDateTime(result.Year, result.Month, result.Day, result.Hour, result.Minute, result.Second, 0, result.era, out result.parsedDate))
			{
				result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "The DateTime represented by the string is not supported in calendar {0}.");
				return false;
			}
			if (result.fraction > 0.0)
			{
				result.parsedDate = result.parsedDate.AddTicks((long)Math.Round(result.fraction * 10000000.0));
			}
			if (parsingInfo.dayOfWeek != -1 && parsingInfo.dayOfWeek != (int)parsingInfo.calendar.GetDayOfWeek(result.parsedDate))
			{
				result.SetFailure(ParseFailureKind.FormatWithOriginalDateTime, "String was not recognized as a valid DateTime because the day of week was incorrect.");
				return false;
			}
			return DateTimeParse.DetermineTimeZoneAdjustments(ref _DTString2, ref result, styles, flag);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0002E7EC File Offset: 0x0002C9EC
		private static Exception GetDateTimeParseException(ref DateTimeResult result)
		{
			switch (result.failure)
			{
			case ParseFailureKind.ArgumentNull:
				return new ArgumentNullException(result.failureArgumentName, SR.GetResourceString(result.failureMessageID));
			case ParseFailureKind.Format:
				return new FormatException(SR.GetResourceString(result.failureMessageID));
			case ParseFailureKind.FormatWithParameter:
				return new FormatException(SR.Format(SR.GetResourceString(result.failureMessageID), result.failureMessageFormatArgument));
			case ParseFailureKind.FormatWithOriginalDateTime:
				return new FormatException(SR.Format(SR.GetResourceString(result.failureMessageID), new string(result.originalDateTimeString)));
			case ParseFailureKind.FormatWithFormatSpecifier:
				return new FormatException(SR.Format(SR.GetResourceString(result.failureMessageID), new string(result.failedFormatSpecifier)));
			case ParseFailureKind.FormatWithOriginalDateTimeAndParameter:
				return new FormatException(SR.Format(SR.GetResourceString(result.failureMessageID), new string(result.originalDateTimeString), result.failureMessageFormatArgument));
			case ParseFailureKind.FormatBadDateTimeCalendar:
				return new FormatException(SR.Format(SR.GetResourceString(result.failureMessageID), new string(result.originalDateTimeString), result.calendar));
			default:
				return null;
			}
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[Conditional("_LOGGING")]
		private static void LexTraceExit(string message, DateTimeParse.DS dps)
		{
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[Conditional("_LOGGING")]
		private static void PTSTraceExit(DateTimeParse.DS dps, bool passed)
		{
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[Conditional("_LOGGING")]
		private static void TPTraceExit(string message, DateTimeParse.DS dps)
		{
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[Conditional("_LOGGING")]
		private static void DTFITrace(DateTimeFormatInfo dtfi)
		{
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0002E900 File Offset: 0x0002CB00
		// Note: this type is marked as 'beforefieldinit'.
		static DateTimeParse()
		{
		}

		// Token: 0x040010F0 RID: 4336
		internal const int MaxDateTimeNumberDigits = 8;

		// Token: 0x040010F1 RID: 4337
		internal static DateTimeParse.MatchNumberDelegate m_hebrewNumberParser;

		// Token: 0x040010F2 RID: 4338
		private static DateTimeParse.DS[][] dateParsingStates = new DateTimeParse.DS[][]
		{
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.BEGIN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.TX_N,
				DateTimeParse.DS.N,
				DateTimeParse.DS.D_Nd,
				DateTimeParse.DS.T_Nt,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_M,
				DateTimeParse.DS.D_M,
				DateTimeParse.DS.D_S,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.BEGIN,
				DateTimeParse.DS.D_Y,
				DateTimeParse.DS.D_Y,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.BEGIN,
				DateTimeParse.DS.BEGIN,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_NN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.NN,
				DateTimeParse.DS.D_NNd,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_NM,
				DateTimeParse.DS.D_NM,
				DateTimeParse.DS.D_MNd,
				DateTimeParse.DS.D_NDS,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.N,
				DateTimeParse.DS.D_YN,
				DateTimeParse.DS.D_YNd,
				DateTimeParse.DS.DX_YN,
				DateTimeParse.DS.N,
				DateTimeParse.DS.N,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.DX_NN,
				DateTimeParse.DS.DX_NNN,
				DateTimeParse.DS.TX_N,
				DateTimeParse.DS.DX_NNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_Nt,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.NN,
				DateTimeParse.DS.DX_NNY,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_NNY,
				DateTimeParse.DS.NN,
				DateTimeParse.DS.NN,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_NN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_NN,
				DateTimeParse.DS.D_NNd,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_NM,
				DateTimeParse.DS.D_MN,
				DateTimeParse.DS.D_MNd,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_Nd,
				DateTimeParse.DS.D_YN,
				DateTimeParse.DS.D_YNd,
				DateTimeParse.DS.DX_YN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_Nd,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.DX_NN,
				DateTimeParse.DS.DX_NNN,
				DateTimeParse.DS.TX_N,
				DateTimeParse.DS.DX_NNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_Nt,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_DS,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.D_NN,
				DateTimeParse.DS.DX_NNY,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_NNY,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_NN,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_NNN,
				DateTimeParse.DS.DX_NNN,
				DateTimeParse.DS.DX_NNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_DS,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_NNd,
				DateTimeParse.DS.DX_NNY,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_NNY,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_NNd,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_MN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_MN,
				DateTimeParse.DS.D_MNd,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_M,
				DateTimeParse.DS.D_YM,
				DateTimeParse.DS.D_YMd,
				DateTimeParse.DS.DX_YM,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_M,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.DX_MN,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_Nt,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_DS,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.D_MN,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_MN,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.DX_NM,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_Nt,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_DS,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.D_NM,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_NM,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_MNd,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_MNd,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.DX_NDS,
				DateTimeParse.DS.DX_NNDS,
				DateTimeParse.DS.DX_NNDS,
				DateTimeParse.DS.DX_NNDS,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_Nt,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_NDS,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.D_NDS,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_NDS,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_YN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_YN,
				DateTimeParse.DS.D_YNd,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_YM,
				DateTimeParse.DS.D_YM,
				DateTimeParse.DS.D_YMd,
				DateTimeParse.DS.D_YM,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_Y,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_Y,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.DX_YN,
				DateTimeParse.DS.DX_YNN,
				DateTimeParse.DS.DX_YNN,
				DateTimeParse.DS.DX_YNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_YN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_YN,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_YNN,
				DateTimeParse.DS.DX_YNN,
				DateTimeParse.DS.DX_YNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_YN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_YN,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.DX_YM,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_YM,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_YM,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_YM,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_YM,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.DX_DS,
				DateTimeParse.DS.DX_DSN,
				DateTimeParse.DS.TX_N,
				DateTimeParse.DS.T_Nt,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_Nt,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_S,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.D_S,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_S,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.TX_TS,
				DateTimeParse.DS.TX_TS,
				DateTimeParse.DS.TX_TS,
				DateTimeParse.DS.T_Nt,
				DateTimeParse.DS.D_Nd,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_S,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.TX_NN,
				DateTimeParse.DS.TX_NN,
				DateTimeParse.DS.TX_NN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_NNt,
				DateTimeParse.DS.DX_NM,
				DateTimeParse.DS.D_NM,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_Nt,
				DateTimeParse.DS.T_Nt,
				DateTimeParse.DS.TX_NN
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.TX_NNN,
				DateTimeParse.DS.TX_NNN,
				DateTimeParse.DS.TX_NNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.T_NNt,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_NNt,
				DateTimeParse.DS.T_NNt,
				DateTimeParse.DS.TX_NNN
			}
		};

		// Token: 0x040010F3 RID: 4339
		internal const string GMTName = "GMT";

		// Token: 0x040010F4 RID: 4340
		internal const string ZuluName = "Z";

		// Token: 0x040010F5 RID: 4341
		private const int ORDER_YMD = 0;

		// Token: 0x040010F6 RID: 4342
		private const int ORDER_MDY = 1;

		// Token: 0x040010F7 RID: 4343
		private const int ORDER_DMY = 2;

		// Token: 0x040010F8 RID: 4344
		private const int ORDER_YDM = 3;

		// Token: 0x040010F9 RID: 4345
		private const int ORDER_YM = 4;

		// Token: 0x040010FA RID: 4346
		private const int ORDER_MY = 5;

		// Token: 0x040010FB RID: 4347
		private const int ORDER_MD = 6;

		// Token: 0x040010FC RID: 4348
		private const int ORDER_DM = 7;

		// Token: 0x02000120 RID: 288
		// (Invoke) Token: 0x06000B6A RID: 2922
		internal delegate bool MatchNumberDelegate(ref __DTString str, int digitLen, out int result);

		// Token: 0x02000121 RID: 289
		internal enum DTT
		{
			// Token: 0x040010FE RID: 4350
			End,
			// Token: 0x040010FF RID: 4351
			NumEnd,
			// Token: 0x04001100 RID: 4352
			NumAmpm,
			// Token: 0x04001101 RID: 4353
			NumSpace,
			// Token: 0x04001102 RID: 4354
			NumDatesep,
			// Token: 0x04001103 RID: 4355
			NumTimesep,
			// Token: 0x04001104 RID: 4356
			MonthEnd,
			// Token: 0x04001105 RID: 4357
			MonthSpace,
			// Token: 0x04001106 RID: 4358
			MonthDatesep,
			// Token: 0x04001107 RID: 4359
			NumDatesuff,
			// Token: 0x04001108 RID: 4360
			NumTimesuff,
			// Token: 0x04001109 RID: 4361
			DayOfWeek,
			// Token: 0x0400110A RID: 4362
			YearSpace,
			// Token: 0x0400110B RID: 4363
			YearDateSep,
			// Token: 0x0400110C RID: 4364
			YearEnd,
			// Token: 0x0400110D RID: 4365
			TimeZone,
			// Token: 0x0400110E RID: 4366
			Era,
			// Token: 0x0400110F RID: 4367
			NumUTCTimeMark,
			// Token: 0x04001110 RID: 4368
			Unk,
			// Token: 0x04001111 RID: 4369
			NumLocalTimeMark,
			// Token: 0x04001112 RID: 4370
			Max
		}

		// Token: 0x02000122 RID: 290
		internal enum TM
		{
			// Token: 0x04001114 RID: 4372
			NotSet = -1,
			// Token: 0x04001115 RID: 4373
			AM,
			// Token: 0x04001116 RID: 4374
			PM
		}

		// Token: 0x02000123 RID: 291
		internal enum DS
		{
			// Token: 0x04001118 RID: 4376
			BEGIN,
			// Token: 0x04001119 RID: 4377
			N,
			// Token: 0x0400111A RID: 4378
			NN,
			// Token: 0x0400111B RID: 4379
			D_Nd,
			// Token: 0x0400111C RID: 4380
			D_NN,
			// Token: 0x0400111D RID: 4381
			D_NNd,
			// Token: 0x0400111E RID: 4382
			D_M,
			// Token: 0x0400111F RID: 4383
			D_MN,
			// Token: 0x04001120 RID: 4384
			D_NM,
			// Token: 0x04001121 RID: 4385
			D_MNd,
			// Token: 0x04001122 RID: 4386
			D_NDS,
			// Token: 0x04001123 RID: 4387
			D_Y,
			// Token: 0x04001124 RID: 4388
			D_YN,
			// Token: 0x04001125 RID: 4389
			D_YNd,
			// Token: 0x04001126 RID: 4390
			D_YM,
			// Token: 0x04001127 RID: 4391
			D_YMd,
			// Token: 0x04001128 RID: 4392
			D_S,
			// Token: 0x04001129 RID: 4393
			T_S,
			// Token: 0x0400112A RID: 4394
			T_Nt,
			// Token: 0x0400112B RID: 4395
			T_NNt,
			// Token: 0x0400112C RID: 4396
			ERROR,
			// Token: 0x0400112D RID: 4397
			DX_NN,
			// Token: 0x0400112E RID: 4398
			DX_NNN,
			// Token: 0x0400112F RID: 4399
			DX_MN,
			// Token: 0x04001130 RID: 4400
			DX_NM,
			// Token: 0x04001131 RID: 4401
			DX_MNN,
			// Token: 0x04001132 RID: 4402
			DX_DS,
			// Token: 0x04001133 RID: 4403
			DX_DSN,
			// Token: 0x04001134 RID: 4404
			DX_NDS,
			// Token: 0x04001135 RID: 4405
			DX_NNDS,
			// Token: 0x04001136 RID: 4406
			DX_YNN,
			// Token: 0x04001137 RID: 4407
			DX_YMN,
			// Token: 0x04001138 RID: 4408
			DX_YN,
			// Token: 0x04001139 RID: 4409
			DX_YM,
			// Token: 0x0400113A RID: 4410
			TX_N,
			// Token: 0x0400113B RID: 4411
			TX_NN,
			// Token: 0x0400113C RID: 4412
			TX_NNN,
			// Token: 0x0400113D RID: 4413
			TX_TS,
			// Token: 0x0400113E RID: 4414
			DX_NNY
		}

		// Token: 0x02000124 RID: 292
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000B6D RID: 2925 RVA: 0x0002EAC8 File Offset: 0x0002CCC8
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000B6E RID: 2926 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x06000B6F RID: 2927 RVA: 0x0002EAD4 File Offset: 0x0002CCD4
			internal DateTimeParse.MatchNumberDelegate <DoStrictParse>b__98_0()
			{
				return new DateTimeParse.MatchNumberDelegate(DateTimeParse.MatchHebrewDigits);
			}

			// Token: 0x0400113F RID: 4415
			public static readonly DateTimeParse.<>c <>9 = new DateTimeParse.<>c();

			// Token: 0x04001140 RID: 4416
			public static Func<DateTimeParse.MatchNumberDelegate> <>9__98_0;
		}
	}
}
