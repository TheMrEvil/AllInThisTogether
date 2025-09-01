using System;

namespace System.Globalization
{
	/// <summary>Represents time in divisions, such as months, days, and years. Years are calculated as for the Japanese calendar, while days and months are calculated using the lunisolar calendar.</summary>
	// Token: 0x02000990 RID: 2448
	[Serializable]
	public class JapaneseLunisolarCalendar : EastAsianLunisolarCalendar
	{
		/// <summary>Gets the minimum date and time supported by the <see cref="T:System.Globalization.JapaneseLunisolarCalendar" /> class.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.JapaneseLunisolarCalendar" /> class, which is equivalent to the first moment of January 28, 1960 C.E. in the Gregorian calendar.</returns>
		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06005730 RID: 22320 RVA: 0x0012701F File Offset: 0x0012521F
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return JapaneseLunisolarCalendar.minDate;
			}
		}

		/// <summary>Gets the maximum date and time supported by the <see cref="T:System.Globalization.JapaneseLunisolarCalendar" /> class.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.JapaneseLunisolarCalendar" /> class, which is equivalent to the last moment of January 22, 2050 C.E. in the Gregorian calendar.</returns>
		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06005731 RID: 22321 RVA: 0x00127026 File Offset: 0x00125226
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return JapaneseLunisolarCalendar.maxDate;
			}
		}

		/// <summary>Gets the number of days in the year that precedes the year that is specified by the <see cref="P:System.Globalization.JapaneseLunisolarCalendar.MinSupportedDateTime" /> property.</summary>
		/// <returns>The number of days in the year that precedes the year specified by <see cref="P:System.Globalization.JapaneseLunisolarCalendar.MinSupportedDateTime" />.</returns>
		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06005732 RID: 22322 RVA: 0x001263B9 File Offset: 0x001245B9
		protected override int DaysInYearBeforeMinSupportedYear
		{
			get
			{
				return 354;
			}
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06005733 RID: 22323 RVA: 0x0012702D File Offset: 0x0012522D
		internal override int MinCalendarYear
		{
			get
			{
				return 1960;
			}
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06005734 RID: 22324 RVA: 0x00127034 File Offset: 0x00125234
		internal override int MaxCalendarYear
		{
			get
			{
				return 2049;
			}
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06005735 RID: 22325 RVA: 0x0012701F File Offset: 0x0012521F
		internal override DateTime MinDate
		{
			get
			{
				return JapaneseLunisolarCalendar.minDate;
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06005736 RID: 22326 RVA: 0x00127026 File Offset: 0x00125226
		internal override DateTime MaxDate
		{
			get
			{
				return JapaneseLunisolarCalendar.maxDate;
			}
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06005737 RID: 22327 RVA: 0x0012703B File Offset: 0x0012523B
		internal override EraInfo[] CalEraInfo
		{
			get
			{
				return JapaneseCalendar.GetEraInfo();
			}
		}

		// Token: 0x06005738 RID: 22328 RVA: 0x00127044 File Offset: 0x00125244
		internal override int GetYearInfo(int LunarYear, int Index)
		{
			if (LunarYear < 1960 || LunarYear > 2049)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1960, 2049));
			}
			return JapaneseLunisolarCalendar.yinfo[LunarYear - 1960, Index];
		}

		// Token: 0x06005739 RID: 22329 RVA: 0x001270A6 File Offset: 0x001252A6
		internal override int GetYear(int year, DateTime time)
		{
			return this.helper.GetYear(year, time);
		}

		// Token: 0x0600573A RID: 22330 RVA: 0x001270B5 File Offset: 0x001252B5
		internal override int GetGregorianYear(int year, int era)
		{
			return this.helper.GetGregorianYear(year, era);
		}

		// Token: 0x0600573B RID: 22331 RVA: 0x001270C4 File Offset: 0x001252C4
		private static EraInfo[] TrimEras(EraInfo[] baseEras)
		{
			EraInfo[] array = new EraInfo[baseEras.Length];
			int num = 0;
			for (int i = 0; i < baseEras.Length; i++)
			{
				if (baseEras[i].yearOffset + baseEras[i].minEraYear < 2049)
				{
					if (baseEras[i].yearOffset + baseEras[i].maxEraYear < 1960)
					{
						break;
					}
					array[num] = baseEras[i];
					num++;
				}
			}
			if (num == 0)
			{
				return baseEras;
			}
			Array.Resize<EraInfo>(ref array, num);
			return array;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.JapaneseLunisolarCalendar" /> class.</summary>
		// Token: 0x0600573C RID: 22332 RVA: 0x00127132 File Offset: 0x00125332
		public JapaneseLunisolarCalendar()
		{
			this.helper = new GregorianCalendarHelper(this, JapaneseLunisolarCalendar.TrimEras(JapaneseCalendar.GetEraInfo()));
		}

		/// <summary>Retrieves the era that corresponds to the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer that represents the era specified in the <paramref name="time" /> parameter.</returns>
		// Token: 0x0600573D RID: 22333 RVA: 0x00127150 File Offset: 0x00125350
		public override int GetEra(DateTime time)
		{
			return this.helper.GetEra(time);
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x0600573E RID: 22334 RVA: 0x000221D2 File Offset: 0x000203D2
		internal override int BaseCalendarID
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x0600573F RID: 22335 RVA: 0x000286A2 File Offset: 0x000268A2
		internal override int ID
		{
			get
			{
				return 14;
			}
		}

		/// <summary>Gets the eras that are relevant to the <see cref="T:System.Globalization.JapaneseLunisolarCalendar" /> object.</summary>
		/// <returns>An array of 32-bit signed integers that specify the relevant eras.</returns>
		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06005740 RID: 22336 RVA: 0x0012715E File Offset: 0x0012535E
		public override int[] Eras
		{
			get
			{
				return this.helper.Eras;
			}
		}

		// Token: 0x06005741 RID: 22337 RVA: 0x0012716C File Offset: 0x0012536C
		// Note: this type is marked as 'beforefieldinit'.
		static JapaneseLunisolarCalendar()
		{
		}

		/// <summary>Specifies the current era.</summary>
		// Token: 0x04003641 RID: 13889
		public const int JapaneseEra = 1;

		// Token: 0x04003642 RID: 13890
		internal GregorianCalendarHelper helper;

		// Token: 0x04003643 RID: 13891
		internal const int MIN_LUNISOLAR_YEAR = 1960;

		// Token: 0x04003644 RID: 13892
		internal const int MAX_LUNISOLAR_YEAR = 2049;

		// Token: 0x04003645 RID: 13893
		internal const int MIN_GREGORIAN_YEAR = 1960;

		// Token: 0x04003646 RID: 13894
		internal const int MIN_GREGORIAN_MONTH = 1;

		// Token: 0x04003647 RID: 13895
		internal const int MIN_GREGORIAN_DAY = 28;

		// Token: 0x04003648 RID: 13896
		internal const int MAX_GREGORIAN_YEAR = 2050;

		// Token: 0x04003649 RID: 13897
		internal const int MAX_GREGORIAN_MONTH = 1;

		// Token: 0x0400364A RID: 13898
		internal const int MAX_GREGORIAN_DAY = 22;

		// Token: 0x0400364B RID: 13899
		internal static DateTime minDate = new DateTime(1960, 1, 28);

		// Token: 0x0400364C RID: 13900
		internal static DateTime maxDate = new DateTime(new DateTime(2050, 1, 22, 23, 59, 59, 999).Ticks + 9999L);

		// Token: 0x0400364D RID: 13901
		private static readonly int[,] yinfo = new int[,]
		{
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
				19808
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
				21104
			},
			{
				3,
				1,
				22,
				26928
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
				27304
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
				39632
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
				19168
			},
			{
				0,
				2,
				3,
				42208
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
				54600
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
				54944
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
				42200
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
				46504
			},
			{
				0,
				2,
				18,
				11104
			},
			{
				0,
				2,
				6,
				38320
			},
			{
				5,
				1,
				27,
				18872
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
				27976
			},
			{
				0,
				2,
				19,
				23248
			},
			{
				0,
				2,
				8,
				11104
			},
			{
				5,
				1,
				28,
				37744
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
				58536
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
				22208
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
				3,
				1,
				23,
				47696
			},
			{
				0,
				2,
				10,
				46416
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
				5,
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
				43344
			},
			{
				4,
				1,
				25,
				46248
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
				21928
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
				42352
			},
			{
				0,
				2,
				7,
				21104
			},
			{
				5,
				1,
				27,
				26928
			},
			{
				0,
				2,
				13,
				55600
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
				43856
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
				19168
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
				53864
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
				46752
			},
			{
				0,
				2,
				1,
				38608
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
				45648
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
				27968
			},
			{
				0,
				2,
				2,
				44448
			}
		};
	}
}
