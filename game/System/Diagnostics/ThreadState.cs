using System;

namespace System.Diagnostics
{
	/// <summary>Specifies the current execution state of the thread.</summary>
	// Token: 0x02000282 RID: 642
	public enum ThreadState
	{
		/// <summary>A state that indicates the thread has been initialized, but has not yet started.</summary>
		// Token: 0x04000B70 RID: 2928
		Initialized,
		/// <summary>A state that indicates the thread is waiting to use a processor because no processor is free. The thread is prepared to run on the next available processor.</summary>
		// Token: 0x04000B71 RID: 2929
		Ready,
		/// <summary>A state that indicates the thread is currently using a processor.</summary>
		// Token: 0x04000B72 RID: 2930
		Running,
		/// <summary>A state that indicates the thread is about to use a processor. Only one thread can be in this state at a time.</summary>
		// Token: 0x04000B73 RID: 2931
		Standby,
		/// <summary>A state that indicates the thread has finished executing and has exited.</summary>
		// Token: 0x04000B74 RID: 2932
		Terminated,
		/// <summary>A state that indicates the thread is waiting for a resource, other than the processor, before it can execute. For example, it might be waiting for its execution stack to be paged in from disk.</summary>
		// Token: 0x04000B75 RID: 2933
		Transition = 6,
		/// <summary>The state of the thread is unknown.</summary>
		// Token: 0x04000B76 RID: 2934
		Unknown,
		/// <summary>A state that indicates the thread is not ready to use the processor because it is waiting for a peripheral operation to complete or a resource to become free. When the thread is ready, it will be rescheduled.</summary>
		// Token: 0x04000B77 RID: 2935
		Wait = 5
	}
}
