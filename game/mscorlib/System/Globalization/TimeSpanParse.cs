using System;
using System.Text;

namespace System.Globalization
{
	// Token: 0x02000972 RID: 2418
	internal static class TimeSpanParse
	{
		// Token: 0x06005561 RID: 21857 RVA: 0x0011EEA4 File Offset: 0x0011D0A4
		internal static long Pow10(int pow)
		{
			switch (pow)
			{
			case 0:
				return 1L;
			case 1:
				return 10L;
			case 2:
				return 100L;
			case 3:
				return 1000L;
			case 4:
				return 10000L;
			case 5:
				return 100000L;
			case 6:
				return 1000000L;
			case 7:
				return 10000000L;
			default:
				return (long)Math.Pow(10.0, (double)pow);
			}
		}

		// Token: 0x06005562 RID: 21858 RVA: 0x0011EF18 File Offset: 0x0011D118
		private static bool TryTimeToTicks(bool positive, TimeSpanParse.TimeSpanToken days, TimeSpanParse.TimeSpanToken hours, TimeSpanParse.TimeSpanToken minutes, TimeSpanParse.TimeSpanToken seconds, TimeSpanParse.TimeSpanToken fraction, out long result)
		{
			if (days._num > 10675199 || hours._num > 23 || minutes._num > 59 || seconds._num > 59 || fraction.IsInvalidFraction())
			{
				result = 0L;
				return false;
			}
			long num = ((long)days._num * 3600L * 24L + (long)hours._num * 3600L + (long)minutes._num * 60L + (long)seconds._num) * 1000L;
			if (num > 922337203685477L || num < -922337203685477L)
			{
				result = 0L;
				return false;
			}
			long num2 = (long)fraction._num;
			if (num2 != 0L)
			{
				long num3 = 1000000L;
				if (fraction._zeroes > 0)
				{
					long num4 = TimeSpanParse.Pow10(fraction._zeroes);
					num3 /= num4;
				}
				while (num2 < num3)
				{
					num2 *= 10L;
				}
			}
			result = num * 10000L + num2;
			if (positive && result < 0L)
			{
				result = 0L;
				return false;
			}
			return true;
		}

		// Token: 0x06005563 RID: 21859 RVA: 0x0011F018 File Offset: 0x0011D218
		internal static TimeSpan Parse(ReadOnlySpan<char> input, IFormatProvider formatProvider)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = new TimeSpanParse.TimeSpanResult(true);
			TimeSpanParse.TryParseTimeSpan(input, TimeSpanParse.TimeSpanStandardStyles.Any, formatProvider, ref timeSpanResult);
			return timeSpanResult.parsedTimeSpan;
		}

