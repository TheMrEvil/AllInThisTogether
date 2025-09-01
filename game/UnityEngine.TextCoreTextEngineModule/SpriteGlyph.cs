using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200001C RID: 28
	[Serializable]
	public class SpriteGlyph : Glyph
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00007A19 File Offset: 0x00005C19
		public SpriteGlyph()
		{
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00007A23 File Offset: 0x00005C23
		public SpriteGlyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex)
		{
			base.index = index;
			base.metrics = metrics;
			base.glyphRect = glyphRect;
			base.scale = scale;
			base.atlasIndex = atlasIndex;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00007A57 File Offset: 0x00005C57
		public SpriteGlyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex, Sprite sprite)
		{
			base.index = index;
			base.metrics = metrics;
			base.glyphRect = glyphRect;
			base.scale = scale;
			base.atlasIndex = atlasIndex;
			this.sprite = sprite;
		}

		// Token: 0x040000B2 RID: 178
		public Sprite sprite;
	}
}
