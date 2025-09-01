using System;

namespace System.Numerics
{
	// Token: 0x0200000F RID: 15
	internal static class BigIntegerCalculator
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x0000D6C4 File Offset: 0x0000B8C4
		public static uint[] Add(uint[] left, uint right)
		{
			uint[] array = new uint[left.Length + 1];
			long num = (long)((ulong)left[0] + (ulong)right);
			array[0] = (uint)num;
			long num2 = num >> 32;
			for (int i = 1; i < left.Length; i++)
			{
				num = (long)((ulong)left[i] + (ulong)num2);
				array[i] = (uint)num;
				num2 = num >> 32;
			}
			array[left.Length] = (uint)num2;
			return array;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000D718 File Offset: 0x0000B918
		public unsafe static uint[] Add(uint[] left, uint[] right)
		{
			uint[] array = new uint[left.Length + 1];
			fixed (uint[] array2 = left)
			{
				uint* left2;
				if (left == null || array2.Length == 0)
				{
					left2 = null;
				}
				else
				{
					left2 = &array2[0];
				}
				fixed (uint[] array3 = right)
				{
					uint* right2;
					if (right == null || array3.Length == 0)
					{
						right2 = null;
					}
					else
					{
						right2 = &array3[0];
					}
					fixed (uint* ptr = &array[0])
					{
						uint* bits = ptr;
						BigIntegerCalculator.Add(left2, left.Length, right2, right.Length, bits, array.Length);
						array2 = null;
						array3 = null;
					}
					return array;
				}
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000D790 File Offset: 0x0000B990
		private unsafe static void Add(uint* left, int leftLength, uint* right, int rightLength, uint* bits, int bitsLength)
		{
			int i = 0;
			long num = 0L;
			while (i < rightLength)
			{
				long num2 = (long)((ulong)left[i] + (ulong)num + (ulong)right[i]);
				bits[i] = (uint)num2;
				num = num2 >> 32;
				i++;
			}
			while (i < leftLength)
			{
				long num3 = (long)((ulong)left[i] + (ulong)num);
				bits[i] = (uint)num3;
				num = num3 >> 32;
				i++;
			}
			bits[i] = (uint)num;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000D800 File Offset: 0x0000BA00
		private unsafe static void AddSelf(uint* left, int leftLength, uint* right, int rightLength)
		{
			int i = 0;
			long num = 0L;
			while (i < rightLength)
			{
				long num2 = (long)((ulong)left[i] + (ulong)num + (ulong)right[i]);
				left[i] = (uint)num2;
				num = num2 >> 32;
				i++;
			}
			while (num != 0L && i < leftLength)
			{
				long num3 = (long)((ulong)left[i] + (ulong)num);
				left[i] = (uint)num3;
				num = num3 >> 32;
				i++;
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000D864 File Offset: 0x0000BA64
		public static uint[] Subtract(uint[] left, uint right)
		{
			uint[] array = new uint[left.Length];
			long num = (long)((ulong)left[0] - (ulong)right);
			array[0] = (uint)num;
			long num2 = num >> 32;
			for (int i = 1; i < left.Length; i++)
			{
				num = (long)((ulong)left[i] + (ulong)num2);
				array[i] = (uint)num;
				num2 = num >> 32;
			}
			return array;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000D8AC File Offset: 0x0000BAAC
		public unsafe static uint[] Subtract(uint[] left, uint[] right)
		{
			uint[] array = new uint[left.Length];
			fixed (uint[] array2 = left)
			{
				uint* left2;
				if (left == null || array2.Length == 0)
				{
					left2 = null;
				}
				else
				{
					left2 = &array2[0];
				}
				uint[] array4;
				fixed (uint[] array3 = right)
				{
					uint* right2;
					if (right == null || array3.Length == 0)
					{
						right2 = null;
					}
					else
					{
						right2 = &array3[0];
					}
					uint* bits;
					if ((array4 = array) == null || array4.Length == 0)
					{
						bits = null;
					}
					else
					{
						bits = &array4[0];
					}
					BigIntegerCalculator.Subtract(left2, left.Length, right2, right.Length, bits, array.Length);
					array2 = null;
				}
				array4 = null;
				return array;
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000D930 File Offset: 0x0000BB30
		private unsafe static void Subtract(uint* left, int leftLength, uint* right, int rightLength, uint* bits, int bitsLength)
		{
			int i = 0;
			long num = 0L;
			while (i < rightLength)
			{
				long num2 = (long)((ulong)left[i] + (ulong)num - (ulong)right[i]);
				bits[i] = (uint)num2;
				num = num2 >> 32;
				i++;
			}
			while (i < leftLength)
			{
				long num3 = (long)((ulong)left[i] + (ulong)num);
				bits[i] = (uint)num3;
				num = num3 >> 32;
				i++;
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000D994 File Offset: 0x0000BB94
		private unsafe static void SubtractSelf(uint* left, int leftLength, uint* right, int rightLength)
		{
			int i = 0;
			long num = 0L;
			while (i < rightLength)
			{
				long num2 = (long)((ulong)left[i] + (ulong)num - (ulong)right[i]);
				left[i] = (uint)num2;
				num = num2 >> 32;
				i++;
			}
			while (num != 0L && i < leftLength)
			{
				long num3 = (long)((ulong)left[i] + (ulong)num);
				left[i] = (uint)num3;
				num = num3 >> 32;
				i++;
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000D9F8 File Offset: 0x0000BBF8
		public static int Compare(uint[] left, uint[] right)
		{
			if (left.Length < right.Length)
			{
				return -1;
			}
			if (left.Length > right.Length)
			{
				return 1;
			}
			for (int i = left.Length - 1; i >= 0; i--)
			{
				if (left[i] < right[i])
				{
					return -1;
				}
				if (left[i] > right[i])
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000DA40 File Offset: 0x0000BC40
		private unsafe static int Compare(uint* left, int leftLength, uint* right, int rightLength)
		{
			if (leftLength < rightLength)
			{
				return -1;
			}
			if (leftLength > rightLength)
			{
				return 1;
			}
			for (int i = leftLength - 1; i >= 0; i--)
			{
				if (left[i] < right[i])
				{
					return -1;
				}
				if (left[i] > right[i])
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000DA8C File Offset: 0x0000BC8C
		public static uint[] Divide(uint[] left, uint right, out uint remainder)
		{
			uint[] array = new uint[left.Length];
			ulong num = 0UL;
			for (int i = left.Length - 1; i >= 0; i--)
			{
				ulong num2 = num << 32 | (ulong)left[i];
				ulong num3 = num2 / (ulong)right;
				array[i] = (uint)num3;
				num = num2 - num3 * (ulong)right;
			}
			remainder = (uint)num;
			return array;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000DAD4 File Offset: 0x0000BCD4
		public static uint[] Divide(uint[] left, uint right)
		{
			uint[] array = new uint[left.Length];
			ulong num = 0UL;
			for (int i = left.Length - 1; i >= 0; i--)
			{
				ulong num2 = num << 32 | (ulong)left[i];
				ulong num3 = num2 / (ulong)right;
				array[i] = (uint)num3;
				num = num2 - num3 * (ulong)right;
			}
			return array;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000DB18 File Offset: 0x0000BD18
		public static uint Remainder(uint[] left, uint right)
		{
			ulong num = 0UL;
			for (int i = left.Length - 1; i >= 0; i--)
			{
				num = (num << 32 | (ulong)left[i]) % (ulong)right;
			}
			return (uint)num;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000DB48 File Offset: 0x0000BD48
		public unsafe static uint[] Divide(uint[] left, uint[] right, out uint[] remainder)
		{
			uint[] array = BigIntegerCalculator.CreateCopy(left);
			uint[] array2 = new uint[left.Length - right.Length + 1];
			fixed (uint* ptr = &array[0])
			{
				uint* left2 = ptr;
				fixed (uint* ptr2 = &right[0])
				{
					uint* right2 = ptr2;
					fixed (uint* ptr3 = &array2[0])
					{
						uint* bits = ptr3;
						BigIntegerCalculator.Divide(left2, array.Length, right2, right.Length, bits, array2.Length);
						ptr = null;
						ptr2 = null;
					}
					remainder = array;
					return array2;
				}
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000DBB4 File Offset: 0x0000BDB4
		public unsafe static uint[] Divide(uint[] left, uint[] right)
		{
			uint[] array = BigIntegerCalculator.CreateCopy(left);
			uint[] array2 = new uint[left.Length - right.Length + 1];
			fixed (uint* ptr = &array[0])
			{
				uint* left2 = ptr;
				fixed (uint* ptr2 = &right[0])
				{
					uint* right2 = ptr2;
					fixed (uint* ptr3 = &array2[0])
					{
						uint* bits = ptr3;
						BigIntegerCalculator.Divide(left2, array.Length, right2, right.Length, bits, array2.Length);
						ptr = null;
						ptr2 = null;
					}
					return array2;
				}
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000DC1C File Offset: 0x0000BE1C
		public unsafe static uint[] Remainder(uint[] left, uint[] right)
		{
			uint[] array = BigIntegerCalculator.CreateCopy(left);
			fixed (uint* ptr = &array[0])
			{
				uint* left2 = ptr;
				fixed (uint* ptr2 = &right[0])
				{
					uint* right2 = ptr2;
					BigIntegerCalculator.Divide(left2, array.Length, right2, right.Length, null, 0);
					ptr = null;
				}
				return array;
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000DC5C File Offset: 0x0000BE5C
		private unsafe static void Divide(uint* left, int leftLength, uint* right, int rightLength, uint* bits, int bitsLength)
		{
			uint num = right[rightLength - 1];
			uint num2 = (rightLength > 1) ? right[rightLength - 2] : 0U;
			int num3 = BigIntegerCalculator.LeadingZeros(num);
			int num4 = 32 - num3;
			if (num3 > 0)
			{
				uint num5 = (rightLength > 2) ? right[rightLength - 3] : 0U;
				num = (num << num3 | num2 >> num4);
				num2 = (num2 << num3 | num5 >> num4);
			}
			for (int i = leftLength; i >= rightLength; i--)
			{
				int num6 = i - rightLength;
				uint num7 = (i < leftLength) ? left[i] : 0U;
				ulong num8 = (ulong)num7 << 32 | (ulong)left[i - 1];
				uint num9 = (i > 1) ? left[i - 2] : 0U;
				if (num3 > 0)
				{
					uint num10 = (i > 2) ? left[i - 3] : 0U;
					num8 = (num8 << num3 | (ulong)(num9 >> num4));
					num9 = (num9 << num3 | num10 >> num4);
				}
				ulong num11 = num8 / (ulong)num;
				if (num11 > (ulong)-1)
				{
					num11 = (ulong)-1;
				}
				while (BigIntegerCalculator.DivideGuessTooBig(num11, num8, num9, num, num2))
				{
					num11 -= 1UL;
				}
				if (num11 > 0UL && BigIntegerCalculator.SubtractDivisor(left + num6, leftLength - num6, right, rightLength, num11) != num7)
				{
					BigIntegerCalculator.AddDivisor(left + num6, leftLength - num6, right, rightLength);
					num11 -= 1UL;
				}
				if (bitsLength != 0)
				{
					bits[num6] = (uint)num11;
				}
				if (i < leftLength)
				{
					left[i] = 0U;
				}
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000DDD4 File Offset: 0x0000BFD4
		private unsafe static uint AddDivisor(uint* left, int leftLength, uint* right, int rightLength)
		{
			ulong num = 0UL;
			for (int i = 0; i < rightLength; i++)
			{
				ulong num2 = (ulong)left[i] + num + (ulong)right[i];
				left[i] = (uint)num2;
				num = num2 >> 32;
			}
			return (uint)num;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000DE14 File Offset: 0x0000C014
		private unsafe static uint SubtractDivisor(uint* left, int leftLength, uint* right, int rightLength, ulong q)
		{
			ulong num = 0UL;
			for (int i = 0; i < rightLength; i++)
			{
				num += (ulong)right[i] * q;
				uint num2 = (uint)num;
				num >>= 32;
				if (left[i] < num2)
				{
					num += 1UL;
				}
				left[i] = left[i] - num2;
			}
			return (uint)num;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000DE68 File Offset: 0x0000C068
		private static bool DivideGuessTooBig(ulong q, ulong valHi, uint valLo, uint divHi, uint divLo)
		{
			ulong num = (ulong)divHi * q;
			ulong num2 = (ulong)divLo * q;
			num += num2 >> 32;
			num2 &= (ulong)-1;
			return num >= valHi && (num > valHi || (num2 >= (ulong)valLo && num2 > (ulong)valLo));
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000DEA8 File Offset: 0x0000C0A8
		private static uint[] CreateCopy(uint[] value)
		{
			uint[] array = new uint[value.Length];
			Array.Copy(value, 0, array, 0, array.Length);
			return array;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000DECC File Offset: 0x0000C0CC
		private static int LeadingZeros(uint value)
		{
			if (value == 0U)
			{
				return 32;
			}
			int num = 0;
			if ((value & 4294901760U) == 0U)
			{
				num += 16;
				value <<= 16;
			}
			if ((value & 4278190080U) == 0U)
			{
				num += 8;
				value <<= 8;
			}
			if ((value & 4026531840U) == 0U)
			{
				num += 4;
				value <<= 4;
			}
			if ((value & 3221225472U) == 0U)
			{
				num += 2;
				value <<= 2;
			}
			if ((value & 2147483648U) == 0U)
			{
				num++;
			}
			return num;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000DF39 File Offset: 0x0000C139
		public static uint Gcd(uint left, uint right)
		{
			while (right != 0U)
			{
				uint num = left % right;
				left = right;
				right = num;
			}
			return left;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000DF49 File Offset: 0x0000C149
		public static ulong Gcd(ulong left, ulong right)
		{
			while (right > (ulong)-1)
			{
				ulong num = left % right;
				left = right;
				right = num;
			}
			if (right != 0UL)
			{
				return (ulong)BigIntegerCalculator.Gcd((uint)right, (uint)(left % right));
			}
			return left;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000DF6C File Offset: 0x0000C16C
		public static uint Gcd(uint[] left, uint right)
		{
			uint right2 = BigIntegerCalculator.Remainder(left, right);
			return BigIntegerCalculator.Gcd(right, right2);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000DF88 File Offset: 0x0000C188
		public static uint[] Gcd(uint[] left, uint[] right)
		{
			BigIntegerCalculator.BitsBuffer bitsBuffer = new BigIntegerCalculator.BitsBuffer(left.Length, left);
			BigIntegerCalculator.BitsBuffer bitsBuffer2 = new BigIntegerCalculator.BitsBuffer(right.Length, right);
			BigIntegerCalculator.Gcd(ref bitsBuffer, ref bitsBuffer2);
			return bitsBuffer.GetBits();
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000DFBC File Offset: 0x0000C1BC
		private static void Gcd(ref BigIntegerCalculator.BitsBuffer left, ref BigIntegerCalculator.BitsBuffer right)
		{
			while (right.GetLength() > 2)
			{
				ulong num;
				ulong num2;
				BigIntegerCalculator.ExtractDigits(ref left, ref right, out num, out num2);
				uint num3 = 1U;
				uint num4 = 0U;
				uint num5 = 0U;
				uint num6 = 1U;
				int num7 = 0;
				while (num2 != 0UL)
				{
					ulong num8 = num / num2;
					if (num8 > (ulong)-1)
					{
						break;
					}
					ulong num9 = (ulong)num3 + num8 * (ulong)num5;
					ulong num10 = (ulong)num4 + num8 * (ulong)num6;
					ulong num11 = num - num8 * num2;
					if (num9 > 2147483647UL || num10 > 2147483647UL || num11 < num10 || num11 + num9 > num2 - (ulong)num5)
					{
						break;
					}
					num3 = (uint)num9;
					num4 = (uint)num10;
					num = num11;
					num7++;
					if (num == (ulong)num4)
					{
						break;
					}
					num8 = num2 / num;
					if (num8 > (ulong)-1)
					{
						break;
					}
					num9 = (ulong)num6 + num8 * (ulong)num4;
					num10 = (ulong)num5 + num8 * (ulong)num3;
					num11 = num2 - num8 * num;
					if (num9 > 2147483647UL || num10 > 2147483647UL || num11 < num10 || num11 + num9 > num - (ulong)num4)
					{
						break;
					}
					num6 = (uint)num9;
					num5 = (uint)num10;
					num2 = num11;
					num7++;
					if (num2 == (ulong)num5)
					{
						break;
					}
				}
				if (num4 == 0U)
				{
					left.Reduce(ref right);
					BigIntegerCalculator.BitsBuffer bitsBuffer = left;
					left = right;
					right = bitsBuffer;
				}
				else
				{
					BigIntegerCalculator.LehmerCore(ref left, ref right, (long)((ulong)num3), (long)((ulong)num4), (long)((ulong)num5), (long)((ulong)num6));
					if (num7 % 2 == 1)
					{
						BigIntegerCalculator.BitsBuffer bitsBuffer2 = left;
						left = right;
						right = bitsBuffer2;
					}
				}
			}
			if (right.GetLength() > 0)
			{
				left.Reduce(ref right);
				uint[] bits = right.GetBits();
				uint[] bits2 = left.GetBits();
				ulong left2 = (ulong)bits[1] << 32 | (ulong)bits[0];
				ulong right2 = (ulong)bits2[1] << 32 | (ulong)bits2[0];
				left.Overwrite(BigIntegerCalculator.Gcd(left2, right2));
				right.Overwrite(0U);
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000E18C File Offset: 0x0000C38C
		private static void ExtractDigits(ref BigIntegerCalculator.BitsBuffer xBuffer, ref BigIntegerCalculator.BitsBuffer yBuffer, out ulong x, out ulong y)
		{
			uint[] bits = xBuffer.GetBits();
			int length = xBuffer.GetLength();
			uint[] bits2 = yBuffer.GetBits();
			int length2 = yBuffer.GetLength();
			ulong num = (ulong)bits[length - 1];
			ulong num2 = (ulong)bits[length - 2];
			ulong num3 = (ulong)bits[length - 3];
			ulong num4;
			ulong num5;
			ulong num6;
			switch (length - length2)
			{
			case 0:
				num4 = (ulong)bits2[length2 - 1];
				num5 = (ulong)bits2[length2 - 2];
				num6 = (ulong)bits2[length2 - 3];
				break;
			case 1:
				num4 = 0UL;
				num5 = (ulong)bits2[length2 - 1];
				num6 = (ulong)bits2[length2 - 2];
				break;
			case 2:
				num4 = 0UL;
				num5 = 0UL;
				num6 = (ulong)bits2[length2 - 1];
				break;
			default:
				num4 = 0UL;
				num5 = 0UL;
				num6 = 0UL;
				break;
			}
			int num7 = BigIntegerCalculator.LeadingZeros((uint)num);
			x = (num << 32 + num7 | num2 << num7 | num3 >> 32 - num7) >> 1;
			y = (num4 << 32 + num7 | num5 << num7 | num6 >> 32 - num7) >> 1;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000E284 File Offset: 0x0000C484
		private static void LehmerCore(ref BigIntegerCalculator.BitsBuffer xBuffer, ref BigIntegerCalculator.BitsBuffer yBuffer, long a, long b, long c, long d)
		{
			uint[] bits = xBuffer.GetBits();
			uint[] bits2 = yBuffer.GetBits();
			int length = yBuffer.GetLength();
			long num = 0L;
			long num2 = 0L;
			for (int i = 0; i < length; i++)
			{
				long num3 = a * (long)((ulong)bits[i]) - b * (long)((ulong)bits2[i]) + num;
				long num4 = d * (long)((ulong)bits2[i]) - c * (long)((ulong)bits[i]) + num2;
				num = num3 >> 32;
				num2 = num4 >> 32;
				bits[i] = (uint)num3;
				bits2[i] = (uint)num4;
			}
			xBuffer.Refresh(length);
			yBuffer.Refresh(length);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000E310 File Offset: 0x0000C510
		public static uint[] Pow(uint value, uint power)
		{
			int size = BigIntegerCalculator.PowBound(power, 1, 1);
			BigIntegerCalculator.BitsBuffer bitsBuffer = new BigIntegerCalculator.BitsBuffer(size, value);
			return BigIntegerCalculator.PowCore(power, ref bitsBuffer);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000E338 File Offset: 0x0000C538
		public static uint[] Pow(uint[] value, uint power)
		{
			int size = BigIntegerCalculator.PowBound(power, value.Length, 1);
			BigIntegerCalculator.BitsBuffer bitsBuffer = new BigIntegerCalculator.BitsBuffer(size, value);
			return BigIntegerCalculator.PowCore(power, ref bitsBuffer);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000E364 File Offset: 0x0000C564
		private static uint[] PowCore(uint power, ref BigIntegerCalculator.BitsBuffer value)
		{
			int size = value.GetSize();
			BigIntegerCalculator.BitsBuffer bitsBuffer = new BigIntegerCalculator.BitsBuffer(size, 0U);
			BigIntegerCalculator.BitsBuffer bitsBuffer2 = new BigIntegerCalculator.BitsBuffer(size, 1U);
			BigIntegerCalculator.PowCore(power, ref value, ref bitsBuffer2, ref bitsBuffer);
			return bitsBuffer2.GetBits();
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000E39C File Offset: 0x0000C59C
		private static int PowBound(uint power, int valueLength, int resultLength)
		{
			checked
			{
				while (power != 0U)
				{
					if ((power & 1U) == 1U)
					{
						resultLength += valueLength;
					}
					if (power != 1U)
					{
						valueLength += valueLength;
					}
					power >>= 1;
				}
				return resultLength;
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000E3BD File Offset: 0x0000C5BD
		private static void PowCore(uint power, ref BigIntegerCalculator.BitsBuffer value, ref BigIntegerCalculator.BitsBuffer result, ref BigIntegerCalculator.BitsBuffer temp)
		{
			while (power != 0U)
			{
				if ((power & 1U) == 1U)
				{
					result.MultiplySelf(ref value, ref temp);
				}
				if (power != 1U)
				{
					value.SquareSelf(ref temp);
				}
				power >>= 1;
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000E3E2 File Offset: 0x0000C5E2
		public static uint Pow(uint value, uint power, uint modulus)
		{
			return BigIntegerCalculator.PowCore(power, modulus, (ulong)value, 1UL);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000E3F0 File Offset: 0x0000C5F0
		public static uint Pow(uint[] value, uint power, uint modulus)
		{
			uint num = BigIntegerCalculator.Remainder(value, modulus);
			return BigIntegerCalculator.PowCore(power, modulus, (ulong)num, 1UL);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000E410 File Offset: 0x0000C610
		public static uint Pow(uint value, uint[] power, uint modulus)
		{
			return BigIntegerCalculator.PowCore(power, modulus, (ulong)value, 1UL);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000E420 File Offset: 0x0000C620
		public static uint Pow(uint[] value, uint[] power, uint modulus)
		{
			uint num = BigIntegerCalculator.Remainder(value, modulus);
			return BigIntegerCalculator.PowCore(power, modulus, (ulong)num, 1UL);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000E440 File Offset: 0x0000C640
		private static uint PowCore(uint[] power, uint modulus, ulong value, ulong result)
		{
			for (int i = 0; i < power.Length - 1; i++)
			{
				uint num = power[i];
				for (int j = 0; j < 32; j++)
				{
					if ((num & 1U) == 1U)
					{
						result = result * value % (ulong)modulus;
					}
					value = value * value % (ulong)modulus;
					num >>= 1;
				}
			}
			return BigIntegerCalculator.PowCore(power[power.Length - 1], modulus, value, result);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000E497 File Offset: 0x0000C697
		private static uint PowCore(uint power, uint modulus, ulong value, ulong result)
		{
			while (power != 0U)
			{
				if ((power & 1U) == 1U)
				{
					result = result * value % (ulong)modulus;
				}
				if (power != 1U)
				{
					value = value * value % (ulong)modulus;
				}
				power >>= 1;
			}
			return (uint)(result % (ulong)modulus);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000E4C4 File Offset: 0x0000C6C4
		public static uint[] Pow(uint value, uint power, uint[] modulus)
		{
			int size = modulus.Length + modulus.Length;
			BigIntegerCalculator.BitsBuffer bitsBuffer = new BigIntegerCalculator.BitsBuffer(size, value);
			return BigIntegerCalculator.PowCore(power, modulus, ref bitsBuffer);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000E4EC File Offset: 0x0000C6EC
		public static uint[] Pow(uint[] value, uint power, uint[] modulus)
		{
			if (value.Length > modulus.Length)
			{
				value = BigIntegerCalculator.Remainder(value, modulus);
			}
			int size = modulus.Length + modulus.Length;
			BigIntegerCalculator.BitsBuffer bitsBuffer = new BigIntegerCalculator.BitsBuffer(size, value);
			return BigIntegerCalculator.PowCore(power, modulus, ref bitsBuffer);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000E524 File Offset: 0x0000C724
		public static uint[] Pow(uint value, uint[] power, uint[] modulus)
		{
			int size = modulus.Length + modulus.Length;
			BigIntegerCalculator.BitsBuffer bitsBuffer = new BigIntegerCalculator.BitsBuffer(size, value);
			return BigIntegerCalculator.PowCore(power, modulus, ref bitsBuffer);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000E54C File Offset: 0x0000C74C
		public static uint[] Pow(uint[] value, uint[] power, uint[] modulus)
		{
			if (value.Length > modulus.Length)
			{
				value = BigIntegerCalculator.Remainder(value, modulus);
			}
			int size = modulus.Length + modulus.Length;
			BigIntegerCalculator.BitsBuffer bitsBuffer = new BigIntegerCalculator.BitsBuffer(size, value);
			return BigIntegerCalculator.PowCore(power, modulus, ref bitsBuffer);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000E584 File Offset: 0x0000C784
		private static uint[] PowCore(uint[] power, uint[] modulus, ref BigIntegerCalculator.BitsBuffer value)
		{
			int size = value.GetSize();
			BigIntegerCalculator.BitsBuffer bitsBuffer = new BigIntegerCalculator.BitsBuffer(size, 0U);
			BigIntegerCalculator.BitsBuffer bitsBuffer2 = new BigIntegerCalculator.BitsBuffer(size, 1U);
			if (modulus.Length < BigIntegerCalculator.ReducerThreshold)
			{
				BigIntegerCalculator.PowCore(power, modulus, ref value, ref bitsBuffer2, ref bitsBuffer);
			}
			else
			{
				BigIntegerCalculator.FastReducer fastReducer = new BigIntegerCalculator.FastReducer(modulus);
				BigIntegerCalculator.PowCore(power, ref fastReducer, ref value, ref bitsBuffer2, ref bitsBuffer);
			}
			return bitsBuffer2.GetBits();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000E5E0 File Offset: 0x0000C7E0
		private static uint[] PowCore(uint power, uint[] modulus, ref BigIntegerCalculator.BitsBuffer value)
		{
			int size = value.GetSize();
			BigIntegerCalculator.BitsBuffer bitsBuffer = new BigIntegerCalculator.BitsBuffer(size, 0U);
			BigIntegerCalculator.BitsBuffer bitsBuffer2 = new BigIntegerCalculator.BitsBuffer(size, 1U);
			if (modulus.Length < BigIntegerCalculator.ReducerThreshold)
			{
				BigIntegerCalculator.PowCore(power, modulus, ref value, ref bitsBuffer2, ref bitsBuffer);
			}
			else
			{
				BigIntegerCalculator.FastReducer fastReducer = new BigIntegerCalculator.FastReducer(modulus);
				BigIntegerCalculator.PowCore(power, ref fastReducer, ref value, ref bitsBuffer2, ref bitsBuffer);
			}
			return bitsBuffer2.GetBits();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000E63C File Offset: 0x0000C83C
		private static void PowCore(uint[] power, uint[] modulus, ref BigIntegerCalculator.BitsBuffer value, ref BigIntegerCalculator.BitsBuffer result, ref BigIntegerCalculator.BitsBuffer temp)
		{
			for (int i = 0; i < power.Length - 1; i++)
			{
				uint num = power[i];
				for (int j = 0; j < 32; j++)
				{
					if ((num & 1U) == 1U)
					{
						result.MultiplySelf(ref value, ref temp);
						result.Reduce(modulus);
					}
					value.SquareSelf(ref temp);
					value.Reduce(modulus);
					num >>= 1;
				}
			}
			BigIntegerCalculator.PowCore(power[power.Length - 1], modulus, ref value, ref result, ref temp);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000E6A4 File Offset: 0x0000C8A4
		private static void PowCore(uint power, uint[] modulus, ref BigIntegerCalculator.BitsBuffer value, ref BigIntegerCalculator.BitsBuffer result, ref BigIntegerCalculator.BitsBuffer temp)
		{
			while (power != 0U)
			{
				if ((power & 1U) == 1U)
				{
					result.MultiplySelf(ref value, ref temp);
					result.Reduce(modulus);
				}
				if (power != 1U)
				{
					value.SquareSelf(ref temp);
					value.Reduce(modulus);
				}
				power >>= 1;
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000E6DC File Offset: 0x0000C8DC
		private static void PowCore(uint[] power, ref BigIntegerCalculator.FastReducer reducer, ref BigIntegerCalculator.BitsBuffer value, ref BigIntegerCalculator.BitsBuffer result, ref BigIntegerCalculator.BitsBuffer temp)
		{
			for (int i = 0; i < power.Length - 1; i++)
			{
				uint num = power[i];
				for (int j = 0; j < 32; j++)
				{
					if ((num & 1U) == 1U)
					{
						result.MultiplySelf(ref value, ref temp);
						result.Reduce(ref reducer);
					}
					value.SquareSelf(ref temp);
					value.Reduce(ref reducer);
					num >>= 1;
				}
			}
			BigIntegerCalculator.PowCore(power[power.Length - 1], ref reducer, ref value, ref result, ref temp);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000E744 File Offset: 0x0000C944
		private static void PowCore(uint power, ref BigIntegerCalculator.FastReducer reducer, ref BigIntegerCalculator.BitsBuffer value, ref BigIntegerCalculator.BitsBuffer result, ref BigIntegerCalculator.BitsBuffer temp)
		{
			while (power != 0U)
			{
				if ((power & 1U) == 1U)
				{
					result.MultiplySelf(ref value, ref temp);
					result.Reduce(ref reducer);
				}
				if (power != 1U)
				{
					value.SquareSelf(ref temp);
					value.Reduce(ref reducer);
				}
				power >>= 1;
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000E779 File Offset: 0x0000C979
		private static int ActualLength(uint[] value)
		{
			return BigIntegerCalculator.ActualLength(value, value.Length);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000E784 File Offset: 0x0000C984
		private static int ActualLength(uint[] value, int length)
		{
			while (length > 0 && value[length - 1] == 0U)
			{
				length--;
			}
			return length;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000E79C File Offset: 0x0000C99C
		public unsafe static uint[] Square(uint[] value)
		{
			uint[] array = new uint[value.Length + value.Length];
			uint[] array3;
			fixed (uint[] array2 = value)
			{
				uint* value2;
				if (value == null || array2.Length == 0)
				{
					value2 = null;
				}
				else
				{
					value2 = &array2[0];
				}
				uint* bits;
				if ((array3 = array) == null || array3.Length == 0)
				{
					bits = null;
				}
				else
				{
					bits = &array3[0];
				}
				BigIntegerCalculator.Square(value2, value.Length, bits, array.Length);
			}
			array3 = null;
			return array;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000E7FC File Offset: 0x0000C9FC
		private unsafe static void Square(uint* value, int valueLength, uint* bits, int bitsLength)
		{
			if (valueLength < BigIntegerCalculator.SquareThreshold)
			{
				for (int i = 0; i < valueLength; i++)
				{
					ulong num = 0UL;
					for (int j = 0; j < i; j++)
					{
						ulong num2 = (ulong)bits[i + j] + num;
						ulong num3 = (ulong)value[j] * (ulong)value[i];
						bits[i + j] = (uint)(num2 + (num3 << 1));
						num = num3 + (num2 >> 1) >> 31;
					}
					ulong num4 = (ulong)value[i] * (ulong)value[i] + num;
					bits[i + i] = (uint)num4;
					bits[i + i + 1] = (uint)(num4 >> 32);
				}
				return;
			}
			int num5 = valueLength >> 1;
			int num6 = num5 << 1;
			int num7 = num5;
			uint* ptr = value + num5;
			int num8 = valueLength - num5;
			int num9 = num6;
			uint* ptr2 = bits + num6;
			int num10 = bitsLength - num6;
			BigIntegerCalculator.Square(value, num7, bits, num9);
			BigIntegerCalculator.Square(ptr, num8, ptr2, num10);
			int num11 = num8 + 1;
			int num12 = num11 + num11;
			if (num12 < BigIntegerCalculator.AllocationThreshold)
			{
				uint* ptr4;
				checked
				{
					uint* ptr3 = stackalloc uint[unchecked((UIntPtr)num11) * 4];
					ptr4 = stackalloc uint[unchecked((UIntPtr)num12) * 4];
					BigIntegerCalculator.Add(ptr, num8, value, num7, ptr3, num11);
					BigIntegerCalculator.Square(ptr3, num11, ptr4, num12);
					BigIntegerCalculator.SubtractCore(ptr2, num10, bits, num9, ptr4, num12);
				}
				BigIntegerCalculator.AddSelf(bits + num5, bitsLength - num5, ptr4, num12);
				return;
			}
			uint[] array;
			uint* ptr5;
			if ((array = new uint[num11]) == null || array.Length == 0)
			{
				ptr5 = null;
			}
			else
			{
				ptr5 = &array[0];
			}
			uint[] array2;
			uint* ptr6;
			if ((array2 = new uint[num12]) == null || array2.Length == 0)
			{
				ptr6 = null;
			}
			else
			{
				ptr6 = &array2[0];
			}
			BigIntegerCalculator.Add(ptr, num8, value, num7, ptr5, num11);
			BigIntegerCalculator.Square(ptr5, num11, ptr6, num12);
			BigIntegerCalculator.SubtractCore(ptr2, num10, bits, num9, ptr6, num12);
			BigIntegerCalculator.AddSelf(bits + num5, bitsLength - num5, ptr6, num12);
			array = null;
			array2 = null;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000E9EC File Offset: 0x0000CBEC
		public static uint[] Multiply(uint[] left, uint right)
		{
			int i = 0;
			ulong num = 0UL;
			uint[] array = new uint[left.Length + 1];
			while (i < left.Length)
			{
				ulong num2 = (ulong)left[i] * (ulong)right + num;
				array[i] = (uint)num2;
				num = num2 >> 32;
				i++;
			}
			array[i] = (uint)num;
			return array;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000EA30 File Offset: 0x0000CC30
		public unsafe static uint[] Multiply(uint[] left, uint[] right)
		{
			uint[] array = new uint[left.Length + right.Length];
			fixed (uint[] array2 = left)
			{
				uint* left2;
				if (left == null || array2.Length == 0)
				{
					left2 = null;
				}
				else
				{
					left2 = &array2[0];
				}
				uint[] array4;
				fixed (uint[] array3 = right)
				{
					uint* right2;
					if (right == null || array3.Length == 0)
					{
						right2 = null;
					}
					else
					{
						right2 = &array3[0];
					}
					uint* bits;
					if ((array4 = array) == null || array4.Length == 0)
					{
						bits = null;
					}
					else
					{
						bits = &array4[0];
					}
					BigIntegerCalculator.Multiply(left2, left.Length, right2, right.Length, bits, array.Length);
					array2 = null;
				}
				array4 = null;
				return array;
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000EAB8 File Offset: 0x0000CCB8
		private unsafe static void Multiply(uint* left, int leftLength, uint* right, int rightLength, uint* bits, int bitsLength)
		{
			if (rightLength < BigIntegerCalculator.MultiplyThreshold)
			{
				for (int i = 0; i < rightLength; i++)
				{
					ulong num = 0UL;
					for (int j = 0; j < leftLength; j++)
					{
						ulong num2 = (ulong)bits[i + j] + num + (ulong)left[j] * (ulong)right[i];
						bits[i + j] = (uint)num2;
						num = num2 >> 32;
					}
					bits[i + leftLength] = (uint)num;
				}
				return;
			}
			int num3 = rightLength >> 1;
			int num4 = num3 << 1;
			int num5 = num3;
			uint* left2 = left + num3;
			int num6 = leftLength - num3;
			int rightLength2 = num3;
			uint* ptr = right + num3;
			int num7 = rightLength - num3;
			int num8 = num4;
			uint* ptr2 = bits + num4;
			int num9 = bitsLength - num4;
			BigIntegerCalculator.Multiply(left, num5, right, rightLength2, bits, num8);
			BigIntegerCalculator.Multiply(left2, num6, ptr, num7, ptr2, num9);
			int num10 = num6 + 1;
			int num11 = num7 + 1;
			int num12 = num10 + num11;
			if (num12 < BigIntegerCalculator.AllocationThreshold)
			{
				uint* ptr5;
				checked
				{
					uint* ptr3 = stackalloc uint[unchecked((UIntPtr)num10) * 4];
					uint* ptr4 = stackalloc uint[unchecked((UIntPtr)num11) * 4];
					ptr5 = stackalloc uint[unchecked((UIntPtr)num12) * 4];
					BigIntegerCalculator.Add(left2, num6, left, num5, ptr3, num10);
					BigIntegerCalculator.Add(ptr, num7, right, rightLength2, ptr4, num11);
					BigIntegerCalculator.Multiply(ptr3, num10, ptr4, num11, ptr5, num12);
					BigIntegerCalculator.SubtractCore(ptr2, num9, bits, num8, ptr5, num12);
				}
				BigIntegerCalculator.AddSelf(bits + num3, bitsLength - num3, ptr5, num12);
				return;
			}
			uint[] array;
			uint* ptr6;
			if ((array = new uint[num10]) == null || array.Length == 0)
			{
				ptr6 = null;
			}
			else
			{
				ptr6 = &array[0];
			}
			uint[] array2;
			uint* ptr7;
			if ((array2 = new uint[num11]) == null || array2.Length == 0)
			{
				ptr7 = null;
			}
			else
			{
				ptr7 = &array2[0];
			}
			uint[] array3;
			uint* ptr8;
			if ((array3 = new uint[num12]) == null || array3.Length == 0)
			{
				ptr8 = null;
			}
			else
			{
				ptr8 = &array3[0];
			}
			BigIntegerCalculator.Add(left2, num6, left, num5, ptr6, num10);
			BigIntegerCalculator.Add(ptr, num7, right, rightLength2, ptr7, num11);
			BigIntegerCalculator.Multiply(ptr6, num10, ptr7, num11, ptr8, num12);
			BigIntegerCalculator.SubtractCore(ptr2, num9, bits, num8, ptr8, num12);
			BigIntegerCalculator.AddSelf(bits + num3, bitsLength - num3, ptr8, num12);
			array = null;
			array2 = null;
			array3 = null;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000ECF8 File Offset: 0x0000CEF8
		private unsafe static void SubtractCore(uint* left, int leftLength, uint* right, int rightLength, uint* core, int coreLength)
		{
			int i = 0;
			long num = 0L;
			while (i < rightLength)
			{
				long num2 = (long)((ulong)core[i] + (ulong)num - (ulong)left[i] - (ulong)right[i]);
				core[i] = (uint)num2;
				num = num2 >> 32;
				i++;
			}
			while (i < leftLength)
			{
				long num3 = (long)((ulong)core[i] + (ulong)num - (ulong)left[i]);
				core[i] = (uint)num3;
				num = num3 >> 32;
				i++;
			}
			while (num != 0L && i < coreLength)
			{
				long num4 = (long)((ulong)core[i] + (ulong)num);
				core[i] = (uint)num4;
				num = num4 >> 32;
				i++;
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000ED9B File Offset: 0x0000CF9B
		// Note: this type is marked as 'beforefieldinit'.
		static BigIntegerCalculator()
		{
		}

		// Token: 0x0400007C RID: 124
		private static int ReducerThreshold = 32;

		// Token: 0x0400007D RID: 125
		private static int SquareThreshold = 32;

		// Token: 0x0400007E RID: 126
		private static int AllocationThreshold = 256;

		// Token: 0x0400007F RID: 127
		private static int MultiplyThreshold = 32;

		// Token: 0x02000010 RID: 16
		internal struct BitsBuffer
		{
			// Token: 0x060001FE RID: 510 RVA: 0x0000EDBC File Offset: 0x0000CFBC
			public BitsBuffer(int size, uint value)
			{
				this._bits = new uint[size];
				this._length = ((value != 0U) ? 1 : 0);
				this._bits[0] = value;
			}

			// Token: 0x060001FF RID: 511 RVA: 0x0000EDE0 File Offset: 0x0000CFE0
			public BitsBuffer(int size, uint[] value)
			{
				this._bits = new uint[size];
				this._length = BigIntegerCalculator.ActualLength(value);
				Array.Copy(value, 0, this._bits, 0, this._length);
			}

			// Token: 0x06000200 RID: 512 RVA: 0x0000EE10 File Offset: 0x0000D010
			public unsafe void MultiplySelf(ref BigIntegerCalculator.BitsBuffer value, ref BigIntegerCalculator.BitsBuffer temp)
			{
				uint[] array;
				uint* ptr;
				if ((array = this._bits) == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				uint[] array2;
				uint* ptr2;
				if ((array2 = value._bits) == null || array2.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array2[0];
				}
				uint[] array3;
				uint* bits;
				if ((array3 = temp._bits) == null || array3.Length == 0)
				{
					bits = null;
				}
				else
				{
					bits = &array3[0];
				}
				if (this._length < value._length)
				{
					BigIntegerCalculator.Multiply(ptr2, value._length, ptr, this._length, bits, this._length + value._length);
				}
				else
				{
					BigIntegerCalculator.Multiply(ptr, this._length, ptr2, value._length, bits, this._length + value._length);
				}
				array = null;
				array2 = null;
				array3 = null;
				this.Apply(ref temp, this._length + value._length);
			}

			// Token: 0x06000201 RID: 513 RVA: 0x0000EEE8 File Offset: 0x0000D0E8
			public unsafe void SquareSelf(ref BigIntegerCalculator.BitsBuffer temp)
			{
				uint[] array;
				uint* value;
				if ((array = this._bits) == null || array.Length == 0)
				{
					value = null;
				}
				else
				{
					value = &array[0];
				}
				uint[] array2;
				uint* bits;
				if ((array2 = temp._bits) == null || array2.Length == 0)
				{
					bits = null;
				}
				else
				{
					bits = &array2[0];
				}
				BigIntegerCalculator.Square(value, this._length, bits, this._length + this._length);
				array = null;
				array2 = null;
				this.Apply(ref temp, this._length + this._length);
			}

			// Token: 0x06000202 RID: 514 RVA: 0x0000EF61 File Offset: 0x0000D161
			public void Reduce(ref BigIntegerCalculator.FastReducer reducer)
			{
				this._length = reducer.Reduce(this._bits, this._length);
			}

			// Token: 0x06000203 RID: 515 RVA: 0x0000EF7C File Offset: 0x0000D17C
			public unsafe void Reduce(uint[] modulus)
			{
				if (this._length >= modulus.Length)
				{
					uint[] array;
					uint* left;
					if ((array = this._bits) == null || array.Length == 0)
					{
						left = null;
					}
					else
					{
						left = &array[0];
					}
					fixed (uint[] array2 = modulus)
					{
						uint* right;
						if (modulus == null || array2.Length == 0)
						{
							right = null;
						}
						else
						{
							right = &array2[0];
						}
						BigIntegerCalculator.Divide(left, this._length, right, modulus.Length, null, 0);
						array = null;
					}
					this._length = BigIntegerCalculator.ActualLength(this._bits, modulus.Length);
				}
			}

			// Token: 0x06000204 RID: 516 RVA: 0x0000EFF4 File Offset: 0x0000D1F4
			public unsafe void Reduce(ref BigIntegerCalculator.BitsBuffer modulus)
			{
				if (this._length >= modulus._length)
				{
					uint[] array;
					uint* left;
					if ((array = this._bits) == null || array.Length == 0)
					{
						left = null;
					}
					else
					{
						left = &array[0];
					}
					uint[] array2;
					uint* right;
					if ((array2 = modulus._bits) == null || array2.Length == 0)
					{
						right = null;
					}
					else
					{
						right = &array2[0];
					}
					BigIntegerCalculator.Divide(left, this._length, right, modulus._length, null, 0);
					array = null;
					array2 = null;
					this._length = BigIntegerCalculator.ActualLength(this._bits, modulus._length);
				}
			}

			// Token: 0x06000205 RID: 517 RVA: 0x0000F07C File Offset: 0x0000D27C
			public void Overwrite(ulong value)
			{
				if (this._length > 2)
				{
					Array.Clear(this._bits, 2, this._length - 2);
				}
				uint num = (uint)value;
				uint num2 = (uint)(value >> 32);
				this._bits[0] = num;
				this._bits[1] = num2;
				this._length = ((num2 != 0U) ? 2 : ((num != 0U) ? 1 : 0));
			}

			// Token: 0x06000206 RID: 518 RVA: 0x0000F0D4 File Offset: 0x0000D2D4
			public void Overwrite(uint value)
			{
				if (this._length > 1)
				{
					Array.Clear(this._bits, 1, this._length - 1);
				}
				this._bits[0] = value;
				this._length = ((value != 0U) ? 1 : 0);
			}

			// Token: 0x06000207 RID: 519 RVA: 0x0000F109 File Offset: 0x0000D309
			public uint[] GetBits()
			{
				return this._bits;
			}

			// Token: 0x06000208 RID: 520 RVA: 0x0000F111 File Offset: 0x0000D311
			public int GetSize()
			{
				return this._bits.Length;
			}

			// Token: 0x06000209 RID: 521 RVA: 0x0000F11B File Offset: 0x0000D31B
			public int GetLength()
			{
				return this._length;
			}

			// Token: 0x0600020A RID: 522 RVA: 0x0000F123 File Offset: 0x0000D323
			public void Refresh(int maxLength)
			{
				if (this._length > maxLength)
				{
					Array.Clear(this._bits, maxLength, this._length - maxLength);
				}
				this._length = BigIntegerCalculator.ActualLength(this._bits, maxLength);
			}

			// Token: 0x0600020B RID: 523 RVA: 0x0000F154 File Offset: 0x0000D354
			private void Apply(ref BigIntegerCalculator.BitsBuffer temp, int maxLength)
			{
				Array.Clear(this._bits, 0, this._length);
				uint[] bits = temp._bits;
				temp._bits = this._bits;
				this._bits = bits;
				this._length = BigIntegerCalculator.ActualLength(this._bits, maxLength);
			}

			// Token: 0x04000080 RID: 128
			private uint[] _bits;

			// Token: 0x04000081 RID: 129
			private int _length;
		}

		// Token: 0x02000011 RID: 17
		internal readonly struct FastReducer
		{
			// Token: 0x0600020C RID: 524 RVA: 0x0000F1A0 File Offset: 0x0000D3A0
			public FastReducer(uint[] modulus)
			{
				uint[] array = new uint[modulus.Length * 2 + 1];
				array[array.Length - 1] = 1U;
				this._mu = BigIntegerCalculator.Divide(array, modulus);
				this._modulus = modulus;
				this._q1 = new uint[modulus.Length * 2 + 2];
				this._q2 = new uint[modulus.Length * 2 + 1];
				this._muLength = BigIntegerCalculator.ActualLength(this._mu);
			}

			// Token: 0x0600020D RID: 525 RVA: 0x0000F20C File Offset: 0x0000D40C
			public int Reduce(uint[] value, int length)
			{
				if (length < this._modulus.Length)
				{
					return length;
				}
				int leftLength = BigIntegerCalculator.FastReducer.DivMul(value, length, this._mu, this._muLength, this._q1, this._modulus.Length - 1);
				int rightLength = BigIntegerCalculator.FastReducer.DivMul(this._q1, leftLength, this._modulus, this._modulus.Length, this._q2, this._modulus.Length + 1);
				return BigIntegerCalculator.FastReducer.SubMod(value, length, this._q2, rightLength, this._modulus, this._modulus.Length + 1);
			}

			// Token: 0x0600020E RID: 526 RVA: 0x0000F294 File Offset: 0x0000D494
			private unsafe static int DivMul(uint[] left, int leftLength, uint[] right, int rightLength, uint[] bits, int k)
			{
				Array.Clear(bits, 0, bits.Length);
				if (leftLength > k)
				{
					leftLength -= k;
					fixed (uint[] array = left)
					{
						uint* ptr;
						if (left == null || array.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array[0];
						}
						fixed (uint[] array2 = right)
						{
							uint* ptr2;
							if (right == null || array2.Length == 0)
							{
								ptr2 = null;
							}
							else
							{
								ptr2 = &array2[0];
							}
							fixed (uint[] array3 = bits)
							{
								uint* bits2;
								if (bits == null || array3.Length == 0)
								{
									bits2 = null;
								}
								else
								{
									bits2 = &array3[0];
								}
								if (leftLength < rightLength)
								{
									BigIntegerCalculator.Multiply(ptr2, rightLength, ptr + k, leftLength, bits2, leftLength + rightLength);
								}
								else
								{
									BigIntegerCalculator.Multiply(ptr + k, leftLength, ptr2, rightLength, bits2, leftLength + rightLength);
								}
								array = null;
								array2 = null;
							}
							return BigIntegerCalculator.ActualLength(bits, leftLength + rightLength);
						}
					}
				}
				return 0;
			}

			// Token: 0x0600020F RID: 527 RVA: 0x0000F34C File Offset: 0x0000D54C
			private unsafe static int SubMod(uint[] left, int leftLength, uint[] right, int rightLength, uint[] modulus, int k)
			{
				if (leftLength > k)
				{
					leftLength = k;
				}
				if (rightLength > k)
				{
					rightLength = k;
				}
				fixed (uint[] array = left)
				{
					uint* left2;
					if (left == null || array.Length == 0)
					{
						left2 = null;
					}
					else
					{
						left2 = &array[0];
					}
					fixed (uint[] array2 = right)
					{
						uint* right2;
						if (right == null || array2.Length == 0)
						{
							right2 = null;
						}
						else
						{
							right2 = &array2[0];
						}
						fixed (uint[] array3 = modulus)
						{
							uint* right3;
							if (modulus == null || array3.Length == 0)
							{
								right3 = null;
							}
							else
							{
								right3 = &array3[0];
							}
							BigIntegerCalculator.SubtractSelf(left2, leftLength, right2, rightLength);
							leftLength = BigIntegerCalculator.ActualLength(left, leftLength);
							while (BigIntegerCalculator.Compare(left2, leftLength, right3, modulus.Length) >= 0)
							{
								BigIntegerCalculator.SubtractSelf(left2, leftLength, right3, modulus.Length);
								leftLength = BigIntegerCalculator.ActualLength(left, leftLength);
							}
							array = null;
							array2 = null;
						}
						Array.Clear(left, leftLength, left.Length - leftLength);
						return leftLength;
					}
				}
			}

			// Token: 0x04000082 RID: 130
			private readonly uint[] _modulus;

			// Token: 0x04000083 RID: 131
			private readonly uint[] _mu;

			// Token: 0x04000084 RID: 132
			private readonly uint[] _q1;

			// Token: 0x04000085 RID: 133
			private readonly uint[] _q2;

			// Token: 0x04000086 RID: 134
			private readonly int _muLength;
		}
	}
}
