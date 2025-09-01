using System;

namespace System.Diagnostics
{
	/// <summary>Specifies the lifetime of a performance counter instance.</summary>
	// Token: 0x02000274 RID: 628
	public enum PerformanceCounterInstanceLifetime
	{
		/// <summary>Remove the performance counter instance when no counters are using the process category.</summary>
		// Token: 0x04000B21 RID: 2849
		Global,
		/// <summary>Remove the performance counter instance when the process is closed.</summary>
		// Token: 0x04000B22 RID: 2850
		Process
	}
}
