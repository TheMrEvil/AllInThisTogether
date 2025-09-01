using System;
using System.Threading;
using Unity.Mathematics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200011E RID: 286
	[BurstCompatible]
	public struct UnsafeAtomicCounter64
	{
		// Token: 0x06000A7D RID: 2685 RVA: 0x0001EF08 File Offset: 0x0001D108
		public unsafe UnsafeAtomicCounter64(void* ptr)
		{
			this.Counter = (long*)ptr;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0001EF11 File Offset: 0x0001D111
		public unsafe void Reset(long value = 0L)
		{
			*this.Counter = value;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0001EF1B File Offset: 0x0001D11B
		public unsafe long Add(long value)
		{
			return Interlocked.Add(UnsafeUtility.AsRef<long>((void*)this.Counter), value) - value;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0001EF30 File Offset: 0x0001D130
		public long Sub(long value)
		{
			return this.Add(-value);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0001EF3C File Offset: 0x0001D13C
		public unsafe long AddSat(long value, long max = 9223372036854775807L)
		{
			long num = *this.Counter;
			long num2;
			do
			{
				num2 = num;
				num = ((num >= max) ? max : math.min(max, num + value));
				num = Interlocked.CompareExchange(UnsafeUtility.AsRef<long>((void*)this.Counter), num, num2);
			}
			while (num2 != num && num2 != max);
			return num2;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0001EF80 File Offset: 0x0001D180
		public unsafe long SubSat(long value, long min = -9223372036854775808L)
		{
			long num = *this.Counter;
			long num2;
			do
			{
				num2 = num;
				num = ((num <= min) ? min : math.max(min, num - value));
				num = Interlocked.CompareExchange(UnsafeUtility.AsRef<long>((void*)this.Counter), num, num2);
			}
			while (num2 != num && num2 != min);
			return num2;
		}

		// Token: 0x04000373 RID: 883
		public unsafe long* Counter;
	}
}
