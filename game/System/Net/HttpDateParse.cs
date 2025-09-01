using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x02000626 RID: 1574
	internal static class HttpDateParse
	{
		// Token: 0x060031CA RID: 12746 RVA: 0x000AC801 File Offset: 0x000AAA01
		private static char MAKE_UPPER(char c)
		{
			return char.ToUpper(c, CultureInfo.InvariantCulture);
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x000AC810 File Offset: 0x000AAA10
		private static int MapDayMonthToDword(char[] lpszDay, int index)
		{
			switch (HttpDateParse.MAKE_UPPER(lpszDay[index]))
			{
			case 'A':
			{
				char c = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c == 'P')
				{
					return 4;
				}
				if (c != 'U')
				{
					return -999;
				}
				return 8;
			}
			case 'D':
				return 12;
			case 'F':
			{
				char c = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c == 'E')
				{
					return 2;
				}
				if (c == 'R')
				{
					return 5;
				}
				return -999;
			}
			case 'G':
				return -1000;
			case 'J':
			{
				char c = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c != 'A')
				{
					if (c == 'U')
					{
						char c2 = HttpDateParse.MAKE_UPPER(lpszDay[index + 2]);
						if (c2 == 'L')
						{
							return 7;
						}
						if (c2 == 'N')
						{
							return 6;
						}
					}
					return -999;
				}
				return 1;
			}
			case 'M':
			{
				char c = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c != 'A')
				{
					if (c == 'O')
					{
						return 1;
					}
				}
				else
				{
					char c2 = HttpDateParse.MAKE_UPPER(lpszDay[index + 2]);
					if (c2 == 'R')
					{
						return 3;
					}
					if (c2 == 'Y')
					{
						return 5;
					}
				}
				return -999;
			}
			case 'N':
				return 11;
			case 'O':
				return 10;
			case 'S':
			{
				char c = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c == 'A')
				{
					return 6;
				}
				if (c == 'E')
				{
					return 9;
				}
				if (c != 'U')
				{
					return -999;
				}
				return 0;
			}
			case 'T':
			{
				char c = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c == 'H')
				{
					return 4;
				}
				if (c == 'U')
				{
					return 2;
				}
				return -999;
			}
			case 'U':
				return -1000;
			case 'W':
				return 3;
			}
			return -999;
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x000AC9A4 File Offset: 0x000AABA4
		public static bool ParseHttpDate(string DateString, out DateTime dtOut)
		{
			int num = 0;
			int num2 = 0;
			int num3 = -1;
			bool flag = false;
			int[] array = new int[8];
			bool result = true;
			char[] array2 = DateString.ToCharArray();
			dtOut = default(DateTime);
			while (num < DateString.Length && num2 < 8)
			{
				if (array2[num] >= '0' && array2[num] <= '9')
				{
					array[num2] = 0;
					do
					{
						array[num2] *= 10;
						array[num2] += (int)(array2[num] - '0');
						num++;
					}
					while (num < DateString.Length && array2[num] >= '0' && array2[num] <= '9');
					num2++;
				}
				else if ((array2[num] >= 'A' && array2[num] <= 'Z') || (array2[num] >= 'a' && array2[num] <= 'z'))
				{
					array[num2] = HttpDateParse.MapDayMonthToDword(array2, num);
					num3 = num2;
					if (array[num2] == -999 && (!flag || num2 != 6))
					{
						result = false;
						return result;
					}
					if (num2 == 1)
					{
						flag = true;
					}
					do
					{
						num++;
					}
					while (num < DateString.Length && ((array2[num] >= 'A' && array2[num] <= 'Z') || (array2[num] >= 'a' && array2[num] <= 'z')));
					num2++;
				}
				else
				{
					num++;
				}
			}
			int millisecond = 0;
			int num4;
			int month;
			int num5;
			int num6;
			int num7;
			int num8;
			if (flag)
			{
				num4 = array[2];
				month = array[1];
				num5 = array[3];
				num6 = array[4];
				num7 = array[5];
				if (num3 != 6)
				{
					num8 = array[6];
				}
				else
				{
					num8 = array[7];
				}
			}
			else
			{
				num4 = array[1];
				month = array[2];
				num8 = array[3];
				num5 = array[4];
				num6 = array[5];
				num7 = array[6];
			}
			if (num8 < 100)
			{
				num8 += ((num8 < 80) ? 2000 : 1900);
			}
			if (num2 < 4 || num4 > 31 || num5 > 23 || num6 > 59 || num7 > 59)
			{
				return false;
			}
			dtOut = new DateTime(num8, month, num4, num5, num6, num7, millisecond);
			if (num3 == 6)
			{
				dtOut = dtOut.ToUniversalTime();
			}
			if (num2 > 7 && array[7] != -1000)
			{
				double value = (double)array[7];
				dtOut.AddHours(value);
			}
			dtOut = dtOut.ToLocalTime();
			return result;
		}

		// Token: 0x04001CF8 RID: 7416
		private const int BASE_DEC = 10;

		// Token: 0x04001CF9 RID: 7417
		private const int DATE_INDEX_DAY_OF_WEEK = 0;

		// Token: 0x04001CFA RID: 7418
		private const int DATE_1123_INDEX_DAY = 1;

		// Token: 0x04001CFB RID: 7419
		private const int DATE_1123_INDEX_MONTH = 2;

		// Token: 0x04001CFC RID: 7420
		private const int DATE_1123_INDEX_YEAR = 3;

		// Token: 0x04001CFD RID: 7421
		private const int DATE_1123_INDEX_HRS = 4;

		// Token: 0x04001CFE RID: 7422
		private const int DATE_1123_INDEX_MINS = 5;

		// Token: 0x04001CFF RID: 7423
		private const int DATE_1123_INDEX_SECS = 6;

		// Token: 0x04001D00 RID: 7424
		private const int DATE_ANSI_INDEX_MONTH = 1;

		// Token: 0x04001D01 RID: 7425
		private const int DATE_ANSI_INDEX_DAY = 2;

		// Token: 0x04001D02 RID: 7426
		private const int DATE_ANSI_INDEX_HRS = 3;

		// Token: 0x04001D03 RID: 7427
		private const int DATE_ANSI_INDEX_MINS = 4;

		// Token: 0x04001D04 RID: 7428
		private const int DATE_ANSI_INDEX_SECS = 5;

		// Token: 0x04001D05 RID: 7429
		private const int DATE_ANSI_INDEX_YEAR = 6;

		// Token: 0x04001D06 RID: 7430
		private const int DATE_INDEX_TZ = 7;

		// Token: 0x04001D07 RID: 7431
		private const int DATE_INDEX_LAST = 7;

		// Token: 0x04001D08 RID: 7432
		private const int MAX_FIELD_DATE_ENTRIES = 8;

		// Token: 0x04001D09 RID: 7433
		private const int DATE_TOKEN_JANUARY = 1;

		// Token: 0x04001D0A RID: 7434
		private const int DATE_TOKEN_FEBRUARY = 2;

		// Token: 0x04001D0B RID: 7435
		private const int DATE_TOKEN_Microsoft = 3;

		// Token: 0x04001D0C RID: 7436
		private const int DATE_TOKEN_APRIL = 4;

		// Token: 0x04001D0D RID: 7437
		private const int DATE_TOKEN_MAY = 5;

		// Token: 0x04001D0E RID: 7438
		private const int DATE_TOKEN_JUNE = 6;

		// Token: 0x04001D0F RID: 7439
		private const int DATE_TOKEN_JULY = 7;

		// Token: 0x04001D10 RID: 7440
		private const int DATE_TOKEN_AUGUST = 8;

		// Token: 0x04001D11 RID: 7441
		private const int DATE_TOKEN_SEPTEMBER = 9;

		// Token: 0x04001D12 RID: 7442
		private const int DATE_TOKEN_OCTOBER = 10;

		// Token: 0x04001D13 RID: 7443
		private const int DATE_TOKEN_NOVEMBER = 11;

		// Token: 0x04001D14 RID: 7444
		private const int DATE_TOKEN_DECEMBER = 12;

		// Token: 0x04001D15 RID: 7445
		private const int DATE_TOKEN_LAST_MONTH = 13;

		// Token: 0x04001D16 RID: 7446
		private const int DATE_TOKEN_SUNDAY = 0;

		// Token: 0x04001D17 RID: 7447
		private const int DATE_TOKEN_MONDAY = 1;

		// Token: 0x04001D18 RID: 7448
		private const int DATE_TOKEN_TUESDAY = 2;

		// Token: 0x04001D19 RID: 7449
		private const int DATE_TOKEN_WEDNESDAY = 3;

		// Token: 0x04001D1A RID: 7450
		private const int DATE_TOKEN_THURSDAY = 4;

		// Token: 0x04001D1B RID: 7451
		private const int DATE_TOKEN_FRIDAY = 5;

		// Token: 0x04001D1C RID: 7452
		private const int DATE_TOKEN_SATURDAY = 6;

		// Token: 0x04001D1D RID: 7453
		private const int DATE_TOKEN_LAST_DAY = 7;

		// Token: 0x04001D1E RID: 7454
		private const int DATE_TOKEN_GMT = -1000;

		// Token: 0x04001D1F RID: 7455
		private const int DATE_TOKEN_LAST = -1000;

		// Token: 0x04001D20 RID: 7456
		private const int DATE_TOKEN_ERROR = -999;
	}
}
