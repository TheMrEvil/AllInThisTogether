using System;

namespace System.ComponentModel.Composition
{
	/// <summary>Specifies values that indicate how the MEF composition engine searches for imports.</summary>
	// Token: 0x02000048 RID: 72
	public enum ImportSource
	{
		/// <summary>Imports may be satisfied from the current scope or any ancestor scope.</summary>
		// Token: 0x040000D2 RID: 210
		Any,
		/// <summary>Imports may be satisfied only from the current scope.</summary>
		// Token: 0x040000D3 RID: 211
		Local,
		/// <summary>Imports may be satisfied only from an ancestor scope.</summary>
		// Token: 0x040000D4 RID: 212
		NonLocal
	}
}
