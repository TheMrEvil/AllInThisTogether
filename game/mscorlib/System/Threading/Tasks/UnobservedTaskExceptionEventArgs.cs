using System;

namespace System.Threading.Tasks
{
	/// <summary>Provides data for the event that is raised when a faulted <see cref="T:System.Threading.Tasks.Task" />'s exception goes unobserved.</summary>
	// Token: 0x0200037D RID: 893
	public class UnobservedTaskExceptionEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.UnobservedTaskExceptionEventArgs" /> class with the unobserved exception.</summary>
		/// <param name="exception">The Exception that has gone unobserved.</param>
		// Token: 0x06002532 RID: 9522 RVA: 0x00084493 File Offset: 0x00082693
		public UnobservedTaskExceptionEventArgs(AggregateException exception)
		{
			this.m_exception = exception;
		}

		/// <summary>Marks the <see cref="P:System.Threading.Tasks.UnobservedTaskExceptionEventArgs.Exception" /> as "observed," thus preventing it from triggering exception escalation policy which, by default, terminates the process.</summary>
		// Token: 0x06002533 RID: 9523 RVA: 0x000844A2 File Offset: 0x000826A2
		public void SetObserved()
		{
			this.m_observed = true;
		}

		/// <summary>Gets whether this exception has been marked as "observed."</summary>
		/// <returns>true if this exception has been marked as "observed"; otherwise false.</returns>
		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06002534 RID: 9524 RVA: 0x000844AB File Offset: 0x000826AB
		public bool Observed
		{
			get
			{
				return this.m_observed;
			}
		}

		/// <summary>The Exception that went unobserved.</summary>
		/// <returns>The Exception that went unobserved.</returns>
		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06002535 RID: 9525 RVA: 0x000844B3 File Offset: 0x000826B3
		public AggregateException Exception
		{
			get
			{
				return this.m_exception;
			}
		}

		// Token: 0x04001D57 RID: 7511
		private AggregateException m_exception;

		// Token: 0x04001D58 RID: 7512
		internal bool m_observed;
	}
}
