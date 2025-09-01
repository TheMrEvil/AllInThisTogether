using System;

namespace System.Diagnostics
{
	/// <summary>Specifies how to handle entries in an event log that has reached its maximum file size.</summary>
	// Token: 0x0200026F RID: 623
	public enum OverflowAction
	{
		/// <summary>Indicates that existing entries are retained when the event log is full and new entries are discarded.</summary>
		// Token: 0x04000B09 RID: 2825
		DoNotOverwrite = -1,
		/// <summary>Indicates that each new entry overwrites the oldest entry when the event log is full.</summary>
		// Token: 0x04000B0A RID: 2826
		OverwriteAsNeeded,
		/// <summary>Indicates that new events overwrite events older than specified by the <see cref="P:System.Diagnostics.EventLog.MinimumRetentionDays" /> property value when the event log is full. New events are discarded if the event log is full and there are no events older than specified by the <see cref="P:System.Diagnostics.EventLog.MinimumRetentionDays" /> property value.</summary>
		// Token: 0x04000B0B RID: 2827
		OverwriteOlder
	}
}
