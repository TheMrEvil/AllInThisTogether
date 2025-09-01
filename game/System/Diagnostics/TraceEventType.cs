using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Identifies the type of event that has caused the trace.</summary>
	// Token: 0x0200022D RID: 557
	public enum TraceEventType
	{
		/// <summary>Fatal error or application crash.</summary>
		// Token: 0x040009D9 RID: 2521
		Critical = 1,
		/// <summary>Recoverable error.</summary>
		// Token: 0x040009DA RID: 2522
		Error,
		/// <summary>Noncritical problem.</summary>
		// Token: 0x040009DB RID: 2523
		Warning = 4,
		/// <summary>Informational message.</summary>
		// Token: 0x040009DC RID: 2524
		Information = 8,
		/// <summary>Debugging trace.</summary>
		// Token: 0x040009DD RID: 2525
		Verbose = 16,
		/// <summary>Starting of a logical operation.</summary>
		// Token: 0x040009DE RID: 2526
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Start = 256,
		/// <summary>Stopping of a logical operation.</summary>
		// Token: 0x040009DF RID: 2527
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Stop = 512,
		/// <summary>Suspension of a logical operation.</summary>
		// Token: 0x040009E0 RID: 2528
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Suspend = 1024,
		/// <summary>Resumption of a logical operation.</summary>
		// Token: 0x040009E1 RID: 2529
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Resume = 2048,
		/// <summary>Changing of correlation identity.</summary>
		// Token: 0x040009E2 RID: 2530
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Transfer = 4096
	}
}
