using System;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x02000060 RID: 96
	public static class IParticleSystemJobExtensions
	{
		// Token: 0x06000736 RID: 1846 RVA: 0x0000658C File Offset: 0x0000478C
		public static JobHandle Schedule<T>(this T jobData, ParticleSystem ps, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParticleSystem
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = IParticleSystemJobExtensions.CreateScheduleParams<T>(ref jobData, ps, dependsOn, ParticleSystemJobStruct<T>.Initialize());
			JobHandle jobHandle = ParticleSystem.ScheduleManagedJob(ref jobScheduleParameters, ps.GetManagedJobData());
			ps.SetManagedJobHandle(jobHandle);
			return jobHandle;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x000065C4 File Offset: 0x000047C4
		public static JobHandle Schedule<T>(this T jobData, ParticleSystem ps, int minIndicesPerJobCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParticleSystemParallelFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = IParticleSystemJobExtensions.CreateScheduleParams<T>(ref jobData, ps, dependsOn, ParticleSystemParallelForJobStruct<T>.Initialize());
			JobHandle jobHandle = JobsUtility.ScheduleParallelForDeferArraySize(ref jobScheduleParameters, minIndicesPerJobCount, ps.GetManagedJobData(), null);
			ps.SetManagedJobHandle(jobHandle);
			return jobHandle;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00006600 File Offset: 0x00004800
		public static JobHandle ScheduleBatch<T>(this T jobData, ParticleSystem ps, int innerLoopBatchCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParticleSystemParallelForBatch
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = IParticleSystemJobExtensions.CreateScheduleParams<T>(ref jobData, ps, dependsOn, ParticleSystemParallelForBatchJobStruct<T>.Initialize());
			JobHandle jobHandle = JobsUtility.ScheduleParallelForDeferArraySize(ref jobScheduleParameters, innerLoopBatchCount, ps.GetManagedJobData(), null);
			ps.SetManagedJobHandle(jobHandle);
			return jobHandle;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0000663C File Offset: 0x0000483C
		private static JobsUtility.JobScheduleParameters CreateScheduleParams<T>(ref T jobData, ParticleSystem ps, JobHandle dependsOn, IntPtr jobReflectionData) where T : struct
		{
			dependsOn = JobHandle.CombineDependencies(ps.GetManagedJobHandle(), dependsOn);
			return new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), jobReflectionData, dependsOn, ScheduleMode.Batched);
		}
	}
}
