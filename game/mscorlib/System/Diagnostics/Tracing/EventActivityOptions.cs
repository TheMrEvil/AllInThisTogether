using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Specifies the tracking of activity start and stop events.</summary>
	// Token: 0x020009E3 RID: 2531
	[Flags]
	public enum EventActivityOptions
	{
		/// <summary>Use the default behavior for start and stop tracking.</summary>
		// Token: 0x040037D7 RID: 14295
		None = 0,
		/// <summary>Turn off start and stop tracking.</summary>
		// Token: 0x040037D8 RID: 14296
		Disable = 2,
		/// <summary>Allow recursive activity starts. By default, an activity cannot be recursive. That is, a sequence of Start A, Start A, Stop A, Stop A is not allowed. Unintentional recursive activities can occur if the app executes and for some the stop is not reached before another start is called.</summary>
		// Token: 0x040037D9 RID: 14297
		Recursive = 4,
		/// <summary>Allow overlapping activities. By default, activity starts and stops must be property nested. That is, a sequence of Start A, Start B, Stop A, Stop B is not allowed will result in B stopping at the same time as A.</summary>
		// Token: 0x040037DA RID: 14298
		Detachable = 8
	}
}
