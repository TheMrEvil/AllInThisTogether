using System;
using System.Diagnostics;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000023 RID: 35
	[DebuggerDisplay("Unicode '{character}'  ({((uint)character).ToString(\"X\")})")]
	public struct TMP_CharacterInfo
	{
		// Token: 0x04000118 RID: 280
		public char character;

		// Token: 0x04000119 RID: 281
		public int index;

		// Token: 0x0400011A RID: 282
		public int stringLength;

		// Token: 0x0400011B RID: 283
		public TMP_TextElementType elementType;

		// Token: 0x0400011C RID: 284
		public TMP_TextElement textElement;

		// Token: 0x0400011D RID: 285
		public TMP_FontAsset fontAsset;

		// Token: 0x0400011E RID: 286
		public TMP_SpriteAsset spriteAsset;

		// Token: 0x0400011F RID: 287
		public int spriteIndex;

		// Token: 0x04000120 RID: 288
		public Material material;

		// Token: 0x04000121 RID: 289
		public int materialReferenceIndex;

		// Token: 0x04000122 RID: 290
		public bool isUsingAlternateTypeface;

		// Token: 0x04000123 RID: 291
		public float pointSize;

		// Token: 0x04000124 RID: 292
		public int lineNumber;

		// Token: 0x04000125 RID: 293
		public int pageNumber;

		// Token: 0x04000126 RID: 294
		public int vertexIndex;

		// Token: 0x04000127 RID: 295
		public TMP_Vertex vertex_BL;

		// Token: 0x04000128 RID: 296
		public TMP_Vertex vertex_TL;

		// Token: 0x04000129 RID: 297
		public TMP_Vertex vertex_TR;

		// Token: 0x0400012A RID: 298
		public TMP_Vertex vertex_BR;

		// Token: 0x0400012B RID: 299
		public Vector3 topLeft;

		// Token: 0x0400012C RID: 300
		public Vector3 bottomLeft;

		// Token: 0x0400012D RID: 301
		public Vector3 topRight;

		// Token: 0x0400012E RID: 302
		public Vector3 bottomRight;

		// Token: 0x0400012F RID: 303
		public float origin;

		// Token: 0x04000130 RID: 304
		public float xAdvance;

		// Token: 0x04000131 RID: 305
		public float ascender;

		// Token: 0x04000132 RID: 306
		public float baseLine;

		// Token: 0x04000133 RID: 307
		public float descender;

		// Token: 0x04000134 RID: 308
		internal float adjustedAscender;

		// Token: 0x04000135 RID: 309
		internal float adjustedDescender;

		// Token: 0x04000136 RID: 310
		public float aspectRatio;

		// Token: 0x04000137 RID: 311
		public float scale;

		// Token: 0x04000138 RID: 312
		public Color32 color;

		// Token: 0x04000139 RID: 313
		public Color32 underlineColor;

		// Token: 0x0400013A RID: 314
		public int underlineVertexIndex;

		// Token: 0x0400013B RID: 315
		public Color32 strikethroughColor;

		// Token: 0x0400013C RID: 316
		public int strikethroughVertexIndex;

		// Token: 0x0400013D RID: 317
		public Color32 highlightColor;

		// Token: 0x0400013E RID: 318
		public HighlightState highlightState;

		// Token: 0x0400013F RID: 319
		public FontStyles style;

		// Token: 0x04000140 RID: 320
		public bool isVisible;
	}
}
