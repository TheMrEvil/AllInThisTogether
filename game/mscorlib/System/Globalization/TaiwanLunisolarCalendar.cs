using System;

namespace System.Globalization
{
	/// <summary>Represents the Taiwan lunisolar calendar. As for the Taiwan calendar, years are calculated using the Gregorian calendar, while days and months are calculated using the lunisolar calendar.</summary>
	// Token: 0x02000997 RID: 2455
	[Serializable]
	public class TaiwanLunisolarCalendar : EastAsianLunisolarCalendar
	{
		/// <summary>Gets the minimum date and time supported by the <see cref="T:System.Globalization.TaiwanLunisolarCalendar" /> class.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.TaiwanLunisolarCalendar" /> class, which is equivalent to the first moment of February 18, 1912 C.E. in the Gregorian calendar.</returns>
		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06005806 RID: 22534 RVA: 0x00128DF0 File Offset: 0x00126FF0
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return TaiwanLunisolarCalendar.minDate;
			}
		}

		/// <summary>Gets the maximum date and time supported by the <see cref="T:System.Globalization.TaiwanLunisolarCalendar" /> class.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.TaiwanLunisolarCalendar" /> class, which is equivalent to the last moment of February 10, 2051 C.E. in the Gregorian calendar.</returns>
		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06005807 RID: 22535 RVA: 0x00128DF7 File Offset: 0x00126FF7
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return TaiwanLunisolarCalendar.maxDate;
			}
		}

		/// <summary>Gets the number of days in the year that precedes the year specified by the <see cref="P:System.Globalization.TaiwanLunisolarCalendar.MinSupportedDateTime" /> property.</summary>
		/// <returns>The number of days in the year that precedes the year specified by <see cref="P:System.Globalization.TaiwanLunisolarCalendar.MinSupportedDateTime" />.</returns>
		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06005808 RID: 22536 RVA: 0x001237EF File Offset: 0x001219EF
		protected override int DaysInYearBeforeMinSupportedYear
		{
			get
			{
				return 384;
			}
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06005809 RID: 22537 RVA: 0x00128DFE File Offset: 0x00126FFE
		internal override int MinCalendarYear
		{
			get
			{
				return 1912;
			}
		}

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x0600580A RID: 22538 RVA: 0x00127A28 File Offset: 0x00125C28
		internal override int MaxCalendarYear
		{
			get
			{
				return 2050;
			}
		}

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x0600580B RID: 22539 RVA: 0x00128DF0 File Offset: 0x00126FF0
		internal override DateTime MinDate
		{
			get
			{
				return TaiwanLunisolarCalendar.minDate;
			}
		}

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x0600580C RID: 22540 RVA: 0x00128DF7 File Offset: 0x00126FF7
		internal override DateTime MaxDate
		{
			get
			{
				return TaiwanLunisolarCalendar.maxDate;
			}
		}

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x0600580D RID: 22541 RVA: 0x00128E05 File Offset: 0x00127005
		internal override EraInfo[] CalEraInfo
		{
			get
			{
				return TaiwanLunisolarCalendar.taiwanLunisolarEraInfo;
			}
		}

		// Token: 0x0600580E RID: 22542 RVA: 0x00128E0C File Offset: 0x0012700C
		internal override int GetYearInfo(int LunarYear, int Index)
		{
			if (LunarYear < 1912 || LunarYear > 2050)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1912, 2050));
			}
			return TaiwanLunisolarCalendar.yinfo[LunarYear - 1912, Index];
		}

		// Token: 0x0600580F RID: 22543 RVA: 0x00128E6E File Offset: 0x0012706E
		internal override int GetYear(int year, DateTime time)
		{
			return this.helper.GetYear(year, time);
		}

		// Token: 0x06005810 RID: 22544 RVA: 0x00128E7D File Offset: 0x0012707D
		internal override int GetGregorianYear(int year, int era)
		{
			return this.helper.GetGregorianYear(year, era);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.TaiwanLunisolarCalendar" /> class.</summary>
		// Token: 0x06005811 RID: 22545 RVA: 0x00128E8C File Offset: 0x0012708C
		public TaiwanLunisolarCalendar()
		{
			this.helper = new GregorianCalendarHelper(this, TaiwanLunisolarCalendar.taiwanLunisolarEraInfo);
		}

		/// <summary>Retrieves the era that corresponds to the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer that represents the era specified in the <paramref name="time" /> parameter.</returns>
		// Token: 0x06005812 RID: 22546 RVA: 0x00128EA5 File Offset: 0x001270A5
		public override int GetEra(DateTime time)
		{
			return this.helper.GetEra(time);
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06005813 RID: 22547 RVA: 0x00022807 File Offset: 0x00020A07
		internal override int BaseCalendarID
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06005814 RID: 22548 RVA: 0x00128EB3 File Offset: 0x001270B3
		internal override int ID
		{
			get
			{
				return 21;
			}
		}

		/// <summary>Gets the eras that are relevant to the current <see cref="T:System.Globalization.TaiwanLunisolarCalendar" /> object.</summary>
		/// <returns>An array that consists of a single element having a value that is always the current era.</returns>
		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06005815 RID: 22549 RVA: 0x00128EB7 File Offset: 0x001270B7
		public override int[] Eras
		{
			get
			{
				return this.helper.Eras;
			}
		}

		// Token: 0x06005816 RID: 22550 RVA: 0x00128EC4 File Offset: 0x001270C4
		// Note: this type is marked as 'beforefieldinit'.
		static TaiwanLunisolarCalendar()
		{
		}

		// Token: 0x04003693 RID: 13971
		internal static EraInfo[] taiwanLunisolarEraInfo = new EraInfo[]
		{
			new EraInfo(1, 1912, 1, 1, 1911, 1, 8088)
		};

		// Token: 0x04003694 RID: 13972
		internal GregorianCalendarHelper helper;

		// Token: 0x04003695 RID: 13973
		internal const int MIN_LUNISOLAR_YEAR = 1912;

		// Token: 0x04003696 RID: 13974
		internal const int MAX_LUNISOLAR_YEAR = 2050;

		// Token: 0x04003697 RID: 13975
		internal const int MIN_GREGORIAN_YEAR = 1912;

		// Token: 0x04003698 RID: 13976
		internal const int MIN_GREGORIAN_MONTH = 2;

		// Token: 0x04003699 RID: 13977
		internal const int MIN_GREGORIAN_DAY = 18;

		// Token: 0x0400369A RID: 13978
		internal const int MAX_GREGORIAN_YEAR = 2051;

		// Token: 0x0400369B RID: 13979
		internal const int MAX_GREGORIAN_MONTH = 2;

		// Token: 0x0400369C RID: 13980
		internal const int MAX_GREGORIAN_DAY = 10;

		// Token: 0x0400369D RID: 13981
		internal static DateTime minDate = new DateTime(1912, 2, 18);

		// Token: 0x0400369E RID: 13982
		internal static DateTime maxDate = new DateTime(new DateTime(2051, 2, 10, 23, 59, 59, 999).Ticks + 9999L);

		// Token: 0x0400369F RID: 13983
		private static readonly int[,] yinfo = new int[,]
		{
			{
				0,
				2,
				18,
				42192
			},
			{
				0,
				2,
				6,
				53840
			},
			{
				5,
				1,
				26,
				54568
			},
			{
				0,
				2,
				14,
				46400
			},
			{
				0,
				2,
				3,
				54944
			},
			{
				2,
				1,
				23,
				38608
			},
			{
				0,
				2,
				11,
				38320
			},
			{
				7,
				2,
				1,
				18872
			},
			{
				0,
				2,
				20,
				18800
			},
			{
				0,
				2,
				8,
				42160
			},
			{
				5,
				1,
				28,
				45656
			},
			{
				0,
				2,
				16,
				27216
			},
			{
				0,
				2,
				5,
				27968
			},
			{
				4,
				1,
				24,
				44456
			},
			{
				0,
				2,
				13,
				11104
			},
			{
				0,
				2,
				2,
				38256
			},
			{
				2,
				1,
				23,
				18808
			},
			{
				0,
				2,
				10,
				18800
			},
			{
				6,
				1,
				30,
				25776
			},
			{
				0,
				2,
				17,
				54432
			},
			{
				0,
				2,
				6,
				59984
			},
			{
				5,
				1,
				26,
				27976
			},
			{
				0,
				2,
				14,
				23248
			},
			{
				0,
				2,
				4,
				11104
			},
			{
				3,
				1,
				24,
				37744
			},
			{
				0,
				2,
				11,
				37600
			},
			{
				7,
				1,
				31,
				51560
			},
			{
				0,
				2,
				19,
				51536
			},
			{
				0,
				2,
				8,
				54432
			},
			{
				6,
				1,
				27,
				55888
			},
			{
				0,
				2,
				15,
				46416
			},
			{
				0,
				2,
				5,
				22176
			},
			{
				4,
				1,
				25,
				43736
			},
			{
				0,
				2,
				13,
				9680
			},
			{
				0,
				2,
				2,
				37584
			},
			{
				2,
				1,
				22,
				51544
			},
			{
				0,
				2,
				10,
				43344
			},
			{
				7,
				1,
				29,
				46248
			},
			{
				0,
				2,
				17,
				27808
			},
			{
				0,
				2,
				6,
				46416
			},
			{
				5,
				1,
				27,
				21928
			},
			{
				0,
				2,
				14,
				19872
			},
			{
				0,
				2,
				3,
				42416
			},
			{
				3,
				1,
				24,
				21176
			},
			{
				0,
				2,
				12,
				21168
			},
			{
				8,
				1,
				31,
				43344
			},
			{
				0,
				2,
				18,
				59728
			},
			{
				0,
				2,
				8,
				27296
			},
			{
				6,
				1,
				28,
				44368
			},
			{
				0,
				2,
				15,
				43856
			},
			{
				0,
				2,
				5,
				19296
			},
			{
				4,
				1,
				25,
				42352
			},
			{
				0,
				2,
				13,
				42352
			},
			{
				0,
				2,
				2,
				21088
			},
			{
				3,
				1,
				21,
				59696
			},
			{
				0,
				2,
				9,
				55632
			},
			{
				7,
				1,
				30,
				23208
			},
			{
				0,
				2,
				17,
				22176
			},
			{
				0,
				2,
				6,
				38608
			},
			{
				5,
				1,
				27,
				19176
			},
			{
				0,
				2,
				15,
				19152
			},
			{
				0,
				2,
				3,
				42192
			},
			{
				4,
				1,
				23,
				53864
			},
			{
				0,
				2,
				11,
				53840
			},
			{
				8,
				1,
				31,
				54568
			},
			{
				0,
				2,
				18,
				46400
			},
			{
				0,
				2,
				7,
				46752
			},
			{
				6,
				1,
				28,
				38608
			},
			{
				0,
				2,
				16,
				38320
			},
			{
				0,
				2,
				5,
				18864
			},
			{
				4,
				1,
				25,
				42168
			},
			{
				0,
				2,
				13,
				42160
			},
			{
				10,
				2,
				2,
				45656
			},
			{
				0,
				2,
				20,
				27216
			},
			{
				0,
				2,
				9,
				27968
			},
			{
				6,
				1,
				29,
				44448
			},
			{
				0,
				2,
				17,
				43872
			},
			{
				0,
				2,
				6,
				38256
			},
			{
				5,
				1,
				27,
				18808
			},
			{
				0,
				2,
				15,
				18800
			},
			{
				0,
				2,
				4,
				25776
			},
			{
				3,
				1,
				23,
				27216
			},
			{
				0,
				2,
				10,
				59984
			},
			{
				8,
				1,
				31,
				27432
			},
			{
				0,
				2,
				19,
				23232
			},
			{
				0,
				2,
				7,
				43872
			},
			{
				5,
				1,
				28,
				37736
			},
			{
				0,
				2,
				16,
				37600
			},
			{
				0,
				2,
				5,
				51552
			},
			{
				4,
				1,
				24,
				54440
			},
			{
				0,
				2,
				12,
				54432
			},
			{
				0,
				2,
				1,
				55888
			},
			{
				2,
				1,
				22,
				23208
			},
			{
				0,
				2,
				9,
				22176
			},
			{
				7,
				1,
				29,
				43736
			},
			{
				0,
				2,
				18,
				9680
			},
			{
				0,
				2,
				7,
				37584
			},
			{
				5,
				1,
				26,
				51544
			},
			{
				0,
				2,
				14,
				43344
			},
			{
				0,
				2,
				3,
				46240
			},
			{
				4,
				1,
				23,
				46416
			},
			{
				0,
				2,
				10,
				44368
			},
			{
				9,
				1,
				31,
				21928
			},
			{
				0,
				2,
				19,
				19360
			},
			{
				0,
				2,
				8,
				42416
			},
			{
				6,
				1,
				28,
				21176
			},
			{
				0,
				2,
				16,
				21168
			},
			{
				0,
				2,
				5,
				43312
			},
			{
				4,
				1,
				25,
				29864
			},
			{
				0,
				2,
				12,
				27296
			},
			{
				0,
				2,
				1,
				44368
			},
			{
				2,
				1,
				22,
				19880
			},
			{
				0,
				2,
				10,
				19296
			},
			{
				6,
				1,
				29,
				42352
			},
			{
				0,
				2,
				17,
				42208
			},
			{
				0,
				2,
				6,
				53856
			},
			{
				5,
				1,
				26,
				59696
			},
			{
				0,
				2,
				13,
				54576
			},
			{
				0,
				2,
				3,
				23200
			},
			{
				3,
				1,
				23,
				27472
			},
			{
				0,
				2,
				11,
				38608
			},
			{
				11,
				1,
				31,
				19176
			},
			{
				0,
				2,
				19,
				19152
			},
			{
				0,
				2,
				8,
				42192
			},
			{
				6,
				1,
				28,
				53848
			},
			{
				0,
				2,
				15,
				53840
			},
			{
				0,
				2,
				4,
				54560
			},
			{
				5,
				1,
				24,
				55968
			},
			{
				0,
				2,
				12,
				46496
			},
			{
				0,
				2,
				1,
				22224
			},
			{
				2,
				1,
				22,
				19160
			},
			{
				0,
				2,
				10,
				18864
			},
			{
				7,
				1,
				30,
				42168
			},
			{
				0,
				2,
				17,
				42160
			},
			{
				0,
				2,
				6,
				43600
			},
			{
				5,
				1,
				26,
				46376
			},
			{
				0,
				2,
				14,
				27936
			},
			{
				0,
				2,
				2,
				44448
			},
			{
				3,
				1,
				23,
				21936
			}
		};
	}
}
