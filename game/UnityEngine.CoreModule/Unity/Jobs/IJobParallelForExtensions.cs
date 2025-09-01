using System;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x02000060 RID: 96
	public static class IJobParallelForExtensions
	{
		// Token: 0x0600016D RID: 365 RVA: 0x00003420 File Offset: 0x00001620
		public static JobHandle Schedule<T>(this T jobData, int arrayLength, int innerloopBatchCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForExtensions.ParallelForJobStruct<T>.jobReflectionData, dependsOn, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, innerloopBatchCount);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00003450 File Offset: 0x00001650
		public static void Run<T>(this T jobData, int arrayLength) where T : struct, IJobParallelFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForExtensions.ParallelForJobStruct<T>.jobReflectionData, default(JobHandle), ScheduleMode.Run);
			JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, arrayLength);
		}

		// Token: 0x02000061 RID: 97
		internal struct ParallelForJobStruct<T> where T : struct, IJobParallelFor
		{
			// Token: 0x0600016F RID: 367 RVA: 0x00003488 File Offset: 0x00001688
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

			// Token: 0x06000170 RID: 368 RVA: 0x000034DB File Offset: 0x000016DB
			// Note: this type is marked as 'beforefieldinit'.
			static ParallelForJobStruct()
			{
			}

			// Token: 0x0400017D RID: 381
			public static readonly IntPtr jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), new IJobParallelForExtensions.ParallelForJobStruct<T>.ExecuteJobFunction(IJobParallelForExtensions.ParallelForJobStruct<T>.Execute), null, null);

			// Token: 0x02000062 RID: 98
			// (Invoke) Token: 0x06000172 RID: 370
			public delegate void ExecuteJobFunction(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
		}
	}
}
