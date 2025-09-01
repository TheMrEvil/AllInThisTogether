using System;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x02000058 RID: 88
	public static class IJobExtensions
	{
		// Token: 0x0600015A RID: 346 RVA: 0x00003278 File Offset: 0x00001478
		public static JobHandle Schedule<T>(this T jobData, JobHandle dependsOn = default(JobHandle)) where T : struct, IJob
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobExtensions.JobStruct<T>.jobReflectionData, dependsOn, ScheduleMode.Single);
			return JobsUtility.Schedule(ref jobScheduleParameters);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000032A8 File Offset: 0x000014A8
		public static void Run<T>(this T jobData) where T : struct, IJob
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobExtensions.JobStruct<T>.jobReflectionData, default(JobHandle), ScheduleMode.Run);
			JobsUtility.Schedule(ref jobScheduleParameters);
		}

		// Token: 0x02000059 RID: 89
		internal struct JobStruct<T> where T : struct, IJob
		{
			// Token: 0x0600015C RID: 348 RVA: 0x000032DB File Offset: 0x000014DB
			public static void Execute(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				data.Execute();
			}

			// Token: 0x0600015D RID: 349 RVA: 0x000032EB File Offset: 0x000014EB
			// Note: this type is marked as 'beforefieldinit'.
			static JobStruct()
			{
			}

			// Token: 0x0400017B RID: 379
			public static readonly IntPtr jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), new IJobExtensions.JobStruct<T>.ExecuteJobFunction(IJobExtensions.JobStruct<T>.Execute), null, null);

			// Token: 0x0200005A RID: 90
			// (Invoke) Token: 0x0600015F RID: 351
			public delegate void ExecuteJobFunction(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
		}
	}
}
