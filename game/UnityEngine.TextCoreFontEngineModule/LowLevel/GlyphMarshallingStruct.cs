using System;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000014 RID: 20
	[UsedByNativeCode]
	internal struct GlyphMarshallingStruct
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00004954 File Offset: 0x00002B54
		public GlyphMarshallingStruct(Glyph glyph)
		{
			this.index = glyph.index;
			this.metrics = glyph.metrics;
			this.glyphRect = glyph.glyphRect;
			this.scale = glyph.scale;
			this.atlasIndex = glyph.atlasIndex;
			this.classDefinitionType = glyph.classDefinitionType;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000049AA File Offset: 0x00002BAA
		public GlyphMarshallingStruct(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex)
		{
			this.index = index;
			this.metrics = metrics;
			this.glyphRect = glyphRect;
			this.scale = scale;
			this.atlasIndex = atlasIndex;
			this.classDefinitionType = GlyphClassDefinitionType.Undefined;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000049D9 File Offset: 0x00002BD9
		public GlyphMarshallingStruct(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex, GlyphClassDefinitionType classDefinitionType)
		{
			this.index = index;
			this.metrics = metrics;
			this.glyphRect = glyphRect;
			this.scale = scale;
			this.atlasIndex = atlasIndex;
			this.classDefinitionType = classDefinitionType;
		}

		// Token: 0x0400008C RID: 140
		public uint index;

		// Token: 0x0400008D RID: 141
		public GlyphMetrics metrics;

		// Token: 0x0400008E RID: 142
		public GlyphRect glyphRect;

		// Token: 0x0400008F RID: 143
		public float scale;

		// Token: 0x04000090 RID: 144
		public int atlasIndex;

		// Token: 0x04000091 RID: 145
		public GlyphClassDefinitionType classDefinitionType;
	}
}
