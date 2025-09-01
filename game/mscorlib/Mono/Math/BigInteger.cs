using System;
using System.Security.Cryptography;
using Mono.Math.Prime;
using Mono.Math.Prime.Generator;

namespace Mono.Math
{
	// Token: 0x02000097 RID: 151
	internal class BigInteger
	{
		// Token: 0x0600038F RID: 911 RVA: 0x000135A2 File Offset: 0x000117A2
		public BigInteger()
		{
			this.data = new uint[20];
			this.length = 20U;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000135C6 File Offset: 0x000117C6
		public BigInteger(BigInteger.Sign sign, uint len)
		{
			this.data = new uint[len];
			this.length = len;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000135E8 File Offset: 0x000117E8
		public BigInteger(BigInteger bi)
		{
			this.data = (uint[])bi.data.Clone();
			this.length = bi.length;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001361C File Offset: 0x0001181C
		public BigInteger(BigInteger bi, uint len)
		{
			this.data = new uint[len];
			for (uint num = 0U; num < bi.length; num += 1U)
			{
				this.data[(int)num] = bi.data[(int)num];
			}
			this.length = bi.length;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00013670 File Offset: 0x00011870
		public BigInteger(byte[] inData)
		{
			if (inData.Length == 0)
			{
				inData = new byte[1];
			}
			this.length = (uint)inData.Length >> 2;
			int num = inData.Length & 3;
			if (num != 0)
			{
				this.length += 1U;
			}
			this.data = new uint[this.length];
			int i = inData.Length - 1;
			int num2 = 0;
			while (i >= 3)
			{
				this.data[num2] = (uint)((int)inData[i - 3] << 24 | (int)inData[i - 2] << 16 | (int)inData[i - 1] << 8 | (int)inData[i]);
				i -= 4;
				num2++;
			}
			switch (num)
			{
			case 1:
				this.data[(int)(this.length - 1U)] = (uint)inData[0];
				break;
			case 2:
				this.data[(int)(this.length - 1U)] = (uint)((int)inData[0] << 8 | (int)inData[1]);
				break;
			case 3:
				this.data[(int)(this.length - 1U)] = (uint)((int)inData[0] << 16 | (int)inData[1] << 8 | (int)inData[2]);
				break;
			}
			this.Normalize();
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00013770 File Offset: 0x00011970
		public BigInteger(uint[] inData)
		{
			if (inData.Length == 0)
			{
				inData = new uint[1];
			}
			this.length = (uint)inData.Length;
			this.data = new uint[this.length];
			int i = (int)(this.length - 1U);
			int num = 0;
			while (i >= 0)
			{
				this.data[num] = inData[i];
				i--;
				num++;
			}
			this.Normalize();
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000137DA File Offset: 0x000119DA
		public BigInteger(uint ui)
		{
			this.data = new uint[]
			{
				ui
			};
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000137F9 File Offset: 0x000119F9
		public BigInteger(ulong ul)
		{
			this.data = new uint[]
			{
				(uint)ul,
				(uint)(ul >> 32)
			};
			this.length = 2U;
			this.Normalize();
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001382E File Offset: 0x00011A2E
		public static implicit operator BigInteger(uint value)
		{
			return new BigInteger(value);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00013836 File Offset: 0x00011A36
		public static implicit operator BigInteger(int value)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			return new BigInteger((uint)value);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0001384D File Offset: 0x00011A4D
		public static implicit operator BigInteger(ulong value)
		{
			return new BigInteger(value);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00013858 File Offset: 0x00011A58
		public static BigInteger Parse(string number)
		{
			if (number == null)
			{
				throw new ArgumentNullException("number");
			}
			int i = 0;
			int num = number.Length;
			bool flag = false;
			BigInteger bigInteger = new BigInteger(0U);
			if (number[i] == '+')
			{
				i++;
			}
			else if (number[i] == '-')
			{
				throw new FormatException("Operation would return a negative value");
			}
			while (i < num)
			{
				char c = number[i];
				if (c == '\0')
				{
					i = num;
				}
				else if (c >= '0' && c <= '9')
				{
					bigInteger = bigInteger * 10 + (int)(c - '0');
					flag = true;
				}
				else
				{
					if (char.IsWhiteSpace(c))
					{
						for (i++; i < num; i++)
						{
							if (!char.IsWhiteSpace(number[i]))
							{
								throw new FormatException();
							}
						}
						break;
					}
					throw new FormatException();
				}
				i++;
			}
			if (!flag)
			{
				throw new FormatException();
			}
			return bigInteger;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0001392B File Offset: 0x00011B2B
		public static BigInteger operator +(BigInteger bi1, BigInteger bi2)
		{
			if (bi1 == 0U)
			{
				return new BigInteger(bi2);
			}
			if (bi2 == 0U)
			{
				return new BigInteger(bi1);
			}
			return BigInteger.Kernel.AddSameSign(bi1, bi2);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00013954 File Offset: 0x00011B54
		public static BigInteger operator -(BigInteger bi1, BigInteger bi2)
		{
			if (bi2 == 0U)
			{
				return new BigInteger(bi1);
			}
			if (bi1 == 0U)
			{
				throw new ArithmeticException("Operation would return a negative value");
			}
			switch (BigInteger.Kernel.Compare(bi1, bi2))
			{
			case BigInteger.Sign.Negative:
				throw new ArithmeticException("Operation would return a negative value");
			case BigInteger.Sign.Zero:
				return 0;
			case BigInteger.Sign.Positive:
				return BigInteger.Kernel.Subtract(bi1, bi2);
			default:
				throw new Exception();
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000139C2 File Offset: 0x00011BC2
		public static int operator %(BigInteger bi, int i)
		{
			if (i > 0)
			{
				return (int)BigInteger.Kernel.DwordMod(bi, (uint)i);
			}
			return (int)(-(int)BigInteger.Kernel.DwordMod(bi, (uint)(-(uint)i)));
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000139D9 File Offset: 0x00011BD9
		public static uint operator %(BigInteger bi, uint ui)
		{
			return BigInteger.Kernel.DwordMod(bi, ui);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000139E2 File Offset: 0x00011BE2
		public static BigInteger operator %(BigInteger bi1, BigInteger bi2)
		{
			return BigInteger.Kernel.multiByteDivide(bi1, bi2)[1];
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000139ED File Offset: 0x00011BED
		public static BigInteger operator /(BigInteger bi, int i)
		{
			if (i > 0)
			{
				return BigInteger.Kernel.DwordDiv(bi, (uint)i);
			}
			throw new ArithmeticException("Operation would return a negative value");
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00013A05 File Offset: 0x00011C05
		public static BigInteger operator /(BigInteger bi1, BigInteger bi2)
		{
			return BigInteger.Kernel.multiByteDivide(bi1, bi2)[0];
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00013A10 File Offset: 0x00011C10
		public static BigInteger operator *(BigInteger bi1, BigInteger bi2)
		{
			if (bi1 == 0U || bi2 == 0U)
			{
				return 0;
			}
			if ((long)bi1.data.Length < (long)((ulong)bi1.length))
			{
				throw new IndexOutOfRangeException("bi1 out of range");
			}
			if ((long)bi2.data.Length < (long)((ulong)bi2.length))
			{
				throw new IndexOutOfRangeException("bi2 out of range");
			}
			BigInteger bigInteger = new BigInteger(BigInteger.Sign.Positive, bi1.length + bi2.length);
			BigInteger.Kernel.Multiply(bi1.data, 0U, bi1.length, bi2.data, 0U, bi2.length, bigInteger.data, 0U);
			bigInteger.Normalize();
			return bigInteger;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00013AB1 File Offset: 0x00011CB1
		public static BigInteger operator *(BigInteger bi, int i)
		{
			if (i < 0)
			{
				throw new ArithmeticException("Operation would return a negative value");
			}
			if (i == 0)
			{
				return 0;
			}
			if (i == 1)
			{
				return new BigInteger(bi);
			}
			return BigInteger.Kernel.MultiplyByDword(bi, (uint)i);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00013ADE File Offset: 0x00011CDE
		public static BigInteger operator <<(BigInteger bi1, int shiftVal)
		{
			return BigInteger.Kernel.LeftShift(bi1, shiftVal);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00013AE7 File Offset: 0x00011CE7
		public static BigInteger operator >>(BigInteger bi1, int shiftVal)
		{
			return BigInteger.Kernel.RightShift(bi1, shiftVal);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00013AF0 File Offset: 0x00011CF0
		public static BigInteger Add(BigInteger bi1, BigInteger bi2)
		{
			return bi1 + bi2;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00013AF9 File Offset: 0x00011CF9
		public static BigInteger Subtract(BigInteger bi1, BigInteger bi2)
		{
			return bi1 - bi2;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00013B02 File Offset: 0x00011D02
		public static int Modulus(BigInteger bi, int i)
		{
			return bi % i;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00013B0B File Offset: 0x00011D0B
		public static uint Modulus(BigInteger bi, uint ui)
		{
			return bi % ui;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00013B14 File Offset: 0x00011D14
		public static BigInteger Modulus(BigInteger bi1, BigInteger bi2)
		{
			return bi1 % bi2;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00013B1D File Offset: 0x00011D1D
		public static BigInteger Divid(BigInteger bi, int i)
		{
			return bi / i;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00013B26 File Offset: 0x00011D26
		public static BigInteger Divid(BigInteger bi1, BigInteger bi2)
		{
			return bi1 / bi2;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00013B2F File Offset: 0x00011D2F
		public static BigInteger Multiply(BigInteger bi1, BigInteger bi2)
		{
			return bi1 * bi2;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00013B38 File Offset: 0x00011D38
		public static BigInteger Multiply(BigInteger bi, int i)
		{
			return bi * i;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060003AF RID: 943 RVA: 0x00013B41 File Offset: 0x00011D41
		private static RandomNumberGenerator Rng
		{
			get
			{
				if (BigInteger.rng == null)
				{
					BigInteger.rng = RandomNumberGenerator.Create();
				}
				return BigInteger.rng;
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00013B5C File Offset: 0x00011D5C
		public static BigInteger GenerateRandom(int bits, RandomNumberGenerator rng)
		{
			int num = bits >> 5;
			int num2 = bits & 31;
			if (num2 != 0)
			{
				num++;
			}
			BigInteger bigInteger = new BigInteger(BigInteger.Sign.Positive, (uint)(num + 1));
			byte[] src = new byte[num << 2];
			rng.GetBytes(src);
			Buffer.BlockCopy(src, 0, bigInteger.data, 0, num << 2);
			if (num2 != 0)
			{
				uint num3 = 1U << num2 - 1;
				bigInteger.data[num - 1] |= num3;
				num3 = uint.MaxValue >> 32 - num2;
				bigInteger.data[num - 1] &= num3;
			}
			else
			{
				bigInteger.data[num - 1] |= 2147483648U;
			}
			bigInteger.Normalize();
			return bigInteger;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00013C04 File Offset: 0x00011E04
		public static BigInteger GenerateRandom(int bits)
		{
			return BigInteger.GenerateRandom(bits, BigInteger.Rng);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00013C14 File Offset: 0x00011E14
		public void Randomize(RandomNumberGenerator rng)
		{
			if (this == 0U)
			{
				return;
			}
			int num = this.BitCount();
			int num2 = num >> 5;
			int num3 = num & 31;
			if (num3 != 0)
			{
				num2++;
			}
			byte[] src = new byte[num2 << 2];
			rng.GetBytes(src);
			Buffer.BlockCopy(src, 0, this.data, 0, num2 << 2);
			if (num3 != 0)
			{
				uint num4 = 1U << num3 - 1;
				this.data[num2 - 1] |= num4;
				num4 = uint.MaxValue >> 32 - num3;
				this.data[num2 - 1] &= num4;
			}
			else
			{
				this.data[num2 - 1] |= 2147483648U;
			}
			this.Normalize();
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00013CBC File Offset: 0x00011EBC
		public void Randomize()
		{
			this.Randomize(BigInteger.Rng);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00013CCC File Offset: 0x00011ECC
		public int BitCount()
		{
			this.Normalize();
			uint num = this.data[(int)(this.length - 1U)];
			uint num2 = 2147483648U;
			uint num3 = 32U;
			while (num3 > 0U && (num & num2) == 0U)
			{
				num3 -= 1U;
				num2 >>= 1;
			}
			return (int)(num3 + (this.length - 1U << 5));
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00013D1C File Offset: 0x00011F1C
		public bool TestBit(uint bitNum)
		{
			uint num = bitNum >> 5;
			byte b = (byte)(bitNum & 31U);
			uint num2 = 1U << (int)b;
			return (this.data[(int)num] & num2) > 0U;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00013D48 File Offset: 0x00011F48
		public bool TestBit(int bitNum)
		{
			if (bitNum < 0)
			{
				throw new IndexOutOfRangeException("bitNum out of range");
			}
			uint num = (uint)bitNum >> 5;
			byte b = (byte)(bitNum & 31);
			uint num2 = 1U << (int)b;
			return (this.data[(int)num] | num2) == this.data[(int)num];
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00013D89 File Offset: 0x00011F89
		public void SetBit(uint bitNum)
		{
			this.SetBit(bitNum, true);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00013D93 File Offset: 0x00011F93
		public void ClearBit(uint bitNum)
		{
			this.SetBit(bitNum, false);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00013DA0 File Offset: 0x00011FA0
		public void SetBit(uint bitNum, bool value)
		{
			uint num = bitNum >> 5;
			if (num < this.length)
			{
				uint num2 = 1U << (int)bitNum;
				if (value)
				{
					this.data[(int)num] |= num2;
					return;
				}
				this.data[(int)num] &= ~num2;
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00013DEC File Offset: 0x00011FEC
		public int LowestSetBit()
		{
			if (this == 0U)
			{
				return -1;
			}
			int num = 0;
			while (!this.TestBit(num))
			{
				num++;
			}
			return num;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00013E18 File Offset: 0x00012018
		public byte[] GetBytes()
		{
			if (this == 0U)
			{
				return new byte[1];
			}
			int num = this.BitCount();
			int num2 = num >> 3;
			if ((num & 7) != 0)
			{
				num2++;
			}
			byte[] array = new byte[num2];
			int num3 = num2 & 3;
			if (num3 == 0)
			{
				num3 = 4;
			}
			int num4 = 0;
			for (int i = (int)(this.length - 1U); i >= 0; i--)
			{
				uint num5 = this.data[i];
				for (int j = num3 - 1; j >= 0; j--)
				{
					array[num4 + j] = (byte)(num5 & 255U);
					num5 >>= 8;
				}
				num4 += num3;
				num3 = 4;
			}
			return array;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00013EA9 File Offset: 0x000120A9
		public static bool operator ==(BigInteger bi1, uint ui)
		{
			if (bi1.length != 1U)
			{
				bi1.Normalize();
			}
			return bi1.length == 1U && bi1.data[0] == ui;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00013ED0 File Offset: 0x000120D0
		public static bool operator !=(BigInteger bi1, uint ui)
		{
			if (bi1.length != 1U)
			{
				bi1.Normalize();
			}
			return bi1.length != 1U || bi1.data[0] != ui;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00013EFA File Offset: 0x000120FA
		public static bool operator ==(BigInteger bi1, BigInteger bi2)
		{
			return bi1 == bi2 || (!(null == bi1) && !(null == bi2) && BigInteger.Kernel.Compare(bi1, bi2) == BigInteger.Sign.Zero);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00013F20 File Offset: 0x00012120
		public static bool operator !=(BigInteger bi1, BigInteger bi2)
		{
			return bi1 != bi2 && (null == bi1 || null == bi2 || BigInteger.Kernel.Compare(bi1, bi2) > BigInteger.Sign.Zero);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00013F46 File Offset: 0x00012146
		public static bool operator >(BigInteger bi1, BigInteger bi2)
		{
			return BigInteger.Kernel.Compare(bi1, bi2) > BigInteger.Sign.Zero;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00013F52 File Offset: 0x00012152
		public static bool operator <(BigInteger bi1, BigInteger bi2)
		{
			return BigInteger.Kernel.Compare(bi1, bi2) < BigInteger.Sign.Zero;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00013F5E File Offset: 0x0001215E
		public static bool operator >=(BigInteger bi1, BigInteger bi2)
		{
			return BigInteger.Kernel.Compare(bi1, bi2) >= BigInteger.Sign.Zero;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00013F6D File Offset: 0x0001216D
		public static bool operator <=(BigInteger bi1, BigInteger bi2)
		{
			return BigInteger.Kernel.Compare(bi1, bi2) <= BigInteger.Sign.Zero;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00013F7C File Offset: 0x0001217C
		public BigInteger.Sign Compare(BigInteger bi)
		{
			return BigInteger.Kernel.Compare(this, bi);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00013F85 File Offset: 0x00012185
		public string ToString(uint radix)
		{
			return this.ToString(radix, "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00013F94 File Offset: 0x00012194
		public string ToString(uint radix, string characterSet)
		{
			if ((long)characterSet.Length < (long)((ulong)radix))
			{
				throw new ArgumentException("charSet length less than radix", "characterSet");
			}
			if (radix == 1U)
			{
				throw new ArgumentException("There is no such thing as radix one notation", "radix");
			}
			if (this == 0U)
			{
				return "0";
			}
			if (this == 1U)
			{
				return "1";
			}
			string text = "";
			BigInteger bigInteger = new BigInteger(this);
			while (bigInteger != 0U)
			{
				uint index = BigInteger.Kernel.SingleByteDivideInPlace(bigInteger, radix);
				text = characterSet[(int)index].ToString() + text;
			}
			return text;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00014028 File Offset: 0x00012228
		private void Normalize()
		{
			while (this.length > 0U && this.data[(int)(this.length - 1U)] == 0U)
			{
				this.length -= 1U;
			}
			if (this.length == 0U)
			{
				this.length += 1U;
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00014078 File Offset: 0x00012278
		public void Clear()
		{
			int num = 0;
			while ((long)num < (long)((ulong)this.length))
			{
				this.data[num] = 0U;
				num++;
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x000140A4 File Offset: 0x000122A4
		public override int GetHashCode()
		{
			uint num = 0U;
			for (uint num2 = 0U; num2 < this.length; num2 += 1U)
			{
				num ^= this.data[(int)num2];
			}
			return (int)num;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000140D0 File Offset: 0x000122D0
		public override string ToString()
		{
			return this.ToString(10U);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000140DC File Offset: 0x000122DC
		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			if (o is int)
			{
				return (int)o >= 0 && this == (uint)o;
			}
			BigInteger bigInteger = o as BigInteger;
			return !(bigInteger == null) && BigInteger.Kernel.Compare(this, bigInteger) == BigInteger.Sign.Zero;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0001412A File Offset: 0x0001232A
		public BigInteger GCD(BigInteger bi)
		{
			return BigInteger.Kernel.gcd(this, bi);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00014133 File Offset: 0x00012333
		public BigInteger ModInverse(BigInteger modulus)
		{
			return BigInteger.Kernel.modInverse(this, modulus);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0001413C File Offset: 0x0001233C
		public BigInteger ModPow(BigInteger exp, BigInteger n)
		{
			return new BigInteger.ModulusRing(n).Pow(this, exp);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001414C File Offset: 0x0001234C
		public bool IsProbablePrime()
		{
			if (this <= BigInteger.smallPrimes[BigInteger.smallPrimes.Length - 1])
			{
				for (int i = 0; i < BigInteger.smallPrimes.Length; i++)
				{
					if (this == BigInteger.smallPrimes[i])
					{
						return true;
					}
				}
				return false;
			}
			for (int j = 0; j < BigInteger.smallPrimes.Length; j++)
			{
				if (this % BigInteger.smallPrimes[j] == 0U)
				{
					return false;
				}
			}
			return PrimalityTests.Test(this, ConfidenceFactor.Medium);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000141C4 File Offset: 0x000123C4
		public static BigInteger NextHighestPrime(BigInteger bi)
		{
			return new NextPrimeFinder().GenerateNewPrime(0, bi);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000141D2 File Offset: 0x000123D2
		public static BigInteger GeneratePseudoPrime(int bits)
		{
			return new SequentialSearchPrimeGeneratorBase().GenerateNewPrime(bits);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000141E0 File Offset: 0x000123E0
		public void Incr2()
		{
			int num = 0;
			this.data[0] += 2U;
			if (this.data[0] < 2U)
			{
				this.data[++num] += 1U;
				while (this.data[num++] == 0U)
				{
					this.data[num] += 1U;
				}
				if (this.length == (uint)num)
				{
					this.length += 1U;
				}
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00014258 File Offset: 0x00012458
		// Note: this type is marked as 'beforefieldinit'.
		static BigInteger()
		{
		}

		// Token: 0x04000F33 RID: 3891
		private uint length = 1U;

		// Token: 0x04000F34 RID: 3892
		private uint[] data;

		// Token: 0x04000F35 RID: 3893
		private const uint DEFAULT_LEN = 20U;

		// Token: 0x04000F36 RID: 3894
		internal static readonly uint[] smallPrimes = new uint[]
		{
			2U,
			3U,
			5U,
			7U,
			11U,
			13U,
			17U,
			19U,
			23U,
			29U,
			31U,
			37U,
			41U,
			43U,
			47U,
			53U,
			59U,
			61U,
			67U,
			71U,
			73U,
			79U,
			83U,
			89U,
			97U,
			101U,
			103U,
			107U,
			109U,
			113U,
			127U,
			131U,
			137U,
			139U,
			149U,
			151U,
			157U,
			163U,
			167U,
			173U,
			179U,
			181U,
			191U,
			193U,
			197U,
			199U,
			211U,
			223U,
			227U,
			229U,
			233U,
			239U,
			241U,
			251U,
			257U,
			263U,
			269U,
			271U,
			277U,
			281U,
			283U,
			293U,
			307U,
			311U,
			313U,
			317U,
			331U,
			337U,
			347U,
			349U,
			353U,
			359U,
			367U,
			373U,
			379U,
			383U,
			389U,
			397U,
			401U,
			409U,
			419U,
			421U,
			431U,
			433U,
			439U,
			443U,
			449U,
			457U,
			461U,
			463U,
			467U,
			479U,
			487U,
			491U,
			499U,
			503U,
			509U,
			521U,
			523U,
			541U,
			547U,
			557U,
			563U,
			569U,
			571U,
			577U,
			587U,
			593U,
			599U,
			601U,
			607U,
			613U,
			617U,
			619U,
			631U,
			641U,
			643U,
			647U,
			653U,
			659U,
			661U,
			673U,
			677U,
			683U,
			691U,
			701U,
			709U,
			719U,
			727U,
			733U,
			739U,
			743U,
			751U,
			757U,
			761U,
			769U,
			773U,
			787U,
			797U,
			809U,
			811U,
			821U,
			823U,
			827U,
			829U,
			839U,
			853U,
			857U,
			859U,
			863U,
			877U,
			881U,
			883U,
			887U,
			907U,
			911U,
			919U,
			929U,
			937U,
			941U,
			947U,
			953U,
			967U,
			971U,
			977U,
			983U,
			991U,
			997U,
			1009U,
			1013U,
			1019U,
			1021U,
			1031U,
			1033U,
			1039U,
			1049U,
			1051U,
			1061U,
			1063U,
			1069U,
			1087U,
			1091U,
			1093U,
			1097U,
			1103U,
			1109U,
			1117U,
			1123U,
			1129U,
			1151U,
			1153U,
			1163U,
			1171U,
			1181U,
			1187U,
			1193U,
			1201U,
			1213U,
			1217U,
			1223U,
			1229U,
			1231U,
			1237U,
			1249U,
			1259U,
			1277U,
			1279U,
			1283U,
			1289U,
			1291U,
			1297U,
			1301U,
			1303U,
			1307U,
			1319U,
			1321U,
			1327U,
			1361U,
			1367U,
			1373U,
			1381U,
			1399U,
			1409U,
			1423U,
			1427U,
			1429U,
			1433U,
			1439U,
			1447U,
			1451U,
			1453U,
			1459U,
			1471U,
			1481U,
			1483U,
			1487U,
			1489U,
			1493U,
			1499U,
			1511U,
			1523U,
			1531U,
			1543U,
			1549U,
			1553U,
			1559U,
			1567U,
			1571U,
			1579U,
			1583U,
			1597U,
			1601U,
			1607U,
			1609U,
			1613U,
			1619U,
			1621U,
			1627U,
			1637U,
			1657U,
			1663U,
			1667U,
			1669U,
			1693U,
			1697U,
			1699U,
			1709U,
			1721U,
			1723U,
			1733U,
			1741U,
			1747U,
			1753U,
			1759U,
			1777U,
			1783U,
			1787U,
			1789U,
			1801U,
			1811U,
			1823U,
			1831U,
			1847U,
			1861U,
			1867U,
			1871U,
			1873U,
			1877U,
			1879U,
			1889U,
			1901U,
			1907U,
			1913U,
			1931U,
			1933U,
			1949U,
			1951U,
			1973U,
			1979U,
			1987U,
			1993U,
			1997U,
			1999U,
			2003U,
			2011U,
			2017U,
			2027U,
			2029U,
			2039U,
			2053U,
			2063U,
			2069U,
			2081U,
			2083U,
			2087U,
			2089U,
			2099U,
			2111U,
			2113U,
			2129U,
			2131U,
			2137U,
			2141U,
			2143U,
			2153U,
			2161U,
			2179U,
			2203U,
			2207U,
			2213U,
			2221U,
			2237U,
			2239U,
			2243U,
			2251U,
			2267U,
			2269U,
			2273U,
			2281U,
			2287U,
			2293U,
			2297U,
			2309U,
			2311U,
			2333U,
			2339U,
			2341U,
			2347U,
			2351U,
			2357U,
			2371U,
			2377U,
			2381U,
			2383U,
			2389U,
			2393U,
			2399U,
			2411U,
			2417U,
			2423U,
			2437U,
			2441U,
			2447U,
			2459U,
			2467U,
			2473U,
			2477U,
			2503U,
			2521U,
			2531U,
			2539U,
			2543U,
			2549U,
			2551U,
			2557U,
			2579U,
			2591U,
			2593U,
			2609U,
			2617U,
			2621U,
			2633U,
			2647U,
			2657U,
			2659U,
			2663U,
			2671U,
			2677U,
			2683U,
			2687U,
			2689U,
			2693U,
			2699U,
			2707U,
			2711U,
			2713U,
			2719U,
			2729U,
			2731U,
			2741U,
			2749U,
			2753U,
			2767U,
			2777U,
			2789U,
			2791U,
			2797U,
			2801U,
			2803U,
			2819U,
			2833U,
			2837U,
			2843U,
			2851U,
			2857U,
			2861U,
			2879U,
			2887U,
			2897U,
			2903U,
			2909U,
			2917U,
			2927U,
			2939U,
			2953U,
			2957U,
			2963U,
			2969U,
			2971U,
			2999U,
			3001U,
			3011U,
			3019U,
			3023U,
			3037U,
			3041U,
			3049U,
			3061U,
			3067U,
			3079U,
			3083U,
			3089U,
			3109U,
			3119U,
			3121U,
			3137U,
			3163U,
			3167U,
			3169U,
			3181U,
			3187U,
			3191U,
			3203U,
			3209U,
			3217U,
			3221U,
			3229U,
			3251U,
			3253U,
			3257U,
			3259U,
			3271U,
			3299U,
			3301U,
			3307U,
			3313U,
			3319U,
			3323U,
			3329U,
			3331U,
			3343U,
			3347U,
			3359U,
			3361U,
			3371U,
			3373U,
			3389U,
			3391U,
			3407U,
			3413U,
			3433U,
			3449U,
			3457U,
			3461U,
			3463U,
			3467U,
			3469U,
			3491U,
			3499U,
			3511U,
			3517U,
			3527U,
			3529U,
			3533U,
			3539U,
			3541U,
			3547U,
			3557U,
			3559U,
			3571U,
			3581U,
			3583U,
			3593U,
			3607U,
			3613U,
			3617U,
			3623U,
			3631U,
			3637U,
			3643U,
			3659U,
			3671U,
			3673U,
			3677U,
			3691U,
			3697U,
			3701U,
			3709U,
			3719U,
			3727U,
			3733U,
			3739U,
			3761U,
			3767U,
			3769U,
			3779U,
			3793U,
			3797U,
			3803U,
			3821U,
			3823U,
			3833U,
			3847U,
			3851U,
			3853U,
			3863U,
			3877U,
			3881U,
			3889U,
			3907U,
			3911U,
			3917U,
			3919U,
			3923U,
			3929U,
			3931U,
			3943U,
			3947U,
			3967U,
			3989U,
			4001U,
			4003U,
			4007U,
			4013U,
			4019U,
			4021U,
			4027U,
			4049U,
			4051U,
			4057U,
			4073U,
			4079U,
			4091U,
			4093U,
			4099U,
			4111U,
			4127U,
			4129U,
			4133U,
			4139U,
			4153U,
			4157U,
			4159U,
			4177U,
			4201U,
			4211U,
			4217U,
			4219U,
			4229U,
			4231U,
			4241U,
			4243U,
			4253U,
			4259U,
			4261U,
			4271U,
			4273U,
			4283U,
			4289U,
			4297U,
			4327U,
			4337U,
			4339U,
			4349U,
			4357U,
			4363U,
			4373U,
			4391U,
			4397U,
			4409U,
			4421U,
			4423U,
			4441U,
			4447U,
			4451U,
			4457U,
			4463U,
			4481U,
			4483U,
			4493U,
			4507U,
			4513U,
			4517U,
			4519U,
			4523U,
			4547U,
			4549U,
			4561U,
			4567U,
			4583U,
			4591U,
			4597U,
			4603U,
			4621U,
			4637U,
			4639U,
			4643U,
			4649U,
			4651U,
			4657U,
			4663U,
			4673U,
			4679U,
			4691U,
			4703U,
			4721U,
			4723U,
			4729U,
			4733U,
			4751U,
			4759U,
			4783U,
			4787U,
			4789U,
			4793U,
			4799U,
			4801U,
			4813U,
			4817U,
			4831U,
			4861U,
			4871U,
			4877U,
			4889U,
			4903U,
			4909U,
			4919U,
			4931U,
			4933U,
			4937U,
			4943U,
			4951U,
			4957U,
			4967U,
			4969U,
			4973U,
			4987U,
			4993U,
			4999U,
			5003U,
			5009U,
			5011U,
			5021U,
			5023U,
			5039U,
			5051U,
			5059U,
			5077U,
			5081U,
			5087U,
			5099U,
			5101U,
			5107U,
			5113U,
			5119U,
			5147U,
			5153U,
			5167U,
			5171U,
			5179U,
			5189U,
			5197U,
			5209U,
			5227U,
			5231U,
			5233U,
			5237U,
			5261U,
			5273U,
			5279U,
			5281U,
			5297U,
			5303U,
			5309U,
			5323U,
			5333U,
			5347U,
			5351U,
			5381U,
			5387U,
			5393U,
			5399U,
			5407U,
			5413U,
			5417U,
			5419U,
			5431U,
			5437U,
			5441U,
			5443U,
			5449U,
			5471U,
			5477U,
			5479U,
			5483U,
			5501U,
			5503U,
			5507U,
			5519U,
			5521U,
			5527U,
			5531U,
			5557U,
			5563U,
			5569U,
			5573U,
			5581U,
			5591U,
			5623U,
			5639U,
			5641U,
			5647U,
			5651U,
			5653U,
			5657U,
			5659U,
			5669U,
			5683U,
			5689U,
			5693U,
			5701U,
			5711U,
			5717U,
			5737U,
			5741U,
			5743U,
			5749U,
			5779U,
			5783U,
			5791U,
			5801U,
			5807U,
			5813U,
			5821U,
			5827U,
			5839U,
			5843U,
			5849U,
			5851U,
			5857U,
			5861U,
			5867U,
			5869U,
			5879U,
			5881U,
			5897U,
			5903U,
			5923U,
			5927U,
			5939U,
			5953U,
			5981U,
			5987U
		};

		// Token: 0x04000F37 RID: 3895
		private const string WouldReturnNegVal = "Operation would return a negative value";

		// Token: 0x04000F38 RID: 3896
		private static RandomNumberGenerator rng;

		// Token: 0x02000098 RID: 152
		public enum Sign
		{
			// Token: 0x04000F3A RID: 3898
			Negative = -1,
			// Token: 0x04000F3B RID: 3899
			Zero,
			// Token: 0x04000F3C RID: 3900
			Positive
		}

		// Token: 0x02000099 RID: 153
		internal sealed class ModulusRing
		{
			// Token: 0x060003D4 RID: 980 RVA: 0x00014274 File Offset: 0x00012474
			public ModulusRing(BigInteger modulus)
			{
				this.mod = modulus;
				uint num = this.mod.length << 1;
				this.constant = new BigInteger(BigInteger.Sign.Positive, num + 1U);
				this.constant.data[(int)num] = 1U;
				this.constant /= this.mod;
			}

			// Token: 0x060003D5 RID: 981 RVA: 0x000142D0 File Offset: 0x000124D0
			public void BarrettReduction(BigInteger x)
			{
				BigInteger bigInteger = this.mod;
				uint length = bigInteger.length;
				uint num = length + 1U;
				uint num2 = length - 1U;
				if (x.length < length)
				{
					return;
				}
				if ((long)x.data.Length < (long)((ulong)x.length))
				{
					throw new IndexOutOfRangeException("x out of range");
				}
				BigInteger bigInteger2 = new BigInteger(BigInteger.Sign.Positive, x.length - num2 + this.constant.length);
				BigInteger.Kernel.Multiply(x.data, num2, x.length - num2, this.constant.data, 0U, this.constant.length, bigInteger2.data, 0U);
				uint length2 = (x.length > num) ? num : x.length;
				x.length = length2;
				x.Normalize();
				BigInteger bigInteger3 = new BigInteger(BigInteger.Sign.Positive, num);
				BigInteger.Kernel.MultiplyMod2p32pmod(bigInteger2.data, (int)num, (int)(bigInteger2.length - num), bigInteger.data, 0, (int)bigInteger.length, bigInteger3.data, 0, (int)num);
				bigInteger3.Normalize();
				if (bigInteger3 <= x)
				{
					BigInteger.Kernel.MinusEq(x, bigInteger3);
				}
				else
				{
					BigInteger bigInteger4 = new BigInteger(BigInteger.Sign.Positive, num + 1U);
					bigInteger4.data[(int)num] = 1U;
					BigInteger.Kernel.MinusEq(bigInteger4, bigInteger3);
					BigInteger.Kernel.PlusEq(x, bigInteger4);
				}
				while (x >= bigInteger)
				{
					BigInteger.Kernel.MinusEq(x, bigInteger);
				}
			}

			// Token: 0x060003D6 RID: 982 RVA: 0x00014414 File Offset: 0x00012614
			public BigInteger Multiply(BigInteger a, BigInteger b)
			{
				if (a == 0U || b == 0U)
				{
					return 0;
				}
				if (a > this.mod)
				{
					a %= this.mod;
				}
				if (b > this.mod)
				{
					b %= this.mod;
				}
				BigInteger bigInteger = a * b;
				this.BarrettReduction(bigInteger);
				return bigInteger;
			}

			// Token: 0x060003D7 RID: 983 RVA: 0x00014484 File Offset: 0x00012684
			public BigInteger Difference(BigInteger a, BigInteger b)
			{
				BigInteger.Sign sign = BigInteger.Kernel.Compare(a, b);
				BigInteger bigInteger;
				switch (sign)
				{
				case BigInteger.Sign.Negative:
					bigInteger = b - a;
					break;
				case BigInteger.Sign.Zero:
					return 0;
				case BigInteger.Sign.Positive:
					bigInteger = a - b;
					break;
				default:
					throw new Exception();
				}
				if (bigInteger >= this.mod)
				{
					if (bigInteger.length >= this.mod.length << 1)
					{
						bigInteger %= this.mod;
					}
					else
					{
						this.BarrettReduction(bigInteger);
					}
				}
				if (sign == BigInteger.Sign.Negative)
				{
					bigInteger = this.mod - bigInteger;
				}
				return bigInteger;
			}

			// Token: 0x060003D8 RID: 984 RVA: 0x0001451C File Offset: 0x0001271C
			public BigInteger Pow(BigInteger a, BigInteger k)
			{
				BigInteger bigInteger = new BigInteger(1U);
				if (k == 0U)
				{
					return bigInteger;
				}
				BigInteger bigInteger2 = a;
				if (k.TestBit(0))
				{
					bigInteger = a;
				}
				for (int i = 1; i < k.BitCount(); i++)
				{
					bigInteger2 = this.Multiply(bigInteger2, bigInteger2);
					if (k.TestBit(i))
					{
						bigInteger = this.Multiply(bigInteger2, bigInteger);
					}
				}
				return bigInteger;
			}

			// Token: 0x060003D9 RID: 985 RVA: 0x00014575 File Offset: 0x00012775
			public BigInteger Pow(uint b, BigInteger exp)
			{
				return this.Pow(new BigInteger(b), exp);
			}

			// Token: 0x04000F3D RID: 3901
			private BigInteger mod;

			// Token: 0x04000F3E RID: 3902
			private BigInteger constant;
		}

		// Token: 0x0200009A RID: 154
		private sealed class Kernel
		{
			// Token: 0x060003DA RID: 986 RVA: 0x00014584 File Offset: 0x00012784
			public static BigInteger AddSameSign(BigInteger bi1, BigInteger bi2)
			{
				uint num = 0U;
				uint[] data;
				uint length;
				uint[] data2;
				uint length2;
				if (bi1.length < bi2.length)
				{
					data = bi2.data;
					length = bi2.length;
					data2 = bi1.data;
					length2 = bi1.length;
				}
				else
				{
					data = bi1.data;
					length = bi1.length;
					data2 = bi2.data;
					length2 = bi2.length;
				}
				BigInteger bigInteger = new BigInteger(BigInteger.Sign.Positive, length + 1U);
				uint[] data3 = bigInteger.data;
				ulong num2 = 0UL;
				do
				{
					num2 = (ulong)data[(int)num] + (ulong)data2[(int)num] + num2;
					data3[(int)num] = (uint)num2;
					num2 >>= 32;
				}
				while ((num += 1U) < length2);
				bool flag = num2 > 0UL;
				if (flag)
				{
					if (num < length)
					{
						do
						{
							flag = ((data3[(int)num] = data[(int)num] + 1U) == 0U);
						}
						while ((num += 1U) < length && flag);
					}
					if (flag)
					{
						data3[(int)num] = 1U;
						bigInteger.length = num + 1U;
						return bigInteger;
					}
				}
				if (num < length)
				{
					do
					{
						data3[(int)num] = data[(int)num];
					}
					while ((num += 1U) < length);
				}
				bigInteger.Normalize();
				return bigInteger;
			}

			// Token: 0x060003DB RID: 987 RVA: 0x00014690 File Offset: 0x00012890
			public static BigInteger Subtract(BigInteger big, BigInteger small)
			{
				BigInteger bigInteger = new BigInteger(BigInteger.Sign.Positive, big.length);
				uint[] data = bigInteger.data;
				uint[] data2 = big.data;
				uint[] data3 = small.data;
				uint num = 0U;
				uint num2 = 0U;
				do
				{
					uint num3 = data3[(int)num];
					if ((num3 += num2) < num2 | (data[(int)num] = data2[(int)num] - num3) > ~num3)
					{
						num2 = 1U;
					}
					else
					{
						num2 = 0U;
					}
				}
				while ((num += 1U) < small.length);
				if (num != big.length)
				{
					if (num2 == 1U)
					{
						do
						{
							data[(int)num] = data2[(int)num] - 1U;
						}
						while (data2[(int)num++] == 0U && num < big.length);
						if (num == big.length)
						{
							goto IL_B8;
						}
					}
					do
					{
						data[(int)num] = data2[(int)num];
					}
					while ((num += 1U) < big.length);
				}
				IL_B8:
				bigInteger.Normalize();
				return bigInteger;
			}

			// Token: 0x060003DC RID: 988 RVA: 0x0001475C File Offset: 0x0001295C
			public static void MinusEq(BigInteger big, BigInteger small)
			{
				uint[] data = big.data;
				uint[] data2 = small.data;
				uint num = 0U;
				uint num2 = 0U;
				do
				{
					uint num3 = data2[(int)num];
					if ((num3 += num2) < num2 | (data[(int)num] -= num3) > ~num3)
					{
						num2 = 1U;
					}
					else
					{
						num2 = 0U;
					}
				}
				while ((num += 1U) < small.length);
				if (num != big.length && num2 == 1U)
				{
					do
					{
						data[(int)num] -= 1U;
						if (data[(int)num++] != 0U)
						{
							break;
						}
					}
					while (num < big.length);
				}
				while (big.length > 0U && big.data[(int)(big.length - 1U)] == 0U)
				{
					big.length -= 1U;
				}
				if (big.length == 0U)
				{
					big.length += 1U;
				}
			}

			// Token: 0x060003DD RID: 989 RVA: 0x00014824 File Offset: 0x00012A24
			public static void PlusEq(BigInteger bi1, BigInteger bi2)
			{
				uint num = 0U;
				bool flag = false;
				uint[] data;
				uint length;
				uint[] data2;
				uint length2;
				if (bi1.length < bi2.length)
				{
					flag = true;
					data = bi2.data;
					length = bi2.length;
					data2 = bi1.data;
					length2 = bi1.length;
				}
				else
				{
					data = bi1.data;
					length = bi1.length;
					data2 = bi2.data;
					length2 = bi2.length;
				}
				uint[] data3 = bi1.data;
				ulong num2 = 0UL;
				do
				{
					num2 += (ulong)data[(int)num] + (ulong)data2[(int)num];
					data3[(int)num] = (uint)num2;
					num2 >>= 32;
				}
				while ((num += 1U) < length2);
				bool flag2 = num2 > 0UL;
				if (flag2)
				{
					if (num < length)
					{
						do
						{
							flag2 = ((data3[(int)num] = data[(int)num] + 1U) == 0U);
						}
						while ((num += 1U) < length && flag2);
					}
					if (flag2)
					{
						data3[(int)num] = 1U;
						bi1.length = num + 1U;
						return;
					}
				}
				if (flag && num < length - 1U)
				{
					do
					{
						data3[(int)num] = data[(int)num];
					}
					while ((num += 1U) < length);
				}
				bi1.length = length + 1U;
				bi1.Normalize();
			}

			// Token: 0x060003DE RID: 990 RVA: 0x00014934 File Offset: 0x00012B34
			public static BigInteger.Sign Compare(BigInteger bi1, BigInteger bi2)
			{
				uint num = bi1.length;
				uint num2 = bi2.length;
				while (num > 0U)
				{
					if (bi1.data[(int)(num - 1U)] != 0U)
					{
						break;
					}
					num -= 1U;
				}
				while (num2 > 0U && bi2.data[(int)(num2 - 1U)] == 0U)
				{
					num2 -= 1U;
				}
				if (num == 0U && num2 == 0U)
				{
					return BigInteger.Sign.Zero;
				}
				if (num < num2)
				{
					return BigInteger.Sign.Negative;
				}
				if (num > num2)
				{
					return BigInteger.Sign.Positive;
				}
				uint num3 = num - 1U;
				while (num3 != 0U && bi1.data[(int)num3] == bi2.data[(int)num3])
				{
					num3 -= 1U;
				}
				if (bi1.data[(int)num3] < bi2.data[(int)num3])
				{
					return BigInteger.Sign.Negative;
				}
				if (bi1.data[(int)num3] > bi2.data[(int)num3])
				{
					return BigInteger.Sign.Positive;
				}
				return BigInteger.Sign.Zero;
			}

			// Token: 0x060003DF RID: 991 RVA: 0x000149D8 File Offset: 0x00012BD8
			public static uint SingleByteDivideInPlace(BigInteger n, uint d)
			{
				ulong num = 0UL;
				uint length = n.length;
				while (length-- > 0U)
				{
					num <<= 32;
					num |= (ulong)n.data[(int)length];
					n.data[(int)length] = (uint)(num / (ulong)d);
					num %= (ulong)d;
				}
				n.Normalize();
				return (uint)num;
			}

			// Token: 0x060003E0 RID: 992 RVA: 0x00014A24 File Offset: 0x00012C24
			public static uint DwordMod(BigInteger n, uint d)
			{
				ulong num = 0UL;
				uint length = n.length;
				while (length-- > 0U)
				{
					num <<= 32;
					num |= (ulong)n.data[(int)length];
					num %= (ulong)d;
				}
				return (uint)num;
			}

			// Token: 0x060003E1 RID: 993 RVA: 0x00014A60 File Offset: 0x00012C60
			public static BigInteger DwordDiv(BigInteger n, uint d)
			{
				BigInteger bigInteger = new BigInteger(BigInteger.Sign.Positive, n.length);
				ulong num = 0UL;
				uint length = n.length;
				while (length-- > 0U)
				{
					num <<= 32;
					num |= (ulong)n.data[(int)length];
					bigInteger.data[(int)length] = (uint)(num / (ulong)d);
					num %= (ulong)d;
				}
				bigInteger.Normalize();
				return bigInteger;
			}

			// Token: 0x060003E2 RID: 994 RVA: 0x00014AB8 File Offset: 0x00012CB8
			public static BigInteger[] DwordDivMod(BigInteger n, uint d)
			{
				BigInteger bigInteger = new BigInteger(BigInteger.Sign.Positive, n.length);
				ulong num = 0UL;
				uint length = n.length;
				while (length-- > 0U)
				{
					num <<= 32;
					num |= (ulong)n.data[(int)length];
					bigInteger.data[(int)length] = (uint)(num / (ulong)d);
					num %= (ulong)d;
				}
				bigInteger.Normalize();
				BigInteger bigInteger2 = (uint)num;
				return new BigInteger[]
				{
					bigInteger,
					bigInteger2
				};
			}

			// Token: 0x060003E3 RID: 995 RVA: 0x00014B28 File Offset: 0x00012D28
			public static BigInteger[] multiByteDivide(BigInteger bi1, BigInteger bi2)
			{
				if (BigInteger.Kernel.Compare(bi1, bi2) == BigInteger.Sign.Negative)
				{
					return new BigInteger[]
					{
						0,
						new BigInteger(bi1)
					};
				}
				bi1.Normalize();
				bi2.Normalize();
				if (bi2.length == 1U)
				{
					return BigInteger.Kernel.DwordDivMod(bi1, bi2.data[0]);
				}
				uint num = bi1.length + 1U;
				int num2 = (int)(bi2.length + 1U);
				uint num3 = 2147483648U;
				uint num4 = bi2.data[(int)(bi2.length - 1U)];
				int num5 = 0;
				int num6 = (int)(bi1.length - bi2.length);
				while (num3 != 0U && (num4 & num3) == 0U)
				{
					num5++;
					num3 >>= 1;
				}
				BigInteger bigInteger = new BigInteger(BigInteger.Sign.Positive, bi1.length - bi2.length + 1U);
				BigInteger bigInteger2 = bi1 << num5;
				uint[] data = bigInteger2.data;
				bi2 <<= num5;
				int i = (int)(num - bi2.length);
				int num7 = (int)(num - 1U);
				uint num8 = bi2.data[(int)(bi2.length - 1U)];
				ulong num9 = (ulong)bi2.data[(int)(bi2.length - 2U)];
				while (i > 0)
				{
					ulong num10 = ((ulong)data[num7] << 32) + (ulong)data[num7 - 1];
					ulong num11 = num10 / (ulong)num8;
					ulong num12 = num10 % (ulong)num8;
					while (num11 == 4294967296UL || num11 * num9 > (num12 << 32) + (ulong)data[num7 - 2])
					{
						num11 -= 1UL;
						num12 += (ulong)num8;
						if (num12 >= 4294967296UL)
						{
							break;
						}
					}
					uint num13 = 0U;
					int num14 = num7 - num2 + 1;
					ulong num15 = 0UL;
					uint num16 = (uint)num11;
					do
					{
						num15 += (ulong)bi2.data[(int)num13] * (ulong)num16;
						uint num17 = data[num14];
						data[num14] -= (uint)num15;
						num15 >>= 32;
						if (data[num14] > num17)
						{
							num15 += 1UL;
						}
						num13 += 1U;
						num14++;
					}
					while ((ulong)num13 < (ulong)((long)num2));
					num14 = num7 - num2 + 1;
					num13 = 0U;
					if (num15 != 0UL)
					{
						num16 -= 1U;
						ulong num18 = 0UL;
						do
						{
							num18 = (ulong)data[num14] + (ulong)bi2.data[(int)num13] + num18;
							data[num14] = (uint)num18;
							num18 >>= 32;
							num13 += 1U;
							num14++;
						}
						while ((ulong)num13 < (ulong)((long)num2));
					}
					bigInteger.data[num6--] = num16;
					num7--;
					i--;
				}
				bigInteger.Normalize();
				bigInteger2.Normalize();
				BigInteger[] array = new BigInteger[]
				{
					bigInteger,
					bigInteger2
				};
				if (num5 != 0)
				{
					BigInteger[] array2 = array;
					array2[1] = array2[1] >> num5;
				}
				return array;
			}

			// Token: 0x060003E4 RID: 996 RVA: 0x00014DAC File Offset: 0x00012FAC
			public static BigInteger LeftShift(BigInteger bi, int n)
			{
				if (n == 0)
				{
					return new BigInteger(bi, bi.length + 1U);
				}
				int num = n >> 5;
				n &= 31;
				BigInteger bigInteger = new BigInteger(BigInteger.Sign.Positive, bi.length + 1U + (uint)num);
				uint num2 = 0U;
				uint length = bi.length;
				if (n != 0)
				{
					uint num3 = 0U;
					while (num2 < length)
					{
						uint num4 = bi.data[(int)num2];
						bigInteger.data[(int)(checked((IntPtr)(unchecked((ulong)num2 + (ulong)((long)num)))))] = (num4 << n | num3);
						num3 = num4 >> 32 - n;
						num2 += 1U;
					}
					bigInteger.data[(int)(checked((IntPtr)(unchecked((ulong)num2 + (ulong)((long)num)))))] = num3;
				}
				else
				{
					while (num2 < length)
					{
						bigInteger.data[(int)(checked((IntPtr)(unchecked((ulong)num2 + (ulong)((long)num)))))] = bi.data[(int)num2];
						num2 += 1U;
					}
				}
				bigInteger.Normalize();
				return bigInteger;
			}

			// Token: 0x060003E5 RID: 997 RVA: 0x00014E64 File Offset: 0x00013064
			public static BigInteger RightShift(BigInteger bi, int n)
			{
				if (n == 0)
				{
					return new BigInteger(bi);
				}
				int num = n >> 5;
				bool flag = (n & 31) != 0;
				BigInteger bigInteger = new BigInteger(BigInteger.Sign.Positive, bi.length - (uint)num + 1U);
				uint num2 = (uint)(bigInteger.data.Length - 1);
				if (flag)
				{
					uint num3 = 0U;
					while (num2-- > 0U)
					{
						uint num4 = bi.data[(int)(checked((IntPtr)(unchecked((ulong)num2 + (ulong)((long)num)))))];
						bigInteger.data[(int)num2] = (num4 >> n | num3);
						num3 = num4 << 32 - n;
					}
				}
				else
				{
					while (num2-- > 0U)
					{
						bigInteger.data[(int)num2] = bi.data[(int)(checked((IntPtr)(unchecked((ulong)num2 + (ulong)((long)num)))))];
					}
				}
				bigInteger.Normalize();
				return bigInteger;
			}

			// Token: 0x060003E6 RID: 998 RVA: 0x00014F00 File Offset: 0x00013100
			public static BigInteger MultiplyByDword(BigInteger n, uint f)
			{
				BigInteger bigInteger = new BigInteger(BigInteger.Sign.Positive, n.length + 1U);
				uint num = 0U;
				ulong num2 = 0UL;
				do
				{
					num2 += (ulong)n.data[(int)num] * (ulong)f;
					bigInteger.data[(int)num] = (uint)num2;
					num2 >>= 32;
				}
				while ((num += 1U) < n.length);
				bigInteger.data[(int)num] = (uint)num2;
				bigInteger.Normalize();
				return bigInteger;
			}

			// Token: 0x060003E7 RID: 999 RVA: 0x00014F60 File Offset: 0x00013160
			public unsafe static void Multiply(uint[] x, uint xOffset, uint xLen, uint[] y, uint yOffset, uint yLen, uint[] d, uint dOffset)
			{
				fixed (uint[] array = x)
				{
					uint* ptr;
					if (x == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					fixed (uint[] array2 = y)
					{
						uint* ptr2;
						if (y == null || array2.Length == 0)
						{
							ptr2 = null;
						}
						else
						{
							ptr2 = &array2[0];
						}
						fixed (uint[] array3 = d)
						{
							uint* ptr3;
							if (d == null || array3.Length == 0)
							{
								ptr3 = null;
							}
							else
							{
								ptr3 = &array3[0];
							}
							uint* ptr4 = ptr + (ulong)xOffset * 4UL / 4UL;
							uint* ptr5 = ptr4 + (ulong)xLen * 4UL / 4UL;
							uint* ptr6 = ptr2 + (ulong)yOffset * 4UL / 4UL;
							uint* ptr7 = ptr6 + (ulong)yLen * 4UL / 4UL;
							uint* ptr8 = ptr3 + (ulong)dOffset * 4UL / 4UL;
							while (ptr4 < ptr5)
							{
								if (*ptr4 != 0U)
								{
									ulong num = 0UL;
									uint* ptr9 = ptr8;
									uint* ptr10 = ptr6;
									while (ptr10 < ptr7)
									{
										num += (ulong)(*ptr4) * (ulong)(*ptr10) + (ulong)(*ptr9);
										*ptr9 = (uint)num;
										num >>= 32;
										ptr10++;
										ptr9++;
									}
									if (num != 0UL)
									{
										*ptr9 = (uint)num;
									}
								}
								ptr4++;
								ptr8++;
							}
							array = null;
							array2 = null;
						}
					}
				}
			}

			// Token: 0x060003E8 RID: 1000 RVA: 0x00015060 File Offset: 0x00013260
			public unsafe static void MultiplyMod2p32pmod(uint[] x, int xOffset, int xLen, uint[] y, int yOffest, int yLen, uint[] d, int dOffset, int mod)
			{
				fixed (uint[] array = x)
				{
					uint* ptr;
					if (x == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					fixed (uint[] array2 = y)
					{
						uint* ptr2;
						if (y == null || array2.Length == 0)
						{
							ptr2 = null;
						}
						else
						{
							ptr2 = &array2[0];
						}
						fixed (uint[] array3 = d)
						{
							uint* ptr3;
							if (d == null || array3.Length == 0)
							{
								ptr3 = null;
							}
							else
							{
								ptr3 = &array3[0];
							}
							uint* ptr4 = ptr + xOffset;
							uint* ptr5 = ptr4 + xLen;
							uint* ptr6 = ptr2 + yOffest;
							uint* ptr7 = ptr6 + yLen;
							uint* ptr8 = ptr3 + dOffset;
							uint* ptr9 = ptr8 + mod;
							while (ptr4 < ptr5)
							{
								if (*ptr4 != 0U)
								{
									ulong num = 0UL;
									uint* ptr10 = ptr8;
									uint* ptr11 = ptr6;
									while (ptr11 < ptr7 && ptr10 < ptr9)
									{
										num += (ulong)(*ptr4) * (ulong)(*ptr11) + (ulong)(*ptr10);
										*ptr10 = (uint)num;
										num >>= 32;
										ptr11++;
										ptr10++;
									}
									if (num != 0UL && ptr10 < ptr9)
									{
										*ptr10 = (uint)num;
									}
								}
								ptr4++;
								ptr8++;
							}
							array = null;
							array2 = null;
						}
					}
				}
			}

			// Token: 0x060003E9 RID: 1001 RVA: 0x0001516C File Offset: 0x0001336C
			public unsafe static void SquarePositive(BigInteger bi, ref uint[] wkSpace)
			{
				uint[] array = wkSpace;
				wkSpace = bi.data;
				uint[] data = bi.data;
				uint length = bi.length;
				bi.data = array;
				uint[] array3;
				fixed (uint[] array2 = data)
				{
					uint* ptr;
					if (data == null || array2.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array2[0];
					}
					uint* ptr2;
					if ((array3 = array) == null || array3.Length == 0)
					{
						ptr2 = null;
					}
					else
					{
						ptr2 = &array3[0];
					}
					uint* ptr3 = ptr2 + array.Length;
					for (uint* ptr4 = ptr2; ptr4 < ptr3; ptr4++)
					{
						*ptr4 = 0U;
					}
					uint* ptr5 = ptr;
					uint* ptr6 = ptr2;
					uint num = 0U;
					while (num < length)
					{
						if (*ptr5 != 0U)
						{
							ulong num2 = 0UL;
							uint num3 = *ptr5;
							uint* ptr7 = ptr5 + 1;
							uint* ptr8 = ptr6 + (ulong)(2U * num) * 4UL / 4UL + 1;
							uint num4 = num + 1U;
							while (num4 < length)
							{
								num2 += (ulong)num3 * (ulong)(*ptr7) + (ulong)(*ptr8);
								*ptr8 = (uint)num2;
								num2 >>= 32;
								num4 += 1U;
								ptr8++;
								ptr7++;
							}
							if (num2 != 0UL)
							{
								*ptr8 = (uint)num2;
							}
						}
						num += 1U;
						ptr5++;
					}
					ptr6 = ptr2;
					uint num5 = 0U;
					while (ptr6 < ptr3)
					{
						uint num6 = *ptr6;
						*ptr6 = (num6 << 1 | num5);
						num5 = num6 >> 31;
						ptr6++;
					}
					if (num5 != 0U)
					{
						*ptr6 = num5;
					}
					ptr5 = ptr;
					ptr6 = ptr2;
					uint* ptr9 = ptr5 + (ulong)length * 4UL / 4UL;
					while (ptr5 < ptr9)
					{
						ulong num7 = (ulong)(*ptr5) * (ulong)(*ptr5) + (ulong)(*ptr6);
						*ptr6 = (uint)num7;
						num7 >>= 32;
						*(++ptr6) += (uint)num7;
						if (*ptr6 < (uint)num7)
						{
							uint* ptr10 = ptr6;
							*(++ptr10) += 1U;
							while (*(ptr10++) == 0U)
							{
								*ptr10 += 1U;
							}
						}
						ptr5++;
						ptr6++;
					}
					bi.length <<= 1;
					while (ptr2[(ulong)(bi.length - 1U) * 4UL / 4UL] == 0U && bi.length > 1U)
					{
						bi.length -= 1U;
					}
				}
				array3 = null;
			}

			// Token: 0x060003EA RID: 1002 RVA: 0x00015364 File Offset: 0x00013564
			public static BigInteger gcd(BigInteger a, BigInteger b)
			{
				BigInteger bigInteger = a;
				BigInteger bigInteger2 = b;
				BigInteger bigInteger3 = bigInteger2;
				while (bigInteger.length > 1U)
				{
					bigInteger3 = bigInteger;
					bigInteger = bigInteger2 % bigInteger;
					bigInteger2 = bigInteger3;
				}
				if (bigInteger == 0U)
				{
					return bigInteger3;
				}
				uint num = bigInteger.data[0];
				uint num2 = bigInteger2 % num;
				int num3 = 0;
				while (((num2 | num) & 1U) == 0U)
				{
					num2 >>= 1;
					num >>= 1;
					num3++;
				}
				while (num2 != 0U)
				{
					while ((num2 & 1U) == 0U)
					{
						num2 >>= 1;
					}
					while ((num & 1U) == 0U)
					{
						num >>= 1;
					}
					if (num2 >= num)
					{
						num2 = num2 - num >> 1;
					}
					else
					{
						num = num - num2 >> 1;
					}
				}
				return num << num3;
			}

			// Token: 0x060003EB RID: 1003 RVA: 0x00015408 File Offset: 0x00013608
			public static uint modInverse(BigInteger bi, uint modulus)
			{
				uint num = modulus;
				uint num2 = bi % modulus;
				uint num3 = 0U;
				uint num4 = 1U;
				while (num2 != 0U)
				{
					if (num2 == 1U)
					{
						return num4;
					}
					num3 += num / num2 * num4;
					num %= num2;
					if (num == 0U)
					{
						break;
					}
					if (num == 1U)
					{
						return modulus - num3;
					}
					num4 += num2 / num * num3;
					num2 %= num;
				}
				return 0U;
			}

			// Token: 0x060003EC RID: 1004 RVA: 0x00015454 File Offset: 0x00013654
			public static BigInteger modInverse(BigInteger bi, BigInteger modulus)
			{
				if (modulus.length == 1U)
				{
					return BigInteger.Kernel.modInverse(bi, modulus.data[0]);
				}
				BigInteger[] array = new BigInteger[]
				{
					0,
					1
				};
				BigInteger[] array2 = new BigInteger[2];
				BigInteger[] array3 = new BigInteger[]
				{
					0,
					0
				};
				int num = 0;
				BigInteger bi2 = modulus;
				BigInteger bigInteger = bi;
				BigInteger.ModulusRing modulusRing = new BigInteger.ModulusRing(modulus);
				while (bigInteger != 0U)
				{
					if (num > 1)
					{
						BigInteger bigInteger2 = modulusRing.Difference(array[0], array[1] * array2[0]);
						array[0] = array[1];
						array[1] = bigInteger2;
					}
					BigInteger[] array4 = BigInteger.Kernel.multiByteDivide(bi2, bigInteger);
					array2[0] = array2[1];
					array2[1] = array4[0];
					array3[0] = array3[1];
					array3[1] = array4[1];
					bi2 = bigInteger;
					bigInteger = array4[1];
					num++;
				}
				if (array3[0] != 1U)
				{
					throw new ArithmeticException("No inverse!");
				}
				return modulusRing.Difference(array[0], array[1] * array2[0]);
			}

			// Token: 0x060003ED RID: 1005 RVA: 0x0000259F File Offset: 0x0000079F
			public Kernel()
			{
			}
		}
	}
}
