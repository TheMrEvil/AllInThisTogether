using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000012 RID: 18
	internal struct LineInfo
	{
		// Token: 0x0400006D RID: 109
		internal int controlCharacterCount;

		// Token: 0x0400006E RID: 110
		public int characterCount;

		// Token: 0x0400006F RID: 111
		public int visibleCharacterCount;

		// Token: 0x04000070 RID: 112
		public int spaceCount;

		// Token: 0x04000071 RID: 113
		public int visibleSpaceCount;

		// Token: 0x04000072 RID: 114
		public int wordCount;

		// Token: 0x04000073 RID: 115
		public int firstCharacterIndex;

		// Token: 0x04000074 RID: 116
		public int firstVisibleCharacterIndex;

		// Token: 0x04000075 RID: 117
		public int lastCharacterIndex;

		// Token: 0x04000076 RID: 118
		public int lastVisibleCharacterIndex;

		// Token: 0x04000077 RID: 119
		public float length;

		// Token: 0x04000078 RID: 120
		public float lineHeight;

		// Token: 0x04000079 RID: 121
		public float ascender;

		// Token: 0x0400007A RID: 122
		public float baseline;

		// Token: 0x0400007B RID: 123
		public float descender;

		// Token: 0x0400007C RID: 124
		public float maxAdvance;

		// Token: 0x0400007D RID: 125
		public float width;

		// Token: 0x0400007E RID: 126
		public float marginLeft;

		// Token: 0x0400007F RID: 127
		public float marginRight;

		// Token: 0x04000080 RID: 128
		public TextAlignment alignment;

		// Token: 0x04000081 RID: 129
		public Extents lineExtents;
	}
}
