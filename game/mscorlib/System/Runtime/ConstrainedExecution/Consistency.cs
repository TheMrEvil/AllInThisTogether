using System;

namespace System.Runtime.ConstrainedExecution
{
	/// <summary>Specifies a reliability contract.</summary>
	// Token: 0x020007D4 RID: 2004
	public enum Consistency
	{
		/// <summary>In the face of exceptional conditions, the CLR makes no guarantees regarding state consistency; that is, the condition might corrupt the process.</summary>
		// Token: 0x04002D1B RID: 11547
		MayCorruptProcess,
		/// <summary>In the face of exceptional conditions, the common language runtime (CLR) makes no guarantees regarding state consistency in the current application domain.</summary>
		// Token: 0x04002D1C RID: 11548
		MayCorruptAppDomain,
		/// <summary>In the face of exceptional conditions, the method is guaranteed to limit state corruption to the current instance.</summary>
		// Token: 0x04002D1D RID: 11549
		MayCorruptInstance,
		/// <summary>In the face of exceptional conditions, the method is guaranteed not to corrupt state.</summary>
		// Token: 0x04002D1E RID: 11550
		WillNotCorruptState
	}
}
