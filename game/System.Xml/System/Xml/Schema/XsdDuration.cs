using System;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x0200060A RID: 1546
	internal struct XsdDuration
	{
		// Token: 0x06003F87 RID: 16263 RVA: 0x00160554 File Offset: 0x0015E754
		public XsdDuration(bool isNegative, int years, int months, int days, int hours, int minutes, int seconds, int nanoseconds)
		{
			if (years < 0)
			{
				throw new ArgumentOutOfRangeException("years");
			}
			if (months < 0)
			{
				throw new ArgumentOutOfRangeException("months");
			}
			if (days < 0)
			{
				throw new ArgumentOutOfRangeException("days");
			}
			if (hours < 0)
			{
				throw new ArgumentOutOfRangeException("hours");
			}
			if (minutes < 0)
			{
				throw new ArgumentOutOfRangeException("minutes");
			}
			if (seconds < 0)
			{
				throw new ArgumentOutOfRangeException("seconds");
			}
			if (nanoseconds < 0 || nanoseconds > 999999999)
			{
				throw new ArgumentOutOfRangeException("nanoseconds");
			}
			this.years = years;
			this.months = months;
			this.days = days;
			this.hours = hours;
			this.minutes = minutes;
			this.seconds = seconds;
			this.nanoseconds = (uint)nanoseconds;
			if (isNegative)
			{
				this.nanoseconds |= 2147483648U;
			}
		}

		// Token: 0x06003F88 RID: 16264 RVA: 0x00160623 File Offset: 0x0015E823
		public XsdDuration(TimeSpan timeSpan)
		{
			this = new XsdDuration(timeSpan, XsdDuration.DurationType.Duration);
		}

		// Token: 0x06003F89 RID: 16265 RVA: 0x00160630 File Offset: 0x0015E830
		public XsdDuration(TimeSpan timeSpan, XsdDuration.DurationType durationType)
		{
			long ticks = timeSpan.Ticks;
			bool flag;
			ulong num;
			if (ticks < 0L)
			{
				flag = true;
				num = (ulong)(-(ulong)ticks);
			}
			else
			{
				flag = false;
				num = (ulong)ticks;
			}
			if (durationType == XsdDuration.DurationType.YearMonthDuration)
			{
				int num2 = (int)(num / 315360000000000UL);
				int num3 = (int)(num % 315360000000000UL / 25920000000000UL);
				if (num3 == 12)
				{
					num2++;
					num3 = 0;
				}
				this = new XsdDuration(flag, num2, num3, 0, 0, 0, 0, 0);
				return;
			}
			this.nanoseconds = (uint)(num % 10000000UL) * 100U;
			if (flag)
			{
				this.nanoseconds |= 2147483648U;
			}
			this.years = 0;
			this.months = 0;
			this.days = (int)(num / 864000000000UL);
			this.hours = (int)(num / 36000000000UL % 24UL);
			this.minutes = (int)(num / 600000000UL % 60UL);
			this.seconds = (int)(num / 10000000UL % 60UL);
		}

		// Token: 0x06003F8A RID: 16266 RVA: 0x00160723 File Offset: 0x0015E923
		public XsdDuration(string s)
		{
			this = new XsdDuration(s, XsdDuration.DurationType.Duration);
		}

		// Token: 0x06003F8B RID: 16267 RVA: 0x00160730 File Offset: 0x0015E930
		public XsdDuration(string s, XsdDuration.DurationType durationType)
		{
			XsdDuration xsdDuration;
			Exception ex = XsdDuration.TryParse(s, durationType, out xsdDuration);
			if (ex != null)
			{
				throw ex;
			}
			this.years = xsdDuration.Years;
			this.months = xsdDuration.Months;
			this.days = xsdDuration.Days;
			this.hours = xsdDuration.Hours;
			this.minutes = xsdDuration.Minutes;
			this.seconds = xsdDuration.Seconds;
			this.nanoseconds = (uint)xsdDuration.Nanoseconds;
			if (xsdDuration.IsNegative)
			{
				this.nanoseconds |= 2147483648U;
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06003F8C RID: 16268 RVA: 0x001607C2 File Offset: 0x0015E9C2
		public bool IsNegative
		{
			get
			{
				return (this.nanoseconds & 2147483648U) > 0U;
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06003F8D RID: 16269 RVA: 0x001607D3 File Offset: 0x0015E9D3
		public int Years
		{
			get
			{
				return this.years;
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06003F8E RID: 16270 RVA: 0x001607DB File Offset: 0x0015E9DB
		public int Months
		{
			get
			{
				return this.months;
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06003F8F RID: 16271 RVA: 0x001607E3 File Offset: 0x0015E9E3
		public int Days
		{
			get
			{
				return this.days;
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06003F90 RID: 16272 RVA: 0x001607EB File Offset: 0x0015E9EB
		public int Hours
		{
			get
			{
				return this.hours;
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06003F91 RID: 16273 RVA: 0x001607F3 File Offset: 0x0015E9F3
		public int Minutes
		{
			get
			{
				return this.minutes;
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06003F92 RID: 16274 RVA: 0x001607FB File Offset: 0x0015E9FB
		public int Seconds
		{
			get
			{
				return this.seconds;
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06003F93 RID: 16275 RVA: 0x00160803 File Offset: 0x0015EA03
		public int Nanoseconds
		{
			get
			{
				return (int)(this.nanoseconds & 2147483647U);
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06003F94 RID: 16276 RVA: 0x00160811 File Offset: 0x0015EA11
		public int Microseconds
		{
			get
			{
				return this.Nanoseconds / 1000;
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06003F95 RID: 16277 RVA: 0x0016081F File Offset: 0x0015EA1F
		public int Milliseconds
		{
			get
			{
				return this.Nanoseconds / 1000000;
			}
		}

		// Token: 0x06003F96 RID: 16278 RVA: 0x00160830 File Offset: 0x0015EA30
		public XsdDuration Normalize()
		{
			int num = this.Years;
			int num2 = this.Months;
			int num3 = this.Days;
			int num4 = this.Hours;
			int num5 = this.Minutes;
			int num6 = this.Seconds;
			checked
			{
				try
				{
					if (num2 >= 12)
					{
						num += num2 / 12;
						num2 %= 12;
					}
					if (num6 >= 60)
					{
						num5 += num6 / 60;
						num6 %= 60;
					}
					if (num5 >= 60)
					{
						num4 += num5 / 60;
						num5 %= 60;
					}
					if (num4 >= 24)
					{
						num3 += num4 / 24;
						num4 %= 24;
					}
				}
				catch (OverflowException)
				{
					throw new OverflowException(Res.GetString("Value '{0}' was either too large or too small for {1}.", new object[]
					{
						this.ToString(),
						"Duration"
					}));
				}
				return new XsdDuration(this.IsNegative, num, num2, num3, num4, num5, num6, this.Nanoseconds);
			}
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x00160910 File Offset: 0x0015EB10
		public TimeSpan ToTimeSpan()
		{
			return this.ToTimeSpan(XsdDuration.DurationType.Duration);
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x0016091C File Offset: 0x0015EB1C
		public TimeSpan ToTimeSpan(XsdDuration.DurationType durationType)
		{
			TimeSpan result;
			Exception ex = this.TryToTimeSpan(durationType, out result);
			if (ex != null)
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x00160939 File Offset: 0x0015EB39
		internal Exception TryToTimeSpan(out TimeSpan result)
		{
			return this.TryToTimeSpan(XsdDuration.DurationType.Duration, out result);
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x00160944 File Offset: 0x0015EB44
		internal Exception TryToTimeSpan(XsdDuration.DurationType durationType, out TimeSpan result)
		{
			Exception result2 = null;
			ulong num = 0UL;
			checked
			{
				try
				{
					if (durationType != XsdDuration.DurationType.DayTimeDuration)
					{
						num += ((ulong)this.years + (ulong)this.months / 12UL) * 365UL;
						num += (ulong)this.months % 12UL * 30UL;
					}
					if (durationType != XsdDuration.DurationType.YearMonthDuration)
					{
						num += (ulong)this.days;
						num *= 24UL;
						num += (ulong)this.hours;
						num *= 60UL;
						num += (ulong)this.minutes;
						num *= 60UL;
						num += (ulong)this.seconds;
						num *= 10000000UL;
						num += (ulong)this.Nanoseconds / 100UL;
					}
					else
					{
						num *= 864000000000UL;
					}
					if (this.IsNegative)
					{
						if (num == 9223372036854775808UL)
						{
							result = new TimeSpan(long.MinValue);
						}
						else
						{
							result = new TimeSpan(0L - (long)num);
						}
					}
					else
					{
						result = new TimeSpan((long)num);
					}
					return null;
				}
				catch (OverflowException)
				{
					result = TimeSpan.MinValue;
					result2 = new OverflowException(Res.GetString("Value '{0}' was either too large or too small for {1}.", new object[]
					{
						durationType,
						"TimeSpan"
					}));
				}
				return result2;
			}
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x00160A84 File Offset: 0x0015EC84
		public override string ToString()
		{
			return this.ToString(XsdDuration.DurationType.Duration);
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x00160A90 File Offset: 0x0015EC90
		internal string ToString(XsdDuration.DurationType durationType)
		{
			StringBuilder stringBuilder = new StringBuilder(20);
			if (this.IsNegative)
			{
				stringBuilder.Append('-');
			}
			stringBuilder.Append('P');
			if (durationType != XsdDuration.DurationType.DayTimeDuration)
			{
				if (this.years != 0)
				{
					stringBuilder.Append(XmlConvert.ToString(this.years));
					stringBuilder.Append('Y');
				}
				if (this.months != 0)
				{
					stringBuilder.Append(XmlConvert.ToString(this.months));
					stringBuilder.Append('M');
				}
			}
			if (durationType != XsdDuration.DurationType.YearMonthDuration)
			{
				if (this.days != 0)
				{
					stringBuilder.Append(XmlConvert.ToString(this.days));
					stringBuilder.Append('D');
				}
				if (this.hours != 0 || this.minutes != 0 || this.seconds != 0 || this.Nanoseconds != 0)
				{
					stringBuilder.Append('T');
					if (this.hours != 0)
					{
						stringBuilder.Append(XmlConvert.ToString(this.hours));
						stringBuilder.Append('H');
					}
					if (this.minutes != 0)
					{
						stringBuilder.Append(XmlConvert.ToString(this.minutes));
						stringBuilder.Append('M');
					}
					int num = this.Nanoseconds;
					if (this.seconds != 0 || num != 0)
					{
						stringBuilder.Append(XmlConvert.ToString(this.seconds));
						if (num != 0)
						{
							stringBuilder.Append('.');
							int length = stringBuilder.Length;
							stringBuilder.Length += 9;
							int num2 = stringBuilder.Length - 1;
							for (int i = num2; i >= length; i--)
							{
								int num3 = num % 10;
								stringBuilder[i] = (char)(num3 + 48);
								if (num2 == i && num3 == 0)
								{
									num2--;
								}
								num /= 10;
							}
							stringBuilder.Length = num2 + 1;
						}
						stringBuilder.Append('S');
					}
				}
				if (stringBuilder[stringBuilder.Length - 1] == 'P')
				{
					stringBuilder.Append("T0S");
				}
			}
			else if (stringBuilder[stringBuilder.Length - 1] == 'P')
			{
				stringBuilder.Append("0M");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x00160C82 File Offset: 0x0015EE82
		internal static Exception TryParse(string s, out XsdDuration result)
		{
			return XsdDuration.TryParse(s, XsdDuration.DurationType.Duration, out result);
		}

		// Token: 0x06003F9E RID: 16286 RVA: 0x00160C8C File Offset: 0x0015EE8C
		internal static Exception TryParse(string s, XsdDuration.DurationType durationType, out XsdDuration result)
		{
			XsdDuration.Parts parts = XsdDuration.Parts.HasNone;
			result = default(XsdDuration);
			s = s.Trim();
			int length = s.Length;
			int num = 0;
			int i = 0;
			if (num < length)
			{
				if (s[num] == '-')
				{
					num++;
					result.nanoseconds = 2147483648U;
				}
				else
				{
					result.nanoseconds = 0U;
				}
				if (num < length && s[num++] == 'P')
				{
					int num2;
					if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) == null)
					{
						if (num >= length)
						{
							goto IL_2B5;
						}
						if (s[num] == 'Y')
						{
							if (i == 0)
							{
								goto IL_2B5;
							}
							parts |= XsdDuration.Parts.HasYears;
							result.years = num2;
							if (++num == length)
							{
								goto IL_298;
							}
							if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) != null)
							{
								goto IL_2D8;
							}
							if (num >= length)
							{
								goto IL_2B5;
							}
						}
						if (s[num] == 'M')
						{
							if (i == 0)
							{
								goto IL_2B5;
							}
							parts |= XsdDuration.Parts.HasMonths;
							result.months = num2;
							if (++num == length)
							{
								goto IL_298;
							}
							if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) != null)
							{
								goto IL_2D8;
							}
							if (num >= length)
							{
								goto IL_2B5;
							}
						}
						if (s[num] == 'D')
						{
							if (i == 0)
							{
								goto IL_2B5;
							}
							parts |= XsdDuration.Parts.HasDays;
							result.days = num2;
							if (++num == length)
							{
								goto IL_298;
							}
							if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) != null)
							{
								goto IL_2D8;
							}
							if (num >= length)
							{
								goto IL_2B5;
							}
						}
						if (s[num] == 'T')
						{
							if (i != 0)
							{
								goto IL_2B5;
							}
							num++;
							if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) != null)
							{
								goto IL_2D8;
							}
							if (num >= length)
							{
								goto IL_2B5;
							}
							if (s[num] == 'H')
							{
								if (i == 0)
								{
									goto IL_2B5;
								}
								parts |= XsdDuration.Parts.HasHours;
								result.hours = num2;
								if (++num == length)
								{
									goto IL_298;
								}
								if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) != null)
								{
									goto IL_2D8;
								}
								if (num >= length)
								{
									goto IL_2B5;
								}
							}
							if (s[num] == 'M')
							{
								if (i == 0)
								{
									goto IL_2B5;
								}
								parts |= XsdDuration.Parts.HasMinutes;
								result.minutes = num2;
								if (++num == length)
								{
									goto IL_298;
								}
								if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) != null)
								{
									goto IL_2D8;
								}
								if (num >= length)
								{
									goto IL_2B5;
								}
							}
							if (s[num] == '.')
							{
								num++;
								parts |= XsdDuration.Parts.HasSeconds;
								result.seconds = num2;
								if (XsdDuration.TryParseDigits(s, ref num, true, out num2, out i) != null)
								{
									goto IL_2D8;
								}
								if (i == 0)
								{
									num2 = 0;
								}
								while (i > 9)
								{
									num2 /= 10;
									i--;
								}
								while (i < 9)
								{
									num2 *= 10;
									i++;
								}
								result.nanoseconds |= (uint)num2;
								if (num >= length || s[num] != 'S')
								{
									goto IL_2B5;
								}
								if (++num == length)
								{
									goto IL_298;
								}
							}
							else if (s[num] == 'S')
							{
								if (i == 0)
								{
									goto IL_2B5;
								}
								parts |= XsdDuration.Parts.HasSeconds;
								result.seconds = num2;
								if (++num == length)
								{
									goto IL_298;
								}
							}
						}
						if (i != 0 || num != length)
						{
							goto IL_2B5;
						}
						IL_298:
						if (parts != XsdDuration.Parts.HasNone)
						{
							if (durationType == XsdDuration.DurationType.DayTimeDuration)
							{
								if ((parts & (XsdDuration.Parts)3) != XsdDuration.Parts.HasNone)
								{
									goto IL_2B5;
								}
							}
							else if (durationType == XsdDuration.DurationType.YearMonthDuration && (parts & (XsdDuration.Parts)(-4)) != XsdDuration.Parts.HasNone)
							{
								goto IL_2B5;
							}
							return null;
						}
						goto IL_2B5;
					}
					IL_2D8:
					return new OverflowException(Res.GetString("Value '{0}' was either too large or too small for {1}.", new object[]
					{
						s,
						durationType
					}));
				}
			}
			IL_2B5:
			return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
			{
				s,
				durationType
			}));
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x00160F94 File Offset: 0x0015F194
		private static string TryParseDigits(string s, ref int offset, bool eatDigits, out int result, out int numDigits)
		{
			int num = offset;
			int length = s.Length;
			result = 0;
			numDigits = 0;
			while (offset < length && s[offset] >= '0' && s[offset] <= '9')
			{
				int num2 = (int)(s[offset] - '0');
				if (result > (2147483647 - num2) / 10)
				{
					if (!eatDigits)
					{
						return "Value '{0}' was either too large or too small for {1}.";
					}
					numDigits = offset - num;
					while (offset < length && s[offset] >= '0' && s[offset] <= '9')
					{
						offset++;
					}
					return null;
				}
				else
				{
					result = result * 10 + num2;
					offset++;
				}
			}
			numDigits = offset - num;
			return null;
		}

		// Token: 0x04002D93 RID: 11667
		private int years;

		// Token: 0x04002D94 RID: 11668
		private int months;

		// Token: 0x04002D95 RID: 11669
		private int days;

		// Token: 0x04002D96 RID: 11670
		private int hours;

		// Token: 0x04002D97 RID: 11671
		private int minutes;

		// Token: 0x04002D98 RID: 11672
		private int seconds;

		// Token: 0x04002D99 RID: 11673
		private uint nanoseconds;

		// Token: 0x04002D9A RID: 11674
		private const uint NegativeBit = 2147483648U;

		// Token: 0x0200060B RID: 1547
		private enum Parts
		{
			// Token: 0x04002D9C RID: 11676
			HasNone,
			// Token: 0x04002D9D RID: 11677
			HasYears,
			// Token: 0x04002D9E RID: 11678
			HasMonths,
			// Token: 0x04002D9F RID: 11679
			HasDays = 4,
			// Token: 0x04002DA0 RID: 11680
			HasHours = 8,
			// Token: 0x04002DA1 RID: 11681
			HasMinutes = 16,
			// Token: 0x04002DA2 RID: 11682
			HasSeconds = 32
		}

		// Token: 0x0200060C RID: 1548
		public enum DurationType
		{
			// Token: 0x04002DA4 RID: 11684
			Duration,
			// Token: 0x04002DA5 RID: 11685
			YearMonthDuration,
			// Token: 0x04002DA6 RID: 11686
			DayTimeDuration
		}
	}
}
