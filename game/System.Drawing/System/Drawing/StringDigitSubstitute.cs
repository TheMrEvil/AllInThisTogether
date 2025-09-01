using System;

namespace System.Drawing
{
	/// <summary>The <see cref="T:System.Drawing.StringDigitSubstitute" /> enumeration specifies how to substitute digits in a string according to a user's locale or language.</summary>
	// Token: 0x0200003C RID: 60
	public enum StringDigitSubstitute
	{
		/// <summary>Specifies a user-defined substitution scheme.</summary>
		// Token: 0x0400035C RID: 860
		User,
		/// <summary>Specifies to disable substitutions.</summary>
		// Token: 0x0400035D RID: 861
		None,
		/// <summary>Specifies substitution digits that correspond with the official national language of the user's locale.</summary>
		// Token: 0x0400035E RID: 862
		National,
		/// <summary>Specifies substitution digits that correspond with the user's native script or language, which may be different from the official national language of the user's locale.</summary>
		// Token: 0x0400035F RID: 863
		Traditional
	}
}
