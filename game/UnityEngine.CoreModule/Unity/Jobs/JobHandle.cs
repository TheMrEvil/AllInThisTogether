using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace Unity.Jobs
{
	// Token: 0x02000063 RID: 99
	[NativeType(Header = "Runtime/Jobs/ScriptBindings/JobsBindings.h")]
	public struct JobHandle
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00003500 File Offset: 0x00001700
		public void Complete()
		{
			bool flag = this.jobGroup == IntPtr.Zero;
			if (!flag)
			{
				JobHandle.ScheduleBatchedJobsAndComplete(ref this);
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000352C File Offset: 0x0000172C
		public unsafe static void CompleteAll(ref JobHandle job0, ref JobHandle job1)
		{
			JobHandle* ptr = stackalloc JobHandle[checked(unchecked((UIntPtr)2) * (UIntPtr)sizeof(JobHandle))];
			*ptr = job0;
			ptr[1] = job1;
			JobHandle.ScheduleBatchedJobsAndCompleteAll((void*)ptr, 2);
			job0 = default(JobHandle);
			job1 = default(JobHandle);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000357C File Offset: 0x0000177C
		public unsafe static void CompleteAll(ref JobHandle job0, ref JobHandle job1, ref JobHandle job2)
		{
			JobHandle* ptr = stackalloc JobHandle[checked(unchecked((UIntPtr)3) * (UIntPtr)sizeof(JobHandle))];
			*ptr = job0;
			ptr[1] = job1;
			ptr[2] = job2;
			JobHandle.ScheduleBatchedJobsAndCompleteAll((void*)ptr, 3);
			job0 = default(JobHandle);
			job1 = default(JobHandle);
			job2 = default(JobHandle);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000035E8 File Offset: 0x000017E8
		public static void CompleteAll(NativeArray<JobHandle> jobs)
		{
			JobHandle.ScheduleBatchedJobsAndCompleteAll(jobs.GetUnsafeReadOnlyPtr<JobHandle>(), jobs.Length);
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00003600 File Offset: 0x00001800
		public bool IsCompleted
		{
			get
			{
				return JobHandle.ScheduleBatchedJobsAndIsCompleted(ref this);
			}
		}

		// Token: 0x0600017A RID: 378
		[NativeMethod("ScheduleBatchedScriptingJobs", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ScheduleBatchedJobs();

		// Token: 0x0600017B RID: 379
		[NativeMethod("ScheduleBatchedScriptingJobsAndComplete", IsFreeFunction = true, IsThreadSafe = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScheduleBatchedJobsAndComplete(ref JobHandle job);

		// Token: 0x0600017C RID: 380
		[NativeMethod("ScheduleBatchedScriptingJobsAndIsCompleted", IsFreeFunction = true, IsThreadSafe = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ScheduleBatchedJobsAndIsCompleted(ref JobHandle job);

		// Token: 0x0600017D RID: 381
		[NativeMethod("ScheduleBatchedScriptingJobsAndCompleteAll", IsFreeFunction = true, IsThreadSafe = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleBatchedJobsAndCompleteAll(void* jobs, int count);

		// Token: 0x0600017E RID: 382 RVA: 0x00003618 File Offset: 0x00001818
		public static JobHandle CombineDependencies(JobHandle job0, JobHandle job1)
		{
			return JobHandle.CombineDependenciesInternal2(ref job0, ref job1);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00003634 File Offset: 0x00001834
		public static JobHandle CombineDependencies(JobHandle job0, JobHandle job1, JobHandle job2)
		{
			return JobHandle.CombineDependenciesInternal3(ref job0, ref job1, ref job2);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00003654 File Offset: 0x00001854
		public static JobHandle CombineDependencies(NativeArray<JobHandle> jobs)
		{
			return JobHandle.CombineDependenciesInternalPtr(jobs.GetUnsafeReadOnlyPtr<JobHandle>(), jobs.Length);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00003678 File Offset: 0x00001878
		public static JobHandle CombineDependencies(NativeSlice<JobHandle> jobs)
		{
			return JobHandle.CombineDependenciesInternalPtr(jobs.GetUnsafeReadOnlyPtr<JobHandle>(), jobs.Length);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000369C File Offset: 0x0000189C
		[NativeMethod(IsFreeFunction = true, IsThreadSafe = true, ThrowsException = true)]
		private static JobHandle CombineDependenciesInternal2(ref JobHandle job0, ref JobHandle job1)
		{
			JobHandle result;
			JobHandle.CombineDependenciesInternal2_Injected(ref job0, ref job1, out result);
			return result;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000036B4 File Offset: 0x000018B4
		[NativeMethod(IsFreeFunction = true, IsThreadSafe = true, ThrowsException = true)]
		private static JobHandle CombineDependenciesInternal3(ref JobHandle job0, ref JobHandle job1, ref JobHandle job2)
		{
			JobHandle result;
			JobHandle.CombineDependenciesInternal3_Injected(ref job0, ref job1, ref job2, out result);
			return result;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000036CC File Offset: 0x000018CC
		[NativeMethod(IsFreeFunction = true, IsThreadSafe = true, ThrowsException = true)]
		internal unsafe static JobHandle CombineDependenciesInternalPtr(void* jobs, int count)
		{
			JobHandle result;
			JobHandle.CombineDependenciesInternalPtr_Injected(jobs, count, out result);
			return result;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000036E3 File Offset: 0x000018E3
		[NativeMethod(IsFreeFunction = true, IsThreadSafe = true)]
		public static bool CheckFenceIsDependencyOrDidSyncFence(JobHandle jobHandle, JobHandle dependsOn)
		{
			return JobHandle.CheckFenceIsDependencyOrDidSyncFence_Injected(ref jobHandle, ref dependsOn);
		}

		// Token: 0x06000186 RID: 390
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CombineDependenciesInternal2_Injected(ref JobHandle job0, ref JobHandle job1, out JobHandle ret);

		// Token: 0x06000187 RID: 391
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CombineDependenciesInternal3_Injected(ref JobHandle job0, ref JobHandle job1, ref JobHandle job2, out JobHandle ret);

		// Token: 0x06000188 RID: 392
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void CombineDependenciesInternalPtr_Injected(void* jobs, int count, out JobHandle ret);

		// Token: 0x06000189 RID: 393
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CheckFenceIsDependencyOrDidSyncFence_Injected(ref JobHandle jobHandle, ref JobHandle dependsOn);

		// Token: 0x0400017E RID: 382
		[NativeDisableUnsafePtrRestriction]
		internal IntPtr jobGroup;

		// Token: 0x0400017F RID: 383
		internal int version;
	}
}
