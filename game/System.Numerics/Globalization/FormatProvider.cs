using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Globalization
{
	// Token: 0x02000018 RID: 24
	internal class FormatProvider
	{
		// Token: 0x06000265 RID: 613 RVA: 0x00010EA8 File Offset: 0x0000F0A8
		internal unsafe static void FormatBigInteger(ref ValueStringBuilder sb, int precision, int scale, bool sign, ReadOnlySpan<char> format, NumberFormatInfo numberFormatInfo, char[] digits, int startIndex)
		{
			fixed (char[] array = digits)
			{
				char* ptr;
				if (digits == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				FormatProvider.Number.NumberBuffer numberBuffer = default(FormatProvider.Number.NumberBuffer);
				numberBuffer.overrideDigits = ptr + startIndex;
				numberBuffer.precision = precision;
				numberBuffer.scale = scale;
				numberBuffer.sign = sign;
				int nMaxDigits;
				char c = FormatProvider.Number.ParseFormatSpecifier(format, out nMaxDigits);
				if (c != '\0')
				{
					FormatProvider.Number.NumberToString(ref sb, ref numberBuffer, c, nMaxDigits, numberFormatInfo, false);
				}
				else
				{
					FormatProvider.Number.NumberToStringFormat(ref sb, ref numberBuffer, format, numberFormatInfo);
				}
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00010F28 File Offset: 0x0000F128
		internal static bool TryStringToBigInteger(ReadOnlySpan<char> s, NumberStyles styles, NumberFormatInfo numberFormatInfo, StringBuilder receiver, out int precision, out int scale, out bool sign)
		{
			FormatProvider.Number.NumberBuffer numberBuffer = default(FormatProvider.Number.NumberBuffer);
			numberBuffer.overrideDigits = 1;
			if (!FormatProvider.Number.TryStringToNumber(s, styles, ref numberBuffer, receiver, numberFormatInfo, false))
			{
				precision = 0;
				scale = 0;
				sign = false;
				return false;
			}
			precision = numberBuffer.precision;
			scale = numberBuffer.scale;
			sign = numberBuffer.sign;
			return true;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00010F7E File Offset: 0x0000F17E
		public FormatProvider()
		{
		}

		// Token: 0x02000019 RID: 25
		private class Number
		{
			// Token: 0x06000268 RID: 616 RVA: 0x00010F7E File Offset: 0x0000F17E
			private Number()
			{
			}

			// Token: 0x06000269 RID: 617 RVA: 0x00010F86 File Offset: 0x0000F186
			private static bool IsWhite(char ch)
			{
				return ch == ' ' || (ch >= '\t' && ch <= '\r');
			}

			// Token: 0x0600026A RID: 618 RVA: 0x00010FA0 File Offset: 0x0000F1A0
			private unsafe static char* MatchChars(char* p, char* pEnd, string str)
			{
				char* ptr = str;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				return FormatProvider.Number.MatchChars(p, pEnd, ptr);
			}

			// Token: 0x0600026B RID: 619 RVA: 0x00010FC8 File Offset: 0x0000F1C8
			private unsafe static char* MatchChars(char* p, char* pEnd, char* str)
			{
				if (*str == '\0')
				{
					return null;
				}
				do
				{
					char c = (p < pEnd) ? (*p) : '\0';
					if (c != *str && (*str != '\u00a0' || c != ' '))
					{
						goto IL_34;
					}
					p++;
					str++;
				}
				while (*str != '\0');
				return p;
				IL_34:
				return null;
			}

			// Token: 0x0600026C RID: 620 RVA: 0x0001100C File Offset: 0x0000F20C
			private unsafe static bool ParseNumber(ref char* str, char* strEnd, NumberStyles options, ref FormatProvider.Number.NumberBuffer number, StringBuilder sb, NumberFormatInfo numfmt, bool parseDecimal)
			{
				number.scale = 0;
				number.sign = false;
				string text = null;
				bool flag = false;
				string str2;
				string str3;
				if ((options & NumberStyles.AllowCurrencySymbol) != NumberStyles.None)
				{
					text = numfmt.CurrencySymbol;
					str2 = numfmt.CurrencyDecimalSeparator;
					str3 = numfmt.CurrencyGroupSeparator;
					flag = true;
				}
				else
				{
					str2 = numfmt.NumberDecimalSeparator;
					str3 = numfmt.NumberGroupSeparator;
				}
				int num = 0;
				bool flag2 = sb != null;
				int num2 = flag2 ? int.MaxValue : 32;
				char* ptr = str;
				char c = (ptr < strEnd) ? (*ptr) : '\0';
				char* digits = number.digits;
				for (;;)
				{
					if (!FormatProvider.Number.IsWhite(c) || (options & NumberStyles.AllowLeadingWhite) == NumberStyles.None || ((num & 1) != 0 && (num & 32) == 0 && numfmt.NumberNegativePattern != 2))
					{
						char* ptr2;
						if ((options & NumberStyles.AllowLeadingSign) != NumberStyles.None && (num & 1) == 0 && ((ptr2 = FormatProvider.Number.MatchChars(ptr, strEnd, numfmt.PositiveSign)) != null || ((ptr2 = FormatProvider.Number.MatchChars(ptr, strEnd, numfmt.NegativeSign)) != null && (number.sign = true))))
						{
							num |= 1;
							ptr = ptr2 - 1;
						}
						else if (c == '(' && (options & NumberStyles.AllowParentheses) != NumberStyles.None && (num & 1) == 0)
						{
							num |= 3;
							number.sign = true;
						}
						else
						{
							if (text == null || (ptr2 = FormatProvider.Number.MatchChars(ptr, strEnd, text)) == null)
							{
								break;
							}
							num |= 32;
							text = null;
							ptr = ptr2 - 1;
						}
					}
					c = ((++ptr < strEnd) ? (*ptr) : '\0');
				}
				int num3 = 0;
				int num4 = 0;
				for (;;)
				{
					char* ptr2;
					if ((c >= '0' && c <= '9') || ((options & NumberStyles.AllowHexSpecifier) != NumberStyles.None && ((c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'))))
					{
						num |= 4;
						if (c != '0' || (num & 8) != 0 || (flag2 && (options & NumberStyles.AllowHexSpecifier) != NumberStyles.None))
						{
							if (num3 < num2)
							{
								if (flag2)
								{
									sb.Append(c);
								}
								else
								{
									digits[(IntPtr)(num3++) * 2] = c;
								}
								if (c != '0' || parseDecimal)
								{
									num4 = num3;
								}
							}
							if ((num & 16) == 0)
							{
								number.scale++;
							}
							num |= 8;
						}
						else if ((num & 16) != 0)
						{
							number.scale--;
						}
					}
					else if ((options & NumberStyles.AllowDecimalPoint) != NumberStyles.None && (num & 16) == 0 && ((ptr2 = FormatProvider.Number.MatchChars(ptr, strEnd, str2)) != null || (flag && (num & 32) == 0 && (ptr2 = FormatProvider.Number.MatchChars(ptr, strEnd, numfmt.NumberDecimalSeparator)) != null)))
					{
						num |= 16;
						ptr = ptr2 - 1;
					}
					else
					{
						if ((options & NumberStyles.AllowThousands) == NumberStyles.None || (num & 4) == 0 || (num & 16) != 0 || ((ptr2 = FormatProvider.Number.MatchChars(ptr, strEnd, str3)) == null && (!flag || (num & 32) != 0 || (ptr2 = FormatProvider.Number.MatchChars(ptr, strEnd, numfmt.NumberGroupSeparator)) == null)))
						{
							break;
						}
						ptr = ptr2 - 1;
					}
					c = ((++ptr < strEnd) ? (*ptr) : '\0');
				}
				bool flag3 = false;
				number.precision = num4;
				if (flag2)
				{
					sb.Append('\0');
				}
				else
				{
					digits[num4] = '\0';
				}
				if ((num & 4) != 0)
				{
					if ((c == 'E' || c == 'e') && (options & NumberStyles.AllowExponent) != NumberStyles.None)
					{
						char* ptr3 = ptr;
						c = ((++ptr < strEnd) ? (*ptr) : '\0');
						char* ptr2;
						if ((ptr2 = FormatProvider.Number.MatchChars(ptr, strEnd, numfmt.PositiveSign)) != null)
						{
							c = (((ptr = ptr2) < strEnd) ? (*ptr) : '\0');
						}
						else if ((ptr2 = FormatProvider.Number.MatchChars(ptr, strEnd, numfmt.NegativeSign)) != null)
						{
							c = (((ptr = ptr2) < strEnd) ? (*ptr) : '\0');
							flag3 = true;
						}
						if (c >= '0' && c <= '9')
						{
							int num5 = 0;
							do
							{
								num5 = num5 * 10 + (int)(c - '0');
								c = ((++ptr < strEnd) ? (*ptr) : '\0');
								if (num5 > 1000)
								{
									num5 = 9999;
									while (c >= '0' && c <= '9')
									{
										c = ((++ptr < strEnd) ? (*ptr) : '\0');
									}
								}
							}
							while (c >= '0' && c <= '9');
							if (flag3)
							{
								num5 = -num5;
							}
							number.scale += num5;
						}
						else
						{
							ptr = ptr3;
							c = ((ptr < strEnd) ? (*ptr) : '\0');
						}
					}
					for (;;)
					{
						if (!FormatProvider.Number.IsWhite(c) || (options & NumberStyles.AllowTrailingWhite) == NumberStyles.None)
						{
							char* ptr2;
							if ((options & NumberStyles.AllowTrailingSign) != NumberStyles.None && (num & 1) == 0 && ((ptr2 = FormatProvider.Number.MatchChars(ptr, strEnd, numfmt.PositiveSign)) != null || ((ptr2 = FormatProvider.Number.MatchChars(ptr, strEnd, numfmt.NegativeSign)) != null && (number.sign = true))))
							{
								num |= 1;
								ptr = ptr2 - 1;
							}
							else if (c == ')' && (num & 2) != 0)
							{
								num &= -3;
							}
							else
							{
								if (text == null || (ptr2 = FormatProvider.Number.MatchChars(ptr, strEnd, text)) == null)
								{
									break;
								}
								text = null;
								ptr = ptr2 - 1;
							}
						}
						c = ((++ptr < strEnd) ? (*ptr) : '\0');
					}
					if ((num & 2) == 0)
					{
						if ((num & 8) == 0)
						{
							if (!parseDecimal)
							{
								number.scale = 0;
							}
							if ((num & 16) == 0)
							{
								number.sign = false;
							}
						}
						str = ptr;
						return true;
					}
				}
				str = ptr;
				return false;
			}

			// Token: 0x0600026D RID: 621 RVA: 0x00011500 File Offset: 0x0000F700
			private unsafe static bool TrailingZeros(ReadOnlySpan<char> s, int index)
			{
				for (int i = index; i < s.Length; i++)
				{
					if (*s[i] != 0)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600026E RID: 622 RVA: 0x00011530 File Offset: 0x0000F730
			internal unsafe static bool TryStringToNumber(ReadOnlySpan<char> str, NumberStyles options, ref FormatProvider.Number.NumberBuffer number, StringBuilder sb, NumberFormatInfo numfmt, bool parseDecimal)
			{
				fixed (char* reference = MemoryMarshal.GetReference<char>(str))
				{
					char* ptr = reference;
					char* ptr2 = ptr;
					if (!FormatProvider.Number.ParseNumber(ref ptr2, ptr2 + str.Length, options, ref number, sb, numfmt, parseDecimal) || ((long)(ptr2 - ptr) < (long)str.Length && !FormatProvider.Number.TrailingZeros(str, (int)((long)(ptr2 - ptr)))))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600026F RID: 623 RVA: 0x0001158C File Offset: 0x0000F78C
			internal unsafe static void Int32ToDecChars(char* buffer, ref int index, uint value, int digits)
			{
				while (--digits >= 0 || value != 0U)
				{
					int num = index - 1;
					index = num;
					buffer[num] = (char)(value % 10U + 48U);
					value /= 10U;
				}
			}

			// Token: 0x06000270 RID: 624 RVA: 0x000115C4 File Offset: 0x0000F7C4
			internal unsafe static char ParseFormatSpecifier(ReadOnlySpan<char> format, out int digits)
			{
				char c = '\0';
				if (format.Length > 0)
				{
					c = (char)(*format[0]);
					if (c - 'A' <= '\u0019' || c - 'a' <= '\u0019')
					{
						if (format.Length == 1)
						{
							digits = -1;
							return c;
						}
						if (format.Length == 2)
						{
							int num = (int)(*format[1] - 48);
							if (num < 10)
							{
								digits = num;
								return c;
							}
						}
						else if (format.Length == 3)
						{
							int num2 = (int)(*format[1] - 48);
							int num3 = (int)(*format[2] - 48);
							if (num2 < 10 && num3 < 10)
							{
								digits = num2 * 10 + num3;
								return c;
							}
						}
						int num4 = 0;
						int num5 = 1;
						while (num5 < format.Length && *format[num5] - 48 < 10 && num4 < 10)
						{
							num4 = num4 * 10 + (int)(*format[num5++]) - 48;
						}
						if (num5 == format.Length || *format[num5] == 0)
						{
							digits = num4;
							return c;
						}
					}
				}
				digits = -1;
				if (format.Length != 0 && c != '\0')
				{
					return '\0';
				}
				return 'G';
			}

			// Token: 0x06000271 RID: 625 RVA: 0x000116D4 File Offset: 0x0000F8D4
			internal unsafe static void NumberToString(ref ValueStringBuilder sb, ref FormatProvider.Number.NumberBuffer number, char format, int nMaxDigits, NumberFormatInfo info, bool isDecimal)
			{
				if (format <= 'P')
				{
					switch (format)
					{
					case 'C':
						break;
					case 'D':
						goto IL_1FB;
					case 'E':
						goto IL_119;
					case 'F':
						goto IL_A1;
					case 'G':
						goto IL_154;
					default:
						if (format == 'N')
						{
							goto IL_EC;
						}
						if (format != 'P')
						{
							goto IL_1FB;
						}
						goto IL_1C3;
					}
				}
				else
				{
					switch (format)
					{
					case 'c':
						break;
					case 'd':
						goto IL_1FB;
					case 'e':
						goto IL_119;
					case 'f':
						goto IL_A1;
					case 'g':
						goto IL_154;
					default:
						if (format == 'n')
						{
							goto IL_EC;
						}
						if (format != 'p')
						{
							goto IL_1FB;
						}
						goto IL_1C3;
					}
				}
				int nMinDigits = (nMaxDigits >= 0) ? nMaxDigits : info.CurrencyDecimalDigits;
				if (nMaxDigits < 0)
				{
					nMaxDigits = info.CurrencyDecimalDigits;
				}
				FormatProvider.Number.RoundNumber(ref number, number.scale + nMaxDigits);
				FormatProvider.Number.FormatCurrency(ref sb, ref number, nMinDigits, nMaxDigits, info);
				return;
				IL_A1:
				if (nMaxDigits < 0)
				{
					nMinDigits = (nMaxDigits = info.NumberDecimalDigits);
				}
				else
				{
					nMinDigits = nMaxDigits;
				}
				FormatProvider.Number.RoundNumber(ref number, number.scale + nMaxDigits);
				if (number.sign)
				{
					sb.Append(info.NegativeSign);
				}
				FormatProvider.Number.FormatFixed(ref sb, ref number, nMinDigits, nMaxDigits, info, null, info.NumberDecimalSeparator, null);
				return;
				IL_EC:
				if (nMaxDigits < 0)
				{
					nMinDigits = (nMaxDigits = info.NumberDecimalDigits);
				}
				else
				{
					nMinDigits = nMaxDigits;
				}
				FormatProvider.Number.RoundNumber(ref number, number.scale + nMaxDigits);
				FormatProvider.Number.FormatNumber(ref sb, ref number, nMinDigits, nMaxDigits, info);
				return;
				IL_119:
				if (nMaxDigits < 0)
				{
					nMinDigits = (nMaxDigits = 6);
				}
				else
				{
					nMinDigits = nMaxDigits;
				}
				nMaxDigits++;
				FormatProvider.Number.RoundNumber(ref number, nMaxDigits);
				if (number.sign)
				{
					sb.Append(info.NegativeSign);
				}
				FormatProvider.Number.FormatScientific(ref sb, ref number, nMinDigits, nMaxDigits, info, format);
				return;
				IL_154:
				bool flag = true;
				if (nMaxDigits < 1)
				{
					if (isDecimal && nMaxDigits == -1)
					{
						nMinDigits = (nMaxDigits = 29);
						flag = false;
					}
					else
					{
						nMinDigits = (nMaxDigits = number.precision);
					}
				}
				else
				{
					nMinDigits = nMaxDigits;
				}
				if (flag)
				{
					FormatProvider.Number.RoundNumber(ref number, nMaxDigits);
				}
				else if (isDecimal && *number.digits == '\0')
				{
					number.sign = false;
				}
				if (number.sign)
				{
					sb.Append(info.NegativeSign);
				}
				FormatProvider.Number.FormatGeneral(ref sb, ref number, nMinDigits, nMaxDigits, info, format - '\u0002', !flag);
				return;
				IL_1C3:
				if (nMaxDigits < 0)
				{
					nMinDigits = (nMaxDigits = info.PercentDecimalDigits);
				}
				else
				{
					nMinDigits = nMaxDigits;
				}
				number.scale += 2;
				FormatProvider.Number.RoundNumber(ref number, number.scale + nMaxDigits);
				FormatProvider.Number.FormatPercent(ref sb, ref number, nMinDigits, nMaxDigits, info);
				return;
				IL_1FB:
				throw new FormatException("Format specifier was invalid.");
			}

			// Token: 0x06000272 RID: 626 RVA: 0x000118E8 File Offset: 0x0000FAE8
			private static void FormatCurrency(ref ValueStringBuilder sb, ref FormatProvider.Number.NumberBuffer number, int nMinDigits, int nMaxDigits, NumberFormatInfo info)
			{
				foreach (char c in number.sign ? FormatProvider.Number.s_negCurrencyFormats[info.CurrencyNegativePattern] : FormatProvider.Number.s_posCurrencyFormats[info.CurrencyPositivePattern])
				{
					if (c != '#')
					{
						if (c != '$')
						{
							if (c != '-')
							{
								sb.Append(c);
							}
							else
							{
								sb.Append(info.NegativeSign);
							}
						}
						else
						{
							sb.Append(info.CurrencySymbol);
						}
					}
					else
					{
						FormatProvider.Number.FormatFixed(ref sb, ref number, nMinDigits, nMaxDigits, info, info.CurrencyGroupSizes, info.CurrencyDecimalSeparator, info.CurrencyGroupSeparator);
					}
				}
			}

			// Token: 0x06000273 RID: 627 RVA: 0x0001198C File Offset: 0x0000FB8C
			private unsafe static int wcslen(char* s)
			{
				int num = 0;
				while (*(s++) != '\0')
				{
					num++;
				}
				return num;
			}

			// Token: 0x06000274 RID: 628 RVA: 0x000119AC File Offset: 0x0000FBAC
			private unsafe static void FormatFixed(ref ValueStringBuilder sb, ref FormatProvider.Number.NumberBuffer number, int nMinDigits, int nMaxDigits, NumberFormatInfo info, int[] groupDigits, string sDecimal, string sGroup)
			{
				int i = number.scale;
				char* ptr = number.digits;
				int num = FormatProvider.Number.wcslen(ptr);
				if (i > 0)
				{
					if (groupDigits != null)
					{
						int num2 = 0;
						int num3 = groupDigits[num2];
						int num4 = groupDigits.Length;
						int num5 = i;
						int length = sGroup.Length;
						int num6 = 0;
						if (num4 != 0)
						{
							while (i > num3)
							{
								num6 = groupDigits[num2];
								if (num6 == 0)
								{
									break;
								}
								num5 += length;
								if (num2 < num4 - 1)
								{
									num2++;
								}
								num3 += groupDigits[num2];
								if (num3 < 0 || num5 < 0)
								{
									throw new ArgumentOutOfRangeException();
								}
							}
							if (num3 == 0)
							{
								num6 = 0;
							}
							else
							{
								num6 = groupDigits[0];
							}
						}
						char* ptr2 = stackalloc char[checked(unchecked((UIntPtr)num5) * 2)];
						num2 = 0;
						int num7 = 0;
						int num8 = (i < num) ? i : num;
						char* ptr3 = ptr2 + num5 - 1;
						for (int j = i - 1; j >= 0; j--)
						{
							*(ptr3--) = ((j < num8) ? ptr[j] : '0');
							if (num6 > 0)
							{
								num7++;
								if (num7 == num6 && j != 0)
								{
									for (int k = length - 1; k >= 0; k--)
									{
										*(ptr3--) = sGroup[k];
									}
									if (num2 < num4 - 1)
									{
										num2++;
										num6 = groupDigits[num2];
									}
									num7 = 0;
								}
							}
						}
						sb.Append(ptr2, num5);
						ptr += num8;
					}
					else
					{
						int num9 = Math.Min(num, i);
						sb.Append(ptr, num9);
						ptr += num9;
						if (i > num)
						{
							sb.Append('0', i - num);
						}
					}
				}
				else
				{
					sb.Append('0');
				}
				if (nMaxDigits > 0)
				{
					sb.Append(sDecimal);
					if (i < 0 && nMaxDigits > 0)
					{
						int num10 = Math.Min(-i, nMaxDigits);
						sb.Append('0', num10);
						i += num10;
						nMaxDigits -= num10;
					}
					while (nMaxDigits > 0)
					{
						sb.Append((*ptr != '\0') ? (*(ptr++)) : '0');
						nMaxDigits--;
					}
				}
			}

			// Token: 0x06000275 RID: 629 RVA: 0x00011B80 File Offset: 0x0000FD80
			private static void FormatNumber(ref ValueStringBuilder sb, ref FormatProvider.Number.NumberBuffer number, int nMinDigits, int nMaxDigits, NumberFormatInfo info)
			{
				foreach (char c in number.sign ? FormatProvider.Number.s_negNumberFormats[info.NumberNegativePattern] : FormatProvider.Number.s_posNumberFormat)
				{
					if (c != '#')
					{
						if (c != '-')
						{
							sb.Append(c);
						}
						else
						{
							sb.Append(info.NegativeSign);
						}
					}
					else
					{
						FormatProvider.Number.FormatFixed(ref sb, ref number, nMinDigits, nMaxDigits, info, info.NumberGroupSizes, info.NumberDecimalSeparator, info.NumberGroupSeparator);
					}
				}
			}

			// Token: 0x06000276 RID: 630 RVA: 0x00011C08 File Offset: 0x0000FE08
			private unsafe static void FormatScientific(ref ValueStringBuilder sb, ref FormatProvider.Number.NumberBuffer number, int nMinDigits, int nMaxDigits, NumberFormatInfo info, char expChar)
			{
				char* digits = number.digits;
				sb.Append((*digits != '\0') ? (*(digits++)) : '0');
				if (nMaxDigits != 1)
				{
					sb.Append(info.NumberDecimalSeparator);
				}
				while (--nMaxDigits > 0)
				{
					sb.Append((*digits != '\0') ? (*(digits++)) : '0');
				}
				int value = (*number.digits == '\0') ? 0 : (number.scale - 1);
				FormatProvider.Number.FormatExponent(ref sb, info, value, expChar, 3, true);
			}

			// Token: 0x06000277 RID: 631 RVA: 0x00011C84 File Offset: 0x0000FE84
			private unsafe static void FormatExponent(ref ValueStringBuilder sb, NumberFormatInfo info, int value, char expChar, int minDigits, bool positiveSign)
			{
				sb.Append(expChar);
				if (value < 0)
				{
					sb.Append(info.NegativeSign);
					value = -value;
				}
				else if (positiveSign)
				{
					sb.Append(info.PositiveSign);
				}
				char* ptr = stackalloc char[(UIntPtr)22];
				int num = 10;
				FormatProvider.Number.Int32ToDecChars(ptr, ref num, (uint)value, minDigits);
				int num2 = 10 - num;
				while (--num2 >= 0)
				{
					sb.Append(ptr[(IntPtr)(num++) * 2]);
				}
			}

			// Token: 0x06000278 RID: 632 RVA: 0x00011CF4 File Offset: 0x0000FEF4
			private unsafe static void FormatGeneral(ref ValueStringBuilder sb, ref FormatProvider.Number.NumberBuffer number, int nMinDigits, int nMaxDigits, NumberFormatInfo info, char expChar, bool bSuppressScientific)
			{
				int i = number.scale;
				bool flag = false;
				if (!bSuppressScientific && (i > nMaxDigits || i < -3))
				{
					i = 1;
					flag = true;
				}
				char* digits = number.digits;
				if (i > 0)
				{
					do
					{
						sb.Append((*digits != '\0') ? (*(digits++)) : '0');
					}
					while (--i > 0);
				}
				else
				{
					sb.Append('0');
				}
				if (*digits != '\0' || i < 0)
				{
					sb.Append(info.NumberDecimalSeparator);
					while (i < 0)
					{
						sb.Append('0');
						i++;
					}
					while (*digits != '\0')
					{
						sb.Append(*(digits++));
					}
				}
				if (flag)
				{
					FormatProvider.Number.FormatExponent(ref sb, info, number.scale - 1, expChar, 2, true);
				}
			}

			// Token: 0x06000279 RID: 633 RVA: 0x00011D9C File Offset: 0x0000FF9C
			private static void FormatPercent(ref ValueStringBuilder sb, ref FormatProvider.Number.NumberBuffer number, int nMinDigits, int nMaxDigits, NumberFormatInfo info)
			{
				foreach (char c in number.sign ? FormatProvider.Number.s_negPercentFormats[info.PercentNegativePattern] : FormatProvider.Number.s_posPercentFormats[info.PercentPositivePattern])
				{
					if (c != '#')
					{
						if (c != '%')
						{
							if (c != '-')
							{
								sb.Append(c);
							}
							else
							{
								sb.Append(info.NegativeSign);
							}
						}
						else
						{
							sb.Append(info.PercentSymbol);
						}
					}
					else
					{
						FormatProvider.Number.FormatFixed(ref sb, ref number, nMinDigits, nMaxDigits, info, info.PercentGroupSizes, info.PercentDecimalSeparator, info.PercentGroupSeparator);
					}
				}
			}

			// Token: 0x0600027A RID: 634 RVA: 0x00011E40 File Offset: 0x00010040
			private unsafe static void RoundNumber(ref FormatProvider.Number.NumberBuffer number, int pos)
			{
				char* digits = number.digits;
				int num = 0;
				while (num < pos && digits[num] != '\0')
				{
					num++;
				}
				if (num == pos && digits[num] >= '5')
				{
					while (num > 0 && digits[num - 1] == '9')
					{
						num--;
					}
					if (num > 0)
					{
						char* ptr = digits + (num - 1);
						*ptr += '\u0001';
					}
					else
					{
						number.scale++;
						*digits = '1';
						num = 1;
					}
				}
				else
				{
					while (num > 0 && digits[num - 1] == '0')
					{
						num--;
					}
				}
				if (num == 0)
				{
					number.scale = 0;
					number.sign = false;
				}
				digits[num] = '\0';
			}

			// Token: 0x0600027B RID: 635 RVA: 0x00011EE4 File Offset: 0x000100E4
			private unsafe static int FindSection(ReadOnlySpan<char> format, int section)
			{
				if (section == 0)
				{
					return 0;
				}
				fixed (char* reference = MemoryMarshal.GetReference<char>(format))
				{
					char* ptr = reference;
					int i = 0;
					while (i < format.Length)
					{
						char c2;
						char c = c2 = ptr[(IntPtr)(i++) * 2];
						if (c2 <= '"')
						{
							if (c2 == '\0')
							{
								return 0;
							}
							if (c2 != '"')
							{
								continue;
							}
						}
						else if (c2 != '\'')
						{
							if (c2 != ';')
							{
								if (c2 != '\\')
								{
									continue;
								}
								if (i < format.Length && ptr[i] != '\0')
								{
									i++;
									continue;
								}
								continue;
							}
							else
							{
								if (--section != 0)
								{
									continue;
								}
								if (i < format.Length && ptr[i] != '\0' && ptr[i] != ';')
								{
									return i;
								}
								return 0;
							}
						}
						while (i < format.Length && ptr[i] != '\0')
						{
							if (ptr[(IntPtr)(i++) * 2] == c)
							{
								break;
							}
						}
					}
					return 0;
				}
			}

			// Token: 0x0600027C RID: 636 RVA: 0x00011FB0 File Offset: 0x000101B0
			internal unsafe static void NumberToStringFormat(ref ValueStringBuilder sb, ref FormatProvider.Number.NumberBuffer number, ReadOnlySpan<char> format, NumberFormatInfo info)
			{
				int num = 0;
				char* digits = number.digits;
				int num2 = FormatProvider.Number.FindSection(format, (*digits == '\0') ? 2 : (number.sign ? 1 : 0));
				int num3;
				int num4;
				int num5;
				int num6;
				bool flag;
				bool flag2;
				int i;
				for (;;)
				{
					num3 = 0;
					num4 = -1;
					num5 = int.MaxValue;
					num6 = 0;
					flag = false;
					int num7 = -1;
					flag2 = false;
					int num8 = 0;
					i = num2;
					fixed (char* reference = MemoryMarshal.GetReference<char>(format))
					{
						char* ptr = reference;
						char c;
						while (i < format.Length && (c = ptr[(IntPtr)(i++) * 2]) != '\0' && c != ';')
						{
							if (c <= 'E')
							{
								switch (c)
								{
								case '"':
								case '\'':
									while (i < format.Length && ptr[i] != '\0')
									{
										if (ptr[(IntPtr)(i++) * 2] == c)
										{
											break;
										}
									}
									continue;
								case '#':
									num3++;
									continue;
								case '$':
								case '&':
									continue;
								case '%':
									num8 += 2;
									continue;
								default:
									switch (c)
									{
									case ',':
										if (num3 > 0 && num4 < 0)
										{
											if (num7 >= 0)
											{
												if (num7 == num3)
												{
													num++;
													continue;
												}
												flag2 = true;
											}
											num7 = num3;
											num = 1;
											continue;
										}
										continue;
									case '-':
									case '/':
										continue;
									case '.':
										if (num4 < 0)
										{
											num4 = num3;
											continue;
										}
										continue;
									case '0':
										if (num5 == 2147483647)
										{
											num5 = num3;
										}
										num3++;
										num6 = num3;
										continue;
									default:
										if (c != 'E')
										{
											continue;
										}
										break;
									}
									break;
								}
							}
							else if (c != '\\')
							{
								if (c != 'e')
								{
									if (c != '‰')
									{
										continue;
									}
									num8 += 3;
									continue;
								}
							}
							else
							{
								if (i < format.Length && ptr[i] != '\0')
								{
									i++;
									continue;
								}
								continue;
							}
							if ((i < format.Length && ptr[i] == '0') || (i + 1 < format.Length && (ptr[i] == '+' || ptr[i] == '-') && ptr[i + 1] == '0'))
							{
								while (++i < format.Length && ptr[i] == '0')
								{
								}
								flag = true;
							}
						}
					}
					if (num4 < 0)
					{
						num4 = num3;
					}
					if (num7 >= 0)
					{
						if (num7 == num4)
						{
							num8 -= num * 3;
						}
						else
						{
							flag2 = true;
						}
					}
					if (*digits == '\0')
					{
						break;
					}
					number.scale += num8;
					int pos = flag ? num3 : (number.scale + num3 - num4);
					FormatProvider.Number.RoundNumber(ref number, pos);
					if (*digits != '\0')
					{
						goto IL_29E;
					}
					i = FormatProvider.Number.FindSection(format, 2);
					if (i == num2)
					{
						goto IL_29E;
					}
					num2 = i;
				}
				number.sign = false;
				number.scale = 0;
				IL_29E:
				num5 = ((num5 < num4) ? (num4 - num5) : 0);
				num6 = ((num6 > num4) ? (num4 - num6) : 0);
				int num9;
				int j;
				if (flag)
				{
					num9 = num4;
					j = 0;
				}
				else
				{
					num9 = ((number.scale > num4) ? number.scale : num4);
					j = number.scale - num4;
				}
				i = num2;
				Span<int> span = new Span<int>(stackalloc byte[(UIntPtr)16], 4);
				int num10 = -1;
				if (flag2 && info.NumberGroupSeparator.Length > 0)
				{
					int[] numberGroupSizes = info.NumberGroupSizes;
					int num11 = 0;
					int num12 = 0;
					int num13 = numberGroupSizes.Length;
					if (num13 != 0)
					{
						num12 = numberGroupSizes[num11];
					}
					int num14 = num12;
					int num15 = num9 + ((j < 0) ? j : 0);
					int num16 = (num5 > num15) ? num5 : num15;
					while (num16 > num12 && num14 != 0)
					{
						num10++;
						if (num10 >= span.Length)
						{
							int[] array = new int[span.Length * 2];
							span.CopyTo(array);
							span = array;
						}
						*span[num10] = num12;
						if (num11 < num13 - 1)
						{
							num11++;
							num14 = numberGroupSizes[num11];
						}
						num12 += num14;
					}
				}
				if (number.sign && num2 == 0)
				{
					sb.Append(info.NegativeSign);
				}
				bool flag3 = false;
				fixed (char* reference = MemoryMarshal.GetReference<char>(format))
				{
					char* ptr2 = reference;
					char* ptr3 = digits;
					char c;
					while (i < format.Length && (c = ptr2[(IntPtr)(i++) * 2]) != '\0' && c != ';')
					{
						if (j > 0)
						{
							if (c == '#' || c == '.' || c == '0')
							{
								while (j > 0)
								{
									sb.Append((*ptr3 != '\0') ? (*(ptr3++)) : '0');
									if (flag2 && num9 > 1 && num10 >= 0 && num9 == *span[num10] + 1)
									{
										sb.Append(info.NumberGroupSeparator);
										num10--;
									}
									num9--;
									j--;
								}
							}
						}
						if (c <= 'E')
						{
							switch (c)
							{
							case '"':
							case '\'':
								while (i < format.Length && ptr2[i] != '\0' && ptr2[i] != c)
								{
									sb.Append(ptr2[(IntPtr)(i++) * 2]);
								}
								if (i < format.Length && ptr2[i] != '\0')
								{
									i++;
									continue;
								}
								continue;
							case '#':
								break;
							case '$':
							case '&':
								goto IL_786;
							case '%':
								sb.Append(info.PercentSymbol);
								continue;
							default:
								switch (c)
								{
								case ',':
									continue;
								case '-':
								case '/':
									goto IL_786;
								case '.':
									if (num9 == 0 && !flag3 && (num6 < 0 || (num4 < num3 && *ptr3 != '\0')))
									{
										sb.Append(info.NumberDecimalSeparator);
										flag3 = true;
										continue;
									}
									continue;
								case '0':
									break;
								default:
									if (c != 'E')
									{
										goto IL_786;
									}
									goto IL_631;
								}
								break;
							}
							if (j < 0)
							{
								j++;
								c = ((num9 <= num5) ? '0' : '\0');
							}
							else
							{
								c = ((*ptr3 != '\0') ? (*(ptr3++)) : ((num9 > num6) ? '0' : '\0'));
							}
							if (c != '\0')
							{
								sb.Append(c);
								if (flag2 && num9 > 1 && num10 >= 0 && num9 == *span[num10] + 1)
								{
									sb.Append(info.NumberGroupSeparator);
									num10--;
								}
							}
							num9--;
							continue;
						}
						if (c != '\\')
						{
							if (c != 'e')
							{
								if (c != '‰')
								{
									goto IL_786;
								}
								sb.Append(info.PerMilleSymbol);
								continue;
							}
						}
						else
						{
							if (i < format.Length && ptr2[i] != '\0')
							{
								sb.Append(ptr2[(IntPtr)(i++) * 2]);
								continue;
							}
							continue;
						}
						IL_631:
						bool positiveSign = false;
						int num17 = 0;
						if (flag)
						{
							if (i < format.Length && ptr2[i] == '0')
							{
								num17++;
							}
							else if (i + 1 < format.Length && ptr2[i] == '+' && ptr2[i + 1] == '0')
							{
								positiveSign = true;
							}
							else if (i + 1 >= format.Length || ptr2[i] != '-' || ptr2[i + 1] != '0')
							{
								sb.Append(c);
								continue;
							}
							while (++i < format.Length && ptr2[i] == '0')
							{
								num17++;
							}
							if (num17 > 10)
							{
								num17 = 10;
							}
							int value = (*digits == '\0') ? 0 : (number.scale - num4);
							FormatProvider.Number.FormatExponent(ref sb, info, value, c, num17, positiveSign);
							flag = false;
							continue;
						}
						sb.Append(c);
						if (i < format.Length)
						{
							if (ptr2[i] == '+' || ptr2[i] == '-')
							{
								sb.Append(ptr2[(IntPtr)(i++) * 2]);
							}
							while (i < format.Length)
							{
								if (ptr2[i] != '0')
								{
									break;
								}
								sb.Append(ptr2[(IntPtr)(i++) * 2]);
							}
							continue;
						}
						continue;
						IL_786:
						sb.Append(c);
					}
				}
			}

			// Token: 0x0600027D RID: 637 RVA: 0x00012778 File Offset: 0x00010978
			// Note: this type is marked as 'beforefieldinit'.
			static Number()
			{
			}

			// Token: 0x04000099 RID: 153
			private const int NumberMaxDigits = 32;

			// Token: 0x0400009A RID: 154
			internal const int DECIMAL_PRECISION = 29;

			// Token: 0x0400009B RID: 155
			private const int MIN_SB_BUFFER_SIZE = 105;

			// Token: 0x0400009C RID: 156
			private static string[] s_posCurrencyFormats = new string[]
			{
				"$#",
				"#$",
				"$ #",
				"# $"
			};

			// Token: 0x0400009D RID: 157
			private static string[] s_negCurrencyFormats = new string[]
			{
				"($#)",
				"-$#",
				"$-#",
				"$#-",
				"(#$)",
				"-#$",
				"#-$",
				"#$-",
				"-# $",
				"-$ #",
				"# $-",
				"$ #-",
				"$ -#",
				"#- $",
				"($ #)",
				"(# $)"
			};

			// Token: 0x0400009E RID: 158
			private static string[] s_posPercentFormats = new string[]
			{
				"# %",
				"#%",
				"%#",
				"% #"
			};

			// Token: 0x0400009F RID: 159
			private static string[] s_negPercentFormats = new string[]
			{
				"-# %",
				"-#%",
				"-%#",
				"%-#",
				"%#-",
				"#-%",
				"#%-",
				"-% #",
				"# %-",
				"% #-",
				"% -#",
				"#- %"
			};

			// Token: 0x040000A0 RID: 160
			private static string[] s_negNumberFormats = new string[]
			{
				"(#)",
				"-#",
				"- #",
				"#-",
				"# -"
			};

			// Token: 0x040000A1 RID: 161
			private static string s_posNumberFormat = "#";

			// Token: 0x0200001A RID: 26
			internal struct NumberBuffer
			{
				// Token: 0x17000024 RID: 36
				// (get) Token: 0x0600027E RID: 638 RVA: 0x0001291A File Offset: 0x00010B1A
				public unsafe char* digits
				{
					get
					{
						return this.overrideDigits;
					}
				}

				// Token: 0x040000A2 RID: 162
				public int precision;

				// Token: 0x040000A3 RID: 163
				public int scale;

				// Token: 0x040000A4 RID: 164
				public bool sign;

				// Token: 0x040000A5 RID: 165
				public unsafe char* overrideDigits;
			}
		}
	}
}
