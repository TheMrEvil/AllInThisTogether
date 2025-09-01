using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Defines how well-known objects are activated.</summary>
	// Token: 0x02000579 RID: 1401
	[ComVisible(true)]
	[Serializable]
	public enum WellKnownObjectMode
	{
		/// <summary>Every incoming message is serviced by the same object instance.</summary>
		// Token: 0x04002571 RID: 9585
		Singleton = 1,
		/// <summary>Every incoming message is serviced by a new object instance.</summary>
		// Token: 0x04002572 RID: 9586
		SingleCall
	}
}
