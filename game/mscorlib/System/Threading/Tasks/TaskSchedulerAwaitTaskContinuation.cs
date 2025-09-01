using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000371 RID: 881
	internal sealed class TaskSchedulerAwaitTaskContinuation : AwaitTaskContinuation
	{
		// Token: 0x0600248B RID: 9355 RVA: 0x00082D11 File Offset: 0x00080F11
		internal TaskSchedulerAwaitTaskContinuation(TaskScheduler scheduler, Action action, bool flowExecutionContext) : base(action, flowExecutionContext)
		{
			this.m_scheduler = scheduler;
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x00082D24 File Offset: 0x00080F24
		internal sealed override void Run(Task ignored, bool canInlineContinuationTask)
		{
			if (this.m_scheduler == TaskScheduler.Default)
			{
				base.Run(ignored, canInlineContinuationTask);
				return;
			}
			bool flag = canInlineContinuationTask && (TaskScheduler.InternalCurrent == this.m_scheduler || ThreadPool.IsThreadPoolThread);
			Task task = base.CreateTask(delegate(object state)
			{
				try
				{
					((Action)state)();
				}
				catch (Exception exc)
				{
					AwaitTaskContinuation.ThrowAsyncIfNecessary(exc);
				}
			}, this.m_action, this.m_scheduler);
			if (flag)
			{
				TaskContinuation.InlineIfPossibleOrElseQueue(task, false);
				return;
			}
			try
			{
				task.ScheduleAndStart(false);
			}
			catch (TaskSchedulerException)
			{
			}
		}

		// Token: 0x04001D39 RID: 7481
		private readonly TaskScheduler m_scheduler;

		// Token: 0x02000372 RID: 882
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600248D RID: 9357 RVA: 0x00082DBC File Offset: 0x00080FBC
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600248E RID: 9358 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x0600248F RID: 9359 RVA: 0x00082DC8 File Offset: 0x00080FC8
			internal void <Run>b__2_0(object state)
			{
				try
				{
					((Action)state)();
				}
				catch (Exception exc)
				{
					AwaitTaskContinuation.ThrowAsyncIfNecessary(exc);
				}
			}

			// Token: 0x04001D3A RID: 7482
			public static readonly TaskSchedulerAwaitTaskContinuation.<>c <>9 = new TaskSchedulerAwaitTaskContinuation.<>c();

			// Token: 0x04001D3B RID: 7483
			public static Action<object> <>9__2_0;
		}
	}
}
