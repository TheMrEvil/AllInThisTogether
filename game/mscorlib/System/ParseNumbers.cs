using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200016E RID: 366
	internal static class ParseNumbers
	{
		// Token: 0x06000E7B RID: 3707 RVA: 0x0003AF5C File Offset: 0x0003915C
		public static long StringToLong(ReadOnlySpan<char> s, int radix, int flags)
		{
			int num = 0;
			return ParseNumbers.StringToLong(s, radix, flags, ref num);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0003AF78 File Offset: 0x00039178
		public unsafe static long StringToLong(ReadOnlySpan<char> s, int radix, int flags, ref int currPos)
		{
			int num = currPos;
			int num2 = (-1 == radix) ? 10 : radix;
			if (num2 != 2 && num2 != 10 && num2 != 8 && num2 != 16)
			{
				throw new ArgumentException("Invalid Base.", "radix");
			}
			int length = s.Length;
			if (num < 0 || num >= length)
			{
				throw new ArgumentOutOfRangeException("Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if ((flags & 4096) == 0 && (flags & 8192) == 0)
			{
				ParseNumbers.EatWhiteSpace(s, ref num);
				if (num == length)
				{
					throw new FormatException("Input string was either empty or contained only whitespace.");
				}
			}
			int num3 = 1;
			if (*s[num] == 45)
			{
				if (num2 != 10)
				{
					throw new ArgumentException("String cannot contain a minus sign if the base is not 10.");
				}
				if ((flags & 512) != 0)
				{
					throw new OverflowException("The string was being parsed as an unsigned number and could not have a negative sign.");
				}
				num3 = -1;
				num++;
			}
			else if (*s[num] == 43)
			{
				num++;
			}
			if ((radix == -1 || radix == 16) && num + 1 < length && *s[num] == 48 && (*s[num + 1] == 120 || *s[num + 1] == 88))
			{
				num2 = 16;
				num += 2;
			}
			int num4 = num;
			long num5 = ParseNumbers.GrabLongs(num2, s, ref num, (flags & 512) != 0);
			if (num == num4)
			{
				throw new FormatException("Could not find any recognizable digits.");
			}
			if ((flags & 4096) != 0 && num < length)
			{
				throw new FormatException("Additional non-parsable characters are at the end of the string.");
			}
			currPos = num;
			if (num5 == -9223372036854775808L && num3 == 1 && num2 == 10 && (flags & 512) == 0)
			{
				throw new OverflowException("Value was either too large or too small for an Int64.");
			}
			if (num2 == 10)
			{
				num5 *= (long)num3;
			}
			return num5;
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0003B100 File Offset: 0x00039300
		public static int StringToInt(ReadOnlySpan<char> s, int radix, int flags)
		{
			int num = 0;
			return ParseNumbers.StringToInt(s, radix, flags, ref num);
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0003B11C File Offset: 0x0003931C
		public unsafe static int StringToInt(ReadOnlySpan<char> s, int radix, int flags, ref int currPos)
		{
			int num = currPos;
			int num2 = (-1 == radix) ? 10 : radix;
			if (num2 != 2 && num2 != 10 && num2 != 8 && num2 != 16)
			{
				throw new ArgumentException("Invalid Base.", "radix");
			}
			int length = s.Length;
			if (num < 0 || num >= length)
			{
				throw new ArgumentOutOfRangeException("Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if ((flags & 4096) == 0 && (flags & 8192) == 0)
			{
				ParseNumbers.EatWhiteSpace(s, ref num);
				if (num == length)
				{
					throw new FormatException("Input string was either empty or contained only whitespace.");
				}
			}
			int num3 = 1;
			if (*s[num] == 45)
			{
				if (num2 != 10)
				{
					throw new ArgumentException("String cannot contain a minus sign if the base is not 10.");
				}
				if ((flags & 512) != 0)
				{
					throw new OverflowException("The string was being parsed as an unsigned number and could not have a negative sign.");
				}
				num3 = -1;
				num++;
			}
			else if (*s[num] == 43)
			{
				num++;
			}
			if ((radix == -1 || radix == 16) && num + 1 < length && *s[num] == 48 && (*s[num + 1] == 120 || *s[num + 1] == 88))
			{
				num2 = 16;
				num += 2;
			}
			int num4 = num;
			int num5 = ParseNumbers.GrabInts(num2, s, ref num, (flags & 512) != 0);
			if (num == num4)
			{
				throw new FormatException("Could not find any recognizable digits.");
			}
			if ((flags & 4096) != 0 && num < length)
			{
				throw new FormatException("Additional non-parsable characters are at the end of the string.");
			}
			currPos = num;
			if ((flags & 1024) != 0)
			{
				if (num5 > 255)
				{
					throw new OverflowException("Value was either too large or too small for a signed byte.");
				}
			}
			else if ((flags & 2048) != 0)
			{
				if (num5 > 65535)
				{
					throw new OverflowException("Value was either too large or too small for an Int16.");
				}
			}
			else if (num5 == -2147483648 && num3 == 1 && num2 == 10 && (flags & 512) == 0)
			{
				throw new OverflowException("Value was either too large or too small for an Int32.");
			}
			if (num2 == 10)
			{
				num5 *= num3;
			}
			return num5;
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0003B2DC File Offset: 0x000394DC
		public unsafe static string IntToString(int n, int radix, int width, char paddingChar, int flags)
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)132], 66);
			if (radix < 2 || radix > 36)
			{
				throw new ArgumentException("Invalid Base.", "radix");
			}
			bool flag = false;
			uint num;
			if (n < 0)
			{
				flag = true;
				num = (uint)((10 == radix) ? (-(uint)n) : n);
			}
			else
			{
				num = (uint)n;
			}
			if ((flags & 64) != 0)
			{
				num &= 255U;
			}
			else if ((flags & 128) != 0)
			{
				num &= 65535U;
			}
			int num2;
			if (num == 0U)
			{
				*span[0] = '0';
				num2 = 1;
			}
			else
			{
				num2 = 0;
				for (int i = 0; i < span.Length; i++)
				{
					uint num3 = num / (uint)radix;
					uint num4 = num - num3 * (uint)radix;
					num = num3;
					*span[i] = ((num4 < 10U) ? ((char)(num4 + 48U)) : ((char)(num4 + 97U - 10U)));
					if (num == 0U)
					{
						num2 = i + 1;
						break;
					}
				}
			}
			if (radix != 10 && (flags & 32) != 0)
			{
				if (16 == radix)
				{
					*span[num2++] = 'x';
					*span[num2++] = '0';
				}
				else if (8 == radix)
				{
					*span[num2++] = '0';
				}
			}
			if (10 == radix)
			{
				if (flag)
				{
					*span[num2++] = '-';
				}
				else if ((flags & 16) != 0)
				{
					*span[num2++] = '+';
				}
				else if ((flags & 8) != 0)
				{
					*span[num2++] = ' ';
				}
			}
			string text = string.FastAllocateString(Math.Max(width, num2));
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr;
				int num5 = text.Length - num2;
				if ((flags & 1) != 0)
				{
					for (int j = 0; j < num5; j++)
					{
						*(ptr2++) = paddingChar;
					}
					for (int k = 0; k < num2; k++)
					{
						*(ptr2++) = *span[num2 - k - 1];
					}
				}
				else
				{
					for (int l = 0; l < num2; l++)
					{
						*(ptr2++) = *span[num2 - l - 1];
					}
					for (int m = 0; m < num5; m++)
					{
						*(ptr2++) = paddingChar;
					}
				}
			}
			return text;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0003B4FC File Offset: 0x000396FC
		public unsafe static string LongToString(long n, int radix, int width, char paddingChar, int flags)
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)134], 67);
			if (radix < 2 || radix > 36)
			{
				throw new ArgumentException("Invalid Base.", "radix");
			}
			bool flag = false;
			ulong num;
			if (n < 0L)
			{
				flag = true;
				num = (ulong)((10 == radix) ? (-(ulong)n) : n);
			}
			else
			{
				num = (ulong)n;
			}
			if ((flags & 64) != 0)
			{
				num &= 255UL;
			}
			else if ((flags & 128) != 0)
			{
				num &= 65535UL;
			}
			else if ((flags & 256) != 0)
			{
				num &= (ulong)-1;
			}
			int num2;
			if (num == 0UL)
			{
				*span[0] = '0';
				num2 = 1;
			}
			else
			{
				num2 = 0;
				for (int i = 0; i < span.Length; i++)
				{
					ulong num3 = num / (ulong)((long)radix);
					int num4 = (int)(num - num3 * (ulong)((long)radix));
					num = num3;
					*span[i] = ((num4 < 10) ? ((char)(num4 + 48)) : ((char)(num4 + 97 - 10)));
					if (num == 0UL)
					{
						num2 = i + 1;
						break;
					}
				}
			}
			if (radix != 10 && (flags & 32) != 0)
			{
				if (16 == radix)
				{
					*span[num2++] = 'x';
					*span[num2++] = '0';
				}
				else if (8 == radix)
				{
					*span[num2++] = '0';
				}
				else if ((flags & 16384) != 0)
				{
					*span[num2++] = '#';
					*span[num2++] = (char)(radix % 10 + 48);
					*span[num2++] = (char)(radix / 10 + 48);
				}
			}
			if (10 == radix)
			{
				if (flag)
				{
					*span[num2++] = '-';
				}
				else if ((flags & 16) != 0)
				{
					*span[num2++] = '+';
				}
				else if ((flags & 8) != 0)
				{
					*span[num2++] = ' ';
				}
			}
			string text = string.FastAllocateString(Math.Max(width, num2));
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr;
				int num5 = text.Length - num2;
				if ((flags & 1) != 0)
				{
					for (int j = 0; j < num5; j++)
					{
						*(ptr2++) = paddingChar;
					}
					for (int k = 0; k < num2; k++)
					{
						*(ptr2++) = *span[num2 - k - 1];
					}
				}
				else
				{
					for (int l = 0; l < num2; l++)
					{
						*(ptr2++) = *span[num2 - l - 1];
					}
					for (int m = 0; m < num5; m++)
					{
						*(ptr2++) = paddingChar;
					}
				}
			}
			return text;
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0003B77C File Offset: 0x0003997C
		private unsafe static void EatWhiteSpace(ReadOnlySpan<char> s, ref int i)
		{
			int num = i;
			while (num < s.Length && char.IsWhiteSpace((char)(*s[num])))
			{
				num++;
			}
			i = num;
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0003B7B0 File Offset: 0x000399B0
		private unsafe static long GrabLongs(int radix, ReadOnlySpan<char> s, ref int i, bool isUnsigned)
		{
			ulong num = 0UL;
			if (radix == 10 && !isUnsigned)
			{
				ulong num2 = 922337203685477580UL;
				int num3;
				while (i < s.Length && ParseNumbers.IsDigit((char)(*s[i]), radix, out num3))
				{
					if (num > num2 || num < 0UL)
					{
						ParseNumbers.ThrowOverflowInt64Exception();
					}
					num = num * (ulong)((long)radix) + (ulong)((long)num3);
					i++;
				}
				if (num < 0UL && num != 9223372036854775808UL)
				{
					ParseNumbers.ThrowOverflowInt64Exception();
				}
			}
			else
			{
				ulong num2 = (radix == 10) ? 1844674407370955161UL : ((radix == 16) ? 1152921504606846975UL : ((radix == 8) ? 2305843009213693951UL : 9223372036854775807UL));
				int num4;
				while (i < s.Length && ParseNumbers.IsDigit((char)(*s[i]), radix, out num4))
				{
					if (num > num2)
					{
						ParseNumbers.ThrowOverflowUInt64Exception();
					}
					ulong num5 = num * (ulong)((long)radix) + (ulong)((long)num4);
					if (num5 < num)
					{
						ParseNumbers.ThrowOverflowUInt64Exception();
					}
					num = num5;
					i++;
				}
			}
			return (long)num;
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0003B8A8 File Offset: 0x00039AA8
		private unsafe static int GrabInts(int radix, ReadOnlySpan<char> s, ref int i, bool isUnsigned)
		{
			uint num = 0U;
			if (radix == 10 && !isUnsigned)
			{
				uint num2 = 214748364U;
				int num3;
				while (i < s.Length && ParseNumbers.IsDigit((char)(*s[i]), radix, out num3))
				{
					if (num > num2 || num < 0U)
					{
						ParseNumbers.ThrowOverflowInt32Exception();
					}
					num = num * (uint)radix + (uint)num3;
					i++;
				}
				if (num < 0U && num != 2147483648U)
				{
					ParseNumbers.ThrowOverflowInt32Exception();
				}
			}
			else
			{
				uint num2 = (radix == 10) ? 429496729U : ((radix == 16) ? 268435455U : ((radix == 8) ? 536870911U : 2147483647U));
				int num4;
				while (i < s.Length && ParseNumbers.IsDigit((char)(*s[i]), radix, out num4))
				{
					if (num > num2)
					{
						throw new OverflowException("Value was either too large or too small for a UInt32.");
					}
					uint num5 = num * (uint)radix + (uint)num4;
					if (num5 < num)
					{
						ParseNumbers.ThrowOverflowUInt32Exception();
					}
					num = num5;
					i++;
				}
			}
			return (int)num;
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00023B2D File Offset: 0x00021D2D
		private static void ThrowOverflowInt32Exception()
		{
			throw new OverflowException("Value was either too large or too small for an Int32.");
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00023B45 File Offset: 0x00021D45
		private static void ThrowOverflowInt64Exception()
		{
			throw new OverflowException("Value was either too large or too small for an Int64.");
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00023B39 File Offset: 0x00021D39
		private static void ThrowOverflowUInt32Exception()
		{
			throw new OverflowException("Value was either too large or too small for a UInt32.");
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00023B51 File Offset: 0x00021D51
		private static void ThrowOverflowUInt64Exception()
		{
			throw new OverflowException("Value was either too large or too small for a UInt64.");
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0003B984 File Offset: 0x00039B84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsDigit(char c, int radix, out int result)
		{
			int num;
			if (c - '0' <= '\t')
			{
				num = (result = (int)(c - '0'));
			}
			else if (c - 'A' <= '\u0019')
			{
				num = (result = (int)(c - 'A' + '\n'));
			}
			else
			{
				if (c - 'a' > '\u0019')
				{
					result = -1;
					return false;
				}
				num = (result = (int)(c - 'a' + '\n'));
			}
			return num < radix;
		}

		// Token: 0x040012AF RID: 4783
		internal const int LeftAlign = 1;

		// Token: 0x040012B0 RID: 4784
		internal const int RightAlign = 4;

		// Token: 0x040012B1 RID: 4785
		internal const int PrefixSpace = 8;

		// Token: 0x040012B2 RID: 4786
		internal const int PrintSign = 16;

		// Token: 0x040012B3 RID: 4787
		internal const int PrintBase = 32;

		// Token: 0x040012B4 RID: 4788
		internal const int PrintAsI1 = 64;

		// Token: 0x040012B5 RID: 4789
		internal const int PrintAsI2 = 128;

		// Token: 0x040012B6 RID: 4790
		internal const int PrintAsI4 = 256;

		// Token: 0x040012B7 RID: 4791
		internal const int TreatAsUnsigned = 512;

		// Token: 0x040012B8 RID: 4792
		internal const int TreatAsI1 = 1024;

		// Token: 0x040012B9 RID: 4793
		internal const int TreatAsI2 = 2048;

		// Token: 0x040012BA RID: 4794
		internal const int IsTight = 4096;

		// Token: 0x040012BB RID: 4795
		internal const int NoSpace = 8192;

		// Token: 0x040012BC RID: 4796
		internal const int PrintRadixBase = 16384;

		// Token: 0x040012BD RID: 4797
		private const int MinRadix = 2;

		// Token: 0x040012BE RID: 4798
		private const int MaxRadix = 36;
	}
}
