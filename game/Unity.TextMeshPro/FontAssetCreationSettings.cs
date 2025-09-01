using System;

namespace TMPro
{
	// Token: 0x02000031 RID: 49
	[Serializable]
	public struct FontAssetCreationSettings
	{
		// Token: 0x06000202 RID: 514 RVA: 0x0001C494 File Offset: 0x0001A694
		internal FontAssetCreationSettings(string sourceFontFileGUID, int pointSize, int pointSizeSamplingMode, int padding, int packingMode, int atlasWidth, int atlasHeight, int characterSelectionMode, string characterSet, int renderMode)
		{
			this.sourceFontFileName = string.Empty;
			this.sourceFontFileGUID = sourceFontFileGUID;
			this.pointSize = pointSize;
			this.pointSizeSamplingMode = pointSizeSamplingMode;
			this.padding = padding;
			this.packingMode = packingMode;
			this.atlasWidth = atlasWidth;
			this.atlasHeight = atlasHeight;
			this.characterSequence = characterSet;
			this.characterSetSelectionMode = characterSelectionMode;
			this.renderMode = renderMode;
			this.referencedFontAssetGUID = string.Empty;
			this.referencedTextAssetGUID = string.Empty;
			this.fontStyle = 0;
			this.fontStyleModifier = 0f;
			this.includeFontFeatures = false;
		}

		// Token: 0x040001C9 RID: 457
		public string sourceFontFileName;

		// Token: 0x040001CA RID: 458
		public string sourceFontFileGUID;

		// Token: 0x040001CB RID: 459
		public int pointSizeSamplingMode;

		// Token: 0x040001CC RID: 460
		public int pointSize;

		// Token: 0x040001CD RID: 461
		public int padding;

		// Token: 0x040001CE RID: 462
		public int packingMode;

		// Token: 0x040001CF RID: 463
		public int atlasWidth;

		// Token: 0x040001D0 RID: 464
		public int atlasHeight;

		// Token: 0x040001D1 RID: 465
		public int characterSetSelectionMode;

		// Token: 0x040001D2 RID: 466
		public string characterSequence;

		// Token: 0x040001D3 RID: 467
		public string referencedFontAssetGUID;

		// Token: 0x040001D4 RID: 468
		public string referencedTextAssetGUID;

		// Token: 0x040001D5 RID: 469
		public int fontStyle;

		// Token: 0x040001D6 RID: 470
		public float fontStyleModifier;

		// Token: 0x040001D7 RID: 471
		public int renderMode;

		// Token: 0x040001D8 RID: 472
		public bool includeFontFeatures;
	}
}
