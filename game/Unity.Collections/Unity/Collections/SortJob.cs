using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x020000D9 RID: 217
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(NativeSortExtension.DefaultComparer<int>)
	}, RequiredUnityDefine = "UNITY_2020_2_OR_NEWER")]
	public struct SortJob<[IsUnmanaged] T, U> where T : struct, ValueType where U : IComparer<T>
	{
		// Token: 0x06000810 RID: 2064 RVA: 0x00018C00 File Offset: 0x00016E00
		[NotBurstCompatible]
		public JobHandle Schedule(JobHandle inputDeps = default(JobHandle))
		{
			if (this.Length == 0)
			{
				return inputDeps;
			}
			int num = (this.Length + 1023) / 1024;
			int num2 = math.max(1, 128);
			int innerloopBatchCount = num / num2;
			JobHandle dependsOn = new SortJob<T, U>.SegmentSort
			{
				Data = this.Data,
				Comp = this.Comp,
				Length = this.Length,
				SegmentWidth = 1024
			}.Schedule(num, innerloopBatchCount, inputDeps);
			return new SortJob<T, U>.SegmentSortMerge
			{
				Data = this.Data,
				Comp = this.Comp,
				Length = this.Length,
				SegmentWidth = 1024
			}.Schedule(dependsOn);
		}

		// Token: 0x040002C1 RID: 705
		public unsafe T* Data;

		// Token: 0x040002C2 RID: 706
		public U Comp;

		// Token: 0x040002C3 RID: 707
		public int Length;

		// Token: 0x020000DA RID: 218
		[BurstCompile]
		private struct SegmentSort : IJobParallelFor
		{
			// Token: 0x06000811 RID: 2065 RVA: 0x00018CC4 File Offset: 0x00016EC4
			public void Execute(int index)
			{
				int num = index * this.SegmentWidth;
				int length = (this.Length - num < this.SegmentWidth) ? (this.Length - num) : this.SegmentWidth;
				NativeSortExtension.Sort<T, U>(this.Data + (IntPtr)num * (IntPtr)sizeof(T) / (IntPtr)sizeof(T), length, this.Comp);
			}

			// Token: 0x040002C4 RID: 708
			[NativeDisableUnsafePtrRestriction]
			public unsafe T* Data;

			// Token: 0x040002C5 RID: 709
			public U Comp;

			// Token: 0x040002C6 RID: 710
			public int Length;

			// Token: 0x040002C7 RID: 711
			public int SegmentWidth;
		}

		// Token: 0x020000DB RID: 219
		[BurstCompile]
		private struct SegmentSortMerge : IJob
		{
			// Token: 0x06000812 RID: 2066 RVA: 0x00018D18 File Offset: 0x00016F18
			public unsafe void Execute()
			{
				int num = (this.Length + (this.SegmentWidth - 1)) / this.SegmentWidth;
				int* ptr = stackalloc int[checked(unchecked((UIntPtr)num) * 4)];
				T* ptr2 = (T*)Memory.Unmanaged.Allocate((long)(UnsafeUtility.SizeOf<T>() * this.Length), 16, Allocator.Temp);
				for (int i = 0; i < this.Length; i++)
				{
					int num2 = -1;
					T t = default(T);
					for (int j = 0; j < num; j++)
					{
						int num3 = j * this.SegmentWidth;
						int num4 = ptr[j];
						int num5 = (this.Length - num3 < this.SegmentWidth) ? (this.Length - num3) : this.SegmentWidth;
						if (num4 != num5)
						{
							T t2 = this.Data[(IntPtr)(num3 + num4) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
							if (num2 == -1 || this.Comp.Compare(t2, t) <= 0)
							{
								t = t2;
								num2 = j;
							}
						}
					}
					ptr[num2]++;
					ptr2[(IntPtr)i * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = t;
				}
				UnsafeUtility.MemCpy((void*)this.Data, (void*)ptr2, (long)(UnsafeUtility.SizeOf<T>() * this.Length));
			}

			// Token: 0x040002C8 RID: 712
			[NativeDisableUnsafePtrRestriction]
			public unsafe T* Data;

			// Token: 0x040002C9 RID: 713
			public U Comp;

			// Token: 0x040002CA RID: 714
			public int Length;

			// Token: 0x040002CB RID: 715
			public int SegmentWidth;
		}
	}
}
