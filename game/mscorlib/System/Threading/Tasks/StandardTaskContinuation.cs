using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200036E RID: 878
	internal class StandardTaskContinuation : TaskContinuation
	{
		// Token: 0x06002480 RID: 9344 RVA: 0x00082B0C File Offset: 0x00080D0C
		internal StandardTaskContinuation(Task task, TaskContinuationOptions options, TaskScheduler scheduler)
		{
			this.m_task = task;
			this.m_options = options;
			this.m_taskScheduler = scheduler;
			if (DebuggerSupport.LoggingOn)
			{
				CausalityTraceLevel traceLevel = CausalityTraceLevel.Required;
				Task task2 = this.m_task;
				string str = "Task.ContinueWith: ";
				Delegate action = task.m_action;
				DebuggerSupport.TraceOperationCreation(traceLevel, task2, str + ((action != null) ? action.ToString() : null), 0UL);
			}
			DebuggerSupport.AddToActiveTasks(this.m_task);
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x00082B70 File Offset: 0x00080D70
		internal override void Run(Task completedTask, bool bCanInlineContinuationTask)
		{
			TaskContinuationOptions options = this.m_options;
			bool flag = completedTask.IsCompletedSuccessfully ? ((options & TaskContinuationOptions.NotOnRanToCompletion) == TaskContinuationOptions.None) : (completedTask.IsCanceled ? ((options & TaskContinuationOptions.NotOnCanceled) == TaskContinuationOptions.None) : ((options & TaskContinuationOptions.NotOnFaulted) == TaskContinuationOptions.None));
			Task task = this.m_task;
			if (flag)
			{
				if (!task.IsCanceled && DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationRelation(CausalityTraceLevel.Important, task, CausalityRelation.AssignDelegate);
				}
				task.m_taskScheduler = this.m_taskScheduler;
				if (bCanInlineContinuationTask && (options & TaskContinuationOptions.ExecuteSynchronously) != TaskContinuationOptions.None)
				{
					TaskContinuation.InlineIfPossibleOrElseQueue(task, true);
					return;
				}
				try
				{
					task.ScheduleAndStart(true);
					return;
				}
				catch (TaskSchedulerException)
				{
					return;
				}
			}
			task.InternalCancel(false);
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x00082C1C File Offset: 0x00080E1C
		internal override Delegate[] GetDelegateContinuationsForDebugger()
		{
			if (this.m_task.m_action == null)
			{
				return this.m_task.GetDelegateContinuationsForDebugger();
			}
			return new Delegate[]
			{
				this.m_task.m_action
			};
		}

		// Token: 0x04001D32 RID: 7474
		internal readonly Task m_task;

		// Token: 0x04001D33 RID: 7475
		internal readonly TaskContinuationOptions m_options;

		// Token: 0x04001D34 RID: 7476
		private readonly TaskScheduler m_taskScheduler;
	}
}
