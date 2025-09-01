using System;
using System.Threading;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x02000044 RID: 68
	internal class ConcurrentMask
	{
		// Token: 0x0600012B RID: 299 RVA: 0x000045C0 File Offset: 0x000027C0
		internal static void longestConsecutiveOnes(long value, out int offset, out int count)
		{
			count = 0;
			long num = value;
			while (num != 0L)
			{
				value = num;
				num = (value & (long)((ulong)value >> 1));
				count++;
			}
			offset = math.tzcnt(value);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000045EE File Offset: 0x000027EE
		internal static bool foundAtLeastThisManyConsecutiveOnes(long value, int minimum, out int offset, out int count)
		{
			if (minimum == 1)
			{
				offset = math.tzcnt(value);
				count = 1;
				return offset != 64;
			}
			ConcurrentMask.longestConsecutiveOnes(value, out offset, out count);
			return count >= minimum;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004619 File Offset: 0x00002819
		internal static bool foundAtLeastThisManyConsecutiveZeroes(long value, int minimum, out int offset, out int count)
		{
			return ConcurrentMask.foundAtLeastThisManyConsecutiveOnes(~value, minimum, out offset, out count);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004625 File Offset: 0x00002825
		internal static bool Succeeded(int error)
		{
			return error >= 0;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000462E File Offset: 0x0000282E
		internal static long MakeMask(int offset, int bits)
		{
			return (long)((long)(ulong.MaxValue >> 64 - bits) << offset);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004640 File Offset: 0x00002840
		internal static int TryAllocate(ref long l, int offset, int bits)
		{
			long num = ConcurrentMask.MakeMask(offset, bits);
			long num2 = Interlocked.Read(ref l);
			while ((num2 & num) == 0L)
			{
				long value = num2 | num;
				long num3 = num2;
				num2 = Interlocked.CompareExchange(ref l, value, num3);
				if (num2 == num3)
				{
					return math.countbits(num2);
				}
			}
			return -2;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004680 File Offset: 0x00002880
		internal static int TryFree(ref long l, int offset, int bits)
		{
			long num = ConcurrentMask.MakeMask(offset, bits);
			long num2 = Interlocked.Read(ref l);
			while ((num2 & num) == num)
			{
				long num3 = num2 & ~num;
				long num4 = num2;
				num2 = Interlocked.CompareExchange(ref l, num3, num4);
				if (num2 == num4)
				{
					return math.countbits(num3);
				}
			}
			return -1;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000046C0 File Offset: 0x000028C0
		internal static int TryAllocate(ref long l, out int offset, int bits)
		{
			long num = Interlocked.Read(ref l);
			int num2;
			while (ConcurrentMask.foundAtLeastThisManyConsecutiveZeroes(num, bits, out offset, out num2))
			{
				long num3 = ConcurrentMask.MakeMask(offset, bits);
				long value = num | num3;
				long num4 = num;
				num = Interlocked.CompareExchange(ref l, value, num4);
				if (num == num4)
				{
					return math.countbits(num);
				}
			}
			return -2;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004708 File Offset: 0x00002908
		internal static int TryAllocate<T>(ref T t, int offset, int bits) where T : IIndexable<long>
		{
			int index = offset >> 6;
			int offset2 = offset & 63;
			return ConcurrentMask.TryAllocate(t.ElementAt(index), offset2, bits);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004734 File Offset: 0x00002934
		internal static int TryFree<T>(ref T t, int offset, int bits) where T : IIndexable<long>
		{
			int index = offset >> 6;
			int offset2 = offset & 63;
			return ConcurrentMask.TryFree(t.ElementAt(index), offset2, bits);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004760 File Offset: 0x00002960
		internal static int TryAllocate<T>(ref T t, out int offset, int begin, int end, int bits) where T : IIndexable<long>
		{
			for (int i = begin; i < end; i++)
			{
				int num2;
				int num = ConcurrentMask.TryAllocate(t.ElementAt(i), out num2, bits);
				if (ConcurrentMask.Succeeded(num))
				{
					offset = i * 64 + num2;
					return num;
				}
			}
			offset = -1;
			return -2;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000047A7 File Offset: 0x000029A7
		internal static int TryAllocate<T>(ref T t, out int offset, int bits) where T : IIndexable<long>
		{
			return ConcurrentMask.TryAllocate<T>(ref t, out offset, 0, t.Length, bits);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000020EA File Offset: 0x000002EA
		public ConcurrentMask()
		{
		}

		// Token: 0x04000096 RID: 150
		internal const int ErrorFailedToFree = -1;

		// Token: 0x04000097 RID: 151
		internal const int ErrorFailedToAllocate = -2;

		// Token: 0x04000098 RID: 152
		internal const int EmptyBeforeAllocation = 0;

		// Token: 0x04000099 RID: 153
		internal const int EmptyAfterFree = 0;
	}
}
