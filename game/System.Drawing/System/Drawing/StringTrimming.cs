using System;

namespace System.Drawing
{
	/// <summary>Specifies how to trim characters from a string that does not completely fit into a layout shape.</summary>
	// Token: 0x0200003E RID: 62
	public enum StringTrimming
	{
		/// <summary>Specifies no trimming.</summary>
		// Token: 0x0400036B RID: 875
		None,
		/// <summary>Specifies that the text is trimmed to the nearest character.</summary>
		// Token: 0x0400036C RID: 876
		Character,
		/// <summary>Specifies that text is trimmed to the nearest word.</summary>
		// Token: 0x0400036D RID: 877
		Word,
		/// <summary>Specifies that the text is trimmed to the nearest character, and an ellipsis is inserted at the end of a trimmed line.</summary>
		// Token: 0x0400036E RID: 878
		EllipsisCharacter,
		/// <summary>Specifies that text is trimmed to the nearest word, and an ellipsis is inserted at the end of a trimmed line.</summary>
		// Token: 0x0400036F RID: 879
		EllipsisWord,
		/// <summary>The center is removed from trimmed lines and replaced by an ellipsis. The algorithm keeps as much of the last slash-delimited segment of the line as possible.</summary>
		// Token: 0x04000370 RID: 880
		EllipsisPath
	}
}
