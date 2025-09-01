using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000031 RID: 49
	internal struct WordWrapState
	{
		// Token: 0x0400021E RID: 542
		public int previousWordBreak;

		// Token: 0x0400021F RID: 543
		public int totalCharacterCount;

		// Token: 0x04000220 RID: 544
		public int visibleCharacterCount;

		// Token: 0x04000221 RID: 545
		public int visibleSpriteCount;

		// Token: 0x04000222 RID: 546
		public int visibleLinkCount;

		// Token: 0x04000223 RID: 547
		public int firstCharacterIndex;

		// Token: 0x04000224 RID: 548
		public int firstVisibleCharacterIndex;

		// Token: 0x04000225 RID: 549
		public int lastCharacterIndex;

		// Token: 0x04000226 RID: 550
		public int lastVisibleCharIndex;

		// Token: 0x04000227 RID: 551
		public int lineNumber;

		// Token: 0x04000228 RID: 552
		public float maxCapHeight;

		// Token: 0x04000229 RID: 553
		public float maxAscender;

		// Token: 0x0400022A RID: 554
		public float maxDescender;

		// Token: 0x0400022B RID: 555
		public float maxLineAscender;

		// Token: 0x0400022C RID: 556
		public float maxLineDescender;

		// Token: 0x0400022D RID: 557
		public float previousLineAscender;

		// Token: 0x0400022E RID: 558
		public float xAdvance;

		// Token: 0x0400022F RID: 559
		public float preferredWidth;

		// Token: 0x04000230 RID: 560
		public float preferredHeight;

		// Token: 0x04000231 RID: 561
		public float previousLineScale;

		// Token: 0x04000232 RID: 562
		public int wordCount;

		// Token: 0x04000233 RID: 563
		public FontStyles fontStyle;

		// Token: 0x04000234 RID: 564
		public float fontScale;

		// Token: 0x04000235 RID: 565
		public float fontScaleMultiplier;

		// Token: 0x04000236 RID: 566
		public float currentFontSize;

		// Token: 0x04000237 RID: 567
		public float baselineOffset;

		// Token: 0x04000238 RID: 568
		public float lineOffset;

		// Token: 0x04000239 RID: 569
		public TextInfo textInfo;

		// Token: 0x0400023A RID: 570
		public LineInfo lineInfo;

		// Token: 0x0400023B RID: 571
		public Color32 vertexColor;

		// Token: 0x0400023C RID: 572
		public Color32 underlineColor;

		// Token: 0x0400023D RID: 573
		public Color32 strikethroughColor;

		// Token: 0x0400023E RID: 574
		public Color32 highlightColor;

		// Token: 0x0400023F RID: 575
		public FontStyleStack basicStyleStack;

		// Token: 0x04000240 RID: 576
		public TextProcessingStack<Color32> colorStack;

		// Token: 0x04000241 RID: 577
		public TextProcessingStack<Color32> underlineColorStack;

		// Token: 0x04000242 RID: 578
		public TextProcessingStack<Color32> strikethroughColorStack;

		// Token: 0x04000243 RID: 579
		public TextProcessingStack<Color32> highlightColorStack;

		// Token: 0x04000244 RID: 580
		public TextProcessingStack<TextColorGradient> colorGradientStack;

		// Token: 0x04000245 RID: 581
		public TextProcessingStack<float> sizeStack;

		// Token: 0x04000246 RID: 582
		public TextProcessingStack<float> indentStack;

		// Token: 0x04000247 RID: 583
		public TextProcessingStack<TextFontWeight> fontWeightStack;

		// Token: 0x04000248 RID: 584
		public TextProcessingStack<int> styleStack;

		// Token: 0x04000249 RID: 585
		public TextProcessingStack<float> baselineStack;

		// Token: 0x0400024A RID: 586
		public TextProcessingStack<int> actionStack;

		// Token: 0x0400024B RID: 587
		public TextProcessingStack<MaterialReference> materialReferenceStack;

		// Token: 0x0400024C RID: 588
		public TextProcessingStack<TextAlignment> lineJustificationStack;

		// Token: 0x0400024D RID: 589
		public int spriteAnimationId;

		// Token: 0x0400024E RID: 590
		public FontAsset currentFontAsset;

		// Token: 0x0400024F RID: 591
		public SpriteAsset currentSpriteAsset;

		// Token: 0x04000250 RID: 592
		public Material currentMaterial;

		// Token: 0x04000251 RID: 593
		public int currentMaterialIndex;

		// Token: 0x04000252 RID: 594
		public Extents meshExtents;

		// Token: 0x04000253 RID: 595
		public bool tagNoParsing;

		// Token: 0x04000254 RID: 596
		public bool isNonBreakingSpace;
	}
}
