using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Specifies various types of data and functions.</summary>
	// Token: 0x020007B3 RID: 1971
	[Serializable]
	public enum TYPEKIND
	{
		/// <summary>A set of enumerators.</summary>
		// Token: 0x04002C61 RID: 11361
		TKIND_ENUM,
		/// <summary>A structure with no methods.</summary>
		// Token: 0x04002C62 RID: 11362
		TKIND_RECORD,
		/// <summary>A module that can have only static functions and data (for example, a DLL).</summary>
		// Token: 0x04002C63 RID: 11363
		TKIND_MODULE,
		/// <summary>A type that has virtual functions, all of which are pure.</summary>
		// Token: 0x04002C64 RID: 11364
		TKIND_INTERFACE,
		/// <summary>A set of methods and properties that are accessible through <see langword="IDispatch::Invoke" />. By default, dual interfaces return <see langword="TKIND_DISPATCH" />.</summary>
		// Token: 0x04002C65 RID: 11365
		TKIND_DISPATCH,
		/// <summary>A set of implemented components interfaces.</summary>
		// Token: 0x04002C66 RID: 11366
		TKIND_COCLASS,
		/// <summary>A type that is an alias for another type.</summary>
		// Token: 0x04002C67 RID: 11367
		TKIND_ALIAS,
		/// <summary>A union of all members that have an offset of zero.</summary>
		// Token: 0x04002C68 RID: 11368
		TKIND_UNION,
		/// <summary>End-of-enumeration marker.</summary>
		// Token: 0x04002C69 RID: 11369
		TKIND_MAX
	}
}
