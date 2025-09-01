using System;

namespace System.Data.SqlTypes
{
	/// <summary>Specifies the compare option values for a <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</summary>
	// Token: 0x02000316 RID: 790
	[Flags]
	public enum SqlCompareOptions
	{
		/// <summary>Specifies the default option settings for <see cref="T:System.Data.SqlTypes.SqlString" /> comparisons.</summary>
		// Token: 0x040018DB RID: 6363
		None = 0,
		/// <summary>Specifies that <see cref="T:System.Data.SqlTypes.SqlString" /> comparisons must ignore case.</summary>
		// Token: 0x040018DC RID: 6364
		IgnoreCase = 1,
		/// <summary>Specifies that <see cref="T:System.Data.SqlTypes.SqlString" /> comparisons must ignore nonspace combining characters, such as diacritics. The Unicode Standard defines combining characters as characters that are combined with base characters to produce a new character. Non-space combining characters do not use character space by themselves when rendered. For more information about non-space combining characters, see the Unicode Standard at https://www.unicode.org.</summary>
		// Token: 0x040018DD RID: 6365
		IgnoreNonSpace = 2,
		/// <summary>Specifies that <see cref="T:System.Data.SqlTypes.SqlString" /> comparisons must ignore the Kana type. Kana type refers to Japanese hiragana and katakana characters that represent phonetic sounds in the Japanese language. Hiragana is used for native Japanese expressions and words, while katakana is used for words borrowed from other languages, such as "computer" or "Internet". A phonetic sound can be expressed in both hiragana and katakana. If this value is selected, the hiragana character for one sound is considered equal to the katakana character for the same sound.</summary>
		// Token: 0x040018DE RID: 6366
		IgnoreKanaType = 8,
		/// <summary>Specifies that <see cref="T:System.Data.SqlTypes.SqlString" /> comparisons must ignore the character width. For example, Japanese katakana characters can be written as full-width or half-width and, if this value is selected, the katakana characters written as full-width are considered equal to the same characters written in half-width.</summary>
		// Token: 0x040018DF RID: 6367
		IgnoreWidth = 16,
		/// <summary>Specifies that sorts should be based on a characters numeric value instead of its alphabetical value.</summary>
		// Token: 0x040018E0 RID: 6368
		BinarySort = 32768,
		/// <summary>Performs a binary sort.</summary>
		// Token: 0x040018E1 RID: 6369
		BinarySort2 = 16384
	}
}
