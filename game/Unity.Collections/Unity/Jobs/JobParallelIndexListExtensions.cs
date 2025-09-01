using System;
using System.Diagnostics;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine.Scripting;

namespace Unity.Jobs
{
	// Token: 0x02000017 RID: 23
	public static class JobParallelIndexListExtensions
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002563 File Offset: 0x00000763
		public static void EarlyJobInit<T>() where T : struct, IJobParallelForFilter
		{
			JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.Initialize();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000022F1 File Offset: 0x000004F1
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckReflectionDataCorrect(IntPtr reflectionData)
		{
			if (reflectionData == IntPtr.Zero)
			{
				throw new InvalidOperationException("Reflection data was not set up by a call to Initialize()");
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000256C File Offset: 0x0000076C
		public unsafe static JobHandle ScheduleAppend<T>(this T jobData, NativeList<int> indices, int arrayLength, int innerloopBatchCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelForFilter
		{
			JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.JobWrapper jobWrapper = new JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.JobWrapper
			{
				JobData = jobData,
				outputIndices = indices,
				appendCount = arrayLength
			};
			IntPtr i_reflectionData = *JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.jobReflectionData.Data;
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.JobWrapper>(ref jobWrapper), i_reflectionData, dependsOn, ScheduleMode.Single);
			return JobsUtility.Schedule(ref jobScheduleParameters);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000025C0 File Offset: 0x000007C0
		public unsafe static JobHandle ScheduleFilter<T>(this T jobData, NativeList<int> indices, int innerloopBatchCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelForFilter
		{
			JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.JobWrapper jobWrapper = new JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.JobWrapper
			{
				JobData = jobData,
				outputIndices = indices,
				appendCount = -1
			};
			IntPtr i_reflectionData = *JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.jobReflectionData.Data;
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.JobWrapper>(ref jobWrapper), i_reflectionData, dependsOn, ScheduleMode.Single);
			return JobsUtility.Schedule(ref jobScheduleParameters);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002614 File Offset: 0x00000814
		public unsafe static void RunAppend<T>(this T jobData, NativeList<int> indices, int arrayLength) where T : struct, IJobParallelForFilter
		{
			JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.JobWrapper jobWrapper = new JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.JobWrapper
			{
				JobData = jobData,
				outputIndices = indices,
				appendCount = arrayLength
			};
			IntPtr i_reflectionData = *JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.jobReflectionData.Data;
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.JobWrapper>(ref jobWrapper), i_reflectionData, default(JobHandle), ScheduleMode.Run);
			JobsUtility.Schedule(ref jobScheduleParameters);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002674 File Offset: 0x00000874
		public unsafe static void RunFilter<T>(this T jobData, NativeList<int> indices) where T : struct, IJobParallelForFilter
		{
			JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.JobWrapper jobWrapper = new JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.JobWrapper
			{
				JobData = jobData,
				outputIndices = indices,
				appendCount = -1
			};
			IntPtr i_reflectionData = *JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.jobReflectionData.Data;
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.JobWrapper>(ref jobWrapper), i_reflectionData, default(JobHandle), ScheduleMode.Run);
			JobsUtility.Schedule(ref jobScheduleParameters);
		}

		// Token: 0x02000018 RID: 24
		internal struct JobParallelForFilterProducer<T> where T : struct, IJobParallelForFilter
		{
			// Token: 0x06000045 RID: 69 RVA: 0x000026D4 File Offset: 0x000008D4
			[Preserve]
			public unsafe static void Initialize()
			{
				if (*JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.jobReflectionData.Data == IntPtr.Zero)
				{
					*JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.jobReflectionData.Data = JobsUtility.CreateJobReflectionData(typeof(JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.JobWrapper), typeof(T), new JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.ExecuteJobFunction(JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.Execute));
				}
			}

			// Token: 0x06000046 RID: 70 RVA: 0x00002728 File Offset: 0x00000928
			public static void Execute(ref JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.JobWrapper jobWrapper, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				if (jobWrapper.appendCount == -1)
				{
					JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.ExecuteFilter(ref jobWrapper, bufferRangePatchData);
					return;
				}
				JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.ExecuteAppend(ref jobWrapper, bufferRangePatchData);
			}

			// Token: 0x06000047 RID: 71 RVA: 0x00002744 File Offset: 0x00000944
			public unsafe static void ExecuteAppend(ref JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.JobWrapper jobWrapper, IntPtr bufferRangePatchData)
			{
				int length = jobWrapper.outputIndices.Length;
				jobWrapper.outputIndices.Capacity = math.max(jobWrapper.appendCount + length, jobWrapper.outputIndices.Capacity);
				int* unsafePtr = (int*)jobWrapper.outputIndices.GetUnsafePtr<int>();
				int num = length;
				for (int num2 = 0; num2 != jobWrapper.appendCount; num2++)
				{
					if (jobWrapper.JobData.Execute(num2))
					{
						unsafePtr[num] = num2;
						num++;
					}
				}
				jobWrapper.outputIndices.ResizeUninitialized(num);
			}

			// Token: 0x06000048 RID: 72 RVA: 0x000027CC File Offset: 0x000009CC
			public unsafe static void ExecuteFilter(ref JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.JobWrapper jobWrapper, IntPtr bufferRangePatchData)
			{
				int* unsafePtr = (int*)jobWrapper.outputIndices.GetUnsafePtr<int>();
				int length = jobWrapper.outputIndices.Length;
				int num = 0;
				for (int num2 = 0; num2 != length; num2++)
				{
					int num3 = unsafePtr[num2];
					if (jobWrapper.JobData.Execute(num3))
					{
						unsafePtr[num] = num3;
						num++;
					}
				}
				jobWrapper.outputIndices.ResizeUninitialized(num);
			}

			// Token: 0x06000049 RID: 73 RVA: 0x00002836 File Offset: 0x00000A36
			// Note: this type is marked as 'beforefieldinit'.
			static JobParallelForFilterProducer()
			{
			}

			// Token: 0x04000006 RID: 6
			internal static readonly SharedStatic<IntPtr> jobReflectionData = SharedStatic<IntPtr>.GetOrCreate<JobParallelIndexListExtensions.JobParallelForFilterProducer<T>>(0U);

			// Token: 0x02000019 RID: 25
			public struct JobWrapper
			{
				// Token: 0x04000007 RID: 7
				[NativeDisableParallelForRestriction]
				public NativeList<int> outputIndices;

				// Token: 0x04000008 RID: 8
				public int appendCount;

				// Token: 0x04000009 RID: 9
				public T JobData;
			}

			// Token: 0x0200001A RID: 26
			// (Invoke) Token: 0x0600004B RID: 75
			public delegate void ExecuteJobFunction(ref JobParallelIndexListExtensions.JobParallelForFilterProducer<T>.JobWrapper jobWrapper, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
		}
	}
}