		// Token: 0x06005564 RID: 21860 RVA: 0x0011F040 File Offset: 0x0011D240
		internal static bool TryParse(ReadOnlySpan<char> input, IFormatProvider formatProvider, out TimeSpan result)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = new TimeSpanParse.TimeSpanResult(false);
			if (TimeSpanParse.TryParseTimeSpan(input, TimeSpanParse.TimeSpanStandardStyles.Any, formatProvider, ref timeSpanResult))
			{
				result = timeSpanResult.parsedTimeSpan;
				return true;
			}
			result = default(TimeSpan);
			return false;
		}

		// Token: 0x06005565 RID: 21861 RVA: 0x0011F078 File Offset: 0x0011D278
		internal static TimeSpan ParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, IFormatProvider formatProvider, TimeSpanStyles styles)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = new TimeSpanParse.TimeSpanResult(true);
			TimeSpanParse.TryParseExactTimeSpan(input, format, formatProvider, styles, ref timeSpanResult);
			return timeSpanResult.parsedTimeSpan;
		}

		// Token: 0x06005566 RID: 21862 RVA: 0x0011F0A0 File Offset: 0x0011D2A0
		internal static bool TryParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = new TimeSpanParse.TimeSpanResult(false);
			if (TimeSpanParse.TryParseExactTimeSpan(input, format, formatProvider, styles, ref timeSpanResult))
			{
				result = timeSpanResult.parsedTimeSpan;
				return true;
			}
			result = default(TimeSpan);
			return false;
		}

		// Token: 0x06005567 RID: 21863 RVA: 0x0011F0DC File Offset: 0x0011D2DC
		internal static TimeSpan ParseExactMultiple(ReadOnlySpan<char> input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = new TimeSpanParse.TimeSpanResult(true);
			TimeSpanParse.TryParseExactMultipleTimeSpan(input, formats, formatProvider, styles, ref timeSpanResult);
			return timeSpanResult.parsedTimeSpan;
		}

		// Token: 0x06005568 RID: 21864 RVA: 0x0011F104 File Offset: 0x0011D304
		internal static bool TryParseExactMultiple(ReadOnlySpan<char> input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = new TimeSpanParse.TimeSpanResult(false);
			if (TimeSpanParse.TryParseExactMultipleTimeSpan(input, formats, formatProvider, styles, ref timeSpanResult))
			{
				result = timeSpanResult.parsedTimeSpan;
				return true;
			}
			result = default(TimeSpan);
			return false;
		}

		// Token: 0x06005569 RID: 21865 RVA: 0x0011F140 File Offset: 0x0011D340
		private static bool TryParseTimeSpan(ReadOnlySpan<char> input, TimeSpanParse.TimeSpanStandardStyles style, IFormatProvider formatProvider, ref TimeSpanParse.TimeSpanResult result)
		{
			input = input.Trim();
			if (input.IsEmpty)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			TimeSpanParse.TimeSpanTokenizer timeSpanTokenizer = new TimeSpanParse.TimeSpanTokenizer(input);
			TimeSpanParse.TimeSpanRawInfo timeSpanRawInfo = default(TimeSpanParse.TimeSpanRawInfo);
			timeSpanRawInfo.Init(DateTimeFormatInfo.GetInstance(formatProvider));
			TimeSpanParse.TimeSpanToken nextToken = timeSpanTokenizer.GetNextToken();
			while (nextToken._ttt != TimeSpanParse.TTT.End)
			{
				if (!timeSpanRawInfo.ProcessToken(ref nextToken, ref result))
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
				}
				nextToken = timeSpanTokenizer.GetNextToken();
			}
			return TimeSpanParse.ProcessTerminalState(ref timeSpanRawInfo, style, ref result) || result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
		}

		// Token: 0x0600556A RID: 21866 RVA: 0x0011F1DC File Offset: 0x0011D3DC
		private static bool ProcessTerminalState(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw._lastSeenTTT == TimeSpanParse.TTT.Num)
			{
				TimeSpanParse.TimeSpanToken timeSpanToken = default(TimeSpanParse.TimeSpanToken);
				timeSpanToken._ttt = TimeSpanParse.TTT.Sep;
				if (!raw.ProcessToken(ref timeSpanToken, ref result))
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
				}
			}
			switch (raw._numCount)
			{
			case 1:
				return TimeSpanParse.ProcessTerminal_D(ref raw, style, ref result);
			case 2:
				return TimeSpanParse.ProcessTerminal_HM(ref raw, style, ref result);
			case 3:
				return TimeSpanParse.ProcessTerminal_HM_S_D(ref raw, style, ref result);
			case 4:
				return TimeSpanParse.ProcessTerminal_HMS_F_D(ref raw, style, ref result);
			case 5:
				return TimeSpanParse.ProcessTerminal_DHMSF(ref raw, style, ref result);
			default:
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
		}

		// Token: 0x0600556B RID: 21867 RVA: 0x0011F27C File Offset: 0x0011D47C
		private static bool ProcessTerminal_DHMSF(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw._sepCount != 6)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			if (flag)
			{
				if (raw.FullMatch(raw.PositiveInvariant))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullMatch(raw.NegativeInvariant))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullMatch(raw.PositiveLocalized))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullMatch(raw.NegativeLocalized))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (!flag4)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			long num;
			if (!TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, raw._numbers4, out num))
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
			}
			if (!flag3)
			{
				num = -num;
				if (num > 0L)
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
				}
			}
			result.parsedTimeSpan = new TimeSpan(num);
			return true;
		}

		// Token: 0x0600556C RID: 21868 RVA: 0x0011F378 File Offset: 0x0011D578
		private static bool ProcessTerminal_HMS_F_D(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw._sepCount != 5 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			long num = 0L;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			TimeSpanParse.TimeSpanToken timeSpanToken = new TimeSpanParse.TimeSpanToken(0);
			if (flag)
			{
				if (raw.FullHMSFMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullDHMSMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, timeSpanToken, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, raw._numbers3, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullHMSFMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullDHMSMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, timeSpanToken, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, raw._numbers3, out num);
					flag5 = (flag5 || !flag4);
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullHMSFMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullDHMSMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, timeSpanToken, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, raw._numbers3, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullHMSFMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullDHMSMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, timeSpanToken, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, raw._numbers3, out num);
					flag5 = (flag5 || !flag4);
				}
			}
			if (flag4)
			{
				if (!flag3)
				{
					num = -num;
					if (num > 0L)
					{
						return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
					}
				}
				result.parsedTimeSpan = new TimeSpan(num);
				return true;
			}
			if (!flag5)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
		}

		// Token: 0x0600556D RID: 21869 RVA: 0x0011F73C File Offset: 0x0011D93C
		private static bool ProcessTerminal_HM_S_D(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw._sepCount != 4 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			TimeSpanParse.TimeSpanToken timeSpanToken = new TimeSpanParse.TimeSpanToken(0);
			long num = 0L;
			if (flag)
			{
				if (raw.FullHMSMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullDHMMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, timeSpanToken, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, timeSpanToken, raw._numbers2, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullHMSMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullDHMMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, timeSpanToken, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, timeSpanToken, raw._numbers2, out num);
					flag5 = (flag5 || !flag4);
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullHMSMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullDHMMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, timeSpanToken, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, timeSpanToken, raw._numbers2, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullHMSMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.FullDHMMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, timeSpanToken, out num);
					flag5 = (flag5 || !flag4);
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, timeSpanToken, raw._numbers2, out num);
					flag5 = (flag5 || !flag4);
				}
			}
			if (flag4)
			{
				if (!flag3)
				{
					num = -num;
					if (num > 0L)
					{
						return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
					}
				}
				result.parsedTimeSpan = new TimeSpan(num);
				return true;
			}
			if (!flag5)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
		}

		// Token: 0x0600556E RID: 21870 RVA: 0x0011FAB8 File Offset: 0x0011DCB8
		private static bool ProcessTerminal_HM(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw._sepCount != 3 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			if (flag)
			{
				if (raw.FullHMMatch(raw.PositiveInvariant))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullHMMatch(raw.NegativeInvariant))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullHMMatch(raw.PositiveLocalized))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullHMMatch(raw.NegativeLocalized))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (!flag4)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			long num = 0L;
			TimeSpanParse.TimeSpanToken timeSpanToken = new TimeSpanParse.TimeSpanToken(0);
			if (!TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, timeSpanToken, timeSpanToken, out num))
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
			}
			if (!flag3)
			{
				num = -num;
				if (num > 0L)
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
				}
			}
			result.parsedTimeSpan = new TimeSpan(num);
			return true;
		}

		// Token: 0x0600556F RID: 21871 RVA: 0x0011FBB8 File Offset: 0x0011DDB8
		private static bool ProcessTerminal_D(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw._sepCount != 2 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			if (flag)
			{
				if (raw.FullDMatch(raw.PositiveInvariant))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullDMatch(raw.NegativeInvariant))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullDMatch(raw.PositiveLocalized))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullDMatch(raw.NegativeLocalized))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (!flag4)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			long num = 0L;
			TimeSpanParse.TimeSpanToken timeSpanToken = new TimeSpanParse.TimeSpanToken(0);
			if (!TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, timeSpanToken, timeSpanToken, timeSpanToken, timeSpanToken, out num))
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
			}
			if (!flag3)
			{
				num = -num;
				if (num > 0L)
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
				}
			}
			result.parsedTimeSpan = new TimeSpan(num);
			return true;
		}

		// Token: 0x06005570 RID: 21872 RVA: 0x0011FCB4 File Offset: 0x0011DEB4
		private unsafe static bool TryParseExactTimeSpan(ReadOnlySpan<char> input, ReadOnlySpan<char> format, IFormatProvider formatProvider, TimeSpanStyles styles, ref TimeSpanParse.TimeSpanResult result)
		{
			if (format.Length == 0)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format specifier was invalid.", null, null);
			}
			if (format.Length == 1)
			{
				char c = (char)(*format[0]);
				if (c <= 'T')
				{
					if (c == 'G')
					{
						return TimeSpanParse.TryParseTimeSpan(input, TimeSpanParse.TimeSpanStandardStyles.Localized | TimeSpanParse.TimeSpanStandardStyles.RequireFull, formatProvider, ref result);
					}
					if (c != 'T')
					{
						goto IL_6C;
					}
				}
				else if (c != 'c')
				{
					if (c == 'g')
					{
						return TimeSpanParse.TryParseTimeSpan(input, TimeSpanParse.TimeSpanStandardStyles.Localized, formatProvider, ref result);
					}
					if (c != 't')
					{
						goto IL_6C;
					}
				}
				return TimeSpanParse.TryParseTimeSpanConstant(input, ref result);
				IL_6C:
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format specifier was invalid.", null, null);
			}
			return TimeSpanParse.TryParseByFormat(input, format, styles, ref result);
		}

		// Token: 0x06005571 RID: 21873 RVA: 0x0011FD48 File Offset: 0x0011DF48
		private unsafe static bool TryParseByFormat(ReadOnlySpan<char> input, ReadOnlySpan<char> format, TimeSpanStyles styles, ref TimeSpanParse.TimeSpanResult result)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			int number = 0;
			int number2 = 0;
			int number3 = 0;
			int number4 = 0;
			int leadingZeroes = 0;
			int number5 = 0;
			int i = 0;
			int num = 0;
			TimeSpanParse.TimeSpanTokenizer timeSpanTokenizer = new TimeSpanParse.TimeSpanTokenizer(input, -1);
			while (i < format.Length)
			{
				char c = (char)(*format[i]);
				if (c <= 'F')
				{
					if (c <= '%')
					{
						if (c != '"')
						{
							if (c != '%')
							{
								goto IL_2E1;
							}
							int num2 = DateTimeFormat.ParseNextChar(format, i);
							if (num2 >= 0 && num2 != 37)
							{
								num = 1;
								goto IL_2F0;
							}
							return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.", null, null);
						}
					}
					else if (c != '\'')
					{
						if (c != 'F')
						{
							goto IL_2E1;
						}
						num = DateTimeFormat.ParseRepeatPattern(format, i, c);
						if (num > 7 || flag5)
						{
							return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.", null, null);
						}
						TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num, num, out leadingZeroes, out number5);
						flag5 = true;
						goto IL_2F0;
					}
					StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
					if (!DateTimeParse.TryParseQuoteString(format, i, stringBuilder, out num))
					{
						StringBuilderCache.Release(stringBuilder);
						return result.SetFailure(TimeSpanParse.ParseFailureKind.FormatWithParameter, "Cannot find a matching quote character for the character '{0}'.", c, null);
					}
					if (!TimeSpanParse.ParseExactLiteral(ref timeSpanTokenizer, stringBuilder))
					{
						StringBuilderCache.Release(stringBuilder);
						return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.", null, null);
					}
					StringBuilderCache.Release(stringBuilder);
				}
				else if (c <= 'h')
				{
					if (c != '\\')
					{
						switch (c)
						{
						case 'd':
						{
							num = DateTimeFormat.ParseRepeatPattern(format, i, c);
							int num3 = 0;
							if (num > 8 || flag || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, (num < 2) ? 1 : num, (num < 2) ? 8 : num, out num3, out number))
							{
								return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.", null, null);
							}
							flag = true;
							break;
						}
						case 'e':
						case 'g':
							goto IL_2E1;
						case 'f':
							num = DateTimeFormat.ParseRepeatPattern(format, i, c);
							if (num > 7 || flag5 || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num, num, out leadingZeroes, out number5))
							{
								return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.", null, null);
							}
							flag5 = true;
							break;
						case 'h':
							num = DateTimeFormat.ParseRepeatPattern(format, i, c);
							if (num > 2 || flag2 || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num, out number2))
							{
								return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.", null, null);
							}
							flag2 = true;
							break;
						default:
							goto IL_2E1;
						}
					}
					else
					{
						int num2 = DateTimeFormat.ParseNextChar(format, i);
						if (num2 < 0 || timeSpanTokenizer.NextChar != (char)num2)
						{
							return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.", null, null);
						}
						num = 2;
					}
				}
				else if (c != 'm')
				{
					if (c != 's')
					{
						goto IL_2E1;
					}
					num = DateTimeFormat.ParseRepeatPattern(format, i, c);
					if (num > 2 || flag4 || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num, out number4))
					{
						return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.", null, null);
					}
					flag4 = true;
				}
				else
				{
					num = DateTimeFormat.ParseRepeatPattern(format, i, c);
					if (num > 2 || flag3 || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num, out number3))
					{
						return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.", null, null);
					}
					flag3 = true;
				}
				IL_2F0:
				i += num;
				continue;
				IL_2E1:
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.", null, null);
			}
			if (!timeSpanTokenizer.EOL)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			bool flag6 = (styles & TimeSpanStyles.AssumeNegative) == TimeSpanStyles.None;
			long num4;
			if (TimeSpanParse.TryTimeToTicks(flag6, new TimeSpanParse.TimeSpanToken(number), new TimeSpanParse.TimeSpanToken(number2), new TimeSpanParse.TimeSpanToken(number3), new TimeSpanParse.TimeSpanToken(number4), new TimeSpanParse.TimeSpanToken(number5, leadingZeroes), out num4))
			{
				if (!flag6)
				{
					num4 = -num4;
				}
				result.parsedTimeSpan = new TimeSpan(num4);
				return true;
			}
			return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
		}

		// Token: 0x06005572 RID: 21874 RVA: 0x001200D0 File Offset: 0x0011E2D0
		private static bool ParseExactDigits(ref TimeSpanParse.TimeSpanTokenizer tokenizer, int minDigitLength, out int result)
		{
			result = 0;
			int num = 0;
			int maxDigitLength = (minDigitLength == 1) ? 2 : minDigitLength;
			return TimeSpanParse.ParseExactDigits(ref tokenizer, minDigitLength, maxDigitLength, out num, out result);
		}

		// Token: 0x06005573 RID: 21875 RVA: 0x001200F8 File Offset: 0x0011E2F8
		private static bool ParseExactDigits(ref TimeSpanParse.TimeSpanTokenizer tokenizer, int minDigitLength, int maxDigitLength, out int zeroes, out int result)
		{
			int num = 0;
			int num2 = 0;
			int i;
			for (i = 0; i < maxDigitLength; i++)
			{
				char nextChar = tokenizer.NextChar;
				if (nextChar < '0' || nextChar > '9')
				{
					tokenizer.BackOne();
					break;
				}
				num = num * 10 + (int)(nextChar - '0');
				if (num == 0)
				{
					num2++;
				}
			}
			zeroes = num2;
			result = num;
			return i >= minDigitLength;
		}

		// Token: 0x06005574 RID: 21876 RVA: 0x00120150 File Offset: 0x0011E350
		private static bool ParseExactLiteral(ref TimeSpanParse.TimeSpanTokenizer tokenizer, StringBuilder enquotedString)
		{
			for (int i = 0; i < enquotedString.Length; i++)
			{
				if (enquotedString[i] != tokenizer.NextChar)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005575 RID: 21877 RVA: 0x00120180 File Offset: 0x0011E380
		private static bool TryParseTimeSpanConstant(ReadOnlySpan<char> input, ref TimeSpanParse.TimeSpanResult result)
		{
			return default(TimeSpanParse.StringParser).TryParse(input, ref result);
		}

		// Token: 0x06005576 RID: 21878 RVA: 0x001201A0 File Offset: 0x0011E3A0
		private static bool TryParseExactMultipleTimeSpan(ReadOnlySpan<char> input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, ref TimeSpanParse.TimeSpanResult result)
		{
			if (formats == null)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "String reference not set to an instance of a String.", null, "formats");
			}
			if (input.Length == 0)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			if (formats.Length == 0)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format specifier was invalid.", null, null);
			}
			for (int i = 0; i < formats.Length; i++)
			{
				if (formats[i] == null || formats[i].Length == 0)
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format specifier was invalid.", null, null);
				}
				TimeSpanParse.TimeSpanResult timeSpanResult = new TimeSpanParse.TimeSpanResult(false);
				if (TimeSpanParse.TryParseExactTimeSpan(input, formats[i], formatProvider, styles, ref timeSpanResult))
				{
					result.parsedTimeSpan = timeSpanResult.parsedTimeSpan;
					return true;
				}
			}
			return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
		}

		// Token: 0x06005577 RID: 21879 RVA: 0x00120258 File Offset: 0x0011E458
		internal static void ValidateStyles(TimeSpanStyles style, string parameterName)
		{
			if (style != TimeSpanStyles.None && style != TimeSpanStyles.AssumeNegative)
			{
				throw new ArgumentException(Environment.GetResourceString("An undefined TimeSpanStyles value is being used."), parameterName);
			}
		}

		// Token: 0x040034F1 RID: 13553
		private const int MaxFractionDigits = 7;

		// Token: 0x040034F2 RID: 13554
		private const int MaxDays = 10675199;

		// Token: 0x040034F3 RID: 13555
		private const int MaxHours = 23;

		// Token: 0x040034F4 RID: 13556
		private const int MaxMinutes = 59;

		// Token: 0x040034F5 RID: 13557
		private const int MaxSeconds = 59;

		// Token: 0x040034F6 RID: 13558
		private const int MaxFraction = 9999999;

		// Token: 0x02000973 RID: 2419
		private enum ParseFailureKind : byte
		{
			// Token: 0x040034F8 RID: 13560
			None,
			// Token: 0x040034F9 RID: 13561
			ArgumentNull,
			// Token: 0x040034FA RID: 13562
			Format,
			// Token: 0x040034FB RID: 13563
			FormatWithParameter,
			// Token: 0x040034FC RID: 13564
			Overflow
		}

		// Token: 0x02000974 RID: 2420
		[Flags]
		private enum TimeSpanStandardStyles : byte
		{
			// Token: 0x040034FE RID: 13566
			None = 0,
			// Token: 0x040034FF RID: 13567
			Invariant = 1,
			// Token: 0x04003500 RID: 13568
			Localized = 2,
			// Token: 0x04003501 RID: 13569
			RequireFull = 4,
			// Token: 0x04003502 RID: 13570
			Any = 3
		}

		// Token: 0x02000975 RID: 2421
		private enum TTT : byte
		{
			// Token: 0x04003504 RID: 13572
			None,
			// Token: 0x04003505 RID: 13573
			End,
			// Token: 0x04003506 RID: 13574
			Num,
			// Token: 0x04003507 RID: 13575
			Sep,
			// Token: 0x04003508 RID: 13576
			NumOverflow
		}

		// Token: 0x02000976 RID: 2422
		private ref struct TimeSpanToken
		{
			// Token: 0x06005578 RID: 21880 RVA: 0x00120274 File Offset: 0x0011E474
			public TimeSpanToken(TimeSpanParse.TTT type)
			{
				this = new TimeSpanParse.TimeSpanToken(type, 0, 0, default(ReadOnlySpan<char>));
			}

			// Token: 0x06005579 RID: 21881 RVA: 0x00120294 File Offset: 0x0011E494
			public TimeSpanToken(int number)
			{
				this = new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.Num, number, 0, default(ReadOnlySpan<char>));
			}

			// Token: 0x0600557A RID: 21882 RVA: 0x001202B4 File Offset: 0x0011E4B4
			public TimeSpanToken(int number, int leadingZeroes)
			{
				this = new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.Num, number, leadingZeroes, default(ReadOnlySpan<char>));
			}

			// Token: 0x0600557B RID: 21883 RVA: 0x001202D3 File Offset: 0x0011E4D3
			public TimeSpanToken(TimeSpanParse.TTT type, int number, int leadingZeroes, ReadOnlySpan<char> separator)
			{
				this._ttt = type;
				this._num = number;
				this._zeroes = leadingZeroes;
				this._sep = separator;
			}

			// Token: 0x0600557C RID: 21884 RVA: 0x001202F4 File Offset: 0x0011E4F4
			public bool IsInvalidFraction()
			{
				return this._num > 9999999 || this._zeroes > 7 || (this._num != 0 && this._zeroes != 0 && (long)this._num >= 9999999L / TimeSpanParse.Pow10(this._zeroes - 1));
			}

			// Token: 0x04003509 RID: 13577
			internal TimeSpanParse.TTT _ttt;

			// Token: 0x0400350A RID: 13578
			internal int _num;

			// Token: 0x0400350B RID: 13579
			internal int _zeroes;

			// Token: 0x0400350C RID: 13580
			internal ReadOnlySpan<char> _sep;
		}

		// Token: 0x02000977 RID: 2423
		private ref struct TimeSpanTokenizer
		{
			// Token: 0x0600557D RID: 21885 RVA: 0x0012034B File Offset: 0x0011E54B
			internal TimeSpanTokenizer(ReadOnlySpan<char> input)
			{
				this = new TimeSpanParse.TimeSpanTokenizer(input, 0);
			}

			// Token: 0x0600557E RID: 21886 RVA: 0x00120355 File Offset: 0x0011E555
			internal TimeSpanTokenizer(ReadOnlySpan<char> input, int startPosition)
			{
				this._value = input;
				this._pos = startPosition;
			}

			// Token: 0x0600557F RID: 21887 RVA: 0x00120368 File Offset: 0x0011E568
			internal unsafe TimeSpanParse.TimeSpanToken GetNextToken()
			{
				int pos = this._pos;
				if (pos >= this._value.Length)
				{
					return new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.End);
				}
				int num = (int)(*this._value[pos] - 48);
				if (num <= 9)
				{
					int num2 = 0;
					if (num == 0)
					{
						num2 = 1;
						int num4;
						for (;;)
						{
							int num3 = this._pos + 1;
							this._pos = num3;
							if (num3 >= this._value.Length || (num4 = (int)(*this._value[this._pos] - 48)) > 9)
							{
								break;
							}
							if (num4 != 0)
							{
								goto IL_99;
							}
							num2++;
						}
						return new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.Num, 0, num2, default(ReadOnlySpan<char>));
						IL_99:
						num = num4;
					}
					do
					{
						int num3 = this._pos + 1;
						this._pos = num3;
						if (num3 >= this._value.Length)
						{
							goto IL_F6;
						}
						int num5 = (int)(*this._value[this._pos] - 48);
						if (num5 > 9)
						{
							goto IL_F6;
						}
						num = num * 10 + num5;
					}
					while (((long)num & (long)((ulong)-268435456)) == 0L);
					return new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.NumOverflow);
					IL_F6:
					return new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.Num, num, num2, default(ReadOnlySpan<char>));
				}
				int num6 = 1;
				for (;;)
				{
					int num3 = this._pos + 1;
					this._pos = num3;
					if (num3 >= this._value.Length || *this._value[this._pos] - 48 <= 9)
					{
						break;
					}
					num6++;
				}
				return new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.Sep, 0, 0, this._value.Slice(pos, num6));
			}

			// Token: 0x17000E33 RID: 3635
			// (get) Token: 0x06005580 RID: 21888 RVA: 0x001204D5 File Offset: 0x0011E6D5
			internal bool EOL
			{
				get
				{
					return this._pos >= this._value.Length - 1;
				}
			}

			// Token: 0x06005581 RID: 21889 RVA: 0x001204EF File Offset: 0x0011E6EF
			internal void BackOne()
			{
				if (this._pos > 0)
				{
					this._pos--;
				}
			}

			// Token: 0x17000E34 RID: 3636
			// (get) Token: 0x06005582 RID: 21890 RVA: 0x00120508 File Offset: 0x0011E708
			internal unsafe char NextChar
			{
				get
				{
					int num = this._pos + 1;
					this._pos = num;
					int num2 = num;
					if (num2 >= this._value.Length)
					{
						return '\0';
					}
					return (char)(*this._value[num2]);
				}
			}

			// Token: 0x0400350D RID: 13581
			private ReadOnlySpan<char> _value;

			// Token: 0x0400350E RID: 13582
			private int _pos;
		}

		// Token: 0x02000978 RID: 2424
		private ref struct TimeSpanRawInfo
		{
			// Token: 0x17000E35 RID: 3637
			// (get) Token: 0x06005583 RID: 21891 RVA: 0x00120544 File Offset: 0x0011E744
			internal TimeSpanFormat.FormatLiterals PositiveInvariant
			{
				get
				{
					return TimeSpanFormat.PositiveInvariantFormatLiterals;
				}
			}

			// Token: 0x17000E36 RID: 3638
			// (get) Token: 0x06005584 RID: 21892 RVA: 0x0012054B File Offset: 0x0011E74B
			internal TimeSpanFormat.FormatLiterals NegativeInvariant
			{
				get
				{
					return TimeSpanFormat.NegativeInvariantFormatLiterals;
				}
			}

			// Token: 0x17000E37 RID: 3639
			// (get) Token: 0x06005585 RID: 21893 RVA: 0x00120552 File Offset: 0x0011E752
			internal TimeSpanFormat.FormatLiterals PositiveLocalized
			{
				get
				{
					if (!this._posLocInit)
					{
						this._posLoc = default(TimeSpanFormat.FormatLiterals);
						this._posLoc.Init(this._fullPosPattern, false);
						this._posLocInit = true;
					}
					return this._posLoc;
				}
			}

			// Token: 0x17000E38 RID: 3640
			// (get) Token: 0x06005586 RID: 21894 RVA: 0x0012058C File Offset: 0x0011E78C
			internal TimeSpanFormat.FormatLiterals NegativeLocalized
			{
				get
				{
					if (!this._negLocInit)
					{
						this._negLoc = default(TimeSpanFormat.FormatLiterals);
						this._negLoc.Init(this._fullNegPattern, false);
						this._negLocInit = true;
					}
					return this._negLoc;
				}
			}

			// Token: 0x06005587 RID: 21895 RVA: 0x001205C8 File Offset: 0x0011E7C8
			internal bool FullAppCompatMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 5 && this._numCount == 4 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.DayHourSep) && this._literals2.EqualsOrdinal(pattern.HourMinuteSep) && this._literals3.EqualsOrdinal(pattern.AppCompatLiteral) && this._literals4.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06005588 RID: 21896 RVA: 0x00120668 File Offset: 0x0011E868
			internal bool PartialAppCompatMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 4 && this._numCount == 3 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.HourMinuteSep) && this._literals2.EqualsOrdinal(pattern.AppCompatLiteral) && this._literals3.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06005589 RID: 21897 RVA: 0x001206EC File Offset: 0x0011E8EC
			internal bool FullMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 6 && this._numCount == 5 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.DayHourSep) && this._literals2.EqualsOrdinal(pattern.HourMinuteSep) && this._literals3.EqualsOrdinal(pattern.MinuteSecondSep) && this._literals4.EqualsOrdinal(pattern.SecondFractionSep) && this._literals5.EqualsOrdinal(pattern.End);
			}

			// Token: 0x0600558A RID: 21898 RVA: 0x001207A8 File Offset: 0x0011E9A8
			internal bool FullDMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 2 && this._numCount == 1 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.End);
			}

			// Token: 0x0600558B RID: 21899 RVA: 0x001207FC File Offset: 0x0011E9FC
			internal bool FullHMMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 3 && this._numCount == 2 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.HourMinuteSep) && this._literals2.EqualsOrdinal(pattern.End);
			}

			// Token: 0x0600558C RID: 21900 RVA: 0x00120868 File Offset: 0x0011EA68
			internal bool FullDHMMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 4 && this._numCount == 3 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.DayHourSep) && this._literals2.EqualsOrdinal(pattern.HourMinuteSep) && this._literals3.EqualsOrdinal(pattern.End);
			}

			// Token: 0x0600558D RID: 21901 RVA: 0x001208EC File Offset: 0x0011EAEC
			internal bool FullHMSMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 4 && this._numCount == 3 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.HourMinuteSep) && this._literals2.EqualsOrdinal(pattern.MinuteSecondSep) && this._literals3.EqualsOrdinal(pattern.End);
			}

			// Token: 0x0600558E RID: 21902 RVA: 0x00120970 File Offset: 0x0011EB70
			internal bool FullDHMSMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 5 && this._numCount == 4 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.DayHourSep) && this._literals2.EqualsOrdinal(pattern.HourMinuteSep) && this._literals3.EqualsOrdinal(pattern.MinuteSecondSep) && this._literals4.EqualsOrdinal(pattern.End);
			}

			// Token: 0x0600558F RID: 21903 RVA: 0x00120A10 File Offset: 0x0011EC10
			internal bool FullHMSFMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 5 && this._numCount == 4 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.HourMinuteSep) && this._literals2.EqualsOrdinal(pattern.MinuteSecondSep) && this._literals3.EqualsOrdinal(pattern.SecondFractionSep) && this._literals4.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06005590 RID: 21904 RVA: 0x00120AB0 File Offset: 0x0011ECB0
			internal void Init(DateTimeFormatInfo dtfi)
			{
				this._lastSeenTTT = TimeSpanParse.TTT.None;
				this._tokenCount = 0;
				this._sepCount = 0;
				this._numCount = 0;
				this._fullPosPattern = dtfi.FullTimeSpanPositivePattern;
				this._fullNegPattern = dtfi.FullTimeSpanNegativePattern;
				this._posLocInit = false;
				this._negLocInit = false;
			}

			// Token: 0x06005591 RID: 21905 RVA: 0x00120B00 File Offset: 0x0011ED00
			internal bool ProcessToken(ref TimeSpanParse.TimeSpanToken tok, ref TimeSpanParse.TimeSpanResult result)
			{
				switch (tok._ttt)
				{
				case TimeSpanParse.TTT.Num:
					if ((this._tokenCount == 0 && !this.AddSep(default(ReadOnlySpan<char>), ref result)) || !this.AddNum(tok, ref result))
					{
						return false;
					}
					break;
				case TimeSpanParse.TTT.Sep:
					if (!this.AddSep(tok._sep, ref result))
					{
						return false;
					}
					break;
				case TimeSpanParse.TTT.NumOverflow:
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
				default:
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
				}
				this._lastSeenTTT = tok._ttt;
				return true;
			}

			// Token: 0x06005592 RID: 21906 RVA: 0x00120B94 File Offset: 0x0011ED94
			private bool AddSep(ReadOnlySpan<char> sep, ref TimeSpanParse.TimeSpanResult result)
			{
				if (this._sepCount >= 6 || this._tokenCount >= 11)
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
				}
				int sepCount = this._sepCount;
				this._sepCount = sepCount + 1;
				switch (sepCount)
				{
				case 0:
					this._literals0 = sep;
					break;
				case 1:
					this._literals1 = sep;
					break;
				case 2:
					this._literals2 = sep;
					break;
				case 3:
					this._literals3 = sep;
					break;
				case 4:
					this._literals4 = sep;
					break;
				default:
					this._literals5 = sep;
					break;
				}
				this._tokenCount++;
				return true;
			}

			// Token: 0x06005593 RID: 21907 RVA: 0x00120C34 File Offset: 0x0011EE34
			private bool AddNum(TimeSpanParse.TimeSpanToken num, ref TimeSpanParse.TimeSpanResult result)
			{
				if (this._numCount >= 5 || this._tokenCount >= 11)
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
				}
				int numCount = this._numCount;
				this._numCount = numCount + 1;
				switch (numCount)
				{
				case 0:
					this._numbers0 = num;
					break;
				case 1:
					this._numbers1 = num;
					break;
				case 2:
					this._numbers2 = num;
					break;
				case 3:
					this._numbers3 = num;
					break;
				default:
					this._numbers4 = num;
					break;
				}
				this._tokenCount++;
				return true;
			}

			// Token: 0x0400350F RID: 13583
			internal TimeSpanParse.TTT _lastSeenTTT;

			// Token: 0x04003510 RID: 13584
			internal int _tokenCount;

			// Token: 0x04003511 RID: 13585
			internal int _sepCount;

			// Token: 0x04003512 RID: 13586
			internal int _numCount;

			// Token: 0x04003513 RID: 13587
			private TimeSpanFormat.FormatLiterals _posLoc;

			// Token: 0x04003514 RID: 13588
			private TimeSpanFormat.FormatLiterals _negLoc;

			// Token: 0x04003515 RID: 13589
			private bool _posLocInit;

			// Token: 0x04003516 RID: 13590
			private bool _negLocInit;

			// Token: 0x04003517 RID: 13591
			private string _fullPosPattern;

			// Token: 0x04003518 RID: 13592
			private string _fullNegPattern;

			// Token: 0x04003519 RID: 13593
			private const int MaxTokens = 11;

			// Token: 0x0400351A RID: 13594
			private const int MaxLiteralTokens = 6;

			// Token: 0x0400351B RID: 13595
			private const int MaxNumericTokens = 5;

			// Token: 0x0400351C RID: 13596
			internal TimeSpanParse.TimeSpanToken _numbers0;

			// Token: 0x0400351D RID: 13597
			internal TimeSpanParse.TimeSpanToken _numbers1;

			// Token: 0x0400351E RID: 13598
			internal TimeSpanParse.TimeSpanToken _numbers2;

			// Token: 0x0400351F RID: 13599
			internal TimeSpanParse.TimeSpanToken _numbers3;

			// Token: 0x04003520 RID: 13600
			internal TimeSpanParse.TimeSpanToken _numbers4;

			// Token: 0x04003521 RID: 13601
			internal ReadOnlySpan<char> _literals0;

			// Token: 0x04003522 RID: 13602
			internal ReadOnlySpan<char> _literals1;

			// Token: 0x04003523 RID: 13603
			internal ReadOnlySpan<char> _literals2;

			// Token: 0x04003524 RID: 13604
			internal ReadOnlySpan<char> _literals3;

			// Token: 0x04003525 RID: 13605
			internal ReadOnlySpan<char> _literals4;

			// Token: 0x04003526 RID: 13606
			internal ReadOnlySpan<char> _literals5;
		}

		// Token: 0x02000979 RID: 2425
		private struct TimeSpanResult
		{
			// Token: 0x06005594 RID: 21908 RVA: 0x00120CC7 File Offset: 0x0011EEC7
			internal TimeSpanResult(bool throwOnFailure)
			{
				this.parsedTimeSpan = default(TimeSpan);
				this._throwOnFailure = throwOnFailure;
			}

			// Token: 0x06005595 RID: 21909 RVA: 0x00120CDC File Offset: 0x0011EEDC
			internal bool SetFailure(TimeSpanParse.ParseFailureKind kind, string resourceKey, object messageArgument = null, string argumentName = null)
			{
				if (!this._throwOnFailure)
				{
					return false;
				}
				string resourceString = SR.GetResourceString(resourceKey);
				switch (kind)
				{
				case TimeSpanParse.ParseFailureKind.ArgumentNull:
					throw new ArgumentNullException(argumentName, resourceString);
				case TimeSpanParse.ParseFailureKind.FormatWithParameter:
					throw new FormatException(SR.Format(resourceString, messageArgument));
				case TimeSpanParse.ParseFailureKind.Overflow:
					throw new OverflowException(resourceString);
				}
				throw new FormatException(resourceString);
			}

			// Token: 0x04003527 RID: 13607
			internal TimeSpan parsedTimeSpan;

			// Token: 0x04003528 RID: 13608
			private readonly bool _throwOnFailure;
		}

		// Token: 0x0200097A RID: 2426
		[Obsolete("Types with embedded references are not supported in this version of your compiler.", true)]
		private ref struct StringParser
		{
			// Token: 0x06005596 RID: 21910 RVA: 0x00120D38 File Offset: 0x0011EF38
			internal unsafe void NextChar()
			{
				if (this._pos < this._len)
				{
					this._pos++;
				}
				this._ch = (char)((this._pos < this._len) ? (*this._str[this._pos]) : 0);
			}

			// Token: 0x06005597 RID: 21911 RVA: 0x00120D8C File Offset: 0x0011EF8C
			internal unsafe char NextNonDigit()
			{
				for (int i = this._pos; i < this._len; i++)
				{
					char c = (char)(*this._str[i]);
					if (c < '0' || c > '9')
					{
						return c;
					}
				}
				return '\0';
			}

			// Token: 0x06005598 RID: 21912 RVA: 0x00120DCC File Offset: 0x0011EFCC
			internal bool TryParse(ReadOnlySpan<char> input, ref TimeSpanParse.TimeSpanResult result)
			{
				result.parsedTimeSpan = default(TimeSpan);
				this._str = input;
				this._len = input.Length;
				this._pos = -1;
				this.NextChar();
				this.SkipBlanks();
				bool flag = false;
				if (this._ch == '-')
				{
					flag = true;
					this.NextChar();
				}
				long num;
				if (this.NextNonDigit() == ':')
				{
					if (!this.ParseTime(out num, ref result))
					{
						return false;
					}
				}
				else
				{
					int num2;
					if (!this.ParseInt(10675199, out num2, ref result))
					{
						return false;
					}
					num = (long)num2 * 864000000000L;
					if (this._ch == '.')
					{
						this.NextChar();
						long num3;
						if (!this.ParseTime(out num3, ref result))
						{
							return false;
						}
						num += num3;
					}
				}
				if (flag)
				{
					num = -num;
					if (num > 0L)
					{
						return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
					}
				}
				else if (num < 0L)
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
				}
				this.SkipBlanks();
				if (this._pos < this._len)
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
				}
				result.parsedTimeSpan = new TimeSpan(num);
				return true;
			}

			// Token: 0x06005599 RID: 21913 RVA: 0x00120ED8 File Offset: 0x0011F0D8
			internal bool ParseInt(int max, out int i, ref TimeSpanParse.TimeSpanResult result)
			{
				i = 0;
				int pos = this._pos;
				while (this._ch >= '0' && this._ch <= '9')
				{
					if (((long)i & (long)((ulong)-268435456)) != 0L)
					{
						return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
					}
					i = i * 10 + (int)this._ch - 48;
					if (i < 0)
					{
						return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
					}
					this.NextChar();
				}
				if (pos == this._pos)
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
				}
				return i <= max || result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
			}

			// Token: 0x0600559A RID: 21914 RVA: 0x00120F78 File Offset: 0x0011F178
			internal bool ParseTime(out long time, ref TimeSpanParse.TimeSpanResult result)
			{
				time = 0L;
				int num;
				if (!this.ParseInt(23, out num, ref result))
				{
					return false;
				}
				time = (long)num * 36000000000L;
				if (this._ch != ':')
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
				}
				this.NextChar();
				if (!this.ParseInt(59, out num, ref result))
				{
					return false;
				}
				time += (long)num * 600000000L;
				if (this._ch == ':')
				{
					this.NextChar();
					if (this._ch != '.')
					{
						if (!this.ParseInt(59, out num, ref result))
						{
							return false;
						}
						time += (long)num * 10000000L;
					}
					if (this._ch == '.')
					{
						this.NextChar();
						int num2 = 10000000;
						while (num2 > 1 && this._ch >= '0' && this._ch <= '9')
						{
							num2 /= 10;
							time += (long)((int)(this._ch - '0') * num2);
							this.NextChar();
						}
					}
				}
				return true;
			}

			// Token: 0x0600559B RID: 21915 RVA: 0x00121066 File Offset: 0x0011F266
			internal void SkipBlanks()
			{
				while (this._ch == ' ' || this._ch == '\t')
				{
					this.NextChar();
				}
			}

			// Token: 0x04003529 RID: 13609
			private ReadOnlySpan<char> _str;

			// Token: 0x0400352A RID: 13610
			private char _ch;

			// Token: 0x0400352B RID: 13611
			private int _pos;

			// Token: 0x0400352C RID: 13612
			private int _len;
		}
	}
}
