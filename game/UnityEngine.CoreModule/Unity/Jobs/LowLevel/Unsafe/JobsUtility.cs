using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x0200006B RID: 107
	[NativeType(Header = "Runtime/Jobs/ScriptBindings/JobsBindings.h")]
	[NativeHeader("Runtime/Jobs/JobSystem.h")]
	public static class JobsUtility
	{
		// Token: 0x0600018F RID: 399 RVA: 0x00003778 File Offset: 0x00001978
		public unsafe static void GetJobRange(ref JobRanges ranges, int jobIndex, out int beginIndex, out int endIndex)
		{
			int* ptr = (int*)((void*)ranges.StartEndIndex);
			beginIndex = ptr[jobIndex * 2];
			endIndex = ptr[jobIndex * 2 + 1];
		}

		// Token: 0x06000190 RID: 400
		[NativeMethod(IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetWorkStealingRange(ref JobRanges ranges, int jobIndex, out int beginIndex, out int endIndex);

		// Token: 0x06000191 RID: 401 RVA: 0x000037AC File Offset: 0x000019AC
		[FreeFunction("ScheduleManagedJob", ThrowsException = true, IsThreadSafe = true)]
		public static JobHandle Schedule(ref JobsUtility.JobScheduleParameters parameters)
		{
			JobHandle result;
			JobsUtility.Schedule_Injected(ref parameters, out result);
			return result;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000037C4 File Offset: 0x000019C4
		[FreeFunction("ScheduleManagedJobParallelFor", ThrowsException = true, IsThreadSafe = true)]
		public static JobHandle ScheduleParallelFor(ref JobsUtility.JobScheduleParameters parameters, int arrayLength, int innerloopBatchCount)
		{
			JobHandle result;
			JobsUtility.ScheduleParallelFor_Injected(ref parameters, arrayLength, innerloopBatchCount, out result);
			return result;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000037DC File Offset: 0x000019DC
		[FreeFunction("ScheduleManagedJobParallelForDeferArraySize", ThrowsException = true, IsThreadSafe = true)]
		public unsafe static JobHandle ScheduleParallelForDeferArraySize(ref JobsUtility.JobScheduleParameters parameters, int innerloopBatchCount, void* listData, void* listDataAtomicSafetyHandle)
		{
			JobHandle result;
			JobsUtility.ScheduleParallelForDeferArraySize_Injected(ref parameters, innerloopBatchCount, listData, listDataAtomicSafetyHandle, out result);
			return result;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000037F8 File Offset: 0x000019F8
		[FreeFunction("ScheduleManagedJobParallelForTransform", ThrowsException = true)]
		public static JobHandle ScheduleParallelForTransform(ref JobsUtility.JobScheduleParameters parameters, IntPtr transfromAccesssArray)
		{
			JobHandle result;
			JobsUtility.ScheduleParallelForTransform_Injected(ref parameters, transfromAccesssArray, out result);
			return result;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00003810 File Offset: 0x00001A10
		[FreeFunction("ScheduleManagedJobParallelForTransformReadOnly", ThrowsException = true)]
		public static JobHandle ScheduleParallelForTransformReadOnly(ref JobsUtility.JobScheduleParameters parameters, IntPtr transfromAccesssArray, int innerloopBatchCount)
		{
			JobHandle result;
			JobsUtility.ScheduleParallelForTransformReadOnly_Injected(ref parameters, transfromAccesssArray, innerloopBatchCount, out result);
			return result;
		}

		// Token: 0x06000196 RID: 406
		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true)]
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void PatchBufferMinMaxRanges(IntPtr bufferRangePatchData, void* jobdata, int startIndex, int rangeSize);

		// Token: 0x06000197 RID: 407
		[FreeFunction(ThrowsException = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateJobReflectionData(Type wrapperJobType, Type userJobType, object managedJobFunction0, object managedJobFunction1, object managedJobFunction2);

		// Token: 0x06000198 RID: 408 RVA: 0x00003828 File Offset: 0x00001A28
		[Obsolete("JobType is obsolete. The parameter should be removed. (UnityUpgradable) -> !1")]
		public static IntPtr CreateJobReflectionData(Type type, JobType jobType, object managedJobFunction0, object managedJobFunction1 = null, object managedJobFunction2 = null)
		{
			return JobsUtility.CreateJobReflectionData(type, type, managedJobFunction0, managedJobFunction1, managedJobFunction2);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00003848 File Offset: 0x00001A48
		public static IntPtr CreateJobReflectionData(Type type, object managedJobFunction0, object managedJobFunction1 = null, object managedJobFunction2 = null)
		{
			return JobsUtility.CreateJobReflectionData(type, type, managedJobFunction0, managedJobFunction1, managedJobFunction2);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00003864 File Offset: 0x00001A64
		[Obsolete("JobType is obsolete. The parameter should be removed. (UnityUpgradable) -> !2")]
		public static IntPtr CreateJobReflectionData(Type wrapperJobType, Type userJobType, JobType jobType, object managedJobFunction0)
		{
			return JobsUtility.CreateJobReflectionData(wrapperJobType, userJobType, managedJobFunction0, null, null);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00003880 File Offset: 0x00001A80
		public static IntPtr CreateJobReflectionData(Type wrapperJobType, Type userJobType, object managedJobFunction0)
		{
			return JobsUtility.CreateJobReflectionData(wrapperJobType, userJobType, managedJobFunction0, null, null);
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600019C RID: 412
		public static extern bool IsExecutingJob { [NativeMethod(IsFreeFunction = true, IsThreadSafe = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600019D RID: 413
		// (set) Token: 0x0600019E RID: 414
		public static extern bool JobDebuggerEnabled { [FreeFunction] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600019F RID: 415
		// (set) Token: 0x060001A0 RID: 416
		public static extern bool JobCompilerEnabled { [FreeFunction] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060001A1 RID: 417
		[FreeFunction("JobSystem::GetJobQueueWorkerThreadCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetJobQueueWorkerThreadCount();

		// Token: 0x060001A2 RID: 418
		[FreeFunction("JobSystem::ForceSetJobQueueWorkerThreadCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetJobQueueMaximumActiveThreadCount(int count);

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001A3 RID: 419
		public static extern int JobWorkerMaximumCount { [FreeFunction("JobSystem::GetJobQueueMaximumThreadCount")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060001A4 RID: 420
		[FreeFunction("JobSystem::ResetJobQueueWorkerThreadCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResetJobWorkerCount();

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000389C File Offset: 0x00001A9C
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x000038B4 File Offset: 0x00001AB4
		public static int JobWorkerCount
		{
			get
			{
				return JobsUtility.GetJobQueueWorkerThreadCount();
			}
			set
			{
				bool flag = value < 0 || value > JobsUtility.JobWorkerMaximumCount;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("JobWorkerCount", string.Format("Invalid JobWorkerCount {0} must be in the range 0 -> {1}", value, JobsUtility.JobWorkerMaximumCount));
				}
				JobsUtility.SetJobQueueMaximumActiveThreadCount(value);
			}
		}

		// Token: 0x060001A7 RID: 423
		[FreeFunction("JobDebuggerGetSystemIdCellPtr")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSystemIdCellPtr();

		// Token: 0x060001A8 RID: 424
		[FreeFunction("JobDebuggerClearSystemIds")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ClearSystemIds();

		// Token: 0x060001A9 RID: 425
		[FreeFunction("JobDebuggerGetSystemIdMappings")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern int GetSystemIdMappings(JobHandle* handles, int* systemIds, int maxCount);

		// Token: 0x060001AA RID: 426 RVA: 0x00003904 File Offset: 0x00001B04
		[RequiredByNativeCode]
		private static void InvokePanicFunction()
		{
			JobsUtility.PanicFunction_ panicFunction = JobsUtility.PanicFunction;
			bool flag = panicFunction == null;
			if (!flag)
			{
				panicFunction();
			}
		}

		// Token: 0x060001AB RID: 427
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Schedule_Injected(ref JobsUtility.JobScheduleParameters parameters, out JobHandle ret);

		// Token: 0x060001AC RID: 428
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScheduleParallelFor_Injected(ref JobsUtility.JobScheduleParameters parameters, int arrayLength, int innerloopBatchCount, out JobHandle ret);

		// Token: 0x060001AD RID: 429
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleParallelForDeferArraySize_Injected(ref JobsUtility.JobScheduleParameters parameters, int innerloopBatchCount, void* listData, void* listDataAtomicSafetyHandle, out JobHandle ret);

		// Token: 0x060001AE RID: 430
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScheduleParallelForTransform_Injected(ref JobsUtility.JobScheduleParameters parameters, IntPtr transfromAccesssArray, out JobHandle ret);

		// Token: 0x060001AF RID: 431
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScheduleParallelForTransformReadOnly_Injected(ref JobsUtility.JobScheduleParameters parameters, IntPtr transfromAccesssArray, int innerloopBatchCount, out JobHandle ret);

		// Token: 0x04000192 RID: 402
		public const int MaxJobThreadCount = 128;

		// Token: 0x04000193 RID: 403
		public const int CacheLineSize = 64;

		// Token: 0x04000194 RID: 404
		internal static JobsUtility.PanicFunction_ PanicFunction;

		// Token: 0x0200006C RID: 108
		public struct JobScheduleParameters
		{
			// Token: 0x060001B0 RID: 432 RVA: 0x00003929 File Offset: 0x00001B29
			public unsafe JobScheduleParameters(void* i_jobData, IntPtr i_reflectionData, JobHandle i_dependency, ScheduleMode i_scheduleMode)
			{
				this.Dependency = i_dependency;
				this.JobDataPtr = (IntPtr)i_jobData;
				this.ReflectionData = i_reflectionData;
				this.ScheduleMode = (int)i_scheduleMode;
			}

			// Token: 0x04000195 RID: 405
			public JobHandle Dependency;

			// Token: 0x04000196 RID: 406
			public int ScheduleMode;

			// Token: 0x04000197 RID: 407
			public IntPtr ReflectionData;

			// Token: 0x04000198 RID: 408
			public IntPtr JobDataPtr;
		}

		// Token: 0x0200006D RID: 109
		// (Invoke) Token: 0x060001B2 RID: 434
		internal delegate void PanicFunction_();
	}
}
