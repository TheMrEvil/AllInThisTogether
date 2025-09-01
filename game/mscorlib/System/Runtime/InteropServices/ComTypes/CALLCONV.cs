using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Identifies the calling convention used by a method described in a METHODDATA structure.</summary>
	// Token: 0x020007C6 RID: 1990
	[Serializable]
	public enum CALLCONV
	{
		/// <summary>Indicates that the C declaration (CDECL) calling convention is used for a method.</summary>
		// Token: 0x04002CDD RID: 11485
		CC_CDECL = 1,
		/// <summary>Indicates that the MSC Pascal (MSCPASCAL) calling convention is used for a method.</summary>
		// Token: 0x04002CDE RID: 11486
		CC_MSCPASCAL,
		/// <summary>Indicates that the Pascal calling convention is used for a method.</summary>
		// Token: 0x04002CDF RID: 11487
		CC_PASCAL = 2,
		/// <summary>Indicates that the Macintosh Pascal (MACPASCAL) calling convention is used for a method.</summary>
		// Token: 0x04002CE0 RID: 11488
		CC_MACPASCAL,
		/// <summary>Indicates that the standard calling convention (STDCALL) is used for a method.</summary>
		// Token: 0x04002CE1 RID: 11489
		CC_STDCALL,
		/// <summary>This value is reserved for future use.</summary>
		// Token: 0x04002CE2 RID: 11490
		CC_RESERVED,
		/// <summary>Indicates that the standard SYSCALL calling convention is used for a method.</summary>
		// Token: 0x04002CE3 RID: 11491
		CC_SYSCALL,
		/// <summary>Indicates that the Macintosh Programmers' Workbench (MPW) CDECL calling convention is used for a method.</summary>
		// Token: 0x04002CE4 RID: 11492
		CC_MPWCDECL,
		/// <summary>Indicates that the Macintosh Programmers' Workbench (MPW) PASCAL calling convention is used for a method.</summary>
		// Token: 0x04002CE5 RID: 11493
		CC_MPWPASCAL,
		/// <summary>Indicates the end of the <see cref="T:System.Runtime.InteropServices.ComTypes.CALLCONV" /> enumeration.</summary>
		// Token: 0x04002CE6 RID: 11494
		CC_MAX
	}
}
