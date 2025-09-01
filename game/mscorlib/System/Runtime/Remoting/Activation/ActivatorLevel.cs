using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Activation
{
	/// <summary>Defines the appropriate position for a <see cref="T:System.Activator" /> in the chain of activators.</summary>
	// Token: 0x020005CC RID: 1484
	[ComVisible(true)]
	[Serializable]
	public enum ActivatorLevel
	{
		/// <summary>Constructs a blank object and runs the constructor.</summary>
		// Token: 0x040025F4 RID: 9716
		Construction = 4,
		/// <summary>Finds or creates a suitable context.</summary>
		// Token: 0x040025F5 RID: 9717
		Context = 8,
		/// <summary>Finds or creates a <see cref="T:System.AppDomain" />.</summary>
		// Token: 0x040025F6 RID: 9718
		AppDomain = 12,
		/// <summary>Starts a process.</summary>
		// Token: 0x040025F7 RID: 9719
		Process = 16,
		/// <summary>Finds a suitable computer.</summary>
		// Token: 0x040025F8 RID: 9720
		Machine = 20
	}
}
