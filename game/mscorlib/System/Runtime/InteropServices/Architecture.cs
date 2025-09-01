using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates the processor architecture.</summary>
	// Token: 0x020006CB RID: 1739
	public enum Architecture
	{
		/// <summary>An Intel-based 32-bit processor architecture.</summary>
		// Token: 0x04002A00 RID: 10752
		X86,
		/// <summary>An Intel-based 64-bit processor architecture.</summary>
		// Token: 0x04002A01 RID: 10753
		X64,
		/// <summary>A 32-bit ARM processor architecture.</summary>
		// Token: 0x04002A02 RID: 10754
		Arm,
		/// <summary>A 64-bit ARM processor architecture.</summary>
		// Token: 0x04002A03 RID: 10755
		Arm64
	}
}
