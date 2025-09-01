using System;

namespace UnityEngine
{
	// Token: 0x02000005 RID: 5
	public struct TextGenerationSettings
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000021E4 File Offset: 0x000003E4
		private bool CompareColors(Color left, Color right)
		{
			return Mathf.Approximately(left.r, right.r) && Mathf.Approximately(left.g, right.g) && Mathf.Approximately(left.b, right.b) && Mathf.Approximately(left.a, right.a);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002244 File Offset: 0x00000444
		private bool CompareVector2(Vector2 left, Vector2 right)
		{
			return Mathf.Approximately(left.x, right.x) && Mathf.Approximately(left.y, right.y);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002280 File Offset: 0x00000480
		public bool Equals(TextGenerationSettings other)
		{
			return this.CompareColors(this.color, other.color) && this.fontSize == other.fontSize && Mathf.Approximately(this.scaleFactor, other.scaleFactor) && this.resizeTextMinSize == other.resizeTextMinSize && this.resizeTextMaxSize == other.resizeTextMaxSize && Mathf.Approximately(this.lineSpacing, other.lineSpacing) && this.fontStyle == other.fontStyle && this.richText == other.richText && this.textAnchor == other.textAnchor && this.alignByGeometry == other.alignByGeometry && this.resizeTextForBestFit == other.resizeTextForBestFit && this.updateBounds == other.updateBounds && this.horizontalOverflow == other.horizontalOverflow && this.verticalOverflow == other.verticalOverflow && this.CompareVector2(this.generationExtents, other.generationExtents) && this.CompareVector2(this.pivot, other.pivot) && this.font == other.font;
		}

		// Token: 0x0400000B RID: 11
		public Font font;

		// Token: 0x0400000C RID: 12
		public Color color;

		// Token: 0x0400000D RID: 13
		public int fontSize;

		// Token: 0x0400000E RID: 14
		public float lineSpacing;

		// Token: 0x0400000F RID: 15
		public bool richText;

		// Token: 0x04000010 RID: 16
		public float scaleFactor;

		// Token: 0x04000011 RID: 17
		public FontStyle fontStyle;

		// Token: 0x04000012 RID: 18
		public TextAnchor textAnchor;

		// Token: 0x04000013 RID: 19
		public bool alignByGeometry;

		// Token: 0x04000014 RID: 20
		public bool resizeTextForBestFit;

		// Token: 0x04000015 RID: 21
		public int resizeTextMinSize;

		// Token: 0x04000016 RID: 22
		public int resizeTextMaxSize;

		// Token: 0x04000017 RID: 23
		public bool updateBounds;

		// Token: 0x04000018 RID: 24
		public VerticalWrapMode verticalOverflow;

		// Token: 0x04000019 RID: 25
		public HorizontalWrapMode horizontalOverflow;

		// Token: 0x0400001A RID: 26
		public Vector2 generationExtents;

		// Token: 0x0400001B RID: 27
		public Vector2 pivot;

		// Token: 0x0400001C RID: 28
		public bool generateOutOfBounds;
	}
}
