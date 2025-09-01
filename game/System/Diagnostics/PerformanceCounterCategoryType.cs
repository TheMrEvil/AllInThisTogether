using System;

namespace System.Diagnostics
{
	/// <summary>Indicates whether the performance counter category can have multiple instances.</summary>
	// Token: 0x02000272 RID: 626
	public enum PerformanceCounterCategoryType
	{
		/// <summary>The performance counter category can have only a single instance.</summary>
		// Token: 0x04000B1D RID: 2845
		SingleInstance,
		/// <summary>The performance counter category can have multiple instances.</summary>
		// Token: 0x04000B1E RID: 2846
		MultiInstance,
		/// <summary>The instance functionality for the performance counter category is unknown.</summary>
		// Token: 0x04000B1F RID: 2847
		Unknown = -1
	}
}
