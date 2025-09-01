using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200001B RID: 27
	public struct WordWrapState
	{
		// Token: 0x040000BD RID: 189
		public int previous_WordBreak;

		// Token: 0x040000BE RID: 190
		public int total_CharacterCount;

		// Token: 0x040000BF RID: 191
		public int visible_CharacterCount;

		// Token: 0x040000C0 RID: 192
		public int visible_SpriteCount;

		// Token: 0x040000C1 RID: 193
		public int visible_LinkCount;

		// Token: 0x040000C2 RID: 194
		public int firstCharacterIndex;

		// Token: 0x040000C3 RID: 195
		public int firstVisibleCharacterIndex;

		// Token: 0x040000C4 RID: 196
		public int lastCharacterIndex;

		// Token: 0x040000C5 RID: 197
		public int lastVisibleCharIndex;

		// Token: 0x040000C6 RID: 198
		public int lineNumber;

		// Token: 0x040000C7 RID: 199
		public float maxCapHeight;

		// Token: 0x040000C8 RID: 200
		public float maxAscender;

		// Token: 0x040000C9 RID: 201
		public float maxDescender;

		// Token: 0x040000CA RID: 202
		public float startOfLineAscender;

		// Token: 0x040000CB RID: 203
		public float maxLineAscender;

		// Token: 0x040000CC RID: 204
		public float maxLineDescender;

		// Token: 0x040000CD RID: 205
		public float pageAscender;

		// Token: 0x040000CE RID: 206
		public HorizontalAlignmentOptions horizontalAlignment;

		// Token: 0x040000CF RID: 207
		public float marginLeft;

		// Token: 0x040000D0 RID: 208
		public float marginRight;

		// Token: 0x040000D1 RID: 209
		public float xAdvance;

		// Token: 0x040000D2 RID: 210
		public float preferredWidth;

		// Token: 0x040000D3 RID: 211
		public float preferredHeight;

		// Token: 0x040000D4 RID: 212
		public float previousLineScale;

		// Token: 0x040000D5 RID: 213
		public int wordCount;

		// Token: 0x040000D6 RID: 214
		public FontStyles fontStyle;

		// Token: 0x040000D7 RID: 215
		public int italicAngle;

		// Token: 0x040000D8 RID: 216
		public float fontScaleMultiplier;

		// Token: 0x040000D9 RID: 217
		public float currentFontSize;

		// Token: 0x040000DA RID: 218
		public float baselineOffset;

		// Token: 0x040000DB RID: 219
		public float lineOffset;

		// Token: 0x040000DC RID: 220
		public bool isDrivenLineSpacing;

		// Token: 0x040000DD RID: 221
		public float glyphHorizontalAdvanceAdjustment;

		// Token: 0x040000DE RID: 222
		public float cSpace;

		// Token: 0x040000DF RID: 223
		public float mSpace;

		// Token: 0x040000E0 RID: 224
		public TMP_TextInfo textInfo;

		// Token: 0x040000E1 RID: 225
		public TMP_LineInfo lineInfo;

		// Token: 0x040000E2 RID: 226
		public Color32 vertexColor;

		// Token: 0x040000E3 RID: 227
		public Color32 underlineColor;

		// Token: 0x040000E4 RID: 228
		public Color32 strikethroughColor;

		// Token: 0x040000E5 RID: 229
		public Color32 highlightColor;

		// Token: 0x040000E6 RID: 230
		public TMP_FontStyleStack basicStyleStack;

		// Token: 0x040000E7 RID: 231
		public TMP_TextProcessingStack<int> italicAngleStack;

		// Token: 0x040000E8 RID: 232
		public TMP_TextProcessingStack<Color32> colorStack;

		// Token: 0x040000E9 RID: 233
		public TMP_TextProcessingStack<Color32> underlineColorStack;

		// Token: 0x040000EA RID: 234
		public TMP_TextProcessingStack<Color32> strikethroughColorStack;

		// Token: 0x040000EB RID: 235
		public TMP_TextProcessingStack<Color32> highlightColorStack;

		// Token: 0x040000EC RID: 236
		public TMP_TextProcessingStack<HighlightState> highlightStateStack;

		// Token: 0x040000ED RID: 237
		public TMP_TextProcessingStack<TMP_ColorGradient> colorGradientStack;

		// Token: 0x040000EE RID: 238
		public TMP_TextProcessingStack<float> sizeStack;

		// Token: 0x040000EF RID: 239
		public TMP_TextProcessingStack<float> indentStack;

		// Token: 0x040000F0 RID: 240
		public TMP_TextProcessingStack<FontWeight> fontWeightStack;

		// Token: 0x040000F1 RID: 241
		public TMP_TextProcessingStack<int> styleStack;

		// Token: 0x040000F2 RID: 242
		public TMP_TextProcessingStack<float> baselineStack;

		// Token: 0x040000F3 RID: 243
		public TMP_TextProcessingStack<int> actionStack;

		// Token: 0x040000F4 RID: 244
		public TMP_TextProcessingStack<MaterialReference> materialReferenceStack;

		// Token: 0x040000F5 RID: 245
		public TMP_TextProcessingStack<HorizontalAlignmentOptions> lineJustificationStack;

		// Token: 0x040000F6 RID: 246
		public int spriteAnimationID;

		// Token: 0x040000F7 RID: 247
		public TMP_FontAsset currentFontAsset;

		// Token: 0x040000F8 RID: 248
		public TMP_SpriteAsset currentSpriteAsset;

		// Token: 0x040000F9 RID: 249
		public Material currentMaterial;

		// Token: 0x040000FA RID: 250
		public int currentMaterialIndex;

		// Token: 0x040000FB RID: 251
		public Extents meshExtents;

		// Token: 0x040000FC RID: 252
		public bool tagNoParsing;

		// Token: 0x040000FD RID: 253
		public bool isNonBreakingSpace;
	}
}
