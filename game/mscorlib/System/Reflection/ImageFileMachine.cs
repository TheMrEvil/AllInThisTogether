using System;

namespace System.Reflection
{
	/// <summary>Identifies the platform targeted by an executable.</summary>
	// Token: 0x020008A4 RID: 2212
	public enum ImageFileMachine
	{
		/// <summary>Targets a 32-bit Intel processor.</summary>
		// Token: 0x04002EAF RID: 11951
		I386 = 332,
		/// <summary>Targets a 64-bit Intel processor.</summary>
		// Token: 0x04002EB0 RID: 11952
		IA64 = 512,
		/// <summary>Targets a 64-bit AMD processor.</summary>
		// Token: 0x04002EB1 RID: 11953
		AMD64 = 34404,
		/// <summary>Targets an ARM processor.</summary>
		// Token: 0x04002EB2 RID: 11954
		ARM = 452
	}
}
