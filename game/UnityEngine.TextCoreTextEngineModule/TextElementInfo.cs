using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000023 RID: 35
	internal struct TextElementInfo
	{
		// Token: 0x040000D2 RID: 210
		public char character;

		// Token: 0x040000D3 RID: 211
		public int index;

		// Token: 0x040000D4 RID: 212
		public TextElementType elementType;

		// Token: 0x040000D5 RID: 213
		public TextElement textElement;

		// Token: 0x040000D6 RID: 214
		public FontAsset fontAsset;

		// Token: 0x040000D7 RID: 215
		public SpriteAsset spriteAsset;

		// Token: 0x040000D8 RID: 216
		public int spriteIndex;

		// Token: 0x040000D9 RID: 217
		public Material material;

		// Token: 0x040000DA RID: 218
		public int materialReferenceIndex;

		// Token: 0x040000DB RID: 219
		public bool isUsingAlternateTypeface;

		// Token: 0x040000DC RID: 220
		public float pointSize;

		// Token: 0x040000DD RID: 221
		public int lineNumber;

		// Token: 0x040000DE RID: 222
		public int pageNumber;

		// Token: 0x040000DF RID: 223
		public int vertexIndex;

		// Token: 0x040000E0 RID: 224
		public TextVertex vertexTopLeft;

		// Token: 0x040000E1 RID: 225
		public TextVertex vertexBottomLeft;

		// Token: 0x040000E2 RID: 226
		public TextVertex vertexTopRight;

		// Token: 0x040000E3 RID: 227
		public TextVertex vertexBottomRight;

		// Token: 0x040000E4 RID: 228
		public Vector3 topLeft;

		// Token: 0x040000E5 RID: 229
		public Vector3 bottomLeft;

		// Token: 0x040000E6 RID: 230
		public Vector3 topRight;

		// Token: 0x040000E7 RID: 231
		public Vector3 bottomRight;

		// Token: 0x040000E8 RID: 232
		public float origin;

		// Token: 0x040000E9 RID: 233
		public float ascender;

		// Token: 0x040000EA RID: 234
		public float baseLine;

		// Token: 0x040000EB RID: 235
		public float descender;

		// Token: 0x040000EC RID: 236
		public float xAdvance;

		// Token: 0x040000ED RID: 237
		public float aspectRatio;

		// Token: 0x040000EE RID: 238
		public float scale;

		// Token: 0x040000EF RID: 239
		public Color32 color;

		// Token: 0x040000F0 RID: 240
		public Color32 underlineColor;

		// Token: 0x040000F1 RID: 241
		public Color32 strikethroughColor;

		// Token: 0x040000F2 RID: 242
		public Color32 highlightColor;

		// Token: 0x040000F3 RID: 243
		public FontStyles style;

		// Token: 0x040000F4 RID: 244
		public bool isVisible;
	}
}
