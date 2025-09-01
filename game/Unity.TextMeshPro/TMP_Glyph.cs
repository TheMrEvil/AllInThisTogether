using System;

namespace TMPro
{
	// Token: 0x02000030 RID: 48
	[Serializable]
	public class TMP_Glyph : TMP_TextElement_Legacy
	{
		// Token: 0x06000200 RID: 512 RVA: 0x0001C40C File Offset: 0x0001A60C
		public static TMP_Glyph Clone(TMP_Glyph source)
		{
			return new TMP_Glyph
			{
				id = source.id,
				x = source.x,
				y = source.y,
				width = source.width,
				height = source.height,
				xOffset = source.xOffset,
				yOffset = source.yOffset,
				xAdvance = source.xAdvance,
				scale = source.scale
			};
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0001C48A File Offset: 0x0001A68A
		public TMP_Glyph()
		{
		}
	}
}
