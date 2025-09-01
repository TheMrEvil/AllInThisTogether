using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using Internal.Runtime.Augments;

namespace System.Threading.Tasks
{
	// Token: 0x02000367 RID: 871
	internal sealed class UnwrapPromise<TResult> : Task<TResult>, ITaskCompletionAction
	{
		// Token: 0x06002469 RID: 9321 RVA: 0x0008259C File Offset: 0x0008079C
		public UnwrapPromise(Task outerTask, bool lookForOce) : base(null, outerTask.CreationOptions & TaskCreationOptions.AttachedToParent)
		{
			this._lookForOce = lookForOce;
			this._state = 0;
			if (DebuggerSupport.LoggingOn)
			{
				DebuggerSupport.TraceOperationCreation(CausalityTraceLevel.Required, this, "Task.Unwrap", 0UL);
			}
			DebuggerSupport.AddToActiveTasks(this);
			if (outerTask.IsCompleted)
			{
				this.ProcessCompletedOuterTask(outerTask);
				return;
			}
			outerTask.AddCompletionAction(this);
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x000825F8 File Offset: 0x000807F8
		public void Invoke(Task completingTask)
		{
			StackGuard currentStackGuard = Task.CurrentStackGuard;
			if (currentStackGuard.TryBeginInliningScope())
			{
				try
				{
					this.InvokeCore(completingTask);
					return;
				}
				finally
				{
					currentStackGuard.EndInliningScope();
				}
			}
			this.InvokeCoreAsync(completingTask);
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x0008263C File Offset: 0x0008083C
		private void InvokeCore(Task completingTask)
		{
			byte state = this._state;
			if (state == 0)
			{
				this.ProcessCompletedOuterTask(completingTask);
				return;
			}
			if (state != 1)
			{
				return;
			}
			this.TrySetFromTask(completingTask, false);
			this._state = 2;
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x00082670 File Offset: 0x00080870
		private void InvokeCoreAsync(Task completingTask)
		{
			ThreadPool.UnsafeQueueUserWorkItem(delegate(object state)
			{
				Tuple<UnwrapPromise<TResult>, Task> tuple = (Tuple<UnwrapPromise<TResult>, Task>)state;
				tuple.Item1.InvokeCore(tuple.Item2);
			}, Tuple.Create<UnwrapPromise<TResult>, Task>(this, completingTask));
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x000826A0 File Offset: 0x000808A0
		private void ProcessCompletedOuterTask(Task task)
		{
			this._state = 1;
			TaskStatus status = task.Status;
			if (status != TaskStatus.RanToCompletion)
			{
				if (status - TaskStatus.Canceled <= 1)
				{
					this.TrySetFromTask(task, this._lookForOce);
					return;
				}
			}
			else
			{
				Task<Task<TResult>> task2 = task as Task<Task<TResult>>;
				this.ProcessInnerTask((task2 != null) ? task2.Result : ((Task<Task>)task).Result);
			}
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000826F8 File Offset: 0x000808F8
		private bool TrySetFromTask(Task task, bool lookForOce)
		{
			if (DebuggerSupport.LoggingOn)
			{
				DebuggerSupport.TraceOperationRelation(CausalityTraceLevel.Important, this, CausalityRelation.Join);
			}
			bool result = false;
			switch (task.Status)
			{
			case TaskStatus.RanToCompletion:
			{
				Task<TResult> task2 = task as Task<TResult>;
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, this, AsyncStatus.Completed);
				}
				DebuggerSupport.RemoveFromActiveTasks(this);
				result = base.TrySetResult((task2 != null) ? task2.Result : default(TResult));
				break;
			}
			case TaskStatus.Canceled:
				result = base.TrySetCanceled(task.CancellationToken, task.GetCancellationExceptionDispatchInfo());
				break;
			case TaskStatus.Faulted:
			{
				ReadOnlyCollection<ExceptionDispatchInfo> exceptionDispatchInfos = task.GetExceptionDispatchInfos();
				ExceptionDispatchInfo exceptionDispatchInfo;
				OperationCanceledException ex;
				if (lookForOce && exceptionDispatchInfos.Count > 0 && (exceptionDispatchInfo = exceptionDispatchInfos[0]) != null && (ex = (exceptionDispatchInfo.SourceException as OperationCanceledException)) != null)
				{
					result = base.TrySetCanceled(ex.CancellationToken, exceptionDispatchInfo);
				}
				else
				{
					result = base.TrySetException(exceptionDispatchInfos);
				}
				break;
			}
			}
			return result;
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x000827D4 File Offset: 0x000809D4
		private void ProcessInnerTask(Task task)
		{
			if (task == null)
			{
				base.TrySetCanceled(default(CancellationToken));
				this._state = 2;
				return;
			}
			if (task.IsCompleted)
			{
				this.TrySetFromTask(task, false);
				this._state = 2;
				return;
			}
			task.AddCompletionAction(this);
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06002470 RID: 9328 RVA: 0x000040F7 File Offset: 0x000022F7
		public bool InvokeMayRunArbitraryCode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001D27 RID: 7463
		private const byte STATE_WAITING_ON_OUTER_TASK = 0;

		// Token: 0x04001D28 RID: 7464
		private const byte STATE_WAITING_ON_INNER_TASK = 1;

		// Token: 0x04001D29 RID: 7465
		private const byte STATE_DONE = 2;

		// Token: 0x04001D2A RID: 7466
		private byte _state;

		// Token: 0x04001D2B RID: 7467
		private readonly bool _lookForOce;

		// Token: 0x02000368 RID: 872
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002471 RID: 9329 RVA: 0x0008281C File Offset: 0x00080A1C
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002472 RID: 9330 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x06002473 RID: 9331 RVA: 0x00082828 File Offset: 0x00080A28
			internal void <InvokeCoreAsync>b__8_0(object state)
			{
				Tuple<UnwrapPromise<TResult>, Task> tuple = (Tuple<UnwrapPromise<TResult>, Task>)state;
				tuple.Item1.InvokeCore(tuple.Item2);
			}

			// Token: 0x04001D2C RID: 7468
			public static readonly UnwrapPromise<TResult>.<>c <>9 = new UnwrapPromise<TResult>.<>c();

			// Token: 0x04001D2D RID: 7469
			public static WaitCallback <>9__8_0;
		}
	}
}
