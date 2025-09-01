using System;
using System.Runtime.CompilerServices;

namespace System.Buffers.Text
{
	// Token: 0x02000AF2 RID: 2802
	internal static class FormattingHelpers
	{
		// Token: 0x060063FE RID: 25598 RVA: 0x0014E8B8 File Offset: 0x0014CAB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CountDigits(ulong value)
		{
			int num = 1;
			uint num2;
			if (value >= 10000000UL)
			{
				if (value >= 100000000000000UL)
				{
					num2 = (uint)(value / 100000000000000UL);
					num += 14;
				}
				else
				{
					num2 = (uint)(value / 10000000UL);
					num += 7;
				}
			}
			else
			{
				num2 = (uint)value;
			}
			if (num2 >= 10U)
			{
				if (num2 < 100U)
				{
					num++;
				}
				else if (num2 < 1000U)
				{
					num += 2;
				}
				else if (num2 < 10000U)
				{
					num += 3;
				}
				else if (num2 < 100000U)
				{
					num += 4;
				}
				else if (num2 < 1000000U)
				{
					num += 5;
				}
				else
				{
					num += 6;
				}
			}
			return num;
		}

		// Token: 0x060063FF RID: 25599 RVA: 0x0014E950 File Offset: 0x0014CB50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CountDigits(uint value)
		{
			int num = 1;
			if (value >= 100000U)
			{
				value /= 100000U;
				num += 5;
			}
			if (value >= 10U)
			{
				if (value < 100U)
				{
					num++;
				}
				else if (value < 1000U)
				{
					num += 2;
				}
				else if (value < 10000U)
				{
					num += 3;
				}
				else
				{
					num += 4;
				}
			}
			return num;
		}

		// Token: 0x06006400 RID: 25600 RVA: 0x0014E9A8 File Offset: 0x0014CBA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CountHexDigits(ulong value)
		{
			int num = 1;
			if (value > (ulong)-1)
			{
				num += 8;
				value >>= 32;
			}
			if (value > 65535UL)
			{
				num += 4;
				value >>= 16;
			}
			if (value > 255UL)
			{
				num += 2;
				value >>= 8;
			}
			if (value > 15UL)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06006401 RID: 25601 RVA: 0x0014E9F8 File Offset: 0x0014CBF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static char GetSymbolOrDefault(in StandardFormat format, char defaultSymbol)
		{
			char c = format.Symbol;
			if (c == '\0' && format.Precision == 0)
			{
				c = defaultSymbol;
			}
			return c;
		}

		// Token: 0x06006402 RID: 25602 RVA: 0x0014EA1C File Offset: 0x0014CC1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void FillWithAsciiZeros(Span<byte> buffer)
		{
			for (int i = 0; i < buffer.Length; i++)
			{
				*buffer[i] = 48;
			}
		}

		// Token: 0x06006403 RID: 25603 RVA: 0x0014EA48 File Offset: 0x0014CC48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteHexByte(byte value, Span<byte> buffer, int startingIndex = 0, FormattingHelpers.HexCasing casing = FormattingHelpers.HexCasing.Uppercase)
		{
			uint num = (uint)(((int)(value & 240) << 4) + (int)(value & 15) - 35209);
			uint num2 = (uint)(((-num & 28784U) >> 4) + num + (FormattingHelpers.HexCasing)47545U | casing);
			*buffer[startingIndex + 1] = (byte)num2;
			*buffer[startingIndex] = (byte)(num2 >> 8);
		}

		// Token: 0x06006404 RID: 25604 RVA: 0x0014EA9C File Offset: 0x0014CC9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteDigits(ulong value, Span<byte> buffer)
		{
			for (int i = buffer.Length - 1; i >= 1; i--)
			{
				ulong num = 48UL + value;
				value /= 10UL;
				*buffer[i] = (byte)(num - value * 10UL);
			}
			*buffer[0] = (byte)(48UL + value);
		}

		// Token: 0x06006405 RID: 25605 RVA: 0x0014EAEC File Offset: 0x0014CCEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteDigitsWithGroupSeparator(ulong value, Span<byte> buffer)
		{
			int num = 0;
			for (int i = buffer.Length - 1; i >= 1; i--)
			{
				ulong num2 = 48UL + value;
				value /= 10UL;
				*buffer[i] = (byte)(num2 - value * 10UL);
				if (num == 2)
				{
					*buffer[--i] = 44;
					num = 0;
				}
				else
				{
					num++;
				}
			}
			*buffer[0] = (byte)(48UL + value);
		}

		// Token: 0x06006406 RID: 25606 RVA: 0x0014EB58 File Offset: 0x0014CD58
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteDigits(uint value, Span<byte> buffer)
		{
			for (int i = buffer.Length - 1; i >= 1; i--)
			{
				uint num = 48U + value;
				value /= 10U;
				*buffer[i] = (byte)(num - value * 10U);
			}
			*buffer[0] = (byte)(48U + value);
		}

		// Token: 0x06006407 RID: 25607 RVA: 0x0014EBA4 File Offset: 0x0014CDA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteFourDecimalDigits(uint value, Span<byte> buffer, int startingIndex = 0)
		{
			uint num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 3] = (byte)(num - value * 10U);
			num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 2] = (byte)(num - value * 10U);
			num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 1] = (byte)(num - value * 10U);
			*buffer[startingIndex] = (byte)(48U + value);
		}

		// Token: 0x06006408 RID: 25608 RVA: 0x0014EC18 File Offset: 0x0014CE18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteTwoDecimalDigits(uint value, Span<byte> buffer, int startingIndex = 0)
		{
			uint num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 1] = (byte)(num - value * 10U);
			*buffer[startingIndex] = (byte)(48U + value);
		}

		// Token: 0x06006409 RID: 25609 RVA: 0x0014EC50 File Offset: 0x0014CE50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong DivMod(ulong numerator, ulong denominator, out ulong modulo)
		{
			ulong num = numerator / denominator;
			modulo = numerator - num * denominator;
			return num;
		}

		// Token: 0x0600640A RID: 25610 RVA: 0x0014EC6C File Offset: 0x0014CE6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint DivMod(uint numerator, uint denominator, out uint modulo)
		{
			uint num = numerator / denominator;
			modulo = numerator - num * denominator;
			return num;
		}

		// Token: 0x0600640B RID: 25611 RVA: 0x0014EC88 File Offset: 0x0014CE88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CountDecimalTrailingZeros(uint value, out uint valueWithoutTrailingZeros)
		{
			int num = 0;
			if (value != 0U)
			{
				for (;;)
				{
					uint num3;
					uint num2 = FormattingHelpers.DivMod(value, 10U, out num3);
					if (num3 != 0U)
					{
						break;
					}
					value = num2;
					num++;
				}
			}
			valueWithoutTrailingZeros = value;
			return num;
		}

		// Token: 0x04003A91 RID: 14993
		internal const string HexTableLower = "0123456789abcdef";

		// Token: 0x04003A92 RID: 14994
		internal const string HexTableUpper = "0123456789ABCDEF";

		// Token: 0x02000AF3 RID: 2803
		public enum HexCasing : uint
		{
			// Token: 0x04003A94 RID: 14996
			Uppercase,
			// Token: 0x04003A95 RID: 14997
			Lowercase = 8224U
		}
	}
}
