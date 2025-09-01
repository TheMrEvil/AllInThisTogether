using System;

namespace System.Security
{
	/// <summary>Specifies the type of a managed code policy level.</summary>
	// Token: 0x020003CA RID: 970
	public enum PolicyLevelType
	{
		/// <summary>Security policy for all managed code in an application.</summary>
		// Token: 0x04001E8F RID: 7823
		AppDomain = 3,
		/// <summary>Security policy for all managed code in an enterprise.</summary>
		// Token: 0x04001E90 RID: 7824
		Enterprise = 2,
		/// <summary>Security policy for all managed code that is run on the computer.</summary>
		// Token: 0x04001E91 RID: 7825
		Machine = 1,
		/// <summary>Security policy for all managed code that is run by the user.</summary>
		// Token: 0x04001E92 RID: 7826
		User = 0
	}
}
