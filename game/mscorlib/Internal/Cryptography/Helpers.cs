using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;

namespace Internal.Cryptography
{
	// Token: 0x020000C9 RID: 201
	internal static class Helpers
	{
		// Token: 0x060004C7 RID: 1223 RVA: 0x000178CC File Offset: 0x00015ACC
		public static byte[] CloneByteArray(this byte[] src)
		{
			if (src == null)
			{
				return null;
			}
			return (byte[])src.Clone();
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000178DE File Offset: 0x00015ADE
		public static KeySizes[] CloneKeySizesArray(this KeySizes[] src)
		{
			return (KeySizes[])src.Clone();
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000178EB File Offset: 0x00015AEB
		public static bool UsesIv(this CipherMode cipherMode)
		{
			return cipherMode != CipherMode.ECB;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x000178F4 File Offset: 0x00015AF4
		public static byte[] GetCipherIv(this CipherMode cipherMode, byte[] iv)
		{
			if (!cipherMode.UsesIv())
			{
				return null;
			}
			if (iv == null)
			{
				throw new CryptographicException("The cipher mode specified requires that an initialization vector (IV) be used.");
			}
			return iv;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00017910 File Offset: 0x00015B10
		public static bool IsLegalSize(this int size, KeySizes[] legalSizes)
		{
			foreach (KeySizes keySizes in legalSizes)
			{
				if (keySizes.SkipSize == 0)
				{
					if (keySizes.MinSize == size)
					{
						return true;
					}
				}
				else if (size >= keySizes.MinSize && size <= keySizes.MaxSize && (size - keySizes.MinSize) % keySizes.SkipSize == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00017968 File Offset: 0x00015B68
		public static byte[] GenerateRandom(int count)
		{
			byte[] array = new byte[count];
			using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
			{
				randomNumberGenerator.GetBytes(array);
			}
			return array;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000179A8 File Offset: 0x00015BA8
		public static void WriteInt(uint i, byte[] arr, int offset)
		{
			arr[offset] = (byte)(i >> 24);
			arr[offset + 1] = (byte)(i >> 16);
			arr[offset + 2] = (byte)(i >> 8);
			arr[offset + 3] = (byte)i;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000179CC File Offset: 0x00015BCC
		public static byte[] FixupKeyParity(this byte[] key)
		{
			byte[] array = new byte[key.Length];
			for (int i = 0; i < key.Length; i++)
			{
				array[i] = (key[i] & 254);
				byte b = (byte)((int)(array[i] & 15) ^ array[i] >> 4);
				byte b2 = (byte)((int)(b & 3) ^ b >> 2);
				if ((byte)((int)(b2 & 1) ^ b2 >> 1) == 0)
				{
					byte[] array2 = array;
					int num = i;
					array2[num] |= 1;
				}
			}
			return array;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00017A2C File Offset: 0x00015C2C
		internal static void ConvertIntToByteArray(uint value, byte[] dest)
		{
			dest[0] = (byte)((value & 4278190080U) >> 24);
			dest[1] = (byte)((value & 16711680U) >> 16);
			dest[2] = (byte)((value & 65280U) >> 8);
			dest[3] = (byte)(value & 255U);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00017A64 File Offset: 0x00015C64
		public static char[] ToHexArrayUpper(this byte[] bytes)
		{
			char[] array = new char[bytes.Length * 2];
			int num = 0;
			foreach (byte b in bytes)
			{
				array[num++] = Helpers.NibbleToHex((byte)(b >> 4));
				array[num++] = Helpers.NibbleToHex(b & 15);
			}
			return array;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00017AB7 File Offset: 0x00015CB7
		public static string ToHexStringUpper(this byte[] bytes)
		{
			return new string(bytes.ToHexArrayUpper());
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00017AC4 File Offset: 0x00015CC4
		public static byte[] DecodeHexString(this string s)
		{
			int num = 0;
			for (int i = 0; i < s.Length; i++)
			{
				if (char.IsWhiteSpace(s[i]))
				{
					num++;
				}
			}
			byte[] array = new byte[(s.Length - num) / 2];
			byte b = 0;
			bool flag = false;
			int num2 = 0;
			foreach (char c in s)
			{
				if (!char.IsWhiteSpace(c))
				{
					b = (byte)(b << 4);
					b |= Helpers.HexToByte(c);
					flag = !flag;
					if (!flag)
					{
						array[num2] = b;
						num2++;
					}
				}
			}
			return array;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00017B5E File Offset: 0x00015D5E
		private static byte HexToByte(char val)
		{
			if (val <= '9' && val >= '0')
			{
				return (byte)(val - '0');
			}
			if (val >= 'a' && val <= 'f')
			{
				return (byte)(val - 'a' + '\n');
			}
			if (val >= 'A' && val <= 'F')
			{
				return (byte)(val - 'A' + '\n');
			}
			return byte.MaxValue;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00017B9B File Offset: 0x00015D9B
		private static char NibbleToHex(byte b)
		{
			return (char)((b >= 0 && b <= 9) ? (48 + b) : (65 + (b - 10)));
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00017BB4 File Offset: 0x00015DB4
		public static bool ContentsEqual(this byte[] a1, byte[] a2)
		{
			if (a1.Length != a2.Length)
			{
				return false;
			}
			for (int i = 0; i < a1.Length; i++)
			{
				if (a1[i] != a2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00017BE4 File Offset: 0x00015DE4
		internal static void AddRange<T>(this ICollection<T> coll, IEnumerable<T> newData)
		{
			foreach (T item in newData)
			{
				coll.Add(item);
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00017C2C File Offset: 0x00015E2C
		public static bool IsValidDay(this Calendar calendar, int year, int month, int day, int era)
		{
			return calendar.IsValidMonth(year, month, era) && day >= 1 && day <= calendar.GetDaysInMonth(year, month, era);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00017C50 File Offset: 0x00015E50
		private static bool IsValidMonth(this Calendar calendar, int year, int month, int era)
		{
			return calendar.IsValidYear(year, era) && month >= 1 && month <= calendar.GetMonthsInYear(year, era);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00017C70 File Offset: 0x00015E70
		private static bool IsValidYear(this Calendar calendar, int year, int era)
		{
			return year >= calendar.GetYear(calendar.MinSupportedDateTime) && year <= calendar.GetYear(calendar.MaxSupportedDateTime);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00017C98 File Offset: 0x00015E98
		internal static void DisposeAll(this IEnumerable<IDisposable> disposables)
		{
			foreach (IDisposable disposable in disposables)
			{
				disposable.Dispose();
			}
		}
	}
}
