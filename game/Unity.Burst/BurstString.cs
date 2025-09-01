using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Burst
{
	// Token: 0x02000011 RID: 17
	internal static class BurstString
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00002F94 File Offset: 0x00001194
		[BurstString.PreserveAttribute]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void CopyFixedString(byte* dest, int destLength, byte* src, int srcLength)
		{
			int num = (srcLength > destLength) ? destLength : srcLength;
			*(short*)(dest - 2) = (short)((ushort)num);
			dest[num] = 0;
			UnsafeUtility.MemCpy((void*)dest, (void*)src, (long)num);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002FC0 File Offset: 0x000011C0
		[BurstString.PreserveAttribute]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, byte* src, int srcLength, int formatOptionsRaw)
		{
			BurstString.FormatOptions formatOptions = *(BurstString.FormatOptions*)(&formatOptionsRaw);
			if (BurstString.AlignLeft(dest, ref destIndex, destLength, (int)formatOptions.AlignAndSize, srcLength))
			{
				return;
			}
			int num = destLength - destIndex;
			int num2 = (srcLength > num) ? num : srcLength;
			if (num2 > 0)
			{
				UnsafeUtility.MemCpy((void*)(dest + destIndex), (void*)src, (long)num2);
				destIndex += num2;
				BurstString.AlignRight(dest, ref destIndex, destLength, (int)formatOptions.AlignAndSize, srcLength);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003020 File Offset: 0x00001220
		[BurstString.PreserveAttribute]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, float value, int formatOptionsRaw)
		{
			BurstString.FormatOptions formatOptions = *(BurstString.FormatOptions*)(&formatOptionsRaw);
			BurstString.ConvertFloatToString(dest, ref destIndex, destLength, value, formatOptions);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003040 File Offset: 0x00001240
		[BurstString.PreserveAttribute]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, double value, int formatOptionsRaw)
		{
			BurstString.FormatOptions formatOptions = *(BurstString.FormatOptions*)(&formatOptionsRaw);
			BurstString.ConvertDoubleToString(dest, ref destIndex, destLength, value, formatOptions);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003060 File Offset: 0x00001260
		[BurstString.PreserveAttribute]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, bool value, int formatOptionsRaw)
		{
			int length = value ? 4 : 5;
			BurstString.FormatOptions formatOptions = *(BurstString.FormatOptions*)(&formatOptionsRaw);
			if (BurstString.AlignLeft(dest, ref destIndex, destLength, (int)formatOptions.AlignAndSize, length))
			{
				return;
			}
			if (value)
			{
				if (destIndex >= destLength)
				{
					return;
				}
				int num = destIndex;
				destIndex = num + 1;
				dest[num] = 84;
				if (destIndex >= destLength)
				{
					return;
				}
				num = destIndex;
				destIndex = num + 1;
				dest[num] = 114;
				if (destIndex >= destLength)
				{
					return;
				}
				num = destIndex;
				destIndex = num + 1;
				dest[num] = 117;
				if (destIndex >= destLength)
				{
					return;
				}
				num = destIndex;
				destIndex = num + 1;
				dest[num] = 101;
			}
			else
			{
				if (destIndex >= destLength)
				{
					return;
				}
				int num = destIndex;
				destIndex = num + 1;
				dest[num] = 70;
				if (destIndex >= destLength)
				{
					return;
				}
				num = destIndex;
				destIndex = num + 1;
				dest[num] = 97;
				if (destIndex >= destLength)
				{
					return;
				}
				num = destIndex;
				destIndex = num + 1;
				dest[num] = 108;
				if (destIndex >= destLength)
				{
					return;
				}
				num = destIndex;
				destIndex = num + 1;
				dest[num] = 115;
				if (destIndex >= destLength)
				{
					return;
				}
				num = destIndex;
				destIndex = num + 1;
				dest[num] = 101;
			}
			BurstString.AlignRight(dest, ref destIndex, destLength, (int)formatOptions.AlignAndSize, length);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000315C File Offset: 0x0000135C
		[BurstString.PreserveAttribute]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, char value, int formatOptionsRaw)
		{
			int num = (value <= '\u007f') ? 1 : ((value <= '߿') ? 2 : 3);
			BurstString.FormatOptions formatOptions = *(BurstString.FormatOptions*)(&formatOptionsRaw);
			if (BurstString.AlignLeft(dest, ref destIndex, destLength, (int)formatOptions.AlignAndSize, 1))
			{
				return;
			}
			if (num == 1)
			{
				if (destIndex >= destLength)
				{
					return;
				}
				int num2 = destIndex;
				destIndex = num2 + 1;
				dest[num2] = (byte)value;
			}
			else if (num == 2)
			{
				if (destIndex >= destLength)
				{
					return;
				}
				int num2 = destIndex;
				destIndex = num2 + 1;
				dest[num2] = (byte)(value >> 6 | 'À');
				if (destIndex >= destLength)
				{
					return;
				}
				num2 = destIndex;
				destIndex = num2 + 1;
				dest[num2] = (byte)((value & '?') | '\u0080');
			}
			else if (num == 3)
			{
				if (value >= '\ud800' && value <= '\udfff')
				{
					if (destIndex >= destLength)
					{
						return;
					}
					int num2 = destIndex;
					destIndex = num2 + 1;
					dest[num2] = 239;
					if (destIndex >= destLength)
					{
						return;
					}
					num2 = destIndex;
					destIndex = num2 + 1;
					dest[num2] = 191;
					if (destIndex >= destLength)
					{
						return;
					}
					num2 = destIndex;
					destIndex = num2 + 1;
					dest[num2] = 189;
				}
				else
				{
					if (destIndex >= destLength)
					{
						return;
					}
					int num2 = destIndex;
					destIndex = num2 + 1;
					dest[num2] = (byte)(value >> 12 | 'à');
					if (destIndex >= destLength)
					{
						return;
					}
					num2 = destIndex;
					destIndex = num2 + 1;
					dest[num2] = (byte)((value >> 6 & '?') | '\u0080');
					if (destIndex >= destLength)
					{
						return;
					}
					num2 = destIndex;
					destIndex = num2 + 1;
					dest[num2] = (byte)((value & '?') | '\u0080');
				}
			}
			BurstString.AlignRight(dest, ref destIndex, destLength, (int)formatOptions.AlignAndSize, 1);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000032C7 File Offset: 0x000014C7
		[BurstString.PreserveAttribute]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, byte value, int formatOptionsRaw)
		{
			BurstString.Format(dest, ref destIndex, destLength, (ulong)value, formatOptionsRaw);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000032D5 File Offset: 0x000014D5
		[BurstString.PreserveAttribute]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, ushort value, int formatOptionsRaw)
		{
			BurstString.Format(dest, ref destIndex, destLength, (ulong)value, formatOptionsRaw);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000032E4 File Offset: 0x000014E4
		[BurstString.PreserveAttribute]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, uint value, int formatOptionsRaw)
		{
			BurstString.FormatOptions options = *(BurstString.FormatOptions*)(&formatOptionsRaw);
			BurstString.ConvertUnsignedIntegerToString(dest, ref destIndex, destLength, (ulong)value, options);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003308 File Offset: 0x00001508
		[BurstString.PreserveAttribute]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, ulong value, int formatOptionsRaw)
		{
			BurstString.FormatOptions options = *(BurstString.FormatOptions*)(&formatOptionsRaw);
			BurstString.ConvertUnsignedIntegerToString(dest, ref destIndex, destLength, value, options);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003328 File Offset: 0x00001528
		[BurstString.PreserveAttribute]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, sbyte value, int formatOptionsRaw)
		{
			BurstString.FormatOptions formatOptions = *(BurstString.FormatOptions*)(&formatOptionsRaw);
			if (formatOptions.Kind == BurstString.NumberFormatKind.Hexadecimal)
			{
				BurstString.ConvertUnsignedIntegerToString(dest, ref destIndex, destLength, (ulong)((byte)value), formatOptions);
				return;
			}
			BurstString.ConvertIntegerToString(dest, ref destIndex, destLength, (long)value, formatOptions);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003360 File Offset: 0x00001560
		[BurstString.PreserveAttribute]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, short value, int formatOptionsRaw)
		{
			BurstString.FormatOptions formatOptions = *(BurstString.FormatOptions*)(&formatOptionsRaw);
			if (formatOptions.Kind == BurstString.NumberFormatKind.Hexadecimal)
			{
				BurstString.ConvertUnsignedIntegerToString(dest, ref destIndex, destLength, (ulong)((ushort)value), formatOptions);
				return;
			}
			BurstString.ConvertIntegerToString(dest, ref destIndex, destLength, (long)value, formatOptions);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003398 File Offset: 0x00001598
		[BurstString.PreserveAttribute]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, int value, int formatOptionsRaw)
		{
			BurstString.FormatOptions formatOptions = *(BurstString.FormatOptions*)(&formatOptionsRaw);
			if (formatOptions.Kind == BurstString.NumberFormatKind.Hexadecimal)
			{
				BurstString.ConvertUnsignedIntegerToString(dest, ref destIndex, destLength, (ulong)value, formatOptions);
				return;
			}
			BurstString.ConvertIntegerToString(dest, ref destIndex, destLength, (long)value, formatOptions);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000033D0 File Offset: 0x000015D0
		[BurstString.PreserveAttribute]
		public unsafe static void Format(byte* dest, ref int destIndex, int destLength, long value, int formatOptionsRaw)
		{
			BurstString.FormatOptions formatOptions = *(BurstString.FormatOptions*)(&formatOptionsRaw);
			if (formatOptions.Kind == BurstString.NumberFormatKind.Hexadecimal)
			{
				BurstString.ConvertUnsignedIntegerToString(dest, ref destIndex, destLength, (ulong)value, formatOptions);
				return;
			}
			BurstString.ConvertIntegerToString(dest, ref destIndex, destLength, value, formatOptions);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003404 File Offset: 0x00001604
		[MethodImpl(MethodImplOptions.NoInlining)]
		private unsafe static void ConvertUnsignedIntegerToString(byte* dest, ref int destIndex, int destLength, ulong value, BurstString.FormatOptions options)
		{
			uint @base = (uint)options.GetBase();
			if (@base < 2U || @base > 36U)
			{
				return;
			}
			int num = 0;
			ulong num2 = value;
			do
			{
				num2 /= (ulong)@base;
				num++;
			}
			while (num2 != 0UL);
			int num3 = num - 1;
			byte* ptr = stackalloc byte[(UIntPtr)(num + 1)];
			num2 = value;
			do
			{
				ptr[num3--] = BurstString.ValueToIntegerChar((int)(num2 % (ulong)@base), options.Uppercase);
				num2 /= (ulong)@base;
			}
			while (num2 != 0UL);
			ptr[num] = 0;
			BurstString.NumberBuffer numberBuffer = new BurstString.NumberBuffer(BurstString.NumberBufferKind.Integer, ptr, num, num, false);
			BurstString.FormatNumber(dest, ref destIndex, destLength, ref numberBuffer, (int)options.Specifier, options);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000348C File Offset: 0x0000168C
		private static int GetLengthIntegerToString(long value, int basis, int zeroPadding)
		{
			int num = 0;
			long num2 = value;
			do
			{
				num2 /= (long)basis;
				num++;
			}
			while (num2 != 0L);
			if (num < zeroPadding)
			{
				num = zeroPadding;
			}
			if (value < 0L)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000034BC File Offset: 0x000016BC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private unsafe static void ConvertIntegerToString(byte* dest, ref int destIndex, int destLength, long value, BurstString.FormatOptions options)
		{
			int @base = options.GetBase();
			if (@base < 2 || @base > 36)
			{
				return;
			}
			int num = 0;
			long num2 = value;
			do
			{
				num2 /= (long)@base;
				num++;
			}
			while (num2 != 0L);
			byte* ptr = stackalloc byte[(UIntPtr)(num + 1)];
			num2 = value;
			int num3 = num - 1;
			do
			{
				ptr[num3--] = BurstString.ValueToIntegerChar((int)(num2 % (long)@base), options.Uppercase);
				num2 /= (long)@base;
			}
			while (num2 != 0L);
			ptr[num] = 0;
			BurstString.NumberBuffer numberBuffer = new BurstString.NumberBuffer(BurstString.NumberBufferKind.Integer, ptr, num, num, value < 0L);
			BurstString.FormatNumber(dest, ref destIndex, destLength, ref numberBuffer, (int)options.Specifier, options);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003544 File Offset: 0x00001744
		private unsafe static void FormatNumber(byte* dest, ref int destIndex, int destLength, ref BurstString.NumberBuffer number, int nMaxDigits, BurstString.FormatOptions options)
		{
			bool isCorrectlyRounded = number.Kind == BurstString.NumberBufferKind.Float;
			if (number.Kind == BurstString.NumberBufferKind.Integer && options.Kind == BurstString.NumberFormatKind.General && options.Specifier == 0)
			{
				options.Kind = BurstString.NumberFormatKind.Decimal;
			}
			BurstString.NumberFormatKind kind = options.Kind;
			if (kind != BurstString.NumberFormatKind.General && kind - BurstString.NumberFormatKind.Decimal <= 2)
			{
				int num = number.DigitsCount;
				int specifier = (int)options.Specifier;
				int zeroPadding = 0;
				if (num < specifier)
				{
					zeroPadding = specifier - num;
					num = specifier;
				}
				bool flag = options.Kind == BurstString.NumberFormatKind.DecimalForceSigned;
				num += ((number.IsNegative || flag) ? 1 : 0);
				if (BurstString.AlignLeft(dest, ref destIndex, destLength, (int)options.AlignAndSize, num))
				{
					return;
				}
				BurstString.FormatDecimalOrHexadecimal(dest, ref destIndex, destLength, ref number, zeroPadding, flag);
				BurstString.AlignRight(dest, ref destIndex, destLength, (int)options.AlignAndSize, num);
				return;
			}
			else
			{
				if (nMaxDigits < 1)
				{
					nMaxDigits = number.DigitsCount;
				}
				BurstString.RoundNumber(ref number, nMaxDigits, isCorrectlyRounded);
				int num = BurstString.GetLengthForFormatGeneral(ref number, nMaxDigits);
				if (BurstString.AlignLeft(dest, ref destIndex, destLength, (int)options.AlignAndSize, num))
				{
					return;
				}
				BurstString.FormatGeneral(dest, ref destIndex, destLength, ref number, nMaxDigits, options.Uppercase ? 69 : 101);
				BurstString.AlignRight(dest, ref destIndex, destLength, (int)options.AlignAndSize, num);
				return;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003658 File Offset: 0x00001858
		private unsafe static void FormatDecimalOrHexadecimal(byte* dest, ref int destIndex, int destLength, ref BurstString.NumberBuffer number, int zeroPadding, bool outputPositiveSign)
		{
			if (number.IsNegative)
			{
				if (destIndex >= destLength)
				{
					return;
				}
				int num = destIndex;
				destIndex = num + 1;
				dest[num] = 45;
			}
			else if (outputPositiveSign)
			{
				if (destIndex >= destLength)
				{
					return;
				}
				int num = destIndex;
				destIndex = num + 1;
				dest[num] = 43;
			}
			for (int i = 0; i < zeroPadding; i++)
			{
				if (destIndex >= destLength)
				{
					return;
				}
				int num = destIndex;
				destIndex = num + 1;
				dest[num] = 48;
			}
			int digitsCount = number.DigitsCount;
			byte* digitsPointer = number.GetDigitsPointer();
			for (int j = 0; j < digitsCount; j++)
			{
				if (destIndex >= destLength)
				{
					return;
				}
				int num = destIndex;
				destIndex = num + 1;
				dest[num] = digitsPointer[j];
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000036F1 File Offset: 0x000018F1
		private static byte ValueToIntegerChar(int value, bool uppercase)
		{
			value = ((value < 0) ? (-value) : value);
			if (value <= 9)
			{
				return (byte)(48 + value);
			}
			if (value < 36)
			{
				return (byte)((uppercase ? 65 : 97) + (value - 10));
			}
			return 63;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003720 File Offset: 0x00001920
		private static void OptsSplit(string fullFormat, out string padding, out string format)
		{
			string[] array = fullFormat.Split(BurstString.SplitByColon, StringSplitOptions.RemoveEmptyEntries);
			format = array[0];
			padding = null;
			if (array.Length == 2)
			{
				padding = format;
				format = array[1];
				return;
			}
			if (array.Length != 1)
			{
				throw new ArgumentException(string.Format("Format `{0}` not supported. Invalid number {1} of :. Expecting no more than one.", format, array.Length));
			}
			if (format[0] == ',')
			{
				padding = format;
				format = null;
				return;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003788 File Offset: 0x00001988
		public static BurstString.FormatOptions ParseFormatToFormatOptions(string fullFormat)
		{
			if (string.IsNullOrWhiteSpace(fullFormat))
			{
				return default(BurstString.FormatOptions);
			}
			string text;
			string text2;
			BurstString.OptsSplit(fullFormat, out text, out text2);
			text2 = ((text2 != null) ? text2.Trim() : null);
			text = ((text != null) ? text.Trim() : null);
			int num = 0;
			BurstString.NumberFormatKind kind = BurstString.NumberFormatKind.General;
			bool lowercase = false;
			int num2 = 0;
			if (!string.IsNullOrEmpty(text2))
			{
				char c = text2[0];
				if (c <= 'X')
				{
					if (c == 'D')
					{
						kind = BurstString.NumberFormatKind.Decimal;
						goto IL_BA;
					}
					if (c == 'G')
					{
						kind = BurstString.NumberFormatKind.General;
						goto IL_BA;
					}
					if (c == 'X')
					{
						kind = BurstString.NumberFormatKind.Hexadecimal;
						goto IL_BA;
					}
				}
				else
				{
					if (c == 'd')
					{
						kind = BurstString.NumberFormatKind.Decimal;
						lowercase = true;
						goto IL_BA;
					}
					if (c == 'g')
					{
						kind = BurstString.NumberFormatKind.General;
						lowercase = true;
						goto IL_BA;
					}
					if (c == 'x')
					{
						kind = BurstString.NumberFormatKind.Hexadecimal;
						lowercase = true;
						goto IL_BA;
					}
				}
				throw new ArgumentException("Format `" + text2 + "` not supported. Only G, g, D, d, X, x are supported.");
				IL_BA:
				if (text2.Length > 1)
				{
					string text3 = text2.Substring(1);
					uint num3;
					if (!uint.TryParse(text3, out num3))
					{
						throw new ArgumentException(string.Concat(new string[]
						{
							"Expecting an unsigned integer for specifier `",
							text2,
							"` instead of ",
							text3,
							"."
						}));
					}
					num2 = (int)num3;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				if (text[0] != ',')
				{
					throw new ArgumentException("Invalid padding `" + text + "`, expecting to start with a leading `,` comma.");
				}
				string text4 = text.Substring(1);
				if (!int.TryParse(text4, out num))
				{
					throw new ArgumentException("Expecting an integer for align/size padding `" + text4 + "`.");
				}
			}
			return new BurstString.FormatOptions(kind, (sbyte)num, (byte)num2, lowercase);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003903 File Offset: 0x00001B03
		private unsafe static bool AlignRight(byte* dest, ref int destIndex, int destLength, int align, int length)
		{
			if (align < 0)
			{
				align = -align;
				return BurstString.AlignLeft(dest, ref destIndex, destLength, align, length);
			}
			return false;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000391C File Offset: 0x00001B1C
		private unsafe static bool AlignLeft(byte* dest, ref int destIndex, int destLength, int align, int length)
		{
			if (align > 0)
			{
				while (length < align)
				{
					if (destIndex >= destLength)
					{
						return true;
					}
					int num = destIndex;
					destIndex = num + 1;
					dest[num] = 32;
					length++;
				}
			}
			return false;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003950 File Offset: 0x00001B50
		private unsafe static int GetLengthForFormatGeneral(ref BurstString.NumberBuffer number, int nMaxDigits)
		{
			int num = 0;
			int i = number.Scale;
			bool flag = false;
			if (i > nMaxDigits || i < -3)
			{
				i = 1;
				flag = true;
			}
			byte* ptr = number.GetDigitsPointer();
			if (number.IsNegative)
			{
				num++;
			}
			if (i > 0)
			{
				do
				{
					if (*ptr != 0)
					{
						ptr++;
					}
					num++;
				}
				while (--i > 0);
			}
			else
			{
				num++;
			}
			if (*ptr != 0 || i < 0)
			{
				num++;
				while (i < 0)
				{
					num++;
					i++;
				}
				while (*ptr != 0)
				{
					num++;
					ptr++;
				}
			}
			if (flag)
			{
				num++;
				int num2 = number.Scale - 1;
				if (num2 >= 0)
				{
					num++;
				}
				num += BurstString.GetLengthIntegerToString((long)num2, 10, 2);
			}
			return num;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000039F8 File Offset: 0x00001BF8
		[MethodImpl(MethodImplOptions.NoInlining)]
		private unsafe static void FormatGeneral(byte* dest, ref int destIndex, int destLength, ref BurstString.NumberBuffer number, int nMaxDigits, byte expChar)
		{
			int i = number.Scale;
			bool flag = false;
			if (i > nMaxDigits || i < -3)
			{
				i = 1;
				flag = true;
			}
			byte* digitsPointer = number.GetDigitsPointer();
			int num;
			if (number.IsNegative)
			{
				if (destIndex >= destLength)
				{
					return;
				}
				num = destIndex;
				destIndex = num + 1;
				dest[num] = 45;
			}
			if (i > 0)
			{
				while (destIndex < destLength)
				{
					num = destIndex;
					destIndex = num + 1;
					dest[num] = ((*digitsPointer != 0) ? (*(digitsPointer++)) : 48);
					if (--i <= 0)
					{
						goto IL_7C;
					}
				}
				return;
			}
			if (destIndex >= destLength)
			{
				return;
			}
			num = destIndex;
			destIndex = num + 1;
			dest[num] = 48;
			IL_7C:
			if (*digitsPointer != 0 || i < 0)
			{
				if (destIndex >= destLength)
				{
					return;
				}
				num = destIndex;
				destIndex = num + 1;
				dest[num] = 46;
				while (i < 0)
				{
					if (destIndex >= destLength)
					{
						return;
					}
					num = destIndex;
					destIndex = num + 1;
					dest[num] = 48;
					i++;
				}
				while (*digitsPointer != 0)
				{
					if (destIndex >= destLength)
					{
						return;
					}
					num = destIndex;
					destIndex = num + 1;
					dest[num] = *(digitsPointer++);
				}
			}
			if (flag)
			{
				if (destIndex >= destLength)
				{
					return;
				}
				num = destIndex;
				destIndex = num + 1;
				dest[num] = expChar;
				int num2 = number.Scale - 1;
				BurstString.FormatOptions options = new BurstString.FormatOptions(BurstString.NumberFormatKind.DecimalForceSigned, 0, 2, false);
				BurstString.ConvertIntegerToString(dest, ref destIndex, destLength, (long)num2, options);
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003B14 File Offset: 0x00001D14
		private unsafe static void RoundNumber(ref BurstString.NumberBuffer number, int pos, bool isCorrectlyRounded)
		{
			byte* digitsPointer = number.GetDigitsPointer();
			int num = 0;
			while (num < pos && digitsPointer[num] != 0)
			{
				num++;
			}
			if (num == pos && BurstString.ShouldRoundUp(digitsPointer, num, isCorrectlyRounded))
			{
				while (num > 0 && digitsPointer[num - 1] == 57)
				{
					num--;
				}
				if (num > 0)
				{
					byte* ptr = digitsPointer + (num - 1);
					*ptr += 1;
				}
				else
				{
					number.Scale++;
					*digitsPointer = 49;
					num = 1;
				}
			}
			else
			{
				while (num > 0 && digitsPointer[num - 1] == 48)
				{
					num--;
				}
			}
			if (num == 0)
			{
				number.Scale = 0;
			}
			digitsPointer[num] = 0;
			number.DigitsCount = num;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003BA8 File Offset: 0x00001DA8
		private unsafe static bool ShouldRoundUp(byte* dig, int i, bool isCorrectlyRounded)
		{
			byte b = dig[i];
			return b != 0 && !isCorrectlyRounded && b >= 53;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003BCC File Offset: 0x00001DCC
		private static uint LogBase2(uint val)
		{
			uint num = val >> 24;
			if (num != 0U)
			{
				return (uint)(24 + BurstString.logTable[(int)num]);
			}
			num = val >> 16;
			if (num != 0U)
			{
				return (uint)(16 + BurstString.logTable[(int)num]);
			}
			num = val >> 8;
			if (num != 0U)
			{
				return (uint)(8 + BurstString.logTable[(int)num]);
			}
			return (uint)BurstString.logTable[(int)val];
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003C18 File Offset: 0x00001E18
		private unsafe static int BigInt_Compare(in BurstString.tBigInt lhs, in BurstString.tBigInt rhs)
		{
			int num = lhs.m_length - rhs.m_length;
			if (num != 0)
			{
				return num;
			}
			int i = lhs.m_length - 1;
			while (i >= 0)
			{
				if (*(ref lhs.m_blocks.FixedElementField + (IntPtr)i * 4) != *(ref rhs.m_blocks.FixedElementField + (IntPtr)i * 4))
				{
					if (*(ref lhs.m_blocks.FixedElementField + (IntPtr)i * 4) > *(ref rhs.m_blocks.FixedElementField + (IntPtr)i * 4))
					{
						return 1;
					}
					return -1;
				}
				else
				{
					i--;
				}
			}
			return 0;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003C98 File Offset: 0x00001E98
		private static void BigInt_Add(out BurstString.tBigInt pResult, in BurstString.tBigInt lhs, in BurstString.tBigInt rhs)
		{
			if (lhs.m_length < rhs.m_length)
			{
				BurstString.BigInt_Add_internal(out pResult, rhs, lhs);
				return;
			}
			BurstString.BigInt_Add_internal(out pResult, lhs, rhs);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003CBC File Offset: 0x00001EBC
		private unsafe static void BigInt_Add_internal(out BurstString.tBigInt pResult, in BurstString.tBigInt pLarge, in BurstString.tBigInt pSmall)
		{
			int length = pLarge.m_length;
			int length2 = pSmall.m_length;
			pResult.m_length = length;
			ulong num = 0UL;
			fixed (uint* ptr = &pLarge.m_blocks.FixedElementField)
			{
				uint* ptr2 = ptr;
				fixed (uint* ptr3 = &pSmall.m_blocks.FixedElementField)
				{
					uint* ptr4 = ptr3;
					fixed (uint* ptr5 = &pResult.m_blocks.FixedElementField)
					{
						uint* ptr6 = ptr5;
						uint* ptr7 = ptr2;
						uint* ptr8 = ptr4;
						uint* ptr9 = ptr6;
						uint* ptr10 = ptr7 + length;
						uint* ptr11 = ptr8 + length2;
						while (ptr8 != ptr11)
						{
							ulong num2 = num + (ulong)(*ptr7) + (ulong)(*ptr8);
							num = num2 >> 32;
							*ptr9 = (uint)(num2 & (ulong)-1);
							ptr7++;
							ptr8++;
							ptr9++;
						}
						while (ptr7 != ptr10)
						{
							ulong num3 = num + (ulong)(*ptr7);
							num = num3 >> 32;
							*ptr9 = (uint)(num3 & (ulong)-1);
							ptr7++;
							ptr9++;
						}
						if (num != 0UL)
						{
							*ptr9 = 1U;
							pResult.m_length = length + 1;
						}
						else
						{
							pResult.m_length = length;
						}
					}
				}
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003DB5 File Offset: 0x00001FB5
		private static void BigInt_Multiply(out BurstString.tBigInt pResult, in BurstString.tBigInt lhs, in BurstString.tBigInt rhs)
		{
			if (lhs.m_length < rhs.m_length)
			{
				BurstString.BigInt_Multiply_internal(out pResult, rhs, lhs);
				return;
			}
			BurstString.BigInt_Multiply_internal(out pResult, lhs, rhs);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003DD8 File Offset: 0x00001FD8
		private unsafe static void BigInt_Multiply_internal(out BurstString.tBigInt pResult, in BurstString.tBigInt pLarge, in BurstString.tBigInt pSmall)
		{
			int num = pLarge.m_length + pSmall.m_length;
			for (int i = 0; i < num; i++)
			{
				*(ref pResult.m_blocks.FixedElementField + (IntPtr)i * 4) = 0U;
			}
			fixed (uint* ptr = &pLarge.m_blocks.FixedElementField)
			{
				uint* ptr2 = ptr;
				uint* ptr3 = ptr2 + pLarge.m_length;
				fixed (uint* ptr4 = &pResult.m_blocks.FixedElementField)
				{
					uint* ptr5 = ptr4;
					fixed (uint* ptr6 = &pSmall.m_blocks.FixedElementField)
					{
						uint* ptr7 = ptr6;
						uint* ptr8 = ptr7 + pSmall.m_length;
						uint* ptr9 = ptr5;
						while (ptr7 != ptr8)
						{
							uint num2 = *ptr7;
							if (num2 != 0U)
							{
								uint* ptr10 = ptr2;
								uint* ptr11 = ptr9;
								ulong num3 = 0UL;
								do
								{
									ulong num4 = (ulong)(*ptr11) + (ulong)(*ptr10) * (ulong)num2 + num3;
									num3 = num4 >> 32;
									*ptr11 = (uint)(num4 & (ulong)-1);
									ptr10++;
									ptr11++;
								}
								while (ptr10 != ptr3);
								*ptr11 = (uint)(num3 & (ulong)-1);
							}
							ptr7++;
							ptr9++;
						}
						if (num > 0 && *(ref pResult.m_blocks.FixedElementField + (IntPtr)(num - 1) * 4) == 0U)
						{
							pResult.m_length = num - 1;
						}
						else
						{
							pResult.m_length = num;
						}
					}
				}
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003EFC File Offset: 0x000020FC
		private unsafe static void BigInt_Multiply(out BurstString.tBigInt pResult, in BurstString.tBigInt lhs, uint rhs)
		{
			uint num = 0U;
			fixed (uint* ptr = &pResult.m_blocks.FixedElementField)
			{
				uint* ptr2 = ptr;
				fixed (uint* ptr3 = &lhs.m_blocks.FixedElementField)
				{
					uint* ptr4 = ptr3;
					uint* ptr5 = ptr2;
					uint* ptr6 = ptr4;
					uint* ptr7 = ptr6 + lhs.m_length;
					while (ptr6 != ptr7)
					{
						ulong num2 = (ulong)(*ptr6) * (ulong)rhs + (ulong)num;
						*ptr5 = (uint)(num2 & (ulong)-1);
						num = (uint)(num2 >> 32);
						ptr6++;
						ptr5++;
					}
					if (num != 0U)
					{
						*ptr5 = num;
						pResult.m_length = lhs.m_length + 1;
					}
					else
					{
						pResult.m_length = lhs.m_length;
					}
				}
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003F94 File Offset: 0x00002194
		private unsafe static void BigInt_Multiply2(out BurstString.tBigInt pResult, in BurstString.tBigInt input)
		{
			uint num = 0U;
			fixed (uint* ptr = &pResult.m_blocks.FixedElementField)
			{
				uint* ptr2 = ptr;
				fixed (uint* ptr3 = &input.m_blocks.FixedElementField)
				{
					uint* ptr4 = ptr3;
					uint* ptr5 = ptr2;
					uint* ptr6 = ptr4;
					uint* ptr7 = ptr6 + input.m_length;
					while (ptr6 != ptr7)
					{
						uint num2 = *ptr6;
						*ptr5 = (num2 << 1 | num);
						num = num2 >> 31;
						ptr6++;
						ptr5++;
					}
					if (num != 0U)
					{
						*ptr5 = num;
						pResult.m_length = input.m_length + 1;
					}
					else
					{
						pResult.m_length = input.m_length;
					}
				}
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004024 File Offset: 0x00002224
		private unsafe static void BigInt_Multiply2(ref BurstString.tBigInt pResult)
		{
			uint num = 0U;
			fixed (uint* ptr = &pResult.m_blocks.FixedElementField)
			{
				uint* ptr2 = ptr;
				uint* ptr3 = ptr2 + pResult.m_length;
				while (ptr2 != ptr3)
				{
					uint num2 = *ptr2;
					*ptr2 = (num2 << 1 | num);
					num = num2 >> 31;
					ptr2++;
				}
				if (num != 0U)
				{
					*ptr2 = num;
					pResult.m_length++;
				}
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004080 File Offset: 0x00002280
		private unsafe static void BigInt_Multiply10(ref BurstString.tBigInt pResult)
		{
			ulong num = 0UL;
			fixed (uint* ptr = &pResult.m_blocks.FixedElementField)
			{
				uint* ptr2 = ptr;
				uint* ptr3 = ptr2 + pResult.m_length;
				while (ptr2 != ptr3)
				{
					ulong num2 = (ulong)(*ptr2) * 10UL + num;
					*ptr2 = (uint)(num2 & (ulong)-1);
					num = num2 >> 32;
					ptr2++;
				}
				if (num != 0UL)
				{
					*ptr2 = (uint)num;
					pResult.m_length++;
				}
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000040E4 File Offset: 0x000022E4
		private unsafe static BurstString.tBigInt g_PowerOf10_Big(int i)
		{
			BurstString.tBigInt result;
			if (i == 0)
			{
				result.m_length = 1;
				result.m_blocks.FixedElementField = 100000000U;
			}
			else if (i == 1)
			{
				result.m_length = 2;
				result.m_blocks.FixedElementField = 1874919424U;
				*(ref result.m_blocks.FixedElementField + 4) = 2328306U;
			}
			else if (i == 2)
			{
				result.m_length = 4;
				result.m_blocks.FixedElementField = 0U;
				*(ref result.m_blocks.FixedElementField + 4) = 2242703233U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)2 * 4) = 762134875U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)3 * 4) = 1262U;
			}
			else if (i == 3)
			{
				result.m_length = 7;
				result.m_blocks.FixedElementField = 0U;
				*(ref result.m_blocks.FixedElementField + 4) = 0U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)2 * 4) = 3211403009U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)3 * 4) = 1849224548U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)4 * 4) = 3668416493U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)5 * 4) = 3913284084U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)6 * 4) = 1593091U;
			}
			else if (i == 4)
			{
				result.m_length = 14;
				result.m_blocks.FixedElementField = 0U;
				*(ref result.m_blocks.FixedElementField + 4) = 0U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)2 * 4) = 0U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)3 * 4) = 0U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)4 * 4) = 781532673U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)5 * 4) = 64985353U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)6 * 4) = 253049085U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)7 * 4) = 594863151U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)8 * 4) = 3553621484U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)9 * 4) = 3288652808U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)10 * 4) = 3167596762U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)11 * 4) = 2788392729U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)12 * 4) = 3911132675U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)13 * 4) = 590U;
			}
			else
			{
				result.m_length = 27;
				result.m_blocks.FixedElementField = 0U;
				*(ref result.m_blocks.FixedElementField + 4) = 0U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)2 * 4) = 0U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)3 * 4) = 0U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)4 * 4) = 0U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)5 * 4) = 0U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)6 * 4) = 0U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)7 * 4) = 0U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)8 * 4) = 2553183233U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)9 * 4) = 3201533787U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)10 * 4) = 3638140786U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)11 * 4) = 303378311U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)12 * 4) = 1809731782U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)13 * 4) = 3477761648U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)14 * 4) = 3583367183U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)15 * 4) = 649228654U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)16 * 4) = 2915460784U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)17 * 4) = 487929380U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)18 * 4) = 1011012442U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)19 * 4) = 1677677582U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)20 * 4) = 3428152256U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)21 * 4) = 1710878487U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)22 * 4) = 1438394610U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)23 * 4) = 2161952759U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)24 * 4) = 4100910556U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)25 * 4) = 1608314830U;
				*(ref result.m_blocks.FixedElementField + (IntPtr)26 * 4) = 349175U;
			}
			return result;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000045F8 File Offset: 0x000027F8
		private static void BigInt_Pow10(out BurstString.tBigInt pResult, uint exponent)
		{
			BurstString.tBigInt tBigInt = default(BurstString.tBigInt);
			BurstString.tBigInt tBigInt2 = default(BurstString.tBigInt);
			ref BurstString.tBigInt ptr = ref tBigInt;
			ref BurstString.tBigInt ptr2 = ref tBigInt2;
			uint num = exponent & 7U;
			ptr.SetU32(BurstString.g_PowerOf10_U32[(int)num]);
			exponent >>= 3;
			int num2 = 0;
			while (exponent != 0U)
			{
				if ((exponent & 1U) != 0U)
				{
					ref BurstString.tBigInt pResult2 = ref ptr2;
					ref BurstString.tBigInt lhs = ref ptr;
					BurstString.tBigInt tBigInt3 = BurstString.g_PowerOf10_Big(num2);
					BurstString.BigInt_Multiply(out pResult2, lhs, tBigInt3);
					ref BurstString.tBigInt ptr3 = ref ptr;
					ptr = ptr2;
					ptr2 = ptr3;
				}
				num2++;
				exponent >>= 1;
			}
			pResult = ptr;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004688 File Offset: 0x00002888
		private static void BigInt_MultiplyPow10(out BurstString.tBigInt pResult, in BurstString.tBigInt input, uint exponent)
		{
			BurstString.tBigInt tBigInt = default(BurstString.tBigInt);
			BurstString.tBigInt tBigInt2 = default(BurstString.tBigInt);
			ref BurstString.tBigInt ptr = ref tBigInt;
			ref BurstString.tBigInt ptr2 = ref tBigInt2;
			uint num = exponent & 7U;
			if (num != 0U)
			{
				BurstString.BigInt_Multiply(out ptr, input, BurstString.g_PowerOf10_U32[(int)num]);
			}
			else
			{
				ptr = input;
			}
			exponent >>= 3;
			int num2 = 0;
			while (exponent != 0U)
			{
				if ((exponent & 1U) != 0U)
				{
					ref BurstString.tBigInt pResult2 = ref ptr2;
					ref BurstString.tBigInt lhs = ref ptr;
					BurstString.tBigInt tBigInt3 = BurstString.g_PowerOf10_Big(num2);
					BurstString.BigInt_Multiply(out pResult2, lhs, tBigInt3);
					ref BurstString.tBigInt ptr3 = ref ptr;
					ptr = ptr2;
					ptr2 = ptr3;
				}
				num2++;
				exponent >>= 1;
			}
			pResult = ptr;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004728 File Offset: 0x00002928
		private unsafe static void BigInt_Pow2(out BurstString.tBigInt pResult, uint exponent)
		{
			int num = (int)(exponent / 32U);
			uint num2 = 0U;
			while ((ulong)num2 <= (ulong)((long)num))
			{
				*(ref pResult.m_blocks.FixedElementField + (IntPtr)((ulong)num2 * 4UL)) = 0U;
				num2 += 1U;
			}
			pResult.m_length = num + 1;
			int num3 = (int)(exponent % 32U);
			*(ref pResult.m_blocks.FixedElementField + (IntPtr)num * 4) |= 1U << num3;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004784 File Offset: 0x00002984
		private unsafe static uint BigInt_DivideWithRemainder_MaxQuotient9(ref BurstString.tBigInt pDividend, in BurstString.tBigInt divisor)
		{
			int num = divisor.m_length;
			if (pDividend.m_length < divisor.m_length)
			{
				return 0U;
			}
			fixed (uint* ptr = &divisor.m_blocks.FixedElementField)
			{
				uint* ptr2 = ptr;
				fixed (uint* ptr3 = &pDividend.m_blocks.FixedElementField)
				{
					uint* ptr4 = ptr3;
					uint* ptr5 = ptr2;
					uint* ptr6 = ptr4;
					uint* ptr7 = ptr5 + num - 1;
					uint num2 = *(ptr6 + num - 1) / (*ptr7 + 1U);
					if (num2 != 0U)
					{
						ulong num3 = 0UL;
						ulong num4 = 0UL;
						do
						{
							ulong num5 = (ulong)(*ptr5) * (ulong)num2 + num4;
							num4 = num5 >> 32;
							ulong num6 = (ulong)(*ptr6) - (num5 & (ulong)-1) - num3;
							num3 = (num6 >> 32 & 1UL);
							*ptr6 = (uint)(num6 & (ulong)-1);
							ptr5++;
							ptr6++;
						}
						while (ptr5 == ptr7);
						while (num > 0 && *(ref pDividend.m_blocks.FixedElementField + (IntPtr)(num - 1) * 4) == 0U)
						{
							num--;
						}
						pDividend.m_length = num;
					}
					if (BurstString.BigInt_Compare(pDividend, divisor) >= 0)
					{
						num2 += 1U;
						ptr5 = ptr2;
						ptr6 = ptr4;
						ulong num7 = 0UL;
						do
						{
							ulong num8 = (ulong)(*ptr6) - (ulong)(*ptr5) - num7;
							num7 = (num8 >> 32 & 1UL);
							*ptr6 = (uint)(num8 & (ulong)-1);
							ptr5++;
							ptr6++;
						}
						while (ptr5 == ptr7);
						while (num > 0 && *(ref pDividend.m_blocks.FixedElementField + (IntPtr)(num - 1) * 4) == 0U)
						{
							num--;
						}
						pDividend.m_length = num;
					}
					return num2;
				}
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000048DC File Offset: 0x00002ADC
		private unsafe static void BigInt_ShiftLeft(ref BurstString.tBigInt pResult, uint shift)
		{
			int num = (int)(shift / 32U);
			int num2 = (int)(shift % 32U);
			int length = pResult.m_length;
			if (num2 == 0)
			{
				fixed (uint* ptr = &pResult.m_blocks.FixedElementField)
				{
					uint* ptr2 = ptr;
					uint* ptr3 = ptr2 + length - 1;
					uint* ptr4 = ptr3 + num;
					while (ptr3 >= ptr2)
					{
						*ptr4 = *ptr3;
						ptr3--;
						ptr4--;
					}
				}
				uint num3 = 0U;
				while ((ulong)num3 < (ulong)((long)num))
				{
					*(ref pResult.m_blocks.FixedElementField + (IntPtr)((ulong)num3 * 4UL)) = 0U;
					num3 += 1U;
				}
				pResult.m_length += num;
				return;
			}
			int i = length - 1;
			int num4 = length + num;
			pResult.m_length = num4 + 1;
			int num5 = 32 - num2;
			uint num6 = 0U;
			uint num7 = *(ref pResult.m_blocks.FixedElementField + (IntPtr)i * 4);
			uint num8 = num7 >> num5;
			while (i > 0)
			{
				*(ref pResult.m_blocks.FixedElementField + (IntPtr)num4 * 4) = (num6 | num8);
				num6 = num7 << num2;
				i--;
				num4--;
				num7 = *(ref pResult.m_blocks.FixedElementField + (IntPtr)i * 4);
				num8 = num7 >> num5;
			}
			*(ref pResult.m_blocks.FixedElementField + (IntPtr)num4 * 4) = (num6 | num8);
			*(ref pResult.m_blocks.FixedElementField + (IntPtr)(num4 - 1) * 4) = num7 << num2;
			uint num9 = 0U;
			while ((ulong)num9 < (ulong)((long)num))
			{
				*(ref pResult.m_blocks.FixedElementField + (IntPtr)((ulong)num9 * 4UL)) = 0U;
				num9 += 1U;
			}
			if (*(ref pResult.m_blocks.FixedElementField + (IntPtr)(pResult.m_length - 1) * 4) == 0U)
			{
				pResult.m_length--;
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004A7C File Offset: 0x00002C7C
		private unsafe static uint Dragon4(ulong mantissa, int exponent, uint mantissaHighBitIdx, bool hasUnequalMargins, BurstString.CutoffMode cutoffMode, uint cutoffNumber, byte* pOutBuffer, uint bufferSize, out int pOutExponent)
		{
			byte* ptr = pOutBuffer;
			if (mantissa == 0UL)
			{
				*ptr = 48;
				pOutExponent = 0;
				return 1U;
			}
			BurstString.tBigInt tBigInt = default(BurstString.tBigInt);
			BurstString.tBigInt tBigInt2 = default(BurstString.tBigInt);
			BurstString.tBigInt tBigInt3 = default(BurstString.tBigInt);
			BurstString.tBigInt tBigInt4 = default(BurstString.tBigInt);
			BurstString.tBigInt* ptr2;
			if (hasUnequalMargins)
			{
				if (exponent > 0)
				{
					tBigInt2.SetU64(4UL * mantissa);
					BurstString.BigInt_ShiftLeft(ref tBigInt2, (uint)exponent);
					tBigInt.SetU32(4U);
					BurstString.BigInt_Pow2(out tBigInt3, (uint)exponent);
					BurstString.BigInt_Pow2(out tBigInt4, (uint)(exponent + 1));
				}
				else
				{
					tBigInt2.SetU64(4UL * mantissa);
					BurstString.BigInt_Pow2(out tBigInt, (uint)(-exponent + 2));
					tBigInt3.SetU32(1U);
					tBigInt4.SetU32(2U);
				}
				ptr2 = &tBigInt4;
			}
			else
			{
				if (exponent > 0)
				{
					tBigInt2.SetU64(2UL * mantissa);
					BurstString.BigInt_ShiftLeft(ref tBigInt2, (uint)exponent);
					tBigInt.SetU32(2U);
					BurstString.BigInt_Pow2(out tBigInt3, (uint)exponent);
				}
				else
				{
					tBigInt2.SetU64(2UL * mantissa);
					BurstString.BigInt_Pow2(out tBigInt, (uint)(-exponent + 1));
					tBigInt3.SetU32(1U);
				}
				ptr2 = &tBigInt3;
			}
			int num = (int)Math.Ceiling((double)(mantissaHighBitIdx + (uint)exponent) * 0.3010299956639812 - 0.69);
			if (cutoffMode == BurstString.CutoffMode.FractionLength && num <= (int)(-(int)cutoffNumber))
			{
				num = (int)(-cutoffNumber + 1U);
			}
			if (num > 0)
			{
				BurstString.tBigInt tBigInt5;
				BurstString.BigInt_MultiplyPow10(out tBigInt5, tBigInt, (uint)num);
				tBigInt = tBigInt5;
			}
			else if (num < 0)
			{
				BurstString.tBigInt tBigInt6;
				BurstString.BigInt_Pow10(out tBigInt6, (uint)(-(uint)num));
				BurstString.tBigInt tBigInt7;
				BurstString.BigInt_Multiply(out tBigInt7, tBigInt2, tBigInt6);
				tBigInt2 = tBigInt7;
				BurstString.BigInt_Multiply(out tBigInt7, tBigInt3, tBigInt6);
				tBigInt3 = tBigInt7;
				if (ptr2 != &tBigInt3)
				{
					BurstString.BigInt_Multiply2(out *ptr2, tBigInt3);
				}
			}
			if (BurstString.BigInt_Compare(tBigInt2, tBigInt) >= 0)
			{
				num++;
			}
			else
			{
				BurstString.BigInt_Multiply10(ref tBigInt2);
				BurstString.BigInt_Multiply10(ref tBigInt3);
				if (ptr2 != &tBigInt3)
				{
					BurstString.BigInt_Multiply2(out *ptr2, tBigInt3);
				}
			}
			int num2 = num - (int)bufferSize;
			switch (cutoffMode)
			{
			case BurstString.CutoffMode.TotalLength:
			{
				int num3 = num - (int)cutoffNumber;
				if (num3 > num2)
				{
					num2 = num3;
				}
				break;
			}
			case BurstString.CutoffMode.FractionLength:
			{
				int num4 = (int)(-(int)cutoffNumber);
				if (num4 > num2)
				{
					num2 = num4;
				}
				break;
			}
			}
			pOutExponent = num - 1;
			uint block = tBigInt.GetBlock(tBigInt.GetLength() - 1);
			if (block < 8U || block > 429496729U)
			{
				uint num5 = BurstString.LogBase2(block);
				uint shift = (59U - num5) % 32U;
				BurstString.BigInt_ShiftLeft(ref tBigInt, shift);
				BurstString.BigInt_ShiftLeft(ref tBigInt2, shift);
				BurstString.BigInt_ShiftLeft(ref tBigInt3, shift);
				if (ptr2 != &tBigInt3)
				{
					BurstString.BigInt_Multiply2(out *ptr2, tBigInt3);
				}
			}
			uint num6;
			bool flag;
			bool flag2;
			if (cutoffMode == BurstString.CutoffMode.Unique)
			{
				for (;;)
				{
					num--;
					num6 = BurstString.BigInt_DivideWithRemainder_MaxQuotient9(ref tBigInt2, tBigInt);
					BurstString.tBigInt tBigInt8;
					BurstString.BigInt_Add(out tBigInt8, tBigInt2, *ptr2);
					flag = (BurstString.BigInt_Compare(tBigInt2, tBigInt3) < 0);
					flag2 = (BurstString.BigInt_Compare(tBigInt8, tBigInt) > 0);
					if ((flag || flag2) | num == num2)
					{
						break;
					}
					*ptr = (byte)(48U + num6);
					ptr++;
					BurstString.BigInt_Multiply10(ref tBigInt2);
					BurstString.BigInt_Multiply10(ref tBigInt3);
					if (ptr2 != &tBigInt3)
					{
						BurstString.BigInt_Multiply2(out *ptr2, tBigInt3);
					}
				}
			}
			else
			{
				flag = false;
				flag2 = false;
				for (;;)
				{
					num--;
					num6 = BurstString.BigInt_DivideWithRemainder_MaxQuotient9(ref tBigInt2, tBigInt);
					if (tBigInt2.IsZero() | num == num2)
					{
						break;
					}
					*ptr = (byte)(48U + num6);
					ptr++;
					BurstString.BigInt_Multiply10(ref tBigInt2);
				}
			}
			bool flag3 = flag;
			if (flag == flag2)
			{
				BurstString.BigInt_Multiply2(ref tBigInt2);
				int num7 = BurstString.BigInt_Compare(tBigInt2, tBigInt);
				flag3 = (num7 < 0);
				if (num7 == 0)
				{
					flag3 = ((num6 & 1U) == 0U);
				}
			}
			if (flag3)
			{
				*ptr = (byte)(48U + num6);
				ptr++;
			}
			else if (num6 == 9U)
			{
				while (ptr != pOutBuffer)
				{
					ptr--;
					if (*ptr != 57)
					{
						byte* ptr3 = ptr;
						*ptr3 += 1;
						ptr++;
						goto IL_368;
					}
				}
				*ptr = 49;
				ptr++;
				pOutExponent++;
			}
			else
			{
				*ptr = (byte)(48U + num6 + 1U);
				ptr++;
			}
			IL_368:
			return (uint)((long)(ptr - pOutBuffer));
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004DFC File Offset: 0x00002FFC
		private unsafe static int FormatPositional(byte* pOutBuffer, uint bufferSize, ulong mantissa, int exponent, uint mantissaHighBitIdx, bool hasUnequalMargins, int precision)
		{
			uint num = bufferSize - 1U;
			int num3;
			uint num2;
			if (precision < 0)
			{
				num2 = BurstString.Dragon4(mantissa, exponent, mantissaHighBitIdx, hasUnequalMargins, BurstString.CutoffMode.Unique, 0U, pOutBuffer, num, out num3);
			}
			else
			{
				num2 = BurstString.Dragon4(mantissa, exponent, mantissaHighBitIdx, hasUnequalMargins, BurstString.CutoffMode.FractionLength, (uint)precision, pOutBuffer, num, out num3);
			}
			uint num4 = 0U;
			if (num3 >= 0)
			{
				uint num5 = (uint)(num3 + 1);
				if (num2 < num5)
				{
					if (num5 > num)
					{
						num5 = num;
					}
					while (num2 < num5)
					{
						pOutBuffer[num2] = 48;
						num2 += 1U;
					}
				}
				else if (num2 > num5)
				{
					num4 = num2 - num5;
					uint num6 = num - num5 - 1U;
					if (num4 > num6)
					{
						num4 = num6;
					}
					Unsafe.CopyBlock((void*)(pOutBuffer + num5 + 1), (void*)(pOutBuffer + num5), num4);
					pOutBuffer[num5] = 46;
					num2 = num5 + 1U + num4;
				}
			}
			else
			{
				if (num > 2U)
				{
					uint num7 = (uint)(-num3 - 1);
					uint num8 = num - 2U;
					if (num7 > num8)
					{
						num7 = num8;
					}
					uint num9 = 2U + num7;
					num4 = num2;
					uint num10 = num - num9;
					if (num4 > num10)
					{
						num4 = num10;
					}
					Unsafe.CopyBlock((void*)(pOutBuffer + num9), (void*)pOutBuffer, num4);
					for (uint num11 = 2U; num11 < num9; num11 += 1U)
					{
						pOutBuffer[num11] = 48;
					}
					num4 += num7;
					num2 = num4;
				}
				if (num > 1U)
				{
					pOutBuffer[1] = 46;
					num2 += 1U;
				}
				if (num > 0U)
				{
					*pOutBuffer = 48;
					num2 += 1U;
				}
			}
			if (precision > (int)num4 && num2 < num)
			{
				if (num4 == 0U)
				{
					pOutBuffer[num2++] = 46;
				}
				uint num12 = (uint)((ulong)num2 + (ulong)((long)(precision - (int)num4)));
				if (num12 > num)
				{
					num12 = num;
				}
				while (num2 < num12)
				{
					pOutBuffer[num2] = 48;
					num2 += 1U;
				}
			}
			return (int)num2;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004F5C File Offset: 0x0000315C
		private unsafe static int FormatScientific(byte* pOutBuffer, uint bufferSize, ulong mantissa, int exponent, uint mantissaHighBitIdx, bool hasUnequalMargins, int precision)
		{
			int num2;
			uint num;
			if (precision < 0)
			{
				num = BurstString.Dragon4(mantissa, exponent, mantissaHighBitIdx, hasUnequalMargins, BurstString.CutoffMode.Unique, 0U, pOutBuffer, bufferSize, out num2);
			}
			else
			{
				num = BurstString.Dragon4(mantissa, exponent, mantissaHighBitIdx, hasUnequalMargins, BurstString.CutoffMode.TotalLength, (uint)(precision + 1), pOutBuffer, bufferSize, out num2);
			}
			byte* ptr = pOutBuffer;
			if (bufferSize > 1U)
			{
				ptr++;
				bufferSize -= 1U;
			}
			uint num3 = num - 1U;
			if (num3 > 0U && bufferSize > 1U)
			{
				uint num4 = bufferSize - 2U;
				if (num3 > num4)
				{
					num3 = num4;
				}
				Unsafe.CopyBlock((void*)(ptr + 1), (void*)ptr, num3);
				*ptr = 46;
				ptr += 1U + num3;
				bufferSize -= 1U + num3;
			}
			if (precision > (int)num3 && bufferSize > 1U)
			{
				if (num3 == 0U)
				{
					*ptr = 46;
					ptr++;
					bufferSize -= 1U;
				}
				uint num5 = (uint)((long)precision - (long)((ulong)num3));
				if (num5 > bufferSize - 1U)
				{
					num5 = bufferSize - 1U;
				}
				byte* ptr2 = ptr + num5;
				while (ptr < ptr2)
				{
					*ptr = 48;
					ptr++;
				}
			}
			if (bufferSize > 1U)
			{
				byte* ptr3 = stackalloc byte[(UIntPtr)5];
				*ptr3 = 101;
				if (num2 >= 0)
				{
					ptr3[1] = 43;
				}
				else
				{
					ptr3[1] = 45;
					num2 = -num2;
				}
				uint num6 = (uint)(num2 / 100);
				uint num7 = (uint)(((long)num2 - (long)((ulong)(num6 * 100U))) / 10L);
				uint num8 = (uint)((long)num2 - (long)((ulong)(num6 * 100U)) - (long)((ulong)(num7 * 10U)));
				ptr3[2] = (byte)(48U + num6);
				ptr3[3] = (byte)(48U + num7);
				ptr3[4] = (byte)(48U + num8);
				uint num9 = bufferSize - 1U;
				uint num10 = (5U < num9) ? 5U : num9;
				Unsafe.CopyBlock((void*)ptr, (void*)ptr3, num10);
				ptr += num10;
				bufferSize -= num10;
			}
			return (int)((long)(ptr - pOutBuffer));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000050C0 File Offset: 0x000032C0
		private unsafe static void FormatInfinityNaN(byte* dest, ref int destIndex, int destLength, ulong mantissa, bool isNegative, BurstString.FormatOptions formatOptions)
		{
			int length = (mantissa == 0UL) ? (8 + (isNegative ? 1 : 0)) : 3;
			int alignAndSize = (int)formatOptions.AlignAndSize;
			if (BurstString.AlignLeft(dest, ref destIndex, destLength, alignAndSize, length))
			{
				return;
			}
			if (mantissa == 0UL)
			{
				if (isNegative)
				{
					if (destIndex >= destLength)
					{
						return;
					}
					int num = destIndex;
					destIndex = num + 1;
					dest[num] = 45;
				}
				for (int i = 0; i < 8; i++)
				{
					if (destIndex >= destLength)
					{
						return;
					}
					int num = destIndex;
					destIndex = num + 1;
					dest[num] = BurstString.InfinityString[i];
				}
			}
			else
			{
				for (int j = 0; j < 3; j++)
				{
					if (destIndex >= destLength)
					{
						return;
					}
					int num = destIndex;
					destIndex = num + 1;
					dest[num] = BurstString.NanString[j];
				}
			}
			BurstString.AlignRight(dest, ref destIndex, destLength, alignAndSize, length);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000516C File Offset: 0x0000336C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private unsafe static void ConvertFloatToString(byte* dest, ref int destIndex, int destLength, float value, BurstString.FormatOptions formatOptions)
		{
			BurstString.tFloatUnion32 tFloatUnion = default(BurstString.tFloatUnion32);
			tFloatUnion.m_floatingPoint = value;
			uint exponent = tFloatUnion.GetExponent();
			uint mantissa = tFloatUnion.GetMantissa();
			if (exponent == 255U)
			{
				BurstString.FormatInfinityNaN(dest, ref destIndex, destLength, (ulong)mantissa, tFloatUnion.IsNegative(), formatOptions);
				return;
			}
			uint num;
			int exponent2;
			uint mantissaHighBitIdx;
			bool hasUnequalMargins;
			if (exponent != 0U)
			{
				num = (uint)(8388608UL | (ulong)mantissa);
				exponent2 = (int)(exponent - 127U - 23U);
				mantissaHighBitIdx = 23U;
				hasUnequalMargins = (exponent != 1U && mantissa == 0U);
			}
			else
			{
				num = mantissa;
				exponent2 = -149;
				mantissaHighBitIdx = BurstString.LogBase2(num);
				hasUnequalMargins = false;
			}
			int num2 = (formatOptions.Specifier == 0) ? -1 : ((int)formatOptions.Specifier);
			int num3 = Math.Max(10, num2 + 1);
			byte* ptr = stackalloc byte[(UIntPtr)num3];
			if (num2 < 0)
			{
				num2 = 7;
			}
			int num5;
			uint num4 = BurstString.Dragon4((ulong)num, exponent2, mantissaHighBitIdx, hasUnequalMargins, BurstString.CutoffMode.TotalLength, (uint)num2, ptr, (uint)(num3 - 1), out num5);
			ptr[num4] = 0;
			bool isNegative = tFloatUnion.IsNegative();
			if (tFloatUnion.m_integer == 2147483648U)
			{
				isNegative = false;
			}
			BurstString.NumberBuffer numberBuffer = new BurstString.NumberBuffer(BurstString.NumberBufferKind.Float, ptr, (int)num4, num5 + 1, isNegative);
			BurstString.FormatNumber(dest, ref destIndex, destLength, ref numberBuffer, num2, formatOptions);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00005280 File Offset: 0x00003480
		[MethodImpl(MethodImplOptions.NoInlining)]
		private unsafe static void ConvertDoubleToString(byte* dest, ref int destIndex, int destLength, double value, BurstString.FormatOptions formatOptions)
		{
			BurstString.tFloatUnion64 tFloatUnion = default(BurstString.tFloatUnion64);
			tFloatUnion.m_floatingPoint = value;
			uint exponent = tFloatUnion.GetExponent();
			ulong mantissa = tFloatUnion.GetMantissa();
			if (exponent == 2047U)
			{
				BurstString.FormatInfinityNaN(dest, ref destIndex, destLength, mantissa, tFloatUnion.IsNegative(), formatOptions);
				return;
			}
			ulong num;
			int exponent2;
			uint mantissaHighBitIdx;
			bool hasUnequalMargins;
			if (exponent != 0U)
			{
				num = (4503599627370496UL | mantissa);
				exponent2 = (int)(exponent - 1023U - 52U);
				mantissaHighBitIdx = 52U;
				hasUnequalMargins = (exponent != 1U && mantissa == 0UL);
			}
			else
			{
				num = mantissa;
				exponent2 = -1074;
				mantissaHighBitIdx = BurstString.LogBase2((uint)num);
				hasUnequalMargins = false;
			}
			int num2 = (formatOptions.Specifier == 0) ? -1 : ((int)formatOptions.Specifier);
			int num3 = Math.Max(18, num2 + 1);
			byte* ptr = stackalloc byte[(UIntPtr)num3];
			if (num2 < 0)
			{
				num2 = 15;
			}
			int num5;
			uint num4 = BurstString.Dragon4(num, exponent2, mantissaHighBitIdx, hasUnequalMargins, BurstString.CutoffMode.TotalLength, (uint)num2, ptr, (uint)(num3 - 1), out num5);
			ptr[num4] = 0;
			bool isNegative = tFloatUnion.IsNegative();
			if (tFloatUnion.m_integer == 9223372036854775808UL)
			{
				isNegative = false;
			}
			BurstString.NumberBuffer numberBuffer = new BurstString.NumberBuffer(BurstString.NumberBufferKind.Float, ptr, (int)num4, num5 + 1, isNegative);
			BurstString.FormatNumber(dest, ref destIndex, destLength, ref numberBuffer, num2, formatOptions);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000539C File Offset: 0x0000359C
		// Note: this type is marked as 'beforefieldinit'.
		static BurstString()
		{
		}

		// Token: 0x040000B9 RID: 185
		private static readonly char[] SplitByColon = new char[]
		{
			':'
		};

		// Token: 0x040000BA RID: 186
		private static readonly byte[] logTable = new byte[]
		{
			0,
			0,
			1,
			1,
			2,
			2,
			2,
			2,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			6,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7,
			7
		};

		// Token: 0x040000BB RID: 187
		private static readonly uint[] g_PowerOf10_U32 = new uint[]
		{
			1U,
			10U,
			100U,
			1000U,
			10000U,
			100000U,
			1000000U,
			10000000U
		};

		// Token: 0x040000BC RID: 188
		private static readonly byte[] InfinityString = new byte[]
		{
			73,
			110,
			102,
			105,
			110,
			105,
			116,
			121
		};

		// Token: 0x040000BD RID: 189
		private static readonly byte[] NanString = new byte[]
		{
			78,
			97,
			78
		};

		// Token: 0x040000BE RID: 190
		private const int SinglePrecision = 9;

		// Token: 0x040000BF RID: 191
		private const int DoublePrecision = 17;

		// Token: 0x040000C0 RID: 192
		internal const int SingleNumberBufferLength = 10;

		// Token: 0x040000C1 RID: 193
		internal const int DoubleNumberBufferLength = 18;

		// Token: 0x040000C2 RID: 194
		private const int SinglePrecisionCustomFormat = 7;

		// Token: 0x040000C3 RID: 195
		private const int DoublePrecisionCustomFormat = 15;

		// Token: 0x02000032 RID: 50
		internal class PreserveAttribute : Attribute
		{
			// Token: 0x0600014F RID: 335 RVA: 0x00007B73 File Offset: 0x00005D73
			public PreserveAttribute()
			{
			}
		}

		// Token: 0x02000033 RID: 51
		private enum NumberBufferKind
		{
			// Token: 0x04000247 RID: 583
			Integer,
			// Token: 0x04000248 RID: 584
			Float
		}

		// Token: 0x02000034 RID: 52
		private struct NumberBuffer
		{
			// Token: 0x06000150 RID: 336 RVA: 0x00007B7B File Offset: 0x00005D7B
			public unsafe NumberBuffer(BurstString.NumberBufferKind kind, byte* buffer, int digitsCount, int scale, bool isNegative)
			{
				this.Kind = kind;
				this._buffer = buffer;
				this.DigitsCount = digitsCount;
				this.Scale = scale;
				this.IsNegative = isNegative;
			}

			// Token: 0x06000151 RID: 337 RVA: 0x00007BA2 File Offset: 0x00005DA2
			public unsafe byte* GetDigitsPointer()
			{
				return this._buffer;
			}

			// Token: 0x04000249 RID: 585
			private unsafe readonly byte* _buffer;

			// Token: 0x0400024A RID: 586
			public BurstString.NumberBufferKind Kind;

			// Token: 0x0400024B RID: 587
			public int DigitsCount;

			// Token: 0x0400024C RID: 588
			public int Scale;

			// Token: 0x0400024D RID: 589
			public readonly bool IsNegative;
		}

		// Token: 0x02000035 RID: 53
		public enum NumberFormatKind : byte
		{
			// Token: 0x0400024F RID: 591
			General,
			// Token: 0x04000250 RID: 592
			Decimal,
			// Token: 0x04000251 RID: 593
			DecimalForceSigned,
			// Token: 0x04000252 RID: 594
			Hexadecimal
		}

		// Token: 0x02000036 RID: 54
		public struct FormatOptions
		{
			// Token: 0x06000152 RID: 338 RVA: 0x00007BAA File Offset: 0x00005DAA
			public FormatOptions(BurstString.NumberFormatKind kind, sbyte alignAndSize, byte specifier, bool lowercase)
			{
				this = default(BurstString.FormatOptions);
				this.Kind = kind;
				this.AlignAndSize = alignAndSize;
				this.Specifier = specifier;
				this.Lowercase = lowercase;
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x06000153 RID: 339 RVA: 0x00007BD0 File Offset: 0x00005DD0
			public bool Uppercase
			{
				get
				{
					return !this.Lowercase;
				}
			}

			// Token: 0x06000154 RID: 340 RVA: 0x00007BDC File Offset: 0x00005DDC
			public unsafe int EncodeToRaw()
			{
				BurstString.FormatOptions formatOptions = this;
				return *(int*)(&formatOptions);
			}

			// Token: 0x06000155 RID: 341 RVA: 0x00007BF4 File Offset: 0x00005DF4
			public int GetBase()
			{
				if (this.Kind == BurstString.NumberFormatKind.Hexadecimal)
				{
					return 16;
				}
				return 10;
			}

			// Token: 0x06000156 RID: 342 RVA: 0x00007C04 File Offset: 0x00005E04
			public override string ToString()
			{
				return string.Format("{0}: {1}, {2}: {3}, {4}: {5}, {6}: {7}", new object[]
				{
					"Kind",
					this.Kind,
					"AlignAndSize",
					this.AlignAndSize,
					"Specifier",
					this.Specifier,
					"Uppercase",
					this.Uppercase
				});
			}

			// Token: 0x04000253 RID: 595
			public BurstString.NumberFormatKind Kind;

			// Token: 0x04000254 RID: 596
			public sbyte AlignAndSize;

			// Token: 0x04000255 RID: 597
			public byte Specifier;

			// Token: 0x04000256 RID: 598
			public bool Lowercase;
		}

		// Token: 0x02000037 RID: 55
		public struct tBigInt
		{
			// Token: 0x06000157 RID: 343 RVA: 0x00007C79 File Offset: 0x00005E79
			public int GetLength()
			{
				return this.m_length;
			}

			// Token: 0x06000158 RID: 344 RVA: 0x00007C81 File Offset: 0x00005E81
			public unsafe uint GetBlock(int idx)
			{
				return *(ref this.m_blocks.FixedElementField + (IntPtr)idx * 4);
			}

			// Token: 0x06000159 RID: 345 RVA: 0x00007C94 File Offset: 0x00005E94
			public void SetZero()
			{
				this.m_length = 0;
			}

			// Token: 0x0600015A RID: 346 RVA: 0x00007C9D File Offset: 0x00005E9D
			public bool IsZero()
			{
				return this.m_length == 0;
			}

			// Token: 0x0600015B RID: 347 RVA: 0x00007CA8 File Offset: 0x00005EA8
			public unsafe void SetU64(ulong val)
			{
				if (val > (ulong)-1)
				{
					this.m_blocks.FixedElementField = (uint)(val & (ulong)-1);
					*(ref this.m_blocks.FixedElementField + 4) = (uint)(val >> 32 & (ulong)-1);
					this.m_length = 2;
					return;
				}
				if (val != 0UL)
				{
					this.m_blocks.FixedElementField = (uint)(val & (ulong)-1);
					this.m_length = 1;
					return;
				}
				this.m_length = 0;
			}

			// Token: 0x0600015C RID: 348 RVA: 0x00007D0C File Offset: 0x00005F0C
			public void SetU32(uint val)
			{
				if (val != 0U)
				{
					this.m_blocks.FixedElementField = val;
					this.m_length = ((val != 0U) ? 1 : 0);
					return;
				}
				this.m_length = 0;
			}

			// Token: 0x0600015D RID: 349 RVA: 0x00007D33 File Offset: 0x00005F33
			public uint GetU32()
			{
				if (this.m_length != 0)
				{
					return this.m_blocks.FixedElementField;
				}
				return 0U;
			}

			// Token: 0x04000257 RID: 599
			private const int c_BigInt_MaxBlocks = 35;

			// Token: 0x04000258 RID: 600
			public int m_length;

			// Token: 0x04000259 RID: 601
			[FixedBuffer(typeof(uint), 35)]
			public BurstString.tBigInt.<m_blocks>e__FixedBuffer m_blocks;

			// Token: 0x02000054 RID: 84
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 140)]
			public struct <m_blocks>e__FixedBuffer
			{
				// Token: 0x04000292 RID: 658
				public uint FixedElementField;
			}
		}

		// Token: 0x02000038 RID: 56
		public enum CutoffMode
		{
			// Token: 0x0400025B RID: 603
			Unique,
			// Token: 0x0400025C RID: 604
			TotalLength,
			// Token: 0x0400025D RID: 605
			FractionLength
		}

		// Token: 0x02000039 RID: 57
		public enum PrintFloatFormat
		{
			// Token: 0x0400025F RID: 607
			Positional,
			// Token: 0x04000260 RID: 608
			Scientific
		}

		// Token: 0x0200003A RID: 58
		[StructLayout(LayoutKind.Explicit)]
		public struct tFloatUnion32
		{
			// Token: 0x0600015E RID: 350 RVA: 0x00007D4B File Offset: 0x00005F4B
			public bool IsNegative()
			{
				return this.m_integer >> 31 > 0U;
			}

			// Token: 0x0600015F RID: 351 RVA: 0x00007D59 File Offset: 0x00005F59
			public uint GetExponent()
			{
				return this.m_integer >> 23 & 255U;
			}

			// Token: 0x06000160 RID: 352 RVA: 0x00007D6A File Offset: 0x00005F6A
			public uint GetMantissa()
			{
				return this.m_integer & 8388607U;
			}

			// Token: 0x04000261 RID: 609
			[FieldOffset(0)]
			public float m_floatingPoint;

			// Token: 0x04000262 RID: 610
			[FieldOffset(0)]
			public uint m_integer;
		}

		// Token: 0x0200003B RID: 59
		[StructLayout(LayoutKind.Explicit)]
		public struct tFloatUnion64
		{
			// Token: 0x06000161 RID: 353 RVA: 0x00007D78 File Offset: 0x00005F78
			public bool IsNegative()
			{
				return this.m_integer >> 63 > 0UL;
			}

			// Token: 0x06000162 RID: 354 RVA: 0x00007D87 File Offset: 0x00005F87
			public uint GetExponent()
			{
				return (uint)(this.m_integer >> 52 & 2047UL);
			}

			// Token: 0x06000163 RID: 355 RVA: 0x00007D9A File Offset: 0x00005F9A
			public ulong GetMantissa()
			{
				return this.m_integer & 4503599627370495UL;
			}

			// Token: 0x04000263 RID: 611
			[FieldOffset(0)]
			public double m_floatingPoint;

			// Token: 0x04000264 RID: 612
			[FieldOffset(0)]
			public ulong m_integer;
		}
	}
}
