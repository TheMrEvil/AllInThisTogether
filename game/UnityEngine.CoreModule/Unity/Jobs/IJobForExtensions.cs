using System;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x0200005C RID: 92
	public static class IJobForExtensions
	{
		// Token: 0x06000163 RID: 355 RVA: 0x00003310 File Offset: 0x00001510
		public static JobHandle Schedule<T>(this T jobData, int arrayLength, JobHandle dependency) where T : struct, IJobFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobForExtensions.ForJobStruct<T>.jobReflectionData, dependency, ScheduleMode.Single);
			return JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, arrayLength);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00003340 File Offset: 0x00001540
		public static JobHandle ScheduleParallel<T>(this T jobData, int arrayLength, int innerloopBatchCount, JobHandle dependency) where T : struct, IJobFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobForExtensions.ForJobStruct<T>.jobReflectionData, dependency, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, innerloopBatchCount);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00003370 File Offset: 0x00001570
		public static void Run<T>(this T jobData, int arrayLength) where T : struct, IJobFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobForExtensions.ForJobStruct<T>.jobReflectionData, default(JobHandle), ScheduleMode.Run);
			JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, arrayLength);
		}

		// Token: 0x0200005D RID: 93
		internal struct ForJobStruct<T> where T : struct, IJobFor
		{
			// Token: 0x06000166 RID: 358 RVA: 0x000033A8 File Offset: 0x000015A8
			public static void Execute(ref T jobData, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				for (;;)
				{
					int num;
					int num2;
					bool flag = !JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out num, out num2);
					if (flag)
					{
						break;
					}
					int num3 = num2;
					for (int i = num; i < num3; i++)
					{
						jobData.Execute(i);
					}
				}
			}

			// Token: 0x06000167 RID: 359 RVA: 0x000033FB File Offset: 0x000015FB
			// Note: this type is marked as 'beforefieldinit'.
			static ForJobStruct()
			{
			}

			// Token: 0x0400017C RID: 380
			public static readonly IntPtr jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), new IJobForExtensions.ForJobStruct<T>.ExecuteJobFunction(IJobForExtensions.ForJobStruct<T>.Execute), null, null);

			// Token: 0x0200005E RID: 94
			// (Invoke) Token: 0x06000169 RID: 361
			public delegate void ExecuteJobFunction(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
		}
	}
}
