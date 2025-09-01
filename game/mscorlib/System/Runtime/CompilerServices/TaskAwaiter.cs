using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Internal.Threading.Tasks.Tracing;

namespace System.Runtime.CompilerServices
{
	/// <summary>Provides an object that waits for the completion of an asynchronous task.</summary>
	// Token: 0x0200080F RID: 2063
	public readonly struct TaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion, ITaskAwaiter
	{
		// Token: 0x06004639 RID: 17977 RVA: 0x000E5BC7 File Offset: 0x000E3DC7
		internal TaskAwaiter(Task task)
		{
			this.m_task = task;
		}

		/// <summary>Gets a value that indicates whether the asynchronous task has completed.</summary>
		/// <returns>
		///   <see langword="true" /> if the task has completed; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The <see cref="T:System.Runtime.CompilerServices.TaskAwaiter" /> object was not properly initialized.</exception>
		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x0600463A RID: 17978 RVA: 0x000E5BD0 File Offset: 0x000E3DD0
		public bool IsCompleted
		{
			get
			{
				return this.m_task.IsCompleted;
			}
		}

		/// <summary>Sets the action to perform when the <see cref="T:System.Runtime.CompilerServices.TaskAwaiter" /> object stops waiting for the asynchronous task to complete.</summary>
		/// <param name="continuation">The action to perform when the wait operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="continuation" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NullReferenceException">The <see cref="T:System.Runtime.CompilerServices.TaskAwaiter" /> object was not properly initialized.</exception>
		// Token: 0x0600463B RID: 17979 RVA: 0x000E5BDD File Offset: 0x000E3DDD
		[SecuritySafeCritical]
		public void OnCompleted(Action continuation)
		{
			TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, true);
		}

		/// <summary>Schedules the continuation action for the asynchronous task that is associated with this awaiter.</summary>
		/// <param name="continuation">The action to invoke when the await operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="continuation" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The awaiter was not properly initialized.</exception>
		// Token: 0x0600463C RID: 17980 RVA: 0x000E5BED File Offset: 0x000E3DED
		[SecurityCritical]
		public void UnsafeOnCompleted(Action continuation)
		{
			TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, false);
		}

		/// <summary>Ends the wait for the completion of the asynchronous task.</summary>
		/// <exception cref="T:System.NullReferenceException">The <see cref="T:System.Runtime.CompilerServices.TaskAwaiter" /> object was not properly initialized.</exception>
		/// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The task was canceled.</exception>
		/// <exception cref="T:System.Exception">The task completed in a <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> state.</exception>
		// Token: 0x0600463D RID: 17981 RVA: 0x000E5BFD File Offset: 0x000E3DFD
		[StackTraceHidden]
		public void GetResult()
		{
			TaskAwaiter.ValidateEnd(this.m_task);
		}

		// Token: 0x0600463E RID: 17982 RVA: 0x000E5C0A File Offset: 0x000E3E0A
		[StackTraceHidden]
		internal static void ValidateEnd(Task task)
		{
			if (task.IsWaitNotificationEnabledOrNotRanToCompletion)
			{
				TaskAwaiter.HandleNonSuccessAndDebuggerNotification(task);
			}
		}

		// Token: 0x0600463F RID: 17983 RVA: 0x000E5C1C File Offset: 0x000E3E1C
		[StackTraceHidden]
		private static void HandleNonSuccessAndDebuggerNotification(Task task)
		{
			if (!task.IsCompleted)
			{
				task.InternalWait(-1, default(CancellationToken));
			}
			task.NotifyDebuggerOfWaitCompletionIfNecessary();
			if (!task.IsCompletedSuccessfully)
			{
				TaskAwaiter.ThrowForNonSuccess(task);
			}
		}

		// Token: 0x06004640 RID: 17984 RVA: 0x000E5C58 File Offset: 0x000E3E58
		[StackTraceHidden]
		private static void ThrowForNonSuccess(Task task)
		{
			TaskStatus status = task.Status;
			if (status == TaskStatus.Canceled)
			{
				ExceptionDispatchInfo cancellationExceptionDispatchInfo = task.GetCancellationExceptionDispatchInfo();
				if (cancellationExceptionDispatchInfo != null)
				{
					cancellationExceptionDispatchInfo.Throw();
				}
				throw new TaskCanceledException(task);
			}
			if (status != TaskStatus.Faulted)
			{
				return;
			}
			ReadOnlyCollection<ExceptionDispatchInfo> exceptionDispatchInfos = task.GetExceptionDispatchInfos();
			if (exceptionDispatchInfos.Count > 0)
			{
				exceptionDispatchInfos[0].Throw();
				return;
			}
			throw task.Exception;
		}

		// Token: 0x06004641 RID: 17985 RVA: 0x000E5CAF File Offset: 0x000E3EAF
		internal static void OnCompletedInternal(Task task, Action continuation, bool continueOnCapturedContext, bool flowExecutionContext)
		{
			if (continuation == null)
			{
				throw new ArgumentNullException("continuation");
			}
			if (TaskTrace.Enabled)
			{
				continuation = TaskAwaiter.OutputWaitEtwEvents(task, continuation);
			}
			task.SetContinuationForAwait(continuation, continueOnCapturedContext, flowExecutionContext);
		}

		// Token: 0x06004642 RID: 17986 RVA: 0x000E5CD8 File Offset: 0x000E3ED8
		private static Action OutputWaitEtwEvents(Task task, Action continuation)
		{
			Task internalCurrent = Task.InternalCurrent;
			TaskTrace.TaskWaitBegin_Asynchronous((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Default.Id, (internalCurrent != null) ? internalCurrent.Id : 0, task.Id);
			return delegate()
			{
				if (TaskTrace.Enabled)
				{
					Task internalCurrent2 = Task.InternalCurrent;
					TaskTrace.TaskWaitEnd((internalCurrent2 != null) ? internalCurrent2.m_taskScheduler.Id : TaskScheduler.Default.Id, (internalCurrent2 != null) ? internalCurrent2.Id : 0, task.Id);
				}
				continuation();
			};
		}

		// Token: 0x04002D49 RID: 11593
		internal readonly Task m_task;

		// Token: 0x02000810 RID: 2064
		[CompilerGenerated]
		private sealed class <>c__DisplayClass11_0
		{
			// Token: 0x06004643 RID: 17987 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass11_0()
			{
			}

			// Token: 0x06004644 RID: 17988 RVA: 0x000E5D44 File Offset: 0x000E3F44
			internal void <OutputWaitEtwEvents>b__0()
			{
				if (TaskTrace.Enabled)
				{
					Task internalCurrent = Task.InternalCurrent;
					TaskTrace.TaskWaitEnd((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Default.Id, (internalCurrent != null) ? internalCurrent.Id : 0, this.task.Id);
				}
				this.continuation();
			}

			// Token: 0x04002D4A RID: 11594
			public Task task;

			// Token: 0x04002D4B RID: 11595
			public Action continuation;
		}
	}
}
