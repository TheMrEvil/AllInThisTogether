using System;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Defines the standard keywords that are attached to events by the event provider. For more information about keywords, see <see cref="T:System.Diagnostics.Eventing.Reader.EventKeyword" />.</summary>
	// Token: 0x020003B5 RID: 949
	[Flags]
	public enum StandardEventKeywords : long
	{
		/// <summary>Attached to all failed security audit events. This keyword should only be used for events in the Security log.</summary>
		// Token: 0x04000D58 RID: 3416
		AuditFailure = 4503599627370496L,
		/// <summary>Attached to all successful security audit events. This keyword should only be used for events in the Security log.</summary>
		// Token: 0x04000D59 RID: 3417
		AuditSuccess = 9007199254740992L,
		/// <summary>Attached to transfer events where the related Activity ID (Correlation ID) is a computed value and is not guaranteed to be unique (not a real GUID).</summary>
		// Token: 0x04000D5A RID: 3418
		[Obsolete("Incorrect value: use CorrelationHint2 instead", false)]
		CorrelationHint = 4503599627370496L,
		/// <summary>Attached to transfer events where the related Activity ID (Correlation ID) is a computed value and is not guaranteed to be unique (not a real GUID).</summary>
		// Token: 0x04000D5B RID: 3419
		CorrelationHint2 = 18014398509481984L,
		/// <summary>Attached to events which are raised using the RaiseEvent function.</summary>
		// Token: 0x04000D5C RID: 3420
		EventLogClassic = 36028797018963968L,
		/// <summary>This value indicates that no filtering on keyword is performed when the event is published.</summary>
		// Token: 0x04000D5D RID: 3421
		None = 0L,
		/// <summary>Attached to all response time events. </summary>
		// Token: 0x04000D5E RID: 3422
		ResponseTime = 281474976710656L,
		/// <summary>Attached to all Service Quality Mechanism (SQM) events.</summary>
		// Token: 0x04000D5F RID: 3423
		Sqm = 2251799813685248L,
		/// <summary>Attached to all Windows Diagnostic Infrastructure (WDI) context events.</summary>
		// Token: 0x04000D60 RID: 3424
		WdiContext = 562949953421312L,
		/// <summary>Attached to all Windows Diagnostic Infrastructure (WDI) diagnostic events.</summary>
		// Token: 0x04000D61 RID: 3425
		WdiDiagnostic = 1125899906842624L
	}
}
