using System;

namespace System.Threading
{
	/// <summary>Specifies the scheduling priority of a <see cref="T:System.Threading.Thread" />.</summary>
	// Token: 0x02000296 RID: 662
	public enum ThreadPriority
	{
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled after threads with any other priority.</summary>
		// Token: 0x04001A3F RID: 6719
		Lowest,
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled after threads with <see langword="Normal" /> priority and before those with <see langword="Lowest" /> priority.</summary>
		// Token: 0x04001A40 RID: 6720
		BelowNormal,
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled after threads with <see langword="AboveNormal" /> priority and before those with <see langword="BelowNormal" /> priority. Threads have <see langword="Normal" /> priority by default.</summary>
		// Token: 0x04001A41 RID: 6721
		Normal,
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled after threads with <see langword="Highest" /> priority and before those with <see langword="Normal" /> priority.</summary>
		// Token: 0x04001A42 RID: 6722
		AboveNormal,
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled before threads with any other priority.</summary>
		// Token: 0x04001A43 RID: 6723
		Highest
	}
}
