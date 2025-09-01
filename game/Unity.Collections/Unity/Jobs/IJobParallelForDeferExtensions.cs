using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Scripting;

namespace Unity.Jobs
{
	// Token: 0x02000013 RID: 19
	public static class IJobParallelForDeferExtensions
	{
		// Token: 0x06000030 RID: 48 RVA: 0x0000240D File Offset: 0x0000060D
		public static void EarlyJobInit<T>() where T : struct, IJobParallelForDefer
		{
			IJobParallelForDeferExtensions.JobParallelForDeferProducer<T>.Initialize();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000022F1 File Offset: 0x000004F1
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckReflectionDataCorrect(IntPtr reflectionData)
		{
			if (reflectionData == IntPtr.Zero)
			{
				throw new InvalidOperationException("Reflection data was not set up by a call to Initialize()");
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002414 File Offset: 0x00000614
		public unsafe static JobHandle Schedule<T, [IsUnmanaged] U>(this T jobData, NativeList<U> list, int innerloopBatchCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelForDefer where U : struct, ValueType
		{
			void* atomicSafetyHandlePtr = null;
			return IJobParallelForDeferExtensions.ScheduleInternal<T>(ref jobData, innerloopBatchCount, NativeListUnsafeUtility.GetInternalListDataPtrUnchecked<U>(ref list), atomicSafetyHandlePtr, dependsOn);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002438 File Offset: 0x00000638
		public unsafe static JobHandle ScheduleByRef<T, [IsUnmanaged] U>(this T jobData, NativeList<U> list, int innerloopBatchCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelForDefer where U : struct, ValueType
		{
			void* atomicSafetyHandlePtr = null;
			return IJobParallelForDeferExtensions.ScheduleInternal<T>(ref jobData, innerloopBatchCount, NativeListUnsafeUtility.GetInternalListDataPtrUnchecked<U>(ref list), atomicSafetyHandlePtr, dependsOn);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002458 File Offset: 0x00000658
		public unsafe static JobHandle Schedule<T>(this T jobData, int* forEachCount, int innerloopBatchCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelForDefer
		{
			byte* forEachListPtr = (byte*)(forEachCount - sizeof(void*) / 4);
			return IJobParallelForDeferExtensions.ScheduleInternal<T>(ref jobData, innerloopBatchCount, (void*)forEachListPtr, null, dependsOn);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000247C File Offset: 0x0000067C
		public unsafe static JobHandle ScheduleByRef<T>(this T jobData, int* forEachCount, int innerloopBatchCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelForDefer
		{
			byte* forEachListPtr = (byte*)(forEachCount - sizeof(void*) / 4);
			return IJobParallelForDeferExtensions.ScheduleInternal<T>(ref jobData, innerloopBatchCount, (void*)forEachListPtr, null, dependsOn);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000024A0 File Offset: 0x000006A0
		private unsafe static JobHandle ScheduleInternal<T>(ref T jobData, int innerloopBatchCount, void* forEachListPtr, void* atomicSafetyHandlePtr, JobHandle dependsOn) where T : struct, IJobParallelForDefer
		{
			IntPtr i_reflectionData = *IJobParallelForDeferExtensions.JobParallelForDeferProducer<T>.jobReflectionData.Data;
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), i_reflectionData, dependsOn, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelForDeferArraySize(ref jobScheduleParameters, innerloopBatchCount, forEachListPtr, atomicSafetyHandlePtr);
		}

		// Token: 0x02000014 RID: 20
		internal struct JobParallelForDeferProducer<T> where T : struct, IJobParallelForDefer
		{
			// Token: 0x06000037 RID: 55 RVA: 0x000024D4 File Offset: 0x000006D4
			[Preserve]
			internal unsafe static void Initialize()
			{
				if (*IJobParallelForDeferExtensions.JobParallelForDeferProducer<T>.jobReflectionData.Data == IntPtr.Zero)
				{
					*IJobParallelForDeferExtensions.JobParallelForDeferProducer<T>.jobReflectionData.Data = JobsUtility.CreateJobReflectionData(typeof(T), new IJobParallelForDeferExtensions.JobParallelForDeferProducer<T>.ExecuteJobFunction(IJobParallelForDeferExtensions.JobParallelForDeferProducer<T>.Execute), null, null);
				}
			}

			// Token: 0x06000038 RID: 56 RVA: 0x00002520 File Offset: 0x00000720
			public static void Execute(ref T jobData, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				int num;
				int num2;
				while (JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out num, out num2))
				{
					for (int i = num; i < num2; i++)
					{
						jobData.Execute(i);
					}
				}
			}

			// Token: 0x06000039 RID: 57 RVA: 0x00002556 File Offset: 0x00000756
			// Note: this type is marked as 'beforefieldinit'.
			static JobParallelForDeferProducer()
			{
			}

			// Token: 0x04000005 RID: 5
			internal static readonly SharedStatic<IntPtr> jobReflectionData = SharedStatic<IntPtr>.GetOrCreate<IJobParallelForDeferExtensions.JobParallelForDeferProducer<T>>(0U);

			// Token: 0x02000015 RID: 21
			// (Invoke) Token: 0x0600003B RID: 59
			public delegate void ExecuteJobFunction(ref T jobData, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
		}
	}
}
