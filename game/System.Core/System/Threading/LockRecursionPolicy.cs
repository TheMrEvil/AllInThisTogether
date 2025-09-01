using System;

namespace System.Threading
{
	/// <summary>Specifies whether a lock can be entered multiple times by the same thread.</summary>
	// Token: 0x02000362 RID: 866
	public enum LockRecursionPolicy
	{
		/// <summary>If a thread tries to enter a lock recursively, an exception is thrown. Some classes may allow certain recursions when this setting is in effect. </summary>
		// Token: 0x04000CA6 RID: 3238
		NoRecursion,
		/// <summary>A thread can enter a lock recursively. Some classes may restrict this capability. </summary>
		// Token: 0x04000CA7 RID: 3239
		SupportsRecursion
	}
}
