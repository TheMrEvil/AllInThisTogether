using System;
using System.Collections.Generic;
using System.Text;

namespace System.Globalization
{
	// Token: 0x0200095F RID: 2399
	internal class DateTimeFormatInfoScanner
	{
		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x0600552A RID: 21802 RVA: 0x0011D644 File Offset: 0x0011B844
		private static Dictionary<string, string> KnownWords
		{
			get
			{
				if (DateTimeFormatInfoScanner.s_knownWords == null)
				{
					DateTimeFormatInfoScanner.s_knownWords = new Dictionary<string, string>
					{
						{
							"/",
							string.Empty
						},
						{
							"-",
							string.Empty
						},
						{
							".",
							string.Empty
						},
						{
							"年",
							string.Empty
						},
						{
							"月",
							string.Empty
						},
						{
							"日",
							string.Empty
						},
						{
							"년",
							string.Empty
						},
						{
							"월",
							string.Empty
						},
						{
							"일",
							string.Empty
						},
						{
							"시",
							string.Empty
						},
						{
							"분",
							string.Empty
						},
						{
							"초",
							string.Empty
						},
						{
							"時",
							string.Empty
						},
						{
							"时",
							string.Empty
						},
						{
							"分",
							string.Empty
						},
						{
							"秒",
							string.Empty
						}
					};
				}
				return DateTimeFormatInfoScanner.s_knownWords;
			}
		}

		// Token: 0x0600552B RID: 21803 RVA: 0x0011D770 File Offset: 0x0011B970
		internal static int SkipWhiteSpacesAndNonLetter(string pattern, int currentIndex)
		{
			while (currentIndex < pattern.Length)
			{
				char c = pattern[currentIndex];
				if (c == '\\')
				{
					currentIndex++;
					if (currentIndex >= pattern.Length)
					{
						break;
					}
					c = pattern[currentIndex];
					if (c == '\'')
					{
						continue;
					}
				}
				if (char.IsLetter(c) || c == '\'' || c == '.')
				{
					break;
				}
				currentIndex++;
			}
			return currentIndex;
		}

