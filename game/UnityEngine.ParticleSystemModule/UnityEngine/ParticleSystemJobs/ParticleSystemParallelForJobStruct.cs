using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x0200006A RID: 106
	internal struct ParticleSystemParallelForJobStruct<T> where T : struct, IJobParticleSystemParallelFor
	{
		// Token: 0x06000756 RID: 1878 RVA: 0x00006ABC File Offset: 0x00004CBC
		public static IntPtr Initialize()
		{
			bool flag = ParticleSystemParallelForJobStruct<T>.jobReflectionData == IntPtr.Zero;
			if (flag)
			{
				ParticleSystemParallelForJobStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), new ParticleSystemParallelForJobStruct<T>.ExecuteJobFunction(ParticleSystemParallelForJobStruct<T>.Execute), null, null);
			}
			return ParticleSystemParallelForJobStruct<T>.jobReflectionData;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00006B08 File Offset: 0x00004D08
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
				for (int i = num; i < num2; i++)
				{
					data.Execute(jobData, i);
				}
			}
		}

		// Token: 0x040001A6 RID: 422
		public static IntPtr jobReflectionData;

		// Token: 0x0200006B RID: 107
		// (Invoke) Token: 0x06000759 RID: 1881
		public delegate void ExecuteJobFunction(ref T data, IntPtr listDataPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
	}
}
