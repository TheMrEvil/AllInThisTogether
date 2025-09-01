using System;
using UnityEngine.TextCore;

namespace TMPro
{
	// Token: 0x0200001F RID: 31
	[Serializable]
	public class TMP_Character : TMP_TextElement
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00017385 File Offset: 0x00015585
		public TMP_Character()
		{
			this.m_ElementType = TextElementType.Character;
			base.scale = 1f;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0001739F File Offset: 0x0001559F
		public TMP_Character(uint unicode, Glyph glyph)
		{
			this.m_ElementType = TextElementType.Character;
			base.unicode = unicode;
			base.textAsset = null;
			base.glyph = glyph;
			base.glyphIndex = glyph.index;
			base.scale = 1f;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000173DA File Offset: 0x000155DA
		public TMP_Character(uint unicode, TMP_FontAsset fontAsset, Glyph glyph)
		{
			this.m_ElementType = TextElementType.Character;
			base.unicode = unicode;
			base.textAsset = fontAsset;
			base.glyph = glyph;
			base.glyphIndex = glyph.index;
			base.scale = 1f;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00017415 File Offset: 0x00015615
		internal TMP_Character(uint unicode, uint glyphIndex)
		{
			this.m_ElementType = TextElementType.Character;
			base.unicode = unicode;
			base.textAsset = null;
			base.glyph = null;
			base.glyphIndex = glyphIndex;
			base.scale = 1f;
		}
	}
}
