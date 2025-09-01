using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Defines the attributes of an implemented or inherited interface of a type.</summary>
	// Token: 0x020007B5 RID: 1973
	[Flags]
	[Serializable]
	public enum IMPLTYPEFLAGS
	{
		/// <summary>The interface or dispinterface represents the default for the source or sink.</summary>
		// Token: 0x04002C7B RID: 11387
		IMPLTYPEFLAG_FDEFAULT = 1,
		/// <summary>This member of a coclass is called rather than implemented.</summary>
		// Token: 0x04002C7C RID: 11388
		IMPLTYPEFLAG_FSOURCE = 2,
		/// <summary>The member should not be displayed or programmable by users.</summary>
		// Token: 0x04002C7D RID: 11389
		IMPLTYPEFLAG_FRESTRICTED = 4,
		/// <summary>Sinks receive events through the virtual function table (VTBL).</summary>
		// Token: 0x04002C7E RID: 11390
		IMPLTYPEFLAG_FDEFAULTVTABLE = 8
	}
}
