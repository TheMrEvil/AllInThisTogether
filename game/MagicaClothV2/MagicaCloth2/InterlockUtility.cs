using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace MagicaCloth2
{
	// Token: 0x020000C5 RID: 197
	public static class InterlockUtility
	{
		// Token: 0x06000308 RID: 776 RVA: 0x0001D5F8 File Offset: 0x0001B7F8
		internal unsafe static void AddFloat3(int index, float3 add, int* cntPt, int* sumPt)
		{
			Interlocked.Increment(ref cntPt[index]);
			int3 @int = (int3)(add * 100000f);
			index *= 3;
			int i = 0;
			while (i < 3)
			{
				if (@int[i] != 0)
				{
					Interlocked.Add(ref sumPt[index], @int[i]);
				}
				i++;
				index++;
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0001D658 File Offset: 0x0001B858
		internal unsafe static void AddFloat3(int index, float3 add, int* sumPt)
		{
			int3 @int = (int3)(add * 100000f);
			index *= 3;
			int i = 0;
			while (i < 3)
			{
				if (@int[i] != 0)
				{
					Interlocked.Add(ref sumPt[index], @int[i]);
				}
				i++;
				index++;
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0001D6AA File Offset: 0x0001B8AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static void Increment(int index, int* cntPt)
		{
			Interlocked.Increment(ref cntPt[index]);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0001D6B8 File Offset: 0x0001B8B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static void Max(int index, float value, int* pt)
		{
			int num = (int)value * 100000;
			int num2 = pt[index];
			int num3 = num2 + 1;
			while (num > num2 && num2 != num3)
			{
				num3 = num2;
				num2 = Interlocked.CompareExchange(ref pt[index], num, num2);
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0001D6F4 File Offset: 0x0001B8F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static float3 ReadAverageFloat3(int index, in NativeArray<int> countArray, in NativeArray<int> sumArray)
		{
			NativeArray<int> nativeArray = countArray;
			int num = nativeArray[index];
			if (num == 0)
			{
				return 0;
			}
			int num2 = index * 3;
			nativeArray = sumArray;
			float x = (float)nativeArray[num2];
			nativeArray = sumArray;
			float y = (float)nativeArray[num2 + 1];
			nativeArray = sumArray;
			return new float3(x, y, (float)nativeArray[num2 + 2]) / (float)num * 1E-05f;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0001D76C File Offset: 0x0001B96C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static float3 ReadFloat3(int index, in NativeArray<int> bufferArray)
		{
			int num = index * 3;
			NativeArray<int> nativeArray = bufferArray;
			float x = (float)nativeArray[num];
			nativeArray = bufferArray;
			float y = (float)nativeArray[num + 1];
			nativeArray = bufferArray;
			return new float3(x, y, (float)nativeArray[num + 2]) * 1E-05f;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0001D7C0 File Offset: 0x0001B9C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static float ReadFloat(int index, in NativeArray<int> bufferArray)
		{
			NativeArray<int> nativeArray = bufferArray;
			return (float)nativeArray[index] * 1E-05f;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0001D7E4 File Offset: 0x0001B9E4
		internal static JobHandle SolveAggregateBufferAndClear(in NativeList<int> particleList, float velocityAttenuation, JobHandle jobHandle)
		{
			SimulationManager simulation = MagicaManager.Simulation;
			if (velocityAttenuation > 1E-06f)
			{
				jobHandle = new InterlockUtility.AggregateWithVelocityJob
				{
					jobParticleIndexList = particleList,
					nextPosArray = simulation.nextPosArray.GetNativeArray(),
					velocityPosArray = simulation.velocityPosArray.GetNativeArray(),
					velocityAttenuation = velocityAttenuation,
					countArray = simulation.countArray,
					sumArray = simulation.sumArray
				}.Schedule(particleList, 16, jobHandle);
			}
			else
			{
				jobHandle = new InterlockUtility.AggregateJob
				{
					jobParticleIndexList = particleList,
					nextPosArray = simulation.nextPosArray.GetNativeArray(),
					countArray = simulation.countArray,
					sumArray = simulation.sumArray
				}.Schedule(particleList, 16, jobHandle);
			}
			return jobHandle;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0001D8C0 File Offset: 0x0001BAC0
		internal static JobHandle SolveAggregateBufferAndClear(in ExProcessingList<int> processingList, float velocityAttenuation, JobHandle jobHandle)
		{
			return InterlockUtility.SolveAggregateBufferAndClear(processingList.Buffer, processingList.Counter, velocityAttenuation, jobHandle);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0001D8D8 File Offset: 0x0001BAD8
		internal unsafe static JobHandle SolveAggregateBufferAndClear(in NativeArray<int> particleArray, in NativeReference<int> counter, float velocityAttenuation, JobHandle jobHandle)
		{
			SimulationManager simulation = MagicaManager.Simulation;
			if (velocityAttenuation > 1E-06f)
			{
				jobHandle = new InterlockUtility.AggregateWithVelocityJob2
				{
					particleIndexArray = particleArray,
					nextPosArray = simulation.nextPosArray.GetNativeArray(),
					velocityPosArray = simulation.velocityPosArray.GetNativeArray(),
					velocityAttenuation = velocityAttenuation,
					countArray = simulation.countArray,
					sumArray = simulation.sumArray
				}.Schedule((int*)counter.GetUnsafePtrWithoutChecks<int>(), 16, jobHandle);
			}
			else
			{
				jobHandle = new InterlockUtility.AggregateJob2
				{
					particleIndexArray = particleArray,
					nextPosArray = simulation.nextPosArray.GetNativeArray(),
					countArray = simulation.countArray,
					sumArray = simulation.sumArray
				}.Schedule((int*)counter.GetUnsafePtrWithoutChecks<int>(), 16, jobHandle);
			}
			return jobHandle;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0001D9C0 File Offset: 0x0001BBC0
		internal static JobHandle ClearCountArray(JobHandle jobHandle)
		{
			SimulationManager simulation = MagicaManager.Simulation;
			return JobUtility.Fill(simulation.countArray, simulation.countArray.Length, 0, jobHandle);
		}

		// Token: 0x040005EE RID: 1518
		private const int ToFixed = 100000;

		// Token: 0x040005EF RID: 1519
		private const float ToFloat = 1E-05f;

		// Token: 0x020000C6 RID: 198
		[BurstCompile]
		private struct AggregateJob : IJobParallelForDefer
		{
			// Token: 0x06000313 RID: 787 RVA: 0x0001D9EC File Offset: 0x0001BBEC
			public void Execute(int index)
			{
				int num = this.jobParticleIndexList[index];
				int num2 = this.countArray[num];
				if (num2 == 0)
				{
					return;
				}
				int num3 = num * 3;
				float3 @float = new float3((float)this.sumArray[num3], (float)this.sumArray[num3 + 1], (float)this.sumArray[num3 + 2]);
				@float /= (float)num2;
				@float *= 1E-05f;
				this.nextPosArray[num] = this.nextPosArray[num] + @float;
				this.countArray[num] = 0;
				this.sumArray[num3] = 0;
				this.sumArray[num3 + 1] = 0;
				this.sumArray[num3 + 2] = 0;
			}

			// Token: 0x040005F0 RID: 1520
			[ReadOnly]
			public NativeList<int> jobParticleIndexList;

			// Token: 0x040005F1 RID: 1521
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> nextPosArray;

			// Token: 0x040005F2 RID: 1522
			[NativeDisableParallelForRestriction]
			public NativeArray<int> countArray;

			// Token: 0x040005F3 RID: 1523
			[NativeDisableParallelForRestriction]
			public NativeArray<int> sumArray;
		}

		// Token: 0x020000C7 RID: 199
		[BurstCompile]
		private struct AggregateWithVelocityJob : IJobParallelForDefer
		{
			// Token: 0x06000314 RID: 788 RVA: 0x0001DAB8 File Offset: 0x0001BCB8
			public void Execute(int index)
			{
				int num = this.jobParticleIndexList[index];
				int num2 = this.countArray[num];
				if (num2 == 0)
				{
					return;
				}
				int num3 = num * 3;
				float3 @float = new float3((float)this.sumArray[num3], (float)this.sumArray[num3 + 1], (float)this.sumArray[num3 + 2]);
				@float /= (float)num2;
				@float *= 1E-05f;
				this.nextPosArray[num] = this.nextPosArray[num] + @float;
				this.velocityPosArray[num] = this.velocityPosArray[num] + @float * this.velocityAttenuation;
				this.countArray[num] = 0;
				this.sumArray[num3] = 0;
				this.sumArray[num3 + 1] = 0;
				this.sumArray[num3 + 2] = 0;
			}

			// Token: 0x040005F4 RID: 1524
			[ReadOnly]
			public NativeList<int> jobParticleIndexList;

			// Token: 0x040005F5 RID: 1525
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> nextPosArray;

			// Token: 0x040005F6 RID: 1526
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> velocityPosArray;

			// Token: 0x040005F7 RID: 1527
			public float velocityAttenuation;

			// Token: 0x040005F8 RID: 1528
			[NativeDisableParallelForRestriction]
			public NativeArray<int> countArray;

			// Token: 0x040005F9 RID: 1529
			[NativeDisableParallelForRestriction]
			public NativeArray<int> sumArray;
		}

		// Token: 0x020000C8 RID: 200
		[BurstCompile]
		private struct AggregateJob2 : IJobParallelForDefer
		{
			// Token: 0x06000315 RID: 789 RVA: 0x0001DBB0 File Offset: 0x0001BDB0
			public void Execute(int index)
			{
				int num = this.particleIndexArray[index];
				int num2 = this.countArray[num];
				if (num2 == 0)
				{
					return;
				}
				int num3 = num * 3;
				float3 @float = new float3((float)this.sumArray[num3], (float)this.sumArray[num3 + 1], (float)this.sumArray[num3 + 2]);
				@float /= (float)num2;
				@float *= 1E-05f;
				this.nextPosArray[num] = this.nextPosArray[num] + @float;
				this.countArray[num] = 0;
				this.sumArray[num3] = 0;
				this.sumArray[num3 + 1] = 0;
				this.sumArray[num3 + 2] = 0;
			}

			// Token: 0x040005FA RID: 1530
			[ReadOnly]
			public NativeArray<int> particleIndexArray;

			// Token: 0x040005FB RID: 1531
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> nextPosArray;

			// Token: 0x040005FC RID: 1532
			[NativeDisableParallelForRestriction]
			public NativeArray<int> countArray;

			// Token: 0x040005FD RID: 1533
			[NativeDisableParallelForRestriction]
			public NativeArray<int> sumArray;
		}

		// Token: 0x020000C9 RID: 201
		[BurstCompile]
		private struct AggregateWithVelocityJob2 : IJobParallelForDefer
		{
			// Token: 0x06000316 RID: 790 RVA: 0x0001DC7C File Offset: 0x0001BE7C
			public void Execute(int index)
			{
				int num = this.particleIndexArray[index];
				int num2 = this.countArray[num];
				if (num2 == 0)
				{
					return;
				}
				int num3 = num * 3;
				float3 @float = new float3((float)this.sumArray[num3], (float)this.sumArray[num3 + 1], (float)this.sumArray[num3 + 2]);
				@float /= (float)num2;
				@float *= 1E-05f;
				this.nextPosArray[num] = this.nextPosArray[num] + @float;
				this.velocityPosArray[num] = this.velocityPosArray[num] + @float * this.velocityAttenuation;
				this.countArray[num] = 0;
				this.sumArray[num3] = 0;
				this.sumArray[num3 + 1] = 0;
				this.sumArray[num3 + 2] = 0;
			}

			// Token: 0x040005FE RID: 1534
			[ReadOnly]
			public NativeArray<int> particleIndexArray;

			// Token: 0x040005FF RID: 1535
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> nextPosArray;

			// Token: 0x04000600 RID: 1536
			[NativeDisableParallelForRestriction]
			public NativeArray<float3> velocityPosArray;

			// Token: 0x04000601 RID: 1537
			public float velocityAttenuation;

			// Token: 0x04000602 RID: 1538
			[NativeDisableParallelForRestriction]
			public NativeArray<int> countArray;

			// Token: 0x04000603 RID: 1539
			[NativeDisableParallelForRestriction]
			public NativeArray<int> sumArray;
		}
	}
}
