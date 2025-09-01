using System;

namespace System.Threading.Tasks
{
	/// <summary>Represents the current stage in the lifecycle of a <see cref="T:System.Threading.Tasks.Task" />.</summary>
	// Token: 0x02000356 RID: 854
	public enum TaskStatus
	{
		/// <summary>The task has been initialized but has not yet been scheduled.</summary>
		// Token: 0x04001CC1 RID: 7361
		Created,
		/// <summary>The task is waiting to be activated and scheduled internally by the .NET Framework infrastructure.</summary>
		// Token: 0x04001CC2 RID: 7362
		WaitingForActivation,
		/// <summary>The task has been scheduled for execution but has not yet begun executing.</summary>
		// Token: 0x04001CC3 RID: 7363
		WaitingToRun,
		/// <summary>The task is running but has not yet completed.</summary>
		// Token: 0x04001CC4 RID: 7364
		Running,
		/// <summary>The task has finished executing and is implicitly waiting for attached child tasks to complete.</summary>
		// Token: 0x04001CC5 RID: 7365
		WaitingForChildrenToComplete,
		/// <summary>The task completed execution successfully.</summary>
		// Token: 0x04001CC6 RID: 7366
		RanToCompletion,
		/// <summary>The task acknowledged cancellation by throwing an OperationCanceledException with its own CancellationToken while the token was in signaled state, or the task's CancellationToken was already signaled before the task started executing. For more information, see Task Cancellation.</summary>
		// Token: 0x04001CC7 RID: 7367
		Canceled,
		/// <summary>The task completed due to an unhandled exception.</summary>
		// Token: 0x04001CC8 RID: 7368
		Faulted
	}
}
