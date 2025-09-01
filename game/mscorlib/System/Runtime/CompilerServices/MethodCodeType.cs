using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Defines how a method is implemented.</summary>
	// Token: 0x02000844 RID: 2116
	[ComVisible(true)]
	[Serializable]
	public enum MethodCodeType
	{
		/// <summary>Specifies that the method implementation is in Microsoft intermediate language (MSIL).</summary>
		// Token: 0x04002D89 RID: 11657
		IL,
		/// <summary>Specifies that the method is implemented in native code.</summary>
		// Token: 0x04002D8A RID: 11658
		Native,
		/// <summary>Specifies that the method implementation is in optimized intermediate language (OPTIL).</summary>
		// Token: 0x04002D8B RID: 11659
		OPTIL,
		/// <summary>Specifies that the method implementation is provided by the runtime.</summary>
		// Token: 0x04002D8C RID: 11660
		Runtime
	}
}
