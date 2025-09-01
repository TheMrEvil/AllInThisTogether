using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Provides display instructions for the debugger.</summary>
	// Token: 0x020009BA RID: 2490
	[ComVisible(true)]
	public enum DebuggerBrowsableState
	{
		/// <summary>Never show the element.</summary>
		// Token: 0x0400377C RID: 14204
		Never,
		/// <summary>Show the element as collapsed.</summary>
		// Token: 0x0400377D RID: 14205
		Collapsed = 2,
		/// <summary>Do not display the root element; display the child elements if the element is a collection or array of items.</summary>
		// Token: 0x0400377E RID: 14206
		RootHidden
	}
}
