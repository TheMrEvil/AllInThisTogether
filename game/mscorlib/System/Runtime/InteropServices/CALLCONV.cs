using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.CALLCONV" /> instead.</summary>
	// Token: 0x02000733 RID: 1843
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.CALLCONV instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	public enum CALLCONV
	{
		/// <summary>Indicates that the Cdecl calling convention is used for a method.</summary>
		// Token: 0x04002B83 RID: 11139
		CC_CDECL = 1,
		/// <summary>Indicates that the Mscpascal calling convention is used for a method.</summary>
		// Token: 0x04002B84 RID: 11140
		CC_MSCPASCAL,
		/// <summary>Indicates that the Pascal calling convention is used for a method.</summary>
		// Token: 0x04002B85 RID: 11141
		CC_PASCAL = 2,
		/// <summary>Indicates that the Macpascal calling convention is used for a method.</summary>
		// Token: 0x04002B86 RID: 11142
		CC_MACPASCAL,
		/// <summary>Indicates that the Stdcall calling convention is used for a method.</summary>
		// Token: 0x04002B87 RID: 11143
		CC_STDCALL,
		/// <summary>This value is reserved for future use.</summary>
		// Token: 0x04002B88 RID: 11144
		CC_RESERVED,
		/// <summary>Indicates that the Syscall calling convention is used for a method.</summary>
		// Token: 0x04002B89 RID: 11145
		CC_SYSCALL,
		/// <summary>Indicates that the Mpwcdecl calling convention is used for a method.</summary>
		// Token: 0x04002B8A RID: 11146
		CC_MPWCDECL,
		/// <summary>Indicates that the Mpwpascal calling convention is used for a method.</summary>
		// Token: 0x04002B8B RID: 11147
		CC_MPWPASCAL,
		/// <summary>Indicates the end of the <see cref="T:System.Runtime.InteropServices.CALLCONV" /> enumeration.</summary>
		// Token: 0x04002B8C RID: 11148
		CC_MAX
	}
}
