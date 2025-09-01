using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Defines how to access a function.</summary>
	// Token: 0x020007C4 RID: 1988
	[Serializable]
	public enum FUNCKIND
	{
		/// <summary>The function is accessed in the same way as <see cref="F:System.Runtime.InteropServices.FUNCKIND.FUNC_PUREVIRTUAL" />, except the function has an implementation.</summary>
		// Token: 0x04002CD2 RID: 11474
		FUNC_VIRTUAL,
		/// <summary>The function is accessed through the virtual function table (VTBL), and takes an implicit <see langword="this" /> pointer.</summary>
		// Token: 0x04002CD3 RID: 11475
		FUNC_PUREVIRTUAL,
		/// <summary>The function is accessed by <see langword="static" /> address and takes an implicit <see langword="this" /> pointer.</summary>
		// Token: 0x04002CD4 RID: 11476
		FUNC_NONVIRTUAL,
		/// <summary>The function is accessed by <see langword="static" /> address and does not take an implicit <see langword="this" /> pointer.</summary>
		// Token: 0x04002CD5 RID: 11477
		FUNC_STATIC,
		/// <summary>The function can be accessed only through <see langword="IDispatch" />.</summary>
		// Token: 0x04002CD6 RID: 11478
		FUNC_DISPATCH
	}
}
