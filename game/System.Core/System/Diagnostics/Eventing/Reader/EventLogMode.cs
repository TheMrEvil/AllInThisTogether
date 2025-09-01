using System;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Determines the behavior for the event log service handles an event log when the log reaches its maximum allowed size (when the event log is full).</summary>
	// Token: 0x020003A0 RID: 928
	public enum EventLogMode
	{
		/// <summary>Archive the log when full, do not overwrite events. The log is automatically archived when necessary. No events are overwritten. </summary>
		// Token: 0x04000D4F RID: 3407
		AutoBackup = 1,
		/// <summary>New events continue to be stored when the log file is full. Each new incoming event replaces the oldest event in the log.</summary>
		// Token: 0x04000D50 RID: 3408
		Circular = 0,
		/// <summary>Do not overwrite events. Clear the log manually rather than automatically.</summary>
		// Token: 0x04000D51 RID: 3409
		Retain = 2
	}
}
