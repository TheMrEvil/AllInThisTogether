using System;

namespace System.Globalization
{
	// Token: 0x0200097F RID: 2431
	internal class CalendricalCalculationsHelper
	{
		// Token: 0x060055A8 RID: 21928 RVA: 0x00121234 File Offset: 0x0011F434
		private static double RadiansFromDegrees(double degree)
		{
			return degree * 3.141592653589793 / 180.0;
		}

		// Token: 0x060055A9 RID: 21929 RVA: 0x0012124B File Offset: 0x0011F44B
		private static double SinOfDegree(double degree)
		{
			return Math.Sin(CalendricalCalculationsHelper.RadiansFromDegrees(degree));
		}

		// Token: 0x060055AA RID: 21930 RVA: 0x00121258 File Offset: 0x0011F458
		private static double CosOfDegree(double degree)
		{
			return Math.Cos(CalendricalCalculationsHelper.RadiansFromDegrees(degree));
		}

		// Token: 0x060055AB RID: 21931 RVA: 0x00121265 File Offset: 0x0011F465
		private static double TanOfDegree(double degree)
		{
			return Math.Tan(CalendricalCalculationsHelper.RadiansFromDegrees(degree));
		}

		// Token: 0x060055AC RID: 21932 RVA: 0x00121272 File Offset: 0x0011F472
		public static double Angle(int degrees, int minutes, double seconds)
		{
			return (seconds / 60.0 + (double)minutes) / 60.0 + (double)degrees;
		}

		// Token: 0x060055AD RID: 21933 RVA: 0x0012128F File Offset: 0x0011F48F
		private static double Obliquity(double julianCenturies)
		{
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients, julianCenturies);
		}

		// Token: 0x060055AE RID: 21934 RVA: 0x0012129C File Offset: 0x0011F49C
		internal static long GetNumberOfDays(DateTime date)
		{
			return date.Ticks / 864000000000L;
		}

		// Token: 0x060055AF RID: 21935 RVA: 0x001212B0 File Offset: 0x0011F4B0
		private static int GetGregorianYear(double numberOfDays)
		{
			return new DateTime(Math.Min((long)(Math.Floor(numberOfDays) * 864000000000.0), DateTime.MaxValue.Ticks)).Year;
		}

		// Token: 0x060055B0 RID: 21936 RVA: 0x001212EC File Offset: 0x0011F4EC
		private static double Reminder(double divisor, double dividend)
		{
			double num = Math.Floor(divisor / dividend);
			return divisor - dividend * num;
		}

		// Token: 0x060055B1 RID: 21937 RVA: 0x00121307 File Offset: 0x0011F507
		private static double NormalizeLongitude(double longitude)
		{
			longitude = CalendricalCalculationsHelper.Reminder(longitude, 360.0);
			if (longitude < 0.0)
			{
				longitude += 360.0;
			}
			return longitude;
		}

		// Token: 0x060055B2 RID: 21938 RVA: 0x00121334 File Offset: 0x0011F534
		public static double AsDayFraction(double longitude)
		{
			return longitude / 360.0;
		}

		// Token: 0x060055B3 RID: 21939 RVA: 0x00121344 File Offset: 0x0011F544
		private static double PolynomialSum(double[] coefficients, double indeterminate)
		{
			double num = coefficients[0];
			double num2 = 1.0;
			for (int i = 1; i < coefficients.Length; i++)
			{
				num2 *= indeterminate;
				num += coefficients[i] * num2;
			}
			return num;
		}

