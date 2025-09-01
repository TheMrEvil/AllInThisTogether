using System;
using System.Diagnostics;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Scripting;

namespace Unity.Jobs
{
	// Token: 0x0200000B RID: 11
	public static class IJobParallelForBatchExtensions
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000021EB File Offset: 0x000003EB
		public static void EarlyJobInit<T>() where T : struct, IJobParallelForBatch
		{
			IJobParallelForBatchExtensions.JobParallelForBatchProducer<T>.Initialize();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000020F9 File Offset: 0x000002F9
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckReflectionDataCorrect(IntPtr reflectionData)
		{
			if (reflectionData == IntPtr.Zero)
			{
				throw new InvalidOperationException("Reflection data was not set up by an Initialize() call");
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000021F4 File Offset: 0x000003F4
		public unsafe static JobHandle ScheduleBatch<T>(this T jobData, int arrayLength, int minIndicesPerJobCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelForBatch
		{
			IntPtr i_reflectionData = *IJobParallelForBatchExtensions.JobParallelForBatchProducer<T>.jobReflectionData.Data;
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), i_reflectionData, dependsOn, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, minIndicesPerJobCount);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002228 File Offset: 0x00000428
		public unsafe static void RunBatch<T>(this T jobData, int arrayLength) where T : struct, IJobParallelForBatch
		{
			IntPtr i_reflectionData = *IJobParallelForBatchExtensions.JobParallelForBatchProducer<T>.jobReflectionData.Data;
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), i_reflectionData, default(JobHandle), ScheduleMode.Run);
			JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, arrayLength);
		}

		// Token: 0x0200000C RID: 12
		internal struct JobParallelForBatchProducer<T> where T : struct, IJobParallelForBatch
		{
			// Token: 0x0600001C RID: 28 RVA: 0x00002264 File Offset: 0x00000464
			[Preserve]
			internal unsafe static void Initialize()
			{
				if (*IJobParallelForBatchExtensions.JobParallelForBatchProducer<T>.jobReflectionData.Data == IntPtr.Zero)
				{
					*IJobParallelForBatchExtensions.JobParallelForBatchProducer<T>.jobReflectionData.Data = JobsUtility.CreateJobReflectionData(typeof(T), new IJobParallelForBatchExtensions.JobParallelForBatchProducer<T>.ExecuteJobFunction(IJobParallelForBatchExtensions.JobParallelForBatchProducer<T>.Execute), null, null);
				}
			}

			// Token: 0x0600001D RID: 29 RVA: 0x000022B0 File Offset: 0x000004B0
			public static void Execute(ref T jobData, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				int num;
				int num2;
				while (JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out num, out num2))
				{
					jobData.Execute(num, num2 - num);
				}
			}

			// Token: 0x0600001E RID: 30 RVA: 0x000022DD File Offset: 0x000004DD
			// Note: this type is marked as 'beforefieldinit'.
			static JobParallelForBatchProducer()
			{
			}

			// Token: 0x04000003 RID: 3
			internal static readonly SharedStatic<IntPtr> jobReflectionData = SharedStatic<IntPtr>.GetOrCreate<IJobParallelForBatchExtensions.JobParallelForBatchProducer<T>>(0U);

			// Token: 0x0200000D RID: 13
			// (Invoke) Token: 0x06000020 RID: 32
			internal delegate void ExecuteJobFunction(ref T jobData, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
		}
	}
}
