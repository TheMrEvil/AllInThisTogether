using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Defines the standard operation codes that the event source attaches to events.</summary>
	// Token: 0x020009E9 RID: 2537
	public enum EventOpcode
	{
		/// <summary>An informational event.</summary>
		// Token: 0x040037F2 RID: 14322
		Info,
		/// <summary>An event that is published when an application starts a new transaction or activity. This operation code can be embedded within another transaction or activity when multiple events that have the <see cref="F:System.Diagnostics.Tracing.EventOpcode.Start" /> code follow each other without an intervening event that has a <see cref="F:System.Diagnostics.Tracing.EventOpcode.Stop" /> code.</summary>
		// Token: 0x040037F3 RID: 14323
		Start,
		/// <summary>An event that is published when an activity or a transaction in an application ends. The event corresponds to the last unpaired event that has a <see cref="F:System.Diagnostics.Tracing.EventOpcode.Start" /> operation code.</summary>
		// Token: 0x040037F4 RID: 14324
		Stop,
		/// <summary>A trace collection start event.</summary>
		// Token: 0x040037F5 RID: 14325
		DataCollectionStart,
		/// <summary>A trace collection stop event.</summary>
		// Token: 0x040037F6 RID: 14326
		DataCollectionStop,
		/// <summary>An extension event.</summary>
		// Token: 0x040037F7 RID: 14327
		Extension,
		/// <summary>An event that is published after an activity in an application replies to an event.</summary>
		// Token: 0x040037F8 RID: 14328
		Reply,
		/// <summary>An event that is published after an activity in an application resumes from a suspended state. The event should follow an event that has the <see cref="F:System.Diagnostics.Tracing.EventOpcode.Suspend" /> operation code.</summary>
		// Token: 0x040037F9 RID: 14329
		Resume,
		/// <summary>An event that is published when an activity in an application is suspended.</summary>
		// Token: 0x040037FA RID: 14330
		Suspend,
		/// <summary>An event that is published when one activity in an application transfers data or system resources to another activity.</summary>
		// Token: 0x040037FB RID: 14331
		Send,
		/// <summary>An event that is published when one activity in an application receives data.</summary>
		// Token: 0x040037FC RID: 14332
		Receive = 240
	}
}
