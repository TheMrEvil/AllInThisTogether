using System;
using System.Diagnostics;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Scripting;

namespace Unity.Jobs
{
	// Token: 0x0200000F RID: 15
	public static class IJobParallelForExtensionsBurstSchedulable
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000022EA File Offset: 0x000004EA
		public static void EarlyJobInit<T>() where T : struct, IJobParallelForBurstSchedulable
		{
			IJobParallelForExtensionsBurstSchedulable.JobParallelForBurstSchedulableProducer<T>.Initialize();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000022F1 File Offset: 0x000004F1
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckReflectionDataCorrect(IntPtr reflectionData)
		{
			if (reflectionData == IntPtr.Zero)
			{
				throw new InvalidOperationException("Reflection data was not set up by a call to Initialize()");
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000230C File Offset: 0x0000050C
		public unsafe static JobHandle Schedule<T>(this T jobData, int arrayLength, int innerloopBatchCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelForBurstSchedulable
		{
			IntPtr i_reflectionData = *IJobParallelForExtensionsBurstSchedulable.JobParallelForBurstSchedulableProducer<T>.jobReflectionData.Data;
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), i_reflectionData, dependsOn, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, innerloopBatchCount);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002340 File Offset: 0x00000540
		public unsafe static void Run<T>(this T jobData, int arrayLength) where T : struct, IJobParallelForBurstSchedulable
		{
			IntPtr i_reflectionData = *IJobParallelForExtensionsBurstSchedulable.JobParallelForBurstSchedulableProducer<T>.jobReflectionData.Data;
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), i_reflectionData, default(JobHandle), ScheduleMode.Run);
			JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, arrayLength);
		}

		// Token: 0x02000010 RID: 16
		internal struct JobParallelForBurstSchedulableProducer<T> where T : struct, IJobParallelForBurstSchedulable
		{
			// Token: 0x06000028 RID: 40 RVA: 0x0000237C File Offset: 0x0000057C
			[Preserve]
			internal unsafe static void Initialize()
			{
				if (*IJobParallelForExtensionsBurstSchedulable.JobParallelForBurstSchedulableProducer<T>.jobReflectionData.Data == IntPtr.Zero)
				{
					*IJobParallelForExtensionsBurstSchedulable.JobParallelForBurstSchedulableProducer<T>.jobReflectionData.Data = JobsUtility.CreateJobReflectionData(typeof(T), new IJobParallelForExtensionsBurstSchedulable.JobParallelForBurstSchedulableProducer<T>.ExecuteJobFunction(IJobParallelForExtensionsBurstSchedulable.JobParallelForBurstSchedulableProducer<T>.Execute), null, null);
				}
			}

			// Token: 0x06000029 RID: 41 RVA: 0x000023C8 File Offset: 0x000005C8
			public static void Execute(ref T jobData, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				int num;
				int num2;
				while (JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out num, out num2))
				{
					int num3 = num2;
					for (int i = num; i < num3; i++)
					{
						jobData.Execute(i);
					}
				}
			}

			// Token: 0x0600002A RID: 42 RVA: 0x00002400 File Offset: 0x00000600
			// Note: this type is marked as 'beforefieldinit'.
			static JobParallelForBurstSchedulableProducer()
			{
			}

			// Token: 0x04000004 RID: 4
			internal static readonly SharedStatic<IntPtr> jobReflectionData = SharedStatic<IntPtr>.GetOrCreate<IJobParallelForExtensionsBurstSchedulable.JobParallelForBurstSchedulableProducer<T>>(0U);

			// Token: 0x02000011 RID: 17
			// (Invoke) Token: 0x0600002C RID: 44
			internal delegate void ExecuteJobFunction(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
		}
	}
}
