using System;

namespace System
{
	/// <summary>Specifies the behavior for a forced garbage collection.</summary>
	// Token: 0x020001FC RID: 508
	[Serializable]
	public enum GCCollectionMode
	{
		/// <summary>The default setting for this enumeration, which is currently <see cref="F:System.GCCollectionMode.Forced" />.</summary>
		// Token: 0x04001537 RID: 5431
		Default,
		/// <summary>Forces the garbage collection to occur immediately.</summary>
		// Token: 0x04001538 RID: 5432
		Forced,
		/// <summary>Allows the garbage collector to determine whether the current time is optimal to reclaim objects.</summary>
		// Token: 0x04001539 RID: 5433
		Optimized
	}
}
