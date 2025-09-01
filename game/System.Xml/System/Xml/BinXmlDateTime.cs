using System;
using System.Globalization;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000020 RID: 32
	internal abstract class BinXmlDateTime
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00004615 File Offset: 0x00002815
		private static void Write2Dig(StringBuilder sb, int val)
		{
			sb.Append((char)(48 + val / 10));
			sb.Append((char)(48 + val % 10));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004635 File Offset: 0x00002835
		private static void Write4DigNeg(StringBuilder sb, int val)
		{
			if (val < 0)
			{
				val = -val;
				sb.Append('-');
			}
			BinXmlDateTime.Write2Dig(sb, val / 100);
			BinXmlDateTime.Write2Dig(sb, val % 100);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000465C File Offset: 0x0000285C
		private static void Write3Dec(StringBuilder sb, int val)
		{
			int num = val % 10;
			val /= 10;
			int num2 = val % 10;
			val /= 10;
			int num3 = val;
			sb.Append('.');
			sb.Append((char)(48 + num3));
			sb.Append((char)(48 + num2));
			sb.Append((char)(48 + num));
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000046AE File Offset: 0x000028AE
		private static void WriteDate(StringBuilder sb, int yr, int mnth, int day)
		{
			BinXmlDateTime.Write4DigNeg(sb, yr);
			sb.Append('-');
			BinXmlDateTime.Write2Dig(sb, mnth);
			sb.Append('-');
			BinXmlDateTime.Write2Dig(sb, day);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000046D7 File Offset: 0x000028D7
		private static void WriteTime(StringBuilder sb, int hr, int min, int sec, int ms)
		{
			BinXmlDateTime.Write2Dig(sb, hr);
			sb.Append(':');
			BinXmlDateTime.Write2Dig(sb, min);
			sb.Append(':');
			BinXmlDateTime.Write2Dig(sb, sec);
			if (ms != 0)
			{
				BinXmlDateTime.Write3Dec(sb, ms);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000470C File Offset: 0x0000290C
		private static void WriteTimeFullPrecision(StringBuilder sb, int hr, int min, int sec, int fraction)
		{
			BinXmlDateTime.Write2Dig(sb, hr);
			sb.Append(':');
			BinXmlDateTime.Write2Dig(sb, min);
			sb.Append(':');
			BinXmlDateTime.Write2Dig(sb, sec);
			if (fraction != 0)
			{
				int i = 7;
				while (fraction % 10 == 0)
				{
					i--;
					fraction /= 10;
				}
				char[] array = new char[i];
				while (i > 0)
				{
					i--;
					array[i] = (char)(fraction % 10 + 48);
					fraction /= 10;
				}
				sb.Append('.');
				sb.Append(array);
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004790 File Offset: 0x00002990
		private static void WriteTimeZone(StringBuilder sb, TimeSpan zone)
		{
			bool negTimeZone = true;
			if (zone.Ticks < 0L)
			{
				negTimeZone = false;
				zone = zone.Negate();
			}
			BinXmlDateTime.WriteTimeZone(sb, negTimeZone, zone.Hours, zone.Minutes);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000047CA File Offset: 0x000029CA
		private static void WriteTimeZone(StringBuilder sb, bool negTimeZone, int hr, int min)
		{
			if (hr == 0 && min == 0)
			{
				sb.Append('Z');
				return;
			}
			sb.Append(negTimeZone ? '+' : '-');
			BinXmlDateTime.Write2Dig(sb, hr);
			sb.Append(':');
			BinXmlDateTime.Write2Dig(sb, min);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004804 File Offset: 0x00002A04
		private static void BreakDownXsdDateTime(long val, out int yr, out int mnth, out int day, out int hr, out int min, out int sec, out int ms)
		{
			if (val >= 0L)
			{
				long num = val / 4L;
				ms = (int)(num % 1000L);
				num /= 1000L;
				sec = (int)(num % 60L);
				num /= 60L;
				min = (int)(num % 60L);
				num /= 60L;
				hr = (int)(num % 24L);
				num /= 24L;
				day = (int)(num % 31L) + 1;
				num /= 31L;
				mnth = (int)(num % 12L) + 1;
				num /= 12L;
				yr = (int)(num - 9999L);
				if (yr >= -9999 && yr <= 9999)
				{
					return;
				}
			}
			throw new XmlException("Arithmetic Overflow.", null);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000048AC File Offset: 0x00002AAC
		private static void BreakDownXsdDate(long val, out int yr, out int mnth, out int day, out bool negTimeZone, out int hr, out int min)
		{
			if (val >= 0L)
			{
				val /= 4L;
				int num = (int)(val % 1740L) - 840;
				long num2 = val / 1740L;
				if (negTimeZone = (num < 0))
				{
					num = -num;
				}
				min = num % 60;
				hr = num / 60;
				day = (int)(num2 % 31L) + 1;
				num2 /= 31L;
				mnth = (int)(num2 % 12L) + 1;
				yr = (int)(num2 / 12L) - 9999;
				if (yr >= -9999 && yr <= 9999)
				{
					return;
				}
			}
			throw new XmlException("Arithmetic Overflow.", null);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004940 File Offset: 0x00002B40
		private static void BreakDownXsdTime(long val, out int hr, out int min, out int sec, out int ms)
		{
			if (val >= 0L)
			{
				val /= 4L;
				ms = (int)(val % 1000L);
				val /= 1000L;
				sec = (int)(val % 60L);
				val /= 60L;
				min = (int)(val % 60L);
				hr = (int)(val / 60L);
				if (0 <= hr && hr <= 23)
				{
					return;
				}
			}
			throw new XmlException("Arithmetic Overflow.", null);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000049A4 File Offset: 0x00002BA4
		public static string XsdDateTimeToString(long val)
		{
			int yr;
			int mnth;
			int day;
			int hr;
			int min;
			int sec;
			int ms;
			BinXmlDateTime.BreakDownXsdDateTime(val, out yr, out mnth, out day, out hr, out min, out sec, out ms);
			StringBuilder stringBuilder = new StringBuilder(20);
			BinXmlDateTime.WriteDate(stringBuilder, yr, mnth, day);
			stringBuilder.Append('T');
			BinXmlDateTime.WriteTime(stringBuilder, hr, min, sec, ms);
			stringBuilder.Append('Z');
			return stringBuilder.ToString();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000049FC File Offset: 0x00002BFC
		public static DateTime XsdDateTimeToDateTime(long val)
		{
			int year;
			int month;
			int day;
			int hour;
			int minute;
			int second;
			int millisecond;
			BinXmlDateTime.BreakDownXsdDateTime(val, out year, out month, out day, out hour, out minute, out second, out millisecond);
			return new DateTime(year, month, day, hour, minute, second, millisecond, DateTimeKind.Utc);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004A30 File Offset: 0x00002C30
		public static string XsdDateToString(long val)
		{
			int yr;
			int mnth;
			int day;
			bool negTimeZone;
			int hr;
			int min;
			BinXmlDateTime.BreakDownXsdDate(val, out yr, out mnth, out day, out negTimeZone, out hr, out min);
			StringBuilder stringBuilder = new StringBuilder(20);
			BinXmlDateTime.WriteDate(stringBuilder, yr, mnth, day);
			BinXmlDateTime.WriteTimeZone(stringBuilder, negTimeZone, hr, min);
			return stringBuilder.ToString();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004A70 File Offset: 0x00002C70
		public static DateTime XsdDateToDateTime(long val)
		{
			int year;
			int month;
			int day;
			bool flag;
			int num;
			int num2;
			BinXmlDateTime.BreakDownXsdDate(val, out year, out month, out day, out flag, out num, out num2);
			DateTime dateTime = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
			int num3 = (flag ? -1 : 1) * (num * 60 + num2);
			return TimeZone.CurrentTimeZone.ToLocalTime(dateTime.AddMinutes((double)num3));
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004AC4 File Offset: 0x00002CC4
		public static string XsdTimeToString(long val)
		{
			int hr;
			int min;
			int sec;
			int ms;
			BinXmlDateTime.BreakDownXsdTime(val, out hr, out min, out sec, out ms);
			StringBuilder stringBuilder = new StringBuilder(16);
			BinXmlDateTime.WriteTime(stringBuilder, hr, min, sec, ms);
			stringBuilder.Append('Z');
			return stringBuilder.ToString();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004B00 File Offset: 0x00002D00
		public static DateTime XsdTimeToDateTime(long val)
		{
			int hour;
			int minute;
			int second;
			int millisecond;
			BinXmlDateTime.BreakDownXsdTime(val, out hour, out minute, out second, out millisecond);
			return new DateTime(1, 1, 1, hour, minute, second, millisecond, DateTimeKind.Utc);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004B28 File Offset: 0x00002D28
		public static string SqlDateTimeToString(int dateticks, uint timeticks)
		{
			DateTime dateTime = BinXmlDateTime.SqlDateTimeToDateTime(dateticks, timeticks);
			string format = (dateTime.Millisecond != 0) ? "yyyy/MM/dd\\THH:mm:ss.ffff" : "yyyy/MM/dd\\THH:mm:ss";
			return dateTime.ToString(format, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004B60 File Offset: 0x00002D60
		public static DateTime SqlDateTimeToDateTime(int dateticks, uint timeticks)
		{
			DateTime dateTime = new DateTime(1900, 1, 1);
			long num = (long)(timeticks / BinXmlDateTime.SQLTicksPerMillisecond + 0.5);
			return dateTime.Add(new TimeSpan((long)dateticks * 864000000000L + num * 10000L));
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004BB4 File Offset: 0x00002DB4
		public static string SqlSmallDateTimeToString(short dateticks, ushort timeticks)
		{
			return BinXmlDateTime.SqlSmallDateTimeToDateTime(dateticks, timeticks).ToString("yyyy/MM/dd\\THH:mm:ss", CultureInfo.InvariantCulture);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004BDA File Offset: 0x00002DDA
		public static DateTime SqlSmallDateTimeToDateTime(short dateticks, ushort timeticks)
		{
			return BinXmlDateTime.SqlDateTimeToDateTime((int)dateticks, (uint)((int)timeticks * BinXmlDateTime.SQLTicksPerMinute));
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004BEC File Offset: 0x00002DEC
		public static DateTime XsdKatmaiDateToDateTime(byte[] data, int offset)
		{
			long katmaiDateTicks = BinXmlDateTime.GetKatmaiDateTicks(data, ref offset);
			return new DateTime(katmaiDateTicks);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004C08 File Offset: 0x00002E08
		public static DateTime XsdKatmaiDateTimeToDateTime(byte[] data, int offset)
		{
			long katmaiTimeTicks = BinXmlDateTime.GetKatmaiTimeTicks(data, ref offset);
			long katmaiDateTicks = BinXmlDateTime.GetKatmaiDateTicks(data, ref offset);
			return new DateTime(katmaiDateTicks + katmaiTimeTicks);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004C2F File Offset: 0x00002E2F
		public static DateTime XsdKatmaiTimeToDateTime(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateTimeToDateTime(data, offset);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004C38 File Offset: 0x00002E38
		public static DateTime XsdKatmaiDateOffsetToDateTime(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateOffsetToDateTimeOffset(data, offset).LocalDateTime;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004C54 File Offset: 0x00002E54
		public static DateTime XsdKatmaiDateTimeOffsetToDateTime(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateTimeOffsetToDateTimeOffset(data, offset).LocalDateTime;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004C70 File Offset: 0x00002E70
		public static DateTime XsdKatmaiTimeOffsetToDateTime(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiTimeOffsetToDateTimeOffset(data, offset).LocalDateTime;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004C8C File Offset: 0x00002E8C
		public static DateTimeOffset XsdKatmaiDateToDateTimeOffset(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateToDateTime(data, offset);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004C9A File Offset: 0x00002E9A
		public static DateTimeOffset XsdKatmaiDateTimeToDateTimeOffset(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateTimeToDateTime(data, offset);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004CA8 File Offset: 0x00002EA8
		public static DateTimeOffset XsdKatmaiTimeToDateTimeOffset(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiTimeToDateTime(data, offset);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004CB6 File Offset: 0x00002EB6
		public static DateTimeOffset XsdKatmaiDateOffsetToDateTimeOffset(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateTimeOffsetToDateTimeOffset(data, offset);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004CC0 File Offset: 0x00002EC0
		public static DateTimeOffset XsdKatmaiDateTimeOffsetToDateTimeOffset(byte[] data, int offset)
		{
			long katmaiTimeTicks = BinXmlDateTime.GetKatmaiTimeTicks(data, ref offset);
			long katmaiDateTicks = BinXmlDateTime.GetKatmaiDateTicks(data, ref offset);
			long katmaiTimeZoneTicks = BinXmlDateTime.GetKatmaiTimeZoneTicks(data, offset);
			return new DateTimeOffset(katmaiDateTicks + katmaiTimeTicks + katmaiTimeZoneTicks, new TimeSpan(katmaiTimeZoneTicks));
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004CB6 File Offset: 0x00002EB6
		public static DateTimeOffset XsdKatmaiTimeOffsetToDateTimeOffset(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateTimeOffsetToDateTimeOffset(data, offset);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004CF8 File Offset: 0x00002EF8
		public static string XsdKatmaiDateToString(byte[] data, int offset)
		{
			DateTime dateTime = BinXmlDateTime.XsdKatmaiDateToDateTime(data, offset);
			StringBuilder stringBuilder = new StringBuilder(10);
			BinXmlDateTime.WriteDate(stringBuilder, dateTime.Year, dateTime.Month, dateTime.Day);
			return stringBuilder.ToString();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004D34 File Offset: 0x00002F34
		public static string XsdKatmaiDateTimeToString(byte[] data, int offset)
		{
			DateTime dt = BinXmlDateTime.XsdKatmaiDateTimeToDateTime(data, offset);
			StringBuilder stringBuilder = new StringBuilder(33);
			BinXmlDateTime.WriteDate(stringBuilder, dt.Year, dt.Month, dt.Day);
			stringBuilder.Append('T');
			BinXmlDateTime.WriteTimeFullPrecision(stringBuilder, dt.Hour, dt.Minute, dt.Second, BinXmlDateTime.GetFractions(dt));
			return stringBuilder.ToString();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004D9C File Offset: 0x00002F9C
		public static string XsdKatmaiTimeToString(byte[] data, int offset)
		{
			DateTime dt = BinXmlDateTime.XsdKatmaiTimeToDateTime(data, offset);
			StringBuilder stringBuilder = new StringBuilder(16);
			BinXmlDateTime.WriteTimeFullPrecision(stringBuilder, dt.Hour, dt.Minute, dt.Second, BinXmlDateTime.GetFractions(dt));
			return stringBuilder.ToString();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004DE0 File Offset: 0x00002FE0
		public static string XsdKatmaiDateOffsetToString(byte[] data, int offset)
		{
			DateTimeOffset dateTimeOffset = BinXmlDateTime.XsdKatmaiDateOffsetToDateTimeOffset(data, offset);
			StringBuilder stringBuilder = new StringBuilder(16);
			BinXmlDateTime.WriteDate(stringBuilder, dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day);
			BinXmlDateTime.WriteTimeZone(stringBuilder, dateTimeOffset.Offset);
			return stringBuilder.ToString();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004E2C File Offset: 0x0000302C
		public static string XsdKatmaiDateTimeOffsetToString(byte[] data, int offset)
		{
			DateTimeOffset dt = BinXmlDateTime.XsdKatmaiDateTimeOffsetToDateTimeOffset(data, offset);
			StringBuilder stringBuilder = new StringBuilder(39);
			BinXmlDateTime.WriteDate(stringBuilder, dt.Year, dt.Month, dt.Day);
			stringBuilder.Append('T');
			BinXmlDateTime.WriteTimeFullPrecision(stringBuilder, dt.Hour, dt.Minute, dt.Second, BinXmlDateTime.GetFractions(dt));
			BinXmlDateTime.WriteTimeZone(stringBuilder, dt.Offset);
			return stringBuilder.ToString();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004EA0 File Offset: 0x000030A0
		public static string XsdKatmaiTimeOffsetToString(byte[] data, int offset)
		{
			DateTimeOffset dt = BinXmlDateTime.XsdKatmaiTimeOffsetToDateTimeOffset(data, offset);
			StringBuilder stringBuilder = new StringBuilder(22);
			BinXmlDateTime.WriteTimeFullPrecision(stringBuilder, dt.Hour, dt.Minute, dt.Second, BinXmlDateTime.GetFractions(dt));
			BinXmlDateTime.WriteTimeZone(stringBuilder, dt.Offset);
			return stringBuilder.ToString();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004EF0 File Offset: 0x000030F0
		private static long GetKatmaiDateTicks(byte[] data, ref int pos)
		{
			int num = pos;
			pos = num + 3;
			return (long)((int)data[num] | (int)data[num + 1] << 8 | (int)data[num + 2] << 16) * 864000000000L;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004F24 File Offset: 0x00003124
		private static long GetKatmaiTimeTicks(byte[] data, ref int pos)
		{
			int num = pos;
			byte b = data[num];
			num++;
			long num2;
			if (b <= 2)
			{
				num2 = (long)((int)data[num] | (int)data[num + 1] << 8 | (int)data[num + 2] << 16);
				pos = num + 3;
			}
			else if (b <= 4)
			{
				num2 = (long)((int)data[num] | (int)data[num + 1] << 8 | (int)data[num + 2] << 16);
				num2 |= (long)((long)((ulong)data[num + 3]) << 24);
				pos = num + 4;
			}
			else
			{
				if (b > 7)
				{
					throw new XmlException("Arithmetic Overflow.", null);
				}
				num2 = (long)((int)data[num] | (int)data[num + 1] << 8 | (int)data[num + 2] << 16);
				num2 |= (long)((ulong)data[num + 3] << 24 | (ulong)data[num + 4] << 32);
				pos = num + 5;
			}
			return num2 * (long)BinXmlDateTime.KatmaiTimeScaleMultiplicator[(int)b];
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004FD7 File Offset: 0x000031D7
		private static long GetKatmaiTimeZoneTicks(byte[] data, int pos)
		{
			return (long)((short)((int)data[pos] | (int)data[pos + 1] << 8)) * 600000000L;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004FF0 File Offset: 0x000031F0
		private static int GetFractions(DateTime dt)
		{
			return (int)(dt.Ticks - new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second).Ticks);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00005040 File Offset: 0x00003240
		private static int GetFractions(DateTimeOffset dt)
		{
			return (int)(dt.Ticks - new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second).Ticks);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000216B File Offset: 0x0000036B
		protected BinXmlDateTime()
		{
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00005090 File Offset: 0x00003290
		// Note: this type is marked as 'beforefieldinit'.
		static BinXmlDateTime()
		{
		}

		// Token: 0x0400056A RID: 1386
		private const int MaxFractionDigits = 7;

		// Token: 0x0400056B RID: 1387
		internal static int[] KatmaiTimeScaleMultiplicator = new int[]
		{
			10000000,
			1000000,
			100000,
			10000,
			1000,
			100,
			10,
			1
		};

		// Token: 0x0400056C RID: 1388
		private static readonly double SQLTicksPerMillisecond = 0.3;

		// Token: 0x0400056D RID: 1389
		public static readonly int SQLTicksPerSecond = 300;

		// Token: 0x0400056E RID: 1390
		public static readonly int SQLTicksPerMinute = BinXmlDateTime.SQLTicksPerSecond * 60;

		// Token: 0x0400056F RID: 1391
		public static readonly int SQLTicksPerHour = BinXmlDateTime.SQLTicksPerMinute * 60;

		// Token: 0x04000570 RID: 1392
		private static readonly int SQLTicksPerDay = BinXmlDateTime.SQLTicksPerHour * 24;
	}
}
