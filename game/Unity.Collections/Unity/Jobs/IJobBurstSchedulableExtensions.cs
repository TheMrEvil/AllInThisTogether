using System;
using System.Diagnostics;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Scripting;

namespace Unity.Jobs
{
	// Token: 0x02000007 RID: 7
	public static class IJobBurstSchedulableExtensions
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000020F2 File Offset: 0x000002F2
		public static void EarlyJobInit<T>() where T : struct, IJobBurstSchedulable
		{
			IJobBurstSchedulableExtensions.JobBurstSchedulableProducer<T>.Initialize();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020F9 File Offset: 0x000002F9
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckReflectionDataCorrect(IntPtr reflectionData)
		{
			if (reflectionData == IntPtr.Zero)
			{
				throw new InvalidOperationException("Reflection data was not set up by an Initialize() call");
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002114 File Offset: 0x00000314
		public unsafe static JobHandle Schedule<T>(this T jobData, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobBurstSchedulable
		{
			IntPtr i_reflectionData = *IJobBurstSchedulableExtensions.JobBurstSchedulableProducer<T>.jobReflectionData.Data;
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), i_reflectionData, dependsOn, ScheduleMode.Single);
			return JobsUtility.Schedule(ref jobScheduleParameters);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002148 File Offset: 0x00000348
		public unsafe static void Run<T>(this T jobData) where T : struct, IJobBurstSchedulable
		{
			IntPtr i_reflectionData = *IJobBurstSchedulableExtensions.JobBurstSchedulableProducer<T>.jobReflectionData.Data;
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), i_reflectionData, default(JobHandle), ScheduleMode.Run);
			JobsUtility.Schedule(ref jobScheduleParameters);
		}

		// Token: 0x02000008 RID: 8
		internal struct JobBurstSchedulableProducer<T> where T : struct, IJobBurstSchedulable
		{
			// Token: 0x06000010 RID: 16 RVA: 0x00002184 File Offset: 0x00000384
			[Preserve]
			internal unsafe static void Initialize()
			{
				if (*IJobBurstSchedulableExtensions.JobBurstSchedulableProducer<T>.jobReflectionData.Data == IntPtr.Zero)
				{
					*IJobBurstSchedulableExtensions.JobBurstSchedulableProducer<T>.jobReflectionData.Data = JobsUtility.CreateJobReflectionData(typeof(T), new IJobBurstSchedulableExtensions.JobBurstSchedulableProducer<T>.ExecuteJobFunction(IJobBurstSchedulableExtensions.JobBurstSchedulableProducer<T>.Execute), null, null);
				}
			}

			// Token: 0x06000011 RID: 17 RVA: 0x000021D0 File Offset: 0x000003D0
			public static void Execute(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				data.Execute();
			}

			// Token: 0x06000012 RID: 18 RVA: 0x000021DE File Offset: 0x000003DE
			// Note: this type is marked as 'beforefieldinit'.
			static JobBurstSchedulableProducer()
			{
			}

			// Token: 0x04000002 RID: 2
			internal static readonly SharedStatic<IntPtr> jobReflectionData = SharedStatic<IntPtr>.GetOrCreate<IJobBurstSchedulableExtensions.JobBurstSchedulableProducer<T>>(0U);

			// Token: 0x02000009 RID: 9
			// (Invoke) Token: 0x06000014 RID: 20
			internal delegate void ExecuteJobFunction(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
		}
	}
}
