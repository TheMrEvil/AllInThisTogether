using System;

namespace System.Linq
{
	/// <summary>Specifies the preferred type of output merge to use in a query. In other words, it indicates how PLINQ should merge the results from the various partitions back into a single result sequence. This is a hint only, and may not be respected by the system when parallelizing all queries.</summary>
	// Token: 0x0200008A RID: 138
	public enum ParallelMergeOptions
	{
		/// <summary>Use the default merge type, which is AutoBuffered.</summary>
		// Token: 0x040003C6 RID: 966
		Default,
		/// <summary>Use a merge without output buffers. As soon as result elements have been computed, make that element available to the consumer of the query.</summary>
		// Token: 0x040003C7 RID: 967
		NotBuffered,
		/// <summary>Use a merge with output buffers of a size chosen by the system. Results will accumulate into an output buffer before they are available to the consumer of the query.</summary>
		// Token: 0x040003C8 RID: 968
		AutoBuffered,
		/// <summary>Use a merge with full output buffers. The system will accumulate all of the results before making any of them available to the consumer of the query.</summary>
		// Token: 0x040003C9 RID: 969
		FullyBuffered
	}
}
