using System;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Defines the type of events that are logged in an event log. Each log can only contain one type of event.</summary>
	// Token: 0x020003A1 RID: 929
	public enum EventLogType
	{
		/// <summary>These events are primarily for end users, administrators, and support. The events that are found in the Administrative type logs indicate a problem and a well-defined solution that an administrator can act on. An example of an administrative event is an event that occurs when an application fails to connect to a printer. </summary>
		// Token: 0x04000D53 RID: 3411
		Administrative,
		/// <summary>Events in an analytic event log are published in high volume. They describe program operation and indicate problems that cannot be handled by user intervention.</summary>
		// Token: 0x04000D54 RID: 3412
		Analytical = 2,
		/// <summary>Events in a debug type event log are used solely by developers to diagnose a problem for debugging.</summary>
		// Token: 0x04000D55 RID: 3413
		Debug,
		/// <summary>Events in an operational type event log are used for analyzing and diagnosing a problem or occurrence. They can be used to trigger tools or tasks based on the problem or occurrence. An example of an operational event is an event that occurs when a printer is added or removed from a system.</summary>
		// Token: 0x04000D56 RID: 3414
		Operational = 1
	}
}
