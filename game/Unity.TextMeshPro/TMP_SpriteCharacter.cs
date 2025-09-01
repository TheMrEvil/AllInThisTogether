using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000054 RID: 84
	[Serializable]
	public class TMP_SpriteCharacter : TMP_TextElement
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0002671D File Offset: 0x0002491D
		// (set) Token: 0x060003BF RID: 959 RVA: 0x00026725 File Offset: 0x00024925
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				if (value == this.m_Name)
				{
					return;
				}
				this.m_Name = value;
				this.m_HashCode = TMP_TextParsingUtilities.GetHashCodeCaseSensitive(this.m_Name);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0002674E File Offset: 0x0002494E
		public int hashCode
		{
			get
			{
				return this.m_HashCode;
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00026756 File Offset: 0x00024956
		public TMP_SpriteCharacter()
		{
			this.m_ElementType = TextElementType.Sprite;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00026765 File Offset: 0x00024965
		public TMP_SpriteCharacter(uint unicode, TMP_SpriteGlyph glyph)
		{
			this.m_ElementType = TextElementType.Sprite;
			base.unicode = unicode;
			base.glyphIndex = glyph.index;
			base.glyph = glyph;
			base.scale = 1f;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00026799 File Offset: 0x00024999
		public TMP_SpriteCharacter(uint unicode, TMP_SpriteAsset spriteAsset, TMP_SpriteGlyph glyph)
		{
			this.m_ElementType = TextElementType.Sprite;
			base.unicode = unicode;
			base.textAsset = spriteAsset;
			base.glyph = glyph;
			base.glyphIndex = glyph.index;
			base.scale = 1f;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x000267D4 File Offset: 0x000249D4
		internal TMP_SpriteCharacter(uint unicode, uint glyphIndex)
		{
			this.m_ElementType = TextElementType.Sprite;
			base.unicode = unicode;
			base.textAsset = null;
			base.glyph = null;
			base.glyphIndex = glyphIndex;
			base.scale = 1f;
		}

		// Token: 0x040003A7 RID: 935
		[SerializeField]
		private string m_Name;

		// Token: 0x040003A8 RID: 936
		[SerializeField]
		private int m_HashCode;
	}
}
