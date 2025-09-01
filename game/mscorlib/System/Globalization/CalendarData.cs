using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x02000984 RID: 2436
	[StructLayout(LayoutKind.Sequential)]
	internal class CalendarData
	{
		// Token: 0x06005631 RID: 22065 RVA: 0x00122D3B File Offset: 0x00120F3B
		private CalendarData()
		{
		}

		// Token: 0x06005632 RID: 22066 RVA: 0x00122D50 File Offset: 0x00120F50
		static CalendarData()
		{
			CalendarData calendarData = new CalendarData();
			calendarData.sNativeName = "Gregorian Calendar";
			calendarData.iTwoDigitYearMax = 2029;
			calendarData.iCurrentEra = 1;
			calendarData.saShortDates = new string[]
			{
				"MM/dd/yyyy",
				"yyyy-MM-dd"
			};
			calendarData.saLongDates = new string[]
			{
				"dddd, dd MMMM yyyy"
			};
			calendarData.saYearMonths = new string[]
			{
				"yyyy MMMM"
			};
			calendarData.sMonthDay = "MMMM dd";
			calendarData.saEraNames = new string[]
			{
				"A.D."
			};
			calendarData.saAbbrevEraNames = new string[]
			{
				"AD"
			};
			calendarData.saAbbrevEnglishEraNames = new string[]
			{
				"AD"
			};
			calendarData.saDayNames = new string[]
			{
				"Sunday",
				"Monday",
				"Tuesday",
				"Wednesday",
				"Thursday",
				"Friday",
				"Saturday"
			};
			calendarData.saAbbrevDayNames = new string[]
			{
				"Sun",
				"Mon",
				"Tue",
				"Wed",
				"Thu",
				"Fri",
				"Sat"
			};
			calendarData.saSuperShortDayNames = new string[]
			{
				"Su",
				"Mo",
				"Tu",
				"We",
				"Th",
				"Fr",
				"Sa"
			};
			calendarData.saMonthNames = new string[]
			{
				"January",
				"February",
				"March",
				"April",
				"May",
				"June",
				"July",
				"August",
				"September",
				"October",
				"November",
				"December",
				string.Empty
			};
			calendarData.saAbbrevMonthNames = new string[]
			{
				"Jan",
				"Feb",
				"Mar",
				"Apr",
				"May",
				"Jun",
				"Jul",
				"Aug",
				"Sep",
				"Oct",
				"Nov",
				"Dec",
				string.Empty
			};
			calendarData.saMonthGenitiveNames = calendarData.saMonthNames;
			calendarData.saAbbrevMonthGenitiveNames = calendarData.saAbbrevMonthNames;
			calendarData.saLeapYearMonthNames = calendarData.saMonthNames;
			calendarData.bUseUserOverrides = false;
			CalendarData.Invariant = calendarData;
		}

		// Token: 0x06005633 RID: 22067 RVA: 0x001230EC File Offset: 0x001212EC
		internal CalendarData(string localeName, int calendarId, bool bUseUserOverrides)
		{
			this.bUseUserOverrides = bUseUserOverrides;
			if (!CalendarData.nativeGetCalendarData(this, localeName, calendarId))
			{
				if (this.sNativeName == null)
				{
					this.sNativeName = string.Empty;
				}
				if (this.saShortDates == null)
				{
					this.saShortDates = CalendarData.Invariant.saShortDates;
				}
				if (this.saYearMonths == null)
				{
					this.saYearMonths = CalendarData.Invariant.saYearMonths;
				}
				if (this.saLongDates == null)
				{
					this.saLongDates = CalendarData.Invariant.saLongDates;
				}
				if (this.sMonthDay == null)
				{
					this.sMonthDay = CalendarData.Invariant.sMonthDay;
				}
				if (this.saEraNames == null)
				{
					this.saEraNames = CalendarData.Invariant.saEraNames;
				}
				if (this.saAbbrevEraNames == null)
				{
					this.saAbbrevEraNames = CalendarData.Invariant.saAbbrevEraNames;
				}
				if (this.saAbbrevEnglishEraNames == null)
				{
					this.saAbbrevEnglishEraNames = CalendarData.Invariant.saAbbrevEnglishEraNames;
				}
				if (this.saDayNames == null)
				{
					this.saDayNames = CalendarData.Invariant.saDayNames;
				}
				if (this.saAbbrevDayNames == null)
				{
					this.saAbbrevDayNames = CalendarData.Invariant.saAbbrevDayNames;
				}
				if (this.saSuperShortDayNames == null)
				{
					this.saSuperShortDayNames = CalendarData.Invariant.saSuperShortDayNames;
				}
				if (this.saMonthNames == null)
				{
					this.saMonthNames = CalendarData.Invariant.saMonthNames;
				}
				if (this.saAbbrevMonthNames == null)
				{
					this.saAbbrevMonthNames = CalendarData.Invariant.saAbbrevMonthNames;
				}
			}
			this.saShortDates = CultureData.ReescapeWin32Strings(this.saShortDates);
			this.saLongDates = CultureData.ReescapeWin32Strings(this.saLongDates);
			this.saYearMonths = CultureData.ReescapeWin32Strings(this.saYearMonths);
			this.sMonthDay = CultureData.ReescapeWin32String(this.sMonthDay);
			if ((ushort)calendarId == 4)
			{
				if (CultureInfo.IsTaiwanSku)
				{
					this.sNativeName = "中華民國曆";
				}
				else
				{
					this.sNativeName = string.Empty;
				}
			}
			if (this.saMonthGenitiveNames == null || string.IsNullOrEmpty(this.saMonthGenitiveNames[0]))
			{
				this.saMonthGenitiveNames = this.saMonthNames;
			}
			if (this.saAbbrevMonthGenitiveNames == null || string.IsNullOrEmpty(this.saAbbrevMonthGenitiveNames[0]))
			{
				this.saAbbrevMonthGenitiveNames = this.saAbbrevMonthNames;
			}
			if (this.saLeapYearMonthNames == null || string.IsNullOrEmpty(this.saLeapYearMonthNames[0]))
			{
				this.saLeapYearMonthNames = this.saMonthNames;
			}
			this.InitializeEraNames(localeName, calendarId);
			this.InitializeAbbreviatedEraNames(localeName, calendarId);
			if (!GlobalizationMode.Invariant && calendarId == 3)
			{
				this.saAbbrevEnglishEraNames = CalendarData.GetJapaneseEnglishEraNames();
			}
			else
			{
				this.saAbbrevEnglishEraNames = new string[]
				{
					""
				};
			}
			this.iCurrentEra = this.saEraNames.Length;
		}

		// Token: 0x06005634 RID: 22068 RVA: 0x0012336C File Offset: 0x0012156C
		private void InitializeEraNames(string localeName, int calendarId)
		{
			switch ((ushort)calendarId)
			{
			case 1:
				if (this.saEraNames == null || this.saEraNames.Length == 0 || string.IsNullOrEmpty(this.saEraNames[0]))
				{
					this.saEraNames = new string[]
					{
						"A.D."
					};
					return;
				}
				return;
			case 2:
			case 13:
				this.saEraNames = new string[]
				{
					"A.D."
				};
				return;
			case 3:
			case 14:
				this.saEraNames = CalendarData.GetJapaneseEraNames();
				return;
			case 4:
				if (CultureInfo.IsTaiwanSku)
				{
					this.saEraNames = new string[]
					{
						"中華民國"
					};
					return;
				}
				this.saEraNames = new string[]
				{
					string.Empty
				};
				return;
			case 5:
				this.saEraNames = new string[]
				{
					"단기"
				};
				return;
			case 6:
			case 23:
				if (localeName == "dv-MV")
				{
					this.saEraNames = new string[]
					{
						"ހިޖްރީ"
					};
					return;
				}
				this.saEraNames = new string[]
				{
					"بعد الهجرة"
				};
				return;
			case 7:
				this.saEraNames = new string[]
				{
					"พ.ศ."
				};
				return;
			case 8:
				this.saEraNames = new string[]
				{
					"C.E."
				};
				return;
			case 9:
				this.saEraNames = new string[]
				{
					"ap. J.-C."
				};
				return;
			case 10:
			case 11:
			case 12:
				this.saEraNames = new string[]
				{
					"م"
				};
				return;
			case 22:
				if (this.saEraNames == null || this.saEraNames.Length == 0 || string.IsNullOrEmpty(this.saEraNames[0]))
				{
					this.saEraNames = new string[]
					{
						"ه.ش"
					};
					return;
				}
				return;
			}
			this.saEraNames = CalendarData.Invariant.saEraNames;
		}

		// Token: 0x06005635 RID: 22069 RVA: 0x00123554 File Offset: 0x00121754
		private static string[] GetJapaneseEraNames()
		{
			if (GlobalizationMode.Invariant)
			{
				throw new PlatformNotSupportedException();
			}
			return JapaneseCalendar.EraNames();
		}

		// Token: 0x06005636 RID: 22070 RVA: 0x00123568 File Offset: 0x00121768
		private static string[] GetJapaneseEnglishEraNames()
		{
			if (GlobalizationMode.Invariant)
			{
				throw new PlatformNotSupportedException();
			}
			return JapaneseCalendar.EnglishEraNames();
		}

		// Token: 0x06005637 RID: 22071 RVA: 0x0012357C File Offset: 0x0012177C
		private void InitializeAbbreviatedEraNames(string localeName, int calendarId)
		{
			CalendarId calendarId2 = (CalendarId)calendarId;
			if (calendarId2 <= CalendarId.JULIAN)
			{
				switch (calendarId2)
				{
				case CalendarId.GREGORIAN:
					if (this.saAbbrevEraNames == null || this.saAbbrevEraNames.Length == 0 || string.IsNullOrEmpty(this.saAbbrevEraNames[0]))
					{
						this.saAbbrevEraNames = new string[]
						{
							"AD"
						};
						return;
					}
					return;
				case CalendarId.GREGORIAN_US:
					break;
				case CalendarId.JAPAN:
					goto IL_96;
				case CalendarId.TAIWAN:
					this.saAbbrevEraNames = new string[1];
					if (this.saEraNames[0].Length == 4)
					{
						this.saAbbrevEraNames[0] = this.saEraNames[0].Substring(2, 2);
						return;
					}
					this.saAbbrevEraNames[0] = this.saEraNames[0];
					return;
				case CalendarId.KOREA:
					goto IL_159;
				case CalendarId.HIJRI:
					goto IL_B0;
				default:
					if (calendarId2 != CalendarId.JULIAN)
					{
						goto IL_159;
					}
					break;
				}
				this.saAbbrevEraNames = new string[]
				{
					"AD"
				};
				return;
			}
			if (calendarId2 != CalendarId.JAPANESELUNISOLAR)
			{
				if (calendarId2 != CalendarId.PERSIAN)
				{
					if (calendarId2 != CalendarId.UMALQURA)
					{
						goto IL_159;
					}
					goto IL_B0;
				}
				else
				{
					if (this.saAbbrevEraNames == null || this.saAbbrevEraNames.Length == 0 || string.IsNullOrEmpty(this.saAbbrevEraNames[0]))
					{
						this.saAbbrevEraNames = this.saEraNames;
						return;
					}
					return;
				}
			}
			IL_96:
			if (GlobalizationMode.Invariant)
			{
				throw new PlatformNotSupportedException();
			}
			this.saAbbrevEraNames = this.saEraNames;
			return;
			IL_B0:
			if (localeName == "dv-MV")
			{
				this.saAbbrevEraNames = new string[]
				{
					"ހ."
				};
				return;
			}
			this.saAbbrevEraNames = new string[]
			{
				"هـ"
			};
			return;
			IL_159:
			this.saAbbrevEraNames = this.saEraNames;
		}

		// Token: 0x06005638 RID: 22072 RVA: 0x001236EE File Offset: 0x001218EE
		internal static CalendarData GetCalendarData(int calendarId)
		{
			return CultureInfo.GetCultureInfo(CalendarData.CalendarIdToCultureName(calendarId)).m_cultureData.GetCalendar(calendarId);
		}

		// Token: 0x06005639 RID: 22073 RVA: 0x00123708 File Offset: 0x00121908
		private static string CalendarIdToCultureName(int calendarId)
		{
			switch (calendarId)
			{
			case 2:
				return "fa-IR";
			case 3:
				return "ja-JP";
			case 4:
				return "zh-TW";
			case 5:
				return "ko-KR";
			case 6:
			case 10:
			case 23:
				return "ar-SA";
			case 7:
				return "th-TH";
			case 8:
				return "he-IL";
			case 9:
				return "ar-DZ";
			case 11:
			case 12:
				return "ar-IQ";
			}
			return "en-US";
		}

		// Token: 0x0600563A RID: 22074 RVA: 0x0012275A File Offset: 0x0012095A
		public static int nativeGetTwoDigitYearMax(int calID)
		{
			return -1;
		}

		// Token: 0x0600563B RID: 22075 RVA: 0x001237B2 File Offset: 0x001219B2
		private static bool nativeGetCalendarData(CalendarData data, string localeName, int calendarId)
		{
			if (data.fill_calendar_data(localeName.ToLowerInvariant(), calendarId))
			{
				if ((ushort)calendarId == 8)
				{
					data.saMonthNames = CalendarData.HEBREW_MONTH_NAMES;
					data.saLeapYearMonthNames = CalendarData.HEBREW_LEAP_MONTH_NAMES;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600563C RID: 22076
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool fill_calendar_data(string localeName, int datetimeIndex);

		// Token: 0x040035B0 RID: 13744
		internal const int MAX_CALENDARS = 23;

		// Token: 0x040035B1 RID: 13745
		internal string sNativeName;

		// Token: 0x040035B2 RID: 13746
		internal string[] saShortDates;

		// Token: 0x040035B3 RID: 13747
		internal string[] saYearMonths;

		// Token: 0x040035B4 RID: 13748
		internal string[] saLongDates;

		// Token: 0x040035B5 RID: 13749
		internal string sMonthDay;

		// Token: 0x040035B6 RID: 13750
		internal string[] saEraNames;

		// Token: 0x040035B7 RID: 13751
		internal string[] saAbbrevEraNames;

		// Token: 0x040035B8 RID: 13752
		internal string[] saAbbrevEnglishEraNames;

		// Token: 0x040035B9 RID: 13753
		internal string[] saDayNames;

		// Token: 0x040035BA RID: 13754
		internal string[] saAbbrevDayNames;

		// Token: 0x040035BB RID: 13755
		internal string[] saSuperShortDayNames;

		// Token: 0x040035BC RID: 13756
		internal string[] saMonthNames;

		// Token: 0x040035BD RID: 13757
		internal string[] saAbbrevMonthNames;

		// Token: 0x040035BE RID: 13758
		internal string[] saMonthGenitiveNames;

		// Token: 0x040035BF RID: 13759
		internal string[] saAbbrevMonthGenitiveNames;

		// Token: 0x040035C0 RID: 13760
		internal string[] saLeapYearMonthNames;

		// Token: 0x040035C1 RID: 13761
		internal int iTwoDigitYearMax = 2029;

		// Token: 0x040035C2 RID: 13762
		internal int iCurrentEra;

		// Token: 0x040035C3 RID: 13763
		internal bool bUseUserOverrides;

		// Token: 0x040035C4 RID: 13764
		internal static CalendarData Invariant;

		// Token: 0x040035C5 RID: 13765
		private static string[] HEBREW_MONTH_NAMES = new string[]
		{
			"תשרי",
			"חשון",
			"כסלו",
			"טבת",
			"שבט",
			"אדר",
			"אדר ב",
			"ניסן",
			"אייר",
			"סיון",
			"תמוז",
			"אב",
			"אלול"
		};

		// Token: 0x040035C6 RID: 13766
		private static string[] HEBREW_LEAP_MONTH_NAMES = new string[]
		{
			"תשרי",
			"חשון",
			"כסלו",
			"טבת",
			"שבט",
			"אדר א",
			"אדר ב",
			"ניסן",
			"אייר",
			"סיון",
			"תמוז",
			"אב",
			"אלול"
		};
	}
}
