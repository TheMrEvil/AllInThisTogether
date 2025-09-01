using System;

namespace System.Diagnostics
{
	/// <summary>Defines access levels used by <see cref="T:System.Diagnostics.EventLog" /> permission classes.</summary>
	// Token: 0x02000261 RID: 609
	[Flags]
	public enum EventLogPermissionAccess
	{
		/// <summary>The <see cref="T:System.Diagnostics.EventLog" /> has no permissions.</summary>
		// Token: 0x04000AD1 RID: 2769
		None = 0,
		/// <summary>The <see cref="T:System.Diagnostics.EventLog" /> can read existing logs. <see langword="Note" /> This member is now obsolete, use <see cref="F:System.Diagnostics.EventLogPermissionAccess.Administer" /> instead.</summary>
		// Token: 0x04000AD2 RID: 2770
		[Obsolete]
		Browse = 2,
		/// <summary>The <see cref="T:System.Diagnostics.EventLog" /> can read or write to existing logs, and create event sources and logs. <see langword="Note" /> This member is now obsolete, use <see cref="F:System.Diagnostics.EventLogPermissionAccess.Write" /> instead.</summary>
		// Token: 0x04000AD3 RID: 2771
		[Obsolete]
		Instrument = 6,
		/// <summary>The <see cref="T:System.Diagnostics.EventLog" /> can read existing logs, delete event sources or logs, respond to entries, clear an event log, listen to events, and access a collection of all event logs. <see langword="Note" /> This member is now obsolete, use <see cref="F:System.Diagnostics.EventLogPermissionAccess.Administer" /> instead.</summary>
		// Token: 0x04000AD4 RID: 2772
		[Obsolete]
		Audit = 10,
		/// <summary>The <see cref="T:System.Diagnostics.EventLog" /> can write to existing logs, and create event sources and logs.</summary>
		// Token: 0x04000AD5 RID: 2773
		Write = 16,
		/// <summary>The <see cref="T:System.Diagnostics.EventLog" /> can create an event source, read existing logs, delete event sources or logs, respond to entries, clear an event log, listen to events, and access a collection of all event logs.</summary>
		// Token: 0x04000AD6 RID: 2774
		Administer = 48
	}
}
