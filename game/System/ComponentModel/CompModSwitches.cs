using System;
using System.Diagnostics;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200040C RID: 1036
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal static class CompModSwitches
	{
		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x0600217C RID: 8572 RVA: 0x000728B3 File Offset: 0x00070AB3
		public static BooleanSwitch CommonDesignerServices
		{
			get
			{
				if (CompModSwitches.commonDesignerServices == null)
				{
					CompModSwitches.commonDesignerServices = new BooleanSwitch("CommonDesignerServices", "Assert if any common designer service is not found.");
				}
				return CompModSwitches.commonDesignerServices;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x0600217D RID: 8573 RVA: 0x000728DB File Offset: 0x00070ADB
		public static TraceSwitch EventLog
		{
			get
			{
				if (CompModSwitches.eventLog == null)
				{
					CompModSwitches.eventLog = new TraceSwitch("EventLog", "Enable tracing for the EventLog component.");
				}
				return CompModSwitches.eventLog;
			}
		}

		// Token: 0x04001014 RID: 4116
		private static volatile BooleanSwitch commonDesignerServices;

		// Token: 0x04001015 RID: 4117
		private static volatile TraceSwitch eventLog;
	}
}
