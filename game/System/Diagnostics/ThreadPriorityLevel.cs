using System;

namespace System.Diagnostics
{
	/// <summary>Specifies the priority level of a thread.</summary>
	// Token: 0x02000281 RID: 641
	public enum ThreadPriorityLevel
	{
		/// <summary>Specifies one step above the normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x04000B68 RID: 2920
		AboveNormal = 1,
		/// <summary>Specifies one step below the normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x04000B69 RID: 2921
		BelowNormal = -1,
		/// <summary>Specifies highest priority. This is two steps above the normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x04000B6A RID: 2922
		Highest = 2,
		/// <summary>Specifies idle priority. This is the lowest possible priority value of all threads, independent of the value of the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x04000B6B RID: 2923
		Idle = -15,
		/// <summary>Specifies lowest priority. This is two steps below the normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x04000B6C RID: 2924
		Lowest = -2,
		/// <summary>Specifies normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x04000B6D RID: 2925
		Normal = 0,
		/// <summary>Specifies time-critical priority. This is the highest priority of all threads, independent of the value of the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x04000B6E RID: 2926
		TimeCritical = 15
	}
}
