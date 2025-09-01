using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Mono.Security
{
	// Token: 0x02000008 RID: 8
	public static class ASN1Convert
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000030F0 File Offset: 0x000012F0
		public static ASN1 FromDateTime(DateTime dt)
		{
			if (dt.Year < 2050)
			{
				return new ASN1(23, Encoding.ASCII.GetBytes(dt.ToUniversalTime().ToString("yyMMddHHmmss", CultureInfo.InvariantCulture) + "Z"));
			}
			return new ASN1(24, Encoding.ASCII.GetBytes(dt.ToUniversalTime().ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture) + "Z"));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003174 File Offset: 0x00001374
		public static ASN1 FromInt32(int value)
		{
			byte[] bytes = BitConverterLE.GetBytes(value);
			Array.Reverse<byte>(bytes);
			int num = 0;
			while (num < bytes.Length && bytes[num] == 0)
			{
				num++;
			}
			ASN1 asn = new ASN1(2);
			if (num != 0)
			{
				if (num != 4)
				{
					byte[] array = new byte[4 - num];
					Buffer.BlockCopy(bytes, num, array, 0, array.Length);
					asn.Value = array;
				}
				else
				{
					asn.Value = new byte[1];
				}
			}
			else
			{
				asn.Value = bytes;
			}
			return asn;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000031E5 File Offset: 0x000013E5
		public static ASN1 FromOid(string oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			return new ASN1(CryptoConfig.EncodeOID(oid));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003200 File Offset: 0x00001400
		public static ASN1 FromUnsignedBigInteger(byte[] big)
		{
			if (big == null)
			{
				throw new ArgumentNullException("big");
			}
			if (big[0] >= 128)
			{
				int num = big.Length + 1;
				byte[] array = new byte[num];
				Buffer.BlockCopy(big, 0, array, 1, num - 1);
				big = array;
			}
			return new ASN1(2, big);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003248 File Offset: 0x00001448
		public static int ToInt32(ASN1 asn1)
		{
			if (asn1 == null)
			{
				throw new ArgumentNullException("asn1");
			}
			if (asn1.Tag != 2)
			{
				throw new FormatException("Only integer can be converted");
			}
			int num = 0;
			for (int i = 0; i < asn1.Value.Length; i++)
			{
				num = (num << 8) + (int)asn1.Value[i];
			}
			return num;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000329C File Offset: 0x0000149C
		public static string ToOid(ASN1 asn1)
		{
			if (asn1 == null)
			{
				throw new ArgumentNullException("asn1");
			}
			byte[] value = asn1.Value;
			StringBuilder stringBuilder = new StringBuilder();
			byte b = value[0] / 40;
			byte b2 = value[0] % 40;
			if (b > 2)
			{
				b2 += (b - 2) * 40;
				b = 2;
			}
			stringBuilder.Append(b.ToString(CultureInfo.InvariantCulture));
			stringBuilder.Append(".");
			stringBuilder.Append(b2.ToString(CultureInfo.InvariantCulture));
			ulong num = 0UL;
			b = 1;
			while ((int)b < value.Length)
			{
				num = (num << 7 | (ulong)(value[(int)b] & 127));
				if ((value[(int)b] & 128) != 128)
				{
					stringBuilder.Append(".");
					stringBuilder.Append(num.ToString(CultureInfo.InvariantCulture));
					num = 0UL;
				}
				b += 1;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003374 File Offset: 0x00001574
		public static DateTime ToDateTime(ASN1 time)
		{
			if (time == null)
			{
				throw new ArgumentNullException("time");
			}
			string text = Encoding.ASCII.GetString(time.Value);
			string format = null;
			switch (text.Length)
			{
			case 11:
				format = "yyMMddHHmmZ";
				break;
			case 13:
				if (Convert.ToInt16(text.Substring(0, 2), CultureInfo.InvariantCulture) >= 50)
				{
					text = "19" + text;
				}
				else
				{
					text = "20" + text;
				}
				format = "yyyyMMddHHmmssZ";
				break;
			case 15:
				format = "yyyyMMddHHmmssZ";
				break;
			case 17:
			{
				string text2 = (Convert.ToInt16(text.Substring(0, 2), CultureInfo.InvariantCulture) >= 50) ? "19" : "20";
				char c = (text[12] == '+') ? '-' : '+';
				text = string.Format("{0}{1}{2}{3}{4}:{5}{6}", new object[]
				{
					text2,
					text.Substring(0, 12),
					c,
					text[13],
					text[14],
					text[15],
					text[16]
				});
				format = "yyyyMMddHHmmsszzz";
				break;
			}
			}
			return DateTime.ParseExact(text, format, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
		}
	}
}
