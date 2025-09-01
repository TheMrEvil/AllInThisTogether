using System;

namespace System.Reflection
{
	/// <summary>Identifies the nature of the code in an executable file.</summary>
	// Token: 0x020008B9 RID: 2233
	[Flags]
	public enum PortableExecutableKinds
	{
		/// <summary>The file is not in portable executable (PE) file format.</summary>
		// Token: 0x04002F10 RID: 12048
		NotAPortableExecutableImage = 0,
		/// <summary>The executable contains only Microsoft intermediate language (MSIL), and is therefore neutral with respect to 32-bit or 64-bit platforms.</summary>
		// Token: 0x04002F11 RID: 12049
		ILOnly = 1,
		/// <summary>The executable can be run on a 32-bit platform, or in the 32-bit Windows on Windows (WOW) environment on a 64-bit platform.</summary>
		// Token: 0x04002F12 RID: 12050
		Required32Bit = 2,
		/// <summary>The executable requires a 64-bit platform.</summary>
		// Token: 0x04002F13 RID: 12051
		PE32Plus = 4,
		/// <summary>The executable contains pure unmanaged code.</summary>
		// Token: 0x04002F14 RID: 12052
		Unmanaged32Bit = 8,
		/// <summary>The executable is platform-agnostic but should be run on a 32-bit platform whenever possible.</summary>
		// Token: 0x04002F15 RID: 12053
		Preferred32Bit = 16
	}
}
