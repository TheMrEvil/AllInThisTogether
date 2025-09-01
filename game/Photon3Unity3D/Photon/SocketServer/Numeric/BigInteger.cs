using System;

namespace Photon.SocketServer.Numeric
{
	// Token: 0x0200004C RID: 76
	internal class BigInteger
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x0001C1C3 File Offset: 0x0001A3C3
		public BigInteger()
		{
			this.data = new uint[70];
			this.dataLength = 1;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0001C1E8 File Offset: 0x0001A3E8
		public BigInteger(long value)
		{
			this.data = new uint[70];
			long num = value;
			this.dataLength = 0;
			while (value != 0L && this.dataLength < 70)
			{
				this.data[this.dataLength] = (uint)(value & (long)((ulong)-1));
				value >>= 32;
				this.dataLength++;
			}
			bool flag = num > 0L;
			if (flag)
			{
				bool flag2 = value != 0L || (this.data[69] & 2147483648U) > 0U;
				if (flag2)
				{
					throw new ArithmeticException("Positive overflow in constructor.");
				}
			}
			else
			{
				bool flag3 = num < 0L;
				if (flag3)
				{
					bool flag4 = value != -1L || (this.data[this.dataLength - 1] & 2147483648U) == 0U;
					if (flag4)
					{
						throw new ArithmeticException("Negative underflow in constructor.");
					}
				}
			}
			bool flag5 = this.dataLength == 0;
			if (flag5)
			{
				this.dataLength = 1;
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001C2E0 File Offset: 0x0001A4E0
		public BigInteger(ulong value)
		{
			this.data = new uint[70];
			this.dataLength = 0;
			while (value != 0UL && this.dataLength < 70)
			{
				this.data[this.dataLength] = (uint)(value & (ulong)-1);
				value >>= 32;
				this.dataLength++;
			}
			bool flag = value != 0UL || (this.data[69] & 2147483648U) > 0U;
			if (flag)
			{
				throw new ArithmeticException("Positive overflow in constructor.");
			}
			bool flag2 = this.dataLength == 0;
			if (flag2)
			{
				this.dataLength = 1;
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0001C38C File Offset: 0x0001A58C
		public BigInteger(BigInteger bi)
		{
			this.data = new uint[70];
			this.dataLength = bi.dataLength;
			for (int i = 0; i < this.dataLength; i++)
			{
				this.data[i] = bi.data[i];
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001C3E8 File Offset: 0x0001A5E8
		public BigInteger(string value, int radix)
		{
			BigInteger bi = new BigInteger(1L);
			BigInteger bigInteger = new BigInteger();
			value = value.ToUpper().Trim();
			int num = 0;
			bool flag = value[0] == '-';
			if (flag)
			{
				num = 1;
			}
			for (int i = value.Length - 1; i >= num; i--)
			{
				int num2 = (int)value[i];
				bool flag2 = num2 >= 48 && num2 <= 57;
				if (flag2)
				{
					num2 -= 48;
				}
				else
				{
					bool flag3 = num2 >= 65 && num2 <= 90;
					if (flag3)
					{
						num2 = num2 - 65 + 10;
					}
					else
					{
						num2 = 9999999;
					}
				}
				bool flag4 = num2 >= radix;
				if (flag4)
				{
					throw new ArithmeticException("Invalid string in constructor.");
				}
				bool flag5 = value[0] == '-';
				if (flag5)
				{
					num2 = -num2;
				}
				bigInteger += bi * num2;
				bool flag6 = i - 1 >= num;
				if (flag6)
				{
					bi *= radix;
				}
			}
			bool flag7 = value[0] == '-';
			if (flag7)
			{
				bool flag8 = (bigInteger.data[69] & 2147483648U) == 0U;
				if (flag8)
				{
					throw new ArithmeticException("Negative underflow in constructor.");
				}
			}
			else
			{
				bool flag9 = (bigInteger.data[69] & 2147483648U) > 0U;
				if (flag9)
				{
					throw new ArithmeticException("Positive overflow in constructor.");
				}
			}
			this.data = new uint[70];
			for (int j = 0; j < bigInteger.dataLength; j++)
			{
				this.data[j] = bigInteger.data[j];
			}
			this.dataLength = bigInteger.dataLength;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0001C5B8 File Offset: 0x0001A7B8
		public BigInteger(byte[] inData)
		{
			this.dataLength = inData.Length >> 2;
			int num = inData.Length & 3;
			bool flag = num != 0;
			if (flag)
			{
				this.dataLength++;
			}
			bool flag2 = this.dataLength > 70;
			if (flag2)
			{
				throw new ArithmeticException("Byte overflow in constructor.");
			}
			this.data = new uint[70];
			int i = inData.Length - 1;
			int num2 = 0;
			while (i >= 3)
			{
				this.data[num2] = (uint)(((int)inData[i - 3] << 24) + ((int)inData[i - 2] << 16) + ((int)inData[i - 1] << 8) + (int)inData[i]);
				i -= 4;
				num2++;
			}
			bool flag3 = num == 1;
			if (flag3)
			{
				this.data[this.dataLength - 1] = (uint)inData[0];
			}
			else
			{
				bool flag4 = num == 2;
				if (flag4)
				{
					this.data[this.dataLength - 1] = (uint)(((int)inData[0] << 8) + (int)inData[1]);
				}
				else
				{
					bool flag5 = num == 3;
					if (flag5)
					{
						this.data[this.dataLength - 1] = (uint)(((int)inData[0] << 16) + ((int)inData[1] << 8) + (int)inData[2]);
					}
				}
			}
			while (this.dataLength > 1 && this.data[this.dataLength - 1] == 0U)
			{
				this.dataLength--;
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0001C70C File Offset: 0x0001A90C
		public BigInteger(byte[] inData, int inLen)
		{
			this.dataLength = inLen >> 2;
			int num = inLen & 3;
			bool flag = num != 0;
			if (flag)
			{
				this.dataLength++;
			}
			bool flag2 = this.dataLength > 70 || inLen > inData.Length;
			if (flag2)
			{
				throw new ArithmeticException("Byte overflow in constructor.");
			}
			this.data = new uint[70];
			int i = inLen - 1;
			int num2 = 0;
			while (i >= 3)
			{
				this.data[num2] = (uint)(((int)inData[i - 3] << 24) + ((int)inData[i - 2] << 16) + ((int)inData[i - 1] << 8) + (int)inData[i]);
				i -= 4;
				num2++;
			}
			bool flag3 = num == 1;
			if (flag3)
			{
				this.data[this.dataLength - 1] = (uint)inData[0];
			}
			else
			{
				bool flag4 = num == 2;
				if (flag4)
				{
					this.data[this.dataLength - 1] = (uint)(((int)inData[0] << 8) + (int)inData[1]);
				}
				else
				{
					bool flag5 = num == 3;
					if (flag5)
					{
						this.data[this.dataLength - 1] = (uint)(((int)inData[0] << 16) + ((int)inData[1] << 8) + (int)inData[2]);
					}
				}
			}
			bool flag6 = this.dataLength == 0;
			if (flag6)
			{
				this.dataLength = 1;
			}
			while (this.dataLength > 1 && this.data[this.dataLength - 1] == 0U)
			{
				this.dataLength--;
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0001C878 File Offset: 0x0001AA78
		public BigInteger(uint[] inData)
		{
			this.dataLength = inData.Length;
			bool flag = this.dataLength > 70;
			if (flag)
			{
				throw new ArithmeticException("Byte overflow in constructor.");
			}
			this.data = new uint[70];
			int i = this.dataLength - 1;
			int num = 0;
			while (i >= 0)
			{
				this.data[num] = inData[i];
				i--;
				num++;
			}
			while (this.dataLength > 1 && this.data[this.dataLength - 1] == 0U)
			{
				this.dataLength--;
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0001C924 File Offset: 0x0001AB24
		public static implicit operator BigInteger(long value)
		{
			return new BigInteger(value);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0001C93C File Offset: 0x0001AB3C
		public static implicit operator BigInteger(ulong value)
		{
			return new BigInteger(value);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001C954 File Offset: 0x0001AB54
		public static implicit operator BigInteger(int value)
		{
			return new BigInteger((long)value);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001C970 File Offset: 0x0001AB70
		public static implicit operator BigInteger(uint value)
		{
			return new BigInteger((ulong)value);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0001C98C File Offset: 0x0001AB8C
		public static BigInteger operator +(BigInteger bi1, BigInteger bi2)
		{
			BigInteger bigInteger = new BigInteger();
			bigInteger.dataLength = ((bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength);
			long num = 0L;
			for (int i = 0; i < bigInteger.dataLength; i++)
			{
				long num2 = (long)((ulong)bi1.data[i] + (ulong)bi2.data[i] + (ulong)num);
				num = num2 >> 32;
				bigInteger.data[i] = (uint)(num2 & (long)((ulong)-1));
			}
			bool flag = num != 0L && bigInteger.dataLength < 70;
			if (flag)
			{
				bigInteger.data[bigInteger.dataLength] = (uint)num;
				bigInteger.dataLength++;
			}
			while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0U)
			{
				bigInteger.dataLength--;
			}
			int num3 = 69;
			bool flag2 = (bi1.data[num3] & 2147483648U) == (bi2.data[num3] & 2147483648U) && (bigInteger.data[num3] & 2147483648U) != (bi1.data[num3] & 2147483648U);
			if (flag2)
			{
				throw new ArithmeticException();
			}
			return bigInteger;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0001CACC File Offset: 0x0001ACCC
		public static BigInteger operator ++(BigInteger bi1)
		{
			BigInteger bigInteger = new BigInteger(bi1);
			long num = 1L;
			int num2 = 0;
			while (num != 0L && num2 < 70)
			{
				long num3 = (long)((ulong)bigInteger.data[num2]);
				num3 += 1L;
				bigInteger.data[num2] = (uint)(num3 & (long)((ulong)-1));
				num = num3 >> 32;
				num2++;
			}
			bool flag = num2 > bigInteger.dataLength;
			if (flag)
			{
				bigInteger.dataLength = num2;
			}
			else
			{
				while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0U)
				{
					bigInteger.dataLength--;
				}
			}
			int num4 = 69;
			bool flag2 = (bi1.data[num4] & 2147483648U) == 0U && (bigInteger.data[num4] & 2147483648U) != (bi1.data[num4] & 2147483648U);
			if (flag2)
			{
				throw new ArithmeticException("Overflow in ++.");
			}
			return bigInteger;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0001CBC4 File Offset: 0x0001ADC4
		public static BigInteger operator -(BigInteger bi1, BigInteger bi2)
		{
			BigInteger bigInteger = new BigInteger();
			bigInteger.dataLength = ((bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength);
			long num = 0L;
			for (int i = 0; i < bigInteger.dataLength; i++)
			{
				long num2 = (long)((ulong)bi1.data[i] - (ulong)bi2.data[i] - (ulong)num);
				bigInteger.data[i] = (uint)(num2 & (long)((ulong)-1));
				bool flag = num2 < 0L;
				if (flag)
				{
					num = 1L;
				}
				else
				{
					num = 0L;
				}
			}
			bool flag2 = num != 0L;
			if (flag2)
			{
				for (int j = bigInteger.dataLength; j < 70; j++)
				{
					bigInteger.data[j] = uint.MaxValue;
				}
				bigInteger.dataLength = 70;
			}
			while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0U)
			{
				bigInteger.dataLength--;
			}
			int num3 = 69;
			bool flag3 = (bi1.data[num3] & 2147483648U) != (bi2.data[num3] & 2147483648U) && (bigInteger.data[num3] & 2147483648U) != (bi1.data[num3] & 2147483648U);
			if (flag3)
			{
				throw new ArithmeticException();
			}
			return bigInteger;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0001CD18 File Offset: 0x0001AF18
		public static BigInteger operator --(BigInteger bi1)
		{
			BigInteger bigInteger = new BigInteger(bi1);
			bool flag = true;
			int num = 0;
			while (flag && num < 70)
			{
				long num2 = (long)((ulong)bigInteger.data[num]);
				num2 -= 1L;
				bigInteger.data[num] = (uint)(num2 & (long)((ulong)-1));
				bool flag2 = num2 >= 0L;
				if (flag2)
				{
					flag = false;
				}
				num++;
			}
			bool flag3 = num > bigInteger.dataLength;
			if (flag3)
			{
				bigInteger.dataLength = num;
			}
			while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0U)
			{
				bigInteger.dataLength--;
			}
			int num3 = 69;
			bool flag4 = (bi1.data[num3] & 2147483648U) != 0U && (bigInteger.data[num3] & 2147483648U) != (bi1.data[num3] & 2147483648U);
			if (flag4)
			{
				throw new ArithmeticException("Underflow in --.");
			}
			return bigInteger;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0001CE14 File Offset: 0x0001B014
		public static BigInteger operator *(BigInteger bi1, BigInteger bi2)
		{
			int num = 69;
			bool flag = false;
			bool flag2 = false;
			try
			{
				bool flag3 = (bi1.data[num] & 2147483648U) > 0U;
				if (flag3)
				{
					flag = true;
					bi1 = -bi1;
				}
				bool flag4 = (bi2.data[num] & 2147483648U) > 0U;
				if (flag4)
				{
					flag2 = true;
					bi2 = -bi2;
				}
			}
			catch (Exception)
			{
			}
			BigInteger bigInteger = new BigInteger();
			try
			{
				for (int i = 0; i < bi1.dataLength; i++)
				{
					bool flag5 = bi1.data[i] == 0U;
					if (!flag5)
					{
						ulong num2 = 0UL;
						int j = 0;
						int num3 = i;
						while (j < bi2.dataLength)
						{
							ulong num4 = (ulong)bi1.data[i] * (ulong)bi2.data[j] + (ulong)bigInteger.data[num3] + num2;
							bigInteger.data[num3] = (uint)(num4 & (ulong)-1);
							num2 = num4 >> 32;
							j++;
							num3++;
						}
						bool flag6 = num2 > 0UL;
						if (flag6)
						{
							bigInteger.data[i + bi2.dataLength] = (uint)num2;
						}
					}
				}
			}
			catch (Exception)
			{
				throw new ArithmeticException("Multiplication overflow.");
			}
			bigInteger.dataLength = bi1.dataLength + bi2.dataLength;
			bool flag7 = bigInteger.dataLength > 70;
			if (flag7)
			{
				bigInteger.dataLength = 70;
			}
			while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0U)
			{
				bigInteger.dataLength--;
			}
			bool flag8 = (bigInteger.data[num] & 2147483648U) > 0U;
			if (flag8)
			{
				bool flag9 = flag != flag2 && bigInteger.data[num] == 2147483648U;
				if (flag9)
				{
					bool flag10 = bigInteger.dataLength == 1;
					if (flag10)
					{
						return bigInteger;
					}
					bool flag11 = true;
					int num5 = 0;
					while (num5 < bigInteger.dataLength - 1 && flag11)
					{
						bool flag12 = bigInteger.data[num5] > 0U;
						if (flag12)
						{
							flag11 = false;
						}
						num5++;
					}
					bool flag13 = flag11;
					if (flag13)
					{
						return bigInteger;
					}
				}
				throw new ArithmeticException("Multiplication overflow.");
			}
			bool flag14 = flag != flag2;
			BigInteger result;
			if (flag14)
			{
				result = -bigInteger;
			}
			else
			{
				result = bigInteger;
			}
			return result;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0001D088 File Offset: 0x0001B288
		public static BigInteger operator <<(BigInteger bi1, int shiftVal)
		{
			BigInteger bigInteger = new BigInteger(bi1);
			bigInteger.dataLength = BigInteger.shiftLeft(bigInteger.data, shiftVal);
			return bigInteger;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0001D0B4 File Offset: 0x0001B2B4
		private static int shiftLeft(uint[] buffer, int shiftVal)
		{
			int num = 32;
			int num2 = buffer.Length;
			while (num2 > 1 && buffer[num2 - 1] == 0U)
			{
				num2--;
			}
			for (int i = shiftVal; i > 0; i -= num)
			{
				bool flag = i < num;
				if (flag)
				{
					num = i;
				}
				ulong num3 = 0UL;
				for (int j = 0; j < num2; j++)
				{
					ulong num4 = (ulong)buffer[j] << num;
					num4 |= num3;
					buffer[j] = (uint)(num4 & (ulong)-1);
					num3 = num4 >> 32;
				}
				bool flag2 = num3 > 0UL;
				if (flag2)
				{
					bool flag3 = num2 + 1 <= buffer.Length;
					if (flag3)
					{
						buffer[num2] = (uint)num3;
						num2++;
					}
				}
			}
			return num2;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0001D178 File Offset: 0x0001B378
		public static BigInteger operator >>(BigInteger bi1, int shiftVal)
		{
			BigInteger bigInteger = new BigInteger(bi1);
			bigInteger.dataLength = BigInteger.shiftRight(bigInteger.data, shiftVal);
			bool flag = (bi1.data[69] & 2147483648U) > 0U;
			if (flag)
			{
				for (int i = 69; i >= bigInteger.dataLength; i--)
				{
					bigInteger.data[i] = uint.MaxValue;
				}
				uint num = 2147483648U;
				for (int j = 0; j < 32; j++)
				{
					bool flag2 = (bigInteger.data[bigInteger.dataLength - 1] & num) > 0U;
					if (flag2)
					{
						break;
					}
					bigInteger.data[bigInteger.dataLength - 1] |= num;
					num >>= 1;
				}
				bigInteger.dataLength = 70;
			}
			return bigInteger;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001D244 File Offset: 0x0001B444
		private static int shiftRight(uint[] buffer, int shiftVal)
		{
			int num = 32;
			int num2 = 0;
			int num3 = buffer.Length;
			while (num3 > 1 && buffer[num3 - 1] == 0U)
			{
				num3--;
			}
			for (int i = shiftVal; i > 0; i -= num)
			{
				bool flag = i < num;
				if (flag)
				{
					num = i;
					num2 = 32 - num;
				}
				ulong num4 = 0UL;
				for (int j = num3 - 1; j >= 0; j--)
				{
					ulong num5 = (ulong)buffer[j] >> num;
					num5 |= num4;
					num4 = (ulong)buffer[j] << num2;
					buffer[j] = (uint)num5;
				}
			}
			while (num3 > 1 && buffer[num3 - 1] == 0U)
			{
				num3--;
			}
			return num3;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001D30C File Offset: 0x0001B50C
		public static BigInteger operator ~(BigInteger bi1)
		{
			BigInteger bigInteger = new BigInteger(bi1);
			for (int i = 0; i < 70; i++)
			{
				bigInteger.data[i] = ~bi1.data[i];
			}
			bigInteger.dataLength = 70;
			while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0U)
			{
				bigInteger.dataLength--;
			}
			return bigInteger;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0001D384 File Offset: 0x0001B584
		public static BigInteger operator -(BigInteger bi1)
		{
			bool flag = bi1.dataLength == 1 && bi1.data[0] == 0U;
			BigInteger result;
			if (flag)
			{
				result = new BigInteger();
			}
			else
			{
				BigInteger bigInteger = new BigInteger(bi1);
				for (int i = 0; i < 70; i++)
				{
					bigInteger.data[i] = ~bi1.data[i];
				}
				long num = 1L;
				int num2 = 0;
				while (num != 0L && num2 < 70)
				{
					long num3 = (long)((ulong)bigInteger.data[num2]);
					num3 += 1L;
					bigInteger.data[num2] = (uint)(num3 & (long)((ulong)-1));
					num = num3 >> 32;
					num2++;
				}
				bool flag2 = (bi1.data[69] & 2147483648U) == (bigInteger.data[69] & 2147483648U);
				if (flag2)
				{
					throw new ArithmeticException("Overflow in negation.\n");
				}
				bigInteger.dataLength = 70;
				while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0U)
				{
					bigInteger.dataLength--;
				}
				result = bigInteger;
			}
			return result;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0001D4A0 File Offset: 0x0001B6A0
		public static bool operator ==(BigInteger bi1, BigInteger bi2)
		{
			return bi1.Equals(bi2);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0001D4BC File Offset: 0x0001B6BC
		public static bool operator !=(BigInteger bi1, BigInteger bi2)
		{
			return !bi1.Equals(bi2);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001D4D8 File Offset: 0x0001B6D8
		public override bool Equals(object o)
		{
			BigInteger bigInteger = (BigInteger)o;
			bool flag = this.dataLength != bigInteger.dataLength;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < this.dataLength; i++)
				{
					bool flag2 = this.data[i] != bigInteger.data[i];
					if (flag2)
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0001D544 File Offset: 0x0001B744
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001D564 File Offset: 0x0001B764
		public static bool operator >(BigInteger bi1, BigInteger bi2)
		{
			int num = 69;
			bool flag = (bi1.data[num] & 2147483648U) != 0U && (bi2.data[num] & 2147483648U) == 0U;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = (bi1.data[num] & 2147483648U) == 0U && (bi2.data[num] & 2147483648U) > 0U;
				if (flag2)
				{
					result = true;
				}
				else
				{
					int num2 = (bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength;
					num = num2 - 1;
					while (num >= 0 && bi1.data[num] == bi2.data[num])
					{
						num--;
					}
					bool flag3 = num >= 0;
					if (flag3)
					{
						bool flag4 = bi1.data[num] > bi2.data[num];
						result = flag4;
					}
					else
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001D64C File Offset: 0x0001B84C
		public static bool operator <(BigInteger bi1, BigInteger bi2)
		{
			int num = 69;
			bool flag = (bi1.data[num] & 2147483648U) != 0U && (bi2.data[num] & 2147483648U) == 0U;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = (bi1.data[num] & 2147483648U) == 0U && (bi2.data[num] & 2147483648U) > 0U;
				if (flag2)
				{
					result = false;
				}
				else
				{
					int num2 = (bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength;
					num = num2 - 1;
					while (num >= 0 && bi1.data[num] == bi2.data[num])
					{
						num--;
					}
					bool flag3 = num >= 0;
					if (flag3)
					{
						bool flag4 = bi1.data[num] < bi2.data[num];
						result = flag4;
					}
					else
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001D734 File Offset: 0x0001B934
		public static bool operator >=(BigInteger bi1, BigInteger bi2)
		{
			return bi1 == bi2 || bi1 > bi2;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001D75C File Offset: 0x0001B95C
		public static bool operator <=(BigInteger bi1, BigInteger bi2)
		{
			return bi1 == bi2 || bi1 < bi2;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001D784 File Offset: 0x0001B984
		private static void multiByteDivide(BigInteger bi1, BigInteger bi2, BigInteger outQuotient, BigInteger outRemainder)
		{
			uint[] array = new uint[70];
			int num = bi1.dataLength + 1;
			uint[] array2 = new uint[num];
			uint num2 = 2147483648U;
			uint num3 = bi2.data[bi2.dataLength - 1];
			int num4 = 0;
			int num5 = 0;
			while (num2 != 0U && (num3 & num2) == 0U)
			{
				num4++;
				num2 >>= 1;
			}
			for (int i = 0; i < bi1.dataLength; i++)
			{
				array2[i] = bi1.data[i];
			}
			BigInteger.shiftLeft(array2, num4);
			bi2 <<= num4;
			int j = num - bi2.dataLength;
			int num6 = num - 1;
			ulong num7 = (ulong)bi2.data[bi2.dataLength - 1];
			ulong num8 = (ulong)bi2.data[bi2.dataLength - 2];
			int num9 = bi2.dataLength + 1;
			uint[] array3 = new uint[num9];
			while (j > 0)
			{
				ulong num10 = ((ulong)array2[num6] << 32) + (ulong)array2[num6 - 1];
				ulong num11 = num10 / num7;
				ulong num12 = num10 % num7;
				bool flag = false;
				while (!flag)
				{
					flag = true;
					bool flag2 = num11 == 4294967296UL || num11 * num8 > (num12 << 32) + (ulong)array2[num6 - 2];
					if (flag2)
					{
						num11 -= 1UL;
						num12 += num7;
						bool flag3 = num12 < 4294967296UL;
						if (flag3)
						{
							flag = false;
						}
					}
				}
				for (int k = 0; k < num9; k++)
				{
					array3[k] = array2[num6 - k];
				}
				BigInteger bigInteger = new BigInteger(array3);
				BigInteger bigInteger2 = bi2 * (long)num11;
				while (bigInteger2 > bigInteger)
				{
					num11 -= 1UL;
					bigInteger2 -= bi2;
				}
				BigInteger bigInteger3 = bigInteger - bigInteger2;
				for (int l = 0; l < num9; l++)
				{
					array2[num6 - l] = bigInteger3.data[bi2.dataLength - l];
				}
				array[num5++] = (uint)num11;
				num6--;
				j--;
			}
			outQuotient.dataLength = num5;
			int m = 0;
			int n = outQuotient.dataLength - 1;
			while (n >= 0)
			{
				outQuotient.data[m] = array[n];
				n--;
				m++;
			}
			while (m < 70)
			{
				outQuotient.data[m] = 0U;
				m++;
			}
			while (outQuotient.dataLength > 1 && outQuotient.data[outQuotient.dataLength - 1] == 0U)
			{
				outQuotient.dataLength--;
			}
			bool flag4 = outQuotient.dataLength == 0;
			if (flag4)
			{
				outQuotient.dataLength = 1;
			}
			outRemainder.dataLength = BigInteger.shiftRight(array2, num4);
			for (m = 0; m < outRemainder.dataLength; m++)
			{
				outRemainder.data[m] = array2[m];
			}
			while (m < 70)
			{
				outRemainder.data[m] = 0U;
				m++;
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0001DAB0 File Offset: 0x0001BCB0
		private static void singleByteDivide(BigInteger bi1, BigInteger bi2, BigInteger outQuotient, BigInteger outRemainder)
		{
			uint[] array = new uint[70];
			int num = 0;
			for (int i = 0; i < 70; i++)
			{
				outRemainder.data[i] = bi1.data[i];
			}
			outRemainder.dataLength = bi1.dataLength;
			while (outRemainder.dataLength > 1 && outRemainder.data[outRemainder.dataLength - 1] == 0U)
			{
				outRemainder.dataLength--;
			}
			ulong num2 = (ulong)bi2.data[0];
			int j = outRemainder.dataLength - 1;
			ulong num3 = (ulong)outRemainder.data[j];
			bool flag = num3 >= num2;
			if (flag)
			{
				ulong num4 = num3 / num2;
				array[num++] = (uint)num4;
				outRemainder.data[j] = (uint)(num3 % num2);
			}
			j--;
			while (j >= 0)
			{
				num3 = ((ulong)outRemainder.data[j + 1] << 32) + (ulong)outRemainder.data[j];
				ulong num5 = num3 / num2;
				array[num++] = (uint)num5;
				outRemainder.data[j + 1] = 0U;
				outRemainder.data[j--] = (uint)(num3 % num2);
			}
			outQuotient.dataLength = num;
			int k = 0;
			int l = outQuotient.dataLength - 1;
			while (l >= 0)
			{
				outQuotient.data[k] = array[l];
				l--;
				k++;
			}
			while (k < 70)
			{
				outQuotient.data[k] = 0U;
				k++;
			}
			while (outQuotient.dataLength > 1 && outQuotient.data[outQuotient.dataLength - 1] == 0U)
			{
				outQuotient.dataLength--;
			}
			bool flag2 = outQuotient.dataLength == 0;
			if (flag2)
			{
				outQuotient.dataLength = 1;
			}
			while (outRemainder.dataLength > 1 && outRemainder.data[outRemainder.dataLength - 1] == 0U)
			{
				outRemainder.dataLength--;
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0001DCB0 File Offset: 0x0001BEB0
		public static BigInteger operator /(BigInteger bi1, BigInteger bi2)
		{
			BigInteger bigInteger = new BigInteger();
			BigInteger outRemainder = new BigInteger();
			int num = 69;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = (bi1.data[num] & 2147483648U) > 0U;
			if (flag3)
			{
				bi1 = -bi1;
				flag2 = true;
			}
			bool flag4 = (bi2.data[num] & 2147483648U) > 0U;
			if (flag4)
			{
				bi2 = -bi2;
				flag = true;
			}
			bool flag5 = bi1 < bi2;
			BigInteger result;
			if (flag5)
			{
				result = bigInteger;
			}
			else
			{
				bool flag6 = bi2.dataLength == 1;
				if (flag6)
				{
					BigInteger.singleByteDivide(bi1, bi2, bigInteger, outRemainder);
				}
				else
				{
					BigInteger.multiByteDivide(bi1, bi2, bigInteger, outRemainder);
				}
				bool flag7 = flag2 != flag;
				if (flag7)
				{
					result = -bigInteger;
				}
				else
				{
					result = bigInteger;
				}
			}
			return result;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0001DD74 File Offset: 0x0001BF74
		public static BigInteger operator %(BigInteger bi1, BigInteger bi2)
		{
			BigInteger outQuotient = new BigInteger();
			BigInteger bigInteger = new BigInteger(bi1);
			int num = 69;
			bool flag = false;
			bool flag2 = (bi1.data[num] & 2147483648U) > 0U;
			if (flag2)
			{
				bi1 = -bi1;
				flag = true;
			}
			bool flag3 = (bi2.data[num] & 2147483648U) > 0U;
			if (flag3)
			{
				bi2 = -bi2;
			}
			bool flag4 = bi1 < bi2;
			BigInteger result;
			if (flag4)
			{
				result = bigInteger;
			}
			else
			{
				bool flag5 = bi2.dataLength == 1;
				if (flag5)
				{
					BigInteger.singleByteDivide(bi1, bi2, outQuotient, bigInteger);
				}
				else
				{
					BigInteger.multiByteDivide(bi1, bi2, outQuotient, bigInteger);
				}
				bool flag6 = flag;
				if (flag6)
				{
					result = -bigInteger;
				}
				else
				{
					result = bigInteger;
				}
			}
			return result;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0001DE28 File Offset: 0x0001C028
		public static BigInteger operator &(BigInteger bi1, BigInteger bi2)
		{
			BigInteger bigInteger = new BigInteger();
			int num = (bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength;
			for (int i = 0; i < num; i++)
			{
				uint num2 = bi1.data[i] & bi2.data[i];
				bigInteger.data[i] = num2;
			}
			bigInteger.dataLength = 70;
			while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0U)
			{
				bigInteger.dataLength--;
			}
			return bigInteger;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001DECC File Offset: 0x0001C0CC
		public static BigInteger operator |(BigInteger bi1, BigInteger bi2)
		{
			BigInteger bigInteger = new BigInteger();
			int num = (bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength;
			for (int i = 0; i < num; i++)
			{
				uint num2 = bi1.data[i] | bi2.data[i];
				bigInteger.data[i] = num2;
			}
			bigInteger.dataLength = 70;
			while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0U)
			{
				bigInteger.dataLength--;
			}
			return bigInteger;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001DF70 File Offset: 0x0001C170
		public static BigInteger operator ^(BigInteger bi1, BigInteger bi2)
		{
			BigInteger bigInteger = new BigInteger();
			int num = (bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength;
			for (int i = 0; i < num; i++)
			{
				uint num2 = bi1.data[i] ^ bi2.data[i];
				bigInteger.data[i] = num2;
			}
			bigInteger.dataLength = 70;
			while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0U)
			{
				bigInteger.dataLength--;
			}
			return bigInteger;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001E014 File Offset: 0x0001C214
		public BigInteger max(BigInteger bi)
		{
			bool flag = this > bi;
			BigInteger result;
			if (flag)
			{
				result = new BigInteger(this);
			}
			else
			{
				result = new BigInteger(bi);
			}
			return result;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0001E040 File Offset: 0x0001C240
		public BigInteger min(BigInteger bi)
		{
			bool flag = this < bi;
			BigInteger result;
			if (flag)
			{
				result = new BigInteger(this);
			}
			else
			{
				result = new BigInteger(bi);
			}
			return result;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001E06C File Offset: 0x0001C26C
		public BigInteger abs()
		{
			bool flag = (this.data[69] & 2147483648U) > 0U;
			BigInteger result;
			if (flag)
			{
				result = -this;
			}
			else
			{
				result = new BigInteger(this);
			}
			return result;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001E0A4 File Offset: 0x0001C2A4
		public override string ToString()
		{
			return this.ToString(10);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001E0C0 File Offset: 0x0001C2C0
		public string ToString(int radix)
		{
			bool flag = radix < 2 || radix > 36;
			if (flag)
			{
				throw new ArgumentException("Radix must be >= 2 and <= 36");
			}
			string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			string text2 = "";
			BigInteger bigInteger = this;
			bool flag2 = false;
			bool flag3 = (bigInteger.data[69] & 2147483648U) > 0U;
			if (flag3)
			{
				flag2 = true;
				try
				{
					bigInteger = -bigInteger;
				}
				catch (Exception)
				{
				}
			}
			BigInteger bigInteger2 = new BigInteger();
			BigInteger bigInteger3 = new BigInteger();
			BigInteger bi = new BigInteger((long)radix);
			bool flag4 = bigInteger.dataLength == 1 && bigInteger.data[0] == 0U;
			if (flag4)
			{
				text2 = "0";
			}
			else
			{
				while (bigInteger.dataLength > 1 || (bigInteger.dataLength == 1 && bigInteger.data[0] > 0U))
				{
					BigInteger.singleByteDivide(bigInteger, bi, bigInteger2, bigInteger3);
					bool flag5 = bigInteger3.data[0] < 10U;
					if (flag5)
					{
						text2 = bigInteger3.data[0].ToString() + text2;
					}
					else
					{
						text2 = text[(int)(bigInteger3.data[0] - 10U)].ToString() + text2;
					}
					bigInteger = bigInteger2;
				}
				bool flag6 = flag2;
				if (flag6)
				{
					text2 = "-" + text2;
				}
			}
			return text2;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001E224 File Offset: 0x0001C424
		public string ToHexString()
		{
			string text = this.data[this.dataLength - 1].ToString("X");
			for (int i = this.dataLength - 2; i >= 0; i--)
			{
				text += this.data[i].ToString("X8");
			}
			return text;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001E290 File Offset: 0x0001C490
		public BigInteger ModPow(BigInteger exp, BigInteger n)
		{
			bool flag = (exp.data[69] & 2147483648U) > 0U;
			if (flag)
			{
				throw new ArithmeticException("Positive exponents only.");
			}
			BigInteger bigInteger = 1;
			bool flag2 = false;
			bool flag3 = (this.data[69] & 2147483648U) > 0U;
			BigInteger bigInteger2;
			if (flag3)
			{
				bigInteger2 = -this % n;
				flag2 = true;
			}
			else
			{
				bigInteger2 = this % n;
			}
			bool flag4 = (n.data[69] & 2147483648U) > 0U;
			if (flag4)
			{
				n = -n;
			}
			BigInteger bigInteger3 = new BigInteger();
			int num = n.dataLength << 1;
			bigInteger3.data[num] = 1U;
			bigInteger3.dataLength = num + 1;
			bigInteger3 /= n;
			int num2 = exp.bitCount();
			int num3 = 0;
			for (int i = 0; i < exp.dataLength; i++)
			{
				uint num4 = 1U;
				int j = 0;
				while (j < 32)
				{
					bool flag5 = (exp.data[i] & num4) > 0U;
					if (flag5)
					{
						bigInteger = this.BarrettReduction(bigInteger * bigInteger2, n, bigInteger3);
					}
					num4 <<= 1;
					bigInteger2 = this.BarrettReduction(bigInteger2 * bigInteger2, n, bigInteger3);
					bool flag6 = bigInteger2.dataLength == 1 && bigInteger2.data[0] == 1U;
					if (flag6)
					{
						bool flag7 = flag2 && (exp.data[0] & 1U) > 0U;
						if (flag7)
						{
							return -bigInteger;
						}
						return bigInteger;
					}
					else
					{
						num3++;
						bool flag8 = num3 == num2;
						if (flag8)
						{
							break;
						}
						j++;
					}
				}
			}
			bool flag9 = flag2 && (exp.data[0] & 1U) > 0U;
			if (flag9)
			{
				return -bigInteger;
			}
			return bigInteger;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001E458 File Offset: 0x0001C658
		private BigInteger BarrettReduction(BigInteger x, BigInteger n, BigInteger constant)
		{
			int num = n.dataLength;
			int num2 = num + 1;
			int num3 = num - 1;
			BigInteger bigInteger = new BigInteger();
			int i = num3;
			int num4 = 0;
			while (i < x.dataLength)
			{
				bigInteger.data[num4] = x.data[i];
				i++;
				num4++;
			}
			bigInteger.dataLength = x.dataLength - num3;
			bool flag = bigInteger.dataLength <= 0;
			if (flag)
			{
				bigInteger.dataLength = 1;
			}
			BigInteger bigInteger2 = bigInteger * constant;
			BigInteger bigInteger3 = new BigInteger();
			int j = num2;
			int num5 = 0;
			while (j < bigInteger2.dataLength)
			{
				bigInteger3.data[num5] = bigInteger2.data[j];
				j++;
				num5++;
			}
			bigInteger3.dataLength = bigInteger2.dataLength - num2;
			bool flag2 = bigInteger3.dataLength <= 0;
			if (flag2)
			{
				bigInteger3.dataLength = 1;
			}
			BigInteger bigInteger4 = new BigInteger();
			int num6 = (x.dataLength > num2) ? num2 : x.dataLength;
			for (int k = 0; k < num6; k++)
			{
				bigInteger4.data[k] = x.data[k];
			}
			bigInteger4.dataLength = num6;
			BigInteger bigInteger5 = new BigInteger();
			for (int l = 0; l < bigInteger3.dataLength; l++)
			{
				bool flag3 = bigInteger3.data[l] == 0U;
				if (!flag3)
				{
					ulong num7 = 0UL;
					int num8 = l;
					int num9 = 0;
					while (num9 < n.dataLength && num8 < num2)
					{
						ulong num10 = (ulong)bigInteger3.data[l] * (ulong)n.data[num9] + (ulong)bigInteger5.data[num8] + num7;
						bigInteger5.data[num8] = (uint)(num10 & (ulong)-1);
						num7 = num10 >> 32;
						num9++;
						num8++;
					}
					bool flag4 = num8 < num2;
					if (flag4)
					{
						bigInteger5.data[num8] = (uint)num7;
					}
				}
			}
			bigInteger5.dataLength = num2;
			while (bigInteger5.dataLength > 1 && bigInteger5.data[bigInteger5.dataLength - 1] == 0U)
			{
				bigInteger5.dataLength--;
			}
			bigInteger4 -= bigInteger5;
			bool flag5 = (bigInteger4.data[69] & 2147483648U) > 0U;
			if (flag5)
			{
				BigInteger bigInteger6 = new BigInteger();
				bigInteger6.data[num2] = 1U;
				bigInteger6.dataLength = num2 + 1;
				bigInteger4 += bigInteger6;
			}
			while (bigInteger4 >= n)
			{
				bigInteger4 -= n;
			}
			return bigInteger4;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001E714 File Offset: 0x0001C914
		public BigInteger gcd(BigInteger bi)
		{
			bool flag = (this.data[69] & 2147483648U) > 0U;
			BigInteger bigInteger;
			if (flag)
			{
				bigInteger = -this;
			}
			else
			{
				bigInteger = this;
			}
			bool flag2 = (bi.data[69] & 2147483648U) > 0U;
			BigInteger bigInteger2;
			if (flag2)
			{
				bigInteger2 = -bi;
			}
			else
			{
				bigInteger2 = bi;
			}
			BigInteger bigInteger3 = bigInteger2;
			while (bigInteger.dataLength > 1 || (bigInteger.dataLength == 1 && bigInteger.data[0] > 0U))
			{
				bigInteger3 = bigInteger;
				bigInteger = bigInteger2 % bigInteger;
				bigInteger2 = bigInteger3;
			}
			return bigInteger3;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0001E7A8 File Offset: 0x0001C9A8
		public static BigInteger GenerateRandom(int bits)
		{
			BigInteger bigInteger = new BigInteger();
			bigInteger.genRandomBits(bits, new Random());
			return bigInteger;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001E7D0 File Offset: 0x0001C9D0
		public void genRandomBits(int bits, Random rand)
		{
			int num = bits >> 5;
			int num2 = bits & 31;
			bool flag = num2 != 0;
			if (flag)
			{
				num++;
			}
			bool flag2 = num > 70;
			if (flag2)
			{
				throw new ArithmeticException("Number of required bits > maxLength.");
			}
			for (int i = 0; i < num; i++)
			{
				this.data[i] = (uint)(rand.NextDouble() * 4294967296.0);
			}
			for (int j = num; j < 70; j++)
			{
				this.data[j] = 0U;
			}
			bool flag3 = num2 != 0;
			if (flag3)
			{
				uint num3 = 1U << num2 - 1;
				this.data[num - 1] |= num3;
				num3 = uint.MaxValue >> 32 - num2;
				this.data[num - 1] &= num3;
			}
			else
			{
				this.data[num - 1] |= 2147483648U;
			}
			this.dataLength = num;
			bool flag4 = this.dataLength == 0;
			if (flag4)
			{
				this.dataLength = 1;
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0001E8D8 File Offset: 0x0001CAD8
		public int bitCount()
		{
			while (this.dataLength > 1 && this.data[this.dataLength - 1] == 0U)
			{
				this.dataLength--;
			}
			uint num = this.data[this.dataLength - 1];
			uint num2 = 2147483648U;
			int num3 = 32;
			while (num3 > 0 && (num & num2) == 0U)
			{
				num3--;
				num2 >>= 1;
			}
			return num3 + (this.dataLength - 1 << 5);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001E964 File Offset: 0x0001CB64
		public bool FermatLittleTest(int confidence)
		{
			bool flag = (this.data[69] & 2147483648U) > 0U;
			BigInteger bigInteger;
			if (flag)
			{
				bigInteger = -this;
			}
			else
			{
				bigInteger = this;
			}
			bool flag2 = bigInteger.dataLength == 1;
			if (flag2)
			{
				bool flag3 = bigInteger.data[0] == 0U || bigInteger.data[0] == 1U;
				if (flag3)
				{
					return false;
				}
				bool flag4 = bigInteger.data[0] == 2U || bigInteger.data[0] == 3U;
				if (flag4)
				{
					return true;
				}
			}
			bool flag5 = (bigInteger.data[0] & 1U) == 0U;
			bool result;
			if (flag5)
			{
				result = false;
			}
			else
			{
				int num = bigInteger.bitCount();
				BigInteger bigInteger2 = new BigInteger();
				BigInteger exp = bigInteger - new BigInteger(1L);
				Random random = new Random();
				for (int i = 0; i < confidence; i++)
				{
					bool flag6 = false;
					while (!flag6)
					{
						int j;
						for (j = 0; j < 2; j = (int)(random.NextDouble() * (double)num))
						{
						}
						bigInteger2.genRandomBits(j, random);
						int num2 = bigInteger2.dataLength;
						bool flag7 = num2 > 1 || (num2 == 1 && bigInteger2.data[0] != 1U);
						if (flag7)
						{
							flag6 = true;
						}
					}
					BigInteger bigInteger3 = bigInteger2.gcd(bigInteger);
					bool flag8 = bigInteger3.dataLength == 1 && bigInteger3.data[0] != 1U;
					if (flag8)
					{
						return false;
					}
					BigInteger bigInteger4 = bigInteger2.ModPow(exp, bigInteger);
					int num3 = bigInteger4.dataLength;
					bool flag9 = num3 > 1 || (num3 == 1 && bigInteger4.data[0] != 1U);
					if (flag9)
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001EB2C File Offset: 0x0001CD2C
		public bool RabinMillerTest(int confidence)
		{
			bool flag = (this.data[69] & 2147483648U) > 0U;
			BigInteger bigInteger;
			if (flag)
			{
				bigInteger = -this;
			}
			else
			{
				bigInteger = this;
			}
			bool flag2 = bigInteger.dataLength == 1;
			if (flag2)
			{
				bool flag3 = bigInteger.data[0] == 0U || bigInteger.data[0] == 1U;
				if (flag3)
				{
					return false;
				}
				bool flag4 = bigInteger.data[0] == 2U || bigInteger.data[0] == 3U;
				if (flag4)
				{
					return true;
				}
			}
			bool flag5 = (bigInteger.data[0] & 1U) == 0U;
			bool result;
			if (flag5)
			{
				result = false;
			}
			else
			{
				BigInteger bigInteger2 = bigInteger - new BigInteger(1L);
				int num = 0;
				for (int i = 0; i < bigInteger2.dataLength; i++)
				{
					uint num2 = 1U;
					for (int j = 0; j < 32; j++)
					{
						bool flag6 = (bigInteger2.data[i] & num2) > 0U;
						if (flag6)
						{
							i = bigInteger2.dataLength;
							break;
						}
						num2 <<= 1;
						num++;
					}
				}
				BigInteger exp = bigInteger2 >> num;
				int num3 = bigInteger.bitCount();
				BigInteger bigInteger3 = new BigInteger();
				Random random = new Random();
				for (int k = 0; k < confidence; k++)
				{
					bool flag7 = false;
					while (!flag7)
					{
						int l;
						for (l = 0; l < 2; l = (int)(random.NextDouble() * (double)num3))
						{
						}
						bigInteger3.genRandomBits(l, random);
						int num4 = bigInteger3.dataLength;
						bool flag8 = num4 > 1 || (num4 == 1 && bigInteger3.data[0] != 1U);
						if (flag8)
						{
							flag7 = true;
						}
					}
					BigInteger bigInteger4 = bigInteger3.gcd(bigInteger);
					bool flag9 = bigInteger4.dataLength == 1 && bigInteger4.data[0] != 1U;
					if (flag9)
					{
						return false;
					}
					BigInteger bigInteger5 = bigInteger3.ModPow(exp, bigInteger);
					bool flag10 = false;
					bool flag11 = bigInteger5.dataLength == 1 && bigInteger5.data[0] == 1U;
					if (flag11)
					{
						flag10 = true;
					}
					int num5 = 0;
					while (!flag10 && num5 < num)
					{
						bool flag12 = bigInteger5 == bigInteger2;
						if (flag12)
						{
							flag10 = true;
							break;
						}
						bigInteger5 = bigInteger5 * bigInteger5 % bigInteger;
						num5++;
					}
					bool flag13 = !flag10;
					if (flag13)
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001EDB4 File Offset: 0x0001CFB4
		public bool SolovayStrassenTest(int confidence)
		{
			bool flag = (this.data[69] & 2147483648U) > 0U;
			BigInteger bigInteger;
			if (flag)
			{
				bigInteger = -this;
			}
			else
			{
				bigInteger = this;
			}
			bool flag2 = bigInteger.dataLength == 1;
			if (flag2)
			{
				bool flag3 = bigInteger.data[0] == 0U || bigInteger.data[0] == 1U;
				if (flag3)
				{
					return false;
				}
				bool flag4 = bigInteger.data[0] == 2U || bigInteger.data[0] == 3U;
				if (flag4)
				{
					return true;
				}
			}
			bool flag5 = (bigInteger.data[0] & 1U) == 0U;
			bool result;
			if (flag5)
			{
				result = false;
			}
			else
			{
				int num = bigInteger.bitCount();
				BigInteger bigInteger2 = new BigInteger();
				BigInteger bigInteger3 = bigInteger - 1;
				BigInteger exp = bigInteger3 >> 1;
				Random random = new Random();
				for (int i = 0; i < confidence; i++)
				{
					bool flag6 = false;
					while (!flag6)
					{
						int j;
						for (j = 0; j < 2; j = (int)(random.NextDouble() * (double)num))
						{
						}
						bigInteger2.genRandomBits(j, random);
						int num2 = bigInteger2.dataLength;
						bool flag7 = num2 > 1 || (num2 == 1 && bigInteger2.data[0] != 1U);
						if (flag7)
						{
							flag6 = true;
						}
					}
					BigInteger bigInteger4 = bigInteger2.gcd(bigInteger);
					bool flag8 = bigInteger4.dataLength == 1 && bigInteger4.data[0] != 1U;
					if (flag8)
					{
						return false;
					}
					BigInteger bi = bigInteger2.ModPow(exp, bigInteger);
					bool flag9 = bi == bigInteger3;
					if (flag9)
					{
						bi = -1;
					}
					BigInteger bi2 = BigInteger.Jacobi(bigInteger2, bigInteger);
					bool flag10 = bi != bi2;
					if (flag10)
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001EF8C File Offset: 0x0001D18C
		public bool LucasStrongTest()
		{
			bool flag = (this.data[69] & 2147483648U) > 0U;
			BigInteger bigInteger;
			if (flag)
			{
				bigInteger = -this;
			}
			else
			{
				bigInteger = this;
			}
			bool flag2 = bigInteger.dataLength == 1;
			if (flag2)
			{
				bool flag3 = bigInteger.data[0] == 0U || bigInteger.data[0] == 1U;
				if (flag3)
				{
					return false;
				}
				bool flag4 = bigInteger.data[0] == 2U || bigInteger.data[0] == 3U;
				if (flag4)
				{
					return true;
				}
			}
			bool flag5 = (bigInteger.data[0] & 1U) == 0U;
			return !flag5 && this.LucasStrongTestHelper(bigInteger);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001F034 File Offset: 0x0001D234
		private bool LucasStrongTestHelper(BigInteger thisVal)
		{
			long num = 5L;
			long num2 = -1L;
			long num3 = 0L;
			bool flag = false;
			while (!flag)
			{
				int num4 = BigInteger.Jacobi(num, thisVal);
				bool flag2 = num4 == -1;
				if (!flag2)
				{
					bool flag3 = num4 == 0 && Math.Abs(num) < thisVal;
					if (!flag3)
					{
						bool flag4 = num3 == 20L;
						if (flag4)
						{
							BigInteger bigInteger = thisVal.sqrt();
							bool flag5 = bigInteger * bigInteger == thisVal;
							if (flag5)
							{
								return false;
							}
						}
						num = (Math.Abs(num) + 2L) * num2;
						num2 = -num2;
						goto IL_99;
					}
					return false;
				}
				flag = true;
				IL_99:
				num3 += 1L;
			}
			long num5 = 1L - num >> 2;
			BigInteger bigInteger2 = thisVal + 1;
			int num6 = 0;
			for (int i = 0; i < bigInteger2.dataLength; i++)
			{
				uint num7 = 1U;
				for (int j = 0; j < 32; j++)
				{
					bool flag6 = (bigInteger2.data[i] & num7) > 0U;
					if (flag6)
					{
						i = bigInteger2.dataLength;
						break;
					}
					num7 <<= 1;
					num6++;
				}
			}
			BigInteger k = bigInteger2 >> num6;
			BigInteger bigInteger3 = new BigInteger();
			int num8 = thisVal.dataLength << 1;
			bigInteger3.data[num8] = 1U;
			bigInteger3.dataLength = num8 + 1;
			bigInteger3 /= thisVal;
			BigInteger[] array = BigInteger.LucasSequenceHelper(1, num5, k, thisVal, bigInteger3, 0);
			bool flag7 = false;
			bool flag8 = (array[0].dataLength == 1 && array[0].data[0] == 0U) || (array[1].dataLength == 1 && array[1].data[0] == 0U);
			if (flag8)
			{
				flag7 = true;
			}
			for (int l = 1; l < num6; l++)
			{
				bool flag9 = !flag7;
				if (flag9)
				{
					array[1] = thisVal.BarrettReduction(array[1] * array[1], thisVal, bigInteger3);
					array[1] = (array[1] - (array[2] << 1)) % thisVal;
					bool flag10 = array[1].dataLength == 1 && array[1].data[0] == 0U;
					if (flag10)
					{
						flag7 = true;
					}
				}
				array[2] = thisVal.BarrettReduction(array[2] * array[2], thisVal, bigInteger3);
			}
			bool flag11 = flag7;
			if (flag11)
			{
				BigInteger bigInteger4 = thisVal.gcd(num5);
				bool flag12 = bigInteger4.dataLength == 1 && bigInteger4.data[0] == 1U;
				if (flag12)
				{
					bool flag13 = (array[2].data[69] & 2147483648U) > 0U;
					if (flag13)
					{
						BigInteger[] array2 = array;
						array2[2] = array2[2] + thisVal;
					}
					BigInteger bigInteger5 = num5 * (long)BigInteger.Jacobi(num5, thisVal) % thisVal;
					bool flag14 = (bigInteger5.data[69] & 2147483648U) > 0U;
					if (flag14)
					{
						bigInteger5 += thisVal;
					}
					bool flag15 = array[2] != bigInteger5;
					if (flag15)
					{
						flag7 = false;
					}
				}
			}
			return flag7;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001F378 File Offset: 0x0001D578
		public bool isProbablePrime(int confidence)
		{
			bool flag = (this.data[69] & 2147483648U) > 0U;
			BigInteger bigInteger;
			if (flag)
			{
				bigInteger = -this;
			}
			else
			{
				bigInteger = this;
			}
			for (int i = 0; i < BigInteger.primesBelow2000.Length; i++)
			{
				BigInteger bigInteger2 = BigInteger.primesBelow2000[i];
				bool flag2 = bigInteger2 >= bigInteger;
				if (flag2)
				{
					break;
				}
				BigInteger bigInteger3 = bigInteger % bigInteger2;
				bool flag3 = bigInteger3.IntValue() == 0;
				if (flag3)
				{
					return false;
				}
			}
			return bigInteger.RabinMillerTest(confidence);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001F418 File Offset: 0x0001D618
		public bool isProbablePrime()
		{
			bool flag = (this.data[69] & 2147483648U) > 0U;
			BigInteger bigInteger;
			if (flag)
			{
				bigInteger = -this;
			}
			else
			{
				bigInteger = this;
			}
			bool flag2 = bigInteger.dataLength == 1;
			if (flag2)
			{
				bool flag3 = bigInteger.data[0] == 0U || bigInteger.data[0] == 1U;
				if (flag3)
				{
					return false;
				}
				bool flag4 = bigInteger.data[0] == 2U || bigInteger.data[0] == 3U;
				if (flag4)
				{
					return true;
				}
			}
			bool flag5 = (bigInteger.data[0] & 1U) == 0U;
			bool result;
			if (flag5)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < BigInteger.primesBelow2000.Length; i++)
				{
					BigInteger bigInteger2 = BigInteger.primesBelow2000[i];
					bool flag6 = bigInteger2 >= bigInteger;
					if (flag6)
					{
						break;
					}
					BigInteger bigInteger3 = bigInteger % bigInteger2;
					bool flag7 = bigInteger3.IntValue() == 0;
					if (flag7)
					{
						return false;
					}
				}
				BigInteger bigInteger4 = bigInteger - new BigInteger(1L);
				int num = 0;
				for (int j = 0; j < bigInteger4.dataLength; j++)
				{
					uint num2 = 1U;
					for (int k = 0; k < 32; k++)
					{
						bool flag8 = (bigInteger4.data[j] & num2) > 0U;
						if (flag8)
						{
							j = bigInteger4.dataLength;
							break;
						}
						num2 <<= 1;
						num++;
					}
				}
				BigInteger exp = bigInteger4 >> num;
				int num3 = bigInteger.bitCount();
				BigInteger bigInteger5 = 2;
				BigInteger bigInteger6 = bigInteger5.ModPow(exp, bigInteger);
				bool flag9 = false;
				bool flag10 = bigInteger6.dataLength == 1 && bigInteger6.data[0] == 1U;
				if (flag10)
				{
					flag9 = true;
				}
				int num4 = 0;
				while (!flag9 && num4 < num)
				{
					bool flag11 = bigInteger6 == bigInteger4;
					if (flag11)
					{
						flag9 = true;
						break;
					}
					bigInteger6 = bigInteger6 * bigInteger6 % bigInteger;
					num4++;
				}
				bool flag12 = flag9;
				if (flag12)
				{
					flag9 = this.LucasStrongTestHelper(bigInteger);
				}
				result = flag9;
			}
			return result;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001F63C File Offset: 0x0001D83C
		public int IntValue()
		{
			return (int)this.data[0];
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0001F658 File Offset: 0x0001D858
		public long LongValue()
		{
			long num = 0L;
			num = (long)((ulong)this.data[0]);
			try
			{
				num |= (long)((long)((ulong)this.data[1]) << 32);
			}
			catch (Exception)
			{
				bool flag = (this.data[0] & 2147483648U) > 0U;
				if (flag)
				{
					num = (long)this.data[0];
				}
			}
			return num;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0001F6C0 File Offset: 0x0001D8C0
		public static int Jacobi(BigInteger a, BigInteger b)
		{
			bool flag = (b.data[0] & 1U) == 0U;
			if (flag)
			{
				throw new ArgumentException("Jacobi defined only for odd integers.");
			}
			bool flag2 = a >= b;
			if (flag2)
			{
				a %= b;
			}
			bool flag3 = a.dataLength == 1 && a.data[0] == 0U;
			int result;
			if (flag3)
			{
				result = 0;
			}
			else
			{
				bool flag4 = a.dataLength == 1 && a.data[0] == 1U;
				if (flag4)
				{
					result = 1;
				}
				else
				{
					bool flag5 = a < 0;
					if (flag5)
					{
						bool flag6 = ((b - 1).data[0] & 2U) == 0U;
						if (flag6)
						{
							result = BigInteger.Jacobi(-a, b);
						}
						else
						{
							result = -BigInteger.Jacobi(-a, b);
						}
					}
					else
					{
						int num = 0;
						for (int i = 0; i < a.dataLength; i++)
						{
							uint num2 = 1U;
							for (int j = 0; j < 32; j++)
							{
								bool flag7 = (a.data[i] & num2) > 0U;
								if (flag7)
								{
									i = a.dataLength;
									break;
								}
								num2 <<= 1;
								num++;
							}
						}
						BigInteger bigInteger = a >> num;
						int num3 = 1;
						bool flag8 = (num & 1) != 0 && ((b.data[0] & 7U) == 3U || (b.data[0] & 7U) == 5U);
						if (flag8)
						{
							num3 = -1;
						}
						bool flag9 = (b.data[0] & 3U) == 3U && (bigInteger.data[0] & 3U) == 3U;
						if (flag9)
						{
							num3 = -num3;
						}
						bool flag10 = bigInteger.dataLength == 1 && bigInteger.data[0] == 1U;
						if (flag10)
						{
							result = num3;
						}
						else
						{
							result = num3 * BigInteger.Jacobi(b % bigInteger, bigInteger);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001F89C File Offset: 0x0001DA9C
		public static BigInteger genPseudoPrime(int bits, int confidence, Random rand)
		{
			BigInteger bigInteger = new BigInteger();
			bool flag = false;
			while (!flag)
			{
				bigInteger.genRandomBits(bits, rand);
				bigInteger.data[0] |= 1U;
				flag = bigInteger.isProbablePrime(confidence);
			}
			return bigInteger;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0001F8E8 File Offset: 0x0001DAE8
		public BigInteger genCoPrime(int bits, Random rand)
		{
			bool flag = false;
			BigInteger bigInteger = new BigInteger();
			while (!flag)
			{
				bigInteger.genRandomBits(bits, rand);
				BigInteger bigInteger2 = bigInteger.gcd(this);
				bool flag2 = bigInteger2.dataLength == 1 && bigInteger2.data[0] == 1U;
				if (flag2)
				{
					flag = true;
				}
			}
			return bigInteger;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001F944 File Offset: 0x0001DB44
		public BigInteger modInverse(BigInteger modulus)
		{
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
			BigInteger bi = modulus;
			BigInteger bigInteger = this;
			while (bigInteger.dataLength > 1 || (bigInteger.dataLength == 1 && bigInteger.data[0] > 0U))
			{
				BigInteger bigInteger2 = new BigInteger();
				BigInteger bigInteger3 = new BigInteger();
				bool flag = num > 1;
				if (flag)
				{
					BigInteger bigInteger4 = (array[0] - array[1] * array2[0]) % modulus;
					array[0] = array[1];
					array[1] = bigInteger4;
				}
				bool flag2 = bigInteger.dataLength == 1;
				if (flag2)
				{
					BigInteger.singleByteDivide(bi, bigInteger, bigInteger2, bigInteger3);
				}
				else
				{
					BigInteger.multiByteDivide(bi, bigInteger, bigInteger2, bigInteger3);
				}
				array2[0] = array2[1];
				array3[0] = array3[1];
				array2[1] = bigInteger2;
				array3[1] = bigInteger3;
				bi = bigInteger;
				bigInteger = bigInteger3;
				num++;
			}
			bool flag3 = array3[0].dataLength > 1 || (array3[0].dataLength == 1 && array3[0].data[0] != 1U);
			if (flag3)
			{
				throw new ArithmeticException("No inverse!");
			}
			BigInteger bigInteger5 = (array[0] - array[1] * array2[0]) % modulus;
			bool flag4 = (bigInteger5.data[69] & 2147483648U) > 0U;
			if (flag4)
			{
				bigInteger5 += modulus;
			}
			return bigInteger5;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0001FADC File Offset: 0x0001DCDC
		public byte[] GetBytes()
		{
			bool flag = this == 0;
			byte[] result;
			if (flag)
			{
				result = new byte[1];
			}
			else
			{
				int num = this.bitCount();
				int num2 = num >> 3;
				bool flag2 = (num & 7) != 0;
				if (flag2)
				{
					num2++;
				}
				byte[] array = new byte[num2];
				int num3 = num2 & 3;
				bool flag3 = num3 == 0;
				if (flag3)
				{
					num3 = 4;
				}
				int num4 = 0;
				for (int i = this.dataLength - 1; i >= 0; i--)
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
				result = array;
			}
			return result;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001FBB4 File Offset: 0x0001DDB4
		public void setBit(uint bitNum)
		{
			uint num = bitNum >> 5;
			byte b = (byte)(bitNum & 31U);
			uint num2 = 1U << (int)b;
			this.data[(int)num] |= num2;
			bool flag = (ulong)num >= (ulong)((long)this.dataLength);
			if (flag)
			{
				this.dataLength = (int)(num + 1U);
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001FC00 File Offset: 0x0001DE00
		public void unsetBit(uint bitNum)
		{
			uint num = bitNum >> 5;
			bool flag = (ulong)num < (ulong)((long)this.dataLength);
			if (flag)
			{
				byte b = (byte)(bitNum & 31U);
				uint num2 = 1U << (int)b;
				uint num3 = uint.MaxValue ^ num2;
				this.data[(int)num] &= num3;
				bool flag2 = this.dataLength > 1 && this.data[this.dataLength - 1] == 0U;
				if (flag2)
				{
					this.dataLength--;
				}
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001FC7C File Offset: 0x0001DE7C
		public BigInteger sqrt()
		{
			uint num = (uint)this.bitCount();
			bool flag = (num & 1U) > 0U;
			if (flag)
			{
				num = (num >> 1) + 1U;
			}
			else
			{
				num >>= 1;
			}
			uint num2 = num >> 5;
			byte b = (byte)(num & 31U);
			BigInteger bigInteger = new BigInteger();
			bool flag2 = b == 0;
			uint num3;
			if (flag2)
			{
				num3 = 2147483648U;
			}
			else
			{
				num3 = 1U << (int)b;
				num2 += 1U;
			}
			bigInteger.dataLength = (int)num2;
			for (int i = (int)(num2 - 1U); i >= 0; i--)
			{
				while (num3 > 0U)
				{
					bigInteger.data[i] ^= num3;
					bool flag3 = bigInteger * bigInteger > this;
					if (flag3)
					{
						bigInteger.data[i] ^= num3;
					}
					num3 >>= 1;
				}
				num3 = 2147483648U;
			}
			return bigInteger;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001FD5C File Offset: 0x0001DF5C
		public static BigInteger[] LucasSequence(BigInteger P, BigInteger Q, BigInteger k, BigInteger n)
		{
			bool flag = k.dataLength == 1 && k.data[0] == 0U;
			BigInteger[] result;
			if (flag)
			{
				result = new BigInteger[]
				{
					0,
					2 % n,
					1 % n
				};
			}
			else
			{
				BigInteger bigInteger = new BigInteger();
				int num = n.dataLength << 1;
				bigInteger.data[num] = 1U;
				bigInteger.dataLength = num + 1;
				bigInteger /= n;
				int num2 = 0;
				for (int i = 0; i < k.dataLength; i++)
				{
					uint num3 = 1U;
					for (int j = 0; j < 32; j++)
					{
						bool flag2 = (k.data[i] & num3) > 0U;
						if (flag2)
						{
							i = k.dataLength;
							break;
						}
						num3 <<= 1;
						num2++;
					}
				}
				BigInteger k2 = k >> num2;
				result = BigInteger.LucasSequenceHelper(P, Q, k2, n, bigInteger, num2);
			}
			return result;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001FE6C File Offset: 0x0001E06C
		private static BigInteger[] LucasSequenceHelper(BigInteger P, BigInteger Q, BigInteger k, BigInteger n, BigInteger constant, int s)
		{
			BigInteger[] array = new BigInteger[3];
			bool flag = (k.data[0] & 1U) == 0U;
			if (flag)
			{
				throw new ArgumentException("Argument k must be odd.");
			}
			int num = k.bitCount();
			uint num2 = 1U << (num & 31) - 1;
			BigInteger bigInteger = 2 % n;
			BigInteger bigInteger2 = 1 % n;
			BigInteger bigInteger3 = P % n;
			BigInteger bigInteger4 = bigInteger2;
			bool flag2 = true;
			for (int i = k.dataLength - 1; i >= 0; i--)
			{
				while (num2 > 0U)
				{
					bool flag3 = i == 0 && num2 == 1U;
					if (flag3)
					{
						break;
					}
					bool flag4 = (k.data[i] & num2) > 0U;
					if (flag4)
					{
						bigInteger4 = bigInteger4 * bigInteger3 % n;
						bigInteger = (bigInteger * bigInteger3 - P * bigInteger2) % n;
						bigInteger3 = n.BarrettReduction(bigInteger3 * bigInteger3, n, constant);
						bigInteger3 = (bigInteger3 - (bigInteger2 * Q << 1)) % n;
						bool flag5 = flag2;
						if (flag5)
						{
							flag2 = false;
						}
						else
						{
							bigInteger2 = n.BarrettReduction(bigInteger2 * bigInteger2, n, constant);
						}
						bigInteger2 = bigInteger2 * Q % n;
					}
					else
					{
						bigInteger4 = (bigInteger4 * bigInteger - bigInteger2) % n;
						bigInteger3 = (bigInteger * bigInteger3 - P * bigInteger2) % n;
						bigInteger = n.BarrettReduction(bigInteger * bigInteger, n, constant);
						bigInteger = (bigInteger - (bigInteger2 << 1)) % n;
						bool flag6 = flag2;
						if (flag6)
						{
							bigInteger2 = Q % n;
							flag2 = false;
						}
						else
						{
							bigInteger2 = n.BarrettReduction(bigInteger2 * bigInteger2, n, constant);
						}
					}
					num2 >>= 1;
				}
				num2 = 2147483648U;
			}
			bigInteger4 = (bigInteger4 * bigInteger - bigInteger2) % n;
			bigInteger = (bigInteger * bigInteger3 - P * bigInteger2) % n;
			bool flag7 = flag2;
			if (flag7)
			{
				flag2 = false;
			}
			else
			{
				bigInteger2 = n.BarrettReduction(bigInteger2 * bigInteger2, n, constant);
			}
			bigInteger2 = bigInteger2 * Q % n;
			for (int j = 0; j < s; j++)
			{
				bigInteger4 = bigInteger4 * bigInteger % n;
				bigInteger = (bigInteger * bigInteger - (bigInteger2 << 1)) % n;
				bool flag8 = flag2;
				if (flag8)
				{
					bigInteger2 = Q % n;
					flag2 = false;
				}
				else
				{
					bigInteger2 = n.BarrettReduction(bigInteger2 * bigInteger2, n, constant);
				}
			}
			array[0] = bigInteger4;
			array[1] = bigInteger;
			array[2] = bigInteger2;
			return array;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00020158 File Offset: 0x0001E358
		public static void MulDivTest(int rounds)
		{
			Random random = new Random();
			byte[] array = new byte[64];
			byte[] array2 = new byte[64];
			for (int i = 0; i < rounds; i++)
			{
				int num;
				for (num = 0; num == 0; num = (int)(random.NextDouble() * 65.0))
				{
				}
				int num2;
				for (num2 = 0; num2 == 0; num2 = (int)(random.NextDouble() * 65.0))
				{
				}
				bool flag = false;
				while (!flag)
				{
					for (int j = 0; j < 64; j++)
					{
						bool flag2 = j < num;
						if (flag2)
						{
							array[j] = (byte)(random.NextDouble() * 256.0);
						}
						else
						{
							array[j] = 0;
						}
						bool flag3 = array[j] > 0;
						if (flag3)
						{
							flag = true;
						}
					}
				}
				flag = false;
				while (!flag)
				{
					for (int k = 0; k < 64; k++)
					{
						bool flag4 = k < num2;
						if (flag4)
						{
							array2[k] = (byte)(random.NextDouble() * 256.0);
						}
						else
						{
							array2[k] = 0;
						}
						bool flag5 = array2[k] > 0;
						if (flag5)
						{
							flag = true;
						}
					}
				}
				while (array[0] == 0)
				{
					array[0] = (byte)(random.NextDouble() * 256.0);
				}
				while (array2[0] == 0)
				{
					array2[0] = (byte)(random.NextDouble() * 256.0);
				}
				Console.WriteLine(i);
				BigInteger bigInteger = new BigInteger(array, num);
				BigInteger bigInteger2 = new BigInteger(array2, num2);
				BigInteger bigInteger3 = bigInteger / bigInteger2;
				BigInteger bigInteger4 = bigInteger % bigInteger2;
				BigInteger bigInteger5 = bigInteger3 * bigInteger2 + bigInteger4;
				bool flag6 = bigInteger5 != bigInteger;
				if (flag6)
				{
					Console.WriteLine("Error at " + i.ToString());
					BigInteger bigInteger6 = bigInteger;
					Console.WriteLine(((bigInteger6 != null) ? bigInteger6.ToString() : null) + "\n");
					BigInteger bigInteger7 = bigInteger2;
					Console.WriteLine(((bigInteger7 != null) ? bigInteger7.ToString() : null) + "\n");
					BigInteger bigInteger8 = bigInteger3;
					Console.WriteLine(((bigInteger8 != null) ? bigInteger8.ToString() : null) + "\n");
					BigInteger bigInteger9 = bigInteger4;
					Console.WriteLine(((bigInteger9 != null) ? bigInteger9.ToString() : null) + "\n");
					BigInteger bigInteger10 = bigInteger5;
					Console.WriteLine(((bigInteger10 != null) ? bigInteger10.ToString() : null) + "\n");
					break;
				}
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000203EC File Offset: 0x0001E5EC
		public static void RSATest(int rounds)
		{
			Random random = new Random(1);
			byte[] array = new byte[64];
			BigInteger bigInteger = new BigInteger("a932b948feed4fb2b692609bd22164fc9edb59fae7880cc1eaff7b3c9626b7e5b241c27a974833b2622ebe09beb451917663d47232488f23a117fc97720f1e7", 16);
			BigInteger bigInteger2 = new BigInteger("4adf2f7a89da93248509347d2ae506d683dd3a16357e859a980c4f77a4e2f7a01fae289f13a851df6e9db5adaa60bfd2b162bbbe31f7c8f828261a6839311929d2cef4f864dde65e556ce43c89bbbf9f1ac5511315847ce9cc8dc92470a747b8792d6a83b0092d2e5ebaf852c85cacf34278efa99160f2f8aa7ee7214de07b7", 16);
			BigInteger bigInteger3 = new BigInteger("e8e77781f36a7b3188d711c2190b560f205a52391b3479cdb99fa010745cbeba5f2adc08e1de6bf38398a0487c4a73610d94ec36f17f3f46ad75e17bc1adfec99839589f45f95ccc94cb2a5c500b477eb3323d8cfab0c8458c96f0147a45d27e45a4d11d54d77684f65d48f15fafcc1ba208e71e921b9bd9017c16a5231af7f", 16);
			Console.WriteLine("e =\n" + bigInteger.ToString(10));
			Console.WriteLine("\nd =\n" + bigInteger2.ToString(10));
			Console.WriteLine("\nn =\n" + bigInteger3.ToString(10) + "\n");
			for (int i = 0; i < rounds; i++)
			{
				int num;
				for (num = 0; num == 0; num = (int)(random.NextDouble() * 65.0))
				{
				}
				bool flag = false;
				while (!flag)
				{
					for (int j = 0; j < 64; j++)
					{
						bool flag2 = j < num;
						if (flag2)
						{
							array[j] = (byte)(random.NextDouble() * 256.0);
						}
						else
						{
							array[j] = 0;
						}
						bool flag3 = array[j] > 0;
						if (flag3)
						{
							flag = true;
						}
					}
				}
				while (array[0] == 0)
				{
					array[0] = (byte)(random.NextDouble() * 256.0);
				}
				Console.Write("Round = " + i.ToString());
				BigInteger bigInteger4 = new BigInteger(array, num);
				BigInteger bigInteger5 = bigInteger4.ModPow(bigInteger, bigInteger3);
				BigInteger bi = bigInteger5.ModPow(bigInteger2, bigInteger3);
				bool flag4 = bi != bigInteger4;
				if (flag4)
				{
					Console.WriteLine("\nError at round " + i.ToString());
					BigInteger bigInteger6 = bigInteger4;
					Console.WriteLine(((bigInteger6 != null) ? bigInteger6.ToString() : null) + "\n");
					break;
				}
				Console.WriteLine(" <PASSED>.");
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000205D0 File Offset: 0x0001E7D0
		public static void RSATest2(int rounds)
		{
			Random random = new Random();
			byte[] array = new byte[64];
			byte[] inData = new byte[]
			{
				133,
				132,
				100,
				253,
				112,
				106,
				159,
				240,
				148,
				12,
				62,
				44,
				116,
				52,
				5,
				201,
				85,
				179,
				133,
				50,
				152,
				113,
				249,
				65,
				33,
				95,
				2,
				158,
				234,
				86,
				141,
				140,
				68,
				204,
				238,
				238,
				61,
				44,
				157,
				44,
				18,
				65,
				30,
				241,
				197,
				50,
				195,
				170,
				49,
				74,
				82,
				216,
				232,
				175,
				66,
				244,
				114,
				161,
				42,
				13,
				151,
				177,
				49,
				179
			};
			byte[] inData2 = new byte[]
			{
				153,
				152,
				202,
				184,
				94,
				215,
				229,
				220,
				40,
				92,
				111,
				14,
				21,
				9,
				89,
				110,
				132,
				243,
				129,
				205,
				222,
				66,
				220,
				147,
				194,
				122,
				98,
				172,
				108,
				175,
				222,
				116,
				227,
				203,
				96,
				32,
				56,
				156,
				33,
				195,
				220,
				200,
				162,
				77,
				198,
				42,
				53,
				127,
				243,
				169,
				232,
				29,
				123,
				44,
				120,
				250,
				184,
				2,
				85,
				128,
				155,
				194,
				165,
				203
			};
			BigInteger bi = new BigInteger(inData);
			BigInteger bigInteger = new BigInteger(inData2);
			BigInteger bigInteger2 = (bi - 1) * (bigInteger - 1);
			BigInteger bigInteger3 = bi * bigInteger;
			for (int i = 0; i < rounds; i++)
			{
				BigInteger bigInteger4 = bigInteger2.genCoPrime(512, random);
				BigInteger bigInteger5 = bigInteger4.modInverse(bigInteger2);
				Console.WriteLine("\ne =\n" + bigInteger4.ToString(10));
				Console.WriteLine("\nd =\n" + bigInteger5.ToString(10));
				Console.WriteLine("\nn =\n" + bigInteger3.ToString(10) + "\n");
				int num;
				for (num = 0; num == 0; num = (int)(random.NextDouble() * 65.0))
				{
				}
				bool flag = false;
				while (!flag)
				{
					for (int j = 0; j < 64; j++)
					{
						bool flag2 = j < num;
						if (flag2)
						{
							array[j] = (byte)(random.NextDouble() * 256.0);
						}
						else
						{
							array[j] = 0;
						}
						bool flag3 = array[j] > 0;
						if (flag3)
						{
							flag = true;
						}
					}
				}
				while (array[0] == 0)
				{
					array[0] = (byte)(random.NextDouble() * 256.0);
				}
				Console.Write("Round = " + i.ToString());
				BigInteger bigInteger6 = new BigInteger(array, num);
				BigInteger bigInteger7 = bigInteger6.ModPow(bigInteger4, bigInteger3);
				BigInteger bi2 = bigInteger7.ModPow(bigInteger5, bigInteger3);
				bool flag4 = bi2 != bigInteger6;
				if (flag4)
				{
					Console.WriteLine("\nError at round " + i.ToString());
					BigInteger bigInteger8 = bigInteger6;
					Console.WriteLine(((bigInteger8 != null) ? bigInteger8.ToString() : null) + "\n");
					break;
				}
				Console.WriteLine(" <PASSED>.");
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0002080C File Offset: 0x0001EA0C
		public static void SqrtTest(int rounds)
		{
			Random random = new Random();
			for (int i = 0; i < rounds; i++)
			{
				int num;
				for (num = 0; num == 0; num = (int)(random.NextDouble() * 1024.0))
				{
				}
				Console.Write("Round = " + i.ToString());
				BigInteger bigInteger = new BigInteger();
				bigInteger.genRandomBits(num, random);
				BigInteger bi = bigInteger.sqrt();
				BigInteger bi2 = (bi + 1) * (bi + 1);
				bool flag = bi2 <= bigInteger;
				if (flag)
				{
					Console.WriteLine("\nError at round " + i.ToString());
					BigInteger bigInteger2 = bigInteger;
					Console.WriteLine(((bigInteger2 != null) ? bigInteger2.ToString() : null) + "\n");
					break;
				}
				Console.WriteLine(" <PASSED>.");
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000208FC File Offset: 0x0001EAFC
		public static void Main(string[] args)
		{
			byte[] inData = new byte[]
			{
				0,
				133,
				132,
				100,
				253,
				112,
				106,
				159,
				240,
				148,
				12,
				62,
				44,
				116,
				52,
				5,
				201,
				85,
				179,
				133,
				50,
				152,
				113,
				249,
				65,
				33,
				95,
				2,
				158,
				234,
				86,
				141,
				140,
				68,
				204,
				238,
				238,
				61,
				44,
				157,
				44,
				18,
				65,
				30,
				241,
				197,
				50,
				195,
				170,
				49,
				74,
				82,
				216,
				232,
				175,
				66,
				244,
				114,
				161,
				42,
				13,
				151,
				177,
				49,
				179
			};
			byte[] array = new byte[]
			{
				0,
				153,
				152,
				202,
				184,
				94,
				215,
				229,
				220,
				40,
				92,
				111,
				14,
				21,
				9,
				89,
				110,
				132,
				243,
				129,
				205,
				222,
				66,
				220,
				147,
				194,
				122,
				98,
				172,
				108,
				175,
				222,
				116,
				227,
				203,
				96,
				32,
				56,
				156,
				33,
				195,
				220,
				200,
				162,
				77,
				198,
				42,
				53,
				127,
				243,
				169,
				232,
				29,
				123,
				44,
				120,
				250,
				184,
				2,
				85,
				128,
				155,
				194,
				165,
				203
			};
			Console.WriteLine("List of primes < 2000\n---------------------");
			int num = 100;
			int num2 = 0;
			for (int i = 0; i < 2000; i++)
			{
				bool flag = i >= num;
				if (flag)
				{
					Console.WriteLine();
					num += 100;
				}
				BigInteger bigInteger = new BigInteger((long)(-(long)i));
				bool flag2 = bigInteger.isProbablePrime();
				if (flag2)
				{
					Console.Write(i.ToString() + ", ");
					num2++;
				}
			}
			Console.WriteLine("\nCount = " + num2.ToString());
			BigInteger bigInteger2 = new BigInteger(inData);
			Console.WriteLine("\n\nPrimality testing for\n" + bigInteger2.ToString() + "\n");
			Console.WriteLine("SolovayStrassenTest(5) = " + bigInteger2.SolovayStrassenTest(5).ToString());
			Console.WriteLine("RabinMillerTest(5) = " + bigInteger2.RabinMillerTest(5).ToString());
			Console.WriteLine("FermatLittleTest(5) = " + bigInteger2.FermatLittleTest(5).ToString());
			Console.WriteLine("isProbablePrime() = " + bigInteger2.isProbablePrime().ToString());
			Console.Write("\nGenerating 512-bits random pseudoprime. . .");
			Random rand = new Random();
			BigInteger bigInteger3 = BigInteger.genPseudoPrime(512, 5, rand);
			string str = "\n";
			BigInteger bigInteger4 = bigInteger3;
			Console.WriteLine(str + ((bigInteger4 != null) ? bigInteger4.ToString() : null));
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00020AA9 File Offset: 0x0001ECA9
		// Note: this type is marked as 'beforefieldinit'.
		static BigInteger()
		{
		}

		// Token: 0x04000208 RID: 520
		private const int maxLength = 70;

		// Token: 0x04000209 RID: 521
		public static readonly int[] primesBelow2000 = new int[]
		{
			2,
			3,
			5,
			7,
			11,
			13,
			17,
			19,
			23,
			29,
			31,
			37,
			41,
			43,
			47,
			53,
			59,
			61,
			67,
			71,
			73,
			79,
			83,
			89,
			97,
			101,
			103,
			107,
			109,
			113,
			127,
			131,
			137,
			139,
			149,
			151,
			157,
			163,
			167,
			173,
			179,
			181,
			191,
			193,
			197,
			199,
			211,
			223,
			227,
			229,
			233,
			239,
			241,
			251,
			257,
			263,
			269,
			271,
			277,
			281,
			283,
			293,
			307,
			311,
			313,
			317,
			331,
			337,
			347,
			349,
			353,
			359,
			367,
			373,
			379,
			383,
			389,
			397,
			401,
			409,
			419,
			421,
			431,
			433,
			439,
			443,
			449,
			457,
			461,
			463,
			467,
			479,
			487,
			491,
			499,
			503,
			509,
			521,
			523,
			541,
			547,
			557,
			563,
			569,
			571,
			577,
			587,
			593,
			599,
			601,
			607,
			613,
			617,
			619,
			631,
			641,
			643,
			647,
			653,
			659,
			661,
			673,
			677,
			683,
			691,
			701,
			709,
			719,
			727,
			733,
			739,
			743,
			751,
			757,
			761,
			769,
			773,
			787,
			797,
			809,
			811,
			821,
			823,
			827,
			829,
			839,
			853,
			857,
			859,
			863,
			877,
			881,
			883,
			887,
			907,
			911,
			919,
			929,
			937,
			941,
			947,
			953,
			967,
			971,
			977,
			983,
			991,
			997,
			1009,
			1013,
			1019,
			1021,
			1031,
			1033,
			1039,
			1049,
			1051,
			1061,
			1063,
			1069,
			1087,
			1091,
			1093,
			1097,
			1103,
			1109,
			1117,
			1123,
			1129,
			1151,
			1153,
			1163,
			1171,
			1181,
			1187,
			1193,
			1201,
			1213,
			1217,
			1223,
			1229,
			1231,
			1237,
			1249,
			1259,
			1277,
			1279,
			1283,
			1289,
			1291,
			1297,
			1301,
			1303,
			1307,
			1319,
			1321,
			1327,
			1361,
			1367,
			1373,
			1381,
			1399,
			1409,
			1423,
			1427,
			1429,
			1433,
			1439,
			1447,
			1451,
			1453,
			1459,
			1471,
			1481,
			1483,
			1487,
			1489,
			1493,
			1499,
			1511,
			1523,
			1531,
			1543,
			1549,
			1553,
			1559,
			1567,
			1571,
			1579,
			1583,
			1597,
			1601,
			1607,
			1609,
			1613,
			1619,
			1621,
			1627,
			1637,
			1657,
			1663,
			1667,
			1669,
			1693,
			1697,
			1699,
			1709,
			1721,
			1723,
			1733,
			1741,
			1747,
			1753,
			1759,
			1777,
			1783,
			1787,
			1789,
			1801,
			1811,
			1823,
			1831,
			1847,
			1861,
			1867,
			1871,
			1873,
			1877,
			1879,
			1889,
			1901,
			1907,
			1913,
			1931,
			1933,
			1949,
			1951,
			1973,
			1979,
			1987,
			1993,
			1997,
			1999
		};

		// Token: 0x0400020A RID: 522
		private uint[] data = null;

		// Token: 0x0400020B RID: 523
		public int dataLength;
	}
}
