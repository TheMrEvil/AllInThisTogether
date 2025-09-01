using System;

namespace System
{
	/// <summary>Specifies whether relevant <see cref="Overload:System.Convert.ToBase64CharArray" /> and <see cref="Overload:System.Convert.ToBase64String" /> methods insert line breaks in their output.</summary>
	// Token: 0x02000108 RID: 264
	[Flags]
	public enum Base64FormattingOptions
	{
		/// <summary>Does not insert line breaks after every 76 characters in the string representation.</summary>
		// Token: 0x0400108C RID: 4236
		None = 0,
		/// <summary>Inserts line breaks after every 76 characters in the string representation.</summary>
		// Token: 0x0400108D RID: 4237
		InsertLineBreaks = 1
	}
}
