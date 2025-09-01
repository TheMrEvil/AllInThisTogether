using System;

namespace System.Runtime
{
	/// <summary>Indicates whether the next blocking garbage collection compacts the large object heap (LOH).</summary>
	// Token: 0x02000550 RID: 1360
	public enum GCLargeObjectHeapCompactionMode
	{
		/// <summary>The large object heap (LOH) is not compacted.</summary>
		// Token: 0x04002506 RID: 9478
		Default = 1,
		/// <summary>The large object heap (LOH) will be compacted during the next blocking generation 2 garbage collection.</summary>
		// Token: 0x04002507 RID: 9479
		CompactOnce
	}
}
