using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x02000068 RID: 104
	internal struct ParticleSystemJobStruct<T> where T : struct, IJobParticleSystem
	{
		// Token: 0x06000750 RID: 1872 RVA: 0x00006A34 File Offset: 0x00004C34
		public static IntPtr Initialize()
		{
			bool flag = ParticleSystemJobStruct<T>.jobReflectionData == IntPtr.Zero;
			if (flag)
			{
				ParticleSystemJobStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), new ParticleSystemJobStruct<T>.ExecuteJobFunction(ParticleSystemJobStruct<T>.Execute), null, null);
			}
			return ParticleSystemJobStruct<T>.jobReflectionData;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00006A80 File Offset: 0x00004C80
		public unsafe static void Execute(ref T data, IntPtr listDataPtr, IntPtr unusedPtr, ref JobRanges ranges, int jobIndex)
		{
			NativeListData* ptr = (NativeListData*)((void*)listDataPtr);
			NativeParticleData nativeParticleData;
			ParticleSystem.CopyManagedJobData(ptr->system, out nativeParticleData);
			ParticleSystemJobData jobData = new ParticleSystemJobData(ref nativeParticleData);
			data.Execute(jobData);
		}

		// Token: 0x040001A5 RID: 421
		public static IntPtr jobReflectionData;

		// Token: 0x02000069 RID: 105
		// (Invoke) Token: 0x06000753 RID: 1875
		public delegate void ExecuteJobFunction(ref T data, IntPtr listDataPtr, IntPtr unusedPtr, ref JobRanges ranges, int jobIndex);
	}
}
