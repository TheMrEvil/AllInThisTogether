using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Specifies the type of user interface (UI) the trust manager should use for trust decisions.</summary>
	// Token: 0x02000423 RID: 1059
	[ComVisible(true)]
	public enum TrustManagerUIContext
	{
		/// <summary>An Install UI.</summary>
		// Token: 0x04001FC3 RID: 8131
		Install,
		/// <summary>An Upgrade UI.</summary>
		// Token: 0x04001FC4 RID: 8132
		Upgrade,
		/// <summary>A Run UI.</summary>
		// Token: 0x04001FC5 RID: 8133
		Run
	}
}