		// Token: 0x060055B4 RID: 21940 RVA: 0x0012137A File Offset: 0x0011F57A
		private static double CenturiesFrom1900(int gregorianYear)
		{
			return (double)(CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(gregorianYear, 7, 1)) - CalendricalCalculationsHelper.StartOf1900Century) / 36525.0;
		}

		// Token: 0x060055B5 RID: 21941 RVA: 0x0012139C File Offset: 0x0011F59C
		private static double DefaultEphemerisCorrection(int gregorianYear)
		{
			double num = (double)(CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(gregorianYear, 1, 1)) - CalendricalCalculationsHelper.StartOf1810);
			return (Math.Pow(0.5 + num, 2.0) / 41048480.0 - 15.0) / 86400.0;
		}

		// Token: 0x060055B6 RID: 21942 RVA: 0x001213F5 File Offset: 0x0011F5F5
		private static double EphemerisCorrection1988to2019(int gregorianYear)
		{
			return (double)(gregorianYear - 1933) / 86400.0;
		}

		// Token: 0x060055B7 RID: 21943 RVA: 0x0012140C File Offset: 0x0011F60C
		private static double EphemerisCorrection1900to1987(int gregorianYear)
		{
			double indeterminate = CalendricalCalculationsHelper.CenturiesFrom1900(gregorianYear);
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients1900to1987, indeterminate);
		}

		// Token: 0x060055B8 RID: 21944 RVA: 0x0012142C File Offset: 0x0011F62C
		private static double EphemerisCorrection1800to1899(int gregorianYear)
		{
			double indeterminate = CalendricalCalculationsHelper.CenturiesFrom1900(gregorianYear);
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients1800to1899, indeterminate);
		}

		// Token: 0x060055B9 RID: 21945 RVA: 0x0012144C File Offset: 0x0011F64C
		private static double EphemerisCorrection1700to1799(int gregorianYear)
		{
			double indeterminate = (double)(gregorianYear - 1700);
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients1700to1799, indeterminate) / 86400.0;
		}

		// Token: 0x060055BA RID: 21946 RVA: 0x00121478 File Offset: 0x0011F678
		private static double EphemerisCorrection1620to1699(int gregorianYear)
		{
			double indeterminate = (double)(gregorianYear - 1600);
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients1620to1699, indeterminate) / 86400.0;
		}

		// Token: 0x060055BB RID: 21947 RVA: 0x001214A4 File Offset: 0x0011F6A4
		private static double EphemerisCorrection(double time)
		{
			int gregorianYear = CalendricalCalculationsHelper.GetGregorianYear(time);
			CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap[] ephemerisCorrectionTable = CalendricalCalculationsHelper.EphemerisCorrectionTable;
			int i = 0;
			while (i < ephemerisCorrectionTable.Length)
			{
				CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap ephemerisCorrectionAlgorithmMap = ephemerisCorrectionTable[i];
				if (ephemerisCorrectionAlgorithmMap._lowestYear <= gregorianYear)
				{
					switch (ephemerisCorrectionAlgorithmMap._algorithm)
					{
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Default:
						return CalendricalCalculationsHelper.DefaultEphemerisCorrection(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1988to2019:
						return CalendricalCalculationsHelper.EphemerisCorrection1988to2019(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1900to1987:
						return CalendricalCalculationsHelper.EphemerisCorrection1900to1987(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1800to1899:
						return CalendricalCalculationsHelper.EphemerisCorrection1800to1899(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1700to1799:
						return CalendricalCalculationsHelper.EphemerisCorrection1700to1799(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1620to1699:
						return CalendricalCalculationsHelper.EphemerisCorrection1620to1699(gregorianYear);
					default:
						goto IL_7F;
					}
				}
				else
				{
					i++;
				}
			}
			IL_7F:
			return CalendricalCalculationsHelper.DefaultEphemerisCorrection(gregorianYear);
		}

		// Token: 0x060055BC RID: 21948 RVA: 0x00121536 File Offset: 0x0011F736
		public static double JulianCenturies(double moment)
		{
			return (moment + CalendricalCalculationsHelper.EphemerisCorrection(moment) - 730120.5) / 36525.0;
		}

		// Token: 0x060055BD RID: 21949 RVA: 0x00121554 File Offset: 0x0011F754
		private static bool IsNegative(double value)
		{
			return Math.Sign(value) == -1;
		}

		// Token: 0x060055BE RID: 21950 RVA: 0x0012155F File Offset: 0x0011F75F
		private static double CopySign(double value, double sign)
		{
			if (CalendricalCalculationsHelper.IsNegative(value) != CalendricalCalculationsHelper.IsNegative(sign))
			{
				return -value;
			}
			return value;
		}

		// Token: 0x060055BF RID: 21951 RVA: 0x00121574 File Offset: 0x0011F774
		private static double EquationOfTime(double time)
		{
			double num = CalendricalCalculationsHelper.JulianCenturies(time);
			double num2 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.LambdaCoefficients, num);
			double num3 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.AnomalyCoefficients, num);
			double num4 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.EccentricityCoefficients, num);
			double num5 = CalendricalCalculationsHelper.TanOfDegree(CalendricalCalculationsHelper.Obliquity(num) / 2.0);
			double num6 = num5 * num5;
			double num7 = num6 * CalendricalCalculationsHelper.SinOfDegree(2.0 * num2) - 2.0 * num4 * CalendricalCalculationsHelper.SinOfDegree(num3) + 4.0 * num4 * num6 * CalendricalCalculationsHelper.SinOfDegree(num3) * CalendricalCalculationsHelper.CosOfDegree(2.0 * num2) - 0.5 * Math.Pow(num6, 2.0) * CalendricalCalculationsHelper.SinOfDegree(4.0 * num2) - 1.25 * Math.Pow(num4, 2.0) * CalendricalCalculationsHelper.SinOfDegree(2.0 * num3);
			double num8 = 6.283185307179586;
			double num9 = num7 / num8;
			return CalendricalCalculationsHelper.CopySign(Math.Min(Math.Abs(num9), 0.5), num9);
		}

		// Token: 0x060055C0 RID: 21952 RVA: 0x00121698 File Offset: 0x0011F898
		private static double AsLocalTime(double apparentMidday, double longitude)
		{
			double time = apparentMidday - CalendricalCalculationsHelper.AsDayFraction(longitude);
			return apparentMidday - CalendricalCalculationsHelper.EquationOfTime(time);
		}

		// Token: 0x060055C1 RID: 21953 RVA: 0x001216B6 File Offset: 0x0011F8B6
		public static double Midday(double date, double longitude)
		{
			return CalendricalCalculationsHelper.AsLocalTime(date + 0.5, longitude) - CalendricalCalculationsHelper.AsDayFraction(longitude);
		}

		// Token: 0x060055C2 RID: 21954 RVA: 0x001216D0 File Offset: 0x0011F8D0
		private static double InitLongitude(double longitude)
		{
			return CalendricalCalculationsHelper.NormalizeLongitude(longitude + 180.0) - 180.0;
		}

		// Token: 0x060055C3 RID: 21955 RVA: 0x001216EC File Offset: 0x0011F8EC
		public static double MiddayAtPersianObservationSite(double date)
		{
			return CalendricalCalculationsHelper.Midday(date, CalendricalCalculationsHelper.InitLongitude(52.5));
		}

		// Token: 0x060055C4 RID: 21956 RVA: 0x00121702 File Offset: 0x0011F902
		private static double PeriodicTerm(double julianCenturies, int x, double y, double z)
		{
			return (double)x * CalendricalCalculationsHelper.SinOfDegree(y + z * julianCenturies);
		}

		// Token: 0x060055C5 RID: 21957 RVA: 0x00121714 File Offset: 0x0011F914
		private static double SumLongSequenceOfPeriodicTerms(double julianCenturies)
		{
			return 0.0 + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 403406, 270.54861, 0.9287892) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 195207, 340.19128, 35999.1376958) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 119433, 63.91854, 35999.4089666) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 112392, 331.2622, 35998.7287385) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 3891, 317.843, 71998.20261) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 2819, 86.631, 71998.4403) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 1721, 240.052, 36000.35726) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 660, 310.26, 71997.4812) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 350, 247.23, 32964.4678) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 334, 260.87, -19.441) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 314, 297.82, 445267.1117) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 268, 343.14, 45036.884) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 242, 166.79, 3.1008) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 234, 81.53, 22518.4434) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 158, 3.5, -19.9739) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 132, 132.75, 65928.9345) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 129, 182.95, 9038.0293) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 114, 162.03, 3034.7684) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 99, 29.8, 33718.148) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 93, 266.4, 3034.448) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 86, 249.2, -2280.773) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 78, 157.6, 29929.992) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 72, 257.8, 31556.493) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 68, 185.1, 149.588) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 64, 69.9, 9037.75) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 46, 8.0, 107997.405) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 38, 197.1, -4444.176) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 37, 250.4, 151.771) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 32, 65.3, 67555.316) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 29, 162.7, 31556.08) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 28, 341.5, -4561.54) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 27, 291.6, 107996.706) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 27, 98.5, 1221.655) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 25, 146.7, 62894.167) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 24, 110.0, 31437.369) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 21, 5.2, 14578.298) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 21, 342.6, -31931.757) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 20, 230.9, 34777.243) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 18, 256.1, 1221.999) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 17, 45.3, 62894.511) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 14, 242.9, -4442.039) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 13, 115.2, 107997.909) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 13, 151.8, 119.066) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 13, 285.3, 16859.071) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 12, 53.3, -4.578) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 126.6, 26895.292) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 205.7, -39.127) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 85.9, 12297.536) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 146.1, 90073.778);
		}

		// Token: 0x060055C6 RID: 21958 RVA: 0x00121C88 File Offset: 0x0011FE88
		private static double Aberration(double julianCenturies)
		{
			return 9.74E-05 * CalendricalCalculationsHelper.CosOfDegree(177.63 + 35999.01848 * julianCenturies) - 0.005575;
		}

		// Token: 0x060055C7 RID: 21959 RVA: 0x00121CB8 File Offset: 0x0011FEB8
		private static double Nutation(double julianCenturies)
		{
			double degree = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.CoefficientsA, julianCenturies);
			double degree2 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.CoefficientsB, julianCenturies);
			return -0.004778 * CalendricalCalculationsHelper.SinOfDegree(degree) - 0.0003667 * CalendricalCalculationsHelper.SinOfDegree(degree2);
		}

		// Token: 0x060055C8 RID: 21960 RVA: 0x00121D00 File Offset: 0x0011FF00
		public static double Compute(double time)
		{
			double num = CalendricalCalculationsHelper.JulianCenturies(time);
			return CalendricalCalculationsHelper.InitLongitude(282.7771834 + 36000.76953744 * num + 5.729577951308232E-06 * CalendricalCalculationsHelper.SumLongSequenceOfPeriodicTerms(num) + CalendricalCalculationsHelper.Aberration(num) + CalendricalCalculationsHelper.Nutation(num));
		}

		// Token: 0x060055C9 RID: 21961 RVA: 0x00121D4D File Offset: 0x0011FF4D
		public static double AsSeason(double longitude)
		{
			if (longitude >= 0.0)
			{
				return longitude;
			}
			return longitude + 360.0;
		}

		// Token: 0x060055CA RID: 21962 RVA: 0x00121D68 File Offset: 0x0011FF68
		private static double EstimatePrior(double longitude, double time)
		{
			double num = time - 1.0145616361111112 * CalendricalCalculationsHelper.AsSeason(CalendricalCalculationsHelper.InitLongitude(CalendricalCalculationsHelper.Compute(time) - longitude));
			double num2 = CalendricalCalculationsHelper.InitLongitude(CalendricalCalculationsHelper.Compute(num) - longitude);
			return Math.Min(time, num - 1.0145616361111112 * num2);
		}

		// Token: 0x060055CB RID: 21963 RVA: 0x00121DB8 File Offset: 0x0011FFB8
		internal static long PersianNewYearOnOrBefore(long numberOfDays)
		{
			double date = (double)numberOfDays;
			long num = (long)Math.Floor(CalendricalCalculationsHelper.EstimatePrior(0.0, CalendricalCalculationsHelper.MiddayAtPersianObservationSite(date))) - 1L;
			long num2 = num + 3L;
			long num3;
			for (num3 = num; num3 != num2; num3 += 1L)
			{
				double num4 = CalendricalCalculationsHelper.Compute(CalendricalCalculationsHelper.MiddayAtPersianObservationSite((double)num3));
				if (0.0 <= num4 && num4 <= 2.0)
				{
					break;
				}
			}
			return num3 - 1L;
		}

		// Token: 0x060055CC RID: 21964 RVA: 0x0000259F File Offset: 0x0000079F
		public CalendricalCalculationsHelper()
		{
		}

		// Token: 0x060055CD RID: 21965 RVA: 0x00121E20 File Offset: 0x00120020
		// Note: this type is marked as 'beforefieldinit'.
		static CalendricalCalculationsHelper()
		{
		}

		// Token: 0x04003555 RID: 13653
		private const double FullCircleOfArc = 360.0;

		// Token: 0x04003556 RID: 13654
		private const int HalfCircleOfArc = 180;

		// Token: 0x04003557 RID: 13655
		private const double TwelveHours = 0.5;

		// Token: 0x04003558 RID: 13656
		private const double Noon2000Jan01 = 730120.5;

		// Token: 0x04003559 RID: 13657
		internal const double MeanTropicalYearInDays = 365.242189;

		// Token: 0x0400355A RID: 13658
		private const double MeanSpeedOfSun = 1.0145616361111112;

		// Token: 0x0400355B RID: 13659
		private const double LongitudeSpring = 0.0;

		// Token: 0x0400355C RID: 13660
		private const double TwoDegreesAfterSpring = 2.0;

		// Token: 0x0400355D RID: 13661
		private const int SecondsPerDay = 86400;

		// Token: 0x0400355E RID: 13662
		private const int DaysInUniformLengthCentury = 36525;

		// Token: 0x0400355F RID: 13663
		private const int SecondsPerMinute = 60;

		// Token: 0x04003560 RID: 13664
		private const int MinutesPerDegree = 60;

		// Token: 0x04003561 RID: 13665
		private static long StartOf1810 = CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(1810, 1, 1));

		// Token: 0x04003562 RID: 13666
		private static long StartOf1900Century = CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(1900, 1, 1));

		// Token: 0x04003563 RID: 13667
		private static double[] Coefficients1900to1987 = new double[]
		{
			-2E-05,
			0.000297,
			0.025184,
			-0.181133,
			0.55304,
			-0.861938,
			0.677066,
			-0.212591
		};

		// Token: 0x04003564 RID: 13668
		private static double[] Coefficients1800to1899 = new double[]
		{
			-9E-06,
			0.003844,
			0.083563,
			0.865736,
			4.867575,
			15.845535,
			31.332267,
			38.291999,
			28.316289,
			11.636204,
			2.043794
		};

		// Token: 0x04003565 RID: 13669
		private static double[] Coefficients1700to1799 = new double[]
		{
			8.118780842,
			-0.005092142,
			0.003336121,
			-2.66484E-05
		};

		// Token: 0x04003566 RID: 13670
		private static double[] Coefficients1620to1699 = new double[]
		{
			196.58333,
			-4.0675,
			0.0219167
		};

		// Token: 0x04003567 RID: 13671
		private static double[] LambdaCoefficients = new double[]
		{
			280.46645,
			36000.76983,
			0.0003032
		};

		// Token: 0x04003568 RID: 13672
		private static double[] AnomalyCoefficients = new double[]
		{
			357.5291,
			35999.0503,
			-0.0001559,
			-4.8E-07
		};

		// Token: 0x04003569 RID: 13673
		private static double[] EccentricityCoefficients = new double[]
		{
			0.016708617,
			-4.2037E-05,
			-1.236E-07
		};

		// Token: 0x0400356A RID: 13674
		private static double[] Coefficients = new double[]
		{
			CalendricalCalculationsHelper.Angle(23, 26, 21.448),
			CalendricalCalculationsHelper.Angle(0, 0, -46.815),
			CalendricalCalculationsHelper.Angle(0, 0, -0.00059),
			CalendricalCalculationsHelper.Angle(0, 0, 0.001813)
		};

		// Token: 0x0400356B RID: 13675
		private static double[] CoefficientsA = new double[]
		{
			124.9,
			-1934.134,
			0.002063
		};

		// Token: 0x0400356C RID: 13676
		private static double[] CoefficientsB = new double[]
		{
			201.11,
			72001.5377,
			0.00057
		};

		// Token: 0x0400356D RID: 13677
		private static CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap[] EphemerisCorrectionTable = new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap[]
		{
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(2020, CalendricalCalculationsHelper.CorrectionAlgorithm.Default),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1988, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1988to2019),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1900, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1900to1987),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1800, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1800to1899),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1700, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1700to1799),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1620, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1620to1699),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(int.MinValue, CalendricalCalculationsHelper.CorrectionAlgorithm.Default)
		};

		// Token: 0x02000980 RID: 2432
		private enum CorrectionAlgorithm
		{
			// Token: 0x0400356F RID: 13679
			Default,
			// Token: 0x04003570 RID: 13680
			Year1988to2019,
			// Token: 0x04003571 RID: 13681
			Year1900to1987,
			// Token: 0x04003572 RID: 13682
			Year1800to1899,
			// Token: 0x04003573 RID: 13683
			Year1700to1799,
			// Token: 0x04003574 RID: 13684
			Year1620to1699
		}

		// Token: 0x02000981 RID: 2433
		private struct EphemerisCorrectionAlgorithmMap
		{
			// Token: 0x060055CE RID: 21966 RVA: 0x00122002 File Offset: 0x00120202
			public EphemerisCorrectionAlgorithmMap(int year, CalendricalCalculationsHelper.CorrectionAlgorithm algorithm)
			{
				this._lowestYear = year;
				this._algorithm = algorithm;
			}

			// Token: 0x04003575 RID: 13685
			internal int _lowestYear;

			// Token: 0x04003576 RID: 13686
			internal CalendricalCalculationsHelper.CorrectionAlgorithm _algorithm;
		}
	}
}
