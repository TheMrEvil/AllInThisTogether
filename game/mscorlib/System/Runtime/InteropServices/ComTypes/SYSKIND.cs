using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Identifies the target operating system platform.</summary>
	// Token: 0x020007CB RID: 1995
	[Serializable]
	public enum SYSKIND
	{
		/// <summary>The target operating system for the type library is 16-bit Windows systems. By default, data fields are packed.</summary>
		// Token: 0x04002D04 RID: 11524
		SYS_WIN16,
		/// <summary>The target operating system for the type library is 32-bit Windows systems. By default, data fields are naturally aligned (for example, 2-byte integers are aligned on even-byte boundaries; 4-byte integers are aligned on quad-word boundaries, and so on).</summary>
		// Token: 0x04002D05 RID: 11525
		SYS_WIN32,
		/// <summary>The target operating system for the type library is Apple Macintosh. By default, all data fields are aligned on even-byte boundaries.</summary>
		// Token: 0x04002D06 RID: 11526
		SYS_MAC,
		/// <summary>The target operating system for the type library is 64-bit Windows systems.</summary>
		// Token: 0x04002D07 RID: 11527
		SYS_WIN64
	}
}
