using System;

namespace System.CodeDom.Compiler
{
	/// <summary>Defines identifiers that indicate special features of a language.</summary>
	// Token: 0x02000354 RID: 852
	[Flags]
	public enum LanguageOptions
	{
		/// <summary>The language has default characteristics.</summary>
		// Token: 0x04000E6C RID: 3692
		None = 0,
		/// <summary>The language is case-insensitive.</summary>
		// Token: 0x04000E6D RID: 3693
		CaseInsensitive = 1
	}
}
