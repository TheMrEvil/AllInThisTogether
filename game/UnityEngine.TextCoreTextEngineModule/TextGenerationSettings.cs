using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000027 RID: 39
	internal class TextGenerationSettings : IEquatable<TextGenerationSettings>
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00007E70 File Offset: 0x00006070
		public bool Equals(TextGenerationSettings other)
		{
			bool flag = other == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this == other;
				result = (flag2 || (this.text == other.text && this.screenRect.Equals(other.screenRect) && this.margins.Equals(other.margins) && this.scale.Equals(other.scale) && object.Equals(this.fontAsset, other.fontAsset) && object.Equals(this.material, other.material) && object.Equals(this.spriteAsset, other.spriteAsset) && object.Equals(this.styleSheet, other.styleSheet) && this.fontStyle == other.fontStyle && object.Equals(this.textSettings, other.textSettings) && this.textAlignment == other.textAlignment && this.overflowMode == other.overflowMode && this.wordWrap == other.wordWrap && this.wordWrappingRatio.Equals(other.wordWrappingRatio) && this.color.Equals(other.color) && object.Equals(this.fontColorGradient, other.fontColorGradient) && this.tintSprites == other.tintSprites && this.overrideRichTextColors == other.overrideRichTextColors && this.fontSize.Equals(other.fontSize) && this.autoSize == other.autoSize && this.fontSizeMin.Equals(other.fontSizeMin) && this.fontSizeMax.Equals(other.fontSizeMax) && this.enableKerning == other.enableKerning && this.richText == other.richText && this.isRightToLeft == other.isRightToLeft && this.extraPadding == other.extraPadding && this.parseControlCharacters == other.parseControlCharacters && this.characterSpacing.Equals(other.characterSpacing) && this.wordSpacing.Equals(other.wordSpacing) && this.lineSpacing.Equals(other.lineSpacing) && this.paragraphSpacing.Equals(other.paragraphSpacing) && this.lineSpacingMax.Equals(other.lineSpacingMax) && this.maxVisibleCharacters == other.maxVisibleCharacters && this.maxVisibleWords == other.maxVisibleWords && this.maxVisibleLines == other.maxVisibleLines && this.firstVisibleCharacter == other.firstVisibleCharacter && this.useMaxVisibleDescender == other.useMaxVisibleDescender && this.fontWeight == other.fontWeight && this.pageToDisplay == other.pageToDisplay && this.horizontalMapping == other.horizontalMapping && this.verticalMapping == other.verticalMapping && this.uvLineOffset.Equals(other.uvLineOffset) && this.geometrySortingOrder == other.geometrySortingOrder && this.inverseYAxis == other.inverseYAxis && this.charWidthMaxAdj.Equals(other.charWidthMaxAdj)));
			}
			return result;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000081F4 File Offset: 0x000063F4
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this == obj;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = obj.GetType() != base.GetType();
					result = (!flag3 && this.Equals((TextGenerationSettings)obj));
				}
			}
			return result;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00008244 File Offset: 0x00006444
		public override int GetHashCode()
		{
			int num = (this.text != null) ? this.text.GetHashCode() : 0;
			num = (num * 397 ^ this.screenRect.GetHashCode());
			num = (num * 397 ^ this.margins.GetHashCode());
			num = (num * 397 ^ this.scale.GetHashCode());
			num = (num * 397 ^ ((this.fontAsset != null) ? this.fontAsset.GetHashCode() : 0));
			num = (num * 397 ^ ((this.material != null) ? this.material.GetHashCode() : 0));
			num = (num * 397 ^ ((this.spriteAsset != null) ? this.spriteAsset.GetHashCode() : 0));
			num = (num * 397 ^ (int)this.fontStyle);
			num = (num * 397 ^ ((this.textSettings != null) ? this.textSettings.GetHashCode() : 0));
			num = (num * 397 ^ (int)this.textAlignment);
			num = (num * 397 ^ (int)this.overflowMode);
			num = (num * 397 ^ this.wordWrap.GetHashCode());
			num = (num * 397 ^ this.wordWrappingRatio.GetHashCode());
			num = (num * 397 ^ this.color.GetHashCode());
			num = (num * 397 ^ ((this.fontColorGradient != null) ? this.fontColorGradient.GetHashCode() : 0));
			num = (num * 397 ^ this.tintSprites.GetHashCode());
			num = (num * 397 ^ this.overrideRichTextColors.GetHashCode());
			num = (num * 397 ^ this.fontSize.GetHashCode());
			num = (num * 397 ^ this.autoSize.GetHashCode());
			num = (num * 397 ^ this.fontSizeMin.GetHashCode());
			num = (num * 397 ^ this.fontSizeMax.GetHashCode());
			num = (num * 397 ^ this.enableKerning.GetHashCode());
			num = (num * 397 ^ this.richText.GetHashCode());
			num = (num * 397 ^ this.isRightToLeft.GetHashCode());
			num = (num * 397 ^ this.extraPadding.GetHashCode());
			num = (num * 397 ^ this.parseControlCharacters.GetHashCode());
			num = (num * 397 ^ this.characterSpacing.GetHashCode());
			num = (num * 397 ^ this.wordSpacing.GetHashCode());
			num = (num * 397 ^ this.lineSpacing.GetHashCode());
			num = (num * 397 ^ this.paragraphSpacing.GetHashCode());
			num = (num * 397 ^ this.lineSpacingMax.GetHashCode());
			num = (num * 397 ^ this.maxVisibleCharacters);
			num = (num * 397 ^ this.maxVisibleWords);
			num = (num * 397 ^ this.maxVisibleLines);
			num = (num * 397 ^ this.firstVisibleCharacter);
			num = (num * 397 ^ this.useMaxVisibleDescender.GetHashCode());
			num = (num * 397 ^ (int)this.fontWeight);
			num = (num * 397 ^ this.pageToDisplay);
			num = (num * 397 ^ (int)this.horizontalMapping);
			num = (num * 397 ^ (int)this.verticalMapping);
			num = (num * 397 ^ this.uvLineOffset.GetHashCode());
			num = (num * 397 ^ (int)this.geometrySortingOrder);
			num = (num * 397 ^ this.inverseYAxis.GetHashCode());
			return num * 397 ^ this.charWidthMaxAdj.GetHashCode();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000085F8 File Offset: 0x000067F8
		public static bool operator ==(TextGenerationSettings left, TextGenerationSettings right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00008614 File Offset: 0x00006814
		public static bool operator !=(TextGenerationSettings left, TextGenerationSettings right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00008630 File Offset: 0x00006830
		public override string ToString()
		{
			return string.Format("{0}: {1}\n {2}: {3}\n {4}: {5}\n {6}: {7}\n {8}: {9}\n {10}: {11}\n {12}: {13}\n {14}: {15}\n {16}: {17}\n {18}: {19}\n {20}: {21}\n {22}: {23}\n {24}: {25}\n {26}: {27}\n {28}: {29}\n {30}: {31}\n {32}: {33}\n {34}: {35}\n  {36}: {37}\n {38}: {39}\n {40}: {41}\n {42}: {43}\n {44}: {45}\n {46}: {47}\n {48}: {49}\n {50}: {51}\n {52}: {53}\n {54}: {55}\n {56}: {57}\n {58}: {59}\n {60}: {61}\n {62}: {63}\n {64}: {65}\n {66}: {67}\n {68}: {69}\n {70}: {71}\n {72}: {73}\n {74}: {75}\n {76}: {77}\n {78}: {79}\n {80}: {81}\n {82}: {83}\n {84}: {85}\n {86}: {87}\n {88}: {89}", new object[]
			{
				"text",
				this.text,
				"screenRect",
				this.screenRect,
				"margins",
				this.margins,
				"scale",
				this.scale,
				"fontAsset",
				this.fontAsset,
				"material",
				this.material,
				"spriteAsset",
				this.spriteAsset,
				"styleSheet",
				this.styleSheet,
				"fontStyle",
				this.fontStyle,
				"textSettings",
				this.textSettings,
				"textAlignment",
				this.textAlignment,
				"overflowMode",
				this.overflowMode,
				"wordWrap",
				this.wordWrap,
				"wordWrappingRatio",
				this.wordWrappingRatio,
				"color",
				this.color,
				"fontColorGradient",
				this.fontColorGradient,
				"tintSprites",
				this.tintSprites,
				"overrideRichTextColors",
				this.overrideRichTextColors,
				"fontSize",
				this.fontSize,
				"autoSize",
				this.autoSize,
				"fontSizeMin",
				this.fontSizeMin,
				"fontSizeMax",
				this.fontSizeMax,
				"enableKerning",
				this.enableKerning,
				"richText",
				this.richText,
				"isRightToLeft",
				this.isRightToLeft,
				"extraPadding",
				this.extraPadding,
				"parseControlCharacters",
				this.parseControlCharacters,
				"characterSpacing",
				this.characterSpacing,
				"wordSpacing",
				this.wordSpacing,
				"lineSpacing",
				this.lineSpacing,
				"paragraphSpacing",
				this.paragraphSpacing,
				"lineSpacingMax",
				this.lineSpacingMax,
				"maxVisibleCharacters",
				this.maxVisibleCharacters,
				"maxVisibleWords",
				this.maxVisibleWords,
				"maxVisibleLines",
				this.maxVisibleLines,
				"firstVisibleCharacter",
				this.firstVisibleCharacter,
				"useMaxVisibleDescender",
				this.useMaxVisibleDescender,
				"fontWeight",
				this.fontWeight,
				"pageToDisplay",
				this.pageToDisplay,
				"horizontalMapping",
				this.horizontalMapping,
				"verticalMapping",
				this.verticalMapping,
				"uvLineOffset",
				this.uvLineOffset,
				"geometrySortingOrder",
				this.geometrySortingOrder,
				"inverseYAxis",
				this.inverseYAxis,
				"charWidthMaxAdj",
				this.charWidthMaxAdj
			});
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00008A60 File Offset: 0x00006C60
		public TextGenerationSettings()
		{
		}

		// Token: 0x0400010E RID: 270
		public string text;

		// Token: 0x0400010F RID: 271
		public Rect screenRect;

		// Token: 0x04000110 RID: 272
		public Vector4 margins;

		// Token: 0x04000111 RID: 273
		public float scale = 1f;

		// Token: 0x04000112 RID: 274
		public FontAsset fontAsset;

		// Token: 0x04000113 RID: 275
		public Material material;

		// Token: 0x04000114 RID: 276
		public SpriteAsset spriteAsset;

		// Token: 0x04000115 RID: 277
		public TextStyleSheet styleSheet;

		// Token: 0x04000116 RID: 278
		public FontStyles fontStyle = FontStyles.Normal;

		// Token: 0x04000117 RID: 279
		public TextSettings textSettings;

		// Token: 0x04000118 RID: 280
		public TextAlignment textAlignment = TextAlignment.TopLeft;

		// Token: 0x04000119 RID: 281
		public TextOverflowMode overflowMode = TextOverflowMode.Overflow;

		// Token: 0x0400011A RID: 282
		public bool wordWrap = false;

		// Token: 0x0400011B RID: 283
		public float wordWrappingRatio;

		// Token: 0x0400011C RID: 284
		public Color color = Color.white;

		// Token: 0x0400011D RID: 285
		public TextColorGradient fontColorGradient;

		// Token: 0x0400011E RID: 286
		public bool tintSprites;

		// Token: 0x0400011F RID: 287
		public bool overrideRichTextColors;

		// Token: 0x04000120 RID: 288
		public float fontSize = 18f;

		// Token: 0x04000121 RID: 289
		public bool autoSize;

		// Token: 0x04000122 RID: 290
		public float fontSizeMin;

		// Token: 0x04000123 RID: 291
		public float fontSizeMax;

		// Token: 0x04000124 RID: 292
		public bool enableKerning = true;

		// Token: 0x04000125 RID: 293
		public bool richText;

		// Token: 0x04000126 RID: 294
		public bool isRightToLeft;

		// Token: 0x04000127 RID: 295
		public bool extraPadding;

		// Token: 0x04000128 RID: 296
		public bool parseControlCharacters = true;

		// Token: 0x04000129 RID: 297
		public float characterSpacing;

		// Token: 0x0400012A RID: 298
		public float wordSpacing;

		// Token: 0x0400012B RID: 299
		public float lineSpacing;

		// Token: 0x0400012C RID: 300
		public float paragraphSpacing;

		// Token: 0x0400012D RID: 301
		public float lineSpacingMax;

		// Token: 0x0400012E RID: 302
		public int maxVisibleCharacters = 99999;

		// Token: 0x0400012F RID: 303
		public int maxVisibleWords = 99999;

		// Token: 0x04000130 RID: 304
		public int maxVisibleLines = 99999;

		// Token: 0x04000131 RID: 305
		public int firstVisibleCharacter = 0;

		// Token: 0x04000132 RID: 306
		public bool useMaxVisibleDescender;

		// Token: 0x04000133 RID: 307
		public TextFontWeight fontWeight = TextFontWeight.Regular;

		// Token: 0x04000134 RID: 308
		public int pageToDisplay = 1;

		// Token: 0x04000135 RID: 309
		public TextureMapping horizontalMapping = TextureMapping.Character;

		// Token: 0x04000136 RID: 310
		public TextureMapping verticalMapping = TextureMapping.Character;

		// Token: 0x04000137 RID: 311
		public float uvLineOffset;

		// Token: 0x04000138 RID: 312
		public VertexSortingOrder geometrySortingOrder = VertexSortingOrder.Normal;

		// Token: 0x04000139 RID: 313
		public bool inverseYAxis;

		// Token: 0x0400013A RID: 314
		public float charWidthMaxAdj;
	}
}
