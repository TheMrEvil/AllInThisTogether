using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Defines the standard keywords that apply to events.</summary>
	// Token: 0x020009EB RID: 2539
	[Flags]
	public enum EventKeywords : long
	{
		/// <summary>No filtering on keywords is performed when the event is published.</summary>
		// Token: 0x04003804 RID: 14340
		None = 0L,
		/// <summary>All the bits are set to 1, representing every possible group of events.</summary>
		// Token: 0x04003805 RID: 14341
		All = -1L,
		/// <summary>Attached to all Microsoft telemetry events.</summary>
		// Token: 0x04003806 RID: 14342
		MicrosoftTelemetry = 562949953421312L,
		/// <summary>Attached to all Windows Diagnostics Infrastructure (WDI) context events.</summary>
		// Token: 0x04003807 RID: 14343
		WdiContext = 562949953421312L,
		/// <summary>Attached to all Windows Diagnostics Infrastructure (WDI) diagnostic events.</summary>
		// Token: 0x04003808 RID: 14344
		WdiDiagnostic = 1125899906842624L,
		/// <summary>Attached to all Service Quality Mechanism (SQM) events.</summary>
		// Token: 0x04003809 RID: 14345
		Sqm = 2251799813685248L,
		/// <summary>Attached to all failed security audit events. Use this keyword only  for events in the security log.</summary>
		// Token: 0x0400380A RID: 14346
		AuditFailure = 4503599627370496L,
		/// <summary>Attached to all successful security audit events. Use this keyword only for events in the security log.</summary>
		// Token: 0x0400380B RID: 14347
		AuditSuccess = 9007199254740992L,
		/// <summary>Attached to transfer events where the related activity ID (correlation ID) is a computed value and is not guaranteed to be unique (that is, it is not a real GUID).</summary>
		// Token: 0x0400380C RID: 14348
		CorrelationHint = 4503599627370496L,
		/// <summary>Attached to events that are raised by using the <see langword="RaiseEvent" /> function.</summary>
		// Token: 0x0400380D RID: 14349
		EventLogClassic = 36028797018963968L
	}
}
