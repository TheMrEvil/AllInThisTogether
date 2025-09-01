using System;
using UnityEngine;
using UnityEngine.TextCore;

namespace TMPro
{
	// Token: 0x02000055 RID: 85
	[Serializable]
	public class TMP_SpriteGlyph : Glyph
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x0002680A File Offset: 0x00024A0A
		public TMP_SpriteGlyph()
		{
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00026812 File Offset: 0x00024A12
		public TMP_SpriteGlyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex)
		{
			base.index = index;
			base.metrics = metrics;
			base.glyphRect = glyphRect;
			base.scale = scale;
			base.atlasIndex = atlasIndex;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0002683F File Offset: 0x00024A3F
		public TMP_SpriteGlyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex, Sprite sprite)
		{
			base.index = index;
			base.metrics = metrics;
			base.glyphRect = glyphRect;
			base.scale = scale;
			base.atlasIndex = atlasIndex;
			this.sprite = sprite;
		}

		// Token: 0x040003A9 RID: 937
		public Sprite sprite;
	}
}
