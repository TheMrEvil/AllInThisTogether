using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200036D RID: 877
	internal abstract class TaskContinuation
	{
		// Token: 0x0600247C RID: 9340
		internal abstract void Run(Task completedTask, bool bCanInlineContinuationTask);

		// Token: 0x0600247D RID: 9341 RVA: 0x00082A98 File Offset: 0x00080C98
		protected static void InlineIfPossibleOrElseQueue(Task task, bool needsProtection)
		{
			if (needsProtection)
			{
				if (!task.MarkStarted())
				{
					return;
				}
			}
			else
			{
				task.m_stateFlags |= 65536;
			}
			try
			{
				if (!task.m_taskScheduler.TryRunInline(task, false))
				{
					task.m_taskScheduler.QueueTask(task);
				}
			}
			catch (Exception innerException)
			{
				TaskSchedulerException exceptionObject = new TaskSchedulerException(innerException);
				task.AddException(exceptionObject);
				task.Finish(false);
			}
		}

		// Token: 0x0600247E RID: 9342
		internal abstract Delegate[] GetDelegateContinuationsForDebugger();

		// Token: 0x0600247F RID: 9343 RVA: 0x0000259F File Offset: 0x0000079F
		protected TaskContinuation()
		{
		}
	}
}
