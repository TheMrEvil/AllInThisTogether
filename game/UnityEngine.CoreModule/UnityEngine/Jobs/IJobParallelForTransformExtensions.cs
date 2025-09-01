using System;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.Jobs
{
	// Token: 0x02000282 RID: 642
	public static class IJobParallelForTransformExtensions
	{
		// Token: 0x06001BE3 RID: 7139 RVA: 0x0002CED4 File Offset: 0x0002B0D4
		public static JobHandle Schedule<T>(this T jobData, TransformAccessArray transforms, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelForTransform
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.Initialize(), dependsOn, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelForTransform(ref jobScheduleParameters, transforms.GetTransformAccessArrayForSchedule());
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x0002CF0C File Offset: 0x0002B10C
		public static JobHandle ScheduleReadOnly<T>(this T jobData, TransformAccessArray transforms, int batchSize, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelForTransform
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.Initialize(), dependsOn, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelForTransformReadOnly(ref jobScheduleParameters, transforms.GetTransformAccessArrayForSchedule(), batchSize);
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x0002CF44 File Offset: 0x0002B144
		public static void RunReadOnly<T>(this T jobData, TransformAccessArray transforms) where T : struct, IJobParallelForTransform
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.Initialize(), default(JobHandle), ScheduleMode.Run);
			JobsUtility.ScheduleParallelForTransformReadOnly(ref jobScheduleParameters, transforms.GetTransformAccessArrayForSchedule(), transforms.length);
		}

		// Token: 0x02000283 RID: 643
		internal struct TransformParallelForLoopStruct<T> where T : struct, IJobParallelForTransform
		{
			// Token: 0x06001BE6 RID: 7142 RVA: 0x0002CF88 File Offset: 0x0002B188
			public static IntPtr Initialize()
			{
				bool flag = IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.jobReflectionData == IntPtr.Zero;
				if (flag)
				{
					IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), new IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.ExecuteJobFunction(IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.Execute), null, null);
				}
				return IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.jobReflectionData;
			}

			// Token: 0x06001BE7 RID: 7143 RVA: 0x0002CFD4 File Offset: 0x0002B1D4
			public unsafe static void Execute(ref T jobData, IntPtr jobData2, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.TransformJobData transformJobData;
				UnsafeUtility.CopyPtrToStructure<IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.TransformJobData>((void*)jobData2, out transformJobData);
				int* ptr = (int*)((void*)TransformAccessArray.GetSortedToUserIndex(transformJobData.TransformAccessArray));
				TransformAccess* ptr2 = (TransformAccess*)((void*)TransformAccessArray.GetSortedTransformAccess(transformJobData.TransformAccessArray));
				bool flag = transformJobData.IsReadOnly == 1;
				if (flag)
				{
					for (;;)
					{
						int num;
						int num2;
						bool flag2 = !JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out num, out num2);
						if (flag2)
						{
							break;
						}
						int num3 = num2;
						for (int i = num; i < num3; i++)
						{
							int num4 = i;
							int index = ptr[num4];
							TransformAccess transform = ptr2[num4];
							jobData.Execute(index, transform);
						}
					}
				}
				else
				{
					int num5;
					int num6;
					JobsUtility.GetJobRange(ref ranges, jobIndex, out num5, out num6);
					for (int j = num5; j < num6; j++)
					{
						int num7 = j;
						int index2 = ptr[num7];
						TransformAccess transform2 = ptr2[num7];
						jobData.Execute(index2, transform2);
					}
				}
			}

			// Token: 0x04000923 RID: 2339
			public static IntPtr jobReflectionData;

			// Token: 0x02000284 RID: 644
			private struct TransformJobData
			{
				// Token: 0x04000924 RID: 2340
				public IntPtr TransformAccessArray;

				// Token: 0x04000925 RID: 2341
				public int IsReadOnly;
			}

			// Token: 0x02000285 RID: 645
			// (Invoke) Token: 0x06001BE9 RID: 7145
			public delegate void ExecuteJobFunction(ref T jobData, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
		}
	}
}
