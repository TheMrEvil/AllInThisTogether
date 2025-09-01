using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.DESCKIND" /> instead.</summary>
	// Token: 0x0200073B RID: 1851
	[Obsolete]
	[Serializable]
	public enum DESCKIND
	{
		/// <summary>Indicates that no match was found.</summary>
		// Token: 0x04002BB4 RID: 11188
		DESCKIND_NONE,
		/// <summary>Indicates that a <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> was returned.</summary>
		// Token: 0x04002BB5 RID: 11189
		DESCKIND_FUNCDESC,
		/// <summary>Indicates that a <see langword="VARDESC" /> was returned.</summary>
		// Token: 0x04002BB6 RID: 11190
		DESCKIND_VARDESC,
		/// <summary>Indicates that a <see langword="TYPECOMP" /> was returned.</summary>
		// Token: 0x04002BB7 RID: 11191
		DESCKIND_TYPECOMP,
		/// <summary>Indicates that an <see langword="IMPLICITAPPOBJ" /> was returned.</summary>
		// Token: 0x04002BB8 RID: 11192
		DESCKIND_IMPLICITAPPOBJ,
		/// <summary>Indicates an end of enumeration marker.</summary>
		// Token: 0x04002BB9 RID: 11193
		DESCKIND_MAX
	}
}
