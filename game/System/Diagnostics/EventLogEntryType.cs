using System;

namespace System.Diagnostics
{
	/// <summary>Specifies the event type of an event log entry.</summary>
	// Token: 0x0200025D RID: 605
	public enum EventLogEntryType
	{
		/// <summary>An error event. This indicates a significant problem the user should know about; usually a loss of functionality or data.</summary>
		// Token: 0x04000AC9 RID: 2761
		Error = 1,
		/// <summary>A warning event. This indicates a problem that is not immediately significant, but that may signify conditions that could cause future problems.</summary>
		// Token: 0x04000ACA RID: 2762
		Warning,
		/// <summary>An information event. This indicates a significant, successful operation.</summary>
		// Token: 0x04000ACB RID: 2763
		Information = 4,
		/// <summary>A success audit event. This indicates a security event that occurs when an audited access attempt is successful; for example, logging on successfully.</summary>
		// Token: 0x04000ACC RID: 2764
		SuccessAudit = 8,
		/// <summary>A failure audit event. This indicates a security event that occurs when an audited access attempt fails; for example, a failed attempt to open a file.</summary>
		// Token: 0x04000ACD RID: 2765
		FailureAudit = 16
	}
}
