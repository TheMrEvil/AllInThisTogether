using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Xml.Xsl
{
	// Token: 0x0200032E RID: 814
	internal static class XPathConvert
	{
		// Token: 0x06002166 RID: 8550 RVA: 0x000D2E91 File Offset: 0x000D1091
		public static uint DblHi(double dbl)
		{
			return (uint)(BitConverter.DoubleToInt64Bits(dbl) >> 32);
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x000D2E9D File Offset: 0x000D109D
		public static uint DblLo(double dbl)
		{
			return (uint)BitConverter.DoubleToInt64Bits(dbl);
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x000D2EA6 File Offset: 0x000D10A6
		public static bool IsSpecial(double dbl)
		{
			return (~XPathConvert.DblHi(dbl) & 2146435072U) == 0U;
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x000D2EB8 File Offset: 0x000D10B8
		public static uint NotZero(uint u)
		{
			if (u == 0U)
			{
				return 0U;
			}
			return 1U;
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x000D2EC0 File Offset: 0x000D10C0
		public static uint AddU(ref uint u1, uint u2)
		{
			u1 += u2;
			if (u1 >= u2)
			{
				return 0U;
			}
			return 1U;
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x000D2ED0 File Offset: 0x000D10D0
		public static uint MulU(uint u1, uint u2, out uint uHi)
		{
			ulong num = (ulong)u1 * (ulong)u2;
			uHi = (uint)(num >> 32);
			return (uint)num;
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x000D2EEC File Offset: 0x000D10EC
		public static int CbitZeroLeft(uint u)
		{
			int num = 0;
			if ((u & 4294901760U) == 0U)
			{
				num += 16;
				u <<= 16;
			}
			if ((u & 4278190080U) == 0U)
			{
				num += 8;
				u <<= 8;
			}
			if ((u & 4026531840U) == 0U)
			{
				num += 4;
				u <<= 4;
			}
			if ((u & 3221225472U) == 0U)
			{
				num += 2;
				u <<= 2;
			}
			if ((u & 2147483648U) == 0U)
			{
				num++;
				u <<= 1;
			}
			return num;
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x000D2F58 File Offset: 0x000D1158
		public static bool IsInteger(double dbl, out int value)
		{
			if (!XPathConvert.IsSpecial(dbl))
			{
				int num = (int)dbl;
				double num2 = (double)num;
				if (dbl == num2)
				{
					value = num;
					return true;
				}
			}
			value = 0;
			return false;
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x000D2F80 File Offset: 0x000D1180
		private unsafe static string IntToString(int val)
		{
			char* ptr = stackalloc char[(UIntPtr)24];
			char* ptr2;
			ptr = (ptr2 = ptr + 12);
			uint num;
			uint num2;
			for (num = (uint)((val < 0) ? (-(uint)val) : val); num >= 10U; num = num2)
			{
				num2 = (uint)(1717986919UL * (ulong)num >> 32) >> 2;
				*(--ptr2) = (char)(num - num2 * 10U + 48U);
			}
			*(--ptr2) = (char)(num + 48U);
			if (val < 0)
			{
				*(--ptr2) = '-';
			}
			return new string(ptr2, 0, (int)((long)(ptr - ptr2)));
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x000D2FF8 File Offset: 0x000D11F8
		public unsafe static string DoubleToString(double dbl)
		{
			int val;
			if (XPathConvert.IsInteger(dbl, out val))
			{
				return XPathConvert.IntToString(val);
			}
			if (!XPathConvert.IsSpecial(dbl))
			{
				XPathConvert.FloatingDecimal floatingDecimal = new XPathConvert.FloatingDecimal(dbl);
				int i = floatingDecimal.MantissaSize - floatingDecimal.Exponent;
				int num;
				if (i > 0)
				{
					num = ((floatingDecimal.Exponent > 0) ? floatingDecimal.Exponent : 0);
				}
				else
				{
					num = floatingDecimal.Exponent;
					i = 0;
				}
				int num2 = num + i + 4;
				char* ptr = stackalloc char[checked(unchecked((UIntPtr)num2) * 2)];
				char* ptr2 = ptr;
				if (floatingDecimal.Sign < 0)
				{
					*(ptr2++) = '-';
				}
				int num3 = floatingDecimal.MantissaSize;
				int num4 = 0;
				if (num != 0)
				{
					do
					{
						if (num3 != 0)
						{
							*(ptr2++) = (char)(floatingDecimal[num4++] | 48);
							num3--;
						}
						else
						{
							*(ptr2++) = '0';
						}
					}
					while (--num != 0);
				}
				else
				{
					*(ptr2++) = '0';
				}
				if (i != 0)
				{
					*(ptr2++) = '.';
					while (i > num3)
					{
						*(ptr2++) = '0';
						i--;
					}
					while (num3 != 0)
					{
						*(ptr2++) = (char)(floatingDecimal[num4++] | 48);
						num3--;
					}
				}
				return new string(ptr, 0, (int)((long)(ptr2 - ptr)));
			}
			if (double.IsNaN(dbl))
			{
				return "NaN";
			}
			if (dbl >= 0.0)
			{
				return "Infinity";
			}
			return "-Infinity";
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x000D314B File Offset: 0x000D134B
		private static bool IsAsciiDigit(char ch)
		{
			return ch - '0' <= '\t';
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x000D3158 File Offset: 0x000D1358
		private static bool IsWhitespace(char ch)
		{
			return ch == ' ' || ch == '\t' || ch == '\n' || ch == '\r';
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x000D3170 File Offset: 0x000D1370
		private unsafe static char* SkipWhitespace(char* pch)
		{
			while (XPathConvert.IsWhitespace(*pch))
			{
				pch++;
			}
			return pch;
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x000D3184 File Offset: 0x000D1384
		public unsafe static double StringToDouble(string s)
		{
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			int num = 0;
			char* ptr2 = ptr;
			char* ptr3 = null;
			int num2 = 1;
			int num3 = 0;
			char c;
			for (;;)
			{
				c = *(ptr2++);
				if (XPathConvert.IsAsciiDigit(c))
				{
					goto IL_73;
				}
				if (c != '-')
				{
					if (c == '.')
					{
						break;
					}
					if (!XPathConvert.IsWhitespace(c) || num2 <= 0)
					{
						goto IL_69;
					}
					ptr2 = XPathConvert.SkipWhitespace(ptr2);
				}
				else
				{
					if (num2 < 0)
					{
						goto IL_69;
					}
					num2 = -1;
				}
			}
			if (XPathConvert.IsAsciiDigit(*ptr2))
			{
				goto IL_B7;
			}
			IL_69:
			return double.NaN;
			IL_73:
			if (c == '0')
			{
				do
				{
					c = *(ptr2++);
				}
				while (c == '0');
				if (!XPathConvert.IsAsciiDigit(c))
				{
					goto IL_B1;
				}
			}
			ptr3 = ptr2 - 1;
			do
			{
				c = *(ptr2++);
			}
			while (XPathConvert.IsAsciiDigit(c));
			num = (int)((long)(ptr2 - ptr3)) - 1;
			IL_B1:
			if (c != '.')
			{
				goto IL_FD;
			}
			IL_B7:
			c = *(ptr2++);
			if (ptr3 == null)
			{
				while (c == '0')
				{
					num3--;
					c = *(ptr2++);
				}
				ptr3 = ptr2 - 1;
			}
			while (XPathConvert.IsAsciiDigit(c))
			{
				num3--;
				num++;
				c = *(ptr2++);
			}
			IL_FD:
			ptr2--;
			char* ptr4 = ptr + s.Length;
			if (ptr2 < ptr4 && XPathConvert.SkipWhitespace(ptr2) < ptr4)
			{
				return double.NaN;
			}
			if (num == 0)
			{
				return 0.0;
			}
			if (num3 == 0 && num <= 9)
			{
				int num4 = (int)(*ptr3 & '\u000f');
				while (--num != 0)
				{
					ptr3++;
					num4 = num4 * 10 + (int)(*ptr3 & '\u000f');
				}
				return (double)((num2 < 0) ? (-(double)num4) : num4);
			}
			if (num > 50)
			{
				ptr2 -= num - 50;
				num3 += num - 50;
				num = 50;
			}
			for (;;)
			{
				if (*(--ptr2) == '0')
				{
					num--;
					num3++;
				}
				else if (*ptr2 != '.')
				{
					break;
				}
			}
			ptr2++;
			XPathConvert.FloatingDecimal floatingDecimal = new XPathConvert.FloatingDecimal();
			floatingDecimal.Exponent = num3 + num;
			floatingDecimal.Sign = num2;
			floatingDecimal.MantissaSize = num;
			byte[] array;
			byte* ptr5;
			if ((array = floatingDecimal.Mantissa) == null || array.Length == 0)
			{
				ptr5 = null;
			}
			else
			{
				ptr5 = &array[0];
			}
			byte* ptr6 = ptr5;
			while (ptr3 < ptr2)
			{
				if (*ptr3 != '.')
				{
					*ptr6 = (byte)(*ptr3 & '\u000f');
					ptr6++;
				}
				ptr3++;
			}
			array = null;
			return (double)floatingDecimal;
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x000D33B5 File Offset: 0x000D15B5
		// Note: this type is marked as 'beforefieldinit'.
		static XPathConvert()
		{
		}

		// Token: 0x04001B9E RID: 7070
		public static readonly double[] C10toN = new double[]
		{
			1.0,
			10.0,
			100.0,
			1000.0,
			10000.0,
			100000.0,
			1000000.0,
			10000000.0,
			100000000.0,
			1000000000.0,
			10000000000.0,
			100000000000.0,
			1000000000000.0,
			10000000000000.0,
			100000000000000.0,
			1000000000000000.0,
			10000000000000000.0,
			1E+17,
			1E+18,
			1E+19,
			1E+20,
			1E+21,
			1E+22
		};

		// Token: 0x0200032F RID: 815
		private struct BigNumber
		{
			// Token: 0x17000686 RID: 1670
			// (get) Token: 0x06002175 RID: 8565 RVA: 0x000D33CE File Offset: 0x000D15CE
			public uint Error
			{
				get
				{
					return this.error;
				}
			}

			// Token: 0x06002176 RID: 8566 RVA: 0x000D33D6 File Offset: 0x000D15D6
			public BigNumber(uint u0, uint u1, uint u2, int exp, uint error)
			{
				this.u0 = u0;
				this.u1 = u1;
				this.u2 = u2;
				this.exp = exp;
				this.error = error;
			}

			// Token: 0x06002177 RID: 8567 RVA: 0x000D3400 File Offset: 0x000D1600
			public BigNumber(XPathConvert.FloatingDecimal dec)
			{
				int num = 0;
				int num2 = dec.Exponent;
				int mantissaSize = dec.MantissaSize;
				this.u2 = (uint)((uint)dec[num] << 28);
				this.u1 = 0U;
				this.u0 = 0U;
				this.exp = 4;
				this.error = 0U;
				num2--;
				this.Normalize();
				while (++num < mantissaSize)
				{
					uint num3 = this.MulTenAdd((uint)dec[num]);
					num2--;
					if (num3 != 0U)
					{
						this.Round(num3);
						if (num < mantissaSize - 1)
						{
							this.error += 1U;
							break;
						}
						break;
					}
				}
				if (num2 == 0)
				{
					return;
				}
				XPathConvert.BigNumber[] array;
				if (num2 < 0)
				{
					array = XPathConvert.BigNumber.TenPowersNeg;
					num2 = -num2;
				}
				else
				{
					array = XPathConvert.BigNumber.TenPowersPos;
				}
				int num4 = num2 & 31;
				if (num4 > 0)
				{
					this.Mul(ref array[num4 - 1]);
				}
				num4 = (num2 >> 5 & 15);
				if (num4 > 0)
				{
					this.Mul(ref array[num4 + 30]);
				}
			}

			// Token: 0x06002178 RID: 8568 RVA: 0x000D34E4 File Offset: 0x000D16E4
			private unsafe uint MulTenAdd(uint digit)
			{
				this.exp += 3;
				uint* ptr = stackalloc uint[(UIntPtr)20];
				for (int i = 0; i < 5; i++)
				{
					ptr[i] = 0U;
				}
				if (digit != 0U)
				{
					int num = 3 - (this.exp >> 5);
					if (num < 0)
					{
						*ptr = 1U;
					}
					else
					{
						int num2 = this.exp & 31;
						if (num2 < 4)
						{
							ptr[num + 1] = digit >> num2;
							if (num2 > 0)
							{
								ptr[num] = digit << 32 - num2;
							}
						}
						else
						{
							ptr[num] = digit << 32 - num2;
						}
					}
				}
				ptr[1] += XPathConvert.AddU(ref *ptr, this.u0 << 30);
				ptr[2] += XPathConvert.AddU(ref this.u0, (this.u0 >> 2) + (this.u1 << 30));
				if (ptr[1] != 0U)
				{
					ptr[2] += XPathConvert.AddU(ref this.u0, ptr[1]);
				}
				ptr[3] += XPathConvert.AddU(ref this.u1, (this.u1 >> 2) + (this.u2 << 30));
				if (ptr[2] != 0U)
				{
					ptr[3] += XPathConvert.AddU(ref this.u1, ptr[2]);
				}
				ptr[4] = XPathConvert.AddU(ref this.u2, (this.u2 >> 2) + ptr[3]);
				if (ptr[4] != 0U)
				{
					*ptr = (*ptr >> 1 | (*ptr & 1U) | this.u0 << 31);
					this.u0 = (this.u0 >> 1 | this.u1 << 31);
					this.u1 = (this.u1 >> 1 | this.u2 << 31);
					this.u2 = (this.u2 >> 1 | 2147483648U);
					this.exp++;
				}
				return *ptr;
			}

			// Token: 0x06002179 RID: 8569 RVA: 0x000D36B0 File Offset: 0x000D18B0
			private void Round(uint uExtra)
			{
				if ((uExtra & 2147483648U) == 0U || ((uExtra & 2147483647U) == 0U && (this.u0 & 1U) == 0U))
				{
					if (uExtra != 0U)
					{
						this.error += 1U;
					}
					return;
				}
				this.error += 1U;
				if (XPathConvert.AddU(ref this.u0, 1U) != 0U && XPathConvert.AddU(ref this.u1, 1U) != 0U && XPathConvert.AddU(ref this.u2, 1U) != 0U)
				{
					this.u2 = 2147483648U;
					this.exp++;
				}
			}

			// Token: 0x17000687 RID: 1671
			// (get) Token: 0x0600217A RID: 8570 RVA: 0x000D373C File Offset: 0x000D193C
			private bool IsZero
			{
				get
				{
					return this.u2 == 0U && this.u1 == 0U && this.u0 == 0U;
				}
			}

			// Token: 0x0600217B RID: 8571 RVA: 0x000D375C File Offset: 0x000D195C
			private void Normalize()
			{
				if (this.u2 == 0U)
				{
					if (this.u1 == 0U)
					{
						if (this.u0 == 0U)
						{
							this.exp = 0;
							return;
						}
						this.u2 = this.u0;
						this.u0 = 0U;
						this.exp -= 64;
					}
					else
					{
						this.u2 = this.u1;
						this.u1 = this.u0;
						this.u0 = 0U;
						this.exp -= 32;
					}
				}
				int num;
				if ((num = XPathConvert.CbitZeroLeft(this.u2)) != 0)
				{
					int num2 = 32 - num;
					this.u2 = (this.u2 << num | this.u1 >> num2);
					this.u1 = (this.u1 << num | this.u0 >> num2);
					this.u0 <<= num;
					this.exp -= num;
				}
			}

			// Token: 0x0600217C RID: 8572 RVA: 0x000D3848 File Offset: 0x000D1A48
			private void Mul(ref XPathConvert.BigNumber numOp)
			{
				uint num = 0U;
				uint num2 = 0U;
				uint num3 = 0U;
				uint num4 = 0U;
				uint num5 = 0U;
				uint num6 = 0U;
				uint num7;
				uint num9;
				uint num8;
				uint num10;
				if ((num7 = this.u0) != 0U)
				{
					num8 = XPathConvert.MulU(num7, numOp.u0, out num9);
					num = num8;
					num2 = num9;
					num8 = XPathConvert.MulU(num7, numOp.u1, out num9);
					num10 = XPathConvert.AddU(ref num2, num8);
					XPathConvert.AddU(ref num3, num9 + num10);
					num8 = XPathConvert.MulU(num7, numOp.u2, out num9);
					num10 = XPathConvert.AddU(ref num3, num8);
					XPathConvert.AddU(ref num4, num9 + num10);
				}
				if ((num7 = this.u1) != 0U)
				{
					num8 = XPathConvert.MulU(num7, numOp.u0, out num9);
					num10 = XPathConvert.AddU(ref num2, num8);
					num10 = XPathConvert.AddU(ref num3, num9 + num10);
					if (num10 != 0U && XPathConvert.AddU(ref num4, 1U) != 0U)
					{
						XPathConvert.AddU(ref num5, 1U);
					}
					num8 = XPathConvert.MulU(num7, numOp.u1, out num9);
					num10 = XPathConvert.AddU(ref num3, num8);
					num10 = XPathConvert.AddU(ref num4, num9 + num10);
					if (num10 != 0U)
					{
						XPathConvert.AddU(ref num5, 1U);
					}
					num8 = XPathConvert.MulU(num7, numOp.u2, out num9);
					num10 = XPathConvert.AddU(ref num4, num8);
					XPathConvert.AddU(ref num5, num9 + num10);
				}
				num7 = this.u2;
				num8 = XPathConvert.MulU(num7, numOp.u0, out num9);
				num10 = XPathConvert.AddU(ref num3, num8);
				num10 = XPathConvert.AddU(ref num4, num9 + num10);
				if (num10 != 0U && XPathConvert.AddU(ref num5, 1U) != 0U)
				{
					XPathConvert.AddU(ref num6, 1U);
				}
				num8 = XPathConvert.MulU(num7, numOp.u1, out num9);
				num10 = XPathConvert.AddU(ref num4, num8);
				num10 = XPathConvert.AddU(ref num5, num9 + num10);
				if (num10 != 0U)
				{
					XPathConvert.AddU(ref num6, 1U);
				}
				num8 = XPathConvert.MulU(num7, numOp.u2, out num9);
				num10 = XPathConvert.AddU(ref num5, num8);
				XPathConvert.AddU(ref num6, num9 + num10);
				this.exp += numOp.exp;
				this.error += numOp.error;
				if ((num6 & 2147483648U) == 0U)
				{
					if ((num3 & 1073741824U) != 0U && ((num3 & 3221225471U) != 0U || num2 != 0U || num != 0U) && XPathConvert.AddU(ref num3, 1073741824U) != 0U && XPathConvert.AddU(ref num4, 1U) != 0U && XPathConvert.AddU(ref num5, 1U) != 0U)
					{
						XPathConvert.AddU(ref num6, 1U);
						if ((num6 & 2147483648U) != 0U)
						{
							goto IL_314;
						}
					}
					this.u2 = (num6 << 1 | num5 >> 31);
					this.u1 = (num5 << 1 | num4 >> 31);
					this.u0 = (num4 << 1 | num3 >> 31);
					this.exp--;
					this.error <<= 1;
					if ((num3 & 2147483647U) != 0U || num2 != 0U || num != 0U)
					{
						this.error += 1U;
					}
					return;
				}
				if ((num3 & 2147483648U) != 0U && ((num4 & 1U) != 0U || (num3 & 2147483647U) != 0U || num2 != 0U || num != 0U) && XPathConvert.AddU(ref num4, 1U) != 0U && XPathConvert.AddU(ref num5, 1U) != 0U && XPathConvert.AddU(ref num6, 1U) != 0U)
				{
					num6 = 2147483648U;
					this.exp++;
				}
				IL_314:
				this.u2 = num6;
				this.u1 = num5;
				this.u0 = num4;
				if (num3 != 0U || num2 != 0U || num != 0U)
				{
					this.error += 1U;
				}
			}

			// Token: 0x0600217D RID: 8573 RVA: 0x000D3B98 File Offset: 0x000D1D98
			public static explicit operator double(XPathConvert.BigNumber bn)
			{
				int num = bn.exp + 1022;
				if (num >= 2047)
				{
					return double.PositiveInfinity;
				}
				uint num2;
				uint num3;
				uint num4;
				if (num > 0)
				{
					num2 = (uint)(num << 20 | (int)((bn.u2 & 2147483647U) >> 11));
					num3 = (bn.u2 << 21 | bn.u1 >> 11);
					num4 = (bn.u1 << 21 | XPathConvert.NotZero(bn.u0));
				}
				else if (num > -20)
				{
					int num5 = 12 - num;
					num2 = bn.u2 >> num5;
					num3 = (bn.u2 << 32 - num5 | bn.u1 >> num5);
					num4 = (bn.u1 << 32 - num5 | XPathConvert.NotZero(bn.u0));
				}
				else if (num == -20)
				{
					num2 = 0U;
					num3 = bn.u2;
					num4 = (bn.u1 | ((bn.u0 != 0U) ? 1U : 0U));
				}
				else if (num > -52)
				{
					int num6 = -num - 20;
					num2 = 0U;
					num3 = bn.u2 >> num6;
					num4 = (bn.u2 << 32 - num6 | XPathConvert.NotZero(bn.u1) | XPathConvert.NotZero(bn.u0));
				}
				else
				{
					if (num != -52)
					{
						return 0.0;
					}
					num2 = 0U;
					num3 = 0U;
					num4 = (bn.u2 | XPathConvert.NotZero(bn.u1) | XPathConvert.NotZero(bn.u0));
				}
				if ((num4 & 2147483648U) != 0U && ((num4 & 2147483647U) != 0U || (num3 & 1U) != 0U) && XPathConvert.AddU(ref num3, 1U) != 0U)
				{
					XPathConvert.AddU(ref num2, 1U);
				}
				return BitConverter.Int64BitsToDouble((long)((ulong)num2 << 32 | (ulong)num3));
			}

			// Token: 0x0600217E RID: 8574 RVA: 0x000D3D38 File Offset: 0x000D1F38
			private uint UMod1()
			{
				if (this.exp <= 0)
				{
					return 0U;
				}
				uint result = this.u2 >> 32 - this.exp;
				this.u2 &= 2147483647U >> this.exp - 1;
				this.Normalize();
				return result;
			}

			// Token: 0x0600217F RID: 8575 RVA: 0x000D3D88 File Offset: 0x000D1F88
			public void MakeUpperBound()
			{
				uint num = this.error + 1U >> 1;
				if (num != 0U && XPathConvert.AddU(ref this.u0, num) != 0U && XPathConvert.AddU(ref this.u1, 1U) != 0U && XPathConvert.AddU(ref this.u2, 1U) != 0U)
				{
					this.u2 = 2147483648U;
					this.u0 = (this.u0 >> 1) + (this.u0 & 1U);
					this.exp++;
				}
				this.error = 0U;
			}

			// Token: 0x06002180 RID: 8576 RVA: 0x000D3E04 File Offset: 0x000D2004
			public void MakeLowerBound()
			{
				uint num = this.error + 1U >> 1;
				if (num != 0U && XPathConvert.AddU(ref this.u0, -num) == 0U && XPathConvert.AddU(ref this.u1, 4294967295U) == 0U)
				{
					XPathConvert.AddU(ref this.u2, uint.MaxValue);
					if ((2147483648U & this.u2) == 0U)
					{
						this.Normalize();
					}
				}
				this.error = 0U;
			}

			// Token: 0x06002181 RID: 8577 RVA: 0x000D3E64 File Offset: 0x000D2064
			public static bool DblToRgbFast(double dbl, byte[] mantissa, out int exponent, out int mantissaSize)
			{
				int num = 0;
				uint num2 = XPathConvert.DblHi(dbl);
				uint num3 = XPathConvert.DblLo(dbl);
				int num4 = (int)(num2 >> 20 & 2047U);
				int num7;
				XPathConvert.BigNumber bigNumber2;
				XPathConvert.BigNumber bigNumber3;
				if (num4 > 0)
				{
					if (num4 >= 1023 && num4 <= 1075 && dbl == Math.Floor(dbl))
					{
						double num5 = dbl;
						int num6 = 0;
						if (num5 >= XPathConvert.C10toN[num6 + 8])
						{
							num6 += 8;
						}
						if (num5 >= XPathConvert.C10toN[num6 + 4])
						{
							num6 += 4;
						}
						if (num5 >= XPathConvert.C10toN[num6 + 2])
						{
							num6 += 2;
						}
						if (num5 >= XPathConvert.C10toN[num6 + 1])
						{
							num6++;
						}
						exponent = num6 + 1;
						num7 = 0;
						while (0.0 != num5)
						{
							byte b = (byte)(num5 / XPathConvert.C10toN[num6]);
							num5 -= (double)b * XPathConvert.C10toN[num6];
							mantissa[num7++] = b;
							num6--;
						}
						mantissaSize = num7;
						return true;
					}
					XPathConvert.BigNumber bigNumber;
					bigNumber.u2 = (2147483648U | (num2 & 16777215U) << 11 | num3 >> 21);
					bigNumber.u1 = num3 << 11;
					bigNumber.u0 = 0U;
					bigNumber.exp = num4 - 1022;
					bigNumber.error = 0U;
					bigNumber2 = bigNumber;
					bigNumber2.u1 |= 1024U;
					bigNumber3 = bigNumber;
					uint num8;
					if (2147483648U == bigNumber3.u2 && bigNumber3.u1 == 0U)
					{
						num8 = 4294966784U;
					}
					else
					{
						num8 = 4294966272U;
					}
					if (XPathConvert.AddU(ref bigNumber3.u1, num8) == 0U)
					{
						XPathConvert.AddU(ref bigNumber3.u2, uint.MaxValue);
						if ((2147483648U & bigNumber3.u2) == 0U)
						{
							bigNumber3.Normalize();
						}
					}
				}
				else
				{
					XPathConvert.BigNumber bigNumber;
					bigNumber.u2 = (num2 & 1048575U);
					bigNumber.u1 = num3;
					bigNumber.u0 = 0U;
					bigNumber.exp = -1010;
					bigNumber.error = 0U;
					bigNumber2 = bigNumber;
					bigNumber2.u0 = 2147483648U;
					bigNumber3 = bigNumber2;
					if (XPathConvert.AddU(ref bigNumber3.u1, 4294967295U) == 0U)
					{
						XPathConvert.AddU(ref bigNumber3.u2, uint.MaxValue);
					}
					bigNumber.Normalize();
					bigNumber2.Normalize();
					bigNumber3.Normalize();
				}
				if (bigNumber2.exp >= 32)
				{
					int num6 = (bigNumber2.exp - 25) * 15 / -XPathConvert.BigNumber.TenPowersNeg[45].exp;
					if (num6 > 0)
					{
						XPathConvert.BigNumber bigNumber4 = XPathConvert.BigNumber.TenPowersNeg[30 + num6];
						bigNumber2.Mul(ref bigNumber4);
						bigNumber3.Mul(ref bigNumber4);
						num += num6 * 32;
					}
					if (bigNumber2.exp >= 32)
					{
						num6 = (bigNumber2.exp - 25) * 32 / -XPathConvert.BigNumber.TenPowersNeg[31].exp;
						XPathConvert.BigNumber bigNumber4 = XPathConvert.BigNumber.TenPowersNeg[num6 - 1];
						bigNumber2.Mul(ref bigNumber4);
						bigNumber3.Mul(ref bigNumber4);
						num += num6;
					}
				}
				else if (bigNumber2.exp < 1)
				{
					int num6 = (25 - bigNumber2.exp) * 15 / XPathConvert.BigNumber.TenPowersPos[45].exp;
					if (num6 > 0)
					{
						XPathConvert.BigNumber bigNumber4 = XPathConvert.BigNumber.TenPowersPos[30 + num6];
						bigNumber2.Mul(ref bigNumber4);
						bigNumber3.Mul(ref bigNumber4);
						num -= num6 * 32;
					}
					if (bigNumber2.exp < 1)
					{
						num6 = (25 - bigNumber2.exp) * 32 / XPathConvert.BigNumber.TenPowersPos[31].exp;
						XPathConvert.BigNumber bigNumber4 = XPathConvert.BigNumber.TenPowersPos[num6 - 1];
						bigNumber2.Mul(ref bigNumber4);
						bigNumber3.Mul(ref bigNumber4);
						num -= num6;
					}
				}
				XPathConvert.BigNumber bigNumber5 = bigNumber2;
				bigNumber2.MakeUpperBound();
				bigNumber5.MakeLowerBound();
				uint num9 = bigNumber2.UMod1();
				uint num10 = bigNumber5.UMod1();
				XPathConvert.BigNumber bigNumber6 = bigNumber3;
				bigNumber6.MakeUpperBound();
				bigNumber3.MakeLowerBound();
				uint num11 = bigNumber6.UMod1();
				uint num12 = bigNumber3.UMod1();
				uint num13 = 1U;
				if (num9 >= 100000000U)
				{
					num13 = 100000000U;
					num += 8;
				}
				else
				{
					if (num9 >= 10000U)
					{
						num13 = 10000U;
						num += 4;
					}
					if (num9 >= 100U * num13)
					{
						num13 *= 100U;
						num += 2;
					}
				}
				if (num9 >= 10U * num13)
				{
					num13 *= 10U;
					num++;
				}
				num++;
				num7 = 0;
				for (;;)
				{
					byte b = (byte)(num9 / num13);
					num9 %= num13;
					byte b2 = (byte)(num12 / num13);
					num12 %= num13;
					if (b != b2)
					{
						break;
					}
					mantissa[num7++] = b;
					if (1U == num13)
					{
						num13 = 10000000U;
						bigNumber2.Mul(ref XPathConvert.BigNumber.TenPowersPos[7]);
						bigNumber2.MakeUpperBound();
						num9 = bigNumber2.UMod1();
						if (num9 >= 100000000U)
						{
							goto IL_5AB;
						}
						bigNumber5.Mul(ref XPathConvert.BigNumber.TenPowersPos[7]);
						bigNumber5.MakeLowerBound();
						num10 = bigNumber5.UMod1();
						bigNumber6.Mul(ref XPathConvert.BigNumber.TenPowersPos[7]);
						bigNumber6.MakeUpperBound();
						num11 = bigNumber6.UMod1();
						bigNumber3.Mul(ref XPathConvert.BigNumber.TenPowersPos[7]);
						bigNumber3.MakeLowerBound();
						num12 = bigNumber3.UMod1();
					}
					else
					{
						num13 /= 10U;
					}
				}
				byte b3 = (byte)(num11 / num13 % 10U);
				num11 %= num13;
				byte b4 = (byte)(num10 / num13 % 10U);
				num10 %= num13;
				if (b3 < b4)
				{
					if (b3 != 0 || num11 != 0U || !bigNumber6.IsZero || (num3 & 1U) != 0U)
					{
						if (b4 - b3 > 1)
						{
							mantissa[num7++] = (b4 + b3 + 1) / 2;
						}
						else
						{
							if (num10 == 0U && bigNumber5.IsZero && (num3 & 1U) != 0U)
							{
								goto IL_5AB;
							}
							mantissa[num7++] = b4;
						}
					}
					exponent = num;
					mantissaSize = num7;
					return true;
				}
				IL_5AB:
				exponent = (mantissaSize = 0);
				return false;
			}

			// Token: 0x06002182 RID: 8578 RVA: 0x000D4428 File Offset: 0x000D2628
			public static void DblToRgbPrecise(double dbl, byte[] mantissa, out int exponent, out int mantissaSize)
			{
				exponent = 0;
				mantissaSize = 0;
				XPathConvert.BigInteger bigInteger = new XPathConvert.BigInteger();
				XPathConvert.BigInteger bigInteger2 = new XPathConvert.BigInteger();
				XPathConvert.BigInteger bigInteger3 = new XPathConvert.BigInteger();
				XPathConvert.BigInteger bigInteger4 = new XPathConvert.BigInteger();
				XPathConvert.BigInteger bigInteger5 = new XPathConvert.BigInteger();
				uint num = XPathConvert.DblHi(dbl);
				uint num2 = XPathConvert.DblLo(dbl);
				bigInteger2.InitFromDigits(1U, 0U, 1);
				bigInteger3.InitFromDigits(1U, 0U, 1);
				int num3 = (int)(((num & 2146435072U) >> 20) - 1075U);
				uint num4 = num & 1048575U;
				uint num5 = num2;
				int num6 = 2;
				bool flag = false;
				double num7;
				int num8;
				if (num3 == -1075)
				{
					if (num4 == 0U)
					{
						num6 = 1;
					}
					num7 = BitConverter.Int64BitsToDouble(5760103923406864384L);
					num7 *= dbl;
					num8 = (int)(((XPathConvert.DblHi(num7) & 2146435072U) >> 20) - 1279U);
					num = XPathConvert.DblHi(num7);
					num &= 1048575U;
					num |= 1072693248U;
					num7 = BitConverter.Int64BitsToDouble((long)((ulong)num << 32 | (ulong)XPathConvert.DblLo(num7)));
					num3++;
				}
				else
				{
					num &= 1048575U;
					num |= 1072693248U;
					num7 = BitConverter.Int64BitsToDouble((long)((ulong)num << 32 | (ulong)num2));
					num8 = num3 + 52;
					if (num5 == 0U && num4 == 0U && num3 > -1074)
					{
						num4 = 2097152U;
						num3--;
						flag = true;
					}
					else
					{
						num4 |= 1048576U;
					}
				}
				num7 = (num7 - 1.5) * 0.289529654602168 + 0.1760912590558 + (double)num8 * 0.301029995663981;
				int num9 = (int)num7;
				if (num7 < 0.0 && num7 != (double)num9)
				{
					num9--;
				}
				int num10;
				int num11;
				if (num3 >= 0)
				{
					num10 = num3;
					num11 = 0;
				}
				else
				{
					num10 = 0;
					num11 = -num3;
				}
				int num12;
				int num13;
				if (num9 >= 0)
				{
					num12 = 0;
					num13 = num9;
					num11 += num9;
				}
				else
				{
					num10 -= num9;
					num12 = -num9;
					num13 = 0;
				}
				if (num10 > 0 && num11 > 0)
				{
					num8 = ((num10 < num11) ? num10 : num11);
					num10 -= num8;
					num11 -= num8;
				}
				num10++;
				num11++;
				if (num12 > 0)
				{
					bigInteger3.MulPow5(num12);
					bigInteger.InitFromBigint(bigInteger3);
					if (1 == num6)
					{
						bigInteger.MulAdd(num5, 0U);
					}
					else
					{
						bigInteger.MulAdd(num4, 0U);
						bigInteger.ShiftLeft(32);
						if (num5 != 0U)
						{
							bigInteger5.InitFromBigint(bigInteger3);
							bigInteger5.MulAdd(num5, 0U);
							bigInteger.Add(bigInteger5);
						}
					}
				}
				else
				{
					bigInteger.InitFromDigits(num5, num4, num6);
					if (num13 > 0)
					{
						bigInteger2.MulPow5(num13);
					}
				}
				num8 = XPathConvert.CbitZeroLeft(bigInteger2[bigInteger2.Length - 1]);
				num8 = (num8 + 28 - num11 & 31);
				num10 += num8;
				num11 += num8;
				bigInteger.ShiftLeft(num10);
				if (num10 > 1)
				{
					bigInteger3.ShiftLeft(num10 - 1);
				}
				bigInteger2.ShiftLeft(num11);
				XPathConvert.BigInteger bigInteger6;
				if (flag)
				{
					bigInteger6 = bigInteger4;
					bigInteger6.InitFromBigint(bigInteger3);
					bigInteger3.ShiftLeft(1);
				}
				else
				{
					bigInteger6 = bigInteger3;
				}
				int i = 0;
				byte b;
				int num14;
				for (;;)
				{
					b = (byte)bigInteger.DivRem(bigInteger2);
					if (i == 0 && b == 0)
					{
						num9--;
					}
					else
					{
						num8 = bigInteger.CompareTo(bigInteger6);
						if (bigInteger2.CompareTo(bigInteger3) < 0)
						{
							num14 = 1;
						}
						else
						{
							bigInteger5.InitFromBigint(bigInteger2);
							bigInteger5.Subtract(bigInteger3);
							num14 = bigInteger.CompareTo(bigInteger5);
						}
						if (num14 == 0 && (num2 & 1U) == 0U)
						{
							goto Block_23;
						}
						if (num8 < 0 || (num8 == 0 && (num2 & 1U) == 0U))
						{
							goto IL_364;
						}
						if (num14 > 0)
						{
							goto Block_31;
						}
						mantissa[i++] = b;
					}
					bigInteger.MulAdd(10U, 0U);
					bigInteger3.MulAdd(10U, 0U);
					if (bigInteger6 != bigInteger3)
					{
						bigInteger6.MulAdd(10U, 0U);
					}
				}
				IL_418:
				while (i > 0)
				{
					if (mantissa[--i] != 9)
					{
						int num15 = i++;
						mantissa[num15] += 1;
						goto IL_42D;
					}
				}
				num9++;
				mantissa[i++] = 1;
				goto IL_42D;
				Block_23:
				if (b != 9)
				{
					if (num8 > 0)
					{
						b += 1;
					}
					mantissa[i++] = b;
					goto IL_42D;
				}
				goto IL_418;
				IL_364:
				if (num14 > 0)
				{
					bigInteger.ShiftLeft(1);
					num14 = bigInteger.CompareTo(bigInteger2);
					if (num14 > 0 || (num14 == 0 && (b & 1) != 0))
					{
						byte b2 = b;
						b = b2 + 1;
						if (b2 == 9)
						{
							goto IL_418;
						}
					}
				}
				mantissa[i++] = b;
				goto IL_42D;
				Block_31:
				if (b == 9)
				{
					goto IL_418;
				}
				mantissa[i++] = b + 1;
				IL_42D:
				exponent = num9 + 1;
				mantissaSize = i;
			}

			// Token: 0x06002183 RID: 8579 RVA: 0x000D486C File Offset: 0x000D2A6C
			// Note: this type is marked as 'beforefieldinit'.
			static BigNumber()
			{
			}

			// Token: 0x04001B9F RID: 7071
			private uint u0;

			// Token: 0x04001BA0 RID: 7072
			private uint u1;

			// Token: 0x04001BA1 RID: 7073
			private uint u2;

			// Token: 0x04001BA2 RID: 7074
			private int exp;

			// Token: 0x04001BA3 RID: 7075
			private uint error;

			// Token: 0x04001BA4 RID: 7076
			private static readonly XPathConvert.BigNumber[] TenPowersPos = new XPathConvert.BigNumber[]
			{
				new XPathConvert.BigNumber(0U, 0U, 2684354560U, 4, 0U),
				new XPathConvert.BigNumber(0U, 0U, 3355443200U, 7, 0U),
				new XPathConvert.BigNumber(0U, 0U, 4194304000U, 10, 0U),
				new XPathConvert.BigNumber(0U, 0U, 2621440000U, 14, 0U),
				new XPathConvert.BigNumber(0U, 0U, 3276800000U, 17, 0U),
				new XPathConvert.BigNumber(0U, 0U, 4096000000U, 20, 0U),
				new XPathConvert.BigNumber(0U, 0U, 2560000000U, 24, 0U),
				new XPathConvert.BigNumber(0U, 0U, 3200000000U, 27, 0U),
				new XPathConvert.BigNumber(0U, 0U, 4000000000U, 30, 0U),
				new XPathConvert.BigNumber(0U, 0U, 2500000000U, 34, 0U),
				new XPathConvert.BigNumber(0U, 0U, 3125000000U, 37, 0U),
				new XPathConvert.BigNumber(0U, 0U, 3906250000U, 40, 0U),
				new XPathConvert.BigNumber(0U, 0U, 2441406250U, 44, 0U),
				new XPathConvert.BigNumber(0U, 2147483648U, 3051757812U, 47, 0U),
				new XPathConvert.BigNumber(0U, 2684354560U, 3814697265U, 50, 0U),
				new XPathConvert.BigNumber(0U, 67108864U, 2384185791U, 54, 0U),
				new XPathConvert.BigNumber(0U, 3305111552U, 2980232238U, 57, 0U),
				new XPathConvert.BigNumber(0U, 1983905792U, 3725290298U, 60, 0U),
				new XPathConvert.BigNumber(0U, 2313682944U, 2328306436U, 64, 0U),
				new XPathConvert.BigNumber(0U, 2892103680U, 2910383045U, 67, 0U),
				new XPathConvert.BigNumber(0U, 393904128U, 3637978807U, 70, 0U),
				new XPathConvert.BigNumber(0U, 1856802816U, 2273736754U, 74, 0U),
				new XPathConvert.BigNumber(0U, 173519872U, 2842170943U, 77, 0U),
				new XPathConvert.BigNumber(0U, 3438125312U, 3552713678U, 80, 0U),
				new XPathConvert.BigNumber(0U, 1075086496U, 2220446049U, 84, 0U),
				new XPathConvert.BigNumber(0U, 2417599944U, 2775557561U, 87, 0U),
				new XPathConvert.BigNumber(0U, 4095741754U, 3469446951U, 90, 0U),
				new XPathConvert.BigNumber(1073741824U, 4170451332U, 2168404344U, 94, 0U),
				new XPathConvert.BigNumber(1342177280U, 918096869U, 2710505431U, 97, 0U),
				new XPathConvert.BigNumber(2751463424U, 73879262U, 3388131789U, 100, 0U),
				new XPathConvert.BigNumber(1291845632U, 1166090902U, 4235164736U, 103, 0U),
				new XPathConvert.BigNumber(4028628992U, 728806813U, 2646977960U, 107, 0U),
				new XPathConvert.BigNumber(1019177842U, 4291798741U, 3262652233U, 213, 1U),
				new XPathConvert.BigNumber(3318737231U, 3315274914U, 4021529366U, 319, 1U),
				new XPathConvert.BigNumber(3329176428U, 2162789599U, 2478458825U, 426, 1U),
				new XPathConvert.BigNumber(1467717739U, 2145785770U, 3054936363U, 532, 1U),
				new XPathConvert.BigNumber(2243682900U, 958879082U, 3765499789U, 638, 1U),
				new XPathConvert.BigNumber(2193451889U, 3812411695U, 2320668415U, 745, 1U),
				new XPathConvert.BigNumber(3720056860U, 2650398349U, 2860444667U, 851, 1U),
				new XPathConvert.BigNumber(1937977068U, 1550462860U, 3525770265U, 957, 1U),
				new XPathConvert.BigNumber(3869316483U, 4073513845U, 2172923689U, 1064, 1U),
				new XPathConvert.BigNumber(1589582007U, 3683650258U, 2678335232U, 1170, 1U),
				new XPathConvert.BigNumber(271056885U, 2935532055U, 3301303056U, 1276, 1U),
				new XPathConvert.BigNumber(3051704177U, 3920665688U, 4069170183U, 1382, 1U),
				new XPathConvert.BigNumber(2817170568U, 3958895571U, 2507819745U, 1489, 1U),
				new XPathConvert.BigNumber(2113145460U, 127246946U, 3091126492U, 1595, 1U)
			};

			// Token: 0x04001BA5 RID: 7077
			private static readonly XPathConvert.BigNumber[] TenPowersNeg = new XPathConvert.BigNumber[]
			{
				new XPathConvert.BigNumber(3435973837U, 3435973836U, 3435973836U, -3, 1U),
				new XPathConvert.BigNumber(1030792151U, 1889785610U, 2748779069U, -6, 1U),
				new XPathConvert.BigNumber(1683627180U, 2370821947U, 2199023255U, -9, 1U),
				new XPathConvert.BigNumber(3552796947U, 3793315115U, 3518437208U, -13, 1U),
				new XPathConvert.BigNumber(265257180U, 457671715U, 2814749767U, -16, 1U),
				new XPathConvert.BigNumber(2789186122U, 2943117749U, 2251799813U, -19, 1U),
				new XPathConvert.BigNumber(1026723958U, 3849994940U, 3602879701U, -23, 1U),
				new XPathConvert.BigNumber(4257353003U, 2221002492U, 2882303761U, -26, 1U),
				new XPathConvert.BigNumber(828902025U, 917808535U, 2305843009U, -29, 1U),
				new XPathConvert.BigNumber(3044230158U, 3186480574U, 3689348814U, -33, 1U),
				new XPathConvert.BigNumber(4153371045U, 3408177918U, 2951479051U, -36, 1U),
				new XPathConvert.BigNumber(4181690295U, 1867548875U, 2361183241U, -39, 1U),
				new XPathConvert.BigNumber(677750258U, 1270091283U, 3777893186U, -43, 1U),
				new XPathConvert.BigNumber(1401193666U, 157079567U, 3022314549U, -46, 1U),
				new XPathConvert.BigNumber(261961473U, 984657113U, 2417851639U, -49, 1U),
				new XPathConvert.BigNumber(1278131816U, 3293438299U, 3868562622U, -53, 1U),
				new XPathConvert.BigNumber(163511994U, 916763721U, 3094850098U, -56, 1U),
				new XPathConvert.BigNumber(989803054U, 2451397895U, 2475880078U, -59, 1U),
				new XPathConvert.BigNumber(724691428U, 3063243173U, 3961408125U, -63, 1U),
				new XPathConvert.BigNumber(2297740061U, 2450594538U, 3169126500U, -66, 1U),
				new XPathConvert.BigNumber(3556178967U, 1960475630U, 2535301200U, -69, 1U),
				new XPathConvert.BigNumber(1394919051U, 3136761009U, 4056481920U, -73, 1U),
				new XPathConvert.BigNumber(1974928700U, 2509408807U, 3245185536U, -76, 1U),
				new XPathConvert.BigNumber(3297929878U, 1148533586U, 2596148429U, -79, 1U),
				new XPathConvert.BigNumber(981720510U, 3555640657U, 4153837486U, -83, 1U),
				new XPathConvert.BigNumber(2503363326U, 1985519066U, 3323069989U, -86, 1U),
				new XPathConvert.BigNumber(2002690661U, 2447408712U, 2658455991U, -89, 1U),
				new XPathConvert.BigNumber(2345311598U, 2197867021U, 4253529586U, -93, 1U),
				new XPathConvert.BigNumber(158262360U, 899300158U, 3402823669U, -96, 1U),
				new XPathConvert.BigNumber(2703590266U, 1578433585U, 2722258935U, -99, 1U),
				new XPathConvert.BigNumber(2162872213U, 1262746868U, 2177807148U, -102, 1U),
				new XPathConvert.BigNumber(1742608622U, 1161401530U, 3484491437U, -106, 1U),
				new XPathConvert.BigNumber(1059297495U, 2772036005U, 2826955303U, -212, 1U),
				new XPathConvert.BigNumber(299617026U, 4252324763U, 2293498615U, -318, 1U),
				new XPathConvert.BigNumber(2893853687U, 1690100896U, 3721414268U, -425, 1U),
				new XPathConvert.BigNumber(1508712807U, 3681788051U, 3019169939U, -531, 1U),
				new XPathConvert.BigNumber(2070087331U, 1411632134U, 2449441655U, -637, 1U),
				new XPathConvert.BigNumber(2767765334U, 1244745405U, 3974446316U, -744, 1U),
				new XPathConvert.BigNumber(4203811158U, 1668946233U, 3224453925U, -850, 1U),
				new XPathConvert.BigNumber(1323526137U, 2204812663U, 2615987810U, -956, 1U),
				new XPathConvert.BigNumber(2300620953U, 1199716560U, 4244682903U, -1063, 1U),
				new XPathConvert.BigNumber(9598332U, 1190350717U, 3443695891U, -1169, 1U),
				new XPathConvert.BigNumber(2296094720U, 2971338839U, 2793858024U, -1275, 1U),
				new XPathConvert.BigNumber(441364487U, 1073506470U, 2266646913U, -1381, 1U),
				new XPathConvert.BigNumber(2227594191U, 3053929028U, 3677844889U, -1488, 1U),
				new XPathConvert.BigNumber(1642812130U, 2030073654U, 2983822260U, -1594, 1U)
			};
		}

		// Token: 0x02000330 RID: 816
		private class BigInteger : IComparable
		{
			// Token: 0x06002184 RID: 8580 RVA: 0x000D5355 File Offset: 0x000D3555
			public BigInteger()
			{
				this.capacity = 30;
				this.length = 0;
				this.digits = new uint[30];
			}

			// Token: 0x17000688 RID: 1672
			// (get) Token: 0x06002185 RID: 8581 RVA: 0x000D5379 File Offset: 0x000D3579
			public int Length
			{
				get
				{
					return this.length;
				}
			}

			// Token: 0x17000689 RID: 1673
			public uint this[int idx]
			{
				get
				{
					return this.digits[idx];
				}
			}

			// Token: 0x06002187 RID: 8583 RVA: 0x0000B528 File Offset: 0x00009728
			[Conditional("DEBUG")]
			private void AssertValidNoVal()
			{
			}

			// Token: 0x06002188 RID: 8584 RVA: 0x0000B528 File Offset: 0x00009728
			[Conditional("DEBUG")]
			private void AssertValid()
			{
			}

			// Token: 0x06002189 RID: 8585 RVA: 0x000D538C File Offset: 0x000D358C
			private void Ensure(int cu)
			{
				if (cu <= this.capacity)
				{
					return;
				}
				cu += cu;
				uint[] array = new uint[cu];
				this.digits.CopyTo(array, 0);
				this.digits = array;
				this.capacity = cu;
			}

			// Token: 0x0600218A RID: 8586 RVA: 0x000D53CC File Offset: 0x000D35CC
			public void InitFromRgu(uint[] rgu, int cu)
			{
				this.Ensure(cu);
				this.length = cu;
				for (int i = 0; i < cu; i++)
				{
					this.digits[i] = rgu[i];
				}
			}

			// Token: 0x0600218B RID: 8587 RVA: 0x000D53FE File Offset: 0x000D35FE
			public void InitFromDigits(uint u0, uint u1, int cu)
			{
				this.length = cu;
				this.digits[0] = u0;
				this.digits[1] = u1;
			}

			// Token: 0x0600218C RID: 8588 RVA: 0x000D5419 File Offset: 0x000D3619
			public void InitFromBigint(XPathConvert.BigInteger biSrc)
			{
				this.InitFromRgu(biSrc.digits, biSrc.length);
			}

			// Token: 0x0600218D RID: 8589 RVA: 0x000D5430 File Offset: 0x000D3630
			public void InitFromFloatingDecimal(XPathConvert.FloatingDecimal dec)
			{
				int cu = (dec.MantissaSize + 8) / 9;
				int mantissaSize = dec.MantissaSize;
				this.Ensure(cu);
				this.length = 0;
				uint num = 0U;
				uint num2 = 1U;
				for (int i = 0; i < mantissaSize; i++)
				{
					if (1000000000U == num2)
					{
						this.MulAdd(num2, num);
						num2 = 1U;
						num = 0U;
					}
					num2 *= 10U;
					num = num * 10U + (uint)dec[i];
				}
				this.MulAdd(num2, num);
			}

			// Token: 0x0600218E RID: 8590 RVA: 0x000D54A4 File Offset: 0x000D36A4
			public void MulAdd(uint uMul, uint uAdd)
			{
				for (int i = 0; i < this.length; i++)
				{
					uint num2;
					uint num = XPathConvert.MulU(this.digits[i], uMul, out num2);
					if (uAdd != 0U)
					{
						num2 += XPathConvert.AddU(ref num, uAdd);
					}
					this.digits[i] = num;
					uAdd = num2;
				}
				if (uAdd != 0U)
				{
					this.Ensure(this.length + 1);
					uint[] array = this.digits;
					int num3 = this.length;
					this.length = num3 + 1;
					array[num3] = uAdd;
				}
			}

			// Token: 0x0600218F RID: 8591 RVA: 0x000D5518 File Offset: 0x000D3718
			public void MulPow5(int c5)
			{
				int num = (c5 + 12) / 13;
				if (this.length == 0 || c5 == 0)
				{
					return;
				}
				this.Ensure(this.length + num);
				while (c5 >= 13)
				{
					this.MulAdd(1220703125U, 0U);
					c5 -= 13;
				}
				if (c5 > 0)
				{
					uint num2 = 5U;
					while (--c5 > 0)
					{
						num2 *= 5U;
					}
					this.MulAdd(num2, 0U);
				}
			}

			// Token: 0x06002190 RID: 8592 RVA: 0x000D5580 File Offset: 0x000D3780
			public void ShiftLeft(int cbit)
			{
				if (cbit == 0 || this.length == 0)
				{
					return;
				}
				int num = cbit >> 5;
				cbit &= 31;
				uint num3;
				if (cbit > 0)
				{
					int num2 = this.length - 1;
					num3 = this.digits[num2] >> 32 - cbit;
					for (;;)
					{
						this.digits[num2] <<= cbit;
						if (num2 == 0)
						{
							break;
						}
						this.digits[num2] |= this.digits[num2 - 1] >> 32 - cbit;
						num2--;
					}
				}
				else
				{
					num3 = 0U;
				}
				if (num > 0 || num3 != 0U)
				{
					int num2 = this.length + ((num3 != 0U) ? 1 : 0) + num;
					this.Ensure(num2);
					if (num > 0)
					{
						int num4 = this.length;
						while (num4-- != 0)
						{
							this.digits[num + num4] = this.digits[num4];
						}
						for (int i = 0; i < num; i++)
						{
							this.digits[i] = 0U;
						}
						this.length += num;
					}
					if (num3 != 0U)
					{
						uint[] array = this.digits;
						int num5 = this.length;
						this.length = num5 + 1;
						array[num5] = num3;
					}
				}
			}

			// Token: 0x06002191 RID: 8593 RVA: 0x000D5694 File Offset: 0x000D3894
			public void ShiftUsRight(int cu)
			{
				if (cu >= this.length)
				{
					this.length = 0;
					return;
				}
				if (cu > 0)
				{
					for (int i = 0; i < this.length - cu; i++)
					{
						this.digits[i] = this.digits[cu + i];
					}
					this.length -= cu;
				}
			}

			// Token: 0x06002192 RID: 8594 RVA: 0x000D56EC File Offset: 0x000D38EC
			public void ShiftRight(int cbit)
			{
				int num = cbit >> 5;
				cbit &= 31;
				if (num > 0)
				{
					this.ShiftUsRight(num);
				}
				if (cbit == 0 || this.length == 0)
				{
					return;
				}
				int num2 = 0;
				for (;;)
				{
					this.digits[num2] >>= cbit;
					if (++num2 >= this.length)
					{
						break;
					}
					this.digits[num2 - 1] |= this.digits[num2] << 32 - cbit;
				}
				if (this.digits[num2 - 1] == 0U)
				{
					this.length--;
					return;
				}
			}

			// Token: 0x06002193 RID: 8595 RVA: 0x000D577C File Offset: 0x000D397C
			public int CompareTo(object obj)
			{
				XPathConvert.BigInteger bigInteger = (XPathConvert.BigInteger)obj;
				if (this.length > bigInteger.length)
				{
					return 1;
				}
				if (this.length < bigInteger.length)
				{
					return -1;
				}
				if (this.length == 0)
				{
					return 0;
				}
				int num = this.length - 1;
				while (this.digits[num] == bigInteger.digits[num])
				{
					if (num == 0)
					{
						return 0;
					}
					num--;
				}
				if (this.digits[num] <= bigInteger.digits[num])
				{
					return -1;
				}
				return 1;
			}

			// Token: 0x06002194 RID: 8596 RVA: 0x000D57F8 File Offset: 0x000D39F8
			public void Add(XPathConvert.BigInteger bi)
			{
				int num;
				int num2;
				if ((num = this.length) < (num2 = bi.length))
				{
					num = bi.length;
					num2 = this.length;
					this.Ensure(num + 1);
				}
				uint num3 = 0U;
				int i;
				for (i = 0; i < num2; i++)
				{
					if (num3 != 0U)
					{
						num3 = XPathConvert.AddU(ref this.digits[i], num3);
					}
					num3 += XPathConvert.AddU(ref this.digits[i], bi.digits[i]);
				}
				if (this.length < bi.length)
				{
					while (i < num)
					{
						this.digits[i] = bi.digits[i];
						if (num3 != 0U)
						{
							num3 = XPathConvert.AddU(ref this.digits[i], num3);
						}
						i++;
					}
					this.length = num;
				}
				else
				{
					while (num3 != 0U && i < num)
					{
						num3 = XPathConvert.AddU(ref this.digits[i], num3);
						i++;
					}
				}
				if (num3 != 0U)
				{
					this.Ensure(this.length + 1);
					uint[] array = this.digits;
					int num4 = this.length;
					this.length = num4 + 1;
					array[num4] = num3;
				}
			}

			// Token: 0x06002195 RID: 8597 RVA: 0x000D5900 File Offset: 0x000D3B00
			public void Subtract(XPathConvert.BigInteger bi)
			{
				if (this.length >= bi.length)
				{
					uint num = 1U;
					int i;
					for (i = 0; i < bi.length; i++)
					{
						uint num2 = bi.digits[i];
						if (num2 != 0U || num == 0U)
						{
							num = XPathConvert.AddU(ref this.digits[i], ~num2 + num);
						}
					}
					while (num == 0U && i < this.length)
					{
						num = XPathConvert.AddU(ref this.digits[i], uint.MaxValue);
					}
					if (num != 0U)
					{
						if (i == this.length)
						{
							while (--i >= 0 && this.digits[i] == 0U)
							{
							}
							this.length = i + 1;
						}
						return;
					}
				}
				this.length = 0;
			}

			// Token: 0x06002196 RID: 8598 RVA: 0x000D59A8 File Offset: 0x000D3BA8
			public uint DivRem(XPathConvert.BigInteger bi)
			{
				int num = bi.length;
				if (this.length < num)
				{
					return 0U;
				}
				uint num2 = this.digits[num - 1] / (bi.digits[num - 1] + 1U);
				if (num2 != 0U)
				{
					if (num2 == 1U)
					{
						this.Subtract(bi);
					}
					else
					{
						uint u = 0U;
						uint num3 = 1U;
						int i;
						for (i = 0; i < num; i++)
						{
							uint num5;
							uint num4 = XPathConvert.MulU(num2, bi.digits[i], out num5);
							u = num5 + XPathConvert.AddU(ref num4, u);
							if (num4 != 0U || num3 == 0U)
							{
								num3 = XPathConvert.AddU(ref this.digits[i], ~num4 + num3);
							}
						}
						while (--i >= 0 && this.digits[i] == 0U)
						{
						}
						this.length = i + 1;
					}
				}
				int num6;
				if (num2 < 9U && (num6 = this.CompareTo(bi)) >= 0)
				{
					num2 += 1U;
					if (num6 == 0)
					{
						this.length = 0;
					}
					else
					{
						this.Subtract(bi);
					}
				}
				return num2;
			}

			// Token: 0x04001BA6 RID: 7078
			private const int InitCapacity = 30;

			// Token: 0x04001BA7 RID: 7079
			private int capacity;

			// Token: 0x04001BA8 RID: 7080
			private int length;

			// Token: 0x04001BA9 RID: 7081
			private uint[] digits;
		}

		// Token: 0x02000331 RID: 817
		private class FloatingDecimal
		{
			// Token: 0x1700068A RID: 1674
			// (get) Token: 0x06002197 RID: 8599 RVA: 0x000D5A84 File Offset: 0x000D3C84
			// (set) Token: 0x06002198 RID: 8600 RVA: 0x000D5A8C File Offset: 0x000D3C8C
			public int Exponent
			{
				get
				{
					return this.exponent;
				}
				set
				{
					this.exponent = value;
				}
			}

			// Token: 0x1700068B RID: 1675
			// (get) Token: 0x06002199 RID: 8601 RVA: 0x000D5A95 File Offset: 0x000D3C95
			// (set) Token: 0x0600219A RID: 8602 RVA: 0x000D5A9D File Offset: 0x000D3C9D
			public int Sign
			{
				get
				{
					return this.sign;
				}
				set
				{
					this.sign = value;
				}
			}

			// Token: 0x1700068C RID: 1676
			// (get) Token: 0x0600219B RID: 8603 RVA: 0x000D5AA6 File Offset: 0x000D3CA6
			public byte[] Mantissa
			{
				get
				{
					return this.mantissa;
				}
			}

			// Token: 0x1700068D RID: 1677
			// (get) Token: 0x0600219C RID: 8604 RVA: 0x000D5AAE File Offset: 0x000D3CAE
			// (set) Token: 0x0600219D RID: 8605 RVA: 0x000D5AB6 File Offset: 0x000D3CB6
			public int MantissaSize
			{
				get
				{
					return this.mantissaSize;
				}
				set
				{
					this.mantissaSize = value;
				}
			}

			// Token: 0x1700068E RID: 1678
			public byte this[int ib]
			{
				get
				{
					return this.mantissa[ib];
				}
			}

			// Token: 0x0600219F RID: 8607 RVA: 0x000D5AC9 File Offset: 0x000D3CC9
			public FloatingDecimal()
			{
				this.exponent = 0;
				this.sign = 1;
				this.mantissaSize = 0;
			}

			// Token: 0x060021A0 RID: 8608 RVA: 0x000D5AF3 File Offset: 0x000D3CF3
			public FloatingDecimal(double dbl)
			{
				this.InitFromDouble(dbl);
			}

			// Token: 0x060021A1 RID: 8609 RVA: 0x000D5B10 File Offset: 0x000D3D10
			public static explicit operator double(XPathConvert.FloatingDecimal dec)
			{
				int num = dec.mantissaSize;
				int num2 = dec.exponent - num;
				double num4;
				if (num <= 15 && num2 >= -22 && dec.exponent <= 37)
				{
					if (num <= 9)
					{
						uint num3 = 0U;
						for (int i = 0; i < num; i++)
						{
							num3 = num3 * 10U + (uint)dec[i];
						}
						num4 = num3;
					}
					else
					{
						num4 = 0.0;
						for (int j = 0; j < num; j++)
						{
							num4 = num4 * 10.0 + (double)dec[j];
						}
					}
					if (num2 > 0)
					{
						if (num2 > 22)
						{
							num4 *= XPathConvert.C10toN[num2 - 22];
							num4 *= XPathConvert.C10toN[22];
						}
						else
						{
							num4 *= XPathConvert.C10toN[num2];
						}
					}
					else if (num2 < 0)
					{
						num4 /= XPathConvert.C10toN[-num2];
					}
				}
				else if (dec.exponent >= 310)
				{
					num4 = double.PositiveInfinity;
				}
				else if (dec.exponent <= -325)
				{
					num4 = 0.0;
				}
				else
				{
					XPathConvert.BigNumber bigNumber = new XPathConvert.BigNumber(dec);
					if (bigNumber.Error == 0U)
					{
						num4 = (double)bigNumber;
					}
					else
					{
						XPathConvert.BigNumber bn = bigNumber;
						bn.MakeUpperBound();
						XPathConvert.BigNumber bn2 = bigNumber;
						bn2.MakeLowerBound();
						num4 = (double)bn;
						double num5 = (double)bn2;
						if (num4 != num5)
						{
							num4 = dec.AdjustDbl((double)bigNumber);
						}
					}
				}
				if (dec.sign >= 0)
				{
					return num4;
				}
				return -num4;
			}

			// Token: 0x060021A2 RID: 8610 RVA: 0x000D5CA4 File Offset: 0x000D3EA4
			private double AdjustDbl(double dbl)
			{
				XPathConvert.BigInteger bigInteger = new XPathConvert.BigInteger();
				XPathConvert.BigInteger bigInteger2 = new XPathConvert.BigInteger();
				bigInteger.InitFromFloatingDecimal(this);
				int num = this.exponent - this.mantissaSize;
				int num3;
				int num2;
				int num5;
				int num4;
				if (num >= 0)
				{
					num2 = (num3 = num);
					num4 = (num5 = 0);
				}
				else
				{
					num2 = (num3 = 0);
					num4 = (num5 = -num);
				}
				uint num6 = XPathConvert.DblHi(dbl);
				uint num7 = XPathConvert.DblLo(dbl);
				int num8 = (int)(num6 >> 20 & 2047U);
				num6 &= 1048575U;
				uint u = 1U;
				if (num8 != 0)
				{
					if (num6 == 0U && num7 == 0U && 1 != num8)
					{
						u = 2U;
						num6 = 2097152U;
						num8--;
					}
					else
					{
						num6 |= 1048576U;
					}
					num8 -= 1076;
				}
				else
				{
					num8 = -1075;
				}
				num6 = (num6 << 1 | num7 >> 31);
				num7 <<= 1;
				int cu;
				if (num7 == 0U && num6 == 0U)
				{
					cu = 0;
				}
				else if (num6 == 0U)
				{
					cu = 1;
				}
				else
				{
					cu = 2;
				}
				bigInteger2.InitFromDigits(num7, num6, cu);
				if (num8 >= 0)
				{
					num4 += num8;
				}
				else
				{
					num2 += -num8;
				}
				if (num4 > num2)
				{
					num4 -= num2;
					num2 = 0;
					int num9 = 0;
					while (num4 >= 32 && bigInteger[num9] == 0U)
					{
						num4 -= 32;
						num9++;
					}
					if (num9 > 0)
					{
						bigInteger.ShiftUsRight(num9);
					}
					uint num10 = bigInteger[0];
					num9 = 0;
					while (num9 < num4 && ((ulong)num10 & 1UL << num9) == 0UL)
					{
						num9++;
					}
					if (num9 > 0)
					{
						num4 -= num9;
						bigInteger.ShiftRight(num9);
					}
				}
				else
				{
					num2 -= num4;
					num4 = 0;
				}
				if (num5 > 0)
				{
					bigInteger2.MulPow5(num5);
				}
				else if (num3 > 0)
				{
					bigInteger.MulPow5(num3);
				}
				if (num4 > 0)
				{
					bigInteger2.ShiftLeft(num4);
				}
				else if (num2 > 0)
				{
					bigInteger.ShiftLeft(num2);
				}
				int num11 = bigInteger2.CompareTo(bigInteger);
				if (num11 == 0)
				{
					return dbl;
				}
				if (num11 > 0)
				{
					if (XPathConvert.AddU(ref num7, 4294967295U) == 0U)
					{
						XPathConvert.AddU(ref num6, uint.MaxValue);
					}
					bigInteger2.InitFromDigits(num7, num6, 1 + ((num6 != 0U) ? 1 : 0));
					if (num5 > 0)
					{
						bigInteger2.MulPow5(num5);
					}
					if (num4 > 0)
					{
						bigInteger2.ShiftLeft(num4);
					}
					num11 = bigInteger2.CompareTo(bigInteger);
					if (num11 > 0 || (num11 == 0 && (XPathConvert.DblLo(dbl) & 1U) != 0U))
					{
						dbl = BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(dbl) - 1L);
					}
				}
				else
				{
					if (XPathConvert.AddU(ref num7, u) != 0U)
					{
						XPathConvert.AddU(ref num6, 1U);
					}
					bigInteger2.InitFromDigits(num7, num6, 1 + ((num6 != 0U) ? 1 : 0));
					if (num5 > 0)
					{
						bigInteger2.MulPow5(num5);
					}
					if (num4 > 0)
					{
						bigInteger2.ShiftLeft(num4);
					}
					num11 = bigInteger2.CompareTo(bigInteger);
					if (num11 < 0 || (num11 == 0 && (XPathConvert.DblLo(dbl) & 1U) != 0U))
					{
						dbl = BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(dbl) + 1L);
					}
				}
				return dbl;
			}

			// Token: 0x060021A3 RID: 8611 RVA: 0x000D5F40 File Offset: 0x000D4140
			private void InitFromDouble(double dbl)
			{
				if (0.0 == dbl || XPathConvert.IsSpecial(dbl))
				{
					this.exponent = 0;
					this.sign = 1;
					this.mantissaSize = 0;
					return;
				}
				if (dbl < 0.0)
				{
					this.sign = -1;
					dbl = -dbl;
				}
				else
				{
					this.sign = 1;
				}
				if (!XPathConvert.BigNumber.DblToRgbFast(dbl, this.mantissa, out this.exponent, out this.mantissaSize))
				{
					XPathConvert.BigNumber.DblToRgbPrecise(dbl, this.mantissa, out this.exponent, out this.mantissaSize);
				}
			}

			// Token: 0x04001BAA RID: 7082
			public const int MaxDigits = 50;

			// Token: 0x04001BAB RID: 7083
			private const int MaxExp10 = 310;

			// Token: 0x04001BAC RID: 7084
			private const int MinExp10 = -325;

			// Token: 0x04001BAD RID: 7085
			private int exponent;

			// Token: 0x04001BAE RID: 7086
			private int sign;

			// Token: 0x04001BAF RID: 7087
			private int mantissaSize;

			// Token: 0x04001BB0 RID: 7088
			private byte[] mantissa = new byte[50];
		}
	}
}
