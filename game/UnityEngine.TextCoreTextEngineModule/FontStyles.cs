using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200002D RID: 45
	[Flags]
	public enum FontStyles
	{
		// Token: 0x04000207 RID: 519
		Normal = 0,
		// Token: 0x04000208 RID: 520
		Bold = 1,
		// Token: 0x04000209 RID: 521
		Italic = 2,
		// Token: 0x0400020A RID: 522
		Underline = 4,
		// Token: 0x0400020B RID: 523
		LowerCase = 8,
		// Token: 0x0400020C RID: 524
		UpperCase = 16,
		// Token: 0x0400020D RID: 525
		SmallCaps = 32,
		// Token: 0x0400020E RID: 526
		Strikethrough = 64,
		// Token: 0x0400020F RID: 527
		Superscript = 128,
		// Token: 0x04000210 RID: 528
		Subscript = 256,
		// Token: 0x04000211 RID: 529
		Highlight = 512
	}
}
