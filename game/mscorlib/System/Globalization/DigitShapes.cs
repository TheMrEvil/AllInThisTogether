using System;

namespace System.Globalization
{
	/// <summary>Specifies the culture-specific display of digits.</summary>
	// Token: 0x02000964 RID: 2404
	public enum DigitShapes
	{
		/// <summary>The digit shape depends on the previous text in the same output. European digits follow Latin scripts; Arabic-Indic digits follow Arabic text; and Thai digits follow Thai text.</summary>
		// Token: 0x0400348B RID: 13451
		Context,
		/// <summary>The digit shape is not changed. Full Unicode compatibility is maintained.</summary>
		// Token: 0x0400348C RID: 13452
		None,
		/// <summary>The digit shape is the native equivalent of the digits from 0 through 9. ASCII digits from 0 through 9 are replaced by equivalent native national digits.</summary>
		// Token: 0x0400348D RID: 13453
		NativeNational
	}
}
