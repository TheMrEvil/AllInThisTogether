using System;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x02000606 RID: 1542
	internal struct XsdDateTime
	{
		// Token: 0x06003F5A RID: 16218 RVA: 0x0015EF3F File Offset: 0x0015D13F
		public XsdDateTime(string text)
		{
			this = new XsdDateTime(text, XsdDateTimeFlags.AllXsd);
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x0015EF50 File Offset: 0x0015D150
		public XsdDateTime(string text, XsdDateTimeFlags kinds)
		{
			this = default(XsdDateTime);
			XsdDateTime.Parser parser = default(XsdDateTime.Parser);
			if (!parser.Parse(text, kinds))
			{
				throw new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
				{
					text,
					kinds
				}));
			}
			this.InitiateXsdDateTime(parser);
		}

		// Token: 0x06003F5C RID: 16220 RVA: 0x0015EFA1 File Offset: 0x0015D1A1
		private XsdDateTime(XsdDateTime.Parser parser)
		{
			this = default(XsdDateTime);
			this.InitiateXsdDateTime(parser);
		}

		// Token: 0x06003F5D RID: 16221 RVA: 0x0015EFB4 File Offset: 0x0015D1B4
		private void InitiateXsdDateTime(XsdDateTime.Parser parser)
		{
			this.dt = new DateTime(parser.year, parser.month, parser.day, parser.hour, parser.minute, parser.second);
			if (parser.fraction != 0)
			{
				this.dt = this.dt.AddTicks((long)parser.fraction);
			}
			this.extra = (uint)((int)parser.typeCode << 24 | (XsdDateTime.DateTimeTypeCode)((int)parser.kind << 16) | (XsdDateTime.DateTimeTypeCode)(parser.zoneHour << 8) | (XsdDateTime.DateTimeTypeCode)parser.zoneMinute);
		}

		// Token: 0x06003F5E RID: 16222 RVA: 0x0015F03C File Offset: 0x0015D23C
		internal static bool TryParse(string text, XsdDateTimeFlags kinds, out XsdDateTime result)
		{
			XsdDateTime.Parser parser = default(XsdDateTime.Parser);
			if (!parser.Parse(text, kinds))
			{
				result = default(XsdDateTime);
				return false;
			}
			result = new XsdDateTime(parser);
			return true;
		}

		// Token: 0x06003F5F RID: 16223 RVA: 0x0015F074 File Offset: 0x0015D274
		public XsdDateTime(DateTime dateTime, XsdDateTimeFlags kinds)
		{
			this.dt = dateTime;
			XsdDateTime.DateTimeTypeCode dateTimeTypeCode = (XsdDateTime.DateTimeTypeCode)(Bits.LeastPosition((uint)kinds) - 1);
			int num = 0;
			int num2 = 0;
			DateTimeKind kind = dateTime.Kind;
			XsdDateTime.XsdDateTimeKind xsdDateTimeKind;
			if (kind != DateTimeKind.Unspecified)
			{
				if (kind != DateTimeKind.Utc)
				{
					TimeSpan utcOffset = TimeZoneInfo.Local.GetUtcOffset(dateTime);
					if (utcOffset.Ticks < 0L)
					{
						xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.LocalWestOfZulu;
						num = -utcOffset.Hours;
						num2 = -utcOffset.Minutes;
					}
					else
					{
						xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.LocalEastOfZulu;
						num = utcOffset.Hours;
						num2 = utcOffset.Minutes;
					}
				}
				else
				{
					xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.Zulu;
				}
			}
			else
			{
				xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.Unspecified;
			}
			this.extra = (uint)((int)dateTimeTypeCode << 24 | (XsdDateTime.DateTimeTypeCode)((int)xsdDateTimeKind << 16) | (XsdDateTime.DateTimeTypeCode)(num << 8) | (XsdDateTime.DateTimeTypeCode)num2);
		}

		// Token: 0x06003F60 RID: 16224 RVA: 0x0015F106 File Offset: 0x0015D306
		public XsdDateTime(DateTimeOffset dateTimeOffset)
		{
			this = new XsdDateTime(dateTimeOffset, XsdDateTimeFlags.DateTime);
		}

		// Token: 0x06003F61 RID: 16225 RVA: 0x0015F110 File Offset: 0x0015D310
		public XsdDateTime(DateTimeOffset dateTimeOffset, XsdDateTimeFlags kinds)
		{
			this.dt = dateTimeOffset.DateTime;
			TimeSpan timeSpan = dateTimeOffset.Offset;
			XsdDateTime.DateTimeTypeCode dateTimeTypeCode = (XsdDateTime.DateTimeTypeCode)(Bits.LeastPosition((uint)kinds) - 1);
			XsdDateTime.XsdDateTimeKind xsdDateTimeKind;
			if (timeSpan.TotalMinutes < 0.0)
			{
				timeSpan = timeSpan.Negate();
				xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.LocalWestOfZulu;
			}
			else if (timeSpan.TotalMinutes > 0.0)
			{
				xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.LocalEastOfZulu;
			}
			else
			{
				xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.Zulu;
			}
			this.extra = (uint)((int)dateTimeTypeCode << 24 | (XsdDateTime.DateTimeTypeCode)((int)xsdDateTimeKind << 16) | (XsdDateTime.DateTimeTypeCode)(timeSpan.Hours << 8) | (XsdDateTime.DateTimeTypeCode)timeSpan.Minutes);
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06003F62 RID: 16226 RVA: 0x0015F192 File Offset: 0x0015D392
		private XsdDateTime.DateTimeTypeCode InternalTypeCode
		{
			get
			{
				return (XsdDateTime.DateTimeTypeCode)((this.extra & 4278190080U) >> 24);
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06003F63 RID: 16227 RVA: 0x0015F1A3 File Offset: 0x0015D3A3
		private XsdDateTime.XsdDateTimeKind InternalKind
		{
			get
			{
				return (XsdDateTime.XsdDateTimeKind)((this.extra & 16711680U) >> 16);
			}
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06003F64 RID: 16228 RVA: 0x0015F1B4 File Offset: 0x0015D3B4
		public XmlTypeCode TypeCode
		{
			get
			{
				return XsdDateTime.typeCodes[(int)this.InternalTypeCode];
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06003F65 RID: 16229 RVA: 0x0015F1C4 File Offset: 0x0015D3C4
		public DateTimeKind Kind
		{
			get
			{
				XsdDateTime.XsdDateTimeKind internalKind = this.InternalKind;
				if (internalKind == XsdDateTime.XsdDateTimeKind.Unspecified)
				{
					return DateTimeKind.Unspecified;
				}
				if (internalKind != XsdDateTime.XsdDateTimeKind.Zulu)
				{
					return DateTimeKind.Local;
				}
				return DateTimeKind.Utc;
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06003F66 RID: 16230 RVA: 0x0015F1E6 File Offset: 0x0015D3E6
		public int Year
		{
			get
			{
				return this.dt.Year;
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06003F67 RID: 16231 RVA: 0x0015F1F3 File Offset: 0x0015D3F3
		public int Month
		{
			get
			{
				return this.dt.Month;
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06003F68 RID: 16232 RVA: 0x0015F200 File Offset: 0x0015D400
		public int Day
		{
			get
			{
				return this.dt.Day;
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06003F69 RID: 16233 RVA: 0x0015F20D File Offset: 0x0015D40D
		public int Hour
		{
			get
			{
				return this.dt.Hour;
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06003F6A RID: 16234 RVA: 0x0015F21A File Offset: 0x0015D41A
		public int Minute
		{
			get
			{
				return this.dt.Minute;
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06003F6B RID: 16235 RVA: 0x0015F227 File Offset: 0x0015D427
		public int Second
		{
			get
			{
				return this.dt.Second;
			}
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06003F6C RID: 16236 RVA: 0x0015F234 File Offset: 0x0015D434
		public int Fraction
		{
			get
			{
				return (int)(this.dt.Ticks - new DateTime(this.dt.Year, this.dt.Month, this.dt.Day, this.dt.Hour, this.dt.Minute, this.dt.Second).Ticks);
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06003F6D RID: 16237 RVA: 0x0015F29D File Offset: 0x0015D49D
		public int ZoneHour
		{
			get
			{
				return (int)((this.extra & 65280U) >> 8);
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06003F6E RID: 16238 RVA: 0x0015F2AD File Offset: 0x0015D4AD
		public int ZoneMinute
		{
			get
			{
				return (int)(this.extra & 255U);
			}
		}

		// Token: 0x06003F6F RID: 16239 RVA: 0x0015F2BC File Offset: 0x0015D4BC
		public DateTime ToZulu()
		{
			switch (this.InternalKind)
			{
			case XsdDateTime.XsdDateTimeKind.Zulu:
				return new DateTime(this.dt.Ticks, DateTimeKind.Utc);
			case XsdDateTime.XsdDateTimeKind.LocalWestOfZulu:
				return new DateTime(this.dt.Add(new TimeSpan(this.ZoneHour, this.ZoneMinute, 0)).Ticks, DateTimeKind.Utc);
			case XsdDateTime.XsdDateTimeKind.LocalEastOfZulu:
				return new DateTime(this.dt.Subtract(new TimeSpan(this.ZoneHour, this.ZoneMinute, 0)).Ticks, DateTimeKind.Utc);
			default:
				return this.dt;
			}
		}

		// Token: 0x06003F70 RID: 16240 RVA: 0x0015F358 File Offset: 0x0015D558
		public static implicit operator DateTime(XsdDateTime xdt)
		{
			XsdDateTime.DateTimeTypeCode internalTypeCode = xdt.InternalTypeCode;
			DateTime dateTime;
			if (internalTypeCode != XsdDateTime.DateTimeTypeCode.Time)
			{
				if (internalTypeCode - XsdDateTime.DateTimeTypeCode.GDay <= 1)
				{
					dateTime = new DateTime(DateTime.Now.Year, xdt.Month, xdt.Day);
				}
				else
				{
					dateTime = xdt.dt;
				}
			}
			else
			{
				DateTime now = DateTime.Now;
				TimeSpan value = new DateTime(now.Year, now.Month, now.Day) - new DateTime(xdt.Year, xdt.Month, xdt.Day);
				dateTime = xdt.dt.Add(value);
			}
			switch (xdt.InternalKind)
			{
			case XsdDateTime.XsdDateTimeKind.Zulu:
				dateTime = new DateTime(dateTime.Ticks, DateTimeKind.Utc);
				break;
			case XsdDateTime.XsdDateTimeKind.LocalWestOfZulu:
			{
				long num = dateTime.Ticks + new TimeSpan(xdt.ZoneHour, xdt.ZoneMinute, 0).Ticks;
				if (num > DateTime.MaxValue.Ticks)
				{
					num += TimeZoneInfo.Local.GetUtcOffset(dateTime).Ticks;
					if (num > DateTime.MaxValue.Ticks)
					{
						num = DateTime.MaxValue.Ticks;
					}
					return new DateTime(num, DateTimeKind.Local);
				}
				dateTime = new DateTime(num, DateTimeKind.Utc).ToLocalTime();
				break;
			}
			case XsdDateTime.XsdDateTimeKind.LocalEastOfZulu:
			{
				long num = dateTime.Ticks - new TimeSpan(xdt.ZoneHour, xdt.ZoneMinute, 0).Ticks;
				if (num < DateTime.MinValue.Ticks)
				{
					num += TimeZoneInfo.Local.GetUtcOffset(dateTime).Ticks;
					if (num < DateTime.MinValue.Ticks)
					{
						num = DateTime.MinValue.Ticks;
					}
					return new DateTime(num, DateTimeKind.Local);
				}
				dateTime = new DateTime(num, DateTimeKind.Utc).ToLocalTime();
				break;
			}
			}
			return dateTime;
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x0015F528 File Offset: 0x0015D728
		public static implicit operator DateTimeOffset(XsdDateTime xdt)
		{
			XsdDateTime.DateTimeTypeCode internalTypeCode = xdt.InternalTypeCode;
			DateTime dateTime;
			if (internalTypeCode != XsdDateTime.DateTimeTypeCode.Time)
			{
				if (internalTypeCode - XsdDateTime.DateTimeTypeCode.GDay <= 1)
				{
					dateTime = new DateTime(DateTime.Now.Year, xdt.Month, xdt.Day);
				}
				else
				{
					dateTime = xdt.dt;
				}
			}
			else
			{
				DateTime now = DateTime.Now;
				TimeSpan value = new DateTime(now.Year, now.Month, now.Day) - new DateTime(xdt.Year, xdt.Month, xdt.Day);
				dateTime = xdt.dt.Add(value);
			}
			DateTimeOffset result;
			switch (xdt.InternalKind)
			{
			case XsdDateTime.XsdDateTimeKind.Zulu:
				result = new DateTimeOffset(dateTime, new TimeSpan(0L));
				return result;
			case XsdDateTime.XsdDateTimeKind.LocalWestOfZulu:
				result = new DateTimeOffset(dateTime, new TimeSpan(-xdt.ZoneHour, -xdt.ZoneMinute, 0));
				return result;
			case XsdDateTime.XsdDateTimeKind.LocalEastOfZulu:
				result = new DateTimeOffset(dateTime, new TimeSpan(xdt.ZoneHour, xdt.ZoneMinute, 0));
				return result;
			}
			result = new DateTimeOffset(dateTime, TimeZoneInfo.Local.GetUtcOffset(dateTime));
			return result;
		}

		// Token: 0x06003F72 RID: 16242 RVA: 0x0015F64C File Offset: 0x0015D84C
		public static int Compare(XsdDateTime left, XsdDateTime right)
		{
			if (left.extra == right.extra)
			{
				return DateTime.Compare(left.dt, right.dt);
			}
			if (left.InternalTypeCode != right.InternalTypeCode)
			{
				throw new ArgumentException(Res.GetString("Cannot compare '{0}' and '{1}'.", new object[]
				{
					left.TypeCode,
					right.TypeCode
				}));
			}
			return DateTime.Compare(left.GetZuluDateTime(), right.GetZuluDateTime());
		}

		// Token: 0x06003F73 RID: 16243 RVA: 0x0015F6D0 File Offset: 0x0015D8D0
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			return XsdDateTime.Compare(this, (XsdDateTime)value);
		}

		// Token: 0x06003F74 RID: 16244 RVA: 0x0015F6E8 File Offset: 0x0015D8E8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			switch (this.InternalTypeCode)
			{
			case XsdDateTime.DateTimeTypeCode.DateTime:
				this.PrintDate(stringBuilder);
				stringBuilder.Append('T');
				this.PrintTime(stringBuilder);
				break;
			case XsdDateTime.DateTimeTypeCode.Time:
				this.PrintTime(stringBuilder);
				break;
			case XsdDateTime.DateTimeTypeCode.Date:
				this.PrintDate(stringBuilder);
				break;
			case XsdDateTime.DateTimeTypeCode.GYearMonth:
			{
				char[] array = new char[XsdDateTime.Lzyyyy_MM];
				this.IntToCharArray(array, 0, this.Year, 4);
				array[XsdDateTime.Lzyyyy] = '-';
				this.ShortToCharArray(array, XsdDateTime.Lzyyyy_, this.Month);
				stringBuilder.Append(array);
				break;
			}
			case XsdDateTime.DateTimeTypeCode.GYear:
			{
				char[] array = new char[XsdDateTime.Lzyyyy];
				this.IntToCharArray(array, 0, this.Year, 4);
				stringBuilder.Append(array);
				break;
			}
			case XsdDateTime.DateTimeTypeCode.GMonthDay:
			{
				char[] array = new char[XsdDateTime.Lz__mm_dd];
				array[0] = '-';
				array[XsdDateTime.Lz_] = '-';
				this.ShortToCharArray(array, XsdDateTime.Lz__, this.Month);
				array[XsdDateTime.Lz__mm] = '-';
				this.ShortToCharArray(array, XsdDateTime.Lz__mm_, this.Day);
				stringBuilder.Append(array);
				break;
			}
			case XsdDateTime.DateTimeTypeCode.GDay:
			{
				char[] array = new char[XsdDateTime.Lz___dd];
				array[0] = '-';
				array[XsdDateTime.Lz_] = '-';
				array[XsdDateTime.Lz__] = '-';
				this.ShortToCharArray(array, XsdDateTime.Lz___, this.Day);
				stringBuilder.Append(array);
				break;
			}
			case XsdDateTime.DateTimeTypeCode.GMonth:
			{
				char[] array = new char[XsdDateTime.Lz__mm__];
				array[0] = '-';
				array[XsdDateTime.Lz_] = '-';
				this.ShortToCharArray(array, XsdDateTime.Lz__, this.Month);
				array[XsdDateTime.Lz__mm] = '-';
				array[XsdDateTime.Lz__mm_] = '-';
				stringBuilder.Append(array);
				break;
			}
			}
			this.PrintZone(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x0015F8B0 File Offset: 0x0015DAB0
		private void PrintDate(StringBuilder sb)
		{
			char[] array = new char[XsdDateTime.Lzyyyy_MM_dd];
			this.IntToCharArray(array, 0, this.Year, 4);
			array[XsdDateTime.Lzyyyy] = '-';
			this.ShortToCharArray(array, XsdDateTime.Lzyyyy_, this.Month);
			array[XsdDateTime.Lzyyyy_MM] = '-';
			this.ShortToCharArray(array, XsdDateTime.Lzyyyy_MM_, this.Day);
			sb.Append(array);
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x0015F918 File Offset: 0x0015DB18
		private void PrintTime(StringBuilder sb)
		{
			char[] array = new char[XsdDateTime.LzHH_mm_ss];
			this.ShortToCharArray(array, 0, this.Hour);
			array[XsdDateTime.LzHH] = ':';
			this.ShortToCharArray(array, XsdDateTime.LzHH_, this.Minute);
			array[XsdDateTime.LzHH_mm] = ':';
			this.ShortToCharArray(array, XsdDateTime.LzHH_mm_, this.Second);
			sb.Append(array);
			int num = this.Fraction;
			if (num != 0)
			{
				int num2 = 7;
				while (num % 10 == 0)
				{
					num2--;
					num /= 10;
				}
				array = new char[num2 + 1];
				array[0] = '.';
				this.IntToCharArray(array, 1, num, num2);
				sb.Append(array);
			}
		}

		// Token: 0x06003F77 RID: 16247 RVA: 0x0015F9BC File Offset: 0x0015DBBC
		private void PrintZone(StringBuilder sb)
		{
			switch (this.InternalKind)
			{
			case XsdDateTime.XsdDateTimeKind.Zulu:
				sb.Append('Z');
				return;
			case XsdDateTime.XsdDateTimeKind.LocalWestOfZulu:
			{
				char[] array = new char[XsdDateTime.Lz_zz_zz];
				array[0] = '-';
				this.ShortToCharArray(array, XsdDateTime.Lz_, this.ZoneHour);
				array[XsdDateTime.Lz_zz] = ':';
				this.ShortToCharArray(array, XsdDateTime.Lz_zz_, this.ZoneMinute);
				sb.Append(array);
				return;
			}
			case XsdDateTime.XsdDateTimeKind.LocalEastOfZulu:
			{
				char[] array = new char[XsdDateTime.Lz_zz_zz];
				array[0] = '+';
				this.ShortToCharArray(array, XsdDateTime.Lz_, this.ZoneHour);
				array[XsdDateTime.Lz_zz] = ':';
				this.ShortToCharArray(array, XsdDateTime.Lz_zz_, this.ZoneMinute);
				sb.Append(array);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06003F78 RID: 16248 RVA: 0x0015FA7A File Offset: 0x0015DC7A
		private void IntToCharArray(char[] text, int start, int value, int digits)
		{
			while (digits-- != 0)
			{
				text[start + digits] = (char)(value % 10 + 48);
				value /= 10;
			}
		}

		// Token: 0x06003F79 RID: 16249 RVA: 0x0015FA9B File Offset: 0x0015DC9B
		private void ShortToCharArray(char[] text, int start, int value)
		{
			text[start] = (char)(value / 10 + 48);
			text[start + 1] = (char)(value % 10 + 48);
		}

		// Token: 0x06003F7A RID: 16250 RVA: 0x0015FAB8 File Offset: 0x0015DCB8
		private DateTime GetZuluDateTime()
		{
			switch (this.InternalKind)
			{
			case XsdDateTime.XsdDateTimeKind.Zulu:
				return this.dt;
			case XsdDateTime.XsdDateTimeKind.LocalWestOfZulu:
				return this.dt.Add(new TimeSpan(this.ZoneHour, this.ZoneMinute, 0));
			case XsdDateTime.XsdDateTimeKind.LocalEastOfZulu:
				return this.dt.Subtract(new TimeSpan(this.ZoneHour, this.ZoneMinute, 0));
			default:
				return this.dt.ToUniversalTime();
			}
		}

		// Token: 0x06003F7B RID: 16251 RVA: 0x0015FB30 File Offset: 0x0015DD30
		// Note: this type is marked as 'beforefieldinit'.
		static XsdDateTime()
		{
		}

		// Token: 0x04002D52 RID: 11602
		private DateTime dt;

		// Token: 0x04002D53 RID: 11603
		private uint extra;

		// Token: 0x04002D54 RID: 11604
		private const uint TypeMask = 4278190080U;

		// Token: 0x04002D55 RID: 11605
		private const uint KindMask = 16711680U;

		// Token: 0x04002D56 RID: 11606
		private const uint ZoneHourMask = 65280U;

		// Token: 0x04002D57 RID: 11607
		private const uint ZoneMinuteMask = 255U;

		// Token: 0x04002D58 RID: 11608
		private const int TypeShift = 24;

		// Token: 0x04002D59 RID: 11609
		private const int KindShift = 16;

		// Token: 0x04002D5A RID: 11610
		private const int ZoneHourShift = 8;

		// Token: 0x04002D5B RID: 11611
		private const short maxFractionDigits = 7;

		// Token: 0x04002D5C RID: 11612
		private static readonly int Lzyyyy = "yyyy".Length;

		// Token: 0x04002D5D RID: 11613
		private static readonly int Lzyyyy_ = "yyyy-".Length;

		// Token: 0x04002D5E RID: 11614
		private static readonly int Lzyyyy_MM = "yyyy-MM".Length;

		// Token: 0x04002D5F RID: 11615
		private static readonly int Lzyyyy_MM_ = "yyyy-MM-".Length;

		// Token: 0x04002D60 RID: 11616
		private static readonly int Lzyyyy_MM_dd = "yyyy-MM-dd".Length;

		// Token: 0x04002D61 RID: 11617
		private static readonly int Lzyyyy_MM_ddT = "yyyy-MM-ddT".Length;

		// Token: 0x04002D62 RID: 11618
		private static readonly int LzHH = "HH".Length;

		// Token: 0x04002D63 RID: 11619
		private static readonly int LzHH_ = "HH:".Length;

		// Token: 0x04002D64 RID: 11620
		private static readonly int LzHH_mm = "HH:mm".Length;

		// Token: 0x04002D65 RID: 11621
		private static readonly int LzHH_mm_ = "HH:mm:".Length;

		// Token: 0x04002D66 RID: 11622
		private static readonly int LzHH_mm_ss = "HH:mm:ss".Length;

		// Token: 0x04002D67 RID: 11623
		private static readonly int Lz_ = "-".Length;

		// Token: 0x04002D68 RID: 11624
		private static readonly int Lz_zz = "-zz".Length;

		// Token: 0x04002D69 RID: 11625
		private static readonly int Lz_zz_ = "-zz:".Length;

		// Token: 0x04002D6A RID: 11626
		private static readonly int Lz_zz_zz = "-zz:zz".Length;

		// Token: 0x04002D6B RID: 11627
		private static readonly int Lz__ = "--".Length;

		// Token: 0x04002D6C RID: 11628
		private static readonly int Lz__mm = "--MM".Length;

		// Token: 0x04002D6D RID: 11629
		private static readonly int Lz__mm_ = "--MM-".Length;

		// Token: 0x04002D6E RID: 11630
		private static readonly int Lz__mm__ = "--MM--".Length;

		// Token: 0x04002D6F RID: 11631
		private static readonly int Lz__mm_dd = "--MM-dd".Length;

		// Token: 0x04002D70 RID: 11632
		private static readonly int Lz___ = "---".Length;

		// Token: 0x04002D71 RID: 11633
		private static readonly int Lz___dd = "---dd".Length;

		// Token: 0x04002D72 RID: 11634
		private static readonly XmlTypeCode[] typeCodes = new XmlTypeCode[]
		{
			XmlTypeCode.DateTime,
			XmlTypeCode.Time,
			XmlTypeCode.Date,
			XmlTypeCode.GYearMonth,
			XmlTypeCode.GYear,
			XmlTypeCode.GMonthDay,
			XmlTypeCode.GDay,
			XmlTypeCode.GMonth
		};

		// Token: 0x02000607 RID: 1543
		private enum DateTimeTypeCode
		{
			// Token: 0x04002D74 RID: 11636
			DateTime,
			// Token: 0x04002D75 RID: 11637
			Time,
			// Token: 0x04002D76 RID: 11638
			Date,
			// Token: 0x04002D77 RID: 11639
			GYearMonth,
			// Token: 0x04002D78 RID: 11640
			GYear,
			// Token: 0x04002D79 RID: 11641
			GMonthDay,
			// Token: 0x04002D7A RID: 11642
			GDay,
			// Token: 0x04002D7B RID: 11643
			GMonth,
			// Token: 0x04002D7C RID: 11644
			XdrDateTime
		}

		// Token: 0x02000608 RID: 1544
		private enum XsdDateTimeKind
		{
			// Token: 0x04002D7E RID: 11646
			Unspecified,
			// Token: 0x04002D7F RID: 11647
			Zulu,
			// Token: 0x04002D80 RID: 11648
			LocalWestOfZulu,
			// Token: 0x04002D81 RID: 11649
			LocalEastOfZulu
		}

		// Token: 0x02000609 RID: 1545
		private struct Parser
		{
			// Token: 0x06003F7C RID: 16252 RVA: 0x0015FCA0 File Offset: 0x0015DEA0
			public bool Parse(string text, XsdDateTimeFlags kinds)
			{
				this.text = text;
				this.length = text.Length;
				int num = 0;
				while (num < this.length && char.IsWhiteSpace(text[num]))
				{
					num++;
				}
				if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.DateTime | XsdDateTimeFlags.Date | XsdDateTimeFlags.XdrDateTimeNoTz | XsdDateTimeFlags.XdrDateTime) && this.ParseDate(num))
				{
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.DateTime) && this.ParseChar(num + XsdDateTime.Lzyyyy_MM_dd, 'T') && this.ParseTimeAndZoneAndWhitespace(num + XsdDateTime.Lzyyyy_MM_ddT))
					{
						this.typeCode = XsdDateTime.DateTimeTypeCode.DateTime;
						return true;
					}
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.Date) && this.ParseZoneAndWhitespace(num + XsdDateTime.Lzyyyy_MM_dd))
					{
						this.typeCode = XsdDateTime.DateTimeTypeCode.Date;
						return true;
					}
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.XdrDateTime) && (this.ParseZoneAndWhitespace(num + XsdDateTime.Lzyyyy_MM_dd) || (this.ParseChar(num + XsdDateTime.Lzyyyy_MM_dd, 'T') && this.ParseTimeAndZoneAndWhitespace(num + XsdDateTime.Lzyyyy_MM_ddT))))
					{
						this.typeCode = XsdDateTime.DateTimeTypeCode.XdrDateTime;
						return true;
					}
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.XdrDateTimeNoTz))
					{
						if (!this.ParseChar(num + XsdDateTime.Lzyyyy_MM_dd, 'T'))
						{
							this.typeCode = XsdDateTime.DateTimeTypeCode.XdrDateTime;
							return true;
						}
						if (this.ParseTimeAndWhitespace(num + XsdDateTime.Lzyyyy_MM_ddT))
						{
							this.typeCode = XsdDateTime.DateTimeTypeCode.XdrDateTime;
							return true;
						}
					}
				}
				if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.Time) && this.ParseTimeAndZoneAndWhitespace(num))
				{
					this.year = 1904;
					this.month = 1;
					this.day = 1;
					this.typeCode = XsdDateTime.DateTimeTypeCode.Time;
					return true;
				}
				if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.XdrTimeNoTz) && this.ParseTimeAndWhitespace(num))
				{
					this.year = 1904;
					this.month = 1;
					this.day = 1;
					this.typeCode = XsdDateTime.DateTimeTypeCode.Time;
					return true;
				}
				if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GYearMonth | XsdDateTimeFlags.GYear) && this.Parse4Dig(num, ref this.year) && 1 <= this.year)
				{
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GYearMonth) && this.ParseChar(num + XsdDateTime.Lzyyyy, '-') && this.Parse2Dig(num + XsdDateTime.Lzyyyy_, ref this.month) && 1 <= this.month && this.month <= 12 && this.ParseZoneAndWhitespace(num + XsdDateTime.Lzyyyy_MM))
					{
						this.day = 1;
						this.typeCode = XsdDateTime.DateTimeTypeCode.GYearMonth;
						return true;
					}
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GYear) && this.ParseZoneAndWhitespace(num + XsdDateTime.Lzyyyy))
					{
						this.month = 1;
						this.day = 1;
						this.typeCode = XsdDateTime.DateTimeTypeCode.GYear;
						return true;
					}
				}
				if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GMonthDay | XsdDateTimeFlags.GMonth) && this.ParseChar(num, '-') && this.ParseChar(num + XsdDateTime.Lz_, '-') && this.Parse2Dig(num + XsdDateTime.Lz__, ref this.month) && 1 <= this.month && this.month <= 12)
				{
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GMonthDay) && this.ParseChar(num + XsdDateTime.Lz__mm, '-') && this.Parse2Dig(num + XsdDateTime.Lz__mm_, ref this.day) && 1 <= this.day && this.day <= DateTime.DaysInMonth(1904, this.month) && this.ParseZoneAndWhitespace(num + XsdDateTime.Lz__mm_dd))
					{
						this.year = 1904;
						this.typeCode = XsdDateTime.DateTimeTypeCode.GMonthDay;
						return true;
					}
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GMonth) && (this.ParseZoneAndWhitespace(num + XsdDateTime.Lz__mm) || (this.ParseChar(num + XsdDateTime.Lz__mm, '-') && this.ParseChar(num + XsdDateTime.Lz__mm_, '-') && this.ParseZoneAndWhitespace(num + XsdDateTime.Lz__mm__))))
					{
						this.year = 1904;
						this.day = 1;
						this.typeCode = XsdDateTime.DateTimeTypeCode.GMonth;
						return true;
					}
				}
				if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GDay) && this.ParseChar(num, '-') && this.ParseChar(num + XsdDateTime.Lz_, '-') && this.ParseChar(num + XsdDateTime.Lz__, '-') && this.Parse2Dig(num + XsdDateTime.Lz___, ref this.day) && 1 <= this.day && this.day <= DateTime.DaysInMonth(1904, 1) && this.ParseZoneAndWhitespace(num + XsdDateTime.Lz___dd))
				{
					this.year = 1904;
					this.month = 1;
					this.typeCode = XsdDateTime.DateTimeTypeCode.GDay;
					return true;
				}
				return false;
			}

			// Token: 0x06003F7D RID: 16253 RVA: 0x001600D0 File Offset: 0x0015E2D0
			private bool ParseDate(int start)
			{
				return this.Parse4Dig(start, ref this.year) && 1 <= this.year && this.ParseChar(start + XsdDateTime.Lzyyyy, '-') && this.Parse2Dig(start + XsdDateTime.Lzyyyy_, ref this.month) && 1 <= this.month && this.month <= 12 && this.ParseChar(start + XsdDateTime.Lzyyyy_MM, '-') && this.Parse2Dig(start + XsdDateTime.Lzyyyy_MM_, ref this.day) && 1 <= this.day && this.day <= DateTime.DaysInMonth(this.year, this.month);
			}

			// Token: 0x06003F7E RID: 16254 RVA: 0x00160181 File Offset: 0x0015E381
			private bool ParseTimeAndZoneAndWhitespace(int start)
			{
				return this.ParseTime(ref start) && this.ParseZoneAndWhitespace(start);
			}

			// Token: 0x06003F7F RID: 16255 RVA: 0x00160199 File Offset: 0x0015E399
			private bool ParseTimeAndWhitespace(int start)
			{
				if (this.ParseTime(ref start))
				{
					while (start < this.length)
					{
						start++;
					}
					return start == this.length;
				}
				return false;
			}

			// Token: 0x06003F80 RID: 16256 RVA: 0x001601C0 File Offset: 0x0015E3C0
			private bool ParseTime(ref int start)
			{
				if (this.Parse2Dig(start, ref this.hour) && this.hour < 24 && this.ParseChar(start + XsdDateTime.LzHH, ':') && this.Parse2Dig(start + XsdDateTime.LzHH_, ref this.minute) && this.minute < 60 && this.ParseChar(start + XsdDateTime.LzHH_mm, ':') && this.Parse2Dig(start + XsdDateTime.LzHH_mm_, ref this.second) && this.second < 60)
				{
					start += XsdDateTime.LzHH_mm_ss;
					if (this.ParseChar(start, '.'))
					{
						this.fraction = 0;
						int num = 0;
						int num2 = 0;
						for (;;)
						{
							int num3 = start + 1;
							start = num3;
							if (num3 >= this.length)
							{
								break;
							}
							int num4 = (int)(this.text[start] - '0');
							if (9 < num4)
							{
								break;
							}
							if (num < 7)
							{
								this.fraction = this.fraction * 10 + num4;
							}
							else if (num == 7)
							{
								if (5 < num4)
								{
									num2 = 1;
								}
								else if (num4 == 5)
								{
									num2 = -1;
								}
							}
							else if (num2 < 0 && num4 != 0)
							{
								num2 = 1;
							}
							num++;
						}
						if (num < 7)
						{
							if (num == 0)
							{
								return false;
							}
							this.fraction *= XsdDateTime.Parser.Power10[7 - num];
						}
						else
						{
							if (num2 < 0)
							{
								num2 = (this.fraction & 1);
							}
							this.fraction += num2;
						}
					}
					return true;
				}
				this.hour = 0;
				return false;
			}

			// Token: 0x06003F81 RID: 16257 RVA: 0x00160330 File Offset: 0x0015E530
			private bool ParseZoneAndWhitespace(int start)
			{
				if (start < this.length)
				{
					char c = this.text[start];
					if (c == 'Z' || c == 'z')
					{
						this.kind = XsdDateTime.XsdDateTimeKind.Zulu;
						start++;
					}
					else if (start + 5 < this.length && this.Parse2Dig(start + XsdDateTime.Lz_, ref this.zoneHour) && this.zoneHour <= 99 && this.ParseChar(start + XsdDateTime.Lz_zz, ':') && this.Parse2Dig(start + XsdDateTime.Lz_zz_, ref this.zoneMinute) && this.zoneMinute <= 99)
					{
						if (c == '-')
						{
							this.kind = XsdDateTime.XsdDateTimeKind.LocalWestOfZulu;
							start += XsdDateTime.Lz_zz_zz;
						}
						else if (c == '+')
						{
							this.kind = XsdDateTime.XsdDateTimeKind.LocalEastOfZulu;
							start += XsdDateTime.Lz_zz_zz;
						}
					}
				}
				while (start < this.length && char.IsWhiteSpace(this.text[start]))
				{
					start++;
				}
				return start == this.length;
			}

			// Token: 0x06003F82 RID: 16258 RVA: 0x00160428 File Offset: 0x0015E628
			private bool Parse4Dig(int start, ref int num)
			{
				if (start + 3 < this.length)
				{
					int num2 = (int)(this.text[start] - '0');
					int num3 = (int)(this.text[start + 1] - '0');
					int num4 = (int)(this.text[start + 2] - '0');
					int num5 = (int)(this.text[start + 3] - '0');
					if (0 <= num2 && num2 < 10 && 0 <= num3 && num3 < 10 && 0 <= num4 && num4 < 10 && 0 <= num5 && num5 < 10)
					{
						num = ((num2 * 10 + num3) * 10 + num4) * 10 + num5;
						return true;
					}
				}
				return false;
			}

			// Token: 0x06003F83 RID: 16259 RVA: 0x001604C0 File Offset: 0x0015E6C0
			private bool Parse2Dig(int start, ref int num)
			{
				if (start + 1 < this.length)
				{
					int num2 = (int)(this.text[start] - '0');
					int num3 = (int)(this.text[start + 1] - '0');
					if (0 <= num2 && num2 < 10 && 0 <= num3 && num3 < 10)
					{
						num = num2 * 10 + num3;
						return true;
					}
				}
				return false;
			}

			// Token: 0x06003F84 RID: 16260 RVA: 0x00160517 File Offset: 0x0015E717
			private bool ParseChar(int start, char ch)
			{
				return start < this.length && this.text[start] == ch;
			}

			// Token: 0x06003F85 RID: 16261 RVA: 0x00160533 File Offset: 0x0015E733
			private static bool Test(XsdDateTimeFlags left, XsdDateTimeFlags right)
			{
				return (left & right) > (XsdDateTimeFlags)0;
			}

			// Token: 0x06003F86 RID: 16262 RVA: 0x0016053B File Offset: 0x0015E73B
			// Note: this type is marked as 'beforefieldinit'.
			static Parser()
			{
			}

			// Token: 0x04002D82 RID: 11650
			private const int leapYear = 1904;

			// Token: 0x04002D83 RID: 11651
			private const int firstMonth = 1;

			// Token: 0x04002D84 RID: 11652
			private const int firstDay = 1;

			// Token: 0x04002D85 RID: 11653
			public XsdDateTime.DateTimeTypeCode typeCode;

			// Token: 0x04002D86 RID: 11654
			public int year;

			// Token: 0x04002D87 RID: 11655
			public int month;

			// Token: 0x04002D88 RID: 11656
			public int day;

			// Token: 0x04002D89 RID: 11657
			public int hour;

			// Token: 0x04002D8A RID: 11658
			public int minute;

			// Token: 0x04002D8B RID: 11659
			public int second;

			// Token: 0x04002D8C RID: 11660
			public int fraction;

			// Token: 0x04002D8D RID: 11661
			public XsdDateTime.XsdDateTimeKind kind;

			// Token: 0x04002D8E RID: 11662
			public int zoneHour;

			// Token: 0x04002D8F RID: 11663
			public int zoneMinute;

			// Token: 0x04002D90 RID: 11664
			private string text;

			// Token: 0x04002D91 RID: 11665
			private int length;

			// Token: 0x04002D92 RID: 11666
			private static int[] Power10 = new int[]
			{
				-1,
				10,
				100,
				1000,
				10000,
				100000,
				1000000
			};
		}
	}
}
