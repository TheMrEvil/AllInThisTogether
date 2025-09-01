using System;

namespace System
{
	/// <summary>Provides information about the current registration for notification of the next full garbage collection.</summary>
	// Token: 0x020001FE RID: 510
	[Serializable]
	public enum GCNotificationStatus
	{
		/// <summary>The notification was successful and the registration was not canceled.</summary>
		// Token: 0x04001540 RID: 5440
		Succeeded,
		/// <summary>The notification failed for any reason.</summary>
		// Token: 0x04001541 RID: 5441
		Failed,
		/// <summary>The current registration was canceled by the user.</summary>
		// Token: 0x04001542 RID: 5442
		Canceled,
		/// <summary>The time specified by the <paramref name="millisecondsTimeout" /> parameter for either <see cref="M:System.GC.WaitForFullGCApproach(System.Int32)" /> or <see cref="M:System.GC.WaitForFullGCComplete(System.Int32)" /> has elapsed.</summary>
		// Token: 0x04001543 RID: 5443
		Timeout,
		/// <summary>This result can be caused by the following: there is no current registration for a garbage collection notification, concurrent garbage collection is enabled, or the time specified for the <paramref name="millisecondsTimeout" /> parameter has expired and no garbage collection notification was obtained. (See the &lt;gcConcurrent&gt; runtime setting for information about how to disable concurrent garbage collection.)</summary>
		// Token: 0x04001544 RID: 5444
		NotApplicable
	}
}
