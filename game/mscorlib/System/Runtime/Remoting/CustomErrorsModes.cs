using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Specifies how custom errors are handled.</summary>
	// Token: 0x0200055A RID: 1370
	[ComVisible(true)]
	public enum CustomErrorsModes
	{
		/// <summary>All callers receive filtered exception information.</summary>
		// Token: 0x04002517 RID: 9495
		On,
		/// <summary>All callers receive complete exception information.</summary>
		// Token: 0x04002518 RID: 9496
		Off,
		/// <summary>Local callers receive complete exception information; remote callers receive filtered exception information.</summary>
		// Token: 0x04002519 RID: 9497
		RemoteOnly
	}
}