		// Token: 0x0600552C RID: 21804 RVA: 0x0011D7C8 File Offset: 0x0011B9C8
		internal void AddDateWordOrPostfix(string formatPostfix, string str)
		{
			if (str.Length > 0)
			{
				if (str.Equals("."))
				{
					this.AddIgnorableSymbols(".");
					return;
				}
				string text;
				if (!DateTimeFormatInfoScanner.KnownWords.TryGetValue(str, out text))
				{
					if (this.m_dateWords == null)
					{
						this.m_dateWords = new List<string>();
					}
					if (formatPostfix == "MMMM")
					{
						string item = "" + str;
						if (!this.m_dateWords.Contains(item))
						{
							this.m_dateWords.Add(item);
							return;
						}
					}
					else
					{
						if (!this.m_dateWords.Contains(str))
						{
							this.m_dateWords.Add(str);
						}
						if (str[str.Length - 1] == '.')
						{
							string item2 = str.Substring(0, str.Length - 1);
							if (!this.m_dateWords.Contains(item2))
							{
								this.m_dateWords.Add(item2);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600552D RID: 21805 RVA: 0x0011D8AC File Offset: 0x0011BAAC
		internal int AddDateWords(string pattern, int index, string formatPostfix)
		{
			int num = DateTimeFormatInfoScanner.SkipWhiteSpacesAndNonLetter(pattern, index);
			if (num != index && formatPostfix != null)
			{
				formatPostfix = null;
			}
			index = num;
			StringBuilder stringBuilder = new StringBuilder();
			while (index < pattern.Length)
			{
				char c = pattern[index];
				if (c == '\'')
				{
					this.AddDateWordOrPostfix(formatPostfix, stringBuilder.ToString());
					index++;
					break;
				}
				if (c == '\\')
				{
					index++;
					if (index < pattern.Length)
					{
						stringBuilder.Append(pattern[index]);
						index++;
					}
				}
				else if (char.IsWhiteSpace(c))
				{
					this.AddDateWordOrPostfix(formatPostfix, stringBuilder.ToString());
					if (formatPostfix != null)
					{
						formatPostfix = null;
					}
					stringBuilder.Length = 0;
					index++;
				}
				else
				{
					stringBuilder.Append(c);
					index++;
				}
			}
			return index;
		}

		// Token: 0x0600552E RID: 21806 RVA: 0x0011D960 File Offset: 0x0011BB60
		internal static int ScanRepeatChar(string pattern, char ch, int index, out int count)
		{
			count = 1;
			while (++index < pattern.Length && pattern[index] == ch)
			{
				count++;
			}
			return index;
		}

		// Token: 0x0600552F RID: 21807 RVA: 0x0011D988 File Offset: 0x0011BB88
		internal void AddIgnorableSymbols(string text)
		{
			if (this.m_dateWords == null)
			{
				this.m_dateWords = new List<string>();
			}
			string item = "" + text;
			if (!this.m_dateWords.Contains(item))
			{
				this.m_dateWords.Add(item);
			}
		}

		// Token: 0x06005530 RID: 21808 RVA: 0x0011D9D0 File Offset: 0x0011BBD0
		internal void ScanDateWord(string pattern)
		{
			this._ymdFlags = DateTimeFormatInfoScanner.FoundDatePattern.None;
			for (int i = 0; i < pattern.Length; i++)
			{
				char c = pattern[i];
				if (c <= 'M')
				{
					if (c == '\'')
					{
						i = this.AddDateWords(pattern, i + 1, null);
						continue;
					}
					if (c == '.')
					{
						if (this._ymdFlags == DateTimeFormatInfoScanner.FoundDatePattern.FoundYMDPatternFlag)
						{
							this.AddIgnorableSymbols(".");
							this._ymdFlags = DateTimeFormatInfoScanner.FoundDatePattern.None;
						}
						i++;
						continue;
					}
					if (c == 'M')
					{
						int num;
						i = DateTimeFormatInfoScanner.ScanRepeatChar(pattern, 'M', i, out num);
						if (num >= 4 && i < pattern.Length && pattern[i] == '\'')
						{
							i = this.AddDateWords(pattern, i + 1, "MMMM");
						}
						this._ymdFlags |= DateTimeFormatInfoScanner.FoundDatePattern.FoundMonthPatternFlag;
						continue;
					}
				}
				else
				{
					if (c == '\\')
					{
						i += 2;
						continue;
					}
					if (c != 'd')
					{
						if (c == 'y')
						{
							int num;
							i = DateTimeFormatInfoScanner.ScanRepeatChar(pattern, 'y', i, out num);
							this._ymdFlags |= DateTimeFormatInfoScanner.FoundDatePattern.FoundYearPatternFlag;
							continue;
						}
					}
					else
					{
						int num;
						i = DateTimeFormatInfoScanner.ScanRepeatChar(pattern, 'd', i, out num);
						if (num <= 2)
						{
							this._ymdFlags |= DateTimeFormatInfoScanner.FoundDatePattern.FoundDayPatternFlag;
							continue;
						}
						continue;
					}
				}
				if (this._ymdFlags == DateTimeFormatInfoScanner.FoundDatePattern.FoundYMDPatternFlag && !char.IsWhiteSpace(c))
				{
					this._ymdFlags = DateTimeFormatInfoScanner.FoundDatePattern.None;
				}
			}
		}

		// Token: 0x06005531 RID: 21809 RVA: 0x0011DB08 File Offset: 0x0011BD08
		internal string[] GetDateWordsOfDTFI(DateTimeFormatInfo dtfi)
		{
			string[] allDateTimePatterns = dtfi.GetAllDateTimePatterns('D');
			for (int i = 0; i < allDateTimePatterns.Length; i++)
			{
				this.ScanDateWord(allDateTimePatterns[i]);
			}
			allDateTimePatterns = dtfi.GetAllDateTimePatterns('d');
			for (int i = 0; i < allDateTimePatterns.Length; i++)
			{
				this.ScanDateWord(allDateTimePatterns[i]);
			}
			allDateTimePatterns = dtfi.GetAllDateTimePatterns('y');
			for (int i = 0; i < allDateTimePatterns.Length; i++)
			{
				this.ScanDateWord(allDateTimePatterns[i]);
			}
			this.ScanDateWord(dtfi.MonthDayPattern);
			allDateTimePatterns = dtfi.GetAllDateTimePatterns('T');
			for (int i = 0; i < allDateTimePatterns.Length; i++)
			{
				this.ScanDateWord(allDateTimePatterns[i]);
			}
			allDateTimePatterns = dtfi.GetAllDateTimePatterns('t');
			for (int i = 0; i < allDateTimePatterns.Length; i++)
			{
				this.ScanDateWord(allDateTimePatterns[i]);
			}
			string[] array = null;
			if (this.m_dateWords != null && this.m_dateWords.Count > 0)
			{
				array = new string[this.m_dateWords.Count];
				for (int i = 0; i < this.m_dateWords.Count; i++)
				{
					array[i] = this.m_dateWords[i];
				}
			}
			return array;
		}

		// Token: 0x06005532 RID: 21810 RVA: 0x0011DC10 File Offset: 0x0011BE10
		internal static FORMATFLAGS GetFormatFlagGenitiveMonth(string[] monthNames, string[] genitveMonthNames, string[] abbrevMonthNames, string[] genetiveAbbrevMonthNames)
		{
			if (DateTimeFormatInfoScanner.EqualStringArrays(monthNames, genitveMonthNames) && DateTimeFormatInfoScanner.EqualStringArrays(abbrevMonthNames, genetiveAbbrevMonthNames))
			{
				return FORMATFLAGS.None;
			}
			return FORMATFLAGS.UseGenitiveMonth;
		}

		// Token: 0x06005533 RID: 21811 RVA: 0x0011DC28 File Offset: 0x0011BE28
		internal static FORMATFLAGS GetFormatFlagUseSpaceInMonthNames(string[] monthNames, string[] genitveMonthNames, string[] abbrevMonthNames, string[] genetiveAbbrevMonthNames)
		{
			return FORMATFLAGS.None | ((DateTimeFormatInfoScanner.ArrayElementsBeginWithDigit(monthNames) || DateTimeFormatInfoScanner.ArrayElementsBeginWithDigit(genitveMonthNames) || DateTimeFormatInfoScanner.ArrayElementsBeginWithDigit(abbrevMonthNames) || DateTimeFormatInfoScanner.ArrayElementsBeginWithDigit(genetiveAbbrevMonthNames)) ? FORMATFLAGS.UseDigitPrefixInTokens : FORMATFLAGS.None) | ((DateTimeFormatInfoScanner.ArrayElementsHaveSpace(monthNames) || DateTimeFormatInfoScanner.ArrayElementsHaveSpace(genitveMonthNames) || DateTimeFormatInfoScanner.ArrayElementsHaveSpace(abbrevMonthNames) || DateTimeFormatInfoScanner.ArrayElementsHaveSpace(genetiveAbbrevMonthNames)) ? FORMATFLAGS.UseSpacesInMonthNames : FORMATFLAGS.None);
		}

		// Token: 0x06005534 RID: 21812 RVA: 0x0011DC81 File Offset: 0x0011BE81
		internal static FORMATFLAGS GetFormatFlagUseSpaceInDayNames(string[] dayNames, string[] abbrevDayNames)
		{
			if (!DateTimeFormatInfoScanner.ArrayElementsHaveSpace(dayNames) && !DateTimeFormatInfoScanner.ArrayElementsHaveSpace(abbrevDayNames))
			{
				return FORMATFLAGS.None;
			}
			return FORMATFLAGS.UseSpacesInDayNames;
		}

		// Token: 0x06005535 RID: 21813 RVA: 0x0011DC97 File Offset: 0x0011BE97
		internal static FORMATFLAGS GetFormatFlagUseHebrewCalendar(int calID)
		{
			if (calID != 8)
			{
				return FORMATFLAGS.None;
			}
			return (FORMATFLAGS)10;
		}

		// Token: 0x06005536 RID: 21814 RVA: 0x0011DCA4 File Offset: 0x0011BEA4
		private static bool EqualStringArrays(string[] array1, string[] array2)
		{
			if (array1 == array2)
			{
				return true;
			}
			if (array1.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array1.Length; i++)
			{
				if (!array1[i].Equals(array2[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005537 RID: 21815 RVA: 0x0011DCE0 File Offset: 0x0011BEE0
		private static bool ArrayElementsHaveSpace(string[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				for (int j = 0; j < array[i].Length; j++)
				{
					if (char.IsWhiteSpace(array[i][j]))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06005538 RID: 21816 RVA: 0x0011DD24 File Offset: 0x0011BF24
		private static bool ArrayElementsBeginWithDigit(string[] array)
		{
			int i = 0;
			while (i < array.Length)
			{
				if (array[i].Length > 0 && array[i][0] >= '0' && array[i][0] <= '9')
				{
					int num = 1;
					while (num < array[i].Length && array[i][num] >= '0' && array[i][num] <= '9')
					{
						num++;
					}
					if (num == array[i].Length)
					{
						return false;
					}
					if (num == array[i].Length - 1)
					{
						char c = array[i][num];
						if (c == '月' || c == '월')
						{
							return false;
						}
					}
					return num != array[i].Length - 4 || array[i][num] != '\'' || array[i][num + 1] != ' ' || array[i][num + 2] != '月' || array[i][num + 3] != '\'';
				}
				else
				{
					i++;
				}
			}
			return false;
		}

		// Token: 0x06005539 RID: 21817 RVA: 0x0011DE25 File Offset: 0x0011C025
		public DateTimeFormatInfoScanner()
		{
		}

		// Token: 0x04003461 RID: 13409
		internal const char MonthPostfixChar = '';

		// Token: 0x04003462 RID: 13410
		internal const char IgnorableSymbolChar = '';

		// Token: 0x04003463 RID: 13411
		internal const string CJKYearSuff = "年";

		// Token: 0x04003464 RID: 13412
		internal const string CJKMonthSuff = "月";

		// Token: 0x04003465 RID: 13413
		internal const string CJKDaySuff = "日";

		// Token: 0x04003466 RID: 13414
		internal const string KoreanYearSuff = "년";

		// Token: 0x04003467 RID: 13415
		internal const string KoreanMonthSuff = "월";

		// Token: 0x04003468 RID: 13416
		internal const string KoreanDaySuff = "일";

		// Token: 0x04003469 RID: 13417
		internal const string KoreanHourSuff = "시";

		// Token: 0x0400346A RID: 13418
		internal const string KoreanMinuteSuff = "분";

		// Token: 0x0400346B RID: 13419
		internal const string KoreanSecondSuff = "초";

		// Token: 0x0400346C RID: 13420
		internal const string CJKHourSuff = "時";

		// Token: 0x0400346D RID: 13421
		internal const string ChineseHourSuff = "时";

		// Token: 0x0400346E RID: 13422
		internal const string CJKMinuteSuff = "分";

		// Token: 0x0400346F RID: 13423
		internal const string CJKSecondSuff = "秒";

		// Token: 0x04003470 RID: 13424
		internal List<string> m_dateWords = new List<string>();

		// Token: 0x04003471 RID: 13425
		private static volatile Dictionary<string, string> s_knownWords;

		// Token: 0x04003472 RID: 13426
		private DateTimeFormatInfoScanner.FoundDatePattern _ymdFlags;

		// Token: 0x02000960 RID: 2400
		private enum FoundDatePattern
		{
			// Token: 0x04003474 RID: 13428
			None,
			// Token: 0x04003475 RID: 13429
			FoundYearPatternFlag,
			// Token: 0x04003476 RID: 13430
			FoundMonthPatternFlag,
			// Token: 0x04003477 RID: 13431
			FoundDayPatternFlag = 4,
			// Token: 0x04003478 RID: 13432
			FoundYMDPatternFlag = 7
		}
	}
}
