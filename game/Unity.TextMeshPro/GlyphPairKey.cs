using System;

namespace TMPro
{
	// Token: 0x0200003D RID: 61
	public struct GlyphPairKey
	{
		// Token: 0x0600023F RID: 575 RVA: 0x0001CFF0 File Offset: 0x0001B1F0
		public GlyphPairKey(uint firstGlyphIndex, uint secondGlyphIndex)
		{
			this.firstGlyphIndex = firstGlyphIndex;
			this.secondGlyphIndex = secondGlyphIndex;
			this.key = (secondGlyphIndex << 16 | firstGlyphIndex);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0001D00C File Offset: 0x0001B20C
		internal GlyphPairKey(TMP_GlyphPairAdjustmentRecord record)
		{
			this.firstGlyphIndex = record.firstAdjustmentRecord.glyphIndex;
			this.secondGlyphIndex = record.secondAdjustmentRecord.glyphIndex;
			this.key = (this.secondGlyphIndex << 16 | this.firstGlyphIndex);
		}

		// Token: 0x040001FB RID: 507
		public uint firstGlyphIndex;

		// Token: 0x040001FC RID: 508
		public uint secondGlyphIndex;

		// Token: 0x040001FD RID: 509
		public uint key;
	}
}
