using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Globalization
{
	// Token: 0x0200099F RID: 2463
	[StructLayout(LayoutKind.Sequential)]
	internal class CultureData
	{
		// Token: 0x060058AE RID: 22702 RVA: 0x0012AC7F File Offset: 0x00128E7F
		private CultureData(string name)
		{
			this.sRealName = name;
		}

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x060058AF RID: 22703 RVA: 0x0012AC90 File Offset: 0x00128E90
		public static CultureData Invariant
		{
			get
			{
				if (CultureData.s_Invariant == null)
				{
					CultureData cultureData = new CultureData("");
					cultureData.sISO639Language = "iv";
					cultureData.sAM1159 = "AM";
					cultureData.sPM2359 = "PM";
					cultureData.sTimeSeparator = ":";
					cultureData.saLongTimes = new string[]
					{
						"HH:mm:ss"
					};
					cultureData.saShortTimes = new string[]
					{
						"HH:mm",
						"hh:mm tt",
						"H:mm",
						"h:mm tt"
					};
					cultureData.iFirstDayOfWeek = 0;
					cultureData.iFirstWeekOfYear = 0;
					cultureData.waCalendars = new int[]
					{
						1
					};
					cultureData.calendars = new CalendarData[23];
					cultureData.calendars[0] = CalendarData.Invariant;
					cultureData.iDefaultAnsiCodePage = 1252;
					cultureData.iDefaultOemCodePage = 437;
					cultureData.iDefaultMacCodePage = 10000;
					cultureData.iDefaultEbcdicCodePage = 37;
					cultureData.sListSeparator = ",";
					Interlocked.CompareExchange<CultureData>(ref CultureData.s_Invariant, cultureData, null);
				}
				return CultureData.s_Invariant;
			}
		}

		// Token: 0x060058B0 RID: 22704 RVA: 0x0012ADA4 File Offset: 0x00128FA4
		public static CultureData GetCultureData(string cultureName, bool useUserOverride)
		{
			CultureData result;
			try
			{
				result = new CultureInfo(cultureName, useUserOverride).m_cultureData;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060058B1 RID: 22705 RVA: 0x0012ADD8 File Offset: 0x00128FD8
		public static CultureData GetCultureData(string cultureName, bool useUserOverride, int datetimeIndex, int calendarId, int numberIndex, string iso2lang, int ansiCodePage, int oemCodePage, int macCodePage, int ebcdicCodePage, bool rightToLeft, string listSeparator)
		{
			if (string.IsNullOrEmpty(cultureName))
			{
				return CultureData.Invariant;
			}
			CultureData cultureData = new CultureData(cultureName);
			cultureData.fill_culture_data(datetimeIndex);
			cultureData.bUseOverrides = useUserOverride;
			cultureData.calendarId = calendarId;
			cultureData.numberIndex = numberIndex;
			cultureData.sISO639Language = iso2lang;
			cultureData.iDefaultAnsiCodePage = ansiCodePage;
			cultureData.iDefaultOemCodePage = oemCodePage;
			cultureData.iDefaultMacCodePage = macCodePage;
			cultureData.iDefaultEbcdicCodePage = ebcdicCodePage;
			cultureData.isRightToLeft = rightToLeft;
			cultureData.sListSeparator = listSeparator;
			return cultureData;
		}

		// Token: 0x060058B2 RID: 22706 RVA: 0x0000AF5E File Offset: 0x0000915E
		internal static CultureData GetCultureData(int culture, bool bUseUserOverride)
		{
			return null;
		}

		// Token: 0x060058B3 RID: 22707
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void fill_culture_data(int datetimeIndex);

		// Token: 0x060058B4 RID: 22708 RVA: 0x0012AE50 File Offset: 0x00129050
		public CalendarData GetCalendar(int calendarId)
		{
			int num = calendarId - 1;
			if (this.calendars == null)
			{
				this.calendars = new CalendarData[23];
			}
			CalendarData calendarData = this.calendars[num];
			if (calendarData == null)
			{
				calendarData = new CalendarData(this.sRealName, calendarId, this.bUseOverrides);
				this.calendars[num] = calendarData;
			}
			return calendarData;
		}

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x060058B5 RID: 22709 RVA: 0x0012AE9F File Offset: 0x0012909F
		internal string[] LongTimes
		{
			get
			{
				return this.saLongTimes;
			}
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x060058B6 RID: 22710 RVA: 0x0012AEA9 File Offset: 0x001290A9
		internal string[] ShortTimes
		{
			get
			{
				return this.saShortTimes;
			}
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x060058B7 RID: 22711 RVA: 0x0012AEB3 File Offset: 0x001290B3
		internal string SISO639LANGNAME
		{
			get
			{
				return this.sISO639Language;
			}
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x060058B8 RID: 22712 RVA: 0x0012AEBB File Offset: 0x001290BB
		internal int IFIRSTDAYOFWEEK
		{
			get
			{
				return this.iFirstDayOfWeek;
			}
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x060058B9 RID: 22713 RVA: 0x0012AEC3 File Offset: 0x001290C3
		internal int IFIRSTWEEKOFYEAR
		{
			get
			{
				return this.iFirstWeekOfYear;
			}
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x060058BA RID: 22714 RVA: 0x0012AECB File Offset: 0x001290CB
		internal string SAM1159
		{
			get
			{
				return this.sAM1159;
			}
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x060058BB RID: 22715 RVA: 0x0012AED3 File Offset: 0x001290D3
		internal string SPM2359
		{
			get
			{
				return this.sPM2359;
			}
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x060058BC RID: 22716 RVA: 0x0012AEDB File Offset: 0x001290DB
		internal string TimeSeparator
		{
			get
			{
				return this.sTimeSeparator;
			}
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x060058BD RID: 22717 RVA: 0x0012AEE4 File Offset: 0x001290E4
		internal int[] CalendarIds
		{
			get
			{
				if (this.waCalendars == null)
				{
					string a = this.sISO639Language;
					if (!(a == "ja"))
					{
						if (!(a == "zh"))
						{
							if (!(a == "he"))
							{
								this.waCalendars = new int[]
								{
									this.calendarId
								};
							}
							else
							{
								this.waCalendars = new int[]
								{
									this.calendarId,
									8
								};
							}
						}
						else
						{
							this.waCalendars = new int[]
							{
								this.calendarId,
								4
							};
						}
					}
					else
					{
						this.waCalendars = new int[]
						{
							this.calendarId,
							3
						};
					}
				}
				return this.waCalendars;
			}
		}

		// Token: 0x060058BE RID: 22718 RVA: 0x0012AFA4 File Offset: 0x001291A4
		internal CalendarId[] GetCalendarIds()
		{
			CalendarId[] array = new CalendarId[this.CalendarIds.Length];
			for (int i = 0; i < this.CalendarIds.Length; i++)
			{
				array[i] = (CalendarId)this.CalendarIds[i];
			}
			return array;
		}

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x060058BF RID: 22719 RVA: 0x0012AFDF File Offset: 0x001291DF
		internal bool IsInvariantCulture
		{
			get
			{
				return string.IsNullOrEmpty(this.sRealName);
			}
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x060058C0 RID: 22720 RVA: 0x0012AFEC File Offset: 0x001291EC
		internal string CultureName
		{
			get
			{
				return this.sRealName;
			}
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x060058C1 RID: 22721 RVA: 0x00061549 File Offset: 0x0005F749
		internal string SCOMPAREINFO
		{
			get
			{
				return "";
			}
		}

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x060058C2 RID: 22722 RVA: 0x0012AFEC File Offset: 0x001291EC
		internal string STEXTINFO
		{
			get
			{
				return this.sRealName;
			}
		}

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x060058C3 RID: 22723 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		internal int ILANGUAGE
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x060058C4 RID: 22724 RVA: 0x0012AFF4 File Offset: 0x001291F4
		internal int IDEFAULTANSICODEPAGE
		{
			get
			{
				return this.iDefaultAnsiCodePage;
			}
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x060058C5 RID: 22725 RVA: 0x0012AFFC File Offset: 0x001291FC
		internal int IDEFAULTOEMCODEPAGE
		{
			get
			{
				return this.iDefaultOemCodePage;
			}
		}

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x060058C6 RID: 22726 RVA: 0x0012B004 File Offset: 0x00129204
		internal int IDEFAULTMACCODEPAGE
		{
			get
			{
				return this.iDefaultMacCodePage;
			}
		}

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x060058C7 RID: 22727 RVA: 0x0012B00C File Offset: 0x0012920C
		internal int IDEFAULTEBCDICCODEPAGE
		{
			get
			{
				return this.iDefaultEbcdicCodePage;
			}
		}

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x060058C8 RID: 22728 RVA: 0x0012B014 File Offset: 0x00129214
		internal bool IsRightToLeft
		{
			get
			{
				return this.isRightToLeft;
			}
		}

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x060058C9 RID: 22729 RVA: 0x0012B01C File Offset: 0x0012921C
		internal string SLIST
		{
			get
			{
				return this.sListSeparator;
			}
		}

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x060058CA RID: 22730 RVA: 0x0012B024 File Offset: 0x00129224
		internal bool UseUserOverride
		{
			get
			{
				return this.bUseOverrides;
			}
		}

		// Token: 0x060058CB RID: 22731 RVA: 0x0012B02C File Offset: 0x0012922C
		internal string CalendarName(int calendarId)
		{
			return this.GetCalendar(calendarId).sNativeName;
		}

		// Token: 0x060058CC RID: 22732 RVA: 0x0012B03A File Offset: 0x0012923A
		internal string[] EraNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saEraNames;
		}

		// Token: 0x060058CD RID: 22733 RVA: 0x0012B048 File Offset: 0x00129248
		internal string[] AbbrevEraNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevEraNames;
		}

		// Token: 0x060058CE RID: 22734 RVA: 0x0012B056 File Offset: 0x00129256
		internal string[] AbbreviatedEnglishEraNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevEnglishEraNames;
		}

		// Token: 0x060058CF RID: 22735 RVA: 0x0012B064 File Offset: 0x00129264
		internal string[] ShortDates(int calendarId)
		{
			return this.GetCalendar(calendarId).saShortDates;
		}

		// Token: 0x060058D0 RID: 22736 RVA: 0x0012B072 File Offset: 0x00129272
		internal string[] LongDates(int calendarId)
		{
			return this.GetCalendar(calendarId).saLongDates;
		}

		// Token: 0x060058D1 RID: 22737 RVA: 0x0012B080 File Offset: 0x00129280
		internal string[] YearMonths(int calendarId)
		{
			return this.GetCalendar(calendarId).saYearMonths;
		}

		// Token: 0x060058D2 RID: 22738 RVA: 0x0012B08E File Offset: 0x0012928E
		internal string[] DayNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saDayNames;
		}

		// Token: 0x060058D3 RID: 22739 RVA: 0x0012B09C File Offset: 0x0012929C
		internal string[] AbbreviatedDayNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevDayNames;
		}

		// Token: 0x060058D4 RID: 22740 RVA: 0x0012B0AA File Offset: 0x001292AA
		internal string[] SuperShortDayNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saSuperShortDayNames;
		}

		// Token: 0x060058D5 RID: 22741 RVA: 0x0012B0B8 File Offset: 0x001292B8
		internal string[] MonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saMonthNames;
		}

		// Token: 0x060058D6 RID: 22742 RVA: 0x0012B0C6 File Offset: 0x001292C6
		internal string[] GenitiveMonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saMonthGenitiveNames;
		}

		// Token: 0x060058D7 RID: 22743 RVA: 0x0012B0D4 File Offset: 0x001292D4
		internal string[] AbbreviatedMonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevMonthNames;
		}

		// Token: 0x060058D8 RID: 22744 RVA: 0x0012B0E2 File Offset: 0x001292E2
		internal string[] AbbreviatedGenitiveMonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevMonthGenitiveNames;
		}

		// Token: 0x060058D9 RID: 22745 RVA: 0x0012B0F0 File Offset: 0x001292F0
		internal string[] LeapYearMonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saLeapYearMonthNames;
		}

		// Token: 0x060058DA RID: 22746 RVA: 0x0012B0FE File Offset: 0x001292FE
		internal string MonthDay(int calendarId)
		{
			return this.GetCalendar(calendarId).sMonthDay;
		}

		// Token: 0x060058DB RID: 22747 RVA: 0x0012B10C File Offset: 0x0012930C
		internal string DateSeparator(int calendarId)
		{
			if (calendarId == 3 && !AppContextSwitches.EnforceLegacyJapaneseDateParsing)
			{
				return "/";
			}
			return CultureData.GetDateSeparator(this.ShortDates(calendarId)[0]);
		}

		// Token: 0x060058DC RID: 22748 RVA: 0x0012B12D File Offset: 0x0012932D
		private static string GetDateSeparator(string format)
		{
			return CultureData.GetSeparator(format, "dyM");
		}

		// Token: 0x060058DD RID: 22749 RVA: 0x0012B13C File Offset: 0x0012933C
		private static string GetSeparator(string format, string timeParts)
		{
			int num = CultureData.IndexOfTimePart(format, 0, timeParts);
			if (num != -1)
			{
				char c = format[num];
				do
				{
					num++;
				}
				while (num < format.Length && format[num] == c);
				int num2 = num;
				if (num2 < format.Length)
				{
					int num3 = CultureData.IndexOfTimePart(format, num2, timeParts);
					if (num3 != -1)
					{
						return CultureData.UnescapeNlsString(format, num2, num3 - 1);
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x060058DE RID: 22750 RVA: 0x0012B1A0 File Offset: 0x001293A0
		private static int IndexOfTimePart(string format, int startIndex, string timeParts)
		{
			bool flag = false;
			for (int i = startIndex; i < format.Length; i++)
			{
				if (!flag && timeParts.IndexOf(format[i]) != -1)
				{
					return i;
				}
				char c = format[i];
				if (c != '\'')
				{
					if (c == '\\' && i + 1 < format.Length)
					{
						i++;
						char c2 = format[i];
						if (c2 != '\'' && c2 != '\\')
						{
							i--;
						}
					}
				}
				else
				{
					flag = !flag;
				}
			}
			return -1;
		}

		// Token: 0x060058DF RID: 22751 RVA: 0x0012B214 File Offset: 0x00129414
		private static string UnescapeNlsString(string str, int start, int end)
		{
			StringBuilder stringBuilder = null;
			int num = start;
			while (num < str.Length && num <= end)
			{
				char c = str[num];
				if (c != '\'')
				{
					if (c != '\\')
					{
						if (stringBuilder != null)
						{
							stringBuilder.Append(str[num]);
						}
					}
					else
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(str, start, num - start, str.Length);
						}
						num++;
						if (num < str.Length)
						{
							stringBuilder.Append(str[num]);
						}
					}
				}
				else if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(str, start, num - start, str.Length);
				}
				num++;
			}
			if (stringBuilder == null)
			{
				return str.Substring(start, end - start + 1);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060058E0 RID: 22752 RVA: 0x0000270D File Offset: 0x0000090D
		internal static string[] ReescapeWin32Strings(string[] array)
		{
			return array;
		}

		// Token: 0x060058E1 RID: 22753 RVA: 0x0000270D File Offset: 0x0000090D
		internal static string ReescapeWin32String(string str)
		{
			return str;
		}

		// Token: 0x060058E2 RID: 22754 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		internal static bool IsCustomCultureId(int cultureId)
		{
			return false;
		}

		// Token: 0x060058E3 RID: 22755 RVA: 0x0012B2BC File Offset: 0x001294BC
		private unsafe static int strlen(byte* s)
		{
			int num = 0;
			while (s[num] != 0)
			{
				num++;
			}
			return num;
		}

		// Token: 0x060058E4 RID: 22756 RVA: 0x0012B2D8 File Offset: 0x001294D8
		private unsafe static string idx2string(byte* data, int idx)
		{
			return Encoding.UTF8.GetString(data + idx, CultureData.strlen(data + idx));
		}

		// Token: 0x060058E5 RID: 22757 RVA: 0x0012B2EF File Offset: 0x001294EF
		private int[] create_group_sizes_array(int gs0, int gs1)
		{
			if (gs0 == -1)
			{
				return new int[0];
			}
			if (gs1 != -1)
			{
				return new int[]
				{
					gs0,
					gs1
				};
			}
			return new int[]
			{
				gs0
			};
		}

		// Token: 0x060058E6 RID: 22758 RVA: 0x0012B31C File Offset: 0x0012951C
		internal unsafe void GetNFIValues(NumberFormatInfo nfi)
		{
			if (!this.IsInvariantCulture)
			{
				CultureData.NumberFormatEntryManaged numberFormatEntryManaged = default(CultureData.NumberFormatEntryManaged);
				byte* data = CultureData.fill_number_data(this.numberIndex, ref numberFormatEntryManaged);
				nfi.currencyGroupSizes = this.create_group_sizes_array(numberFormatEntryManaged.currency_group_sizes0, numberFormatEntryManaged.currency_group_sizes1);
				nfi.numberGroupSizes = this.create_group_sizes_array(numberFormatEntryManaged.number_group_sizes0, numberFormatEntryManaged.number_group_sizes1);
				nfi.NaNSymbol = CultureData.idx2string(data, numberFormatEntryManaged.nan_symbol);
				nfi.currencyDecimalDigits = numberFormatEntryManaged.currency_decimal_digits;
				nfi.currencyDecimalSeparator = CultureData.idx2string(data, numberFormatEntryManaged.currency_decimal_separator);
				nfi.currencyGroupSeparator = CultureData.idx2string(data, numberFormatEntryManaged.currency_group_separator);
				nfi.currencyNegativePattern = numberFormatEntryManaged.currency_negative_pattern;
				nfi.currencyPositivePattern = numberFormatEntryManaged.currency_positive_pattern;
				nfi.currencySymbol = CultureData.idx2string(data, numberFormatEntryManaged.currency_symbol);
				nfi.negativeInfinitySymbol = CultureData.idx2string(data, numberFormatEntryManaged.negative_infinity_symbol);
				nfi.negativeSign = CultureData.idx2string(data, numberFormatEntryManaged.negative_sign);
				nfi.numberDecimalDigits = numberFormatEntryManaged.number_decimal_digits;
				nfi.numberDecimalSeparator = CultureData.idx2string(data, numberFormatEntryManaged.number_decimal_separator);
				nfi.numberGroupSeparator = CultureData.idx2string(data, numberFormatEntryManaged.number_group_separator);
				nfi.numberNegativePattern = numberFormatEntryManaged.number_negative_pattern;
				nfi.perMilleSymbol = CultureData.idx2string(data, numberFormatEntryManaged.per_mille_symbol);
				nfi.percentNegativePattern = numberFormatEntryManaged.percent_negative_pattern;
				nfi.percentPositivePattern = numberFormatEntryManaged.percent_positive_pattern;
				nfi.percentSymbol = CultureData.idx2string(data, numberFormatEntryManaged.percent_symbol);
				nfi.positiveInfinitySymbol = CultureData.idx2string(data, numberFormatEntryManaged.positive_infinity_symbol);
				nfi.positiveSign = CultureData.idx2string(data, numberFormatEntryManaged.positive_sign);
			}
			nfi.percentDecimalDigits = nfi.numberDecimalDigits;
			nfi.percentDecimalSeparator = nfi.numberDecimalSeparator;
			nfi.percentGroupSizes = nfi.numberGroupSizes;
			nfi.percentGroupSeparator = nfi.numberGroupSeparator;
		}

		// Token: 0x060058E7 RID: 22759
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern byte* fill_number_data(int index, ref CultureData.NumberFormatEntryManaged nfe);

		// Token: 0x040036CB RID: 14027
		private string sAM1159;

		// Token: 0x040036CC RID: 14028
		private string sPM2359;

		// Token: 0x040036CD RID: 14029
		private string sTimeSeparator;

		// Token: 0x040036CE RID: 14030
		private volatile string[] saLongTimes;

		// Token: 0x040036CF RID: 14031
		private volatile string[] saShortTimes;

		// Token: 0x040036D0 RID: 14032
		private int iFirstDayOfWeek;

		// Token: 0x040036D1 RID: 14033
		private int iFirstWeekOfYear;

		// Token: 0x040036D2 RID: 14034
		private volatile int[] waCalendars;

		// Token: 0x040036D3 RID: 14035
		private CalendarData[] calendars;

		// Token: 0x040036D4 RID: 14036
		private string sISO639Language;

		// Token: 0x040036D5 RID: 14037
		private readonly string sRealName;

		// Token: 0x040036D6 RID: 14038
		private bool bUseOverrides;

		// Token: 0x040036D7 RID: 14039
		private int calendarId;

		// Token: 0x040036D8 RID: 14040
		private int numberIndex;

		// Token: 0x040036D9 RID: 14041
		private int iDefaultAnsiCodePage;

		// Token: 0x040036DA RID: 14042
		private int iDefaultOemCodePage;

		// Token: 0x040036DB RID: 14043
		private int iDefaultMacCodePage;

		// Token: 0x040036DC RID: 14044
		private int iDefaultEbcdicCodePage;

		// Token: 0x040036DD RID: 14045
		private bool isRightToLeft;

		// Token: 0x040036DE RID: 14046
		private string sListSeparator;

		// Token: 0x040036DF RID: 14047
		private static CultureData s_Invariant;

		// Token: 0x020009A0 RID: 2464
		internal struct NumberFormatEntryManaged
		{
			// Token: 0x040036E0 RID: 14048
			internal int currency_decimal_digits;

			// Token: 0x040036E1 RID: 14049
			internal int currency_decimal_separator;

			// Token: 0x040036E2 RID: 14050
			internal int currency_group_separator;

			// Token: 0x040036E3 RID: 14051
			internal int currency_group_sizes0;

			// Token: 0x040036E4 RID: 14052
			internal int currency_group_sizes1;

			// Token: 0x040036E5 RID: 14053
			internal int currency_negative_pattern;

			// Token: 0x040036E6 RID: 14054
			internal int currency_positive_pattern;

			// Token: 0x040036E7 RID: 14055
			internal int currency_symbol;

			// Token: 0x040036E8 RID: 14056
			internal int nan_symbol;

			// Token: 0x040036E9 RID: 14057
			internal int negative_infinity_symbol;

			// Token: 0x040036EA RID: 14058
			internal int negative_sign;

			// Token: 0x040036EB RID: 14059
			internal int number_decimal_digits;

			// Token: 0x040036EC RID: 14060
			internal int number_decimal_separator;

			// Token: 0x040036ED RID: 14061
			internal int number_group_separator;

			// Token: 0x040036EE RID: 14062
			internal int number_group_sizes0;

			// Token: 0x040036EF RID: 14063
			internal int number_group_sizes1;

			// Token: 0x040036F0 RID: 14064
			internal int number_negative_pattern;

			// Token: 0x040036F1 RID: 14065
			internal int per_mille_symbol;

			// Token: 0x040036F2 RID: 14066
			internal int percent_negative_pattern;

			// Token: 0x040036F3 RID: 14067
			internal int percent_positive_pattern;

			// Token: 0x040036F4 RID: 14068
			internal int percent_symbol;

			// Token: 0x040036F5 RID: 14069
			internal int positive_infinity_symbol;

			// Token: 0x040036F6 RID: 14070
			internal int positive_sign;
		}
	}
}
