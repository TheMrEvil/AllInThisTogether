using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x0200006C RID: 108
	internal struct ParticleSystemParallelForBatchJobStruct<T> where T : struct, IJobParticleSystemParallelForBatch
	{
		// Token: 0x0600075C RID: 1884 RVA: 0x00006B7C File Offset: 0x00004D7C
		public static IntPtr Initialize()
		{
			bool flag = ParticleSystemParallelForBatchJobStruct<T>.jobReflectionData == IntPtr.Zero;
			if (flag)
			{
				ParticleSystemParallelForBatchJobStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), new ParticleSystemParallelForBatchJobStruct<T>.ExecuteJobFunction(ParticleSystemParallelForBatchJobStruct<T>.Execute), null, null);
			}
			return ParticleSystemParallelForBatchJobStruct<T>.jobReflectionData;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00006BC8 File Offset: 0x00004DC8
		public unsafe static void Execute(ref T data, IntPtr listDataPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
		{
			NativeListData* ptr = (NativeListData*)((void*)listDataPtr);
			NativeParticleData nativeParticleData;
			ParticleSystem.CopyManagedJobData(ptr->system, out nativeParticleData);
			ParticleSystemJobData jobData = new ParticleSystemJobData(ref nativeParticleData);
			for (;;)
			{
				int num;
				int num2;
				bool flag = !JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out num, out num2);
				if (flag)
				{
					break;
				}
				data.Execute(jobData, num, num2 - num);
			}
		}

		// Token: 0x040001A7 RID: 423
		public static IntPtr jobReflectionData;

		// Token: 0x0200006D RID: 109
		// (Invoke) Token: 0x0600075F RID: 1887
		public delegate void ExecuteJobFunction(ref T data, IntPtr listDataPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
	}
}
