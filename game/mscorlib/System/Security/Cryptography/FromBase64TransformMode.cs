using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Specifies whether white space should be ignored in the base 64 transformation.</summary>
	// Token: 0x0200047D RID: 1149
	[ComVisible(true)]
	[Serializable]
	public enum FromBase64TransformMode
	{
		/// <summary>White space should be ignored.</summary>
		// Token: 0x0400212E RID: 8494
		IgnoreWhiteSpaces,
		/// <summary>White space should not be ignored.</summary>
		// Token: 0x0400212F RID: 8495
		DoNotIgnoreWhiteSpaces
	}
}
