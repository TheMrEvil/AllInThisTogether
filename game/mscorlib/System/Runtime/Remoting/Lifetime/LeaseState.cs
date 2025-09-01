using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime
{
	/// <summary>Indicates the possible lease states of a lifetime lease.</summary>
	// Token: 0x02000589 RID: 1417
	[ComVisible(true)]
	[Serializable]
	public enum LeaseState
	{
		/// <summary>The lease is not initialized.</summary>
		// Token: 0x04002595 RID: 9621
		Null,
		/// <summary>The lease has been created, but is not yet active.</summary>
		// Token: 0x04002596 RID: 9622
		Initial,
		/// <summary>The lease is active and has not expired.</summary>
		// Token: 0x04002597 RID: 9623
		Active,
		/// <summary>The lease has expired and is seeking sponsorship.</summary>
		// Token: 0x04002598 RID: 9624
		Renewing,
		/// <summary>The lease has expired and cannot be renewed.</summary>
		// Token: 0x04002599 RID: 9625
		Expired
	}
}
