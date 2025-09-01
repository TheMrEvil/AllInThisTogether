using System;

namespace System.Reflection
{
	/// <summary>Identifies the processor and bits-per-word of the platform targeted by an executable.</summary>
	// Token: 0x020008BA RID: 2234
	public enum ProcessorArchitecture
	{
		/// <summary>An unknown or unspecified combination of processor and bits-per-word.</summary>
		// Token: 0x04002F17 RID: 12055
		None,
		/// <summary>Neutral with respect to processor and bits-per-word.</summary>
		// Token: 0x04002F18 RID: 12056
		MSIL,
		/// <summary>A 32-bit Intel processor, either native or in the Windows on Windows environment on a 64-bit platform (WOW64).</summary>
		// Token: 0x04002F19 RID: 12057
		X86,
		/// <summary>A 64-bit Intel Itanium processor only.</summary>
		// Token: 0x04002F1A RID: 12058
		IA64,
		/// <summary>A 64-bit processor based on the x64 architecture.</summary>
		// Token: 0x04002F1B RID: 12059
		Amd64,
		/// <summary>An ARM processor.</summary>
		// Token: 0x04002F1C RID: 12060
		Arm
	}
}
